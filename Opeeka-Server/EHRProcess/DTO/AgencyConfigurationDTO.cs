using System;
using System.Collections.Generic;
using System.Text;

namespace EHRProcess.DTO
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
    public class InstrumentConfiguration
    {
        public string TypeName { get; set; }
        public string TypeID { get; set; }
        public string URLBodyParam { get; set; }
        public string URLToPost { get; set; }
        public string URLToPostComplete { get; set; }
    }
}
