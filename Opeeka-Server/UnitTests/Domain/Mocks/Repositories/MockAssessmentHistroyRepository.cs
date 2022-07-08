using System;
using Moq;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;

namespace Opeeka.PICS.UnitTests.Domain.Mocks.Repositories
{
    public class MockAssessmentHistroyRepository : Mock<IAssessmentHistoryRepository>
    {
        public MockAssessmentHistroyRepository AddAssessmentHistory(ReviewerHistory result)
        {
            Setup(x => x.AddAssessmentHistory(It.IsAny<ReviewerHistory>()))
                .Returns(result);
            return this;
        }
        public MockAssessmentHistroyRepository AddAssessmentHistoryException()
        {
            Setup(x => x.AddAssessmentHistory(It.IsAny<ReviewerHistory>()))
                .Throws<Exception>();
            return this;
        }

        public MockAssessmentHistroyRepository GetHistoryForAssessment()
        {
            Setup(x => x.GetHistoryForAssessment(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new ReviewerHistory());
            return this;
        }
    }
}
