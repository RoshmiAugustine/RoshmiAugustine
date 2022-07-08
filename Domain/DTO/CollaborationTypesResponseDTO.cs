// -----------------------------------------------------------------------
// <copyright file="CollaborationTypesResponseDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System.Collections.Generic;

namespace Opeeka.PICS.Domain.DTO
{
    public class CollaborationTypesResponseDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public List<CollaborationDataDTO> CollaborationCategories { get; set; }
    }
}



