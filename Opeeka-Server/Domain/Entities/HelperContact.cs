// -----------------------------------------------------------------------
// <copyright file="HelperContact.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Domain.Entities
{
    public class HelperContact : BaseEntity
    {
        public int HelperContactID { get; set; }
        public int ContactID { get; set; }
        public int HelperID { get; set; }
        public int? ListOrder { get; set; }

        public Contact Contact { get; set; }
        public Helper Helper { get; set; }
    }
}
