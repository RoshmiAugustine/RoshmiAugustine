// -----------------------------------------------------------------------
// <copyright file="UsersDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
namespace Opeeka.PICS.Domain.DTO
{
    public class AccountConfirmationModelDTO
    {
        public string UserID { get; set; }
        public string Password { get; set; }
        public string EmailConfirmationToken { get; set; }
        public string PasswordConfirmationToken { get; set; }
        public string TenantId { get; set; }
    }
}
