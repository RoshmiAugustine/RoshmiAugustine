// -----------------------------------------------------------------------
// <copyright file="AgencyController.cs" company="Naico ITS">
// Copyright (c) Naico ITS. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Api.Controllers
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Opeeka.PICS.Domain.DTO;
    using Opeeka.PICS.Domain.DTO.Input;
    using Opeeka.PICS.Domain.DTO.Output;
    using Opeeka.PICS.Domain.Interfaces.Services;

    /// <summary>
    /// ConsumerController.
    /// </summary>
    [Route("api")]
    [ApiController]
    public class AgencyController : BaseController
    {
        /// Initializes a new instance of the <see cref="agencyService"/> class.
        /// <summary>
        /// Defines the agencyService.
        /// </summary>
        private readonly IAgencyService agencyService;

        /// Initializes a new instance of the <see cref="logger"/> class.
        /// <summary>
        /// Defines the logger.
        /// </summary>
        private readonly ILogger<AgencyController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AgencyController"/> class.
        /// </summary>
        /// <param name="logger">logger.</param>
        /// <param name="agencyService">agencyService.</param>
        public AgencyController(ILogger<AgencyController> logger, IAgencyService agencyService)
        {
            this.agencyService = agencyService;
            this.logger = logger;
        }

        /// <summary>
        /// Get.
        /// </summary>
        /// <param name="agencyDetailsDTO">The agencyDetailsDTO<see cref="AgencyDetailsDTO"/>.</param>
        /// <returns>ActionResult.<ResultDTO>.</returns>
        // [ServiceFilter(typeof(TransactionFilter))]
        [HttpPost]
        [Route("agency")]
        public ActionResult<CRUDResponseDTO> AddAgencyDetails([FromBody] AgencyDetailsDTO agencyDetailsDTO)
        {
            if (agencyDetailsDTO is null)
            {
                throw new ArgumentNullException(nameof(agencyDetailsDTO));
            }

            try
            {
                if (this.ModelState.IsValid)
                {
                    CRUDResponseDTO returnData = this.agencyService.AddAgencyDetails(agencyDetailsDTO);
                    return returnData;
                }
                else
                {
                    return new EmptyResult();
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"AddAgencyDetails/POST : Adding item : Exception  : Exception occurred Adding Agency details. {ex.Message}");
                return this.HandleException(ex, "An error occurred while adding agency details. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// Get.
        /// </summary>
        /// <param name="agencyDetailsDTO">The agencyDetailsDTO<see cref="AgencyDetailsDTO"/>.</param>
        /// <returns>ActionResult.<ResultDTO>.</returns>
        // [ServiceFilter(typeof(TransactionFilter))]
        [HttpPut]
        [Route("agency")]
        public ActionResult<CRUDResponseDTO> UpdateAgencyDetails([FromBody] AgencyDetailsDTO agencyDetailsDTO)
        {
            if (agencyDetailsDTO is null)
            {
                throw new ArgumentNullException(nameof(agencyDetailsDTO));
            }

            try
            {
                if (this.ModelState.IsValid)
                {
                    CRUDResponseDTO returnData = this.agencyService.UpdateAgencyDetails(agencyDetailsDTO);
                    return returnData;
                }
                else
                {
                    return new EmptyResult();
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"UpdateAgencyDetails/PUT : Updating item : Exception  : Exception occurred Updating Agency details. {ex.Message}");
                return this.HandleException(ex, "An error occurred updating agency details. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetAgencyList.
        /// </summary>
        /// <returns>GetAgencyListDTO.</returns>
        [HttpPost]
        [Route("agencies/list")]
        public ActionResult<GetAgencyListDTO> GetAgencyList([FromBody] AgencySearchDTO agencySearchDTO)
        {
            try
            {
                long agencyID = this.GetTenantID();
                var roles = this.GetRole();
                var response = this.agencyService.GetAgencyList(agencySearchDTO, agencyID, roles);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"GetAgencyList/POST : Adding item : Exception  : Exception occurred adding Agency list. {ex.Message}");
                return this.HandleException(ex, "An error occurred while adding agency list. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetAgencyDetails.
        /// </summary>
        /// <param name="agencyIndex">agencyIndex.</param>
        /// <returns>GetAgencyDetailsDTO.</returns>
        [HttpGet]
        [Route("agency/{agencyIndex}")]
        public ActionResult<GetAgencyDetailsDTO> GetAgencyDetails(Guid agencyIndex)
        {
            try
            {
                var response = this.agencyService.GetAgencyDetails(agencyIndex);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"GetAgencyDetails/GET : Getting item : Exception  : Exception occurred Getting agency details. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving agency details. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// Get.
        /// </summary>
        /// <param name="AgencyIndexId">The AgencyIndexId<see cref="Guid"/>.</param>
        /// <returns>ActionResult.<ResultDTO>.</returns>
        [HttpDelete]
        [Route("agency/{agencyIndex}")]
        public ActionResult<CRUDResponseDTO> RemoveAgencyDetails(Guid agencyIndex)
        {
            try
            {
                CRUDResponseDTO returnData = this.agencyService.RemoveAgencyDetails(agencyIndex);
                return returnData;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"RemoveAgencyDetails/DELETE : Deleting item : Exception  : Exception occurred Deleting agency details. {ex.Message}");
                return this.HandleException(ex, "An error occurred while deleting agency details. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// Get All Agencies.
        /// </summary>
        /// <returns>ActionResult.<AgencyLookupResponseDTO></returns>
        [HttpGet]
        [Route("agency/GetAllAgencies")]
        public ActionResult<AgencyLookupResponseDTO> GetAllAgencies()
        {
            try
            {
                AgencyLookupResponseDTO agencyLookupResponseDTO = this.agencyService.GetAllAgencyLookup();
                return agencyLookupResponseDTO;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"GetAllAgencies/GET : Getting item : Exception  : Exception occurred Getting all agencies. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving all agencies. Please try again later or contact support.");
            }
        }
    }
}
