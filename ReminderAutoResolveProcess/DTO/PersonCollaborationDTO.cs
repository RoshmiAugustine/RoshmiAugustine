

using System;

namespace ReminderAutoResolveProcess.DTO
{
    public class PersonCollaborationDTO
    {
        public long PersonCollaborationID { get; set; }
        public long PersonID { get; set; }

        public int CollaborationID { get; set; }
        public DateTime? EnrollDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool IsPrimary { get; set; }

        public bool IsCurrent { get; set; }


        public bool IsRemoved { get; set; }

        public int UpdateUserID { get; set; }

        public DateTime UpdateDate { get; set; }
    }
}
