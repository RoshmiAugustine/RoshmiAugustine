using System;
using System.Collections.Generic;
using System.Text;

namespace ImportProcess.DTO
{
    public class PeopleCollaborationDTO
    {
        public int CollaborationID { get; set; }
        public string CollaborationName { get; set; }
        public DateTime CollaborationStartDate { get; set; }
        public DateTime? CollaborationEndDate { get; set; }
        public long PersonCollaborationID { get; set; }
        public bool IsPrimary { get; set; }
        public bool IsCurrent { get; set; }
        public int WindowOpenOffsetDays { get; set; }
        public int WindowCloseOffsetDays { get; set; }
        public DateTime EnrollDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
