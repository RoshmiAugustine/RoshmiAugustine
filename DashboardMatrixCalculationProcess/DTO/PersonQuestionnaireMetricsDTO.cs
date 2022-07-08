// -----------------------------------------------------------------------
// <copyright file="PersonQuestionnaireMetrics.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace DashboardMatrixCalculationProcess.DTO
{
    public class PersonQuestionnaireMetricsDTO
    {
        public long PersonQuestionnaireMetricsID { get; set; }
        public long PersonID { get; set; }
        public int InstrumentID { get; set; }
        public int PersonQuestionnaireID { get; set; }
        public int ItemID { get; set; }
        public int NeedsEver { get; set; }
        public int NeedsIdentified { get; set; }
        public int NeedsAddressed { get; set; }
        public int NeedsAddressing { get; set; }
        public int NeedsImproved { get; set; }
        public int StrengthsEver { get; set; }
        public int StrengthsIdentified { get; set; }
        public int StrengthsBuilt { get; set; }
        public int StrengthsBuilding { get; set; }
        public int StrengthsImproved { get; set; }
    }
    public class PersonAssessmentMetricsDTO
    {
        public long PersonAssessmentMetricsID { get; set; }
        public long PersonID { get; set; }
        public int InstrumentID { get; set; }
        public int PersonQuestionnaireID { get; set; }
        public int ItemID { get; set; }
        public int NeedsEver { get; set; }
        public int NeedsIdentified { get; set; }
        public int NeedsAddressed { get; set; }
        public int NeedsAddressing { get; set; }
        public int NeedsImproved { get; set; }
        public int StrengthsEver { get; set; }
        public int StrengthsIdentified { get; set; }
        public int StrengthsBuilt { get; set; }
        public int StrengthsBuilding { get; set; }
        public int StrengthsImproved { get; set; }
        public int AssessmentID { get; set; }
        public DateTime UpdateDate { get; set; }
        public int? QuestionnaireID { get; set; }
    }
}
