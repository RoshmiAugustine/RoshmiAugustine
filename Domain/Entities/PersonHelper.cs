// -----------------------------------------------------------------------
// <copyright file="PersonHelper.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;

namespace Opeeka.PICS.Domain.Entities
{
    public class PersonHelper : BaseEntity
    {
        public long PersonHelperID { get; set; }
        public long PersonID { get; set; }
        public int HelperID { get; set; }
        public bool IsLead { get; set; }
        public bool? IsCurrent { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsRemoved { get; set; }
        public int UpdateUserID { get; set; }
        public DateTime UpdateDate { get; set; }
        public int? CollaborationID { get; set; }

        public Helper Helper { get; set; }
        public Person Person { get; set; }
        public User UpdateUser { get; set; }
        public Collaboration Collaboration { get; set; }
    }
}
