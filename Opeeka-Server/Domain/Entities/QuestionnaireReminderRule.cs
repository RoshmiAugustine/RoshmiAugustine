// -----------------------------------------------------------------------
// <copyright file="QuestionnaireReminderRule.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace Opeeka.PICS.Domain.Entities
{
    public class QuestionnaireReminderRule : BaseEntity
    {
        public int QuestionnaireReminderRuleID { get; set; }
        public int QuestionnaireID { get; set; }
        public int QuestionnaireReminderTypeID { get; set; }
        public int? ReminderOffsetDays { get; set; }
        public char? ReminderOffsetTypeID { get; set; }
        public bool CanRepeat { get; set; }
        public int? RepeatInterval { get; set; }
        public bool IsSelected { get; set; }
        public DateTime UpdateDate { get; set; }

        public Questionnaire Questionnaire { get; set; }
        public QuestionnaireReminderType QuestionnaireReminderType { get; set; }
        public OffsetType ReminderOffsetType { get; set; }
    }
}
