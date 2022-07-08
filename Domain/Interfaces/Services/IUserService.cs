// -----------------------------------------------------------------------
// <copyright file="IUserService.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Opeeka.PICS.Domain.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.DTO.Input;
using Microsoft.AspNetCore.Identity;
using Opeeka.PICS.Domain.DTO.Output;
namespace Opeeka.PICS.Domain.Interfaces.Services
{
    public interface IUserService
    {
        UserDTO VerifyUser(string username, string password);
        // List<ApplicationObjectDTO> GetUserPermissionsByUserID(long userID);
        // UserDTO GetUser(string email, string tenantID);
        // Task<User> CreateUser(UserDTO userDto);
        Task<UsersDTO> Login(LoginDTO user);
        Task<AspNetUser> FindByNameAsync(string userName);
        Task<AspNetUser> FindByEmailAsync(string email);
        Task<TokenResultDTO> Register(UsersDTO user);
        /// <summary>
        /// RegisterBulk.
        /// </summary>
        /// <param name="user">user.</param>
        /// <returns>TokenResultDTO.</returns>
        Task<List<TokenResultDTO>> RegisterBulk(List<UsersDTO> user);
        Task<bool> ConfirmEmail(string userId, string token);
        int CreateUser(UsersDTO user);
        Task<string> GetResetPasswordToken(string email);
        Task<IdentityResult> ResetPassword(AspNetUser user, PasswordResetDTO model);
        Task<TokenResultDTO> UpdateUser(UsersDTO user);
        Task<AspNetUser> FindByIdAsync(string userId);
        Task<bool> ConfirmEmailAndPassword(AspNetUser user, AccountConfirmationModelDTO passwordResetModel);
        Task<bool> ForgotPasswordReset(AspNetUser user, ForgotPasswordDTO model);

        /// <summary>
        /// GetUserProfile.
        /// </summary>
        /// <param name="userID">userID.</param>
        /// <returns>UserProfileResponseDTO.</returns>
        UserProfileResponseDTO GetUserProfile(int userID, List<string> userRole, long agencyID);
        UserProfileResponseDTO GetUserProfilePic(int userID, long agencyID, List<string> lists);

        /// <summary>
        /// UpdateUserLastLogin.
        /// </summary>
        /// <param name="userID">ID of the user</param>
        /// <returns>CRUDResponseDTO.</returns>
        CRUDResponseDTO UpdateUserLastLogin(int userID, long agencyID);

        /// <summary>
        /// GetUserEmail.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>UserDetailsResponseDTO.</returns>
        UserDataResponseDTO GetUserEmail(int agencyID);
        /// <summary>
        /// UpdateUserNotificationViewDate
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="agencyID"></param>
        /// <returns></returns>
        CRUDResponseDTO UpdateUserNotificationViewDate(int userID, long agencyID);
        AssessmentConfigurationDTO GetAssessmentConfigurations(int userID, long agencyID);
        SSOResponseDTO GetSSOConfigurations(string client);
    }
}
