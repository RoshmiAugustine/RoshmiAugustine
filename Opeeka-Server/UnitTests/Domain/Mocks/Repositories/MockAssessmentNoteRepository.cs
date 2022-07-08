using Moq;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;

namespace Opeeka.PICS.UnitTests.Domain.Mocks.Repositories
{
    public class MockAssessmentNoteRepository : Mock<IAssessmentNoteRepository>
    {
        public MockAssessmentNoteRepository MockAddAssessmentNote(AssessmentNote result)
        {
            Setup(x => x.AddAssessmentNote(It.IsAny<AssessmentNote>()))
                .Returns(result);
            return this;
        }
    }
}