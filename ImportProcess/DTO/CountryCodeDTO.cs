using System;
using System.Collections.Generic;
using System.Text;

namespace ImportProcess.DTO
{
    public class CountryCodeDTO
    {
        public int CountryID { get; set; }
        public string Name { get; set; }
        public string Abbrev { get; set; }
        public string Description { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UpdateUserID { get; set; }
        public string CountryCode { get; set; }
    }
}
