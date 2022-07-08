    using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ReminderAutoResolveProcess.Common;
using ReminderAutoResolveProcess.DTO;
using ReminderAutoResolveProcess.Enums;

namespace ReminderAutoResolveProcess
{
    public static class ReminderAutoResolveProcess
    {
        [FunctionName("ReminderAutoResolveProcess")]
        public static void Run([QueueTrigger("resolveremindernotifications", Connection = "QueueStorageUrlFromKeyVault")]string myQueueItem, ILogger log)
        {
            log.LogInformation($" ReminderAutoResolveProcess : C# Queue trigger function processed: {myQueueItem}");
            try
            {
                Utility Utility = new Utility();
                var assessmentId = Convert.ToInt32(myQueueItem);
                List<QuestionnaireWindowsDTO> questionnaireWindowList = new List<QuestionnaireWindowsDTO>();
                log.LogInformation($" ReminderAutoResolveProcess : Queue Item : {myQueueItem}");
                var apiurl = Environment.GetEnvironmentVariable(PCISEnum.EnvironmentVariables.ApiUrlFromKeyVault, EnvironmentVariableTarget.Process);
                log.LogInformation($" ReminderAutoResolveProcess : Queue Item : {myQueueItem} : GetPersonQuestionnaireByID Start");
                var assessment = GetAssessmentByID(Utility, apiurl, assessmentId, log, myQueueItem);
                log.LogInformation($" ReminderAutoResolveProcess : Queue Item : {myQueueItem} : GetPersonQuestionnaireByID End");
                log.LogInformation($" ReminderAutoResolveProcess : Queue Item : {myQueueItem} : GetPersonQuestionnaireByID Start");
                var personQuestionnaireList = GetPersonQuestionnaireByID(Utility, apiurl, assessment.PersonQuestionnaireID, log, myQueueItem);
                log.LogInformation($" ReminderAutoResolveProcess : Queue Item : {myQueueItem} : GetPersonQuestionnaireByID End");

                if (personQuestionnaireList != null && personQuestionnaireList.Count > 0)
                {
                    log.LogInformation($" ReminderAutoResolveProcess : Queue Item : {myQueueItem} : GetQuestionnaireWindowsByQuestionnaire Start");
                    questionnaireWindowList = GetQuestionnaireWindowsByQuestionnaire(Utility, apiurl, personQuestionnaireList[0].QuestionnaireID, log, myQueueItem);
                    log.LogInformation($" ReminderAutoResolveProcess : Queue Item : {myQueueItem} : GetQuestionnaireWindowsByQuestionnaire End");

                };

                var questionnaireWindowListID = questionnaireWindowList.Select(x => x.QuestionnaireWindowID).ToList();

                foreach (var personQuestionnaire in personQuestionnaireList)
                {
                    if (personQuestionnaire != null && personQuestionnaire.CollaborationID > 0)
                    {
                        log.LogInformation($" ReminderAutoResolveProcess : Queue Item : {myQueueItem} : GetPersonCollaborationByPersonIdAndCollaborationId Start");
                        var personCollaboration = GetPersonCollaborationByPersonIdAndCollaborationId(Utility, apiurl, personQuestionnaire.PersonID, personQuestionnaire.CollaborationID, log, myQueueItem);
                        log.LogInformation($" ReminderAutoResolveProcess : Queue Item : {myQueueItem} : GetPersonCollaborationByPersonIdAndCollaborationId end");

                        if (personCollaboration != null)
                        {
                            personQuestionnaire.EndDueDate = personCollaboration.EndDate;
                            personQuestionnaire.StartDate = personCollaboration.EnrollDate ?? DateTime.UtcNow;
                        }
                    }
                    var dateTaken = assessment.DateTaken;

                    PersonQuestionnaireScheduleInputDTO personQuestionnaireSchedule = new PersonQuestionnaireScheduleInputDTO();
                    personQuestionnaireSchedule.DateTaken = dateTaken;
                    personQuestionnaireSchedule.PersonQuestionnaireID = personQuestionnaire.PersonQuestionnaireID;
                    personQuestionnaireSchedule.QuestionnaireWindowListID = questionnaireWindowListID;
                    log.LogInformation($" ReminderAutoResolveProcess : Queue Item : {myQueueItem} : GetPersonQuestionnaireScheduleList Start");
                    var personQuestionnaireScheduleList = GetPersonQuestionnaireScheduleList(Utility, apiurl, personQuestionnaireSchedule, log, myQueueItem);
                    log.LogInformation($" ReminderAutoResolveProcess : Queue Item : {myQueueItem} : GetPersonQuestionnaireScheduleList end");

                    if (personQuestionnaireScheduleList != null)
                    {
                        personQuestionnaireScheduleList.ForEach(x => x.IsRemoved = true);
                        log.LogInformation($" ReminderAutoResolveProcess : Queue Item : {myQueueItem} : UpdateBulkPersonQuestionnaireSchedule Start");
                        UpdateBulkPersonQuestionnaireSchedule(Utility, apiurl, personQuestionnaireScheduleList);
                        log.LogInformation($" ReminderAutoResolveProcess : Queue Item : {myQueueItem} : UpdateBulkPersonQuestionnaireSchedule end");

                        var personQuestionnaireScheduleIDList = personQuestionnaireScheduleList.Select(x => x.PersonQuestionnaireScheduleID).ToList();

                        if (personQuestionnaireScheduleIDList.Count > 0)
                        {
                            GetNotifyReminderInputDTO notifyReminderInput = new GetNotifyReminderInputDTO();
                            notifyReminderInput.personQuestionnaireScheduleIDList = personQuestionnaireScheduleIDList;
                            log.LogInformation($" ReminderAutoResolveProcess : Queue Item : {myQueueItem} : NotificationUpdate Start");
                            var notifyReminderIDList = NotificationUpdate(Utility, apiurl, notifyReminderInput);
                            log.LogInformation($" ReminderAutoResolveProcess : Queue Item : {myQueueItem} : NotificationUpdate end");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.LogError(ex, $@"ReminderAutoResolveProcess : Exception occurred.Exception Message : {ex.Message}.Location : {ex.StackTrace}.Details : {ex.InnerException}");
            }
        }

        private static CRUDResponse NotificationUpdate(Utility Utility, string apiurl, GetNotifyReminderInputDTO notifyReminderInput)
        {
            try
            {
                CRUDResponse NotifyReminder = new CRUDResponse();
                var url = apiurl + PCISEnum.APIurl.NotificationUpdate;
                var result = Utility.RestApiCall(url, false, false, PCISEnum.APIMethodType.PostRequest, null, notifyReminderInput.ToJSON());
                if (!string.IsNullOrEmpty(result))
                {
                    var NotifyReminderResponse = JsonConvert.DeserializeObject<CRUDResponseDTO>(result);
                    NotifyReminder = NotifyReminderResponse?.result;
                }
                return NotifyReminder;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static CRUDResponse UpdateBulkPersonQuestionnaireSchedule(Utility Utility, string apiurl, List<PersonQuestionnaireScheduleDTO> personQuestionnaireSchedules)
        {
            try
            {
                CRUDResponse Response = new CRUDResponse();
                var url = apiurl + PCISEnum.APIurl.PersonQuestionnaireSchedule;
                var result = Utility.RestApiCall(url, false, false, PCISEnum.APIMethodType.PutRequest, null, personQuestionnaireSchedules.ToJSON());
                if (!string.IsNullOrEmpty(result))
                {
                    var TotalResponse = JsonConvert.DeserializeObject<CRUDResponseDTO>(result);
                    Response = TotalResponse?.result;
                }
                return Response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static List<PersonQuestionnaireScheduleDTO> GetPersonQuestionnaireScheduleList(Utility Utility, string apiurl, PersonQuestionnaireScheduleInputDTO personQuestionnaireSchedule, ILogger log, string myQueueItem)
        {
            try
            {
                List<PersonQuestionnaireScheduleDTO> PersonQuestionnaireScheduleList = new List<PersonQuestionnaireScheduleDTO>();
                var url = apiurl + PCISEnum.APIurl.PersonQuestionnaireScheduleList;
                log.LogInformation($" ReminderAutoResolveProcess : Queue Item : {myQueueItem} : GetPersonCollaborationByPersonIdAndCollaborationId url : {url}");
                
                var result = Utility.RestApiCall(url, false,false,  PCISEnum.APIMethodType.PostRequest, null, personQuestionnaireSchedule.ToJSON());
                if (!string.IsNullOrEmpty(result))
                {
                    var PersonQuestionnaireScheduleListResponse = JsonConvert.DeserializeObject<PersonQuestionnaireScheduleDetailsDTO>(result);
                    PersonQuestionnaireScheduleList = PersonQuestionnaireScheduleListResponse?.result?.PersonQuestionnaireSchedules;
                }
                return PersonQuestionnaireScheduleList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static PersonCollaborationDTO GetPersonCollaborationByPersonIdAndCollaborationId(Utility Utility, string apiurl, long personId, int? collaborationId, ILogger log, string myQueueItem)
        {
            try
            {
                PersonCollaborationDTO PersonQuestionnaire = new PersonCollaborationDTO();
                var url = apiurl + PCISEnum.APIurl.PersonCollaborationByPersonIdAndCollaborationId.
                    Replace(PCISEnum.APIReplacableValues.PersonId, Convert.ToString(personId)).Replace(PCISEnum.APIReplacableValues.CollaborationId, Convert.ToString(collaborationId));
                log.LogInformation($" ReminderAutoResolveProcess : Queue Item : {myQueueItem} : GetPersonCollaborationByPersonIdAndCollaborationId url : {url}");

                var result = Utility.RestApiCall(url, false);
                if (!string.IsNullOrEmpty(result))
                {
                    var PersonQuestionnaireResponse = JsonConvert.DeserializeObject<PersonCollaborationDetailsDTO>(result);
                    PersonQuestionnaire = PersonQuestionnaireResponse?.result?.PersonCollaboration;
                }
                return PersonQuestionnaire;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static List<QuestionnaireWindowsDTO> GetQuestionnaireWindowsByQuestionnaire(Utility Utility, string apiurl, int QuestionnaireId, ILogger log, string myQueueItem)
        {
            try
            {
                List<QuestionnaireWindowsDTO> QuestionnaireWindow = new List<QuestionnaireWindowsDTO>();
                var url = apiurl + PCISEnum.APIurl.QuestionnaireWindowsByQuestionnaire.Replace(PCISEnum.APIReplacableValues.QuestionnaireId, Convert.ToString(QuestionnaireId));
                log.LogInformation($" ReminderAutoResolveProcess : Queue Item : {myQueueItem} :GetQuestionnaireWindowsByQuestionnaire url : {url}");
                var result = Utility.RestApiCall(url, false);
                if (!string.IsNullOrEmpty(result))
                {
                    var QuestionnaireWindowResponse = JsonConvert.DeserializeObject<QuestionnaireWindowDetailsDTO>(result);
                    QuestionnaireWindow = QuestionnaireWindowResponse?.result?.QuestionnaireWindow;
                }
                return QuestionnaireWindow;
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
                log.LogInformation($" ReminderAutoResolveProcess : Queue Item : {myQueueItem} : GetAssessmentByID url : {url}");
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
        private static List<PersonQuestionnaireDTO> GetPersonQuestionnaireByID(Utility Utility, string apiurl, long personQuestionnaireId, ILogger log, string myQueueItem)
        {
            try
            {
                List<PersonQuestionnaireDTO> PersonQuestionnaire = new List<PersonQuestionnaireDTO>();
                var url = apiurl + PCISEnum.APIurl.PersonQuestionnaireList.Replace(PCISEnum.APIReplacableValues.PersonQuestionnaireId, Convert.ToString(personQuestionnaireId));
                log.LogInformation($" ReminderAutoResolveProcess : Queue Item : {myQueueItem} : GetPersonQuestionnaireByID url : {url}");
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

    }
}
