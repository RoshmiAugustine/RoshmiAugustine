// -----------------------------------------------------------------------
// <copyright file="PersonInitialsResponseDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Domain.DTO.Output
{
    public class PersonInitialsResponseDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public PersonInitialsDTO PersonInitials { get; set; }
    }
}
