// -----------------------------------------------------------------------
// <copyright file="NotifyReminder.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace Opeeka.PICS.Domain.Entities
{
    public class NotifyReminder : BaseEntity
    {
        public int NotifyReminderID { get; set; }

        public long? PersonQuestionnaireScheduleID { get; set; }

        public int QuestionnaireReminderRuleID { get; set; }

        public DateTime NotifyDate { get; set; }

        public bool IsLogAdded { get; set; }

        public bool IsRemoved { get; set; }
        public DateTime? InviteToCompleteSentAt { get; set; }
        public string? InviteToCompleteMailStatus { get; set; }

        public PersonQuestionnaireSchedule PersonQuestionnaireSchedule { get; set; }
        public QuestionnaireReminderRule QuestionnaireReminderRule { get; set; }
    }
}
