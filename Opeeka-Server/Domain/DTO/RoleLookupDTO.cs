// -----------------------------------------------------------------------
// <copyright file="RoleLookupDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Domain.DTO
{
    public class RoleLookupDTO
    {
        public int SystemRoleID { get; set; }
        public string Name { get; set; }
        public int ListOrder { get; set; }
        public bool IsExternal { get; set; }

    }
}
