using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Opeeka.PICS.Domain.DTO
{
    public class PeopleHelperEmailDTO
    {
        public long PersonID { get; set; }
        public Guid PersonIndex { get; set; }
        public string PersonFirstName { get; set; }
        public string PersonMiddleName { get; set; }
        public string PersonLastName { get; set; }
        public int HelperID { get; set; }
        public Guid HelperIndex { get; set; }
        public string HelperFirstName { get; set; }
        public string HelperMiddleName { get; set; }
        public string HelperLastName { get; set; }
        public string HelperEmail { get; set; }
        public long AgencyID { get; set; }
        public string PersonInitials
        {
            get
            {
                var personInitials = string.IsNullOrWhiteSpace(PersonMiddleName) ? string.Format("{0}{1}", PersonFirstName.ToUpper().First(), PersonLastName.ToUpper().First()) : string.Format("{0}{1}{2}", PersonFirstName.ToUpper().First(), PersonMiddleName.ToUpper().First(), PersonLastName.ToUpper().First());
                return personInitials;
            }
        }
    }
}
