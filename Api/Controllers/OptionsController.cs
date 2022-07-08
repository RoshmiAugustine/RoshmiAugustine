// -----------------------------------------------------------------------
// <copyright file="OptionsController.cs" company="Naico ITS">
// Copyright (c) Naico ITS. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Api.Controllers
{
    using System;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Opeeka.PICS.Domain.DTO;
    using Opeeka.PICS.Domain.DTO.Input;
    using Opeeka.PICS.Domain.DTO.Output;
    using Opeeka.PICS.Domain.Interfaces.Services;
    using Opeeka.PICS.Infrastructure.Logging;

    /// <summary>
    /// OptionsController.
    /// </summary>
    [Route("api")]
    [ApiController]
    public class OptionsController : BaseController
    {
        /// Initializes a new instance of the <see cref="optionsService"/> class.
        private readonly IOptionsService optionsService;

        /// Initializes a new instance of the <see cref="logger"/> class.
        private readonly ILogger<OptionsController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="OptionsController"/> class.
        /// </summary>
        /// <param name="logger">logger.</param>
        /// <param name="optionsService">optionsService.</param>
        public OptionsController(ILogger<OptionsController> logger, IOptionsService optionsService)
        {
            this.optionsService = optionsService;
            this.logger = logger;
        }

        /// <summary>
        /// GetCollaborationLevelList.
        /// </summary>
        /// <param name="pageNumber">page Number.</param>
        /// <param name="pageSize"> page Size.</param>
        /// <returns>CollaborationLevelResponseDTO.</returns>
        [HttpGet]
        [Route("collaboration-level/{pageNumber}/{pageSize}")]
        public ActionResult<CollaborationLevelResponseDTO> GetCollaborationLevelList(int pageNumber, int pageSize)
        {
            try
            {
                var response = this.optionsService.GetCollaborationLevelList(pageNumber, pageSize, this.GetTenantID());
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.ListItems, $"GetCollaborationLevelList/Get : Listing Collaboration Level : Exception  : Exception occurred getting collaboration level list. {ex.Message}");
                return this.HandleException(ex, "An error occurred rgetting collaboration level list. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// AddCollaborationLevel.
        /// </summary>
        /// <param name="collaborationLevelData">CollaborationLevelDTO.</param>
        /// <returns>CRUDResponseDTO.</returns>
        [HttpPost]
        [Route("collaboration-level")]
        public ActionResult<CRUDResponseDTO> AddCollaborationLevel([FromBody] CollaborationLevelDTO collaborationLevelData)
        {
            try
            {
                if (collaborationLevelData != null)
                {
                    if (this.ModelState.IsValid)
                    {
                        var response = this.optionsService.AddCollaborationLevel(collaborationLevelData, this.GetUserID(), this.GetTenantID());
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
                this.logger.LogError(MyLogEvents.InsertItem, $"AddCollaborationLevel/Post : Adding Collaboration Level : Exception  : Exception occurred Adding Collaboration Level. {ex.Message}");
                return this.HandleException(ex, "An error occurred adding Collaboration Level. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// UpdateCollaborationLevel.
        /// </summary>
        /// <param name="collaborationLevelData">collaborationLevelData.</param>
        /// <returns>ActionResult.<CRUDResponseDTO>.</returns>
        [HttpPut]
        [Route("collaboration-level")]
        public ActionResult<CRUDResponseDTO> UpdateCollaborationLevel([FromBody] CollaborationLevelDTO collaborationLevelData)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var response = this.optionsService.UpdateCollaborationLevel(collaborationLevelData, this.GetUserID(), this.GetTenantID());
                    return response;
                }
                else
                {
                    return new EmptyResult();
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.UpdateItem, $"UpdateCollaborationLevel/Put : Updating Collaboration Level : Exception  : Exception occurred Updating Collaboration Level. {ex.Message}");
                return this.HandleException(ex, "An error occurred updating Collaboration Level. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// DeleteCollaborationLevel.
        /// </summary>
        /// <param name="collaborationlevelID">collaborationlevelID.</param>
        /// <returns>ActionResult.<CRUDResponseDTO>.</returns>
        [HttpDelete]
        [Route("collaboration-level/{collaborationlevelID}")]
        public ActionResult<CRUDResponseDTO> DeleteCollaborationLevel(int collaborationlevelID)
        {
            try
            {
                var result = this.optionsService.DeleteCollaborationLevel(collaborationlevelID,this.GetTenantID());
                return result;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.DeleteItem, $"DeleteCollaborationLevel/Delete : Deleting Collaboration Level : Exception  : Exception occurred Deleting Collaboration Level. {ex.Message}");
                return this.HandleException(ex, "An error occurred deleting Collaboration Level. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// Get the Collaboration Tag Types list paginated.
        /// </summary>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <returns>CollaborationTagTypeListResponseDTO.</returns>
        [HttpGet]
        [Route("collaboration-tag-type/{pageNumber}/{pageSize}")]
        public ActionResult<CollaborationTagTypeListResponseDTO> GetCollaborationTagTypeList(int pageNumber, int pageSize)
        {
            try
            {
                var response = this.optionsService.GetCollaborationTagTypeList(pageNumber, pageSize, this.GetTenantID());
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.ListItems, $"GetCollaborationTagTypeList/Get : Listing collaboration tag type : Exception  : Exception occurred getting collaboration tag type. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving collaboration tag type. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// Add a new Collaboration Tag Type.
        /// </summary>
        /// <param name="collaborationTagTypeData">collaborationTagTypeData.</param>
        /// <returns>CRUDResponseDTO.</returns>
        [HttpPost]
        [Route("collaboration-tag-type")]
        public ActionResult<CRUDResponseDTO> AddCollaborationTagType([FromBody] CollaborationTagTypeDetailsDTO collaborationTagTypeData)
        {
            try
            {
                if (collaborationTagTypeData != null)
                {
                    if (this.ModelState.IsValid)
                    {
                        var response = this.optionsService.AddCollaborationTagType(collaborationTagTypeData, this.GetUserID(), this.GetTenantID());
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
                this.logger.LogError(MyLogEvents.InsertItem, $"AddCollaborationTagType/Post : Adding collaboration tag type : Exception  : Exception occurred adding collaboration tag type. {ex.Message}");
                return this.HandleException(ex, "An error occurred adding collaboration tag types. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// Update an existing Collaboration Tag Type.
        /// </summary>
        /// <param name="collaborationTagTypeData">collaborationTagTypeData.</param>
        /// <returns>CRUDResponseDTO.</returns>
        [HttpPut]
        [Route("collaboration-tag-type")]
        public ActionResult<CRUDResponseDTO> UpdateCollaborationTagType([FromBody] CollaborationTagTypeDetailsDTO collaborationTagTypeData)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var response = this.optionsService.UpdateCollaborationTagType(collaborationTagTypeData, this.GetUserID(), this.GetTenantID());
                    return response;
                }
                else
                {
                    return new EmptyResult();
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.UpdateItem, $"UpdateCollaborationTagType/Put : Updating collaboration tag type : Exception  : Exception occurred updating collaboration tag type. {ex.Message}");
                return this.HandleException(ex, "An error occurred updating collaboration tag type. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// Delete an existing Collaboration Tag Type.
        /// </summary>
        /// <param name="collaborationTagTypeID">collaborationTagTypeID.</param>
        /// <returns>CRUDResponseDTO.</returns>
        [HttpDelete]
        [Route("collaboration-tag-type/{collaborationTagTypeID}")]
        public ActionResult<CRUDResponseDTO> DeleteCollaborationTagType(int collaborationTagTypeID)
        {
            try
            {
                var result = this.optionsService.DeleteCollaborationTagType(collaborationTagTypeID, this.GetTenantID());
                return result;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.DeleteItem, $"DeleteCollaborationTagType/Delete : Deleting collaborationTagType : Exception  : Exception occurred deleting collaboration tag type. {ex.Message}");
                return this.HandleException(ex, "An error occurred deleting collaboration tag type. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetTherapyTypeList.
        /// </summary>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <returns>Action result.</returns>
        [HttpGet]
        [Route("therapy-type/{pageNumber}/{pageSize}")]
        public ActionResult<TherapyTypesResponseDTO> GetTherapyTypeList(int pageNumber, int pageSize)
        {
            try
            {
                var response = this.optionsService.GetTherapyTypeList(pageNumber, pageSize, this.GetTenantID());
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.ListItems, $"GetTherapyTypeList/Get : Listing therapy type : Exception  : Exception occurred listing therapy type. {ex.Message}");
                return this.HandleException(ex, "An error occurred listing therapy type. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// Add a new Therapy Type.
        /// </summary>
        /// <param name="therapyTypeData">therapyTypeData.</param>
        /// <returns>CRUDResponseDTO.</returns>
        [HttpPost]
        [Route("therapy-type")]
        public ActionResult<CRUDResponseDTO> AddTherapyType([FromBody] TherapyTypeDetailsDTO therapyTypeData)
        {
            try
            {
                if (therapyTypeData != null)
                {
                    if (this.ModelState.IsValid)
                    {
                        var response = this.optionsService.AddTherapyType(therapyTypeData, this.GetUserID(), this.GetTenantID());
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
                this.logger.LogError(MyLogEvents.InsertItem, $"AddTherapyType/Post : Adding therapy type : Exception  : Exception occurred adding therapy type. {ex.Message}");
                return this.HandleException(ex, "An error occurred while adding therapy type. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// Update an existing Therapy Type.
        /// </summary>
        /// <param name="therapyTypeData">therapyTypeData.</param>
        /// <returns>CRUDResponseDTO.</returns>
        [HttpPut]
        [Route("therapy-type")]
        public ActionResult<CRUDResponseDTO> UpdateTherapyType([FromBody] TherapyTypeDetailsDTO therapyTypeData)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var response = this.optionsService.UpdateTherapyType(therapyTypeData, this.GetUserID(), this.GetTenantID());
                    return response;
                }
                else
                {
                    return new EmptyResult();
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.UpdateItem, $"UpdateTherapyType/Put : Updating therapy type : Exception  : Exception occurred while updating therapy type. {ex.Message}");
                return this.HandleException(ex, "An error occurred while updating therapy type. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// Delete an existing Therapy Type.
        /// </summary>
        /// <param name="therapyTypeID">therapyTypeID.</param>
        /// <returns>CRUDResponseDTO.</returns>
        [HttpDelete]
        [Route("therapy-type/{therapyTypeID}")]
        public ActionResult<CRUDResponseDTO> DeleteTherapyType(int therapyTypeID)
        {
            try
            {
                var result = this.optionsService.DeleteTherapyType(therapyTypeID, this.GetTenantID());
                return result;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.DeleteItem, $"DeleteTherapyType/Delete : Deleting therapyType : Exception  : Exception occurred deleting therapy type. {ex.Message}");
                return this.HandleException(ex, "An error occurred deleting therapy type. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// Get the Helper title list paginated.
        /// </summary>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <returns>HelperTitleListResponseDTO.</returns>
        [HttpGet]
        [Route("helper-title/{pageNumber}/{pageSize}")]
        public ActionResult<HelperTitleResponseDTO> GetHelperTitleList(int pageNumber, int pageSize)
        {
            try
            {
                var response = this.optionsService.GetHelperTitleList(pageNumber, pageSize, this.GetTenantID());
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.ListItems, $"GetHelperTitleList/Get : Listing helper title : Exception  : Exception occurred getting helper title list. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving helper title list. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// POST.
        /// </summary>
        /// <param name="helperTitleData">helperTitleData.</param>
        /// <returns>CRUDResponseDTO.</returns>
        [HttpPost]
        [Route("helper-title")]
        public ActionResult<CRUDResponseDTO> AddHelperTitle([FromBody] HelperTitleDetailsDTO helperTitleData)
        {
            try
            {
                if (helperTitleData != null)
                {
                    if (this.ModelState.IsValid)
                    {
                        var response = this.optionsService.AddHelperTitle(helperTitleData, this.GetUserID(), this.GetTenantID());
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
                this.logger.LogError(MyLogEvents.InsertItem, $"AddHelperTitle/Post : Adding helper title : Exception  : Exception occurred adding helper title. {ex.Message}");
                return this.HandleException(ex, "An error occurred adding helper title. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// PUT.
        /// </summary>
        /// <param name="helperTitleData">helperTitleData.</param>
        /// <returns>CRUDResponseDTO.</returns>
        [HttpPut]
        [Route("helper-title")]
        public ActionResult<CRUDResponseDTO> UpdateHelperTitle([FromBody] HelperTitleDetailsDTO helperTitleData)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var response = this.optionsService.UpdateHelperTitle(helperTitleData, this.GetUserID(), this.GetTenantID());
                    return response;
                }
                else
                {
                    return new EmptyResult();
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.UpdateItem, $"UpdateHelperTitle/Put : Updating helper title : Exception  : Exception occurred updating helper title. {ex.Message}");
                return this.HandleException(ex, "An error occurred updating helper title. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// DeleteHelperTitle.
        /// </summary>
        /// <param name="helperTitleID">helperTitleID.</param>
        /// <returns>CRUDResponseDTO.</returns>
        [HttpDelete]
        [Route("helper-title/{helperTitleID}")]
        public ActionResult<CRUDResponseDTO> DeleteHelperTitle(int helperTitleID)
        {
            try
            {
                var result = this.optionsService.DeleteHelperTitle(helperTitleID, this.GetTenantID());
                return result;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.DeleteItem, $"DeleteHelperTitle/Delete : Deleting helperTitle : Exception  : Exception occurred deleting helper title. {ex.Message}");
                return this.HandleException(ex, "An error occurred deleting helper title. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetNotificationLevelList.
        /// </summary>
        /// <param name="pageNumber">page Number.</param>
        /// <param name="pageSize"> page Size.</param>
        /// <returns>NotificationLevelResponseDTO.</returns>
        [HttpGet]
        [Route("notification-level/{pageNumber}/{pageSize}")]
        public ActionResult<NotificationLevelResponseDTO> GetNotificationLevelList(int pageNumber, int pageSize)
        {
            try
            {
                var response = this.optionsService.GetNotificationLevelList(pageNumber, pageSize, this.GetTenantID());
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.ListItems, $"GetNotificationLevelList/Get : Listing Notification Level : Exception  : Exception occurred listing notification level list. {ex.Message}");
                return this.HandleException(ex, "An error occurred listing notification level list. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// AddNotificationLevel.
        /// </summary>
        /// <param name="notificationLevelData">NotificationLevelDTO.</param>
        /// <returns>CRUDResponseDTO.</returns>
        [HttpPost]
        [Route("notification-level")]
        public ActionResult<CRUDResponseDTO> AddNotificationLevel([FromBody] NotificationLevelDTO notificationLevelData)
        {
            try
            {
                if (notificationLevelData != null)
                {
                    if (this.ModelState.IsValid)
                    {
                        var response = this.optionsService.AddNotificationLevel(notificationLevelData, this.GetUserID(), this.GetTenantID());
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
                this.logger.LogError(MyLogEvents.InsertItem, $"AddNotificationLevel/Post : Adding Notification Level : Exception  : Exception occurred adding notification level. {ex.Message}");
                return this.HandleException(ex, "An error occurred adding notification level. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// UpdateNotificationLevel.
        /// </summary>
        /// <param name="notificationLevelData">NotificationLevelData.</param>
        /// <returns>ActionResult.<CRUDResponseDTO>.</returns>
        [HttpPut]
        [Route("notification-level")]
        public ActionResult<CRUDResponseDTO> UpdateNotificationLevel([FromBody] NotificationLevelDTO notificationLevelData)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var response = this.optionsService.UpdateNotificationLevel(notificationLevelData, this.GetUserID(), this.GetTenantID());
                    return response;
                }
                else
                {
                    return new EmptyResult();
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.UpdateItem, $"UpdateNotificationLevel/Put : Updating Notification Level : Exception  : Exception occurred updating notification level. {ex.Message}");
                return this.HandleException(ex, "An error occurred updating notification level. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// DeleteNotificationLevel.
        /// </summary>
        /// <param name="notificationLevelID">NotificationLevelID.</param>
        /// <returns>ActionResult.<CRUDResponseDTO>.</returns>
        [HttpDelete]
        [Route("notification-level/{notificationLevelID}")]
        public ActionResult<CRUDResponseDTO> DeleteNotificationLevel(int notificationLevelID)
        {
            try
            {
                var result = this.optionsService.DeleteNotificationLevel(notificationLevelID, this.GetTenantID());
                return result;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.DeleteItem, $"DeleteNotificationLevel/Delete : Deleting Notification Level : Exception  : Exception occurred deleting notification level. {ex.Message}");
                return this.HandleException(ex, "An error occurred deleting notification level. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetGenderList.
        /// </summary>
        /// <param name="pageNumber">page Number.</param>
        /// <param name="pageSize"> page Size.</param>
        /// <returns>GenderResponseDTO.</returns>
        [HttpGet]
        [Route("gender/{pageNumber}/{pageSize}")]
        public ActionResult<GenderResponseDTO> GetGenderList(int pageNumber, int pageSize)
        {
            try
            {
                var response = this.optionsService.GetGenderList(pageNumber, pageSize, this.GetTenantID());
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.ListItems, $"GetGenderList/Get : Listing Gender : Exception  : Exception occurred getting gender list. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving gender list. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// AddGender.
        /// </summary>
        /// <param name="genderData">GenderDTO.</param>
        /// <returns>CRUDResponseDTO.</returns>
        [HttpPost]
        [Route("gender")]
        public ActionResult<CRUDResponseDTO> AddGender([FromBody] GenderDTO genderData)
        {
            try
            {
                if (genderData != null)
                {
                    if (this.ModelState.IsValid)
                    {
                        var response = this.optionsService.AddGender(genderData, this.GetUserID(), this.GetTenantID());
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
                this.logger.LogError(MyLogEvents.InsertItem, $"AddGender/Post : Adding Gender : Exception  : Exception occurred adding gender. {ex.Message}");
                return this.HandleException(ex, "An error occurred adding gender. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// UpdateGender.
        /// </summary>
        /// <param name="genderData">GenderData.</param>
        /// <returns>ActionResult.<CRUDResponseDTO>.</returns>
        [HttpPut]
        [Route("gender")]
        public ActionResult<CRUDResponseDTO> UpdateGender([FromBody] GenderDTO genderData)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var response = this.optionsService.UpdateGender(genderData, this.GetUserID(), this.GetTenantID());
                    return response;
                }
                else
                {
                    return new EmptyResult();
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.UpdateItem, $"UpdateGender/Put : Updating Gender : Exception  : Exception occurred updating gender. {ex.Message}");
                return this.HandleException(ex, "An error occurred updating gender. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// DeleteGender.
        /// </summary>
        /// <param name="genderID">GenderID.</param>
        /// <returns>ActionResult.<CRUDResponseDTO>.</returns>
        [HttpDelete]
        [Route("gender/{genderID}")]
        public ActionResult<CRUDResponseDTO> DeleteGender(int genderID)
        {
            try
            {
                var result = this.optionsService.DeleteGender(genderID, this.GetTenantID());
                return result;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.DeleteItem, $"DeleteGender/Delete : Deleting Gender : Exception  : Exception occurred deleting gender. {ex.Message}");
                return this.HandleException(ex, "An error occurred deleting gender. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetIdentificationTypeList.
        /// </summary>
        /// <param name="pageNumber">page Number.</param>
        /// <param name="pageSize"> page Size.</param>
        /// <returns>IdentificationTypeResponseDTO.</returns>
        [HttpGet]
        [Route("identification-type/{pageNumber}/{pageSize}")]
        public ActionResult<IdentificationTypesResponseDTO> GetIdentificationTypeList(int pageNumber, int pageSize)
        {
            try
            {
                var response = this.optionsService.GetIdentificationTypeList(pageNumber, pageSize, this.GetTenantID());
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.ListItems, $"GetIdentificationTypeList/Get : Listing Identification Type : Exception  : Exception occurred listing identification type. {ex.Message}");
                return this.HandleException(ex, "An error occurred listing identification type. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// AddIdentificationType.
        /// </summary>
        /// <param name="identificationTypeData">IdentificationTypeDTO.</param>
        /// <returns>CRUDResponseDTO.</returns>
        [HttpPost]
        [Route("identification-type")]
        public ActionResult<CRUDResponseDTO> AddIdentificationType([FromBody] IdentificationTypeDTO identificationTypeData)
        {
            try
            {
                if (identificationTypeData != null)
                {
                    if (this.ModelState.IsValid)
                    {
                        var response = this.optionsService.AddIdentificationType(identificationTypeData, this.GetUserID(), this.GetTenantID());
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
                this.logger.LogError(MyLogEvents.InsertItem, $"AddIdentificationType/Post : Adding Identification Type : Exception  : Exception occurred adding identification type. {ex.Message}");
                return this.HandleException(ex, "An error occurred adding identification type. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// UpdateIdentificationType.
        /// </summary>
        /// <param name="identificationTypeData">IdentificationTypeData.</param>
        /// <returns>ActionResult.<CRUDResponseDTO>.</returns>
        [HttpPut]
        [Route("identification-type")]
        public ActionResult<CRUDResponseDTO> UpdateIdentificationType([FromBody] IdentificationTypeDTO identificationTypeData)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var response = this.optionsService.UpdateIdentificationType(identificationTypeData, this.GetUserID(), this.GetTenantID());
                    return response;
                }
                else
                {
                    return new EmptyResult();
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.UpdateItem, $"UpdateIdentificationType/Put : Updating Identification Type : Exception  : Exception occurred updating identification type. {ex.Message}");
                return this.HandleException(ex, "An error occurred updating identification type. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// DeleteIdentificationType.
        /// </summary>
        /// <param name="identificationTypeID">IdentificationTypeID.</param>
        /// <returns>ActionResult.<CRUDResponseDTO>.</returns>
        [HttpDelete]
        [Route("identification-type/{identificationTypeID}")]
        public ActionResult<CRUDResponseDTO> DeleteIdentificationType(long identificationTypeID)
        {
            try
            {
                var result = this.optionsService.DeleteIdentificationType(identificationTypeID, this.GetTenantID());
                return result;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.DeleteItem, $"DeleteIdentificationType/Delete : Deleting Identification Type : Exception  : Exception occurred deleting identification type. {ex.Message}");
                return this.HandleException(ex, "An error occurred deleting identification type. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetRaceEthnicityList.
        /// </summary>
        /// <param name="pageNumber">page Number.</param>
        /// <param name="pageSize"> page Size.</param>
        /// <returns>RaceEthnicityResponseDTO.</returns>
        [HttpGet]
        [Route("race-ethnicity/{pageNumber}/{pageSize}")]
        public ActionResult<RaceEthnicityResponseDTO> GetRaceEthnicityList(int pageNumber, int pageSize)
        {
            try
            {
                var response = this.optionsService.GetRaceEthnicityList(pageNumber, pageSize, this.GetTenantID());
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.ListItems, $"GetRaceEthnicityList/Get : Listing Race Ethnicity : Exception  : Exception occurred listing race ethnicity. {ex.Message}");
                return this.HandleException(ex, "An error occurred listing race ethnicity. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// AddRaceEthnicity.
        /// </summary>
        /// <param name="raceEthnicityData">RaceEthnicityDTO.</param>
        /// <returns>CRUDResponseDTO.</returns>
        [HttpPost]
        [Route("race-ethnicity")]
        public ActionResult<CRUDResponseDTO> AddRaceEthnicity([FromBody] RaceEthnicityDTO raceEthnicityData)
        {
            try
            {
                if (raceEthnicityData != null)
                {
                    if (this.ModelState.IsValid)
                    {
                        var response = this.optionsService.AddRaceEthnicity(raceEthnicityData, this.GetUserID(), this.GetTenantID());
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
                this.logger.LogError(MyLogEvents.InsertItem, $"AddRaceEthnicity/Post : Adding Race Ethnicity  : Exception  : Exception occurred adding race ethnicity. {ex.Message}");
                return this.HandleException(ex, "An error occurred adding race ethnicity. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// UpdateRaceEthnicity.
        /// </summary>
        /// <param name="raceEthnicityData">RaceEthnicityData.</param>
        /// <returns>ActionResult.<CRUDResponseDTO>.</returns>
        [HttpPut]
        [Route("race-ethnicity")]
        public ActionResult<CRUDResponseDTO> UpdateRaceEthnicity([FromBody] RaceEthnicityDTO raceEthnicityData)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var response = this.optionsService.UpdateRaceEthnicity(raceEthnicityData, this.GetUserID(), this.GetTenantID());
                    return response;
                }
                else
                {
                    return new EmptyResult();
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.UpdateItem, $"UpdateRaceEthnicity/Put : Updating Race Ethnicity : Exception  : Exception occurred updating race ethnicity. {ex.Message}");
                return this.HandleException(ex, "An error occurred updating race ethnicity. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// DeleteRaceEthnicity.
        /// </summary>
        /// <param name="raceEthnicityID">RaceEthnicityID.</param>
        /// <returns>ActionResult.<CRUDResponseDTO>.</returns>
        [HttpDelete]
        [Route("race-ethnicity/{raceEthnicityID}")]
        public ActionResult<CRUDResponseDTO> DeleteRaceEthnicity(long raceEthnicityID)
        {
            try
            {
                var result = this.optionsService.DeleteRaceEthnicity(raceEthnicityID, this.GetTenantID());
                return result;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.DeleteItem, $"DeleteRaceEthnicity/Delete : Deleting Race Ethnicity : Exception  : Exception occurred deleting race ethnicity. {ex.Message}");
                return this.HandleException(ex, "An error occurred deleting race ethnicity. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetSupportTypeList.
        /// </summary>
        /// <param name="pageNumber">page Number.</param>
        /// <param name="pageSize"> page Size.</param>
        /// <returns>SupportTypeResponseDTO.</returns>
        [HttpGet]
        [Route("support-type/{pageNumber}/{pageSize}")]
        public ActionResult<SupportTypeResponseDTO> GetSupportTypeList(int pageNumber, int pageSize)
        {
            try
            {
                var response = this.optionsService.GetSupportTypeList(pageNumber, pageSize, this.GetTenantID());
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.ListItems, $"GetSupportTypeList/Get : Listing Support Type : Exception  : Exception occurred listing support type. {ex.Message}");
                return this.HandleException(ex, "An error occurred listing support type. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// AddSupportType.
        /// </summary>
        /// <param name="supportTypeData">SupportTypeDTO.</param>
        /// <returns>CRUDResponseDTO.</returns>
        [HttpPost]
        [Route("support-type")]
        public ActionResult<CRUDResponseDTO> AddSupportType([FromBody] SupportTypeDTO supportTypeData)
        {
            try
            {
                if (supportTypeData != null)
                {
                    if (this.ModelState.IsValid)
                    {
                        var response = this.optionsService.AddSupportType(supportTypeData, this.GetUserID(), this.GetTenantID());
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
                this.logger.LogError(MyLogEvents.InsertItem, $"AddSupportType/Post : Adding Support Type : Exception  : Exception occurred adding support type. {ex.Message}");
                return this.HandleException(ex, "An error occurred adding support type. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// UpdateSupportType.
        /// </summary>
        /// <param name="supportTypeData">SupportTypeData.</param>
        /// <returns>ActionResult.<CRUDResponseDTO>.</returns>
        [HttpPut]
        [Route("support-type")]
        public ActionResult<CRUDResponseDTO> UpdateSupportType([FromBody] SupportTypeDTO supportTypeData)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var response = this.optionsService.UpdateSupportType(supportTypeData, this.GetUserID(), this.GetTenantID());
                    return response;
                }
                else
                {
                    return new EmptyResult();
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.UpdateItem, $"UpdateSupportType/Put : Updating Support Type : Exception  : Exception occurred updating support type. {ex.Message}");
                return this.HandleException(ex, "An error occurred updating support type. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// DeleteSupportType.
        /// </summary>
        /// <param name="supportTypeID">SupportTypeID.</param>
        /// <returns>ActionResult.<CRUDResponseDTO>.</returns>
        [HttpDelete]
        [Route("support-type/{supportTypeID}")]
        public ActionResult<CRUDResponseDTO> DeleteSupportType(long supportTypeID)
        {
            try
            {
                var result = this.optionsService.DeleteSupportType(supportTypeID, this.GetTenantID());
                return result;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.DeleteItem, $"DeleteSupportType/Delete : Deleting Support Type : Exception  : Exception occurred deleting support type. {ex.Message}");
                return this.HandleException(ex, "An error occurred deleting support type. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// UpdateSexuality.
        /// </summary>
        /// <param name="EditSexualityDTO">EditSexualityDTO.</param>
        /// <returns>ActionResult.<CRUDResponseDTO>.</returns>
        [HttpPut]
        [Route("sexuality")]
        public ActionResult<CRUDResponseDTO> UpdateSexuality([FromBody] EditSexualityDTO editSexualityDTO)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var response = this.optionsService.UpdateSexuality(editSexualityDTO, this.GetUserID(), this.GetTenantID());
                    return response;
                }
                else
                {
                    return new EmptyResult();
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.UpdateItem, $"UpdateSexuality/Put : Updating sexuality : Exception  : Exception occurred updating sexuality. {ex.Message}");
                return this.HandleException(ex, "An error occurred updating sexuality. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// DeleteSexuality.
        /// </summary>
        /// <param name="sexualityId">sexualityId.</param>
        /// <returns>ActionResult.<CRUDResponseDTO>.</returns>
        [HttpDelete]
        [Route("sexuality/{sexualityId}")]
        public ActionResult<CRUDResponseDTO> DeleteSexuality(int sexualityId)
        {
            try
            {
                var result = this.optionsService.RemoveSexuality(sexualityId, this.GetTenantID());
                return result;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.DeleteItem, $"DeleteSexuality/Delete : Deleting sexuality : Exception  : Exception occurred deleting sexuality. {ex.Message}");
                return this.HandleException(ex, "An error occurred deleting sexuality. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// AddSexuality.
        /// </summary>
        /// <param name="sexualityData">sexualityData.</param>
        /// <returns>CRUDResponseDTO.</returns>
        [HttpPost]
        [Route("sexuality")]
        public ActionResult<CRUDResponseDTO> AddSexuality([FromBody] SexualityInputDTO sexualityData)
        {
            try
            {
                if (sexualityData != null)
                {
                    if (this.ModelState.IsValid)
                    {
                        var response = this.optionsService.AddSexuality(sexualityData, this.GetTenantID(), this.GetUserID());
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
                this.logger.LogError(MyLogEvents.InsertItem, $"AddSexuality/Post : Adding Sexuality : Exception  : Exception occurred adding sexuality. {ex.Message}");
                return this.HandleException(ex, "An error occurred adding sexuality. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetSexualityList.
        /// </summary>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <returns>SexualityResponseDTO.</returns>
        [HttpGet]
        [Route("sexuality/{pageNumber}/{pageSize}")]
        public ActionResult<SexualityResponseDTO> GetSexualityList(int pageNumber, int pageSize)
        {
            try
            {
                var response = this.optionsService.GetSexualityList(pageNumber, pageSize, this.GetTenantID());
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.ListItems, $"GetSexualityList/Get : Listing Sexuality : Exception  : Exception occurred listing sexuality. {ex.Message}");
                return this.HandleException(ex, "An error occurred listing sexuality. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetIdentifiedGenderList.
        /// </summary>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <returns>IdentifiedGenderResponseDTO.</returns>
        [HttpGet]
        [Route("identified-gender/{pageNumber}/{pageSize}")]
        public ActionResult<IdentifiedGenderResponseDTO> GetIdentifiedGenderList(int pageNumber, int pageSize)
        {
            try
            {
                IdentifiedGenderResponseDTO response = this.optionsService.GetIdentifiedGenderList(pageNumber, pageSize, this.GetTenantID());
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.ListItems, $"GetIdentifiedGenderList/Get : Listing identified gender : Exception  : Exception occurred getting identified gender list. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving identified gender list. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// AddIdentifiedGender.
        /// </summary>
        /// <param name="identifiedGenderData">identifiedGenderData.</param>
        /// <returns>CRUDResponseDTO.</returns>
        [HttpPost]
        [Route("identified-gender")]
        public ActionResult<CRUDResponseDTO> AddIdentifiedGender([FromBody] IdentifiedGenderDTO identifiedGenderData)
        {
            try
            {
                if (identifiedGenderData != null)
                {
                    if (this.ModelState.IsValid)
                    {
                        CRUDResponseDTO response = this.optionsService.AddIdentifiedGender(identifiedGenderData, this.GetUserID(), this.GetTenantID());
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
                this.logger.LogError(MyLogEvents.InsertItem, $"AddIdentifiedGender/Post : Adding  identified gender : Exception  : Exception occurred adding identified gender. {ex.Message}");
                return this.HandleException(ex, "An error occurred while adding  identified gender. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// UpdateIdentifiedGender.
        /// </summary>
        /// <param name="identifiedGenderData">IdentifiedGenderData.</param>
        /// <returns>ActionResult.<CRUDResponseDTO>.</returns>
        [HttpPut]
        [Route("identified-gender")]
        public ActionResult<CRUDResponseDTO> UpdateIdentifiedGender([FromBody] IdentifiedGenderDTO identifiedGenderData)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var response = this.optionsService.UpdateIdentifiedGender(identifiedGenderData, this.GetUserID(), this.GetTenantID());
                    return response;
                }
                else
                {
                    return new EmptyResult();
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.UpdateItem, $"UpdateIdentifiedGender/Put : Updating  identified gender : Exception  : Exception occurred updating identified gender. {ex.Message}");
                return this.HandleException(ex, "An error occurred while updating  identified gender. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// Delete an Identified Gender.
        /// </summary>
        /// <param name="identifiedGenderID">identifiedGenderID.</param>
        /// <returns>ActionResult.<CRUDResponseDTO>.</returns>
        [HttpDelete]
        [Route("identified-gender/{identifiedGenderID}")]
        public ActionResult<CRUDResponseDTO> DeleteIdentifiedGender(int identifiedGenderID)
        {
            try
            {
                var result = this.optionsService.DeleteIdentifiedGender(identifiedGenderID, this.GetTenantID());
                return result;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.DeleteItem, $"DeleteIdentifiedGender/Delete : Deleting Identifie dGender : Exception  : Exception occurred deleting Identified Gender. {ex.Message}");
                return this.HandleException(ex, "An error occurred deleting Identified Gender. Please try again later or contact support.");
            }
        }
    }
}
