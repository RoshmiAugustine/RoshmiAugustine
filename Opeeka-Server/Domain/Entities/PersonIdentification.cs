// -----------------------------------------------------------------------
// <copyright file="PersonIdentification.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace Opeeka.PICS.Domain.Entities
{
    public class PersonIdentification : BaseEntity
    {
        public long PersonIdentificationID { get; set; }
        public long PersonID { get; set; }
        public int IdentificationTypeID { get; set; }
        public string IdentificationNumber { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UpdateUserID { get; set; }

        public IdentificationType IdentificationType { get; set; }
        public Person Person { get; set; }
        public User UpdateUser { get; set; }
    }
}
