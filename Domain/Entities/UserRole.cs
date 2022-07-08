// -----------------------------------------------------------------------
// <copyright file="UserRole.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;

namespace Opeeka.PICS.Domain.Entities
{
    public class UserRole : BaseEntity
    {
        public int UserRoleID { get; set; }

        public Guid UserRoleIndex { get; set; }

        public int UserID { get; set; }

        public int SystemRoleID { get; set; }

        public User User { get; set; }
        public SystemRole SystemRole { get; set; }
    }
}
