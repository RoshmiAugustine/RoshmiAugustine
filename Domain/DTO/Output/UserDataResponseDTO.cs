// -----------------------------------------------------------------------
// <copyright file="UserDataResponseDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
namespace Opeeka.PICS.Domain.DTO.Output
{
    public class UserDataResponseDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public UsersDTO UserDetails { get; set; }
    }
}
