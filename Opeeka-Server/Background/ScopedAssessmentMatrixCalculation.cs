//using System;
//using System.Threading;
//using System.Threading.Tasks;
//using Microsoft.Extensions.Logging;
//using Azure.Storage.Queues;
//using Azure.Storage.Queues.Models;
//using Microsoft.Extensions.Configuration;
//using Opeeka.PICS.Domain.Interfaces.Repositories;
//using Opeeka.PICS.Domain.Entities;
//using Opeeka.PICS.Domain.DTO;
//using AutoMapper;
//using Opeeka.PICS.Infrastructure.Enums;
//using System.Linq;
//using System.Collections.Generic;

//namespace Background
//{
//    internal interface IScopedAssessmentMatrixCalculation
//    {
//        Task DoWork(CancellationToken stoppingToken);
//    }
//    public class ScopedAssessmentMatrixCalculation : IScopedAssessmentMatrixCalculation
//    {
//        private readonly ILogger<ScopedAssessmentMatrixCalculation> _logger;
//        private QueueClient _queueClient { get; set; }
//        private static string QueuName = "assessmentmatrixcalulation";
//        private readonly IAssessmentResponseRepository assessmentResponseRepository;
//        private readonly IAssessmentRepository assessmentRepository;
//        private readonly IPersonQuestionnaireRepository personQuestionnaireRepository;
//        private readonly IQuestionnaireRepository questionnaireRepository;
//        private readonly IQuestionnaireItemRepository questionnaireItemRepository;
//        private readonly IPersonQuestionnaireMetricsRepository personQuestionnaireMetricsRepository;
//        private readonly ILookupRepository lookupRepository;
//        private readonly IMapper mapper;
//        private readonly IAssessmentStatusRepository assessmentStatusRepository;


//        public ScopedAssessmentMatrixCalculation(ILogger<ScopedAssessmentMatrixCalculation> logger, IConfiguration configuration, IAssessmentResponseRepository assessmentResponseRepository, 
//            IAssessmentRepository assessmentRepository, IPersonQuestionnaireRepository personQuestionnaireRepository, IQuestionnaireRepository questionnaireRepository,
//            IQuestionnaireItemRepository questionnaireItemRepository, IPersonQuestionnaireMetricsRepository personQuestionnaireMetricsRepository, ILookupRepository lookupRepository, 
//            IMapper mapper, IAssessmentStatusRepository assessmentStatusRepository)
//        {
//            _logger = logger;
//            _queueClient = new QueueClient(configuration.GetValue<string>("queuestoragekey"), ScopedAssessmentMatrixCalculation.QueuName);
//            this.assessmentResponseRepository = assessmentResponseRepository;
//            this.assessmentRepository = assessmentRepository;
//            this.personQuestionnaireRepository = personQuestionnaireRepository;
//            this.questionnaireRepository = questionnaireRepository;
//            this.questionnaireItemRepository = questionnaireItemRepository;
//            this.personQuestionnaireMetricsRepository = personQuestionnaireMetricsRepository;
//            this.lookupRepository = lookupRepository;
//            this.mapper = mapper;
//            this.assessmentStatusRepository = assessmentStatusRepository;
//        }

//        public async Task DoWork(CancellationToken stoppingToken)
//        {
//            // Dummy Code to get the Serialized JSON to insert into Queue.      
//            //var sampleModel = new SampleModel(){
//            //    Id = 1,
//            //    Field1 = "This is the field 1"
//            //};
//            //Console.WriteLine(JsonSerializer.Serialize(sampleModel));

//            while (!stoppingToken.IsCancellationRequested)
//            {
//                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

//                try
//                {
//                    var triedCount = 0;
//                    QueueMessage message = await RetrieveNextMessageAsync(_queueClient);
//                    while (triedCount < 5)
//                    {
//                        if (await ProcessQueueItemAsync(message.MessageText))
//                        {
//                            triedCount = 5;
//                            break;
//                        }
//                        triedCount++;
//                    }

//                    await DeQueueMessageAsync(_queueClient, message);
//                }
//                catch (System.Exception e)
//                {
//                    _logger.LogInformation("Problem reading queue, {message}", e.Message);
//                }

//                await Task.Delay(1, stoppingToken);
//            }
//        }

