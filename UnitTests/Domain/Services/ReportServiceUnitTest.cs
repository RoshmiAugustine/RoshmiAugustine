using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Moq;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.DTO.Input;
using Opeeka.PICS.Domain.DTO.Output;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Common;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Domain.Services;
using Opeeka.PICS.Infrastructure.Enums;
using Opeeka.PICS.UnitTests.Domain.Mocks.Repositories;
using Xunit;

namespace Opeeka.PICS.UnitTests.Domain.Services
{

    public class ReportServiceUnitTest
    {
        private ReportService reportService;
        private Mock<IReportRepository> mockReportRepository;
        private Mock<IPersonRepository> mockPersonRepository;
        private Mock<IAssessmentRepository> mockAssessmentRepository;
        private Mock<IConfiguration> mockConfig;
        private Mock<IUtility> mockUtility;
        private Mock<IMapper> mockMapper;
        private Mock<IAgencyPowerBIReportRepository> mockAgencyPowerBIReportRepository;
        private Mock<IConfigurationRepository> mockConfigurationRepository;
        public ReportServiceUnitTest()
        {
            this.mockReportRepository = new Mock<IReportRepository>();
            this.mockPersonRepository = new Mock<IPersonRepository>();
            this.mockAssessmentRepository = new Mock<IAssessmentRepository>();
            this.mockConfig = new Mock<IConfiguration>();
            this.mockUtility = new Mock<IUtility>();
            this.mockMapper = new Mock<IMapper>();
            this.mockAgencyPowerBIReportRepository = new Mock<IAgencyPowerBIReportRepository>();
            this.mockConfigurationRepository = new Mock<IConfigurationRepository>();
        }

        private void InitialiseUserService()
        {
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.InsertionSuccess);
            this.reportService = new ReportService(localize.Object, this.mockReportRepository.Object, this.mockPersonRepository.Object,
                           this.mockAssessmentRepository.Object, null, this.mockAgencyPowerBIReportRepository.Object, this.mockMapper.Object,
                           this.mockConfigurationRepository.Object, this.mockConfig.Object, this.mockUtility.Object);
        }

        private ReportInputDTO GetReportInputDTO()
        {
            return new ReportInputDTO
            {
                CollaborationId = 1,
                PersonIndex = Guid.NewGuid(),
                QuestionnaireId = 5,
                VoiceTypeID = 1,
                AssessmentID = 1,
                PersonCollaborationID = 1,
                PersonQuestionnaireID = 1
            };
        }

