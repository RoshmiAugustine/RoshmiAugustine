// -----------------------------------------------------------------------
// <copyright file="NotificationResolutionNoteDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Domain.DTO
{
    public class NotificationResolutionNoteDTO
    {
        public int NotificationResolutionNoteID { get; set; }

        public int NotificationLogID { get; set; }

        public int Note_NoteID { get; set; }

        public int? NotificationResolutionHistoryID { get; set; }
    }
}
