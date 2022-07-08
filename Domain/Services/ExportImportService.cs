// -----------------------------------------------------------------------
// <copyright file="ExportImportService.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.DTO.Input;
using Opeeka.PICS.Domain.DTO.Output;
using Opeeka.PICS.Domain.Interfaces.Common;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Domain.Interfaces.Services;
using Opeeka.PICS.Domain.Resources;
using Opeeka.PICS.Infrastructure.Enums;

namespace Opeeka.PICS.Domain.Services
{
    public class ExportImportService : BaseService, IExportImportService
    {

        private readonly IExportImportRepository exportImportRepository;
        private readonly LocalizeService localize;
        private readonly IImportRepository importRepository;
        private readonly ILookupRepository lookupRepository;
        private readonly IMapper mapper;
        private readonly IConfigurationRepository configurationRepository;
        private readonly IHelperRepository helperRepository;
        private readonly IEmailSender emailSender;

        public ExportImportService(IExportImportRepository exportImportRepository, IImportRepository importRepository, LocalizeService localizeService, IMapper mapper, ILookupRepository lookupRepository, IConfigurationRepository configurationRepository, IHelperRepository helperRepository, IEmailSender emailSender, IConfigurationRepository configRepo, IHttpContextAccessor httpContext): base(configRepo, httpContext)
        {
            this.exportImportRepository = exportImportRepository;
            this.importRepository = importRepository;
            this.mapper = mapper;
            this.localize = localizeService;
            this.lookupRepository = lookupRepository;
            this.configurationRepository = configurationRepository;
            this.helperRepository = helperRepository;
            this.emailSender = emailSender;
        }

