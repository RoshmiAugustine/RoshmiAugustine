
using Moq;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using System;
using System.Threading.Tasks;

namespace Opeeka.PICS.UnitTests.Domain.Mocks.Repositories
{
    public class MockAddressRepository : Mock<IAddressrepository>
    {
        public MockAddressRepository MockAddAddress(Int64 result)
        {
            Setup(x => x.AddAddress(It.IsAny<AddressDTO>()))
                .Returns(result);
            return this;
        }
        public MockAddressRepository MockEditAddress(AddressDTO result)
        {
            Setup(x => x.UpdateAddress(It.IsAny<AddressDTO>()))
               .Returns(result);

            Setup(x => x.GetAddress(It.IsAny<long>()))
                .Returns(Task.FromResult(result));
            return this;
        }

    }
}
