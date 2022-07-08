using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;

namespace ImportProcess.DTO
{
    public class EmailListDTO
    {
        public List<JObject> helperEmailCSV { get; set; }
        public string agencyID { get; set; }
    }
    public class ImportParameterDTO
    {
        public string JsonData { get; set; }
        public long agencyID { get; set; }
    }

}
