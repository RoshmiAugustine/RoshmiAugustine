// -----------------------------------------------------------------------
// <copyright file="SecurityHeadersPolicy.cs" company="Naico ITS">
// Copyright (c) Naico ITS. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Api.Middleware.Security
{
    using System.Collections.Generic;

    /// <summary>
    /// SecurityHeadersPolicy.
    /// </summary>
    public class SecurityHeadersPolicy
    {
        /// <summary>
        /// Gets setHeaders.
        /// </summary>
        public IDictionary<string, string> SetHeaders { get; }
     = new Dictionary<string, string>();

        /// <summary>
        /// Gets removeHeaders.
        /// </summary>
        public ISet<string> RemoveHeaders { get; }
            = new HashSet<string>();
    }
}
