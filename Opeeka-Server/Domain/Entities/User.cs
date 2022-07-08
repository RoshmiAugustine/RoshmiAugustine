// -----------------------------------------------------------------------
// <copyright file="User.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;

namespace Opeeka.PICS.Domain.Entities
{
    public class User : BaseEntity
    {
        public int UserID { get; set; }
        public Guid UserIndex { get; set; }
        public string UserName { get; set; }
        public DateTime? LastLogin { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public long? AgencyID { get; set; }
        public string AspNetUserID { get; set; }
        public DateTime? NotificationViewedOn { get; set; }

        //public Agency Agency { get; set; }

    }
}
