// -----------------------------------------------------------------------
// <copyright file="NotificationLevelResponseDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System.Collections.Generic;

namespace Opeeka.PICS.Domain.DTO.Output
{
    public class NotificationLevelResponseDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public int TotalCount { get; set; }

        public List<NotificationLevelDTO> NotificationLevels { get; set; }
        public NotificationLevelDTO NotificationLevel { get; set; }
    }
}
