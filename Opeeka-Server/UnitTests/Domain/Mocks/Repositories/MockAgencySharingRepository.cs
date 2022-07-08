
using Moq;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using System;
using System.Threading.Tasks;

namespace Opeeka.PICS.UnitTests.Domain.Mocks.Repositories
{
    public class MockAgencySharingRepository : Mock<IAgencySharingRepository>
    {
        public MockAgencySharingRepository AgencySharing(AgencySharing result)
        {
            Setup(x => x.UpdateAgencySharing(It.IsAny<AgencySharing>()))
               .Returns(result);

            Setup(x => x.GetAgencySharing(It.IsAny<Guid>()))
                .Returns(Task.FromResult(result));
            return this;
        }

        public MockAgencySharingRepository MockAddAgencySharing(AgencySharingDTO result)
        {
            Setup(x => x.AddAgencySharing(It.IsAny<AgencySharingDTO>()))
                .Returns(result);
            return this;
        }

    }
}
