// -----------------------------------------------------------------------
// <copyright file="UpperPaneSearchResponseDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System.Collections.Generic;

namespace Opeeka.PICS.Domain.DTO
{
    public class UpperpaneSearchResponseDTO
    {
        public string ResponseStatus { get; set; }
        public int ResponseStatusCode { get; set; }
        public List<UpperpaneSearchDTO> UpperpaneSearchList { get; set; }
    }
}
