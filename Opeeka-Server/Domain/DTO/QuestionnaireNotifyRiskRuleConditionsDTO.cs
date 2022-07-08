// -----------------------------------------------------------------------
// <copyright file="QuestionnaireNotifyRiskRuleConditionsDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace Opeeka.PICS.Domain.DTO
{
    public class QuestionnaireNotifyRiskRuleConditionsDTO
    {
        public int QuestionnaireNotifyRiskRuleConditionID { get; set; }
        public int QuestionnaireItemId { get; set; }
        public string ComparisonOperator { get; set; }
        public int ComparisonValue { get; set; }
        public int QuestionnaireNotifyRiskRuleID { get; set; }
        public int ListOrder { get; set; }
        public string JoiningOperator { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UpdateUserID { get; set; }
    }
}
