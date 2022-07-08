// -----------------------------------------------------------------------
// <copyright file="AssessmentResponseInputDTO.cs" company="Naico ITS">
// Copyright (c) Naico ITS. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Opeeka.PICS.Domain.DTO.Input
{
    public class AssessmentResponseInputDTO
    {
        public int? AssessmentResponseID { get; set; }

        public int? PersonSupportID { get; set; }

        [Required]
        [Range(1, Int32.MaxValue, ErrorMessage = "Please enter a valid ResponseID")]
        public int ResponseID { get; set; }

        public int? ItemResponseBehaviorID { get; set; }

        [Required]
        public bool IsRequiredConfidential { get; set; }

        public bool IsPersonRequestedConfidential { get; set; }

        public bool IsOtherConfidential { get; set; }

        [Required]
        [Range(1, Int32.MaxValue, ErrorMessage = "Please enter a valid QuestionnaireItemID")]
        public int QuestionnaireItemID { get; set; }

        [Required]
        public bool IsCloned { get; set; }

        public string CaregiverCategory { get; set; }
        public Guid? AssessmentResponseGUID { get; set; }

        public bool canAddResponseNote { get; set; }
        public int? Priority { get; set; }
        public string Value { get; set; }

        [Required]
        public int ItemID { get; set; }

        public List<AssessmentResponseNoteInputDTO> AssessmentResponseNotes { get; set; }
        public List<List<AssessmentResponseChildItemsDTO>> AssessmentResponseChildItems { get; set; }
        public List<AssessmentResponseAttachmentInputDTO> AssessmentResponseFiles { get; set; }
        //If Response ValueType as Doodle or Signature
        public byte[] ImageValue { get; set; }
        public int ResponseValueTypeId { get; set; }
    }
    public class AssessmentResponseChildItemsDTO
    {
        public int? AssessmentResponseID { get; set; }

        [Required]
        public int? ChildItemID { get; set; }
        /// <summary>
        /// Holds the value for responce valuetypes like slider,text,date,checkbox(as true/false) etc
        /// For dropdowns it stores the label from response table.
        /// </summary>
        public string Value { get; set; }
        public int? ResponseID { get; set; }

    }
}
