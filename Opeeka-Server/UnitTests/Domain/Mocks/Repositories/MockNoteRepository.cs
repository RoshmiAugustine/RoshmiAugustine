
using Moq;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Opeeka.PICS.UnitTests.Domain.Mocks.Repositories
{
    public class MockNoteRepository : Mock<INoteRepository>
    {
        public MockNoteRepository MockAddNote(Note note, List<Note> notes = null)
        {
            Setup(x => x.GetNotesByAssessmentResponseID(It.IsAny<int>()))
                .Returns(notes);

            Setup(x => x.UpdateBulkNotes(It.IsAny<List<Note>>()))
                .Returns(notes);

            Setup(x => x.AddNote(It.IsAny<Note>()))
                .Returns(note);
            Setup(x => x.AddBulkAsync(It.IsAny<List<Note>>()));
            
            return this;
        }

        public MockNoteRepository MockUpdateNote(Note note, List<Note> notes = null)
        {
            Setup(x => x.GetNotesByAssessmentResponseID(It.IsAny<int>()))
                .Returns(notes);

            Setup(x => x.UpdateBulkNotes(It.IsAny<List<Note>>()))
                .Returns(notes);

            Setup(x => x.UpdateNote(It.IsAny<Note>()))
                .Returns(note);
            Setup(x => x.GetNotesAsync(It.IsAny<List<Guid>>()))
                .Returns(notes);
            return this;
        }
    }
}
