
namespace ReminderTriggerProcess.Enums
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
            public static string GetBackgroundProcess = "api/background-process/{name}";
            public static string BackgroundProcess = "api/background-process";
            public static string GetActivePerson = "api/active-person";
            public static string GetActivePersonCollaboration = "api/active-person-collaboration/{personId}";
            public static string UpdatePerson = "api/person-isactive-update";
            public static string EmailDetails = "api/emaildetails";
            public static string PersonQuestionnaireByID = "api/person-questionnaire/{personQuestionnaireId}";
            public static string PersonCollaborationByPersonIdAndCollaborationId = "api/person-collaboration-for-reminder/{personId}/{collaborationId}";
            public static string QuestionnaireWindowsByQuestionnaire = "api/questionnaire-window/{questionnaireId}";
            public static string QuestionnaireReminderRulesByQuestionnaire = "api/questionnaire-reminder-rule/{questionnaireId}";
            public static string AllAssessmentReason = "api/assessment-reason-all";
            public static string GetPersonQuestionnaireSchedule = "api/person-questionnaire-schedule/{personQuestionnaireID}/{questionnaireWindowId}";
            public static string AllQuestionnaireReminderType = "api/questionnaire-reminder-type";
            public static string PersonQuestionnaireSchedule = "api/person-questionnaire-schedule";
            public static string NotifyReminder = "api/notify-reminder";
            public static string GetAssessmentById = "api/assessment/{assessmentId}";
            public static string PersonQuestionnaireList = "api/person-questionnaire-list/{personQuestionnaireId}";
            public static string PersonQuestionnaireScheduleList = "api/person-questionnaire-schedule-list";
            public static string NotificationUpdate = "api/notification-update";
            public static string GetAssessmentByPersonQuestionaireIdAndStatus = "api/assessment-foralert/{personQuestionnaireId}/{status}";
            public static string AssessmentResponseByAssessmentID = "api/assessment-response/{assessmentId}";
            public static string RiskRuleByQuestionnaireId = "api/risk-rule/{questionnaireId}";
            public static string PersonHelperByPersonId = "api/person-helper-bypersonId/{personId}";
            public static string GetNotificationLevel = "api/notification-level/{notificationlevelId}";
            public static string RiskRuleCondition = "api/risk-rule-condition/{ruleId}";
            public static string GetResponse = "api/response/{responseId}";
            public static string NotificationTypeByName = "api/notification-type-byname/{notificationType}";
            public static string NotifyRisk = "api/notify-risk";
            public static string NotifyRiskValues = "api/notify-risk-values";
            public static string NotificationLog = "api/notificationlog";
            public static string NotificationStatus = "api/notification-resolution-Status-byname /{status}";
            public static string NotifyReminderScheduleCount = "api/notify-reminder-schedule-count";
            public static string NotifyReminderSchedule = "api/notify-reminder-schedule";
            public static string NotifyReminderByIds = "api/notify-reminder-byids";
            public static string PersonHelperByListofPersonId = "api/person-helper-bypersonId";
            public static string PersonQuestionnaireScheduleListByIds = "api/person-questionnire-schedule-byids";
            public static string GetQuestionaireDetails = "api/questionaire-details-byids";
            
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
            public static string Name = "{name}";
            public static string PersonId = "{personId}";
            public static string CollaborationId = "{collaborationId}";
            public static string QuestionnaireId = "{questionnaireId}";
            public static string QuestionnaireWindowId = "{questionnaireWindowId}";
            public static string AssessmentId = "{assessmentId}";
            public static string Status = "{status}";
            public static string NotificationlevelId = "{notificationlevelId}";
            public static string RuleId = "{ruleId}";
            public static string ResponseId = "{responseId}";
            public static string notificationType = "{notificationType}";
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
        }

        public static class Constants
        {
            public static string Bearer = "bearer";
        }

        public static class ConfigurationParameters
        {
            public static string Culture = "Culture";
            public static string DateTimeFormat = "DateTimeFormat";
            public static string Domain = "Domain";
            public static string PCIS_BaseURL = "PCIS_BaseURL";
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

        public static class ConfigurationKey
        {
            public static string EHRAgencyIds = "EHR_AgencyIDs";
            public static string EHRLoginURL = "EHR_LoginURL";
            public static string EHRUsername = "EHR_Username";
            public static string EHRPassword = "EHR_Password";
            public static string EHRClientURL = "EHR_ClientURL";
            public static string EHRRelationshipURL = "EHR_RelationshipURL";
            public static string EHRProviderURL = "EHR_ProviderURL";
            public static string EHRDefaultCollaborationID = "EHRDefaultCollaborationID";
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

        }
    }
}
