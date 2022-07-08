// -----------------------------------------------------------------------
// <copyright file="UserProfileController.cs" company="Naico ITS">
// Copyright (c) Naico ITS. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Api.Controllers
{
    using System;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Opeeka.PICS.Domain.DTO;
    using Opeeka.PICS.Domain.DTO.Output;
    using Opeeka.PICS.Domain.Interfaces.Services;
    using Opeeka.PICS.Infrastructure.Enums;
    using Opeeka.PICS.Infrastructure.Logging;

    /// <summary>
    /// UserProfileController.
    /// </summary>
    [Route("api")]
    [ApiController]
    public class UserProfileController : BaseController
    {
        /// Initializes a new instance of the <see cref="logger"/> class.
        private readonly ILogger<UserProfileController> logger;

        /// Initializes a new instance of the <see cref="userProfileService"/> class.
        private readonly IUserProfileService userProfileService;

        /// Initializes a new instance of the <see cref="fileService"/> class.
        private readonly IFileService fileService;

        /// Initializes a new instance of the <see cref="userService"/> class.
        private readonly IUserService userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserProfileController"/> class.
        /// UserProfileController.
        /// </summary>
        /// <param name="logger">logger.</param>
        /// <param name="userProfileService">userProfileService.</param>
        /// <param name="fileService">fileService.</param>
        /// <param name="userService">userService.</param>
        public UserProfileController(ILogger<UserProfileController> logger, IUserProfileService userProfileService, IFileService fileService, IUserService userService)
        {
            this.userService = userService;
            this.logger = logger;
            this.userProfileService = userProfileService;
            this.fileService = fileService;
        }

        /// <summary>
        /// AddUserProfile.To update profile pic from UI.
        /// </summary>
        /// <param name="uploadfile">uploadfile.</param>
        /// <returns>UserProfileResponseDTO.</returns>
        [HttpPost]
        [Route("user-profile")]
        public ActionResult<UserProfileResponseDTO> AddUserProfile(IFormFile uploadfile)
        {
            try
            {
                UserProfileDTO userProfileDTO = new UserProfileDTO();
                UserProfileResponseDTO returnData = new UserProfileResponseDTO();
                var res = this.fileService.SaveProfilePicAsync(uploadfile, this.GetTenantID(), this.GetUserID(), this.GetRole()).Result;
                if (res.ResponseStatusCode == PCISEnum.StatusCodes.ImageUploadSuccess)
                {
                    userProfileDTO.ImageFileID = res.fileID;
                    userProfileDTO.UserID = this.GetUserID();
                    returnData = this.userProfileService.AddUserProfile(userProfileDTO);
                    returnData.ResponseStatus = res.ResponseStatus;
                    returnData.ResponseStatusCode = res.ResponseStatusCode;
                    var response = this.userService.GetUserProfile(this.GetUserID(), this.GetRole(), this.GetTenantID());
                    returnData.UserProfile = response.UserProfile;
                }
                else
                {
                    returnData.ResponseStatus = res.ResponseStatus;
                    returnData.ResponseStatusCode = res.ResponseStatusCode;
                }

                return returnData;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.InsertItem, $"AddUserProfile/POST : Adding item : Exception  : Exception occurred Adding user profile table Details. {ex.Message}");
                return this.HandleException(ex, ex.InnerException + ".An error occurred while adding user profile. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetUserEmail.
        /// </summary>
        /// <param name="userID">userID.</param>
        /// <returns>UserDataResponseDTO.</returns>
        [HttpGet]
        [Authorize(Policy = "EHRAPIPermission")]
        [Route("user-email")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<UserDataResponseDTO> GetUserEmail(int userID)
        {
            try
            {
                UserDataResponseDTO responseDTO = this.userService.GetUserEmail(userID);
                return responseDTO;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"GetUserEmail/GET : Getting item : Exception  : Exception occurred Getting User email. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving user email. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// Function to update user profile details
        /// </summary>
        /// <param name="userProfileDTO"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("user-profile")]
        public ActionResult<UserProfileResponseDTO> UpdateUserProfile([FromBody] UpdateUserProfileDTO userProfileDTO)
        {
            try
            {
                if (userProfileDTO is null)
                {
                    throw new ArgumentNullException(nameof(userProfileDTO));
                }

                var userID = Convert.ToInt32(this.GetUserID());
                var agencyID = this.GetTenantID();
                UserProfileResponseDTO returnData = this.userProfileService.UpdateUserProfile(userProfileDTO, agencyID, userID);
                var response = this.userService.GetUserProfile(this.GetUserID(), this.GetRole(), this.GetTenantID());
                returnData.UserProfile = response.UserProfile;
                return returnData;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.InsertItem, $"UpdateUserProfile/PUT : Updating item : Exception  : Exception occurred updating user profile table Details. {ex.Message}");
                return this.HandleException(ex, ex.InnerException + ".An error occurred while updating user profile. Please try again later or contact support.");
            }
        }

    }
}
