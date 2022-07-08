using System;
using System.Collections.Generic;
using System.Text;

namespace ReminderTriggerProcess.DTO
{
    public class NotifyReminderDetails
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public List<NotifyReminderDTO> NotifyReminders { get; set; }
    }
    public class NotifyReminderDetailsDTO
    {
        public string version { get; set; }
        public int statusCode { get; set; }
        public string message { get; set; }
        public NotifyReminderDetails result { get; set; }
    }
}
