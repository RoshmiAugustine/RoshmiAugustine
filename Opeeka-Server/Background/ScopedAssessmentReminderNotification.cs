using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using System.Text.Json;
using System.Data;
using Microsoft.Extensions.Configuration;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Domain.Interfaces.Services;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Infrastructure.Enums;
using System.Collections.Generic;
using System.Linq;

namespace Background
{
    internal interface IScopedAssessmentReminderNotification
    {
        Task DoWork(CancellationToken stoppingToken);
    }
    public class ScopedAssessmentReminderNotification : IScopedAssessmentReminderNotification
    {
        private readonly ILogger<AssessmentReminderNotification> _logger;
        private QueueClient _queueClient { get; set; }
        private static string QueuName = "assessmentremindernotification";
        private readonly IPersonQuestionnaireRepository personQuestionnaireRepository;
        private readonly IQuestionnaireWindowRepository questionnaireWindowRepository;
        private readonly IAssessmentReasonRepository assessmentReasonRepository;
        private readonly IQuestionnaireReminderRuleRespository questionnaireReminderRuleRespository;
        private readonly IQuestionnaireReminderTypeRepository questionnaireReminderTypeRepository;
        private readonly INotifyReminderRepository notifyReminderRepository;
        private readonly IPersonQuestionnaireScheduleRepository personQuestionnaireScheduleRepository;
        private readonly IPersonQuestionnaireScheduleService personQuestionnaireScheduleService;
        private readonly ICollaborationRepository collaborationRepository;
        private readonly IPersonCollaborationRepository personCollaborationRepository;
        private readonly IConfigurationRepository configurationRepository;

        public ScopedAssessmentReminderNotification(ILogger<AssessmentReminderNotification> logger, IConfiguration configuration, IPersonQuestionnaireRepository personQuestionnaireRepository,
            IQuestionnaireWindowRepository questionnaireWindowRepository, IAssessmentReasonRepository assessmentReasonRepository, IQuestionnaireReminderRuleRespository questionnaireReminderRuleRespository,
            IQuestionnaireReminderTypeRepository questionnaireReminderTypeRepository, INotifyReminderRepository notifyReminderRepository,
            IPersonQuestionnaireScheduleRepository personQuestionnaireScheduleRepository, IPersonQuestionnaireScheduleService personQuestionnaireScheduleService,
            ICollaborationRepository collaborationRepository, IPersonCollaborationRepository personCollaborationRepository, IConfigurationRepository configurationRepository)
        {
            _logger = logger;
            _queueClient = new QueueClient(configuration.GetValue<string>("queuestoragekey"), ScopedAssessmentReminderNotification.QueuName);
            this.personQuestionnaireRepository = personQuestionnaireRepository;
            this.questionnaireWindowRepository = questionnaireWindowRepository;
            this.assessmentReasonRepository = assessmentReasonRepository;
            this.questionnaireReminderRuleRespository = questionnaireReminderRuleRespository;
            this.questionnaireReminderTypeRepository = questionnaireReminderTypeRepository;
            this.notifyReminderRepository = notifyReminderRepository;
            this.personQuestionnaireScheduleRepository = personQuestionnaireScheduleRepository;
            this.personQuestionnaireScheduleService = personQuestionnaireScheduleService;
            this.collaborationRepository = collaborationRepository;
            this.personCollaborationRepository = personCollaborationRepository;
            this.configurationRepository = configurationRepository;
        }

        public async Task DoWork(CancellationToken stoppingToken)
        {
            // Dummy Code to get the Serialized JSON to insert into Queue.      
            var sampleModel = new SampleModel()
            {
                Id = 1,
                Field1 = "This is the field 1"
            };
            Console.WriteLine(JsonSerializer.Serialize(sampleModel));

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                try
                {
                    var triedCount = 0;
                    QueueMessage message = await RetrieveNextMessageAsync(_queueClient);
                    while (triedCount < 5)
                    {
                        if (await ProcessReminderNotificationAsync(message.MessageText))
                        {
                            triedCount = 5;
                            break;
                        }
                        triedCount++;
                    }
                    await DeQueueMessageAsync(_queueClient, message);
                }
                catch (System.Exception e)
                {
                    _logger.LogInformation("Problem reading queue, {message}", e.Message);
                }

                await Task.Delay(1, stoppingToken);
            }
        }

