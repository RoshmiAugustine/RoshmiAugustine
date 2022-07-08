// -----------------------------------------------------------------------
// <copyright file="NotificationResolutionNote.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Domain.Entities
{
    public class NotificationResolutionNote : BaseEntity
    {
        public int NotificationResolutionNoteID { get; set; }

        public int NotificationLogID { get; set; }

        public int Note_NoteID { get; set; }

        public int? NotificationResolutionHistoryID { get; set; }

        public Note Note { get; set; }
        public NotificationLog NotificationLog { get; set; }
        public NotificationResolutionHistory NotificationResolutionHistory { get; set; }
    }
}
