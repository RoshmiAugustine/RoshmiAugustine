// -----------------------------------------------------------------------
// <copyright file="ItemResponseBehaviorResponseDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System.Collections.Generic;

namespace DashboardMatrixCalculationProcess.DTO
{
    public class ItemResponseBehaviorResponse
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public List<ItemResponseBehaviorDTO> ItemResponseBehavior { get; set; }
    }

    public class ItemResponseBehaviorResponseDTO
    {
        public string version { get; set; }
        public int statusCode { get; set; }
        public string message { get; set; }
        public ItemResponseBehaviorResponse result { get; set; }
    }
}
