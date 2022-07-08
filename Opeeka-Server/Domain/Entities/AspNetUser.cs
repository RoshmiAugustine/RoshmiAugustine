// -----------------------------------------------------------------------
// <copyright file="User.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using Microsoft.AspNetCore.Identity;

namespace Opeeka.PICS.Domain.Entities
{
    public class AspNetUser : IdentityUser
    {
        public int UserID { get; set; }

        public Guid UserIndex { get; set; }

        public DateTime? LastLogin { get; set; }

        public bool IsActive { get; set; }

        public long? AgencyId { get; set; }
        public override string NormalizedUserName { get; set; }

        public Agency Agency { get; set; }
    }
}
