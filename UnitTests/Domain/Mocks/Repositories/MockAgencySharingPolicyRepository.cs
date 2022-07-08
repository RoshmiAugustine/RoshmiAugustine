
using Moq;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using System.Threading.Tasks;

namespace Opeeka.PICS.UnitTests.Domain.Mocks.Repositories
{
    public class MockAgencySharingPolicyRepository : Mock<IAgencySharingPolicyRepository>
    {
        public MockAgencySharingPolicyRepository AgencySharingPolicy(AgencySharingPolicyDTO result)
        {
            Setup(x => x.UpdateAgencySharingPolicy(It.IsAny<AgencySharingPolicyDTO>()))
               .Returns(result);

            Setup(x => x.GetAgencySharingPolicy(It.IsAny<long>()))
                .Returns(Task.FromResult(result));
            return this;
        }

        public MockAgencySharingPolicyRepository MockAddAgencySharingPolicy(AgencySharingPolicyDTO result)
        {
            Setup(x => x.AddAgencySharingPolicy(It.IsAny<AgencySharingPolicyDTO>()))
                .Returns(result);
            return this;
        }
    }
}
