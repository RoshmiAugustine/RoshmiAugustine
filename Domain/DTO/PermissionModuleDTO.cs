// -----------------------------------------------------------------------
// <copyright file="PermissionModuleDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System.Collections.Generic;

namespace Opeeka.PICS.Domain.DTO
{
    public class PermissionModuleDTO
    {
        public string ModuleName { get; set; }
        public List<PermissionDTO> Permissions { get; set; }
    }
}
