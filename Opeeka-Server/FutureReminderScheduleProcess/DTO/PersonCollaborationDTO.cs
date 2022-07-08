using System;
using System.Collections.Generic;
using System.Text;

namespace FutureReminderScheduleProcess.DTO
{

    public class PersonCollaborationDetailsDTO
    {
        public string version { get; set; }
        public int statusCode { get; set; }
        public string message { get; set; }
        public PersonCollaborationResponseDTO result { get; set; }
    }
    public class PersonCollaborationResponseDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }
        public List<PersonCollaborationDTO> personCollaborations { get; set; }

    }
    public class PersonCollaborationDTO
    {
        public long PersonCollaborationID { get; set; }
        public long PersonQuestionnaireID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
