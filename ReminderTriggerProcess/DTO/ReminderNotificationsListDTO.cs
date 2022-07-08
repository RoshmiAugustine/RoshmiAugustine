// -----------------------------------------------------------------------
// <copyright file="ReminderNotificationsListDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;

namespace ReminderTriggerProcess.DTO
{
    public class ReminderNotificationsListDTO
    {
        public int NotificationLogID { get; set; }
        public string NotificationType { get; set; }
        public DateTime NotificationDate { get; set; }
        public string Status { get; set; }
        public string Note { get; set; }
        public long PersonID { get; set; }
        public bool IsRemoved { get; set; }
        public int NotificationTypeID { get; set; }
        public int NotifyReminderID { get; set; }
        public DateTime NotifyDate { get; set; }
        public DateTime DueDate { get; set; }
        public long PersonQuestionnaireScheduleID { get; set; }
        public int QuestionnaireReminderRuleID { get; set; }
        public long QuestionnaireID { get; set; }
        public string InstrumentAbbrev { get; set; }
        public string AssessmentReasonName { get; set; }
        public string QuestionnaireName { get; set; }
        public string ReminderScheduleName { get; set; }
        public string LeadHelperName { get; set; }
        public int Count { get; set; }
        public bool IsEmailInviteToCompleteReminders { get; set; }
        public bool IsEmailRemindersHelpers { get; set; }
        public long PersonQuestionnaireID { get; set; }
        public string InviteToCompleteMailStatus { get; set; }

    }
    public class RemiderNotificationTriggerTimeDTO
    {
        public DateTime LastRunDatetime { get; set; }
        public DateTime CurrentRunDatetime { get; set; }
    }
}
