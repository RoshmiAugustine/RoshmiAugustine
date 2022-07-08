// -----------------------------------------------------------------------
// <copyright file="HelperAddressDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace Opeeka.PICS.Domain.DTO
{
    public class HelperAddressDTO
    {
        public int HelperAddressID { get; set; }

        public Guid HelperAddressIndex { get; set; }

        public int HelperID { get; set; }

        public long AddressID { get; set; }

        public bool IsPrimary { get; set; }
    }
}
