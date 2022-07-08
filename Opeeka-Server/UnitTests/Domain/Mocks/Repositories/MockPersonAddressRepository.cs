
using Moq;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using System.Threading.Tasks;

namespace Opeeka.PICS.UnitTests.Domain.Mocks.Repositories
{
    public class MockPersonAddressRepository : Mock<IPersonAddressRepository>
    {
        public MockPersonAddressRepository MockPersonAddressAdd(long result)
        {
            Setup(x => x.AddPersonAddress(It.IsAny<PersonAddressDTO>()))
                 .Returns(result);
            return this;
        }
        public MockPersonAddressRepository MockPersonAddressEdit(PersonAddressDTO result)
        {

            Setup(x => x.GetPersonAddress(It.IsAny<long>()))
                .Returns(Task.FromResult(result));
            return this;
        }
    }
}
