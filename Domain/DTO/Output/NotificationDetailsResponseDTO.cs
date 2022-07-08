// -----------------------------------------------------------------------
// <copyright file="NotificationDetailsResponseDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Domain.DTO.Output
{
    public class NotificationDetailsResponseDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public QuestionnaireNotifyRiskScheduleDTO NotificationDetails { get; set; }
    }
}
