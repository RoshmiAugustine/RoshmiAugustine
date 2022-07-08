using System;
using System.Collections.Generic;
using System.Dynamic;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using ImportProcess.Enums;
using ImportProcess.Common;
using Newtonsoft.Json.Linq;
using System.IO;
using Newtonsoft.Json;
using ImportProcess.DTO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace ImportProcess
{
    public static class ProcessData
    {
        [FunctionName("ImportProcess")]
        public static void Run([TimerTrigger("%timer-frequency%")] TimerInfo myTimer, ILogger log, ExecutionContext ctx)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            try

            {
                Utility utility = new Utility();
                var apiBaseUrl = Environment.GetEnvironmentVariable(PCISEnum.EnvironmentVariables.ApiUrlFromKeyVault, EnvironmentVariableTarget.Process);
                log.LogInformation($"ImportProcess : {DateTime.Now} : Azure function started.");
                var importProcessDetails = GetImportDetailsForProcess(utility, apiBaseUrl, log);
                log.LogInformation($"ImportProcess : {DateTime.Now} : Total files to import = {importProcessDetails.importFileList.Count}");

                Dictionary<string, LookupDTO> dict_AgencyLookups = new Dictionary<string, LookupDTO>();
                Dictionary<int, string> dict_QuestionnaireTemplate = new Dictionary<int, string>();

                //Fetch all files in open status to process from database..
                foreach (var fileToBeUploaded in importProcessDetails.importFileList)
                {
                    string resultMessage = string.Empty;
                    ImportEmailInputDTO importDTO = new ImportEmailInputDTO()
                    {
                        ImportFileType = fileToBeUploaded[PCISEnum.FileImportFields.ImportType],
                        AgencyID = fileToBeUploaded[PCISEnum.FileImportFields.AgencyID],
                        HelperUserID = fileToBeUploaded[PCISEnum.FileImportFields.UpdateUserID],
                        ImportFileID = fileToBeUploaded[PCISEnum.FileImportFields.FileImportID],
                        ImportFileName = fileToBeUploaded[PCISEnum.FileImportFields.ImportFileName] ?? ""
                    };
                    log.LogInformation($"ImportProcess : {DateTime.Now} : FileImportID-{importDTO.ImportFileID} : *****Initiating {importDTO.ImportFileType} Import To PCIS*****");

                    if (importDTO.ImportFileType == PCISEnum.ImportTypes.Person)
                    {
                        resultMessage = ProcessPersonImport(utility, fileToBeUploaded, apiBaseUrl, log);
                    }
                    if (importDTO.ImportFileType == PCISEnum.ImportTypes.Assessment)
                    {
                        resultMessage = ProcessAssessmentImport(utility, apiBaseUrl, fileToBeUploaded, ref dict_AgencyLookups, ref dict_QuestionnaireTemplate, importDTO, log);
                    }
                    if (importDTO.ImportFileType == PCISEnum.ImportTypes.Helper)
                    {
                        resultMessage = ProcessHelperImport(utility, fileToBeUploaded, apiBaseUrl, log);
                    }

                    if (!resultMessage.IsNullOrEmpty())
                    {
                        ///DraftEmail with details of resultMessgae(Either error or success) and update Database status..        
                        importDTO.Message = resultMessage;
                        var response = DraftEmail(utility, apiBaseUrl, importDTO, log);
                        log.LogInformation($"ImportProcess : {DateTime.Now} : FileImportID-{importDTO.ImportFileID} : {response.ResponseStatus}.");

                        //Update processdate for record in database
                        log.LogInformation($"ImportProcess : {DateTime.Now} : FileImportID-{importDTO.ImportFileID} : *****End of {importDTO.ImportFileType} Import To PCIS*****");
                    }
                }
                log.LogInformation($"ImportProcess : {DateTime.Now} : Import Completed.");
            }

            catch (Exception ex)
            {
                //throw;
            }

        }

        #region PersonImport
        /// <summary>
        /// ProcessPersonImport
        /// 2021 Oct 22 - Removed the identifierType1 mandatory validation, failure message returned for all needed validations.
        /// </summary>
        /// <param name="Utility"></param>
        /// <param name="fileList"></param>
        /// <param name="apiurl"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        private static string ProcessPersonImport(Utility Utility, dynamic fileList, string apiurl, ILogger log)
        {
            int rowNo = 0;
            try
            {
                string resultMessage = string.Empty;
                string fileJsonData = fileList["fileJsonData"];
                var PersonData = JArray.Parse(fileJsonData);
                bool isFailed = false;
                long agencyID = fileList["agencyID"];
                int updateUserID = fileList["updateUserID"];
                string fileImportID = fileList["fileImportID"];

                log.LogInformation($"Person-Import : {DateTime.Now} : FileImportID-{fileImportID} : Total Rows to upload = {PersonData.Count}");

                var configAPiurl = apiurl + PCISEnum.APIurl.getAllIdentifiedGender + "?agencyId=" + agencyID;
                var GenderResult = Utility.RestApiCall(log, configAPiurl, false);
                var GenderResponse = JsonConvert.DeserializeObject<CRUDResponseDTO>(GenderResult);

                configAPiurl = apiurl + PCISEnum.APIurl.getAllCountryCodes;
                var CountryCodeResult = Utility.RestApiCall(log, configAPiurl, false);
                var CountryCodeResponse = JsonConvert.DeserializeObject<CRUDResponseDTO>(CountryCodeResult);

                List<JObject> HelperEmailList = new List<JObject>();
                List<JObject> CollaborationNameList = new List<JObject>();
                List<JObject> EthinicityList = new List<JObject>();
                List<JObject> IdentifierTypeList = new List<JObject>();
                foreach (var item in PersonData)
                {
                    rowNo++;
                    JObject HelperJObj = new JObject();
                    JObject CollaborationJObj = new JObject();
                    JObject EthinicityJObj1 = new JObject();
                    JObject EthinicityJObj2 = new JObject();
                    JObject EthinicityJObj3 = new JObject();
                    JObject EthinicityJObj4 = new JObject();
                    JObject EthinicityJObj5 = new JObject();
                    JObject IdentifierTypeJobj1 = new JObject();
                    JObject IdentifierTypeJobj2 = new JObject();
                    JObject IdentifierTypeJobj3 = new JObject();
                    JObject IdentifierTypeJobj4 = new JObject();
                    JObject IdentifierTypeJobj5 = new JObject();
                    if (string.IsNullOrEmpty(item[PCISEnum.PersonFields.HelperEmail].ToString()) || string.IsNullOrEmpty(item[PCISEnum.PersonFields.HelperEmail].ToString()) ||
                          string.IsNullOrEmpty(item[PCISEnum.PersonFields.RaceEthnicity1].ToString()))
                    {
                        resultMessage = $"Columns like {PCISEnum.PersonFields.HelperEmail}/{PCISEnum.PersonFields.RaceEthnicity1} should not be empty. ";
                        resultMessage = DraftResultMessage(item, log, rowNo, resultMessage, PCISEnum.ImportTypes.Person);
                        isFailed = true;
                        break;
                    }
                    else
                    {
                        HelperJObj[PCISEnum.PersonFields.HelperEmail] = item[PCISEnum.PersonFields.HelperEmail].ToString();
                        CollaborationJObj[PCISEnum.PersonFields.CollaborationName] = item[PCISEnum.PersonFields.CollaborationName].ToString();
                        HelperEmailList.Add(HelperJObj);
                        CollaborationNameList.Add(CollaborationJObj);
                        EthinicityJObj1[PCISEnum.PersonFields.Ethinicity] = item[PCISEnum.PersonFields.RaceEthnicity1].ToString();
                        EthinicityList.Add(EthinicityJObj1);
                        if (!string.IsNullOrEmpty(item[PCISEnum.PersonFields.RaceEthnicity2].ToString()))
                        {
                            EthinicityJObj2[PCISEnum.PersonFields.Ethinicity] = item[PCISEnum.PersonFields.RaceEthnicity2].ToString();
                            EthinicityList.Add(EthinicityJObj2);
                        }
                        if (!string.IsNullOrEmpty(item[PCISEnum.PersonFields.RaceEthnicity3].ToString()))
                        {
                            EthinicityJObj3[PCISEnum.PersonFields.Ethinicity] = item[PCISEnum.PersonFields.RaceEthnicity3].ToString();
                            EthinicityList.Add(EthinicityJObj3);
                        }
                        if (!string.IsNullOrEmpty(item[PCISEnum.PersonFields.RaceEthnicity4].ToString()))
                        {
                            EthinicityJObj4[PCISEnum.PersonFields.Ethinicity] = item[PCISEnum.PersonFields.RaceEthnicity4].ToString();
                            EthinicityList.Add(EthinicityJObj4);
                        }
                        if (!string.IsNullOrEmpty(item[PCISEnum.PersonFields.RaceEthnicity5].ToString()))
                        {
                            EthinicityJObj5[PCISEnum.PersonFields.Ethinicity] = item[PCISEnum.PersonFields.RaceEthnicity5].ToString();
                            EthinicityList.Add(EthinicityJObj5);
                        }
                        if (!string.IsNullOrEmpty(item[PCISEnum.PersonFields.IdentifierType1].ToString()))
                        {
                            IdentifierTypeJobj1[PCISEnum.PersonFields.IdentifierType] = item[PCISEnum.PersonFields.IdentifierType1].ToString();
                            IdentifierTypeList.Add(IdentifierTypeJobj1);
                        }
                        if (!string.IsNullOrEmpty(item[PCISEnum.PersonFields.IdentifierType2].ToString()))
                        {
                            IdentifierTypeJobj2[PCISEnum.PersonFields.IdentifierType] = item[PCISEnum.PersonFields.IdentifierType2].ToString();
                            IdentifierTypeList.Add(IdentifierTypeJobj2);
                        }
                        if (!string.IsNullOrEmpty(item[PCISEnum.PersonFields.IdentifierType3].ToString()))
                        {
                            IdentifierTypeJobj3[PCISEnum.PersonFields.IdentifierType] = item[PCISEnum.PersonFields.IdentifierType3].ToString();
                            IdentifierTypeList.Add(IdentifierTypeJobj3);
                        }
                        if (!string.IsNullOrEmpty(item[PCISEnum.PersonFields.IdentifierType4].ToString()))
                        {
                            IdentifierTypeJobj4[PCISEnum.PersonFields.IdentifierType] = item[PCISEnum.PersonFields.IdentifierType4].ToString();
                            IdentifierTypeList.Add(IdentifierTypeJobj4);
                        }
                        if (!string.IsNullOrEmpty(item[PCISEnum.PersonFields.IdentifierType5].ToString()))
                        {
                            IdentifierTypeJobj5[PCISEnum.PersonFields.IdentifierType] = item[PCISEnum.PersonFields.IdentifierType5].ToString();
                            IdentifierTypeList.Add(IdentifierTypeJobj5);
                        }
                    }
                }
                if (!isFailed)
                {
                    configAPiurl = apiurl + PCISEnum.APIurl.HelperList;
                    ImportParameterDTO Parameters = new ImportParameterDTO();
                    Parameters.JsonData = JsonConvert.SerializeObject(HelperEmailList);
                    Parameters.agencyID = agencyID;
                    var helperResult = Utility.RestApiCall(log, configAPiurl, false, false, PCISEnum.APIMethodType.PostRequest, null, JsonConvert.SerializeObject(Parameters));
                    log.LogInformation($"Person-Import  : {DateTime.Now} : Initiating Get Helper List by mail..");
                    var helperResponse = JsonConvert.DeserializeObject<CRUDResponseDTO>(helperResult);

                    configAPiurl = apiurl + PCISEnum.APIurl.CollaberationList;
                    Parameters.JsonData = JsonConvert.SerializeObject(CollaborationNameList);
                    var CollaborationResult = Utility.RestApiCall(log, configAPiurl, false, false, PCISEnum.APIMethodType.PostRequest, null, JsonConvert.SerializeObject(Parameters));
                    log.LogInformation($"Person-Import  : {DateTime.Now} : Initiating Get collaberation List by mail..");
                    var collaberationResponse = JsonConvert.DeserializeObject<CRUDResponseDTO>(CollaborationResult);

                    configAPiurl = apiurl + PCISEnum.APIurl.EthinicityList;
                    Parameters.JsonData = JsonConvert.SerializeObject(EthinicityList);
                    var EthinicityResult = Utility.RestApiCall(log, configAPiurl, false, false, PCISEnum.APIMethodType.PostRequest, null, JsonConvert.SerializeObject(Parameters));
                    log.LogInformation($"Person-Import  : {DateTime.Now} : Initiating Get Ethinicity List..");
                    var ethinicityResponse = JsonConvert.DeserializeObject<CRUDResponseDTO>(EthinicityResult);
                    CRUDResponseDTO identifierResponse = new CRUDResponseDTO();
                    if (IdentifierTypeList.Count > 0)
                    {
                        configAPiurl = apiurl + PCISEnum.APIurl.IdentifierList;
                        Parameters.JsonData = JsonConvert.SerializeObject(IdentifierTypeList);
                        var IdenitifierResult = Utility.RestApiCall(log, configAPiurl, false, false, PCISEnum.APIMethodType.PostRequest, null, JsonConvert.SerializeObject(Parameters));
                        log.LogInformation($"Person-Import  : {DateTime.Now} : Initiating Get Identifier..");
                        identifierResponse = JsonConvert.DeserializeObject<CRUDResponseDTO>(IdenitifierResult);
                    }
                    List<PeopleDetailsDTO> ParsedData = ParseData(PersonData, helperResponse.result.helperList, collaberationResponse?.result.collaborationList,
                        ethinicityResponse.result.raceEthnicities, identifierResponse?.result?.identificationTypes, GenderResponse.result.identifiedGender, agencyID, updateUserID, log, ref resultMessage, CountryCodeResponse.result.countries);
                    if (ParsedData != null && ParsedData.Count > 0)
                    {
                        configAPiurl = apiurl + PCISEnum.APIurl.ImportPerson;
                        var ImportResponse = Utility.RestApiCall(log, configAPiurl, false, false, PCISEnum.APIMethodType.PostRequest, null, JsonConvert.SerializeObject(ParsedData));
                        log.LogInformation($"Person-Import  : {DateTime.Now} : Initiating Get Identifier..");
                        var importResponse = JsonConvert.DeserializeObject<CRUDResponseDTO>(ImportResponse);
                        if (importResponse?.result.ResponseStatusCode == PCISEnum.Constants.InsertionSuccess)
                        {
                            log.LogInformation($"Person-Import  : {DateTime.Now} : Import file Success with fileID...." + fileImportID);
                            resultMessage = DraftResultMessage(null, log, 0, string.Empty, PCISEnum.ImportTypes.Person, true, ParsedData.Count);
                        }
                        else
                        {
                            log.LogInformation($"Person-Import  : {DateTime.Now} : Import file failed with fileID...." + fileImportID);
                            resultMessage = DraftResultMessage(null, log, 0, string.Empty, PCISEnum.ImportTypes.Person, false, ParsedData.Count);
                        }
                    }
                }
                return resultMessage;
            }
            catch (Exception ex)
            {
                log.LogError($"Person-Import : {DateTime.Now} : ImportTypeID-{0} : Exception occurred at {MethodInfo.GetCurrentMethod().Name} : {ex.Message}");
                return DraftResultMessage(null, log, rowNo, ex.Message, PCISEnum.ImportTypes.Person, false);
            }
        }

        public static List<PeopleDetailsDTO> ParseData(JArray DataList, List<HelperInfoDTO> HelperList, List<CollaborationInfoDTO> collaborationInfos,
            List<RaceEthnicityDTO> raceEthnicities, List<IdentificationTypeDTO> identificationTypes, List<GenderDTO> GenderList, long agencyID, int updateUserID, ILogger log, ref string resultMessage, List<CountryCodeDTO> countryCodes)
        {
            try
            {
                int rowNo = 0;
                bool isFailed = false;
                List<PeopleDetailsDTO> peopleDataDTOList = new List<PeopleDetailsDTO>();
                foreach (var data in DataList)
                {
                    if (isFailed)
                        break;
                    rowNo++;
                    PeopleDetailsDTO PeopleData = new PeopleDetailsDTO();
                    PersonHelperDTO peopleHelperDto = new PersonHelperDTO();
                    List<PersonHelperDTO> peopleHelperDtoList = new List<PersonHelperDTO>();
                    PersonCollaborationDTO collaborationDto = new PersonCollaborationDTO();
                    List<PersonCollaborationDTO> collaborationDtoList = new List<PersonCollaborationDTO>();
                    PersonRaceEthnicityDTO raceEthnicityDto1 = new PersonRaceEthnicityDTO();
                    PersonRaceEthnicityDTO raceEthnicityDto2 = new PersonRaceEthnicityDTO();
                    PersonRaceEthnicityDTO raceEthnicityDto3 = new PersonRaceEthnicityDTO();
                    PersonRaceEthnicityDTO raceEthnicityDto4 = new PersonRaceEthnicityDTO();
                    PersonRaceEthnicityDTO raceEthnicityDto5 = new PersonRaceEthnicityDTO();
                    List<PersonRaceEthnicityDTO> raceEthnicityDtoList = new List<PersonRaceEthnicityDTO>();
                    PersonIdentificationDTO identifierDto1 = new PersonIdentificationDTO();
                    PersonIdentificationDTO identifierDto2 = new PersonIdentificationDTO();
                    PersonIdentificationDTO identifierDto3 = new PersonIdentificationDTO();
                    PersonIdentificationDTO identifierDto4 = new PersonIdentificationDTO();
                    PersonIdentificationDTO identifierDto5 = new PersonIdentificationDTO();
                    List<PersonIdentificationDTO> identifierDtoList = new List<PersonIdentificationDTO>();
                    JObject JObj = new JObject();
                    var Helper = HelperList.Where(X => X.Email.Equals(data[PCISEnum.PersonFields.HelperEmail].ToString())).ToList();
                    var Collaboration = collaborationInfos.Where(X => X.Name.Equals(data[PCISEnum.PersonFields.CollaborationName].ToString())).ToList();
                    var Ethinicity = raceEthnicities.Where(X => X.Name.Equals(data[PCISEnum.PersonFields.RaceEthnicity1].ToString())).ToList();
                    var Gender = GenderList.Where(X => X.Name.Equals(data[PCISEnum.PersonFields.IdentifiedGender].ToString())).ToList();
                    List<CountryCodeDTO> countryCode = new List<CountryCodeDTO>();
                    if (data[PCISEnum.PersonFields.Phone1Code].ToString().Contains("+"))
                    {
                        countryCode = countryCodes.Where(X => X.CountryCode.Equals(data[PCISEnum.PersonFields.Phone1Code].ToString())).ToList();
                    }
                    else
                    {
                        countryCode = countryCodes.Where(X => X.CountryCode.Equals("+" + data[PCISEnum.PersonFields.Phone1Code].ToString())).ToList();
                    }

                    if (Helper.Count == 0 || Collaboration.Count == 0 || Ethinicity.Count == 0)
                    {
                        isFailed = true;
                        var message = $"No matchng found for {PCISEnum.PersonFields.HelperEmail}/{PCISEnum.PersonFields.CollaborationName}/{PCISEnum.PersonFields.RaceEthnicity1.Replace("1", "")}.";
                        resultMessage = DraftResultMessage(data, log, rowNo, message, PCISEnum.ImportTypes.Person);
                        return null;
                    }
                    else
                    {
                        peopleHelperDto.HelperID = Helper[0].HelperID;
                        peopleHelperDto.IsLead = true;
                        peopleHelperDto.UpdateUserID = updateUserID;
                        if (string.IsNullOrEmpty(data[PCISEnum.PersonFields.HelperStartDate].ToString()))
                        {
                            resultMessage = DraftResultMessage(DataList, log, rowNo, $"Please enter valid input in  '{PCISEnum.PersonFields.HelperStartDate}'.", PCISEnum.ImportTypes.Person);
                            isFailed = true;
                            return null;
                        }
                        DateTime HelperStartDate = new DateTime();
                        if (!data[PCISEnum.PersonFields.HelperStartDate].ToString().TryParseValidDate(ref HelperStartDate))
                        {
                            resultMessage = DraftResultMessage(DataList, log, rowNo, $"Invalid date format for the column '{PCISEnum.PersonFields.HelperStartDate}'.It should be in 'MM/DD/YYYY' format.", PCISEnum.ImportTypes.Person);
                            isFailed = true;
                            return null;
                        }
                        if (HelperStartDate == DateTime.MinValue)
                        {
                            resultMessage = DraftResultMessage(DataList, log, rowNo, $"Please enter valid input in '{PCISEnum.PersonFields.HelperStartDate}'.", PCISEnum.ImportTypes.Person);
                            isFailed = true;
                            return null;
                        }
                        if (Helper[0].EndDate != null && HelperStartDate.Date > Helper[0].EndDate.Value.Date)
                        {
                            resultMessage = DraftResultMessage(DataList, log, rowNo, $"Please enter valid input in '{PCISEnum.PersonFields.HelperStartDate}'. It should be less than '{Helper[0].EndDate.Value.Date.ToString("MMM dd,yyyy")}.", PCISEnum.ImportTypes.Person);
                            isFailed = true;
                            return null;
                        }
                        if (Helper[0].StartDate != null && HelperStartDate.Date < Helper[0].StartDate.Date)
                        {
                            resultMessage = DraftResultMessage(DataList, log, rowNo, $"Please enter valid input in '{PCISEnum.PersonFields.HelperStartDate}'. It should be greater than '{Helper[0].StartDate.Date.ToString("MMM dd,yyyy")}.", PCISEnum.ImportTypes.Person);
                            isFailed = true;
                            return null;
                        }
                        peopleHelperDto.StartDate = HelperStartDate;
                        collaborationDto.CollaborationID = Collaboration[0].CollaborationID;
                        if (string.IsNullOrEmpty(data[PCISEnum.PersonFields.CollaborationStartDate].ToString()))
                        {
                            resultMessage = DraftResultMessage(DataList, log, rowNo, $"Please enter valid input in  '{PCISEnum.PersonFields.CollaborationStartDate}'.", PCISEnum.ImportTypes.Person);
                            isFailed = true;
                            return null;
                        }
                        DateTime CollaborationStartDate = new DateTime(); ;
                        if (!data[PCISEnum.PersonFields.CollaborationStartDate].ToString().TryParseValidDate(ref CollaborationStartDate))
                        {
                            resultMessage = DraftResultMessage(DataList, log, rowNo, $"Invalid date format for the column '{PCISEnum.PersonFields.CollaborationStartDate}'.It should be in 'MM/DD/YYYY' format.", PCISEnum.ImportTypes.Person);
                            isFailed = true;
                            return null;
                        }
                        if (CollaborationStartDate == DateTime.MinValue)
                        {
                            resultMessage = DraftResultMessage(DataList, log, rowNo, $"Please enter valid input in  '{PCISEnum.PersonFields.CollaborationStartDate}'.", PCISEnum.ImportTypes.Person);
                            isFailed = true;
                            return null;
                        }
                        if (Collaboration[0].EndDate != null && CollaborationStartDate.Date > Collaboration[0].EndDate.Value.Date)
                        {
                            resultMessage = DraftResultMessage(DataList, log, rowNo, $"Please enter valid input in '{PCISEnum.PersonFields.CollaborationStartDate}'. It should be less than '{Collaboration[0].EndDate.Value.Date.ToString("MMM dd,yyyy")}.", PCISEnum.ImportTypes.Person);
                            isFailed = true;
                            return null;
                        }
                        if (Collaboration[0].StartDate != null && CollaborationStartDate.Date < Collaboration[0].StartDate.Date)
                        {
                            resultMessage = DraftResultMessage(DataList, log, rowNo, $"Please enter valid input in '{PCISEnum.PersonFields.CollaborationStartDate}'. It should be greater than '{Collaboration[0].StartDate.Date.ToString("MMM dd,yyyy")}.", PCISEnum.ImportTypes.Person);
                            isFailed = true;
                            return null;
                        }
                        collaborationDto.EnrollDate = CollaborationStartDate;
                        collaborationDto.UpdateUserID = updateUserID;
                        collaborationDto.IsPrimary = true;
                        raceEthnicityDto1.RaceEthnicityID = Ethinicity[0].RaceEthnicityID;
                        identifierDto1.IdentificationNumber = data[PCISEnum.PersonFields.IdentifierTypeID1].ToString();
                        peopleHelperDtoList.Add(peopleHelperDto);
                        collaborationDtoList.Add(collaborationDto);
                        raceEthnicityDtoList.Add(raceEthnicityDto1);

                        if (!string.IsNullOrEmpty(data[PCISEnum.PersonFields.RaceEthnicity2].ToString()))
                        {
                            Ethinicity = raceEthnicities.Where(X => X.Name.Equals(data[PCISEnum.PersonFields.RaceEthnicity2].ToString())).ToList();
                            if (Ethinicity.Count > 0)
                            {
                                raceEthnicityDto2.RaceEthnicityID = Ethinicity[0].RaceEthnicityID;
                                raceEthnicityDtoList.Add(raceEthnicityDto2);
                            }
                        }
                        if (!string.IsNullOrEmpty(data[PCISEnum.PersonFields.RaceEthnicity3].ToString()))
                        {
                            Ethinicity = raceEthnicities.Where(X => X.Name.Equals(data[PCISEnum.PersonFields.RaceEthnicity3].ToString())).ToList();
                            if (Ethinicity.Count > 0)
                            {
                                raceEthnicityDto3.RaceEthnicityID = Ethinicity[0].RaceEthnicityID;
                                raceEthnicityDtoList.Add(raceEthnicityDto3);
                            }
                        }
                        if (!string.IsNullOrEmpty(data[PCISEnum.PersonFields.RaceEthnicity4].ToString()))
                        {
                            Ethinicity = raceEthnicities.Where(X => X.Name.Equals(data[PCISEnum.PersonFields.RaceEthnicity4].ToString())).ToList();
                            if (Ethinicity.Count > 0)
                            {
                                raceEthnicityDto4.RaceEthnicityID = Ethinicity[0].RaceEthnicityID;
                                raceEthnicityDtoList.Add(raceEthnicityDto4);
                            }
                        }
                        if (!string.IsNullOrEmpty(data[PCISEnum.PersonFields.RaceEthnicity5].ToString()))
                        {
                            Ethinicity = raceEthnicities.Where(X => X.Name.Equals(data[PCISEnum.PersonFields.RaceEthnicity5].ToString())).ToList();
                            if (Ethinicity.Count > 0)
                            {
                                raceEthnicityDto5.RaceEthnicityID = Ethinicity[0].RaceEthnicityID;
                                raceEthnicityDtoList.Add(raceEthnicityDto5);
                            }
                        }

                        var Identifier = new List<IdentificationTypeDTO>();
                        if (!string.IsNullOrEmpty(data[PCISEnum.PersonFields.IdentifierType1].ToString()))
                        {
                            Identifier = identificationTypes.Where(X => X.Name.Equals(data[PCISEnum.PersonFields.IdentifierType1].ToString())).ToList();
                            if (Identifier.Count > 0)
                            {
                                if (!ValidateString(data[PCISEnum.PersonFields.IdentifierTypeID1].ToString()))
                                {
                                    var message = $"Column {PCISEnum.PersonFields.IdentifierTypeID1} should be valid. ";
                                    isFailed = true;
                                    resultMessage = DraftResultMessage(data, log, rowNo, message, PCISEnum.ImportTypes.Person);
                                    return null;
                                }
                                identifierDto1.IdentificationTypeID = Identifier[0].IdentificationTypeID;
                                identifierDto1.IdentificationNumber = data[PCISEnum.PersonFields.IdentifierTypeID1].ToString();
                                identifierDto1.UpdateUserID = updateUserID;
                                identifierDtoList.Add(identifierDto1);
                            }
                        }
                        if (!string.IsNullOrEmpty(data[PCISEnum.PersonFields.IdentifierType2].ToString()))
                        {
                            Identifier = identificationTypes.Where(X => X.Name.Equals(data[PCISEnum.PersonFields.IdentifierType2].ToString())).ToList();
                            if (Identifier.Count > 0)
                            {
                                if (string.IsNullOrEmpty(data[PCISEnum.PersonFields.IdentifierTypeID2].ToString()) ||
                                    !ValidateString(data[PCISEnum.PersonFields.IdentifierTypeID2].ToString()))
                                {
                                    var message = $"Column {PCISEnum.PersonFields.IdentifierTypeID2} should be valid. ";
                                    isFailed = true;
                                    resultMessage = DraftResultMessage(data, log, rowNo, message, PCISEnum.ImportTypes.Person);
                                    return null;
                                }
                                identifierDto2.IdentificationTypeID = Identifier[0].IdentificationTypeID;
                                identifierDto2.IdentificationNumber = data[PCISEnum.PersonFields.IdentifierTypeID2].ToString();
                                identifierDto2.UpdateUserID = updateUserID;
                                identifierDtoList.Add(identifierDto2);
                            }
                        }
                        if (!string.IsNullOrEmpty(data[PCISEnum.PersonFields.IdentifierType3].ToString()))
                        {
                            Identifier = identificationTypes.Where(X => X.Name.Equals(data[PCISEnum.PersonFields.IdentifierType3].ToString())).ToList();
                            if (Identifier.Count > 0)
                            {
                                if (string.IsNullOrEmpty(data[PCISEnum.PersonFields.IdentifierTypeID3].ToString()) ||
                                    !ValidateString(data[PCISEnum.PersonFields.IdentifierTypeID3].ToString()))
                                {
                                    var message = $"Column {PCISEnum.PersonFields.IdentifierTypeID3} should be valid. ";
                                    isFailed = true;
                                    resultMessage = DraftResultMessage(data, log, rowNo, message, PCISEnum.ImportTypes.Person);
                                    return null;
                                }
                                identifierDto3.IdentificationTypeID = Identifier[0].IdentificationTypeID;
                                identifierDto3.IdentificationNumber = data[PCISEnum.PersonFields.IdentifierTypeID3].ToString();
                                identifierDto3.UpdateUserID = updateUserID;
                                identifierDtoList.Add(identifierDto3);
                            }
                        }
                        if (!string.IsNullOrEmpty(data[PCISEnum.PersonFields.IdentifierType4].ToString()))
                        {
                            Identifier = identificationTypes.Where(X => X.Name.Equals(data[PCISEnum.PersonFields.IdentifierType4].ToString())).ToList();
                            if (Identifier.Count > 0)
                            {
                                if (string.IsNullOrEmpty(data[PCISEnum.PersonFields.IdentifierTypeID4].ToString()) ||
                                   !ValidateString(data[PCISEnum.PersonFields.IdentifierTypeID4].ToString()))
                                {
                                    var message = $"Column {PCISEnum.PersonFields.IdentifierTypeID4} should be valid. ";
                                    isFailed = true;
                                    resultMessage = DraftResultMessage(data, log, rowNo, message, PCISEnum.ImportTypes.Person);
                                    return null;
                                }
                                identifierDto4.IdentificationTypeID = Identifier[0].IdentificationTypeID;
                                identifierDto4.IdentificationNumber = data[PCISEnum.PersonFields.IdentifierTypeID4].ToString();
                                identifierDto4.UpdateUserID = updateUserID;
                                identifierDtoList.Add(identifierDto4);
                            }
                        }
                        if (!string.IsNullOrEmpty(data[PCISEnum.PersonFields.IdentifierType5].ToString()))
                        {
                            Identifier = identificationTypes.Where(X => X.Name.Equals(data[PCISEnum.PersonFields.IdentifierType5].ToString())).ToList();
                            if (Identifier.Count > 0)
                            {
                                if (string.IsNullOrEmpty(data[PCISEnum.PersonFields.IdentifierTypeID5].ToString()) ||
                                    !ValidateString(data[PCISEnum.PersonFields.IdentifierTypeID5].ToString()))
                                {
                                    var message = $"Column {PCISEnum.PersonFields.IdentifierTypeID5} should be valid. ";
                                    isFailed = true;
                                    resultMessage = DraftResultMessage(data, log, rowNo, message, PCISEnum.ImportTypes.Person);
                                    return null;
                                }
                                identifierDto5.IdentificationTypeID = Identifier[0].IdentificationTypeID;
                                identifierDto5.IdentificationNumber = data[PCISEnum.PersonFields.IdentifierTypeID5].ToString();
                                identifierDto5.UpdateUserID = updateUserID;
                                identifierDtoList.Add(identifierDto5);
                            }
                        }
                        if (Gender.Count == 0)
                        {
                            var message = $"Column {PCISEnum.PersonFields.IdentifiedGender} should be valid. ";
                            isFailed = true;
                            resultMessage = DraftResultMessage(data, log, rowNo, message, PCISEnum.ImportTypes.Person);
                            return null;
                        }
                        PeopleData.GenderID = Gender[0].identifiedGenderID;
                        if (!ValidateString(data[PCISEnum.PersonFields.FirstName].ToString()))
                        {
                            var message = $"Column {PCISEnum.PersonFields.FirstName} should be valid. ";
                            isFailed = true;
                            resultMessage = DraftResultMessage(data, log, rowNo, message, PCISEnum.ImportTypes.Person);
                            return null;
                        }
                        PeopleData.FirstName = data[PCISEnum.PersonFields.FirstName].ToString();
                        if (!ValidateString(data[PCISEnum.PersonFields.LastName].ToString()))
                        {
                            var message = $"Column {PCISEnum.PersonFields.LastName} should be valid. ";
                            isFailed = true;
                            resultMessage = DraftResultMessage(data, log, rowNo, message, PCISEnum.ImportTypes.Person);
                            return null;
                        }
                        PeopleData.LastName = data[PCISEnum.PersonFields.LastName].ToString();
                        if (!string.IsNullOrWhiteSpace(data[PCISEnum.PersonFields.Email].ToString()))
                        {
                            bool isEmail = data[PCISEnum.PersonFields.Email].ToString().IsValidEmail();
                            if (!isEmail)
                            {
                                var message = $"Column {PCISEnum.PersonFields.Email} should be either empty or valid.";
                                isFailed = true;
                                resultMessage = DraftResultMessage(data, log, rowNo, message, PCISEnum.ImportTypes.Person);
                                return null;
                            }
                            PeopleData.Email = data[PCISEnum.PersonFields.Email].ToString();
                        }
                        if (!string.IsNullOrWhiteSpace(data[PCISEnum.PersonFields.Phone1].ToString()))
                        {
                            if (!ValidateNumber(data[PCISEnum.PersonFields.Phone1].ToString()))
                            {
                                var message = $"Column {PCISEnum.PersonFields.Phone1} should be either empty or valid.";
                                isFailed = true;
                                resultMessage = DraftResultMessage(data, log, rowNo, message, PCISEnum.ImportTypes.Person);
                                return null;
                            }
                            PeopleData.Phone1 = data[PCISEnum.PersonFields.Phone1].ToString();
                        }
                        if (!string.IsNullOrWhiteSpace(data[PCISEnum.PersonFields.Phone1Code].ToString()))
                        {
                            if (!ValidateNumber(data[PCISEnum.PersonFields.Phone1Code].ToString().Replace("+", "")))
                            {
                                var message = $"Column {PCISEnum.PersonFields.Phone1Code} should be either empty or valid.";
                                isFailed = true;
                                resultMessage = DraftResultMessage(data, log, rowNo, message, PCISEnum.ImportTypes.Person);
                                return null;
                            }
                            if (countryCode.Count == 0)
                            {
                                var message = $"Column {PCISEnum.PersonFields.Phone1Code} should be either empty or valid.";
                                isFailed = true;
                                resultMessage = DraftResultMessage(data, log, rowNo, message, PCISEnum.ImportTypes.Person);
                                return null;
                            }
                            PeopleData.Phone1Code = countryCode[0]?.CountryCode;
                        }
                        // PeopleData.GenderID = data[PCISEnum.PersonFields.IdentifiedGender].ToString();
                        PeopleData.PersonHelpers = peopleHelperDtoList;
                        PeopleData.PersonCollaborations = collaborationDtoList;
                        PeopleData.PersonRaceEthnicities = raceEthnicityDtoList;
                        PeopleData.PersonIdentifications = identifierDtoList;
                        PeopleData.AgencyID = agencyID;
                        PeopleData.UpdateUserID = updateUserID;
                        PeopleData.PersonScreeningStatusID = 1;
                        peopleDataDTOList.Add(PeopleData);
                        DateTime dob = new DateTime(); 
                        if (!data[PCISEnum.PersonFields.DateOfBirth].ToString().TryParseValidDate(ref dob))
                        {
                            resultMessage = DraftResultMessage(DataList, log, rowNo, $"Invalid date format for the column '{PCISEnum.PersonFields.DateOfBirth}'.It should be in 'MM/DD/YYYY' format.", PCISEnum.ImportTypes.Person);
                            isFailed = true;
                            return null;
                        }
                        if (dob > DateTime.Today)
                        {
                            resultMessage = DraftResultMessage(DataList, log, rowNo, $"Invalid date format for the column '{PCISEnum.PersonFields.DateOfBirth}'. Future dates will not be accepted.", PCISEnum.ImportTypes.Person);
                            isFailed = true;
                            return null;
                        }
                        PeopleData.DateOfBirth = dob;
                    }


                }
                if (!isFailed)
                {
                    return peopleDataDTOList;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        private static string GetConfigurationValue(Utility Utility, string apiurl, string configurationKey, ILogger log)
        {
            try
            {
                string configuration = string.Empty;
                var configAPiurl = apiurl + PCISEnum.APIurl.ConfigurationWithKey.Replace(PCISEnum.APIReplacableValues.Key, configurationKey).Replace(PCISEnum.APIReplacableValues.AgencyID, "0");
                var result = Utility.RestApiCall(log, configAPiurl, false);
                if (!string.IsNullOrEmpty(result))
                {
                    var config = JsonConvert.DeserializeObject<ConfigurationResponseDTO>(result);
                    configuration = config.result.ConfigurationValue;
                }
                return configuration;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region AssessmentImport
        private static string ProcessAssessmentImport(Utility utility, string apiBaseUrl, dynamic fileList, ref Dictionary<string, LookupDTO> dict_AgencyLookups, ref Dictionary<int, string> dict_QuestionnaireTemplate, ImportEmailInputDTO importDTO, ILogger log)
        {
            int rowNo = 0;
            try
            {
                var resultMessage = string.Empty;
                List<AssessmentProgressDTO> list_assessmentProgressInputDTOs = new List<AssessmentProgressDTO>();
                string fileJsonData = fileList[PCISEnum.FileImportFields.JsonData];
                var assessmentData = JArray.Parse(fileJsonData);
                int questionnaireID = fileList[PCISEnum.FileImportFields.QuestionnaireID];
                string agencyLookupKey = string.Format("{0}-{1}", importDTO.AgencyID, questionnaireID);

                #region FetchLookupsAndDefaultTemplate
                //Fetching Lookups related to assessment and questionnaireItemResponses and keeping in dictionary Lookup against agencyID..
                if (!dict_AgencyLookups.ContainsKey(agencyLookupKey))
                {
                    /*------------------------------Lookups-------------------------------*/
                    log.LogInformation($"Assessment-Import : {DateTime.Now} : Fetching lookups for agency-{importDTO.AgencyID}..");
                    var lookupsAll = GetPCISAgencyLookups(utility, apiBaseUrl, importDTO.AgencyID.ToString(), log, questionnaireID.ToString());
                    if (lookupsAll == null)
                    {
                        resultMessage = $"Assessment-Import : {DateTime.Now} : Fetching lookups Failed..";
                        log.LogInformation(resultMessage);
                        return resultMessage;
                    }
                    dict_AgencyLookups.Add(agencyLookupKey, lookupsAll);
                }

                //Fetching dynamic template related to questionnaire and keeping in dictionary Lookup against questionnaireID..
                if (!dict_QuestionnaireTemplate.ContainsKey(questionnaireID))
                {
                    log.LogInformation($"Assessment-Import : {DateTime.Now} : Fetching AssessmentTemplate for QuestionnaireID-{questionnaireID}..");
                    var assessmentTemplate = GetAssessmentTemplate(utility, apiBaseUrl, importDTO.AgencyID.ToString(), log, questionnaireID.ToString());
                    if (assessmentTemplate == null)
                    {
                        resultMessage = $"Assessment-Import : {DateTime.Now} : Fetching AssessmentTemplate Failed..";
                        log.LogInformation(resultMessage);
                        return resultMessage;
                    }
                    dict_QuestionnaireTemplate.Add(questionnaireID, assessmentTemplate.TemplateJson);
                }
                #endregion

                #region ProcessTheImportedFileDataIfLookupsExists
                //If default templates and lookups are ready move forward with fileimport..
                if (dict_QuestionnaireTemplate.ContainsKey(questionnaireID) && dict_AgencyLookups.ContainsKey(agencyLookupKey))
                {
                    log.LogInformation($"Assessment-Import : {DateTime.Now} : FileImportID-{importDTO.ImportFileID} : QuestionnaireID-{questionnaireID} : Total Rows to upload = {assessmentData.Count}");
                    var dynfileJsonData = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(fileJsonData);

                    //Validating the headers of csv and template..Filtering out the dynamic columns or questionnaireItems....
                    log.LogInformation($"Assessment-Import : {DateTime.Now} : Prepare dynamic column headers..");
                    var filteredDynamicHeadersInTemplate = PrepareDynamicColumnHeadersForAssessmentUpload(dynfileJsonData[0], dict_QuestionnaireTemplate[questionnaireID].ToString(), log, ref resultMessage);
                    if (filteredDynamicHeadersInTemplate != null)
                    {
                        //Validate the assessment basic details and fecth persons with their supports and helpers from PCIS in a dictionnary against personindex...
                        log.LogInformation($"Assessment-Import : {DateTime.Now} : Validate and fetch person details from PCIS..");
                        var dict_personList = ValidateAndGetPersonVoiceTypeDetailsToUpload(utility, assessmentData, importDTO.AgencyID, apiBaseUrl, log, ref resultMessage);
                        if (dict_personList.Count > 0 && resultMessage.IsNullOrEmpty())
                        {
                            //Start records row by row processing..
                            bool isFailed = false;
                            foreach (var assessmentRow in assessmentData)
                            {
                                rowNo++;
                                string personIndex = assessmentRow[PCISEnum.AsessmentTemplateFixedFields.PersonIndex].TrimToString();
                                if (dict_personList.ContainsKey(personIndex.ToLower()) && !isFailed)
                                {
                                    string assessmentDateTaken = assessmentRow[PCISEnum.AsessmentTemplateFixedFields.AssessmentDateTaken].TrimToString();
                                    string reasoningText = assessmentRow[PCISEnum.AsessmentTemplateFixedFields.ReasoningText].TrimToString();
                                    string assessmentReason = assessmentRow[PCISEnum.AsessmentTemplateFixedFields.AssessmentReason].TrimToString();
                                    string voiceType = assessmentRow[PCISEnum.AsessmentTemplateFixedFields.VoiceType].TrimToString();
                                    string personSupportID = assessmentRow[PCISEnum.AsessmentTemplateFixedFields.PersonSupportID].TrimToString();
                                    string helperEmail = assessmentRow[PCISEnum.AsessmentTemplateFixedFields.HelperEmail].TrimToString();
                                    string assessmentStatus = assessmentRow[PCISEnum.AsessmentTemplateFixedFields.AssessmentStatus].TrimToString();
                                    string triggeringEventDate = string.Empty;
                                    string triggeringEventNotes = string.Empty;
                                    string sumOfAssessmentDetails = personIndex + assessmentDateTaken + assessmentStatus + assessmentReason + voiceType + personSupportID + helperEmail;
                                    if (sumOfAssessmentDetails.IsNullOrEmpty())
                                    {
                                        continue;
                                    }
                                    if (assessmentReason.TrimToLower() == PCISEnum.AssessmentReason.TriggeringEvent.TrimToLower())
                                    {
                                        triggeringEventDate = assessmentRow[PCISEnum.AsessmentTemplateFixedFields.TriggeringEventDate].ToString();
                                        triggeringEventNotes = assessmentRow[PCISEnum.AsessmentTemplateFixedFields.TriggeringEventNotes].ToString();
                                    }

                                    #region ReplaceTheLookupValuesWithIDs
                                    //-------------------------------------Replace Assessment lookups---------------------------------------

                                    string assessmentReasonID = ValidateLookup(PCISEnum.ImportAssessmentLookups.AssessmentReasons, assessmentReason, dict_AgencyLookups[agencyLookupKey]);
                                    string voiceTypeID = ValidateLookup(PCISEnum.ImportAssessmentLookups.VoiceTypes, voiceType, dict_AgencyLookups[agencyLookupKey]);
                                    string assessmentStatusID = ValidateLookup(PCISEnum.ImportAssessmentLookups.AssessmentStatus, assessmentStatus, dict_AgencyLookups[agencyLookupKey]);

                                    string voiceTypeFKID = null;
                                    if (voiceType == PCISEnum.VoiceType.Support)
                                        voiceTypeFKID = GetVoiceTypeFKID(dict_personList[personIndex.ToLower()], voiceType, personSupportID);
                                    else if (voiceType == PCISEnum.VoiceType.Helper)
                                        voiceTypeFKID = GetVoiceTypeFKID(dict_personList[personIndex.ToLower()], voiceType, helperEmail);
                                    if ((voiceType == PCISEnum.VoiceType.Support || voiceType == PCISEnum.VoiceType.Helper) && voiceTypeFKID.IsNullOrEmpty())
                                    {
                                        var inputheader = voiceType == PCISEnum.VoiceType.Support ? PCISEnum.AsessmentTemplateFixedFields.PersonSupportID : PCISEnum.AsessmentTemplateFixedFields.HelperEmail;
                                        resultMessage = $"Please give valid input in '{inputheader}' for the voicetype '{voiceType}'.";
                                        resultMessage = DraftResultMessage(assessmentRow, log, rowNo, resultMessage, PCISEnum.ImportTypes.Assessment);
                                        list_assessmentProgressInputDTOs.Clear();
                                        isFailed = true;
                                        break;
                                    }
                                    #endregion
                                    #region AndCreateAssessmentObject
                                    ///------------------------------------Prepare Assessment Response Object-----------------------------
                                    var assessmentResponses = CreateAssessmentResponses(assessmentRow, filteredDynamicHeadersInTemplate, dict_AgencyLookups[agencyLookupKey].QuestionItemDetails, dict_personList[personIndex.ToLower()], assessmentStatus, rowNo, log, ref resultMessage);
                                    if (!resultMessage.IsNullOrEmpty())
                                    {
                                        isFailed = true;
                                        list_assessmentProgressInputDTOs.Clear();
                                        break;
                                    }
                                    if (assessmentStatus == PCISEnum.AssessmentStatus.Submitted && assessmentResponses != null)
                                    {
                                        assessmentResponses = UpdatePriorityOnAssessmentResponses(assessmentResponses);
                                    }
                                    ///----------------Prepare Assessment Object and add responses if no errors in building response object-------------
                                    AssessmentProgressDTO assessmentProgressInputDTO = new AssessmentProgressDTO();
                                    assessmentProgressInputDTO.AssessmentID = 0;
                                    assessmentProgressInputDTO.PersonID = dict_personList[personIndex.ToLower()].PersonID;
                                    assessmentProgressInputDTO.PersonIndex = personIndex.ToGuid();
                                    assessmentProgressInputDTO.AssessmentReasonID = assessmentReasonID.ToInt();
                                    assessmentProgressInputDTO.AssessmentStatus = assessmentStatus;
                                    assessmentProgressInputDTO.AssessmentStatusID = assessmentStatusID.ToInt();
                                    assessmentProgressInputDTO.VoiceTypeFKID = voiceTypeFKID.ToLongNull();
                                    assessmentProgressInputDTO.VoiceTypeID = voiceTypeID.ToInt();
                                    assessmentProgressInputDTO.ReasoningText = reasoningText;
                                    assessmentProgressInputDTO.DateTaken = assessmentDateTaken.ToDateTime();
                                    assessmentProgressInputDTO.EventDate = triggeringEventDate.ToDateTimeNull();
                                    assessmentProgressInputDTO.EventNotes = triggeringEventNotes;
                                    assessmentProgressInputDTO.QuestionnaireID = questionnaireID;
                                    assessmentProgressInputDTO.AssessmentResponses = new List<AssessmentResponseInputDTO>();
                                    assessmentProgressInputDTO.UpdateUserID = importDTO.HelperUserID;
                                    assessmentProgressInputDTO.CloseDate = null;
                                    assessmentProgressInputDTO.AssessmentResponses = assessmentResponses;
                                    list_assessmentProgressInputDTOs.Add(assessmentProgressInputDTO);
                                    #endregion
                                }
                                else
                                {
                                    return resultMessage;
                                }
                            }
                            if (list_assessmentProgressInputDTOs.Count > 0)
                            {
                                //Upload the result to PCIS..........
                                log.LogInformation($"Assessment-Import : {DateTime.Now} : FileImportID-{importDTO.ImportFileID} : QuestionnaireID-{questionnaireID} : *****PCIS Upload for Assessment Records start*****");
                                var result = UploadToPCIS(utility, apiBaseUrl, importDTO.AgencyID, questionnaireID, list_assessmentProgressInputDTOs, importDTO, log);
                                if (result?.result.ResponseStatusCode == PCISEnum.Constants.InsertionSuccess)
                                {
                                    resultMessage = DraftResultMessage(null, log, 0, string.Empty, PCISEnum.ImportTypes.Assessment, true, list_assessmentProgressInputDTOs.Count);
                                }
                                else
                                {
                                    resultMessage = DraftResultMessage(null, log, 0, string.Empty, PCISEnum.ImportTypes.Assessment, false, list_assessmentProgressInputDTOs.Count);
                                }
                                log.LogInformation($"Assessment-Import : {DateTime.Now} : FileImportID-{importDTO.ImportFileID} : QuestionnaireID-{questionnaireID} : *****PCIS Upload for Assessment Records End*****");
                                return resultMessage;
                            }
                        }
                    }
                }
                #endregion

                return resultMessage;
            }
            catch (Exception ex)
            {
                log.LogError($"Assessment-Import : {DateTime.Now} : ImportTypeID-{0} : Exception occurred at {MethodInfo.GetCurrentMethod().Name} : {ex.Message}");
                return DraftResultMessage(null, log, rowNo, ex.Message, PCISEnum.ImportTypes.Assessment, false);
            }
        }

        private static List<AssessmentResponseInputDTO> UpdatePriorityOnAssessmentResponses(List<AssessmentResponseInputDTO> assessmentResponses)
        {
            try
            {
                if (assessmentResponses.Count > 0)
                {
                    var listResponsesWithNeedForFocus = assessmentResponses.Where(x => x.ItemResponseBehaviorID == 1).ToList();
                    listResponsesWithNeedForFocus = listResponsesWithNeedForFocus.OrderByDescending(x => x.ResponseValue).ThenBy(x => x.ItemListOrder).ToList();
                    int priority = 1;
                    foreach (var item in listResponsesWithNeedForFocus)
                    {
                        foreach (var response in assessmentResponses)
                        {
                            if (response.AssessmentResponseGUID == item.AssessmentResponseGUID)
                            {
                                response.Priority = priority;
                                priority++;
                                break;
                            }
                        }
                    }
                }
                return assessmentResponses;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private static CRUDResponseDTO UploadToPCIS(Utility utility, string apiBaseUrl, long agencyID, int questionnaireID, List<AssessmentProgressDTO> list_uploadAssessmentDTO, ImportEmailInputDTO importDTO, ILogger log)
        {
            try
            {
                string result = string.Empty, returnMessage = string.Empty;
                var uploadAPIurl = apiBaseUrl + PCISEnum.APIurl.UploadAssessments;
                CRUDResponseDTO response = new CRUDResponseDTO();
                if (list_uploadAssessmentDTO.Count > 0)
                {
                    int loopCount = list_uploadAssessmentDTO.Count;
                    for (int i = 0; i < loopCount; i = i + PCISEnum.Constants.UploadBlockCount)
                    {
                        var assesssmentsToUpload = list_uploadAssessmentDTO.Skip(i).Take(PCISEnum.Constants.UploadBlockCount).ToList();
                        if (assesssmentsToUpload.Count > 0)
                        {
                            UploadAssessmentDTO uploadAssessmentDTO = new UploadAssessmentDTO();
                            uploadAssessmentDTO.AgencyID = agencyID;
                            uploadAssessmentDTO.QuestionnaireID = questionnaireID;
                            uploadAssessmentDTO.UpdateUserID = importDTO.HelperUserID;
                            uploadAssessmentDTO.AssessmentsToUpload = assesssmentsToUpload;
                            var recordCount = (i + PCISEnum.Constants.UploadBlockCount) > loopCount ? loopCount : i + PCISEnum.Constants.UploadBlockCount;
                            log.LogInformation($"Assessment-Import : {DateTime.Now} : FileImportID-{importDTO.ImportFileID} : QuestionnaireID-{questionnaireID} : {recordCount}/{loopCount} : Initiating upload to PCIS..");

                            result = utility.RestApiCall(log, uploadAPIurl, false, false, PCISEnum.APIMethodType.PostRequest, null, uploadAssessmentDTO.ToJSON());
                            response = JsonConvert.DeserializeObject<CRUDResponseDTO>(result);
                            if (response?.result?.ResponseStatusCode == PCISEnum.Constants.InsertionSuccess)
                            {
                                returnMessage = response?.result?.ResponseStatus;
                                log.LogInformation($"Assessment-Import : {DateTime.Now} : FileImportID-{importDTO.ImportFileID} : QuestionnaireID-{questionnaireID} : {recordCount}/{loopCount} : {response?.result?.ResponseStatus}");
                            }
                            else
                            {
                                returnMessage = string.IsNullOrEmpty(result) ? "Failed to Upload" : response?.result?.ResponseStatus;
                                log.LogInformation($"Assessment-Import : {DateTime.Now} : AgencyID-{agencyID} : {recordCount}/{loopCount} : {returnMessage} ");
                                //var missedPersonlist = assesssmentsToUpload.Select(x => x.UniversalID).ToArray();
                                //log.LogInformation($"EHR : {DateTime.Now} : AgencyID-{agencyId} : {recordCount}/{loopCount} : Upload failed Person list : {string.Join(",", missedPersonlist)}");
                            }
                        }
                    }
                    return response;
                }
                else
                {
                    log.LogInformation($"Assessment-Import : {DateTime.Now} : FileImportID-{importDTO.ImportFileID} : QuestionnaireID-{questionnaireID} : No records to upload");
                }
                return null;
            }
            catch (Exception ex)
            {
                log.LogError($"Assessment-Import : {DateTime.Now} : Exception occurred at {MethodInfo.GetCurrentMethod().Name} : {ex.Message}");
                throw;
            }
        }


        private static string GetVoiceTypeFKID(PersonVoiceTypeDetailsForImportDTO allpersonsLookup, string voiceType, string responseValue)
        {
            try
            {
                if (voiceType == PCISEnum.VoiceType.Support && !responseValue.IsNullOrEmpty())
                {
                    var supports = allpersonsLookup.PersonSupportlookups.Where(x => x.Id == responseValue).ToList();
                    if (supports.Count > 0)
                    {
                        return supports[0].Id;
                    }
                }
                if (voiceType == PCISEnum.VoiceType.Helper && !responseValue.IsNullOrEmpty())
                {
                    var helpers = allpersonsLookup.PersonHelperlookups.Where(x => x.Value == responseValue).ToList();
                    if (helpers.Count > 0)
                    {
                        return helpers[0].Id;
                    }
                }
                return string.Empty;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private static Dictionary<string, PersonVoiceTypeDetailsForImportDTO> ValidateAndGetPersonVoiceTypeDetailsToUpload(Utility utility, JArray assessmentData, long agencyID, string apiBaseUrl, ILogger log, ref string resultMessage)
        {
            int rowNo = 0;
            try
            {
                bool isFailed = false;
                //-----------------------------------------Fetch PersonIndexes----------------------------------------------------------
                PersonIndexToUploadDTO allPersonsToUpload = new PersonIndexToUploadDTO() { PersonIndexesToUpload = new List<string>() };
                var resultPersons = new Dictionary<string, PersonVoiceTypeDetailsForImportDTO>();
                allPersonsToUpload.AgencyID = agencyID;

                foreach (var assessmentRow in assessmentData)
                {
                    rowNo++;
                    string personIndex = assessmentRow[PCISEnum.AsessmentTemplateFixedFields.PersonIndex].TrimToString();
                    if (personIndex.ToGuid() == Guid.Empty)
                    {
                        isFailed = true;
                        break;
                    }
                    if (!allPersonsToUpload.PersonIndexesToUpload.Contains(personIndex) && !string.IsNullOrEmpty(personIndex))
                        allPersonsToUpload.PersonIndexesToUpload.Add(personIndex.Trim());
                }
                if (isFailed)
                {
                    resultMessage = $"Please give valid input in '{PCISEnum.AsessmentTemplateFixedFields.PersonIndex}'.";
                    resultMessage = DraftResultMessage(null, log, rowNo, resultMessage, PCISEnum.ImportTypes.Assessment);
                    allPersonsToUpload.PersonIndexesToUpload.Clear();
                    return resultPersons;
                }
                if (allPersonsToUpload.PersonIndexesToUpload.Count > 0)
                {
                    var lookupAPIurl = apiBaseUrl + PCISEnum.APIurl.PersonVoiceTypeDetails;
                    var allPersonsToUploadList = allPersonsToUpload.PersonIndexesToUpload.Distinct().Where(x => !string.IsNullOrEmpty(x)).ToList();
                    var personResult = utility.RestApiCall(log, lookupAPIurl, false, false, PCISEnum.APIMethodType.PostRequest, null, allPersonsToUpload.ToJSON());
                    var result = JsonConvert.DeserializeObject<LookupResponseDTO>(personResult);
                    if (result?.statusCode == 200)
                    {
                        resultPersons = result.result.PersonVoiceTypeDetails;
                    }
                    else
                    {
                        resultMessage = $"Error in processing 'PersonIndex' values of the file.";
                        resultMessage = DraftResultMessage(null, log, 0, resultMessage, PCISEnum.ImportTypes.Assessment);
                        return new Dictionary<string, PersonVoiceTypeDetailsForImportDTO>();
                    }
                }
                rowNo = 0;
                var assessmentReasonFields = typeof(PCISEnum.AssessmentReason).GetFields().ToList();
                var assessmentStatusFields = typeof(PCISEnum.AssessmentStatus).GetFields().ToList();
                var voiceTypeFields = typeof(PCISEnum.VoiceType).GetFields().ToList();
                foreach (var assessmentRow in assessmentData)
                {
                    rowNo++;
                    string personIndex = assessmentRow[PCISEnum.AsessmentTemplateFixedFields.PersonIndex].TrimToString();
                    string assessmentDateTaken = assessmentRow[PCISEnum.AsessmentTemplateFixedFields.AssessmentDateTaken].TrimToString();
                    string assessmentStatus = assessmentRow[PCISEnum.AsessmentTemplateFixedFields.AssessmentStatus].TrimToString();
                    string assessmentReason = assessmentRow[PCISEnum.AsessmentTemplateFixedFields.AssessmentReason].TrimToString();
                    string triggeringEventDate = assessmentRow[PCISEnum.AsessmentTemplateFixedFields.TriggeringEventDate].TrimToString();
                    string triggeringEventNotes = assessmentRow[PCISEnum.AsessmentTemplateFixedFields.TriggeringEventNotes].TrimToString();
                    string voiceType = assessmentRow[PCISEnum.AsessmentTemplateFixedFields.VoiceType].TrimToString();
                    string personsupportID = assessmentRow[PCISEnum.AsessmentTemplateFixedFields.PersonSupportID].TrimToString();
                    string helperEmail = assessmentRow[PCISEnum.AsessmentTemplateFixedFields.HelperEmail].TrimToString();

                    string sumOfAssessmentDetails = personIndex + assessmentDateTaken + assessmentStatus + assessmentReason + voiceType + personsupportID + helperEmail;
                    if (sumOfAssessmentDetails.IsNullOrEmpty())
                    {
                        continue;
                    }

                    if (personIndex.IsNullOrEmpty() || assessmentReason.IsNullOrEmpty() || assessmentStatus.IsNullOrEmpty() || assessmentDateTaken.IsNullOrEmpty() || voiceType.IsNullOrEmpty())
                    {
                        resultMessage = $"Please give valid input in '{PCISEnum.AsessmentTemplateFixedFields.PersonIndex}/{PCISEnum.AsessmentTemplateFixedFields.AssessmentReason}/{PCISEnum.AsessmentTemplateFixedFields.AssessmentStatus}/{PCISEnum.AsessmentTemplateFixedFields.AssessmentDateTaken}'.";
                        resultMessage = DraftResultMessage(assessmentRow, log, rowNo, resultMessage, PCISEnum.ImportTypes.Assessment);
                        resultPersons.Clear();
                        break;
                    }

                    if (!resultPersons.Select(x => x.Key).Contains(personIndex))//Validate personIndex
                    {
                        resultMessage = $"Please give valid input in '{PCISEnum.AsessmentTemplateFixedFields.PersonIndex}'.";
                        resultMessage = DraftResultMessage(null, log, rowNo, resultMessage, PCISEnum.ImportTypes.Assessment);
                        resultPersons.Clear();
                        break;
                    }

                    DateTime dateTaken = new DateTime();
                    var dateOfAssessment = assessmentDateTaken.TryParseValidDate(ref dateTaken);
                    if (!dateOfAssessment)
                    {
                        resultMessage = DraftResultMessage(assessmentRow, log, rowNo, $"Invalid date format for the column '{PCISEnum.AsessmentTemplateFixedFields.AssessmentDateTaken}'.It should be in 'MM/DD/YYYY' format.", PCISEnum.ImportTypes.Assessment);
                        resultPersons.Clear();
                        break;
                    }
                    if (dateTaken.Date > DateTime.UtcNow.Date)
                    {
                        resultMessage = DraftResultMessage(assessmentRow, log, rowNo, $"Future dates are not allowed in '{PCISEnum.AsessmentTemplateFixedFields.AssessmentDateTaken}'.", PCISEnum.ImportTypes.Assessment);
                        resultPersons.Clear();
                        break;
                    }
                    if (voiceType == PCISEnum.VoiceType.Support && string.IsNullOrEmpty(personsupportID))
                    {
                        resultMessage = $"Please give valid input in '{PCISEnum.AsessmentTemplateFixedFields.PersonSupportID}'.";
                        resultMessage = DraftResultMessage(assessmentRow, log, rowNo, resultMessage, PCISEnum.ImportTypes.Assessment);
                        resultPersons.Clear();
                        break;
                    }
                    if (voiceType == PCISEnum.VoiceType.Helper && string.IsNullOrEmpty(helperEmail))
                    {
                        resultMessage = $"Please give valid input in '{PCISEnum.AsessmentTemplateFixedFields.HelperEmail}'.";
                        resultMessage = DraftResultMessage(assessmentRow, log, rowNo, resultMessage, PCISEnum.ImportTypes.Assessment);
                        resultPersons.Clear();
                        break;
                    }
                    if (assessmentReason.TrimToLower() == PCISEnum.AssessmentReason.TriggeringEvent.TrimToLower())
                    {
                        DateTime trigerringDate = new DateTime();
                        if (triggeringEventNotes.IsNullOrEmpty())
                        {
                            resultMessage = $"Please give valid input in '{PCISEnum.AsessmentTemplateFixedFields.TriggeringEventNotes}'.";
                            resultMessage = DraftResultMessage(assessmentRow, log, rowNo, resultMessage, PCISEnum.ImportTypes.Assessment);
                            resultPersons.Clear();
                            break;
                        }
                        var dateOfTrigger = triggeringEventDate.TryParseValidDate(ref trigerringDate);
                        if (!dateOfTrigger || triggeringEventDate.IsNullOrEmpty())
                        {
                            resultMessage = $"Please give valid input in '{PCISEnum.AsessmentTemplateFixedFields.TriggeringEventDate}.It should be in 'MM/DD/YYYY' format.'.";
                            resultMessage = DraftResultMessage(assessmentRow, log, rowNo, resultMessage, PCISEnum.ImportTypes.Assessment);
                            resultPersons.Clear();
                            break;
                        }
                        if (trigerringDate > DateTime.UtcNow.Date)
                        {
                            resultMessage = $"Future dates are not allowed in '{PCISEnum.AsessmentTemplateFixedFields.TriggeringEventDate}'.";
                            resultMessage = DraftResultMessage(assessmentRow, log, rowNo, resultMessage, PCISEnum.ImportTypes.Assessment);
                            resultPersons.Clear();
                            break;
                        }
                    }
                    if (!(assessmentReasonFields.Select(x => x.Name.TrimToLower()).ToList().Contains(assessmentReason.TrimToLower())))
                    {
                        resultMessage = $"Please give valid input in '{PCISEnum.AsessmentTemplateFixedFields.AssessmentReason}'.";
                        resultMessage = DraftResultMessage(assessmentRow, log, rowNo, resultMessage, PCISEnum.ImportTypes.Assessment);
                        resultPersons.Clear();
                        break;
                    }
                    if (!assessmentStatusFields.Select(x => x.Name.TrimToLower()).Contains(assessmentStatus.TrimToLower()))
                    {
                        resultMessage = $"Please give valid input in '{PCISEnum.AsessmentTemplateFixedFields.AssessmentStatus}'.";
                        resultMessage = DraftResultMessage(assessmentRow, log, rowNo, resultMessage, PCISEnum.ImportTypes.Assessment);
                        resultPersons.Clear();
                        break;
                    }
                    if (!voiceTypeFields.Select(x => x.Name.TrimToLower()).Contains(voiceType.TrimToLower()))
                    {
                        resultMessage = $"Please give valid input in '{PCISEnum.AsessmentTemplateFixedFields.VoiceType}'.";
                        resultMessage = DraftResultMessage(assessmentRow, log, rowNo, resultMessage, PCISEnum.ImportTypes.Assessment);
                        resultPersons.Clear();
                        break;
                    }
                }
                //resultMessage = string.IsNullOrEmpty(resultMessage) ? "Error in processing the file" : resultMessage;
                return resultPersons;
            }
            catch (Exception ex)
            {
                log.LogError($"Assessment-Import : {DateTime.Now} : RecordNo-{rowNo} : Exception occurred at {MethodInfo.GetCurrentMethod().Name} : {ex.Message}");
                throw;
            }
        }

        private static List<AssessmentResponseInputDTO> CreateAssessmentResponses(JToken assessmentRow, Dictionary<string, string> filteredDynamicHeadersInTemplate, List<QuestionnaireItemsForImportDTO> questionnaireItemsInDBWithResponseDetails, PersonVoiceTypeDetailsForImportDTO personSupportDetails, string assessmentStatus, int rowNo, ILogger log, ref string resultMessage)
        {
            try
            {
                Regex regx = new Regex(PCISEnum.Constants.CaregiverRegx1);
                Regex regx1 = new Regex(PCISEnum.Constants.CaregiverRegx2);
                bool isFailed = false;
                var dict_QItemResponses = questionnaireItemsInDBWithResponseDetails.ToDictionary(x => x, x => JsonConvert.DeserializeObject<List<Response>>(x.Responses));
                List<AssessmentResponseInputDTO> list_assessmentResponseInputDTO = new List<AssessmentResponseInputDTO>();
                int? careGiverID = null; int itemResponseBehaviourID = 0;
                List<int?> caregiverList = new List<int?>();
                ///Processing columns based on the dynamic headers created for creating AssessmentResponseList..
                foreach (var columns in filteredDynamicHeadersInTemplate)
                {
                    var assessmentResponseInputDTO = new AssessmentResponseInputDTO();
                    assessmentResponseInputDTO.AssessmentResponseGUID = Guid.NewGuid();
                    assessmentResponseInputDTO.AssessmentResponseID = 0;
                    assessmentResponseInputDTO.IsOtherConfidential = false;
                    assessmentResponseInputDTO.IsRequiredConfidential = false;
                    assessmentResponseInputDTO.IsCloned = false;
                    assessmentResponseInputDTO.Priority = null;
                    var questionItemID = columns.Value.ToString().ToInt();
                    assessmentResponseInputDTO.QuestionnaireItemID = questionItemID;
                    var response = assessmentRow[columns.Key].TrimToString();
                    assessmentResponseInputDTO.PersonSupportID = careGiverID;

                    var caregivrCol = regx1.Matches(columns.Key).Count;
                    if (assessmentStatus.TrimToLower() != PCISEnum.AssessmentStatus.InProgress.TrimToLower() && (response.IsNullOrEmpty() && caregivrCol == 0))
                    {
                        resultMessage = $"Please give valid label for '{columns.Key}', since assessment is having '{assessmentStatus}' status.";
                        //resultMessage = $"Assessment with '{assessmentStatus}' status should not have null value for '{columns.Key}'";
                        resultMessage = DraftResultMessage(null, log, rowNo, resultMessage, PCISEnum.ImportTypes.Assessment);
                        isFailed = true;
                        break;
                    }
                    //--------------------------------Caregiver matchng to fetch personsupportID-------------------------------------
                    if (regx.Matches(columns.Key).Count > 0)
                    {
                        careGiverID = null;
                        var careGiver = GetVoiceTypeFKID(personSupportDetails, PCISEnum.VoiceType.Support, response);
                        if (careGiver.IsNullOrEmpty() && !response.IsNullOrEmpty())
                        {
                            resultMessage = $"Please give valid input in '{columns.Key}'.";
                            resultMessage = DraftResultMessage(null, log, rowNo, resultMessage, PCISEnum.ImportTypes.Assessment);
                            isFailed = true;
                            break;
                        }
                        else if (!careGiver.IsNullOrEmpty())
                        {
                            careGiverID = careGiver.ToIntNull();
                            if (caregiverList.Contains(careGiverID))
                            {
                                resultMessage = $"Please give valid input in '{columns.Key}'.(Already Used)";
                                resultMessage = DraftResultMessage(null, log, rowNo, resultMessage, PCISEnum.ImportTypes.Assessment);
                                isFailed = true;
                                break;
                            }
                            caregiverList.Add(careGiverID);
                        }
                        continue;
                    }
                    if ((caregivrCol > 0 && careGiverID != null) || caregivrCol == 0)
                    {
                        //Process the values/Labels for each item and Fetchng ResponIDs and itemResponseBehaviourID from lookups..
                        assessmentResponseInputDTO.PersonSupportID = careGiverID;
                        var itemResponses = dict_QItemResponses.Where(x => x.Key.QuestionnaireItemID == questionItemID).FirstOrDefault();
                        var responseId = FilterResponseID(itemResponses, response, out itemResponseBehaviourID, ref resultMessage);
                        if (responseId != null)
                        {
                            assessmentResponseInputDTO.ItemListOrder = itemResponses.Key.ListOrder;
                            assessmentResponseInputDTO.ResponseValue = responseId.Value;
                            assessmentResponseInputDTO.ResponseID = responseId.ResponseId.ToString().ToInt();
                            assessmentResponseInputDTO.ItemResponseBehaviorID = itemResponseBehaviourID;
                            list_assessmentResponseInputDTO.Add(assessmentResponseInputDTO);
                        }
                        else if (!resultMessage.IsNullOrEmpty())
                        {
                            resultMessage = string.Format(resultMessage, columns.Key);
                            resultMessage = DraftResultMessage(null, log, rowNo, resultMessage, PCISEnum.ImportTypes.Assessment);
                            isFailed = true;
                            break;
                        }
                        else if (responseId == null && assessmentStatus.TrimToLower() != PCISEnum.AssessmentStatus.InProgress.TrimToLower() && resultMessage.IsNullOrEmpty())
                        {
                            resultMessage = $"Please give valid label for '{columns.Key}' since assessment is having {assessmentStatus}' status.";
                            resultMessage = DraftResultMessage(null, log, rowNo, resultMessage, PCISEnum.ImportTypes.Assessment);
                            isFailed = true;
                            break;
                        }
                    }
                }
                if (!isFailed)
                {
                    if (assessmentStatus.TrimToLower() != PCISEnum.AssessmentStatus.InProgress.TrimToLower() && list_assessmentResponseInputDTO.Count == 0)
                    {
                        resultMessage = $"Please give valid input labels, since assessment is having '{assessmentStatus}' status.";
                        resultMessage = DraftResultMessage(null, log, rowNo, resultMessage, PCISEnum.ImportTypes.Assessment);
                        list_assessmentResponseInputDTO.Clear();
                        return null;
                    }
                    return list_assessmentResponseInputDTO;
                }
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }


        private static Response FilterResponseID(KeyValuePair<QuestionnaireItemsForImportDTO, List<Response>> itemResponses, string ResponseValueFromTemplate, out int itemResponseBehaviourID, ref string resultMessage)
        {
            try
            {
                resultMessage = string.Empty;
                itemResponseBehaviourID = 0;
                if (!ResponseValueFromTemplate.IsNullOrEmpty())
                {
                    var QuestionItemDetails = itemResponses.Key;
                    var responseObj = itemResponses.Value.Where(x => x.Label.ToLower() == ResponseValueFromTemplate.ToLower()).FirstOrDefault();
                    if (responseObj != null)
                    {
                        itemResponseBehaviourID = 0;
                        if (responseObj?.ListOrder <= QuestionItemDetails.MinThreshold)
                        {
                            itemResponseBehaviourID = QuestionItemDetails.MinItemResponseBehaviorID;
                        }
                        else if (responseObj?.ListOrder >= QuestionItemDetails.MaxThreshold)
                        {
                            itemResponseBehaviourID = QuestionItemDetails.MaxItemResponseBehaviorID;
                        }
                        else if (responseObj?.ListOrder == 0)
                        {
                            itemResponseBehaviourID = 0;
                        }
                        else
                        {
                            itemResponseBehaviourID = QuestionItemDetails.DefaultItemResponseBehaviorID;
                        }

                        return responseObj;
                    }
                    resultMessage = "Please give valid label for '{0}'";
                }
                return null;

            }
            catch (Exception)
            {

                throw;
            }
        }

        private static Dictionary<string, string> PrepareDynamicColumnHeadersForAssessmentUpload(Dictionary<string, string> columnUploaded, string ColumnsHeadersInDefaultTemplate, ILogger log, ref string resultMessage)
        {
            try
            {
                Regex regxformat = new Regex(PCISEnum.Constants.CaregiverRegx1, RegexOptions.IgnoreCase);
                Regex regx1 = new Regex(PCISEnum.Constants.Caregiver, RegexOptions.IgnoreCase);
                var fixedColumnHeaders = typeof(PCISEnum.AsessmentTemplateFixedFields).GetFields().ToList().Select(x => x.Name).ToList();
                Dictionary<string, string> dict_QItemsDynamicHeadersToUpload = new Dictionary<string, string>();
                List<Dictionary<string, string>> lstdict_QItemsDefaultHeadersInTemplate = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(ColumnsHeadersInDefaultTemplate);
                var dict_QItemDynamicHeadersInDefaultTemplate = lstdict_QItemsDefaultHeadersInTemplate[0].Where(x => !fixedColumnHeaders.Contains(x.Key)).ToDictionary(x => x.Key, x => x.Value);
                dict_QItemDynamicHeadersInDefaultTemplate = dict_QItemDynamicHeadersInDefaultTemplate.Where(x => regx1.Matches(x.Key).Count == 0).ToDictionary(x => x.Key, x => x.Value);
                var dict_DynamicHeadersToUpload = columnUploaded.Where(x => !fixedColumnHeaders.Contains(x.Key) && !x.Key.IsNullOrEmpty()).ToDictionary(x => x.Key, x => x.Value);
                dict_QItemsDynamicHeadersToUpload = dict_DynamicHeadersToUpload.Where(x => regx1.Matches(x.Key).Count == 0 && !x.Key.IsNullOrEmpty()).ToDictionary(x => x.Key, x => x.Value);
                if (dict_QItemDynamicHeadersInDefaultTemplate.Count != dict_QItemsDynamicHeadersToUpload.Count)
                {
                    resultMessage = $"Column headers of the uploaded file do not match with template headers.";
                    resultMessage = DraftResultMessage(null, log, 0, resultMessage, PCISEnum.ImportTypes.Assessment);
                    return null;
                }
                //caregiver columns building and matchng
                var caregiverCount = columnUploaded.Keys.ToList().Where(x => regxformat.Matches(x).Count > 0).ToList();
                regxformat = new Regex(PCISEnum.Constants.CaregiverAppendedOnItem, RegexOptions.IgnoreCase);

                var dict_caregiverHeadersInDefaultTemplate = lstdict_QItemsDefaultHeadersInTemplate[0].Where(x => regxformat.Matches(x.Key).Count > 0).ToDictionary(x => x.Key, x => x.Value);
                int i = 1;
                if (dict_caregiverHeadersInDefaultTemplate.Count > 0)
                {
                    dict_QItemDynamicHeadersInDefaultTemplate.Add(string.Format("{0}{1}", PCISEnum.Constants.Caregiver, i), "");
                    foreach (var caregiver in dict_caregiverHeadersInDefaultTemplate)
                    {
                        dict_QItemDynamicHeadersInDefaultTemplate.Add(caregiver.Key, caregiver.Value);
                    }
                    i++;
                    while (caregiverCount.Count > 1 && i <= caregiverCount.Count)
                    {
                        dict_QItemDynamicHeadersInDefaultTemplate.Add(string.Format("{0}{1}", PCISEnum.Constants.Caregiver, i), "");
                        foreach (var caregiverItems in dict_caregiverHeadersInDefaultTemplate)
                        {
                            var CaregiverNo = string.Format(PCISEnum.Constants.CaregiverFormatWithNumber, i);
                            dict_QItemDynamicHeadersInDefaultTemplate.Add(Regex.Replace(caregiverItems.Key, PCISEnum.Constants.CaregiverAppendedOnItem, CaregiverNo), caregiverItems.Value);
                        }
                        i++;
                    }
                }
                var isAllcolumnsExistsToUpload = dict_DynamicHeadersToUpload.Where(x => !dict_QItemDynamicHeadersInDefaultTemplate.ContainsKey(x.Key)).ToList();
                if (isAllcolumnsExistsToUpload.Count() == 0)
                {
                    return dict_QItemDynamicHeadersInDefaultTemplate;
                }
                else
                {
                    resultMessage = $"PersonSupport section headers in the uploaded file do not match with template headers.";
                    resultMessage = DraftResultMessage(null, log, 0, resultMessage, PCISEnum.ImportTypes.Assessment);
                }
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private static AssessmentTemplateResponse GetAssessmentTemplate(Utility utility, string apiBaseUrl, string agencyId, ILogger log, string questionnaireID)
        {
            try
            {
                var lookupAPIurl = apiBaseUrl + PCISEnum.APIurl.AssessmentTemplate.Replace(PCISEnum.APIReplacableValues.QuestionnaireID, questionnaireID);
                var result = utility.RestApiCall(log, lookupAPIurl, false);
                if (!string.IsNullOrEmpty(result))
                {
                    var assessmentTemplate = JsonConvert.DeserializeObject<AssessmentTemplateResponseDTO>(result);
                    return assessmentTemplate.result;
                }
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private static string ValidateLookup(string lookupName, string fieldValue, LookupDTO lookupsDTO)
        {
            try
            {
                string fieldID = string.Empty;
                if (lookupsDTO?.Lookups?.Count > 0)
                {
                    var fieldWiseLookup = lookupsDTO.Lookups[lookupName];
                    if (!string.IsNullOrEmpty(fieldValue) && !string.IsNullOrEmpty(fieldValue))
                    {
                        var lookupID = fieldWiseLookup.Where(x => x.Value.TrimToLower() == fieldValue.TrimToLower()).FirstOrDefault();
                        if (lookupID != null)
                            return lookupID.Id;
                    }
                }
                return fieldID;
            }
            catch (Exception)
            {

                throw;
            }
        }
        private static LookupDTO GetPCISAgencyLookups(Utility utility, string apiBaseUrl, string agencyId, ILogger log, string questionnaireID)
        {
            try
            {
                var lookupAPIurl = apiBaseUrl + PCISEnum.APIurl.LookupsForAgency.Replace(PCISEnum.APIReplacableValues.AgencyID, agencyId).Replace(PCISEnum.APIReplacableValues.QuestionnaireID, questionnaireID);
                var result = utility.RestApiCall(log, lookupAPIurl, false);
                if (!string.IsNullOrEmpty(result))
                {
                    var lookups = JsonConvert.DeserializeObject<LookupResponseDTO>(result);
                    return lookups.result;
                }
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region CommmonMethods
        public static string DraftResultMessage(JToken row, ILogger log, int rowNo, string message, string importType, bool isSuccess = false, int count = 0)
        {
            var resultMessage = string.Empty;
            if (!message.IsNullOrEmpty() && rowNo != 0)
            {
                rowNo++;
                resultMessage = $"{importType}-Import : {DateTime.UtcNow} : Import to PCIS failed at the row {rowNo}. Reason : {message}";
                log.LogInformation(resultMessage);
            }
            else if (!message.IsNullOrEmpty() && rowNo == 0)
            {
                resultMessage = $"{importType}-Import : {DateTime.UtcNow} : Import to PCIS failed. Reason : {message}";
                log.LogInformation(resultMessage);
            }
            else if (!isSuccess && message.IsNullOrEmpty())
            {
                resultMessage = $"{importType}-Import : {DateTime.UtcNow} : Failed to Import {count} Records to PCIS.";
                log.LogInformation(resultMessage);
            }
            else if (!isSuccess && !message.IsNullOrEmpty())
            {
                resultMessage = $"{importType}-Import : {DateTime.UtcNow} : Some error in the uploaded file type. Exception : {message}";
                log.LogInformation(resultMessage);
            }
            else if (isSuccess && message.IsNullOrEmpty())
            {
                resultMessage = $"{importType}-Import : {DateTime.UtcNow} : Successfully Imported {count} Record(s) to PCIS.";
                log.LogInformation(resultMessage);
            }
            return resultMessage;
        }

        private static CRUDResponse DraftEmail(Utility utility, string apiBaseUrl, ImportEmailInputDTO importDTO, ILogger log)
        {
            try
            {
                CRUDResponse responseResult = new CRUDResponse();
                var configAPiurl = apiBaseUrl + PCISEnum.APIurl.sendEmailImport;
                var result = utility.RestApiCall(log, configAPiurl, false, false, PCISEnum.APIMethodType.PostRequest, null, importDTO.ToJSON());
                if (!result.IsNullOrEmpty())
                {
                    var response = JsonConvert.DeserializeObject<CRUDResponseDTO>(result);
                    return response.result;
                }
                return responseResult;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static string UpdateProcessDateToImportEntry(Utility utility, string apiBaseUrl, string result1, string fileImportID, ILogger log)
        {
            try
            {
                var configAPiurl = apiBaseUrl + PCISEnum.APIurl.importIsprocessedUpdate + "?fileImportID=" + fileImportID;
                log.LogInformation($"ImportProcess : {DateTime.Now} : FileImportID-{fileImportID} : Updated as processed in Database..");
                return utility.RestApiCall(log, configAPiurl, false, false, PCISEnum.APIMethodType.PutRequest);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private static CRUDResponse GetImportDetailsForProcess(Utility Utility, string apiBaseUrl, ILogger log, string importType = "", string APItype = PCISEnum.APIMethodType.GetRequest)
        {
            try
            {
                CRUDResponse responseResult = new CRUDResponse() { importFileList = new List<dynamic>() };
                var apiUrl = apiBaseUrl + PCISEnum.APIurl.ImportFileList.Replace(PCISEnum.APIReplacableValues.ImportType, importType);
                var result = Utility.RestApiCall(log, apiUrl, false, false, APItype);
                if (!string.IsNullOrEmpty(result))
                {
                    var response = JsonConvert.DeserializeObject<CRUDResponseDTO>(result);
                    return response.result;
                }
                return responseResult;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region ImportHelper
        private static string ProcessHelperImport(Utility Utility, dynamic fileList, string apiurl, ILogger log)
        {
            int rowNo = 0;
            try
            {
                string resultMessage = string.Empty;
                string fileJsonData = fileList["fileJsonData"];
                var helperData = JArray.Parse(fileJsonData);
                bool isFailed = false;
                long agencyID = fileList["agencyID"];
                int updateUserID = fileList["updateUserID"];
                string fileImportID = fileList["fileImportID"];

                log.LogInformation($"Helper-Import : {DateTime.Now} : FileImportID-{fileImportID} : Total Rows to upload = {helperData.Count}");

                var configAPiurl = apiurl + PCISEnum.APIurl.rolesByname;
                var RoleResult = Utility.RestApiCall(log, configAPiurl, false);
                var RoleResponse = JsonConvert.DeserializeObject<CRUDResponseDTO>(RoleResult);

                List<JObject> HelperEmailList = new List<JObject>();
                List<JObject> ReviewerEmailList = new List<JObject>();

                foreach (var item in helperData)
                {
                    rowNo++;
                    if (string.IsNullOrEmpty(item[PCISEnum.HelperFields.FirstName].ToString()) || string.IsNullOrEmpty(item[PCISEnum.HelperFields.LastName].ToString()) ||
                       string.IsNullOrEmpty(item[PCISEnum.HelperFields.Role].ToString()) || string.IsNullOrEmpty(item[PCISEnum.HelperFields.Email].ToString())
                       || string.IsNullOrEmpty(item[PCISEnum.HelperFields.ReviewerEmail].ToString()) || string.IsNullOrEmpty(item[PCISEnum.HelperFields.StartDate].ToString()))
                    {
                        resultMessage = $"Columns like {PCISEnum.HelperFields.FirstName}/{PCISEnum.HelperFields.LastName}/{PCISEnum.HelperFields.Email}/{PCISEnum.HelperFields.ReviewerEmail} /{PCISEnum.HelperFields.StartDate} should not be empty. ";
                        resultMessage = DraftResultMessage(item, log, rowNo, resultMessage, PCISEnum.ImportTypes.Helper);
                        isFailed = true;
                        break;
                    }
                    else
                    {
                        JObject EmailJObj = new JObject();
                        JObject ReviewerEmailJObj = new JObject();
                        EmailJObj[PCISEnum.HelperFields.Email] = item[PCISEnum.HelperFields.Email].ToString();
                        ReviewerEmailJObj[PCISEnum.HelperFields.ReviewerEmail] = item[PCISEnum.HelperFields.ReviewerEmail].ToString();
                        if (HelperEmailList.Count > 0)
                            if (HelperEmailList.Contains(EmailJObj))
                            {
                                resultMessage = $"Email {item[PCISEnum.HelperFields.Email].ToString()} already existing in the same file.";
                                resultMessage = DraftResultMessage(item, log, rowNo, resultMessage, PCISEnum.ImportTypes.Helper);
                                isFailed = true;
                                break;
                            }
                        HelperEmailList.Add(EmailJObj);
                        ReviewerEmailList.Add(ReviewerEmailJObj);
                    }
                }
                if (!isFailed)
                {
                    configAPiurl = apiurl + PCISEnum.APIurl.emailValidation;
                    ImportParameterDTO Parameters = new ImportParameterDTO();
                    Parameters.JsonData = JsonConvert.SerializeObject(HelperEmailList);
                    Parameters.agencyID = agencyID;
                    var emailValidationResult = Utility.RestApiCall(log, configAPiurl, false, false, PCISEnum.APIMethodType.PostRequest, null, JsonConvert.SerializeObject(Parameters));
                    log.LogInformation($"Helper-Import  : {DateTime.Now} : Initiating Get Helper List by mail..");
                    var existingEmail = JsonConvert.DeserializeObject<CRUDResponseDTO>(emailValidationResult);
                    if (!existingEmail.result.isVaildEmails)
                    {
                        resultMessage = $"Email already exist in PCIS.";
                        resultMessage = DraftResultMessage(null, log, rowNo, resultMessage, PCISEnum.ImportTypes.Helper);
                        isFailed = true;
                        return resultMessage;
                    }

                    configAPiurl = apiurl + PCISEnum.APIurl.HelperList;
                    Parameters.JsonData = JsonConvert.SerializeObject(ReviewerEmailList);
                    Parameters.agencyID = agencyID;
                    var helperResult = Utility.RestApiCall(log, configAPiurl, false, false, PCISEnum.APIMethodType.PostRequest, null, JsonConvert.SerializeObject(Parameters));
                    log.LogInformation($"Helper-Import  : {DateTime.Now} : Initiating Get Reviewer List by mail..");
                    var helperResponse = JsonConvert.DeserializeObject<CRUDResponseDTO>(helperResult);
                    List<HelperDetailsDTO> ParsedData = parseHelperData(helperData, RoleResponse.result.roleLookup, helperResponse.result.helperList, agencyID, ref resultMessage, log, updateUserID);
                    if (ParsedData != null && ParsedData.Count > 0)
                    {
                        configAPiurl = apiurl + PCISEnum.APIurl.importHelper;
                        var ImpoertResponse = Utility.RestApiCall(log, configAPiurl, false, false, PCISEnum.APIMethodType.PostRequest, null, JsonConvert.SerializeObject(ParsedData), agencyID);
                        log.LogInformation($"Helper-Import  : {DateTime.Now} : Initiating Get Identifier..");
                        var importResponse = JsonConvert.DeserializeObject<CRUDResponseDTO>(ImpoertResponse);
                        log.LogInformation($"Helper-Import  : {DateTime.Now} : Import file Success with fileID...." + fileImportID);
                        resultMessage = DraftResultMessage(null, log, rowNo, resultMessage, PCISEnum.ImportTypes.Helper, true, helperData.Count);
                    }
                }
                return resultMessage;
            }
            catch (Exception ex)
            {

                log.LogError($"Person-Import : {DateTime.Now} : ImportTypeID-{0} : Exception occurred at {MethodInfo.GetCurrentMethod().Name} : {ex.Message}");
                return DraftResultMessage(null, log, rowNo, ex.Message, PCISEnum.ImportTypes.Helper, false);
            }
        }

        public static List<HelperDetailsDTO> parseHelperData(JArray DataList, List<RoleLookupDTO> Rols, List<HelperInfoDTO> ReviewerList, long agencyID, ref string resultMessage, ILogger log, int updateUserID)
        {
            try
            {
                List<HelperDetailsDTO> Helpers = new List<HelperDetailsDTO>();
                int rowNo = 0;
                bool isFailed = false;
                foreach (var item in DataList)
                {
                    rowNo++;
                    List<RoleLookupDTO> roleList = new List<RoleLookupDTO>();
                    HelperDetailsDTO Helper = new HelperDetailsDTO();
                    List<HelperInfoDTO> Reviewers = new List<HelperInfoDTO>();
                    if (!string.IsNullOrEmpty(item[PCISEnum.HelperFields.Role].ToString()))
                    {
                        roleList = Rols.Where(X => X.Name.Equals(item[PCISEnum.HelperFields.Role].ToString())).ToList();
                        if (roleList.Count > 0)
                        {
                            Helper.RoleId = roleList[0].SystemRoleID;
                        }
                        else
                        {
                            var message = $"Column {PCISEnum.HelperFields.Role} should be valid.";
                            isFailed = true;
                            resultMessage = DraftResultMessage(item, log, rowNo, message, PCISEnum.ImportTypes.Helper);
                            return null;
                        }
                    }

                    if (!string.IsNullOrEmpty(item[PCISEnum.HelperFields.ReviewerEmail].ToString()))
                    {
                        Reviewers = ReviewerList.Where(X => X.Email.Equals(item[PCISEnum.HelperFields.ReviewerEmail].ToString())).ToList();
                        if (Reviewers.Count > 0)
                        {
                            Helper.ReviewerID = Reviewers[0].HelperID;
                        }
                        else
                        {
                            var message = $"Column {PCISEnum.HelperFields.ReviewerEmail} should be valid. ";
                            isFailed = true;
                            resultMessage = DraftResultMessage(item, log, rowNo, message, PCISEnum.ImportTypes.Helper);
                            return null;
                        }
                    }
                    if (string.IsNullOrEmpty(item[PCISEnum.HelperFields.FirstName].ToString()) ||
                         !ValidateString(item[PCISEnum.HelperFields.FirstName].ToString()))
                    {
                        var message = $"Column {PCISEnum.HelperFields.FirstName} should not be empty. ";
                        isFailed = true;
                        resultMessage = DraftResultMessage(item, log, rowNo, message, PCISEnum.ImportTypes.Helper);
                        return null;
                    }
                    Helper.FirstName = item[PCISEnum.HelperFields.FirstName].ToString();
                    if (string.IsNullOrEmpty(item[PCISEnum.HelperFields.LastName].ToString()) ||
                        !ValidateString(item[PCISEnum.HelperFields.LastName].ToString()))
                    {
                        var message = $"Column {PCISEnum.HelperFields.LastName} should not be empty. ";
                        isFailed = true;
                        resultMessage = DraftResultMessage(item, log, rowNo, message, PCISEnum.ImportTypes.Helper);
                        return null;
                    }
                    Helper.LastName = item[PCISEnum.HelperFields.LastName].ToString();
                    if (string.IsNullOrEmpty(item[PCISEnum.HelperFields.LastName].ToString()))
                    {
                        var message = $"Column {PCISEnum.HelperFields.Email} should not be empty. ";
                        isFailed = true;
                        resultMessage = DraftResultMessage(item, log, rowNo, message, PCISEnum.ImportTypes.Helper);
                        return null;
                    }
                    Helper.Email = item[PCISEnum.HelperFields.Email].ToString();
                    if (item[PCISEnum.HelperFields.Email].ToString() == item[PCISEnum.HelperFields.ReviewerEmail].ToString())
                        Helper.ReviewerID = 0;
                    Helper.AgencyID = agencyID;
                    if (string.IsNullOrEmpty(item[PCISEnum.HelperFields.StartDate].ToString()))
                    {
                        var message = $"Column {PCISEnum.HelperFields.StartDate} should not be empty. ";
                        isFailed = true;
                        resultMessage = DraftResultMessage(item, log, rowNo, message, PCISEnum.ImportTypes.Helper);
                        return null;
                    }
                    DateTime startDate = new DateTime();
                    if (!item[PCISEnum.HelperFields.StartDate].ToString().TryParseValidDate(ref startDate))
                    {
                        resultMessage = DraftResultMessage(DataList, log, rowNo, $"Invalid date format for the column '{PCISEnum.HelperFields.StartDate}'.It should be in 'MM/DD/YYYY' format.", PCISEnum.ImportTypes.Helper);
                        isFailed = true;
                        return null;
                    }
                    string IsSignUpMail = ValidateSignUpMailColumnValue(item[PCISEnum.HelperFields.SendSignUpMail].ToString());
                    if (string.IsNullOrWhiteSpace(IsSignUpMail) || IsSignUpMail == "2")
                    {
                        var message1 = $"Column {PCISEnum.HelperFields.SendSignUpMailText} should be either True/False or 1/0.";
                        var message2 = $"Column {PCISEnum.HelperFields.SendSignUpMailText} should not be empty. Should be either True/False or 1/0.";
                        var message = string.IsNullOrWhiteSpace(IsSignUpMail) ? message2 : message1;
                        isFailed = true;
                        resultMessage = DraftResultMessage(item, log, rowNo, message, PCISEnum.ImportTypes.Helper);
                        return null;
                    }
                    Helper.StartDate = startDate;
                    Helper.UpdateUserID = updateUserID;
                    Helper.IsEmailReminderAlerts = true;
                    Helper.SendSignUpMail = IsSignUpMail == "1" ? true : false;
                    Helpers.Add(Helper);

                }
                if (!isFailed)
                    return Helpers;
                return null;
            }
            catch (Exception)
            {
                throw;
            }

        }

        //signUpMail should be either True/1 or False/0 always.
        private static string ValidateSignUpMailColumnValue(string signUpMail)
        {
            try
            {
                var signupValue = signUpMail.ToUpper();
                if (string.IsNullOrWhiteSpace(signupValue))
                {
                    return string.Empty;
                }
                if (signupValue == "0" || signupValue == "FALSE")
                {
                    return "0";
                }
                if (signupValue == "1" || signupValue == "TRUE")
                {
                    return "1";
                }
                return "2";
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        public static bool ValidateString(string text)
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                var regexItem = new Regex("^[a-zA-Z0-9 ]*$");

                if (regexItem.IsMatch(text))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool ValidateNumber(string text)
        {
            if (text.Length > 10)
            {
                return false;
            }
            var regexItem = new Regex("^[0-9 ]*$");
            if (regexItem.IsMatch(text))
            {
                return true;
            }
            return false;
        }
    }
}
