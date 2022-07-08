// -----------------------------------------------------------------------
// <copyright file="Note.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;

namespace Opeeka.PICS.Domain.Entities
{
    public class Note : BaseEntity
    {
        public int NoteID { get; set; }
        public string NoteText { get; set; }
        public bool IsConfidential { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UpdateUserID { get; set; }
        public int? AddedByVoiceTypeID { get; set; }
        public long? VoiceTypeFKID { get; set; }
        public Guid NoteGUID { get; set; }

        public User UpdateUser { get; set; }

    }
}
