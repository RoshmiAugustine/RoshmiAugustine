// -----------------------------------------------------------------------
// <copyright file="CollaborationLevelResponseDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System.Collections.Generic;

namespace Opeeka.PICS.Domain.DTO.Output
{
    public class CollaborationLevelResponseDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }
        public int TotalCount { get; set; }
        public List<CollaborationLevelDTO> CollaborationLevels { get; set; }
    }
}
