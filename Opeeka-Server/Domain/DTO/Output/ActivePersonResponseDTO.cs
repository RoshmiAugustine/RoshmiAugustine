using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Domain.DTO.Output
{
    public class ActivePersonResponseDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }
        public List<long> PersonIds { get; set; }
        public List<PersonCollaborationDTO> PersonCollaboration { get; set; }

    }
}
