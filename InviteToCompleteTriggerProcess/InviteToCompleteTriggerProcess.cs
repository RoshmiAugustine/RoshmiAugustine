using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using InviteToCompleteTriggerProcess.Common;
using InviteToCompleteTriggerProcess.DTO;
using InviteToCompleteTriggerProcess.Enums;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace InviteToCompleteTriggerProcess
{
    /// <summary>
    /// Created As part of PCIS-3225 
    /// Purpose: To Batch Upload assessmnets and trigger the inviteToComplete mails/SMS.
    /// Logic:1) Receive all reminders in NotifyReminder Table with InviteTocomplete Status column = pending.
    ///       2) Group reminders by Receiver types 'person/support/helper/lead' and get the distinct personquestionnaireIDs.
    ///       3) Fetch the receiver details for triggering SMS/Email by personquestionnaireIDs
    ///       4) For each Reminder create the Assessments and AssessmentEMailLinkDetails entry for each receivers.    ///       
    ///       5) Also create an object of ReminderInviteToComplete(used for sending email/sm by Email Process- AF) along with it.
    ///       5) Create AssessmentEmail URL for attaching in mail/sms and update in ReminderInviteToComplete
    ///       6) At Last insert ReminderInviteToComplete to DB.
    ///       7) Later in EmailProcess - AF these entries will be picked up and mail/sms will be triggered accordingly
    /// </summary>
    public static class InviteToCompleteTriggerProcess
    {
        [FunctionName("InviteToCompleteTriggerProcess")]
        public static void Run([TimerTrigger("%timer-frequency%")] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"InviteToCompleteTriggerProcess : C# Timer trigger function executed at: {DateTime.Now}");
            try
            {
                Utility utility = new Utility();
                var apiurl = Environment.GetEnvironmentVariable(PCISEnum.EnvironmentVariables.ApiUrlFromKeyVault, EnvironmentVariableTarget.Process);

                var inviteToCompleteReminders = GetReminderDetailsForInviteToComplete(utility, apiurl, log);
                log.LogInformation($"InviteToCompleteTriggerProcess : {DateTime.Now} : ##### Fetch all pending inviteToCompleteReminders. Total Count : {inviteToCompleteReminders?.Count } #######");
                inviteToCompleteReminders?.ForEach(x =>
                {
                    x.InviteToCompleteReceiversList = JsonConvert.DeserializeObject<List<InviteToCompleteReceiversDTO>>(x.InviteToCompleteReceivers);
                });

                //process personquestionnaire as batches of all fetched personQuestionnaires
                int loopCount = inviteToCompleteReminders.Count;
                for (int i = 0; i < loopCount; i = i + PCISEnum.Constants.UploadBlockCount)
                {
                    var remindersTobeProcessed = inviteToCompleteReminders.Skip(i).Take(PCISEnum.Constants.UploadBlockCount).ToList();
                    if (remindersTobeProcessed.Count > 0)
                    {
                        var recordCount = (i + PCISEnum.Constants.UploadBlockCount) > loopCount ? loopCount : i + PCISEnum.Constants.UploadBlockCount;
                         ProcessQuestionnairesToFetchInviteDetails(remindersTobeProcessed, utility, apiurl, log);
                    }
                }
            }
            catch (Exception ex)
            {
                log.LogError(ex, $@"InviteToCompleteTriggerProcess : Exception occurred.Exception Message : {ex.Message}.Location : {ex.StackTrace}.Details : {ex.InnerException}");
            }
            log.LogInformation($"InviteToCompleteTriggerProcess : C# Timer trigger function execute ended at: {DateTime.Now}");
            
        }
        /// <summary>
        /// ProcessQuestionnairesToFetchInviteDetails.
        /// A PersonQuestionnaire may have mul
        /// </summary>
        /// <param name="inviteToCompleteReminders"></param>
        /// <param name="utility"></param>
        /// <param name="apiurl"></param>
        /// <param name="log"></param>
        private static void ProcessQuestionnairesToFetchInviteDetails(List<RemindersToTriggerInviteToCompleteDTO> inviteToCompleteReminders, Utility utility, string apiurl, ILogger log)
        {
            try
            {
                List<InviteMailReceiversInDetailDTO> lst_AllinviteMailsToDetails = new List<InviteMailReceiversInDetailDTO>();
                List<AssessmentEmailLinkDetailsDTO> lst_AllAssessmentEmailLinkDetail = new List<AssessmentEmailLinkDetailsDTO>();
                List<ReminderInviteToCompleteDTO> lst_AllReminderInviteDetails = new List<ReminderInviteToCompleteDTO>();
                List<AssessmentDTO> lst_AllAssessments = new List<AssessmentDTO>();

                log.LogInformation($"InviteToCompleteTriggerProcess : {DateTime.Now} : Fetching details for different InviteTypes-Start");
                //Filter all reminders having inviteToComplete Receiver as Person
                var lst_InviteToPersons = inviteToCompleteReminders.Where(x => x.InviteToCompleteReceiversList.Where(x => x.Name.ToLower() == PCISEnum.InviteToCompleteReceivers.Person.ToLower()).ToList()?.Count > 0).ToList();

                //Filter all reminders having inviteToComplete Receiver as Support
                var lst_InviteToSupports = inviteToCompleteReminders.Where(x => x.InviteToCompleteReceiversList.Where(x => x.Name.ToLower() == PCISEnum.InviteToCompleteReceivers.Supports.ToLower()).ToList()?.Count > 0).ToList();

                //Filter all reminders having inviteToComplete Receiver as Helper
                var lst_InviteToHelpers = inviteToCompleteReminders.Where(x => x.InviteToCompleteReceiversList.Where(x => x.Name.ToLower() == PCISEnum.InviteToCompleteReceivers.Helpers.ToLower()).ToList()?.Count > 0).ToList();

                //Filter all reminders having inviteToComplete Receiver as LeadHelper
                var lst_InviteToLeadHelpers = inviteToCompleteReminders.Where(x => x.InviteToCompleteReceiversList.Where(x => x.Name.ToLower() == PCISEnum.InviteToCompleteReceivers.LeadHelper.ToLower()).ToList()?.Count > 0).ToList();

                //Get the details for Invite Type -  Person 
                if (lst_InviteToPersons.Count > 0)
                {
                    var receiverType = lst_InviteToPersons[0].InviteToCompleteReceiversList.Where(x => x.Name.ToLower() == PCISEnum.InviteToCompleteReceivers.Person.ToLower()).FirstOrDefault();
                    log.LogInformation($"InviteToCompleteTriggerProcess : {DateTime.Now} : InviteTypes-Person");
                    var lst_personQuestionnaireIDs = lst_InviteToPersons.Select(x => x.PersonQuestionnaireID).Distinct().ToList();
                    var result = GetReceiversDetailsForReminderInvite(utility, apiurl, lst_personQuestionnaireIDs,PCISEnum.InviteToCompleteReceivers.Person, log);
                    result?.ForEach(x => x.InviteToCompleteReceiverID = receiverType.InviteToCompleteReceiverID);
                    lst_AllinviteMailsToDetails.AddRange(result);
                }

                //Get the details for Invite Type -  Supports 
                if (lst_InviteToSupports.Count > 0)
                {
                    var receiverType = lst_InviteToSupports[0].InviteToCompleteReceiversList.Where(x => x.Name.ToLower() == PCISEnum.InviteToCompleteReceivers.Supports.ToLower()).FirstOrDefault();
                    log.LogInformation($"InviteToCompleteTriggerProcess : {DateTime.Now} : InviteTypes-Support");
                    var lst_personQuestionnaireIDs = lst_InviteToSupports.Select(x => x.PersonQuestionnaireID).Distinct().ToList();
                    var result = GetReceiversDetailsForReminderInvite(utility, apiurl, lst_personQuestionnaireIDs, PCISEnum.InviteToCompleteReceivers.Supports, log);
                    result?.ForEach(x => x.InviteToCompleteReceiverID = receiverType.InviteToCompleteReceiverID);
                    lst_AllinviteMailsToDetails.AddRange(result);
                }
                var helperlst_personQuestionnaireIDs = new List<long>();
                //Get the details for Invite Type - Helpers 
                if (lst_InviteToHelpers.Count > 0)
                {
                    var receiverType = lst_InviteToHelpers[0].InviteToCompleteReceiversList.Where(x => x.Name.ToLower() == PCISEnum.InviteToCompleteReceivers.Helpers.ToLower()).FirstOrDefault();
                    log.LogInformation($"InviteToCompleteTriggerProcess : {DateTime.Now} : InviteTypes-Helper");
                    var lst_personQuestionnaireIDs = helperlst_personQuestionnaireIDs = lst_InviteToHelpers.Select(x => x.PersonQuestionnaireID).Distinct().ToList();
                    var result = GetReceiversDetailsForReminderInvite(utility, apiurl, lst_personQuestionnaireIDs, PCISEnum.InviteToCompleteReceivers.Helpers, log);
                    result?.ForEach(x => x.InviteToCompleteReceiverID = receiverType.InviteToCompleteReceiverID);
                    lst_AllinviteMailsToDetails.AddRange(result);
                }

                //Get the details for Invite Type -  Lead Helpers
                if (lst_InviteToLeadHelpers.Count > 0)
                {
                    var receiverType = lst_InviteToLeadHelpers[0].InviteToCompleteReceiversList.Where(x => x.Name.ToLower() == PCISEnum.InviteToCompleteReceivers.LeadHelper.ToLower()).FirstOrDefault();
                    log.LogInformation($"InviteToCompleteTriggerProcess : {DateTime.Now} : InviteTypes-LeadHelper");
                    var lst_personQuestionnaireIDs = lst_InviteToLeadHelpers.Select(x => x.PersonQuestionnaireID).Distinct().ToList();
                    lst_personQuestionnaireIDs = lst_personQuestionnaireIDs?.Where(x => !helperlst_personQuestionnaireIDs.Contains(x)).ToList();
                    if (lst_personQuestionnaireIDs.Count > 0)
                    {
                        var result = GetReceiversDetailsForReminderInvite(utility, apiurl, lst_personQuestionnaireIDs, PCISEnum.InviteToCompleteReceivers.LeadHelper, log);
                        result?.ForEach(x => x.InviteToCompleteReceiverID = receiverType.InviteToCompleteReceiverID);
                        lst_AllinviteMailsToDetails.AddRange(result);
                    }
                }

                log.LogInformation($"InviteToCompleteTriggerProcess : {DateTime.Now} : Fetching details for different InviteTypes-END ");

                log.LogInformation($"InviteToCompleteTriggerProcess : {DateTime.Now} : Prepare Assessments and EmailLinkDetails to be inserted for reminders");
                if (lst_AllinviteMailsToDetails.Count > 0)
                {
                    foreach (var reminder in inviteToCompleteReminders)
                    {
                        //Get the voiceTypes details to all the mail/sms should be sent
                        var mailToList = lst_AllinviteMailsToDetails.Where(x => x.PersonQuestionnaireID == reminder.PersonQuestionnaireID).ToList();
                        foreach (var mailTo in mailToList)
                        {
                            var displayname = string.Format("{0} {1} {2}", mailTo.FirstName, string.IsNullOrEmpty(mailTo.MiddleName) ? "" : mailTo.MiddleName, mailTo.LastName).Replace("  ", " ");

                            //Prepare AssessmentDTO
                            var assessmentGuid = Guid.NewGuid();
                            var assessmentDTO = new AssessmentDTO
                            {
                                PersonQuestionnaireID = reminder.PersonQuestionnaireID,
                                VoiceTypeID = mailTo.VoiceTypeID,
                                VoiceTypeFKID = mailTo.VoiceTypeFKID,
                                DateTaken = reminder.NotifyReminderDate,
                                ReasoningText = "",
                                AssessmentReasonID = reminder.AssessmentReasonID,
                                AssessmentStatusID = mailTo.AssessmentStatusID,
                                PersonQuestionnaireScheduleID = reminder.PersonQuestionnaireScheduleID,
                                IsUpdate = true,
                                Approved = null,
                                CloseDate = null,
                                IsRemoved = false,
                                UpdateDate = DateTime.UtcNow,
                                UpdateUserID = 1,
                                EventDate = null,
                                EventNotes = null,
                                EventNoteUpdatedBy = null,
                                AssessmentGuid = assessmentGuid,
                                NotifyReminderID = reminder.NotifyReminderId
                            };
                            lst_AllAssessments.Add(assessmentDTO);

                            //Prepare AssessmentEmailLinkDTO
                            var assessmentEmailLinkGuid = Guid.NewGuid();
                            var assessmentEmailLinkDTO = new AssessmentEmailLinkDetailsDTO()
                            {
                                AssessmentEmailLinkGuid = assessmentEmailLinkGuid,
                                AssessmentGUID = assessmentGuid,
                                PersonIndex = mailTo.PersonIndex,
                                PersonSupportID = mailTo.InviteReceiverType == PCISEnum.InviteToCompleteReceivers.Supports ? (int?)mailTo.VoiceTypeFKID : null,
                                QuestionnaireID = reminder.QuestionnaireID,
                                HelperID = (mailTo.InviteReceiverType == PCISEnum.InviteToCompleteReceivers.Helpers || mailTo.InviteReceiverType == PCISEnum.InviteToCompleteReceivers.LeadHelper) ? mailTo.HelperId : null,
                                UpdateDate = DateTime.UtcNow,
                                PersonORSupportEmail = mailTo.EmailID,
                                UpdateUserID = 1, 
                                VoiceTypeID = mailTo.VoiceTypeID,
                                PhoneNumber = string.Format("{0}{1}", mailTo.PhoneCode, mailTo.Phone),
                            };
                            lst_AllAssessmentEmailLinkDetail.Add(assessmentEmailLinkDTO);

                            //Prepare ReminderInviteToCompleteDetailsDTO
                            if (mailTo.EmailPermission)
                            {
                                var reminderInvite = new ReminderInviteToCompleteDTO()
                                {
                                    QuestionnaireID = reminder.QuestionnaireID,
                                    NotifyReminderID = reminder.NotifyReminderId,
                                    AssesmentEmailLinkGUID = assessmentEmailLinkGuid,
                                    CreatedDate = DateTime.UtcNow,
                                    UpdateDate = DateTime.UtcNow,
                                    InviteToCompleteReceiverID = mailTo.InviteToCompleteReceiverID,
                                    Status = PCISEnum.EmailStatus.Pending,
                                    TypeOfInviteSend = PCISEnum.Invitation.Email,
                                    Email = mailTo.EmailID,
                                    PhoneNumber = null,
                                    Attributes = string.Format("ReceiversFirstName={0}|ReceiversDisplayName={1}|ReasoningText={2}", mailTo.FirstName, displayname, assessmentDTO.ReasoningText),
                                    HelperID = (mailTo.InviteReceiverType == PCISEnum.InviteToCompleteReceivers.Helpers || mailTo.InviteReceiverType == PCISEnum.InviteToCompleteReceivers.LeadHelper) ? mailTo.HelperId : null,
                                    PersonID = mailTo.InviteReceiverType == PCISEnum.InviteToCompleteReceivers.Person ? (long?)mailTo.PersonId : null,
                                    PersonSupportID = mailTo.InviteReceiverType == PCISEnum.InviteToCompleteReceivers.Supports ? (int?)mailTo.VoiceTypeFKID : null
                                };
                                lst_AllReminderInviteDetails.Add(reminderInvite);
                            }
                            if (mailTo.TextPermission)
                            {
                                var reminderInvite = new ReminderInviteToCompleteDTO()
                                {
                                    QuestionnaireID = reminder.QuestionnaireID,
                                    NotifyReminderID = reminder.NotifyReminderId,
                                    AssesmentEmailLinkGUID = assessmentEmailLinkGuid,
                                    CreatedDate = DateTime.UtcNow,
                                    UpdateDate = DateTime.UtcNow,
                                    InviteToCompleteReceiverID = mailTo.InviteToCompleteReceiverID,
                                    Status = PCISEnum.EmailStatus.Pending,
                                    TypeOfInviteSend = PCISEnum.Invitation.SMS,
                                    Email = null,
                                    PhoneNumber = string.Format("{0}{1}", mailTo.PhoneCode, mailTo.Phone),
                                    Attributes = string.Format("ReceiversFirstName={0}|ReceiversDisplayName={1}|ReasoningText={2}", mailTo.FirstName, displayname, assessmentDTO.ReasoningText),
                                    HelperID = (mailTo.InviteReceiverType == PCISEnum.InviteToCompleteReceivers.Helpers || mailTo.InviteReceiverType == PCISEnum.InviteToCompleteReceivers.LeadHelper) ? mailTo.HelperId : null,
                                    PersonID = mailTo.InviteReceiverType == PCISEnum.InviteToCompleteReceivers.Person ? (long?)mailTo.PersonId : null,
                                    PersonSupportID = mailTo.InviteReceiverType == PCISEnum.InviteToCompleteReceivers.Supports ? (int?)mailTo.VoiceTypeFKID : null
                                };
                                lst_AllReminderInviteDetails.Add(reminderInvite);
                            }
                        }
                    }
                    if (lst_AllReminderInviteDetails.Count > 0)
                    {
                        log.LogInformation($"InviteToCompleteTriggerProcess : {DateTime.Now} : Bulk Upload Assessments and EmailLinkDetails");
                        //AddbulkAssessment and get the AssessmentEmailLinkIndex for all EmailLinkDetails inserted
                        AssessmentBulkAddOnInviteDTO assessmentInputDTOs = new AssessmentBulkAddOnInviteDTO()
                        {
                            AssessmentEmailLinkDetailsDTO = lst_AllAssessmentEmailLinkDetail,
                            AssessmentsDTO = lst_AllAssessments
                        };

                        log.LogInformation($"InviteToCompleteTriggerProcess : Bulk Assessment Count : {lst_AllAssessments.Count}");
                        var list_AllEmailLinkIndex = BulkUploadAssessments(utility, apiurl, log, assessmentInputDTOs);
                        if (list_AllEmailLinkIndex.Count > 0)
                        {
                            log.LogInformation($"InviteToCompleteTriggerProcess : {DateTime.Now} : Get Base URL");
                            var currentEnvironment = Environment.GetEnvironmentVariable(PCISEnum.EnvironmentVariables.CurrentEnvironment, EnvironmentVariableTarget.Process);

                            var baseURL = GetConfigurationValue(utility, apiurl, string.Format("{0}_{1}", PCISEnum.ConfigurationParameters.PCIS_BaseURL, currentEnvironment));
                            log.LogInformation($"InviteToCompleteTriggerProcess : {DateTime.Now} : Invite SMS/Email to be send : {lst_AllReminderInviteDetails.Count}");

                            //Create AssessmentLink for Sending Reminder Invite .
                            log.LogInformation($"InviteToCompleteTriggerProcess : {DateTime.Now} : CreateAssessmentLinks - Email with the EmailLinkDetailsIndex of each assessment");
                            foreach (var inviteDetail in lst_AllReminderInviteDetails)
                            {
                                var emailLinkDetails = list_AllEmailLinkIndex.Where(x => x.AssessmentEmailLinkGuid == inviteDetail.AssesmentEmailLinkGUID.Value).FirstOrDefault();
                                if (emailLinkDetails != null)
                                {
                                    inviteDetail.AssesmentEmailLinkIndex = emailLinkDetails.EmailLinkDetailsIndex;
                                    inviteDetail.AssessmentID = emailLinkDetails.AssessmentID;
                                    inviteDetail.AssessmentURL = CreateAssessmentLink(inviteDetail.AssesmentEmailLinkIndex, inviteDetail.TypeOfInviteSend, baseURL, log);
                                }
                            }

                            //BulkAdd Reminder Invite Details
                            log.LogInformation($"InviteToCompleteTriggerProcess : {DateTime.Now} : BulkAddInvitationDetails Count - {lst_AllReminderInviteDetails.Count}");
                            //Add to ReminderInviteToComplete and at same time move the pending status in NotifyReminder to Processing.
                            BulkAddInvitationDetails(utility, apiurl, lst_AllReminderInviteDetails);
                            log.LogInformation($"InviteToCompleteTriggerProcess : {DateTime.Now} : Bulk Add SMS Completed");
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Utility"></param>
        /// <param name="apiurl"></param>
        /// <param name="emailDetails"></param>
        /// <returns></returns>
        private static CRUDResponse BulkAddInvitationDetails(Utility Utility, string apiurl, List<ReminderInviteToCompleteDTO> inviteDetails)
        {
            try
            {
                CRUDResponse Response = new CRUDResponse();
                string result = string.Empty;
                var url = apiurl + PCISEnum.APIurl.ReminderInviteDetails;
                result = Utility.RestApiCall(url, false, false, PCISEnum.APIMethodType.PostRequest, null, inviteDetails.ToJSON());
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
        /// CreateAssessmentLink.
        /// </summary>
        /// <param name="emailParametersIndexId"></param>
        /// <param name="invitationVia"></param>
        /// <param name="baseURL"></param>
        /// <returns></returns>
        public static string CreateAssessmentLink(Guid emailParametersIndexId, string invitationVia, string baseURL, ILogger log)
        {
            try
            {
                if (!emailParametersIndexId.Equals(Guid.Empty))
                {
                    DataProtection dataProtection = new DataProtection();
                    var purposeStringKey = Environment.GetEnvironmentVariable(PCISEnum.EnvironmentVariables.AssessmentEmailLinkKey, EnvironmentVariableTarget.Process);

                    log.LogInformation($"InviteToCompleteTriggerProcess : {emailParametersIndexId}");
                    string assessmentEncryptedURL = string.Empty;
                    if (!string.IsNullOrEmpty(purposeStringKey))
                    {
                        log.LogInformation($"InviteToCompleteTriggerProcess : {purposeStringKey}  {emailParametersIndexId}");
                        string assessmentUrl = baseURL + "?id={0}";
                        string queryParameterId = dataProtection.Encrypt(emailParametersIndexId.ToString() + $"|{PCISEnum.Invitation.Email}", purposeStringKey, log);
                        if (invitationVia.ToLower() == PCISEnum.Invitation.SMS.ToLower())
                            queryParameterId = dataProtection.Encrypt(emailParametersIndexId.ToString() + $"|{PCISEnum.Invitation.SMS}", purposeStringKey, log);
                        assessmentEncryptedURL = string.Format(assessmentUrl, HttpUtility.UrlEncode(queryParameterId));
                    }

                    log.LogInformation($"InviteToCompleteTriggerProcess : URL: {assessmentEncryptedURL}");
                    var Isvalid = Uri.IsWellFormedUriString(assessmentEncryptedURL, UriKind.Absolute);
                    if (Isvalid)
                    {
                        return assessmentEncryptedURL;
                    }
                }
                return string.Empty;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static List<AssessmentEmailLinkDetailsDTO> BulkUploadAssessments(Utility utility, string apiurl, ILogger log, AssessmentBulkAddOnInviteDTO assessmentInputDTO)
        {
            try
            {
                List<AssessmentEmailLinkDetailsDTO> assessments = new List<AssessmentEmailLinkDetailsDTO>();
                var url = apiurl + PCISEnum.APIurl.BulkUploadAssessments;
                log.LogInformation($"InviteToCompleteTriggerProcess : {DateTime.Now} : BulkUploadAssessments url : {url}");
                var result = utility.RestApiCall(url, false, false, PCISEnum.APIMethodType.PostRequest, null, assessmentInputDTO.ToJSON());
                if (!string.IsNullOrEmpty(result))
                {
                    var response = JsonConvert.DeserializeObject<BulkAddAssessmentResponseDetailsDTO>(result);
                    assessments = JsonConvert.DeserializeObject<List<AssessmentEmailLinkDetailsDTO>>(response.result.AssessmentEmailLinkDetails);
                }
                return assessments;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private static List<InviteMailReceiversInDetailDTO> GetReceiversDetailsForReminderInvite(Utility utility, string apiurl, List<long> lst_personQuestionnaireIDs, string typeOfInvite, ILogger log)
        {
            try
            {
                List<InviteMailReceiversInDetailDTO> reminderInviteToCompleteDTO = new List<InviteMailReceiversInDetailDTO>();
                var url = apiurl + PCISEnum.APIurl.GetReceiversDetailsForReminderInvite.Replace(PCISEnum.APIReplacableValues.TypeOfInvite, typeOfInvite);
                log.LogInformation($"InviteToCompleteTriggerProcess : {DateTime.Now} : GetVoiceTypeInDetailForReminderInvite url : {url}");
                var result = utility.RestApiCall(url, false, false, PCISEnum.APIMethodType.PostRequest, null, lst_personQuestionnaireIDs.ToJSON());
                if (!string.IsNullOrEmpty(result))
                {
                    var response = JsonConvert.DeserializeObject<ReminderInviteMailReceiversDTO>(result);
                    reminderInviteToCompleteDTO = response?.result?.iniviteToCompleteMailDetails;
                }
                return reminderInviteToCompleteDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static List<RemindersToTriggerInviteToCompleteDTO> GetReminderDetailsForInviteToComplete(Utility utility, string apiurl, ILogger log)
        {
            try
            {   
                List<RemindersToTriggerInviteToCompleteDTO> reminderInviteToCompleteDTO = new List<RemindersToTriggerInviteToCompleteDTO>();
                var url = apiurl + PCISEnum.APIurl.GetRemindersForInviteToComplete;
                log.LogInformation($"InviteToCompleteTriggerProcess : {DateTime.Now} : GetReminderDetailsForInviteToComplete url : {url}");
                var result = utility.RestApiCall(url, false);
                if (!string.IsNullOrEmpty(result))
                {
                    var response = JsonConvert.DeserializeObject<RemindersToTriggerInviteToCompleteDetailsDTO>(result);
                    reminderInviteToCompleteDTO = response?.result?.ReminderInviteDetails;
                }
                reminderInviteToCompleteDTO = reminderInviteToCompleteDTO?.Where(x => !string.IsNullOrWhiteSpace(x.InviteToCompleteReceivers)).ToList();
                return reminderInviteToCompleteDTO;
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
    }
}
