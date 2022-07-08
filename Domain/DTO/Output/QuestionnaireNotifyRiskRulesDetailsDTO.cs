using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Domain.DTO.Output
{
    public class QuestionnaireNotifyRiskRulesDetailsDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public List<QuestionnaireNotifyRiskRulesDTO> NotifyRiskRules { get; set; }
    }
}
