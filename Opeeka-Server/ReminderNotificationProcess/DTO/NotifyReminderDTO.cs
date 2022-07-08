// -----------------------------------------------------------------------
// <copyright file="NotifyReminderDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace ReminderNotificationProcess.DTO
{
    public class NotifyReminderDTO 
    {
        public int NotifyReminderID { get; set; }

        public long? PersonQuestionnaireScheduleID { get; set; }

        public int QuestionnaireReminderRuleID { get; set; }

        public DateTime NotifyDate { get; set; }

        public bool IsLogAdded { get; set; }
        public Guid? PersonQuestionnaireScheduleIndex { get; set; }
        public string NotifyReminderTypeName { get; set; }

    }
}
