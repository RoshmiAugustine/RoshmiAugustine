using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Domain.DTO.Output
{
    public class PersonQuestionnaireListDetailsDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public List<PersonQuestionnaireDTO> PersonQuestionnaire { get; set; }
    }
}
