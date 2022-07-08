// -----------------------------------------------------------------------
// <copyright file="PermittedCrossDomainPolicyConstants.cs" company="Naico ITS">
// Copyright (c) Naico ITS. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Api.Middleware.Security.Constants
{
    /// <summary>
    /// PermittedCrossDomainPolicyConstants.
    /// </summary>
    public static class PermittedCrossDomainPolicyConstants
    {
        /// <summary>
        /// Header value for Permissions Policy.
        /// </summary>
        public static readonly string Header = "X-Permitted-Cross-Domain-Policies";

        /// <summary>
        /// value.
        /// </summary>
        public static readonly string Value = "none";
    }
}
