
namespace InviteToCompleteTriggerProcess.Enums
{
    public class PCISEnum
    {        
        public static class APIurl
        {
            public static string ConfigurationWithKey = "api/configurationvalue/{key}/{agencyID}";
            public static string ConfigurationForAgency = "api/configurations/{agencyID}"; 
            public static string GetRemindersForInviteToComplete = "api/invitetocomplete-regular-reminder-details"; 
            public static string GetReceiversDetailsForReminderInvite = "api/invitetocomplete-receivers-details/{typeOfReciever}";
            public static string BulkUploadAssessments = "api/reminderinvite-bulkupload-assessments";
            public static string ReminderInviteDetails = "api/reminder-invite-details";


        }
        public static class APIMethodType
        {
            public const string GetRequest = "GET";
            public const string PostRequest = "POST";
            public const string PutRequest = "PUT";
        }

        public static class Limits
        {
            public static int StatusLimit = 100;
            public static int StatusCount = 0;
        }

        public static class APIReplacableValues
        {
            public static string Key = "{key}";
            public static string AgencyID = "{agencyID}";
            public static string TypeOfInvite = "{typeOfReciever}";
        }

        public static class BackgroundProcess
        {
            public static string TriggerStatusUpdate = "TriggerStatusUpdate";
            public static string TriggerReminderNotification = "TriggerReminderNotification";

        }
        public static class NotificationStatus
        {
            public static string Unresolved = "Unresolved";
            public static string Resolved = "Resolved";
        }

        public static class NotificationType
        {
            public static string Alert = "Alert";
            public static string Reminder = "Reminder";
        }
        public static class EnvironmentVariables
        {
            public static string ApiUrlFromKeyVault = "ApiUrlFromKeyVault";
            public static string ApiSecretFromKeyVault = "ApiSecretFromKeyVault";
            public static string EmailKeyFromKeyVault = "EmailKeyFromKeyVault";
            public static string AssessmentEmailLinkKey = "AssessmentEmailLinkKey";
            public static string CurrentEnvironment = "CurrentEnvironment";
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
        }

        public static class Constants
        {
            public static string Bearer = "bearer";
            public static int UploadBlockCount = 10;
            public static string InviteToComplete = "invitetocomplete";
        }

        public static class ConfigurationParameters
        {
            public static string Culture = "Culture";
            public static string DateTimeFormat = "DateTimeFormat";
            public static string Domain = "Domain";
            public static string PCIS_BaseURL = "EmailAssessmentURL";
            public static string PeopleQuestionnaireURL = "PeopleQuestionnaireURL";
        }

        public static class EmailDetail
        {
            public static string AssessmentID = "{assessmentid}";
            public static string PersonIndex = "{personindex}";
            public static string QuestionnaireId = "{questionnaireid}";
            public static string NotificationType = "{notificationtype}";
            public const string ReminderOverDue = "reminderoverdue";
            public const string ReminderDueDate = "reminderduedate";
            public const string ReminderDueApproching = "reminderdueapproching";
        }
        public static class InviteToCompleteReceivers
        {
            public static string Helpers = "All Helpers";
            public static string LeadHelper = "Lead Helper";
            public static string Person = "Person In Care";
            public static string Supports = "Supports";
        }
        public static class Invitation
        {
            public static string SMS = "SMS";
            public static string Email = "Email";
        }
        public static class SwitchCase
        {
            public const string ReceiversFirstName = "ReceiversFirstName";
            public const string ReceiversDisplayName = "ReceiversDisplayName";
            public const string ReasoningText = "ReasoningText";
        }

    }
}
