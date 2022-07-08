using System;
using System.Collections.Generic;
using System.Text;

namespace ImportProcess.DTO
{
    public class PersonHelperDTO
    {
        public long PersonHelperID { get; set; }
        public long PersonID { get; set; }

        //[Range(1, long.MaxValue, ErrorMessage = "Please enter a Helper ID")]
        public int HelperID { get; set; }
        //[Required]
        public bool IsLead { get; set; }
       // [Required]
        public bool IsCurrent { get; set; }
       // [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
        public bool IsRemoved { get; set; }
       // [Range(1, int.MaxValue, ErrorMessage = "Please enter a UpdateUserID ")]
        public int UpdateUserID { get; set; }
       // [Required]
        public DateTime UpdateDate { get; set; }
        public Guid PersonIndexGuid { get; set; }
    }
}
