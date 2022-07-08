// -----------------------------------------------------------------------
// <copyright file="UsersDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
namespace Opeeka.PICS.Domain.DTO
{
    public class UsersDTO
    {
        public int UserID { get; set; }

        public Guid UserIndex { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public List<string> Roles { get; set; }
        public DateTime? LastLogin { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public long AgencyID { get; set; }
        public string AspNetUserID { get; set; }
        public string AgencyAbbrev { get; set; }
        public int RoleID { get; set; }
        public string ExistingEmail { get; set; }
        public Guid helperIndex { get; set; }
        public string InstanceURL { get; set; }
        public DateTime? NotificationViewedOn { get; set; }
        
    }
}
