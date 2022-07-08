// <copyright file="SupportContact.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Domain.Entities
{
    public class SupportContact : BaseEntity
    {
        public int SupportContactID { get; set; }
        public int ContactID { get; set; }
        public int SupportID { get; set; }
        public int? ListOrder { get; set; }

        public Contact Contact { get; set; }
        public PersonSupport Support { get; set; }
    }
}
