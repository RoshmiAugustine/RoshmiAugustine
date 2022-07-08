using System;
using System.Collections.Generic;
using System.Text;

namespace ImportProcess
{
    public class GenderDTO
    {

        public int identifiedGenderID { get; set; }
        public string Name { get; set; }
        public string Abbrev { get; set; }
        public string Description { get; set; }
        public int ListOrder { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UpdateUserID { get; set; }
        public long AgencyID { get; set; }


    }
}
