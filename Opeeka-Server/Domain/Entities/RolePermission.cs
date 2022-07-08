// -----------------------------------------------------------------------
// <copyright file="RolePermission.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Domain.Entities
{
    public class RolePermission : BaseEntity
    {
        public int RolePermissionID { get; set; }
        public int UserRoleID { get; set; }
        public int PermissionID { get; set; }

        public UserRole UserRole { get; set; }
        public Permission Permission { get; set; }
    }
}
