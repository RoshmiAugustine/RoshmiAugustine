// -----------------------------------------------------------------------
// <copyright file="IUserRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.Threading.Tasks;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using System.Collections.Generic;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    public interface ISharingRolePermissionRepository : IAsyncRepository<SharingRolePermission>
    {
        /// <summary>
        /// To get system Role details by role name
        /// </summary>
        /// <param name="role"></param>
        /// <returns>SystemRoleDTO</returns>
        Task<List<SharingRolePermission>> GetSharingRolePermissionsAsync(int agencySharingPolicyID, List<int> collaborationSharingPolicyIDs);

    }
}
