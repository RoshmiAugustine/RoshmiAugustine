// -----------------------------------------------------------------------
// <copyright file="AssessmentResponseNoteInputDTO.cs" company="Naico ITS">
// Copyright (c) Naico ITS. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace Opeeka.PICS.Domain.DTO.Input
{
    public class AssessmentResponseNoteInputDTO
    {
        public int? NoteID { get; set; }

        [Required]
        public string NoteText { get; set; }

        [Required]
        public bool IsConfidential { get; set; }
    }
    /// <summary>
    /// If responseValueTypeID = Attachments, the files should be passed as a child array of this type under its AssessmentResponse.
    /// If responseValueTypeID = Doodle/Sign, Nothing is passed and the createdBy/Date is handled via code only.
    /// </summary>
    public class AssessmentResponseAttachmentInputDTO
    {
        public int? AssessmentResponseAttachmentID { get; set; }

        public string FileName { get; set; }        
        //If Response ValueType as Attachments
        public byte[] ByteArrayValue { get; set; }
    }
}
