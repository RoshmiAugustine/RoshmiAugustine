using System;
using System.Collections.Generic;
using System.Text;

namespace AlertNotficationProcess.DTO
{
    public class PersonHelperEmailDetail
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public List<PeopleHelperEmailDTO> PersonHelper { get; set; }
    }
    public class PersonHelperEmailDetailDTO
    {
        public string version { get; set; }
        public int statusCode { get; set; }
        public string message { get; set; }
        public PersonHelperEmailDetail result { get; set; }
    }
}
