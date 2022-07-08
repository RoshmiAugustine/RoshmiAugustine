// -----------------------------------------------------------------------
// <copyright file="ItemResponseBehavior.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;

namespace Opeeka.PICS.Domain.Entities
{
    public class ItemResponseBehavior : BaseEntity
    {
        public int ItemResponseBehaviorID { get; set; }
        public int ItemResponseTypeID { get; set; }
        public string Name { get; set; }
        public string? Abbrev { get; set; }
        public string? Description { get; set; }
        public int ListOrder { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UpdateUserID { get; set; }

        public ItemResponseType ItemResponseType { get; set; }
        public User UpdateUser { get; set; }

    }
}
