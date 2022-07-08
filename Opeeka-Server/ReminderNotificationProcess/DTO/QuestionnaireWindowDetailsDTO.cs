using System;
using System.Collections.Generic;
using System.Text;

namespace ReminderNotificationProcess.DTO
{
    public class QuestionnaireWindowDetails
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public List<QuestionnaireWindowsDTO> QuestionnaireWindow { get; set; }
    }
    public class QuestionnaireWindowDetailsDTO
    {
        public string version { get; set; }
        public int statusCode { get; set; }
        public string message { get; set; }
        public QuestionnaireWindowDetails result { get; set; }
    }
}