        static async Task<QueueMessage> RetrieveNextMessageAsync(QueueClient theQueue)
        {
            var queueExists = await theQueue.ExistsAsync();
            if (queueExists.Value)
            {
                QueueProperties properties = await theQueue.GetPropertiesAsync();

                if (properties.ApproximateMessagesCount > 0)
                {
                    QueueMessage[] retrievedMessage = await theQueue.ReceiveMessagesAsync(1);
                    return retrievedMessage[0];
                }
                else
                {
                    throw new Exception("No Items in the queue!");
                }
            }
            else
            {
                throw new PCISNoItemsInQueue("The queue does not exist!");
            }
        }

        static async Task<bool> DeQueueMessageAsync(QueueClient theQueue, QueueMessage message)
        {
            var response = await theQueue.DeleteMessageAsync(message.MessageId, message.PopReceipt);
            return response.Status.Equals(200);
        }

        private async Task<bool> ProcessReminderNotificationAsync(string messageBody)
        {
            bool processCompleted = false;
            try
            {
                var personQuestionnaireId = Convert.ToInt32(messageBody);
                var personQuestionnaire = await personQuestionnaireRepository.GetPersonQuestionnaireByID(personQuestionnaireId);
                if (personQuestionnaire != null && personQuestionnaire.CollaborationID > 0)
                {
                    var personCollaboration = personCollaborationRepository.GetAsync(x => x.PersonID == personQuestionnaire.PersonID && x.CollaborationID == personQuestionnaire.CollaborationID && !x.IsRemoved).Result.FirstOrDefault();
                    if (personCollaboration != null)
                    {
                        personQuestionnaire.EndDueDate = personCollaboration.EndDate;
                        personQuestionnaire.StartDate = personCollaboration.EnrollDate ?? DateTime.UtcNow;
                    }
                }
                var questionnaireId = personQuestionnaire.QuestionnaireID;
                var questionnaireWindowList = this.questionnaireWindowRepository.GetQuestionnaireWindowsByQuestionnaire(questionnaireId).Where(x => x.IsSelected).ToList(); ;
                var questionnaireReminderRuleList = this.questionnaireReminderRuleRespository.GetQuestionnaireReminderRulesByQuestionnaire(questionnaireId);

                foreach (var item in questionnaireWindowList)
                {
                    var assessmentReason = this.assessmentReasonRepository.GetRowAsync(x => x.AssessmentReasonID == item.AssessmentReasonID).Result;

                    if (assessmentReason.Name.ToLower() == PCISEnum.AssessmentReason.Initial.ToLower())
                    {
                        ProcessInitial(personQuestionnaire, item, questionnaireReminderRuleList);
                    }
                    else if (assessmentReason.Name.ToLower() == PCISEnum.AssessmentReason.Scheduled.ToLower())
                    {
                        ProcessRegularInterval(personQuestionnaire, item, questionnaireReminderRuleList);
                    }
                    else if (assessmentReason.Name.ToLower() == PCISEnum.AssessmentReason.Discharge.ToLower())
                    {
                        ProcessDischarge(personQuestionnaire, item, questionnaireReminderRuleList);
                    }
                }
                processCompleted = true;
            }
            catch (Exception)
            {
                processCompleted = false;
            }
            return processCompleted;
        }
        public void ProcessInitial(PersonQuestionnaireDTO personQuestionnaireDTO, QuestionnaireWindowsDTO questionnaireWindowDTO, List<QuestionnaireReminderRulesDTO> questionnaireReminderRuleList)
        {
            var windowOpenDay = questionnaireWindowDTO.IsSelected && questionnaireWindowDTO.WindowOpenOffsetDays > 0 ? personQuestionnaireDTO.StartDate.Date.AddDays(-questionnaireWindowDTO.WindowOpenOffsetDays) : personQuestionnaireDTO.StartDate;
            var windowDueDate = personQuestionnaireDTO.StartDate;
            var windowCloseDay = questionnaireWindowDTO.IsSelected && questionnaireWindowDTO.WindowCloseOffsetDays > 0 ? personQuestionnaireDTO.StartDate.AddDays((questionnaireWindowDTO.WindowCloseOffsetDays)) : personQuestionnaireDTO.StartDate;
            var personQuestionnaireScheduleObj = personQuestionnaireScheduleRepository.GetPersonQuestionnaireSchedule(personQuestionnaireDTO.PersonQuestionnaireID, questionnaireWindowDTO.QuestionnaireWindowID).Result;
            if (personQuestionnaireScheduleObj.Count > 0)
            {
                personQuestionnaireScheduleObj.ForEach(x => x.IsRemoved = true);
                personQuestionnaireScheduleService.UpdateBulkPersonQuestionnaireSchedule(personQuestionnaireScheduleObj);
            }
            PersonQuestionnaireSchedule personQuestionnaireSchedule = new PersonQuestionnaireSchedule
            {
                PersonQuestionnaireID = personQuestionnaireDTO.PersonQuestionnaireID,
                QuestionnaireWindowID = questionnaireWindowDTO.QuestionnaireWindowID,
                WindowDueDate = windowDueDate
            };
            var personQuestionnaireScheduleID = personQuestionnaireScheduleService.AddPersonQuestionnaireSchedule(personQuestionnaireSchedule);

            List<NotifyReminder> notifyReminderList = new List<NotifyReminder>();
            foreach (var item in questionnaireReminderRuleList)
            {
                //NotifyReminder notifyReminder = new NotifyReminder();
                var questionnaireReminderType = questionnaireReminderTypeRepository.GetQuestionnaireReminderType(item.QuestionnaireReminderTypeID).Result;
                if (item.IsSelected)
                {
                    var notifyReminderToAdd = CreateNotifyReminder(questionnaireReminderType.Name, personQuestionnaireScheduleID, windowOpenDay, windowDueDate, windowCloseDay, item);
                    foreach (var notifyReminder in notifyReminderToAdd)
                    {
                        if (notifyReminder.PersonQuestionnaireScheduleID > 0)
                        {
                            notifyReminderList.Add(notifyReminder);
                        }
                    }
                }
            }
            //notifyReminderRepository.AddBulkNotifyReminder(notifyReminderList);
        }

