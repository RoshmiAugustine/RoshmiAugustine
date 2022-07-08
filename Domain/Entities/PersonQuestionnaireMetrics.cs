// -----------------------------------------------------------------------
// <copyright file="PersonQuestionnaireMetrics.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Domain.Entities
{
    public class PersonQuestionnaireMetrics : BaseEntity
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
}
