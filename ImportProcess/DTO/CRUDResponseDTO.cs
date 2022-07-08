using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ImportProcess.DTO
{
    public class CRUDResponseDTO
    {
        public string version { get; set; }
        public int statusCode { get; set; }
        public string message { get; set; }
        public CRUDResponse result { get; set; }

    }
    public class CRUDResponse
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public bool isVaildEmails { get; set; }

        public List<dynamic> importFileList { get; set; }

        public List<HelperInfoDTO> helperList { get; set; }

        public List<CollaborationInfoDTO> collaborationList { get; set; }

        public List<RaceEthnicityDTO> raceEthnicities { get; set; }

        public List<IdentificationTypeDTO> identificationTypes { get; set; }

        public List<GenderDTO> identifiedGender { get; set; }

        public List<RoleLookupDTO> roleLookup { get; set; }

        public List<UsersDTO> existingEmails { get; set; }
        public List<CountryCodeDTO> countries { get; set; }

    }

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
