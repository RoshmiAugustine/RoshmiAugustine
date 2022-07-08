
using AlertAutoResolveProcess.DTO;

namespace AlertAutoResolveProcess.DTO
{
    public class PersonQuestionnaireDetails
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public PersonQuestionnaireDTO PersonQuestionnaire { get; set; }
    }

    public class PersonQuestionnaireDetailsDTo
    {
        public string version { get; set; }
        public int statusCode { get; set; }
        public string message { get; set; }
        public PersonQuestionnaireDetails result { get; set; }
    }
}
