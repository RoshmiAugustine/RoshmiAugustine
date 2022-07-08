// -----------------------------------------------------------------------
// <copyright file="AgencyAddressDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
namespace Opeeka.PICS.Domain.DTO
{
    public class AgencyAddressDTO
    {

        public long AgencyAddressID { get; set; }

        public long AgencyID { get; set; }

        public long AddressID { get; set; }

        public int UpdateUserID { get; set; }

        public bool IsPrimary { get; set; }


    }
}
