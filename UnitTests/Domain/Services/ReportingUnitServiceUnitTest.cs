using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.DTO.Input;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Domain.Services;
using Opeeka.PICS.Infrastructure.Enums;
using Opeeka.PICS.UnitTests.Domain.Mocks.Repositories;
using Opeeka.PICS.Domain.Interfaces.Common;
using Xunit;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.UnitTests.Domain.Services
{
    public class ReportingUnitServiceUnitTest
    {
        /// Initializes a new instance of the agencyRepository/> class.
        private Mock<IReportingUnitRepository> mockReportingUnitRepository;
        /// Initializes a new instance of the agencyRepository/> class.
        private Mock<IAgencySharingPolicyRepository> mockAgencySharingPolicyRepository;

        /// Initializes a new instance of the addressRepository/> class.
        private Mock<IAgencySharingRepository> mockAgencySharingRepository;

        /// Initializes a new instance of the <see cref="logger"/> class.
        private Mock<ILogger<ReportingUnitService>> mockLogger;
        /// Initializes a new instance of the reportingUnitRepository/> class.
        //private Mock<IReportingUnitRepository> mockReportingUnitRepository;

        /// Initializes a new instance of the collaborationSharingRepository/> class.
        private Mock<ICollaborationSharingRepository> mockCollaborationSharingRepository;
        /// Initializes a new instance of the collaborationSharingPolicyRepository/> class.
        private Mock<ICollaborationSharingPolicyRepository> mockCollaborationSharingPolicyRepository;

        /// Initializes a new instance of the <see cref="mockAgencySharingHistroyRepository"/> class.
        private readonly Mock<IAgencySharingHistoryRepository> mockAgencySharingHistroyRepository;
        /// Initializes a new instance of the <see cref="mockCollaborationSharingHistroyRepository"/> class.
        private readonly Mock<ICollaborationSharingHistoryRepository> mockCollaborationSharingHistroyRepository;

        /// Initializes a new instance of the AgencySharingPolicyRepository/> class.
        private Mock<IConfigurationRepository> mockConfigRepository;
        private Mock<IHttpContextAccessor> httpContextAccessor;
        private Mock<IUtility> utility;

        private ReportingUnitService reportingUnitService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AgencyService"/> class.
        /// </summary>
        public ReportingUnitServiceUnitTest()
        {
            this.mockAgencySharingPolicyRepository = new Mock<IAgencySharingPolicyRepository>();
            this.mockAgencySharingRepository = new Mock<IAgencySharingRepository>();
            this.mockLogger = new Mock<ILogger<ReportingUnitService>>();
            this.mockReportingUnitRepository = new Mock<IReportingUnitRepository>();
            this.mockCollaborationSharingRepository = new Mock<ICollaborationSharingRepository>();
            this.mockCollaborationSharingPolicyRepository = new Mock<ICollaborationSharingPolicyRepository>();
            this.mockConfigRepository = new Mock<IConfigurationRepository>();
            this.httpContextAccessor = new Mock<IHttpContextAccessor>();
            this.mockCollaborationSharingHistroyRepository = new Mock<ICollaborationSharingHistoryRepository>();
            this.mockAgencySharingHistroyRepository = new Mock<IAgencySharingHistoryRepository>();

            var context = new DefaultHttpContext();
            context.Request.Headers[PCISEnum.TokenHeaders.timeZone] = "-330";
            this.httpContextAccessor.Setup(_ => _.HttpContext).Returns(context);

            this.utility = new Mock<IUtility>();
        }


        private void InitialiseUserServiceUpdationSuccess()
        {
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.UpdationSuccess);
            this.reportingUnitService = new ReportingUnitService(this.mockReportingUnitRepository.Object,
                this.mockAgencySharingRepository.Object, this.mockAgencySharingPolicyRepository.Object,
                this.mockLogger.Object, this.mockCollaborationSharingRepository.Object,
                this.mockCollaborationSharingPolicyRepository.Object, localize.Object,
                this.mockConfigRepository.Object, this.httpContextAccessor.Object, this.utility.Object, this.mockAgencySharingHistroyRepository.Object, this.mockCollaborationSharingHistroyRepository.Object);
        }
        private void InitialiseUserServiceUpdationFailed()
        {
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.UpdationFailed);
            this.reportingUnitService = new ReportingUnitService(this.mockReportingUnitRepository.Object,
                this.mockAgencySharingRepository.Object, this.mockAgencySharingPolicyRepository.Object,
                this.mockLogger.Object, this.mockCollaborationSharingRepository.Object,
                this.mockCollaborationSharingPolicyRepository.Object, localize.Object,
                this.mockConfigRepository.Object, this.httpContextAccessor.Object, this.utility.Object, this.mockAgencySharingHistroyRepository.Object, this.mockCollaborationSharingHistroyRepository.Object);
        }

        private void InitialiseUserServiceInsertionSuccess()
        {
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.InsertionSuccess);
            this.reportingUnitService = new ReportingUnitService(this.mockReportingUnitRepository.Object,
                this.mockAgencySharingRepository.Object, this.mockAgencySharingPolicyRepository.Object,
                this.mockLogger.Object, this.mockCollaborationSharingRepository.Object,
                this.mockCollaborationSharingPolicyRepository.Object, localize.Object,
                this.mockConfigRepository.Object, this.httpContextAccessor.Object, this.utility.Object, this.mockAgencySharingHistroyRepository.Object, this.mockCollaborationSharingHistroyRepository.Object);
        }
        private void InitialiseUserServiceSuccess()
        {
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Success);
            this.reportingUnitService = new ReportingUnitService(this.mockReportingUnitRepository.Object,
                this.mockAgencySharingRepository.Object, this.mockAgencySharingPolicyRepository.Object,
                this.mockLogger.Object, this.mockCollaborationSharingRepository.Object,
                this.mockCollaborationSharingPolicyRepository.Object, localize.Object,
                this.mockConfigRepository.Object, this.httpContextAccessor.Object, this.utility.Object, this.mockAgencySharingHistroyRepository.Object, this.mockCollaborationSharingHistroyRepository.Object);
        }

        //--------------------------------------------------------Add Agency--------------------------------------------------------
        [Fact]
        public void UpdatePartnerAgency_Success_ReturnsCorrectResult()
        {
            var mockAgencies = GetMockAgencySharing();
            var mockPartner = GetPartnerAgency();
            var AgencySharingPolicy = GetMockAgencySharingPolicy();

            this.mockAgencySharingRepository = new MockAgencySharingRepository().AgencySharing(mockAgencies);
            this.mockAgencySharingPolicyRepository = new MockAgencySharingPolicyRepository().AgencySharingPolicy(AgencySharingPolicy);
            InitialiseUserServiceUpdationSuccess();
            var result = this.reportingUnitService.UpdatePartnerAgency(mockPartner, 1);
            Assert.Equal(result.ResponseStatus, PCISEnum.StatusMessages.UpdationSuccess);
            //Assert.NotNull
        }




        [Fact]
        public void UpdatePartnerAgency_InvalidParameterResult()
        {
            var mockAgencies = GetMockAgencySharing();
            var mockPartner = new PartnerAgencyInputDTO();
            var AgencySharingPolicy = GetMockAgencySharingPolicy();

            this.mockAgencySharingRepository = new MockAgencySharingRepository().AgencySharing(mockAgencies);

            this.mockAgencySharingPolicyRepository = new MockAgencySharingPolicyRepository().AgencySharingPolicy(AgencySharingPolicy);
            InitialiseUserServiceUpdationFailed();
            var result = this.reportingUnitService.UpdatePartnerAgency(mockPartner, 1);
            Assert.Equal(result.ResponseStatus, PCISEnum.StatusMessages.UpdationFailed);
        }

        private PartnerAgencyInputDTO GetPartnerAgency()
        {

            PartnerAgencyInputDTO partnerAgencyInputDTO = new PartnerAgencyInputDTO()
            {
                AgencyID = 1,
                AgencySharingIndex = Guid.NewGuid(),
                ReportingUnitID = 1,
                AgencySharingPolicyID = 1,
                HistoricalView = true,
                StartDate = DateTime.UtcNow,
                EndDate = null,

            };
            return partnerAgencyInputDTO;
        }

        private AgencySharing GetMockAgencySharing()
        {
            AgencySharing agencySharingDTO = new AgencySharing()
            {
                AgencyID = 1,
                AgencySharingIndex = new Guid("23894AE8-B94A-48CC-AFC0-DB07D5E93883"),
                ReportingUnitID = 1,
                AgencySharingID = 1,
                AgencySharingPolicyID = 1,
                HistoricalView = true,
                StartDate = DateTime.UtcNow,
                EndDate = null
            };
            return agencySharingDTO;
        }

        private AgencySharingDTO GetMockAgencySharingDTO()
        {
            AgencySharingDTO agencySharingDTO = new AgencySharingDTO()
            {
                AgencyID = 1,
                AgencySharingIndex = new Guid("23894AE8-B94A-48CC-AFC0-DB07D5E93883"),
                ReportingUnitID = 1,
                AgencySharingID = 1,
                AgencySharingPolicyID = 1,
                HistoricalView = true,
                StartDate = DateTime.UtcNow,
                EndDate = null
            };
            return agencySharingDTO;
        }

        private AgencySharingPolicyDTO GetMockAgencySharingPolicy()
        {
            AgencySharingPolicyDTO agencySharingDTO = new AgencySharingPolicyDTO()
            {

                //AgencySharingID = 1,
                AgencySharingPolicyID = 1
                //SharingPolicyID = 1
            };
            return agencySharingDTO;
        }


        ///// <summary>
        ///// Initializes a new instance of the <see cref="ReportingUnitService"/> class.
        ///// </summary>
        //public ReportingUnitServiceUnitTest()
        //{
        //    this.mockReportingUnitRepository = new Mock<IReportingUnitRepository>();
        //    this.mockAgencySharingRepository = new Mock<IAgencySharingRepository>();
        //    this.mockAgencySharingPolicyRepository = new Mock<IAgencySharingPolicyRepository>();
        //    this.mockLogger = new Mock<ILogger<ReportingUnitService>>();
        //}

        //--------------------------------------------------------Add ReportingUnit--------------------------------------------------------
        //[Fact]
        //public void AddReportingUnit_Success_ReturnsCorrectResult()
        //{
        //    var mockReportingUnitInput = GetMockReportingUnitInput();
        //    var mockReportingUnit = GetMockReportingUnitDTO();
        //    var mockAgencySharing = GetMockAgencySharing();


        //    this.mockReportingUnitRepository = new MockReportingUnitRepository().MockAddReportingUnit(mockReportingUnit);
        //    this.mockAgencySharingRepository = new MockAgencySharingRepository().MockAddAgencySharing(mockAgencySharing);

        //    InitialiseUserServiceInsertionSuccess();
        //    var result = this.reportingUnitService.AddReportingUnit(mockReportingUnitInput);
        //    Assert.Equal(result.ResponseStatus, PCISEnum.StatusMessages.InsertionSuccess);
        //    //Assert.NotNull
        //}

        [Fact]
        public void AddPartnerAgency_Success_ReturnsCorrectResult()
        {
            var mockPartnerAgencyInput = GetPartnerAgency();
            var mockAgencySharing = GetMockAgencySharingDTO();
            var mockAgencySharingPolicy = GetMockAgencySharingPolicy();

            this.mockAgencySharingRepository = new MockAgencySharingRepository().MockAddAgencySharing(mockAgencySharing);
            this.mockAgencySharingPolicyRepository = new MockAgencySharingPolicyRepository().MockAddAgencySharingPolicy(mockAgencySharingPolicy);

            InitialiseUserServiceInsertionSuccess();
            var result = this.reportingUnitService.AddPartnerAgency(mockPartnerAgencyInput);
            Assert.Equal(result.ResponseStatus, PCISEnum.StatusMessages.InsertionSuccess);
            //Assert.NotNull
        }

        [Fact]
        public void AddCollaborationSharing_Success_ReturnsCorrectResult()
        {
            var mockCollaborationSharingInput = GetMockCollaborationSharingInput();
            var mockCollaborationSharing = GetMockCollaborationSharing();
            var mockCollaborationSharingPolicy = GetMockCollaborationSharingPolicy();

            this.mockCollaborationSharingRepository = new MockCollaborationSharingRepository().MockAddCollaborationSharing(mockCollaborationSharing);
            this.mockCollaborationSharingPolicyRepository = new MockCollaborationSharingPolicyRepository().MockAddCollaborationSharingPolicy(mockCollaborationSharingPolicy);

            InitialiseUserServiceInsertionSuccess();
            this.reportingUnitService.GetTimeZoneFromHeader();
            var result = this.reportingUnitService.AddCollaborationSharing(mockCollaborationSharingInput);
            Assert.Equal(result.ResponseStatus, PCISEnum.StatusMessages.InsertionSuccess);
            //Assert.NotNull
        }

        private CollaborationSharingInputDTO GetMockCollaborationSharingInput()
        {
            CollaborationSharingInputDTO collaborationSharingInputDTO = new CollaborationSharingInputDTO()
            {
                CollaborationSharingIndex = new Guid("D2B2E677-247F-4F29-9922-1E28E19B58BF"),
                ReportingUnitID = 1,
                AgencyID = 1,
                CollaborationID = 1,
                CollaborationSharingPolicyID = 1,
                HistoricalView = true,
                StartDate = DateTime.UtcNow,
                EndDate = null
            };
            return collaborationSharingInputDTO;
        }

        private CollaborationSharingDTO GetMockCollaborationSharing()
        {
            CollaborationSharingDTO collaborationSharingDTO = new CollaborationSharingDTO()
            {
                CollaborationSharingID = 1,
                CollaborationSharingIndex = new Guid("23894AE8-B94A-48CC-AFC0-DB07D5E93883"),
                ReportingUnitID = 1,
                AgencyID = 1,
                CollaborationID = 1,
                CollaborationSharingPolicyID = 1,
                HistoricalView = true,
                StartDate = DateTime.UtcNow,
                EndDate = null
            };
            return collaborationSharingDTO;
        }
        private CollaborationSharingPolicyDTO GetMockCollaborationSharingPolicy()
        {
            CollaborationSharingPolicyDTO collaborationSharingDTO = new CollaborationSharingPolicyDTO()
            {
                CollaborationSharingPolicyID = 1,
                CollaborationSharingID = 1,
                SharingPolicyID = 1
            };
            return collaborationSharingDTO;
        }

        private ReportingUnitInputDTO GetMockReportingUnitInput()
        {
            ReportingUnitInputDTO reportingUnitInputDTO = new ReportingUnitInputDTO()
            {
                Name = "Santa Clara",
                Abbrev = "SCC",
                ParentAgencyID = 1,
                UpdateUserID = 1,
                StartDate = Convert.ToDateTime("2020 -01-01 00:00:00.000"),
                EndDate = null
            };
            return reportingUnitInputDTO;
        }

        private ReportingUnit GetMockReportingUnit()
        {
            ReportingUnit reportingUnitDTO = new ReportingUnit()
            {
                ReportingUnitID = 1,
                Name = "Santa Clara",
                Abbrev = "SCC",
                ParentAgencyID = 1,
                IsRemoved = false,
                UpdateUserID = 1,
                UpdateDate = DateTime.UtcNow,
                StartDate = Convert.ToDateTime("2020 -01-01 00:00:00.000"),
                EndDate = null
            };
            return reportingUnitDTO;
        }

        private ReportingUnitDTO GetMockReportingUnitDTO()
        {
            ReportingUnitDTO reportingUnitDTO = new ReportingUnitDTO()
            {
                ReportingUnitID = 1,
                Name = "Santa Clara",
                Abbrev = "SCC",
                ParentAgencyID = 1,
                IsRemoved = false,
                UpdateUserID = 1,
                UpdateDate = DateTime.UtcNow,
                StartDate = Convert.ToDateTime("2020 -01-01 00:00:00.000"),
                EndDate = null
            };
            return reportingUnitDTO;
        }

        [Fact]
        public void GetReportUnitList_Success_ReturnsCorrectResult()
        {

            var mockReportingUnits = GetMockReportingUnits();
            this.mockReportingUnitRepository = new MockReportingUnitRepository().MockReportingUnitList(mockReportingUnits);
            InitialiseUserServiceSuccess();
            long agencyID = 1;
            int pageNumber = 1;
            int pageSize = 10;
            var result = this.reportingUnitService.GetReportingUnitList(agencyID, pageNumber, pageSize);
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.Success);
        }

        [Fact]
        public void GetReportUnitList_Success_ReturnsNoResult()
        {
            var mockReportingUnits = new List<ReportingUnitDataDTO>();
            this.mockReportingUnitRepository = new MockReportingUnitRepository().MockReportingUnitList(mockReportingUnits);
            InitialiseUserServiceInsertionSuccess();
            long agencyID = 1;
            int pageNumber = 1;
            int pageSize = 10;
            var result = this.reportingUnitService.GetReportingUnitList(agencyID, pageNumber, pageSize);
            Assert.Equal(0, result.TotalCount);
        }

        [Fact]
        public void GetReportUnitList_Failure_InvalidParameterResult()
        {
            var mockReportingUnits = new List<ReportingUnitDataDTO>();
            this.mockReportingUnitRepository = new MockReportingUnitRepository().MockReportingUnitList(mockReportingUnits);
            InitialiseUserServiceInsertionSuccess();
            long agencyID = 1;
            int pageNumber = 0;
            int pageSize = 10;
            var result = this.reportingUnitService.GetReportingUnitList(agencyID, pageNumber, pageSize);
            Assert.Equal(0, result.TotalCount);
        }

        [Fact]
        public void GetReportUnitList_ExceptionResult()
        {
            List<ReportingUnitDataDTO> mockReportingUnits = new List<ReportingUnitDataDTO>();
            this.mockReportingUnitRepository = new MockReportingUnitRepository().MockReportingUnitListException(mockReportingUnits);
            InitialiseUserServiceInsertionSuccess();
            long agencyID = 1;
            int pageNumber = 1;
            int pageSize = 10;
            Assert.ThrowsAny<Exception>(() => this.reportingUnitService.GetReportingUnitList(agencyID, pageNumber, pageSize));
        }

        private List<ReportingUnitDataDTO> GetMockReportingUnits()
        {
            return new List<ReportingUnitDataDTO>()
            {
                new ReportingUnitDataDTO()
                {
                    ReportingUnitID = 1,
                    ReportingUnitIndex = Guid.NewGuid(),
                    Name = "ABC Reporting Unit 1",
                    Abbrev = "ABC",
                    Agency = "ABC Health Agency",
                    StartDate = Convert.ToDateTime("2020 -01-01 00:00:00.000"),
                    EndDate = null
                },

                new ReportingUnitDataDTO()
                {
                    ReportingUnitID = 2,
                    ReportingUnitIndex = Guid.NewGuid(),
                    Name = "Franklin Reporting Unit 1",
                    Abbrev = "ABC",
                    Agency = "ABC Health Agency",
                    StartDate = Convert.ToDateTime("2020 -01-01 00:00:00.000"),
                    EndDate = null
                }
            };
        }

        [Fact]
        public void UpdateReporingUnit_Success()
        {
            var mockReportingUnitInput = GetMockReportingUnitEditInput();
            var mockReportingUnit = GetMockReportingUnit();

            this.mockReportingUnitRepository = new MockReportingUnitRepository().MockEditReportingUnit(mockReportingUnit);

            InitialiseUserServiceUpdationSuccess();
            var result = this.reportingUnitService.EditReportingUnit(mockReportingUnitInput);
            Assert.Equal(result.ResponseStatus, PCISEnum.StatusMessages.UpdationSuccess);
        }
        [Fact]
        public void UpdateReporingUnit_InvalidParameterResult()
        {
            var mockReportingUnitInput = new EditReportingUnitDTO();
            var mockReportingUnit = GetMockReportingUnit();

            this.mockReportingUnitRepository = new MockReportingUnitRepository().MockEditReportingUnit(mockReportingUnit);

            InitialiseUserServiceUpdationFailed();
            var result = this.reportingUnitService.EditReportingUnit(mockReportingUnitInput);
            Assert.Equal(result.ResponseStatus, PCISEnum.StatusMessages.UpdationFailed);
        }

        private EditReportingUnitDTO GetMockReportingUnitEditInput()
        {
            EditReportingUnitDTO reportingUnitInputDTO = new EditReportingUnitDTO()
            {
                ReportingUnitIndex = new Guid("589983CF-115A-4FDB-BD34-E1851B26B376"),
                Name = "Santa Clara",
                Abbrev = "SCC",
                UpdateUserID = 1,
                StartDate = Convert.ToDateTime("2020 -01-01 00:00:00.000"),
                EndDate = null
            };
            return reportingUnitInputDTO;
        }

        [Fact]
        public void GetPartnerAgencyList_Success_ReturnsCorrectResult()
        {

            var mockPartnerAgencies = GetMockPartnerAgencies();
            this.mockReportingUnitRepository = new MockReportingUnitRepository().MockPartnerAgencyList(mockPartnerAgencies);

            InitialiseUserServiceSuccess();
            int reportingUnitID = 1;
            int pageNumber = 1;
            int pageSize = 10;
            var result = this.reportingUnitService.GetPartnerAgencyList(reportingUnitID, pageNumber, pageSize);
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.Success);
        }

        [Fact]
        public void GetPartnerAgencyList_Success_ReturnsNoResult()
        {
            var mockPartnerAgencies = new List<PartnerAgencyDataDTO>();
            this.mockReportingUnitRepository = new MockReportingUnitRepository().MockPartnerAgencyList(mockPartnerAgencies);
            InitialiseUserServiceInsertionSuccess();
            int reportingUnitID = 1;
            int pageNumber = 1;
            int pageSize = 10;
            var result = this.reportingUnitService.GetPartnerAgencyList(reportingUnitID, pageNumber, pageSize);
            Assert.Null(result.PartnerAgencyList);
        }

        [Fact]
        public void GetPartnerAgencyList_Failure_InvalidParameterResult()
        {
            var mockPartnerAgencies = new List<PartnerAgencyDataDTO>();
            this.mockReportingUnitRepository = new MockReportingUnitRepository().MockPartnerAgencyList(mockPartnerAgencies);
            InitialiseUserServiceInsertionSuccess();

            int reportingUnitID = 1;
            int pageNumber = 0;
            int pageSize = 10;
            var result = this.reportingUnitService.GetPartnerAgencyList(reportingUnitID, pageNumber, pageSize);
            Assert.Null(result.PartnerAgencyList);
        }

        [Fact]
        public void GetPartnerAgencyList_ExceptionResult()
        {
            List<PartnerAgencyDataDTO> mockPartnerAgencies = new List<PartnerAgencyDataDTO>();
            this.mockReportingUnitRepository = new MockReportingUnitRepository().MockPartnerAgencyListException(mockPartnerAgencies);
            InitialiseUserServiceInsertionSuccess();
            int reportingUnitID = 1;
            int pageNumber = 1;
            int pageSize = 10;
            Assert.ThrowsAny<Exception>(() => this.reportingUnitService.GetPartnerAgencyList(reportingUnitID, pageNumber, pageSize));
        }

        private List<PartnerAgencyDataDTO> GetMockPartnerAgencies()
        {
            return new List<PartnerAgencyDataDTO>()
            {
                new PartnerAgencyDataDTO()
                {
                    AgencySharingID = 1,
                    ReportingUnitID = 1,
                    Agency = "ABC Health Agency",
                    AgencySharingPolicyID = 1,
                    StartDate = Convert.ToDateTime("2020 -01-01 00:00:00.000"),
                    EndDate = null,
                    HistoricalView = true,
                    AccessName = "Read/Write",
                    SharingPolicyID = 1,
                    AgencyID = 1
                },

                new PartnerAgencyDataDTO()
                {
                    AgencySharingID = 2,
                    ReportingUnitID = 1,
                    Agency = "Franklin County",
                    AgencySharingPolicyID = 2,
                    StartDate = Convert.ToDateTime("2020 -01-01 00:00:00.000"),
                    EndDate = null,
                    HistoricalView = true,
                    AccessName = "Read Only",
                    SharingPolicyID = 1,
                    AgencyID = 2
                }
            };
        }

        [Fact]
        public void GetRUCollaborationList_Success_ReturnsCorrectResult()
        {

            var mockCollaborations = GetMockCollaborations();
            this.mockReportingUnitRepository = new MockReportingUnitRepository().MockRUCollaborationList(mockCollaborations);
            InitialiseUserServiceSuccess();

            long agencyID = 1;
            int reportingUnitID = 1;
            int pageNumber = 1;
            int pageSize = 10;
            var result = this.reportingUnitService.GetRUCollaborationList(agencyID, reportingUnitID, pageNumber, pageSize);
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.Success);
        }

        [Fact]
        public void GetRUCollaborationList_Success_ReturnsNoResult()
        {
            var mockCollaborations = new List<RUCollaborationDataDTO>();
            this.mockReportingUnitRepository = new MockReportingUnitRepository().MockRUCollaborationList(mockCollaborations);
            InitialiseUserServiceSuccess();

            long agencyID = 1;
            int reportingUnitID = 1;
            int pageNumber = 1;
            int pageSize = 10;
            var result = this.reportingUnitService.GetRUCollaborationList(agencyID, reportingUnitID, pageNumber, pageSize);
            Assert.Null(result.CollaborationList);
        }

        [Fact]
        public void GetRUCollaborationList_Failure_InvalidParameterResult()
        {
            var mockCollaborations = new List<RUCollaborationDataDTO>();
            this.mockReportingUnitRepository = new MockReportingUnitRepository().MockRUCollaborationList(mockCollaborations);
            InitialiseUserServiceSuccess();

            long agencyID = 1;
            int reportingUnitID = 1;
            int pageNumber = 0;
            int pageSize = 10;
            var result = this.reportingUnitService.GetRUCollaborationList(agencyID, reportingUnitID, pageNumber, pageSize);
            Assert.Null(result.CollaborationList);
        }

        [Fact]
        public void GetRUCollaborationList_ExceptionResult()
        {
            List<RUCollaborationDataDTO> mockCollaborations = new List<RUCollaborationDataDTO>();
            this.mockReportingUnitRepository = new MockReportingUnitRepository().MockRUCollaborationListException(mockCollaborations);
            InitialiseUserServiceSuccess();

            long agencyID = 1;
            int reportingUnitID = 1;
            int pageNumber = 1;
            int pageSize = 10;
            Assert.ThrowsAny<Exception>(() => this.reportingUnitService.GetRUCollaborationList(agencyID, reportingUnitID, pageNumber, pageSize));
        }

        private List<RUCollaborationDataDTO> GetMockCollaborations()
        {
            return new List<RUCollaborationDataDTO>()
            {
                new RUCollaborationDataDTO()
                {
                    CollaborationSharingID = 1,
                    CollaborationSharingIndex = Guid.NewGuid(),
                    CollaborationSharingPolicyID = 1,
                    HistoricalView = true,
                    StartDate = Convert.ToDateTime("2020 -01-01 00:00:00.000"),
                    EndDate = null,
                    IsActive = true,
                    CollaborationID = 1,
                    Collaboration = "ABC Collaboration",
                    AccessName = "Read/Write",
                     AgencyID = 1,
                    Agency = "ABC Health Agency"
                },

                new RUCollaborationDataDTO()
                {
                    CollaborationSharingID = 1,
                    CollaborationSharingIndex = Guid.NewGuid(),
                    CollaborationSharingPolicyID = 1,
                    HistoricalView = true,
                    StartDate = Convert.ToDateTime("2020 -01-01 00:00:00.000"),
                    EndDate = null,
                    IsActive = true,
                    CollaborationID = 1,
                    Collaboration = "ABC Collaboration",
                    AccessName = "Read/Write",
                    AgencyID = 1,
                    Agency = "ABC Health Agency"
                }
            };
        }

        [Fact]
        public void UpdateCollaborationSharing_Success_ReturnsCorrectResult()
        {
            var mockCollaborationSharingInput = GetMockCollaborationSharingInput();
            var mockCollaborationSharing = GetMockCollaborationSharing();
            var mockCollaborationSharingPolicy = GetMockCollaborationSharingPolicy();

            this.mockCollaborationSharingRepository = new MockCollaborationSharingRepository().MockEditCollaborationSharing(mockCollaborationSharing);
            this.mockCollaborationSharingPolicyRepository = new MockCollaborationSharingPolicyRepository().MockEditCollaborationSharingPolicy(mockCollaborationSharingPolicy);

            InitialiseUserServiceUpdationSuccess();

            var result = this.reportingUnitService.EditCollaborationSharing(mockCollaborationSharingInput, 1);
            Assert.Equal(result.ResponseStatus, PCISEnum.StatusMessages.UpdationSuccess);
            //Assert.NotNull
        }
        [Fact]
        public void UpdateCollaborationSharing_Failure_InvalidParameterResult()
        {
            var mockCollaborationSharingInput = new CollaborationSharingInputDTO();
            var mockCollaborationSharing = GetMockCollaborationSharing();
            var mockCollaborationSharingPolicy = GetMockCollaborationSharingPolicy();

            this.mockCollaborationSharingRepository = new MockCollaborationSharingRepository().MockEditCollaborationSharing(mockCollaborationSharing);
            this.mockCollaborationSharingPolicyRepository = new MockCollaborationSharingPolicyRepository().MockEditCollaborationSharingPolicy(mockCollaborationSharingPolicy);

            InitialiseUserServiceUpdationFailed();

            var result = this.reportingUnitService.EditCollaborationSharing(mockCollaborationSharingInput, 1);
            Assert.Equal(result.ResponseStatus, PCISEnum.StatusMessages.UpdationFailed);
            //Assert.NotNull
        }
    }
}
