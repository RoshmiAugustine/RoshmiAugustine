// -----------------------------------------------------------------------
// <copyright file="HelperController.cs" company="Naico ITS">
// Copyright (c) Naico ITS. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Opeeka.PICS.Api.Filters;
    using Opeeka.PICS.Domain.DTO;
    using Opeeka.PICS.Domain.DTO.ExternalAPI;
    using Opeeka.PICS.Domain.DTO.Input;
    using Opeeka.PICS.Domain.DTO.Output;
    using Opeeka.PICS.Domain.Interfaces.Services;
    using Opeeka.PICS.Infrastructure.Enums;
    using Opeeka.PICS.Infrastructure.Logging;

    /// <summary>
    /// HelperController.
    /// </summary>
    [Route("api")]
    [ApiController]
    public class HelperController : BaseController
    {
        /// Initializes a new instance of the <see cref="helperService"/> class.
        private readonly IHelperService helperService;

        /// Initializes a new instance of the <see cref="logger"/> class.
        private readonly ILogger<HelperController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="HelperController"/> class.
        /// </summary>
        /// <param name="logger">logger.</param>
        /// <param name="helperService">helperService.</param>
        public HelperController(ILogger<HelperController> logger, IHelperService helperService)
        {
            this.helperService = helperService;
            this.logger = logger;
        }

        /// <summary>
        /// SaveHelperDetails.
        /// </summary>
        /// <param name="helperData">helperData.</param>
        /// <returns>ActionResult.<StatusResponseDTO>.</returns>
        [HttpPost]
        //[ServiceFilter(typeof(TransactionFilter))]
        [Route("helper")]
        public ActionResult<CRUDResponseDTO> SaveHelperDetails([FromBody] HelperDetailsDTO helperData)
        {
            try
            {
                if (helperData != null)
                {
                    if (this.ModelState.IsValid)
                    {
                        helperData.UpdateUserID = this.GetUserID();
                        long agencyID = this.GetTenantID();
                        var response = this.helperService.SaveHelperDetails(helperData, agencyID);
                        return response;
                    }
                    else
                    {
                        return new EmptyResult();
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.InsertItem, $"SaveHelperDetails/POST : Adding item : Exception  : Exception occurred Adding helper Details. {ex.Message}");
                return this.HandleException(ex, "An error occurred while adding helper. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// Get Helper details.
        /// </summary>
        /// <param name="helperIndex">helperIndex.</param>
        /// <returns>Helper details.</returns>
        [HttpGet]
        [Route("helper/{helperIndex}")]
        public ActionResult<HelperViewResponseDTO> GetHelperInfo(Guid helperIndex)
        {
            try
            {
                var response = this.helperService.GetHelperInfo(helperIndex, this.GetTenantID());
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetHelperInfo/GET : Getting item : Exception  : Exception occurred Getting helper info. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving helper info. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// PUT.
        /// </summary>
        /// <param name="helperData">helperData.</param>
        /// <returns>ActionResult.</returns>
        [HttpPut]
        //[ServiceFilter(typeof(TransactionFilter))]
        [Route("helper")]
        public ActionResult<CRUDResponseDTO> UpdateHelperDetails([FromBody] HelperDetailsDTO helperData)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    int updateUserID = this.GetUserID();
                    long agencyID = this.GetTenantID();

                    helperData.UpdateUserID = updateUserID;
                    var result = this.helperService.UpdateHelperDetails(helperData, agencyID);
                    return result.Result;
                }
                else
                {
                    return new EmptyResult();
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.UpdateItem, $"UpdateHelperDetails/PUT : Updating item : Exception  : Exception occurred updating helper details. {ex.Message}");
                return this.HandleException(ex, "An error occurred updating helper details. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// Remove RemoveHelperDetails.
        /// </summary>
        /// <param name="helperIndex">helperIndex.</param>
        /// <returns>ActionResult.</returns>
        [HttpDelete]
        [Route("helper/{helperIndex}")]
        public ActionResult<CRUDResponseDTO> RemoveHelperDetails(Guid helperIndex)
        {
            try
            {
                var result = this.helperService.RemoveHelperDetails(helperIndex, this.GetTenantID());
                return result;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.DeleteItem, $"RemoveHelperDetails/DELETE : Deleting item : Exception  : Exception occurred while removing helper details. {ex.Message}");
                return this.HandleException(ex, "An error occurred while removing helper details. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetUserDetails.
        /// </summary>
        /// <returns>ActionResult UserDetailsResponseDTO.</returns>
        [HttpGet]
        [Route("helper/userdetails")]
        public ActionResult<UserDetailsResponseDTO> GetUserDetails()
        {
            try
            {
                var response = this.helperService.GetUserDetails(this.GetUserID());
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetUserDetails/GET : Getting item : Exception  : Exception occurred getting user details. {ex.Message}");
                return this.HandleException(ex, "An error occurred getting user details. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetHelperDetails.
        /// </summary>
        /// <param name="helperSearchDTO">helperSearchDTO.</param>
        /// <returns>GetHelperResponseDTO.</returns>
        [HttpPost]
        [Route("helpers/list")]
        public ActionResult<GetHelperResponseDTO> GetHelperDetails([FromBody] HelperSearchDTO helperSearchDTO)
        {
            try
            {
                int? helperID = null;
                var userID = this.GetUserID();
                var userRole = this.GetRole();
                var agencyID = this.GetTenantID();

                helperSearchDTO.userID = userID;
                if (helperSearchDTO.helperIndex == null)
                {
                    var helperData = this.helperService.GetUserDetails(Convert.ToInt32(userID));
                    if (helperData != null && helperData.UserDetails != null)
                    {
                        helperID = helperData.UserDetails.HelperID;
                    }
                }
                else
                {
                    var helperInfo = this.helperService.GetHelperInfo((Guid)helperSearchDTO.helperIndex, agencyID);
                    if (helperInfo != null && helperInfo.HelperDetails != null)
                    {
                        helperSearchDTO.userID = helperInfo.HelperDetails.UserId;
                        helperID = helperInfo.HelperDetails.HelperID;
                        userRole = new List<string>() { helperInfo.HelperDetails.Role };
                        if (helperInfo.HelperDetails.UserId == userID)
                        {
                            helperSearchDTO.IsSameUser = true;
                        }
                    }
                    else
                    {
                        GetHelperResponseDTO emptyResponseDTO = new GetHelperResponseDTO();
                        emptyResponseDTO.HelperData = null;
                        emptyResponseDTO.TotalCount = 0;
                        emptyResponseDTO.ResponseStatus = PCISEnum.StatusMessages.Success;
                        emptyResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                        return emptyResponseDTO;
                    }
                }

                helperSearchDTO.helperID = helperID.HasValue ? helperID.Value : 0;
                helperSearchDTO.userRole = userRole;
                helperSearchDTO.agencyID = agencyID;
                GetHelperResponseDTO response = this.helperService.GetHelperDetailsByHelperID(helperSearchDTO);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetHelperDetails/POST : Getting item : Exception  : Exception occurred getting helper details. {ex.Message}");
                return this.HandleException(ex, "An error occurred getting helper details. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// Set Super Admin Default Agency
        /// </summary>
        /// <param name="agencyId"></param>
        /// <returns> ActionResult<CRUDResponseDTO></returns>
        [HttpPut]
        [Route("helpers/SetSuperAdminDefaultAgency")]
        public ActionResult<CRUDResponseDTO> SetSuperAdminDefaultAgency(UsersDTO usersDTO)
        {
            try
            {
                CRUDResponseDTO returnData = this.helperService.SetSuperAdminDefaultAgency(usersDTO.UserID, usersDTO.AgencyID);
                return returnData;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"SetSuperAdminDefaultAgency/PUT : Setting default agency : Exception  : Exception occurred while setting default agency. {ex.Message}");
                return this.HandleException(ex, "An error occurred while setting default agency. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// Resend Helper Invitation
        /// </summary>
        /// <param name="helperData"></param>
        /// <returns>ActionResult<CRUDResponseDTO></returns>
        [HttpPut]
        [Route("helpers/resendhelperinvitation/{helperIndex}")]
        public ActionResult<CRUDResponseDTO> ResendHelperInvitation(Guid helperIndex)
        {
            try
            {
                var result = this.helperService.ResendHelperPassword(helperIndex, this.GetTenantID());
                return result;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"ResendHelperInvitation/PUT : Resending item : Exception  : Exception occurred during resending helper invitation. {ex.Message}");
                return this.HandleException(ex, "An error occurred during resending helper invitation. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetHelperDetailsByHelperEmail.
        /// </summary>
        /// <param name="importParameterDTO">importParameterDTO.</param>
        /// <returns>HelperDetailsDTO.</returns>
        [HttpPost]
        [Authorize(Policy = "EHRAPIPermission")]
        [Route("helper-list-byemail")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<HelperViewResponseDTO> GetHelperDetailsByHelperEmail(ImportParameterDTO importParameterDTO)
        {
            try
            {
                HelperViewResponseDTO response = this.helperService.GetHelperDetailsByHelperEmail(importParameterDTO.JsonData, importParameterDTO.agencyID);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetHelperDetailsByHelperEmail/POST : Getting item : Exception  : Exception occurred getting helper details by helper email. {ex.Message}");
                return this.HandleException(ex, "An error occurred getting helper details by helper email. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// ImportHelper.
        /// </summary>
        /// <param name="helperDetailsDTO">helperDetailsDTO.</param>
        /// <returns>CRUDResponseDTO.</returns>
        [HttpPost]
        [Authorize(Policy = "EHRAPIPermission")]
        [Route("import-helper")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<CRUDResponseDTO> ImportHelper([FromBody] List<HelperDetailsDTO> helperDetailsDTO)
        {
            try
            {
                CRUDResponseDTO response = this.helperService.ImportHelper(helperDetailsDTO, this.GetTenantID());
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"ImportHelper/POST : Importing item : Exception  : Exception occurred while importing helper. {ex.Message}");
                return this.HandleException(ex, "An error occurred while importing helper. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// ValidateHelperEmail.
        /// </summary>
        /// <param name="importParameterDTO">importParameterDTO.</param>
        /// <returns>EmailValidationResponseDTO.</returns>
        [HttpPost]
        [Authorize(Policy = "EHRAPIPermission")]
        [Route("email-validation")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<EmailValidationResponseDTO> ValidateHelperEmail(ImportParameterDTO importParameterDTO)
        {
            try
            {
                EmailValidationResponseDTO response = this.helperService.ValidateHelperEmail(importParameterDTO.JsonData, importParameterDTO.agencyID);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.TestItem, $"ValidateHelperEmail/POST : Post item : Exception  : Exception occurred during validating helper email. {ex.Message}");
                return this.HandleException(ex, "An error occurred during validating helper email. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetHelperDetailsForRiskNotification.
        /// </summary>
        /// <param name="userId">userId.</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Policy = "EHRAPIPermission")]
        [Route("helper-details/{userId}")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<UserDetailsResponseDTO> GetHelperDetailsForRiskNotification(long userId)
        {
            try
            {
                UserDetailsResponseDTO response = this.helperService.GetUserDetails(userId);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.TestItem, $"GetHelperDetailsForRiskNotification/GET : get item : Exception  : Exception occurred during Get HelperDetails For RiskNotification. {ex.Message}");
                return this.HandleException(ex, "An error occurred during Get HelperDetails For RiskNotification. Please try again later or contact support.");
            }
        }
    }
}