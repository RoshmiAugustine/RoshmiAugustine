// -----------------------------------------------------------------------
// <copyright file="UsersDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Domain.DTO
{
    public class ForgotPasswordDTO
    {
        public string Email { get; set; }
        public string PasswordToken { get; set; }
        public string Password { get; set; }

    }
}
