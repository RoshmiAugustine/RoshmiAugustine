using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using EHRProcess.Common;
using EHRProcess.DTO;
using EHRProcess.Enums;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EHRProcess
{
    public static class ProcessData
    {
        [FunctionName("ProcessData")]
        public static void Run([TimerTrigger("%timer-frequency%")] TimerInfo myTimer, ILogger log)
        {
            try
            {
                Utility Utility = new Utility();


                log.LogInformation($"EHR : {DateTime.Now} : Azure function started.");
                var apiurl = Environment.GetEnvironmentVariable(PCISEnum.EnvironmentVariables.ApiUrlFromKeyVault, EnvironmentVariableTarget.Process);

                log.LogInformation($"EHR : {DateTime.Now} : Fetching AgencyIDs..");

                string[] agencyIdsToProcess = GetAgncyIdsForProcess(Utility, apiurl, log);

                log.LogInformation($"EHR : {DateTime.Now} : AgencyIDs fetched to process : {string.Join(',', agencyIdsToProcess)}");

                foreach (var agencyId in agencyIdsToProcess)
                {
                    AgencyConfigurationResponseDTO agencySpecificConfiguration = GetAgencySpecificConfiguration(Utility, apiurl, agencyId, log);

                    log.LogInformation($"EHR : {DateTime.Now} : AgencyID-{agencyId} : Login API fetching started.");
                    var cookie = EHRLoginAPI(Utility, agencySpecificConfiguration, log);

                    if (!string.IsNullOrWhiteSpace(cookie))
                    {
                        try
                        {
                            /*------------------------------Lookups and EHR Count From PCIS-------------------------------*/
                            log.LogInformation($"EHR : {DateTime.Now} : AgencyID-{agencyId} : PCIS Lookup API fetching started.");
                            var lookupsAll = GetPCISAgencyLookups(Utility, apiurl, agencyId, log);

                            int peopleCountInPCIS = 0;
                            var lookups = FilterFromLookups(lookupsAll, out peopleCountInPCIS);
                            log.LogInformation($"EHR : {DateTime.Now} : AgencyID-{agencyId} : Existing EHR people count in PCIS : {peopleCountInPCIS}");

                            List<EHRRootData> validatedRelationshipAPIResult = new List<EHRRootData>();
                            List<EHRRootData> validateProviderAPIResult = new List<EHRRootData>();
                            List<EHRRootData> validateHouseMemberAPIResult = new List<EHRRootData>();

                            #region FetchAndUploadOpenRecords();
                            #region Client_API_OpenCases

                            #region FosterCare_OpenCases
                            /*------------------------------Client API(Foster Care Open Cases)-------------------------------*/
                            log.LogInformation($"EHR : {DateTime.Now} : AgencyID-{agencyId} : Client API(Foster Care Cases)-Fetching started.");
                            var clientResult = EHRClientAPI(Utility, agencySpecificConfiguration, cookie, log, PCISEnum.ConfigurationKey.EHRClientURL, PCISEnum.Constants.OpenStatusInURL);

                            log.LogInformation($"EHR : {DateTime.Now} : AgencyID-{agencyId} : Client API(Foster Care Cases)-Deserialize started.");
                            var deserializedClientAPIResult = DeserializeEHRAPIResult(clientResult, PCISEnum.ConfigurationKey.EHRClientURL, peopleCountInPCIS);

                            log.LogInformation($"EHR : {DateTime.Now} : AgencyID-{agencyId} : Client API(Foster Care Cases)-Total records from EHR : { deserializedClientAPIResult.Count }");

                            if (peopleCountInPCIS > 0)
                            {
                                deserializedClientAPIResult = deserializedClientAPIResult.Where(x => x.IsValid == true).ToList();
                                log.LogInformation($"EHR : {DateTime.Now} : AgencyID-{agencyId} : Client API-Total records to PCIS based on LastModified : { deserializedClientAPIResult.Count }");
                            }
                            #endregion

                            #region Adoption_OpenCases
                            /*------------------------------Client API(Adoption Cases)-------------------------------*/
                            log.LogInformation($"EHR : {DateTime.Now} : AgencyID-{agencyId} : Client API(Adoption Cases)-Fetching started.");
                            var clientAdoptResult = EHRClientAPI(Utility, agencySpecificConfiguration, cookie, log, PCISEnum.ConfigurationKey.EHRAdoptionURL, PCISEnum.Constants.OpenStatusInURL);

                            log.LogInformation($"EHR : {DateTime.Now} : AgencyID-{agencyId} : Client API(Adoption Cases)-Deserialize started.");
                            var deserializedClientAdoptAPIResult = DeserializeEHRAPIResult(clientAdoptResult, PCISEnum.ConfigurationKey.EHRAdoptionURL, peopleCountInPCIS);

                            log.LogInformation($"EHR : {DateTime.Now} : AgencyID-{agencyId} : Client API(Adoption Cases)-Total records from EHR : { deserializedClientAdoptAPIResult.Count }");

                            if (peopleCountInPCIS > 0)
                            {
                                deserializedClientAdoptAPIResult = deserializedClientAdoptAPIResult.Where(x => x.IsValid == true).ToList();
                                log.LogInformation($"EHR : {DateTime.Now} : AgencyID-{agencyId} : Client API(Adoption Cases)-Total records to PCIS based on LastModified : { deserializedClientAdoptAPIResult.Count }");
                            }
                            #endregion

                            log.LogInformation($"EHR : {DateTime.Now} : AgencyID-{agencyId} : Client API-Total records to PCIS - Foster Care Open Cases-{deserializedClientAPIResult.Count} + Adoption Cases-{deserializedClientAdoptAPIResult.Count} = {deserializedClientAPIResult.Count + deserializedClientAdoptAPIResult.Count}.");

                            deserializedClientAPIResult.AddRange(deserializedClientAdoptAPIResult);
                            List<PeopleDetailsDTO> peopleDetails = new List<PeopleDetailsDTO>();
                            if (deserializedClientAPIResult.Count > 0)
                            {
                                log.LogInformation($"EHR : {DateTime.Now} : AgencyID-{agencyId} : Client API-Replace lookups started.");
                                var lookupReplacedClientAPIResult = ReplaceLookupIDInEHRAPIResult(deserializedClientAPIResult, lookups);

                                log.LogInformation($"EHR : {DateTime.Now} : AgencyID-{agencyId} : Client API-Process and validate started.");
                                var validateClientAPIResult = ValidateEHRAPIResult(lookupReplacedClientAPIResult, lookups, PCISEnum.ConfigurationKey.EHRClientURL, agencyId, log);

                                log.LogInformation($"EHR : {DateTime.Now} : AgencyID-{agencyId} : Client API-Format to PCIS started.");
                                peopleDetails = FormatClientAPIResultsToPCIS(validateClientAPIResult, agencySpecificConfiguration, agencyId);

                                /*------------------------------Relationship API-------------------------------*/
                                log.LogInformation($"EHR : {DateTime.Now} : AgencyID-{agencyId} : Relationship API-Fetching started.");
                                var relationshipResult = EHRRelationshipAPI(Utility, agencySpecificConfiguration, cookie, log);

                                log.LogInformation($"EHR : {DateTime.Now} : AgencyID-{agencyId} : Relationship API-Deserialize started.");
                                var desrializedRelationAPIResult = DeserializeEHRAPIResult(relationshipResult, PCISEnum.ConfigurationKey.EHRRelationshipURL);

                                log.LogInformation($"EHR : {DateTime.Now} : AgencyID-{agencyId} : Relationship API-Total records : { desrializedRelationAPIResult.Count }");

                                log.LogInformation($"EHR : {DateTime.Now} : AgencyID-{agencyId} : Relationship API-Replace lookups started.");
                                var lookupReplacedRelationshipAPIResult = ReplaceLookupIDInEHRAPIResult(desrializedRelationAPIResult, lookups);

                                log.LogInformation($"EHR : {DateTime.Now} : AgencyID-{agencyId} : Relationship API-Process and validate started.");
                                validatedRelationshipAPIResult = ValidateEHRAPIResult(lookupReplacedRelationshipAPIResult, lookups, PCISEnum.ConfigurationKey.EHRRelationshipURL, agencyId, log);

                                log.LogInformation($"EHR : {DateTime.Now} : AgencyID-{agencyId} : Relationship API-Combine to Client API Result and Format to PCIS started.");
                                peopleDetails = FormatRelationshipAPIResultsToPCIS(validatedRelationshipAPIResult, peopleDetails);

                                /*------------------------------Provider API-------------------------------*/
                                log.LogInformation($"EHR : {DateTime.Now} : AgencyID-{agencyId} : Provider API-Fetching started.");
                                var providerResult = EHRProviderAPI(Utility, agencySpecificConfiguration, cookie, log);//Include both Provider and home details

                                log.LogInformation($"EHR : {DateTime.Now} : AgencyID-{agencyId} : Provider API-Deserialize started.");
                                var deserializedproviderAPIResult = DeserializeEHRAPIResult(providerResult, PCISEnum.ConfigurationKey.EHRProviderURL);

                                log.LogInformation($"EHR : {DateTime.Now} : AgencyID-{agencyId} : Provider API-Total records : { deserializedproviderAPIResult.Count}");

                                log.LogInformation($"EHR : {DateTime.Now} : AgencyID-{agencyId} : Provider API-Replace lookups started.");
                                var lookupReplacedProviderAPIResult = ReplaceLookupIDInEHRAPIResult(deserializedproviderAPIResult, lookups);

                                log.LogInformation($"EHR : {DateTime.Now} : AgencyID-{agencyId} : Provider API-Process and validate started.");
                                validateProviderAPIResult = ValidateEHRAPIResult(lookupReplacedProviderAPIResult, lookups, PCISEnum.ConfigurationKey.EHRProviderURL, agencyId, log);

                                log.LogInformation($"EHR : {DateTime.Now} : AgencyID-{agencyId} : Provider API-Combine to Client API Result and Format to PCIS started.");
                                peopleDetails = FormatProviderAPIResultsToPCIS(validateProviderAPIResult, peopleDetails);
                            }
                            #endregion

                            #region Home_API_OpenCases
                            /*------------------------------Person Home API-------------------------------*/
                            log.LogInformation($"EHR : {DateTime.Now} : AgencyID-{agencyId} : Home API-Fetching started.");
                            var homeResult = EHRHomeAPI(Utility, agencySpecificConfiguration, cookie, log, PCISEnum.Constants.OpenStatusInURL);

                            log.LogInformation($"EHR : {DateTime.Now} : AgencyID-{agencyId} : Home API-Deserialize started.");
                            var deserializedHomeAPIResult = DeserializeEHRAPIResult(homeResult, PCISEnum.ConfigurationKey.EHRHomeURL, peopleCountInPCIS);

                            log.LogInformation($"EHR : {DateTime.Now} : AgencyID-{agencyId} : Home API-Total records from EHR : { deserializedHomeAPIResult.Count }");

                            if (peopleCountInPCIS > 0)
                            {
                                deserializedHomeAPIResult = deserializedHomeAPIResult.Where(x => x.IsValid == true).ToList();
                                log.LogInformation($"EHR : {DateTime.Now} : AgencyID-{agencyId} : Home API-Total records to PCIS based on LastModified : { deserializedHomeAPIResult.Count }");
                            }
                            List<PeopleDetailsDTO> homePeopleDetails = new List<PeopleDetailsDTO>();
                            if (deserializedHomeAPIResult.Count > 0)
                            {
                                log.LogInformation($"EHR : {DateTime.Now} : AgencyID-{agencyId} : Home API-Replace lookups started.");
                                var lookupReplacedHomeAPIResult = ReplaceLookupIDInEHRAPIResult(deserializedHomeAPIResult, lookups);

                                log.LogInformation($"EHR : {DateTime.Now} : AgencyID-{agencyId} : Home API-Process and validate started.");
                                var validateHomeAPIResult = ValidateEHRAPIResult(lookupReplacedHomeAPIResult, lookups, PCISEnum.ConfigurationKey.EHRHomeURL, agencyId, log);

                                log.LogInformation($"EHR : {DateTime.Now} : AgencyID-{agencyId} : Home API-Format to PCIS started.");
                                homePeopleDetails = FormatHomeAPIResultsToPCIS(validateHomeAPIResult, agencySpecificConfiguration, agencyId);

                                /*------------------------------House Members API-------------------------------*/
                                log.LogInformation($"EHR : {DateTime.Now} : AgencyID-{agencyId} : HouseMember API-Fetching started.");
                                var houseMemberResult = EHRHouseMemberAPI(Utility, agencySpecificConfiguration, cookie, log);

                                log.LogInformation($"EHR : {DateTime.Now} : AgencyID-{agencyId} : HouseMember API-Deserialize started.");
                                var deserializedHouseMemberAPIResult = DeserializeEHRAPIResult(houseMemberResult, PCISEnum.ConfigurationKey.EHRHouseMemberURL, peopleCountInPCIS);

                                log.LogInformation($"EHR : {DateTime.Now} : AgencyID-{agencyId} : HouseMember API-Total records from EHR : { deserializedHouseMemberAPIResult.Count }");

                                log.LogInformation($"EHR : {DateTime.Now} : AgencyID-{agencyId} : HouseMember API-Replace lookups started.");
                                var lookupReplacedHomeMemberAPIResult = ReplaceLookupIDInEHRAPIResult(deserializedHouseMemberAPIResult, lookups);

                                log.LogInformation($"EHR : {DateTime.Now} : AgencyID-{agencyId} : HouseMember API-Process and validate started.");
                                validateHouseMemberAPIResult = ValidateEHRAPIResult(lookupReplacedHomeMemberAPIResult, lookups, PCISEnum.ConfigurationKey.EHRHouseMemberURL, agencyId, log);

                                log.LogInformation($"EHR : {DateTime.Now} : AgencyID-{agencyId} : HouseMember API-Combine to Home API result and Format to PCIS started.");
                                homePeopleDetails = FormatHouseMemberAPIResultsToPCIS(validateHouseMemberAPIResult, homePeopleDetails);
                            }
                            #endregion

                            #region Upload_OpenRecords
                            /*------------------------------Upload for Open Records To PCIS-------------------------------*/
                            log.LogInformation($"EHR : {DateTime.Now} : AgencyID-{agencyId} : *****PCIS Upload for Open Records start*****");
                            log.LogInformation($"EHR : {DateTime.Now} : AgencyID-{agencyId} : Total Open Records to upload-ClientAPI Records-{peopleDetails.Count} + HomeAPI Records-{homePeopleDetails.Count} = {peopleDetails.Count + homePeopleDetails.Count}.");
                            peopleDetails.AddRange(homePeopleDetails);
                            var result = UploadToPCIS(Utility, apiurl, peopleDetails, agencyId, log, false);
                            log.LogInformation($"EHR : {DateTime.Now} : AgencyID-{agencyId} : *****PCIS Upload for Open Records End*****");
                            #endregion
                            #endregion

                            #region FetchAndUploadClosedRecords();
                            //if (peopleCountInPCIS > 0)
                            //{
                            //    #region Client_API_ClosedCases

                            //    #region FosterCare_ClosedCases
                            //    /*------------------------------Client API(Foster Care Closed Cases)-------------------------------*/
                            //    log.LogInformation($"EHR : {DateTime.Now} : AgencyID-{agencyId} : Client API(Foster Care Cases)-Fetching started.");
                            //    var closed_clientResult = EHRClientAPI(Utility, agencySpecificConfiguration, cookie, log, PCISEnum.ConfigurationKey.EHRClientURL, PCISEnum.Constants.CloseStatusInURL);

                            //    log.LogInformation($"EHR : {DateTime.Now} : AgencyID-{agencyId} : Client API(Foster Care Cases)-Deserialize started.");
                            //    var closed_deserializedClientAPIResult = DeserializeEHRAPIResult(closed_clientResult, PCISEnum.ConfigurationKey.EHRClientURL, peopleCountInPCIS);

                            //    log.LogInformation($"EHR : {DateTime.Now} : AgencyID-{agencyId} : Client API(Foster Care Cases)-Total records from EHR : { closed_deserializedClientAPIResult.Count }");

                            //    if (peopleCountInPCIS > 0)
                            //    {
                            //        closed_deserializedClientAPIResult = closed_deserializedClientAPIResult.Where(x => x.IsValid == true).ToList();
                            //        log.LogInformation($"EHR : {DateTime.Now} : AgencyID-{agencyId} : Client API-Total records to PCIS based on LastModified : { closed_deserializedClientAPIResult.Count }");
                            //    }
                            //    #endregion

                            //    #region Adoption_ClosedCases
                            //    /*------------------------------Client API(Adoption Closed Cases)-------------------------------*/
                            //    log.LogInformation($"EHR : {DateTime.Now} : AgencyID-{agencyId} : Client API(Adoption Cases)-Fetching started.");
                            //    var closed_clientAdoptResult = EHRClientAPI(Utility, agencySpecificConfiguration, cookie, log, PCISEnum.ConfigurationKey.EHRAdoptionURL, PCISEnum.Constants.CloseStatusInURL);

                            //    log.LogInformation($"EHR : {DateTime.Now} : AgencyID-{agencyId} : Client API(Adoption Cases)-Deserialize started.");
                            //    var closed_deserializedClientAdoptAPIResult = DeserializeEHRAPIResult(closed_clientAdoptResult, PCISEnum.ConfigurationKey.EHRAdoptionURL, peopleCountInPCIS);

                            //    log.LogInformation($"EHR : {DateTime.Now} : AgencyID-{agencyId} : Client API(Adoption Cases)-Total records from EHR : { closed_deserializedClientAdoptAPIResult.Count }");

                            //    if (peopleCountInPCIS > 0)
                            //    {
                            //        closed_deserializedClientAdoptAPIResult = closed_deserializedClientAdoptAPIResult.Where(x => x.IsValid == true).ToList();
                            //        log.LogInformation($"EHR : {DateTime.Now} : AgencyID-{agencyId} : Client API(Adoption Cases)-Total records to PCIS based on LastModified : { closed_deserializedClientAdoptAPIResult.Count }");
                            //    }
                            //    #endregion

                            //    log.LogInformation($"EHR : {DateTime.Now} : AgencyID-{agencyId} : Client API-Total records to PCIS - Foster Care Open Cases-{closed_deserializedClientAPIResult.Count} + Adoption Cases-{closed_deserializedClientAdoptAPIResult.Count} = {closed_deserializedClientAPIResult.Count + closed_deserializedClientAdoptAPIResult.Count}.");

                            //    closed_deserializedClientAPIResult.AddRange(closed_deserializedClientAdoptAPIResult);
                            //    List<PeopleDetailsDTO> closed_peopleDetails = new List<PeopleDetailsDTO>();
                            //    if (closed_deserializedClientAPIResult.Count > 0)
                            //    {
                            //        log.LogInformation($"EHR : {DateTime.Now} : AgencyID-{agencyId} : Client API-Replace lookups started.");
                            //        var closed_lookupReplacedClientAPIResult = ReplaceLookupIDInEHRAPIResult(closed_deserializedClientAPIResult, lookups);

                            //        log.LogInformation($"EHR : {DateTime.Now} : AgencyID-{agencyId} : Client API-Process and validate started.");
                            //        var closed_validateClientAPIResult = ValidateEHRAPIResult(closed_lookupReplacedClientAPIResult, lookups, PCISEnum.ConfigurationKey.EHRClientURL, agencyId, log);

                            //        log.LogInformation($"EHR : {DateTime.Now} : AgencyID-{agencyId} : Client API-Format to PCIS started.");
                            //        closed_peopleDetails = FormatClientAPIResultsToPCIS(closed_validateClientAPIResult, agencySpecificConfiguration, agencyId);

                            //        /*------------------------------Relationship API-------------------------------*/
                            //        log.LogInformation($"EHR : {DateTime.Now} : AgencyID-{agencyId} : Relationship API-Combine to Client API Result and Format to PCIS started.");
                            //        closed_peopleDetails = FormatRelationshipAPIResultsToPCIS(validatedRelationshipAPIResult, closed_peopleDetails);

                            //        /*------------------------------Provider API-------------------------------*/
                            //        log.LogInformation($"EHR : {DateTime.Now} : AgencyID-{agencyId} : Provider API-Combine to Client API Result and Format to PCIS started.");
                            //        closed_peopleDetails = FormatProviderAPIResultsToPCIS(validateProviderAPIResult, closed_peopleDetails);
                            //    }
                            //    #endregion

                            //    #region Home_API_ClosedCases
                            //    /*------------------------------Person Home API Closed Cases-------------------------------*/
                            //    log.LogInformation($"EHR : {DateTime.Now} : AgencyID-{agencyId} : Home API-Fetching started.");
                            //    var closed_homeResult = EHRHomeAPI(Utility, agencySpecificConfiguration, cookie, log, PCISEnum.Constants.CloseStatusInURL);

                            //    log.LogInformation($"EHR : {DateTime.Now} : AgencyID-{agencyId} : Home API-Deserialize started.");
                            //    var closed_deserializedHomeAPIResult = DeserializeEHRAPIResult(closed_homeResult, PCISEnum.ConfigurationKey.EHRHomeURL, peopleCountInPCIS);

                            //    log.LogInformation($"EHR : {DateTime.Now} : AgencyID-{agencyId} : Home API-Total records from EHR : { closed_deserializedHomeAPIResult.Count }");

                            //    if (peopleCountInPCIS > 0)
                            //    {
                            //        closed_deserializedHomeAPIResult = closed_deserializedHomeAPIResult.Where(x => x.IsValid == true).ToList();
                            //        log.LogInformation($"EHR : {DateTime.Now} : AgencyID-{agencyId} : Home API-Total records to PCIS based on LastModified : { closed_deserializedHomeAPIResult.Count }");
                            //    }
                            //    List<PeopleDetailsDTO> closed_homePeopleDetails = new List<PeopleDetailsDTO>();
                            //    if (closed_deserializedHomeAPIResult.Count > 0)
                            //    {
                            //        log.LogInformation($"EHR : {DateTime.Now} : AgencyID-{agencyId} : Home API-Replace lookups started.");
                            //        var closed_lookupReplacedHomeAPIResult = ReplaceLookupIDInEHRAPIResult(closed_deserializedHomeAPIResult, lookups);

                            //        log.LogInformation($"EHR : {DateTime.Now} : AgencyID-{agencyId} : Home API-Process and validate started.");
                            //        var closed_validateHomeAPIResult = ValidateEHRAPIResult(closed_lookupReplacedHomeAPIResult, lookups, PCISEnum.ConfigurationKey.EHRHomeURL, agencyId, log);

                            //        log.LogInformation($"EHR : {DateTime.Now} : AgencyID-{agencyId} : Home API-Format to PCIS started.");
                            //        closed_homePeopleDetails = FormatHomeAPIResultsToPCIS(closed_validateHomeAPIResult, agencySpecificConfiguration, agencyId);

                            //        /*------------------------------House Members API-------------------------------*/
                            //        log.LogInformation($"EHR : {DateTime.Now} : AgencyID-{agencyId} : HouseMember API-Combine to Home API result and Format to PCIS started.");
                            //        closed_homePeopleDetails = FormatHouseMemberAPIResultsToPCIS(validateHouseMemberAPIResult, closed_homePeopleDetails);
                            //    }
                            //    #endregion

                            //    #region Upload_ClosedRecords
                            //    /*------------------------------Upload for Closed Records To PCIS-------------------------------*/
                            //    log.LogInformation($"EHR : {DateTime.Now} : AgencyID-{agencyId} : *****PCIS Upload for Closed Records start*****");
                            //    log.LogInformation($"EHR : {DateTime.Now} : AgencyID-{agencyId} : Total Closed Records to upload-ClientAPI Records-{closed_peopleDetails.Count} + HomeAPI Records-{closed_homePeopleDetails.Count} = {closed_peopleDetails.Count + closed_homePeopleDetails.Count}.");
                            //    closed_peopleDetails.AddRange(closed_homePeopleDetails);
                            //    var closed_result = UploadToPCIS(Utility, apiurl, closed_peopleDetails, agencyId, log, true);
                            //    log.LogInformation($"EHR : {DateTime.Now} : AgencyID-{agencyId} : *****PCIS Upload for Closed Records End*****");
                            //    #endregion
                            //}
                            #endregion

                        }
                        catch (Exception)
                        {
                            throw;
                        }
                        finally
                        {
                            //Update Assessment Approve Date to EHR
                            UpdateToEHR(Utility, cookie, apiurl, agencyId, agencySpecificConfiguration, log);
                        }
                    }
                }
                log.LogInformation($"EHR : {DateTime.Now} : Azure function Ended.");
            }
            catch (Exception ex)
            {
                log.LogError(ex, $@"EHR : Exception occurred.Exception Message : {ex.Message}.Location : {ex.StackTrace}.Details : {ex.InnerException}");
            }

        }
        private static object UploadToPCIS(Utility utility, string apiurl, List<PeopleDetailsDTO> peopleDetails, string agencyId, ILogger log, bool isclosed)
        {
            string result = string.Empty;
            try
            {
                CRUDResponseDTO response = new CRUDResponseDTO();
                if (peopleDetails.Count > 0)
                {
                    int loopCount = peopleDetails.Count;
                    for (int i = 0; i < loopCount; i = i + PCISEnum.Constants.UploadBlockCount)
                    {
                        var peopleDetailsToUpload = peopleDetails.Skip(i).Take(PCISEnum.Constants.UploadBlockCount).ToList();
                        if (peopleDetailsToUpload.Count > 0)
                        {
                            var recordCount = (i + PCISEnum.Constants.UploadBlockCount) > loopCount ? loopCount : i + PCISEnum.Constants.UploadBlockCount;
                            log.LogInformation($"EHR : {DateTime.Now} : AgencyID-{agencyId} : {recordCount}/{loopCount} : Initiating Upload to PCIS..");
                            var uploadAPIurl = apiurl + PCISEnum.APIurl.PCISUpload.Replace(PCISEnum.APIReplacableValues.Isclosed, isclosed.ToString());
                            result = utility.RestApiCall(log, uploadAPIurl, false, false, PCISEnum.APIMethodType.PostRequest, null, peopleDetailsToUpload.ToJSON());
                            response = JsonConvert.DeserializeObject<CRUDResponseDTO>(result);
                            if (response?.result?.ResponseStatusCode == PCISEnum.Constants.InsertionSuccess)
                            {
                                log.LogInformation($"EHR : {DateTime.Now} : AgencyID-{agencyId} : {recordCount}/{loopCount} : {response?.result?.ResponseStatus}");
                            }
                            else
                            {
                                var message = string.IsNullOrEmpty(result) ? "Failed to Upload" : response?.result?.ResponseStatus;
                                log.LogInformation($"EHR : {DateTime.Now} : AgencyID-{agencyId} : {recordCount}/{loopCount} : {message} ");
                                var missedPersonlist = peopleDetailsToUpload.Select(x => x.UniversalID).ToArray();
                                log.LogInformation($"EHR : {DateTime.Now} : AgencyID-{agencyId} : {recordCount}/{loopCount} : Upload failed Person list : {string.Join(",", missedPersonlist)}");
                            }
                        }
                    }
                    return result;
                }
                else
                {
                    log.LogInformation($"EHR : {DateTime.Now} : AgencyID-{agencyId} : No records to upload");
                }
                return null;
            }
            catch (Exception)
            {
                log.LogError($"EHR : {DateTime.Now} : AgencyID-{agencyId} : Exception occurred at {MethodInfo.GetCurrentMethod().Name} APIResult : {result}");
                throw;
            }
        }

        private static List<PeopleDetailsDTO> FormatHouseMemberAPIResultsToPCIS(List<EHRRootData> validateHomeMemberAPIResult, List<PeopleDetailsDTO> homePeopleDetails)
        {
            try
            {
                foreach (var person in homePeopleDetails)
                {
                    var homeSupports = person.PersonSupports.Where(x => x.UpdateUserID != PCISEnum.Constants.EHRUpdateUserID).ToList();
                    foreach (var support in homeSupports)
                    {
                        var eHRsupport = validateHomeMemberAPIResult.Where(x => x.UniversalID == support.UniversalID).ToList();
                        if (eHRsupport.Count > 0 && eHRsupport != null)
                        {
                            support.IsCurrent = true;
                            support.IsRemoved = false;
                            support.UpdateUserID = PCISEnum.Constants.EHRUpdateUserID;
                            support.StartDate = person.StartDate;
                            support.Phone = PCISEnum.Constants.EHRDummyPhoneNumber;
                            support.PhoneCode = PCISEnum.Constants.EHRPhoneCode;
                            support.Email = string.Empty;
                            foreach (var field in eHRsupport?[0].InputFields)
                            {
                                var value = "";
                                if (field.Values.Count > 0)
                                    value = field.Values[0];
                                switch (field.FieldName)
                                {
                                    case PCISEnum.EHRHouseMemberAPIFields.hm_lastname:
                                        support.LastName = value.ToString();
                                        break;
                                    case PCISEnum.EHRHouseMemberAPIFields.hm_firstname:
                                        support.FirstName = value.ToString();
                                        break;
                                    case PCISEnum.EHRHouseMemberAPIFields.hm_phone_home:
                                        if (value.IsValidPhoneNumber())
                                        {
                                            support.PhoneCode = PCISEnum.Constants.EHRPhoneCode;
                                            support.Phone = value.ToString();
                                        }
                                        break;
                                    case PCISEnum.EHRHouseMemberAPIFields.hm_relationship_detail:
                                        support.SupportTypeID = value.ToInt();
                                        break;
                                    case PCISEnum.EHRHouseMemberAPIFields.hm_email:
                                        if (value.IsValidEmail())
                                            support.Email = value.ToString();
                                        break;
                                    case PCISEnum.EHRHouseMemberAPIFields.hm_middleinitial:
                                        support.MiddleName = value.ToString();
                                        break;
                                }
                            }
                        }
                        if (support.SupportTypeID == 0 || support.FirstName == string.Empty)
                        {
                            person.PersonSupports.Remove(support);
                        }
                    }
                }
                return homePeopleDetails;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private static string EHRHouseMemberAPI(Utility utility, AgencyConfigurationResponseDTO agencySpecificConfiguration, string cookie, ILogger log)
        {

            try
            {
                var providerUrl = agencySpecificConfiguration.result.Configurations[PCISEnum.ConfigurationKey.EHRHouseMemberURL];
                return utility.RestApiCall(log, providerUrl, true, false, PCISEnum.APIMethodType.GetRequest, cookie);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static string EHRHomeAPI(Utility utility, AgencyConfigurationResponseDTO agencySpecificConfiguration, string cookie, ILogger log, string statusInURL = "")
        {
            try
            {
                var homeUrl = agencySpecificConfiguration.result.Configurations[PCISEnum.ConfigurationKey.EHRHomeURL];
                homeUrl = homeUrl.Replace(PCISEnum.APIReplacableValues.StatusInURL, statusInURL);
                return utility.RestApiCall(log, homeUrl, true, false, PCISEnum.APIMethodType.GetRequest, cookie);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static List<PeopleDetailsDTO> FormatHomeAPIResultsToPCIS(List<EHRRootData> validateHomeAPIResult, AgencyConfigurationResponseDTO agencySpecificConfiguration, string agencyId)
        {
            try
            {
                List<PeopleDetailsDTO> lst_peopleDetailsDTO = new List<PeopleDetailsDTO>();
                if (validateHomeAPIResult.Count > 0)
                {
                    PersonSupportDTO personSupportDTO = new PersonSupportDTO();
                    PersonSupportDTO personSupportDTO1 = new PersonSupportDTO();
                    PersonSupportDTO personSupportDTO2 = new PersonSupportDTO();
                    PersonRaceEthnicityDTO personRaceEthnicityDTO = new PersonRaceEthnicityDTO();
                    PersonHelperDTO personHelperDTO = new PersonHelperDTO();
                    var defaultcollaborationID = agencySpecificConfiguration.result.Configurations[PCISEnum.ConfigurationKey.EHRDefaultCollaborationID];
                    foreach (var persons in validateHomeAPIResult)
                    {
                        bool islead = true;
                        personSupportDTO1 = new PersonSupportDTO();
                        personSupportDTO2 = new PersonSupportDTO();
                        PeopleDetailsDTO peopleDetailsDTO = CreatePersonObject(defaultcollaborationID);
                        peopleDetailsDTO.UniversalID = persons.UniversalID;
                        peopleDetailsDTO.AgencyID = agencyId.ToLong();
                        personSupportDTO1.IsCurrent = personSupportDTO2.IsCurrent = true;
                        personSupportDTO1.IsRemoved = personSupportDTO2.IsRemoved = false;
                        personSupportDTO1.UniversalID = string.Format("{0}-A", persons.UniversalID);
                        personSupportDTO2.UniversalID = string.Format("{0}-B", persons.UniversalID);
                        personSupportDTO1.UpdateUserID = PCISEnum.Constants.EHRUpdateUserID;
                        personSupportDTO2.UpdateUserID = PCISEnum.Constants.EHRUpdateUserID;
                        peopleDetailsDTO.Phone1 = PCISEnum.Constants.EHRDummyPhoneNumber;
                        personSupportDTO1.Phone = PCISEnum.Constants.EHRDummyPhoneNumber;
                        personSupportDTO2.Phone = PCISEnum.Constants.EHRDummyPhoneNumber;
                        peopleDetailsDTO.Phone1Code = PCISEnum.Constants.EHRPhoneCode;
                        personSupportDTO1.PhoneCode = PCISEnum.Constants.EHRPhoneCode;
                        personSupportDTO2.PhoneCode = PCISEnum.Constants.EHRPhoneCode;
                        peopleDetailsDTO.DateOfBirth = PCISEnum.Constants.EHRDummyDOB.ToDateTime();
                        personSupportDTO1.Email = personSupportDTO2.Email = string.Empty;
                        foreach (var field in persons.InputFields)
                        {
                            var values = string.Empty;
                            if (field.Values.Count > 0)
                                values = field.Values[0];
                            switch (field.FieldName)
                            {
                                case PCISEnum.EHRProviderAPIFieldsForHomeAPI.fh_dob_a:
                                    if (!string.IsNullOrEmpty(values.ToString()))
                                    {
                                        peopleDetailsDTO.DateOfBirth = values.ToDateTime();
                                    }
                                    break;
                                case PCISEnum.EHRProviderAPIFields.fh_relationship_detail:
                                    personSupportDTO1.SupportTypeID = values.ToInt();
                                    personSupportDTO2.SupportTypeID = values.ToInt();
                                    break;
                                case PCISEnum.EHRProviderAPIFields.fh_name_middle_a:
                                    personSupportDTO1.MiddleName = values.ToString();
                                    break;
                                case PCISEnum.EHRProviderAPIFields.fh_name_middle_b:
                                    personSupportDTO2.MiddleName = values.ToString();
                                    break;
                                case PCISEnum.EHRProviderAPIFields.fh_phone_home:
                                    if (values.IsValidPhoneNumber())
                                    {
                                        peopleDetailsDTO.Phone1Code = PCISEnum.Constants.EHRPhoneCode;
                                        personSupportDTO1.PhoneCode = PCISEnum.Constants.EHRPhoneCode;
                                        personSupportDTO2.PhoneCode = PCISEnum.Constants.EHRPhoneCode;
                                        peopleDetailsDTO.Phone1 = values.ToString();
                                        personSupportDTO1.Phone = values.ToString();
                                        personSupportDTO2.Phone = values.ToString();
                                    }
                                    break;
                                case PCISEnum.EHRProviderAPIFieldsForHomeAPI.fh_fullname:
                                    if (values.ToString().Contains(','))
                                    {
                                        var names = values.ToString().Split(',').ToArray();
                                        peopleDetailsDTO.LastName = names[0].ToString().Trim();
                                        if (names.Count() > 1)
                                        {
                                            peopleDetailsDTO.FirstName = values.ToString().Substring(values.ToString().IndexOf(",") + 1).Trim();
                                        }
                                    }
                                    else
                                    {
                                        peopleDetailsDTO.FirstName = values.ToString().Trim();
                                        peopleDetailsDTO.LastName = values.ToString().Trim();
                                    }
                                    break;
                                case PCISEnum.EHRProviderAPIFields.fh_name_last_a:
                                    personSupportDTO1.LastName = values.ToString();
                                    break;
                                case PCISEnum.EHRProviderAPIFields.fh_name_first_a:
                                    personSupportDTO1.FirstName = values.ToString();
                                    break;
                                case PCISEnum.EHRProviderAPIFieldsForHomeAPI.fh_intakedate:
                                    peopleDetailsDTO.StartDate = values.ToDateTime();
                                    personSupportDTO1.StartDate = values.ToDateTime();
                                    personSupportDTO2.StartDate = values.ToDateTime();
                                    break;
                                case PCISEnum.EHRProviderAPIFields.fh_name_last_b:
                                    personSupportDTO2.LastName = values.ToString();
                                    break;
                                case PCISEnum.EHRProviderAPIFields.fh_name_first_b:
                                    personSupportDTO2.FirstName = values.ToString();
                                    break;
                                case PCISEnum.EHRProviderAPIFields.fh_email:
                                    if (values.IsValidEmail())
                                    {
                                        peopleDetailsDTO.Email = values.ToString();
                                        personSupportDTO1.Email = values.ToString();
                                        personSupportDTO2.Email = values.ToString();
                                    }
                                    break;
                                case PCISEnum.EHRProviderAPIFieldsForHomeAPI.fh_worker_unid://helpers
                                case PCISEnum.EHRProviderAPIFieldsForHomeAPI.fh_secworker_unid:
                                    foreach (var item in field.Values)
                                    {
                                        personHelperDTO = new PersonHelperDTO();
                                        personHelperDTO.HelperID = item.ToInt();
                                        personHelperDTO.IsRemoved = false;
                                        personHelperDTO.IsLead = islead;
                                        islead = false;
                                        personHelperDTO.IsCurrent = true;
                                        personHelperDTO.StartDate = peopleDetailsDTO.StartDate;
                                        personHelperDTO.UpdateUserID = PCISEnum.Constants.EHRUpdateUserID;
                                        peopleDetailsDTO.PersonHelpers.Add(personHelperDTO);
                                    }
                                    break;
                                case PCISEnum.EHRProviderAPIFieldsForHomeAPI.fh_member_unids://HomeMemberUnids
                                    foreach (var item in field.Values)
                                    {
                                        personSupportDTO = new PersonSupportDTO();
                                        personSupportDTO.UniversalID = item.ToString();
                                        personSupportDTO.UpdateUserID = 0;
                                        personSupportDTO.StartDate = peopleDetailsDTO.StartDate;
                                        peopleDetailsDTO.PersonSupports.Add(personSupportDTO);
                                    }
                                    break;
                                case PCISEnum.EHRProviderAPIFieldsForHomeAPI.fh_race_a:
                                    foreach (var item in field.Values)
                                    {
                                        personRaceEthnicityDTO = new PersonRaceEthnicityDTO();
                                        personRaceEthnicityDTO.RaceEthnicityID = item.ToInt();
                                        personRaceEthnicityDTO.IsRemoved = false;
                                        peopleDetailsDTO.PersonRaceEthnicities.Add(personRaceEthnicityDTO);
                                    }
                                    break;
                                case PCISEnum.EHRProviderAPIFieldsForHomeAPI.fh_gender_a:
                                    foreach (var item in field.Values)
                                    {
                                        peopleDetailsDTO.GenderID = item.ToInt();
                                    }
                                    break;
                                case PCISEnum.EHRProviderAPIFieldsForHomeAPI.fh_close://provider
                                    if (defaultcollaborationID.ToInt() != 0)
                                    {
                                        peopleDetailsDTO.PersonCollaborations[0].EndDate = (string.IsNullOrEmpty(values)) ? (DateTime?)null : values.ToDateTime();  
                                    }
                                    break;
                            }
                        }
                        if (personSupportDTO1.FirstName != string.Empty && personSupportDTO1.SupportTypeID != 0)
                            peopleDetailsDTO.PersonSupports.Add(personSupportDTO1);
                        if (personSupportDTO2.FirstName != string.Empty && personSupportDTO2.SupportTypeID != 0)
                            peopleDetailsDTO.PersonSupports.Add(personSupportDTO2);
                        if (defaultcollaborationID.ToInt() != 0)
                        {
                            peopleDetailsDTO.PersonCollaborations[0].EnrollDate = peopleDetailsDTO.StartDate;
                        }
                        foreach (var helpers in peopleDetailsDTO.PersonHelpers)
                        {
                            helpers.StartDate = peopleDetailsDTO.StartDate;
                        }
                        if (!string.IsNullOrEmpty(peopleDetailsDTO.FirstName) && !string.IsNullOrEmpty(peopleDetailsDTO.LastName))
                            lst_peopleDetailsDTO.Add(peopleDetailsDTO);
                    }
                }
                return lst_peopleDetailsDTO;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private static LookupDTO FilterFromLookups(LookupDTO lookupsDTO, out int peopleCountInPCIS)
        {
            try
            {
                peopleCountInPCIS = 0;
                if (lookupsDTO?.Lookups?.Count > 0)
                {
                    List<Lookup> peopleCount = new List<Lookup>();
                    lookupsDTO.Lookups.TryGetValue(PCISEnum.Lookups.PCISPeopleCount, out peopleCount);
                    if (peopleCount?.Count > 0)
                    {
                        peopleCountInPCIS = peopleCount[0].Value.ToInt();
                        lookupsDTO.Lookups.Remove(PCISEnum.Lookups.PCISPeopleCount);
                    }
                }
                return lookupsDTO;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private static List<PeopleDetailsDTO> FormatRelationshipAPIResultsToPCIS(List<EHRRootData> validatedRelationshipAPIResult, List<PeopleDetailsDTO> peopleDetails)
        {
            try
            {
                foreach (var person in peopleDetails)
                {
                    var personSupports = person.PersonSupports.ToList();
                    foreach (var support in personSupports)
                    {
                        var eHRsupport = validatedRelationshipAPIResult.Where(x => x.UniversalID == support.UniversalID).ToList();
                        if (eHRsupport.Count > 0 && eHRsupport != null)
                        {
                            support.IsCurrent = true;
                            support.IsRemoved = false;
                            support.Phone = PCISEnum.Constants.EHRDummyPhoneNumber;
                            support.PhoneCode = PCISEnum.Constants.EHRPhoneCode;
                            support.FirstName = PCISEnum.Constants.EHRRelationAPIFirstname;
                            support.UpdateUserID = PCISEnum.Constants.EHRUpdateUserID;
                            support.StartDate = person.StartDate;
                            support.Email = string.Empty;
                            foreach (var field in eHRsupport?[0].InputFields)
                            {
                                var value = string.Empty;
                                if (field.Values.Count > 0)
                                    value = field.Values[0];
                                switch (field.FieldName)
                                {
                                    case PCISEnum.EHRRelationshipAPIFields.r_fullname:
                                        support.LastName = value.ToString();
                                        break;
                                    case PCISEnum.EHRRelationshipAPIFields.r_phone_work:
                                        if (value.IsValidPhoneNumber())
                                        {
                                            support.PhoneCode = PCISEnum.Constants.EHRPhoneCode;
                                            support.Phone = value.ToString();
                                        }
                                        break;
                                    case PCISEnum.EHRRelationshipAPIFields.r_relationship_detail:
                                        support.SupportTypeID = value.ToInt();
                                        break;
                                    case PCISEnum.EHRRelationshipAPIFields.r_email:
                                        if (value.IsValidEmail())
                                        {
                                            support.Email = value.ToString();
                                        }
                                        break;
                                }
                            }
                            if (support.SupportTypeID == 0)
                            {
                                person.PersonSupports.Remove(support);
                            }
                        }
                    }
                }
                return peopleDetails;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private static List<PeopleDetailsDTO> FormatProviderAPIResultsToPCIS(List<EHRRootData> validatedProviderAPIResult, List<PeopleDetailsDTO> peopleDetails)
        {
            try
            {
                foreach (var person in peopleDetails)
                {
                    var homeSupports = person.PersonSupports.Where(x => x.UpdateUserID != PCISEnum.Constants.EHRUpdateUserID).ToList();
                    foreach (var support in homeSupports)
                    {
                        PersonSupportDTO support2 = new PersonSupportDTO();
                        var eHRsupport = validatedProviderAPIResult.Where(x => x.UniversalID == support.UniversalID).ToList();
                        if (eHRsupport.Count > 0 && eHRsupport != null)
                        {
                            support2.IsCurrent = support.IsCurrent = true;
                            support2.IsRemoved = support.IsRemoved = false;
                            support2.Phone = support.Phone;
                            support2.PhoneCode = support.PhoneCode;
                            support2.UpdateUserID = support.UpdateUserID = PCISEnum.Constants.EHRUpdateUserID;
                            support2.StartDate = support.StartDate = person.StartDate;
                            var universalID = support.UniversalID;
                            support.UniversalID = string.Format("{0}-A", universalID);
                            support2.UniversalID = string.Format("{0}-B", universalID);
                            support2.Email = support.Email = string.Empty;
                            foreach (var field in eHRsupport?[0].InputFields)
                            {
                                var value = string.Empty;
                                if (field.Values.Count > 0)
                                    value = field.Values[0];
                                switch (field.FieldName)
                                {
                                    case PCISEnum.EHRProviderAPIFields.fh_name_first_a:
                                        support.FirstName = value.ToString();
                                        break;
                                    case PCISEnum.EHRProviderAPIFields.fh_name_first_b:
                                        support2.FirstName = value.ToString();
                                        break;
                                    case PCISEnum.EHRProviderAPIFields.fh_name_last_a:
                                        support.LastName = value.ToString();
                                        break;
                                    case PCISEnum.EHRProviderAPIFields.fh_name_last_b:
                                        support2.LastName = value.ToString();
                                        break;
                                    case PCISEnum.EHRProviderAPIFields.fh_name_middle_a:
                                        support.MiddleName = value.ToString();
                                        break;
                                    case PCISEnum.EHRProviderAPIFields.fh_name_middle_b:
                                        support2.MiddleName = value.ToString();
                                        break;
                                    case PCISEnum.EHRProviderAPIFields.fh_phone_home:
                                        if (value.IsValidPhoneNumber())
                                        {
                                            support2.PhoneCode = support.PhoneCode = PCISEnum.Constants.EHRPhoneCode;
                                            support2.Phone = support.Phone = value.ToString();
                                        }
                                        break;
                                    case PCISEnum.EHRProviderAPIFields.fh_email:
                                        if (value.IsValidEmail())
                                        {
                                            support2.Email = support.Email = value.ToString();
                                        }
                                        break;
                                    case PCISEnum.EHRProviderAPIFields.fh_relationship_detail:
                                        support2.SupportTypeID = support.SupportTypeID = value.ToInt();
                                        break;
                                }
                            }
                        }
                        if (support.SupportTypeID == 0)
                            person.PersonSupports.Remove(support);
                        if (support2.SupportTypeID != 0 && support2.FirstName != string.Empty)
                            person.PersonSupports.Add(support2);
                    }
                }
                return peopleDetails;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private static List<PeopleDetailsDTO> FormatClientAPIResultsToPCIS(List<EHRRootData> validatedEHRAPIResult, AgencyConfigurationResponseDTO agencySpecificConfiguration, string agencyId)
        {
            try
            {
                List<PeopleDetailsDTO> lst_peopleDetailsDTO = new List<PeopleDetailsDTO>();
                if (validatedEHRAPIResult.Count > 0)
                {
                    PersonSupportDTO personSupportDTO = new PersonSupportDTO();
                    PersonRaceEthnicityDTO personRaceEthnicityDTO = new PersonRaceEthnicityDTO();
                    PersonHelperDTO personHelperDTO = new PersonHelperDTO();
                    var defaultcollaborationID = agencySpecificConfiguration.result.Configurations[PCISEnum.ConfigurationKey.EHRDefaultCollaborationID];
                    foreach (var persons in validatedEHRAPIResult)
                    {
                        PeopleDetailsDTO peopleDetailsDTO = CreatePersonObject(defaultcollaborationID);
                        peopleDetailsDTO.UniversalID = persons.UniversalID;
                        peopleDetailsDTO.AgencyID = agencyId.ToLong();
                        persons.InputFields?.Reverse();
                        foreach (var field in persons.InputFields)
                        {
                            var values = string.Empty;
                            if (field.Values.Count > 0)
                                values = field.Values[0];
                            switch (field.FieldName)
                            {
                                case PCISEnum.EHRClientAPIFields.cc_dob:
                                    peopleDetailsDTO.DateOfBirth = values.ToDateTime();
                                    break;
                                case PCISEnum.EHRClientAPIFields.cc_lastname:
                                    peopleDetailsDTO.LastName = values.ToString();
                                    break;
                                case PCISEnum.EHRClientAPIFields.cc_firstname:
                                    peopleDetailsDTO.FirstName = values.ToString();
                                    break;
                                case PCISEnum.EHRClientAPIFields.cc_middleinitial:
                                    peopleDetailsDTO.MiddleName = values.ToString();
                                    break;
                                case PCISEnum.EHRClientAPIFields.cc_personalphone:
                                    if (!string.IsNullOrEmpty(values.ToString()))
                                    {
                                        peopleDetailsDTO.Phone1Code = PCISEnum.Constants.EHRPhoneCode;
                                        peopleDetailsDTO.Phone1 = values.ToString();
                                    }
                                    break;
                                case PCISEnum.EHRClientAPIFields.cc_personalemail:
                                    if (values.IsValidEmail())
                                        peopleDetailsDTO.Email = values.ToString();
                                    break;
                                case PCISEnum.EHRClientAPIFields.cc_referral_date:
                                    peopleDetailsDTO.StartDate = values.ToDateTime();
                                    break;
                                case PCISEnum.EHRClientAPIFields.cc_gender:
                                    peopleDetailsDTO.GenderID = values.ToInt();
                                    break;
                                case PCISEnum.EHRClientAPIFields.cc_primarylanguage:
                                    peopleDetailsDTO.PrimaryLanguageID = values.ToInt();
                                    break;
                                case PCISEnum.EHRClientAPIFields.cc_race:
                                    foreach (var item in field.Values)
                                    {
                                        personRaceEthnicityDTO = new PersonRaceEthnicityDTO();
                                        personRaceEthnicityDTO.RaceEthnicityID = item.ToInt();
                                        personRaceEthnicityDTO.IsRemoved = false;
                                        peopleDetailsDTO.PersonRaceEthnicities.Add(personRaceEthnicityDTO);
                                    }
                                    break;
                                case PCISEnum.EHRClientAPIFields.cc_therapist_unid://helper
                                    bool IsLead = true;
                                    foreach (var item in field.Values)
                                    {
                                        personHelperDTO = new PersonHelperDTO();
                                        personHelperDTO.HelperID = item.ToInt();
                                        personHelperDTO.IsRemoved = false;
                                        personHelperDTO.IsLead = IsLead;
                                        IsLead = false;
                                        personHelperDTO.IsCurrent = true;
                                        personHelperDTO.StartDate = peopleDetailsDTO.StartDate;
                                        personHelperDTO.UpdateUserID = PCISEnum.Constants.EHRUpdateUserID;
                                        peopleDetailsDTO.PersonHelpers.Add(personHelperDTO);

                                    }
                                    break;
                                case PCISEnum.EHRClientAPIFields.cc_relationship_unids://relationship
                                    foreach (var item in field.Values)
                                    {
                                        personSupportDTO = new PersonSupportDTO();
                                        personSupportDTO.UniversalID = item.ToString();
                                        personSupportDTO.UpdateUserID = 0;
                                        personSupportDTO.Phone = PCISEnum.Constants.EHRDummyPhoneNumber;
                                        personSupportDTO.PhoneCode = PCISEnum.Constants.EHRPhoneCode;
                                        personSupportDTO.StartDate = peopleDetailsDTO.StartDate;
                                        peopleDetailsDTO.PersonSupports.Add(personSupportDTO);
                                    }
                                    break;
                                case PCISEnum.EHRClientAPIFields.cc_home_unid://provider
                                    foreach (var item in field.Values)
                                    {
                                        personSupportDTO = new PersonSupportDTO();
                                        personSupportDTO.UniversalID = item.ToString();
                                        personSupportDTO.Phone = PCISEnum.Constants.EHRDummyPhoneNumber;
                                        personSupportDTO.PhoneCode = PCISEnum.Constants.EHRPhoneCode;
                                        personSupportDTO.StartDate = peopleDetailsDTO.StartDate;
                                        peopleDetailsDTO.PersonSupports.Add(personSupportDTO);
                                    }
                                    break;
                                case PCISEnum.EHRClientAPIFields.cc_close://provider
                                    if (defaultcollaborationID.ToInt() != 0)
                                    {
                                         peopleDetailsDTO.PersonCollaborations[0].EndDate = (string.IsNullOrEmpty(values)) ? (DateTime?)null : values.ToDateTime();  
                                    }
                                    break;
                            }
                        }
                        if (defaultcollaborationID.ToInt() != 0)
                        {
                            peopleDetailsDTO.PersonCollaborations[0].EnrollDate = peopleDetailsDTO.StartDate;
                        }
                        lst_peopleDetailsDTO.Add(peopleDetailsDTO);
                    }
                }
                return lst_peopleDetailsDTO;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private static PeopleDetailsDTO CreatePersonObject(string defaultcollaborationID)
        {
            try
            {
                PeopleDetailsDTO peopleDetailsDTO = new PeopleDetailsDTO();
                peopleDetailsDTO.FirstName = string.Empty;
                peopleDetailsDTO.MiddleName = string.Empty;
                peopleDetailsDTO.LastName = string.Empty;
                peopleDetailsDTO.IsActive = true;
                peopleDetailsDTO.IsRemoved = false;
                peopleDetailsDTO.UpdateUserID = PCISEnum.Constants.EHRUpdateUserID;
                peopleDetailsDTO.PersonScreeningStatusID = 1;
                peopleDetailsDTO.Address1 = string.Empty;
                peopleDetailsDTO.Address2 = string.Empty;
                peopleDetailsDTO.Phone1 = PCISEnum.Constants.EHRDummyPhoneNumber;
                peopleDetailsDTO.Phone2 = string.Empty;
                peopleDetailsDTO.Phone1Code = PCISEnum.Constants.EHRPhoneCode;
                peopleDetailsDTO.Phone2Code = string.Empty;
                peopleDetailsDTO.Suffix = string.Empty;
                peopleDetailsDTO.Zip = string.Empty;
                peopleDetailsDTO.Zip4 = string.Empty;
                peopleDetailsDTO.CountryStateId = 0;
                peopleDetailsDTO.City = string.Empty;
                peopleDetailsDTO.Email = string.Empty;
                peopleDetailsDTO.PersonRaceEthnicities = new List<PersonRaceEthnicityDTO>();
                peopleDetailsDTO.PersonSupports = new List<PersonSupportDTO>();
                peopleDetailsDTO.PersonHelpers = new List<PersonHelperDTO>();
                peopleDetailsDTO.PersonIdentifications = new List<PersonIdentificationDTO>();
                peopleDetailsDTO.PersonCollaborations = new List<PersonCollaborationDTO>();
                PersonCollaborationDTO defaultPersonCollaborationDTO = new PersonCollaborationDTO();
                if (defaultcollaborationID.ToInt() != 0)
                {
                    defaultPersonCollaborationDTO.CollaborationID = defaultcollaborationID.ToInt();
                    defaultPersonCollaborationDTO.IsPrimary = true;
                    defaultPersonCollaborationDTO.IsRemoved = false;
                    defaultPersonCollaborationDTO.UpdateUserID = PCISEnum.Constants.EHRUpdateUserID;
                    peopleDetailsDTO.PersonCollaborations.Add(defaultPersonCollaborationDTO);
                }
                return peopleDetailsDTO;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private static List<EHRRootData> ReplaceLookupIDInEHRAPIResult(List<EHRRootData> deserializedResult, LookupDTO lookups)
        {
            try
            {
                foreach (var data in deserializedResult)
                {
                    data.IsValid = true;
                    foreach (var fields in data.InputFields)
                    {
                        List<string> newIDFields = new List<string>();
                        switch (fields.FieldName)
                        {
                            case PCISEnum.EHRProviderAPIFieldsForHomeAPI.fh_gender_a:
                            case PCISEnum.EHRProviderAPIFieldsForHomeAPI.fh_gender_b:
                                for(int i= 0; i<fields.Values.Count; i++)
                                {
                                    if(fields.Values[i].ToUpper() == "M")
                                    {
                                        fields.Values[i] = "Male";
                                    }
                                    if (fields.Values[i].ToUpper() == "F")
                                    {
                                        fields.Values[i] = "Female";
                                    }
                                }
                                fields.Values = ValidateLookup(PCISEnum.Lookups.Gender, fields.Values, lookups);
                                //if no gender assign an unknown gender.
                                List<string> unknownGender = new List<string>() { PCISEnum.Constants.EHRDummyRace };
                                if (fields.Values.Count == 0)
                                {
                                    fields.Values = unknownGender;
                                    fields.Values = ValidateLookup(PCISEnum.Lookups.Gender, fields.Values, lookups);
                                }
                                break;
                            case PCISEnum.EHRClientAPIFields.cc_gender:
                                fields.Values = ValidateLookup(PCISEnum.Lookups.Gender, fields.Values, lookups);
                                List<string> unknownGender1 = new List<string>() { PCISEnum.Constants.EHRDummyRace };
                                if (fields.Values.Count == 0)
                                {
                                    fields.Values = unknownGender1;
                                    fields.Values = ValidateLookup(PCISEnum.Lookups.Gender, fields.Values, lookups);
                                }
                                break;
                            case PCISEnum.EHRClientAPIFields.cc_primarylanguage:
                                fields.Values = ValidateLookup(PCISEnum.Lookups.Language, fields.Values, lookups);
                                break;
                            case PCISEnum.EHRProviderAPIFieldsForHomeAPI.fh_race_a:
                            case PCISEnum.EHRProviderAPIFieldsForHomeAPI.fh_race_b:
                            case PCISEnum.EHRClientAPIFields.cc_race:
                                fields.Values = ValidateLookup(PCISEnum.Lookups.Race, fields.Values, lookups);
                                List<string> unknownRace = new List<string>() { PCISEnum.Constants.EHRDummyRace };
                                if (fields.Values.Count == 0)
                                {
                                    fields.Values = unknownRace;
                                    fields.Values = ValidateLookup(PCISEnum.Lookups.Race, fields.Values, lookups);
                                    //data.IsValid = false;
                                }
                                break;
                            //Helpers
                            case PCISEnum.EHRClientAPIFields.cc_therapist_unid:
                            case PCISEnum.EHRProviderAPIFieldsForHomeAPI.fh_worker_unid:
                            case PCISEnum.EHRProviderAPIFieldsForHomeAPI.fh_secworker_unid:
                                fields.Values = ValidateLookup(PCISEnum.Lookups.Helper, fields.Values, lookups);
                                if (fields.Values.Count == 0)
                                {
                                    data.IsValid = false;
                                }
                                break;
                            //supportType
                            case PCISEnum.EHRRelationshipAPIFields.r_relationship_detail:
                            case PCISEnum.EHRProviderAPIFields.fh_relationship_detail:
                            case PCISEnum.EHRHouseMemberAPIFields.hm_relationship_detail:
                                fields.Values = ValidateLookup(PCISEnum.Lookups.SupportType, fields.Values, lookups);
                                if (fields.Values.Count == 0)
                                {
                                    data.IsValid = false;
                                }
                                break;
                        }
                    }
                }
                return deserializedResult;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private static List<string> ReplaceLookupIDs(EHRAPIInputDTO fields, LookupDTO lookups)
        {
            try
            {
                List<string> newIDFields = new List<string>();
                switch (fields.FieldName)
                {
                    case PCISEnum.EHRClientAPIFields.cc_gender:
                        fields.Values = ValidateLookup(PCISEnum.Lookups.Gender, fields.Values, lookups);
                        break;
                    case PCISEnum.EHRClientAPIFields.cc_primarylanguage:
                        fields.Values = ValidateLookup(PCISEnum.Lookups.Language, fields.Values, lookups);
                        break;
                    case PCISEnum.EHRClientAPIFields.cc_race:
                        fields.Values = ValidateLookup(PCISEnum.Lookups.Race, fields.Values, lookups);
                        break;
                    case PCISEnum.EHRClientAPIFields.cc_therapist_unid:
                        fields.Values = ValidateLookup(PCISEnum.Lookups.Helper, fields.Values, lookups);
                        break;
                }
                return fields.Values;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private static LookupDTO GetPCISAgencyLookups(Utility utility, string apiurl, string agencyId, ILogger log)
        {
            try
            {
                var lookupAPIurl = apiurl + PCISEnum.APIurl.LookupsForAgency.Replace(PCISEnum.APIReplacableValues.AgencyID, agencyId);
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


        private static List<EHRRootData> ValidateEHRAPIResult(List<EHRRootData> deserializedResult, LookupDTO lookups, string apiType, string agencyId, ILogger log)
        {
            try
            {
                List<PeopleDetailsDTO> lst_persons = new List<PeopleDetailsDTO>();
                foreach (var dataRow in deserializedResult)
                {
                    dataRow.IsValid = true;
                    var IsValid = false;
                    foreach (var fields in dataRow.InputFields)
                    {
                        IsValid = true;
                        switch (fields.FieldName)
                        {
                            case PCISEnum.EHRClientAPIFields.cc_lastname:
                            case PCISEnum.EHRClientAPIFields.cc_firstname:
                            case PCISEnum.EHRRelationshipAPIFields.r_fullname:
                            case PCISEnum.EHRHouseMemberAPIFields.hm_firstname:
                                IsValid = ValidateName(fields.Values);
                                break;
                            case PCISEnum.EHRClientAPIFields.cc_dob:
                                IsValid = ValidateDOB(fields.Values);
                                break;
                            case PCISEnum.EHRClientAPIFields.cc_middleinitial:
                            case PCISEnum.EHRClientAPIFields.cc_gender:
                            case PCISEnum.EHRClientAPIFields.cc_personalphone:
                            case PCISEnum.EHRClientAPIFields.cc_personalemail:
                            case PCISEnum.EHRClientAPIFields.cc_primarylanguage:
                            case PCISEnum.EHRClientAPIFields.cc_race:
                            case PCISEnum.EHRClientAPIFields.cc_referral_date:
                            case PCISEnum.EHRClientAPIFields.cc_therapist_unid:
                            case PCISEnum.EHRClientAPIFields.cc_relationship_unids:
                                break;
                            case PCISEnum.EHRRelationshipAPIFields.r_relationship_detail:
                            case PCISEnum.EHRHouseMemberAPIFields.hm_relationship_detail:
                                IsValid = ValidateName(fields.Values);
                                break;
                            case PCISEnum.EHRRelationshipAPIFields.r_phone_work:
                            case PCISEnum.EHRRelationshipAPIFields.r_email:
                                break;
                            case PCISEnum.EHRProviderAPIFieldsForHomeAPI.fh_fullname:
                                IsValid = ValidateName(fields.Values);
                                break;
                            //not validated here.validated while converting To PCIS Format
                            case PCISEnum.EHRProviderAPIFields.fh_name_first_a:
                            case PCISEnum.EHRProviderAPIFields.fh_name_first_b:
                            case PCISEnum.EHRProviderAPIFields.fh_relationship_detail:
                                break;
                            case PCISEnum.EHRProviderAPIFields.fh_name_last_a:
                            case PCISEnum.EHRProviderAPIFields.fh_name_last_b:
                            case PCISEnum.EHRProviderAPIFields.fh_phone_home:
                            case PCISEnum.EHRProviderAPIFields.fh_email:
                                break;
                            case PCISEnum.EHRProviderAPIFieldsForHomeAPI.fh_providertype:
                                IsValid = ValidateProviderType(fields.Values);
                                break;
                        }
                        if (!IsValid)
                        {
                            break;
                        }
                    }
                    dataRow.IsValid = IsValid;
                }

                var list_InvalidPersons = deserializedResult.Where(x => x.IsValid == false).ToList();
                if (list_InvalidPersons?.Count > 0)
                {
                    var invalidPersons = string.Join(",", list_InvalidPersons.Select(x => x.UniversalID).ToArray());
                    log.LogInformation($"EHR : {DateTime.Now} : AgencyID-{agencyId} : Invalid UniversalIDs from {apiType} on applying validations - {invalidPersons}");
                }
                return deserializedResult.Where(x => x.IsValid == true).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private static bool ValidateProviderType(List<string> fieldValues)
        {
            if (fieldValues.Count > 0)
            {
                if (string.IsNullOrEmpty(fieldValues[0]))
                    return false;
                else if (PCISEnum.EHRProviderType.Contains(fieldValues[0].ToUpper()))
                {
                    return true;
                }
            }
            return false;
        }

        private static bool ValidateFields(List<string> fieldValues)
        {
            if (fieldValues.Count > 0)
            {
                if (string.IsNullOrEmpty(fieldValues[0]))
                    return false;
                return true;
            }
            return false;
        }


        private static bool ValidatePhone(List<string> values)
        {
            return ValidateFields(values);
        }

        private static List<string> ValidateLookup(string lookupName, List<string> fieldValues, LookupDTO lookupsDTO)
        {
            try
            {
                List<string> fieldIDs = new List<string>();
                if (lookupsDTO?.Lookups?.Count > 0)
                {
                    var fieldWiseLookup = lookupsDTO.Lookups[lookupName];
                    foreach (var Value in fieldValues)
                    {
                        if (!string.IsNullOrEmpty(Value) && Value != "0")
                        {
                            var lookupID = fieldWiseLookup.Where(x => x.Value.ToUpper() == Value.ToUpper()).FirstOrDefault();
                            if (lookupID != null)
                                fieldIDs.Add(lookupID.Id);
                        }
                    }
                }
                return fieldIDs;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private static bool ValidateEmail(List<string> values)
        {
            try
            {
                return ValidateFields(values);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private static bool ValidateName(List<string> values)
        {
            try
            {
                return ValidateFields(values);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private static bool ValidateDOB(List<string> values)
        {
            try
            {
                return ValidateFields(values);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private static List<EHRRootData> DeserializeEHRAPIResult(string result, string APIType, int peopleCountInPCIS = 0)
        {
            try
            {
                List<EHRRootData> eHRClients = new List<EHRRootData>();
                if (!string.IsNullOrEmpty(result))
                {
                    var currentTime = DateTime.UtcNow;
                    var last48hrs = currentTime.AddHours(-PCISEnum.Constants.Hours);
                    var allData = JsonConvert.DeserializeObject<Dictionary<string, object>>(result);
                    foreach (var data in allData)
                    {
                        bool isValid = false;
                        dynamic dyn = JsonConvert.DeserializeObject(data.Value.ToString());
                        var items = dyn.Items.ToString();
                        string lastModifiedDate = dyn.LastModified.ToString();
                        var updatedDate = lastModifiedDate.ToDateTimeUTC();
                        var allFields = JsonConvert.DeserializeObject<Dictionary<string, object>>(items.ToString());
                        List<EHRAPIInputDTO> lst_allFields = new List<EHRAPIInputDTO>();
                        foreach (var fields in allFields)
                        {
                            EHRAPIInputDTO eHRAPIInputDTO = new EHRAPIInputDTO();
                            eHRAPIInputDTO = JsonConvert.DeserializeObject<EHRAPIInputDTO>(fields.Value.ToString());
                            eHRAPIInputDTO.FieldName = fields.Key;
                            lst_allFields.Add(eHRAPIInputDTO);
                        }
                        if ((APIType == PCISEnum.ConfigurationKey.EHRProviderURL || APIType == PCISEnum.ConfigurationKey.EHRHomeURL) && lst_allFields.Count > 0)
                        {
                            EHRAPIInputDTO eHRAPIInputDTO = new EHRAPIInputDTO()
                            {
                                FieldName = PCISEnum.EHRProviderAPIFields.fh_relationship_detail,
                                Values = new List<string>() { PCISEnum.Constants.EHRProviderAPIRelation }
                            };
                            lst_allFields.Add(eHRAPIInputDTO);
                        }
                        if (peopleCountInPCIS > 0 && updatedDate > last48hrs && (APIType == PCISEnum.ConfigurationKey.EHRClientURL || APIType == PCISEnum.ConfigurationKey.EHRHomeURL))
                        {
                            isValid = true;
                        }
                        eHRClients.Add(
                            new EHRRootData
                            {
                                UniversalID = data.Key,
                                IsValid = isValid,
                                LastModified = updatedDate,
                                InputFields = lst_allFields
                            });
                    }
                }
                return eHRClients;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private static string EHRClientAPI(Utility Utility, AgencyConfigurationResponseDTO agencySpecificConfiguration, string cookie, ILogger log,string APItype = "",string statusInURL = "")
        {
            try
            {
                var clientUrl = agencySpecificConfiguration.result.Configurations[APItype];
                clientUrl = clientUrl.Replace(PCISEnum.APIReplacableValues.StatusInURL, statusInURL);
                return Utility.RestApiCall(log, clientUrl, true, false, PCISEnum.APIMethodType.GetRequest, cookie);
            }
            catch (Exception)
            {
                throw;
            }
        }


        private static string EHRRelationshipAPI(Utility Utility, AgencyConfigurationResponseDTO agencySpecificConfiguration, string cookie, ILogger log)
        {
            try
            {
                var relationshipUrl = agencySpecificConfiguration.result.Configurations[PCISEnum.ConfigurationKey.EHRRelationshipURL];
                return Utility.RestApiCall(log, relationshipUrl, true, false, PCISEnum.APIMethodType.GetRequest, cookie);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static string EHRProviderAPI(Utility Utility, AgencyConfigurationResponseDTO agencySpecificConfiguration, string cookie, ILogger log)
        {
            try
            {
                var providerUrl = agencySpecificConfiguration.result.Configurations[PCISEnum.ConfigurationKey.EHRProviderURL];
                return Utility.RestApiCall(log, providerUrl, true, false, PCISEnum.APIMethodType.GetRequest, cookie);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static string EHRLoginAPI(Utility Utility, AgencyConfigurationResponseDTO agencySpecificConfiguration, ILogger log)
        {
            try
            {
                var loginurl = agencySpecificConfiguration.result.Configurations[PCISEnum.ConfigurationKey.EHRLoginURL];
                var loginUserName = agencySpecificConfiguration.result.Configurations[PCISEnum.ConfigurationKey.EHRUsername];
                var loginPassword = agencySpecificConfiguration.result.Configurations[PCISEnum.ConfigurationKey.EHRPassword];
                loginurl += PCISEnum.Constants.UsernameForUrl + loginUserName + PCISEnum.Constants.PasswordForUrl + loginPassword;

                return Utility.RestApiCall(log, loginurl, true, true);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static AgencyConfigurationResponseDTO GetAgencySpecificConfiguration(Utility Utility, string apiurl, string agencyId, ILogger log)
        {
            try
            {
                var agencySpecificConfigurationurl = apiurl + PCISEnum.APIurl.ConfigurationForAgency.Replace(PCISEnum.APIReplacableValues.AgencyID, agencyId);
                var agencySpecificConfigurationResult = Utility.RestApiCall(log, agencySpecificConfigurationurl, false);
                var agencySpecificConfiguration = JsonConvert.DeserializeObject<AgencyConfigurationResponseDTO>(agencySpecificConfigurationResult);
                return agencySpecificConfiguration;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static string[] GetAgncyIdsForProcess(Utility Utility, string apiurl, ILogger log)
        {
            try
            {
                string[] agencyIdsToProcess = new string[0];
                var configAPiurl = apiurl + PCISEnum.APIurl.ConfigurationWithKey.Replace(PCISEnum.APIReplacableValues.Key, PCISEnum.ConfigurationKey.EHRAgencyIds).Replace(PCISEnum.APIReplacableValues.AgencyID, "0");
                var result = Utility.RestApiCall(log, configAPiurl, false);
                if (!string.IsNullOrEmpty(result))
                {
                    var config = JsonConvert.DeserializeObject<ConfigurationResponseDTO>(result);
                    agencyIdsToProcess = config.result.ConfigurationValue?.Split(',');
                }
                return agencyIdsToProcess;
            }
            catch (Exception)
            {
                throw;
            }
        }


        #region UpdatingEHRWithAppovedAssessmentDate(pcis-2775)
        private static void UpdateToEHR(Utility utility, string cookie, string baseapiurl, string agencyId, AgencyConfigurationResponseDTO agencySpecificConfiguration, ILogger log)
        {
            try
            {
                var timezone = string.Empty;
                log.LogInformation($"EHRUpdateProcess : {DateTime.Now} : AgencyID-{agencyId} : *************START*************");
                List<int> updatedAssessments = new List<int>();
                List<int> missedAssessments = new List<int>();
                List<InstrumentConfiguration> instrumentList = new List<InstrumentConfiguration>();
                log.LogInformation($"EHRUpdateProcess : {DateTime.Now} : AgencyID-{agencyId} : Fetching configuration related to Instruments");
                if (agencySpecificConfiguration.result.Configurations.ContainsKey(PCISEnum.ConfigurationKey.EHRInstrumentsToUpdate))
                {
                    var instrumentsDetails = agencySpecificConfiguration.result.Configurations[PCISEnum.ConfigurationKey.EHRInstrumentsToUpdate];
                    instrumentList = instrumentsDetails.DeserializeJSON<List<InstrumentConfiguration>>();
                }
                if (agencySpecificConfiguration.result.Configurations.ContainsKey(PCISEnum.ConfigurationKey.EHRTimezone))
                {
                    timezone = agencySpecificConfiguration.result.Configurations[PCISEnum.ConfigurationKey.EHRTimezone];
                }
                log.LogInformation($"EHRUpdateProcess : {DateTime.Now} : AgencyID-{agencyId} : Fetch Assessment IDs from PCIS");
                var ehrAssessmentIDsList = GetAssessmentIDsForEHRUpdate(utility, baseapiurl, agencyId, log);
                log.LogInformation($"EHRUpdateProcess : {DateTime.Now} : AgencyID-{agencyId} : Total AssessmentIDs from PCIS={ehrAssessmentIDsList.Count}");
                if (ehrAssessmentIDsList.Count > 0 && instrumentList.Count > 0)
                {
                    int loopCount = ehrAssessmentIDsList.Count;
                    for (int i = 0; i < loopCount; i = i + PCISEnum.Constants.EHRDetailsFetchCount)
                    {
                        var recordCount = (i + PCISEnum.Constants.EHRDetailsFetchCount) > loopCount ? loopCount : i + PCISEnum.Constants.UploadBlockCount;
                        var ehrAssessmentIds = ehrAssessmentIDsList.Skip(i).Take(PCISEnum.Constants.EHRDetailsFetchCount).ToList();
                        if (ehrAssessmentIds.Count > 0)
                        {
                            log.LogInformation($"EHRUpdateProcess : {DateTime.Now} : AgencyID-{agencyId} : {recordCount}/{ehrAssessmentIDsList.Count} : Get Assessment Details From PCIS..");
                            string message = string.Empty;
                            var ehrAssessmentList = GetDetailsForEHRUpdate(utility, baseapiurl, ehrAssessmentIds, log);
                            log.LogInformation($"EHRUpdateProcess : {DateTime.Now} : AgencyID-{agencyId} : {recordCount}/{ehrAssessmentIDsList.Count} : Initiating Update to EHR..");
                            foreach (var ehrAssessmentDTO in ehrAssessmentList)
                            {
                                foreach (InstrumentConfiguration instrument in instrumentList)
                                {
                                    if (ehrAssessmentDTO.InstrumentAbbrev.Contains(instrument.TypeName) && !string.IsNullOrEmpty(instrument.TypeID))
                                    {
                                        if (utility.EHRPostAPICall(cookie, ehrAssessmentDTO, instrument, log, timezone))
                                        {
                                            message = "Successfully Updated";
                                            updatedAssessments.Add(ehrAssessmentDTO.AssessmentID);
                                        }
                                        else
                                        {
                                            message = "Failed To Update";
                                            missedAssessments.Add(ehrAssessmentDTO.AssessmentID);
                                        }
                                        break;
                                    }
                                }
                            }
                            log.LogInformation($"EHRUpdateProcess : {DateTime.Now} : AgencyID-{agencyId} : {recordCount}/{ehrAssessmentIDsList.Count} : {message}..");
                        }
                    }
                    if (missedAssessments.Count > 0)
                    {
                        log.LogInformation($"EHRUpdateProcess : {DateTime.Now} : AgencyID-{agencyId} :Missed Updating in EHR : {string.Join(",",missedAssessments.ToArray())}");
                    }
                    var result = UpdateAssessmentsAfterEHRUpdate(utility, baseapiurl, agencyId,updatedAssessments, log);
                }
                log.LogInformation($"EHRUpdateProcess : {DateTime.Now} : AgencyID-{agencyId} : *************END*************");
            }
            catch (Exception ex)
            {
                log.LogError($"EHRUpdateProcess : {DateTime.Now} : AgencyID-{agencyId} : Exception occurred at {MethodInfo.GetCurrentMethod().Name} : {ex.Message}");
                throw;
            }
        }

        private static CRUDResponseDTO UpdateAssessmentsAfterEHRUpdate(Utility utility, string baseapiurl, string agencyId, List<int> updatedAssessments, ILogger log)
        {
            try
            {
                if (updatedAssessments.Count > 0)
                {
                    log.LogInformation($"EHRUpdateProcess : {DateTime.Now} : AgencyID-{agencyId} : Update StatusFlag of {updatedAssessments.Count} Assessments in PCIS");
                    var url = baseapiurl + PCISEnum.APIurl.UpdateAssessmentFlagAfterEHRUpdate;
                    var result = utility.RestApiCall(log, url, false, false, PCISEnum.APIMethodType.PostRequest, null, updatedAssessments.ToJSON());
                    if (!string.IsNullOrEmpty(result))
                    {
                        return result.DeserializeJSON<CRUDResponseDTO>();
                    }
                }
                return new CRUDResponseDTO();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private static List<string> GetAssessmentIDsForEHRUpdate(Utility utility, string baseapiurl, string agencyId, ILogger log)
        {
            try
            {
                var url = baseapiurl + PCISEnum.APIurl.GetAssessmentIDsForEHRUpdate.Replace(PCISEnum.APIReplacableValues.AgencyID, Convert.ToString(agencyId));
                var result = utility.RestApiCall(log, url, false);
                if (!string.IsNullOrEmpty(result))
                {
                    var response = result.DeserializeJSON<EHRAssessmentResponseDetailsDTO>();
                    return response?.result?.EHRAssessmentIDs;
                }
                return new List<string>();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private static List<EHRAssessmentDTO> GetDetailsForEHRUpdate(Utility utility, string baseapiurl, List<string> ehrAssessmentIds, ILogger log)
        {
            try
            {
                List<EHRAssessmentDTO> ehrAssessments = new List<EHRAssessmentDTO>();
                var url = baseapiurl + PCISEnum.APIurl.GetDetailsForEHRUpdate;
                var result = utility.RestApiCall(log, url, false, false, PCISEnum.APIMethodType.PostRequest, null, ehrAssessmentIds.ToJSON());
                if (!string.IsNullOrEmpty(result))
                {
                    var ehrAssessmentResponse = result.DeserializeJSON<EHRAssessmentResponseDetailsDTO>();
                    ehrAssessments = ehrAssessmentResponse?.result?.EHRAssessments;
                }
                return ehrAssessments;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}