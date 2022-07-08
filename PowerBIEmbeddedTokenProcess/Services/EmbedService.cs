using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.PowerBI.Api;
using Microsoft.PowerBI.Api.Models;
using Microsoft.Rest;
using PowerBIEmbeddedTokenAPI.DTO;

namespace PowerBIEmbeddedTokenAPI.Services
{

    public class EmbedService : IEmbedService
    {
        private static readonly string authorityUrl = System.Environment.GetEnvironmentVariable("AuthorityUrl");
        private static readonly string resourceUrl = System.Environment.GetEnvironmentVariable("ResourceUrl");
        private static readonly string applicationId = System.Environment.GetEnvironmentVariable("ApplicationId");
        private static readonly string apiUrl = System.Environment.GetEnvironmentVariable("PowerBiApiUrl");
        private static readonly string appSecret = System.Environment.GetEnvironmentVariable("ApplicationSecret");
        private static readonly string tenantId = System.Environment.GetEnvironmentVariable("TenantId");

        public EmbedConfig EmbedConfig
        {
            get { return m_embedConfig; }
        }

        private EmbedConfig m_embedConfig;
        private TokenCredentials m_tokenCredentials;

        public EmbedService()
        {
            m_tokenCredentials = null;
            m_embedConfig = new EmbedConfig();
        }

        public async Task<bool> EmbedReport(string username, string roles, Guid reportId, Guid workspaceId)
        {
            //FindErrors in Configurations
            var error = GetConfigurationErrors();
            if (error != null)
            {
                m_embedConfig.ErrorMessage = error;
                return false;
            }

            // Get token credentials for user
            var getCredentialsResult = await DoAuthenticationToGetTokenCredentials();
            if (!getCredentialsResult)
            {
                // The error message from GetTokenCredentials set to m_embedConfig
                return false;
            }

            try
            {
                // Create a Power BI Client object. It will be used to call Power BI APIs.
                using (var client = new PowerBIClient(new Uri(apiUrl), m_tokenCredentials))
                {
                    // Get a list of reports.
                    var reports = await client.Reports.GetReportsInGroupAsync(workspaceId);

                    // No reports retrieved for the given workspace.
                    if (reports.Value.Count == 0)
                    {
                        m_embedConfig.ErrorMessage = "No reports were found in the workspace";
                        return false;
                    }

                    Report report;
                    if (string.IsNullOrWhiteSpace(reportId.ToString()))
                    {
                        // Get the first report in the workspace.
                        report = reports.Value.FirstOrDefault();
                    }
                    else
                    {
                        report = reports.Value.FirstOrDefault(r => r.Id.Equals(reportId));
                    }

                    if (report == null)
                    {
                        m_embedConfig.ErrorMessage = "No report with the given ID was found in the workspace. Make sure ReportId is valid.";
                        return false;
                    }
                    
                    GenerateTokenRequest generateTokenRequestParameters;
                    // This is how you create embed token with effective identities
                    if (!string.IsNullOrWhiteSpace(username))
                    {
                        var datasets = await client.Datasets.GetDatasetInGroupAsync(workspaceId, report?.DatasetId);
                        m_embedConfig.IsEffectiveIdentityRequired = datasets?.IsEffectiveIdentityRequired;
                        m_embedConfig.IsEffectiveIdentityRolesRequired = datasets?.IsEffectiveIdentityRolesRequired;
                        var rls = new EffectiveIdentity(username, new List<string> { report?.DatasetId });
                        if (!string.IsNullOrWhiteSpace(roles))
                        {
                            var rolesList = new List<string>();
                            rolesList.AddRange(roles.Split(','));
                            rls.Roles = rolesList;
                        }
                        // Generate Embed Token with effective identities.
                        generateTokenRequestParameters = new GenerateTokenRequest(accessLevel: "view", identities: new List<EffectiveIdentity> { rls });
                    }
                    else
                    {
                        // Generate Embed Token for reports without effective identities.
                        generateTokenRequestParameters = new GenerateTokenRequest(accessLevel: "view");
                    }

                    var tokenResponse = await client.Reports.GenerateTokenInGroupAsync(workspaceId, report.Id, generateTokenRequestParameters);

                    if (tokenResponse == null)
                    {
                        m_embedConfig.ErrorMessage = "Failed to generate embed token.";
                        return false;
                    }

                    // Generate Embed Configuration.
                    m_embedConfig.EmbedToken = tokenResponse;
                    m_embedConfig.EmbedUrl = report.EmbedUrl;
                    m_embedConfig.Id = report.Id.ToString();

                }
            }
            catch (HttpOperationException exc)
            {
                m_embedConfig.ErrorMessage = string.Format("Status: {0} ({1})\r\nResponse: {2}\r\nRequestId: {3}", exc.Response.StatusCode, (int)exc.Response.StatusCode, exc.Response.Content, exc.Response.Headers["RequestId"].FirstOrDefault());
                return false;
            }

            return true;
        }

        /// <summary>
        /// Check if config embed parameters have valid values.
        /// </summary>
        /// <returns>Null if config parameters are valid, otherwise returns specific error string.</returns>
        private string GetConfigurationErrors()
        {
            // Application Id must have a value.
            if (string.IsNullOrWhiteSpace(applicationId))
            {
                return "ApplicationId is empty. please register your application as Native app in https://dev.powerbi.com/apps and fill client Id in web.config.";
            }

            // Application Id must be a Guid object.
            Guid result;
            if (!Guid.TryParse(applicationId, out result))
            {
                return "ApplicationId must be a Guid object. please register your application as Native app in https://dev.powerbi.com/apps and fill application Id in web.config.";
            }

            if (string.IsNullOrWhiteSpace(appSecret))
            {
                return "ApplicationSecret is empty. please register your application as Web app and fill appSecret in web.config.";
            }

            // Must fill tenant Id
            if (string.IsNullOrWhiteSpace(tenantId))
            {
                return "Invalid Tenant. Please fill Tenant ID in Tenant under web.config";
            }       

            return null;
        }

        /// <summary>
        /// DoAuthenticationToGetTokenCredentials
        /// Authenticate to ServicePriciple inside the Azure tenant and retrieve the token through which power bi reports are accessed .
        /// </summary>
        /// <returns></returns>
        private async Task<bool> DoAuthenticationToGetTokenCredentials()
        {
            // Authenticate using created credentials
            AuthenticationResult authenticationResult = null;
            try
            {
                // For app only authentication, we need the specific tenant id in the authority url
                var tenantSpecificURL = authorityUrl.Replace("common", tenantId);
                var authenticationContext = new AuthenticationContext(tenantSpecificURL);

                // Authentication using app credentials
                var credential = new ClientCredential(applicationId, appSecret);
                authenticationResult = await authenticationContext.AcquireTokenAsync(resourceUrl, credential);

            }
            catch (AggregateException exc)
            {
                m_embedConfig.ErrorMessage = exc.InnerException.Message;
                return false;
            }

            if (authenticationResult == null)
            {
                m_embedConfig.ErrorMessage = "Authentication Failed.";
                return false;
            }

            m_tokenCredentials = new TokenCredentials(authenticationResult.AccessToken, "Bearer");
            return true;
        }
    }
}
