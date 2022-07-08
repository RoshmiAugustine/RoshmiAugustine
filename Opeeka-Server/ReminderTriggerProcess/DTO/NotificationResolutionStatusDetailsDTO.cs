// -----------------------------------------------------------------------
// <copyright file="NotificationLogResponseDTO.cs" company="Naico ITS">
// Copyright (c) Naico ITS. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;

namespace ReminderTriggerProcess.DTO
{
    public class NotificationResolutionStatusDetails
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public int TotalCount { get; set; }

        public NotificationResolutionStatusDTO NotificationStatus { get; set; }
    }

    public class NotificationResolutionStatusDetailsDTO
    {
        public string version { get; set; }
        public int statusCode { get; set; }
        public string message { get; set; }
        public NotificationResolutionStatusDetails result { get; set; }
    }
}