using Moq;
using Opeeka.PICS.Domain.Interfaces.Common;
using System;

namespace Opeeka.PICS.UnitTests.Domain.Mocks.Repositories
{
    public class MockUtility : Mock<IUtility>
    {
        // mock class
        public MockUtility SetupUtility(DateTime returnValue)
        {
            Setup(x => x.ConvertToUtcDateTime(It.IsAny<DateTime>(), It.IsAny<int>()))
                             .Returns(returnValue);
            return this;
        }
    }
}
