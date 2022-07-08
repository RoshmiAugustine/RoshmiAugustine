// -----------------------------------------------------------------------
// <copyright file="AssessmentResponseNoteDTO.cs" company="Naico ITS">
// Copyright (c) Naico ITS. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Domain.DTO
{
    public class AssessmentResponseNoteDTO
    {
        public int AssessmentResponseNoteID { get; set; }
        public int AssessmentResponseID { get; set; }
        public int NoteID { get; set; }
    }
}
