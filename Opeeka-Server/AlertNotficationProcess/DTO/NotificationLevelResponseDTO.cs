// -----------------------------------------------------------------------
// <copyright file="NotificationLevelResponseDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using AlertNotificationProcess.DTO;
using System.Collections.Generic;

namespace AlertNotificationProcess.Output
{
    public class NotificationLevelResponse
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public int TotalCount { get; set; }

        public List<NotificationLevelDTO> NotificationLevels { get; set; }
        public NotificationLevelDTO NotificationLevel { get; set; }
    }

    public class NotificationLevelResponseDTO
    {
        public string version { get; set; }
        public int statusCode { get; set; }
        public string message { get; set; }
        public NotificationLevelResponse result { get; set; }
    }
}
