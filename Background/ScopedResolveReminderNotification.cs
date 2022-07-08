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
using Opeeka.PICS.Infrastructure.Enums;
using System.Linq;
using System.Collections.Generic;
using Opeeka.PICS.Domain.Interfaces.Services;
using Opeeka.PICS.Domain.DTO;

namespace Background
{
    internal interface IScopedResolveReminderNotification
    {
        Task DoWork(CancellationToken stoppingToken);
    }
    public class ScopedResolveReminderNotification : IScopedResolveReminderNotification
    {
        private readonly ILogger<ScopedResolveReminderNotification> _logger;
        private QueueClient _queueClient { get; set; }
        private static string QueuName = "resolveremindernotifications";
        private readonly IAssessmentRepository assessmentRepository;
        private readonly IPersonQuestionnaireRepository personQuestionnaireRepository;
        private readonly INotifiationResolutionStatusRepository notifiationResolutionStatusRepository;
        private readonly INotificationTypeRepository notificationTypeRepository;
        private readonly INotificationLogRepository notificationLogRepository;
        private readonly INotifyReminderRepository notifyReminderRepository;
        private readonly IPersonCollaborationRepository personCollaborationRepository;
        private readonly IPersonQuestionnaireScheduleRepository personQuestionnaireScheduleRepository;
        private readonly IQuestionnaireWindowRepository questionnaireWindowRepository;
        private readonly IConfigurationRepository configurationRepository;
        private readonly IAssessmentReasonRepository assessmentReasonRepository;
        private readonly IPersonQuestionnaireScheduleService personQuestionnaireScheduleService;

        public ScopedResolveReminderNotification(ILogger<ScopedResolveReminderNotification> logger, IConfiguration configuration,
            IAssessmentRepository assessmentRepository, IPersonQuestionnaireRepository personQuestionnaireRepository, INotifiationResolutionStatusRepository notifiationResolutionStatusRepository,
            INotificationTypeRepository notificationTypeRepository, INotificationLogRepository notificationLogRepository, IPersonCollaborationRepository personCollaborationRepository,
            IPersonQuestionnaireScheduleRepository personQuestionnaireScheduleRepository, IQuestionnaireWindowRepository questionnaireWindowRepository,
            IAssessmentReasonRepository assessmentReasonRepository, IConfigurationRepository configurationRepository,
            IPersonQuestionnaireScheduleService personQuestionnaireScheduleService, INotifyReminderRepository notifyReminderRepository)
        {
            _logger = logger;
            _queueClient = new QueueClient(configuration.GetValue<string>("queuestoragekey"), ScopedResolveReminderNotification.QueuName);
            this.assessmentRepository = assessmentRepository;
            this.personQuestionnaireRepository = personQuestionnaireRepository;
            this.notifiationResolutionStatusRepository = notifiationResolutionStatusRepository;
            this.notificationTypeRepository = notificationTypeRepository;
            this.notificationLogRepository = notificationLogRepository;
            this.personCollaborationRepository = personCollaborationRepository;
            this.personQuestionnaireScheduleRepository = personQuestionnaireScheduleRepository;
            this.questionnaireWindowRepository = questionnaireWindowRepository;
            this.assessmentReasonRepository = assessmentReasonRepository;
            this.configurationRepository = configurationRepository;
            this.personQuestionnaireScheduleService = personQuestionnaireScheduleService;
            this.notifyReminderRepository = notifyReminderRepository;
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
                        if (await ProcessResolveReminderNotification(message.MessageText))
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

        private async Task<bool> ProcessResolveReminderNotification(string messageBody)
        {
            bool processCompleted = false;
            try
            {
                var assessmentId = Convert.ToInt32(messageBody);
                List<QuestionnaireWindowsDTO> questionnaireWindowList = new List<QuestionnaireWindowsDTO>();
                var assessment = assessmentRepository.GetAssessment(assessmentId).Result;
                var personQuestionnaireList = personQuestionnaireRepository.GetPersonQuestionaireByPersonQuestionaireID(assessment.PersonQuestionnaireID);

                if (personQuestionnaireList != null && personQuestionnaireList.Count > 0)
                {
                    questionnaireWindowList = this.questionnaireWindowRepository.GetQuestionnaireWindowsByQuestionnaire(personQuestionnaireList[0].QuestionnaireID).Where(x => x.IsSelected).ToList();
                };
                var questionnaireWindowListID = questionnaireWindowList.Select(x => x.QuestionnaireWindowID).ToList();

                foreach (var personQuestionnaire in personQuestionnaireList)
                {
                    if (personQuestionnaire != null && personQuestionnaire.CollaborationID > 0)
                    {
                        var personCollaboration = personCollaborationRepository.GetAsync(x => x.PersonID == personQuestionnaire.PersonID && x.CollaborationID == personQuestionnaire.CollaborationID && !x.IsRemoved).Result.FirstOrDefault();
                        if (personCollaboration != null)
                        {
                            personQuestionnaire.EndDueDate = personCollaboration.EndDate;
                            personQuestionnaire.StartDate = personCollaboration.EnrollDate ?? DateTime.UtcNow;
                        }
                    }
                    var dateTaken = assessment.DateTaken;

                    var personQuestionnaireScheduleList = personQuestionnaireScheduleRepository.GetPersonQuestionnaireScheduleList(dateTaken, questionnaireWindowListID, personQuestionnaire.PersonQuestionnaireID).Result;
                    if (personQuestionnaireScheduleList != null)
                    {
                        personQuestionnaireScheduleList.ForEach(x => x.IsRemoved = true);
                        personQuestionnaireScheduleService.UpdateBulkPersonQuestionnaireSchedule(personQuestionnaireScheduleList);
                        var personQuestionnaireScheduleIDList = personQuestionnaireScheduleList.Select(x => x.PersonQuestionnaireScheduleID).ToList();
                        var notifyReminderIDList = notifyReminderRepository.GetAsync(x => personQuestionnaireScheduleIDList.Contains(x.PersonQuestionnaireScheduleID ?? 0)).Result.Select(x => x.NotifyReminderID).ToList();
                        var notificationType = notificationTypeRepository.GetNotificationType(PCISEnum.NotificationType.Reminder).Result;
                        var notificationLogToBeResolved = notificationLogRepository.GetAsync(x => notifyReminderIDList.Contains(x.FKeyValue ?? 0) && x.NotificationTypeID == notificationType.NotificationTypeID).Result.ToList();
                        var notificationResolutionStatusID = this.notifiationResolutionStatusRepository.GetNotificationStatus(PCISEnum.NotificationStatus.Resolved).NotificationResolutionStatusID;

                        notificationLogToBeResolved.ForEach(x => x.NotificationResolutionStatusID = notificationResolutionStatusID);
                        notificationLogRepository.UpdateBulkNotificationLog(notificationLogToBeResolved);
                    }

                }
            }
            catch (Exception)
            {
                processCompleted = false;
            }
            return processCompleted;
        }
    }
}

