// -----------------------------------------------------------------------
// <copyright file="PeopleDetailsResponseDTO.cs" company="Naico ITS">
// Copyright (c) Naico ITS. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;

namespace Opeeka.PICS.Domain.DTO.Output
{
    public class PeopleDetailsResponseDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public PeopleDataDTO PeopleData { get; set; }
    }

    public class AuditPersonProfileResponseDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public List<AuditPersonProfileDTO> HistoryDetails { get; set; }
    }
}
