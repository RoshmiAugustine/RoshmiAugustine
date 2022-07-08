// -----------------------------------------------------------------------
// <copyright file="PeopleDataDTO.cs" company="Naico ITS">
// Copyright (c) Naico ITS. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Domain.DTO
{
    public class PersonAddressDTO
    {
        public long PersonAddressID { get; set; }
        public long PersonID { get; set; }
        public long AddressID { get; set; }
        public bool IsPrimary { get; set; }
    }
}
