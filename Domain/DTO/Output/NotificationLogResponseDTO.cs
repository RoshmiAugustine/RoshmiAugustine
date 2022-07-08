// -----------------------------------------------------------------------
// <copyright file="NotificationLogResponseDTO.cs" company="Naico ITS">
// Copyright (c) Naico ITS. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;

namespace Opeeka.PICS.Domain.DTO.Output
{
    public class NotificationLogResponseDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public int TotalCount { get; set; }

        public List<NotificationLogDTO> NotificationLog { get; set; }
    }
}