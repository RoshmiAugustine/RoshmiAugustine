using System;
using System.Collections.Generic;
using System.Text;

namespace ImportProcess.DTO
{
    public class PeopleSupportDTO
    {
        public string SupportFirstName { get; set; }
        public string SupportMiddleName { get; set; }
        public string SupportLastName { get; set; }
        public string SupportPhoneCode { get; set; }
        public string SupportPhone { get; set; }
        public string SupportEmail { get; set; }
        public int RelationshipID { get; set; }
        public string Relationship { get; set; }
        public int PersonSupportID { get; set; }
        public bool IsCurrent { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Suffix { get; set; }
        public long PersonID { get; set; }
    }
}
