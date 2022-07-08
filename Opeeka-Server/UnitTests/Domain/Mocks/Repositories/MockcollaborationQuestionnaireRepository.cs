
using Moq;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Interfaces.Repositories;

namespace Opeeka.PICS.UnitTests.Domain.Mocks.Repositories
{
    public class MockcollaborationQuestionnaireRepository : Mock<ICollaborationQuestionnaireRepository>
    {
        public MockcollaborationQuestionnaireRepository MockcollaborationQuestionnaireAdd(long result)
        {
            Setup(x => x.AddCollaborationQuestionnaire(It.IsAny<CollaborationQuestionnaireDTO>()))
                 .Returns(result);

            return this;
        }
    }
}
