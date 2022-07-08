// -----------------------------------------------------------------------
// <copyright file="AssessmentsDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;

namespace Opeeka.PICS.Domain.DTO
{
    public class AssessmentsDTO
    {
        public int AssessmentID { get; set; }
        public string Time { get; set; }
        public DateTime DateTaken { get; set; }
        public string AssessmentReason { get; set; }
        public int AssessmentReasonID { get; set; }
        public string AssessmentStatus { get; set; }
        public int AssessmentStatusID { get; set; }
        public int DaysInEpisode { get; set; }
    }
}
