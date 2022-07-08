// -----------------------------------------------------------------------
// <copyright file="CRUDResponseDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Opeeka.PICS.Domain.DTO
{
    public class CRUDResponseDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }
        public int Id { get; set; }

        public DateTime? LastLogin { get; set; }
        public string Language { get; set; }

        public string AssesmentAutoSaveTime { get; set; }
        public string ApplicationTimeout { get; set; }

        public string AssesmentPageCount { get; set; }

        public Guid IndexId { get; set; }

        public long ResultId { get; set; }

    }
    public class UserTokenDetails
    {
        public long AgencyID { get; set; }
        public int UserID { get; set; }
        public string Role { get; set; }
    }
    public class AssessmentConfigurationDTO
    {
        public string ResponseStatus { get; set; }
        public int ResponseStatusCode { get; set; }
        public string AssesmentAutoSaveTime { get; set; }
        public string ApplicationTimeout { get; set; }
        public string AssesmentPageCount { get; set; }
        public string AssesmentFileTypes { get; set; }
    }

    public class SSOResponseDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }
        public string RedirectURL { get; set; }

    }
    public class SSOSettingsDTO
    {
        public string SSOBaserUrl { get; set; }
        public List<RedirectUrl> RedirectUrls { get; set; }
    }
    public class RedirectUrl
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
