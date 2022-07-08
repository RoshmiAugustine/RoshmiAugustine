// -----------------------------------------------------------------------
// <copyright file="CustomIPRateLimitMiddleware.cs" company="Naico ITS">
// Copyright (c) Naico ITS. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Api.Middleware
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AspNetCoreRateLimit;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Opeeka.PICS.Domain.Interfaces.Services;
    using Opeeka.PICS.Domain.Resources;
    using Opeeka.PICS.Infrastructure.Enums;
    using Opeeka.PICS.Infrastructure.Logging;

    /// <summary>
    /// CustomIPRateLimitMiddleware.
    /// </summary>
    public class CustomIPRateLimitMiddleware : IpRateLimitMiddleware
    {
        private readonly ILogger<CustomIPRateLimitMiddleware> _logger;
        private readonly IConfigurationService _configurations;
        private readonly IpRateLimitOptions _options;
        private readonly LocalizeService localize;

        public CustomIPRateLimitMiddleware(
            RequestDelegate next,
            IOptions<IpRateLimitOptions> options,
            IRateLimitCounterStore counterStore,
            IIpPolicyStore policyStore,
            IRateLimitConfiguration config,
            ILogger<CustomIPRateLimitMiddleware> logger,
            IConfigurationService configurations, LocalizeService localizeService)
        : base(next, options, counterStore, policyStore, config, logger)

        {
            this._options = options.Value;
            this._logger = logger;
            this.localize = localizeService;
            this._configurations = configurations;
            this.SetRulesOnOptions();
        }

        /// <summary>
        /// ReturnQuotaExceededResponse.
        /// </summary>
        /// <param name="httpContext">httpContext.</param>
        /// <param name="rule">rule.</param>
        /// <param name="retryAfter">retryAfter.</param>
        /// <returns>HttpResponse.</returns>
        public override Task ReturnQuotaExceededResponse(HttpContext httpContext, RateLimitRule rule, string retryAfter)
        {
            try
            {
                this._logger.LogInformation(MyLogEvents.TestItem, $"CustomIPRateLimitMiddleware/ReturnQuotaExceededResponse() Executed.");
                var message = new
                {
                    rule.Limit,
                    rule.Period,
                    retryAfter = retryAfter.ToString() + "sec",
                    responseStatusCode = PCISEnum.StatusCodes.APIRateLimitReached,
                    responseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.APIRateLimitReached), rule.Limit, retryAfter),
                };
                var response = message.ToJSON();
                httpContext.Response.Headers["Retry-After"] = retryAfter.ToString() + "sec";
                httpContext.Response.StatusCode = 200;
                httpContext.Response.ContentType = "application/json";
                return httpContext.Response.WriteAsync(this.localize.GetLocalizedHtmlString(response));
            }
            catch (Exception ex)
            {
                this._logger.LogError(MyLogEvents.TestItem, $"CustomIPRateLimitMiddleware/Initialize Rules. Exception: {ex.Message}");
                httpContext.Response.StatusCode = 200;
                var message = new
                {
                    responseStatusCode = PCISEnum.StatusCodes.APIRateLimitReached,
                    responseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.APIRateLimitReached), rule.Limit, retryAfter),
                };
                return httpContext.Response.WriteAsync(message.ToJSON());
            }
        }

        /// <summary>
        /// Apply Custom General Rules From DB Configuration.
        /// Limit = integer/double.
        /// Period = m/s/h.
        /// endpoint = {HTTP_Verb}:{PATH}.
        /// EnableEndpointRateLimiting = true (Appy to specific endpoint).
        /// </summary>
        private void SetRulesOnOptions()
        {
            try
            {
                this._logger.LogInformation(MyLogEvents.TestItem, $"CustomIPRateLimitMiddleware/SetRulesOnOptions-Start");
                this._options.EnableEndpointRateLimiting = true;
                var endpoint = this._configurations.GetConfigurationByName(PCISEnum.APIRateLimit.Endpoint);
                var limit = this._configurations.GetConfigurationByName(PCISEnum.APIRateLimit.Count);
                var period = this._configurations.GetConfigurationByName(PCISEnum.APIRateLimit.Period);
                if (this._options.GeneralRules == null)
                {
                    this._options.GeneralRules = new List<RateLimitRule>();
                    this._options.GeneralRules.Add(new RateLimitRule
                    {
                        Endpoint = endpoint?.Value.ToString(),
                        Limit = Convert.ToDouble(limit?.Value),
                        Period = period?.Value.ToString(),
                    });
                }

                this._logger.LogInformation(MyLogEvents.TestItem, $"CustomIPRateLimitMiddleware/SetRulesOnOptions-End.EndPoint: {endpoint?.Value.ToString()}.Limit: {Convert.ToDouble(limit?.Value)}.Period: {period?.Value.ToString()}");
            }
            catch (Exception ex)
            {
                this._logger.LogError(MyLogEvents.TestItem, $"CustomIPRateLimitMiddleware/Initialize Rules. Exception: {ex.Message}");
            }
        }
    }
}
