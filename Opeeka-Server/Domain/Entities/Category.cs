// -----------------------------------------------------------------------
// <copyright file="Category.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.ComponentModel.DataAnnotations;

namespace Opeeka.PICS.Domain.Entities
{
    public class Category
    {
        public int CategoryID { get; set; }
        public int CategoryFocusID { get; set; }
        public string Name { get; set; }
        public string? Abbrev { get; set; }
        public string? Description { get; set; }
        public int ListOrder { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UpdateUserID { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }

        public User UpdateUser { get; set; }
        public bool ShowAverage { get; set; }
    }
}