        public void ProcessRegularInterval(PersonQuestionnaireDTO personQuestionnaireDTO, QuestionnaireWindowsDTO questionnaireWindowDTO, List<QuestionnaireReminderRulesDTO> questionnaireReminderRuleList)
        {
            var daysToBeAddedToOpenDay = (questionnaireWindowDTO.RepeatIntervalDays ?? 0) - questionnaireWindowDTO.WindowOpenOffsetDays;
            var daysToBeAddedToCloseDay = (questionnaireWindowDTO.RepeatIntervalDays ?? 0) + questionnaireWindowDTO.WindowCloseOffsetDays;
            bool completed = false;
            var startDate = personQuestionnaireDTO.StartDate;
            var personQuestionnaireScheduleObj = personQuestionnaireScheduleRepository.GetPersonQuestionnaireSchedule(personQuestionnaireDTO.PersonQuestionnaireID, questionnaireWindowDTO.QuestionnaireWindowID).Result;
            if (personQuestionnaireScheduleObj.Count > 0)
            {
                personQuestionnaireScheduleObj.ForEach(x => x.IsRemoved = true);
                personQuestionnaireScheduleService.UpdateBulkPersonQuestionnaireSchedule(personQuestionnaireScheduleObj);
            }
            while (!completed)
            {
                var limitAfterWindowClose = Convert.ToInt32(this.configurationRepository.GetConfigurationByName(PCISEnum.ReminderNotification.Reminder_LimitInMonth_If_EndDate_Null, 0).Value);

                var windowOpenDay = questionnaireWindowDTO.IsSelected ? startDate.AddDays(daysToBeAddedToOpenDay) : startDate;
                var windowDueDate = startDate.AddDays(questionnaireWindowDTO.RepeatIntervalDays ?? 0);
                var windowCloseDay = questionnaireWindowDTO.IsSelected ? startDate.AddDays(daysToBeAddedToCloseDay) : startDate;

                PersonQuestionnaireSchedule personQuestionnaireSchedule = new PersonQuestionnaireSchedule
                {
                    PersonQuestionnaireID = personQuestionnaireDTO.PersonQuestionnaireID,
                    QuestionnaireWindowID = questionnaireWindowDTO.QuestionnaireWindowID,
                    WindowDueDate = windowDueDate
                };
                var personQuestionnaireScheduleID = personQuestionnaireScheduleRepository.AddPersonQuestionnaireSchedule(personQuestionnaireSchedule);
                List<NotifyReminder> notifyReminderList = new List<NotifyReminder>();
                foreach (var item in questionnaireReminderRuleList)
                {
                    //NotifyReminder notifyReminder = new NotifyReminder();
                    var questionnaireReminderType = questionnaireReminderTypeRepository.GetQuestionnaireReminderType(item.QuestionnaireReminderTypeID).Result;
                    if (item.IsSelected)
                    {
                        var notifyReminderToAdd = CreateNotifyReminder(questionnaireReminderType.Name, personQuestionnaireScheduleID, windowOpenDay, windowDueDate, windowCloseDay, item);
                        foreach (var notifyReminder in notifyReminderToAdd)
                        {
                            if (notifyReminder.PersonQuestionnaireScheduleID > 0)
                            {
                                notifyReminderList.Add(notifyReminder);
                            }
                        }
                    }
                }
               // notifyReminderRepository.AddBulkNotifyReminder(notifyReminderList);
                startDate = windowDueDate;
                completed = personQuestionnaireDTO.EndDueDate.HasValue ? (personQuestionnaireDTO.EndDueDate.Value < startDate.AddDays(questionnaireWindowDTO.RepeatIntervalDays ?? 0)) : (personQuestionnaireDTO.StartDate.AddMonths(limitAfterWindowClose) < startDate.AddDays(questionnaireWindowDTO.RepeatIntervalDays ?? 0));
            }
        }

