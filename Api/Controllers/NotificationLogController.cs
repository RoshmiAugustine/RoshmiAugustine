// -----------------------------------------------------------------------
// <copyright file="NotificationLogController.cs" company="Naico ITS">
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
    using Opeeka.PICS.Domain.DTO;
    using Opeeka.PICS.Domain.DTO.Output;
    using Opeeka.PICS.Domain.Interfaces.Services;
    using Opeeka.PICS.Infrastructure.Enums;
    using Opeeka.PICS.Infrastructure.Logging;

    /// <summary>
    /// NotificationLogController.
    /// </summary>
    [Route("api")]
    [ApiController]
    public class NotificationLogController : BaseController
    {
        /// Initializes a new instance of the <see cref="notificationLogService"/> class.
        private readonly INotificationLogService notificationLogService;

        /// Initializes a new instance of the <see cref="logger"/> class.
        private readonly ILogger<NotificationLogController> logger;

        /// Initializes a new instance of the <see cref="helperService"/> class.
        private readonly IHelperService helperService;

        /// Initializes a new instance of the <see cref="lookupService"/> class.
        private readonly ILookupService lookupService;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationLogController"/> class.
        /// </summary>
        /// <param name="logger">logger.</param>
        /// <param name="notificationLogService">notificationLogService.</param>
        /// <param name="helperService">helperService.</param>
        public NotificationLogController(ILogger<NotificationLogController> logger, INotificationLogService notificationLogService, IHelperService helperService, ILookupService lookupService)
        {
            this.notificationLogService = notificationLogService;
            this.helperService = helperService;
            this.lookupService = lookupService;
            this.logger = logger;
        }

        /// <summary>
        /// GetNotificationLogList.
        /// </summary>
        /// <param name="notificationLogSearchDTO">notificationLogSearchDTO.</param>
        /// <returns>NotificationLogResponseDTO.</returns>
        [HttpPost]
        [Route("notification-log")]
        public ActionResult<NotificationLogResponseDTO> GetNotificationLogList([FromBody] NotificationLogSearchDTO notificationLogSearchDTO)
        {
            try
            {
                int? helperID = null;
                bool isSameAsLoggedInUser = false;
                var userID = this.GetUserID();
                var userRole = this.GetRole();
                var agencyID = this.GetTenantID();

                notificationLogSearchDTO.UserID = userID;
                if (notificationLogSearchDTO.helperIndex == null)
                {
                    var helperData = this.helperService.GetUserDetails(Convert.ToInt32(userID));
                    if (helperData != null && helperData.UserDetails != null)
                    {
                        helperID = helperData.UserDetails.HelperID;
                    }
                }
                else
                {
                    var helperInfo = this.helperService.GetHelperInfo((Guid)notificationLogSearchDTO.helperIndex, agencyID);
                    if (helperInfo != null && helperInfo.HelperDetails != null)
                    {
                        notificationLogSearchDTO.UserID = helperInfo.HelperDetails.UserId; ;
                        helperID = helperInfo.HelperDetails.HelperID;
                        userRole = new List<string>() { helperInfo.HelperDetails.Role };
                        if (helperInfo.HelperDetails.UserId == userID)
                        {
                            isSameAsLoggedInUser = true;
                        }
                    }
                    else
                    {
                        NotificationLogResponseDTO notificationLogDTO = new NotificationLogResponseDTO();
                        notificationLogDTO.NotificationLog = null;
                        notificationLogDTO.TotalCount = 0;
                        notificationLogDTO.ResponseStatus = PCISEnum.StatusMessages.Success;
                        notificationLogDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                        return notificationLogDTO;
                    }
                }

                notificationLogSearchDTO.isSameAsLoggedInUser = isSameAsLoggedInUser;
                notificationLogSearchDTO.helperID = helperID.HasValue ? helperID.Value : 0;
                notificationLogSearchDTO.userRole = userRole;
                notificationLogSearchDTO.agencyID = agencyID;
                var response = this.notificationLogService.GetNotificationLogList(notificationLogSearchDTO);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.ListItems, $"GetNotificationLogList/POST : Listing notificationLog : Exception  : Exception occurred listing notification log. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving notification log list. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetPastNotificationLogList.
        /// </summary>
        /// <param name="notificationLogSearchDTO">notificationLogSearchDTO.</param>
        /// <returns>NotificationLogResponseDTO.</returns>
        [HttpPost]
        [Route("past-notification-log")]
        public ActionResult<NotificationLogResponseDTO> GetPastNotificationLogList([FromBody] NotificationLogSearchDTO notificationLogSearchDTO)
        {
            try
            {
                int? helperID = null;
                var userID = this.GetUserID();
                var userRole = this.GetRole();
                var agencyID = this.GetTenantID();

                notificationLogSearchDTO.UserID = userID;
                if (notificationLogSearchDTO.helperIndex == null)
                {
                    var helperData = this.helperService.GetUserDetails(Convert.ToInt32(userID));
                    if (helperData != null && helperData.UserDetails != null)
                    {
                        helperID = helperData.UserDetails.HelperID;
                    }
                }
                else
                {
                    var helperInfo = this.helperService.GetHelperInfo((Guid)notificationLogSearchDTO.helperIndex,agencyID);
                    if (helperInfo != null && helperInfo.HelperDetails != null)
                    {
                        notificationLogSearchDTO.UserID = helperInfo.HelperDetails.UserId; ;
                        helperID = helperInfo.HelperDetails.HelperID;
                        userRole = new List<string>() { helperInfo.HelperDetails.Role };
                    }
                    else
                    {
                        NotificationLogResponseDTO notificationLogDTO = new NotificationLogResponseDTO();
                        notificationLogDTO.NotificationLog = null;
                        notificationLogDTO.TotalCount = 0;
                        notificationLogDTO.ResponseStatus = PCISEnum.StatusMessages.Success;
                        notificationLogDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                        return notificationLogDTO;
                    }
                }

                notificationLogSearchDTO.helperID = helperID.HasValue ? helperID.Value : 0;
                notificationLogSearchDTO.userRole = userRole;
                notificationLogSearchDTO.agencyID = agencyID;
                notificationLogSearchDTO.UserID = userID;
                var response = this.notificationLogService.GetPastNotificationLogList(notificationLogSearchDTO);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.ListItems, $"GetPastNotificationLogList/Get : Listing notificationLog : Exception  : Exception occurred listing past notification log. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving past notification log list. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// AddBulkNotificationLog.
        /// </summary>
        /// <param name="notificationLog">notificationLog.</param>
        /// <returns>CRUDResponseDTO.</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpPost]
        [Route("notificationlog")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<CRUDResponseDTO> AddBulkNotificationLog([FromBody] List<NotificationLogDTO> notificationLog)
        {
            try
            {
                var response = this.notificationLogService.AddBulkNotificationLog(notificationLog);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.InsertItem, $"AddBulkNotificationLog/Post : POSt item : Exception  : Exception occurred while insert AddBulkNotificationLog. {ex.Message}");
                return this.HandleException(ex, "An error occurred while Insert AddBulkNotificationLog. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetNotificationLogForNotificationResolveAlert.
        /// </summary>
        /// <param name="personId">personId.</param>
        /// <returns>NotificationLogResponseDTO.</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpGet]
        [Route("notification-log-by-personid/{personId}")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<NotificationLogResponseDTO> GetNotificationLogForNotificationResolveAlert(long personId)
        {
            try
            {
                var response = this.notificationLogService.GetNotificationLogForNotificationResolveAlert(personId);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.InsertItem, $"GetNotificationLogForNotificationResolveAlert/Get : Get item : Exception  : Exception occurred while getting GetNotificationLogForNotificationResolveAlert. {ex.Message}");
                return this.HandleException(ex, "An error occurred while getting GetNotificationLogForNotificationResolveAlert. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetUnResolvedNotificationLogForNotificationResolveAlert.
        /// </summary>
        /// <param name="personId">personId.</param>
        /// <returns>RiskNotificationResponseDTO.</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpGet]
        [Route("unresolved-notification/{personId}")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<RiskNotificationResponseDTO> GetUnResolvedNotificationLogForNotificationResolveAlert(long personId)
        {
            try
            {
                var response = this.notificationLogService.GetUnResolvedNotificationLogForNotificationResolveAlert(personId);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.InsertItem, $"GetUnResolvedNotificationLogForNotificationResolveAlert/Get : Get item : Exception  : Exception occurred while getting GetUnResolvedNotificationLogForNotificationResolveAlert. {ex.Message}");
                return this.HandleException(ex, "An error occurred while getting GetUnResolvedNotificationLogForNotificationResolveAlert. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// UpdateBulkNotificationLog.
        /// </summary>
        /// <param name="notificationLog">notificationLog.</param>
        /// <returns>CRUDResponseDTO.</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpPut]
        [Route("notification-log")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<CRUDResponseDTO> UpdateBulkNotificationLog([FromBody] List<NotificationLogDTO> notificationLog)
        {
            try
            {
                var response = this.notificationLogService.UpdateBulkNotificationLog(notificationLog);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.InsertItem, $"UpdateBulkNotificationLog/Put : Put item : Exception  : Exception occurred while updating BulkNotificationLog. {ex.Message}");
                return this.HandleException(ex, "An error occurred while updating BulkNotificationLog. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// NotificationCountIndication.
        /// </summary>
        /// <returns>NotificationLogResponseDTO.</returns>
        /// Updated the function as part of bug-25307,Bug 25396.
        [HttpGet]
        [Route("notification-count")]
        public ActionResult<NotificationLogResponseDTO> NotificationCountIndication()
        {
            try
            {
               NotificationLogSearchDTO notificationLogSearchDTO = new NotificationLogSearchDTO();
               int? helperID = null;
               var userID = this.GetUserID();
               var userRole = this.GetRole();
               var agencyID = this.GetTenantID();
               if (notificationLogSearchDTO.helperIndex == null)
                {
                    var helperData = this.helperService.GetUserDetails(Convert.ToInt32(userID));
                    if (helperData != null && helperData.UserDetails != null)
                    {
                        helperID = helperData.UserDetails.HelperID;
                    }
                }
                else
                {
                    var helperInfo = this.helperService.GetHelperInfo((Guid)notificationLogSearchDTO.helperIndex);
                    if (helperInfo != null && helperInfo.HelperDetails != null)
                    {
                        helperID = helperInfo.HelperDetails.HelperID;
                        userRole = new List<string>() { helperInfo.HelperDetails.Role };
                    }
                    else
                    {
                        NotificationLogResponseDTO notificationLogDTO = new NotificationLogResponseDTO();
                        notificationLogDTO.NotificationLog = null;
                        notificationLogDTO.TotalCount = 0;
                        notificationLogDTO.ResponseStatus = PCISEnum.StatusMessages.Success;
                        notificationLogDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                        return notificationLogDTO;
                    }
                }

               notificationLogSearchDTO.helperID = helperID.HasValue ? helperID.Value : 0;
               notificationLogSearchDTO.userRole = userRole;
               notificationLogSearchDTO.agencyID = agencyID;
               notificationLogSearchDTO.UserID = userID;
               var response = this.notificationLogService.NotificationCountIndication(notificationLogSearchDTO, userID);
               return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.InsertItem, $"NotificationCountIndication/Get : Get item : Exception  : Exception occurred while getting NotificationCountIndication. {ex.Message}");
                return this.HandleException(ex, "An error occurred while getting NotificationCountIndication. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// AddReminderInviteDetails.
        /// </summary>
        /// <param name="inviteDetails">inviteDetails.</param>
        /// <returns>CRUDResponseDTO.</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpPost]
        [Route("reminder-invite-details")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<CRUDResponseDTO> AddReminderInviteDetails([FromBody] List<ReminderInviteToCompleteDTO> inviteDetails)
        {
            try
            {
                var response = this.lookupService.AddReminderInviteDetails(inviteDetails);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.InsertItem, $"AddReminderInviteDetails/POST : Adding item : Exception  : Exception occurred AddReminderInviteDetails. {ex.Message}");
                return this.HandleException(ex, "An error occurred AddReminderInviteDetails. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetReminderInviteDetails.
        /// </summary>
        /// <returns>ReminderInviteToCompleteResponseDTO.</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpGet]
        [Route("reminder-invite-details")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<ReminderInviteToCompleteResponseDTO> GetReminderInviteDetails()
        {
            try
            {
                ReminderInviteToCompleteResponseDTO response = this.lookupService.GetReminderInviteDetails();
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.InsertItem, $"GetReminderInviteDetails/POST : Adding item : Exception  : Exception occurred GetSMSDetails. {ex.Message}");
                return this.HandleException(ex, "An error occurred GetReminderInviteDetails. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// UpdateReminderInvite.
        /// </summary>
        /// <param name="inviteDetails">inviteDetails.</param>
        /// <returns>CRUDResponseDTO.</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpPut]
        [Route("reminder-invite-details")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<CRUDResponseDTO> UpdateReminderInvite([FromBody] List<ReminderInviteToCompleteDTO> inviteDetails)
        {
            try
            {
                var response = this.lookupService.UpdateReminderInviteDetails(inviteDetails);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.UpdateItem, $"UpdateReminderInvite/put : Updating item : Exception  : Exception occurred UpdateReminderInvite. {ex.Message}");
                return this.HandleException(ex, "An error occurred UpdateReminderInvite. Please try again later or contact support.");
            }
        }
    }
}