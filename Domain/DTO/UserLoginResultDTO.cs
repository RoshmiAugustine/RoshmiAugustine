// -----------------------------------------------------------------------
// <copyright file="UsersDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;

namespace Opeeka.PICS.Domain.DTO
{
    public class UserLoginResultDTO
    {
        public long UserID { get; set; }

        public Guid UserIndex { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public DateTime? LastLogin { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public int AgencyId { get; set; }

        public string AspNetUserID { get; set; }
    }
}
