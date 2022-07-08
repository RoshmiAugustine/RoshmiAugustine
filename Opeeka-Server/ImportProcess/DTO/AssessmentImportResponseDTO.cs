using System;
using System.Collections.Generic;
using System.Text;

namespace ImportProcess.DTO
{
    public class AssessmentTemplateResponseDTO
    {
        public string version { get; set; }
        public int statusCode { get; set; }
        public string message { get; set; }
        public AssessmentTemplateResponse result { get; set; }

    }
    public class AssessmentTemplateResponse
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public string TemplateJson { get; set; }
    }
}
