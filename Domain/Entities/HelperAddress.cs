// -----------------------------------------------------------------------
// <copyright file="HelperAddress.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace Opeeka.PICS.Domain.Entities
{
    public class HelperAddress : BaseEntity
    {
        public int HelperAddressID { get; set; }

        public Guid HelperAddressIndex { get; set; }

        public int HelperID { get; set; }

        public long AddressID { get; set; }

        public bool IsPrimary { get; set; }

        public Address Address { get; set; }
        public Helper Helper { get; set; }
    }
}