        public void ProcessDischarge(PersonQuestionnaireDTO personQuestionnaireDTO, QuestionnaireWindowsDTO questionnaireWindowDTO, List<QuestionnaireReminderRulesDTO> questionnaireReminderRuleList)
        {
            var limitAfterWindowClose = Convert.ToInt32(this.configurationRepository.GetConfigurationByName(PCISEnum.ReminderNotification.Reminder_LimitInMonth_If_EndDate_Null, 0).Value);

            var windowOpenDay = personQuestionnaireDTO.EndDueDate.HasValue ? personQuestionnaireDTO.EndDueDate.Value.Date.AddDays(-questionnaireWindowDTO.WindowOpenOffsetDays) : personQuestionnaireDTO.StartDate.AddMonths(limitAfterWindowClose).AddDays(-questionnaireWindowDTO.WindowOpenOffsetDays);
            var windowDueDate = personQuestionnaireDTO.EndDueDate.HasValue ? personQuestionnaireDTO.EndDueDate.Value : personQuestionnaireDTO.StartDate.AddMonths(limitAfterWindowClose);
            var windowCloseDay = personQuestionnaireDTO.EndDueDate.HasValue ? personQuestionnaireDTO.EndDueDate.Value.AddDays((questionnaireWindowDTO.WindowCloseOffsetDays)) : personQuestionnaireDTO.StartDate.AddMonths(limitAfterWindowClose).AddDays((questionnaireWindowDTO.WindowCloseOffsetDays));
            var personQuestionnaireScheduleObj = personQuestionnaireScheduleRepository.GetPersonQuestionnaireSchedule(personQuestionnaireDTO.PersonQuestionnaireID, questionnaireWindowDTO.QuestionnaireWindowID).Result;
            if (personQuestionnaireScheduleObj.Count > 0)
            {
                personQuestionnaireScheduleObj.ForEach(x => x.IsRemoved = true);
                personQuestionnaireScheduleService.UpdateBulkPersonQuestionnaireSchedule(personQuestionnaireScheduleObj);
            }
            PersonQuestionnaireSchedule personQuestionnaireSchedule = new PersonQuestionnaireSchedule
            {
                PersonQuestionnaireID = personQuestionnaireDTO.PersonQuestionnaireID,
                QuestionnaireWindowID = questionnaireWindowDTO.QuestionnaireWindowID,
                WindowDueDate = windowDueDate
            };
            var personQuestionnaireScheduleID = personQuestionnaireScheduleRepository.AddPersonQuestionnaireSchedule(personQuestionnaireSchedule);
            List<NotifyReminder> notifyReminderList = new List<NotifyReminder>();
            foreach (var item in questionnaireReminderRuleList)
            {
                //NotifyReminder notifyReminder = new NotifyReminder();
                var questionnaireReminderType = questionnaireReminderTypeRepository.GetQuestionnaireReminderType(item.QuestionnaireReminderTypeID).Result;
                if (item.IsSelected)
                {
                    var notifyReminderToAdd = CreateNotifyReminder(questionnaireReminderType.Name, personQuestionnaireScheduleID, windowOpenDay, windowDueDate, windowCloseDay, item);
                    foreach (var notifyReminder in notifyReminderToAdd)
                    {
                        if (notifyReminder.PersonQuestionnaireScheduleID > 0)
                        {
                            notifyReminderList.Add(notifyReminder);
                        }
                    }
                }
            }
            //notifyReminderRepository.AddBulkNotifyReminder(notifyReminderList);
        }

