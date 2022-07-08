using System;
using System.Collections.Generic;
using EmailProcess.Common;
using EmailProcess.DTO;
using EmailProcess.Enums;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace EmailProcess
{
    public static class ProcessEmail
    {
        [FunctionName("ProcessEmail")]
        public static void Run([TimerTrigger("%timer-frequency%")] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            try
            {
                Utility Utility = new Utility();
                EmailSender emailSender = new EmailSender();
                log.LogInformation($"EmailProcess : {DateTime.Now} : Azure function started.");
                var apiurl = Environment.GetEnvironmentVariable(PCISEnum.EnvironmentVariables.ApiUrlFromKeyVault, EnvironmentVariableTarget.Process);
                //Process all Helper related reminder emails
                var EmailProcessDetails = GetEmailDetailsForProcess(Utility, apiurl);
                log.LogInformation($"EmailProcess : {DateTime.Now} : Email details count= {EmailProcessDetails.Count}");
                //Fetch All configurations in one api call
                log.LogInformation($"EmailProcess : {DateTime.Now} : Fetching configurations");
                var configurations = GetAgencySpecificConfiguration(Utility, apiurl, "0", log); 
                var fromemail = configurations[PCISEnum.ConfigurationKey.FromEmail];
                var fromemailDisplayName = configurations[PCISEnum.ConfigurationKey.FromDisplayName];
                if (EmailProcessDetails != null && EmailProcessDetails.Count > 0)
                {
                    log.LogInformation($"EmailProcess : {DateTime.Now} : Fetching Email Related configurationvalue started");
                    var AlertTemplate = configurations[PCISEnum.ConfigurationKey.AlertTemplate];
                    var ReminderDueDateTemplate = configurations[PCISEnum.ConfigurationKey.ReminderDueDate];
                    var ReminderOverDueTemplate = configurations[PCISEnum.ConfigurationKey.ReminderOverDue];
                    var ReminderDueApprochingTemplate = configurations[PCISEnum.ConfigurationKey.ReminderDueApproching];
                    var AlertSubject = configurations[PCISEnum.ConfigurationKey.AlertSubject];
                    var ReminderDueDateSubject = configurations[PCISEnum.ConfigurationKey.ReminderDueDateSubject];
                    var ReminderOverDueSubject = configurations[PCISEnum.ConfigurationKey.ReminderOverDueSubject];
                    var ReminderDueApprochingSubject = configurations[PCISEnum.ConfigurationKey.ReminderDueApprochingSubject];
                    log.LogInformation($"EmailProcess : {DateTime.Now} : Fetching configurationvalue ended");

                    foreach (var item in EmailProcessDetails)
                    {
                        log.LogInformation($"EmailProcess : {DateTime.Now} : Processing of Email details started EmailDetailID= {item.EmailDetailID}");

                        if (item.IsEmailReminderAlerts)
                        {
                            SendEmail sendEmail = new SendEmail();
                            switch (item.Type.ToLower())
                            {
                                case PCISEnum.SwitchCase.Alert:
                                    DraftEmail(AlertTemplate, fromemail, fromemailDisplayName, item, sendEmail, AlertSubject);
                                    break;
                                case PCISEnum.SwitchCase.ReminderDueDate:
                                    DraftEmail(ReminderDueDateTemplate, fromemail, fromemailDisplayName, item, sendEmail, ReminderDueDateSubject);
                                    break;
                                case PCISEnum.SwitchCase.ReminderOverDue:
                                    DraftEmail(ReminderOverDueTemplate, fromemail, fromemailDisplayName, item, sendEmail, ReminderOverDueSubject);
                                    break;
                                case PCISEnum.SwitchCase.ReminderDueApproching:
                                    DraftEmail(ReminderDueApprochingTemplate, fromemail, fromemailDisplayName, item, sendEmail, ReminderDueApprochingSubject);
                                    break;
                            }
                            var response = emailSender.SendEmailAsync(sendEmail);
                            if (response == System.Net.HttpStatusCode.Accepted)
                                item.Status = PCISEnum.EmailStatus.Sent;
                            else
                                item.Status = PCISEnum.EmailStatus.Failed;
                        }
                        else
                        {
                            item.Status = PCISEnum.EmailStatus.EmailDisabled;
                        }
                        log.LogInformation($"EmailProcess : {DateTime.Now} : Email status : {item.Status} for EmailDetailID= {item.EmailDetailID}");
                        item.UpdateDate = DateTime.UtcNow;
                        item.UpdateUserID = 1;
                        log.LogInformation($"EmailProcess : {DateTime.Now} : Processing of Email details ended EmailDetailID= {item.EmailDetailID}");
                    }

                    log.LogInformation($"EmailProcess : {DateTime.Now} : Email details update started.");
                    UpdateEmailDetails(Utility, apiurl, EmailProcessDetails);
                    log.LogInformation($"EmailProcess : {DateTime.Now} : Email details update ended.");
                }

                //PCIS-3225 - Process all InviteToComplete reminder Emails
                #region Reminder-InviteToComplete
                log.LogInformation($"EmailProcess : {DateTime.Now} : ReminderInviteToComplete : Process START");
                var inviteReminderProcessDetails = GetReminderInviteDetailsForProcess(Utility, apiurl);
                log.LogInformation($"EmailProcess : {DateTime.Now} : ReminderInviteToComplete : Count= {EmailProcessDetails.Count}");
                if (inviteReminderProcessDetails.Count> 0)
                {
                    SMSSender smsSender = new SMSSender();
                    string status = string.Empty;
                    //email config
                    var assessmentTemplate = configurations[PCISEnum.ConfigurationKey.AssessmentEmailText];
                    var assessmentEmailSubject = configurations[PCISEnum.ConfigurationKey.AssessmentEmailSubject];
                    var assessmentEmailLinkExpiry = configurations[PCISEnum.ConfigurationKey.AssessmentEmailLinkExpiry];
                    //sms config
                    var smsBodyText = configurations[PCISEnum.AssessmentSMS.ConfigKeyToBody];
                    var fromNumber = configurations[PCISEnum.AssessmentSMS.ConfigKeyToFromNo];
                    foreach (var item in inviteReminderProcessDetails)
                    {
                        log.LogInformation($"EmailProcess : {DateTime.Now} : ReminderInviteToComplete :{item.ReminderInviteToCompleteID} : {item.TypeOfInviteSend}");
                        status = string.Empty;
                        switch (item.TypeOfInviteSend.ToLower())
                        {
                            case PCISEnum.Invitation.Email:
                                status = DraftAndSendEmailForReminder(assessmentTemplate, fromemail, fromemailDisplayName, item, emailSender, assessmentEmailSubject, assessmentEmailLinkExpiry);
                                break;
                            case PCISEnum.Invitation.SMS:
                                status = DraftAndSendTextForReminder(item, smsBodyText, fromNumber, smsSender);   
                                break;
                        }
                        item.Status = status;
                        item.UpdateDate = DateTime.UtcNow;
                        log.LogInformation($"EmailProcess : {DateTime.Now} : ReminderInviteToComplete :{item.ReminderInviteToCompleteID} : {item.TypeOfInviteSend} : Status = {status}");
                    }
                    log.LogInformation($"EmailProcess : {DateTime.Now} : ReminderInviteToComplete : Status update to DB started.");
                    UpdateReminderInviteDetails(Utility, apiurl, inviteReminderProcessDetails);
                    log.LogInformation($"EmailProcess : {DateTime.Now} : ReminderInviteToComplete : Status update to DB ended.");
                }

                log.LogInformation($"EmailProcess : {DateTime.Now} : InviteToComplete-Email : Process END");
                #endregion

                log.LogInformation($"EmailProcess : {DateTime.Now} : Azure function ended.");
            }
            catch (Exception ex)
            {
                log.LogError(ex, $@"EmailProcess : Exception occurred.Exception Message : {ex.Message}.Location : {ex.StackTrace}.Details : {ex.InnerException}");
            }
        }

        private static string DraftAndSendTextForReminder(ReminderInviteToCompleteDTO item, string smsBodyText, string fromNumber, SMSSender smsSender)
        {
            try
            {
                var SMSbody = smsBodyText.Replace(PCISEnum.AssessmentSMS.emailUrlReplaceCode, item.AssessmentURL);
                var response = smsSender.SendSMS(SMSbody, item.PhoneNumber, fromNumber);
                if (response == true)
                    return PCISEnum.EmailStatus.Sent;
                else
                    return PCISEnum.EmailStatus.Failed;

            }
            catch (Exception)
            {

                throw;
            }
        }

        private static string DraftAndSendEmailForReminder(string assessmentTemplate, string fromemail, string fromemailDisplayName, ReminderInviteToCompleteDTO item, EmailSender emailSender, string assessmentEmailSubject, string assessmentEmailLinkExpiry)
        {
            try
            {
                SendEmail sendEmail = new SendEmail();
                string personFirstName = string.Empty;
                string displayName = string.Empty;
                string reasonText = string.Empty;
                string assessmentURL = item.AssessmentURL;
                var baseUri = string.Empty;
                var replacableList = item.Attributes.Split('|');
                foreach (var attributes in replacableList)
                {
                    var values = attributes?.Split('=');
                    switch (values[0]?.Trim().ToLower())
                    {
                        case PCISEnum.SwitchCase.ReceiversFirstName:
                            personFirstName = values[1] != null ? values[1] : string.Empty;
                            break;
                        case PCISEnum.SwitchCase.ReceiversDisplayName:
                            displayName = values[1] != null ? values[1] : string.Empty;
                            break;
                        case PCISEnum.SwitchCase.ReasoningText:
                            reasonText = values[1] != null ? values[1] : string.Empty;
                            break;
                    }

                }
                baseUri = new Uri(assessmentURL).GetLeftPart(System.UriPartial.Authority);
                //replace email url code in email text
                if (assessmentTemplate != null)
                {
                    assessmentTemplate = assessmentTemplate.Replace(PCISEnum.AssessmentEmail.applicationUrlReplaceText, baseUri);
                    assessmentTemplate = assessmentTemplate.Replace(PCISEnum.AssessmentEmail.emailUrlCodeReplaceText, assessmentURL);
                    assessmentTemplate = assessmentTemplate.Replace(PCISEnum.AssessmentEmail.emailexpiryCodeReplaceText, assessmentEmailLinkExpiry);
                    assessmentTemplate = assessmentTemplate.Replace(PCISEnum.AssessmentEmail.personNameReplaceText, personFirstName);
                    assessmentTemplate = assessmentTemplate.Replace(PCISEnum.AssessmentEmail.AssessmentNotes, reasonText);
                }

                sendEmail.Body = assessmentTemplate;
                sendEmail.IsHtmlEmail = true;
                sendEmail.Subject = assessmentEmailSubject;
                sendEmail.FromDisplayName = fromemailDisplayName;
                sendEmail.FromEmail = fromemail;
                sendEmail.ToDisplayName = displayName;
                sendEmail.ToEmail = item.Email;

                var response = emailSender.SendEmailAsync(sendEmail);
                if (response == System.Net.HttpStatusCode.Accepted)
                    return PCISEnum.EmailStatus.Sent;
                else
                    return PCISEnum.EmailStatus.Failed;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private static void DraftEmail(string AlertTemplate, string fromemail, string fromemailDisplayName, EmailDetailsDTO item, SendEmail sendEmail, string subject)
        {
            try
            {
                string displayName = BindTemplate(ref AlertTemplate, item);

                sendEmail.Body = AlertTemplate;
                sendEmail.IsHtmlEmail = true;
                sendEmail.Subject = subject;
                sendEmail.FromDisplayName = fromemailDisplayName;
                sendEmail.FromEmail = fromemail;
                sendEmail.ToDisplayName = displayName;
                sendEmail.ToEmail = item.Email;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static string BindTemplate(ref string AlertTemplate, EmailDetailsDTO item)
        {
            try
            {
                string personInitial = string.Empty;
                string displayName = string.Empty;
                string dueDate = string.Empty;
                string redirecturl = string.Empty;
                var replacableList = item.EmailAttributes.Split('|');
                foreach (var attributes in replacableList)
                {
                    var values = attributes?.Split('=');
                    switch (values[0]?.Trim().ToLower())
                    {
                        case PCISEnum.SwitchCase.PersonInitial:
                            personInitial = values[1] != null ? values[1] : string.Empty;
                            break;
                        case PCISEnum.SwitchCase.DisplayName:
                            displayName = values[1] != null ? values[1] : string.Empty;
                            break;
                        case PCISEnum.SwitchCase.DueDate:
                            dueDate = values[1] != null ? values[1] : string.Empty;
                            break;
                    }

                }
                var Isvalid = Uri.IsWellFormedUriString(item.URL, UriKind.Absolute);
                if (Isvalid)
                {
                    redirecturl = item.URL;
                }
                AlertTemplate = AlertTemplate.Replace(PCISEnum.TemplateVariables.PersonInitial, personInitial);
                AlertTemplate = AlertTemplate.Replace(PCISEnum.TemplateVariables.DueDate, dueDate);
                AlertTemplate = AlertTemplate.Replace(PCISEnum.TemplateVariables.RedirectUrl, redirecturl);
                return displayName;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static List<ReminderInviteToCompleteDTO> GetReminderInviteDetailsForProcess(Utility Utility, string apiurl)
        {
            try
            {
                List<ReminderInviteToCompleteDTO> inviteDetails = new List<ReminderInviteToCompleteDTO>();
                var configAPiurl = string.Empty;
                configAPiurl = apiurl + PCISEnum.APIurl.ReminderInviteDetails;
                var result = Utility.RestApiCall(configAPiurl, false);
                if (!string.IsNullOrEmpty(result))
                {
                    var inviteDetailsResponse = JsonConvert.DeserializeObject<GetEmailDetailsResponseDTO>(result);
                    inviteDetails = inviteDetailsResponse?.result?.reminderInviteDetails;
                }
                return inviteDetails;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static List<EmailDetailsDTO> GetEmailDetailsForProcess(Utility Utility, string apiurl)
        {
            try
            {
                List<EmailDetailsDTO> EmailDetails = new List<EmailDetailsDTO>();
                var configAPiurl = apiurl + PCISEnum.APIurl.GetEmailDetails;
                var result = Utility.RestApiCall(configAPiurl, false);
                if (!string.IsNullOrEmpty(result))
                {
                    var emailDetailsResponse = JsonConvert.DeserializeObject<GetEmailDetailsResponseDTO>(result);
                    EmailDetails = emailDetailsResponse?.result?.EmailDetails;
                }
                return EmailDetails;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static List<EmailDetailsDTO> UpdateEmailDetails(Utility Utility, string apiurl, List<EmailDetailsDTO> emailDetails)
        {
            try
            {
                List<EmailDetailsDTO> EmailDetails = new List<EmailDetailsDTO>();
                var updateApiUrl = apiurl + PCISEnum.APIurl.UpdateEmailDetails;
                var result = Utility.RestApiCall(updateApiUrl, false, false, PCISEnum.APIMethodType.PutRequest, null, emailDetails.ToJSON());
                if (!string.IsNullOrEmpty(result))
                {
                    var emailDetailsResponse = JsonConvert.DeserializeObject<GetEmailDetailsResponseDTO>(result);
                    EmailDetails = emailDetailsResponse?.result?.EmailDetails;
                }
                return EmailDetails;
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

        private static Dictionary<string, string> GetAgencySpecificConfiguration(Utility Utility, string apiurl, string agencyId, ILogger log)
        {
            try
            {
                Dictionary<string, string> configs = new Dictionary<string, string>();
                   var agencySpecificConfigurationurl = apiurl + PCISEnum.APIurl.ConfigurationForAgency.Replace(PCISEnum.APIReplacableValues.AgencyID, agencyId);
                var response = Utility.RestApiCall(agencySpecificConfigurationurl, false);

                if (!string.IsNullOrWhiteSpace(response))
                {
                    var agencySpecificConfiguration = JsonConvert.DeserializeObject<AgencyConfigurationResponseDTO>(response);
                    configs = agencySpecificConfiguration.result.Configurations;
                }
                return configs;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static CRUDResponse UpdateReminderInviteDetails(Utility Utility, string apiurl, List<ReminderInviteToCompleteDTO> smsDetailsDTO)
        {
            try
            {
                CRUDResponse inviteDetails = new CRUDResponse();
                var updateApiUrl = apiurl + PCISEnum.APIurl.ReminderInviteDetails;
                var result = Utility.RestApiCall(updateApiUrl, false, false, PCISEnum.APIMethodType.PutRequest, null, smsDetailsDTO.ToJSON());
                if (!string.IsNullOrEmpty(result))
                {
                    var inviteDetailsResponse = JsonConvert.DeserializeObject<CRUDResponse>(result);
                    inviteDetails = inviteDetailsResponse;
                }
                return inviteDetails;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
