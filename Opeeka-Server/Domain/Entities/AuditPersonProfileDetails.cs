using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Domain.Entities
{
    public class AuditPersonProfile : BaseEntity
    {
        public int AuditPersonProfileID { get; set; }
        public string TypeName { get; set; }
        public long ParentID { get; set; }
        public int ChildRecordID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UpdateUserID { get; set; }
        //public bool IsCurrent { get; set; }

        public User UpdateUser { get; set; }
    }
}
