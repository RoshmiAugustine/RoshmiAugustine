// -----------------------------------------------------------------------
// <copyright file="PermissionResultDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System.Collections.Generic;

namespace Opeeka.PICS.Domain.DTO
{
    public class OperationTypeWithPolicyDTO
    {
        public string OperationType { get; set; }
        public int AgencySharingPolicyID { get; set; }
        public int CollaborationSharingPolicyID { get; set; }
    }
}
