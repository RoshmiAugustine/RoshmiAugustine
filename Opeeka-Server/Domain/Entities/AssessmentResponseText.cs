// -----------------------------------------------------------------------
// <copyright file="AssessmentResponseText.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Domain.Entities
{
    public class AssessmentResponseText : BaseEntity
    {
        public int AssessmentResponseTextID { get; set; }
        public string ResponseText { get; set; }
        public int AssessmentResponseID { get; set; }

        public AssessmentResponse AssessmentResponse { get; set; }
    }
}
