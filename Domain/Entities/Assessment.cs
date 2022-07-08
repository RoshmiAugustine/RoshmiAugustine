// -----------------------------------------------------------------------
// <copyright file="Assessment.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;

namespace Opeeka.PICS.Domain.Entities
{
    public class Assessment : BaseEntity
    {
        public int AssessmentID { get; set; }
        public long PersonQuestionnaireID { get; set; }
        public int VoiceTypeID { get; set; }
        public DateTime DateTaken { get; set; }
        public string? ReasoningText { get; set; }
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
        public int? EventNoteUpdatedBy { get; set; }
        public int? SubmittedUserID { get; set; }

        public PersonQuestionnaire PersonQuestionnaire { get; set; }
        public VoiceType VoiceType { get; set; }
        public AssessmentReason AssessmentReason { get; set; }
        public AssessmentStatus AssessmentStatus { get; set; }
        public PersonQuestionnaireSchedule PersonQuestionnaireSchedule { get; set; }
        public User UpdateUser { get; set; }
        public User SubmittedUser { get; set; }

        public DateTime NoteUpdateDate { get; set; }
        public int NoteUpdateUserID { get; set; }
        public Guid? AssessmentGUID { get; set; }
        public string EHRUpdateStatus { get; set; }
        public int? NotifyReminderID { get; set; }
    }
}
