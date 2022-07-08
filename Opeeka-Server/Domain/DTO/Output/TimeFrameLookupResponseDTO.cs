// -----------------------------------------------------------------------
// <copyright file="TimeFrameLookupResponseDTO.cs" company="Naico ITS">
// Copyright (c) Naico ITS. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Domain.DTO.Output
{
    public class TimeFrameResponseDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public TimeFrameDTO TimeFrameDetails { get; set; }
    }
}
