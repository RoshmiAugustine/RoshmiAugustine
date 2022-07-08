// -----------------------------------------------------------------------
// <copyright file="QuestionnaireReminderRulesDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace ReminderNotificationProcess.DTO
{
    public class QuestionnaireReminderRulesDTO
    {
        public int QuestionnaireReminderRuleID { get; set; }
        public int QuestionnaireID { get; set; }
        public int QuestionnaireReminderTypeID { get; set; }
        public int? ReminderOffsetDays { get; set; }
        public char? ReminderOffsetTypeID { get; set; }
        public bool CanRepeat { get; set; }
        public int? RepeatInterval { get; set; }
        public bool IsSelected { get; set; }
    }
}
