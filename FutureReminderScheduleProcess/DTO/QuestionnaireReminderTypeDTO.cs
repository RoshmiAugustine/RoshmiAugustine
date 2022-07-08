
using System;

namespace FutureReminderScheduleProcess.DTO
{
    public class QuestionnaireReminderTypeDTO
    {
        public int QuestionnaireReminderTypeID { get; set; }
        public string Name { get; set; }
        public string? Abbrev { get; set; }
        public string? Description { get; set; }
        public int ListOrder { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UpdateUserID { get; set; }
        public int NotificationLevelID { get; set; }

    }
}
