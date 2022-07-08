// -----------------------------------------------------------------------
// <copyright file="PersonAddress.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Domain.Entities
{
    public class PersonAddress : BaseEntity
    {
        public long PersonAddressID { get; set; }
        public long PersonID { get; set; }
        public long AddressID { get; set; }
        public bool IsPrimary { get; set; }

        public Address Address { get; set; }
        public Person Person { get; set; }
    }
}
