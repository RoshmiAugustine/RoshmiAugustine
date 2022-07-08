using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Domain.DTO
{
    public class QuestionnaireDefaultResponseRuleActionDTO
    {
        public int QuestionnaireDefaultResponseRuleActionID { get; set; }
        public int QuestionnaireDefaultResponseRuleID { get; set; }
        public int? QuestionnaireItemID { get; set; }
        public int? DefaultResponseID { get; set; }
        public int ListOrder { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UpdateUserID { get; set; }
        public int ItemID { get; set; }
        public decimal DefaultValue { get; set; }
    }
}
