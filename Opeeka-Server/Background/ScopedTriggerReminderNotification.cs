using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Azure.Storage.Queues;
using Microsoft.Extensions.Configuration;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Domain.Interfaces.Services;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Infrastructure.Enums;
using System.Collections.Generic;
using System.Linq;

namespace Background
{
    internal interface IScopedTriggerReminderNotification
    {
        Task DoWork(CancellationToken stoppingToken);
    }
    public class ScopedTriggerReminderNotification : IScopedTriggerReminderNotification
    {
        private readonly ILogger<ScopedTriggerReminderNotification> _logger;
        private QueueClient _queueClient { get; set; }
        private readonly IPersonQuestionnaireRepository personQuestionnaireRepository;
        private readonly IQuestionnaireWindowRepository questionnaireWindowRepository;
        private readonly IAssessmentReasonRepository assessmentReasonRepository;
        private readonly IQuestionnaireReminderRuleRespository questionnaireReminderRuleRespository;
        private readonly IQuestionnaireReminderTypeRepository questionnaireReminderTypeRepository;
        private readonly INotifyReminderRepository notifyReminderRepository;
        private readonly IPersonQuestionnaireScheduleRepository personQuestionnaireScheduleRepository;
        private readonly IPersonQuestionnaireScheduleService personQuestionnaireScheduleService;
        private readonly ICollaborationRepository collaborationRepository;
        private readonly IBackgroundProcessLogRepository backgroundProcessLogRepository;
        private readonly INotificationTypeRepository notificationTypeRepository;
        private readonly INotifiationResolutionStatusRepository notifiationResolutionStatusRepository;
        private readonly INotificationLogRepository notificationLogRepository;
        private readonly IPersonRepository personRepository;
        private readonly IConfigurationRepository configurationRepository;
        private readonly IEmailDetailRepository emailDetailRepository;

        public ScopedTriggerReminderNotification(ILogger<ScopedTriggerReminderNotification> logger, IConfiguration configuration, IPersonQuestionnaireRepository personQuestionnaireRepository,
            IQuestionnaireWindowRepository questionnaireWindowRepository, IAssessmentReasonRepository assessmentReasonRepository, IQuestionnaireReminderRuleRespository questionnaireReminderRuleRespository,
            IQuestionnaireReminderTypeRepository questionnaireReminderTypeRepository, INotifyReminderRepository notifyReminderRepository,
            IPersonQuestionnaireScheduleRepository personQuestionnaireScheduleRepository, IPersonQuestionnaireScheduleService personQuestionnaireScheduleService,
            ICollaborationRepository collaborationRepository, IBackgroundProcessLogRepository backgroundProcessLogRepository, INotificationTypeRepository notificationTypeRepository,
            INotifiationResolutionStatusRepository notifiationResolutionStatusRepository, INotificationLogRepository notificationLogRepository, IPersonRepository personRepository, IConfigurationRepository configurationRepository, IEmailDetailRepository emailDetailRepository)
        {
            _logger = logger;
            this.personQuestionnaireRepository = personQuestionnaireRepository;
            this.questionnaireWindowRepository = questionnaireWindowRepository;
            this.assessmentReasonRepository = assessmentReasonRepository;
            this.questionnaireReminderRuleRespository = questionnaireReminderRuleRespository;
            this.questionnaireReminderTypeRepository = questionnaireReminderTypeRepository;
            this.notifyReminderRepository = notifyReminderRepository;
            this.personQuestionnaireScheduleRepository = personQuestionnaireScheduleRepository;
            this.personQuestionnaireScheduleService = personQuestionnaireScheduleService;
            this.collaborationRepository = collaborationRepository;
            this.backgroundProcessLogRepository = backgroundProcessLogRepository;
            this.notificationTypeRepository = notificationTypeRepository;
            this.notifiationResolutionStatusRepository = notifiationResolutionStatusRepository;
            this.notificationLogRepository = notificationLogRepository;
            this.personRepository = personRepository;
            this.configurationRepository = configurationRepository;
            this.emailDetailRepository = emailDetailRepository;
        }

        public async Task DoWork(CancellationToken stoppingToken)
        {
            var processLog = backgroundProcessLogRepository.GetBackgroundProcessLog("TriggerReminderNotification");
            await ProcessTriggerReminderNotificationAsync();
            if (processLog == null)
            {
                processLog = new BackgroundProcessLog()
                {
                    ProcessName = "TriggerReminderNotification",
                    LastProcessedDate = DateTime.UtcNow.Date.AddDays(-1)
                };
                backgroundProcessLogRepository.AddBackgroundProcessLog(processLog);
            }
            else
            {
                processLog.LastProcessedDate = DateTime.UtcNow.Date.AddDays(-1);
                var result = backgroundProcessLogRepository.UpdateBackgroundProcessLog(processLog);
            }
        }

