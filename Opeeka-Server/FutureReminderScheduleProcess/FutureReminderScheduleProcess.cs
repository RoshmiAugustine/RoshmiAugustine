using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using FutureReminderScheduleProcess.Common;
using FutureReminderScheduleProcess.DTO;
using FutureReminderScheduleProcess.Enums;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FutureReminderScheduleProcess
{
    public static class FutureReminderScheduleProcess
    {
        /// <summary>
        /// Azure Function for scheduling future Reminders (PCIS-3165)
        /// </summary>
        /// <param name="myTimer"></param>
        /// <param name="log"></param>
        [FunctionName("FutureReminderScheduleProcess")]
        public static void Run([TimerTrigger("%timer-frequency%")] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"FutureReminderScheduleProcess : {DateTime.Now} : Azure function started.");

            try
            {
                Utility Utility = new Utility();
                var apiurl = Environment.GetEnvironmentVariable(PCISEnum.EnvironmentVariables.ApiUrlFromKeyVault, EnvironmentVariableTarget.Process);
                //Fetching the lastRuntime from DB.
                var processLog = GetBackgroundProcessLog(Utility, apiurl, PCISEnum.BackgroundProcess.TriggerFutureReminder);
                var LastRunTime = processLog?.LastProcessedDate;
                DateTime currentRunTime = DateTime.UtcNow;

                //Get the configuration for Reminder Count After WindowCloseDay.
                var questionnaireLateCountLimit = Convert.ToInt32(GetConfiguration(Utility, apiurl, PCISEnum.ConfigurationKey.Reminder_Count_After_WindowCloseDay));

                //Get ALL PersonQuestioners to be scheduled.
                var AllPersonQuestionnaires = GetAllPersonQuestionnairesToBeScheduled(Utility, apiurl, log);
                var lst_allQuestionnaireIds = AllPersonQuestionnaires.list_QuestionnaireIds.OrderByDescending(x=>x).ToList() ?? new List<int>();
                var lst_allPersonQuestionnaireIds = AllPersonQuestionnaires.list_PersonQuestionnaireIds.OrderByDescending(x => x).ToList() ?? new List<long>();

                if (lst_allPersonQuestionnaireIds?.Count > 0)
                {
                    //Get All Reminder Types
                    var questionnaireReminderTypes = GetQuestionnaireReminderType(Utility, apiurl, log);

                    //Get all Recurrence Settings and Time Rule settings for all distinctquestionnaires included
                    var allQuestionnaireRegularReminderSettings = GetRegularReminderSettingsForAllQuestionnaire(Utility, apiurl, lst_allQuestionnaireIds, log);

                    //Get all reminder rules set for all distinctquestionnaires included
                    var allQuestionnaireReminderRuleList = GetReminderRulesForAllQuestionnaire(Utility, apiurl, lst_allQuestionnaireIds, log);

                    //Get all reminder Time rules set for all distinct Personquestionnaires included
                    var allPersonCollaborationList = GetPersonCollaborationForAllQuestionnaire(Utility, apiurl, lst_allPersonQuestionnaireIds, log);

                    //Start the schedule creation as batches of all fetched personQuestionnaires
                    int loopCount = lst_allPersonQuestionnaireIds.Count;
                    for (int i = 0; i < loopCount; i = i + PCISEnum.Constants.UploadBlockCount)
                    {
                        List<PersonQuestionnaireRegularScheduleDTO> lst_AllpersonQuestionnaireSchedule = new List<PersonQuestionnaireRegularScheduleDTO>();
                        List<NotifyReminderDTO> lst_AllnotifyReminderList = new List<NotifyReminderDTO>();

                        var personQuestioniresTobeProcessed = lst_allPersonQuestionnaireIds.Skip(i).Take(PCISEnum.Constants.UploadBlockCount).ToList();
                        log.LogInformation($"$$$$=================================================================$$$$)");
                        log.LogInformation($"FutureReminderScheduleProcess : {DateTime.Now} : Start a batch with count = {personQuestioniresTobeProcessed.Count})");

                        //Get ALL Last Occurrence Schedules with its MAx Due date.
                        var scheduleLastReminderDetails = GetAllLastOccurredSchedulesWithDueDates(Utility, apiurl, log, personQuestioniresTobeProcessed);
                        log.LogInformation($"FutureReminderScheduleProcess : {DateTime.Now} : ##### PersonQuestionnaires - Total Count : {scheduleLastReminderDetails?.Count } #######");
                                                
                        PersonCollaborationDTO personCollaborationDTO;
                        //Process each personQuestionire from the batch
                        foreach (var item in scheduleLastReminderDetails)
                        {
                            List<PersonQuestionnaireRegularScheduleDTO> lst_personQuestionnaireSchedule = new List<PersonQuestionnaireRegularScheduleDTO>();
                            List<NotifyReminderDTO> lst_notifyReminderList = new List<NotifyReminderDTO>();
                            var questionnaireReminderRuleList = allQuestionnaireReminderRuleList.Where(x => x.QuestionnaireID == item.QuestionnaireID & x.IsSelected == true).ToList();

                            personCollaborationDTO = new PersonCollaborationDTO();
                            if (allPersonCollaborationList != null && allPersonCollaborationList.Count > 0)
                            {
                                var personCollaboration = allPersonCollaborationList?.Where(x => x.PersonQuestionnaireID == item.PersonQuestionnaireID).ToList();
                                if (personCollaboration != null && personCollaboration.Count > 0)
                                {
                                    personCollaborationDTO = personCollaboration.FirstOrDefault();
                                }
                            }
                            var regularReminderSettings = allQuestionnaireRegularReminderSettings.Recurrence.Where(x => x.QuestionnaireID == item.QuestionnaireID).FirstOrDefault();
                            var regularReminderTimeSettings = allQuestionnaireRegularReminderSettings.TimeRule.Where(x => x.QuestionnaireID == item.QuestionnaireID).ToList();
                            if (regularReminderSettings != null && questionnaireReminderRuleList?.Count > 0)
                            {
                                ProcessRegularIntervalWithRecurrencePattern(Utility, apiurl, item, questionnaireReminderRuleList, questionnaireReminderTypes, lst_personQuestionnaireSchedule, lst_notifyReminderList, questionnaireLateCountLimit, regularReminderSettings, regularReminderTimeSettings, personCollaborationDTO, log);
                            }
                            lst_AllpersonQuestionnaireSchedule.AddRange(lst_personQuestionnaireSchedule);
                            lst_AllnotifyReminderList.AddRange(lst_notifyReminderList);
                        }

                        log.LogInformation($"FutureReminderScheduleProcess : {DateTime.Now} : personQuestionnaireSchedule To Be Uploaded Count: {lst_AllpersonQuestionnaireSchedule.Count}");
                        if (lst_AllpersonQuestionnaireSchedule.Count > 0)
                        {
                            //Upload a batch of personQuestionnaireSchedule to DB and get the respective list of ID against Index
                            log.LogInformation($"FutureReminderScheduleProcess : {DateTime.Now} : Upload personQuestionnaireSchedule for a batch.)");
                            var dict_AllpersonSchedules = AddBulkPersonQuestionnaireSchedule(Utility, apiurl, lst_AllpersonQuestionnaireSchedule, log);

                            //Update the notify reminders with their personQuestionnaireScheduleIds and upload to DB
                            lst_AllnotifyReminderList.ForEach(x => x.PersonQuestionnaireScheduleID = dict_AllpersonSchedules[x.PersonQuestionnaireScheduleIndex]);
                            log.LogInformation($"FutureReminderScheduleProcess : {DateTime.Now} : Upload notifyReminders for a batch.");
                            AddBulkNotifyReminder(Utility, apiurl, lst_AllnotifyReminderList);
                        }

                        log.LogInformation($"FutureReminderScheduleProcess : {DateTime.Now} : End of a batch.");
                    }
                }
                //Update Current RunTime to DB
                UpdateProcessLastRunTime(Utility, apiurl, currentRunTime);
                log.LogInformation($"FutureReminderScheduleProcess : {DateTime.Now} : Azure function Ended.");
            }
            catch (Exception ex)
            {
                log.LogError(ex, $@"FutureReminderScheduleProcess : Exception occurred.Exception Message : {ex.Message}.Location : {ex.StackTrace}.Details : {ex.InnerException}");
            }
        }

        /// <summary>
        /// Get all personcollaboration start and end date for personquestionnaires.
        /// </summary>
        /// <param name="utility"></param>
        /// <param name="apiurl"></param>
        /// <param name="allpersonQuestionnaireIDs"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        private static List<PersonCollaborationDTO> GetPersonCollaborationForAllQuestionnaire(Utility utility, string apiurl, List<long> allpersonQuestionnaireIDs, ILogger log)
        {

            try
            {
                List<PersonCollaborationDTO> personCollaborationDTO = new List<PersonCollaborationDTO>();
                var url = apiurl + PCISEnum.APIurl.GetAllPersonCollaboration;
                log.LogInformation($"FutureReminderScheduleProcess : {DateTime.Now} : GetQuestionnaireReminderRulesByQuestionnaire url : {url}");
                var result = utility.RestApiCall(url, false, false, PCISEnum.APIMethodType.PostRequest, null, allpersonQuestionnaireIDs.ToJSON());
                if (!string.IsNullOrEmpty(result))
                {
                    var personCollaborationResponse = JsonConvert.DeserializeObject<PersonCollaborationDetailsDTO>(result);
                    personCollaborationDTO = personCollaborationResponse?.result?.personCollaborations;
                }
                return personCollaborationDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///  To schedule regular interval.
        /// </summary>
        /// <param name="Utility"></param>
        /// <param name="apiurl"></param>
        /// <param name="personQuestionnaireScheduleDTO"></param>
        /// <param name="questionnaireReminderRuleList"></param>
        /// <param name="questionnaireReminderTypes"></param>
        /// <param name="log"></param>
        public static void ProcessRegularInterval(Utility Utility, string apiurl, PersonQuestionnaireRegularScheduleDTO personQuestionnaireScheduleDTO, List<QuestionnaireReminderRulesDTO> questionnaireReminderRuleList, List<QuestionnaireReminderTypeDTO> questionnaireReminderTypes, List<PersonQuestionnaireRegularScheduleDTO> lst_personQuestionnaireSchedule, List<NotifyReminderDTO> lst_notifyReminderList, int questionnaireLateCountLimit, ILogger log)
        {
            try
            {
                log.LogInformation($"FutureReminderScheduleProcess : {DateTime.Now} : {personQuestionnaireScheduleDTO.PersonQuestionnaireID} : ****Start schedule process****");
                var daysToBeAddedToOpenDay = (personQuestionnaireScheduleDTO.RepeatIntervalDays ?? 0) - personQuestionnaireScheduleDTO.WindowOpenOffsetDays;
                var daysToBeAddedToCloseDay = (personQuestionnaireScheduleDTO.RepeatIntervalDays ?? 0) + personQuestionnaireScheduleDTO.WindowCloseOffsetDays;
                bool completed = false;
                var startDate = personQuestionnaireScheduleDTO.CurrentlyScheduled;
                log.LogInformation($"FutureReminderScheduleProcess : {DateTime.Now} : {personQuestionnaireScheduleDTO.PersonQuestionnaireID} : Calculate schedule dates.");

                //Generate the schedule/window dates for a regular assessment
                List<ScheduleWindowDetails> lst_ScheduleDetails = new List<ScheduleWindowDetails>();
                while (!completed)
                {
                    ScheduleWindowDetails scheduleDetails = new ScheduleWindowDetails();
                    scheduleDetails.WindowOpenDay = personQuestionnaireScheduleDTO.IsSelected ? startDate.AddDays(daysToBeAddedToOpenDay) : startDate;
                    scheduleDetails.WindowDueDate = startDate.AddDays(personQuestionnaireScheduleDTO.RepeatIntervalDays ?? 0);
                    scheduleDetails.WindowCloseDay = personQuestionnaireScheduleDTO.IsSelected ? startDate.AddDays(daysToBeAddedToCloseDay) : startDate;

                    lst_ScheduleDetails.Add(scheduleDetails);

                    startDate = scheduleDetails.WindowDueDate;
                    completed = personQuestionnaireScheduleDTO.ToBeScheduled.HasValue ? (personQuestionnaireScheduleDTO.TwelveMonthsFromToday < startDate.AddDays(personQuestionnaireScheduleDTO.RepeatIntervalDays ?? 0)) : (personQuestionnaireScheduleDTO.TwelveMonthsFromToday < startDate.AddDays(personQuestionnaireScheduleDTO.RepeatIntervalDays ?? 0));                    
                }

                log.LogInformation($"FutureReminderScheduleProcess : {DateTime.Now} : {personQuestionnaireScheduleDTO.PersonQuestionnaireID} : Create schedules : From = { personQuestionnaireScheduleDTO.CurrentlyScheduled.Date} : Interval = {personQuestionnaireScheduleDTO.RepeatIntervalDays ?? 0} : Total = {lst_ScheduleDetails.Count}");

                //create the schedule and notify reminders
                foreach (var schedules in lst_ScheduleDetails)
                {
                    var personQuestionnaireScheduleIndex = Guid.NewGuid();
                    PersonQuestionnaireRegularScheduleDTO personQuestionnaireSchedule = new PersonQuestionnaireRegularScheduleDTO
                    {
                        PersonQuestionnaireID = personQuestionnaireScheduleDTO.PersonQuestionnaireID,
                        QuestionnaireWindowID = personQuestionnaireScheduleDTO.QuestionnaireWindowID,
                        WindowDueDate = schedules.WindowDueDate,
                        PersonQuestionnaireScheduleIndex = personQuestionnaireScheduleIndex//an index is kept to track records
                    };
                    lst_personQuestionnaireSchedule.Add(personQuestionnaireSchedule);

                    //Create the notifyReminderDTO for selected reminder rules only.
                    foreach (var item in questionnaireReminderRuleList)
                    {
                        var questionnaireReminderType = questionnaireReminderTypes.Where(x => x.QuestionnaireReminderTypeID == item.QuestionnaireReminderTypeID).FirstOrDefault();
                        var notifyReminderToAdd = CreateNotifyReminder(Utility, apiurl, questionnaireReminderType.Name, personQuestionnaireScheduleIndex, schedules.WindowOpenDay, schedules.WindowDueDate, schedules.WindowCloseDay, item, questionnaireLateCountLimit);
                        lst_notifyReminderList.AddRange(notifyReminderToAdd);
                    }
                }

                log.LogInformation($"FutureReminderScheduleProcess : {DateTime.Now} : {personQuestionnaireScheduleDTO.PersonQuestionnaireID} : ****End schedule process****");
            }
            catch(Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// To add Bulk Reminders.
        /// </summary>
        /// <param name="Utility"></param>
        /// <param name="apiurl"></param>
        /// <param name="notifyReminders"></param>
        /// <returns></returns>
        private static CRUDResponse AddBulkNotifyReminder(Utility Utility, string apiurl, List<NotifyReminderDTO> notifyReminders)
        {
            try
            {
                CRUDResponse Response = new CRUDResponse();
                if (notifyReminders.Count == 0) return Response; 
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

        /// <summary>
        /// To create notify reminder.
        /// Update as part of PCIS-3225 : To calculate notifydate based on day/hour offsetType.
        /// </summary>
        /// <param name="Utility"></param>
        /// <param name="apiurl"></param>
        /// <param name="reminderTypeName"></param>
        /// <param name="personQuestionnaireScheduleIndex"></param>
        /// <param name="windowOpenDay"></param>
        /// <param name="windowDueDate"></param>
        /// <param name="windowCloseDay"></param>
        /// <param name="questionnaireReminderRulesDTO"></param>
        /// <returns></returns>
        public static List<NotifyReminderDTO> CreateNotifyReminder(Utility Utility, string apiurl, string reminderTypeName, Guid personQuestionnaireScheduleIndex, DateTime windowOpenDay, DateTime windowDueDate, DateTime windowCloseDay, QuestionnaireReminderRulesDTO questionnaireReminderRulesDTO, int questionnaireLateCountLimit)
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
                            PersonQuestionnaireScheduleIndex = personQuestionnaireScheduleIndex,
                            QuestionnaireReminderRuleID = questionnaireReminderRulesDTO.QuestionnaireReminderRuleID,
                            NotifyReminderTypeName = PCISEnum.QuestionnaireReminderType.WindowOpen,
                            NotifyDate = windowOpenDay.Date.AddDaysOrHours(-questionnaireReminderRulesDTO.ReminderOffsetDays ?? default(int), questionnaireReminderRulesDTO.ReminderOffsetTypeID)
                        };
                        notifyReminderList.Add(notifyReminder);
                        break;
                    case PCISEnum.QuestionnaireReminderType.AssesmentDue:
                        notifyReminder = new NotifyReminderDTO
                        {
                            NotifyReminderTypeName = PCISEnum.QuestionnaireReminderType.AssesmentDue,
                            PersonQuestionnaireScheduleIndex = personQuestionnaireScheduleIndex,
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
                            QuestionnaireReminderRuleID = questionnaireReminderRulesDTO.QuestionnaireReminderRuleID,
                            NotifyDate = windowCloseDay
                        };
                        notifyReminderList.Add(notifyReminder);
                        break;
                    case PCISEnum.QuestionnaireReminderType.QuestionnaireLate:
                        var closeDay = windowCloseDay.AddDaysOrHours(questionnaireReminderRulesDTO.ReminderOffsetDays ?? default(int), questionnaireReminderRulesDTO.ReminderOffsetTypeID);
                        for (var i = 0; i < questionnaireLateCountLimit; i++)
                        {
                            notifyReminder = new NotifyReminderDTO
                            {
                                NotifyReminderTypeName = PCISEnum.QuestionnaireReminderType.QuestionnaireLate,
                                PersonQuestionnaireScheduleIndex = personQuestionnaireScheduleIndex,
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
        
        /// <summary>
        /// To get configuration
        /// </summary>
        /// <param name="Utility"></param>
        /// <param name="apiurl"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private static string GetConfiguration(Utility Utility, string apiurl, string key)
        {
            try
            {
                string configValue = string.Empty;
                var configAPiurl = apiurl + PCISEnum.APIurl.ConfigurationWithKey.Replace(PCISEnum.APIReplacableValues.Key, key).Replace(PCISEnum.APIReplacableValues.AgencyID, "0");
                var result = Utility.RestApiCall(configAPiurl, false);
                if (!string.IsNullOrEmpty(result))
                {
                    var config = JsonConvert.DeserializeObject<ConfigurationResponseDTO>(result);
                    configValue = config.result.ConfigurationValue;
                }
                return configValue;
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
        private static Dictionary<Guid, long> AddBulkPersonQuestionnaireSchedule(Utility Utility, string apiurl, List<PersonQuestionnaireRegularScheduleDTO> personQuestionnaireSchedules, ILogger log)
        {
            try
            {
                Dictionary<Guid, long> Response = new Dictionary<Guid, long>();
                if (personQuestionnaireSchedules.Count == 0) return Response;
                var url = apiurl + PCISEnum.APIurl.PersonQuestionnaireSchedule;
                var result = Utility.RestApiCall(url, false, false, PCISEnum.APIMethodType.PostRequest, null, personQuestionnaireSchedules.ToJSON());
                if (!string.IsNullOrEmpty(result))
                {
                    var TotalResponse = JsonConvert.DeserializeObject<AddPersonQuestionnaireScheduleResponseDTO>(result);
                    Response = JsonConvert.DeserializeObject<Dictionary<Guid,long>>(TotalResponse.result.PersonQuestionnaireSchedule);
                }
                return Response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// To get all person questionnaire schedules.
        /// </summary>
        /// <param name="Utility"></param>
        /// <param name="apiurl"></param>
        /// <returns></returns>
        private static PersonQuestionnaireRegularScheduleDetails GetAllPersonQuestionnairesToBeScheduled(Utility Utility, string apiurl, ILogger log)
        {
            try
            {
                PersonQuestionnaireRegularScheduleDetails PersonQuestionnaireSchedule = new PersonQuestionnaireRegularScheduleDetails();
                var url = apiurl + PCISEnum.APIurl.GetAllPersonQuestionnairesToBeScheduled;
                log.LogInformation($"FutureReminderScheduleProcess : {DateTime.Now} : GetAllPersonQuestionnairesSchedule url : {url}");
                var result = Utility.RestApiCall(url, false);
                if (!string.IsNullOrEmpty(result))
                {
                    var PersonQuestionnaireScheduleResponse = JsonConvert.DeserializeObject<PersonQuestionnairesRegularScheduleDetailsDTO>(result);
                    PersonQuestionnaireSchedule = PersonQuestionnaireScheduleResponse?.result;
                }
                return PersonQuestionnaireSchedule;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// To get questionnaire reminder rules by questionnaire.
        /// </summary>
        /// <param name="Utility"></param>
        /// <param name="apiurl"></param>
        /// <param name="QuestionnaireId"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        private static List<QuestionnaireReminderRulesDTO> GetReminderRulesForAllQuestionnaire(Utility Utility, string apiurl, List<int> QuestionnaireIds, ILogger log)
        {
            try
            {
                List<QuestionnaireReminderRulesDTO> QuestionnaireReminderRule = new List<QuestionnaireReminderRulesDTO>();
                var url = apiurl + PCISEnum.APIurl.GetAllQuestionnaireReminderRule;
                log.LogInformation($"FutureReminderScheduleProcess : {DateTime.Now} : GetQuestionnaireReminderRulesByQuestionnaire url : {url}");
                var result = Utility.RestApiCall(url, false, false, PCISEnum.APIMethodType.PostRequest, null, QuestionnaireIds.ToJSON());
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

        /// <summary>
        /// To get questionnaire reminder type.
        /// </summary>
        /// <param name="Utility"></param>
        /// <param name="apiurl"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        private static List<QuestionnaireReminderTypeDTO> GetQuestionnaireReminderType(Utility Utility, string apiurl, ILogger log)
        {
            try
            {
                List<QuestionnaireReminderTypeDTO> QuestionnaireReminderType = new List<QuestionnaireReminderTypeDTO>();
                var url = apiurl + PCISEnum.APIurl.AllQuestionnaireReminderType;
                log.LogInformation($"FutureReminderScheduleProcess : {DateTime.Now} : GetQuestionnaireReminderType url : {url}");

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
        /// <summary>
        /// To get all person questionnaire schedules.
        /// </summary>
        /// <param name="Utility"></param>
        /// <param name="apiurl"></param>
        /// <returns></returns>
        private static List<PersonQuestionnaireRegularScheduleDTO> GetAllLastOccurredSchedulesWithDueDates(Utility Utility, string apiurl, ILogger log, List<long> personQuestioniresTobeProcessed)
        {
            try
            {
                List<PersonQuestionnaireRegularScheduleDTO> PersonQuestionnaireSchedule = new List<PersonQuestionnaireRegularScheduleDTO>();
                var url = apiurl + PCISEnum.APIurl.GetSchedulesWithMaxEndDate;
                log.LogInformation($"FutureReminderScheduleProcess : {DateTime.Now} : GetAllPersonQuestionnairesSchedule url : {url}");
                var result = Utility.RestApiCall(url, false, false, PCISEnum.APIMethodType.PostRequest, null, personQuestioniresTobeProcessed.ToJSON());
                if (!string.IsNullOrEmpty(result))
                {
                    var PersonQuestionnaireScheduleResponse = JsonConvert.DeserializeObject<PersonQuestionnairesRegularScheduleDetailsDTO>(result);
                    PersonQuestionnaireSchedule = PersonQuestionnaireScheduleResponse?.result?.personQuestionnaireSchedules;
                }
                return PersonQuestionnaireSchedule;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static void UpdateProcessLastRunTime(Utility utility, string apiURL, DateTime currentRunTime)
        {
            try
            {
                var processLog = GetBackgroundProcessLog(utility, apiURL, PCISEnum.BackgroundProcess.TriggerFutureReminder);
                if (processLog == null)
                {
                    processLog = new BackgroundProcessLogDTO()
                    {
                        ProcessName = PCISEnum.BackgroundProcess.TriggerFutureReminder,
                        LastProcessedDate = DateTime.UtcNow.Date.AddDays(-1)
                    };
                    AddBackgroundProcessLog(utility, apiURL, processLog);
                }
                else
                {
                    processLog.LastProcessedDate = currentRunTime;
                    var result = UpdateBackgroundProcessLog(utility, apiURL, processLog);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private static CRUDResponse UpdateBackgroundProcessLog(Utility Utility, string apiurl, BackgroundProcessLogDTO backgroundProcess)
        {
            try
            {
                CRUDResponse Response = new CRUDResponse();
                var url = apiurl + PCISEnum.APIurl.BackgroundProcess;
                var result = Utility.RestApiCall(url, false, false, PCISEnum.APIMethodType.PutRequest, null, backgroundProcess.ToJSON());
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

        private static CRUDResponse AddBackgroundProcessLog(Utility Utility, string apiurl, BackgroundProcessLogDTO backgroundProcess)
        {
            try
            {
                CRUDResponse Response = new CRUDResponse();
                var url = apiurl + PCISEnum.APIurl.BackgroundProcess;
                var result = Utility.RestApiCall(url, false, false, PCISEnum.APIMethodType.PostRequest, null, backgroundProcess.ToJSON());
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
        private static BackgroundProcessLogDTO GetBackgroundProcessLog(Utility utility, string apiurl, string name)
        {
            try
            {
                BackgroundProcessLogDTO response = new BackgroundProcessLogDTO();
                var configAPiurl = apiurl + PCISEnum.APIurl.GetBackgroundProcess.Replace(PCISEnum.APIReplacableValues.Name, Convert.ToString(name));
                var result = utility.RestApiCall(configAPiurl, false);
                if (!string.IsNullOrEmpty(result))
                {
                    var responseDetails = JsonConvert.DeserializeObject<BackgroundProcessResponseDTO>(result);
                    response = responseDetails.result.BackgroundProcess;
                }
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// ProcessRegularIntervalWithRecurrencePattern - Created as part of PCIS-3225
        /// This function always make sure to create a schedule for an occurrence with first reminder notifyDate = currentDay+1. 
        /// </summary>
        /// <param name="utility"></param>
        /// <param name="apiurl"></param>
        /// <param name="personQuestionnaireScheduleDTO"></param>
        /// <param name="questionnaireReminderRuleList"></param>
        /// <param name="questionnaireReminderTypes"></param>
        /// <param name="lst_AllPersonQuestionnaireSchedule"></param>
        /// <param name="lst_AllNotifyReminderList"></param>
        /// <param name="questionnaireLateCountLimit"></param>
        /// <param name="recurrenceSettingsInDetailDTO"></param>
        /// <param name="regularReminderTimeSettings"></param>
        /// <param name="personCollaborationDTO"></param>
        /// <param name="log"></param>
        private static void ProcessRegularIntervalWithRecurrencePattern(Utility utility, string apiurl, PersonQuestionnaireRegularScheduleDTO personQuestionnaireScheduleDTO, List<QuestionnaireReminderRulesDTO> questionnaireReminderRuleList, List<QuestionnaireReminderTypeDTO> questionnaireReminderTypes, List<PersonQuestionnaireRegularScheduleDTO> lst_AllPersonQuestionnaireSchedule, List<NotifyReminderDTO> lst_AllNotifyReminderList, int questionnaireLateCountLimit, RegularReminderRecurrenceDTO recurrenceSettingsInDetailDTO, List<RegularReminderTimeRuleDTO> regularReminderTimeSettings, PersonCollaborationDTO personCollaborationDTO, ILogger log)
        {
            try
            {
                List<PersonQuestionnaireRegularScheduleDTO> lst_personQuestionnaireRegularSchedule;
                List<NotifyReminderDTO> lst_regularNotifyReminderList;
                //Get collaboration

                var reminderCurrentDueDate = personQuestionnaireScheduleDTO.WindowDueDate.Date;
                log.LogInformation($" FutureReminderScheduleProcess : ProcessRegularReminder : Start calculating DueDates for future.. ");

                log.LogInformation($" PersonQuestionnaireID = {personQuestionnaireScheduleDTO.PersonQuestionnaireID}");
                bool ifAReminderFor2mrw = false; bool isEndDateArrived = false;
                personQuestionnaireScheduleDTO.OccurrenceCounter = personQuestionnaireScheduleDTO.OccurrenceCounter == 0 ? 1 : personQuestionnaireScheduleDTO.OccurrenceCounter;
                var occurrenceCounter =  personQuestionnaireScheduleDTO.OccurrenceCounter + 1;
                do
                {
                    lst_personQuestionnaireRegularSchedule = new List<PersonQuestionnaireRegularScheduleDTO>();
                    lst_regularNotifyReminderList = new List<NotifyReminderDTO>();
                    log.LogInformation($" OccurrenceCounter = {occurrenceCounter}");
                    log.LogInformation($" CurrentDueDate = {reminderCurrentDueDate}");
                    log.LogInformation($" ReminderRecurrancePattern = {recurrenceSettingsInDetailDTO.RecurrencePattern}");
                    log.LogInformation($" ReminderRecurranceType = {recurrenceSettingsInDetailDTO.RecurrenceRangeEndType}");
                    var lst_duedate = GetRegularAssessmentDueDateFromRecurrenceSettings(recurrenceSettingsInDetailDTO, reminderCurrentDueDate, occurrenceCounter, log);

                    log.LogInformation($" FutureReminderScheduleProcess : ProcessRegularReminder : Duedates Count = {lst_duedate?.Count}");

                    //if due dates are less than personquestionnaireEndate
                    isEndDateArrived = ValidationForDueDates(ref lst_duedate, recurrenceSettingsInDetailDTO, personCollaborationDTO, occurrenceCounter, log);

                    if (lst_duedate?.Count == 0) break;
                    //Attach Time to due dates.
                    var lst_dueDateWithTime = new List<DateTime>();
                    foreach (var recurtime in regularReminderTimeSettings)
                    {
                        var time = string.Format("{0}:{1} {2}", recurtime.Hour, recurtime.Minute, recurtime.AMorPM).ToTimeSpan();
                        lst_dueDateWithTime.AddRange(lst_duedate.Select(x => (x.Date + time).GetUTCDateTime(recurtime.TimeZoneName)).ToList());
                    }
                    log.LogInformation($" FutureReminderScheduleProcess : DueDates with time attached count = {lst_dueDateWithTime.Count}");

                    log.LogInformation($" FutureReminderScheduleProcess : ProcessRegularReminder : Creating PersonQuestionnaireSchedules");

                    lst_dueDateWithTime = lst_dueDateWithTime.Distinct().ToList();
                    foreach (var duedate in lst_dueDateWithTime)
                    {
                        //Create PersonQuestionnaireSchedule window for all available duedateTime
                        var windowOpenDay = duedate.AddDaysOrHours(-personQuestionnaireScheduleDTO.WindowOpenOffsetDays, personQuestionnaireScheduleDTO.OpenOffsetTypeID ?? PCISEnum.OffsetType.Day);
                        var windowDueDate = duedate;
                        var windowCloseDay = duedate.AddDaysOrHours(personQuestionnaireScheduleDTO.WindowCloseOffsetDays,
                            personQuestionnaireScheduleDTO.CloseOffsetTypeID);
                        PersonQuestionnaireRegularScheduleDTO personQuestionnaireSchedule = new PersonQuestionnaireRegularScheduleDTO
                        {
                            PersonQuestionnaireID = personQuestionnaireScheduleDTO.PersonQuestionnaireID,
                            QuestionnaireWindowID = personQuestionnaireScheduleDTO.QuestionnaireWindowID,
                            WindowDueDate = windowDueDate,
                            WindowOpenDate = windowOpenDay,
                            WindowCloseDate = windowCloseDay,
                            OccurrenceCounter = occurrenceCounter,
                            PersonQuestionnaireScheduleIndex = Guid.NewGuid()
                        };
                        lst_personQuestionnaireRegularSchedule.Add(personQuestionnaireSchedule);
                    }

                    log.LogInformation($" FutureReminderScheduleProcess : ProcessRegularReminder : Creating NotifyReminders");
                    foreach (var schedule in lst_personQuestionnaireRegularSchedule)
                    {
                        //Create the notifyReminderDTO for selected reminder rules only.
                        questionnaireReminderRuleList = questionnaireReminderRuleList.Where(x => x.IsSelected == true).ToList();
                        foreach (var item in questionnaireReminderRuleList)
                        {
                            var questionnaireReminderType = questionnaireReminderTypes.Where(x => x.QuestionnaireReminderTypeID == item.QuestionnaireReminderTypeID).FirstOrDefault();
                            var notifyReminderToAdd = CreateNotifyReminder(utility, apiurl, questionnaireReminderType.Name, schedule.PersonQuestionnaireScheduleIndex.Value, schedule.WindowOpenDate.Value, schedule.WindowDueDate, schedule.WindowCloseDate.Value, item, questionnaireLateCountLimit);
                            notifyReminderToAdd.ForEach(x => x.PersonQuestionnaireScheduleIndex = schedule.PersonQuestionnaireScheduleIndex.Value);
                            lst_regularNotifyReminderList.AddRange(notifyReminderToAdd);
                        }
                    }

                    lst_AllPersonQuestionnaireSchedule.AddRange(lst_personQuestionnaireRegularSchedule);
                    lst_AllNotifyReminderList.AddRange(lst_regularNotifyReminderList);

                    //The first reminder notifydate for this set of occurence should be atleast 1 day 12hr after today.
                    var lst_windowopendueOrder = lst_regularNotifyReminderList.OrderBy(x => x.NotifyDate).FirstOrDefault();
                    ifAReminderFor2mrw = lst_windowopendueOrder.NotifyDate.Date >= DateTime.UtcNow.AddDays(1).Date;

                    //The next duedate is the latest duedates from an occurence.
                    reminderCurrentDueDate = lst_duedate.OrderByDescending(x => x).ToList().FirstOrDefault().Date;

                    //increment the occurence counter
                    occurrenceCounter = occurrenceCounter + 1;
                } while (!ifAReminderFor2mrw && !isEndDateArrived);
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
        private static QuestionnaireRegularReminderSettingsDTO GetRegularReminderSettingsForAllQuestionnaire(Utility utility, string apiurl, List<int> questionnaireIds, ILogger log)
        {
            try
            {
                QuestionnaireRegularReminderSettingsDTO regularReminderSettings = new QuestionnaireRegularReminderSettingsDTO();
                var url = apiurl + PCISEnum.APIurl.RegularReminderSettingsByQuestionnaire;
                log.LogInformation($"FutureReminderScheduleProcess : GetRegularReminderSettingsForAllQuestionnaire url : {url}");
                var result = utility.RestApiCall(url, false, false, PCISEnum.APIMethodType.PostRequest, null, questionnaireIds.ToJSON());
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
        /// ValidationForDueDates.
        /// Valdiate for PersonCollaboration EndDate/No of Occurences/rangeEndate
        /// </summary>
        /// <param name="lst_dueDate"></param>
        /// <param name="regularReminderRecurrenceDTO"></param>
        /// <param name="personQuestionnaireDTO"></param>
        /// <param name="occurrenceCounter"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        private static bool ValidationForDueDates(ref List<DateTime> lst_dueDate, RegularReminderRecurrenceDTO regularReminderRecurrenceDTO, PersonCollaborationDTO personCollaborationDTO, int occurrenceCounter, ILogger log)
        {
            try
            {
                log.LogInformation($"FutureReminderScheduleProcess : ValidationForDueDates Start");
                bool isEndDateArrived = false;
                if (lst_dueDate?.Count > 0)
                {
                    log.LogInformation($"FutureReminderScheduleProcess : The final DueDate = {lst_dueDate.OrderByDescending(x => x).FirstOrDefault().Date}");
                    //Valdiate for PersonCollaboration EndDate.
                    if (personCollaborationDTO.EndDate.HasValue && lst_dueDate?.Count > 0)
                    {
                        var dueDatesAfterpersonQuestionnaireEnd = lst_dueDate.Where(x => x.Date > personCollaborationDTO.EndDate.Value.Date).Count();
                        if (dueDatesAfterpersonQuestionnaireEnd > 0)
                        {
                            log.LogInformation($"FutureReminderScheduleProcess : Schedule Ended by PersonCollaboration Endate = {personCollaborationDTO.EndDate.Value}");

                            isEndDateArrived = true;
                            lst_dueDate = lst_dueDate.Where(x => x.Date <= personCollaborationDTO.EndDate.Value.Date).ToList();
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
        private static List<DateTime> GetRegularAssessmentDueDateFromRecurrenceSettings(RegularReminderRecurrenceDTO recurrenceSettingsInDetailDTO, DateTime currentDueDate, int occurrenceCounter, ILogger log)
        {
            try
            {
                List<DateTime> lst_dueDate = new List<DateTime>();                
                log.LogInformation($"FutureReminderScheduleProcess : GetRegularAssessmentDueDate : Reccurrence Pattern = {recurrenceSettingsInDetailDTO.RecurrencePattern}");

                if (!ValidateForPatternRecurrenceSettings(recurrenceSettingsInDetailDTO))
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
                    default: break;
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
            var isValid = true;
            if (recurrenceSettingsInDetailDTO.RecurrencePattern.ToLower() != PCISEnum.RecurrencePattern.DailyWeekdays)
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

        /// <summary>
        /// Calculate duedate for Yearly RecurrenceType
        /// </summary>
        /// <param name="recurrenceSettingsInDetailDTO"></param>
        /// <param name="currentDueDate"></param>
        /// <returns></returns>
        private static List<DateTime> ProcessYearlyReminders(RegularReminderRecurrenceDTO recurrenceSettingsInDetailDTO, DateTime currentDueDate, int occurrenceCounter)
        {
            try
            {
                var monthNamesSettings = JsonConvert.DeserializeObject<List<LookupDTO>>(recurrenceSettingsInDetailDTO.RecurrenceMonths);
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
        /// 
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
        /// 
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
    }
}
