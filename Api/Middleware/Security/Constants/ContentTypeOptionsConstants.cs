// -----------------------------------------------------------------------
// <copyright file="ContentTypeOptionsConstants.cs" company="Naico ITS">
// Copyright (c) Naico ITS. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Api.Middleware.Security.Constants
{
    /// <summary>
    /// X-Content-Type-Options-related constants.
    /// </summary>
    public static class ContentTypeOptionsConstants
    {
        /// <summary>
        /// Header value for X-Content-Type-Options.
        /// </summary>
        public static readonly string Header = "X-Content-Type-Options";

        /// <summary>
        /// Disables content sniffing.
        /// </summary>
        public static readonly string NoSniff = "nosniff";

    }
}