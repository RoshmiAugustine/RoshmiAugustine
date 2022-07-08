using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Domain.DTO.Output
{
    public class EHRLookupResponseDTO
    {
        public string ResponseStatus { get; set; }
        public int ResponseStatusCode { get; set; }
        public Dictionary<string, object> lookups { get; set; }
        public List<QuestionnaireItemsForImportDTO> QuestionItemDetails { get; set; }
        public Dictionary<string, PersonVoiceTypeDetailsForImportDTO> personVoiceTypeDetails { get; set; }
    }
}
