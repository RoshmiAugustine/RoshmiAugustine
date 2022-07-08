// -----------------------------------------------------------------------
// <copyright file="SupportAddress.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Domain.Entities
{
    public class SupportAddress : BaseEntity
    {
        public int SupportAddressID { get; set; }
        public int SupportID { get; set; }
        public long AddressID { get; set; }
        public bool IsPrimary { get; set; }

        public Address Address { get; set; }
        public PersonSupport Support { get; set; }
    }
}
