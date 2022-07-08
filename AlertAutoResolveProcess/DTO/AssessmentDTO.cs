// -----------------------------------------------------------------------
// <copyright file="AssessmentDTO.cs" company="Naico ITS">
// Copyright (c) Naico ITS. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace AlertAutoResolveProcess.DTO
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
    }
}
