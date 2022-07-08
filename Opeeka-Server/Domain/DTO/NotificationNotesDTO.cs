// -----------------------------------------------------------------------
// <copyright file="NotificationNotesDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace Opeeka.PICS.Domain.DTO
{
    public class NotificationNotesDTO
    {
        public int NotificationLogID { get; set; }

        public int NotificationResolutionNoteID { get; set; }

        public int NotificationResolutionHistoryID { get; set; }

        public int NoteID { get; set; }

        public string NoteText { get; set; }

        public DateTime UpdateDate { get; set; }

        public int HelperID { get; set; }

        public int UpdateUserID { get; set; }

        public string User { get; set; }

        public int HelperTitleID { get; set; }

        public string HelperTitle { get; set; }

        public string HelperName { get; set; }

        public string NoteType { get; set; }
    }
}
