// -----------------------------------------------------------------------
// <copyright file="UserRoleDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;

namespace Opeeka.PICS.Domain.DTO
{
    public class SystemRoleDTO
    {
        public int SystemRoleID { get; set; }
        public string Name { get; set; }
        public string Abbrev { get; set; }
        public bool IsExternal { get; set; }
        public int ListOrder { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UpdateUserID { get; set; }
        public int Weight { get; set; }
    }
}
