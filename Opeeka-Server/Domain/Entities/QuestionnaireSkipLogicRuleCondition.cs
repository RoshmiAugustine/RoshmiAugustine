// -----------------------------------------------------------------------
// <copyright file="QuestionnaireSkipLogicRuleCondition.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;

namespace Opeeka.PICS.Domain.Entities
{
    public class QuestionnaireSkipLogicRuleCondition : BaseEntity
    {
        public int QuestionnaireSkipLogicRuleConditionID { get; set; }
        public int QuestionnaireItemID { get; set; }
        public string ComparisonOperator { get; set; }
        public decimal ComparisonValue { get; set; }
        public int QuestionnaireSkipLogicRuleID { get; set; }
        public int ListOrder { get; set; }
        public string? JoiningOperator { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UpdateUserID { get; set; }

        public QuestionnaireItem QuestionnaireItem { get; set; }
        public QuestionnaireSkipLogicRule QuestionnaireSkipLogicRule { get; set; }
        public User UpdateUser { get; set; }
    }
}