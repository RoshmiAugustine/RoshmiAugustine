using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Domain.DTO
{
    public class QuestionnaireDefaultResponseRuleDTO
    {
        public int QuestionnaireDefaultResponseRuleID { get; set; }
        public string Name { get; set; }
        public int QuestionnaireID { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UpdateUserID { get; set; }
        public int? ClonedQuestionnaireDefaultResponseRuleID { get; set; }
        public bool HasDefaultResponseRule { get; set; }

        public List<QuestionnaireDefaultResponseRuleConditionDTO> QuestionnaireDefaultResponseRuleConditions { get; set; }
        public List<QuestionnaireDefaultResponseRuleActionDTO> QuestionnaireDefaultResponseRuleActions { get; set; }
    }
}
