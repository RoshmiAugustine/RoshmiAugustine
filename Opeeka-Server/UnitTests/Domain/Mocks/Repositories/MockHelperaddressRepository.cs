using Moq;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using System.Threading.Tasks;

namespace Opeeka.PICS.UnitTests.Domain.Mocks.Repositories
{
    public class MockHelperaddressRepository : Mock<IHelperAddressRepository>
    {

        public MockHelperaddressRepository MockEditHelperAddress(HelperAddressDTO result)
        {
            Setup(x => x.GetHelperAddressByHelperIDAsync(It.IsAny<long>()))
             .Returns(Task.FromResult(result));

            return this;

        }
        public MockHelperaddressRepository MockAddHelperaddress()
        {
            Setup(x => x.CreateHelperAddress(It.IsAny<int>(), It.IsAny<int>()))
              ;
            return this;
        }

    }
}
