// -----------------------------------------------------------------------
// <copyright file="AgencyAddress.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Domain.Entities
{
    public class AgencyAddress : BaseEntity
    {
        public long AgencyAddressID { get; set; }
        public long AgencyID { get; set; }
        public long AddressID { get; set; }
        public int UpdateUserID { get; set; }
        public bool IsPrimary { get; set; }

        public Agency Agency { get; set; }
        public Address Address { get; set; }
        public User UpdateUser { get; set; }
    }
}
