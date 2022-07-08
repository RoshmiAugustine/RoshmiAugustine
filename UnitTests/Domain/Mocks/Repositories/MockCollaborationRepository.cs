
using Moq;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Opeeka.PICS.UnitTests.Domain.Mocks.Repositories
{
    public class MockCollaborationRepository : Mock<ICollaborationRepository>
    {
        public MockCollaborationRepository MockGetCollaboration(List<CollaborationLookupDTO> result)
        {
            Setup(x => x.GetAllAgencycollaborations(It.IsAny<long>()))
                 .Returns(result);
            return this;
        }

        public MockCollaborationRepository MockGetCollaborationCountByTherapy(int therapyTypeId, int count)
        {
            Setup(x => x.GetCollaborationCountByTherapy(therapyTypeId))
                .Returns(count);
            return this;
        }
    }
}
