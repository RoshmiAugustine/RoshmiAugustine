
using System;
using System.ComponentModel.DataAnnotations;

namespace Opeeka.PICS.Domain.DTO.Input
{
    public class AssesmentValuesInptDTO
    {

        [Required]
        public Guid personIndex { get; set; }

        [Required]
        [Range(1, Int32.MaxValue, ErrorMessage = "Please enter a valid Questionnaire")]
        public int questionnaireId { get; set; }

      
        public string? AssessmentIDs { get; set; }
    }
    
}
