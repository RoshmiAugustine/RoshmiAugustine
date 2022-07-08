// -----------------------------------------------------------------------
// <copyright file="PermissionsPolicyConstants.cs" company="Naico ITS">
// Copyright (c) Naico ITS. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Api.Middleware.Security.Constants
{
    /// <summary>
    /// PermissionsPolicyConstants.
    /// </summary>
    public static class PermissionsPolicyConstants
    {
        /// <summary>
        /// Header value for Permissions Policy.
        /// </summary>
        public static readonly string Header = "Permissions-Policy";

        /// <summary>
        /// Default.
        /// </summary>
        public static readonly string Default = "camera=(), geolocation=()";

        /// <summary>
        /// value.
        /// </summary>
        public static readonly string All = "accelerometer=(), camera=(), geolocation=(), gyroscope=(), magnetometer=(), microphone=(), payment=(), usb=()";
    }
}
