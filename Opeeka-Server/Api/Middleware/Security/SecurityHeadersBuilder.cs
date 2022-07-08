// -----------------------------------------------------------------------
// <copyright file="SecurityHeadersBuilder.cs" company="Naico ITS">
// Copyright (c) Naico ITS. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Api.Middleware.Security
{
    using System;
    using Opeeka.PICS.Api.Middleware.Security.Constants;

    /// <summary>
    /// SecurityHeadersBuilder.
    /// </summary>
    public class SecurityHeadersBuilder
    {
        /// <summary>
        /// The number of seconds in one year.
        /// </summary>
        public const int OneYearInSeconds = 60 * 60 * 24 * 365;

        /// <summary>
        /// policy.
        /// </summary>
        private readonly SecurityHeadersPolicy policy = new SecurityHeadersPolicy();

        /// <summary>
        /// Add default headers in accordance with most secure approach.
        /// </summary>
        /// <returns>SecurityHeadersBuilder.</returns>
        public SecurityHeadersBuilder AddDefaultSecurePolicy()
        {
            this.AddFrameOptionsDeny();
            this.AddXssProtectionBlock();
            this.AddContentTypeOptionsNoSniff();
            this.AddStrictTransportSecurityMaxAge();
            this.AddContentSecurityPolicy();
            this.AddPermissionsPolicy();
            this.AddReferrerPolicy();
            this.AddPermittedCrossDomainPolicy();
            this.RemoveServerHeader();

            return this;
        }

        /// <summary>
        /// /// Add X-Frame-Options DENY to all requests.
        /// The page cannot be displayed in a frame, regardless of the site attempting to do so.
        /// </summary>
        /// <returns>SecurityHeadersBuilder.</returns>
        public SecurityHeadersBuilder AddFrameOptionsDeny()
        {
            this.policy.SetHeaders[FrameOptionsConstants.Header] = FrameOptionsConstants.Deny;
            return this;
        }

        /// <summary>
        /// Add X-Frame-Options SAMEORIGIN to all requests.
        /// The page can only be displayed in a frame on the same origin as the page itself.
        /// </summary>
        /// <returns>SecurityHeadersBuilder.</returns>
        public SecurityHeadersBuilder AddFrameOptionsSameOrigin()
        {
            this.policy.SetHeaders[FrameOptionsConstants.Header] = FrameOptionsConstants.SameOrigin;
            return this;
        }

        /// <summary>
        /// Add X-Frame-Options ALLOW-FROM {uri} to all requests, where the uri is provided
        /// The page can only be displayed in a frame on the specified origin.
        /// </summary>
        /// <param name="uri">The uri of the origin in which the page may be displayed in a frame.</param>
        /// <returns>SecurityHeadersBuilder.</returns>
        public SecurityHeadersBuilder AddFrameOptionsSameOrigin(string uri)
        {
            this.policy.SetHeaders[FrameOptionsConstants.Header] = string.Format(FrameOptionsConstants.AllowFromUri, uri);
            return this;
        }

        /// <summary>
        /// Add X-XSS-Protection 1 to all requests.
        /// Enables the XSS Protections.
        /// </summary>
        /// <returns>SecurityHeadersBuilder.</returns>
        public SecurityHeadersBuilder AddXssProtectionEnabled()
        {
            this.policy.SetHeaders[XssProtectionConstants.Header] = XssProtectionConstants.Enabled;
            return this;
        }

        /// <summary>
        /// Add X-XSS-Protection 0 to all requests.
        /// Disables the XSS Protections offered by the user-agent.
        /// </summary>
        /// <returns>SecurityHeadersBuilder.</returns>
        public SecurityHeadersBuilder AddXssProtectionDisabled()
        {
            this.policy.SetHeaders[XssProtectionConstants.Header] = XssProtectionConstants.Disabled;
            return this;
        }

        /// <summary>
        /// Add X-XSS-Protection 1; mode=block to all requests.
        /// Enables XSS protections and instructs the user-agent to block the response in the event that script has been inserted from user input, instead of sanitizing.
        /// </summary>
        /// <returns>SecurityHeadersBuilder.</returns>
        public SecurityHeadersBuilder AddXssProtectionBlock()
        {
            this.policy.SetHeaders[XssProtectionConstants.Header] = XssProtectionConstants.Block;
            return this;
        }

        /// <summary>
        /// Add X-XSS-Protection 1; report=http://site.com/report to all requests.
        /// A partially supported directive that tells the user-agent to report potential XSS attacks to a single URL. Data will be POST'd to the report URL in JSON format.
        /// </summary>
        /// <returns>SecurityHeadersBuilder.</returns>
        public SecurityHeadersBuilder AddXssProtectionReport(string reportUrl)
        {
            this.policy.SetHeaders[XssProtectionConstants.Header] =
                string.Format(XssProtectionConstants.Report, reportUrl);
            return this;
        }

        /// <summary>
        /// Add Strict-Transport-Security max-age to all requests.
        /// Tells the user-agent to cache the domain in the STS list for the number of seconds provided.
        /// </summary>
        /// <returns>SecurityHeadersBuilder.</returns>
        public SecurityHeadersBuilder AddStrictTransportSecurityMaxAge(int maxAge = OneYearInSeconds)
        {
            this.policy.SetHeaders[StrictTransportSecurityConstants.Header] =
                string.Format(StrictTransportSecurityConstants.MaxAge, maxAge);
            return this;
        }

        /// <summary>
        /// Add Strict-Transport-Security max-age includeSubDomains to all requests.
        /// Tells the user-agent to cache the domain in the STS list for the number of seconds provided and include any sub-domains.
        /// </summary>
        /// <returns>SecurityHeadersBuilder.</returns>
        public SecurityHeadersBuilder AddStrictTransportSecurityMaxAgeIncludeSubDomains(int maxAge = OneYearInSeconds)
        {
            this.policy.SetHeaders[StrictTransportSecurityConstants.Header] =
                string.Format(StrictTransportSecurityConstants.MaxAgeIncludeSubdomains, maxAge);
            return this;
        }

        /// <summary>
        /// Add Strict-Transport-Security max-age=0 to all requests.
        /// Tells the user-agent to remove, or not cache the host in the STS cache.
        /// </summary>
        /// <returns>SecurityHeadersBuilder.</returns>
        public SecurityHeadersBuilder AddStrictTransportSecurityNoCache()
        {
            this.policy.SetHeaders[StrictTransportSecurityConstants.Header] =
                StrictTransportSecurityConstants.NoCache;
            return this;
        }

        /// <summary>
        /// Add X-Content-Type-Options nosniff to all requests.
        /// Can be set to protect against MIME type confusion attacks.
        /// </summary>
        /// <returns>SecurityHeadersBuilder.</returns>
        public SecurityHeadersBuilder AddContentTypeOptionsNoSniff()
        {
            this.policy.SetHeaders[ContentTypeOptionsConstants.Header] = ContentTypeOptionsConstants.NoSniff;
            return this;
        }

        /// <summary>
        /// AddContentSecurityPolicy.
        /// </summary>
        /// <returns>SecurityHeadersBuilder.</returns>
        public SecurityHeadersBuilder AddContentSecurityPolicy()
        {
            this.policy.SetHeaders[ContentSecurityConstants.Header] = ContentSecurityConstants.Default;
            return this;
        }

        /// <summary>
        /// AddPermissionsPolicy.
        /// </summary>
        /// <returns>SecurityHeadersBuilder.</returns>
        public SecurityHeadersBuilder AddPermissionsPolicy()
        {
            this.policy.SetHeaders[PermissionsPolicyConstants.Header] = PermissionsPolicyConstants.Default;
            return this;
        }

        /// <summary>
        /// AddReferrerPolicy.
        /// </summary>
        /// <returns>SecurityHeadersBuilder.</returns>
        public SecurityHeadersBuilder AddReferrerPolicy()
        {
            this.policy.SetHeaders[ReferrerPolicyConstants.Header] = ReferrerPolicyConstants.Value;
            return this;
        }

        /// <summary>
        /// AddPermittedCrossDomainPolicy.
        /// </summary>
        /// <returns>SecurityHeadersBuilder.</returns>
        public SecurityHeadersBuilder AddPermittedCrossDomainPolicy()
        {
            this.policy.SetHeaders[PermittedCrossDomainPolicyConstants.Header] = PermittedCrossDomainPolicyConstants.Value;
            return this;
        }

        /// <summary>
        /// Removes the Server header from all responses.
        /// </summary>
        /// <returns>SecurityHeadersBuilder.</returns>
        public SecurityHeadersBuilder RemoveServerHeader()
        {
            this.policy.RemoveHeaders.Add(ServerConstants.Header);
            return this;
        }

        /// <summary>
        /// Adds a custom header to all requests.
        /// </summary>
        /// <param name="header">The header name.</param>
        /// <param name="value">The value for the header.</param>
        /// <returns>SecurityHeadersBuilder.</returns>
        public SecurityHeadersBuilder AddCustomHeader(string header, string value)
        {
            if (string.IsNullOrEmpty(header))
            {
                throw new ArgumentNullException(nameof(header));
            }

            this.policy.SetHeaders[header] = value;
            return this;
        }

        /// <summary>
        /// Remove a header from all requests.
        /// </summary>
        /// <param name="header">The to remove.</param>
        /// <returns>SecurityHeadersBuilder.</returns>
        public SecurityHeadersBuilder RemoveHeader(string header)
        {
            if (string.IsNullOrEmpty(header))
            {
                throw new ArgumentNullException(nameof(header));
            }

            this.policy.RemoveHeaders.Add(header);
            return this;
        }

        /// <summary>
        /// Builds a new <see cref="SecurityHeadersPolicy"/> using the entries added.
        /// </summary>
        /// <returns>The constructed <see cref="SecurityHeadersPolicy"/>.</returns>
        public SecurityHeadersPolicy Build()
        {
            return this.policy;
        }
    }
}
