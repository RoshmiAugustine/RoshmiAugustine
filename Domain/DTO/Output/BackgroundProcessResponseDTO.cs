// -----------------------------------------------------------------------
// <copyright file="CategoryResponseDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System.Collections.Generic;

namespace Opeeka.PICS.Domain.DTO.Output
{
    public class BackgroundProcessResponseDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public BackgroundProcessLogDTO BackgroundProcess { get; set; }
    }
}