        public List<NotifyReminder> CreateNotifyReminder(string reminderTypeName, long personQuestionnaireScheduleID, DateTime windowOpenDay, DateTime windowDueDate, DateTime windowCloseDay, QuestionnaireReminderRulesDTO questionnaireReminderRulesDTO)
        {
            var questionnaireLateCountLimit = Convert.ToInt32(this.configurationRepository.GetConfigurationByName(PCISEnum.ReminderNotification.Reminder_Count_After_WindowCloseDay, 0).Value);
            List<NotifyReminder> notifyReminderList = new List<NotifyReminder>();
            NotifyReminder notifyReminder = new NotifyReminder();
            switch (reminderTypeName)
            {
                case PCISEnum.QuestionnaireReminderType.WindowOpen:
                    notifyReminder = new NotifyReminder
                    {
                        PersonQuestionnaireScheduleID = personQuestionnaireScheduleID,
                        QuestionnaireReminderRuleID = questionnaireReminderRulesDTO.QuestionnaireReminderRuleID,
                        NotifyDate = windowOpenDay.Date.AddDays(-questionnaireReminderRulesDTO.ReminderOffsetDays ?? default(int))
                    };
                    notifyReminderList.Add(notifyReminder);
                    break;
                case PCISEnum.QuestionnaireReminderType.AssesmentDue:
                    notifyReminder = new NotifyReminder
                    {
                        PersonQuestionnaireScheduleID = personQuestionnaireScheduleID,
                        QuestionnaireReminderRuleID = questionnaireReminderRulesDTO.QuestionnaireReminderRuleID,
                        NotifyDate = windowDueDate
                    };
                    notifyReminderList.Add(notifyReminder);
                    break;
                case PCISEnum.QuestionnaireReminderType.WindowClose:
                    notifyReminder = new NotifyReminder
                    {
                        PersonQuestionnaireScheduleID = personQuestionnaireScheduleID,
                        QuestionnaireReminderRuleID = questionnaireReminderRulesDTO.QuestionnaireReminderRuleID,
                        NotifyDate = windowCloseDay
                    };
                    notifyReminderList.Add(notifyReminder);
                    break;
                case PCISEnum.QuestionnaireReminderType.QuestionnaireLate:
                    var closeDay = windowCloseDay.AddDays(questionnaireReminderRulesDTO.ReminderOffsetDays ?? default(int));
                    for (var i = 0; i < questionnaireLateCountLimit; i++)
                    {
                        notifyReminder = new NotifyReminder
                        {
                            PersonQuestionnaireScheduleID = personQuestionnaireScheduleID,
                            QuestionnaireReminderRuleID = questionnaireReminderRulesDTO.QuestionnaireReminderRuleID,
                            NotifyDate = closeDay
                        };
                        closeDay = closeDay.AddDays(questionnaireReminderRulesDTO.ReminderOffsetDays ?? default(int));
                        notifyReminderList.Add(notifyReminder);
                    }
                    break;
                default:
                    break;
            }
            return notifyReminderList;
        }
    }
}

