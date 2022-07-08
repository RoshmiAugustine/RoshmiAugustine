using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Domain.DTO.Output
{
    public class PersonQuestionnaireMetricsDetailsDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public List<PersonQuestionnaireMetricsDTO> PersonQuestionnaireMetrics { get; set; }
    }

    public class PersonAssessmentMetricsDetailsDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public List<PersonAssessmentMetricsDTO> PersonQuestionnaireMetrics { get; set; }
    }
}
