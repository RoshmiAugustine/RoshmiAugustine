// -----------------------------------------------------------------------
// <copyright file="QuestionnaireNotifyRiskRuleConditionDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;

namespace AlertAutoResolveProcess.DTO
{
    public class QuestionnaireNotifyRiskRuleConditionDTO
    {
        public int QuestionnaireNotifyRiskRuleConditionID { get; set; }
        public int QuestionnaireNotifyRiskRuleID { get; set; }
        public int QuestionnaireItemId { get; set; }
        public string ComparisonOperator { get; set; }
        public decimal ComparisonValue { get; set; }
        public string JoiningOperator { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UpdateUserID { get; set; }
    }
}
