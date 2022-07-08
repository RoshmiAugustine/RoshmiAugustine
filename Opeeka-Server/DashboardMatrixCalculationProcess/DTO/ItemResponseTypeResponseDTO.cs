// -----------------------------------------------------------------------
// <copyright file="ItemResponseBehaviorResponseDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System.Collections.Generic;

namespace DashboardMatrixCalculationProcess.DTO
{ 
    public class ItemResponseTypeResponse
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public List<ItemResponseTypeDTO> ItemResponseType { get; set; }
    }

    public class ItemResponseTypeResponseDTO
    {
        public string version { get; set; }
        public int statusCode { get; set; }
        public string message { get; set; }
        public ItemResponseTypeResponse result { get; set; }
    }
}
