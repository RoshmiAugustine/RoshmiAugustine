using Moq;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;

namespace Opeeka.PICS.UnitTests.Domain.Mocks.Repositories
{
    public class MockLookupRepository : Mock<ILookupRepository>
    {
        public MockLookupRepository MockGetAllCountries(List<CountryLookupDTO> result)
        {
            Setup(x => x.GetAllCountries())
                .Returns(result);
            return this;
        }
        public MockLookupRepository MockGetAllCountriesException(List<CountryLookupDTO> result)
        {
            Setup(x => x.GetAllCountries())
                .Throws<Exception>();
            return this;
        }
        public MockLookupRepository MockGetTimeFrameDetails(TimeFrame result)
        {
            Setup(x => x.GetTimeFrameDetails(It.IsAny<int>()))
                 .Returns(result);
            return this;
        }
        public MockLookupRepository MockGetTimeFrameDetailsException()
        {
            Setup(x => x.GetTimeFrameDetails(It.IsAny<int>()))
                 .Throws<Exception>();
            return this;
        }
        public MockLookupRepository MockGetAllAssessments(List<AssessmentsDTO> result)
        {
            Setup(x => x.GetAllAssessments(It.IsAny<int>()))
                .Returns(result);
            return this;
        }

        public MockLookupRepository MockGetAllAssessmentsException(List<AssessmentDetailsDTO> result)
        {
            Setup(x => x.GetAllAssessments(It.IsAny<int>()))
               .Throws<Exception>();
            return this;
        }

        public MockLookupRepository MockGetAllAssessments(List<AssessmentsDTO> result, long collaborationID, int voiceTypeID)
        {
            Setup(x => x.GetAllAssessments(It.IsAny<long>(), It.IsAny<long>(), collaborationID, voiceTypeID, It.IsAny<int>(), It.IsAny<SharedDetailsDTO>(), It.IsAny<SharedDetailsDTO>()))
                .Returns(result);
            return this;
        }

        public MockLookupRepository MockGetAllAssessmentsException(List<AssessmentDetailsDTO> result, long collaborationID, int voiceTypeID)
        {
            Setup(x => x.GetAllAssessments(It.IsAny<long>(), It.IsAny<long>(), collaborationID, voiceTypeID, It.IsAny<long>(), It.IsAny<SharedDetailsDTO>(), It.IsAny<SharedDetailsDTO>()))
               .Throws<Exception>();
            return this;
        }

        internal Mock<ILookupRepository> MockGetAllImportTypes(List<QuestionnaireDTO> result)
        {
            Setup(x => x.GetAllAgencyQuestionnaire(It.IsAny<long>()))
             .Returns(result);
            return this;
        }

        internal Mock<ILookupRepository> MockGetAllImportTypesException()
        {
            Setup(x => x.GetAllAgencyQuestionnaire(It.IsAny<long>()))
               .Throws<Exception>();
            return this;
        }
    }
}
