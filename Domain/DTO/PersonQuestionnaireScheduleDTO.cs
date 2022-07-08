using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Domain.DTO
{
    public class PersonQuestionnaireScheduleDTO
    {
        public long PersonQuestionnaireScheduleID { get; set; }
        public Guid? PersonQuestionnaireScheduleIndex { get; set; }
        public long PersonQuestionnaireID { get; set; }
        public DateTime WindowDueDate { get; set; }
        public int QuestionnaireWindowID { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool IsRemoveReminderNotifications { get; set; }
        public DateTime? WindowOpenDate { get; set; }
        public DateTime? WindowCloseDate { get; set; }
        public int OccurrenceCounter { get; set; }
    }
}
