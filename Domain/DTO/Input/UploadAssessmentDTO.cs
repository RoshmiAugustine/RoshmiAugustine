using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Domain.DTO.Input
{
    public class UploadAssessmentDTO
    {
        public long AgencyID { get; set; }
        public int QuestionnaireID { get; set; }
        public int UpdateUserID { get; set; }
        public List<AssessmentProgressDTO> AssessmentsToUpload { get; set; }
    }

    public class AssessmentProgressDTO : AssessmentProgressInputDTO
    {
        public long PersonID { get; set; }
        public int AssessmentStatusID { get; set; }
        public Guid? AssessmentGUID { get; set; }
        public long PersonQuestionnaireID { get; set; }
    }

    public class AssessmentBulkAddOnInviteDTO
    {
        public List<AssessmentDTO> AssessmentsDTO { get; set; }
        public List<AssessmentEmailLinkDetailsDTO> AssessmentEmailLinkDetailsDTO { get; set; }

    }
}
