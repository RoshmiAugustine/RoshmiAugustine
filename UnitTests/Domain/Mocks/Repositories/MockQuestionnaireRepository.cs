using Moq;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Opeeka.PICS.UnitTests.Domain.Mocks.Repositories
{
    public class MockQuestionnaireRepository : Mock<IQuestionnaireRepository>
    {
        public MockQuestionnaireRepository MockPersonQuestionnaireList(List<PersonQuestionnaireListDTO> results)
        {
            Setup(x => x.GetPersonQuestionnaireList(It.IsAny<Guid>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(results);
            Setup(x => x.GetPersonQuestionnaireCount(It.IsAny<Guid>()))
                .Returns(results.Count);
            return this;
        }

        public MockQuestionnaireRepository MockPersonQuestionnaireListException(List<PersonQuestionnaireListDTO> results)
        {
            Setup(x => x.GetPersonQuestionnaireList(It.IsAny<Guid>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>()))
                .Throws<Exception>();
            Setup(x => x.GetPersonQuestionnaireCount(It.IsAny<Guid>()))
                .Returns(results.Count);
            return this;
        }

        public MockQuestionnaireRepository MockGetQuestions(QuestionsDTO result)
        {
            Setup(x => x.GetQuestions(It.IsAny<int>()))
                .Returns(result);
            return this;
        }

        public MockQuestionnaireRepository MockGetQuestionsException(QuestionsDTO results)
        {
            Setup(x => x.GetQuestions(It.IsAny<int>()))
                .Throws<Exception>();
            return this;
        }

        public MockQuestionnaireRepository MockQuestionnaireData(List<PersonQuestionnaireDataDTO> results)
        {
            Setup(x => x.GetAllQuestionnairesWithAgency(It.IsAny<long>()))
                .Returns(results);
            return this;
        }

        public MockQuestionnaireRepository MockQuestionnaireDataException(List<PersonQuestionnaireDataDTO> results)
        {
            Setup(x => x.GetAllQuestionnairesWithAgency(It.IsAny<long>()))
                .Throws<Exception>();
            return this;
        }

        public MockQuestionnaireRepository MockCloneQuestionnaire(QuestionnairesDTO result)
        {
            Setup(x => x.CloneQuestionnaire(It.IsAny<QuestionnairesDTO>()))
               .Returns(result);

            Setup(x => x.GetQuestionnaire(It.IsAny<int>()))
                .Returns(Task.FromResult(result));
            return this;
        }

        public MockQuestionnaireRepository MockCloneQuestionnaireException(QuestionnairesDTO result)
        {
            Setup(x => x.CloneQuestionnaire(It.IsAny<QuestionnairesDTO>())).Throws<Exception>();

            Setup(x => x.GetQuestionnaire(It.IsAny<int>()))
                .Returns(Task.FromResult(result));
            return this;
        }

        public MockQuestionnaireRepository MockAssessedQuestionnaireData(List<AssessmentQuestionnaireDataDTO> results)
        {
            Setup(x => x.GetAllQuestionnaireWithCompletedAssessment(It.IsAny<long>(), It.IsAny<long>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<long>(), It.IsAny<int>(), It.IsAny<long>(), It.IsAny<SharedDetailsDTO>(), It.IsAny<SharedDetailsDTO>()))
                .Returns(Tuple.Create(results, results.Count));
            Setup(x => x.GetAllQuestionnaireWithCompletedAssessmentCount(It.IsAny<long>(), It.IsAny<long>()))
                .Returns(results.Count);
            return this;
        }

        public MockQuestionnaireRepository MockAssessedQuestionnaireDataException(List<AssessmentQuestionnaireDataDTO> results)
        {
            Setup(x => x.GetAllQuestionnaireWithCompletedAssessment(It.IsAny<long>(), It.IsAny<long>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<long>(), It.IsAny<int>(), It.IsAny<long>(), It.IsAny<SharedDetailsDTO>(), It.IsAny<SharedDetailsDTO>()))
                .Throws<Exception>();
            return this;
        }

        internal Mock<IQuestionnaireRepository> MockdeleteQuestionnaire(QuestionnairesDTO mockQuestionnaireDTO)
        {
            Setup(x => x.GetQuestionnaire(It.IsAny<int>()))
              .Returns(Task.FromResult(mockQuestionnaireDTO));

            Setup(x => x.GetQuestionnaireUsedCountByID(It.IsAny<int>()))
              .Returns(0);

            Setup(x => x.UpdateQuestionnaire(It.IsAny<QuestionnairesDTO>()))
               .Returns(mockQuestionnaireDTO);
            return this;
        }

        internal Mock<IQuestionnaireRepository> MockDeleteQuestionnaireException()
        {
            Setup(x => x.GetQuestionnaire(It.IsAny<int>()))
              .Throws<Exception>();

            Setup(x => x.UpdateQuestionnaire(It.IsAny<QuestionnairesDTO>()))
             .Throws<Exception>();
            return this;
        }
    }
}
