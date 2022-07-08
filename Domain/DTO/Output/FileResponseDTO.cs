﻿// -----------------------------------------------------------------------
// <copyright file="FileResponseDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Domain.DTO.Output
{
    public class FileResponseDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public long fileID { get; set; }
    }
}
