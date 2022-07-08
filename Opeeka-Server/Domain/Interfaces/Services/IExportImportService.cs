// -----------------------------------------------------------------------
// <copyright file="IExportImportService.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.AspNetCore.Http;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.DTO.Input;
using Opeeka.PICS.Domain.DTO.Output;

namespace Opeeka.PICS.Domain.Interfaces.Services
{
    public interface IExportImportService
    {
        /// <summary>
        /// GetExportTemplateList.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>ExportImportResponseDTO</returns>
        ExportImportResponseDTO GetExportTemplateList(long agencyID);

        /// <summary>
        /// GetExportTemplateData.
        /// </summary>
        /// <param name="ExportTemplateDTO"></param>
        /// <param name="agencyID"></param>
        /// <returns></returns>
        ExportTemplateReponseDTO GetExportTemplateData(ExportTemplateInputDTO ExportTemplateDTO, long agencyID);

        /// <summary>
        /// ImportFile.
        /// </summary>
        /// <param name="uploadfile">uploadfile.</param>
        /// <param name="FileImportDTO">FileImportDTO.</param>
        /// <returns>ImportReponseDTO.</returns>
        ImportReponseDTO ImportFile(FileImportInputDTO uploadfile,FileImportDTO fileImportDTO);

        /// <summary>
        /// ImportJSON.
        /// </summary>
        /// <param name="uploadJSON">uploadJSON.</param>
        /// <param name="FileImportDTO">FileImportDTO.</param>
        /// <returns>ImportReponseDTO.</returns>
        ImportReponseDTO ImportJson(JsonImportInputDTO uploadJSON, FileImportDTO fileImportDTO);

        /// <summary>
        /// GetFileImportDAta.
        /// </summary>
        /// <param name="importType">importType.</param>
        /// <returns>ImportReponseDTO.</returns>
        ImportReponseDTO GetFileImportData(string importType);

        /// <summary>
        /// ImportIsProcessedUpdate.
        /// </summary>
        /// <param name="fileImportID">fileImportID</param>
        /// <returns>ImportReponseDTO</returns>
        ImportReponseDTO ImportIsProcessedUpdate(int fileImportID);

        /// <summary>
        /// GetAllImportTypes.
        /// </summary>
        /// <param name="agencyID"></param>
        /// <returns></returns>
        ImportTypeResponseDTO GetAllImportTypes(long agencyID);

        /// <summary>
        /// GetAssessmentTemplateFromQuestionnaireId.
        /// </summary>
        /// <param name="questionnaireId"></param>
        /// <returns></returns>
        AssessmentTemplateResponseDTO GetAssessmentTemplateFromQuestionnaireId(int questionnaireId, bool isExternal = false);

        /// <summary>
        /// SendEmailAfterImport.
        /// </summary>
        /// <param name="importEmailInputDTO"></param>
        /// <returns></returns>
        CRUDResponseDTO SendEmailAfterImport(ImportEmailInputDTO importEmailInputDTO);
    }

}
