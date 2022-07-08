// -----------------------------------------------------------------------
// <copyright file="NotificationLogResponseDTO.cs" company="Naico ITS">
// Copyright (c) Naico ITS. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;

namespace AlertAutoResolveProcess.DTO
{
    public class NotificationLogResponse
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public int TotalCount { get; set; }

        public List<NotificationLogDTO> NotificationLog { get; set; }
    }
    public class NotificationLogResponseDTO
    {
        public string version { get; set; }
        public int statusCode { get; set; }
        public string message { get; set; }
        public NotificationLogResponse result { get; set; }
    }
}