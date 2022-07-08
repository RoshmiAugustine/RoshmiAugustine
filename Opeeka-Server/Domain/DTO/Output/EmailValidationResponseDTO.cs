// -----------------------------------------------------------------------
// <copyright file="EmailValidationResponseDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System.Collections.Generic;

namespace Opeeka.PICS.Domain.DTO.Output
{
    public class EmailValidationResponseDTO
    {
        public string ResponseStatus { get; set; }
        public int ResponseStatusCode { get; set; }
        public bool isVaildEmails { get; set; }
       public List<UsersDTO> existingEmails { get; set; }
    }
}
