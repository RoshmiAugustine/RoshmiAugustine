using System;

namespace Opeeka.PICS.Domain.DTO
{
    public class AssessmentDetailsDTO
    {
        public int AssessmentID { get; set; }
        public int? DaysInProgram { get; set; }
        public string TimePeriod { get; set; }
        public long PersonID { get; set; }
        public DateTime Date { get; set; }
        public int AssessmentStatusID { get; set; }
        public int AssessmentReasonID { get; set; }
        public long PersonQuestionnaireID { get; set; }
        public string QuestionnaireStatus { get; set; }
        public string Note { get; set; }
        public DateTime? SubmittedDate { get; set; }
        public string Status { get; set; }
        public string Values { get; set; }
        public string? ReasoningText { get; set; }
        public long? PersonQuestionnaireScheduleID { get; set; }
        public bool IsUpdate { get; set; }
        public int? Approved { get; set; }
        public int VoiceTypeID { get; set; }
        public DateTime? EventDate { get; set; }
        public string? EventNotes { get; set; }
        public long? VoiceTypeFKID { get; set; }
        public int TotalCount { get; set; }
        public int RowNumber { get; set; }
    }
}