//        static async Task<QueueMessage> RetrieveNextMessageAsync(QueueClient theQueue)
//        {
//            var queueExists = await theQueue.ExistsAsync();
//            if (queueExists.Value)
//            {
//                QueueProperties properties = await theQueue.GetPropertiesAsync();

//                if (properties.ApproximateMessagesCount > 0)
//                {
//                    QueueMessage[] retrievedMessage = await theQueue.ReceiveMessagesAsync(1);
//                    return retrievedMessage[0];
//                }
//                else
//                {
//                    throw new Exception("No Items in the queue!");
//                }
//            }
//            else
//            {
//                throw new PCISNoItemsInQueue("The queue does not exist!");
//            }
//        }

//        static async Task<bool> DeQueueMessageAsync(QueueClient theQueue, QueueMessage message)
//        {
//            var response = await theQueue.DeleteMessageAsync(message.MessageId, message.PopReceipt);
//            return response.Status.Equals(200);
//        }

//        private async Task<bool> ProcessQueueItemAsync(string messageBody)
//        {
//            try
//            {
//                var assessmentId = Convert.ToInt32(messageBody);
//                _logger.LogInformation("assessment Id", assessmentId);
//                var assessment = assessmentRepository.GetAssessment(assessmentId).Result;
//                _logger.LogInformation("Processing AssessmentID {0} started", assessmentId);
//                List<int> assessmentStatusIDList = new List<int>();
//                assessmentStatusIDList.Add(this.assessmentStatusRepository.GetAssessmentStatus(PCISEnum.AssessmentStatus.Submitted).Result.AssessmentStatusID);
//                assessmentStatusIDList.Add(this.assessmentStatusRepository.GetAssessmentStatus(PCISEnum.AssessmentStatus.Approved).Result.AssessmentStatusID);

//                var assessments = personQuestionnaireRepository.GetAssessmentsByPersonQuestionaireID(assessment.PersonQuestionnaireID, assessmentStatusIDList).ToList().Take(2);
//                var currentAssessment = assessments.Where(x => x.AssessmentID == assessmentId).FirstOrDefault();
//                var otherAssessment = new Assessment();
//                if (currentAssessment != null)
//                {
//                    var lastAssessment = assessments.FirstOrDefault();
//                    var secondLastAssessment = assessments.Count() == 2 ? assessments.Where(x => x.AssessmentID != lastAssessment.AssessmentID).FirstOrDefault() : null;
//                    var lastAssessmentResponse = assessmentResponseRepository.GetAssessmentResponse(lastAssessment.AssessmentID).Result;
//                    var lastPersonQuestionnaire = personQuestionnaireRepository.GetPersonQuestionnaireByID(lastAssessment.PersonQuestionnaireID).Result;
//                    var lastQuestionnaire = questionnaireRepository.GetQuestionnaire(lastPersonQuestionnaire.QuestionnaireID).Result;
//                    //otherAssessment = assessments.Where(x => x.AssessmentID != assessmentId).FirstOrDefault();
//                    if (secondLastAssessment != null)
//                    {
//                        var secondLastAssessmentResponse = assessmentResponseRepository.GetAssessmentResponse(secondLastAssessment.AssessmentID).Result;
//                        var secondLastPersonQuestionnaire = personQuestionnaireRepository.GetPersonQuestionnaireByID(secondLastAssessment.PersonQuestionnaireID).Result;
//                        var secondLastQuestionnaire = questionnaireRepository.GetQuestionnaire(secondLastPersonQuestionnaire.QuestionnaireID).Result;
//                        foreach (var item in lastAssessmentResponse)
//                        {
//                            _logger.LogInformation("Processing AssessmentResponseID {0} started", item.AssessmentResponseID);

