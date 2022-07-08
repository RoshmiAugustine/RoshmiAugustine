// -----------------------------------------------------------------------
// <copyright file="IUserRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using System.Collections.Generic;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    public interface IPermissionRepository : IAsyncRepository<Permission>
    {


        /// <summary>
        /// To get User Permissions
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns>UsersDTO</returns>
        List<ApplicationObjectDTO> GetUserPermissionsByUserID(long userID, List<string> applicationObjectTypes);

        /// <summary>
        /// To get Role Permissions
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns>UsersDTO</returns>
        List<ApplicationObjectDTO> GetUserPermissionsByRoleID(int roleID, List<string> applicationObjectTypes);

        List<ApplicationObjectDTO> GetUserSharedPermissionsByRoleID(int roleID, List<string> applicationObjectTypes, int agencySharingPolicyID = 0, int collaborationSharingPolicyID = 0, bool isActivePerson = true);

    }
}
