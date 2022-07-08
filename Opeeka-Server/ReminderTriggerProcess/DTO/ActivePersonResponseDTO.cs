using System;
using System.Collections.Generic;
using System.Text;

namespace ReminderTriggerProcess.DTO
{
    public class ActivePersonResponse
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }
        public List<long> PersonIds { get; set; }

        public List<PersonCollaborationDTO> PersonCollaboration { get; set; }
    }
    public class ActivePersonResponseDTO
    {
        public string version { get; set; }
        public int statusCode { get; set; }
        public string message { get; set; }
        public ActivePersonResponse result { get; set; }
    }
}
