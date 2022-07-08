// -----------------------------------------------------------------------
// <copyright file="Contact.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Domain.Entities
{
    public class Contact : BaseEntity
    {
        public int ContactID { get; set; }
        public int ContactTypeID { get; set; }
        public string Value { get; set; }
        public bool IsPrimary { get; set; }
        public bool IsRemoved { get; set; }

        public ContactType ContactType { get; set; }
    }
}
