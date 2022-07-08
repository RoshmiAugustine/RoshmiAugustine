using System;
using System.Collections.Generic;
using System.Text;

namespace ReminderAutoResolveProcess.DTO
{
    public class PersonQuestionnaireScheduleDTO
    {
        public long PersonQuestionnaireScheduleID { get; set; }
        public long PersonQuestionnaireID { get; set; }
        public DateTime WindowDueDate { get; set; }
        public int QuestionnaireWindowID { get; set; }
        public bool IsRemoved { get; set; }
    }
}
