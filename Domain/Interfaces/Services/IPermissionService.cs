// -----------------------------------------------------------------------
// <copyright file="IPermissionService.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Opeeka.PICS.Domain.DTO;
using System.Collections.Generic;

namespace Opeeka.PICS.Domain.Interfaces.Services
{
    /// <summary>
    /// IAgencyService.
    /// </summary>
    public interface IPermissionService
    {
        List<PermissionResultDTO> GetUserPermissionsByUserID(long userID, List<string> applicationObjectTypes);
        List<PermissionResultDTO> GetUserPermissionsByRoleID(int roleID, List<string> applicationObjectTypes);
        List<PermissionResultDTO> GetSharedPersonPermissionsByRoleID(int roleID, List<string> applicationObjectTypes, string agencySharingIndex, string collaborationSharingIndex, bool isActiveForSharing);
    }
}
