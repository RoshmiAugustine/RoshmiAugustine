using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Domain.DTO
{
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
