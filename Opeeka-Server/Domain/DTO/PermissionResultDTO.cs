// -----------------------------------------------------------------------
// <copyright file="PermissionResultDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System.Collections.Generic;

namespace Opeeka.PICS.Domain.DTO
{
    public class PermissionResultDTO
    {
        public string Permission { get; set; }
        public List<string> OperationType { get; set; }
    }
}
