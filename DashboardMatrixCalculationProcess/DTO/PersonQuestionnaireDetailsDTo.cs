
using System.Collections.Generic;

namespace DashboardMatrixCalculationProcess.DTO
{
    public class PersonQuestionnaireDetails
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public List<PersonQuestionnaireDTO> PersonQuestionnaire { get; set; }
    }

    public class PersonQuestionnaireDetailsDTo
    {
        public string version { get; set; }
        public int statusCode { get; set; }
        public string message { get; set; }
        public PersonQuestionnaireDetails result { get; set; }
    }
}
