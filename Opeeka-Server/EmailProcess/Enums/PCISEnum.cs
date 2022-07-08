using System;
using System.Collections.Generic;
using System.Text;

namespace EmailProcess.Enums
{
    public class PCISEnum
    {
        public static class APIurl
        {
            public static string ConfigurationWithKey = "api/configurationvalue/{key}/{agencyID}";
            public static string ConfigurationForAgency = "api/configurations/{agencyID}";
            public static string LookupsForAgency = "api/lookups/{agencyID}";
            public static string PCISUpload = "api/person-upsert";
            public static string GetEmailDetails = "api/emaildetails";
            public static string UpdateEmailDetails = "api/emaildetails";
            public static string ReminderInviteDetails = "api/reminder-invite-details";
            public static string GetHelperDetails = "api/helperdetails";

        }
        public static class APIMethodType
        {
            public const string GetRequest = "GET";
            public const string PostRequest = "POST";
            public const string PutRequest = "PUT";
        }

        public static class APIReplacableValues
        {
            public static string Key = "{key}";
            public static string AgencyID = "{agencyID}";

        }
        public static class EnvironmentVariables
        {
            public static string ApiUrlFromKeyVault = "ApiUrlFromKeyVault";
            public static string ApiSecretFromKeyVault = "ApiSecretFromKeyVault";
            public static string EmailKeyFromKeyVault = "EmailKeyFromKeyVault";
            public static string TwiloAccountSid = "TwiloAccountSid";
            public static string TwiloAuthToken = "TwiloAuthToken";
        }
        public static class TemplateVariables
        {
            public static string PersonInitial = "{{PersonInitial}}";
            public static string DueDate = "{{DueDate}}";
            public static string RedirectUrl = "{{RedirectionUrl}}";
        }

        public static class EmailStatus
        {
            public static string Pending = "Pending";
            public static string Sent = "Sent";
            public static string Failed = "Failed";
            public static string EmailDisabled = "Email Disabled";
        }

        public static class SwitchCase
        {
            public const string PersonInitial = "personinitial";
            public const string DisplayName = "displayname";
            public const string DueDate = "duedate";
            public const string RedirectUrl = "redirecturl";
            public const string Alert = "alert";
            public const string ReminderOverDue = "reminderoverdue";
            public const string ReminderDueDate = "reminderduedate";
            public const string ReminderDueApproching = "reminderdueapproching";
            public const string ReceiversFirstName = "receiversfirstname";
            public const string ReceiversDisplayName = "receiversdisplayname";
            public const string ReasoningText = "reasoningtext";
        }

        public static class Constants
        {
            public static string Bearer = "bearer";
        }
        public static class ConfigurationKey
        {
            public static string AlertTemplate = "AlertTemplate";
            public static string ReminderOverDue = "ReminderOverDueTemplate";
            public static string ReminderDueDate = "ReminderDueDateTemplate";
            public static string ReminderDueApproching = "ReminderDueApprochingTemplate";
            public static string AlertSubject = "AlertSubject";
            public static string ReminderOverDueSubject = "ReminderOverDueSubject";
            public static string ReminderDueDateSubject = "ReminderDueDateSubject";
            public static string ReminderDueApprochingSubject = "ReminderDueApprochingSubject";
            public static string FromEmail = "FromEmailID";
            public static string FromDisplayName = "FromEmailDisplayName";
            public static string PCIS_BaseURL = "PCIS_BaseURL";
            public static string AssessmentEmailText = "AssessmentEmailText";
            public static string AssessmentEmailSubject = "AssessmentEmailSubject";
            public static string AssessmentEmailLinkExpiry = "AssessmentEmailLinkExpiry";

        }
        public static class AssessmentEmail
        {
            public static string emailUrlCodeReplaceText = "{{emailurl}}";
            public static string emailexpiryCodeReplaceText = "{{expiry}}";
            public static string applicationUrlReplaceText = "{{applicationUrl}}";
            public static string personNameReplaceText = "{{personname}}";
            public static string AssessmentNotes = "{{assessmentnotes}}";
        }
        public static class Invitation
        {
            public const string SMS = "sms";
            public const string Email = "email";
        }
        public static class AssessmentSMS
        {
            public static string ConfigKeyToBody = "AssessmentSMSBody";
            public static string ConfigKeyToFromNo = "AssessmentSMSFromNumber";
            public static string StopKey = "AssessmentSMSStop";
            public static string StopMessage = "AssessmentSMSUnsubscribe";
            public static string emailUrlReplaceCode = "{{assessmenturl}}";
        }
    }
}
