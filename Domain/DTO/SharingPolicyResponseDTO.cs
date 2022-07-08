// -----------------------------------------------------------------------
// <copyright file="AddressDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;

namespace Opeeka.PICS.Domain.DTO
{
    public class SharingPolicyResponseDTO
    {

        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public List<SharingPolicyDTO> sharingPolicyDTOs { get; set; }


    }
}
