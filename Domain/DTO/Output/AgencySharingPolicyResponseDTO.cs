// -----------------------------------------------------------------------
// <copyright file="IdentifiedGenderResponseDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System.Collections.Generic;

namespace Opeeka.PICS.Domain.DTO.Output
{
    public class AgencySharingPolicyResponseDTO
    {
        public string ResponseStatus { get; set; }
        public int ResponseStatusCode { get; set; }
        public int TotalCount { get; set; }
        public List<AgencySharingPolicyDTO> AgencySharingPolicy { get; set; }
    }
}
