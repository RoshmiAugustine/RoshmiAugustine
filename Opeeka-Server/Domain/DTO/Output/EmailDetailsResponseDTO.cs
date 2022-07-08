using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Domain.DTO.Output
{
    public class EmailDetailsResponseDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public List<EmailDetailsDTO> EmailDetails { get; set; }
    }
    public class ReminderInviteToCompleteResponseDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public List<ReminderInviteToCompleteDTO> reminderInviteDetails { get; set; }
    }
}
