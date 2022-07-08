// -----------------------------------------------------------------------
// <copyright file="UsersDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;

namespace Opeeka.PICS.Domain.DTO
{
    public class TokenResultDTO
    {
        public string EmailToken { get; set; }
        public string PasswordToken { get; set; }
        public string Id { get; set; }
        public string TenantId { get; set; }
        public Int64 UserID { get; set; }
        public bool AlreadyExists { get; set; }
        public Guid UserIndex { get; set; }
        public Guid helperIndex { get; set; }

    }
}
