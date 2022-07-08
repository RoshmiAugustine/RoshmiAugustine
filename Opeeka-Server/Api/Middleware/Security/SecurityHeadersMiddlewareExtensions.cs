// -----------------------------------------------------------------------
// <copyright file="SecurityHeadersMiddlewareExtensions.cs" company="Naico ITS">
// Copyright (c) Naico ITS. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Api.Middleware.Security
{
    using Microsoft.AspNetCore.Builder;

    /// <summary>
    /// SecurityHeadersMiddlewareExtensions.
    /// </summary>
    public static class SecurityHeadersMiddlewareExtensions
    {
        /// <summary>
        /// UseSecurityHeadersMiddleware.
        /// </summary>
        /// <param name="app">IApplicationBuilder.</param>
        /// <param name="builder">SecurityHeadersBuilder.</param>
        /// <returns>ApplicationBuilder.</returns>
        public static IApplicationBuilder UseSecurityHeadersMiddleware(this IApplicationBuilder app, SecurityHeadersBuilder builder)
        {
            SecurityHeadersPolicy policy = builder.Build();
            return app.UseMiddleware<SecurityHeadersMiddleware>(policy);
        }
    }
}
