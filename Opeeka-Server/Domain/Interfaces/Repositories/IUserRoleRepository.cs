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
    public interface IUserRoleRepository : IAsyncRepository<UserRole>
    {
        /// <summary>
        /// To post userRole details.
        /// </summary>
        /// <param name="userRole"></param>
        /// <param name="UserID"></param>
        int CreateUserRole(UserRoleDTO userRole, int UserID);

        /// <summary>
        /// To get User Role details by UserID
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns>UserRoleDTO</returns>
        Task<UserRoleDTO> GetUserRoleByUserIDAsync(int UserID);

        /// <summary>
        /// To update User Role details.
        /// </summary>
        /// <param name="userRoleDTO"></param>
        /// <returns>UserRoleDTO</returns>
        UserRoleDTO UpdateUserRole(UserRoleDTO userRoleDTO);

        void CreateBulkUserRole(List<UserRoleDTO> userRoledto);
        Task<List<UserRoleDTO>> GetUserRoleList(int userId);

        Task DeleteBulkUserRole(List<UserRoleDTO> userRoleDto);
    }
}
