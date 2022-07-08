// -----------------------------------------------------------------------
// <copyright file="AccountController.cs" company="Naico ITS">
// Copyright (c) Naico ITS. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Opeeka.PICS.Domain.DTO;
    using Opeeka.PICS.Domain.DTO.Input;
    using Opeeka.PICS.Domain.DTO.Output;
    using Opeeka.PICS.Domain.Interfaces.Common;
    using Opeeka.PICS.Domain.Interfaces.Services;
    using Opeeka.PICS.Domain.Resources;
    using Opeeka.PICS.Infrastructure.Enums;

    /// <summary>
    /// AccountController. 
    /// </summary>
    [ApiController]
    [Route("api")]
    public class AccountController : BaseController
    {
        /// Initializes a new instance of the <see cref="userService"/> class.
        private readonly IUserService userService;

        /// Initializes a new instance of the <see cref="_jWTTokenService"/> class.
        private readonly IJWTTokenService _jWTTokenService;

        /// Initializes a new instance of the <see cref="locService"/> class.
        private readonly LocalizeService locService;

        /// Initializes a new instance of the <see cref="emailSender"/> class.
        private readonly IEmailSender emailSender;

        /// Initializes a new instance of the <see cref="logger"/> class.
        private readonly ILogger<AccountController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="userService">userService.</param>
        /// <param name="jWTTokenService">jWTTokenService.</param>
        /// <param name="sss">sss.</param>
        /// <param name="emailSender">emailSender.</param>
        /// <param name="logger">logger.</param>
        public AccountController(IUserService userService, IJWTTokenService jWTTokenService, LocalizeService sss, IEmailSender emailSender, ILogger<AccountController> logger)
        {
            this.userService = userService;
            _jWTTokenService = jWTTokenService;
            locService = sss;
            this.emailSender = emailSender;
            this.logger = logger;
        }

        /// <summary>
        /// Login.
        /// </summary>
        /// <param name="user">user.</param>
        /// <returns>Task.IActionResult.</returns>
        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDTO user)
        {
            IActionResult response = Unauthorized();

            var result = await userService.Login(user);
            if (result != null)
            {
                var tokenStr = _jWTTokenService.GenerateJSONWebToken(result);
                response = Ok(new { token = tokenStr });
                return response;
            }
            else
            {
                return Ok(new
                {
                    ResponseStatusCode = PCISEnum.StatusCodes.InvalidLogin,
                    ResponseStatus = PCISEnum.StatusMessages.InvalidLogin,
                });
            }
        }

         /// <summary>
         /// GetData.
         /// </summary>
         /// <returns>IActionResult.</returns>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Route("Account")]
        public IActionResult GetData()
        {
            return Ok(new { Response = "I am an authorized admin" });
        }

        /// <summary>
        /// LoginWithB2CToken.
        /// </summary>
        /// <param name="tenantId">tenantId.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        [Route("LoginWithB2CToken/{tenantId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult LoginWithB2CToken(string tenantId)
        {
            string token = base.GetToken();
            string b2cToken = base.GetB2CToken();
            return Ok(new { token = token, b2cToken = b2cToken });
        }

        /// <summary>
        /// Register.
        /// </summary>
        /// <param name="user">user.</param>
        /// <returns>Task.ActionResult.</returns>
        [HttpPost]
        [Route("Account")]
        public async Task<ActionResult> Register(UsersDTO user)
        {
            var tokenResult = await userService.Register(user);

            var callbackUrl = Url.Action("ConfirmEmail", "Account",
            new { userId = tokenResult.Id, emailToken = tokenResult.EmailToken, passwordToken = tokenResult.PasswordToken, tenantId = tokenResult.TenantId }, Request.Scheme);
            SendEmail sendEmail = new SendEmail()
            {
                Body = "Welcome!     To activate your account, please confirm your email and password: " + callbackUrl,
                FromDisplayName = "Haseena ",
                IsHtmlEmail = true,
                Subject = "Confirm your Email",
                ToDisplayName = "Hazeena",
                ToEmail = user.Email,
            };
            var response = emailSender.SendEmailAsync(sendEmail);

            return Ok(callbackUrl);
        }

        /// <summary>
        /// ConfirmEmailAndPassword.
        /// </summary>
        /// <param name="model">AccountConfirmationModelDTO model.</param>
        /// <returns>Task.ActionResult.</returns>
        [HttpPost]
        [Route("ConfirmEmailAndPassword")]
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmailAndPassword(AccountConfirmationModelDTO model)
        {
            if (model.UserID == null || model.EmailConfirmationToken == null || model.PasswordConfirmationToken == null)
            {
                return RedirectToAction("NotFound");
            }

            HttpContext.Request.Headers[PCISEnum.Parameters.tenantId] = model.TenantId.ToString();

            var user = await userService.FindByIdAsync(model.UserID);
            if (user == null)
            {
                return RedirectToPage("NotFound");
            }

            bool isSuccess = await userService.ConfirmEmailAndPassword(user, model);

            if (isSuccess)
            {
                return Ok("Success");
            }

            return RedirectToPage("NotFound");
        }

        /// <summary>
        /// UpdateUser.
        /// </summary>
        /// <param name="user">user.</param>
        /// <returns>Task.ActionResult.</returns>
        [HttpPut]
        [Route("Account")]
        public async Task<ActionResult> UpdateUser(UsersDTO user)
        {
            var tokenResult = await userService.UpdateUser(user);

            return Ok("Successfully updated");
        }

        /// <summary>
        /// ConfirmEmail.
        /// </summary>
        /// <param name="userId">userId.</param>
        /// <param name="emailToken">emailToken.</param>
        /// <param name="passwordToken">passwordToken.</param>
        /// <param name="tenantId">tenantId.</param>
        /// <returns>Task Action Result.</returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("ConfirmEmail/{userId}/{emailToken}/{passwordToken}/{tenantId}")]
        public async Task<ActionResult> ConfirmEmail(string userId, string emailToken, string passwordToken, string tenantId)
        {
            if (userId == null || emailToken == null)
            {
                return RedirectToAction("");
            }

            HttpContext.Request.Headers[PCISEnum.Parameters.tenantId] = tenantId.ToString();
            var result = await userService.ConfirmEmail(userId, emailToken);

            if (result)
            {
                return Ok("Success");
            }

            return RedirectToPage("NotFound");
        }

        /// <summary>
        /// GetResetPasswordToken.
        /// </summary>
        /// <param name="email">email.</param>
        /// <returns>Task.ActionResult.</returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("GetResetPasswordToken/{email}")]
        public async Task<ActionResult> GetResetPasswordToken(string email)
        {
            var result = await userService.GetResetPasswordToken(email);
            return Ok(result);
        }

        /// <summary>
        /// ForgotPassword.
        /// </summary>
        /// <param name="model">model.</param>
        /// <returns>Task.ActionResult.</returns>
        [HttpPost]
        [Route("ForgotPassword")]
        [AllowAnonymous]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordDTO model)
        {
            var user = await userService.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return RedirectToPage("NotFound");
            }

            bool isSuccess = await userService.ForgotPasswordReset(user, model);

            if (isSuccess)
            {
                return Ok("Reset Password Successfull ");
            }

            return RedirectToPage("NotFound");
        }

        /// <summary>
        /// ResetPassword.
        /// </summary>
        /// <param name="model">model.</param>
        /// <returns>Task.ActionResult.</returns>
        [HttpPost]
        [Route("ResetPassword")]
        public async Task<ActionResult> ResetPassword(PasswordResetDTO model)
        {
            var email = base.GetEmailID();
            var user = await userService.FindByEmailAsync(email);
            if (user == null)
            {
                return RedirectToPage("NotFound");
            }

            var result = await userService.ResetPassword(user, model);

            if (result.Succeeded)
            {
                return Ok(new
                {
                    ResponseStatusCode = PCISEnum.StatusCodes.UpdationSuccess,
                    ResponseStatus = PCISEnum.StatusMessages.UpdationSuccess,
                });
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    if (error.Code.Contains("PasswordMismatch"))
                    {
                        return Ok(new
                        {
                            ResponseStatusCode = PCISEnum.StatusCodes.InvalidPassword,
                            ResponseStatus = PCISEnum.StatusMessages.InvalidPassword,
                        });
                    }
                }

                return Ok(new
                {
                    ResponseStatusCode = PCISEnum.StatusCodes.Failure,
                    ResponseStatus = PCISEnum.StatusMessages.Failure,
                });
            }
        }

        /// <summary>
        /// GetUserProfile.
        /// </summary>
        /// <returns>UserProfileResponseDTO.</returns>
        [HttpGet]
        [Route("user-profile")]
        public ActionResult<UserProfileResponseDTO> GetUserProfile()
        {
            try
            {
                List<string> userRole = this.GetRole();
                var response = this.userService.GetUserProfile(this.GetUserID(), userRole, this.GetTenantID());
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"GetUserProfile/GET : Getting item : Exception  : Exception occurred Getting User Profile. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving user profile. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetUserProfilePic.
        /// </summary>
        /// <returns>UserProfileResponseDTO.</returns>
        [HttpGet]
        [Route("user-profile-pic")]
        public ActionResult<UserProfileResponseDTO> GetUserProfilePic()
        {
            try
            {
                var response = this.userService.GetUserProfilePic(this.GetUserID(), this.GetTenantID(), this.GetRole());
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"GetUserProfilePic/GET : Getting item : Exception  : Exception occurred Getting User Profile Pic. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving user profile pic. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// UpdateUserLastLogin.
        /// </summary>
        /// <returns>CRUDResponseDTO.</returns>
        [HttpPut]
        [Route("user-last-login")]
        public ActionResult<CRUDResponseDTO> UpdateUserLastLogin()
        {
            try
            {
                var response = this.userService.UpdateUserLastLogin(this.GetUserID(), this.GetTenantID());
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"UpdateUserLastLogin/PUT : Updating item : Exception  : Exception occurred Updating User Last Login. {ex.Message}");
                return this.HandleException(ex, "An error occurred updating user last login. Please try again later or contact support.");
            }
        }
        /// <summary>
        /// UpdateUserNotificationViewDate.
        /// </summary>
        /// <returns>CRUDResponseDTO.</returns>
        [AllowAnonymous]
        [HttpPut]
        [Route("user-notification-viewon")]
        public ActionResult<CRUDResponseDTO> UpdateUserNotificationViewDate()
        {
            try
            {
                var response = this.userService.UpdateUserNotificationViewDate(this.GetUserID(), this.GetTenantID());
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"UpdateUserNotificationViewDate/PUT : Updating item : Exception  : Exception occurred Updating User Last Login. {ex.Message}");
                return this.HandleException(ex, "An error occurred updating user notificationViewDate. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetAssessmentConfigurations.
        /// </summary>
        /// <returns>CRUDResponseDTO.</returns>
        [HttpGet]
        [Route("assessment-configurations")]
        public ActionResult<AssessmentConfigurationDTO> GetAssessmentConfigurations()
        {
            try
            {
                var response = this.userService.GetAssessmentConfigurations(this.GetUserID(), this.GetTenantID());
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"GetAssessmentConfigurations/GET : Getting item : Exception  : Exception occurred in GetAssessmentConfigurations. {ex.Message}");
                return this.HandleException(ex, "An error occurred in GetAssessmentConfigurations. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetSSOConfigurations.
        /// </summary>
        /// <param name="client">client</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("sso-redirect-url/{client}")]
        public ActionResult<SSOResponseDTO> GetSSOConfigurations(string client)
        {
            try
            {
                var response = this.userService.GetSSOConfigurations(client);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"GetAssessmentConfigurations/GET : Getting item : Exception  : Exception occurred in GetAssessmentConfigurations. {ex.Message}");
                return this.HandleException(ex, "An error occurred in GetAssessmentConfigurations. Please try again later or contact support.");
            }
        }
    }
}
