using System;
using System.Collections.Generic;
using System.Linq;
using DashboardMatrixCalculationProcess.Common;
using DashboardMatrixCalculationProcess.DTO;
using DashboardMatrixCalculationProcess.DTOt;
using DashboardMatrixCalculationProcess.Enums;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DashboardMatrixCalculationProcess
{
    public static class DashboardMatrixCalculationProcess
    {
        [FunctionName("DashboardMatrixCalculationProcess")]
        public static void Run([QueueTrigger("dashboardmetricscalculation", Connection = "QueueStorageUrlFromKeyVault")]string myQueueItem, ILogger log)
        {
            log.LogInformation($" DashboardMatrixCalculationProcess : C# Queue trigger function processed: {myQueueItem}");
            try
            {
                Utility Utility = new Utility();
                var assessmentId = Convert.ToInt32(myQueueItem);
                List<QuestionnaireWindowsDTO> questionnaireWindowList = new List<QuestionnaireWindowsDTO>();
                log.LogInformation($" DashboardMatrixCalculationProcess : Queue Item : {myQueueItem}");
                var apiurl = Environment.GetEnvironmentVariable(PCISEnum.EnvironmentVariables.ApiUrlFromKeyVault, EnvironmentVariableTarget.Process);

                var assessment = GetAssessmentByID(Utility, apiurl, assessmentId, log, myQueueItem);
                List<int> assessmentStatusIDList = new List<int>();

                List<PersonQuestionnaireMetricsDTO> personQuestionnairreMetricsListToAdd = new List<PersonQuestionnaireMetricsDTO>();
                List<PersonQuestionnaireMetricsDTO> personQuestionnairreMetricsListToUpdate = new List<PersonQuestionnaireMetricsDTO>();
                var itemResponseBehaviours = GetItemResponseBehaviors(Utility, apiurl);
                var itemResponseTypes = GetItemResponseType(Utility, apiurl);
                log.LogInformation($" DashboardMatrixCalculationProcess : Queue Item : {myQueueItem} : GetPersonQuestionnaireByID Start");
                var personQuestionnaire = GetPersonQuestionnaireByID(Utility, apiurl, assessment.PersonQuestionnaireID).FirstOrDefault();
                log.LogInformation($" DashboardMatrixCalculationProcess : Queue Item : {myQueueItem} : GetPersonQuestionnaireByID End");

                log.LogInformation($" DashboardMatrixCalculationProcess : Queue Item : {myQueueItem} : GetAssessmentResponse Start");
                var assessmentResponse = GetAssessmentResponse(Utility, apiurl, assessment.AssessmentID);
                log.LogInformation($" DashboardMatrixCalculationProcess : Queue Item : {myQueueItem} : GetAssessmentResponse End");

                log.LogInformation($" DashboardMatrixCalculationProcess : Queue Item : {myQueueItem} : GetQuestionnaireItems Start");
                var QuestionnaireItems = GetQuestionnaireItems(Utility, apiurl, personQuestionnaire.QuestionnaireID);
                log.LogInformation($" DashboardMatrixCalculationProcess : Queue Item : {myQueueItem} : GetQuestionnaireItems End");

                log.LogInformation($" DashboardMatrixCalculationProcess : Queue Item : {myQueueItem} : AssessmentResponseForeachItems Start");
                var AssessmentResponseValues = AssessmentResponseForeachItems(Utility, apiurl, personQuestionnaire.PersonID, assessment.AssessmentID);
                log.LogInformation($" DashboardMatrixCalculationProcess : Queue Item : {myQueueItem} : AssessmentResponseForeachItems End");

                log.LogInformation($" DashboardMatrixCalculationProcess : Queue Item : {myQueueItem} : GetPersonQuestionnaireMetrics Start");
                var existingMetrics = GetPersonQuestionnaireMetrics(Utility, apiurl, personQuestionnaire.PersonID, QuestionnaireItems.Select(x => x.ItemID).ToList());
                log.LogInformation($" DashboardMatrixCalculationProcess : Queue Item : {myQueueItem} : GetPersonQuestionnaireMetrics End");


                if (assessment.IsRemoved)
                {
                    if (assessmentResponse != null)
                    {
                        foreach (var response in assessmentResponse)
                        {
                            log.LogInformation($" DashboardMatrixCalculationProcess : DELETED : Queue Item : {myQueueItem} : assessmentResponseID : {response.AssessmentResponseID}");

                            var itemId = QuestionnaireItems.Where(x => x.QuestionnaireItemID == response.QuestionnaireItemID).Select(y => y.ItemID).FirstOrDefault();
                            var Assessmentvalues = AssessmentResponseValues.Where(x => x.ItemId == itemId).ToList();
                            var lastAssessmentValue = AssessmentResponseValues.Where(x => x.ItemId == itemId)?.Skip(0)?.Take(1)?.FirstOrDefault();
                            var secondlastAssessmentValue = AssessmentResponseValues.Where(x => x.ItemId == itemId)?.Skip(1)?.Take(1)?.FirstOrDefault();
                            if (lastAssessmentValue != null && secondlastAssessmentValue != null)
                            {
                                log.LogInformation($" DashboardMatrixCalculationProcess : DELETED : Queue Item : {myQueueItem} : assessmentResponseID : {response.AssessmentResponseID} : More than one assessment exist");
                                var secondLastItemResponseBehaviour = itemResponseBehaviours.Where(x => x.ItemResponseBehaviorID == (secondlastAssessmentValue.ItemResponseBehaviorID ?? default(int))).FirstOrDefault();
                                ItemResponseTypeDTO secondLastItemResponseType = new ItemResponseTypeDTO();
                                if (secondLastItemResponseBehaviour != null && (secondLastItemResponseBehaviour.ItemResponseTypeID == 1 || secondLastItemResponseBehaviour.ItemResponseTypeID == 2
                                    || secondLastItemResponseBehaviour.ItemResponseTypeID == 5 || secondLastItemResponseBehaviour.ItemResponseTypeID == 6))
                                {
                                    secondLastItemResponseType = itemResponseTypes.Where(x => x.ItemResponseTypeID == secondLastItemResponseBehaviour.ItemResponseTypeID).FirstOrDefault();
                                }
                                else
                                {
                                    secondLastItemResponseType = null;
                                }
                                PersonQuestionnaireMetricsDTO personQuestionnaireMetrics = new PersonQuestionnaireMetricsDTO();

                                var existingPersonQuestionnaireMatrics = existingMetrics.Where(x => x.ItemID == itemId).ToList();
                                var lastItemResponseBehaviour = itemResponseBehaviours.Where(x => x.ItemResponseBehaviorID == (lastAssessmentValue.ItemResponseBehaviorID ?? default(int))).FirstOrDefault();
                                ItemResponseTypeDTO lastItemResponseType = new ItemResponseTypeDTO();
                                if (lastItemResponseBehaviour != null && (lastItemResponseBehaviour.ItemResponseTypeID == 1 || lastItemResponseBehaviour.ItemResponseTypeID == 2 ||
                                    lastItemResponseBehaviour.ItemResponseTypeID == 5 || lastItemResponseBehaviour.ItemResponseTypeID == 6))
                                {
                                    lastItemResponseType = itemResponseTypes.Where(x => x.ItemResponseTypeID == lastItemResponseBehaviour.ItemResponseTypeID).FirstOrDefault();
                                }
                                else
                                {
                                    lastItemResponseType = null;
                                }
                                personQuestionnaireMetrics = PopulatePersonQuestionnaireMetrics(lastItemResponseBehaviour, lastItemResponseType, secondLastItemResponseBehaviour, secondLastItemResponseType, existingPersonQuestionnaireMatrics.FirstOrDefault());

                                var listFocusItems = itemResponseBehaviours.Where(x => (x.ItemResponseTypeID == 1 || x.ItemResponseTypeID == 5) && x.Name == PCISEnum.ToDo.Focus).ToList();
                                if (listFocusItems.Where(x => Assessmentvalues.Select(x => x.ItemResponseBehaviorID).ToList().Contains(x.ItemResponseBehaviorID))?.FirstOrDefault() == null)
                                {
                                    personQuestionnaireMetrics.NeedsEver = 0;
                                }

                                var listBuildItems = itemResponseBehaviours.Where(x => (x.ItemResponseTypeID == 2 || x.ItemResponseTypeID == 6) && x.Name == PCISEnum.ToDo.Build).ToList();
                                if (listBuildItems.Where(x => Assessmentvalues.Select(x => x.ItemResponseBehaviorID).ToList().Contains(x.ItemResponseBehaviorID))?.FirstOrDefault() == null)
                                {
                                    personQuestionnaireMetrics.StrengthsEver = 0;
                                }
                                Assessmentvalues.Select(x => x.ItemResponseBehaviorID).ToList();
                                personQuestionnaireMetrics.InstrumentID = secondlastAssessmentValue.InstrumentID;
                                personQuestionnaireMetrics.ItemID = itemId;
                                personQuestionnaireMetrics.PersonID = personQuestionnaire.PersonID;
                                personQuestionnaireMetrics.PersonQuestionnaireID = Convert.ToInt32(personQuestionnaire.PersonQuestionnaireID);
                                if (existingPersonQuestionnaireMatrics == null || existingPersonQuestionnaireMatrics.Count == 0)
                                {
                                    personQuestionnairreMetricsListToAdd.Add(personQuestionnaireMetrics);
                                }
                                else
                                {
                                    foreach (var metrics in existingPersonQuestionnaireMatrics)
                                    {
                                        PersonQuestionnaireMetricsDTO newpersonQuestionnaireMetrics = new PersonQuestionnaireMetricsDTO();
                                        newpersonQuestionnaireMetrics.NeedsAddressed = personQuestionnaireMetrics.NeedsAddressed;
                                        newpersonQuestionnaireMetrics.NeedsAddressing = personQuestionnaireMetrics.NeedsAddressing;
                                        newpersonQuestionnaireMetrics.NeedsEver = personQuestionnaireMetrics.NeedsEver;
                                        newpersonQuestionnaireMetrics.NeedsIdentified = personQuestionnaireMetrics.NeedsIdentified;
                                        newpersonQuestionnaireMetrics.NeedsImproved = personQuestionnaireMetrics.NeedsImproved;
                                        newpersonQuestionnaireMetrics.StrengthsBuilt = personQuestionnaireMetrics.StrengthsBuilt;
                                        newpersonQuestionnaireMetrics.StrengthsBuilding = personQuestionnaireMetrics.StrengthsBuilding;
                                        newpersonQuestionnaireMetrics.StrengthsEver = personQuestionnaireMetrics.StrengthsEver;
                                        newpersonQuestionnaireMetrics.StrengthsIdentified = personQuestionnaireMetrics.StrengthsIdentified;
                                        newpersonQuestionnaireMetrics.StrengthsImproved = personQuestionnaireMetrics.StrengthsImproved;
                                        newpersonQuestionnaireMetrics.InstrumentID = personQuestionnaireMetrics.InstrumentID;
                                        newpersonQuestionnaireMetrics.ItemID = personQuestionnaireMetrics.ItemID;
                                        newpersonQuestionnaireMetrics.PersonID = personQuestionnaireMetrics.PersonID;
                                        newpersonQuestionnaireMetrics.PersonQuestionnaireID = personQuestionnaireMetrics.PersonQuestionnaireID;
                                        newpersonQuestionnaireMetrics.PersonQuestionnaireMetricsID = metrics.PersonQuestionnaireMetricsID;
                                        personQuestionnairreMetricsListToUpdate.Add(newpersonQuestionnaireMetrics);
                                    }
                                }
                            }
                            else if (lastAssessmentValue != null && secondlastAssessmentValue == null)
                            {
                                log.LogInformation($" DashboardMatrixCalculationProcess : DELETED : Queue Item : {myQueueItem} : assessmentResponseID : {response.AssessmentResponseID}");
                                var currentItemResponseBehaviour = itemResponseBehaviours.Where(x => x.ItemResponseBehaviorID == (lastAssessmentValue.ItemResponseBehaviorID ?? default(int))).FirstOrDefault();
                                ItemResponseTypeDTO currentItemResponseType = new ItemResponseTypeDTO();
                                if (currentItemResponseBehaviour != null && (currentItemResponseBehaviour.ItemResponseTypeID == 1 || currentItemResponseBehaviour.ItemResponseTypeID == 2
                                    || currentItemResponseBehaviour.ItemResponseTypeID == 5 || currentItemResponseBehaviour.ItemResponseTypeID == 6))
                                {
                                    currentItemResponseType = itemResponseTypes.Where(x => x.ItemResponseTypeID == currentItemResponseBehaviour.ItemResponseTypeID).FirstOrDefault();
                                }
                                else
                                {
                                    currentItemResponseType = null;
                                }
                                PersonQuestionnaireMetricsDTO personQuestionnaireMetrics = new PersonQuestionnaireMetricsDTO();
                                var existingPersonQuestionnaireMatrics = existingMetrics.Where(x => x.ItemID == itemId).ToList();

                                personQuestionnaireMetrics = PopulatePersonQuestionnaireMetrics(currentItemResponseBehaviour, currentItemResponseType, null, null, null);

                                var listFocusItems = itemResponseBehaviours.Where(x => (x.ItemResponseTypeID == 1 || x.ItemResponseTypeID == 5) && x.Name == PCISEnum.ToDo.Focus).ToList();
                                if (listFocusItems.Where(x => Assessmentvalues.Select(x => x.ItemResponseBehaviorID).ToList().Contains(x.ItemResponseBehaviorID))?.FirstOrDefault() == null)
                                {
                                    personQuestionnaireMetrics.NeedsEver = 0;
                                }

                                var listBuildItems = itemResponseBehaviours.Where(x => (x.ItemResponseTypeID == 2 || x.ItemResponseTypeID == 6) && x.Name == PCISEnum.ToDo.Build).ToList();
                                if (listBuildItems.Where(x => Assessmentvalues.Select(x => x.ItemResponseBehaviorID).ToList().Contains(x.ItemResponseBehaviorID))?.FirstOrDefault() == null)
                                {
                                    personQuestionnaireMetrics.StrengthsEver = 0;
                                }
                                personQuestionnaireMetrics.InstrumentID = lastAssessmentValue.InstrumentID;
                                personQuestionnaireMetrics.ItemID = itemId;
                                personQuestionnaireMetrics.PersonID = personQuestionnaire.PersonID;
                                personQuestionnaireMetrics.PersonQuestionnaireID = Convert.ToInt32(personQuestionnaire.PersonQuestionnaireID);
                                if (existingPersonQuestionnaireMatrics == null || existingPersonQuestionnaireMatrics.Count == 0)
                                {
                                    personQuestionnairreMetricsListToAdd.Add(personQuestionnaireMetrics);
                                }
                                else
                                {
                                    foreach (var metrics in existingPersonQuestionnaireMatrics)
                                    {
                                        PersonQuestionnaireMetricsDTO newpersonQuestionnaireMetrics = new PersonQuestionnaireMetricsDTO();
                                        newpersonQuestionnaireMetrics.NeedsAddressed = personQuestionnaireMetrics.NeedsAddressed;
                                        newpersonQuestionnaireMetrics.NeedsAddressing = personQuestionnaireMetrics.NeedsAddressing;
                                        newpersonQuestionnaireMetrics.NeedsEver = personQuestionnaireMetrics.NeedsEver;
                                        newpersonQuestionnaireMetrics.NeedsIdentified = personQuestionnaireMetrics.NeedsIdentified;
                                        newpersonQuestionnaireMetrics.NeedsImproved = personQuestionnaireMetrics.NeedsImproved;
                                        newpersonQuestionnaireMetrics.StrengthsBuilt = personQuestionnaireMetrics.StrengthsBuilt;
                                        newpersonQuestionnaireMetrics.StrengthsBuilding = personQuestionnaireMetrics.StrengthsBuilding;
                                        newpersonQuestionnaireMetrics.StrengthsEver = personQuestionnaireMetrics.StrengthsEver;
                                        newpersonQuestionnaireMetrics.StrengthsIdentified = personQuestionnaireMetrics.StrengthsIdentified;
                                        newpersonQuestionnaireMetrics.StrengthsImproved = personQuestionnaireMetrics.StrengthsImproved;
                                        newpersonQuestionnaireMetrics.InstrumentID = personQuestionnaireMetrics.InstrumentID;
                                        newpersonQuestionnaireMetrics.ItemID = personQuestionnaireMetrics.ItemID;
                                        newpersonQuestionnaireMetrics.PersonID = personQuestionnaireMetrics.PersonID;
                                        newpersonQuestionnaireMetrics.PersonQuestionnaireID = personQuestionnaireMetrics.PersonQuestionnaireID;
                                        newpersonQuestionnaireMetrics.PersonQuestionnaireMetricsID = metrics.PersonQuestionnaireMetricsID;
                                        personQuestionnairreMetricsListToUpdate.Add(newpersonQuestionnaireMetrics);
                                    }
                                }
                            }
                            else
                            {
                                var existingPersonQuestionnaireMatrics = existingMetrics.Where(x => x.ItemID == itemId).ToList();

                                foreach (var metrics in existingPersonQuestionnaireMatrics)
                                {
                                    metrics.NeedsAddressed = 0;
                                    metrics.NeedsAddressing = 0;
                                    metrics.NeedsEver = 0;
                                    metrics.NeedsIdentified = 0;
                                    metrics.NeedsImproved = 0;
                                    metrics.StrengthsBuilt = 0;
                                    metrics.StrengthsBuilding = 0;
                                    metrics.StrengthsEver = 0;
                                    metrics.StrengthsIdentified = 0;
                                    metrics.StrengthsImproved = 0;
                                    personQuestionnairreMetricsListToUpdate.Add(metrics);
                                }
                            }

                            log.LogInformation($" DashboardMatrixCalculationProcess : DELETED : Queue Item : {myQueueItem} : count to add : {personQuestionnairreMetricsListToAdd.Count}");
                            log.LogInformation($" DashboardMatrixCalculationProcess : DELETED : Queue Item : {myQueueItem} : count to update : {personQuestionnairreMetricsListToUpdate.Count}");

                            if (personQuestionnairreMetricsListToAdd.Count > 0)
                                AddPersonQuestionnaireMetrics(Utility, apiurl, personQuestionnairreMetricsListToAdd);

                            if (personQuestionnairreMetricsListToUpdate.Count > 0)
                                UpdatePersonQuestionnaireMetrics(Utility, apiurl, personQuestionnairreMetricsListToUpdate);
                        }
                    }
                }
                else
                {
                    var otherAssessment = new AssessmentDTO();
                    if (assessmentResponse != null)
                    {
                        foreach (var response in assessmentResponse)
                        {
                            log.LogInformation($" DashboardMatrixCalculationProcess : Queue Item : {myQueueItem} : assessmentResponseID : {response.AssessmentResponseID}");

                            var itemId = QuestionnaireItems.Where(x => x.QuestionnaireItemID == response.QuestionnaireItemID).Select(y => y.ItemID).FirstOrDefault();
                            var Assessmentvalues = AssessmentResponseValues.Where(x => x.ItemId == itemId).ToList();
                            var lastAssessmentValue = AssessmentResponseValues.Where(x => x.ItemId == itemId)?.Skip(0)?.Take(1)?.FirstOrDefault();
                            var secondlastAssessmentValue = AssessmentResponseValues.Where(x => x.ItemId == itemId)?.Skip(1)?.Take(1)?.FirstOrDefault();
                            if (response.AssessmentID == lastAssessmentValue?.AssessmentID || response.AssessmentID == secondlastAssessmentValue?.AssessmentID)
                            {
                                log.LogInformation($" DashboardMatrixCalculationProcess : Queue Item : {myQueueItem} : current asseessment in last two assessment");

                                if (secondlastAssessmentValue != null)
                                {
                                    var secondLastItemResponseBehaviour = itemResponseBehaviours.Where(x => x.ItemResponseBehaviorID == (secondlastAssessmentValue.ItemResponseBehaviorID ?? default(int))).FirstOrDefault();
                                    ItemResponseTypeDTO secondLastItemResponseType = new ItemResponseTypeDTO();
                                    if (secondLastItemResponseBehaviour != null && (secondLastItemResponseBehaviour.ItemResponseTypeID == 1 || secondLastItemResponseBehaviour.ItemResponseTypeID == 2
                                        || secondLastItemResponseBehaviour.ItemResponseTypeID == 5 || secondLastItemResponseBehaviour.ItemResponseTypeID == 6))
                                    {
                                        secondLastItemResponseType = itemResponseTypes.Where(x => x.ItemResponseTypeID == secondLastItemResponseBehaviour.ItemResponseTypeID).FirstOrDefault();
                                    }
                                    else
                                    {
                                        secondLastItemResponseType = null;
                                    }
                                    PersonQuestionnaireMetricsDTO personQuestionnaireMetrics = new PersonQuestionnaireMetricsDTO();

                                    var existingPersonQuestionnaireMatrics = existingMetrics.Where(x => x.ItemID == itemId).ToList();
                                    var lastItemResponseBehaviour = itemResponseBehaviours.Where(x => x.ItemResponseBehaviorID == (lastAssessmentValue.ItemResponseBehaviorID ?? default(int))).FirstOrDefault();
                                    ItemResponseTypeDTO lastItemResponseType = new ItemResponseTypeDTO();
                                    if (lastItemResponseBehaviour != null && (lastItemResponseBehaviour.ItemResponseTypeID == 1 || lastItemResponseBehaviour.ItemResponseTypeID == 2 ||
                                        lastItemResponseBehaviour.ItemResponseTypeID == 5 || lastItemResponseBehaviour.ItemResponseTypeID == 6))
                                    {
                                        lastItemResponseType = itemResponseTypes.Where(x => x.ItemResponseTypeID == lastItemResponseBehaviour.ItemResponseTypeID).FirstOrDefault();
                                    }
                                    else
                                    {
                                        lastItemResponseType = null;
                                    }
                                    personQuestionnaireMetrics = PopulatePersonQuestionnaireMetrics(lastItemResponseBehaviour, lastItemResponseType, secondLastItemResponseBehaviour, secondLastItemResponseType, existingPersonQuestionnaireMatrics.FirstOrDefault());
                                    personQuestionnaireMetrics.InstrumentID = secondlastAssessmentValue.InstrumentID;
                                    personQuestionnaireMetrics.ItemID = itemId;
                                    personQuestionnaireMetrics.PersonID = personQuestionnaire.PersonID;
                                    personQuestionnaireMetrics.PersonQuestionnaireID = Convert.ToInt32(personQuestionnaire.PersonQuestionnaireID);
                                    if (existingPersonQuestionnaireMatrics == null || existingPersonQuestionnaireMatrics.Count == 0)
                                    {
                                        personQuestionnairreMetricsListToAdd.Add(personQuestionnaireMetrics);
                                    }
                                    else
                                    {
                                        foreach (var metrics in existingPersonQuestionnaireMatrics)
                                        {
                                            PersonQuestionnaireMetricsDTO newpersonQuestionnaireMetrics = new PersonQuestionnaireMetricsDTO();
                                            newpersonQuestionnaireMetrics.NeedsAddressed = personQuestionnaireMetrics.NeedsAddressed;
                                            newpersonQuestionnaireMetrics.NeedsAddressing = personQuestionnaireMetrics.NeedsAddressing;
                                            newpersonQuestionnaireMetrics.NeedsEver = personQuestionnaireMetrics.NeedsEver;
                                            newpersonQuestionnaireMetrics.NeedsIdentified = personQuestionnaireMetrics.NeedsIdentified;
                                            newpersonQuestionnaireMetrics.NeedsImproved = personQuestionnaireMetrics.NeedsImproved;
                                            newpersonQuestionnaireMetrics.StrengthsBuilt = personQuestionnaireMetrics.StrengthsBuilt;
                                            newpersonQuestionnaireMetrics.StrengthsBuilding = personQuestionnaireMetrics.StrengthsBuilding;
                                            newpersonQuestionnaireMetrics.StrengthsEver = personQuestionnaireMetrics.StrengthsEver;
                                            newpersonQuestionnaireMetrics.StrengthsIdentified = personQuestionnaireMetrics.StrengthsIdentified;
                                            newpersonQuestionnaireMetrics.StrengthsImproved = personQuestionnaireMetrics.StrengthsImproved;
                                            newpersonQuestionnaireMetrics.InstrumentID = personQuestionnaireMetrics.InstrumentID;
                                            newpersonQuestionnaireMetrics.ItemID = personQuestionnaireMetrics.ItemID;
                                            newpersonQuestionnaireMetrics.PersonID = personQuestionnaireMetrics.PersonID;
                                            newpersonQuestionnaireMetrics.PersonQuestionnaireID = personQuestionnaireMetrics.PersonQuestionnaireID;
                                            newpersonQuestionnaireMetrics.PersonQuestionnaireMetricsID = metrics.PersonQuestionnaireMetricsID;
                                            personQuestionnairreMetricsListToUpdate.Add(newpersonQuestionnaireMetrics);
                                        }
                                    }

                                }
                                else
                                {
                                    var currentItemResponseBehaviour = itemResponseBehaviours.Where(x => x.ItemResponseBehaviorID == (lastAssessmentValue.ItemResponseBehaviorID ?? default(int))).FirstOrDefault();
                                    ItemResponseTypeDTO currentItemResponseType = new ItemResponseTypeDTO();
                                    if (currentItemResponseBehaviour != null && (currentItemResponseBehaviour.ItemResponseTypeID == 1 || currentItemResponseBehaviour.ItemResponseTypeID == 2
                                        || currentItemResponseBehaviour.ItemResponseTypeID == 5 || currentItemResponseBehaviour.ItemResponseTypeID == 6))
                                    {
                                        currentItemResponseType = itemResponseTypes.Where(x => x.ItemResponseTypeID == currentItemResponseBehaviour.ItemResponseTypeID).FirstOrDefault();
                                    }
                                    else
                                    {
                                        currentItemResponseType = null;
                                    }
                                    PersonQuestionnaireMetricsDTO personQuestionnaireMetrics = new PersonQuestionnaireMetricsDTO();

                                    personQuestionnaireMetrics = PopulatePersonQuestionnaireMetrics(currentItemResponseBehaviour, currentItemResponseType, null, null, null);
                                    personQuestionnaireMetrics.InstrumentID = lastAssessmentValue.InstrumentID;
                                    personQuestionnaireMetrics.ItemID = itemId;
                                    personQuestionnaireMetrics.PersonID = personQuestionnaire.PersonID;
                                    personQuestionnaireMetrics.PersonQuestionnaireID = Convert.ToInt32(personQuestionnaire.PersonQuestionnaireID);

                                    personQuestionnairreMetricsListToAdd.Add(personQuestionnaireMetrics);
                                }
                            }
                            else
                            {
                                log.LogInformation($" DashboardMatrixCalculationProcess : Queue Item : {myQueueItem} : current asseessment is not in last two assessment");

                                var existingPersonQuestionnaireMatrics = existingMetrics.Where(x => x.ItemID == itemId).ToList();
                                var itemResponseBehaviour = itemResponseBehaviours.Where(x => x.ItemResponseBehaviorID == (response.ItemResponseBehaviorID ?? default(int))).FirstOrDefault();
                                ItemResponseTypeDTO itemResponseType = new ItemResponseTypeDTO();
                                if (itemResponseBehaviour != null && (itemResponseBehaviour.ItemResponseTypeID == 1 || itemResponseBehaviour.ItemResponseTypeID == 2
                                    || itemResponseBehaviour.ItemResponseTypeID == 5 || itemResponseBehaviour.ItemResponseTypeID == 6))
                                {
                                    itemResponseType = itemResponseTypes.Where(x => x.ItemResponseTypeID == itemResponseBehaviour.ItemResponseTypeID).FirstOrDefault();
                                }
                                else
                                {
                                    itemResponseType = null;
                                }

                                foreach (var metrics in existingPersonQuestionnaireMatrics)
                                {
                                    PersonQuestionnaireMetricsDTO personQuestionnaireMetrics = new PersonQuestionnaireMetricsDTO();
                                    personQuestionnaireMetrics = PopulateOldPersonQuestionnaireMetrics(metrics, itemResponseBehaviour, itemResponseType);
                                    if (personQuestionnaireMetrics != null)
                                    {
                                        personQuestionnairreMetricsListToUpdate.Add(personQuestionnaireMetrics);
                                    }
                                }
                            }
                        }

                        log.LogInformation($" DashboardMatrixCalculationProcess : Queue Item : {myQueueItem} : count to add : {personQuestionnairreMetricsListToAdd.Count}");
                        log.LogInformation($" DashboardMatrixCalculationProcess : Queue Item : {myQueueItem} : count to update : {personQuestionnairreMetricsListToUpdate.Count}");

                        if (personQuestionnairreMetricsListToAdd.Count > 0)
                            AddPersonQuestionnaireMetrics(Utility, apiurl, personQuestionnairreMetricsListToAdd);

                        if (personQuestionnairreMetricsListToUpdate.Count > 0)
                            UpdatePersonQuestionnaireMetrics(Utility, apiurl, personQuestionnairreMetricsListToUpdate);
                    }
                }

                #region PopulatePersonAssessmentMetricsInDetailForDashboard
                log.LogInformation($" DashboardMatrixCalculationProcess : Queue Item : {myQueueItem} : GetPersonAssessmentMetricsInDetail Start");
                var existingMetricsForAssessment = GetPersonAssessmentMetricsInDetail(Utility, apiurl, personQuestionnaire.PersonID, assessment.AssessmentID, log);

                PopulatePersonAssessmentMetricsInDetail(myQueueItem, personQuestionnaire, assessment, assessmentResponse, AssessmentResponseValues, QuestionnaireItems, itemResponseTypes, itemResponseBehaviours, existingMetricsForAssessment, Utility, apiurl, log);

                log.LogInformation($" DashboardMatrixCalculationProcess : Queue Item : {myQueueItem} : GetPersonAssessmentMetricsInDetail End");
                #endregion
                log.LogInformation($" DashboardMatrixCalculationProcess : Queue Item : {myQueueItem} :END");
            }
            catch (Exception ex)
            {
                log.LogError(ex, $@"DashboardMatrixCalculationProcess : Exception occurred.Exception Message : {ex.Message}.Location : {ex.StackTrace}.Details : {ex.InnerException}");
            }
        }

        private static PersonQuestionnaireMetricsDTO PopulatePersonQuestionnaireMetrics(ItemResponseBehaviorDTO lastItemResponseBehaviour, ItemResponseTypeDTO lastItemResponseType, ItemResponseBehaviorDTO secondLastItemResponseBehaviour, ItemResponseTypeDTO secondLastItemResponseType, PersonQuestionnaireMetricsDTO existingPersonQuestionnaireMatrics)
        {
            PersonQuestionnaireMetricsDTO matrix = new PersonQuestionnaireMetricsDTO();
            if (lastItemResponseBehaviour != null && lastItemResponseType != null && secondLastItemResponseBehaviour != null && secondLastItemResponseType != null)
            {
                matrix.NeedsEver = ((lastItemResponseType.Name == PCISEnum.ItemResponseType.Need || lastItemResponseType.Name == PCISEnum.ItemResponseType.SupportNeed) && lastItemResponseBehaviour.Name == PCISEnum.ToDo.Focus)
                    || ((secondLastItemResponseType.Name == PCISEnum.ItemResponseType.Need || secondLastItemResponseType.Name == PCISEnum.ItemResponseType.SupportNeed) && secondLastItemResponseBehaviour.Name == PCISEnum.ToDo.Focus) || existingPersonQuestionnaireMatrics?.NeedsEver == 1 ? 1 : 0;
                matrix.NeedsAddressing = ((lastItemResponseType.Name == PCISEnum.ItemResponseType.Need || lastItemResponseType.Name == PCISEnum.ItemResponseType.SupportNeed) && lastItemResponseBehaviour.Name == PCISEnum.ToDo.Focus)
                    ? 1 : 0;
                matrix.NeedsIdentified = ((lastItemResponseType.Name == PCISEnum.ItemResponseType.Need || lastItemResponseType.Name == PCISEnum.ItemResponseType.SupportNeed) && lastItemResponseBehaviour.Name == PCISEnum.ToDo.Focus)
                    && !((secondLastItemResponseType.Name == PCISEnum.ItemResponseType.Need || secondLastItemResponseType.Name == PCISEnum.ItemResponseType.SupportNeed) && secondLastItemResponseBehaviour.Name == PCISEnum.ToDo.Focus) ? 1 : 0;
                matrix.NeedsAddressed = !((lastItemResponseType.Name == PCISEnum.ItemResponseType.Need || lastItemResponseType.Name == PCISEnum.ItemResponseType.SupportNeed) && lastItemResponseBehaviour.Name == PCISEnum.ToDo.Focus)
                    && ((secondLastItemResponseType.Name == PCISEnum.ItemResponseType.Need || secondLastItemResponseType.Name == PCISEnum.ItemResponseType.SupportNeed) && secondLastItemResponseBehaviour.Name == PCISEnum.ToDo.Focus) ? 1 : 0;
                matrix.NeedsImproved = !((lastItemResponseType.Name == PCISEnum.ItemResponseType.Need || lastItemResponseType.Name == PCISEnum.ItemResponseType.SupportNeed) && lastItemResponseBehaviour.Name == PCISEnum.ToDo.Focus)
                    && ((secondLastItemResponseType.Name == PCISEnum.ItemResponseType.Need || secondLastItemResponseType.Name == PCISEnum.ItemResponseType.SupportNeed) && secondLastItemResponseBehaviour.Name == PCISEnum.ToDo.Focus) ? 1 : 0;

                matrix.StrengthsEver = ((lastItemResponseType.Name == PCISEnum.ItemResponseType.Strength || lastItemResponseType.Name == PCISEnum.ItemResponseType.SupportResource) && lastItemResponseBehaviour.Name == PCISEnum.ToDo.Build)
                    || ((secondLastItemResponseType.Name == PCISEnum.ItemResponseType.Strength || secondLastItemResponseType.Name == PCISEnum.ItemResponseType.SupportResource) && secondLastItemResponseBehaviour.Name == PCISEnum.ToDo.Build) || existingPersonQuestionnaireMatrics?.StrengthsEver == 1 ? 1 : 0;
                matrix.StrengthsBuilding = ((lastItemResponseType.Name == PCISEnum.ItemResponseType.Strength || lastItemResponseType.Name == PCISEnum.ItemResponseType.SupportResource) && lastItemResponseBehaviour.Name == PCISEnum.ToDo.Build)
                    ? 1 : 0;
                matrix.StrengthsIdentified = ((lastItemResponseType.Name == PCISEnum.ItemResponseType.Strength || lastItemResponseType.Name == PCISEnum.ItemResponseType.SupportResource) && lastItemResponseBehaviour.Name == PCISEnum.ToDo.Build)
                    && !((secondLastItemResponseType.Name == PCISEnum.ItemResponseType.Strength || secondLastItemResponseType.Name == PCISEnum.ItemResponseType.SupportResource) && secondLastItemResponseBehaviour.Name == PCISEnum.ToDo.Build) ? 1 : 0;
                matrix.StrengthsBuilt = !((lastItemResponseType.Name == PCISEnum.ItemResponseType.Strength || lastItemResponseType.Name == PCISEnum.ItemResponseType.SupportResource) && lastItemResponseBehaviour.Name == PCISEnum.ToDo.Build)
                    && ((secondLastItemResponseType.Name == PCISEnum.ItemResponseType.Strength || secondLastItemResponseType.Name == PCISEnum.ItemResponseType.SupportResource) && secondLastItemResponseBehaviour.Name == PCISEnum.ToDo.Build) ? 1 : 0;
                matrix.StrengthsBuilt = !((lastItemResponseType.Name == PCISEnum.ItemResponseType.Strength || lastItemResponseType.Name == PCISEnum.ItemResponseType.SupportResource) && lastItemResponseBehaviour.Name == PCISEnum.ToDo.Build)
                    && ((secondLastItemResponseType.Name == PCISEnum.ItemResponseType.Strength || secondLastItemResponseType.Name == PCISEnum.ItemResponseType.SupportResource) && secondLastItemResponseBehaviour.Name == PCISEnum.ToDo.Build) ? 1 : 0;

            }
            else if (lastItemResponseBehaviour != null && lastItemResponseType != null)
            {
                matrix.NeedsEver = ((lastItemResponseType.Name == PCISEnum.ItemResponseType.Need || lastItemResponseType.Name == PCISEnum.ItemResponseType.SupportNeed) && lastItemResponseBehaviour.Name == PCISEnum.ToDo.Focus) ? 1 : 0;
                matrix.NeedsAddressing = ((lastItemResponseType.Name == PCISEnum.ItemResponseType.Need || lastItemResponseType.Name == PCISEnum.ItemResponseType.SupportNeed) && lastItemResponseBehaviour.Name == PCISEnum.ToDo.Focus) ? 1 : 0;
                matrix.NeedsIdentified = 0;
                matrix.NeedsAddressed = 0;
                matrix.NeedsImproved = 0;

                matrix.StrengthsEver = ((lastItemResponseType.Name == PCISEnum.ItemResponseType.Strength || lastItemResponseType.Name == PCISEnum.ItemResponseType.SupportResource) && lastItemResponseBehaviour.Name == PCISEnum.ToDo.Build) ? 1 : 0;
                matrix.StrengthsBuilding = ((lastItemResponseType.Name == PCISEnum.ItemResponseType.Strength || lastItemResponseType.Name == PCISEnum.ItemResponseType.SupportResource) && lastItemResponseBehaviour.Name == PCISEnum.ToDo.Build) ? 1 : 0;
                matrix.StrengthsIdentified = 0;
                matrix.StrengthsBuilt = 0;
                matrix.StrengthsBuilt = 0;

            }
            return matrix;
        }

        private static PersonQuestionnaireMetricsDTO PopulateOldPersonQuestionnaireMetrics(PersonQuestionnaireMetricsDTO personQuestionnaireMetrics, ItemResponseBehaviorDTO itemResponseBehaviour, ItemResponseTypeDTO itemResponseType)
        {
            if (personQuestionnaireMetrics != null && itemResponseBehaviour != null && itemResponseType != null)
            {
                //Needs section
                if ((itemResponseType.Name == PCISEnum.ItemResponseType.Need || itemResponseType.Name == PCISEnum.ItemResponseType.SupportNeed) && itemResponseBehaviour.Name == PCISEnum.ToDo.Focus)
                {
                    personQuestionnaireMetrics.NeedsEver = 1;
                }
                //Strength section
                if ((itemResponseType.Name == PCISEnum.ItemResponseType.Strength || itemResponseType.Name == PCISEnum.ItemResponseType.SupportResource) && itemResponseBehaviour.Name == PCISEnum.ToDo.Build)
                {
                    personQuestionnaireMetrics.StrengthsEver = 1;
                }
            }
            return personQuestionnaireMetrics;
        }

        private static List<AssessmentResponsesDTO> AssessmentResponseForeachItems(Utility Utility, string apiurl, long personId, int assessmentId)
        {
            try
            {
                List<AssessmentResponsesDTO> response = new List<AssessmentResponsesDTO>();
                var url = apiurl + PCISEnum.APIurl.GetAssessmentResponseForeachItems.Replace(PCISEnum.APIReplacableValues.PersonId, Convert.ToString(personId))
                    .Replace(PCISEnum.APIReplacableValues.AssessmentId, Convert.ToString(assessmentId));
                var result = Utility.RestApiCall(url, false);
                if (!string.IsNullOrEmpty(result))
                {
                    var responseresult = JsonConvert.DeserializeObject<AssessmentResponsesDetailsDTO>(result);
                    response = responseresult?.result?.AssessmentResponses;
                }
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static List<QuestionnaireItemsDTO> GetQuestionnaireItems(Utility Utility, string apiurl, int questionnaireId)
        {
            try
            {
                List<QuestionnaireItemsDTO> response = new List<QuestionnaireItemsDTO>();
                var url = apiurl + PCISEnum.APIurl.GetQuestionnaireItems.Replace(PCISEnum.APIReplacableValues.QuestionnaireId, Convert.ToString(questionnaireId));
                var result = Utility.RestApiCall(url, false);
                if (!string.IsNullOrEmpty(result))
                {
                    var responseresult = JsonConvert.DeserializeObject<QuestionnaireItemsResponseDTO>(result);
                    response = responseresult?.result?.QuestionnaireItemsForDashBoard;
                }
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static List<PersonQuestionnaireDTO> GetPersonQuestionnaireByID(Utility Utility, string apiurl, long personQuestionnaireId)
        {
            try
            {
                List<PersonQuestionnaireDTO> PersonQuestionnaire = new List<PersonQuestionnaireDTO>();
                var url = apiurl + PCISEnum.APIurl.PersonQuestionnaireList.Replace(PCISEnum.APIReplacableValues.PersonQuestionnaireId, Convert.ToString(personQuestionnaireId));
                var result = Utility.RestApiCall(url, false);
                if (!string.IsNullOrEmpty(result))
                {
                    var PersonQuestionnaireResponse = JsonConvert.DeserializeObject<PersonQuestionnaireDetailsDTo>(result);
                    PersonQuestionnaire = PersonQuestionnaireResponse?.result?.PersonQuestionnaire;
                }
                return PersonQuestionnaire;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static List<AssessmentResponsesDTO> GetAssessmentResponse(Utility Utility, string apiurl, int assessmentId)
        {
            try
            {
                List<AssessmentResponsesDTO> assessmentResponse = new List<AssessmentResponsesDTO>();
                var url = apiurl + PCISEnum.APIurl.AssessmentResponseByAssessmentID.Replace(PCISEnum.APIReplacableValues.AssessmentId, Convert.ToString(assessmentId));
                var result = Utility.RestApiCall(url, false);
                if (!string.IsNullOrEmpty(result))
                {
                    var assessmentResponses = JsonConvert.DeserializeObject<AssessmentResponsesDetailsDTO>(result);
                    assessmentResponse = assessmentResponses?.result?.AssessmentResponses;
                }
                return assessmentResponse;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static List<ItemResponseTypeDTO> GetItemResponseType(Utility Utility, string apiurl)
        {
            try
            {
                List<ItemResponseTypeDTO> Response = new List<ItemResponseTypeDTO>();
                var url = apiurl + PCISEnum.APIurl.AllItemResponseTypes;
                var result = Utility.RestApiCall(url, false);
                if (!string.IsNullOrEmpty(result))
                {
                    var TotalResponse = JsonConvert.DeserializeObject<ItemResponseTypeResponseDTO>(result);
                    Response = TotalResponse.result.ItemResponseType;
                }
                return Response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static List<ItemResponseBehaviorDTO> GetItemResponseBehaviors(Utility Utility, string apiurl)
        {
            try
            {
                List<ItemResponseBehaviorDTO> Response = new List<ItemResponseBehaviorDTO>();
                var url = apiurl + PCISEnum.APIurl.AllItemResponseBehaviours;
                var result = Utility.RestApiCall(url, false);
                if (!string.IsNullOrEmpty(result))
                {
                    var TotalResponse = JsonConvert.DeserializeObject<ItemResponseBehaviorResponseDTO>(result);
                    Response = TotalResponse.result.ItemResponseBehavior;
                }
                return Response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static List<PersonQuestionnaireMetricsDTO> GetPersonQuestionnaireMetrics(Utility Utility, string apiurl, long personId, List<int> itemId)
        {
            try
            {
                MetricsInputDTO obj = new MetricsInputDTO();
                obj.personId = personId;
                obj.itemIds = itemId;
                List<PersonQuestionnaireMetricsDTO> Response = new List<PersonQuestionnaireMetricsDTO>();
                var url = apiurl + PCISEnum.APIurl.GetPersonQuestionnireMetrics;
                var result = Utility.RestApiCall(url, false, false, PCISEnum.APIMethodType.PostRequest, null, obj.ToJSON());
                if (!string.IsNullOrEmpty(result))
                {
                    var TotalResponse = JsonConvert.DeserializeObject<PersonQuestionnaireMetricsDetailsDTO>(result);
                    Response = TotalResponse.result.PersonQuestionnaireMetrics;
                }
                return Response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static CRUDResponse UpdatePersonQuestionnaireMetrics(Utility Utility, string apiurl, List<PersonQuestionnaireMetricsDTO> personQuestionnaireMetrics)
        {
            try
            {
                CRUDResponse Response = new CRUDResponse();
                var url = apiurl + PCISEnum.APIurl.PersonQuestionnireMetrics;
                var result = Utility.RestApiCall(url, false, false, PCISEnum.APIMethodType.PutRequest, null, personQuestionnaireMetrics.ToJSON());
                if (!string.IsNullOrEmpty(result))
                {
                    var TotalResponse = JsonConvert.DeserializeObject<CRUDResponseDTO>(result);
                    Response = TotalResponse.result;
                }
                return Response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static CRUDResponse AddPersonQuestionnaireMetrics(Utility Utility, string apiurl, List<PersonQuestionnaireMetricsDTO> personQuestionnaireMetrics)
        {
            try
            {
                CRUDResponse Response = new CRUDResponse();
                var url = apiurl + PCISEnum.APIurl.PersonQuestionnireMetrics;
                var result = Utility.RestApiCall(url, false, false, PCISEnum.APIMethodType.PostRequest, null, personQuestionnaireMetrics.ToJSON());
                if (!string.IsNullOrEmpty(result))
                {
                    var TotalResponse = JsonConvert.DeserializeObject<CRUDResponseDTO>(result);
                    Response = TotalResponse.result;
                }
                return Response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static AssessmentDTO GetAssessmentByID(Utility Utility, string apiurl, int assessmentId, ILogger log, string myQueueItem)
        {
            try
            {
                AssessmentDTO Assessment = new AssessmentDTO();
                var url = apiurl + PCISEnum.APIurl.GetAssessmentById.Replace(PCISEnum.APIReplacableValues.AssessmentId, Convert.ToString(assessmentId));
                log.LogInformation($" DashboardMatrixCalculationProcess : Queue Item : {myQueueItem} : GetAssessmentByID url : {url}");
                var result = Utility.RestApiCall(url, false);
                if (!string.IsNullOrEmpty(result))
                {
                    var AssessmentResponse = JsonConvert.DeserializeObject<AssessmentResponseDetailsDTO>(result);
                    Assessment = AssessmentResponse?.result?.Assessment;
                }
                return Assessment;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #region PopulatePersonAssessmentMetricsInDetailForDashboard
        private static void PopulatePersonAssessmentMetricsInDetail(string myQueueItem, PersonQuestionnaireDTO personQuestionnaire, AssessmentDTO assessment, List<AssessmentResponsesDTO> assessmentResponse, List<AssessmentResponsesDTO> assessmentResponseValues, List<QuestionnaireItemsDTO> questionnaireItems, List<ItemResponseTypeDTO> itemResponseTypes, List<ItemResponseBehaviorDTO> itemResponseBehaviours, List<PersonAssessmentMetricsDTO> existingMetrics, Utility utility, string apiBaseUrl, ILogger log)
        {
            try
            {
                //To fetch the assessmentResponseValues for Items in current Assessment-QuestionnaireID
                List<int> questionnaireItemIDs = questionnaireItems.Select(x => x.QuestionnaireItemID).ToList();
                assessmentResponseValues = assessmentResponseValues.Where(x => questionnaireItemIDs.Contains(x.QuestionnaireItemID)).ToList();

                //To fetch the assessment in curently in queue and its previous assessments only
                assessmentResponseValues = assessmentResponseValues.Where(x => x.DateTaken <= assessment.DateTaken).ToList();
                List<PersonAssessmentMetricsDTO> personQuestionnairreMetricsListToAdd = new List<PersonAssessmentMetricsDTO>();
                List<PersonAssessmentMetricsDTO> personQuestionnairreMetricsListToUpdate = new List<PersonAssessmentMetricsDTO>();
                if (assessment.IsRemoved)
                {
                    if (assessmentResponse != null)
                    {
                        foreach (var response in assessmentResponse)
                        {
                            log.LogInformation($" DashboardMatrixCalculationProcess : DELETED : Queue Item : {myQueueItem} : assessmentResponseID : {response.AssessmentResponseID}");

                            var itemId = questionnaireItems.Where(x => x.QuestionnaireItemID == response.QuestionnaireItemID).Select(y => y.ItemID).FirstOrDefault();
                            var Assessmentvalues = assessmentResponseValues.Where(x => x.ItemId == itemId).ToList();
                            var lastAssessmentValue = assessmentResponseValues.Where(x => x.ItemId == itemId)?.Skip(0)?.Take(1)?.FirstOrDefault();
                            var secondlastAssessmentValue = assessmentResponseValues.Where(x => x.ItemId == itemId)?.Skip(1)?.Take(1)?.FirstOrDefault();
                            if (lastAssessmentValue != null && secondlastAssessmentValue != null)
                            {
                                log.LogInformation($" DashboardMatrixCalculationProcess : DELETED : Queue Item : {myQueueItem} : assessmentResponseID : {response.AssessmentResponseID} : More than one assessment exist");
                                var secondLastItemResponseBehaviour = itemResponseBehaviours.Where(x => x.ItemResponseBehaviorID == (secondlastAssessmentValue.ItemResponseBehaviorID ?? default(int))).FirstOrDefault();
                                ItemResponseTypeDTO secondLastItemResponseType = new ItemResponseTypeDTO();
                                if (secondLastItemResponseBehaviour != null && (secondLastItemResponseBehaviour.ItemResponseTypeID == 1 || secondLastItemResponseBehaviour.ItemResponseTypeID == 2
                                    || secondLastItemResponseBehaviour.ItemResponseTypeID == 5 || secondLastItemResponseBehaviour.ItemResponseTypeID == 6))
                                {
                                    secondLastItemResponseType = itemResponseTypes.Where(x => x.ItemResponseTypeID == secondLastItemResponseBehaviour.ItemResponseTypeID).FirstOrDefault();
                                }
                                else
                                {
                                    secondLastItemResponseType = null;
                                }
                                PersonQuestionnaireMetricsDTO personQuestionnaireMetrics = new PersonQuestionnaireMetricsDTO();

                                var existingPersonQuestionnaireMatrics = existingMetrics.Where(x => x.ItemID == itemId).ToList();
                                var lastItemResponseBehaviour = itemResponseBehaviours.Where(x => x.ItemResponseBehaviorID == (lastAssessmentValue.ItemResponseBehaviorID ?? default(int))).FirstOrDefault();
                                ItemResponseTypeDTO lastItemResponseType = new ItemResponseTypeDTO();
                                if (lastItemResponseBehaviour != null && (lastItemResponseBehaviour.ItemResponseTypeID == 1 || lastItemResponseBehaviour.ItemResponseTypeID == 2 ||
                                    lastItemResponseBehaviour.ItemResponseTypeID == 5 || lastItemResponseBehaviour.ItemResponseTypeID == 6))
                                {
                                    lastItemResponseType = itemResponseTypes.Where(x => x.ItemResponseTypeID == lastItemResponseBehaviour.ItemResponseTypeID).FirstOrDefault();
                                }
                                else
                                {
                                    lastItemResponseType = null;
                                }
                                PersonQuestionnaireMetricsDTO personQuestionnaireMetrics1 = CreateObjectForPersonMetrix(existingPersonQuestionnaireMatrics.FirstOrDefault());
                                personQuestionnaireMetrics = PopulatePersonQuestionnaireMetrics(lastItemResponseBehaviour, lastItemResponseType, secondLastItemResponseBehaviour, secondLastItemResponseType, personQuestionnaireMetrics1);
                                var listFocusItems = itemResponseBehaviours.Where(x => (x.ItemResponseTypeID == 1 || x.ItemResponseTypeID == 5) && x.Name == PCISEnum.ToDo.Focus).ToList();
                                if (listFocusItems.Where(x => Assessmentvalues.Select(x => x.ItemResponseBehaviorID).ToList().Contains(x.ItemResponseBehaviorID))?.FirstOrDefault() == null)
                                {
                                    personQuestionnaireMetrics.NeedsEver = 0;
                                }

                                var listBuildItems = itemResponseBehaviours.Where(x => (x.ItemResponseTypeID == 2 || x.ItemResponseTypeID == 6) && x.Name == PCISEnum.ToDo.Build).ToList();
                                if (listBuildItems.Where(x => Assessmentvalues.Select(x => x.ItemResponseBehaviorID).ToList().Contains(x.ItemResponseBehaviorID))?.FirstOrDefault() == null)
                                {
                                    personQuestionnaireMetrics.StrengthsEver = 0;
                                }
                                Assessmentvalues.Select(x => x.ItemResponseBehaviorID).ToList();

                                personQuestionnaireMetrics = SetNeedEverAndStrengthBuild(personQuestionnaireMetrics, Assessmentvalues, itemResponseBehaviours);
                                personQuestionnaireMetrics.InstrumentID = secondlastAssessmentValue.InstrumentID;
                                personQuestionnaireMetrics.ItemID = itemId;
                                personQuestionnaireMetrics.PersonID = personQuestionnaire.PersonID;
                                personQuestionnaireMetrics.PersonQuestionnaireID = Convert.ToInt32(personQuestionnaire.PersonQuestionnaireID);
                                if (existingPersonQuestionnaireMatrics == null || existingPersonQuestionnaireMatrics.Count == 0)
                                {
                                    var personQuestionnaireMetricsInDetail = CreateObjectForAssessmentMetrics(personQuestionnaireMetrics, assessment.AssessmentID, personQuestionnaire.QuestionnaireID);
                                    personQuestionnairreMetricsListToAdd.Add(personQuestionnaireMetricsInDetail);
                                }
                                else
                                {
                                    foreach (var metrics in existingPersonQuestionnaireMatrics)
                                    {
                                        var personQuestionnaireMetricsInDetail = CreateObjectForAssessmentMetrics(personQuestionnaireMetrics, assessment.AssessmentID, personQuestionnaire.QuestionnaireID);
                                        personQuestionnaireMetricsInDetail.PersonAssessmentMetricsID = metrics.PersonAssessmentMetricsID;
                                        personQuestionnairreMetricsListToUpdate.Add(personQuestionnaireMetricsInDetail);
                                    }
                                }
                            }
                            else if (lastAssessmentValue != null && secondlastAssessmentValue == null)
                            {
                                log.LogInformation($" DashboardMatrixCalculationProcess : DELETED : Queue Item : {myQueueItem} : assessmentResponseID : {response.AssessmentResponseID}");
                                var currentItemResponseBehaviour = itemResponseBehaviours.Where(x => x.ItemResponseBehaviorID == (lastAssessmentValue.ItemResponseBehaviorID ?? default(int))).FirstOrDefault();
                                ItemResponseTypeDTO currentItemResponseType = new ItemResponseTypeDTO();
                                if (currentItemResponseBehaviour != null && (currentItemResponseBehaviour.ItemResponseTypeID == 1 || currentItemResponseBehaviour.ItemResponseTypeID == 2
                                    || currentItemResponseBehaviour.ItemResponseTypeID == 5 || currentItemResponseBehaviour.ItemResponseTypeID == 6))
                                {
                                    currentItemResponseType = itemResponseTypes.Where(x => x.ItemResponseTypeID == currentItemResponseBehaviour.ItemResponseTypeID).FirstOrDefault();
                                }
                                else
                                {
                                    currentItemResponseType = null;
                                }
                                PersonQuestionnaireMetricsDTO personQuestionnaireMetrics = new PersonQuestionnaireMetricsDTO();
                                var existingPersonQuestionnaireMatrics = existingMetrics.Where(x => x.ItemID == itemId).ToList();

                                personQuestionnaireMetrics = PopulatePersonQuestionnaireMetrics(currentItemResponseBehaviour, currentItemResponseType, null, null, null);

                                var listFocusItems = itemResponseBehaviours.Where(x => (x.ItemResponseTypeID == 1 || x.ItemResponseTypeID == 5) && x.Name == PCISEnum.ToDo.Focus).ToList();
                                if (listFocusItems.Where(x => Assessmentvalues.Select(x => x.ItemResponseBehaviorID).ToList().Contains(x.ItemResponseBehaviorID))?.FirstOrDefault() == null)
                                {
                                    personQuestionnaireMetrics.NeedsEver = 0;
                                }

                                var listBuildItems = itemResponseBehaviours.Where(x => (x.ItemResponseTypeID == 2 || x.ItemResponseTypeID == 6) && x.Name == PCISEnum.ToDo.Build).ToList();
                                if (listBuildItems.Where(x => Assessmentvalues.Select(x => x.ItemResponseBehaviorID).ToList().Contains(x.ItemResponseBehaviorID))?.FirstOrDefault() == null)
                                {
                                    personQuestionnaireMetrics.StrengthsEver = 0;
                                }
                                personQuestionnaireMetrics = SetNeedEverAndStrengthBuild(personQuestionnaireMetrics, Assessmentvalues, itemResponseBehaviours);
                                personQuestionnaireMetrics.InstrumentID = lastAssessmentValue.InstrumentID;
                                personQuestionnaireMetrics.ItemID = itemId;
                                personQuestionnaireMetrics.PersonID = personQuestionnaire.PersonID;
                                personQuestionnaireMetrics.PersonQuestionnaireID = Convert.ToInt32(personQuestionnaire.PersonQuestionnaireID);
                                if (existingPersonQuestionnaireMatrics == null || existingPersonQuestionnaireMatrics.Count == 0)
                                {
                                    var personQuestionnaireMetricsInDetail = CreateObjectForAssessmentMetrics(personQuestionnaireMetrics, assessment.AssessmentID, personQuestionnaire.QuestionnaireID);
                                    personQuestionnairreMetricsListToAdd.Add(personQuestionnaireMetricsInDetail);
                                }
                                else
                                {
                                    foreach (var metrics in existingPersonQuestionnaireMatrics)
                                    {
                                        var personQuestionnaireMetricsInDetail = CreateObjectForAssessmentMetrics(personQuestionnaireMetrics, assessment.AssessmentID, personQuestionnaire.QuestionnaireID);
                                        personQuestionnaireMetricsInDetail.PersonAssessmentMetricsID = metrics.PersonAssessmentMetricsID;
                                        personQuestionnairreMetricsListToUpdate.Add(personQuestionnaireMetricsInDetail);
                                    }
                                }
                            }
                            else
                            {
                                var existingPersonQuestionnaireMatrics = existingMetrics.Where(x => x.ItemID == itemId).ToList();

                                foreach (var metrics in existingPersonQuestionnaireMatrics)
                                {
                                    metrics.NeedsAddressed = 0;
                                    metrics.NeedsAddressing = 0;
                                    metrics.NeedsEver = 0;
                                    metrics.NeedsIdentified = 0;
                                    metrics.NeedsImproved = 0;
                                    metrics.StrengthsBuilt = 0;
                                    metrics.StrengthsBuilding = 0;
                                    metrics.StrengthsEver = 0;
                                    metrics.StrengthsIdentified = 0;
                                    metrics.StrengthsImproved = 0;
                                    personQuestionnairreMetricsListToUpdate.Add(metrics);
                                }
                            }

                            log.LogInformation($" DashboardMatrixCalculationProcess : DELETED : Queue Item : {myQueueItem} : count to add : {personQuestionnairreMetricsListToAdd.Count}");
                            log.LogInformation($" DashboardMatrixCalculationProcess : DELETED : Queue Item : {myQueueItem} : count to update : {personQuestionnairreMetricsListToUpdate.Count}");

                            if (personQuestionnairreMetricsListToAdd.Count > 0)
                                AddPersonQuestionnaireMetricsInDetail(utility, apiBaseUrl, personQuestionnairreMetricsListToAdd, PCISEnum.APIMethodType.PostRequest);

                            if (personQuestionnairreMetricsListToUpdate.Count > 0)
                                AddPersonQuestionnaireMetricsInDetail(utility, apiBaseUrl, personQuestionnairreMetricsListToUpdate, PCISEnum.APIMethodType.PutRequest);
                        }
                    }
                }
                else
                {
                    var otherAssessment = new AssessmentDTO();
                    if (assessmentResponse != null)
                    {
                        foreach (var response in assessmentResponse)
                        {
                            log.LogInformation($" DashboardMatrixCalculationProcess : Queue Item : {myQueueItem} : assessmentResponseID : {response.AssessmentResponseID}");

                            var itemId = questionnaireItems.Where(x => x.QuestionnaireItemID == response.QuestionnaireItemID).Select(y => y.ItemID).FirstOrDefault();
                            var Assessmentvalues = assessmentResponseValues.Where(x => x.ItemId == itemId).ToList();
                            var lastAssessmentValue = assessmentResponseValues.Where(x => x.ItemId == itemId)?.Skip(0)?.Take(1)?.FirstOrDefault();
                            var secondlastAssessmentValue = assessmentResponseValues.Where(x => x.ItemId == itemId)?.Skip(1)?.Take(1)?.FirstOrDefault();
                            if (response.AssessmentID == lastAssessmentValue?.AssessmentID || response.AssessmentID == secondlastAssessmentValue?.AssessmentID)
                            {
                                log.LogInformation($" DashboardMatrixCalculationProcess : Queue Item : {myQueueItem} : current asseessment in last two assessment");

                                if (secondlastAssessmentValue != null)
                                {
                                    var secondLastItemResponseBehaviour = itemResponseBehaviours.Where(x => x.ItemResponseBehaviorID == (secondlastAssessmentValue.ItemResponseBehaviorID ?? default(int))).FirstOrDefault();
                                    ItemResponseTypeDTO secondLastItemResponseType = new ItemResponseTypeDTO();
                                    if (secondLastItemResponseBehaviour != null && (secondLastItemResponseBehaviour.ItemResponseTypeID == 1 || secondLastItemResponseBehaviour.ItemResponseTypeID == 2
                                        || secondLastItemResponseBehaviour.ItemResponseTypeID == 5 || secondLastItemResponseBehaviour.ItemResponseTypeID == 6))
                                    {
                                        secondLastItemResponseType = itemResponseTypes.Where(x => x.ItemResponseTypeID == secondLastItemResponseBehaviour.ItemResponseTypeID).FirstOrDefault();
                                    }
                                    else
                                    {
                                        secondLastItemResponseType = null;
                                    }
                                    PersonQuestionnaireMetricsDTO personQuestionnaireMetrics = new PersonQuestionnaireMetricsDTO();

                                    var existingPersonQuestionnaireMatrics = existingMetrics?.Where(x => x.ItemID == itemId).ToList();
                                    var lastItemResponseBehaviour = itemResponseBehaviours.Where(x => x.ItemResponseBehaviorID == (lastAssessmentValue.ItemResponseBehaviorID ?? default(int))).FirstOrDefault();
                                    ItemResponseTypeDTO lastItemResponseType = new ItemResponseTypeDTO();
                                    if (lastItemResponseBehaviour != null && (lastItemResponseBehaviour.ItemResponseTypeID == 1 || lastItemResponseBehaviour.ItemResponseTypeID == 2 ||
                                        lastItemResponseBehaviour.ItemResponseTypeID == 5 || lastItemResponseBehaviour.ItemResponseTypeID == 6))
                                    {
                                        lastItemResponseType = itemResponseTypes.Where(x => x.ItemResponseTypeID == lastItemResponseBehaviour.ItemResponseTypeID).FirstOrDefault();
                                    }
                                    else
                                    {
                                        lastItemResponseType = null;
                                    }
                                    PersonQuestionnaireMetricsDTO personQuestionnaireMetrics1 = CreateObjectForPersonMetrix(existingPersonQuestionnaireMatrics?.FirstOrDefault());
                                    personQuestionnaireMetrics = PopulatePersonQuestionnaireMetrics(lastItemResponseBehaviour, lastItemResponseType, secondLastItemResponseBehaviour, secondLastItemResponseType, personQuestionnaireMetrics1);

                                    personQuestionnaireMetrics = SetNeedEverAndStrengthBuild(personQuestionnaireMetrics, Assessmentvalues, itemResponseBehaviours);
                                    personQuestionnaireMetrics.InstrumentID = secondlastAssessmentValue.InstrumentID;
                                    personQuestionnaireMetrics.ItemID = itemId;
                                    personQuestionnaireMetrics.PersonID = personQuestionnaire.PersonID;
                                    personQuestionnaireMetrics.PersonQuestionnaireID = Convert.ToInt32(personQuestionnaire.PersonQuestionnaireID);
                                    if (existingPersonQuestionnaireMatrics == null || existingPersonQuestionnaireMatrics.Count == 0)
                                    {
                                        var personQuestionnaireMetricsInDetail = CreateObjectForAssessmentMetrics(personQuestionnaireMetrics, assessment.AssessmentID, personQuestionnaire.QuestionnaireID);
                                        personQuestionnairreMetricsListToAdd.Add(personQuestionnaireMetricsInDetail);
                                    }
                                    else
                                    {
                                        foreach (var metrics in existingPersonQuestionnaireMatrics)
                                        {
                                            var personQuestionnaireMetricsInDetail = CreateObjectForAssessmentMetrics(personQuestionnaireMetrics, assessment.AssessmentID, personQuestionnaire.QuestionnaireID);
                                            personQuestionnaireMetricsInDetail.PersonAssessmentMetricsID = metrics.PersonAssessmentMetricsID;
                                            personQuestionnairreMetricsListToUpdate.Add(personQuestionnaireMetricsInDetail);
                                        }
                                    }

                                }
                                else
                                {
                                    var currentItemResponseBehaviour = itemResponseBehaviours.Where(x => x.ItemResponseBehaviorID == (lastAssessmentValue.ItemResponseBehaviorID ?? default(int))).FirstOrDefault();
                                    ItemResponseTypeDTO currentItemResponseType = new ItemResponseTypeDTO();
                                    if (currentItemResponseBehaviour != null && (currentItemResponseBehaviour.ItemResponseTypeID == 1 || currentItemResponseBehaviour.ItemResponseTypeID == 2
                                        || currentItemResponseBehaviour.ItemResponseTypeID == 5 || currentItemResponseBehaviour.ItemResponseTypeID == 6))
                                    {
                                        currentItemResponseType = itemResponseTypes.Where(x => x.ItemResponseTypeID == currentItemResponseBehaviour.ItemResponseTypeID).FirstOrDefault();
                                    }
                                    else
                                    {
                                        currentItemResponseType = null;
                                    }
                                    PersonQuestionnaireMetricsDTO personQuestionnaireMetrics = new PersonQuestionnaireMetricsDTO();

                                    personQuestionnaireMetrics = PopulatePersonQuestionnaireMetrics(currentItemResponseBehaviour, currentItemResponseType, null, null, null);
                                    personQuestionnaireMetrics = SetNeedEverAndStrengthBuild(personQuestionnaireMetrics, Assessmentvalues, itemResponseBehaviours);
                                    personQuestionnaireMetrics.InstrumentID = lastAssessmentValue.InstrumentID;
                                    personQuestionnaireMetrics.ItemID = itemId;
                                    personQuestionnaireMetrics.PersonID = personQuestionnaire.PersonID;
                                    personQuestionnaireMetrics.PersonQuestionnaireID = Convert.ToInt32(personQuestionnaire.PersonQuestionnaireID);
                                    var personQuestionnaireMetricsInDetail = CreateObjectForAssessmentMetrics(personQuestionnaireMetrics, assessment.AssessmentID, personQuestionnaire.QuestionnaireID);
                                    personQuestionnairreMetricsListToAdd.Add(personQuestionnaireMetricsInDetail);
                                }
                            }
                            else
                            {
                                log.LogInformation($" DashboardMatrixCalculationProcess : Queue Item : {myQueueItem} : current asseessment is not in last two assessment");

                                var existingPersonQuestionnaireMatrics = existingMetrics.Where(x => x.ItemID == itemId).ToList();
                                var itemResponseBehaviour = itemResponseBehaviours.Where(x => x.ItemResponseBehaviorID == (response.ItemResponseBehaviorID ?? default(int))).FirstOrDefault();
                                ItemResponseTypeDTO itemResponseType = new ItemResponseTypeDTO();
                                if (itemResponseBehaviour != null && (itemResponseBehaviour.ItemResponseTypeID == 1 || itemResponseBehaviour.ItemResponseTypeID == 2
                                    || itemResponseBehaviour.ItemResponseTypeID == 5 || itemResponseBehaviour.ItemResponseTypeID == 6))
                                {
                                    itemResponseType = itemResponseTypes.Where(x => x.ItemResponseTypeID == itemResponseBehaviour.ItemResponseTypeID).FirstOrDefault();
                                }
                                else
                                {
                                    itemResponseType = null;
                                }

                                foreach (var metrics in existingPersonQuestionnaireMatrics)
                                {
                                    PersonQuestionnaireMetricsDTO personMetrics1 = CreateObjectForPersonMetrix(metrics);
                                    PersonQuestionnaireMetricsDTO personQuestionnaireMetrics = new PersonQuestionnaireMetricsDTO();
                                    personQuestionnaireMetrics = PopulateOldPersonQuestionnaireMetrics(personMetrics1, itemResponseBehaviour, itemResponseType);
                                    if (personQuestionnaireMetrics != null)
                                    {
                                        var newPersonQuestionnaireMetrics = CreateObjectForAssessmentMetrics(personQuestionnaireMetrics, assessment.AssessmentID, personQuestionnaire.QuestionnaireID);
                                        personQuestionnairreMetricsListToUpdate.Add(newPersonQuestionnaireMetrics);
                                    }
                                }
                            }
                        }

                        log.LogInformation($" DashboardMatrixCalculationProcess : Queue Item : {myQueueItem} : count to add : {personQuestionnairreMetricsListToAdd.Count}");
                        log.LogInformation($" DashboardMatrixCalculationProcess : Queue Item : {myQueueItem} : count to update : {personQuestionnairreMetricsListToUpdate.Count}");

                        if (personQuestionnairreMetricsListToAdd.Count > 0)
                            AddPersonQuestionnaireMetricsInDetail(utility, apiBaseUrl, personQuestionnairreMetricsListToAdd, PCISEnum.APIMethodType.PostRequest);

                        if (personQuestionnairreMetricsListToUpdate.Count > 0)
                            AddPersonQuestionnaireMetricsInDetail(utility, apiBaseUrl, personQuestionnairreMetricsListToUpdate, PCISEnum.APIMethodType.PutRequest);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private static PersonQuestionnaireMetricsDTO SetNeedEverAndStrengthBuild(PersonQuestionnaireMetricsDTO personQuestionnaireMetrics, List<AssessmentResponsesDTO> assessmentResponseValues, List<ItemResponseBehaviorDTO> itemResponseBehaviours)
        {
            try
            {
                var listFocusItems = itemResponseBehaviours.Where(x => (x.ItemResponseTypeID == 1 || x.ItemResponseTypeID == 5) && x.Name == PCISEnum.ToDo.Focus).ToList();
                var listBuildItems = itemResponseBehaviours.Where(x => (x.ItemResponseTypeID == 2 || x.ItemResponseTypeID == 6) && x.Name == PCISEnum.ToDo.Build).ToList();
                var focusBehaviorIDs = listFocusItems.Select(x => x.ItemResponseBehaviorID).ToList();
                if (assessmentResponseValues.Where(x => focusBehaviorIDs.Contains(x.ItemResponseBehaviorID ?? 0)).ToList().Count> 0)
                {
                    personQuestionnaireMetrics.NeedsEver = 1;
                }
                var buildBehaviorIDs = listBuildItems.Select(x => x.ItemResponseBehaviorID).ToList();
                if (assessmentResponseValues.Where(x => buildBehaviorIDs.Contains(x.ItemResponseBehaviorID ?? 0)).ToList().Count > 0)
                {
                    personQuestionnaireMetrics.StrengthsEver = 1;
                }
                return personQuestionnaireMetrics;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private static PersonQuestionnaireMetricsDTO CreateObjectForPersonMetrix(PersonAssessmentMetricsDTO personAssessmentMetricsDTO)
        {
            try
            {
                if (personAssessmentMetricsDTO == null)
                    return new PersonQuestionnaireMetricsDTO();
                PersonQuestionnaireMetricsDTO metrix = new PersonQuestionnaireMetricsDTO()
                {
                    PersonID = personAssessmentMetricsDTO.PersonID,
                    InstrumentID = personAssessmentMetricsDTO.InstrumentID,
                    ItemID = personAssessmentMetricsDTO.ItemID,
                    NeedsAddressed = personAssessmentMetricsDTO.NeedsAddressed,
                    NeedsAddressing = personAssessmentMetricsDTO.NeedsAddressing,
                    NeedsEver = personAssessmentMetricsDTO.NeedsEver,
                    NeedsIdentified = personAssessmentMetricsDTO.NeedsIdentified,
                    NeedsImproved = personAssessmentMetricsDTO.NeedsImproved,
                    PersonQuestionnaireID = personAssessmentMetricsDTO.PersonQuestionnaireID,
                    StrengthsBuilding = personAssessmentMetricsDTO.StrengthsBuilding,
                    StrengthsBuilt = personAssessmentMetricsDTO.StrengthsBuilt,
                    StrengthsEver = personAssessmentMetricsDTO.StrengthsEver,
                    StrengthsIdentified = personAssessmentMetricsDTO.StrengthsIdentified,
                    StrengthsImproved = personAssessmentMetricsDTO.StrengthsImproved,
                };
                return metrix;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private static PersonAssessmentMetricsDTO CreateObjectForAssessmentMetrics(PersonQuestionnaireMetricsDTO personQuestionnaireMetrics, int assessmentID, int questionnaireID)
        {
            try
            {
                PersonAssessmentMetricsDTO metrixInDetail = new PersonAssessmentMetricsDTO()
                {
                    PersonID = personQuestionnaireMetrics.PersonID,
                    InstrumentID = personQuestionnaireMetrics.InstrumentID,
                    ItemID = personQuestionnaireMetrics.ItemID,
                    NeedsAddressed = personQuestionnaireMetrics.NeedsAddressed,
                    NeedsAddressing = personQuestionnaireMetrics.NeedsAddressing,
                    NeedsEver = personQuestionnaireMetrics.NeedsEver,
                    NeedsIdentified = personQuestionnaireMetrics.NeedsIdentified,
                    NeedsImproved = personQuestionnaireMetrics.NeedsImproved,
                    PersonQuestionnaireID = personQuestionnaireMetrics.PersonQuestionnaireID,
                    StrengthsBuilding = personQuestionnaireMetrics.StrengthsBuilding,
                    StrengthsBuilt = personQuestionnaireMetrics.StrengthsBuilt,
                    StrengthsEver = personQuestionnaireMetrics.StrengthsEver,
                    StrengthsIdentified = personQuestionnaireMetrics.StrengthsIdentified,
                    StrengthsImproved = personQuestionnaireMetrics.StrengthsImproved,
                    AssessmentID = assessmentID,
                    UpdateDate = DateTime.UtcNow,
                    QuestionnaireID = questionnaireID
                };
                return metrixInDetail;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private static List<PersonAssessmentMetricsDTO> GetPersonAssessmentMetricsInDetail(Utility Utility, string apiurl, long personId, int assessmnetID, ILogger log)
        {
            try
            {
                MetricsInputDTO obj = new MetricsInputDTO();
                obj.personId = personId;
                obj.AssessmentID = assessmnetID;
                List<PersonAssessmentMetricsDTO> Response = new List<PersonAssessmentMetricsDTO>();
                var url = apiurl + PCISEnum.APIurl.GetPersonAssessmentMetrics;
                log.LogInformation($" DashboardMatrixCalculationProcess : Queue Item : {assessmnetID} : GetPersonAssessmentMetricsInDetail : {url}");
                var result = Utility.RestApiCall(url, false, false, PCISEnum.APIMethodType.PostRequest, null, obj.ToJSON());
                if (!string.IsNullOrEmpty(result))
                {
                    var TotalResponse = JsonConvert.DeserializeObject<PersonAssessmentMetricsDetailsDTO>(result);
                    Response = TotalResponse.result.PersonQuestionnaireMetrics;
                }
                return Response;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private static CRUDResponse AddPersonQuestionnaireMetricsInDetail(Utility Utility, string apiurl, List<PersonAssessmentMetricsDTO> personQuestionnaireMetrics, string requestType)
        {
            try
            {
                CRUDResponse Response = new CRUDResponse();
                var url = apiurl + PCISEnum.APIurl.PersonAssessmentMetrics;
                var result = Utility.RestApiCall(url, false, false, requestType, null, personQuestionnaireMetrics.ToJSON());
                if (!string.IsNullOrEmpty(result))
                {
                    var TotalResponse = JsonConvert.DeserializeObject<CRUDResponseDTO>(result);
                    Response = TotalResponse.result;
                }
                return Response;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
