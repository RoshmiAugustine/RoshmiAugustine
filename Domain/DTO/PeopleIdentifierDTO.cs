// -----------------------------------------------------------------------
// <copyright file="PeopleInfoDTO.cs" company="Naico ITS">
// Copyright (c) Naico ITS. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Domain.DTO
{
    public class PeopleIdentifierDTO
    {
        public int IdentificationTypeID { get; set; }
        public string IdentifierType { get; set; }
        public string IdentifierID { get; set; }
        public long PersonIdentificationID { get; set; }
    }
}
