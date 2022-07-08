using System;
using System.Collections.Generic;
using System.Text;

namespace FutureReminderScheduleProcess.DTO
{
    public class PersonQuestionnaireRegularScheduleDetails
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public List<PersonQuestionnaireRegularScheduleDTO> personQuestionnaireSchedules { get; set; }
        public List<long> list_PersonQuestionnaireIds { get; set; }
        public List<int> list_QuestionnaireIds { get; set; }
    }

    public class PersonQuestionnairesRegularScheduleDetailsDTO
    {
        public string version { get; set; }
        public int statusCode { get; set; }
        public string message { get; set; }
        public PersonQuestionnaireRegularScheduleDetails result { get; set; }
    }
}
