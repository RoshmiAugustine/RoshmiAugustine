using System;
using System.Collections.Generic;
using System.Text;

namespace ImportProcess.DTO
{
    public class PersonCollaborationDTO
    {
        public long PersonCollaborationID { get; set; }
        public long PersonID { get; set; }

       // [Range(1, int.MaxValue, ErrorMessage = "Please enter a Collaboration ID")]
        public int CollaborationID { get; set; }

       // [Required]
        public DateTime EnrollDate { get; set; }

        public DateTime? EndDate { get; set; }

       // [Required]
        public bool IsPrimary { get; set; }

       // [Required]
        public bool IsCurrent { get; set; }


        public bool IsRemoved { get; set; }

      //  [Range(1, int.MaxValue, ErrorMessage = "Please enterUpdateUserID")]
        public int UpdateUserID { get; set; }

       // [Required]
        public DateTime UpdateDate { get; set; }
        public Guid PersonIndexGuid { get; set; }
    }
}
