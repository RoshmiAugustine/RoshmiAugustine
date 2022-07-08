// -----------------------------------------------------------------------
// <copyright file="AgencyLookupResponseDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System.Collections.Generic;

namespace Opeeka.PICS.Domain.DTO
{
    public class AgencyLookupResponseDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public List<AgencyLookupDTO> AgencyLookup { get; set; }
    }
}