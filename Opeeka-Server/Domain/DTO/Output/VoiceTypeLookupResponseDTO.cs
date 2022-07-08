// -----------------------------------------------------------------------
// <copyright file="VoiceTypeLookupResponseDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;

namespace Opeeka.PICS.Domain.DTO.Output
{
    public class VoiceTypeLookupResponseDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public List<VoiceTypeLookupDTO> VoiceTypeLookup { get; set; }
    }
}