        /// <summary>
        /// GetExportTemplateData.
        /// </summary>
        /// <param name="ExportTemplateID">ExportTemplateID.</param>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>ExportTemplateReponseDTO.</returns>
        public ExportTemplateReponseDTO GetExportTemplateData(int ExportTemplateID, long agencyID)
        {
            try
            {
                ExportTemplateReponseDTO responseDTO = new ExportTemplateReponseDTO();                           
                responseDTO.ExportTemplate = exportImportRepository.GetAsync(ExportTemplateID).Result;
                responseDTO.ExportByteArray = this.GetExportTemplateDataByteArray(responseDTO.ExportTemplate.TemplateSourceText, agencyID);
                responseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                responseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                return responseDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// GetExportTemplateData.
        /// </summary>
        /// <param name="exportTemplateDTO"></param>
        /// <param name="agencyID"></param>
        /// <returns></returns>
        public ExportTemplateReponseDTO GetExportTemplateData(ExportTemplateInputDTO exportTemplateDTO, long agencyID)
        {
            try
            {
                var offset = Convert.ToInt32(base.GetTimeZoneFromHeader());
                ExportTemplateReponseDTO responseDTO = new ExportTemplateReponseDTO();
                var exportTemplateDetails = exportImportRepository.GetAsync(exportTemplateDTO.ExportTemplateID).Result;
                if (exportTemplateDetails.TemplateType == PCISEnum.ExportTypes.Assessment)
                {
                    var searchFilterCondition = string.Empty;
                    if (exportTemplateDTO.AssessmentFilter == null)
                    {
                        responseDTO.ExportTemplate = null;
                        responseDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                        responseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(string.Format(PCISEnum.StatusMessages.RequiredParameterMissing, "AssessmentFilter details"));
                        return responseDTO;
                    }
                    if (exportTemplateDTO.AssessmentFilter.QuestionnaireID != 0)
                    {
                        searchFilterCondition = $@" AND PQ.QuestionnaireID = {exportTemplateDTO.AssessmentFilter.QuestionnaireID}";
                    }
                    if (exportTemplateDTO.AssessmentFilter.FromDate.HasValue && exportTemplateDTO.AssessmentFilter.ToDate.HasValue)
                    {
                        var fromDate = exportTemplateDTO.AssessmentFilter.FromDate.Value;
                        var toDate = exportTemplateDTO.AssessmentFilter.ToDate.Value;
                        searchFilterCondition += $@" AND CAST(A.DateTaken AS DATE) BETWEEN CAST(DateAdd(MINUTE,{0 - offset},'{fromDate}') AS DATE) AND CAST(DateAdd(MINUTE,{0 - offset},'{toDate}') AS DATE)";
                    }
                    else
                    {
                        var fromdate = string.Empty;
                        var todate = string.Empty;
                        if (exportTemplateDTO.AssessmentFilter.FromDate.HasValue)
                        {
                            fromdate = $@"AND CAST(A.DateTaken AS DATE) >= CAST(DateAdd(MINUTE,{0 - offset},'{exportTemplateDTO.AssessmentFilter.FromDate.Value}') AS DATE)";
                        }
                        //var todate = $@"{DateTime.UtcNow.Date}";
                        if (exportTemplateDTO.AssessmentFilter.ToDate.HasValue)
                        {
                            todate = $@"AND CAST(A.DateTaken AS DATE) <= CAST(DateAdd(MINUTE,{0 - offset},'{exportTemplateDTO.AssessmentFilter.ToDate.Value}') AS DATE)";
                        }
                        searchFilterCondition += @$"{fromdate} {todate}";
                    }
                        exportTemplateDetails.TemplateSourceText = exportTemplateDetails.TemplateSourceText.Replace(PCISEnum.ExportReplace.assessmentFilter, searchFilterCondition);
                }
                responseDTO.ExportTemplate = exportTemplateDetails;
                responseDTO.ExportByteArray = this.GetExportTemplateDataByteArray(responseDTO.ExportTemplate.TemplateSourceText, agencyID);
                responseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                responseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                return responseDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// GetExportTemplateDataByteArray.
        /// </summary>
        /// <param name="TemplateSourceText"></param>
        /// <param name="agencyID"></param>
        /// <returns></returns>
        public byte[] GetExportTemplateDataByteArray(string templateSourceText, long agencyID)
        {
            byte[] result = null;
            if (!string.IsNullOrEmpty(templateSourceText))
            {
                templateSourceText = templateSourceText.Replace(PCISEnum.ExportReplace.agencyID, agencyID.ToString());
                var responseDTO = exportImportRepository.GetExportData(templateSourceText);

                result =  ObjectToByteArray(responseDTO);
            }
            return result;
        }

        /// <summary>
        /// ObjectToByteArray.
        /// </summary>
        /// <param name="obj">obj.</param>
        /// <returns>byte array.</returns>
        public byte[] ObjectToByteArray(object obj)
        {
            if (obj == null)
                return null;
            string json = JsonConvert.SerializeObject(obj);
            if(json == "[]")
            {
                return null;
            }
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, json);
                return ms.ToArray();
            }
        }

        /// <summary>
        /// GetExportTemplateList.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>ExportImportResponseDTO.</returns>
        public ExportImportResponseDTO GetExportTemplateList(long agencyID)
        {
            try
            {
                ExportImportResponseDTO responseDTO = new ExportImportResponseDTO();
                responseDTO.ExportTemplate = exportImportRepository.GetExportTemplateList(agencyID);
                responseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                responseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                return responseDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// ImportFile.
        /// </summary>
        /// <param name="uploadfileDTO">uploadfileDTO.</param>
        /// <param name="importDTO">importDTO.</param>
        /// <returns>ImportReponseDTO.</returns>
        public ImportReponseDTO ImportFile(FileImportInputDTO uploadfileDTO, FileImportDTO importDTO)
        {
            try
            {
                ImportReponseDTO responseDTO = new ImportReponseDTO();
                responseDTO.ResponseStatusCode = PCISEnum.StatusCodes.insertionFailed;
                responseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.insertionFailed);
                FileInfo fi = new FileInfo(uploadfileDTO.UploadFile.FileName);
                string filenameextension = fi.Extension;
                var importTypeDetails = this.importRepository.GetAllImportTypes(importDTO.AgencyID);
                if (uploadfileDTO.UploadFile != null && filenameextension == ".csv")
                {
                    List<string> list = new List<string>();
                    using (var reader = new StreamReader(uploadfileDTO.UploadFile.OpenReadStream()))
                    {
                        int counter = 0;
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            list.Add(line);
                            counter++;
                        }
                    }
                    var jsonData = ConvertCsvFileToJsonObject(list, importTypeDetails?.Where(x => x.ImportTypeID == uploadfileDTO.ImportTypeID).ToList());
                    if (jsonData == null)
                    {
                        responseDTO.ResponseStatusCode = PCISEnum.StatusCodes.FileImportFailedOnDuplicateHeaders;
                        responseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.FileImportFailedOnDuplicateHeaders);
                        return responseDTO;
                    }
                    if (string.IsNullOrEmpty(jsonData))
                    {
                        responseDTO.ResponseStatusCode = PCISEnum.StatusCodes.FileImportFailedOnHeaders;
                        responseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.FileImportFailedOnHeaders);
                        return responseDTO;
                    }
                    if (jsonData == "[]")
                    {
                        responseDTO.ResponseStatusCode = PCISEnum.StatusCodes.FileImportFailedOnEmpty;
                        responseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.FileImportFailedOnEmpty);
                        return responseDTO;
                    }
                    importDTO.FileJsonData = jsonData;
                    importDTO.CreatedDate = DateTime.UtcNow;
                    importDTO.IsProcessed = false;
                    importDTO.ImportFileName = fi.Name;
                    importDTO.ImportTypeID = uploadfileDTO.ImportTypeID;
                    importDTO.QuestionnaireID = uploadfileDTO.QuestionnaireID == 0 ? null : uploadfileDTO.QuestionnaireID;
                    int FileImportID = importRepository.InsertImportFileDetails(importDTO);
                    if (FileImportID != 0)
                    {
                        responseDTO.ResponseStatusCode = PCISEnum.StatusCodes.InsertionSuccess;
                        responseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.InsertionSuccess);
                    }
                }
                return responseDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
                
