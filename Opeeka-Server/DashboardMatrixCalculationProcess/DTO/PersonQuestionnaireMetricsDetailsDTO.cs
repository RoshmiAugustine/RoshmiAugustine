using System;
using System.Collections.Generic;
using System.Text;

namespace DashboardMatrixCalculationProcess.DTO
{
    public class PersonQuestionnaireMetricsDetails
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public List<PersonQuestionnaireMetricsDTO> PersonQuestionnaireMetrics { get; set; }
    }

    public class PersonQuestionnaireMetricsDetailsDTO
    {
        public string version { get; set; }
        public int statusCode { get; set; }
        public string message { get; set; }
        public PersonQuestionnaireMetricsDetails result { get; set; }
    }
    public class PersonAssessmentMetricsDetailsDTO
    {
        public string version { get; set; }
        public int statusCode { get; set; }
        public string message { get; set; }
        public PersonAssessmentMetricsDetails result { get; set; }
    }
    public class PersonAssessmentMetricsDetails
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public List<PersonAssessmentMetricsDTO> PersonQuestionnaireMetrics { get; set; }
    }

}
