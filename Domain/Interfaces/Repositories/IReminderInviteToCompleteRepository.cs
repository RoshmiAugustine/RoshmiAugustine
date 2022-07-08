using System;
using System.Collections.Generic;
using System.Text;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    public interface IReminderInviteToCompleteRepository
    {

        /// <summary>
        /// GetInviteToCompleteReminderDetails.
        /// </summary>
        /// <returns></returns>
        List<ReminderInviteToCompleteDTO> GetReminderInviteDetails();
        /// <summary>
        /// UpdateReminderInviteDetails.
        /// </summary>
        /// <param name="inviteDetails"></param>
        void UpdateReminderInviteDetails(List<ReminderInviteToComplete> inviteDetails);

        /// <summary>
        /// AddReminderInviteDetails.
        /// </summary>
        /// <param name="inviteDetails"></param>
        void AddReminderInviteDetails(List<ReminderInviteToComplete> inviteDetails);
    }
}
