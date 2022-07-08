using System;
using System.Collections.Generic;
using System.Text;

namespace AlertAutoResolveProcess.DTO
{
    public class AssessmentResponseDetails
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public AssessmentDTO Assessment { get; set; }
    }

    public class AssessmentResponseDetailsDTO
    {
        public string version { get; set; }
        public int statusCode { get; set; }
        public string message { get; set; }
        public AssessmentResponseDetails result { get; set; }
    }
}
