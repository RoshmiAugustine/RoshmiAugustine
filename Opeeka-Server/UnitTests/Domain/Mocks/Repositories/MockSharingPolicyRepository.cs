
using Moq;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Opeeka.PICS.UnitTests.Domain.Mocks.Repositories
{
    public class MockSharingPolicyRepository : Mock<ISharingPolicyRepository>
    {
        public MockSharingPolicyRepository MockSharingPolicy(List<SharingPolicyDTO> result)
        {
            Setup(x => x.GetAllSharingPolicy())
                 .Returns(Task.FromResult(result));
            return this;
        }
    }
}
