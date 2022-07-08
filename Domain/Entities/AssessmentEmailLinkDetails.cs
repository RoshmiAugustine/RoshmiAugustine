// -----------------------------------------------------------------------
// <copyright file="AssessmentEmailLink.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;

namespace Opeeka.PICS.Domain.Entities
{
    public class AssessmentEmailLinkDetails : BaseEntity
    {
        public int AssessmentEmailLinkDetailsID { get; set; }
        public Guid EmailLinkDetailsIndex { get; set; }
        public Guid PersonIndex { get; set; }
        public int AssessmentID { get; set; }
        public int QuestionnaireID { get; set; }
        public int? PersonSupportID { get; set; }
        public int? HelperID { get; set; }
        public string PersonOrSupportEmail { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UpdateUserID { get; set; }
        public int VoiceTypeID { get; set; }
        public string? PhoneNumber { get; set; }
        public Guid? AssessmentEmailLinkGUID { get; set; }

        public Assessment Assessment { get; set; }
        public Questionnaire Questionnaire { get; set; }
        public PersonSupport PersonSupport { get; set; }
        public Helper Helper { get; set; }
        public VoiceType VoiceType { get; set; }
    }
}
