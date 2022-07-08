using System;
using System.Collections.Generic;
using System.Text;

namespace EHRProcess.DTO
{
    public class EHRAssessmentResponseDetailsDTO
    {
        public string version { get; set; }
        public int statusCode { get; set; }
        public string message { get; set; }
        public EHRAssessmentResponseDTO result { get; set; }
    }

    public class EHRAssessmentResponseDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }
        public List<string> EHRAssessmentIDs { get; set; }
        public List<EHRAssessmentDTO> EHRAssessments { get; set; }
    }

    public class EHRAssessmentDTO
    {
        public int AssessmentID { get; set; }
        public long PersonQuestionnaireID { get; set; }
        public long PersonID { get; set; }
        public int QuestionnaireID { get; set; }
        public int InstrumentID { get; set; }
        public string PersonUniversalID { get; set; }
        public string InstrumentAbbrev { get; set; }
        public DateTime ApprovedDate { get; set; }
        public int HelperID { get; set; }
        public string HelperExternalID { get; set; }
        public long AgencyID { get; set; }
    }
}
