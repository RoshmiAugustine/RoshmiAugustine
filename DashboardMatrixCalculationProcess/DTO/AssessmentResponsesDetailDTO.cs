using DashboardMatrixCalculationProcess.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DashboardMatrixCalculationProcess.DTOt
{
    public class AssessmentResponsesDetails
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public List<AssessmentResponsesDTO> AssessmentResponses { get; set; }
    }

    public class AssessmentResponsesDetailsDTO
    {
        public string version { get; set; }
        public int statusCode { get; set; }
        public string message { get; set; }
        public AssessmentResponsesDetails result { get; set; }
    }
}
