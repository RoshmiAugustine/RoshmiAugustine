// -----------------------------------------------------------------------
// <copyright file="HelperTitleResponseDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;

namespace Opeeka.PICS.Domain.DTO
{
    public class HelperTitleResponseDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public int TotalCount { get; set; }

        public List<HelperTitleDTO> HelperTitles { get; set; }
    }
}
