// -----------------------------------------------------------------------
// <copyright file="PersonScreeningStatus.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;

namespace Opeeka.PICS.Domain.Entities
{
    public class PersonScreeningStatus : BaseEntity
    {
        public int PersonScreeningStatusId { get; set; }
        public string Name { get; set; }
        public int ListOrder { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UpdateUserId { get; set; }

        public User UpdateUser { get; set; }
    }
}
