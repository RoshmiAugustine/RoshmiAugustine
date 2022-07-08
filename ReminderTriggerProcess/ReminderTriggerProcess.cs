using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ReminderTriggerProcess.Common;
using ReminderTriggerProcess.DTO;
using ReminderTriggerProcess.Enums;

namespace ReminderTriggerProcess
{
    /// <summary>
    /// ReminderTriggerProcess.
    /// Updated as part of 3225: To fetch reminders between last runtime and current runtime a datetime.
    /// </summary>
    public static class ReminderTriggerProcess
    {
        [FunctionName("ReminderTriggerProcess")]
        public static void Run([TimerTrigger("%timer-frequency%")]TimerInfo myTimer, ILogger log)
            {
            log.LogInformation($"C# ReminderTriggerProcess function executed at: {DateTime.Now}");
            try
            {
                Utility Utility = new Utility();
                var apiurl = Environment.GetEnvironmentVariable(PCISEnum.EnvironmentVariables.ApiUrlFromKeyVault, EnvironmentVariableTarget.Process);
                var processLog = GetBackgroundProcessLog(Utility, apiurl, PCISEnum.BackgroundProcess.TriggerReminderNotification);
                var LastRunTime = processLog.LastProcessedDate;
                log.LogInformation($"C# ReminderTriggerProcess: ProcessTriggerReminderNotification Start");
                log.LogInformation($"C# LastRunTime : {LastRunTime}");
                DateTime currentRunTime;
                ProcessTriggerReminderNotification(Utility, apiurl, LastRunTime, log, out currentRunTime);
                log.LogInformation($"C# ReminderTriggerProcess: ProcessTriggerReminderNotification End");

                if (processLog == null)
                {
                    processLog = new BackgroundProcessLogDTO()
                    {
                        ProcessName = PCISEnum.BackgroundProcess.TriggerReminderNotification,
                        LastProcessedDate = DateTime.UtcNow.Date.AddDays(-1)
                    };
                    AddBackgroundProcessLog(Utility, apiurl, processLog);
                }
                else
                {
                    processLog.LastProcessedDate = currentRunTime;
                    var result = UpdateBackgroundProcessLog(Utility, apiurl, processLog);
                }
                log.LogInformation($"C# ReminderTriggerProcess: End");

            }
            catch (Exception ex)
            {
                log.LogError(ex, $@"ReminderTriggerProcess : Exception occurred.Exception Message : {ex.Message}.Location : {ex.StackTrace}.Details : {ex.InnerException}");
            }
        }