//                            var questionnaireItem = questionnaireItemRepository.GetQuestionnaireItems(item.QuestionnaireItemID).Result;
//                            var matchedResponse = secondLastAssessmentResponse.Where(x => x.QuestionnaireItemID == item.QuestionnaireItemID).FirstOrDefault();
//                            var secondLastItemResponseBehaviour = lookupRepository.GetItemResponseBehaviorById(matchedResponse.ItemResponseBehaviorID ?? default(int));
//                            ItemResponseType secondLastItemResponseType = new ItemResponseType();
//                            if (secondLastItemResponseBehaviour != null && (secondLastItemResponseBehaviour.ItemResponseTypeID == 1 || secondLastItemResponseBehaviour.ItemResponseTypeID == 2
//                                || secondLastItemResponseBehaviour.ItemResponseTypeID == 5 || secondLastItemResponseBehaviour.ItemResponseTypeID == 6))
//                            {
//                                secondLastItemResponseType = lookupRepository.GetItemResponseTypeById(secondLastItemResponseBehaviour.ItemResponseTypeID);
//                            }
//                            else
//                            {
//                                secondLastItemResponseType = null;
//                            }
//                            PersonQuestionnaireMetrics personQuestionnaireMetrics = new PersonQuestionnaireMetrics();
                            
//                            var existingPersonQuestionnaireMatrics = personQuestionnaireMetricsRepository.GetPersonQuestionnaireMetrics(lastPersonQuestionnaire.PersonID, questionnaireItem.ItemID).FirstOrDefault();
//                            var lastItemResponseBehaviour = lookupRepository.GetItemResponseBehaviorById(item.ItemResponseBehaviorID ?? default(int));
//                            ItemResponseType lastItemResponseType = new ItemResponseType();
//                            if (lastItemResponseBehaviour != null && (lastItemResponseBehaviour.ItemResponseTypeID == 1 || lastItemResponseBehaviour.ItemResponseTypeID == 2 || 
//                                lastItemResponseBehaviour.ItemResponseTypeID == 5 || lastItemResponseBehaviour.ItemResponseTypeID == 6))
//                            {
//                                lastItemResponseType = lookupRepository.GetItemResponseTypeById(lastItemResponseBehaviour.ItemResponseTypeID);
//                            }
//                            else
//                            {
//                                lastItemResponseType = null;
//                            }
//                            personQuestionnaireMetrics = PopulatePersonQuestionnaireMetrics(lastItemResponseBehaviour, lastItemResponseType, secondLastItemResponseBehaviour, secondLastItemResponseType, existingPersonQuestionnaireMatrics);
//                            personQuestionnaireMetrics.InstrumentID = lastQuestionnaire.InstrumentID;
//                            personQuestionnaireMetrics.ItemID = questionnaireItem.ItemID;
//                            personQuestionnaireMetrics.PersonID = lastPersonQuestionnaire.PersonID;
//                            personQuestionnaireMetrics.PersonQuestionnaireID = Convert.ToInt32(lastPersonQuestionnaire.PersonQuestionnaireID);
//                            if (existingPersonQuestionnaireMatrics == null)
//                            {
//                                personQuestionnaireMetricsRepository.AddPersonQuestionnaireMetrics(personQuestionnaireMetrics);
//                            }
//                            else
//                            {
//                                personQuestionnaireMetrics.PersonQuestionnaireMetricsID = existingPersonQuestionnaireMatrics.PersonQuestionnaireMetricsID;
//                                personQuestionnaireMetricsRepository.UpdatePersonQuestionnaireMetrics(personQuestionnaireMetrics);
//                            }
//                            _logger.LogInformation("Adding PersonQuestionnaireMetrics for AssessmentResponseID {0}", item.AssessmentResponseID);

//                            _logger.LogInformation("Processing AssessmentResponseID {0} ended", item.AssessmentResponseID);
//                        }
//                    }
//                    else
//                    {
//                        foreach (var item in lastAssessmentResponse)
//                        {
//                            _logger.LogInformation("Processing AssessmentResponseID {0} started", item.AssessmentResponseID);

//                            var questionnaireItem = questionnaireItemRepository.GetQuestionnaireItems(item.QuestionnaireItemID).Result;
//                            var currentItemResponseBehaviour = lookupRepository.GetItemResponseBehaviorById(item.ItemResponseBehaviorID ?? default(int));
//                            ItemResponseType currentItemResponseType = new ItemResponseType();
//                            if (currentItemResponseBehaviour != null && (currentItemResponseBehaviour.ItemResponseTypeID == 1 || currentItemResponseBehaviour.ItemResponseTypeID == 2
//                                || currentItemResponseBehaviour.ItemResponseTypeID == 5 || currentItemResponseBehaviour.ItemResponseTypeID == 6))
//                            {
//                                currentItemResponseType = lookupRepository.GetItemResponseTypeById(currentItemResponseBehaviour.ItemResponseTypeID);
//                            }
//                            else
//                            {
//                                currentItemResponseType = null;
//                            }
//                            PersonQuestionnaireMetrics personQuestionnaireMetrics = new PersonQuestionnaireMetrics();
                           
