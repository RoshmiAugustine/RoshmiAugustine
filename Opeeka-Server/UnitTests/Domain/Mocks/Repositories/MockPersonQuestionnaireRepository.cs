
using Moq;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Opeeka.PICS.UnitTests.Domain.Mocks.Repositories
{
    public class MockPersonQuestionnaireRepository : Mock<IPersonQuestionnaireRepository>
    {
        public MockPersonQuestionnaireRepository MockPersonQuestionnaireAdd(long res)
        {
            Setup(x => x.AddPersonQuestionnaire(It.IsAny<PersonQuestionnaireDTO>()))
                 .Returns(res);
            return this;
        }


        public MockPersonQuestionnaireRepository MockPersonQuestionnaireData(IReadOnlyList<PersonQuestionnaireDTO> results)
        {
            Setup(x => x.GetPersonQuestionnaireList(It.IsAny<long>()))
                .Returns(Task.FromResult(results));
            return this;
        }
        public MockPersonQuestionnaireRepository MockGetPersonQuestionnaireByID(PersonQuestionnaireDTO results)
        {
            Setup(x => x.GetPersonQuestionnaireByID(It.IsAny<long>()))
                .Returns(Task.FromResult(results));
            return this;
        }

        internal Mock<IPersonQuestionnaireRepository> MockGetAssessmentsByPersonQuestionaireID(List<Assessment> assessment)
        {
            Setup(x => x.GetAssessmentsByPersonQuestionaireID(It.IsAny<long>(), It.IsAny<List<int>>()))
              .Returns(assessment);
            return this;
        }

        internal Mock<IPersonQuestionnaireRepository> MockGetAssessmentsByPersonQuestionaireIDException()
        {
            Setup(x => x.GetAssessmentsByPersonQuestionaireID(It.IsAny<long>(), It.IsAny<List<int>>()))
              .Throws<Exception>();
            return this;
        }

        internal Mock<IPersonQuestionnaireRepository> MockGetAllpersonQuestionnaire(List<PersonQuestionnaire> personQuestionnaireList)
        {
             Setup(x => x.GetAllpersonQuestionnaire(It.IsAny<List<long>>(), It.IsAny<int>()))
                 .Returns(personQuestionnaireList);
            return this;
        }
        public MockPersonQuestionnaireRepository MockGetPersonAndQuestionnaireForAgency(PersonQuestionnaireDTO results, Person person)
        {
            Setup(x => x.GetPersonByPersonQuestionnaireID(It.IsAny<long>()))
                   .Returns(person);

            Setup(x => x.GetPersonQuestionnaireByID(It.IsAny<long>()))
                .Returns(Task.FromResult(results));
            return this;
        }
    }
}
