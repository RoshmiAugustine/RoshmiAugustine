// -----------------------------------------------------------------------
// <copyright file="AssessmentResponsesDTO.cs" company="Naico ITS">
// Copyright (c) Naico ITS. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace AlertAutoResolveProcess.DTO
{
    public class AssessmentResponsesDTO
    {
        public int AssessmentResponseID { get; set; }
        public int AssessmentID { get; set; }
        public int? PersonSupportID { get; set; }
        public int ResponseID { get; set; }
        public int? ItemResponseBehaviorID { get; set; }
        public bool IsRequiredConfidential { get; set; }
        public bool? IsPersonRequestedConfidential { get; set; }
        public bool? IsOtherConfidential { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UpdateUserID { get; set; }
        public int QuestionnaireItemID { get; set; }
        public bool IsCloned { get; set; }
        public string CaregiverCategory { get; set; }
        public int? Priority { get; set; }
    }
}
