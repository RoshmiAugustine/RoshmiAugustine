using Moq;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Opeeka.PICS.UnitTests.Domain.Mocks.Repositories
{
    public class MockAssessmentResponseRepository : Mock<IAssessmentResponseRepository>
    {
        public MockAssessmentResponseRepository MockGetAssessmentValues(List<AssessmentValuesDTO> result)
        {
            Setup(x => x.GetAssessmentValues(It.IsAny<Guid>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(result);
            return this;
        }

        public MockAssessmentResponseRepository MockGetAssessmentValuesException(List<AssessmentValuesDTO> results)
        {
            Setup(x => x.GetAssessmentValues(It.IsAny<Guid>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>()))
                .Throws<Exception>();
            return this;
        }

        public MockAssessmentResponseRepository MockAddAssessmentProgress(AssessmentResponse result, List<AssessmentValuesDTO> result1, IReadOnlyList<AssessmentResponsesDTO> result2, IReadOnlyList<AssessmentResponse> result3, List<AssessmentResponse> result4)
        {
            Setup(x => x.GetAssessmentResponse(It.IsAny<int>()))
               .Returns(Task.FromResult(result2));
            Setup(x => x.AddAssessmentResponse(It.IsAny<AssessmentResponse>()))
               .Returns(result);
            Setup(x => x.GetNeedforFocusValues(It.IsAny<List<string>>()))
               .Returns(result1);
            Setup(x => x.GetAssessmentResponseListByGUID(It.IsAny<List<Guid>>()))
               .Returns(Task.FromResult(result3));
            Setup(x => x.GetAssessmentResponseList(It.IsAny<List<int>>()))
              .Returns(Task.FromResult(result3));
            Setup(x => x.GetAssessmentResponseListByAssessmentId(It.IsAny<int>()))
              .Returns(Task.FromResult(result3));
            Setup(x => x.UpdatePriority(It.IsAny<List<AssessmentResponse>>()))
              .Returns(result4);
            return this;
        }

        public MockAssessmentResponseRepository MockgetAssessmentResponse(IReadOnlyList<AssessmentResponsesDTO> result, AssessmentResponse updatedResult)
        {
            Setup(x => x.GetAssessmentResponse(It.IsAny<int>()))
               .Returns(Task.FromResult(result));

            Setup(x => x.UpdateAssessmentResponse(It.IsAny<AssessmentResponse>(), It.IsAny<bool>()))
               .Returns(updatedResult);

            return this;
        }

        public MockAssessmentResponseRepository MockUpdateAssessmentProgress(IReadOnlyList<AssessmentResponsesDTO> result1, AssessmentResponse result, IReadOnlyList<AssessmentResponse> result3)
        {
            Setup(x => x.GetAssessmentResponse(It.IsAny<int>()))
               .Returns(Task.FromResult(result1));

            Setup(x => x.UpdateAssessmentResponse(It.IsAny<AssessmentResponse>(), It.IsAny<bool>()))
               .Returns(result);

            Setup(x => x.GetAssessmentResponseList(It.IsAny<List<int>>()))
                 .Returns(Task.FromResult(result3));

            Setup(x => x.GetAssessmentResponseListByGUID(It.IsAny<List<Guid>>()))
               .Returns(Task.FromResult(result3));
            return this;
        }

        public MockAssessmentResponseRepository GetAssessmentResponses(AssessmentResponse mockGetAssessmentResponses)
        {
            Setup(x => x.GetAssessmentResponses(It.IsAny<int>()))
               .Returns(Task.FromResult(mockGetAssessmentResponses));
            return this;
        }
        public MockAssessmentResponseRepository MockAssessmentPriority(List<AssessmentResponse> mockGetAssessmentResponses)
        {
            Setup(x => x.UpdateBulkAssessmentResponse(It.IsAny<List<AssessmentResponse>>()))
               .Returns(mockGetAssessmentResponses);
            return this;
        }
        public MockAssessmentResponseRepository MockgetAssessmentResponse1(IReadOnlyList<AssessmentResponsesDTO> result, AssessmentResponse updatedResult, List<AssessmentResponse> mockGetAssessmentResponses)
        {
            MockgetAssessmentResponse(result, updatedResult);
            MockAssessmentPriority(mockGetAssessmentResponses);
            return this;
        }

        internal Mock<IAssessmentResponseRepository> MockgetAssessmentResponseException()
        {
            Setup(x => x.UpdateBulkAssessmentResponse(It.IsAny<List<AssessmentResponse>>()))
                .Throws<Exception>();
            return this;
        }

        internal Mock<IAssessmentResponseRepository> MockGetAssessmentResponseFOrDashboardCalculation(List<AssessmentResponsesDTO> mockAssessmentResponselist)
        {
            Setup(x => x.GetAssessmentResponseFOrDashboardCalculation(It.IsAny<long>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
               .Returns(mockAssessmentResponselist);
            return this;
        }

        internal Mock<IAssessmentResponseRepository> MockGetAssessmentResponseFOrDashboardCalculationException()
        {
            Setup(x => x.GetAssessmentResponseFOrDashboardCalculation(It.IsAny<long>(), It.IsAny<int>(),It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .Throws<Exception>();
            return this;
        }

        internal Mock<IAssessmentResponseRepository> MockAddBulkAssessmentResponse(List<AssessmentResponse> mockAssessmentResponselist, IReadOnlyList<AssessmentResponse> mockAssessmentResponselist1)
        {
            Setup(x => x.GetAssessmentResponseListByGUID(It.IsAny<List<Guid>>()))
          .Returns(Task.FromResult(mockAssessmentResponselist1));

            Setup(x => x.AddBulkAssessmentResponse(It.IsAny<List<AssessmentResponse>>()))
               .Returns(mockAssessmentResponselist);
            return this;
        }
        public MockAssessmentResponseRepository MockgetAssessmentAllResponse(IReadOnlyList<AssessmentResponsesDTO> result, AssessmentResponse updatedResult)
        {
            Setup(x => x.GetAllAssessmentResponses(It.IsAny<int>()))
               .Returns(Task.FromResult(result));

            Setup(x => x.UpdateAssessmentResponse(It.IsAny<AssessmentResponse>(), It.IsAny<bool>()))
               .Returns(updatedResult);

            return this;
        }

    }
}
