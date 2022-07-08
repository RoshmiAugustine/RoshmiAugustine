using Moq;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.DTO.Output;
using Opeeka.PICS.Domain.Interfaces.Services;
using System;


namespace Opeeka.PICS.UnitTests.Api.Services
{
   public  class MockAssessmentService : Mock<IAssessmentService>
    {

        public MockAssessmentService MockGetQuestions(QuestionsResponseDTO result)
        {
            Setup(x => x.GetQuestions(It.IsAny<int>()))
                .Returns(result);
            return this;
        }

        public MockAssessmentService MockGetQuestionsException()
        {
            Setup(x => x.GetQuestions(It.IsAny<int>()))
                .Throws<Exception>();
            return this;
        }
        public MockAssessmentService MockGetAssessmentDetails(AssessmentDetailsResponseDTO result)
        {
            Setup(x => x.GetAssessmentDetails(It.IsAny<Guid>(), It.IsAny<int>(), It.IsAny<DateTime?>(), It.IsAny<UserTokenDetails>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(result);
            return this;
        }

        public MockAssessmentService MockGetAssessmentDetailsException()
        {
            Setup(x => x.GetAssessmentDetails(It.IsAny<Guid>(), It.IsAny<int>(), It.IsAny<DateTime?>(), It.IsAny<UserTokenDetails>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .Throws<Exception>();
            return this;
        }

    }
}
