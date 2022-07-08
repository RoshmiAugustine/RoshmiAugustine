using System;
using System.Collections.Generic;
using System.Text;

namespace ReminderNotificationProcess.DTO
{
    public class PersonQuestionnaireScheduleDetails
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public List<PersonQuestionnaireScheduleDTO> PersonQuestionnaireSchedules { get; set; }
    }

    public class PersonQuestionnaireScheduleDetailsDTO
    {
        public string version { get; set; }
        public int statusCode { get; set; }
        public string message { get; set; }
        public PersonQuestionnaireScheduleDetails result { get; set; }
    }
}
