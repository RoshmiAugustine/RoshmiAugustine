// -----------------------------------------------------------------------
// <copyright file="PersonInitialsDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace Opeeka.PICS.Domain.DTO
{
    public class PersonInitialsDTO
    {
        public Guid PersonIndex { get; set; }

        public string PersonInitials { get; set; }
    }
}
