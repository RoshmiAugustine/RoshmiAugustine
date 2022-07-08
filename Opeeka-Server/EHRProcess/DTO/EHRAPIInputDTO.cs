using System;
using System.Collections.Generic;
using System.Text;

namespace EHRProcess.DTO
{    
    public class EHRRootData
    {
        public string UniversalID { get; set; }
        public bool IsValid { get; set; }
        public DateTime LastModified { get; set; }
        public List<EHRAPIInputDTO> InputFields { get; set; }
    }
    public class EHRAPIInputDTO
    {
        public string FieldName { get; set; }
        public List<string> Values { get; set; }
        public string Type { get; set; }
    }
}