        private static void ProcessTriggerReminderNotification(Utility utility, string apiUrl, DateTime lastRunTime, ILogger log, out DateTime currentRunTime)
        {
            try
            {
                var notificationType = GetNotificationType(utility, apiUrl, PCISEnum.NotificationType.Reminder);
                var notificationResolutionStatusID = GetNotificationStatus(utility, apiUrl, PCISEnum.NotificationStatus.Unresolved).NotificationResolutionStatusID;
                currentRunTime = DateTime.UtcNow;//Get currentTime of fetching details from DB.
                log.LogInformation($"C# CurrentRunTime : {currentRunTime}");
                //TO-DO:Get ReminderIds and then process by batches.
                var count = GetNotifyReminderScheduledCountForToday(utility, apiUrl,lastRunTime,currentRunTime);
                log.LogInformation($"C# ReminderTriggerProcess: GetNotifyReminderScheduledCountForToday {count}");

                if (count > 0)
                {
                    log.LogInformation($"C# ReminderTriggerProcess: GetNotifyReminderScheduledForToday start");

                    var notifyRemindersScheduled = GetNotifyReminderScheduledForToday(utility, apiUrl, lastRunTime, currentRunTime);
                    log.LogInformation($"C# ReminderTriggerProcess: GetNotifyReminderScheduledForToday End");
                    List<int> notifyReminderIds = new List<int>();
                    List<NotificationLogDTO> notificationLogList = new List<NotificationLogDTO>();
                    List<int> questionaireIdList = new List<int>();
                    foreach (var item in notifyRemindersScheduled)
                    {
                        notifyReminderIds.Add(item.NotifyReminderID);
                        var notificationLog = new NotificationLogDTO
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
                            Details = item.QuestionnaireID + " - " + item.DueDate.ToString("MM/dd/yyyy") + "-" + item.InstrumentAbbrev + " - " + item.QuestionnaireName + " - " + item.ReminderScheduleName + " - " + item.AssessmentReasonName,
                            HelperName = item.LeadHelperName
                        };
                        notificationLogList.Add(notificationLog);
                        questionaireIdList.Add(notificationLog.QuestionnaireID);
                    }
                    log.LogInformation($"C# ReminderTriggerProcess: AddBulkNotificationLog Start");

                    AddBulkNotificationLog(utility, apiUrl, notificationLogList);
                    log.LogInformation($"C# ReminderTriggerProcess: AddBulkNotificationLog ENd");
                    //Fetch the schedules having questionnaires with IsEmailRemindersHelpers = true
                    var notifyRemindersScheduledHelpers = notifyRemindersScheduled.Where(x => x.IsEmailRemindersHelpers == true).ToList();
                    log.LogInformation($"C# ReminderTriggerProcess: InsertDataToEmailDetail Start");
                    InsertDataToEmailDetail(utility, apiUrl, notifyRemindersScheduledHelpers, log);
                    log.LogInformation($"C# ReminderTriggerProcess: InsertDataToEmailDetail End");

                    var notifyReminders = GetNotifyReminderSchedule(utility, apiUrl, notifyReminderIds);
                    notifyReminders.ForEach(x => x.IsLogAdded = true);
                    log.LogInformation($"C# ReminderTriggerProcess: UpdateBulkNotifyReminder Start");

                    UpdateBulkNotifyReminder(utility, apiUrl, notifyReminders);
                    log.LogInformation($"C# ReminderTriggerProcess: UpdateBulkNotifyReminder End");

                    //Added as part of PCIS-3225
                    log.LogInformation($" ReminderTriggerProcess :  Mark Reminders For InviteToCompleteMail Pending Status - Start");
                    //Mark all Reminders InvitToCompleteMailStatus = pending For all questionnaires having IsEmailInviteToCompleteReminders = 1.
                    var notifyInviteRemindersIds = notifyRemindersScheduled.Where(x => x.IsEmailInviteToCompleteReminders == true).Select(x => x.NotifyReminderID).ToList();
                    if (notifyInviteRemindersIds?.Count > 0)
                    {
                        var notifyInviteReminders = GetNotifyReminderSchedule(utility, apiUrl, notifyInviteRemindersIds);
                        notifyReminders.ForEach(x => x.InviteToCompleteMailStatus = PCISEnum.EmailStatus.Pending);
                        UpdateBulkNotifyReminder(utility, apiUrl, notifyReminders);
                    }
                    log.LogInformation($" ReminderTriggerProcess :  Mark Reminders InviteToCompleteMail Pending Status- End");
                }
                else
                {
                    log.LogInformation($"C# ReminderTriggerProcess : Nothing to process");
                }

            }
            catch (Exception)
            {
                throw;
            }

        }

