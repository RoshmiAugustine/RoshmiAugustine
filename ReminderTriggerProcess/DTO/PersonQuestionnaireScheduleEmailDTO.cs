using System;
using System.Collections.Generic;
using System.Text;

namespace ReminderTriggerProcess.DTO
{
    public class PersonQuestionnaireScheduleEmailDTO
    {
        public long PersonID { get; set; }
        public int QuestionnaireID { get; set; }
        public long PersonQuestionnaireID { get; set; }
        public long PersonQuestionnaireScheduleID { get; set; }
        public DateTime WindowDueDate { get; set; }
        public string NotificationType { get; set; }
    }
}
