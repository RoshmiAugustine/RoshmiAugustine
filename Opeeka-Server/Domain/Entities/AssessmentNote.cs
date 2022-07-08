// -----------------------------------------------------------------------
// <copyright file="AssessmentNote.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Domain.Entities
{
    public class AssessmentNote : BaseEntity
    {
        public int AssessmentNoteID { get; set; }
        public int AssessmentID { get; set; }
        public int NoteID { get; set; }
        public int? AssessmentReviewHistoryID { get; set; }

        public Assessment Assessment { get; set; }
        public ReviewerHistory AssessmentReviewHistory { get; set; }
        public Note Note { get; set; }
    }
}
