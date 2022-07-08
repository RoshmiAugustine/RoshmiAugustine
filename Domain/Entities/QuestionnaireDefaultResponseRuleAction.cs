// -----------------------------------------------------------------------
// <copyright file="QuestionnaireSkipLogicRuleCondition.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;

namespace Opeeka.PICS.Domain.Entities
{
    public class QuestionnaireDefaultResponseRuleAction : BaseEntity
    {
        public int QuestionnaireDefaultResponseRuleActionID { get; set; }
        public int QuestionnaireDefaultResponseRuleID { get; set; }
        public int? QuestionnaireItemID { get; set; }
        public int? DefaultResponseID { get; set; }
        public int ListOrder { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UpdateUserID { get; set; }

        public QuestionnaireItem QuestionnaireItem { get; set; }
        public QuestionnaireDefaultResponseRule QuestionnaireDefaultResponseRule { get; set; }
        public User UpdateUser { get; set; }
        public Response DefaultResponse { get; set; }
    }
}
