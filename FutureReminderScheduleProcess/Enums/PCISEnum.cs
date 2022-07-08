using System;
using System.Collections.Generic;
using System.Text;

namespace FutureReminderScheduleProcess.Enums
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
            public static string PersonQuestionnaireSchedule = "api/person-questionnaire-schedule-bulk";
            public static string NotifyReminder = "api/notify-reminder";
            public static string GetSchedulesWithMaxEndDate = "api/person-questionnaire-schedule-details";
            public static string GetAllPersonQuestionnairesToBeScheduled = "api/person-questionnaires-tobescheduled";
            public static string GetAllQuestionnaireReminderRule = "api/questionnaire-reminder-rule-all";
            public static string GetBackgroundProcess = "api/background-process/{name}";
            public static string BackgroundProcess = "api/background-process";
            public static string RegularReminderSettingsByQuestionnaire = "api/questionnaire-regular-reminder-settings";
            public static string GetAllPersonCollaboration = "api/person-collaboration-regular-schedules";
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
            public static string QuestionnaireId = "{questionnaireId}";
            public static string Name = "{name}";
            public static string LastRunDateTime = "{lastrundatetime}";
            public static string CurrentRunDateTime = "{currentrundatetime}";
        }
        public static class EnvironmentVariables
        {
            public static string ApiUrlFromKeyVault = "ApiUrlFromKeyVault";
            public static string ApiSecretFromKeyVault = "ApiSecretFromKeyVault";
            public static string EmailKeyFromKeyVault = "EmailKeyFromKeyVault";
        }
        public static class Constants
        {
            public static string Bearer = "bearer";
            public static int UploadBlockCount = 10;
        }
        public static class QuestionnaireReminderType
        {
            public const string WindowOpen = "Window Open";
            public const string AssesmentDue = "Assesment Due";
            public const string WindowClose = "Window Close";
            public const string QuestionnaireLate = "Questionnaire Late";
            public const string QuestionnaireOverdue = "Questionnaire Overdue";
        }
        public static class ConfigurationKey
        {
            public static string Reminder_Count_After_WindowCloseDay = "Reminder_Count_After_WindowCloseDay";
        }
        public static class BackgroundProcess
        {
            public static string TriggerFutureReminder = "TriggerFutureReminderSchedule";
        }
        public static class RecurrencePatternGroup
        {
            public const string Daily = "daily";
            public const string Weekly = "weekly";
            public const string Monthly = "monthly";
            public const string Yearly = "yearly";
        }
        public static class RecurrencePattern
        {
            public const string DailyDays = "dailydays";
            public const string DailyWeekdays = "dailyweekdays";
            public const string Weekly = "weekly";
            public const string MonthlyByDay = "monthlybyday";
            public const string MonthlyByOrdinalDay = "monthlybyordinalday";
            public const string YearlyByMonth = "yearlybymonth";
            public const string YearlyByOrdinal = "yearlybyordinal";
        }
        public static class RecurrenceOrdinal
        {
            public const string First = "first";
            public const string Second = "second";
            public const string Third = "third";
            public const string Fourth = "fourth";
            public const string Last = "last";
        }
        public static class OffsetType
        {
            public const char Day = 'd';
            public const char Hour = 'h';
        }
        public static class RecurrenceEndType
        {
            public const string EndByEndate = "endbyenddate";
            public const string EndByNumberOfOccurences = "endbynmbrofoccurence";
            public const string EndByNoEndate = "endbynoenddate";
        }
        public static class RecurrenceDay
        {
            public const string ByWeekDay = "weekday";
            public const string ByDay = "day";
            public const string Sunday = "sunday";
            public const string Monday = "monday";
            public const string Tuesday = "tuesday";
            public const string Wednesday = "wednesday";
            public const string Thursday = "thursday";
            public const string Friday = "friday";
            public const string Saturday = "saturday";
        }
        public static class TimeZones
        {
            public const string UTC = "Coordinated Universal Time";
        }
    }
}
