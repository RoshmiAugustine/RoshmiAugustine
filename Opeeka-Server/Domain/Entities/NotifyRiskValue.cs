// -----------------------------------------------------------------------
// <copyright file="NotifyRiskValue .cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Domain.Entities
{
    public class NotifyRiskValue : BaseEntity
    {
        public int NotifyRiskValueID { get; set; }
        public int NotifyRiskID { get; set; }
        public int QuestionnaireNotifyRiskRuleConditionID { get; set; }
        public int AssessmentResponseID { get; set; }
        public decimal AssessmentResponseValue { get; set; }

        public AssessmentResponse AssessmentResponse { get; set; }
        public NotifyRisk NotifyRisk { get; set; }
        public QuestionnaireNotifyRiskRuleCondition QuestionnaireNotifyRiskRuleCondition { get; set; }
    }
}
