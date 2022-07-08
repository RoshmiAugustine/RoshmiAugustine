using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Domain.DTO.Output
{
    public class PersonHelperEmailDetailDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public List<PeopleHelperEmailDTO> PersonHelper { get; set; }
    }
}
