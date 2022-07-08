using System;
using System.Collections.Generic;
using System.Text;

namespace ReminderTriggerProcess.DTO
{
    public class QuestionnaireDTO
    {
        public int QuestionnaireID { get; set; }
        public string Name { get; set; }
        public int InstrumentID { get; set; }
        public long? AgencyID { get; set; }
        public string Description { get; set; }
        public string Abbrev { get; set; }
        public string ReminderScheduleName { get; set; }
        public bool IsEmailRemindersHelpers { get; set; }
        public bool IsAlertsHelpersManagers { get; set; }
        public bool IsEmailInviteToCompleteReminders { get; set; }
    }

    public class QuestionnaireResponseDTO
    {
        public string version { get; set; }
        public int statusCode { get; set; }
        public string message { get; set; }
        public QuestionnairesResponseDTO result { get; set; }
    }

    public class QuestionnairesResponseDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public List<QuestionnaireDTO> QuestionnaireList { get; set; }
    }
}
