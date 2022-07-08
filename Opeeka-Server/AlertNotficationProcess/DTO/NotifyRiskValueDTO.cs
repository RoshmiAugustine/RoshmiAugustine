using System;
using System.Collections.Generic;
using System.Text;

namespace AlertNotficationProcess.DTO
{
    public class NotifyRiskValueDTO
    {
        public int NotifyRiskValueID { get; set; }
        public int NotifyRiskID { get; set; }
        public int QuestionnaireNotifyRiskRuleConditionID { get; set; }
        public int AssessmentResponseID { get; set; }
        public decimal AssessmentResponseValue { get; set; }
    }
}
