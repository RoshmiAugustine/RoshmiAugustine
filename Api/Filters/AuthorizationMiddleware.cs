// -----------------------------------------------------------------------
// <copyright file="AuthorizationMiddleware.cs" company="Naico ITS">
// Copyright (c) Naico ITS. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Api.Filters
{
    using System;
    using System.IO;
    using System.Net.Http.Headers;
    using System.Security.Claims;
    using System.Text;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Extensions;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;
    using Opeeka.PICS.Domain.Interfaces.Repositories;
    using Opeeka.PICS.Domain.Interfaces.Services;
    using Opeeka.PICS.Infrastructure.Enums;
    using Serilog;

    /// <summary>
    /// The default middleware to authorize a request.
    /// </summary>
    public class AuthorizationMiddleware : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private static readonly Serilog.ILogger logger = Log.ForContext<AuthorizationMiddleware>();
        private readonly IJWTTokenService JWTTokenService;
        private readonly IConfigurationService configurationService;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IAgencyRepository agencyRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationMiddleware"/> class.
        /// </summary>
        /// <param name="options">The options used by the AuthenticationHandler.</param>
        /// <param name="logger">The registered logger.</param>
        /// <param name="encoder">The URL character encoder.</param>
        /// <param name="clock">The system clock.</param>
        /// <param name="jwtTokenService">The token service.</param>
        public AuthorizationMiddleware(IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IJWTTokenService jwtTokenService,
            IConfigurationService configurations,
            IHttpContextAccessor httpContext,
            IAgencyRepository agencyRepository) : base(options, logger, encoder, clock)
        {
            JWTTokenService = jwtTokenService;
            configurationService = configurations;
            httpContextAccessor = httpContext;
            agencyRepository = agencyRepository;
        }

        /// <summary>
        /// Determines whether the system received an authenticated request or not.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            string tenantId = string.Empty;
            string personIndex = string.Empty;
            string collaborationSharingIndex = string.Empty;
            string agencySharingIndex = string.Empty;
            bool isActivePerson = false;
            int agencyId = 0;

            if (Request.Headers.ContainsKey(PCISEnum.TokenHeaders.agencyId))
                agencyId = Convert.ToInt32(Request.Headers[PCISEnum.TokenHeaders.agencyId]);

            if (Request.Headers.ContainsKey(PCISEnum.Parameters.tenantId))
                tenantId = Request.Headers[PCISEnum.Parameters.tenantId].ToString();

            if (Request.Headers.ContainsKey(PCISEnum.TokenHeaders.tenantUrl))
            {
                if (agencyId == 0 && tenantId != string.Empty)
                {
                    agencyId = Convert.ToInt32(agencyRepository.GetAgencyDetailsByAbbrev(tenantId).Result.AgencyID);
                }
                var agencyConfiguration = configurationService.GetConfigurationByName(PCISEnum.ConfigurationParameters.Domain, agencyId);
                if (agencyConfiguration != null && !string.IsNullOrEmpty(agencyConfiguration.Value))
                {
                    httpContextAccessor.HttpContext.Items.Add(PCISEnum.ConfigurationParameters.Culture, agencyConfiguration.Value);
                }
            }

            if (Request.Headers.ContainsKey(PCISEnum.TokenHeaders.timeZone))
            {
                httpContextAccessor.HttpContext.Items.Add(PCISEnum.TokenHeaders.timeZone, Request.Headers[PCISEnum.TokenHeaders.timeZone].ToString());
            }

            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return Task.FromResult(AuthenticateResult.Fail("Missing Authorization Header"));
            }

            if (Request.Headers.ContainsKey(PCISEnum.Parameters.tenantId))
            {
                tenantId = Request.Headers[PCISEnum.Parameters.tenantId].ToString();
            }

            if (Request.Headers.ContainsKey(PCISEnum.Parameters.personIndex))
            {
                personIndex = Request.Headers[PCISEnum.Parameters.personIndex].ToString();
            }

            if (Request.Headers.ContainsKey(PCISEnum.Parameters.CollaborationSharingIndex))
            {
                collaborationSharingIndex = Request.Headers[PCISEnum.Parameters.CollaborationSharingIndex].ToString();
            }

            if (Request.Headers.ContainsKey(PCISEnum.Parameters.AgencySharingIndex))
            {
                agencySharingIndex = Request.Headers[PCISEnum.Parameters.AgencySharingIndex].ToString();
            }

            if (Request.Headers.ContainsKey(PCISEnum.Parameters.IsActivePerson))
            {
                isActivePerson = Convert.ToBoolean(Request.Headers[PCISEnum.Parameters.IsActivePerson]);
            }

            ClaimsPrincipal principal;
            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);

                // Request.RouteValues.TryGetValue("tenantId", out var value);
                // string tenantId = value != null ? value.ToString() : string.Empty;
                if (!authHeader.Scheme.Equals("bearer", StringComparison.OrdinalIgnoreCase) || string.IsNullOrEmpty(authHeader.Parameter))
                {
                    return Task.FromResult(AuthenticateResult.Fail("Invalid Authorization Header"));
                }
                principal = JWTTokenService.GetPrincipal(authHeader.Parameter, tenantId, agencyId, personIndex, collaborationSharingIndex, agencySharingIndex, isActivePerson);
            }
            catch (Exception e)
            {
                using var reader = new StreamReader(Request.Body, Encoding.UTF8);
                var requestBody = reader.ReadToEndAsync().Result;
                logger.Error(e, $"Error getting claims principal from JWT for request with Schema: {Request.Scheme} Host: {Request.Host} Path: {Request.Path} QueryString: {Request.QueryString} RequestBody: {requestBody} DisplayURL: {Request.GetDisplayUrl()} Headers: {JsonConvert.SerializeObject(Request.Headers)}");
                return Task.FromResult(AuthenticateResult.Fail("Invalid Authorization Header"));
            }

            if (principal == null)
            {
                using var reader = new StreamReader(Request.Body, Encoding.UTF8);
                var requestBody = reader.ReadToEndAsync().Result;
                logger.Warning($"Unable to get claims principal from JWT for request with Schema: {Request.Scheme} Host: {Request.Host} Path: {Request.Path} QueryString: {Request.QueryString} RequestBody: {requestBody} DisplayURL: {Request.GetDisplayUrl()} Headers: {JsonConvert.SerializeObject(Request.Headers)}");
                return Task.FromResult(AuthenticateResult.Fail("Invalid Authorization Header"));
            }

            var claimsIdentity = new ClaimsIdentity(principal.Claims, "AuthenticationTypes.Federation");
            Request.Headers[PCISEnum.Parameters.tenantId] = claimsIdentity.FindFirst(PCISEnum.TokenClaims.AgencyAbbrev) != null ? claimsIdentity.FindFirst(PCISEnum.TokenClaims.AgencyAbbrev).Value : string.Empty;
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
}
