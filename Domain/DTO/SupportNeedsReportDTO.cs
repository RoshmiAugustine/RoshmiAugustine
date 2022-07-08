// -----------------------------------------------------------------------
// <copyright file="SupportNeedsReportDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System.Collections.Generic;

namespace Opeeka.PICS.Domain.DTO
{
    public class SupportNeedsReportDTO
    {
        public ReportPeriodDateDTO ReportPeriodDate { get; set; }
        public PersonDetailsInReportDTO PersonDetails { get; set; }
        public List<SupportNeedsDTO> SupportDetails { get; set; }
        public QuestionnaireDetailsInReportDTO QuestionnaireDetails { get; set; }
    }

    public class SupportNeedsDTO : SupportDetailsInReportDTO
    {
        public NeedsReportSummaryDTO ReportSummary { get; set; }
        public NeedsReportDetailsDTO ReportDetails { get; set; }
    }

    public class SupportNeedsReportDetailsDTO
    {
        public List<LatestNeedsPerItemDTO> GoalsReachedPerItem { get; set; }
        public List<LatestNeedsPerItemDTO> InProgressFocusPerItem { get; set; }
        public List<LatestNeedsPerItemDTO> BackgroundNeedsPerItem { get; set; }
        public List<ItemNoteDTO> ItemNotes { get; set; }
    }
}

