// -----------------------------------------------------------------------
// <copyright file="CollaborationResponseDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace Opeeka.PICS.Domain.DTO.Output
{
    public class CollaborationResponseDTO
    {

        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public Guid Id { get; set; }

        public int CollaborationId { get; set;}
    }
}
