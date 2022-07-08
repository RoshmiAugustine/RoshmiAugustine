using System;
using System.Collections.Generic;
using System.Text;

namespace ImportProcess.DTO
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
    public class AssessmentProgressInputDTO
    {
        public int? AssessmentID { get; set; }

        public Guid PersonIndex { get; set; }

        public int QuestionnaireID { get; set; }

        public int VoiceTypeID { get; set; }

        public DateTime DateTaken { get; set; }

        public string ReasoningText { get; set; }

        public int AssessmentReasonID { get; set; }

        public string AssessmentStatus { get; set; }

        public DateTime? CloseDate { get; set; }

        public DateTime? EventDate { get; set; }

        public string? EventNotes { get; set; }
        public long? VoiceTypeFKID { get; set; }

        public int UpdateUserID { get; set; }
        public List<AssessmentResponseInputDTO> AssessmentResponses { get; set; }
    }
    public class AssessmentResponseInputDTO
    {
        public int? AssessmentResponseID { get; set; }

        public int? PersonSupportID { get; set; }

        public int ResponseID { get; set; }

        public int ItemResponseBehaviorID { get; set; }

        public bool IsRequiredConfidential { get; set; }

        public bool IsPersonRequestedConfidential { get; set; }

        public bool IsOtherConfidential { get; set; }

        public int QuestionnaireItemID { get; set; }

        public bool IsCloned { get; set; }

        public string CaregiverCategory { get; set; }
        public Guid? AssessmentResponseGUID { get; set; }
        
        public bool canAddResponseNote { get; set; }
        public decimal ResponseValue { get; set; }
        public int ItemListOrder { get; set; }
        public int? Priority { get; set; }
        public string Value { get; set; }
    }
}
