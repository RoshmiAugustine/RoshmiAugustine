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
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Infrastructure.Enums;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Background
{
    internal interface IScopedAssessmentRiskNotifications
    {
        Task DoWork(CancellationToken stoppingToken);
    }
    public class ScopedAssessmentRiskNotifications : IScopedAssessmentRiskNotifications
    {
        private readonly ILogger<AssessmentRiskNotifications> _logger;
        private QueueClient _queueClient { get; set; }
        private static string QueuName = "assessmentrisknotification";
        private readonly IResponseRepository responseRepository;
        private readonly IAssessmentRepository assessmentRepository;
        private readonly IPersonQuestionnaireRepository personQuestionnaireRepository;
        private readonly IAssessmentResponseRepository assessmentResponseRepository;
        private readonly IQuestionnaireNotifyRiskRuleConditionRepository questionnaireNotifyRiskRuleConditionRepository;
        private readonly INotifiationResolutionStatusRepository notifiationResolutionStatusRepository;
        private readonly INotificationTypeRepository notificationTypeRepository;
        private readonly INotificationLogRepository notificationLogRepository;
        private readonly INotifyRiskRepository notifyRiskRepository;
        private readonly IAssessmentStatusRepository assessmentStatusRepository;
        private readonly IQuestionnaireNotifyRiskScheduleRepository questionnaireNotifyRiskScheduleRepository;
        private readonly IQuestionnaireNotifyRiskRuleRepository questionnaireNotifyRiskRuleRepository;
        private readonly INotifyRiskValueRepository notifyRiskValueRepository;
        private readonly IPersonHelperRepository personHelperRepository;
        private readonly IHelperRepository helperRepository;
        private readonly IPersonRepository personRepository;
        private readonly IEmailDetailRepository emailDetailRepository;
        private readonly IConfigurationRepository configurationRepository;
        private readonly INotificationLevelRepository notificationLevelRepository;

        public ScopedAssessmentRiskNotifications(ILogger<AssessmentRiskNotifications> logger, IConfiguration configuration, IResponseRepository responseRepository,
            IAssessmentRepository assessmentRepository, IPersonQuestionnaireRepository personQuestionnaireRepository, IAssessmentResponseRepository assessmentResponseRepository,
            IQuestionnaireNotifyRiskRuleConditionRepository questionnaireNotifyRiskRuleConditionRepository, INotifiationResolutionStatusRepository notifiationResolutionStatusRepository,
            INotificationTypeRepository notificationTypeRepository, INotifyRiskRepository notifyRiskRepository, INotificationLogRepository notificationLogRepository,
            IAssessmentStatusRepository assessmentStatusRepository, IQuestionnaireNotifyRiskScheduleRepository questionnaireNotifyRiskScheduleRepository,
            IQuestionnaireNotifyRiskRuleRepository questionnaireNotifyRiskRuleRepository, INotifyRiskValueRepository notifyRiskValueRepository, IPersonHelperRepository personHelperRepository, IHelperRepository helperRepository, IPersonRepository personRepository, IEmailDetailRepository emailDetailRepository, IConfigurationRepository configurationRepository, INotificationLevelRepository notificationLevelRepository)
        {
            _logger = logger;
            _queueClient = new QueueClient(configuration.GetValue<string>("queuestoragekey"), ScopedAssessmentRiskNotifications.QueuName);
            this.responseRepository = responseRepository;
            this.assessmentRepository = assessmentRepository;
            this.personQuestionnaireRepository = personQuestionnaireRepository;
            this.assessmentResponseRepository = assessmentResponseRepository;
            this.questionnaireNotifyRiskRuleConditionRepository = questionnaireNotifyRiskRuleConditionRepository;
            this.notifiationResolutionStatusRepository = notifiationResolutionStatusRepository;
            this.notificationTypeRepository = notificationTypeRepository;
            this.notifyRiskRepository = notifyRiskRepository;
            this.notificationLogRepository = notificationLogRepository;
            this.assessmentStatusRepository = assessmentStatusRepository;
            this.questionnaireNotifyRiskScheduleRepository = questionnaireNotifyRiskScheduleRepository;
            this.questionnaireNotifyRiskRuleRepository = questionnaireNotifyRiskRuleRepository;
            this.notifyRiskValueRepository = notifyRiskValueRepository;
            this.personHelperRepository = personHelperRepository;
            this.helperRepository = helperRepository;
            this.personRepository = personRepository;
            this.emailDetailRepository = emailDetailRepository;
            this.configurationRepository = configurationRepository;
            this.notificationLevelRepository = notificationLevelRepository;
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
                        if (await ProcessRiskNotificationAsync(message.MessageText))
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

        private async Task<bool> ProcessRiskNotificationAsync(string messageBody)
        {
            bool processCompleted = false;
            try
            {
                var assessmentId = Convert.ToInt32(messageBody);
                var assessment = assessmentRepository.GetAssessment(assessmentId).Result;
                var assessmentStatusID = this.assessmentStatusRepository.GetAssessmentStatus(PCISEnum.AssessmentStatus.Approved).Result.AssessmentStatusID;

                var latestSubmittedAssessment = assessmentRepository.GetAssessmentByPersonQuestionaireID(assessment.PersonQuestionnaireID, assessmentStatusID);
                int notificationResolutionStatusID = 0;
                if (latestSubmittedAssessment != null && latestSubmittedAssessment.AssessmentID == assessment.AssessmentID)
                {
                    notificationResolutionStatusID = this.notifiationResolutionStatusRepository.GetNotificationStatus(PCISEnum.NotificationStatus.Unresolved).NotificationResolutionStatusID;
                }
                else
                {
                    notificationResolutionStatusID = this.notifiationResolutionStatusRepository.GetNotificationStatus(PCISEnum.NotificationStatus.Resolved).NotificationResolutionStatusID;
                }
                bool alreadyNotified = notifyRiskRepository.GetNotifyRiskCount(assessmentId) == 0 ? false : true;
                if (!alreadyNotified)
                {
                    var personQuestionnaire = await personQuestionnaireRepository.GetPersonQuestionnaireByID(assessment.PersonQuestionnaireID);
                      
                    var assessmentResponses = assessmentResponseRepository.GetAssessmentResponse(assessment.AssessmentID).Result;
                    QuestionnaireNotifyRiskSchedulesDTO questionnaireNotifyRiskSchedulesDTO = this.questionnaireNotifyRiskScheduleRepository.GetQuestionnaireNotifyRiskScheduleByQuestionnaire(personQuestionnaire.QuestionnaireID).Result;
                    List<QuestionnaireNotifyRiskRulesDTO> questionnaireNotifyRiskRulesDTO = this.questionnaireNotifyRiskRuleRepository.GetQuestionnaireNotifyRiskRuleBySchedule(questionnaireNotifyRiskSchedulesDTO.QuestionnaireNotifyRiskScheduleID);
                    List<NotificationLog> notificationLogList = new List<NotificationLog>();
                    List<EmailDetail> emailDetailList = new List<EmailDetail>();
                    
                    
                    var personHelpers = personRepository.getPersonsAndHelpersByPersonIDListForAlert(personQuestionnaire.PersonID);
                    
                    var PCISBaseURL = this.configurationRepository.GetConfigurationByName(PCISEnum.ConfigurationParameters.PCIS_BaseURL, 0).Value;

                    var PeopleQuestionnaireURL = this.configurationRepository.GetConfigurationByName(PCISEnum.ConfigurationParameters.PeopleQuestionnaireURL, 0).Value;

                    foreach (var questionnaireNotifyRiskRule in questionnaireNotifyRiskRulesDTO)
                    {
                        var notificationLevel = notificationLevelRepository.GetNotificationLevel(questionnaireNotifyRiskRule.NotificationLevelID).Result;
                        var questionnaireNotifyRiskRuleConditionList = this.questionnaireNotifyRiskRuleConditionRepository.GetQuestionnaireNotifyRiskRuleConditionByRuleID(questionnaireNotifyRiskRule.QuestionnaireNotifyRiskRuleID);
                        bool? notify = null;
                        List<NotifyRiskValue> notifyRiskValueList = new List<NotifyRiskValue>();
                        foreach (var item in questionnaireNotifyRiskRuleConditionList)
                        {
                            NotifyRiskValue notifyRiskValue = new NotifyRiskValue();
                            var assessmentResponse = assessmentResponses.Where(x => x.QuestionnaireItemID == item.QuestionnaireItemID).FirstOrDefault();
                            var responseDto = responseRepository.GetResponse(assessmentResponse.ResponseID).Result;
                            switch (item.ComparisonOperator)
                            {
                                case PCISEnum.ComparisonOperator.Equal:
                                    if (item.JoiningOperator == null || (item.JoiningOperator != null ? item.JoiningOperator.ToLower() == "and" : true))
                                    {
                                        notify = (notify ?? true) && responseDto.Value == item.ComparisonValue;
                                    }
                                    else
                                    {
                                        notify = (notify ?? false) || responseDto.Value == item.ComparisonValue;
                                    }
                                    break;
                                case PCISEnum.ComparisonOperator.EqualEqual:
                                    if (item.JoiningOperator == null || (item.JoiningOperator != null ? item.JoiningOperator.ToLower() == "and" : true))
                                    {
                                        notify = (notify ?? true) && responseDto.Value == item.ComparisonValue;
                                    }
                                    else
                                    {
                                        notify = (notify ?? false) || responseDto.Value == item.ComparisonValue;
                                    }
                                    break;
                                case PCISEnum.ComparisonOperator.GreaterThan:
                                    if (item.JoiningOperator == null || (item.JoiningOperator != null ? item.JoiningOperator.ToLower() == "and" : true))
                                    {
                                        notify = (notify ?? true) && responseDto.Value > item.ComparisonValue;
                                    }
                                    else
                                    {
                                        notify = (notify ?? false) || responseDto.Value > item.ComparisonValue;
                                    }
                                    break;
                                case PCISEnum.ComparisonOperator.GreaterThanOrEqual:
                                    if (item.JoiningOperator == null || (item.JoiningOperator != null ? item.JoiningOperator.ToLower() == "and" : true))
                                    {
                                        notify = (notify ?? true) && responseDto.Value >= item.ComparisonValue;
                                    }
                                    else
                                    {
                                        notify = (notify ?? false) || responseDto.Value >= item.ComparisonValue;
                                    }
                                    break;
                                case PCISEnum.ComparisonOperator.LessThan:
                                    if (item.JoiningOperator == null || (item.JoiningOperator != null ? item.JoiningOperator.ToLower() == "and" : true))
                                    {
                                        notify = (notify ?? true) && responseDto.Value < item.ComparisonValue;
                                    }
                                    else
                                    {
                                        notify = (notify ?? false) || responseDto.Value < item.ComparisonValue;
                                    }
                                    break;
                                case PCISEnum.ComparisonOperator.LessThanOrEqual:
                                    if (item.JoiningOperator == null || (item.JoiningOperator != null ? item.JoiningOperator.ToLower() == "and" : true))
                                    {
                                        notify = (notify ?? true) && responseDto.Value <= item.ComparisonValue;
                                    }
                                    else
                                    {
                                        notify = (notify ?? false) || responseDto.Value <= item.ComparisonValue;
                                    }
                                    break;
                                case PCISEnum.ComparisonOperator.NotEqual:
                                    if (item.JoiningOperator == null || (item.JoiningOperator != null ? item.JoiningOperator.ToLower() == "and" : true))
                                    {
                                        notify = (notify ?? true) && responseDto.Value != item.ComparisonValue;
                                    }
                                    else
                                    {
                                        notify = (notify ?? false) || responseDto.Value != item.ComparisonValue;
                                    }
                                    break;
                                default:
                                    notify = null;
                                    break;
                            }
                            if (notify == true)
                            {
                                notifyRiskValue = new NotifyRiskValue()
                                {
                                    AssessmentResponseID = assessmentResponse.AssessmentResponseID,
                                    AssessmentResponseValue = responseDto.Value,
                                    QuestionnaireNotifyRiskRuleConditionID = item.QuestionnaireNotifyRiskRuleConditionID
                                };
                                notifyRiskValueList.Add(notifyRiskValue);
                            }
                        }
                        if (notify == true)
                        {
                            var notificationType = notificationTypeRepository.GetNotificationType(PCISEnum.NotificationType.Alert).Result;
                            AssessmentNotificationRiskDTO assessmentNotificationRisk = new AssessmentNotificationRiskDTO();
                            assessmentNotificationRisk.AssessmentID = assessment.AssessmentID;
                            assessmentNotificationRisk.PersonID = personQuestionnaire.PersonID;
                            assessmentNotificationRisk.UpdateUserID = 1;
                            assessmentNotificationRisk.NotificationResolutionStatusID = notificationResolutionStatusID;
                            assessmentNotificationRisk.QuestionnaireNotifyRiskRuleID = questionnaireNotifyRiskRule.QuestionnaireNotifyRiskRuleID;
                            assessmentNotificationRisk.NotificationTypeID = notificationType.NotificationTypeID;
                            assessmentNotificationRisk.NotifyRiskValueList = notifyRiskValueList;
                            assessmentNotificationRisk.QuestionnaireID = personQuestionnaire.QuestionnaireID;
                            assessmentNotificationRisk.Detail = personQuestionnaire.QuestionnaireID + " - " + questionnaireNotifyRiskRule.Name + " - " + notificationLevel.Name;
                            var notificationLog = HandleRiskNotification(assessmentNotificationRisk);
                            notificationLogList.Add(notificationLog);  
                           
                            foreach (var item in personHelpers)
                            {
                                EmailDetail emailDetail = new EmailDetail
                                {
                                    Email = item.HelperEmail,
                                    AgencyID = item.AgencyID,
                                    EmailAttributes = "PersonInitial = " + item.PersonInitials + " | DisplayName = " + item.HelperFirstName+ " "+item.HelperMiddleName+ " "+ item.HelperLastName,
                                    HelperID = item.HelperID,
                                    PersonID = personQuestionnaire.PersonID,
                                    Status = "Pending",
                                    UpdateDate = DateTime.UtcNow,
                                    UpdateUserID = 1,
                                    CreatedDate = DateTime.UtcNow,
                                    Type = PCISEnum.NotificationType.Alert,
                                    URL = PCISBaseURL + new StringBuilder(PeopleQuestionnaireURL).Replace("{assessmentid}", assessmentId.ToString()).Replace("{personindex}", item.PersonIndex.ToString()).Replace("{notificationtype}", PCISEnum.NotificationType.Alert).Replace("{questionnaireid}", personQuestionnaire.QuestionnaireID.ToString()).ToString(),
                                    FKeyValue = notificationLog.FKeyValue
                                };
                                emailDetailList.Add(emailDetail);
                            }
                        }                       
                    }
                    notificationLogRepository.AddBulkNotificationLog(notificationLogList);
                    emailDetailRepository.AddEmailDetails(emailDetailList);
                }
                processCompleted = true;
            }
            catch (Exception)
            {
                processCompleted = false;
            }
            return processCompleted;
        }
        public NotificationLog HandleRiskNotification(AssessmentNotificationRiskDTO assessmentNotificationRiskDTO)
        {

            //NotifyRisk
            NotifyRisk notifyRisk = new NotifyRisk
            {
                QuestionnaireNotifyRiskRuleID = assessmentNotificationRiskDTO.QuestionnaireNotifyRiskRuleID,
                PersonID = assessmentNotificationRiskDTO.PersonID,
                AssessmentID = assessmentNotificationRiskDTO.AssessmentID,
                NotifyDate = DateTime.UtcNow,
                UpdateDate = DateTime.UtcNow,
                UpdateUserID = assessmentNotificationRiskDTO.UpdateUserID
            };
            notifyRisk = notifyRiskRepository.AddNotifyRisk(notifyRisk);

            //NotificationLog
            var notificationLog = new NotificationLog
            {
                NotificationDate = DateTime.UtcNow,
                PersonID = assessmentNotificationRiskDTO.PersonID,
                NotificationTypeID = assessmentNotificationRiskDTO.NotificationTypeID,
                NotificationResolutionStatusID = assessmentNotificationRiskDTO.NotificationResolutionStatusID,
                UpdateDate = DateTime.UtcNow,
                UpdateUserID = assessmentNotificationRiskDTO.UpdateUserID,
                FKeyValue = notifyRisk.NotifyRiskID,
                QuestionnaireID = assessmentNotificationRiskDTO.QuestionnaireID,
                AssessmentID = assessmentNotificationRiskDTO.AssessmentID,
                Details = assessmentNotificationRiskDTO.Detail,
                IsRemoved = false
            };
            if (assessmentNotificationRiskDTO.NotifyRiskValueList != null)
            {
                assessmentNotificationRiskDTO.NotifyRiskValueList.ForEach(x => x.NotifyRiskID = notifyRisk.NotifyRiskID);
                notifyRiskValueRepository.AddBulkAsync(assessmentNotificationRiskDTO.NotifyRiskValueList);
            }
            return notificationLog;
        }
    }
}