        private static List<QuestionnaireDTO> CheckQuestionaireAlertNotificationEnabled(Utility utility, string apiUrl, List<int> questionnaireIds)
        {
            try
            {
                List<QuestionnaireDTO> response = new List<QuestionnaireDTO>();
                var configAPiurl = apiUrl + PCISEnum.APIurl.GetQuestionaireDetails;
                var result = utility.RestApiCall(configAPiurl, false, false, PCISEnum.APIMethodType.PostRequest, null, questionnaireIds.ToJSON());
                if (!string.IsNullOrEmpty(result))
                {
                    var responseDetails = JsonConvert.DeserializeObject<QuestionnaireResponseDTO>(result);
                    response = responseDetails.result.QuestionnaireList;
                }
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static CRUDResponse UpdateBulkNotifyReminder(Utility Utility, string apiurl, List<NotifyReminderDTO> notifyReminders)
        {
            try
            {
                CRUDResponse Response = new CRUDResponse();
                var url = apiurl + PCISEnum.APIurl.NotifyReminder;
                var result = Utility.RestApiCall(url, false, false, PCISEnum.APIMethodType.PutRequest, null, notifyReminders.ToJSON());
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

        private static void InsertDataToEmailDetail(Utility utility, string apiUrl, List<ReminderNotificationsListDTO> notifyRemindersScheduled, ILogger log)
        {
            try
            {
                List<EmailDetailsDTO> list_emailDetails = new List<EmailDetailsDTO>();
                var lstpersonID = notifyRemindersScheduled.Select(x => x.PersonID).ToList();
                log.LogInformation($"C# ReminderTriggerProcess: GetPersonsAndHelpersByPersonIDList Start");
                var allPersonshelpers = GetPersonsAndHelpersByPersonIDList(utility, apiUrl, lstpersonID);
                log.LogInformation($"C# ReminderTriggerProcess: GetPersonsAndHelpersByPersonIDList end");

                var lstPersonQuestionSchedule = notifyRemindersScheduled.Select(x => x.PersonQuestionnaireScheduleID).ToList();
                log.LogInformation($"C# ReminderTriggerProcess: getDetailsByPersonQuestionScheduleList Start");
                var allpersonQuestions = getDetailsByPersonQuestionScheduleList(utility, apiUrl, lstPersonQuestionSchedule);
                log.LogInformation($"C# ReminderTriggerProcess: getDetailsByPersonQuestionScheduleList end");

                var PCISBaseURL = GetConfigurationValue(utility, apiUrl, PCISEnum.ConfigurationParameters.PCIS_BaseURL);
                var PeopleQuestionnaireURL = GetConfigurationValue(utility, apiUrl, PCISEnum.ConfigurationParameters.PeopleQuestionnaireURL);
                var urllink = string.Format("{0}{1}", PCISBaseURL, PeopleQuestionnaireURL);

                foreach (var item in notifyRemindersScheduled)
                {
                    var personhelpers = allPersonshelpers.Where(x => x.PersonID == item.PersonID).ToList();
                    var questionnaireID = allpersonQuestions.Where(x => x.PersonQuestionnaireScheduleID == item.PersonQuestionnaireScheduleID).Select(x => x.QuestionnaireID).FirstOrDefault();
                    var windowDetails = allpersonQuestions.Where(x => x.PersonQuestionnaireScheduleID == item.PersonQuestionnaireScheduleID).FirstOrDefault();
                    foreach (var personHelper in personhelpers)
                    {
                        EmailDetailsDTO emailDetail = new EmailDetailsDTO
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
                            URL = urllink.Replace(PCISEnum.EmailDetail.AssessmentID, "0").Replace(PCISEnum.EmailDetail.PersonIndex, personHelper.PersonIndex.ToString()).Replace(PCISEnum.EmailDetail.NotificationType, PCISEnum.NotificationType.Reminder).Replace(PCISEnum.EmailDetail.QuestionnaireId, questionnaireID.ToString()).ToString(),
                            FKeyValue = item.NotifyReminderID
                        };
                        list_emailDetails.Add(emailDetail);
                    }
                }
                log.LogInformation($" ReminderTriggerProcess :AddEmailDetails start");
                AddEmailDetails(utility, apiUrl, list_emailDetails);
                log.LogInformation($" ReminderTriggerProcess : AddEmailDetails end");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static List<PersonQuestionnaireScheduleEmailDTO> getDetailsByPersonQuestionScheduleList(Utility Utility, string apiurl, List<long> personScheduleIds)
        {
            try
            {
                List<PersonQuestionnaireScheduleEmailDTO> personHelper = new List<PersonQuestionnaireScheduleEmailDTO>();
                var url = apiurl + PCISEnum.APIurl.PersonQuestionnaireScheduleListByIds;
                var result = Utility.RestApiCall(url, false, false, PCISEnum.APIMethodType.PostRequest, null, personScheduleIds.ToJSON());
                if (!string.IsNullOrEmpty(result))
                {
                    var personHelperResponses = JsonConvert.DeserializeObject<ReminderNotificationScheduleResponseDTO>(result);
                    personHelper = personHelperResponses?.result?.personQuestionnaireScheduleEmails;
                }
                return personHelper;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static List<PeopleHelperEmailDTO> GetPersonsAndHelpersByPersonIDList(Utility Utility, string apiurl, List<long> personId)
        {
            try
            {
                List<PeopleHelperEmailDTO> personHelper = new List<PeopleHelperEmailDTO>();
                var url = apiurl + PCISEnum.APIurl.PersonHelperByListofPersonId;
                var result = Utility.RestApiCall(url, false, false, PCISEnum.APIMethodType.PostRequest, null, personId.ToJSON());
                if (!string.IsNullOrEmpty(result))
                {
                    var personHelperResponses = JsonConvert.DeserializeObject<PersonHelperEmailDetailDTO>(result);
                    personHelper = personHelperResponses?.result?.PersonHelper;
                }
                return personHelper;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static List<NotifyReminderDTO> GetNotifyReminderSchedule(Utility Utility, string apiurl, List<int> notifyReminderIds)
        {
            try
            {
                List<NotifyReminderDTO> Response = new List<NotifyReminderDTO>();
                var url = apiurl + PCISEnum.APIurl.NotifyReminderByIds;
                var result = Utility.RestApiCall(url, false, false, PCISEnum.APIMethodType.PostRequest, null, notifyReminderIds.ToJSON());
                if (!string.IsNullOrEmpty(result))
                {
                    var TotalResponse = JsonConvert.DeserializeObject<NotifyReminderDetailsDTO>(result);
                    Response = TotalResponse.result.NotifyReminders;
                }
                return Response;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// GetNotifyReminderScheduledCountForToday.
        /// Updated get API to POST to fetch reminders netween 2 dates.
        /// </summary>
        /// <param name="Utility"></param>
        /// <param name="apiurl"></param>
        /// <param name="lastRunTime"></param>
        /// <param name="currentRunTime"></param>
        /// <returns></returns>
        private static int GetNotifyReminderScheduledCountForToday(Utility Utility, string apiurl, DateTime lastRunTime, DateTime currentRunTime)
        {
            try
            {
                int response = 0;
                RemiderNotificationTriggerTimeDTO triggerTime = new RemiderNotificationTriggerTimeDTO();
                triggerTime.LastRunDatetime = lastRunTime;
                triggerTime.CurrentRunDatetime = currentRunTime;
                var configAPiurl = apiurl + PCISEnum.APIurl.NotifyReminderScheduleCount;
                var result = Utility.RestApiCall(configAPiurl, false, false, PCISEnum.APIMethodType.PostRequest, null, triggerTime.ToJSON());
                if (!string.IsNullOrEmpty(result))  
                {
                    var responseDetails = JsonConvert.DeserializeObject<ReminderNotificationScheduleResponseDTO>(result);
                    response = responseDetails.result.Count;
                }
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// GetNotifyReminderScheduledForToday.
        /// Updated get API to POST to fetch reminders netween 2 dates.
        /// </summary>
        /// <param name="Utility"></param>
        /// <param name="apiurl"></param>
        /// <param name="lastRunTime"></param>
        /// <param name="currentRunTime"></param>
        /// <returns></returns>
        private static List<ReminderNotificationsListDTO> GetNotifyReminderScheduledForToday(Utility Utility, string apiurl, DateTime lastRunTime, DateTime currentRunTime)
        {
            try
            {
                List<ReminderNotificationsListDTO> response = new List<ReminderNotificationsListDTO>();
                RemiderNotificationTriggerTimeDTO triggerTime = new RemiderNotificationTriggerTimeDTO();
                triggerTime.LastRunDatetime = lastRunTime;
                triggerTime.CurrentRunDatetime = currentRunTime;
                var configAPiurl = apiurl + PCISEnum.APIurl.NotifyReminderSchedule;
                var result = Utility.RestApiCall(configAPiurl, false, false, PCISEnum.APIMethodType.PostRequest, null, triggerTime.ToJSON());
                if (!string.IsNullOrEmpty(result))
                {
                    var responseDetails = JsonConvert.DeserializeObject<ReminderNotificationScheduleResponseDTO>(result);
                    response = responseDetails.result.ReminderNotification;
                }
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static NotificationResolutionStatusDTO GetNotificationStatus(Utility Utility, string apiurl, string status)
        {
            try
            {
                NotificationResolutionStatusDTO response = new NotificationResolutionStatusDTO();
                var configAPiurl = apiurl + PCISEnum.APIurl.NotificationStatus.Replace(PCISEnum.APIReplacableValues.Status, Convert.ToString(status));
                var result = Utility.RestApiCall(configAPiurl, false);
                if (!string.IsNullOrEmpty(result))
                {
                    var responseDetails = JsonConvert.DeserializeObject<NotificationResolutionStatusDetailsDTO>(result);
                    response = responseDetails.result.NotificationStatus;
                }
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static NotificationTypeDTO GetNotificationType(Utility Utility, string apiurl, string type)
        {
            try
            {
                NotificationTypeDTO response = new NotificationTypeDTO();
                var configAPiurl = apiurl + PCISEnum.APIurl.NotificationTypeByName.Replace(PCISEnum.APIReplacableValues.notificationType, Convert.ToString(type));
                var result = Utility.RestApiCall(configAPiurl, false);
                if (!string.IsNullOrEmpty(result))
                {
                    var responseDetails = JsonConvert.DeserializeObject<NotificationTypeListResponseDTO>(result);
                    response = responseDetails.result.NotificationType;
                }
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static CRUDResponse AddBulkNotificationLog(Utility Utility, string apiurl, List<NotificationLogDTO> notificationLog)
        {
            try
            {
                CRUDResponse Response = new CRUDResponse();
                var url = apiurl + PCISEnum.APIurl.NotificationLog;
                var result = Utility.RestApiCall(url, false, false, PCISEnum.APIMethodType.PostRequest, null, notificationLog.ToJSON());
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

        private static CRUDResponse AddEmailDetails(Utility Utility, string apiurl, List<EmailDetailsDTO> emailDetails)
        {
            try
            {
                CRUDResponse Response = new CRUDResponse();
                if (emailDetails?.Count > 0)
                {
                    var url = apiurl + PCISEnum.APIurl.EmailDetails;
                    var result = Utility.RestApiCall(url, false, false, PCISEnum.APIMethodType.PostRequest, null, emailDetails.ToJSON());
                    if (!string.IsNullOrEmpty(result))
                    {
                        var TotalResponse = JsonConvert.DeserializeObject<CRUDResponseDTO>(result);
                        Response = TotalResponse.result;
                    }
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

        private static string GetConfigurationValue(Utility Utility, string apiurl, string configurationKey)
        {
            try
            {
                string configuration = string.Empty;
                var configAPiurl = apiurl + PCISEnum.APIurl.ConfigurationWithKey.Replace(PCISEnum.APIReplacableValues.Key, configurationKey).Replace(PCISEnum.APIReplacableValues.AgencyID, "0");
                var result = Utility.RestApiCall(configAPiurl, false);
                if (!string.IsNullOrEmpty(result))
                {
                    var config = JsonConvert.DeserializeObject<ConfigurationResponseDTO>(result);
                    configuration = config.result.ConfigurationValue;
                }
                return configuration;
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

        private static BackgroundProcessLogDTO GetBackgroundProcessLog(Utility Utility, string apiurl, string name)
        {
            try
            {
                BackgroundProcessLogDTO response = new BackgroundProcessLogDTO();
                var configAPiurl = apiurl + PCISEnum.APIurl.GetBackgroundProcess.Replace(PCISEnum.APIReplacableValues.Name, Convert.ToString(name));
                var result = Utility.RestApiCall(configAPiurl, false);
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
    }
}
