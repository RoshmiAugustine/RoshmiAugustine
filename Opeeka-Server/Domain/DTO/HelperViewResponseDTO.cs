// -----------------------------------------------------------------------
// <copyright file="HelperViewResponseDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;

namespace Opeeka.PICS.Domain.DTO.Output
{
    public class HelperViewResponseDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public HelperInfoDTO HelperDetails { get; set; }

        public List<HelperInfoDTO> HelperList { get; set; }
    }
}
