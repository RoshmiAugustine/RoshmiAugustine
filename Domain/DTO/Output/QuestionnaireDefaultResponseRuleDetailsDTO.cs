using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Domain.DTO.Output
{
    public class QuestionnaireDefaultResponseRuleDetailsDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }
        public int? QuestionnaireID { get; set; }
        public bool? HasDefaultResponseRule { get; set; }

        public List<QuestionnaireDefaultResponseRuleDTO> QuestionnaireDefaultResponseRules { get; set; }
    }
}
