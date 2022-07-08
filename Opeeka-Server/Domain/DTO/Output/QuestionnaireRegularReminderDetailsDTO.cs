using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Domain.DTO.Output
{
    public class QuestionnaireRegularReminderSettingsDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public List<QuestionnaireRegularReminderRecurrenceDTO> Recurrence { get; set; }

        public List<QuestionnaireRegularReminderTimeRuleDTO> TimeRule { get; set; }
    }
}
