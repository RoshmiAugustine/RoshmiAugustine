// -----------------------------------------------------------------------
// <copyright file="LanguageController.cs" company="Naico ITS">
// Copyright (c) Naico ITS. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Api.Controllers
{
    using System;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Opeeka.PICS.Api.Controllers;
    using Opeeka.PICS.Domain.DTO;
    using Opeeka.PICS.Domain.DTO.Output;
    using Opeeka.PICS.Domain.Interfaces.Services;
    using Opeeka.PICS.Infrastructure.Logging;

    /// <summary>
    /// Language Controller.
    /// </summary>
    [Route("api")]
    [ApiController]
    public class LanguageController : BaseController
    {
        /// Initializes a new instance of the <see cref="languageService"/> class.
        private readonly ILanguageService languageService;

        /// Initializes a new instance of the <see cref="logger"/> class.
        private readonly ILogger<LanguageController> logger;

        /// <summary>
        ///  Initializes a new instance of the <see cref="LanguageController"/> class.
        /// </summary>
        /// <param name="logger">logger.</param>
        /// <param name="languageService">languageService.</param>
        public LanguageController(ILogger<LanguageController> logger, ILanguageService languageService)
        {
            this.languageService = languageService;
            this.logger = logger;
        }

        /// <summary>
        /// POST.
        /// </summary>
        /// <param name="languageData">languageData.</param>
        /// <returns>ActionResult.<CRUDResponseDTO>.</returns>
        [HttpPost]
        [Route("language")]
        public ActionResult<CRUDResponseDTO> AddLanguage([FromBody] LanguageDetailsDTO languageData)
        {
            try
            {
                if (languageData != null)
                {
                    if (this.ModelState.IsValid)
                    {
                        var response = this.languageService.AddLanguage(languageData, this.GetUserID(), this.GetTenantID());
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
                this.logger.LogError(MyLogEvents.InsertItem, $"AddLanguage/Post : Adding language : Exception  : Exception occurred adding language. {ex.Message}");
                return this.HandleException(ex, "An error occurred while adding language. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// PUT.
        /// </summary>
        /// <param name="languageData">languageData.</param>
        /// <returns>CRUDResponseDTO.</returns>
        [HttpPut]
        [Route("language")]
        public ActionResult<CRUDResponseDTO> UpdateLanguage([FromBody] LanguageDetailsDTO languageData)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var response = this.languageService.UpdateLanguage(languageData, this.GetUserID(), this.GetTenantID());
                    return response;
                }
                else
                {
                    return new EmptyResult();
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.UpdateItem, $"UpdateLanguage/Put : Updating language : Exception  : Exception occurred while updating language. {ex.Message}");
                return this.HandleException(ex, "An error occurred while updating language. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// DeleteLanguage.
        /// </summary>
        /// <param name="languageID">languageID.</param>
        /// <returns>CRUDResponseDTO.</returns>
        [HttpDelete]
        [Route("language/{languageID}")]
        public ActionResult<CRUDResponseDTO> DeleteLanguage(int languageID)
        {
            try
            {
                var result = this.languageService.DeleteLanguage(languageID, this.GetTenantID());
                return result;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.DeleteItem, $"DeleteLanguage/Delete : Deleting language : Exception  : Exception occurred while deleting language. {ex.Message}");
                return this.HandleException(ex, "An error occurred while deleting language. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetLanguageList.
        /// </summary>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <returns>LanguageListResponseDTO.</returns>
        [HttpGet]
        [Route("language/{pageNumber}/{pageSize}")]
        public ActionResult<LanguageListResponseDTO> GetLanguageList(int pageNumber, int pageSize)
        {
            try
            {
                var response = this.languageService.GetLanguageList(pageNumber, pageSize, this.GetTenantID());
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.ListItems, $"GetLanguageList/Get : Listing language : Exception  : Exception occurred getting language list. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving language list. Please try again later or contact support.");
            }
        }
    }
}