//                            personQuestionnaireMetrics = PopulatePersonQuestionnaireMetrics(currentItemResponseBehaviour, currentItemResponseType, null, null, null);
//                            personQuestionnaireMetrics.InstrumentID = lastQuestionnaire.InstrumentID;
//                            personQuestionnaireMetrics.ItemID = questionnaireItem.ItemID;
//                            personQuestionnaireMetrics.PersonID = lastPersonQuestionnaire.PersonID;
//                            personQuestionnaireMetrics.PersonQuestionnaireID = Convert.ToInt32(lastPersonQuestionnaire.PersonQuestionnaireID);
//                            _logger.LogInformation("Adding PersonQuestionnaireMetrics for AssessmentResponseID {0}", item.AssessmentResponseID);

//                            personQuestionnaireMetricsRepository.AddPersonQuestionnaireMetrics(personQuestionnaireMetrics);
//                            _logger.LogInformation("Processing AssessmentResponseID {0} ended", item.AssessmentResponseID);
//                        }
//                    }
//                    _logger.LogInformation("Processing AssessmentID {0} ended", assessmentId);
//                }
//                else
//                {
//                    var assessmentResponse = assessmentResponseRepository.GetAssessmentResponse(assessment.AssessmentID).Result;
//                    var personQuestionnaire = personQuestionnaireRepository.GetPersonQuestionnaireByID(assessment.PersonQuestionnaireID).Result;
//                    var questionnaire = questionnaireRepository.GetQuestionnaire(personQuestionnaire.QuestionnaireID).Result;
//                    foreach (var item in assessmentResponse)
//                    {
//                        _logger.LogInformation("Processing AssessmentResponseID {0} started", item.AssessmentResponseID);

//                        var questionnaireItem = questionnaireItemRepository.GetQuestionnaireItems(item.QuestionnaireItemID).Result;
//                        var existingPersonQuestionnaireMatrics = personQuestionnaireMetricsRepository.GetPersonQuestionnaireMetrics(personQuestionnaire.PersonID, questionnaireItem.ItemID).FirstOrDefault();
//                        var itemResponseBehaviour = lookupRepository.GetItemResponseBehaviorById(item.ItemResponseBehaviorID ?? default(int));
//                        ItemResponseType itemResponseType = new ItemResponseType();
//                        if (itemResponseBehaviour != null && (itemResponseBehaviour.ItemResponseTypeID == 1 || itemResponseBehaviour.ItemResponseTypeID == 2
//                            || itemResponseBehaviour.ItemResponseTypeID == 5 || itemResponseBehaviour.ItemResponseTypeID == 6))
//                        {
//                            itemResponseType = lookupRepository.GetItemResponseTypeById(itemResponseBehaviour.ItemResponseTypeID);
//                        }
//                        else
//                        {
//                            itemResponseType = null;
//                        }
//                        PersonQuestionnaireMetrics personQuestionnaireMetrics = new PersonQuestionnaireMetrics();
//                        personQuestionnaireMetrics = PopulateOldPersonQuestionnaireMetrics(existingPersonQuestionnaireMatrics, itemResponseBehaviour, itemResponseType);
//                        if (personQuestionnaireMetrics != null)
//                        {
//                            personQuestionnaireMetricsRepository.UpdatePersonQuestionnaireMetrics(personQuestionnaireMetrics);
//                        }
//                    }
//                }
//                return true;
//            }
//            catch (System.Exception)
//            {
//                return false;
//            }
//        }

