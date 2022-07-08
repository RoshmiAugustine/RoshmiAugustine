// -----------------------------------------------------------------------
// <copyright file="UserRole.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;

namespace Opeeka.PICS.Domain.Entities
{
    public class SharingRolePermission : BaseEntity
    {
        public int SharingRolePermissionID { get; set; }
        public int SystemRolePermissionID { get; set; }
        public int AgencySharingPolicyID { get; set; }
        public int CollaborationSharingPolicyID { get; set; }
        public bool AllowInactiveAccess { get; set; }

        public AgencySharingPolicy AgencySharingPolicy { get; set; }
        public CollaborationSharingPolicy CollaborationSharingPolicy { get; set; }
        public SystemRolePermission SystemRolePermission { get; set; }

    }
}
