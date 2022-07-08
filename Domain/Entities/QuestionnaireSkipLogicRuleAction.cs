// -----------------------------------------------------------------------
// <copyright file="QuestionnaireSkipLogicRuleCondition.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;

namespace Opeeka.PICS.Domain.Entities
{
    public class QuestionnaireSkipLogicRuleAction : BaseEntity
    {
        public int QuestionnaireSkipLogicRuleActionID { get; set; }
        public int QuestionnaireSkipLogicRuleID { get; set; }
        public int ActionLevelID { get; set; }
        public int? QuestionnaireItemID { get; set; }
        public int? CategoryID { get; set; }
        public int? ChildItemID { get; set; }
        public int? ParentItemID { get; set; }
        public int ActionTypeID { get; set; }
        public int ListOrder { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UpdateUserID { get; set; }

        public QuestionnaireItem QuestionnaireItem { get; set; }
        public Category Category { get; set; }
        public Item Item { get; set; }
        public Item ItemForParent { get; set; }
        public ActionType ActionType { get; set; }
        public ActionLevel ActionLevel { get; set; }
        public QuestionnaireSkipLogicRule QuestionnaireSkipLogicRule { get; set; }
        public User UpdateUser { get; set; }
    }
}
