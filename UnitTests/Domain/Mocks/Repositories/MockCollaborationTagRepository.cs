using Moq;
using Opeeka.PICS.Domain.Interfaces.Repositories;

namespace Opeeka.PICS.UnitTests.Domain.Mocks.Repositories
{
    public class MockCollaborationTagRepository : Mock<ICollaborationTagRepository>
    {
        public MockCollaborationTagRepository MockGetCollaborationTagTypeCountByCollaborationTag(int collaborationTagTypeId, int count)
        {
            Setup(x => x.GetCollaborationTagTypeCountByCollaborationTag(collaborationTagTypeId))
                .Returns(count);
            return this;
        }
    }
}
