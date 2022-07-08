// -----------------------------------------------------------------------
// <copyright file="NotificationLogResponseDTO.cs" company="Naico ITS">
// Copyright (c) Naico ITS. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;

namespace AlertAutoResolveProcess.DTO
{
    public class RiskNotificationResponse
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public List<RiskNotificationsListDTO> NotificationLog { get; set; }
    }
    public class RiskNotificationResponseDTO
    {
        public string version { get; set; }
        public int statusCode { get; set; }
        public string message { get; set; }
        public RiskNotificationResponse result { get; set; }
    }
}