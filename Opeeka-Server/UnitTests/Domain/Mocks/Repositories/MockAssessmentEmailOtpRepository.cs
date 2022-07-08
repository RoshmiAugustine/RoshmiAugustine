using Moq;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using System.Threading.Tasks;

namespace Opeeka.PICS.UnitTests.Domain.Mocks.Repositories
{
    public class MockAssessmentEmailOtpRepository : Mock<IAssessmentEmailOtpRepository>
    {
        public MockAssessmentEmailOtpRepository MockAddOrUpdateEmailOtp(AssessmentEmailOtp assessmentEmailOtpData)
        {
            Setup(x => x.AddAssessmentEmailOtpData(It.IsAny<AssessmentEmailOtpDTO>()))
                .Returns(assessmentEmailOtpData);
            Setup(x => x.UpdateEmailOtp(It.IsAny<AssessmentEmailOtpDTO>()))
                .Returns(assessmentEmailOtpData);
            Setup(x => x.FindEmailOtpByEmailLink(It.IsAny<int>()))
               .Returns(Task.FromResult(assessmentEmailOtpData));
            return this;
        }

        public MockAssessmentEmailOtpRepository MockFindIsEmailOtpValid(AssessmentEmailOtp assessmentEmailOtp)
        {
            Setup(x => x.FindIsEmailOtpValid(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(Task.FromResult(assessmentEmailOtp));
            return this;
        }
    }
}
