using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Domain.DTO
{
    public class NotifyReminderDTO
    {
        public int NotifyReminderID { get; set; }

        public long? PersonQuestionnaireScheduleID { get; set; }

        public int QuestionnaireReminderRuleID { get; set; }

        public DateTime NotifyDate { get; set; }

        public bool IsLogAdded { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime? InviteToCompleteSentAt { get; set; }
        public string InviteToCompleteMailStatus { get; set; }

    }
}
