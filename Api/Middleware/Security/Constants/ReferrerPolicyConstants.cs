// -----------------------------------------------------------------------
// <copyright file="ReferrerPolicyConstants.cs" company="Naico ITS">
// Copyright (c) Naico ITS. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Api.Middleware.Security.Constants
{
    /// <summary>
    /// ReferrerPolicyConstants.
    /// </summary>
    public static class ReferrerPolicyConstants
    {
        /// <summary>
        /// Header value for Permissions Policy.
        /// </summary>
        public static readonly string Header = "Referrer-Policy";

        /// <summary>
        /// value.
        /// </summary>
        public static readonly string Value = "no-referrer";
    }
}
