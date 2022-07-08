// -----------------------------------------------------------------------
// <copyright file="IUserRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System.Threading.Tasks;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using System.Collections.Generic;
using System;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IAsyncRepository<User>
    {
        /// <summary>
        /// To post helpers details.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Object of users</returns>
        int CreateUser(UsersDTO user);

        /// <summary>
        /// To get Users details by UsersID
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns>UsersDTO</returns>
        Task<UsersDTO> GetUsersByUsersIDAsync(int UserID);

        /// <summary>
        /// To update user details.
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns>UsersDTO</returns>
        UsersDTO UpdateUser(UsersDTO userDTO);

        List<ApplicationObjectDTO> GetUserPermissionsByUserID(long userID, string applicationObjectType);
        Task<UsersDTO> GetUserByUserNameAndAgencyIdAsync(string email, long agencyId);
        Task<List<SystemRole>> GetUserRoleList(long userId);
        /// <summary>
        /// GetUserProfile.
        /// </summary>
        /// <param name="userID">userID.</param>
        /// <param name="IsSuperAdmin">IsSuperAdmin.</param>
        /// <returns>UserProfileDTO.</returns>
        UserProfileDTO GetUserProfile(int userID, bool IsSuperAdmin);

        /// <summary>
        /// GetUserProfilePicDetails.
        /// </summary>
        /// <param name="userID">userID.</param>
        /// <returns>UserProfileDTO.</returns>
        UserProfileDTO GetUserProfilePicDetails(int userID);
        /// <summary>
        /// CreateUserBulk.
        /// </summary>
        /// <param name="userList">userList.</param>
        /// <returns>UsersDTO.</returns>
        List<UsersDTO> CreateUserBulk(List<UsersDTO> userList);
        /// <summary>
        /// GetUserListByGUID.
        /// </summary>
        /// <param name="UserIndexGuids">UserIndexGuids.</param>
        /// <returns>User.</returns>
        Task<IReadOnlyList<User>> GetUserListByGUID(List<Guid> UserIndexGuids);
        /// <summary>
        /// UpdateUserBulk.
        /// </summary>
        /// <param name="userList">userList.</param>
        /// <returns>UsersDTO.</returns>
        List<User> UpdateUserBulk(List<User> userList);

        /// <summary>
        ///  GetUserEmail.
        /// </summary>
        /// <param name="userID">userID.</param>
        /// <returns>UsersDTO.</returns>
        UsersDTO GetUserEmail(int userID);
        /// <summary>
        /// ValidateHelperEmail.
        /// </summary>
        /// <param name="nameCSV">nameCSV.</param>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>UsersDTO.</returns>
        List<UsersDTO> ValidateHelperEmail(string nameCSV, long agencyID);
        Task<User> GetUsersDetailsByUsersIDAsync(int UserID);

        /// <summary>
        /// UserLastLoginTime
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        UsersDTO UserLastLoginTime(int userId);
    }
}
