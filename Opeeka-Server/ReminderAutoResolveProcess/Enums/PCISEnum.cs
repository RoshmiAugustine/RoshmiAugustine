using System;
using System.Collections.Generic;
using System.Text;

namespace ReminderAutoResolveProcess.Enums
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

        }
        public static class NotificationType
        {
            public static string Alert = "Alert";
            public static string Reminder = "Reminder";
        }
        public static class QuestionnaireReminderType
        {
            public const string WindowOpen = "Window Open";
            public const string AssesmentDue = "Assesment Due";
            public const string WindowClose = "Window Close";
            public const string QuestionnaireLate = "Questionnaire Late";
            public const string QuestionnaireOverdue = "Questionnaire Overdue";
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
            public static string PersonQuestionnaireId = "{personQuestionnaireId}";
            public static string PersonId = "{personId}";
            public static string CollaborationId = "{collaborationId}";
            public static string QuestionnaireId = "{questionnaireId}";
            public static string QuestionnaireWindowId = "{questionnaireWindowId}";
            public static string AssessmentId = "{assessmentId}";
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
            public static string Reminder_Count_After_WindowCloseDay = "Reminder_Count_After_WindowCloseDay";
            public static string Reminder_LimitInMonth_If_EndDate_Null = "Reminder_LimitInMonth_If_EndDate_Null";
        }

        public static class AssessmentReason
        {
            public static string Trigger = "Triggering Event";
            public static string Initial = "Initial";
            public static string Scheduled = "Scheduled";
            public static string Discharge = "Discharge";
        }
    }
}
