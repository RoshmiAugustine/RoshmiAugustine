// -----------------------------------------------------------------------
// <copyright file="HelperTitleResponseDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;

namespace AlertAutoResolveProcess.DTO
{
    public class NotificationTypeListResponse
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public int TotalCount { get; set; }

        public List<NotificationTypeDTO> NotificationTypes { get; set; }
        public NotificationTypeDTO NotificationType { get; set; }

    }

    public class NotificationTypeListResponseDTO
    {
        public string version { get; set; }
        public int statusCode { get; set; }
        public string message { get; set; }
        public NotificationTypeListResponse result { get; set; }
    }
}