//        private PersonQuestionnaireMetrics PopulatePersonQuestionnaireMetrics( ItemResponseBehaviorDTO lastItemResponseBehaviour, ItemResponseType lastItemResponseType, ItemResponseBehaviorDTO secondLastItemResponseBehaviour, ItemResponseType secondLastItemResponseType, PersonQuestionnaireMetrics existingPersonQuestionnaireMatrics)
//        {
//            PersonQuestionnaireMetrics matrix = new PersonQuestionnaireMetrics();
//            if (lastItemResponseBehaviour != null && lastItemResponseType != null && secondLastItemResponseBehaviour != null && secondLastItemResponseType != null)
//            {
//                matrix.NeedsEver = ((lastItemResponseType.Name == PCISEnum.ItemResponseType.Need || lastItemResponseType.Name == PCISEnum.ItemResponseType.SupportNeed) && lastItemResponseBehaviour.Name == PCISEnum.ToDo.Focus)
//                    || ((secondLastItemResponseType.Name == PCISEnum.ItemResponseType.Need || secondLastItemResponseType.Name == PCISEnum.ItemResponseType.SupportNeed) && secondLastItemResponseBehaviour.Name == PCISEnum.ToDo.Focus) || existingPersonQuestionnaireMatrics.NeedsEver == 1 ? 1 : 0;
//                matrix.NeedsAddressing = ((lastItemResponseType.Name == PCISEnum.ItemResponseType.Need || lastItemResponseType.Name == PCISEnum.ItemResponseType.SupportNeed) && lastItemResponseBehaviour.Name == PCISEnum.ToDo.Focus)
//                    ? 1 : 0;
//                matrix.NeedsIdentified = ((lastItemResponseType.Name == PCISEnum.ItemResponseType.Need || lastItemResponseType.Name == PCISEnum.ItemResponseType.SupportNeed) && lastItemResponseBehaviour.Name == PCISEnum.ToDo.Focus)
//                    && !((secondLastItemResponseType.Name == PCISEnum.ItemResponseType.Need || secondLastItemResponseType.Name == PCISEnum.ItemResponseType.SupportNeed) && secondLastItemResponseBehaviour.Name == PCISEnum.ToDo.Focus) ? 1 : 0;
//                matrix.NeedsAddressed = !((lastItemResponseType.Name == PCISEnum.ItemResponseType.Need || lastItemResponseType.Name == PCISEnum.ItemResponseType.SupportNeed) && lastItemResponseBehaviour.Name == PCISEnum.ToDo.Focus)
//                    && ((secondLastItemResponseType.Name == PCISEnum.ItemResponseType.Need || secondLastItemResponseType.Name == PCISEnum.ItemResponseType.SupportNeed) && secondLastItemResponseBehaviour.Name == PCISEnum.ToDo.Focus) ? 1 : 0;
//                matrix.NeedsImproved = !((lastItemResponseType.Name == PCISEnum.ItemResponseType.Need || lastItemResponseType.Name == PCISEnum.ItemResponseType.SupportNeed) && lastItemResponseBehaviour.Name == PCISEnum.ToDo.Focus)
//                    && ((secondLastItemResponseType.Name == PCISEnum.ItemResponseType.Need || secondLastItemResponseType.Name == PCISEnum.ItemResponseType.SupportNeed) && secondLastItemResponseBehaviour.Name == PCISEnum.ToDo.Focus) ? 1 : 0;

