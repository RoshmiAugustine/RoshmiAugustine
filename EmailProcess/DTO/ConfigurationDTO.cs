using System;
using System.Collections.Generic;
using System.Text;

namespace EmailProcess.DTO
{
    public class ConfigurationDTO 
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public string ConfigurationValue { get; set; }
    }

    public class ConfigurationResponseDTO
    {
        public string version { get; set; }
        public int statusCode { get; set; }
        public string message { get; set; }
        public ConfigurationDTO result { get; set; }
    }
}
