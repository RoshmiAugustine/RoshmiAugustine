// -----------------------------------------------------------------------
// <copyright file="PersonNote.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Domain.Entities
{
    public class PersonNote : BaseEntity
    {
        public long PersonNoteID { get; set; }
        public long PersonID { get; set; }
        public int NoteID { get; set; }

        public Note Note { get; set; }
        public Person Person { get; set; }
    }
}
