// -----------------------------------------------------------------------
// <copyright file="AssessmentReviewStatusDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Domain.DTO
{
    public class AssessmentReviewStatusDTO
    {
        public int AssessmentID { get; set; }
        public string AssessmentStatus { get; set; }
        public string ReviewNote { get; set; }
        public int ReviewUserID { get; set; }
    }
}
