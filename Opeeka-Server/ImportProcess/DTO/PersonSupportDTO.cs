using System;
using System.Collections.Generic;
using System.Text;

namespace ImportProcess.DTO
{
    public class PersonSupportDTO
    {
        public int PersonSupportID { get; set; }
        public long PersonID { get; set; }

        public int SupportTypeID { get; set; }

        public bool IsCurrent { get; set; }


        public string FirstName { get; set; }

        public string MiddleName { get; set; }


        public string LastName { get; set; }

        public string Suffix { get; set; }
        public string Email { get; set; }

        public string PhoneCode { get; set; }

        public string Phone { get; set; }

        public string Note { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool IsRemoved { get; set; }

        public int UpdateUserID { get; set; }

        public DateTime UpdateDate { get; set; }
        public string UniversalID { get; set; }

    }
}