        private async Task<bool> ProcessTriggerReminderNotificationAsync()
        {
            bool processCompleted = false;
            try
            {
                var notificationType = notificationTypeRepository.GetNotificationType(PCISEnum.NotificationType.Reminder).Result;
                var notificationResolutionStatusID = this.notifiationResolutionStatusRepository.GetNotificationStatus(PCISEnum.NotificationStatus.Unresolved).NotificationResolutionStatusID;
                var notifyRemindersScheduled = personQuestionnaireScheduleRepository.GetNotifyReminderScheduledForToday();
                if (notifyRemindersScheduled.Count == 0)
                {
                    processCompleted = true;
                    return processCompleted;
                }
                List<int> notifyReminderIds = new List<int>();
                List<NotificationLog> notificationLogList = new List<NotificationLog>();
                foreach (var item in notifyRemindersScheduled)
                {
                    notifyReminderIds.Add(item.NotifyReminderID);
                    var notificationLog = new NotificationLog
                    {
                        NotificationDate = DateTime.UtcNow,
                        PersonID = item.PersonID,
                        NotificationTypeID = notificationType.NotificationTypeID,
                        NotificationResolutionStatusID = notificationResolutionStatusID,
                        UpdateDate = DateTime.UtcNow,
                        UpdateUserID = 1,
                        FKeyValue = item.NotifyReminderID,
                        IsRemoved = false,
                        AssessmentID = 0,
                        QuestionnaireID = Convert.ToInt32(item.QuestionnaireID),
                        Details = item.QuestionnaireID + " - " + item.InstrumentAbbrev + " - " + item.QuestionnaireName + " - " + item.ReminderScheduleName +" - " +item.AssessmentReasonName
                    };
                    notificationLogList.Add(notificationLog);
                }
                notificationLogRepository.AddBulkNotificationLog(notificationLogList);
                InsertDataToEmailDetail(notifyRemindersScheduled);
                var notifyReminders = notifyReminderRepository.GetAsync(x => notifyReminderIds.Contains(x.NotifyReminderID)).Result.ToList();
                notifyReminders.ForEach(x => x.IsLogAdded = true);
                notifyReminderRepository.UpdateBulkNotifyReminder(notifyReminders);
                processCompleted = true;
            }
            catch (Exception)
            {
                processCompleted = false;
            }
            return processCompleted;
        }

        private void InsertDataToEmailDetail(List<Opeeka.PICS.Domain.DTO.ReminderNotificationsListDTO> notifyRemindersScheduled)
        {
            try
            {
                List<EmailDetail> list_emailDetails = new List<EmailDetail>();
                var lstpersonID = notifyRemindersScheduled.Select(x => x.PersonID).ToList();
                var allPersonshelpers = this.personRepository.getPersonsAndHelpersByPersonIDList(lstpersonID);
                var lstPersonQuestionSchedule = notifyRemindersScheduled.Select(x => x.PersonQuestionnaireScheduleID).ToList();
                var allpersonQuestions = this.personRepository.getDetailsByPersonQuestionScheduleList(lstPersonQuestionSchedule);
                var PCISBaseURL = this.configurationRepository.GetConfigurationByName(PCISEnum.ConfigurationParameters.PCIS_BaseURL, 0).Value;
                var PeopleQuestionnaireURL = this.configurationRepository.GetConfigurationByName(PCISEnum.ConfigurationParameters.PeopleQuestionnaireURL, 0).Value;
                var urllink = string.Format("{0}{1}", PCISBaseURL, PeopleQuestionnaireURL);
                
                foreach (var item in notifyRemindersScheduled)
                {
                    var personhelpers = allPersonshelpers.Where(x => x.PersonID == item.PersonID).ToList();
                    var questionnaireID = allpersonQuestions.Where(x => x.PersonQuestionnaireScheduleID == item.PersonQuestionnaireScheduleID).Select(x=>x.QuestionnaireID).FirstOrDefault(); 
                    var windowDetails = allpersonQuestions.Where(x => x.PersonQuestionnaireScheduleID == item.PersonQuestionnaireScheduleID).FirstOrDefault();
                    foreach (var personHelper in personhelpers)
                    {
                        EmailDetail emailDetail = new EmailDetail
                        {
                            Email = personHelper.HelperEmail,
                            AgencyID = personHelper.AgencyID,
                            EmailAttributes = string.Format("PersonInitial={0}|DisplayName={1}{2}{3}|DueDate={4}", personHelper.PersonInitials, personHelper.HelperFirstName, personHelper.HelperMiddleName, personHelper.HelperLastName, windowDetails.WindowDueDate.ToString("MMM dd, yyyy")),
                            HelperID = personHelper.HelperID,
                            PersonID = personHelper.PersonID,
                            Status = "Pending",
                            UpdateDate = DateTime.UtcNow,
                            UpdateUserID = 1,
                            CreatedDate = DateTime.UtcNow,
                            Type = windowDetails.NotificationType,
                            URL = urllink.Replace(PCISEnum.EmailDetail.AssessmentID,"0").Replace(PCISEnum.EmailDetail.PersonIndex, personHelper.PersonIndex.ToString()).Replace(PCISEnum.EmailDetail.NotificationType, PCISEnum.NotificationType.Reminder).Replace(PCISEnum.EmailDetail.QuestionnaireId, questionnaireID.ToString()).ToString(),
                            FKeyValue = item.NotifyReminderID
                        };
                        list_emailDetails.Add(emailDetail);
                    }
                }
                emailDetailRepository.AddEmailDetails(list_emailDetails);
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}

