// -----------------------------------------------------------------------
// <copyright file="UserProfileDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace Opeeka.PICS.Domain.DTO
{
    public class UserProfileDTO
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone1 { get; set; }

        public string Phone2 { get; set; }

        public string Title { get; set; }

        public string Address { get; set; }

        public int UserProfileID { get; set; }

        public int UserID { get; set; }

        public long ImageFileID { get; set; }

        public byte[] ImageByteArray { get; set; }

        public string AzureFileName { get; set; }

        public int HelperTitleID { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public Guid HelperIndex { get; set; }

        public int CountryID { get; set; }

        public string CountryName { get; set; }
        public long AgencyID { get; set; }
        public string AgencyName { get; set; }
        public string AgencyAbbrev { get; set; }
    }
}
