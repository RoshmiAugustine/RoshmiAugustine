// -----------------------------------------------------------------------
// <copyright file="PersonCollaboration.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;

namespace Opeeka.PICS.Domain.Entities
{
    public class PersonCollaboration : BaseEntity
    {
        public long PersonCollaborationID { get; set; }
        public long PersonID { get; set; }
        public int CollaborationID { get; set; }
        public DateTime? EnrollDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? IsPrimary { get; set; }
        public bool? IsCurrent { get; set; }
        public bool IsRemoved { get; set; }
        public int UpdateUserID { get; set; }
        public DateTime UpdateDate { get; set; }

        public Collaboration Collaboration { get; set; }
        public Person Person { get; set; }
        public User UpdateUser { get; set; }
    }
}