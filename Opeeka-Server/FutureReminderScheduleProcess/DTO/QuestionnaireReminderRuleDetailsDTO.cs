using System;
using System.Collections.Generic;
using System.Text;

namespace FutureReminderScheduleProcess.DTO
{
    public class QuestionnaireReminderRuleDetails
    {

        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public List<QuestionnaireReminderRulesDTO> QuestionnaireReminderRule { get; set; }        
    }

    public class QuestionnaireReminderRuleDetailsDTO
    {
        public string version { get; set; }
        public int statusCode { get; set; }
        public string message { get; set; }
        public QuestionnaireReminderRuleDetails result { get; set; }
    }
}
