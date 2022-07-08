using Moq;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.DTO.Input;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;

namespace Opeeka.PICS.UnitTests.Domain.Mocks.Repositories
{
    public class MockPersonQuestionnaireMetricsRepository : Mock<IPersonQuestionnaireMetricsRepository>
    {
        public MockPersonQuestionnaireMetricsRepository MockGetDashboardStrengthMetrics(List<DashboardStrengthMetricsDTO> result)
        {
            Setup(x => x.GetDashboardStrengthMetrics(It.IsAny<StrengthMetricsSearchDTO>(), It.IsAny<DynamicQueryBuilderDTO>()))
                .Returns(Tuple.Create(result, result.Count));
            return this;
        }

        public MockPersonQuestionnaireMetricsRepository MockGetDashboardStrengthMetricsException(List<DashboardStrengthMetricsDTO> results)
        {
            Setup(x => x.GetDashboardStrengthMetrics(It.IsAny<StrengthMetricsSearchDTO>(), It.IsAny<DynamicQueryBuilderDTO>()))
            .Throws<Exception>();
            return this;
        }

        public MockPersonQuestionnaireMetricsRepository MockGetDashboardNeedMetrics(List<DashboardNeedMetricsDTO> result)
        {
            Setup(x => x.GetDashboardNeedMetrics(It.IsAny<NeedMetricsSearchDTO>(), It.IsAny<DynamicQueryBuilderDTO>()))
              .Returns(Tuple.Create(result, result.Count));
            return this;
        }

        public MockPersonQuestionnaireMetricsRepository MockGetDashboardNeedMetricsException(List<DashboardNeedMetricsDTO> results)
        {
            Setup(x => x.GetDashboardNeedMetrics(It.IsAny<NeedMetricsSearchDTO>(), It.IsAny<DynamicQueryBuilderDTO>()))
            .Throws<Exception>();
            return this;
        }

        public MockPersonQuestionnaireMetricsRepository MockGetDashboardStrengthPiechartData(DashboardStrengthPieChartDTO result)
        {
            Setup(x => x.GetDashboardStrengthPiechartData(It.IsAny<string>(), It.IsAny<long>(), It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<int>()))
                .Returns(result);
            return this;
        }

        public MockPersonQuestionnaireMetricsRepository MockGetDashboardStrengthPiechartDataException(DashboardStrengthPieChartDTO results)
        {
            Setup(x => x.GetDashboardStrengthPiechartData(It.IsAny<string>(), It.IsAny<long>(), It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<int>()))
            .Throws<Exception>();
            return this;
        }

        public MockPersonQuestionnaireMetricsRepository MockGetDashboardNeedPiechartData(DashboardNeedPieChartDTO result)
        {
            Setup(x => x.GetDashboardNeedPiechartData(It.IsAny<string>(), It.IsAny<long>(), It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<int>()))
                .Returns(result);
            return this;
        }

        public MockPersonQuestionnaireMetricsRepository MockGetDashboardNeedPiechartDataException(DashboardNeedPieChartDTO results)
        {
            Setup(x => x.GetDashboardNeedPiechartData(It.IsAny<string>(), It.IsAny<long>(), It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<int>()))
            .Throws<Exception>();
            return this;
        }
    }
}
