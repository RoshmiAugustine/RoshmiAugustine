// -----------------------------------------------------------------------
// <copyright file="AssessmentResponseNote.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Domain.Entities
{
    public class AssessmentResponseNote : BaseEntity
    {
        public int AssessmentResponseNoteID { get; set; }
        public int AssessmentResponseID { get; set; }
        public int NoteID { get; set; }

        public AssessmentResponse AssessmentResponse { get; set; }
        public Note Note { get; set; }

    }
}
