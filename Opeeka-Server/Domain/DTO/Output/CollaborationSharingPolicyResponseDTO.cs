// -----------------------------------------------------------------------
// <copyright file="CollaborationSharingPolicyResponseDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System.Collections.Generic;

namespace Opeeka.PICS.Domain.DTO.Output
{
    public class CollaborationSharingPolicyResponseDTO
    {
        public string ResponseStatus { get; set; }
        public int ResponseStatusCode { get; set; }
        public int TotalCount { get; set; }
        public List<CollaborationSharingPolicyDTO> collaborationSharingPolicy { get; set; }
    }
}
