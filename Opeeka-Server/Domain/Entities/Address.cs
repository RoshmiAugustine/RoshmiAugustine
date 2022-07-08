// -----------------------------------------------------------------------
// <copyright file="Address.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace Opeeka.PICS.Domain.Entities
{
    public class Address : BaseEntity
    {
        public long AddressID { get; set; }
        public Guid AddressIndex { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? City { get; set; }
        public int? CountryStateId { get; set; }
        public string? Zip { get; set; }
        public string? Zip4 { get; set; }
        public bool IsPrimary { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UpdateUserID { get; set; }
        public int? CountryId { get; set; }
        public User UpdateUser { get; set; }
        public User CountryState { get; set; }
        public Country Country { get; set; }

    }
}
