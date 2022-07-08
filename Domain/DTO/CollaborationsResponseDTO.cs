// -----------------------------------------------------------------------
// <copyright file="CollaborationSharingDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;

namespace Opeeka.PICS.Domain.DTO
{
    public class CollaborationsResponseDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public List<CollaborationLookupDTO>
            Collaborations
        { get; set; }
    }
}
