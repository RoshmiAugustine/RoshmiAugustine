// -----------------------------------------------------------------------
// <copyright file="ErrorLogDTO.cs" company="Naico ITS">
// Copyright (c) Naico ITS. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Domain.DTO.Input
{
    public class ErrorLogDTO
    {
        public string ErrorMessage { get; set; }
        public bool IsClient { get; set; }

    }
}
