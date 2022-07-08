// -----------------------------------------------------------------------
// <copyright file="AgencyContact.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Domain.Entities
{
    public class AgencyContact : BaseEntity
    {
        public int AgencyContactID { get; set; }
        public int ContactID { get; set; }
        public long AgencyID { get; set; }
        public int? ListOrder { get; set; }

        public Agency Agency { get; set; }
        public Contact Contact { get; set; }
    }
}
