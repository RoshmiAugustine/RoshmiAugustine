﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Domain.DTO
{
    public class QuestionnaireDefaultResponseRuleConditionDTO
    {
        public int QuestionnaireDefaultResponseRuleConditionID { get; set; }
        public int QuestionnaireID { get; set; }
        public int QuestionnaireItemID { get; set; }
        public string ComparisonOperator { get; set; }
        public decimal ComparisonValue { get; set; }
        public int QuestionnaireDefaultResponseRuleID { get; set; }
        public int ListOrder { get; set; }
        public string? JoiningOperator { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UpdateUserID { get; set; }
        public int ItemID { get; set; }
    }
}
