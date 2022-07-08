using System;
using System.Collections.Generic;
using System.Text;

namespace EmailProcess.DTO
{
    public class GetEmailDetailsDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public List<EmailDetailsDTO> EmailDetails { get; set; }
        public List<ReminderInviteToCompleteDTO> reminderInviteDetails { get; set; }
    }
    public class GetEmailDetailsResponseDTO
    {
        public string version { get; set; }
        public int statusCode { get; set; }
        public string message { get; set; }
        public GetEmailDetailsDTO result { get; set; }
    }
}