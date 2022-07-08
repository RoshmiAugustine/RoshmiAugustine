using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Domain.Entities
{
    public class EmailDetail : BaseEntity
    {
        public long EmailDetailID { get; set; }
        public long? PersonID { get; set; }
        public int? HelperID { get; set; }
        public long? AgencyID { get; set; }
        public string Email { get; set; }
        public string EmailAttributes { get; set; }
        public string Status { get; set; }
        public int? UpdateUserID { get; set; }
        public DateTime UpdateDate { get; set; }
        public string URL { get; set; }
        public string Type { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? FKeyValue { get; set; }

        public Agency Agency { get; set; }
        public Helper Helper { get; set; }
        public Person Person { get; set; }
    }
}
