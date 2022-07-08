// -----------------------------------------------------------------------
// <copyright file="SecurityHeadersMiddleware.cs" company="Naico ITS">
// Copyright (c) Naico ITS. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Api.Middleware.Security
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// SecurityHeadersMiddleware.
    /// </summary>
    public class SecurityHeadersMiddleware
    {
        /// <summary>
        /// next.
        /// </summary>
        private readonly RequestDelegate next;

        /// <summary>
        /// policy.
        /// </summary>
        private readonly SecurityHeadersPolicy policy;

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityHeadersMiddleware"/> class.
        /// </summary>
        /// <param name="next">RequestDelegate.</param>
        /// <param name="policy">SecurityHeadersPolicy.</param>
        public SecurityHeadersMiddleware(RequestDelegate next, SecurityHeadersPolicy policy)
        {
            this.next = next;
            this.policy = policy;
        }

        /// <summary>
        /// Invoke.
        /// </summary>
        /// <param name="context">HttpContext.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        public async Task Invoke(HttpContext context)
        {
            IHeaderDictionary headers = context.Response.Headers;

            foreach (var headerValuePair in this.policy.SetHeaders)
            {
                headers[headerValuePair.Key] = headerValuePair.Value;
            }

            foreach (var header in this.policy.RemoveHeaders)
            {
                headers.Remove(header);
            }

            await this.next(context);
        }
    }
}
