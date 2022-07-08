using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.DTO.Input;
using Opeeka.PICS.Domain.Interfaces.Common;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Domain.Services;
using Opeeka.PICS.Infrastructure.Enums;
using Opeeka.PICS.UnitTests.Domain.Mocks.Common;
using Opeeka.PICS.UnitTests.Domain.Mocks.Repositories;
using System;
using System.Collections.Generic;
using Xunit;

namespace Opeeka.PICS.UnitTests.Domain.Services
{
    public class PersonQuestionnaireMetricsServiceUnitTest
    {
        private Mock<IPersonQuestionnaireMetricsRepository> mockPersonQuestionnaireMetricsRepository;
        /// Initializes a new instance of the <see cref="logger"/> class.
        private Mock<ILogger<OptionsService>> mockLogger;
        private PersonQuestionnaireMetricsService personQuestionnaireMetricsService;
        private Mock<IConfigurationRepository> mockConfigRepository;
        private Mock<IHttpContextAccessor> httpContextAccessor;
        private Mock<IQueryBuilder> querybuild;
        private Mock<IMapper> mapper;
        private Mock<IPersonAssessmentMetricsRepository> mockPersonAssessmentMetricsRepository;

        public PersonQuestionnaireMetricsServiceUnitTest()
        {
            this.mockPersonQuestionnaireMetricsRepository = new Mock<IPersonQuestionnaireMetricsRepository>();
            this.mapper = new Mock<IMapper>();
            this.mockLogger = new Mock<ILogger<OptionsService>>();
            this.mockConfigRepository = new Mock<IConfigurationRepository>();
            this.httpContextAccessor = new Mock<IHttpContextAccessor>();
            var context = new DefaultHttpContext();
            context.Request.Headers[PCISEnum.TokenHeaders.timeZone] = "-330";
            this.httpContextAccessor.Setup(_ => _.HttpContext).Returns(context);
            this.querybuild = new MockQueryBuilder().SetupQueryBuilder(new DynamicQueryBuilderDTO());
            this.mockPersonAssessmentMetricsRepository = new Mock<IPersonAssessmentMetricsRepository>();
        }

