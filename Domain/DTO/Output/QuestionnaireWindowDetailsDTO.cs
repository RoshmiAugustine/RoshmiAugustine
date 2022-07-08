using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Domain.DTO.Output
{
    public class QuestionnaireWindowDetailsDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public List<QuestionnaireWindowsDTO> QuestionnaireWindow { get; set; }
    }
}
