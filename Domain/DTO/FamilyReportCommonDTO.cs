// -----------------------------------------------------------------------
// <copyright file="FamilyReportCommonDTO.cs.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------


using System;

namespace Opeeka.PICS.Domain.DTO
{
    public class ReportPeriodDateDTO
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class QuestionnaireDetailsInReportDTO
    {
        public int InstrumentID { get; set; }
        public string InstrumentAbbrevation { get; set; }
        public string InstrumentName { get; set; }
        public int QuestionnaireID { get; set; }
        public string QuestionnaireAbbrevation { get; set; }
        public string QuestionnaireName { get; set; }
    }

    public class PersonDetailsInReportDTO
    {
        public Guid PersonIndex { get; set; }
        public long PersonID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
    }
    public class ItemNoteDTO
    {
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public int AssessmentID { get; set; }
        public DateTime AssessmentDate { get; set; }
        public string TimePeriod { get; set; }
        public int NoteID { get; set; }
        public DateTime NoteDate { get; set; }
        public string Note { get; set; }
        public string Author { get; set; }
        public string VoiceTypeFKName { get; set; }
        public string Title { get; set; }
    }
    public class LatestProgressDTO
    {
        public int ItemID { get; set; }
        public string ItemLabel { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public int AssessmentID { get; set; }
        public DateTime AssessmentDate { get; set; }
        public int ResponseID { get; set; }
        public decimal ResponseScore { get; set; }
        public int PersonCareGiverID { get; set; }
    }
    public class AssessmentInReportDetailsDTO
    {
        public int AssessmentID { get; set; }
        public DateTime? AssessmentDate { get; set; }
        public int ResponseID { get; set; }
        public decimal ResponseScore { get; set; }
    }
    public class LatestProgressPerItemDTO
    {
        public int ItemID { get; set; }
        public string ItemLabel { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public int PersonCareGiverID { get; set; }
    }
    public class SupportDetailsInReportDTO
    {
        public int PersonSupportID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Relation { get; set; }
    }
}
