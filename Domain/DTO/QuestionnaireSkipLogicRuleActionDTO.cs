using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Domain.DTO
{
    public class QuestionnaireSkipLogicRuleActionDTO
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
        public string ActionLevelName { get; set; }
        public string ActionTypeName { get; set; }
        public int ItemID { get; set; }
    }
}
