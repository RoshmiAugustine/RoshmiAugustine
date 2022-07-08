using System.Threading.Tasks;
using Moq;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Interfaces.Repositories;

namespace Opeeka.PICS.UnitTests.Domain.Mocks.Repositories
{
    public class MockHelperAddressRespository : Mock<IHelperAddressRepository>
    {
        internal Mock<IHelperAddressRepository> MockHelperAddress(HelperAddressDTO mockHelper)
        {
            Setup(x => x.GetHelperAddressByHelperIDAsync(It.IsAny<long>()))
                .Returns(Task.FromResult(mockHelper));
            return this;
        }
    }
}
