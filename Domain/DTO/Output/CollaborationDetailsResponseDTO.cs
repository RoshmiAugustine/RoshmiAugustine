// -----------------------------------------------------------------------
// <copyright file="PeopleDetailsResponseDTO.cs" company="Naico ITS">
// Copyright (c) Naico ITS. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;

namespace Opeeka.PICS.Domain.DTO.Output
{
    public class CollaborationDetailsResponseDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public CollaborationInfoDTO collaborationInfo { get; set; }
         
        public List<CollaborationInfoDTO> collaborationList { get; set; }
    }
}
