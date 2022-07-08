// -----------------------------------------------------------------------
// <copyright file="SystemRole.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;

namespace Opeeka.PICS.Domain.Entities
{
    public class SystemRole : BaseEntity
    {
        public int SystemRoleID { get; set; }
        public string Name { get; set; }
        public string? Abbrev { get; set; }
        public bool IsExternal { get; set; }
        public int ListOrder { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime UpdateDate { get; set; }
        public int? UpdateUserID { get; set; }
        public int? Weight { get; set; }

        public User UpdateUser { get; set; }
    }
}
