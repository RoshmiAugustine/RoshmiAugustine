using System;
using System.Collections.Generic;
using System.Text;

namespace EHRProcess.DTO
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

    }
}
