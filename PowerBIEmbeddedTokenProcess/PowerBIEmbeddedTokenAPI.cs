using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PowerBIEmbeddedTokenAPI.Services;
using PowerBIEmbeddedTokenAPI.DTO;

namespace PowerBIEmbeddedTokenAPI
{
    public static class PowerBIEmbeddedTokenAPI
    {
        /// <summary>
        /// PowerBIEmbeddedTokenAPI.
        /// This Azure function act as an API to return the token for accessing Power Bi reports to be embebded on UI.
        /// It expects a GET with query string "reportid","workspaceid"  
        /// or a POST with body as json {"reportId":"{{reportid}},"workspaceId":"{{workspaceId}}"}
        /// Returns a JSON having EmbedToken,EmbedUrl,ReportId
        /// </summary> 
        /// <param name="req"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName("PowerBIEmbeddedTokenAPI")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            try
            {
                log.LogInformation("Power BI Report Token Requested");

                string reportId = req.Query["reportid"];
                string workspaceId = req.Query["workspaceid"];

                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                dynamic data = JsonConvert.DeserializeObject(requestBody);
                reportId = reportId ?? data?.reportId;
                workspaceId = workspaceId ?? data?.workspaceId;

                log.LogInformation("reportId: " + reportId);
                log.LogInformation("workspaceId: " + workspaceId);

                Guid reportGuid, workspaceGuid;
                if (!Guid.TryParse(reportId, out reportGuid))
                {
                    return (ActionResult)new BadRequestObjectResult("Report Id is empty or null.");
                }
                if (!Guid.TryParse(workspaceId, out workspaceGuid))
                {
                    return (ActionResult)new BadRequestObjectResult("Workspace Id is empty or null.");
                }
                string username = null;
                string roles = null;

                IEmbedService m_embedService = new EmbedService();
                var embedResult = await m_embedService.EmbedReport(username, roles, reportGuid, workspaceGuid);

                if (embedResult)
                {
                    ResultJson resultJson = new ResultJson()
                    {
                        EmbedToken = m_embedService.EmbedConfig.EmbedToken.Token,
                        EmbedUrl = m_embedService.EmbedConfig.EmbedUrl,
                        ReportId = reportGuid
                    };

                    string resultString = JsonConvert.SerializeObject(resultJson);

                    return (ActionResult)new OkObjectResult(resultString);
                }
                else
                {
                    string errorMsg = @$"Could not generate a token. ErrorMessage : '{m_embedService.EmbedConfig.ErrorMessage}'";
                    return (ActionResult)new BadRequestObjectResult(errorMsg);
                }
            }
            catch (Exception ex)
            {
                log.LogInformation("Exception: " + ex.ToString());
                return (ActionResult)new BadRequestObjectResult("An Exception has occurred.");
            }
        }
    }
}