//                matrix.StrengthsEver = ((lastItemResponseType.Name == PCISEnum.ItemResponseType.Strength || lastItemResponseType.Name == PCISEnum.ItemResponseType.SupportResource) && lastItemResponseBehaviour.Name == PCISEnum.ToDo.Build)
//                    || ((secondLastItemResponseType.Name == PCISEnum.ItemResponseType.Strength || secondLastItemResponseType.Name == PCISEnum.ItemResponseType.SupportResource) && secondLastItemResponseBehaviour.Name == PCISEnum.ToDo.Build) || existingPersonQuestionnaireMatrics.StrengthsEver == 1 ? 1 : 0;
//                matrix.StrengthsBuilding = ((lastItemResponseType.Name == PCISEnum.ItemResponseType.Strength || lastItemResponseType.Name == PCISEnum.ItemResponseType.SupportResource) && lastItemResponseBehaviour.Name == PCISEnum.ToDo.Build)
//                    ? 1 : 0;
//                matrix.StrengthsIdentified = ((lastItemResponseType.Name == PCISEnum.ItemResponseType.Strength || lastItemResponseType.Name == PCISEnum.ItemResponseType.SupportResource) && lastItemResponseBehaviour.Name == PCISEnum.ToDo.Build)
//                    && !((secondLastItemResponseType.Name == PCISEnum.ItemResponseType.Strength || secondLastItemResponseType.Name == PCISEnum.ItemResponseType.SupportResource) && secondLastItemResponseBehaviour.Name == PCISEnum.ToDo.Build) ? 1 : 0;
//                matrix.StrengthsBuilt = !((lastItemResponseType.Name == PCISEnum.ItemResponseType.Strength || lastItemResponseType.Name == PCISEnum.ItemResponseType.SupportResource) && lastItemResponseBehaviour.Name == PCISEnum.ToDo.Build)
//                    && ((secondLastItemResponseType.Name == PCISEnum.ItemResponseType.Strength || secondLastItemResponseType.Name == PCISEnum.ItemResponseType.SupportResource) && secondLastItemResponseBehaviour.Name == PCISEnum.ToDo.Build) ? 1 : 0;
//                matrix.StrengthsBuilt = !((lastItemResponseType.Name == PCISEnum.ItemResponseType.Strength || lastItemResponseType.Name == PCISEnum.ItemResponseType.SupportResource) && lastItemResponseBehaviour.Name == PCISEnum.ToDo.Build)
//                    && ((secondLastItemResponseType.Name == PCISEnum.ItemResponseType.Strength || secondLastItemResponseType.Name == PCISEnum.ItemResponseType.SupportResource) && secondLastItemResponseBehaviour.Name == PCISEnum.ToDo.Build) ? 1 : 0;

//            }
//            else if (lastItemResponseBehaviour != null && lastItemResponseType != null)
//            {
//                matrix.NeedsEver = ((lastItemResponseType.Name == PCISEnum.ItemResponseType.Need || lastItemResponseType.Name == PCISEnum.ItemResponseType.SupportNeed) && lastItemResponseBehaviour.Name == PCISEnum.ToDo.Focus) ? 1 : 0;
//                matrix.NeedsAddressing = ((lastItemResponseType.Name == PCISEnum.ItemResponseType.Need || lastItemResponseType.Name == PCISEnum.ItemResponseType.SupportNeed )&& lastItemResponseBehaviour.Name == PCISEnum.ToDo.Focus) ? 1 : 0;
//                matrix.NeedsIdentified =  0;
//                matrix.NeedsAddressed =  0;
//                matrix.NeedsImproved =  0;

//                matrix.StrengthsEver = ((lastItemResponseType.Name == PCISEnum.ItemResponseType.Strength || lastItemResponseType.Name == PCISEnum.ItemResponseType.SupportResource) && lastItemResponseBehaviour.Name == PCISEnum.ToDo.Build) ? 1 : 0;
//                matrix.StrengthsBuilding = ((lastItemResponseType.Name == PCISEnum.ItemResponseType.Strength || lastItemResponseType.Name == PCISEnum.ItemResponseType.SupportResource) && lastItemResponseBehaviour.Name == PCISEnum.ToDo.Build) ? 1 : 0;
//                matrix.StrengthsIdentified = 0;
//                matrix.StrengthsBuilt = 0;
//                matrix.StrengthsBuilt = 0;

//            }
//            return matrix;
//        }

//        private PersonQuestionnaireMetrics PopulateOldPersonQuestionnaireMetrics(PersonQuestionnaireMetrics personQuestionnaireMetrics, ItemResponseBehaviorDTO itemResponseBehaviour, ItemResponseType itemResponseType)
//        {
//            if (personQuestionnaireMetrics != null && itemResponseBehaviour != null && itemResponseType != null)
//            {
//                //Needs section
//                if ((itemResponseType.Name == PCISEnum.ItemResponseType.Need || itemResponseType.Name == PCISEnum.ItemResponseType.SupportNeed) && itemResponseBehaviour.Name == PCISEnum.ToDo.Focus)
//                {
//                    personQuestionnaireMetrics.NeedsEver = 1;
//                }   
//                //Strength section
//                if ((itemResponseType.Name == PCISEnum.ItemResponseType.Strength|| itemResponseType.Name == PCISEnum.ItemResponseType.SupportResource) && itemResponseBehaviour.Name == PCISEnum.ToDo.Build)
//                {
//                    personQuestionnaireMetrics.StrengthsEver = 1;
//                }
//            }
//            return personQuestionnaireMetrics;
//        }

//    }
//}
