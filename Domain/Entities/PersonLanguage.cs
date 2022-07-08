// -----------------------------------------------------------------------
// <copyright file="PersonLanguage.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Domain.Entities
{
    public class PersonLanguage : BaseEntity
    {
        public long PersonID { get; set; }
        public int LanguageID { get; set; }
        public bool IsPrimary { get; set; }
        public bool IsPreferred { get; set; }

        public Language Language { get; set; }
        public Person Person { get; set; }
    }
}
