// -----------------------------------------------------------------------
// <copyright file="NotificationNoteDataDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.ComponentModel.DataAnnotations;

namespace Opeeka.PICS.Domain.DTO
{
    public class NotificationNoteDataDTO
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid NotificationLogID")]
        public int NotificationLogID { get; set; }

        [Required]
        public string NoteText { get; set; }

        [Required]
        public bool IsConfidential { get; set; }

        public int NoteID { get; set; }
    }
}
