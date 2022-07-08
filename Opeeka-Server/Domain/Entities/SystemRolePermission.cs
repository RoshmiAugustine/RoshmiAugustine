// -----------------------------------------------------------------------
// <copyright file="UserRole.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Domain.Entities
{
    public class SystemRolePermission : BaseEntity
    {
        public int SystemRolePermissionID { get; set; }

        public int SystemRoleID { get; set; }

        public int PermissionID { get; set; }

        public SystemRole SystemRole { get; set; }
        public Permission Permission { get; set; }

    }
}
