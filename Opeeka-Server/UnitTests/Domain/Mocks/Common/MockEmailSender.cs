using System;
using System.Net;
using Moq;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Interfaces.Common;

namespace Opeeka.PICS.UnitTests.Domain.Mocks.Common
{
    public class MockEmailSender : Mock<IEmailSender>
    {
        public MockEmailSender MockSendEmailAsync(HttpStatusCode returnValue)
        {
            Setup(x => x.SendEmailAsync(It.IsAny<SendEmail>()))
                             .Returns(returnValue);
            return this;
        }

        public MockEmailSender MockSendEmailAsyncException()
        {
            Setup(x => x.SendEmailAsync(It.IsAny<SendEmail>()))
                             .Throws<Exception>();
            return this;
        }

    }
}
