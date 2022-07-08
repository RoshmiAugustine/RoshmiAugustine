using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Opeeka.PICS.Domain.DTO.Input
{
    public class ExportTemplateInputDTO
    {
        public int ExportTemplateID { get; set; }
        public ExportAssessmentDTO? AssessmentFilter { get; set; }
    }
    public class ExportAssessmentDTO
    {
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid questionnaireID.")]
        public int QuestionnaireID { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
