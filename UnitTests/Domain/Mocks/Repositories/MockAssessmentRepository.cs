using Moq;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Opeeka.PICS.UnitTests.Domain.Mocks.Repositories
{
    public class MockAssessmentRepository : Mock<IAssessmentRepository>
    {
        public MockAssessmentRepository MockGetAssessmentDetails(List<AssessmentDetailsDTO> result)
        {
            Setup(x => x.GetAssessmentDetails(It.IsAny<Guid>(), It.IsAny<int>(), It.IsAny<DateTime?>(), It.IsAny<string>(), It.IsAny<long>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(result);
            return this;
        }

        public MockAssessmentRepository MockGetAssessmentDetailsException(List<AssessmentDetailsDTO> results)
        {
            Setup(x => x.GetAssessmentDetails(It.IsAny<Guid>(), It.IsAny<int>(), It.IsAny<DateTime?>(), It.IsAny<string>(), It.IsAny<long>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .Throws<Exception>();
            return this;
        }

        public MockAssessmentRepository MockAddAssessmentProgress(Assessment result)
        {
            Setup(x => x.AddAssessment(It.IsAny<Assessment>()))
               .Returns(result);

            return this;
        }

        public MockAssessmentRepository MockAddAssessmentProgressException(Assessment result)
        {
            Setup(x => x.AddAssessment(It.IsAny<Assessment>())).Throws<Exception>();

            return this;
        }

        public MockAssessmentRepository MockGetAssessment(Assessment result, Assessment updatedResult)
        {
            Setup(x => x.GetAssessment(It.IsAny<int>())).Returns(Task.FromResult(result));

            Setup(x => x.UpdateAssessment(It.IsAny<Assessment>())).Returns(updatedResult);

            return this;
        }

        public MockAssessmentRepository MockUpdateAssessmentException(Assessment result, Assessment updatedResult)
        {
            Setup(x => x.GetAssessment(It.IsAny<int>())).Returns(Task.FromResult(result));

            Setup(x => x.UpdateAssessment(It.IsAny<Assessment>())).Throws<Exception>();

            return this;
        }

        public MockAssessmentRepository MockGetPersonIDFromAssessment(long result)
        {
            Setup(x => x.GetPersonIdFromAssessment(It.IsAny<int>()))
                .Returns(result);

            return this;
        }
        public MockAssessmentRepository MockGetAssessmentByID(Assessment result)
        {
            Setup(x => x.GetAssessment(It.IsAny<int>())).Returns(Task.FromResult(result));

            return this;
        }

        public Mock<IAssessmentRepository> MockGetLastAssessmentByPerson(List<Assessment> result)
        {
            Setup(x => x.GetLastAssessmentByPerson(It.IsAny<long>(), It.IsAny<long>(), It.IsAny<long>(), It.IsAny<int>(), It.IsAny<long>(), It.IsAny< SharedDetailsDTO>(), It.IsAny<long>(), It.IsAny<SharedDetailsDTO>(), It.IsAny<int>())).Returns(result);
            return this;
        }
        public Mock<IAssessmentRepository> MockGetLastAssessmentByPersonException()
        {
            Setup(x => x.GetLastAssessmentByPerson(It.IsAny<long>(), It.IsAny<long>(), It.IsAny<long>(), It.IsAny<int>(), It.IsAny<long>(), It.IsAny<SharedDetailsDTO>(), It.IsAny<long>(), It.IsAny<SharedDetailsDTO>(), It.IsAny<int>())).Throws<Exception>();
            return this;
        }
        internal Mock<IAssessmentRepository> MockGetAssessmentException()
        {
            Setup(x => x.GetAssessment(It.IsAny<int>())).Throws<Exception>();
            return this;
        }

        internal Mock<IAssessmentRepository> MockGetAssessmentByPersonQuestionaireID(Assessment mockAssessment)
        {
            Setup(x => x.GetAssessmentByPersonQuestionaireID(It.IsAny<long>(), It.IsAny<int>()))
                .Returns(mockAssessment);
            return this;
        }

        internal Mock<IAssessmentRepository> MockGetAssessmentByPersonQuestionaireIDException()
        {
            Setup(x => x.GetAssessmentByPersonQuestionaireID(It.IsAny<long>(), It.IsAny<int>()))
               .Throws<Exception>();
            return this;
        }

        internal Mock<IAssessmentRepository> MockGetPersonQuestionnaireID(long questionaireId, List<Assessment> assessmentList)
        {
            Setup(x => x.GetAssessmentListByGUID(It.IsAny<List<Guid?>>()))
              .Returns(assessmentList);
            Setup(x => x.GetPersonQuestionnaireID(It.IsAny<Guid>(), It.IsAny<int>()))
              .Returns(questionaireId);
            return this;
        }

        internal Mock<IAssessmentRepository> MockGetPersonQuestionnaireIDException()
        {
            Setup(x => x.GetAssessmentListByGUID(It.IsAny<List<Guid?>>()))
                .Throws<Exception>();
            Setup(x => x.GetPersonQuestionnaireID(It.IsAny<Guid>(), It.IsAny<int>()))
               .Throws<Exception>();
            return this;
        }
    }
}
