

namespace ReminderNotificationProcess.DTO
{
    public class PersonCollaborationDetails
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public PersonCollaborationDTO PersonCollaboration { get; set; }
    }
    public class PersonCollaborationDetailsDTO
    {
        public string version { get; set; }
        public int statusCode { get; set; }
        public string message { get; set; }
        public PersonCollaborationDetails result { get; set; }
    }
}
