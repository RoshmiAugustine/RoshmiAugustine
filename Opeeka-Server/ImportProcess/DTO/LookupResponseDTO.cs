using System;
using System.Collections.Generic;
using System.Text;

namespace ImportProcess.DTO
{
    public class LookupResponseDTO
    {
        public string version { get; set; }
        public int statusCode { get; set; }
        public string message { get; set; }
        public LookupDTO result { get; set; }
    }

    public class LookupDTO
    {
        public string ResponseStatus { get; set; }
        public int ResponseStatusCode { get; set; }
        public Dictionary<string, List<Lookup>> Lookups { get; set; }
        public List<QuestionnaireItemsForImportDTO> QuestionItemDetails { get;set;}
        public Dictionary<string, PersonVoiceTypeDetailsForImportDTO> PersonVoiceTypeDetails { get; set; }
    }

    public class Lookup
    {
        public string Id;
        public string Value;
    }

    public class PersonVoiceTypeDetailsForImportDTO
    {
        public long PersonID { get; set; }
        public List<Lookup> PersonSupportlookups { get; set; }
        public List<Lookup> PersonHelperlookups { get; set; }
    }
    public class PersonIndexToUploadDTO
    {
        public long AgencyID { get; set; }
        public List<string> PersonIndexesToUpload { get; set; }
    }
}
