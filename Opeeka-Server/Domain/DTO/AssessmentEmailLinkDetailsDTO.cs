// -----------------------------------------------------------------------
// <copyright file="AssessmentEmailLinkDetailsDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;

namespace Opeeka.PICS.Domain.DTO
{
    public class AssessmentEmailLinkDetailsDTO
    {
        public Guid PersonIndex { get; set; }
        public int? PersonSupportID { get; set; }
        public string PersonORSupportEmail { get; set; }
        public string PersonSupportName { get; set; }
        public int AssessmentID { get; set; }
        public int QuestionnaireID { get; set; }
        public int? HelperID { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UpdateUserID { get; set; }
        public int VoiceTypeID { get; set; }
        public string? PhoneNumber { get; set; }
        public Guid? EmailLinkDetailsIndex { get; set; }
        public Guid? AssessmentGUID { get; set; }
        public Guid? AssessmentEmailLinkGUID { get; set; }
    }
}
