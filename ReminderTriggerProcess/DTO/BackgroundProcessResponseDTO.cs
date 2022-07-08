// -----------------------------------------------------------------------
// <copyright file="CategoryResponseDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System.Collections.Generic;

namespace ReminderTriggerProcess.DTO
{
    public class BackgroundProcessResponse
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public BackgroundProcessLogDTO BackgroundProcess { get; set; }
    }
    public class BackgroundProcessResponseDTO
    {
        public string version { get; set; }
        public int statusCode { get; set; }
        public string message { get; set; }
        public BackgroundProcessResponse result { get; set; }
    }
}
