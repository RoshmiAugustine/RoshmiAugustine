// -----------------------------------------------------------------------
// <copyright file="PersonCollaborationResponseDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System.Collections.Generic;

namespace Opeeka.PICS.Domain.DTO.Output
{
    public class PersonCollaborationResponseDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public List<PeopleCollaborationDTO> PersonCollaborations { get; set; }
    }
}
