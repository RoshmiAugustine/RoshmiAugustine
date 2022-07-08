// -----------------------------------------------------------------------
// <copyright file="PersonLanguageDTO.cs" company="Naico ITS">
// Copyright (c) Naico ITS. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace Opeeka.PICS.Domain.DTO
{
    public class PersonLanguageDTO
    {

        public long PersonID { get; set; }
        public Guid PersonLanguageIndex { get; set; }
        public int LanguageID { get; set; }
        public bool IsPrimary { get; set; }
        public bool IsPreferred { get; set; }
    }
}
