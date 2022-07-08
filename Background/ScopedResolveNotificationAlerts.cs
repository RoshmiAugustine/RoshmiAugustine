using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using System.Data;
using Microsoft.Extensions.Configuration;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Infrastructure.Enums;
using System.Linq;
using System.Collections.Generic;
namespace Background
{
    internal interface IScopedResolveNotificationAlerts
    {
        Task DoWork(CancellationToken stoppingToken);
    }
    public class ScopedResolveNotificationAlerts : IScopedResolveNotificationAlerts
    {
        private readonly ILogger<AssessmentRiskNotifications> _logger;
        private QueueClient _queueClient { get; set; }
        private static string QueuName = "resolvenotificationalerts";
        private readonly IAssessmentRepository assessmentRepository;
        private readonly IResponseRepository responseRepository;
        private readonly IPersonQuestionnaireRepository personQuestionnaireRepository;
        private readonly IAssessmentResponseRepository assessmentResponseRepository;
        private readonly IQuestionnaireNotifyRiskRuleConditionRepository questionnaireNotifyRiskRuleConditionRepository;
        private readonly INotifiationResolutionStatusRepository notifiationResolutionStatusRepository;
        private readonly INotificationLogRepository notificationLogRepository;

        public ScopedResolveNotificationAlerts(ILogger<AssessmentRiskNotifications> logger, IConfiguration configuration, IResponseRepository responseRepository, IAssessmentRepository assessmentRepository, IPersonQuestionnaireRepository personQuestionnaireRepository, IAssessmentResponseRepository assessmentResponseRepository, IQuestionnaireNotifyRiskRuleConditionRepository questionnaireNotifyRiskRuleConditionRepository, INotifiationResolutionStatusRepository notifiationResolutionStatusRepository, INotificationLogRepository notificationLogRepository)
        {
            _logger = logger;
            _queueClient = new QueueClient(configuration.GetValue<string>("queuestoragekey"), ScopedResolveNotificationAlerts.QueuName);
            this.assessmentRepository = assessmentRepository;
            this.responseRepository = responseRepository;
            this.personQuestionnaireRepository = personQuestionnaireRepository;
            this.assessmentResponseRepository = assessmentResponseRepository;
            this.questionnaireNotifyRiskRuleConditionRepository = questionnaireNotifyRiskRuleConditionRepository;
            this.notifiationResolutionStatusRepository = notifiationResolutionStatusRepository;
            this.notificationLogRepository = notificationLogRepository;
        }

        public async Task DoWork(CancellationToken stoppingToken)
        {

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                try
                {
                    var triedCount = 0;
                    QueueMessage message = await RetrieveNextMessageAsync(_queueClient);
                    while (triedCount < 5)
                    {
                        if (await ProcessResolveNotificationAlert(message.MessageText))
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

        /// <summary>
        /// Process and Resolve Notification Alert
        /// </summary>
        /// <param name="messageBody"></param>
        /// <returns>Task<bool></returns>
        private async Task<bool> ProcessResolveNotificationAlert(string messageBody)
        {
            bool processCompleted = false;
            try
            {
                var assessmentId = Convert.ToInt32(messageBody);
                var assessment = assessmentRepository.GetAssessment(assessmentId).Result;

                if (assessment != null)
                {
                    //get persion id
                    var personQuestionnaire = personQuestionnaireRepository.GetPersonQuestionnaireByID(assessment.PersonQuestionnaireID).Result;

                    //get removed notification log details
                    var removedNotificationLogs = notificationLogRepository.GetNotificationLogForNotificationResolveAlert(personQuestionnaire.PersonID);
                    int notificationResolutionStatusID = this.notifiationResolutionStatusRepository.GetNotificationStatus(PCISEnum.NotificationStatus.Resolved).NotificationResolutionStatusID;
                    if (removedNotificationLogs.Count > 0)
                    {
                        foreach (var notificationLog in removedNotificationLogs)
                        {
                            notificationLog.NotificationResolutionStatusID = notificationResolutionStatusID;
                            notificationLog.UpdateDate = DateTime.UtcNow;
                        }
                        //update removed log status
                        notificationLogRepository.UpdateNotificationLog(removedNotificationLogs);
                    }

                    //take unresolved notification logs
                    var unresolvedNotificationLogs = notificationLogRepository.GetUnResolvedNotificationLogForNotificationResolveAlert(personQuestionnaire.PersonID);

                    if (unresolvedNotificationLogs.Count > 0)
                    {
                        var assessmentResponses = assessmentResponseRepository.GetAssessmentResponse(assessment.AssessmentID).Result;
                        List<NotificationLog> notificationLogs = HandleRiskNotificationAlerts(assessmentResponses, unresolvedNotificationLogs, notificationResolutionStatusID);
                        if (notificationLogs.Count > 0)
                        {
                            notificationLogRepository.UpdateNotificationLog(notificationLogs);
                        }
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

        /// <summary>
        /// Handle Risk Notification Alerts
        /// </summary>
        /// <param name="assessmentResponses"></param>
        /// <param name="unresolvedNotificationLogs"></param>
        /// <returns>List<NotificationLog></returns>
        public List<NotificationLog> HandleRiskNotificationAlerts(IReadOnlyList<AssessmentResponsesDTO> assessmentResponses, List<RiskNotificationsListDTO> unresolvedNotificationLogs, int resolvedStatusID)
        {
            List<NotificationLog> notificationLogs = new List<NotificationLog>();

            foreach (var unresolvedNotifyRiskRule in unresolvedNotificationLogs)
            {
                if (unresolvedNotifyRiskRule.QuestionnaireNotifyRiskRuleID == 0)
                {
                    continue;
                }
                var questionnaireNotifyRiskRuleConditionList = this.questionnaireNotifyRiskRuleConditionRepository.GetQuestionnaireNotifyRiskRuleConditionByRuleID(unresolvedNotifyRiskRule.QuestionnaireNotifyRiskRuleID);

                bool? notify = null;

                foreach (var item in questionnaireNotifyRiskRuleConditionList)
                {
                    var responseId = assessmentResponses.Where(x => x.QuestionnaireItemID == item.QuestionnaireItemID).Select(x => x.ResponseID).FirstOrDefault();
                    var responseDto = responseRepository.GetResponse(responseId).Result;
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

                }
                if (notify == !true)
                {
                    var notificationLog = new NotificationLog
                    {
                        NotificationLogID = unresolvedNotifyRiskRule.NotificationLogID,
                        NotificationDate = unresolvedNotifyRiskRule.NotificationDate,
                        PersonID = unresolvedNotifyRiskRule.PersonID,
                        NotificationTypeID = unresolvedNotifyRiskRule.NotificationTypeID,
                        NotificationResolutionStatusID = resolvedStatusID,
                        UpdateDate = DateTime.UtcNow,
                        UpdateUserID = unresolvedNotifyRiskRule.UpdateUserID,
                        FKeyValue = unresolvedNotifyRiskRule.NotifyRiskID,
                        IsRemoved = unresolvedNotifyRiskRule.IsRemoved,
                        NotificationData = unresolvedNotifyRiskRule.NotificationData,
                        StatusDate = unresolvedNotifyRiskRule.StatusDate,
                    };
                    notificationLogs.Add(notificationLog);
                }
            }
            return notificationLogs;
        }
    }
}
