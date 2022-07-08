using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Domain.DTO.Output
{
    public class EHRAssessmentResponseDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public List<string> EHRAssessmentIDs { get; set; }
        public List<EHRAssessmentDTO> EHRAssessments { get; set; }
    }
}
