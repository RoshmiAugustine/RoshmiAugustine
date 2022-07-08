using Moq;
using Opeeka.PICS.Domain.Interfaces.Repositories;

namespace Opeeka.PICS.UnitTests.Domain.Mocks.Repositories
{
    public class MockOptionsRepository : Mock<IOptionsRepository>
    {
        internal Mock<IOptionsRepository> MockIsValidListOrder(bool v)
        {
            Setup(x => x.IsValidListOrder(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<long>(), It.IsAny<long>()))
                  .Returns(v);
            return this;
        }
    }
}
