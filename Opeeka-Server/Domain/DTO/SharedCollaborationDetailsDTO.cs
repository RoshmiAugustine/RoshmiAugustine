using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Domain.DTO
{
    public class SharedCollaborationDetailsDTO
    {
        public long PersonQuestionnaireID { get; set; }
        public long PersonID { get; set; }
        public int QuestionnaireID { get; set; }
        public int CollaborationID { get; set; }
        public long PersonCollaborationID { get; set; }
    }
    public class SharedDetailsDTO
    {
        public long AgencyID { get; set; }
        public long PersonID { get; set; }
        public string SharedQuestionnaireIDs { get; set; }
        public string SharedCollaborationIDs { get; set; }
        public string SharedAssessmentIDs { get; set; }
    }

}
