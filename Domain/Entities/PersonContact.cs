// -----------------------------------------------------------------------
// <copyright file="PersonContact.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Domain.Entities
{
    public class PersonContact : BaseEntity
    {
        public long PersonContactID { get; set; }
        public int ContactID { get; set; }
        public long PersonID { get; set; }
        public int? ListOrder { get; set; }

        public Contact Contact { get; set; }
        public Person Person { get; set; }
    }
}
