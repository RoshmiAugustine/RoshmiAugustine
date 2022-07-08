// -----------------------------------------------------------------------
// <copyright file="AssessmentEmailLinkInputDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;

namespace Opeeka.PICS.Domain.DTO
{
    public class AssessmentEmailLinkInputDTO
    {
        public Guid PersonIndex { get; set; }
        public int VoiceTypeID { get; set; }
        public int? PersonSupportID { get; set; }
        public int QuestionnaireID { get; set; }
        public int? HelperID { get; set; }
        public int AssessmentID { get; set; }
        public int UpdateUserID { get; set; }
        public string ReasoningText { get; set; }
        public DateTime? EventDate { get; set; }
        public string? EventNotes { get; set; }
    }
}
