// -----------------------------------------------------------------------
// <copyright file="AddPersonResponseDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;

namespace Opeeka.PICS.Domain.DTO.Output
{
    public class AddPersonResponseDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public Guid PersonIndex { get; set; }

        public long Id { get; set; }

    }
}
