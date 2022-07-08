using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Domain.DTO
{
    public class ImportParameterDTO
    {
        public string JsonData { get; set; }
        public long agencyID { get; set; }
    }
    public class PersonVoiceTypeDetailsForImportDTO
    {
        public long PersonID { get; set; }
        public List<Lookup> PersonSupportlookups { get; set; }
        public List<Lookup> PersonHelperlookups { get; set; }
    }
    public class Lookup
    {
        public string Id { get; set; }
        public string Value { get; set; }
    }

    public class PersonHelperDetailsDTO
    {
        public Guid HelperIndex { get; set; }
        public long PersonHelperID { get; set; }
        public int HelperID { get; set; }
        public string Email { get; set; }
    }
    
}
