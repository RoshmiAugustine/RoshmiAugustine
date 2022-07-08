using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Domain.DTO.Output
{
    public class ConfigurationResponseDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public string ConfigurationValue { get; set; }
    }
}
