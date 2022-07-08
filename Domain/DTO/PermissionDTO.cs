// -----------------------------------------------------------------------
// <copyright file="PermissionDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System.Collections.Generic;

namespace Opeeka.PICS.Domain.DTO
{
    public class PermissionDTO
    {
        public string Name { get; set; }
        public List<string> OperationTypes { get; set; }
        public List<PermissionDTO> SubPermissions { get; set; }
        public string ApplicationObjectType { get; set; }
    }
}
