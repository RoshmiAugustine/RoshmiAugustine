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
    public interface ISystemRoleRepository : IAsyncRepository<SystemRole>
    {
        /// <summary>
        /// To get system Role details by role name
        /// </summary>
        /// <param name="role"></param>
        /// <returns>SystemRoleDTO</returns>
        List<SystemRoleDTO> GetSystemRoleByRoleNameAsync(List<string> roles);

        /// <summary>
        /// To get all system Roles
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns>List<SystemRoleDTO></returns>
        List<SystemRoleDTO> GetAllSystemRoles(int pageNumber, int pageSize);

        /// <summary>
        /// Get the count of all System Roles
        /// </summary>
        /// <returns>int</returns>
        int GetSystemRoleCount();

        /// <summary>
        /// GetSystemRoleByRoleID.
        /// </summary>
        /// <param name="roleID">roleID.</param>
        /// <returns>SystemRoleDTO.</returns>
        SystemRoleDTO GetSystemRoleByRoleID(int roleID);

        /// <summary>
        /// GetSystemRoleByRoleName.
        /// </summary>
        /// <param name="systemRoleName">systemRoleName</param>
        /// <returns>SystemRoleDTO.</returns>
        SystemRoleDTO GetSystemRoleByRoleName(string systemRoleName);
    }
}
