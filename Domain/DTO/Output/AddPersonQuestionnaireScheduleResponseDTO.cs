using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Domain.DTO.Output
{
    public class AddPersonQuestionnaireScheduleResponseDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }
        public long PersonQuestionnaireScheduleID { get; set; }
    }
    public class AddBulkPersonQuestionnaireScheduleResponseDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }
        public string PersonQuestionnaireSchedule { get; set; }
    }
}
