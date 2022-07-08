// -----------------------------------------------------------------------
// <copyright file="AssessmentDTO.cs" company="Naico ITS">
// Copyright (c) Naico ITS. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace InviteToCompleteTriggerProcess.DTO
{
    public class AssessmentDTO
    {
        public int AssessmentID { get; set; }
        public long PersonQuestionnaireID { get; set; }
        public int VoiceTypeID { get; set; }
        public DateTime DateTaken { get; set; }
        public string ReasoningText { get; set; }
        public int AssessmentReasonID { get; set; }
        public int AssessmentStatusID { get; set; }
        public long? PersonQuestionnaireScheduleID { get; set; }
        public bool IsUpdate { get; set; }
        public int? Approved { get; set; }
        public DateTime? CloseDate { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UpdateUserID { get; set; }
        public DateTime? EventDate { get; set; }
        public string? EventNotes { get; set; }
        public long? VoiceTypeFKID { get; set; }
        public DateTime NoteUpdateDate { get; set; }
        public int NoteUpdateUserID { get; set; }
        public int? EventNoteUpdatedBy { get; set; }
        public int? SubmittedUserID { get; set; }
        public Guid? AssessmentGuid { get; set; }
        public int? NotifyReminderID { get; set; }
    }

    public class AssessmentEmailLinkDetailsDTO
    {
        public Guid EmailLinkDetailsIndex { get; set; }
        public Guid PersonIndex { get; set; }
        public int? PersonSupportID { get; set; }
        public string PersonORSupportEmail { get; set; }
        public int AssessmentID { get; set; }
        public int QuestionnaireID { get; set; }
        public int? HelperID { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UpdateUserID { get; set; }
        public int VoiceTypeID { get; set; }
        public string? PhoneNumber { get; set; }
        public Guid? AssessmentGUID { get; set; }
        public Guid? AssessmentEmailLinkGuid { get; set; }
    }

        public class AssessmentBulkAddOnInviteDTO
    {
        public List<AssessmentDTO> AssessmentsDTO { get; set; }
        public List<AssessmentEmailLinkDetailsDTO> AssessmentEmailLinkDetailsDTO { get; set; }

    }

    public class BulkAddAssessmentResponseDetailsDTO
    {
        public string version { get; set; }
        public int statusCode { get; set; }
        public string message { get; set; }
        public BulkAddAssessmentResponseDTO result { get; set; }
    }
    public class BulkAddAssessmentResponseDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }
        public string AssessmentIDDetails { get; set; }
        public string AssessmentEmailLinkDetails { get; set; }
    }
}