        #region Dashboard Strength Metrics
        [Fact]
        public void GetDashboardStrengthMetrics_Success_ReturnsCorrectResult()
        {
            var mockStrengthMetrics = GetMockStrengthMetrics();
            this.mockPersonQuestionnaireMetricsRepository = new MockPersonQuestionnaireMetricsRepository().MockGetDashboardStrengthMetrics(mockStrengthMetrics);
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Success);
            this.querybuild = new MockQueryBuilder().SetupQueryBuilder(new DynamicQueryBuilderDTO()
            {
                OrderBy = " order by [TOP] desc",
                Page = 1,
                PageSize = 10,
                Paginate = " OFFSET 0 ROWS FETCH NEXT 5 ROWS ONLY"
            });
            this.personQuestionnaireMetricsService = new PersonQuestionnaireMetricsService(this.mapper.Object,this.mockPersonQuestionnaireMetricsRepository.Object, localize.Object, mockConfigRepository.Object, httpContextAccessor.Object, this.querybuild.Object, this.mockPersonAssessmentMetricsRepository.Object);
            List<string> roles = new List<string>() { "Super Admin" };
            var strengthMetricsSearchDTO = new StrengthMetricsSearchDTO()
            {
                agencyID = 1,
                SearchFilter = "{\"filters\":[],\"orderby\":{\"field\":\"top\",\"value\":\"asc\"},\"page\":1,\"size\":20}",
                userRole = roles,
                helperID = 1
            };
            var result = this.personQuestionnaireMetricsService.GetDashboardStrengthMetrics(strengthMetricsSearchDTO);
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.Success);
        }

        [Fact]
        public void GetDashboardStrengthMetrics_Success_ReturnsNoResult()
        {
            var mockStrengthMetrics = GetMockStrengthMetrics();
            this.mockPersonQuestionnaireMetricsRepository = new MockPersonQuestionnaireMetricsRepository().MockGetDashboardStrengthMetrics(mockStrengthMetrics);
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Success);
            this.querybuild = new MockQueryBuilder().SetupQueryBuilder(new DynamicQueryBuilderDTO()
            {
                OrderBy = " order by [TOP] desc",
                Page = 0,
                PageSize = 10,
                Paginate = " OFFSET 0 ROWS FETCH NEXT 5 ROWS ONLY"
            });
            this.personQuestionnaireMetricsService = new PersonQuestionnaireMetricsService(this.mapper.Object, this.mockPersonQuestionnaireMetricsRepository.Object, localize.Object, mockConfigRepository.Object, httpContextAccessor.Object, this.querybuild.Object, this.mockPersonAssessmentMetricsRepository.Object);
            List<string> roles = new List<string>() { "Super Admin" };
            var strengthMetricsSearchDTO = new StrengthMetricsSearchDTO()
            {
                agencyID = 1,
                SearchFilter = "{\"filters\":[],\"orderby\":{\"field\":\"top\",\"value\":\"asc\"},\"page\":1,\"size\":20}",
                userRole = roles,
                helperID = 1
            };
            var result = this.personQuestionnaireMetricsService.GetDashboardStrengthMetrics(strengthMetricsSearchDTO);
            Assert.Equal(0, result.TotalCount);
        }

        [Fact]
        public void GetDashboardStrengthMetrics_Failure_InvalidParameterResult()
        {
            var mockStrengthMetrics = GetMockStrengthMetrics();
            this.mockPersonQuestionnaireMetricsRepository = new MockPersonQuestionnaireMetricsRepository().MockGetDashboardStrengthMetrics(mockStrengthMetrics);
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Success);
            this.querybuild = new MockQueryBuilder().SetupQueryBuilder(new DynamicQueryBuilderDTO()
            {
                OrderBy = " order by [TOP] desc",
                Page = 0,
                PageSize = 10,
                Paginate = " OFFSET 0 ROWS FETCH NEXT 5 ROWS ONLY"
            });
            this.personQuestionnaireMetricsService = new PersonQuestionnaireMetricsService(this.mapper.Object, this.mockPersonQuestionnaireMetricsRepository.Object, localize.Object, mockConfigRepository.Object, httpContextAccessor.Object, this.querybuild.Object, this.mockPersonAssessmentMetricsRepository.Object);
            List<string> roles = new List<string>() { "Super Admin" };
            var strengthMetricsSearchDTO = new StrengthMetricsSearchDTO()
            {
                agencyID = 1,
                SearchFilter = "{\"filters\":[],\"orderby\":{\"field\":\"top\",\"value\":\"asc\"},\"page\":0,\"size\":10}",
                userRole = roles,
                helperID = 0
            };
            var result = this.personQuestionnaireMetricsService.GetDashboardStrengthMetrics(strengthMetricsSearchDTO);
            Assert.Null(result.DashboardStrengthMetricsList);
        }

        [Fact]
        public void GetDashboardStrengthMetrics_Failure_ExceptionResult()
        {
            var mockStrengthMetrics = GetMockStrengthMetrics();
            this.mockPersonQuestionnaireMetricsRepository = new MockPersonQuestionnaireMetricsRepository().MockGetDashboardStrengthMetricsException(mockStrengthMetrics);
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Success);
            this.querybuild = new MockQueryBuilder().SetupQueryBuilder(new DynamicQueryBuilderDTO()
            {
                OrderBy = " order by [TOP] desc",
                Page = 1,
                PageSize = 10,
                Paginate = " OFFSET 0 ROWS FETCH NEXT 5 ROWS ONLY"
            });
            this.personQuestionnaireMetricsService = new PersonQuestionnaireMetricsService(this.mapper.Object, this.mockPersonQuestionnaireMetricsRepository.Object, localize.Object, mockConfigRepository.Object, httpContextAccessor.Object, this.querybuild.Object, this.mockPersonAssessmentMetricsRepository.Object);
            List<string> roles = new List<string>() { "Super Admin" };
            var strengthMetricsSearchDTO = new StrengthMetricsSearchDTO()
            {
                agencyID = 1,
                SearchFilter = "{\"filters\":[],\"orderby\":{\"field\":\"top\",\"value\":\"asc\"},\"page\":1,\"size\":10}",
                userRole = roles,
                helperID = 1
            };
            Assert.ThrowsAny<Exception>(() => this.personQuestionnaireMetricsService.GetDashboardStrengthMetrics(strengthMetricsSearchDTO));
        }
        #endregion

        #region Dashboard Need Metrics
        [Fact]
        public void GetDashboardNeedMetrics_Success_ReturnsCorrectResult()
        {
            var mockNeedMetrics = GetMockNeedMetrics();
            this.mockPersonQuestionnaireMetricsRepository = new MockPersonQuestionnaireMetricsRepository().MockGetDashboardNeedMetrics(mockNeedMetrics);
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Success);
            this.querybuild = new MockQueryBuilder().SetupQueryBuilder(new DynamicQueryBuilderDTO()
            {
                OrderBy = " order by [TOP] desc",
                Page = 1,
                PageSize = 10,
                Paginate = " OFFSET 0 ROWS FETCH NEXT 5 ROWS ONLY"
            });
            this.personQuestionnaireMetricsService = new PersonQuestionnaireMetricsService(this.mapper.Object, this.mockPersonQuestionnaireMetricsRepository.Object, localize.Object, mockConfigRepository.Object, httpContextAccessor.Object, this.querybuild.Object, this.mockPersonAssessmentMetricsRepository.Object);
            List<string> roles = new List<string>() { "Super Admin" };
            var needMetricsSearchDTO = new NeedMetricsSearchDTO()
            {
                agencyID = 1,
                SearchFilter = "{\"filters\":[],\"orderby\":{\"field\":\"top\",\"value\":\"asc\"},\"page\":1,\"size\":20}",
                userRole = roles,
                helperID = 1
            };
            var result = this.personQuestionnaireMetricsService.GetDashboardNeedMetrics(needMetricsSearchDTO);
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.Success);
        }

        [Fact]
        public void GetDashboardNeedMetrics_Success_ReturnsNoResult()
        {
            var mockNeedMetrics = GetMockNeedMetrics();
            this.mockPersonQuestionnaireMetricsRepository = new MockPersonQuestionnaireMetricsRepository().MockGetDashboardNeedMetrics(mockNeedMetrics);
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Success);
            this.querybuild = new MockQueryBuilder().SetupQueryBuilder(new DynamicQueryBuilderDTO()
            {
                OrderBy = " order by [TOP] desc",
                Page = 0,
                PageSize = 10,
                Paginate = " OFFSET 0 ROWS FETCH NEXT 5 ROWS ONLY"
            });
            this.personQuestionnaireMetricsService = new PersonQuestionnaireMetricsService(this.mapper.Object, this.mockPersonQuestionnaireMetricsRepository.Object, localize.Object, mockConfigRepository.Object, httpContextAccessor.Object, this.querybuild.Object, this.mockPersonAssessmentMetricsRepository.Object);
            List<string> roles = new List<string>() { "Super Admin" };
            var needMetricsSearchDTO = new NeedMetricsSearchDTO()
            {
                agencyID = 1,
                SearchFilter = "{\"filters\":[],\"orderby\":{\"field\":\"top\",\"value\":\"asc\"},\"page\":1,\"size\":20}",
                userRole = roles,
                helperID = 1
            };
            var result = this.personQuestionnaireMetricsService.GetDashboardNeedMetrics(needMetricsSearchDTO);
            Assert.Equal(0, result.TotalCount);
        }

        [Fact]
        public void GetDashboardNeedMetrics_Failure_InvalidParameterResult()
        {
            var mockNeedMetrics = GetMockNeedMetrics();
            this.mockPersonQuestionnaireMetricsRepository = new MockPersonQuestionnaireMetricsRepository().MockGetDashboardNeedMetrics(mockNeedMetrics);
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Success);
            this.querybuild = new MockQueryBuilder().SetupQueryBuilder(new DynamicQueryBuilderDTO()
            {
                OrderBy = " order by [TOP] desc",
                Page = 0,
                PageSize = 10,
                Paginate = " OFFSET 0 ROWS FETCH NEXT 5 ROWS ONLY"
            });
            this.personQuestionnaireMetricsService = new PersonQuestionnaireMetricsService(this.mapper.Object, this.mockPersonQuestionnaireMetricsRepository.Object, localize.Object, mockConfigRepository.Object, httpContextAccessor.Object, this.querybuild.Object, this.mockPersonAssessmentMetricsRepository.Object);
            List<string> roles = new List<string>() { "Super Admin" };
            var needMetricsSearchDTO = new NeedMetricsSearchDTO()
            {
                agencyID = 1,
                SearchFilter = "{\"filters\":[],\"orderby\":{\"field\":\"top\",\"value\":\"asc\"},\"page\":1,\"size\":20}",
                userRole = roles,
                helperID = 1
            };
            var result = this.personQuestionnaireMetricsService.GetDashboardNeedMetrics(needMetricsSearchDTO);
            Assert.Null(result.DashboardNeedMetricsList);
        }

        [Fact]
        public void GetDashboardNeedMetrics_Failure_ExceptionResult()
        {
            var mockNeedMetrics = GetMockNeedMetrics();
            this.mockPersonQuestionnaireMetricsRepository = new MockPersonQuestionnaireMetricsRepository().MockGetDashboardNeedMetricsException(mockNeedMetrics);
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Success);
            this.querybuild = new MockQueryBuilder().SetupQueryBuilder(new DynamicQueryBuilderDTO()
            {
                OrderBy = " order by [TOP] desc",
                Page = 1,
                PageSize = 10,
                Paginate = " OFFSET 0 ROWS FETCH NEXT 5 ROWS ONLY"
            });
            this.personQuestionnaireMetricsService = new PersonQuestionnaireMetricsService(this.mapper.Object, this.mockPersonQuestionnaireMetricsRepository.Object, localize.Object, mockConfigRepository.Object, httpContextAccessor.Object, this.querybuild.Object, this.mockPersonAssessmentMetricsRepository.Object);
            List<string> roles = new List<string>() { "Super Admin" };
            var needMetricsSearchDTO = new NeedMetricsSearchDTO()
            {
                agencyID = 1,
                SearchFilter = "{\"filters\":[],\"orderby\":{\"field\":\"top\",\"value\":\"asc\"},\"page\":1,\"size\":20}",
                userRole = roles,
                helperID = 1
            };
            Assert.ThrowsAny<Exception>(() => this.personQuestionnaireMetricsService.GetDashboardNeedMetrics(needMetricsSearchDTO));
        }
        #endregion

        #region Dashboard Strength Pie Chart
        [Fact]
        public void GetDashboardStrengthPieChartData_Success_ReturnsCorrectResult()
        {
            var mockStrengthPieChart = GetMockStrengthPieChartData();
            this.mockPersonQuestionnaireMetricsRepository = new MockPersonQuestionnaireMetricsRepository().MockGetDashboardStrengthPiechartData(mockStrengthPieChart);
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Success);
            this.personQuestionnaireMetricsService = new PersonQuestionnaireMetricsService(this.mapper.Object, this.mockPersonQuestionnaireMetricsRepository.Object, localize.Object, mockConfigRepository.Object, httpContextAccessor.Object, this.querybuild.Object, this.mockPersonAssessmentMetricsRepository.Object);
            long agencyID = 1;
            List<string> roles = new List<string>() { "Super Admin" };
            int helperID = 1;
            var result = this.personQuestionnaireMetricsService.GetDashboardStrengthPiechartData(helperID, agencyID, roles, false, 1);
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.Success);
        }

        [Fact]
        public void GetDashboardStrengthPieChartData_Success_ReturnsNoResult()
        {
            var mockStrengthPieChart = GetMockStrengthPieChartData();
            this.mockPersonQuestionnaireMetricsRepository = new MockPersonQuestionnaireMetricsRepository().MockGetDashboardStrengthPiechartData(null);
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Success);
            this.personQuestionnaireMetricsService = new PersonQuestionnaireMetricsService(this.mapper.Object, this.mockPersonQuestionnaireMetricsRepository.Object, localize.Object, mockConfigRepository.Object, httpContextAccessor.Object, this.querybuild.Object, this.mockPersonAssessmentMetricsRepository.Object);
            long agencyID = 1;
            List<string> roles = new List<string>() { "Super Admin" };
            int helperID = 1;
            var result = this.personQuestionnaireMetricsService.GetDashboardStrengthPiechartData(helperID, agencyID, roles, false, 1);
            Assert.Null(result.DashboardStrengthPieChartData);
        }

        [Fact]
        public void GetDashboardStrengthPieChartData_Failure_InvalidParameterResult()
        {
            var mockStrengthPieChart = GetMockStrengthPieChartData();
            this.mockPersonQuestionnaireMetricsRepository = new MockPersonQuestionnaireMetricsRepository().MockGetDashboardStrengthPiechartData(null);
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Success);
            this.personQuestionnaireMetricsService = new PersonQuestionnaireMetricsService(this.mapper.Object, this.mockPersonQuestionnaireMetricsRepository.Object, localize.Object, mockConfigRepository.Object, httpContextAccessor.Object, this.querybuild.Object, this.mockPersonAssessmentMetricsRepository.Object);
            long agencyID = 1;
            List<string> roles = new List<string>() { "Super Admin" };
            int helperID = 1;
            var result = this.personQuestionnaireMetricsService.GetDashboardStrengthPiechartData(helperID, agencyID, roles, false, 1);
            Assert.Null(result.DashboardStrengthPieChartData);
        }

        [Fact]
        public void GetDashboardStrengthPieChartData_Failure_ExceptionResult()
        {
            var mockStrengthPieChart = GetMockStrengthPieChartData();
            this.mockPersonQuestionnaireMetricsRepository = new MockPersonQuestionnaireMetricsRepository().MockGetDashboardStrengthPiechartDataException(mockStrengthPieChart);
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Success);
            this.personQuestionnaireMetricsService = new PersonQuestionnaireMetricsService(this.mapper.Object, this.mockPersonQuestionnaireMetricsRepository.Object, localize.Object, mockConfigRepository.Object, httpContextAccessor.Object, this.querybuild.Object, this.mockPersonAssessmentMetricsRepository.Object);
            long agencyID = 1;
            List<string> roles = new List<string>() { "Super Admin" };
            int helperID = 1;
            Assert.ThrowsAny<Exception>(() => this.personQuestionnaireMetricsService.GetDashboardStrengthPiechartData(helperID, agencyID, roles, false, 1));
        }
        #endregion

        #region Dashboard Need Pie Chart
        [Fact]
        public void GetDashboardNeedPieChartData_Success_ReturnsCorrectResult()
        {
            var mockNeedPieChart = GetMockNeedPieChartData();
            this.mockPersonQuestionnaireMetricsRepository = new MockPersonQuestionnaireMetricsRepository().MockGetDashboardNeedPiechartData(mockNeedPieChart);
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Success);
            this.personQuestionnaireMetricsService = new PersonQuestionnaireMetricsService(this.mapper.Object, this.mockPersonQuestionnaireMetricsRepository.Object, localize.Object, mockConfigRepository.Object, httpContextAccessor.Object, this.querybuild.Object, this.mockPersonAssessmentMetricsRepository.Object);
            long agencyID = 1;
            List<string> roles = new List<string>() { "Super Admin" };
            int helperID = 1;
            var result = this.personQuestionnaireMetricsService.GetDashboardNeedPiechartData(helperID, agencyID, roles, false, 1);
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.Success);
        }

        [Fact]
        public void GetDashboardNeedPieChartData_Success_ReturnsNoResult()
        {
            var mockNeedPieChart = GetMockNeedPieChartData();
            this.mockPersonQuestionnaireMetricsRepository = new MockPersonQuestionnaireMetricsRepository().MockGetDashboardNeedPiechartData(null);
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Success);
            this.personQuestionnaireMetricsService = new PersonQuestionnaireMetricsService(this.mapper.Object, this.mockPersonQuestionnaireMetricsRepository.Object, localize.Object, mockConfigRepository.Object, httpContextAccessor.Object, this.querybuild.Object, this.mockPersonAssessmentMetricsRepository.Object);
            long agencyID = 1;
            List<string> roles = new List<string>() { "Super Admin" };
            int helperID = 1;
            var result = this.personQuestionnaireMetricsService.GetDashboardNeedPiechartData(helperID, agencyID, roles, false, 1);
            Assert.Null(result.DashboardNeedPieChartData);
        }

        [Fact]
        public void GetDashboardNeedPieChartData_Failure_InvalidParameterResult()
        {
            var mockNeedPieChart = GetMockNeedPieChartData();
            this.mockPersonQuestionnaireMetricsRepository = new MockPersonQuestionnaireMetricsRepository().MockGetDashboardNeedPiechartData(null);
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Success);
            this.personQuestionnaireMetricsService = new PersonQuestionnaireMetricsService(this.mapper.Object, this.mockPersonQuestionnaireMetricsRepository.Object, localize.Object, mockConfigRepository.Object, httpContextAccessor.Object, this.querybuild.Object, this.mockPersonAssessmentMetricsRepository.Object);
            long agencyID = 1;
            List<string> roles = new List<string>() { "Super Admin" };
            int helperID = 1;
            var result = this.personQuestionnaireMetricsService.GetDashboardNeedPiechartData(helperID, agencyID, roles, false, 1);
            Assert.Null(result.DashboardNeedPieChartData);
        }

        [Fact]
        public void GetDashboardNeedPieChartData_Failure_ExceptionResult()
        {
            var mockNeedPieChart = GetMockNeedPieChartData();
            this.mockPersonQuestionnaireMetricsRepository = new MockPersonQuestionnaireMetricsRepository().MockGetDashboardNeedPiechartDataException(mockNeedPieChart);
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Success);
            this.personQuestionnaireMetricsService = new PersonQuestionnaireMetricsService(this.mapper.Object, this.mockPersonQuestionnaireMetricsRepository.Object, localize.Object, mockConfigRepository.Object, httpContextAccessor.Object, this.querybuild.Object, this.mockPersonAssessmentMetricsRepository.Object);
            long agencyID = 1;
            List<string> roles = new List<string>() { "Super Admin" };
            int helperID = 1;
            Assert.ThrowsAny<Exception>(() => this.personQuestionnaireMetricsService.GetDashboardNeedPiechartData(helperID, agencyID, roles, false, 1));
        }
        #endregion

        #region Mock Data
        private List<DashboardStrengthMetricsDTO> GetMockStrengthMetrics()
        {
            return new List<DashboardStrengthMetricsDTO>()
            {
                new DashboardStrengthMetricsDTO()
                {
                    Top=1,
                    Item="Test Item 1",
                    Instrument="TEST",
                    ItemID=1,
                    InstrumentID=1,
                    Helping=1,
                    Improved=1
                },
                new DashboardStrengthMetricsDTO()
                {
                    Top=2,
                    Item="Test Item 2",
                    Instrument="TEST2",
                    ItemID=2,
                    InstrumentID=2,
                    Helping=2,
                    Improved=2
                }
            };
        }
        private List<DashboardNeedMetricsDTO> GetMockNeedMetrics()
        {
            return new List<DashboardNeedMetricsDTO>()
            {
                new DashboardNeedMetricsDTO()
                {
                    Top=1,
                    Item="Test Item 1",
                    Instrument="TEST",
                    ItemID=1,
                    InstrumentID=1,
                    Helping=1,
                    Improved=1
                },
                new DashboardNeedMetricsDTO()
                {
                    Top=2,
                    Item="Test Item 2",
                    Instrument="TEST2",
                    ItemID=2,
                    InstrumentID=2,
                    Helping=2,
                    Improved=2
                }
            };
        }
        private DashboardStrengthPieChartDTO GetMockStrengthPieChartData()
        {
            return new DashboardStrengthPieChartDTO()
            {
                Built = 50,
                ToBeBuilt = 50
            };
        }
        private DashboardNeedPieChartDTO GetMockNeedPieChartData()
        {
            return new DashboardNeedPieChartDTO()
            {
                Addressing = 50,
                Improved = 50
            };
        }
        #endregion
    }
}
