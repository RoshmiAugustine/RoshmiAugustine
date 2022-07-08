// -----------------------------------------------------------------------
// <copyright file="StatusResponseDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Domain.DTO
{
    public class StatusResponseDTO
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public bool Status { get; set; }
    }
}
