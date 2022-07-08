// -----------------------------------------------------------------------
// <copyright file="AssessmentProgressInputDTO.cs" company="Naico ITS">
// Copyright (c) Naico ITS. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Opeeka.PICS.Domain.DTO.Input
{
    public class AssessmentProgressInputDTO
    {
        public int? AssessmentID { get; set; }

        [Required]
        public Guid PersonIndex { get; set; }

        [Required]
        [Range(1, Int32.MaxValue, ErrorMessage = "Please enter a valid Questionnaire")]
        public int QuestionnaireID { get; set; }

        [Required]
        [Range(1, Int32.MaxValue, ErrorMessage = "Please enter a valid Voice Type")]
        public int VoiceTypeID { get; set; }

        [Required]
        public DateTime DateTaken { get; set; }

        public string ReasoningText { get; set; }

        [Required]
        [Range(1, Int32.MaxValue, ErrorMessage = "Please enter a valid Assessment Reason")]
        public int AssessmentReasonID { get; set; }

        [Required]
        public string AssessmentStatus { get; set; }

        public DateTime? CloseDate { get; set; }

        public DateTime? EventDate { get; set; }

        public string? EventNotes { get; set; }
        public long? VoiceTypeFKID { get; set; }

        public List<AssessmentResponseInputDTO> AssessmentResponses { get; set; }
    }
}
