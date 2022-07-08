// -----------------------------------------------------------------------
// <copyright file="UserRoleDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;

namespace Opeeka.PICS.Domain.DTO
{
    public class UserRoleDTO
    {
        public int UserRoleID { get; set; }

        public Guid UserRoleIndex { get; set; }

        public int UserID { get; set; }

        public int SystemRoleID { get; set; }

        public Guid UserIndex { get; set; }
    }
}
