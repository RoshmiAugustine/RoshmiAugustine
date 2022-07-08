using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Domain.DTO.Output
{
    public class QuestionnaireDefaultResponseValuesDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }
        public List<QuestionnaireDefaultResponseDTO> QuestionnaireDefaultResponses { get; set; }
    }

    public class QuestionnaireDefaultResponseDTO
    {
        public int QuestionnaireID { get; set; }
        public int QuestionnaireItemID { get; set; }
        public int ItemID { get; set; }
        public int? DefaultResponseID { get; set; }
        public decimal DefaultValue { get; set; }
    }
}
