using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Domain.DTO.Output
{
    public class SkipLogicResponseDetailsDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }
        public int? QuestionnaireID { get; set; }
        public bool? HasSkipLogic { get; set; }

        public List<QuestionnaireSkipLogicRuleDTO> QuestionnaireSkipLogicRules { get; set; }
    }
}
