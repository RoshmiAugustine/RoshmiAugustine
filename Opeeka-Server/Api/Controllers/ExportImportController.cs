// -----------------------------------------------------------------------
// <copyright file="ExportImportController.cs" company="Naico ITS">
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
    /// ExportImportController.
    /// </summary>
    [Route("api")]
    [ApiController]
    public class ExportImportController : BaseController
    {

        /// Initializes a new instance of the <see cref="logger"/> class.
        private readonly ILogger<ExportImportController> logger;

        /// Initializes a new instance of the <see cref="exportImportService"/> class.
        private readonly IExportImportService exportImportService;


        /// <summary>
        /// Initializes a new instance of the <see cref="ExportImportController"/> class.
        /// </summary>
        public ExportImportController(ILogger<ExportImportController> logger, IExportImportService exportImportService)
        {
            this.logger = logger;
            this.exportImportService = exportImportService;
        }

        /// <summary>
        /// GetExportTemplateList.
        /// </summary>
        /// <returns>ExportImportResponseDTO.</returns>
        [HttpGet]
        [Route("list-export-template")]
        public ActionResult<ExportImportResponseDTO> GetExportTemplateList()
        {
            try
            {
                var agencyID = this.GetTenantID();
                var response = this.exportImportService.GetExportTemplateList(agencyID);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.ListItems, $"GetExportTemplateList/Get : Listing Export Template : Exception  : Exception occurred getting Export Template List. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving Export Template. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetExportTemplateData.
        /// </summary>
        /// <param name="exportTemplateDTO">exportTemplateDTO.</param>
        /// <returns>ExportTemplateReponseDTO.</returns>
        [HttpPost]
        [Route("export-template")]
        public ActionResult<ExportTemplateReponseDTO> GetExportTemplateData(ExportTemplateInputDTO exportTemplateDTO)
        {
            try
            {

                ExportTemplateReponseDTO response = this.exportImportService.GetExportTemplateData(exportTemplateDTO, this.GetTenantID());
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.ListItems, $"GetExportTemplateData/Get : Export Template Data: Exception  : Exception occurred getting Export Template Data. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving Export Template Data. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// ImportFile.
        /// </summary>
        /// <param name="fileImportInputDTO">fileImportInputDTO.</param>
        /// <returns>ImportReponseDTO.</returns>
        [HttpPost]
        [Route("import")]
        [Consumes("multipart/form-data")]
        public ActionResult<ImportReponseDTO> ImportFile([FromForm] FileImportInputDTO fileImportInputDTO)
        {
            try
            {
                ImportReponseDTO importReponseDTO = new ImportReponseDTO();
                FileImportDTO importDTO = new FileImportDTO();

                if (fileImportInputDTO.UploadFile != null)
                {
                    importDTO.AgencyID = this.GetTenantID();
                    importDTO.UpdateUserID = this.GetUserID();

                    importReponseDTO = this.exportImportService.ImportFile(fileImportInputDTO, importDTO);
                }

                return importReponseDTO;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.InsertItem, $"ImportFile/POST : Importing File : Exception  : Exception occurred improting file. {ex.Message}");
                return this.HandleException(ex, ex.InnerException + ". An error occurred while importing file. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// Import JSON as File.
        /// </summary>
        /// <returns>ImportReponseDTO.</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpPost]
        [Route("import-json")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<ImportReponseDTO> ImportJSON(JsonImportInputDTO jsonImportInputDTO)
        {
            try
            {
                ImportReponseDTO importReponseDTO = new ImportReponseDTO();
                FileImportDTO importDTO = new FileImportDTO();

                if (!string.IsNullOrEmpty(jsonImportInputDTO.UploadJSON))
                {
                    importDTO.AgencyID = jsonImportInputDTO.AgencyID;
                    importDTO.UpdateUserID = 1;

                    importReponseDTO = this.exportImportService.ImportJson(jsonImportInputDTO, importDTO);
                }

                return importReponseDTO;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.InsertItem, $"ImportJSON/POST : Importing JSON : Exception  : Exception occurred importing file. {ex.Message}");
                return this.HandleException(ex, ex.InnerException + ". An error occurred while importing JSON. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetFilesToImport.
        /// </summary>
        /// <param name="importType">importType.</param>
        /// <returns>ImportReponseDTO.</returns>
        [HttpGet]
        [Authorize(Policy = "EHRAPIPermission")]
        [Route("import-file-list")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<ImportReponseDTO> GetFilesToImport(string importType)
        {
            try
            {
                ImportReponseDTO response = this.exportImportService.GetFileImportData(importType);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetFilesToImport/GET : Files To Import : Exception  : Exception occurred while fetching data to import. {ex.Message}");
                return this.HandleException(ex, ex.InnerException + ". An error occurred while fetching files to import. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// ImportIsProcessedUpdate.
        /// </summary>
        /// <param name="fileImportID">fileImportID.</param>
        /// <returns>ImportReponseDTO.</returns>
        [HttpPut]
        [Authorize(Policy = "EHRAPIPermission")]
        [Route("import-isprocessed-update")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<ImportReponseDTO> ImportIsProcessedUpdate(int fileImportID)
        {
            try
            {
                ImportReponseDTO importReponseDTO = new ImportReponseDTO();

                if (fileImportID != 0)
                {
                    importReponseDTO = this.exportImportService.ImportIsProcessedUpdate(fileImportID);
                }

                return importReponseDTO;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.UpdateItem, $"ImportIsProcessedUpdate/PUT : Import Is Processed Update : Exception  : Exception occurred updating file. {ex.Message}");
                return this.HandleException(ex, ex.InnerException + ". An error occurred while updating import files. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// Get all ImportTypes.
        /// </summary>
        /// <returns>ImportTypeResponseDTO.<LanguageResponseDTO>.</returns>
        [HttpGet]
        [Route("import-types")]
        public ActionResult<ImportTypeResponseDTO> GetAllImportTypes()
        {
            try
            {
                long agencyID = this.GetTenantID();
                ImportTypeResponseDTO importTypes = this.exportImportService.GetAllImportTypes(agencyID);
                return importTypes;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetAllImportTypes /GET : Exception occurred while getting all ImportTypes. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving all ImportTypes. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetAssessmentTemplateForimportProcess.
        /// </summary>
        /// <returns>AssessmentTemplateResponseDTO.</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpGet]
        [Route("assessment-template-json-import/{questionnaireId}")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<AssessmentTemplateResponseDTO> GetAssessmentTemplateForimportProcess(int questionnaireId)
        {
            try
            {
                bool IsExternal = true;
                AssessmentTemplateResponseDTO response = this.exportImportService.GetAssessmentTemplateFromQuestionnaireId(questionnaireId, IsExternal);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetAssessmentTemplateForimportProcess /GET : Exception occurred while getting all Json  template from QuestionnaireId. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving all Json template from QuestionnaireId. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// GetAssessmentTemplateFromQuestionnaireId.
        /// </summary>
        /// <returns>AssessmentTemplateResponseDTO.</returns>
        [HttpGet]
        [Route("assessment-template-json/{questionnaireId}")]
        public ActionResult<AssessmentTemplateResponseDTO> GetAssessmentTemplateFromQuestionnaireId(int questionnaireId)
        {
            try
            {
                AssessmentTemplateResponseDTO response = this.exportImportService.GetAssessmentTemplateFromQuestionnaireId(questionnaireId);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"GetAssessmentTemplateFromQuestionnaireId /GET : Exception occurred while getting all Json assessment template from QuestionnaireId. {ex.Message}");
                return this.HandleException(ex, "An error occurred retrieving all Json assessment template from QuestionnaireId. Please try again later or contact support.");
            }
        }

        /// <summary>
        /// SendEmailAfterImport.
        /// </summary>
        /// <returns>AssessmentTemplateResponseDTO.</returns>
        [Authorize(Policy = "EHRAPIPermission")]
        [HttpPost]
        [Route("send-email-import")]
        [ApiExplorerSettings(GroupName = "api-azurefunction")]
        public ActionResult<CRUDResponseDTO> SendEmailAfterImport(ImportEmailInputDTO importEmailInputDTO)
        {
            try
            {
                CRUDResponseDTO response = this.exportImportService.SendEmailAfterImport(importEmailInputDTO);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(MyLogEvents.GetItem, $"SendEmailAfterImport /GET : Exception occurred while Send Email AfterImport. {ex.Message}");
                return this.HandleException(ex, "An error occurred while Send Email AfterImport. Please try again later or contact support.");
            }
        }
    }
}
