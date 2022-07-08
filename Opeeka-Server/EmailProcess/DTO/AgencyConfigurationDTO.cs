using System;
using System.Collections.Generic;
using System.Text;

namespace EmailProcess.DTO
{
    public class AgencyConfigurationDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public Dictionary<string, string> Configurations { get; set; }
    }

    public class AgencyConfigurationResponseDTO
    {
        public string version { get; set; }
        public int statusCode { get; set; }
        public string message { get; set; }
        public AgencyConfigurationDTO result { get; set; }
    }
}
