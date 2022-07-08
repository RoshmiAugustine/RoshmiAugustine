// -----------------------------------------------------------------------
// <copyright file="SupportResourceReportDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Opeeka.PICS.Domain.DTO
{
    public class SupportResourceReportDTO
    {
        public ReportPeriodDateDTO ReportPeriodDate { get; set; }
        public PersonDetailsInReportDTO PersonDetails { get; set; }
        public QuestionnaireDetailsInReportDTO QuestionnaireDetails { get; set; }
        public List<SupportResourceDTO> SupportDetails { get; set; }
    }

    public class SupportResourceDTO : SupportDetailsInReportDTO
    {
        public SupportResourceReportSummaryDTO ReportSummary { get; set; }
        public ResourceReportDetailsDTO ReportDetails { get; set; }
    }

    public class ResourceReportDetailsDTO
    {
        public List<SupportResourceToUsePerAssessmentDTO> ResourceToUsePerAssessmentforGraph { get; set; }
        public List<LatestSupportResourcePerItemDTO> LatestProgressPerItem { get; set; }
        public List<ItemNoteDTO> ItemNotes { get; set; }
    }

    public class LatestSupportResourcePerItemDTO : LatestProgressPerItemDTO
    {
        public AssessmentSupportResourceDetailsDTO ResourceInPreviousAssessment { get; set; }
        public AssessmentSupportResourceDetailsDTO ResourceInLatestAssessment { get; set; }
    }

    public class AssessmentSupportResourceDetailsDTO : AssessmentInReportDetailsDTO
    {
        public bool ResourceToBuild { get; set; }
        public bool ResourceToUse { get; set; }
    }

    public class SupportResourceToUsePerAssessmentDTO
    {
        public int AssessmentID { get; set; }
        public DateTime AssessmentDate { get; set; }
        public int DaysInEpisode { get; set; }
        public string TimePeriod { get; set; }
        public int ResourceToUse { get; set; }
    }

    public class SupportResourceReportSummaryDTO
    {
        public int ResourceToBuild { get; set; }
        public int ResourceToUse { get; set; }
    }

    public class LatestSupportResourceDTO : LatestProgressDTO
    {
        public bool ResourceToBuild { get; set; }
        public bool ResourceToUse { get; set; }
    }
}