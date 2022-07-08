// -----------------------------------------------------------------------
// <copyright file="PersonNeedsReportDTO.cs" company="Naico ITS">
// Copyright (c) Naico ITS. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;

namespace Opeeka.PICS.Domain.DTO
{
    public class PersonNeedsReportDTO
    {
        public ReportPeriodDateDTO ReportPeriodDate { get; set; }
        public PersonDetailsInReportDTO PersonDetails { get; set; }
        public QuestionnaireDetailsInReportDTO QuestionnaireDetails { get; set; }
        public NeedsReportSummaryDTO ReportSummary { get; set; }
        public NeedsReportDetailsDTO ReportDetails { get; set; }
    }

    public class NeedsReportDetailsDTO
    {
        public List<LatestNeedsPerItemDTO> GoalsReachedPerItem { get; set; }
        public List<LatestNeedsPerItemDTO> InProgressFocusPerItem { get; set; }
        public List<LatestNeedsPerItemDTO> BackgroundNeedsPerItem { get; set; }
        public List<ItemNoteDTO> ItemNotes { get; set; }
    }
    public class NeedsReportSummaryDTO
    {
        public int GoalsReached { get; set; }
        public int InProgress { get; set; }
    }
    public class LatestNeedsPerItemDTO : LatestProgressPerItemDTO
    {
        public bool IsNewFocus { get; set; }
        public AssessmentNeedsDetailsDTO NeedsInPreviousAssessment { get; set; }
        public AssessmentNeedsDetailsDTO NeedsInLatestAssessment { get; set; }
    }

    public class AssessmentNeedsDetailsDTO : AssessmentInReportDetailsDTO
    {
        public bool NeedForFocus { get; set; }
        public bool NeedInBackground { get; set; }
        public bool NeedNone { get; set; }
    }

    public class LatestNeedsDTO : LatestProgressDTO
    {
        public bool NeedForFocus { get; set; }
        public bool NeedInBackground { get; set; }
        public bool NeedNone { get; set; }
    }

}
