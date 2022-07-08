using System;
using Moq;
using Opeeka.PICS.Domain.Interfaces.Common;

namespace Opeeka.PICS.UnitTests.Domain.Mocks.Common
{
    public class MockDataProtection : Mock<IDataProtection>
    {
        public MockDataProtection MockProtect(string returnValue)
        {
            Setup(x => x.Encrypt(It.IsAny<string>(), It.IsAny<string>()))
                             .Returns(returnValue);
            return this;
        }
        public MockDataProtection MockUnProtect(string returnValue)
        {
            Setup(x => x.Decrypt(It.IsAny<string>(), It.IsAny<string>()))
                             .Returns(returnValue);
            return this;
        }
        public MockDataProtection MockProtectException()
        {
            Setup(x => x.Encrypt(It.IsAny<string>(), It.IsAny<string>()))
                             .Throws<Exception>();
            return this;
        }
    }
}
