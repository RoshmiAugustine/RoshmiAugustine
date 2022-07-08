// -----------------------------------------------------------------------
// <copyright file="Agency.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;

namespace Opeeka.PICS.Domain.Entities
{
    public class Agency : BaseEntity
    {
        public string Name { get; set; }
        public long AgencyID { get; set; }
        public Guid AgencyIndex { get; set; }
        public int UpdateUserID { get; set; }
        public bool IsCustomer { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime UpdateDate { get; set; }
        public string Note { get; set; }
        public string Abbrev { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Email { get; set; }
        public string ContactLastName { get; set; }
        public string ContactFirstName { get; set; }
        public User UpdateUser { get; set; }

    }
}
