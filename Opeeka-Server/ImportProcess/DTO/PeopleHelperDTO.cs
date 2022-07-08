using System;
using System.Collections.Generic;
using System.Text;

namespace ImportProcess.DTO
{
    public class PeopleHelperDTO
    {
        public int HelperID { get; set; }
        public string HelperName { get; set; }
        public DateTime HelperStartDate { get; set; }
        public DateTime? HelperEndDate { get; set; }
        public long PersonHelperID { get; set; }
        public bool IsCurrent { get; set; }
        public bool IsLead { get; set; }
    }
}
