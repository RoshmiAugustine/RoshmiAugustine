// -----------------------------------------------------------------------
// <copyright file="CollaborationAgencyAddress.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Domain.Entities
{
    public class CollaborationAgencyAddress : BaseEntity
    {
        public int CollaborationID { get; set; }
        public long AddressID { get; set; }

        public Address Address { get; set; }
        public Collaboration Collaboration { get; set; }
    }
}
