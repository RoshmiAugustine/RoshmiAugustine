// -----------------------------------------------------------------------
// <copyright file="AssessmentNoteDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
namespace Opeeka.PICS.Domain.DTO
{
    public class AssessmentNoteDTO
    {
        public int AssessmentNoteID { get; set; }
        public int AssessmentID { get; set; }
        public int NoteID { get; set; }
        public int? AssessmentReviewHistoryID { get; set; }
    }
}
