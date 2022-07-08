using System;
using System.Collections.Generic;
using System.Text;

namespace ReminderAutoResolveProcess.DTO
{
    public class PersonQuestionnaireScheduleInputDTO
    {
        public DateTime DateTaken { get; set; }
        public List<int> QuestionnaireWindowListID { get; set; }
        public long PersonQuestionnaireID { get; set; }
    }
}
