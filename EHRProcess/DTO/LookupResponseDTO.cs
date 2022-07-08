using System;
using System.Collections.Generic;
using System.Text;

namespace EHRProcess.DTO
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
    }

    public class Lookup
    {
        public string Id;
        public string Value;
    }
}
