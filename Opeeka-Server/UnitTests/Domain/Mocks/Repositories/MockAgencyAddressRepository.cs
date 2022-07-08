
using Moq;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using System;
using System.Threading.Tasks;

namespace Opeeka.PICS.UnitTests.Domain.Mocks.Repositories
{
    public class MockAgencyAddressRepository : Mock<IAgencyAddressRepository>
    {
        public MockAgencyAddressRepository MockAddAgencyAddress(Int64 result)
        {
            Setup(x => x.AddAgencyAddress(It.IsAny<AgencyAddressDTO>()))
                .Returns(result);
            return this;
        }
        public MockAgencyAddressRepository MockUpdateAgencyAddress(AgencyAddress result, AgencyAddressDTO res)
        {
            Setup(x => x.UpdateAsync(It.IsAny<AgencyAddress>()))
              .Returns(Task.FromResult(result));

            Setup(x => x.GetAgencyAddress(It.IsAny<long>()))
                .Returns(Task.FromResult(res));
            return this;
        }
    }
}
