using System;
using System.Collections.Generic;
using System.Text;

namespace AlertAutoResolveProcess.DTO
{
    public class QuestionnaireNotifyRiskRulesConditionDetails
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public List<QuestionnaireNotifyRiskRuleConditionDTO> NotifyRiskRuleConditions { get; set; }
    }

    public class QuestionnaireNotifyRiskRulesConditionDetailsDTO
    {
        public string version { get; set; }
        public int statusCode { get; set; }
        public string message { get; set; }
        public QuestionnaireNotifyRiskRulesConditionDetails result { get; set; }
    }
}
