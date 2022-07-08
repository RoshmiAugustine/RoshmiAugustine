
using System.Collections.Generic;

namespace FutureReminderScheduleProcess.DTO
{
    public class QuestionnaireReminderTypeResponse
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public List<QuestionnaireReminderTypeDTO> QuestionnaireReminderType { get; set; }
    }
    public class QuestionnaireReminderTypeResponseDTO
    {
        public string version { get; set; }
        public int statusCode { get; set; }
        public string message { get; set; }
        public QuestionnaireReminderTypeResponse result { get; set; }
    }
}
