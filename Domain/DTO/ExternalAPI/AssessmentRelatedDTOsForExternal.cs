using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Opeeka.PICS.Domain.DTO.ExternalAPI
{
    public class AssessmentSearchInputDTO : Paginate
    {
        public AssessmentSearchFields SearchFields { get; set; }
    }
    public class AssessmentSearchFields
    {
        [DefaultValue(null)]
        public int? QuestionnaireID { get; set; } = null;
        [DefaultValue(null)]
        public Guid? HelperIndex { get; set; } = null;
        [DefaultValue(null)]
        public Guid? PersonIndex { get; set; }
        public int? AssessmentID { get; set; }
    }

    public class AssessmentDetailsListDTO
    {
        public int TotalCount { get; set; }
        public int AssessmentID { get; set; }
        public Guid PersonIndex { get; set; }
        public string PersonName { get; set; }
        public DateTime DateTaken { get; set; }
        public string? ReasonNote { get; set; }
        public int AssessmentStatusID { get; set; }
        public string AssessmentStatus { get; set; }
        public DateTime? EventDate { get; set; }
        public string? EventNotes { get; set; }
        public int AssessmentReasonID { get; set; }
        public string AssessmentReason { get; set; }
        public int QuestionnaireID { get; set; }
        public int VoiceTypeID { get; set; }
        public string VoiceType { get; set; }
        public long? VoiceTypeFKID { get; set; }
        public DateTime? SubmittedDate { get; set; }
        public bool IsUpdate { get; set; }
        public int? Approved { get; set; }
        public long PersonQuestionnaireID { get; set; }
        public long? PersonQuestionnaireScheduleID { get; set; }
        public int? DaysInProgram { get; set; }
        public string TimePeriod { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UpdateUserID { get; set; }
        public string AssessmentResponses { get; set; }
    }
    public class AssessmentDetailsResponseDTOForExternal
    {

        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public int TotalCount { get; set; }
        public List<AssessmentDetailsListDTO> AssessmentDataList { get; set; }
    }
}
