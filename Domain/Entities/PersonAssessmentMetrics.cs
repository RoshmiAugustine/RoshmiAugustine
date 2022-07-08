using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Domain.Entities
{
    public class PersonAssessmentMetrics : BaseEntity
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

        public Assessment Assessment { get; set; }
    }
}
