// -----------------------------------------------------------------------
// <copyright file="ContentSecurityConstants.cs" company="Naico ITS">
// Copyright (c) Naico ITS. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Api.Middleware.Security.Constants
{
    /// <summary>
    /// ContentSecurityConstants.
    /// </summary>
    public static class ContentSecurityConstants
    {
        /// <summary>
        /// Header value for Content-Security-Policy.
        /// </summary>
        public static readonly string Header = "Content-Security-Policy";

        /// <summary>
        /// Default.
        /// </summary>
        public static readonly string Default = "default-src 'self'";

        /// <summary>
        /// script.
        /// </summary>
        public static readonly string Script = "script-src 'self'";

        /// <summary>
        /// object.
        /// </summary>
        public static readonly string Object = "object-src 'self'";

        /// <summary>
        /// Style.
        /// </summary>
        public static readonly string Style = "style-src 'self'";
    }
}
