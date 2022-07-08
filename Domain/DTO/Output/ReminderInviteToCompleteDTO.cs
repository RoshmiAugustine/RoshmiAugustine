using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Domain.DTO.Output
{
    public class RemindersForInviteToCompleteResponseDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public List<RemindersToTriggerInviteToCompleteDTO> ReminderInviteDetails { get; set; }
    }

    public class InviteMailReceiversDetailsResponseDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public List<InviteReceiversInDetailDTO> IniviteToCompleteMailDetails { get; set; }
    }

}
