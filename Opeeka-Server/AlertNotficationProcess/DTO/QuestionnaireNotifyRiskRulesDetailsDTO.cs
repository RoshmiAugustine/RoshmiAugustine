using System;
using System.Collections.Generic;
using System.Text;

namespace AlertNotficationProcess.DTO
{
    public class QuestionnaireNotifyRiskRulesDetails
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public List<QuestionnaireNotifyRiskRulesDTO> NotifyRiskRules { get; set; }
    }
    public class QuestionnaireNotifyRiskRulesDetailsDTO
    {
        public string version { get; set; }
        public int statusCode { get; set; }
        public string message { get; set; }
        public QuestionnaireNotifyRiskRulesDetails result { get; set; }
    }
}
