// -----------------------------------------------------------------------
// <copyright file="IUserRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System.Threading.Tasks;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using System.Collections.Generic;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    public interface IRolePermissionRepository : IAsyncRepository<RolePermission>
    {
        /// <summary>
        /// To get system Role details by role name
        /// </summary>
        /// <param name="role"></param>
        /// <returns>SystemRoleDTO</returns>
        Task CreateBulkRolePermissions(List<RolePermissionDTO> rolePermissionDto);
        Task DeleteBulkRolePermissions(List<RolePermissionDTO> rolePermissionDto);
        Task<List<RolePermissionDTO>> GetRolePermissionsAsync(List<int> userRoleIds);

    }
}
