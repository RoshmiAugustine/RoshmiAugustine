using Moq;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using System;

namespace Opeeka.PICS.UnitTests.Domain.Mocks.Repositories
{
    public class MockAssessmentEmailLinkRepository : Mock<IAssessmentEmailLinkRepository>
    {
        public MockAssessmentEmailLinkRepository MockAddEmailLinkData(AssessmentEmailLinkDetails result)
        {
            Setup(x => x.AddEmailLinkData(It.IsAny<AssessmentEmailLinkDetails>()))
                .Returns(result);
            return this;
        }
        public MockAssessmentEmailLinkRepository MockAddEmailLinkDataException(AssessmentEmailLinkDetails result)
        {
            Setup(x => x.AddEmailLinkData(It.IsAny<AssessmentEmailLinkDetails>()))
              .Throws<Exception>();
            return this;
        }
        public MockAssessmentEmailLinkRepository MockGetEmailLinkData(AssessmentEmailLinkDetails result)
        {
            Setup(x => x.GetEmailLinkData(It.IsAny<Guid>()))
                .Returns(result);
            return this;
        }
        public MockAssessmentEmailLinkRepository MockGetEmailLinkDataException(AssessmentEmailLinkDetails result)
        {
            Setup(x => x.GetEmailLinkData(It.IsAny<Guid>()))
              .Throws<Exception>();
            return this;
        }

    }
}