        #region Get Item Details Report
        [Fact]
        public void GetItemReportData_Success_ReturnsCorrectResult()
        {
            List<ItemDetailsDTO> mockUpperpaneSearch = (List<ItemDetailsDTO>)this.GetItemReportDataResults();
            UserTokenDetails userTokenDetails = new UserTokenDetails() { AgencyID = 0, UserID = 0 };
            this.mockPersonRepository = new MockPersonRepository().MockGetPersonIDS(new PeopleDTO(), new SharedDetailsDTO() { PersonID = 0 });
            this.mockReportRepository = new MockReportRepository().MockGetItemReportData(mockUpperpaneSearch);
            InitialiseUserService();
            var result = this.reportService.GetItemReportData(GetReportInputDTO(), userTokenDetails);
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.Success);
        }

        [Fact]
        public void GetItemReportData_Success_ReturnsNoResult()
        {
            var mockUpperpaneSearch = new List<ItemDetailsDTO>();
            this.mockPersonRepository = new MockPersonRepository().MockGetPersonIDS(new PeopleDTO(), new SharedDetailsDTO() { PersonID = 0});
            this.mockReportRepository = new MockReportRepository().MockGetItemReportData(mockUpperpaneSearch);
            InitialiseUserService();
            UserTokenDetails userTokenDetails = new UserTokenDetails() { UserID = 0, AgencyID = 0 };
            var result = this.reportService.GetItemReportData(GetReportInputDTO(), userTokenDetails);
            Assert.Empty(result.ItemDetails);
        }

        [Fact]
        public void GetItemReportData_Failure_ExceptionResult()
        {
            this.mockReportRepository = new MockReportRepository().MockGetItemReportDataException();
            InitialiseUserService();
            UserTokenDetails userTokenDetails = new UserTokenDetails() { AgencyID = 0, UserID = 0 };
            Assert.ThrowsAny<Exception>(() => this.reportService.GetItemReportData(GetReportInputDTO(), userTokenDetails));
        }




        private object GetItemReportDataResults()
        {
            return new List<ItemDetailsDTO>()
            {
                new ItemDetailsDTO()
                {
                    AssessmentId = 25731,
                    Time=1,
                    Category="LIFE FUNCTIONING DOMAIN",
                    CategoryId = 11,
                    DateTaken=DateTime.UtcNow,
                    Item="FAMILY FUNCTIONING",
                    ItemId=101,
                    Notes="sfre ref erfge",
                    Period="Initial",
                    Value=null
                },
                new ItemDetailsDTO()
                {
                    AssessmentId = 25731,
                    Time=1,
                    Category="LIFE FUNCTIONING DOMAIN",
                    CategoryId = 11,
                    DateTaken=DateTime.UtcNow,
                    Item="LIVING SITUATION",
                    ItemId=102,
                    Notes="sfre ref erfge",
                    Period="Initial",
                    Value=1
                },
                new ItemDetailsDTO()
                {
                    AssessmentId = 25731,
                    Time=1,
                    Category="LIFE FUNCTIONING DOMAIN",
                    CategoryId = 11,
                    DateTaken=DateTime.UtcNow,
                    Item="SOCIAL FUNCTIONING",
                    ItemId=103,
                    Notes="sfre ref erfge",
                    Period="Initial",
                    Value=1
                },
                new ItemDetailsDTO()
                {
                    AssessmentId = 25731,
                    Time=1,
                    Category="LIFE FUNCTIONING DOMAIN",
                    CategoryId = 11,
                    DateTaken=DateTime.UtcNow,
                    Item="RECREATIONAL",
                    ItemId=104,
                    Notes="sfre ref erfge",
                    Period="Initial",
                    Value=1
                }
            };
        }
        #endregion Get Item Details Report

        #region Get story Map Report
        [Fact]
        public void GetStoryMapReportData_Success_ReturnsCorrectResult()
        {
            PeopleDTO peopleDetails = new PeopleDTO();
            peopleDetails.FirstName = "Rock";
            peopleDetails.LastName = "Francis";
            List<StoryMapDTO> mockitemDetailsList = (List<StoryMapDTO>)this.GetStoryMapReportDataResults();
            this.mockReportRepository = new MockReportRepository().MockGetStoryMapReportData(mockitemDetailsList);
            var sharedDetails = new SharedDetailsDTO() { PersonID = 123 };
            this.mockPersonRepository = new MockPersonRepository().MockGetPeopleDetails(peopleDetails, sharedDetails);
            InitialiseUserService();
            UserTokenDetails userTokenDetails = new UserTokenDetails() { AgencyID = 0, UserID = 0 };
            var result = this.reportService.GetStoryMapReportData(GetReportInputDTO(), userTokenDetails);
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.Success);
        }

        [Fact]
        public void GetStoryMapReportData_Success_ReturnsNoResult()
        {
            PeopleDTO peopleDetails = new PeopleDTO();
            peopleDetails.FirstName = "Rock";
            peopleDetails.LastName = "Francis";
            var mockitemDetailsList = new List<StoryMapDTO>();
            this.mockReportRepository = new MockReportRepository().MockGetStoryMapReportData(mockitemDetailsList);
            var sharedDetails = new SharedDetailsDTO() { PersonID = 123 };
            this.mockPersonRepository = new MockPersonRepository().MockGetPeopleDetails(peopleDetails, sharedDetails);
            InitialiseUserService();
            UserTokenDetails userTokenDetails = new UserTokenDetails() { AgencyID = 0, UserID = 0 };
            var result = this.reportService.GetStoryMapReportData(GetReportInputDTO(), userTokenDetails);
            Assert.Empty(result.storyMapList);
        }

        [Fact]
        public void GetStoryMapReportData_Failure_ExceptionResult()
        {
            this.mockReportRepository = new MockReportRepository().MockGetStoryMapReportDataException();
            InitialiseUserService();
            UserTokenDetails userTokenDetails = new UserTokenDetails() { AgencyID = 0, UserID = 0 };
            Assert.ThrowsAny<Exception>(() => this.reportService.GetStoryMapReportData(GetReportInputDTO(), userTokenDetails));
        }

        private object GetStoryMapReportDataResults()
        {
            return new List<StoryMapDTO>()
            {
                new StoryMapDTO()
                {
                    Type="Exposure",
                    ItemID=55,
                    Item="Medical Trauma",
                    Score=1,
                    Priority=2,
                    ToDo ="Underlying"
                },
                new StoryMapDTO()
                {
                    Type="Need",
                    ItemID=9,
                    Item="Adjustment to Trauma",
                    Score=2,
                    Priority=3,
                    ToDo ="Background"
                },
                new StoryMapDTO()
                {
                    Type="Strength",
                    ItemID=37,
                    Item="Cultural Identity",
                    Score=0,
                    Priority=1,
                    ToDo ="Build"
                }
            };
        }
        #endregion  Get story Map Report 

        #region GetPersonStrengthFamilyReportData
        [Fact]
        public void GetPersonStrengthFamilyReportData_Success_ReturnsCorrectResult()
        {
            var familyReportInputDTO = MockFamilyReportInputDTO();
            var person = new PeopleDTO() { PersonID = 123 };
            var assessments = MockLastAssessments();
            var personStregthData = new PersonStrengthReportDTO();
            var sharedDetails = new SharedDetailsDTO() { PersonID = 123 };
            this.mockPersonRepository = new MockPersonRepository().MockGetPersonIDS(person, sharedDetails);
            this.mockAssessmentRepository = new MockAssessmentRepository().MockGetLastAssessmentByPerson(assessments);
            this.mockReportRepository = new MockReportRepository().MockGetPersonStrengthFamilyReportData(personStregthData);
            InitialiseUserService();
            UserTokenDetails userTokenDetails = new UserTokenDetails() { AgencyID = 0, UserID = 0 };
            var result = this.reportService.GetPersonStrengthFamilyReportData(familyReportInputDTO,userTokenDetails);
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.Success);
        }

        private List<Assessment> MockLastAssessments()
        {
            return new List<Assessment>()
            {
                new Assessment()
                {
                    AssessmentID = 1,
                    PersonQuestionnaireID = 1,
                    VoiceTypeID = 1,
                    DateTaken = DateTime.UtcNow,
                    ReasoningText = null,
                    AssessmentReasonID = 2,
                    AssessmentStatusID = 2,
                    PersonQuestionnaireScheduleID = null,
                    IsUpdate = true,
                    Approved = null,
                    CloseDate = DateTime.UtcNow,
                    IsRemoved = false,
                    UpdateDate = DateTime.UtcNow,
                    UpdateUserID = 2
                }
            };
        }

        private FamilyReportInputDTO MockFamilyReportInputDTO()
        {
            return new FamilyReportInputDTO()
            {
                PersonCollaborationID = 0,
                PersonIndex = Guid.NewGuid(),
                PersonQuestionnaireID = 0,
                VoiceTypeFKID = 0,
                VoiceTypeID = 0
            };
        }

        [Fact]
        public void GetPersonStrengthFamilyReportData_Success_ReturnsFailureResult()
        {
            var familyReportInputDTO = MockFamilyReportInputDTO();
            var person = new PeopleDTO() { PersonID = 123 };
            var assessments = new List<Assessment>();
            var personStrengthData = new PersonStrengthReportDTO();
            var sharedDetails = new SharedDetailsDTO() { PersonID = 123 };
            this.mockPersonRepository = new MockPersonRepository().MockGetPersonIDS(person, sharedDetails);
            this.mockAssessmentRepository = new MockAssessmentRepository().MockGetLastAssessmentByPerson(assessments);
            this.mockReportRepository = new MockReportRepository().MockGetPersonStrengthFamilyReportData(personStrengthData);
            InitialiseUserService();
            UserTokenDetails userTokenDetails = new UserTokenDetails() { AgencyID = 0, UserID = 0 };
            var result = this.reportService.GetPersonStrengthFamilyReportData(familyReportInputDTO, userTokenDetails);
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.Failure);
        }
        [Fact]
        public void GetPersonStrengthFamilyReportData_Failure_ExceptionResult()
        {
            var familyReportInputDTO = MockFamilyReportInputDTO();
            var person = new PeopleDTO() { PersonID = 123 };
            var assessments = MockLastAssessments();
            var personStregthData = new PersonStrengthReportDTO();
            var sharedDetails = new SharedDetailsDTO() { PersonID = 123 };
            this.mockPersonRepository = new MockPersonRepository().MockGetPersonIDS(person, sharedDetails);
            this.mockAssessmentRepository = new MockAssessmentRepository().MockGetLastAssessmentByPerson(assessments);
            this.mockReportRepository = new MockReportRepository().MockGetPersonStrengthFamilyReportDataException();
            InitialiseUserService();
            UserTokenDetails userTokenDetails = new UserTokenDetails() { AgencyID = 0, UserID = 0 };
            Assert.ThrowsAny<Exception>(() => this.reportService.GetPersonStrengthFamilyReportData(familyReportInputDTO, userTokenDetails));
        }
        #endregion

        #region GetPersonNeedsFamilyReportData
        [Fact]
        public void GetPersonNeedsFamilyReportData_Success_ReturnsCorrectResult()
        {
            var familyReportInputDTO = MockFamilyReportInputDTO();
            var person = new PeopleDTO() { PersonID = 123 };
            var assessments = MockLastAssessments();
            var personNeedsData = new PersonNeedsReportDTO();
            var sharedDetails = new SharedDetailsDTO() { PersonID = 123 };
            this.mockPersonRepository = new MockPersonRepository().MockGetPersonIDS(person, sharedDetails);
            this.mockAssessmentRepository = new MockAssessmentRepository().MockGetLastAssessmentByPerson(assessments);
            this.mockReportRepository = new MockReportRepository().MockGetPersonNeedsFamilyReportData(personNeedsData);
            InitialiseUserService();
            UserTokenDetails userTokenDetails = new UserTokenDetails() { AgencyID = 0, UserID = 0 };
            var result = this.reportService.GetPersonNeedsFamilyReportData(familyReportInputDTO,userTokenDetails);
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.Success);
        }

        [Fact]
        public void GetPersonNeedsFamilyReportData_Success_ReturnsFailureResult()
        {
            var familyReportInputDTO = MockFamilyReportInputDTO();
            var person = new PeopleDTO() { PersonID = 123 };
            var assessments = new List<Assessment>();
            var personNeedsData = new PersonNeedsReportDTO();
            var sharedDetails = new SharedDetailsDTO() { PersonID = 123 };
            this.mockPersonRepository = new MockPersonRepository().MockGetPersonIDS(person, sharedDetails);
            this.mockAssessmentRepository = new MockAssessmentRepository().MockGetLastAssessmentByPerson(assessments);
            this.mockReportRepository = new MockReportRepository().MockGetPersonNeedsFamilyReportData(personNeedsData);
            InitialiseUserService();
            UserTokenDetails userTokenDetails = new UserTokenDetails() { AgencyID = 0, UserID = 0 };
            var result = this.reportService.GetPersonNeedsFamilyReportData(familyReportInputDTO,userTokenDetails);
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.Failure);
        }
        [Fact]
        public void GetPersonNeedsFamilyReportData_Failure_ExceptionResult()
        {
            var familyReportInputDTO = MockFamilyReportInputDTO();
            var person = new PeopleDTO() { PersonID = 123 };
            var assessments = MockLastAssessments();
            var sharedDetails = new SharedDetailsDTO() { PersonID = 123 };
            this.mockPersonRepository = new MockPersonRepository().MockGetPersonIDS(person, sharedDetails);
            this.mockAssessmentRepository = new MockAssessmentRepository().MockGetLastAssessmentByPerson(assessments);
            this.mockReportRepository = new MockReportRepository().MockGetPersonNeedsFamilyReportDataException();
            InitialiseUserService();
            UserTokenDetails userTokenDetails = new UserTokenDetails() { AgencyID = 0, UserID = 0 };
            Assert.ThrowsAny<Exception>(() => this.reportService.GetPersonNeedsFamilyReportData(familyReportInputDTO, userTokenDetails));
        }
        #endregion

        #region GetSupportResourcesFamilyReportData
        [Fact]
        public void GetSupportResourcesFamilyReportData_Success_ReturnsCorrectResult()
        {
            var familyReportInputDTO = MockFamilyReportInputDTO();
            var person = new PeopleDTO() { PersonID = 123 };
            var assessments = MockLastAssessments();
            var supportResourceData = new SupportResourceReportDTO();
            var sharedDetails = new SharedDetailsDTO() { PersonID = 123 };
            this.mockPersonRepository = new MockPersonRepository().MockGetPersonIDS(person, sharedDetails);
            this.mockAssessmentRepository = new MockAssessmentRepository().MockGetLastAssessmentByPerson(assessments);
            this.mockReportRepository = new MockReportRepository().MockGetSupportResourcesFamilyReportData(supportResourceData);
            InitialiseUserService();
            UserTokenDetails userTokenDetails = new UserTokenDetails() { AgencyID = 0, UserID = 0 };
            var result = this.reportService.GetSupportResourcesFamilyReportData(familyReportInputDTO, userTokenDetails);
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.Success);
        }

        [Fact]
        public void GetSupportResourcesFamilyReportData_Success_ReturnsFailureResult()
        {
            var familyReportInputDTO = MockFamilyReportInputDTO();
            var person = new PeopleDTO() { PersonID = 123 };
            var assessments = new List<Assessment>();
            var SupportResourceData = new SupportResourceReportDTO();
            var sharedDetails = new SharedDetailsDTO() { PersonID = 123 };
            this.mockPersonRepository = new MockPersonRepository().MockGetPersonIDS(person, sharedDetails);
            this.mockAssessmentRepository = new MockAssessmentRepository().MockGetLastAssessmentByPerson(assessments);
            this.mockReportRepository = new MockReportRepository().MockGetSupportResourcesFamilyReportData(SupportResourceData);
            InitialiseUserService();
            UserTokenDetails userTokenDetails = new UserTokenDetails() { AgencyID = 0, UserID = 0 };
            var result = this.reportService.GetSupportResourcesFamilyReportData(familyReportInputDTO, userTokenDetails);
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.Failure);
        }
        [Fact]
        public void GetSupportResourcesFamilyReportData_Failure_ExceptionResult()
        {
            var familyReportInputDTO = MockFamilyReportInputDTO();
            var person = new PeopleDTO() { PersonID = 123 };
            var assessments = MockLastAssessments();
            var sharedDetails = new SharedDetailsDTO() { PersonID = 123 };
            this.mockPersonRepository = new MockPersonRepository().MockGetPersonIDS(person, sharedDetails);
            this.mockAssessmentRepository = new MockAssessmentRepository().MockGetLastAssessmentByPerson(assessments);
            this.mockReportRepository = new MockReportRepository().MockGetSupportResourcesFamilyReportDataException();
            InitialiseUserService();
            UserTokenDetails userTokenDetails = new UserTokenDetails() { AgencyID = 0, UserID = 0 };
            Assert.ThrowsAny<Exception>(() => this.reportService.GetSupportResourcesFamilyReportData(familyReportInputDTO,userTokenDetails));
        }
        #endregion

        #region GetSupportNeedsFamilyReportData
        [Fact]
        public void GetSupportNeedsFamilyReportData_Success_ReturnsCorrectResult()
        {
            var familyReportInputDTO = MockFamilyReportInputDTO();
            var person = new PeopleDTO() { PersonID = 123 };
            var assessments = MockLastAssessments();
            var supportResourceData = new SupportNeedsReportDTO();
            var sharedDetails = new SharedDetailsDTO() { PersonID = 123 };
            this.mockPersonRepository = new MockPersonRepository().MockGetPersonIDS(person, sharedDetails);
            this.mockAssessmentRepository = new MockAssessmentRepository().MockGetLastAssessmentByPerson(assessments);
            this.mockReportRepository = new MockReportRepository().MockGetSupportNeedsFamilyReportData(supportResourceData);
            InitialiseUserService();
            UserTokenDetails userTokenDetails = new UserTokenDetails() { AgencyID = 0, UserID = 0 };
            var result = this.reportService.GetSupportNeedsFamilyReportData(familyReportInputDTO,userTokenDetails);
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.Success);
        }

        [Fact]
        public void GetSupportNeedsFamilyReportData_Success_ReturnsFailureResult()
        {
            var familyReportInputDTO = MockFamilyReportInputDTO();
            var person = new PeopleDTO() { PersonID = 123 };
            var assessments = new List<Assessment>();
            var SupportResourceData = new SupportNeedsReportDTO();
            var sharedDetails = new SharedDetailsDTO() { PersonID = 123 };
            this.mockPersonRepository = new MockPersonRepository().MockGetPersonIDS(person, sharedDetails);
            this.mockAssessmentRepository = new MockAssessmentRepository().MockGetLastAssessmentByPerson(assessments);
            this.mockReportRepository = new MockReportRepository().MockGetSupportNeedsFamilyReportData(SupportResourceData);
            InitialiseUserService();
            UserTokenDetails userTokenDetails = new UserTokenDetails() { AgencyID = 0, UserID = 0 };
            var result = this.reportService.GetSupportNeedsFamilyReportData(familyReportInputDTO,userTokenDetails);
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.Failure);
        }
        [Fact]
        public void GetSupportNeedsFamilyReportData_Failure_ExceptionResult()
        {
            var familyReportInputDTO = MockFamilyReportInputDTO();
            var person = new PeopleDTO() { PersonID = 123 };
            var assessments = MockLastAssessments();
            var sharedDetails = new SharedDetailsDTO() { PersonID = 123 };
            this.mockPersonRepository = new MockPersonRepository().MockGetPersonIDS(person, sharedDetails);
            this.mockAssessmentRepository = new MockAssessmentRepository().MockGetLastAssessmentByPerson(assessments);
            this.mockReportRepository = new MockReportRepository().MockGetSupportNeedsFamilyReportDataException();
            InitialiseUserService();
            UserTokenDetails userTokenDetails = new UserTokenDetails() { AgencyID = 0, UserID = 0 };
            Assert.ThrowsAny<Exception>(() => this.reportService.GetSupportNeedsFamilyReportData(familyReportInputDTO,userTokenDetails));
        }
        #endregion

        #region GetFamilyReportStatus
        [Fact]
        public void GetFamilyReportStatus_Success_ReturnsCorrectResult()
        {
            var familyReportInputDTO = MockFamilyReportInputDTO();
            var person = new PeopleDTO() { PersonID = 123 };
            var assessments = MockLastAssessments();
            var familyReportStatus = new FamilyReportStatusDTO();
            var sharedDetails = new SharedDetailsDTO() { PersonID = 123 };
            this.mockPersonRepository = new MockPersonRepository().MockGetPersonIDS(person, sharedDetails);
            this.mockAssessmentRepository = new MockAssessmentRepository().MockGetLastAssessmentByPerson(assessments);
            this.mockReportRepository = new MockReportRepository().MockGetFamilyReportStatus(familyReportStatus);
            InitialiseUserService();
            UserTokenDetails userTokenDetails = new UserTokenDetails() { AgencyID = 0, UserID = 0 };
            var result = this.reportService.GetFamilyReportStatus(familyReportInputDTO, userTokenDetails);
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.Success);
        }

        [Fact]
        public void GetFamilyReportStatus_Success_ReturnsFailureResult()
        {
            var familyReportInputDTO = MockFamilyReportInputDTO();
            var person = new PeopleDTO() { PersonID = 123 };
            var assessments = new List<Assessment>();
            var familyReportStatus = new FamilyReportStatusDTO();
            var sharedDetails = new SharedDetailsDTO() { PersonID = 123 };
            this.mockPersonRepository = new MockPersonRepository().MockGetPersonIDS(person, sharedDetails);
            this.mockAssessmentRepository = new MockAssessmentRepository().MockGetLastAssessmentByPerson(assessments);
            this.mockReportRepository = new MockReportRepository().MockGetFamilyReportStatus(familyReportStatus);
            InitialiseUserService();
            UserTokenDetails userTokenDetails = new UserTokenDetails() { AgencyID = 0, UserID = 0 };
            var result = this.reportService.GetFamilyReportStatus(familyReportInputDTO, userTokenDetails);
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.Failure);
        }
        [Fact]
        public void GetFamilyReportStatus_Failure_ExceptionResult()
        {
            var familyReportInputDTO = MockFamilyReportInputDTO();
            var person = new PeopleDTO() { PersonID = 123 };
            var assessments = MockLastAssessments();
            var sharedDetails = new SharedDetailsDTO() { PersonID = 123 };
            this.mockPersonRepository = new MockPersonRepository().MockGetPersonIDS(person, sharedDetails);
            this.mockAssessmentRepository = new MockAssessmentRepository().MockGetLastAssessmentByPerson(assessments);
            this.mockReportRepository = new MockReportRepository().MockGetFamilyReportStatusException();
            InitialiseUserService();
            UserTokenDetails userTokenDetails = new UserTokenDetails() { AgencyID = 0, UserID = 0 };
            Assert.ThrowsAny<Exception>(() => this.reportService.GetFamilyReportStatus(familyReportInputDTO, userTokenDetails));
        }
        #endregion
    }
}
