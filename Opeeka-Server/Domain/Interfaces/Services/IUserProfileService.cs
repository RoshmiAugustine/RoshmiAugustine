// -----------------------------------------------------------------------
// <copyright file="IUserProfileService.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.DTO.Output;

namespace Opeeka.PICS.Domain.Interfaces.Services
{
    public interface IUserProfileService
    {
        /// <summary>
        /// AddUserProfile.
        /// </summary>
        /// <param name="userProfileDTO">userProfileDTO.</param>
        /// <returns>UserProfileResponseDTO.</returns>
        UserProfileResponseDTO AddUserProfile(UserProfileDTO userProfileDTO);

        ///// <summary>
        ///// DeleteUserProfile.
        ///// </summary>
        ///// <param name="userID">userID.</param>
        ///// <returns>UserProfileResponseDTO.</returns>
        //UserProfileResponseDTO DeleteUserProfile(int userID);

        /// <summary>
        /// Function to update user profile data
        /// </summary>
        /// <param name="userData"></param>
        /// <param name="agencyID"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        UserProfileResponseDTO UpdateUserProfile(UpdateUserProfileDTO userData, long agencyID, int userId);
    }
}