        /// <summary>
        /// ImportJSON.
        /// </summary>
        /// <param name="uploadJsonDTO">uploadJsonDTO.</param>
        /// <param name="importDTO">importDTO.</param>
        /// <returns>ImportReponseDTO.</returns>
        public ImportReponseDTO ImportJson(JsonImportInputDTO uploadJsonDTO, FileImportDTO importDTO)
        {
            try
            {
                ImportReponseDTO responseDTO = new ImportReponseDTO();
                responseDTO.ResponseStatusCode = PCISEnum.StatusCodes.insertionFailed;
                responseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.insertionFailed);
                FileInfo fi = new FileInfo(uploadJsonDTO.FileName);
                string filenameextension = fi.Extension;
                var importTypeDetails = this.importRepository.GetAllImportTypes(importDTO.AgencyID);
                if (!string.IsNullOrEmpty(uploadJsonDTO.UploadJSON))
                {
                    var jsonData = uploadJsonDTO.UploadJSON;
                    if (jsonData == null)
                    {
                        responseDTO.ResponseStatusCode = PCISEnum.StatusCodes.FileImportFailedOnDuplicateHeaders;
                        responseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.FileImportFailedOnDuplicateHeaders);
                        return responseDTO;
                    }
                    if (string.IsNullOrEmpty(jsonData))
                    {
                        responseDTO.ResponseStatusCode = PCISEnum.StatusCodes.FileImportFailedOnHeaders;
                        responseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.FileImportFailedOnHeaders);
                        return responseDTO;
                    }
                    if (jsonData == "[]")
                    {
                        responseDTO.ResponseStatusCode = PCISEnum.StatusCodes.FileImportFailedOnEmpty;
                        responseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.FileImportFailedOnEmpty);
                        return responseDTO;
                    }
                    importDTO.FileJsonData = jsonData;
                    importDTO.CreatedDate = DateTime.UtcNow;
                    importDTO.IsProcessed = false;
                    importDTO.ImportFileName = fi.Name;
                    importDTO.ImportTypeID = uploadJsonDTO.ImportTypeID;
                    importDTO.QuestionnaireID = uploadJsonDTO.QuestionnaireID == 0 ? null : uploadJsonDTO.QuestionnaireID;
                    int FileImportID = importRepository.InsertImportFileDetails(importDTO);
                    if (FileImportID != 0)
                    {
                        responseDTO.ResponseStatusCode = PCISEnum.StatusCodes.InsertionSuccess;
                        responseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.InsertionSuccess);
                    }
                }

                return responseDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// ConvertCsvFileToJsonObject.
        /// </summary>
        /// <param name="path">path.</param>
        /// <returns>string.</returns>
        public string ConvertCsvFileToJsonObject(List<string> lines, List<Entities.ImportType> importTypeDetails)
        {
            try
            {
                var listObjResult = new List<Dictionary<string, string>>();
                var defaultTemplate = importTypeDetails[0]?.TemplateJson;
                var csv = new List<string[]>();
                foreach (string line in lines)
                    csv.Add(line.Split(','));
                var properties = lines[0].Split(',').Where(x => !string.IsNullOrEmpty(x.Trim())).ToArray();
                if (properties.ToList().Count() > 0)
                {
                    if (!compareWithTemplateHeaders(defaultTemplate, properties.ToList()))
                    {
                        return string.Empty;
                    }
                    var duplicated = properties.ToList().GroupBy(x => x).Where(g => g.Count() > 1);
                    if (duplicated.Count() > 0)
                    {
                        var result = string.Join(",", duplicated.Select(x => x.Key));
                        return null;
                    }
                    for (int i = 1; i < lines.Count; i++)
                    {
                        var objResult = new Dictionary<string, string>();
                        for (int j = 0; j < properties.Length; j++)
                            objResult.Add(properties[j], csv[i][j].Trim());
                        if (string.Join("", objResult.Values.ToArray()).Replace("\"\"", "") != string.Empty)
                            listObjResult.Add(objResult);
                    }
                }
                return JsonConvert.SerializeObject(listObjResult);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool compareWithTemplateHeaders(string defaultTemplateHeaders, List<string> fileHeaders)
        {
            try
            {
                var rootHeaders = JToken.Parse(defaultTemplateHeaders);
                var defaultHeaders = rootHeaders.SelectMany(t => t.Children().OfType<JProperty>().Select(p => p.Name)).ToList();
                var result = fileHeaders.Where(x => defaultHeaders.Select(x => x).ToList().Contains(x)).ToList();
                if(result.Count != defaultHeaders.Count)
                {
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// GetFileImportDAta.
        /// </summary>
        /// <param name="importType">importType.</param>
        /// <returns>ImportReponseDTO.</returns>
        public ImportReponseDTO GetFileImportData(string importType)
        {
            try
            {
                ImportReponseDTO responseDTO = new ImportReponseDTO();
                responseDTO.ImportFileList = this.importRepository.GetFileImportData(importType);
                responseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                responseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                return responseDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// ImportIsProcessedUpdate.
        /// </summary>
        /// <param name="fileImportID">fileImportID.</param>
        /// <returns>ImportReponseDTO.</returns>
        public ImportReponseDTO ImportIsProcessedUpdate(int fileImportID)
        {
            try
            {
                ImportReponseDTO responseDTO = new ImportReponseDTO();
                Entities.FileImport fileImport = new Entities.FileImport();
                fileImport = this.importRepository.GetFileImportDataByID(fileImportID).Result;
                fileImport.IsProcessed = true;
                int res = this.importRepository.UpdateFileImport(fileImport).FileImportID;
                if (res != 0)
                {
                    responseDTO.ResponseStatusCode = PCISEnum.StatusCodes.UpdationSuccess;
                    responseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationSuccess);
                }
                else
                {
                    responseDTO.ResponseStatusCode = PCISEnum.StatusCodes.UpdationFailed;
                    responseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationFailed);
                }

                return responseDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ImportTypeResponseDTO GetAllImportTypes(long agencyID)
        {
            try
            {
                ImportTypeResponseDTO responseDTO = new ImportTypeResponseDTO();
                responseDTO.ImportTypes = new List<ImportTypeDTO>();
                List<Entities.ImportType> result = importRepository.GetAllImportTypes(agencyID);
                var questionnaires = this.lookupRepository.GetAllAgencyQuestionnaire(agencyID);
                this.mapper.Map<List<Entities.ImportType>, List<ImportTypeDTO>>(result, responseDTO.ImportTypes);
                var assessmentIndex = responseDTO.ImportTypes.FindIndex(x => x.Name == PCISEnum.ImportTypes.Assessment);
                if (assessmentIndex != -1)
                    responseDTO.ImportTypes[assessmentIndex].Questionnaires = questionnaires;
                responseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                responseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                return responseDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public AssessmentTemplateResponseDTO GetAssessmentTemplateFromQuestionnaireId(int questionnaireId, bool IsExternal)
        {
            try
            {
                AssessmentTemplateResponseDTO responseDTO = new AssessmentTemplateResponseDTO();
                responseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Failure;
                responseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Failure);
                var assessmentDefaultTemplate = importRepository.GetImportTypeIDByName(PCISEnum.ImportTypes.Assessment).Result;
                var defaultColumnList = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(assessmentDefaultTemplate.TemplateJson);
                var dict_defaultColumn = defaultColumnList[0];
                if (dict_defaultColumn.Count > 0)
                {
                    var defaultColumnString = string.Join(",", defaultColumnList[0].Keys.ToList());
                    var result = importRepository.GetAllQuestionnaireItems(questionnaireId);
                    if (result.Count > 0)
                    {
                        var dict_itemsToAppend = new Dictionary<string, string>();
                        if (IsExternal)
                        {
                            dict_itemsToAppend = result.ToDictionary(x => x.CategoryAndItemName, x => x.QuestionnaireItemID.ToString());
                        }
                        else
                        {
                            dict_itemsToAppend = result.ToDictionary(x => x.CategoryAndItemName, x => x.DefaultItemvalue);
                        }
                        bool isSet = false;
                        var dict_finalColumns = new List<Dictionary<string, string>>() { dict_defaultColumn };
                        foreach (KeyValuePair<string, string> keyValue in dict_itemsToAppend)
                        {
                            if (keyValue.Key.Contains(PCISEnum.ImportConstants.Caregiver) && !isSet)
                            {
                                dict_finalColumns[0].Add(PCISEnum.ImportConstants.Caregiver, "");
                                isSet = true;
                            }
                            dict_finalColumns[0].Add(keyValue.Key, keyValue.Value);
                        }

                        var jsonResult = JsonConvert.SerializeObject(dict_finalColumns);
                        responseDTO.TemplateJson = jsonResult;
                        responseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                        responseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                    }
                }
                return responseDTO;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public CRUDResponseDTO SendEmailAfterImport(ImportEmailInputDTO importEmailInputDTO)
        {
            try
            {
                CRUDResponseDTO response = new CRUDResponseDTO();
                response.ResponseStatusCode = PCISEnum.StatusCodes.MissingEmailID;
                response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.MissingEmailID);
                var helper = this.helperRepository.GetUserDetails(importEmailInputDTO.HelperUserID);
                if (!string.IsNullOrEmpty(helper?.HelperEmail))
                {
                    var fromemail = this.configurationRepository.GetConfigurationByName(PCISEnum.AssessmentEmail.FromEmail, importEmailInputDTO.AgencyID);
                    var fromemailDisplayName = this.configurationRepository.GetConfigurationByName(PCISEnum.AssessmentEmail.FromDisplayName, importEmailInputDTO.AgencyID);
                    var emailTemplateText = this.configurationRepository.GetConfigurationByName(PCISEnum.ImportEmail.EmailTemplateText, importEmailInputDTO.AgencyID);
                    var emailSubject = this.configurationRepository.GetConfigurationByName(PCISEnum.ImportEmail.EmailSubject, importEmailInputDTO.AgencyID);

                    var emailTemplateBody = emailTemplateText.Value.Replace(PCISEnum.ImportEmail.ResultMessage, importEmailInputDTO.Message);
                    emailTemplateBody = emailTemplateBody.Replace(PCISEnum.ImportEmail.HelperName, helper.Name);
                    emailTemplateBody = emailTemplateBody.Replace(PCISEnum.ImportEmail.Importfilename, importEmailInputDTO.ImportFileName);

                    var emailresponse = DraftEmailToSend(fromemail.Value, fromemailDisplayName.Value, helper.HelperEmail, helper.Name, emailSubject.Value, emailTemplateBody);
                    if (emailresponse == HttpStatusCode.Accepted)
                    {
                        response.ResponseStatusCode = PCISEnum.StatusCodes.EmailSendSuccess;
                        response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.EmailSendSuccess);
                    }
                    else
                    {
                        response.ResponseStatusCode = PCISEnum.StatusCodes.EmailSendFailed;
                        response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.EmailSendFailed);
                    }
                    ImportIsProcessedUpdate(importEmailInputDTO.ImportFileID);
                }
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private HttpStatusCode DraftEmailToSend(string fromEmailID, string fromEmailDisplayName, string ToEmailID, string ToEmailDisplayName, string emailSubject, string emailBodyTemplate)
        {
            try
            {
                string emailBody = this.localize.GetLocalizedHtmlString(emailBodyTemplate);
                SendEmail sendEmail = new SendEmail()
                {
                    Body = emailBody,
                    IsHtmlEmail = true,
                    Subject = this.localize.GetLocalizedHtmlString(emailSubject),
                    FromDisplayName = fromEmailDisplayName,
                    FromEmail = fromEmailID,
                    ToDisplayName = ToEmailDisplayName,
                    ToEmail = ToEmailID
                };
                return emailSender.SendEmailAsync(sendEmail);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
