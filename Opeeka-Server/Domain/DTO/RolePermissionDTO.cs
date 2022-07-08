// -----------------------------------------------------------------------
// <copyright file="UserRoleDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Domain.DTO
{
    public class RolePermissionDTO
    {
        public int RolePermissionID { get; set; }
        public int UserRoleID { get; set; }
        public int PermissionID { get; set; }
    }
}
