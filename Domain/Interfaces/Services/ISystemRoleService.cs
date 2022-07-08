// -----------------------------------------------------------------------
// <copyright file="ISystemRoleService.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Opeeka.PICS.Domain.DTO.Output;

namespace Opeeka.PICS.Domain.Interfaces.Services
{
    /// <summary>
    /// ISystemRoleService.
    /// </summary>
    public interface ISystemRoleService
    {
        /// <summary>
        /// Get all System Roles
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns>SystemRoleListResponseDTO</returns>
        SystemRoleListResponseDTO GetSystemRoleList(int pageNumber, int pageSize);
    }
}
