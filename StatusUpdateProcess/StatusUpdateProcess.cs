using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StatusUpdateProcess.Common;
using StatusUpdateProcess.DTO;
using StatusUpdateProcess.Enums;

namespace StatusUpdateProcess
{
    public static class StatusUpdateProcess
    {
        [FunctionName("StatusUpdateProcess")]
        public static void Run([TimerTrigger("%timer-frequency%")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# StatusUpdateProcess function executed at: {DateTime.Now}");
            try
            {
                Utility Utility = new Utility();
                var apiurl = Environment.GetEnvironmentVariable(PCISEnum.EnvironmentVariables.ApiUrlFromKeyVault, EnvironmentVariableTarget.Process);
                log.LogInformation($"C# StatusUpdateProcess : GetBackgroundProcessLog Start");
                var processLog = GetBackgroundProcessLog(Utility, apiurl, PCISEnum.BackgroundProcess.TriggerStatusUpdate);
                log.LogInformation($"C# StatusUpdateProcess : GetBackgroundProcessLog End");

                if (processLog == null)
                {
                    processLog = new BackgroundProcessLogDTO()
                    {
                        ProcessName = "TriggerStatusUpdate",
                        LastProcessedDate = DateTime.UtcNow.Date.AddDays(-1)
                    };
                    AddBackgroundProcessLog(Utility, apiurl, processLog);
                }
                else
                {
                    if (processLog.LastProcessedDate != DateTime.Now.Date)
                    {
                        ProcessTriggerStatusUpdate(Utility, apiurl, log);
                        processLog.LastProcessedDate = DateTime.UtcNow.Date;
                        var result = UpdateBackgroundProcessLog(Utility, apiurl, processLog);
                    }

                }
                log.LogInformation($"C# StatusUpdateProcess : End");

            }
            catch (Exception ex)
            {
                log.LogError(ex, $@"StatusUpdateProcess : Exception occurred.Exception Message : {ex.Message}.Location : {ex.StackTrace}.Details : {ex.InnerException}");
            }
        }

        private static CRUDResponse AddBackgroundProcessLog(Utility Utility, string apiurl, BackgroundProcessLogDTO backgroundProcess)
        {
            try
            {
                CRUDResponse Response = new CRUDResponse();
                var url = apiurl + PCISEnum.APIurl.BackgroundProcess;
                var result = Utility.RestApiCall(url, false, false, PCISEnum.APIMethodType.PostRequest, null, backgroundProcess.ToJSON());
                if (!string.IsNullOrEmpty(result))
                {
                    var TotalResponse = JsonConvert.DeserializeObject<CRUDResponseDTO>(result);
                    Response = TotalResponse.result;
                }
                return Response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static CRUDResponse UpdateIsActiveForPerson(Utility Utility, string apiurl, List<long> PersonIds)
        {
            try
            {
                CRUDResponse Response = new CRUDResponse();
                var url = apiurl + PCISEnum.APIurl.UpdatePerson;
                var result = Utility.RestApiCall(url, false, false, PCISEnum.APIMethodType.PutRequest, null, PersonIds.ToJSON());
                if (!string.IsNullOrEmpty(result))
                {
                    var TotalResponse = JsonConvert.DeserializeObject<CRUDResponseDTO>(result);
                    Response = TotalResponse.result;
                }
                return Response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static CRUDResponse UpdateBackgroundProcessLog(Utility Utility, string apiurl, BackgroundProcessLogDTO backgroundProcess)
        {
            try
            {
                CRUDResponse Response = new CRUDResponse();
                var url = apiurl + PCISEnum.APIurl.BackgroundProcess;
                var result = Utility.RestApiCall(url, false, false, PCISEnum.APIMethodType.PutRequest, null, backgroundProcess.ToJSON());
                if (!string.IsNullOrEmpty(result))
                {
                    var TotalResponse = JsonConvert.DeserializeObject<CRUDResponseDTO>(result);
                    Response = TotalResponse.result;
                }
                return Response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static BackgroundProcessLogDTO GetBackgroundProcessLog(Utility Utility, string apiurl, string name)
        {
            try
            {
                BackgroundProcessLogDTO response = new BackgroundProcessLogDTO();
                var configAPiurl = apiurl + PCISEnum.APIurl.GetBackgroundProcess.Replace(PCISEnum.APIReplacableValues.Name, Convert.ToString(name));
                var result = Utility.RestApiCall(configAPiurl, false);
                if (!string.IsNullOrEmpty(result))
                {
                    var responseDetails = JsonConvert.DeserializeObject<BackgroundProcessResponseDTO>(result);
                    response = responseDetails.result.BackgroundProcess;
                }
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static List<long> GetActivePersons(Utility Utility, string apiurl)
        {
            try
            {
                List<long> response = new List<long>();
                var configAPiurl = apiurl + PCISEnum.APIurl.GetActivePerson;
                var result = Utility.RestApiCall(configAPiurl, false);
                if (!string.IsNullOrEmpty(result))
                {
                    var responseDetails = JsonConvert.DeserializeObject<ActivePersonResponseDTO>(result);
                    response = responseDetails.result.PersonIds;
                }
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static List<long> GetActivePersonsCollaboration(Utility Utility, string apiurl, List<long> personId)
        {
            try
            {
                List<long> response = new List<long>();
                var url = apiurl + PCISEnum.APIurl.GetActivePersonCollaboration;
                var result = Utility.RestApiCall(url, false, false, PCISEnum.APIMethodType.PostRequest, null, personId.ToJSON());

                if (!string.IsNullOrEmpty(result))
                {
                    var responseDetails = JsonConvert.DeserializeObject<ActivePersonResponseDTO>(result);
                    response = responseDetails.result.PersonIds;
                }
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static void ProcessTriggerStatusUpdate(Utility Utility, string apiurl, ILogger log)
        {

            try
            {
                int limit = PCISEnum.Limits.StatusLimit;
                int count = PCISEnum.Limits.StatusCount;
                log.LogInformation($"C# StatusUpdateProcess : GetActivePersons Start");
                List<long> personIDs = GetActivePersons(Utility, apiurl);
                log.LogInformation($"C# StatusUpdateProcess : GetActivePersons End");
                log.LogInformation($"C# StatusUpdateProcess : GetActivePersons count {personIDs.Count}");

                if (personIDs != null && personIDs.Count > 0)
                {
                    count = personIDs.Count();
                    for (int i = 0; count > 0 && i <= count / limit; i++)
                    {
                        List<long> idList = new List<long>();
                        idList = personIDs.Skip(i * limit).Take(limit).ToList();
                        List<long> activeCollaboration = new List<long>();
                        if (idList != null && idList.Count > 0)
                        {
                            var PersonIds = GetActivePersonsCollaboration(Utility, apiurl, idList);
                            log.LogInformation($"C# StatusUpdateProcess : Person count to update count {PersonIds.Count} PersonIDs : {PersonIds.ToJSON()}");

                            if (PersonIds.Count > 0)
                                UpdateIsActiveForPerson(Utility, apiurl, PersonIds);
                            else
                                log.LogInformation($"C# StatusUpdateProcess : nothing to process");

                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
