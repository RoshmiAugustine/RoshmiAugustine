// -----------------------------------------------------------------------
// <copyright file="ItemResponseBehaviorResponseDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System.Collections.Generic;

namespace Opeeka.PICS.Domain.DTO.Output
{
    public class ItemResponseBehaviorResponseDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public List<ItemResponseBehaviorDTO> ItemResponseBehavior { get; set; }
    }
}
