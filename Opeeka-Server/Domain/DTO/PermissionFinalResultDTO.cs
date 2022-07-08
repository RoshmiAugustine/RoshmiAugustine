// -----------------------------------------------------------------------
// <copyright file="PermissionResultDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System.Collections.Generic;

namespace Opeeka.PICS.Domain.DTO
{
    public class PermissionFinalResultDTO
    {
        public List<PermissionResultDTO> Permission { get; set; }
        public List<PermissionResultDTO> SharedPermission { get; set; }
    }
}
