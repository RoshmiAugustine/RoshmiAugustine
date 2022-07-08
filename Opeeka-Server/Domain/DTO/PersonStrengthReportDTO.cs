// -----------------------------------------------------------------------
// <copyright file="PersonStrengthReportDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Opeeka.PICS.Domain.DTO
{
    public class PersonStrengthReportDTO
    {
        public ReportPeriodDateDTO ReportPeriodDate { get; set; }
        public PersonDetailsInReportDTO PersonDetails { get; set; }
        public QuestionnaireDetailsInReportDTO QuestionnaireDetails { get; set; }
        public StrengthReportSummaryDTO ReportSummary { get; set; }
        public StrengthReportDetailsDTO ReportDetails { get; set; }
    }

    public class StrengthReportDetailsDTO
    {
        public List<StrengthToUsePerAssessmentDTO> StrengthToUsePerAssessmentforGraph { get; set; }
        public List<LatestStrengthPerItemDTO> LatestProgressPerItem { get; set; }
        public List<ItemNoteDTO> ItemNotes { get; set; }
    }

    public class LatestStrengthPerItemDTO : LatestProgressPerItemDTO
    {
        public AssessmentStrengthDetailsDTO StrengthInPreviousAssessment { get; set; }
        public AssessmentStrengthDetailsDTO StrengthInLatestAssessment { get; set; }
    }

    public class AssessmentStrengthDetailsDTO : AssessmentInReportDetailsDTO
    {
        public bool StrengthToBuild { get; set; }
        public bool StrengthToUse { get; set; }
    }

    public class StrengthToUsePerAssessmentDTO
    {
        public int AssessmentID { get; set; }
        public DateTime AssessmentDate { get; set; }
        public int DaysInEpisode { get; set; }
        public string TimePeriod { get; set; }
        public int StrengthsToUse { get; set; }
    }

    public class StrengthReportSummaryDTO
    {
        public int StrengthsToBuild { get; set; }
        public int StrengthsToUse { get; set; }
    }

    public class LatestStrengthDTO : LatestProgressDTO
    {
        public bool StrengthToBuild { get; set; }
        public bool StrengthToUse { get; set; }
    }
}

