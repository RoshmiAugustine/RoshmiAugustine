// -----------------------------------------------------------------------
// <copyright file="NotifyReminderDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace ReminderAutoResolveProcess.DTO
{
    public class NotifyReminderDTO 
    {
        public int NotifyReminderID { get; set; }

        public long? PersonQuestionnaireScheduleID { get; set; }

        public int QuestionnaireReminderRuleID { get; set; }

        public DateTime NotifyDate { get; set; }

        public bool IsLogAdded { get; set; }

    }
}
