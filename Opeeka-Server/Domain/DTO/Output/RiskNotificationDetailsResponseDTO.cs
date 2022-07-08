// -----------------------------------------------------------------------
// <copyright file="RiskNotificationDetailsResponseDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System.Collections.Generic;

namespace Opeeka.PICS.Domain.DTO.Output
{
    public class RiskNotificationDetailsResponseDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public List<RiskNotificationsListDTO> RiskNotifications { get; set; }

        public List<ReminderNotificationsListDTO> ReminderNotifications { get; set; }
    }
}
