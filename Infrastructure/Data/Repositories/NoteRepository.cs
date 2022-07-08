using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Infrastructure.Data.Repositories.Base;

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{
    public class NoteRepository : BaseRepository<Note>, INoteRepository
    {
        private readonly IMapper mapper;
        private readonly ILogger<NoteRepository> logger;
        private readonly OpeekaDBContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="NoteRepository"/> class.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public NoteRepository(ILogger<NoteRepository> logger, OpeekaDBContext dbContext, IMapper mapper)
            : base(dbContext)
        {
            this.mapper = mapper;
            this.logger = logger;
            this._dbContext = dbContext;
        }

        /// <summary>
        /// AddNote.
        /// </summary>
        /// <param name="note">note.</param>
        /// <returns>Note.</returns>
        public Note AddNote(Note note)
        {
            try
            {
                var result = this.AddAsync(note).Result;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// UpdateNote
        /// </summary>
        /// <param name="note"></param>
        /// <returns>Note</returns>
        public Note UpdateNote(Note note)
        {
            try
            {
                var result = this.UpdateAsync(note).Result;
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// To get note.
        /// </summary>
        /// <param id="noteID">id.</param>
        /// <returns>NoteDTO.</returns>
        public async Task<NoteDTO> GetNotes(int id)
        {
            try
            {
                NoteDTO noteDTO = new NoteDTO();
                Note note = await this.GetRowAsync(x => x.NoteID == id);
                this.mapper.Map<Note, NoteDTO>(note, noteDTO);

                return noteDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// GetNotesByAssessmentResponseID.
        /// </summary>
        /// <param name="assessmentResponseID">Id of the assessment response</param>
        /// <returns>List of Notes</returns>
        public List<Note> GetNotesByAssessmentResponseID(int assessmentResponseID)
        {

            try
            {

                var query = string.Empty;
                query = @"select N.NoteID,N.NoteText,N.IsConfidential,N.IsRemoved, N.AddedByVoiceTypeID, N.VoiceTypeFKID from AssessmentResponse AR
                            join AssessmentResponseNote ARN on ARN.AssessmentResponseID=AR.AssessmentResponseID
                            join Note N on N.NoteID=ARN.NoteID
                            where N.IsRemoved=0 and AR.AssessmentResponseID= " + assessmentResponseID;

                var notes = ExecuteSqlQuery(query, x => new Note
                {
                    NoteID = x[0] == DBNull.Value ? 0 : (int)x[0],
                    NoteText = x[1] == DBNull.Value ? null : (string)x[1],
                    IsConfidential = x[2] == DBNull.Value ? false : (bool)x[2],
                    IsRemoved = x[3] == DBNull.Value ? false : (bool)x[3],
                    AddedByVoiceTypeID = x[4] == DBNull.Value ? 0 : (int)x[4],
                    VoiceTypeFKID = x[5] == DBNull.Value ? 0 : (long?)x[5]
                }).ToList();

                return notes;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// UpdateBulkNotes.
        /// </summary>
        /// <param name="notes">List of Note</param>
        /// <returns>List of Note</returns>
        public List<Note> UpdateBulkNotes(List<Note> notes)
        {
            try
            {
                var res = this.UpdateBulkAsync(notes);
                res.Wait();
                return notes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To get note.
        /// </summary>
        /// <param id="noteID">id.</param>
        /// <returns>NoteDTO.</returns>
        public List<Note> GetNotesAsync(List<Guid> noteGuidList)
        {
            try
            {
                var notes = this.GetAsync(x => noteGuidList.Contains(x.NoteGUID)).Result;
                return notes.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
