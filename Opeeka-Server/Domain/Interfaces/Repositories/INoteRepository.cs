// -----------------------------------------------------------------------
// <copyright file="INoteRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    /// <summary>
    /// INoteRepository.
    /// </summary>
    public interface INoteRepository : IAsyncRepository<Note>
    {
        /// <summary>
        /// AddNote.
        /// </summary>
        /// <param name="note">note.</param>
        /// <returns>Note.</returns>
        Note AddNote(Note note);

        /// <summary>
        /// UpdateNote
        /// </summary>
        /// <param name="note"></param>
        /// <returns>Note</returns>
        Note UpdateNote(Note note);

        /// <summary>
        /// To get note.
        /// </summary>
        /// <param id="noteID">id.</param>
        /// <returns>NoteDTO.</returns>
        Task<NoteDTO> GetNotes(int id);

        /// GetNotesByAssessmentResponseID.
        /// </summary>
        /// <param name="assessmentResponseID">ID of the response</param>
        /// <returns>List of notes</returns>
        List<Note> GetNotesByAssessmentResponseID(int assessmentResponseID);

        /// <summary>
        /// UpdateBulkNotes.
        /// </summary>
        /// <param name="notes">List of Note</param>
        /// <returns>List of Note</returns>
        List<Note> UpdateBulkNotes(List<Note> notes);

        List<Note> GetNotesAsync(List<Guid> noteGuidList);
    }
}
