using System;
using System.Collections.Generic;
using System.Text;

namespace ReminderTriggerProcess.DTO
{
    public class NotifyReminderDTO
    {
        public int NotifyReminderID { get; set; }

        public long? PersonQuestionnaireScheduleID { get; set; }

        public int QuestionnaireReminderRuleID { get; set; }

        public DateTime NotifyDate { get; set; }

        public bool IsLogAdded { get; set; }
        public string InviteToCompleteMailStatus { get; set; }

    }
}
