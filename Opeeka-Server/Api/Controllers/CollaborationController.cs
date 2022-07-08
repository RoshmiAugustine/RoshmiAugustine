// -----------------------------------------------------------------------
// <copyright file="CollaborationController.cs" company="Naico ITS">
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
    /// ConsumerController.
    /// </summary>
    [Route("api")]
    [ApiController]
    public class CollaborationController : BaseController
    {
        /// Initializes a new instance of the <see cref="collaborationService"/> class.
        private readonly ICollaborationService collaborationService;

        /// Initializes a new instance of the <see cref="logger"/> class.
        private readonly ILogger<CollaborationController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CollaborationController"/> class.
        /// </summary>
        /// <param name="logger">logger.</param>
        /// <param name="collaborationService">collaborationService.</param>
        public CollaborationController(ILogger<CollaborationController> logger, ICollaborationService collaborationService)
        {
            this.collaborationService = collaborationService;
            this.logger = logger;
        }

        /// <summary>
        /// Add Collaboration.
        /// </summary>
        /// <param name="CollaborationDetailsDTO">The CollaborationDetailsDTO<see cref="CollaborationDetailsDTO"/>.</param>
        /// <returns>ActionResult.<ResultDTO>.</returns>
        // [ServiceFilter(typeof(TransactionFilter))]
        [HttpPost]
        [Route("collaboration")]
        public ActionResult<CollaborationResponseDTO> AddCollaborationDetails([FromBody] CollaborationDetailsDTO collaborationDetailsDTO)
        {
            if (collaborationDetailsDTO is null)
            {
                throw new ArgumentNullException(nameof(collaborationDetailsDTO));
            }

            try
            {
                if (this.ModelState.IsValid)
                {
                    collaborationDetailsDTO.AgencyID = this.GetTenantID();
                    CollaborationResponseDTO returnData = this.collaborationService.AddCollaborationDetails(collaborationDetailsDTO);
                    return returnData;
                }
                else
                {
                    return new EmptyResult();
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.InsertItem, $"AddCollaborationDetails/POST : Adding item : Exception  : Exception occurred Adding Collaboration Details. {ex.Message}");
                return this.HandleException(ex, "An error occurred while adding collaboration. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// UpdateCollaborationDetails.
        /// </summary>
        /// <param name="collaborationDetailsDTO">The collaborationDetailsDTO<see cref="CollaborationDetailsDTO"/>.</param>
        /// <returns>CollaborationResponseDTO.<ResultDTO>.</returns>
        // [ServiceFilter(typeof(TransactionFilter))]
        [HttpPut]
        [Route("collaboration")]
        public ActionResult<CollaborationResponseDTO> UpdateCollaborationDetails([FromBody] CollaborationDetailsDTO collaborationDetailsDTO)
        {
            if (collaborationDetailsDTO is null)
            {
                throw new ArgumentNullException(nameof(collaborationDetailsDTO));
            }

            try
            {
                if (this.ModelState.IsValid)
                {
                    int updateUserID = 0;
                    var userID = this.GetUserID();
                    updateUserID = Convert.ToInt32(userID);
                    collaborationDetailsDTO.AgencyID = this.GetTenantID();
                    CollaborationResponseDTO returnData = this.collaborationService.UpdateCollaborationDetails(collaborationDetailsDTO, updateUserID);
                    return returnData;
                }
                else
                {
                    return new EmptyResult();
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.UpdateItem, $"UpdateCollaborationDetails/PUT : Updating item : Exception  : Exception occurred Updating Collaboration Details. {ex.Message}");
                return this.HandleException(ex, "An error occurred updating collaboration. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetCollaborationList.
        /// </summary>
        /// <param name="collaborationSearchDTO">pageNumber.</param>
        /// <returns>Collaboration List.</returns>
        [HttpPost]
        [Route("collaborations")]
        public ActionResult<GetCollaborationListDTO> GetCollaborationList([FromBody] CollaborationSearchDTO collaborationSearchDTO)
        {
            try
            {
                long agencyID = this.GetTenantID();
                var response = this.collaborationService.GetCollaborationList(collaborationSearchDTO, agencyID);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetCollaborationList/POST : POST item : Exception  : Exception occurred Getting Collaboration List. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving collaboration list. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetCollaborationDetails.
        /// </summary>
        /// <param name="collaborationIndex">collaborationIndex.</param>
        /// <returns>CollaborationDetailsResponseDTO.</returns>
        [HttpGet]
        [Route("collaboration/{collaborationIndex}")]
        public ActionResult<CollaborationDetailsResponseDTO> GetCollaborationDetails(Guid collaborationIndex)
        {
            try
            {
                long agencyID = this.GetTenantID();
                var response = this.collaborationService.GetCollaborationDetails(collaborationIndex, agencyID);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetCollaborationDetails/GET : Getting item : Exception  : Exception occurred Getting Collaboration Details. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving collaboration details. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetCollaborationDetailsByName.
        /// </summary>
        /// <param name="importParameterDTO">importParameterDTO.</param>
        /// <returns>CollaborationDetailsResponseDTO.</returns>
        [HttpPost]
        [Authorize(Policy = "EHRAPIPermission")]
        [Route("collaboration-list-byname")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<CollaborationDetailsResponseDTO> GetCollaborationDetailsByName(ImportParameterDTO importParameterDTO)
        {
            try
            {
                CollaborationDetailsResponseDTO response = this.collaborationService.GetCollaborationDetailsByName(importParameterDTO.JsonData, importParameterDTO.agencyID);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetCollaborationDetailsByName/POST : POST item : Exception  : Exception occurred while fetching collaboration details by name. {ex.Message}");
                return this.HandleException(ex, ex.InnerException + ". An error occurred while fetching collaboration details by name. Please try again later or contact support.");
            }
        }
    }
}