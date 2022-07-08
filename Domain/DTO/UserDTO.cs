// -----------------------------------------------------------------------
// <copyright file="WeatherForecastDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace Opeeka.PICS.Domain.DTO
{
    public class UserDTO
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        [Required(ErrorMessage = "emailRequired")]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string Role { get; set; }
        public string TenantID { get; set; }
        public bool RememberMe { get; set; }
    }
}
