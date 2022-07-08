using System;
using ReminderNotificationProcess.Common;
using ReminderNotificationProcess.Enums;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ReminderNotificationProcess.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

namespace ReminderNotificationProcess
{
    public static class ReminderNotificationProcess
    {
        [FunctionName("ReminderNotificationProcess")]
        public static void Run([QueueTrigger("assessmentremindernotification", Connection = "QueueStorageUrlFromKeyVault")]string myQueueItem, ILogger log)
        {
            log.LogInformation($" ReminderNotificationProcess : C# Queue trigger function processed: {myQueueItem}");
            try
            {
                Utility Utility = new Utility();
                var personQuestionnaireId = Convert.ToInt64(myQueueItem);
                log.LogInformation($" ReminderNotificationProcess : Queue Item : {myQueueItem}");
                var apiurl = Environment.GetEnvironmentVariable(PCISEnum.EnvironmentVariables.ApiUrlFromKeyVault, EnvironmentVariableTarget.Process);
                log.LogInformation($" ReminderNotificationProcess : Queue Item : {myQueueItem} : GetPersonQuestionnaireByID Start");

                var questionnaireLateCountLimit = Convert.ToInt32(GetConfiguration(Utility, apiurl, PCISEnum.ConfigurationKey.Reminder_Count_After_WindowCloseDay, log));

                var limitAfterWindowClose = Convert.ToInt32(GetConfiguration(Utility, apiurl, PCISEnum.ConfigurationKey.Reminder_LimitInMonth_If_EndDate_Null, log));

                var personQuestionnaire = GetPersonQuestionnaireByID(Utility, apiurl, personQuestionnaireId, log, myQueueItem);
                log.LogInformation($" ReminderNotificationProcess : Queue Item : {myQueueItem} : GetPersonQuestionnaireByID end");

                if (personQuestionnaire != null && personQuestionnaire.CollaborationID > 0)
                {
                    log.LogInformation($" ReminderNotificationProcess : Queue Item : {myQueueItem} : GetPersonCollaborationByPersonIdAndCollaborationId Start");

                    var personCollaboration = GetPersonCollaborationByPersonIdAndCollaborationId(Utility, apiurl, personQuestionnaire.PersonID, personQuestionnaire.CollaborationID, log, myQueueItem);
                    log.LogInformation($" ReminderNotificationProcess : Queue Item : {myQueueItem} : GetPersonCollaborationByPersonIdAndCollaborationId end");

                    if (personCollaboration != null)
                    {
                        personQuestionnaire.EndDueDate = personCollaboration.EndDate;
                        personQuestionnaire.StartDate = personCollaboration.EnrollDate ?? DateTime.UtcNow;
                    }
                }

                var questionnaireId = personQuestionnaire.QuestionnaireID;
                log.LogInformation($" ReminderNotificationProcess : Queue Item : {myQueueItem} : GetQuestionnaireWindowsByQuestionnaire Start");
                log.LogInformation($" ReminderNotificationProcess : Queue Item : {myQueueItem} : questionnaireID :{questionnaireId}");

                var questionnaireWindowList = GetQuestionnaireWindowsByQuestionnaire(Utility, apiurl, questionnaireId, log, myQueueItem);
                log.LogInformation($" ReminderNotificationProcess : Queue Item : {myQueueItem} : GetQuestionnaireWindowsByQuestionnaire end: count : {questionnaireWindowList?.Count} ");
                log.LogInformation($" ReminderNotificationProcess : Queue Item : {myQueueItem} : GetQuestionnaireReminderRulesByQuestionnaire Start");

                var questionnaireReminderRuleList = GetQuestionnaireReminderRulesByQuestionnaire(Utility, apiurl, questionnaireId, log, myQueueItem);
                log.LogInformation($" ReminderNotificationProcess : Queue Item : {myQueueItem} : GetQuestionnaireReminderRulesByQuestionnaire End : count : {questionnaireReminderRuleList.Count} ");
                log.LogInformation($" ReminderNotificationProcess : Queue Item : {myQueueItem} : GetAllAssessmentReason Start");

                var assessmentAllReason = GetAllAssessmentReason(Utility, apiurl, log, myQueueItem);
                log.LogInformation($" ReminderNotificationProcess : Queue Item : {myQueueItem} : GetAllAssessmentReason end: count : {assessmentAllReason.Count} ");
                log.LogInformation($" ReminderNotificationProcess : Queue Item : {myQueueItem} : GetQuestionnaireReminderType Start");
                var questionnaireReminderTypes = GetQuestionnaireReminderType(Utility, apiurl, log, myQueueItem);
                log.LogInformation($" ReminderNotificationProcess : Queue Item : {myQueueItem} : GetQuestionnaireReminderType end: count : {questionnaireReminderTypes.Count} ");

                var questionnaireWindowListForRemoval = GetUnSelectedQuestionnaireWindowsByQuestionnaire(Utility, apiurl, questionnaireId, log, myQueueItem);
                foreach(var item in questionnaireWindowListForRemoval)
                {
                    var personQuestionnaireScheduleObj = GetPersonQuestionnaireSchedule(Utility, apiurl, personQuestionnaire.PersonQuestionnaireID, item.QuestionnaireWindowID);
                    if (personQuestionnaireScheduleObj.Count > 0)
                    {
                        personQuestionnaireScheduleObj.ForEach(x => x.IsRemoved = true);
                        UpdateBulkPersonQuestionnaireSchedule(Utility, apiurl, personQuestionnaireScheduleObj);
                    }
                }

                foreach (var item in questionnaireWindowList)
                {
                    var assessmentReason = assessmentAllReason.Where(x => x.AssessmentReasonID == item.AssessmentReasonID).FirstOrDefault();

                    if (assessmentReason.Name.ToLower() == PCISEnum.AssessmentReason.Initial.ToLower())
                    {
                        log.LogInformation($" ReminderNotificationProcess : Queue Item : {myQueueItem} : ProcessInitial Start");
                        ProcessInitial(Utility, apiurl, personQuestionnaire, item, questionnaireReminderRuleList, questionnaireReminderTypes, questionnaireLateCountLimit, log);
                        log.LogInformation($" ReminderNotificationProcess : Queue Item : {myQueueItem} : ProcessInitial end");

                    }
                    else if (assessmentReason.Name.ToLower() == PCISEnum.AssessmentReason.Scheduled.ToLower())
                    {
                        log.LogInformation($" ReminderNotificationProcess : Queue Item : {myQueueItem} : GetRegularReminderSettingsByQuestionnaire Start");
                        var questionnaireRegularReminderSettings = GetRegularReminderSettingsByQuestionnaire(Utility, apiurl, questionnaireId, log, myQueueItem);
                        log.LogInformation($" ReminderNotificationProcess : Queue Item : {myQueueItem} : GetRegularReminderSettingsByQuestionnaire End ");
                        if (questionnaireRegularReminderSettings?.Recurrence != null && questionnaireRegularReminderSettings?.Recurrence?.Count > 0)
                        {
                            log.LogInformation($" ReminderNotificationProcess : Queue Item : {myQueueItem} : ProcessRegularInterval Start");
                            ProcessRegularIntervalWithRecurrencePattern(Utility, apiurl, personQuestionnaire, item, questionnaireReminderRuleList, questionnaireReminderTypes, questionnaireRegularReminderSettings, questionnaireLateCountLimit, limitAfterWindowClose, log);
                        }
                        log.LogInformation($" ReminderNotificationProcess : Queue Item : {myQueueItem} : ProcessRegularInterval end");
                    }
                    else if (assessmentReason.Name.ToLower() == PCISEnum.AssessmentReason.Discharge.ToLower())
                    {
                        if (personQuestionnaire.EndDueDate.HasValue && personQuestionnaire.CollaborationID > 0)
                        {
                            log.LogInformation($" ReminderNotificationProcess : Queue Item : {myQueueItem} : ProcessDischarge Start");
                            ProcessDischarge(Utility, apiurl, personQuestionnaire, item, questionnaireReminderRuleList, questionnaireReminderTypes, questionnaireLateCountLimit, log);
                            log.LogInformation($" ReminderNotificationProcess : Queue Item : {myQueueItem} : ProcessDischarge end");
                        }
                        else
                        {
                            var personQuestionnaireScheduleObj = GetPersonQuestionnaireSchedule(Utility, apiurl, personQuestionnaire.PersonQuestionnaireID, item.QuestionnaireWindowID);
                            if (personQuestionnaireScheduleObj.Count > 0)
                            {
                                personQuestionnaireScheduleObj.ForEach(x => x.IsRemoved = true);
                                UpdateBulkPersonQuestionnaireSchedule(Utility, apiurl, personQuestionnaireScheduleObj);
                            }
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                log.LogError(ex, $@"ReminderNotificationProcess : Exception occurred.Exception Message : {ex.Message}.Location : {ex.StackTrace}.Details : {ex.InnerException}");
            }
        }


        /// <summary>
        /// This is not Used now. As part of changes from PCIS-3225
        /// </summary>
        /// <param name="Utility"></param>
        /// <param name="apiurl"></param>
        /// <param name="personQuestionnaireDTO"></param>
        /// <param name="questionnaireWindowDTO"></param>
        /// <param name="questionnaireReminderRuleList"></param>
        /// <param name="questionnaireReminderTypes"></param>
        /// <param name="questionnaireLateCountLimit"></param>
        /// <param name="limitAfterWindowClose"></param>
        /// <param name="log"></param>
        public static void ProcessRegularIntervalOld(Utility Utility, string apiurl, PersonQuestionnaireDTO personQuestionnaireDTO, QuestionnaireWindowsDTO questionnaireWindowDTO,
            List<QuestionnaireReminderRulesDTO> questionnaireReminderRuleList, List<QuestionnaireReminderTypeDTO> questionnaireReminderTypes, int questionnaireLateCountLimit, int limitAfterWindowClose, ILogger log)
        {
            var daysToBeAddedToOpenDay = (questionnaireWindowDTO.RepeatIntervalDays ?? 0) - questionnaireWindowDTO.WindowOpenOffsetDays;
            var daysToBeAddedToCloseDay = (questionnaireWindowDTO.RepeatIntervalDays ?? 0) + questionnaireWindowDTO.WindowCloseOffsetDays;
            bool completed = false;
            var startDate = personQuestionnaireDTO.StartDate.Date;
            var personQuestionnaireScheduleObj = GetPersonQuestionnaireSchedule(Utility, apiurl, personQuestionnaireDTO.PersonQuestionnaireID, questionnaireWindowDTO.QuestionnaireWindowID);
            if (personQuestionnaireScheduleObj.Count > 0)
            {
                personQuestionnaireScheduleObj.ForEach(x => { x.IsRemoved = true; });
                UpdateBulkPersonQuestionnaireSchedule(Utility, apiurl, personQuestionnaireScheduleObj);
            }
            while (!completed)
            {
                var windowOpenDay = questionnaireWindowDTO.IsSelected ? startDate.AddDays(daysToBeAddedToOpenDay) : startDate;
                var windowDueDate = startDate.AddDays(questionnaireWindowDTO.RepeatIntervalDays ?? 0);
                var windowCloseDay = questionnaireWindowDTO.IsSelected ? startDate.AddDays(daysToBeAddedToCloseDay) : startDate;

                PersonQuestionnaireScheduleDTO personQuestionnaireSchedule = new PersonQuestionnaireScheduleDTO
                {
                    PersonQuestionnaireID = personQuestionnaireDTO.PersonQuestionnaireID,
                    QuestionnaireWindowID = questionnaireWindowDTO.QuestionnaireWindowID,
                    WindowDueDate = windowDueDate,
                };
                var personQuestionnaireScheduleID = AddPersonQuestionnaireSchedule(Utility, apiurl, personQuestionnaireSchedule);
                List<NotifyReminderDTO> notifyReminderList = new List<NotifyReminderDTO>();
                foreach (var item in questionnaireReminderRuleList)
                {
                    //NotifyReminder notifyReminder = new NotifyReminder();
                    var questionnaireReminderType = questionnaireReminderTypes.Where(x => x.QuestionnaireReminderTypeID == item.QuestionnaireReminderTypeID).FirstOrDefault();
                    if (item.IsSelected)
                    {
                        var notifyReminderToAdd = CreateNotifyReminder(Utility, apiurl, questionnaireReminderType.Name, personQuestionnaireScheduleID, windowOpenDay, windowDueDate, windowCloseDay, item, questionnaireLateCountLimit);
                        foreach (var notifyReminder in notifyReminderToAdd)
                        {
                            if (notifyReminder.PersonQuestionnaireScheduleID > 0)
                            {
                                notifyReminderList.Add(notifyReminder);
                            }
                        }
                    }
                }
                AddBulkNotifyReminder(Utility, apiurl, notifyReminderList);
                startDate = windowDueDate;
                completed = personQuestionnaireDTO.EndDueDate.HasValue ? 
                    (personQuestionnaireDTO.EndDueDate.Value < startDate.AddDays(questionnaireWindowDTO.RepeatIntervalDays ?? 0)) 
                    : (personQuestionnaireDTO.StartDate.AddMonths(limitAfterWindowClose) < startDate.AddDays(questionnaireWindowDTO.RepeatIntervalDays ?? 0));
            }
        }

        public static void ProcessDischarge(Utility Utility, string apiurl, PersonQuestionnaireDTO personQuestionnaireDTO, QuestionnaireWindowsDTO questionnaireWindowDTO,
            List<QuestionnaireReminderRulesDTO> questionnaireReminderRuleList, List<QuestionnaireReminderTypeDTO> questionnaireReminderTypes, int questionnaireLateCountLimit, ILogger Log)
        {
            var personQuestionnaireScheduleObj = GetPersonQuestionnaireSchedule(Utility, apiurl, personQuestionnaireDTO.PersonQuestionnaireID, questionnaireWindowDTO.QuestionnaireWindowID);
            if (personQuestionnaireScheduleObj.Count > 0)
            {
                personQuestionnaireScheduleObj.ForEach(x => x.IsRemoved = true);
                UpdateBulkPersonQuestionnaireSchedule(Utility, apiurl, personQuestionnaireScheduleObj);
            }
            var windowOpenDay = personQuestionnaireDTO.EndDueDate.Value.Date.AddDays(-questionnaireWindowDTO.WindowOpenOffsetDays);
            var windowDueDate = personQuestionnaireDTO.EndDueDate.Value.Date;
            var windowCloseDay = personQuestionnaireDTO.EndDueDate.Value.Date.AddDays((questionnaireWindowDTO.WindowCloseOffsetDays));
           
            PersonQuestionnaireScheduleDTO personQuestionnaireSchedule = new PersonQuestionnaireScheduleDTO
            {
                PersonQuestionnaireID = personQuestionnaireDTO.PersonQuestionnaireID,
                QuestionnaireWindowID = questionnaireWindowDTO.QuestionnaireWindowID,
                WindowDueDate = windowDueDate
            };
            var personQuestionnaireScheduleID = AddPersonQuestionnaireSchedule(Utility, apiurl, personQuestionnaireSchedule);
            List<NotifyReminderDTO> notifyReminderList = new List<NotifyReminderDTO>();
            foreach (var item in questionnaireReminderRuleList)
            {
                //NotifyReminder notifyReminder = new NotifyReminder();
                var questionnaireReminderType = questionnaireReminderTypes.Where(x => x.QuestionnaireReminderTypeID == item.QuestionnaireReminderTypeID).FirstOrDefault();
                if (item.IsSelected)
                {
                    var notifyReminderToAdd = CreateNotifyReminder(Utility, apiurl, questionnaireReminderType.Name, personQuestionnaireScheduleID, windowOpenDay.Date, windowDueDate.Date, windowCloseDay.Date, item, questionnaireLateCountLimit);
                    foreach (var notifyReminder in notifyReminderToAdd)
                    {
                        if (notifyReminder.PersonQuestionnaireScheduleID > 0)
                        {
                            notifyReminderList.Add(notifyReminder);
                        }
                    }
                }
            }
            AddBulkNotifyReminder(Utility, apiurl, notifyReminderList);
        }


        public static void ProcessInitial(Utility Utility, string apiurl, PersonQuestionnaireDTO personQuestionnaireDTO, QuestionnaireWindowsDTO questionnaireWindowDTO,
            List<QuestionnaireReminderRulesDTO> questionnaireReminderRuleList, List<QuestionnaireReminderTypeDTO> questionnaireReminderTypes, int questionnaireLateCountLimit, ILogger Log)
        {
            var windowOpenDay = questionnaireWindowDTO.IsSelected && questionnaireWindowDTO.WindowOpenOffsetDays > 0 ? personQuestionnaireDTO.StartDate.Date.AddDays(-questionnaireWindowDTO.WindowOpenOffsetDays) : personQuestionnaireDTO.StartDate;
            var windowDueDate = personQuestionnaireDTO.StartDate.Date;
            var windowCloseDay = questionnaireWindowDTO.IsSelected && questionnaireWindowDTO.WindowCloseOffsetDays > 0 ? personQuestionnaireDTO.StartDate.Date.AddDays((questionnaireWindowDTO.WindowCloseOffsetDays)) : personQuestionnaireDTO.StartDate;
            var personQuestionnaireScheduleObj = GetPersonQuestionnaireSchedule(Utility, apiurl, personQuestionnaireDTO.PersonQuestionnaireID, questionnaireWindowDTO.QuestionnaireWindowID);
            if (personQuestionnaireScheduleObj.Count > 0)
            {
                personQuestionnaireScheduleObj.ForEach(x => { x.IsRemoved = true; });
                UpdateBulkPersonQuestionnaireSchedule(Utility, apiurl, personQuestionnaireScheduleObj);
            }
            PersonQuestionnaireScheduleDTO personQuestionnaireSchedule = new PersonQuestionnaireScheduleDTO
            {
                PersonQuestionnaireID = personQuestionnaireDTO.PersonQuestionnaireID,
                QuestionnaireWindowID = questionnaireWindowDTO.QuestionnaireWindowID,
                WindowDueDate = windowDueDate
            };
            var personQuestionnaireScheduleID = AddPersonQuestionnaireSchedule(Utility, apiurl, personQuestionnaireSchedule);


            List<NotifyReminderDTO> notifyReminderList = new List<NotifyReminderDTO>();
            foreach (var item in questionnaireReminderRuleList)
            {
                //NotifyReminder notifyReminder = new NotifyReminder();
                var questionnaireReminderType = questionnaireReminderTypes.Where(x => x.QuestionnaireReminderTypeID == item.QuestionnaireReminderTypeID).FirstOrDefault();
                if (item.IsSelected)
                {
                    var notifyReminderToAdd = CreateNotifyReminder(Utility, apiurl, questionnaireReminderType.Name, personQuestionnaireScheduleID, windowOpenDay.Date, windowDueDate.Date, windowCloseDay.Date, item, questionnaireLateCountLimit);
                    foreach (var notifyReminder in notifyReminderToAdd)
                    {
                        if (notifyReminder.PersonQuestionnaireScheduleID > 0)
                        {
                            notifyReminderList.Add(notifyReminder);
                        }
                    }
                }
            }
            AddBulkNotifyReminder(Utility, apiurl, notifyReminderList);
        }

        private static CRUDResponse AddBulkNotifyReminder(Utility Utility, string apiurl, List<NotifyReminderDTO> notifyReminders)
        {
            try
            {
                CRUDResponse Response = new CRUDResponse();
                var url = apiurl + PCISEnum.APIurl.NotifyReminder;
                var result = Utility.RestApiCall(url, false, false, PCISEnum.APIMethodType.PostRequest, null, notifyReminders.ToJSON());
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

        private static string GetConfiguration(Utility Utility, string apiurl, string key, ILogger log)
        {
            try
            {
                string configValue = string.Empty;
                var configAPiurl = apiurl + PCISEnum.APIurl.ConfigurationWithKey.Replace(PCISEnum.APIReplacableValues.Key, key).Replace(PCISEnum.APIReplacableValues.AgencyID, "0");

                log.LogInformation($" ReminderNotificationProcess : Queue Item : GetConfiguration URL { configAPiurl}");
                var result = Utility.RestApiCall(configAPiurl, false);
                if (!string.IsNullOrEmpty(result))
                {
                    var config = JsonConvert.DeserializeObject<ConfigurationResponseDTO>(result);
                    configValue = config.result.ConfigurationValue;
                }
                log.LogInformation($" ReminderNotificationProcess : Queue Item : GetConfiguration URL { configValue}");
                return configValue;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// CreateNotifyReminder.
        /// Update as part of PCIS-3225 : To calculate notifydate based on day/hour offsetType.
        /// </summary>
        /// <param name="Utility"></param>
        /// <param name="apiurl"></param>
        /// <param name="reminderTypeName"></param>
        /// <param name="personQuestionnaireScheduleID"></param>
        /// <param name="windowOpenDay"></param>
        /// <param name="windowDueDate"></param>
        /// <param name="windowCloseDay"></param>
        /// <param name="questionnaireReminderRulesDTO"></param>
        /// <param name="questionnaireLateCountLimit"></param>
        /// <param name="personQuestionnaireScheduleIndex"></param>
        /// <returns></returns>
        public static List<NotifyReminderDTO> CreateNotifyReminder(Utility Utility, string apiurl, string reminderTypeName, long personQuestionnaireScheduleID, DateTime windowOpenDay, DateTime windowDueDate, DateTime windowCloseDay, QuestionnaireReminderRulesDTO questionnaireReminderRulesDTO, int questionnaireLateCountLimit, Guid? personQuestionnaireScheduleIndex = null)
        {
            try
            {
                List<NotifyReminderDTO> notifyReminderList = new List<NotifyReminderDTO>();
                NotifyReminderDTO notifyReminder = new NotifyReminderDTO();
                switch (reminderTypeName)
                {
                    case PCISEnum.QuestionnaireReminderType.WindowOpen:
                        notifyReminder = new NotifyReminderDTO
                        {
                            PersonQuestionnaireScheduleID = personQuestionnaireScheduleID,
                            QuestionnaireReminderRuleID = questionnaireReminderRulesDTO.QuestionnaireReminderRuleID,
                            NotifyReminderTypeName = PCISEnum.QuestionnaireReminderType.WindowOpen,
                            PersonQuestionnaireScheduleIndex = personQuestionnaireScheduleIndex,
                            NotifyDate = windowOpenDay.AddDaysOrHours(-questionnaireReminderRulesDTO.ReminderOffsetDays ?? default(int), questionnaireReminderRulesDTO.ReminderOffsetTypeID)                   
                        };
                        notifyReminderList.Add(notifyReminder);
                        break;
                    case PCISEnum.QuestionnaireReminderType.AssesmentDue:
                        notifyReminder = new NotifyReminderDTO
                        {
                            NotifyReminderTypeName = PCISEnum.QuestionnaireReminderType.AssesmentDue,
                            PersonQuestionnaireScheduleIndex = personQuestionnaireScheduleIndex,
                            PersonQuestionnaireScheduleID = personQuestionnaireScheduleID,
                            QuestionnaireReminderRuleID = questionnaireReminderRulesDTO.QuestionnaireReminderRuleID,
                            NotifyDate = windowDueDate
                        };
                        notifyReminderList.Add(notifyReminder);
                        break;
                    case PCISEnum.QuestionnaireReminderType.WindowClose:
                        notifyReminder = new NotifyReminderDTO
                        {
                            NotifyReminderTypeName = PCISEnum.QuestionnaireReminderType.WindowClose,
                            PersonQuestionnaireScheduleIndex = personQuestionnaireScheduleIndex,
                            PersonQuestionnaireScheduleID = personQuestionnaireScheduleID,
                            QuestionnaireReminderRuleID = questionnaireReminderRulesDTO.QuestionnaireReminderRuleID,
                            NotifyDate = windowCloseDay
                        };
                        notifyReminderList.Add(notifyReminder);
                        break;
                    case PCISEnum.QuestionnaireReminderType.QuestionnaireLate:
                        var closeDay = windowCloseDay.AddDaysOrHours(questionnaireReminderRulesDTO.ReminderOffsetDays ?? default(int),questionnaireReminderRulesDTO.ReminderOffsetTypeID);
                        for (var i = 0; i < questionnaireLateCountLimit; i++)
                        {
                            notifyReminder = new NotifyReminderDTO
                            {
                                NotifyReminderTypeName = PCISEnum.QuestionnaireReminderType.QuestionnaireLate,
                                PersonQuestionnaireScheduleIndex = personQuestionnaireScheduleIndex,
                                PersonQuestionnaireScheduleID = personQuestionnaireScheduleID,
                                QuestionnaireReminderRuleID = questionnaireReminderRulesDTO.QuestionnaireReminderRuleID,
                                NotifyDate = closeDay
                            };
                            closeDay = closeDay.AddDaysOrHours(questionnaireReminderRulesDTO.ReminderOffsetDays ?? default(int), questionnaireReminderRulesDTO.ReminderOffsetTypeID);
                            notifyReminderList.Add(notifyReminder);
                        }
                        break;
                    default:
                        break;
                }
                return notifyReminderList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static long AddPersonQuestionnaireSchedule(Utility Utility, string apiurl, PersonQuestionnaireScheduleDTO personQuestionnaireSchedules)
        {
            try
            {
                long Response = new long();
                var url = apiurl + PCISEnum.APIurl.PersonQuestionnaireSchedule;
                var result = Utility.RestApiCall(url, false, false, PCISEnum.APIMethodType.PostRequest, null, personQuestionnaireSchedules.ToJSON());
                if (!string.IsNullOrEmpty(result))
                {
                    var TotalResponse = JsonConvert.DeserializeObject<AddPersonQuestionnaireScheduleResponseDTO>(result);
                    Response = TotalResponse.result.PersonQuestionnaireScheduleID;
                }
                return Response;
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
                personQuestionnaireSchedules?.ForEach(x => { x.IsRemoveReminderNotifications = true; });
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

        private static List<PersonQuestionnaireScheduleDTO> GetPersonQuestionnaireSchedule(Utility Utility, string apiurl, long PersonQuestionnaireId, int QuestionnaireWindowId)
        {
            try
            {
                List<PersonQuestionnaireScheduleDTO> PersonQuestionnaireSchedule = new List<PersonQuestionnaireScheduleDTO>();
                var url = apiurl + PCISEnum.APIurl.GetPersonQuestionnaireSchedule.Replace(PCISEnum.APIReplacableValues.PersonQuestionnaireId, Convert.ToString(PersonQuestionnaireId))
                    .Replace(PCISEnum.APIReplacableValues.QuestionnaireWindowId, Convert.ToString(QuestionnaireWindowId));
                var result = Utility.RestApiCall(url, false);
                if (!string.IsNullOrEmpty(result))
                {
                    var PersonQuestionnaireScheduleResponse = JsonConvert.DeserializeObject<PersonQuestionnaireScheduleDetailsDTO>(result);
                    PersonQuestionnaireSchedule = PersonQuestionnaireScheduleResponse?.result?.PersonQuestionnaireSchedules;
                }
                return PersonQuestionnaireSchedule;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static List<QuestionnaireReminderTypeDTO> GetQuestionnaireReminderType(Utility Utility, string apiurl, ILogger log, string myQueueItem)
        {
            try
            {
                List<QuestionnaireReminderTypeDTO> QuestionnaireReminderType = new List<QuestionnaireReminderTypeDTO>();
                var url = apiurl + PCISEnum.APIurl.AllQuestionnaireReminderType;
                log.LogInformation($" ReminderNotificationProcess : Queue Item : {myQueueItem} : GetQuestionnaireReminderType url : {url}");

                var result = Utility.RestApiCall(url, false);
                if (!string.IsNullOrEmpty(result))
                {
                    var QuestionnaireReminderTypeResponse = JsonConvert.DeserializeObject<QuestionnaireReminderTypeResponseDTO>(result);
                    QuestionnaireReminderType = QuestionnaireReminderTypeResponse?.result?.QuestionnaireReminderType;
                }
                return QuestionnaireReminderType;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static List<AssessmentReasonLookupDTO> GetAllAssessmentReason(Utility Utility, string apiurl, ILogger log, string myQueueItem)
        {
            try
            {
                List<AssessmentReasonLookupDTO> AssessmentReason = new List<AssessmentReasonLookupDTO>();
                var url = apiurl + PCISEnum.APIurl.AllAssessmentReason;
                log.LogInformation($" ReminderNotificationProcess : Queue Item : {myQueueItem} : GetAllAssessmentReason url : {url}");

                var result = Utility.RestApiCall(url, false);
                if (!string.IsNullOrEmpty(result))
                {
                    var AssessmentReasonResponse = JsonConvert.DeserializeObject<AssessmentReasonLookupResponseDTO>(result);
                    AssessmentReason = AssessmentReasonResponse?.result?.AssessmentReasonLookup;
                }
                return AssessmentReason;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static List<QuestionnaireReminderRulesDTO> GetQuestionnaireReminderRulesByQuestionnaire(Utility Utility, string apiurl, int QuestionnaireId, ILogger log, string myQueueItem)
        {
            try
            {
                List<QuestionnaireReminderRulesDTO> QuestionnaireReminderRule = new List<QuestionnaireReminderRulesDTO>();
                var url = apiurl + PCISEnum.APIurl.QuestionnaireReminderRulesByQuestionnaire.Replace(PCISEnum.APIReplacableValues.QuestionnaireId, Convert.ToString(QuestionnaireId));
                log.LogInformation($" ReminderNotificationProcess : Queue Item : {myQueueItem} : GetQuestionnaireReminderRulesByQuestionnaire url : {url}");
                var result = Utility.RestApiCall(url, false);
                if (!string.IsNullOrEmpty(result))
                {
                    var QuestionnaireReminderRuleResponse = JsonConvert.DeserializeObject<QuestionnaireReminderRuleDetailsDTO>(result);
                    QuestionnaireReminderRule = QuestionnaireReminderRuleResponse?.result?.QuestionnaireReminderRule;
                }
                return QuestionnaireReminderRule;
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
                log.LogInformation($" ReminderNotificationProcess : Queue Item : {myQueueItem} :GetQuestionnaireWindowsByQuestionnaire url : {url}");
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

        private static PersonQuestionnaireDTO GetPersonQuestionnaireByID(Utility Utility, string apiurl, long personQuestionnaireId, ILogger log, string myQueueItem)
        {
            try
            {
                PersonQuestionnaireDTO PersonQuestionnaire = new PersonQuestionnaireDTO();
                var url = apiurl + PCISEnum.APIurl.PersonQuestionnaireByID.Replace(PCISEnum.APIReplacableValues.PersonQuestionnaireId, Convert.ToString(personQuestionnaireId));
                log.LogInformation($" ReminderNotificationProcess : Queue Item : {myQueueItem} : GetPersonQuestionnaireByID url : {url}");

                var result = Utility.RestApiCall(url, false);
                log.LogInformation($" ReminderNotificationProcess : Queue Item : {myQueueItem} : result : {result}");

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
        private static PersonCollaborationDTO GetPersonCollaborationByPersonIdAndCollaborationId(Utility Utility, string apiurl, long personId, int? collaborationId, ILogger log, string myQueueItem)
        {
            try
            {
                PersonCollaborationDTO PersonQuestionnaire = new PersonCollaborationDTO();
                var url = apiurl + PCISEnum.APIurl.PersonCollaborationByPersonIdAndCollaborationId.
                    Replace(PCISEnum.APIReplacableValues.PersonId, Convert.ToString(personId)).Replace(PCISEnum.APIReplacableValues.CollaborationId, Convert.ToString(collaborationId));
                log.LogInformation($" ReminderNotificationProcess : Queue Item : {myQueueItem} : GetPersonCollaborationByPersonIdAndCollaborationId url : {url}");

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

        /// <summary>
        /// To get unselected questionnaireWindows By Questionnaire.
        /// </summary>
        /// <param name="Utility"></param>
        /// <param name="apiurl"></param>
        /// <param name="QuestionnaireId"></param>
        /// <param name="log"></param>
        /// <param name="myQueueItem"></param>
        /// <returns></returns>
        private static List<QuestionnaireWindowsDTO> GetUnSelectedQuestionnaireWindowsByQuestionnaire(Utility Utility, string apiurl, int QuestionnaireId, ILogger log, string myQueueItem)
        {
            try
            {
                List<QuestionnaireWindowsDTO> QuestionnaireWindow = new List<QuestionnaireWindowsDTO>();
                var url = apiurl + PCISEnum.APIurl.UnselectedQuestionnaireWindowsByQuestionnaire.Replace(PCISEnum.APIReplacableValues.QuestionnaireId, Convert.ToString(QuestionnaireId));
                log.LogInformation($" ReminderNotificationProcess : Queue Item : {myQueueItem} :GetQuestionnaireWindowsByQuestionnaire url : {url}");
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

        /// <summary>
        /// Get Regular Reminder Recurrence settings for a QuestionnaireID
        /// </summary>
        /// <param name="utility"></param>
        /// <param name="apiurl"></param>
        /// <param name="questionnaireId"></param>
        /// <param name="log"></param>
        /// <param name="myQueueItem"></param>
        /// <returns></returns>
        private static QuestionnaireRegularReminderSettingsDTO GetRegularReminderSettingsByQuestionnaire(Utility utility, string apiurl, int questionnaireId, ILogger log, string myQueueItem)
        {
            try
            {
                QuestionnaireRegularReminderSettingsDTO regularReminderSettings = new QuestionnaireRegularReminderSettingsDTO();
                var url = apiurl + PCISEnum.APIurl.RegularReminderSettingsByQuestionnaire;
                log.LogInformation($" ReminderNotificationProcess : Queue Item : {myQueueItem} :GetRegularReminderSettingsByQuestionnaire url : {url}");
                var lst_questionnaireIds = new List<int>() { questionnaireId };
                var result = utility.RestApiCall(url, false, false, PCISEnum.APIMethodType.PostRequest, null, lst_questionnaireIds.ToJSON());
                if (!string.IsNullOrEmpty(result))
                {
                    var regularReminderSettingsResponse = JsonConvert.DeserializeObject<QuestionnaireRegularReminderResponseDTO>(result);
                    regularReminderSettings = regularReminderSettingsResponse?.result;
                }
                return regularReminderSettings;
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        /// <summary>
        /// To add person questionnaire schedule.
        /// </summary>
        /// <param name="Utility"></param>
        /// <param name="apiurl"></param>
        /// <param name="personQuestionnaireSchedules"></param>
        /// <returns></returns>
        private static Dictionary<Guid, long> AddBulkPersonQuestionnaireSchedule(Utility Utility, string apiurl, List<PersonQuestionnaireScheduleDTO> personQuestionnaireSchedules, ILogger log)
        {
            try
            {
                Dictionary<Guid, long> Response = new Dictionary<Guid, long>();
                if (personQuestionnaireSchedules.Count == 0) return Response;
                var url = apiurl + PCISEnum.APIurl.PersonQuestionnaireScheduleBulk;
                var result = Utility.RestApiCall(url, false, false, PCISEnum.APIMethodType.PostRequest, null, personQuestionnaireSchedules.ToJSON());
                if (!string.IsNullOrEmpty(result))
                {
                    var TotalResponse = JsonConvert.DeserializeObject<AddBulkPersonQuestionnaireScheduleResponseDTO>(result);
                    Response = JsonConvert.DeserializeObject<Dictionary<Guid, long>>(TotalResponse.result.PersonQuestionnaireSchedule);
                }
                return Response;
            }
            catch (Exception)
            {
                throw;
            }
        }


        #region RegularReminderWithRecurrencePattern 
        /// <summary>
        /// ProcessRegularIntervalWithRecurrencePattern - Created as part of PCIS-3225
        /// This function creates first occurence schedule 
        /// And also always make sure to create a schedule for an occurrence with first reminder notifyDate = currentDay+1. 
        /// </summary>
        /// <param name="utility"></param>
        /// <param name="apiurl"></param>
        /// <param name="personQuestionnaireDTO"></param>
        /// <param name="questionnaireWindowDTO"></param>
        /// <param name="questionnaireReminderRuleList"></param>
        /// <param name="questionnaireReminderTypes"></param>
        /// <param name="regularReminderSettings"></param>
        /// <param name="questionnaireLateCountLimit"></param>
        /// <param name="limitAfterWindowClose"></param>
        /// <param name="log"></param>
        private static void ProcessRegularIntervalWithRecurrencePattern(Utility utility, string apiurl, PersonQuestionnaireDTO personQuestionnaireDTO, QuestionnaireWindowsDTO questionnaireWindowDTO, List<QuestionnaireReminderRulesDTO> questionnaireReminderRuleList, List<QuestionnaireReminderTypeDTO> questionnaireReminderTypes, QuestionnaireRegularReminderSettingsDTO regularReminderSettings, int questionnaireLateCountLimit, int limitAfterWindowClose, ILogger log)
        {
            try
            {
                List<PersonQuestionnaireScheduleDTO> lst_AllPersonQuestionnaireSchedule = new List<PersonQuestionnaireScheduleDTO>();
                List<NotifyReminderDTO> lst_AllNotifyReminderList = new List<NotifyReminderDTO>();
                List<PersonQuestionnaireScheduleDTO> lst_personQuestionnaireRegularSchedule;
                List<NotifyReminderDTO> lst_regularNotifyReminderList;

                log.LogInformation($" ReminderNotificationProcess : ProcessRegularReminder : Removing the existing regular reminders.");
                //Remove the existing schedules since we are yet to create a new schedule.
                var personQuestionnaireScheduleObj = GetPersonQuestionnaireSchedule(utility, apiurl, personQuestionnaireDTO.PersonQuestionnaireID, questionnaireWindowDTO.QuestionnaireWindowID);
                if (personQuestionnaireScheduleObj.Count > 0)
                {
                    personQuestionnaireScheduleObj.ForEach(x => { x.IsRemoved = true; });
                    UpdateBulkPersonQuestionnaireSchedule(utility, apiurl, personQuestionnaireScheduleObj);
                }
                var reminderRuleList = questionnaireReminderRuleList?.Where(x => x.IsSelected == true).Count();
                //If it has a regular recuurenece schedule and atleast one reminder rule set for it then start process.
                if (regularReminderSettings?.Recurrence != null && regularReminderSettings?.Recurrence.Count > 0 && reminderRuleList > 0)
                {
                    log.LogInformation($" ReminderNotificationProcess : Queue Item : RegularReminder RecurrencePattern : {regularReminderSettings?.Recurrence?[0].RecurrencePattern} ");
                    var recurrenceSettingsInDetailDTO = regularReminderSettings.Recurrence;
                    var reminderCurrentDueDate = regularReminderSettings.Recurrence[0].RecurrenceRangeStartDate.Date;
                    //Calculate start date : Always start a schedule from today
                    if (personQuestionnaireDTO.StartDate.Date >= regularReminderSettings.Recurrence[0].RecurrenceRangeStartDate.Date)
                    {
                        reminderCurrentDueDate = personQuestionnaireDTO.StartDate.Date;
                    }
                    if (personQuestionnaireDTO.StartDate.Date < regularReminderSettings.Recurrence[0].RecurrenceRangeStartDate.Date)
                    {
                        reminderCurrentDueDate = regularReminderSettings.Recurrence[0].RecurrenceRangeStartDate.Date;
                    }
                    if (reminderCurrentDueDate <= DateTime.UtcNow.Date)
                    {
                        reminderCurrentDueDate = DateTime.UtcNow.Date;
                    }
                    log.LogInformation($" ReminderNotificationProcess : ProcessRegularReminder : Start calculating DueDates.. ");
                    bool IfAReminderFor2mrw = false; bool isEndDateArrived = false;
                    int occurrenceCounter = 1;
                    do
                    {
                        log.LogInformation($" ReminderNotificationProcess : ***********OccurrenceCounter = {occurrenceCounter} START*********** ");
                        lst_personQuestionnaireRegularSchedule = new List<PersonQuestionnaireScheduleDTO>();
                        lst_regularNotifyReminderList = new List<NotifyReminderDTO>();
                        log.LogInformation($" StartDate = {reminderCurrentDueDate}");
                        log.LogInformation($" ReminderRecurrancePattern = {recurrenceSettingsInDetailDTO[0].RecurrencePattern}");
                        log.LogInformation($" ReminderRecurranceType = {recurrenceSettingsInDetailDTO[0].RecurrenceRangeEndType}");
                        var lst_duedate = GetRegularAssessmentDueDateFromRecurrenceSettings(recurrenceSettingsInDetailDTO[0], reminderCurrentDueDate, occurrenceCounter);
                        log.LogInformation($" ReminderNotificationProcess : ProcessRegularReminder : Next Occurrence Duedates Count = {lst_duedate?.Count}");

                        //if due dates are less than personquestionnaireEndate
                        isEndDateArrived = ValidationForDueDates(ref lst_duedate, regularReminderSettings.Recurrence[0], personQuestionnaireDTO, occurrenceCounter, log);
                        
                        if (lst_duedate?.Count == 0) break;

                        //Attach Time to due dates.
                        var lst_dueDateWithTime = new List<DateTime>();
                        foreach (var recurtime in regularReminderSettings.TimeRule)
                        {
                            var time = string.Format("{0}:{1} {2}", recurtime.Hour, recurtime.Minute, recurtime.AMorPM).ToTimeSpan();
                            lst_dueDateWithTime.AddRange(lst_duedate.Select(x => (x.Date + time).GetUTCDateTime(recurtime.TimeZoneName)).ToList());
                        }
                        log.LogInformation($" ReminderNotificationProcess : DueDates with time attached count = {lst_dueDateWithTime.Count}");
                        lst_dueDateWithTime = lst_dueDateWithTime.Distinct().ToList();
                        log.LogInformation($" ReminderNotificationProcess : ProcessRegularReminder : Creating PersonQuestionnaireSchedules");
                        foreach (var duedate in lst_dueDateWithTime)
                        {
                            //Create PersonQuestionnaireSchedule window for all available duedateTime
                            var windowOpenDay = duedate.AddDaysOrHours(-questionnaireWindowDTO.WindowOpenOffsetDays, questionnaireWindowDTO.OpenOffsetTypeID);
                            var windowDueDate = duedate;
                            var windowCloseDay = duedate.AddDaysOrHours(questionnaireWindowDTO.WindowCloseOffsetDays,
                                questionnaireWindowDTO.CloseOffsetTypeID);
                            PersonQuestionnaireScheduleDTO personQuestionnaireSchedule = new PersonQuestionnaireScheduleDTO
                            {
                                PersonQuestionnaireID = personQuestionnaireDTO.PersonQuestionnaireID,
                                QuestionnaireWindowID = questionnaireWindowDTO.QuestionnaireWindowID,
                                WindowDueDate = windowDueDate,
                                WindowOpenDate = windowOpenDay,
                                WindowCloseDate = windowCloseDay,
                                OccurrenceCounter = occurrenceCounter,
                                PersonQuestionnaireScheduleIndex = Guid.NewGuid()
                            };
                            lst_personQuestionnaireRegularSchedule.Add(personQuestionnaireSchedule);
                        }

                        log.LogInformation($" ReminderNotificationProcess : ProcessRegularReminder : Creating NotifyReminders");
                        foreach (var schedule in lst_personQuestionnaireRegularSchedule)
                        {
                            //Create the notifyReminderDTO for selected reminder rules only.
                            questionnaireReminderRuleList = questionnaireReminderRuleList.Where(x => x.IsSelected == true).ToList();
                            foreach (var item in questionnaireReminderRuleList)
                            {
                                var questionnaireReminderType = questionnaireReminderTypes.Where(x => x.QuestionnaireReminderTypeID == item.QuestionnaireReminderTypeID).FirstOrDefault();
                                var notifyReminderToAdd = CreateNotifyReminder(utility, apiurl, questionnaireReminderType.Name, 0, schedule.WindowOpenDate.Value, schedule.WindowDueDate, schedule.WindowCloseDate.Value, item, questionnaireLateCountLimit, schedule.PersonQuestionnaireScheduleIndex);
                                notifyReminderToAdd.ForEach(x => x.PersonQuestionnaireScheduleIndex = schedule.PersonQuestionnaireScheduleIndex);
                                lst_regularNotifyReminderList.AddRange(notifyReminderToAdd);
                            }
                        }
                        lst_AllPersonQuestionnaireSchedule.AddRange(lst_personQuestionnaireRegularSchedule);
                        lst_AllNotifyReminderList.AddRange(lst_regularNotifyReminderList);

                        //The first reminder notifydate for this set of occurence should be atleast 1 day 12hr after today.
                        var lst_windowopendueOrder = lst_regularNotifyReminderList?.OrderBy(x => x.NotifyDate).FirstOrDefault();
                        IfAReminderFor2mrw = lst_windowopendueOrder?.NotifyDate.Date >= DateTime.UtcNow.AddDays(2).Date;

                        log.LogInformation($" ReminderNotificationProcess : First reminder Date for this ocurrence : {lst_windowopendueOrder?.NotifyDate.Date} ");
                        //The next duedate is the latest duedates from an occurence.
                        reminderCurrentDueDate = lst_duedate.OrderByDescending(x => x).ToList().FirstOrDefault().Date;

                        log.LogInformation($" ReminderNotificationProcess : ***********OccurrenceCounter = {occurrenceCounter} END*********** ");
                        occurrenceCounter = occurrenceCounter + 1;
                    } while (!IfAReminderFor2mrw && !isEndDateArrived && questionnaireReminderRuleList.Count > 0);

                    if (lst_AllPersonQuestionnaireSchedule.Count > 0)
                    {
                        //Upload a batch of personQuestionnaireSchedule to DB and get the respective list of ID against Index
                        log.LogInformation($" ReminderNotificationProcess : ProcessRegularReminder : Upload personQuestionnaireSchedule.");
                        var dict_AllpersonSchedules = AddBulkPersonQuestionnaireSchedule(utility, apiurl, lst_AllPersonQuestionnaireSchedule, log);

                        //Update the notify reminders with their personQuestionnaireScheduleIds and upload to DB
                        lst_AllNotifyReminderList?.ForEach(x => x.PersonQuestionnaireScheduleID = dict_AllpersonSchedules[x.PersonQuestionnaireScheduleIndex.Value]);
                        log.LogInformation($" ReminderNotificationProcess : ProcessRegularReminder : Upload notifyReminders.)");
                        AddBulkNotifyReminder(utility, apiurl, lst_AllNotifyReminderList);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// ValidationForDueDates.
        /// Valdiate for PersonCollaboration EndDate/No of Occurences/rangeEndate
        /// </summary>
        /// <param name="lst_dueDate"></param>
        /// <param name="regularReminderRecurrenceDTO"></param>
        /// <param name="personQuestionnaireDTO"></param>
        /// <param name="occurrenceCounter"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        private static bool ValidationForDueDates(ref List<DateTime> lst_dueDate, RegularReminderRecurrenceDTO regularReminderRecurrenceDTO, PersonQuestionnaireDTO personQuestionnaireDTO, int occurrenceCounter, ILogger log)
        {
            try
            {
                log.LogInformation($"FutureReminderScheduleProcess : ValidationForDueDates Start");
                bool isEndDateArrived = false;
                if (lst_dueDate?.Count > 0)
                {
                    log.LogInformation($"FutureReminderScheduleProcess : The final DueDate = {lst_dueDate.OrderByDescending(x => x).FirstOrDefault().Date}");
                    //Valdiate for PersonCollaboration EndDate.
                    if (personQuestionnaireDTO.EndDueDate.HasValue && lst_dueDate?.Count > 0)
                    {
                        var dueDatesAfterpersonQuestionnaireEnd = lst_dueDate.Where(x => x.Date > personQuestionnaireDTO.EndDueDate.Value.Date).Count();
                        if (dueDatesAfterpersonQuestionnaireEnd > 0)
                        {
                            log.LogInformation($"FutureReminderScheduleProcess : Schedule Ended by PersonCollaboration Endate = {personQuestionnaireDTO.EndDueDate.Value}");

                            isEndDateArrived = true;
                            lst_dueDate = lst_dueDate.Where(x => x.Date <= personQuestionnaireDTO.EndDueDate.Value.Date).ToList();
                        }
                    }

                    //Valdiate for RecurrencaRange EndType.
                   
                    if (regularReminderRecurrenceDTO.RecurrenceRangeEndType.ToLower() ==
                        PCISEnum.RecurrenceEndType.EndByNumberOfOccurences && occurrenceCounter - 1 == regularReminderRecurrenceDTO.RecurrenceRangeEndInNumber)
                    {
                        log.LogInformation($"FutureReminderScheduleProcess : Schedule Ended by NumberOfOccurences = {occurrenceCounter}");
                        isEndDateArrived = true;
                        lst_dueDate = new List<DateTime>();
                    }
                    if (regularReminderRecurrenceDTO.RecurrenceRangeEndType.ToLower() ==
                       PCISEnum.RecurrenceEndType.EndByEndate && regularReminderRecurrenceDTO.RecurrenceRangeEndDate.HasValue && lst_dueDate?.Count > 0)
                    {
                        var dueDatesAfterRangeEndDate = lst_dueDate.Where(x => x.Date > regularReminderRecurrenceDTO.RecurrenceRangeEndDate?.Date).Count();
                        if (dueDatesAfterRangeEndDate > 0)
                        {
                            log.LogInformation($"FutureReminderScheduleProcess : Schedule Ended by Range Endate = {regularReminderRecurrenceDTO.RecurrenceRangeEndDate?.Date}");

                            isEndDateArrived = true;
                            lst_dueDate = lst_dueDate.Where(x => x.Date <= regularReminderRecurrenceDTO.RecurrenceRangeEndDate?.Date).ToList();
                        }
                    }
                }

                log.LogInformation($"FutureReminderScheduleProcess : ValidationForDueDates End");
                return isEndDateArrived;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Get RegularAssessment DueDate FromRecurrenceSettings.
        /// </summary>
        /// <param name="recurrenceSettingsInDetailDTO"></param>
        /// <param name="currentDueDate"></param>
        /// <returns></returns>
        private static List<DateTime> GetRegularAssessmentDueDateFromRecurrenceSettings(RegularReminderRecurrenceDTO recurrenceSettingsInDetailDTO, DateTime currentDueDate, int occurrenceCounter)
        {
            try
            {
                List<DateTime> lst_dueDate = new List<DateTime>();
                if(!ValidateForPatternRecurrenceSettings(recurrenceSettingsInDetailDTO))
                {
                    return lst_dueDate;
                }
                switch (recurrenceSettingsInDetailDTO.PatternGroup.ToLower())
                {
                    case PCISEnum.RecurrencePatternGroup.Daily:
                        lst_dueDate = ProcessDailyReminders(recurrenceSettingsInDetailDTO, currentDueDate, occurrenceCounter);
                        break;
                    case PCISEnum.RecurrencePatternGroup.Weekly:
                        lst_dueDate = ProcessWeeklyReminders(recurrenceSettingsInDetailDTO, currentDueDate, occurrenceCounter);
                        break;
                    case PCISEnum.RecurrencePatternGroup.Monthly:
                        lst_dueDate = ProcessMonthlyReminders(recurrenceSettingsInDetailDTO, currentDueDate, occurrenceCounter);
                        break;
                    case PCISEnum.RecurrencePatternGroup.Yearly:
                        lst_dueDate = ProcessYearlyReminders(recurrenceSettingsInDetailDTO, currentDueDate, occurrenceCounter);
                        break;
                }
                lst_dueDate = lst_dueDate?.Distinct().ToList();
                return lst_dueDate;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Added to prevent infinite loop incase of 0 for recurenceinterval and dayNoOfMonth and null for daynames
        /// </summary>
        /// <param name="recurrenceSettingsInDetailDTO"></param>
        /// <returns></returns>
        private static bool ValidateForPatternRecurrenceSettings(RegularReminderRecurrenceDTO recurrenceSettingsInDetailDTO)
        {
            try
            {
                var isValid = true;
                if(recurrenceSettingsInDetailDTO.RecurrencePattern.ToLower() != PCISEnum.RecurrencePattern.DailyWeekdays)
                {
                    isValid = recurrenceSettingsInDetailDTO.RecurrenceInterval > 0 ? true : false;
                }
                if (recurrenceSettingsInDetailDTO.RecurrencePattern.ToLower() == PCISEnum.RecurrencePattern.Weekly)
                {
                    isValid = string.IsNullOrWhiteSpace(recurrenceSettingsInDetailDTO.RecurrenceDayNames) ? false : true;
                }
                if (recurrenceSettingsInDetailDTO.RecurrencePattern.ToLower() == PCISEnum.RecurrencePattern.MonthlyByDay || recurrenceSettingsInDetailDTO.RecurrencePattern.ToLower() == PCISEnum.RecurrencePattern.YearlyByMonth)
                {
                    isValid = recurrenceSettingsInDetailDTO.RecurrenceDayNoOfMonth > 0 ? true : false;
                }
                return isValid;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Calculate duedate for Yearly RecurrenceType.
        /// </summary>
        /// <param name="recurrenceSettingsInDetailDTO"></param>
        /// <param name="startDate"></param>
        /// <returns></returns>
        private static List<DateTime> ProcessYearlyReminders(RegularReminderRecurrenceDTO recurrenceSettingsInDetailDTO, DateTime currentDueDate, int occurrenceCounter)
        {
            try
            {
                var monthNamesSettings = JsonConvert.DeserializeObject<List<LookupDTO>>(recurrenceSettingsInDetailDTO.RecurrenceMonths);
               // var monthNamesSettings = recurrenceSettingsInDetailDTO.RecurrenceMonths.Split(",").ToList();
                List<DateTime> lst_dueDate = new List<DateTime>();
                var nextDueYearDate = currentDueDate;
                if (recurrenceSettingsInDetailDTO.RecurrencePattern.ToLower() == PCISEnum.RecurrencePattern.YearlyByMonth)
                {
                    //If occurrence > 1 increment year by recurrenceInterval
                    if (occurrenceCounter != 1)
                    {
                        nextDueYearDate = nextDueYearDate.AddYears(recurrenceSettingsInDetailDTO.RecurrenceInterval);
                    }
                    do
                    {
                        foreach (var month in monthNamesSettings)
                        {
                            int monthNo = DateTime.ParseExact(month.Name, "MMMM", CultureInfo.InvariantCulture).Month;
                            //For each month in settings check whetehr the day No to be set is valid..if yes proceed
                            if (nextDueYearDate.IsDayInMonthValid(recurrenceSettingsInDetailDTO.RecurrenceDayNoOfMonth))
                            {
                                nextDueYearDate = new DateTime(nextDueYearDate.Year, monthNo, recurrenceSettingsInDetailDTO.RecurrenceDayNoOfMonth);
                                //if ocurrence = 1 check whether next duedate is < startdate then continue loop elase add the date to result LIst.
                                if (occurrenceCounter == 1 && (nextDueYearDate < currentDueDate))
                                {                                    
                                    continue;
                                }
                                else
                                {
                                    lst_dueDate.Add(nextDueYearDate);
                                }
                            }
                        }
                        //increment year by recurrenceInterval
                        nextDueYearDate = nextDueYearDate.AddYears(recurrenceSettingsInDetailDTO.RecurrenceInterval);
                    } while (lst_dueDate.Count == 0);
                }
                if (recurrenceSettingsInDetailDTO.RecurrencePattern.ToLower() == PCISEnum.RecurrencePattern.YearlyByOrdinal)
                {
                    var ordinalsSettings = JsonConvert.DeserializeObject<List<LookupDTO>>(recurrenceSettingsInDetailDTO.RecurrenceOrdinals);
                    var dayNames = JsonConvert.DeserializeObject<List<LookupDTO>>(recurrenceSettingsInDetailDTO.RecurrenceDayNames);
                    //Get Day and Weekday reccurence in a list
                    var dayNotByDayName = dayNames.Where(x => x.Name.ToLower() == PCISEnum.RecurrenceDay.ByDay || x.Name.ToLower() == PCISEnum.RecurrenceDay.ByWeekDay).ToList();
                    dayNames = dayNames.Where(x => x.Name.ToLower() != PCISEnum.RecurrenceDay.ByDay && x.Name.ToLower() != PCISEnum.RecurrenceDay.ByWeekDay).ToList();
                    //Get day of weeks in a list and conevrt to DateTime.DayOfWeek enum
                    var dayOFWeekSettings = dayNames.Select(x => GetDayOfWeekFromDay(x.Name)).ToList();
                    var nextDueYearOrdinalDate = currentDueDate;

                    //If occurrence > 1 increment year by recurrenceInterval
                    if (occurrenceCounter != 1)
                    {
                        nextDueYearOrdinalDate = nextDueYearOrdinalDate.AddYears(recurrenceSettingsInDetailDTO.RecurrenceInterval);
                    }
                    do
                    {
                        foreach (var month in monthNamesSettings)
                        {
                            int monthNo = DateTime.ParseExact(month.Name, "MMMM", CultureInfo.InvariantCulture).Month;
                            //Foreach month in recurrence settings, stay on the 1st day of that month
                            nextDueYearOrdinalDate = new DateTime(nextDueYearOrdinalDate.Year, monthNo, 1);
                            foreach (var ordinal in ordinalsSettings)
                            {
                                //Foreach ordinal get the due dates based on recurrence day settings
                                foreach (var day in dayNotByDayName)
                                {
                                    switch (day.Name.ToLower())
                                    {
                                        case PCISEnum.RecurrenceDay.ByDay:
                                            nextDueYearOrdinalDate = nextDueYearOrdinalDate.FindOrdinalDayOfMonth(ordinal.Name);
                                            break;
                                        case PCISEnum.RecurrenceDay.ByWeekDay:
                                            nextDueYearOrdinalDate = nextDueYearOrdinalDate.FindOrdinalWeekDayOfMonth(ordinal.Name);
                                            break;
                                    }
                                    if (nextDueYearOrdinalDate < currentDueDate && occurrenceCounter == 1)
                                    {
                                        continue;
                                    }
                                    if (nextDueYearOrdinalDate != DateTime.MinValue)
                                    {
                                        lst_dueDate.Add(nextDueYearOrdinalDate);
                                    }
                                }

                                foreach (var recurDay in dayOFWeekSettings)
                                {
                                    nextDueYearOrdinalDate = nextDueYearOrdinalDate.FindTheOrdinalDayNameOfMonth(recurDay, ordinal.Name);

                                    if (nextDueYearOrdinalDate < currentDueDate && occurrenceCounter == 1)
                                    {
                                        continue;
                                    }
                                    if (nextDueYearOrdinalDate != DateTime.MinValue)
                                    {
                                        lst_dueDate.Add(nextDueYearOrdinalDate);
                                    }
                                }
                            }
                        }
                        //increment year by recurrenceInterval
                        nextDueYearOrdinalDate = nextDueYearOrdinalDate.AddYears(recurrenceSettingsInDetailDTO.RecurrenceInterval);
                    } while (lst_dueDate.Count == 0);
                }
                return lst_dueDate;

            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Calculate duedate for Monthly RecurrenceType
        /// </summary>
        /// <param name="recurrenceSettingsInDetailDTO"></param>
        /// <param name="currentDueDate"></param>
        /// <returns></returns>
        private static List<DateTime> ProcessMonthlyReminders(RegularReminderRecurrenceDTO recurrenceSettingsInDetailDTO, DateTime currentDueDate, int occurrenceCounter)
        {
            try
            {

                List<DateTime> lst_dueDate = new List<DateTime>();
                if (recurrenceSettingsInDetailDTO.RecurrencePattern.ToLower() == PCISEnum.RecurrencePattern.MonthlyByDay)
                {
                    var nextDueMonthDate = currentDueDate;
                    //For 1st occurrence find the ordinal days in same month else increment month by the repeatinterval
                    //For occurrence > 1 increment month by the repeatinterval and days. 
                    if (occurrenceCounter != 1)
                    {
                        nextDueMonthDate = nextDueMonthDate.AddMonths(recurrenceSettingsInDetailDTO.RecurrenceInterval);
                    }
                    do
                    {
                        //if the required day is Valid for current year and month proceed.
                        if (nextDueMonthDate.IsDayInMonthValid(recurrenceSettingsInDetailDTO.RecurrenceDayNoOfMonth))
                        {
                            nextDueMonthDate = new DateTime(nextDueMonthDate.Year, nextDueMonthDate.Month, recurrenceSettingsInDetailDTO.RecurrenceDayNoOfMonth);
                            //if duedate  is less than startdate and occurenc = 1 then continue else add to list..
                            //please note the NOT operator in if condition.
                            if (!(occurrenceCounter == 1 && nextDueMonthDate < currentDueDate))
                            {
                                lst_dueDate.Add(nextDueMonthDate);
                            }
                        }
                        nextDueMonthDate = nextDueMonthDate.AddMonths(recurrenceSettingsInDetailDTO.RecurrenceInterval);
                    } while (lst_dueDate.Count == 0);//add atleast one duedate.
                }
                if (recurrenceSettingsInDetailDTO.RecurrencePattern.ToLower() == PCISEnum.RecurrencePattern.MonthlyByOrdinalDay)
                {
                    var ordinalsSettings = JsonConvert.DeserializeObject<List<LookupDTO>>(recurrenceSettingsInDetailDTO.RecurrenceOrdinals);
                    var dayNames = JsonConvert.DeserializeObject<List<LookupDTO>>(recurrenceSettingsInDetailDTO.RecurrenceDayNames);
                    //Get Day and Weekday reccurence in a list
                    var dayNotByDayName = dayNames.Where(x => x.Name.ToLower() == PCISEnum.RecurrenceDay.ByDay || x.Name.ToLower() == PCISEnum.RecurrenceDay.ByWeekDay).ToList();
                    dayNames = dayNames.Where(x => x.Name.ToLower() != PCISEnum.RecurrenceDay.ByDay && x.Name.ToLower() != PCISEnum.RecurrenceDay.ByWeekDay).ToList();
                    //Get day of weeks in a list and conevrt to DateTime.DayOfWeek enum
                    var dayOFWeekSettings = dayNames.Select(x => GetDayOfWeekFromDay(x.Name)).ToList();
                    var nextDueMonthOrdinalDate = currentDueDate;
                    //For 1st occurrence find the ordinal days in same month else increment month by the repeatinterval
                    //For occurrence > 1 increment month by the repeatinterval and find the ordinal days. 
                    if (occurrenceCounter != 1)
                    {
                        nextDueMonthOrdinalDate = nextDueMonthOrdinalDate.AddMonths(recurrenceSettingsInDetailDTO.RecurrenceInterval);
                    }
                    do
                    {
                        foreach (var ordinal in ordinalsSettings.OrderBy(x => x.Name))
                        {
                            foreach (var day in dayNotByDayName)
                            {
                                switch (day.Name.ToLower())
                                {
                                    case PCISEnum.RecurrenceDay.ByDay:
                                        nextDueMonthOrdinalDate = nextDueMonthOrdinalDate.FindOrdinalDayOfMonth(ordinal.Name);
                                        break;
                                    case PCISEnum.RecurrenceDay.ByWeekDay:
                                        nextDueMonthOrdinalDate = nextDueMonthOrdinalDate.FindOrdinalWeekDayOfMonth(ordinal.Name);
                                        break;
                                }
                                if (nextDueMonthOrdinalDate < currentDueDate && occurrenceCounter == 1)
                                {
                                    continue;
                                }
                                else
                                {
                                    lst_dueDate.Add(nextDueMonthOrdinalDate);
                                }
                            }
                            foreach (var recurDay in dayOFWeekSettings)
                            {
                                nextDueMonthOrdinalDate = nextDueMonthOrdinalDate.FindTheOrdinalDayNameOfMonth(recurDay, ordinal.Name);

                                if (nextDueMonthOrdinalDate < currentDueDate && occurrenceCounter == 1)
                                {
                                    continue;
                                }
                                if (nextDueMonthOrdinalDate != DateTime.MinValue)
                                {
                                    lst_dueDate.Add(nextDueMonthOrdinalDate);
                                }
                            }
                        }
                        nextDueMonthOrdinalDate = nextDueMonthOrdinalDate.AddMonths(recurrenceSettingsInDetailDTO.RecurrenceInterval);
                    } while (lst_dueDate.Count == 0);
                }
                return lst_dueDate;
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Calculate duedate for Weekly RecurrenceType
        /// </summary>
        /// <param name="recurrenceSettingsInDetailDTO"></param>
        /// <param name="currentDueDate"></param>
        /// <returns></returns>
        private static List<DateTime> ProcessWeeklyReminders(RegularReminderRecurrenceDTO recurrenceSettingsInDetailDTO, DateTime currentDueDate, int occurrenceCounter)
        {
            try
            {
                var dayNames = JsonConvert.DeserializeObject<List<LookupDTO>>(recurrenceSettingsInDetailDTO.RecurrenceDayNames);
                //Recurence Day names as DateTime.DayOfWeek enum
                var dayOFWeekSettings = dayNames.Select(x => GetDayOfWeekFromDay(x.Name)).ToList();
                List<DateTime> lst_dueDate = new List<DateTime>();
                var nextDueWeekDate = currentDueDate;
                //Week = Sunday..to..Saturday
                //For a dueDate Find StartDate Of The Week.
                //Loop through each day upto next sunday and get all required dayof week
                if (occurrenceCounter != 1)
                {
                    nextDueWeekDate = nextDueWeekDate.AddDays(7 * recurrenceSettingsInDetailDTO.RecurrenceInterval);
                }
                do
                {
                    nextDueWeekDate = nextDueWeekDate.FindStartDateOfTheWeek();
                    do
                    {
                        if (nextDueWeekDate < currentDueDate && occurrenceCounter == 1)
                        {
                            nextDueWeekDate = nextDueWeekDate.AddDays(1);
                            continue;
                        }
                        else
                        {
                            if (dayOFWeekSettings.Contains(nextDueWeekDate.DayOfWeek))
                            {
                                lst_dueDate.Add(nextDueWeekDate);
                            }
                            nextDueWeekDate = nextDueWeekDate.AddDays(1);
                        }
                    } while (nextDueWeekDate.DayOfWeek != DayOfWeek.Sunday);
                    nextDueWeekDate = nextDueWeekDate.AddDays(7 * recurrenceSettingsInDetailDTO.RecurrenceInterval);
                } while (lst_dueDate.Count == 0);
                return lst_dueDate;
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Calculate duedate for Daily RecurrenceType
        /// </summary>
        /// <param name="recurrenceSettingsInDetailDTO"></param>
        /// <param name="currentDueDate"></param>
        /// <returns></returns>
        private static List<DateTime> ProcessDailyReminders(RegularReminderRecurrenceDTO recurrenceSettingsInDetailDTO, DateTime currentDueDate, int occurrenceCounter)
        {
            try
            {
                List<DateTime> lst_dueDate = new List<DateTime>();
                if (recurrenceSettingsInDetailDTO.RecurrencePattern.ToLower() == PCISEnum.RecurrencePattern.DailyDays)
                {
                    lst_dueDate.Add(currentDueDate.AddDays(recurrenceSettingsInDetailDTO.RecurrenceInterval));
                }
                else if (recurrenceSettingsInDetailDTO.RecurrencePattern.ToLower() == PCISEnum.RecurrencePattern.DailyWeekdays)
                {
                    //If duedate is saturday or sunday move it to a weekday.
                    var nextdayDate = occurrenceCounter == 1 ? currentDueDate : currentDueDate.AddDays(1);
                    if (nextdayDate.DayOfWeek == DayOfWeek.Saturday)
                        lst_dueDate.Add(nextdayDate.AddDays(2));
                    else if (nextdayDate.DayOfWeek == DayOfWeek.Sunday)
                        lst_dueDate.Add(nextdayDate.AddDays(1));
                    else
                        lst_dueDate.Add(nextdayDate);
                }
                return lst_dueDate;
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Get DayOfWeek From a day
        /// </summary>
        /// <param name="recurDay"></param>
        /// <returns></returns>
        private static DayOfWeek GetDayOfWeekFromDay(string recurDay)
        {
            DayOfWeek dayOfWeek = DayOfWeek.Monday;
            switch (recurDay.ToLower())
            {
                case PCISEnum.RecurrenceDay.Sunday:
                    dayOfWeek = DayOfWeek.Sunday;
                    break;
                case PCISEnum.RecurrenceDay.Monday:
                    dayOfWeek = DayOfWeek.Monday;
                    break;
                case PCISEnum.RecurrenceDay.Tuesday:
                    dayOfWeek = DayOfWeek.Tuesday;
                    break;
                case PCISEnum.RecurrenceDay.Wednesday:
                    dayOfWeek = DayOfWeek.Wednesday;
                    break;
                case PCISEnum.RecurrenceDay.Thursday:
                    dayOfWeek = DayOfWeek.Thursday;
                    break;
                case PCISEnum.RecurrenceDay.Friday:
                    dayOfWeek = DayOfWeek.Friday;
                    break;
                case PCISEnum.RecurrenceDay.Saturday:
                    dayOfWeek = DayOfWeek.Saturday;
                    break;
                default:
                    break;
            }
            return dayOfWeek;
        }        
        #endregion
    }

}
