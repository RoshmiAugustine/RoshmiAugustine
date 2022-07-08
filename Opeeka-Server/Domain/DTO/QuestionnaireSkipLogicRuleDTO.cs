using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Domain.DTO
{
    public class QuestionnaireSkipLogicRuleDTO
    {
        public int QuestionnaireSkipLogicRuleID { get; set; }
        public string Name { get; set; }
        public int QuestionnaireID { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UpdateUserID { get; set; }
        public int? ClonedQuestionnaireSkipLogicRuleID { get; set; }
        public bool HasSkipLogic { get; set; }

        public List<QuestionnaireSkipLogicRuleConditionDTO> QuestionnaireSkipLogicRuleConditions { get; set; }
        public List<QuestionnaireSkipLogicRuleActionDTO> QuestionnaireSkipLogicRuleActions { get; set; }
    }
}
