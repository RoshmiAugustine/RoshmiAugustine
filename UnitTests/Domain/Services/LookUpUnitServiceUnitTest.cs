using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Domain.Resources;
using Opeeka.PICS.Domain.Services;
using Opeeka.PICS.Infrastructure.Enums;
using Opeeka.PICS.UnitTests.Domain.Mocks.Repositories;
using Xunit;

namespace Opeeka.PICS.UnitTests.Domain.Services
{
    public class LookUpUnitServiceUnitTest
    {

        /// Initializes a new instance of the agencyRepository/> class.
        private Mock<ILookupRepository> mock_lookupRepository;
        /// Initializes a new instance of the agencyRepository/> class.
        private Mock<IAgencyRepository> mock_agencyRepository;

        /// Initializes a new instance of the addressRepository/> class.
        private Mock<IGenderRepository> mock_genderRepository;

        /// Initializes a new instance of the addressRepository/> class.
        private Mock<ILanguageRepository> mock_languageRepository;

        /// Initializes a new instance of the addressRepository/> class.
        private Mock<ISexualityRepository> mock_sexualityrepository;

        /// Initializes a new instance of the addressRepository/> class.
        private Mock<IRaceEthnicityRepository> mock_raceEthnicityRepository;

        /// Initializes a new instance of the addressRepository/> class.
        private Mock<IIdentificationTypeRepository> mock_identificationrepository;

        /// Initializes a new instance of the addressRepository/> class.
        private Mock<ISupportTypeRepository> mock_supportTypeRepository;

        /// Initializes a new instance of the addressRepository/> class.
        private Mock<ICollaborationRepository> mock_collaborationRepository;

        /// Initializes a new instance of the addressRepository/> class.
        private Mock<IHelperTitleRepository> mock_helperTitleRepository;

        /// Initializes a new instance of the addressRepository/> class.
        private Mock<ISharingPolicyRepository> mock_sharingPolicyRepository;

        /// Initializes a new instance of the <see cref="logger"/> class.
        private Mock<ILogger<LookupService>> mockLogger;

        /// Initializes a new instance of the personRepository"/> class.
        private Mock<IPersonRepository> mock_PersonRepository;

        /// Initializes a new instance of the PersonQuestionnaireRepository"/> class.
        private Mock<IPersonQuestionnaireRepository> mock_PersonQuestionnaireRepository;

        /// Initializes a new instance of the QuestionnaireRepository"/> class.
        private Mock<IQuestionnaireRepository> mock_QuestionnaireRepository;

        /// Initializes a new instance of the assessmentReasonRepository"/> class.
        private Mock<IAssessmentReasonRepository> mock_AssessmentReasonRepository;

        /// Initializes a new instance of the voiceTypeRepository"/> class.
        private Mock<IVoiceTypeRepository> mock_VoiceTypeRepository;

        /// Initializes a new instance of the notificationTypeRepository"/> class.
        private Mock<INotificationTypeRepository> mock_NotificationTypeRepository;

        /// Initializes a new instance of the voiceTypeRepository"/> class.
        private Mock<IMapper> mock_mapper;

        /// Initializes a new instance of the voiceTypeRepository"/> class.
        private Mock<IHelperRepository> mock_helperRepository;

        /// Initializes a new instance of the addressRepository/> class.
        private Mock<IIdentifiedGenderRepository> mockIdentifiedGenderRepository;

        private LookupService lookupService;
        private Mock<IConfigurationRepository> mockConfigRepository;
        private Mock<IHttpContextAccessor> httpContextAccessor;
        private Mock<IAgencySharingPolicyRepository> mockAgencySharingPolicyRepository;
        private Mock<ICollaborationSharingPolicyRepository> mockColloborationSharingPolicyRepository;
        private Mock<IQuestionnaireReminderTypeRepository> mockquestionnaireReminderTypeRepository;
        private Mock<IAssessmentStatusRepository> mockAssessmentStatusRepository;


        private Mock<IEmailDetailRepository> emailDetailRepository;

        private Mock<INotificationLevelRepository> mocknotificationLevelRepository;
        private Mock<IResponseRepository> mockresponseRepository;
        private Mock<IBackgroundProcessLogRepository> mockbackgroundProcessLogRepository;
        private Mock<IReminderInviteToCompleteRepository> mockReminderInviteToCompleteRepository;
        private Mock<INotifyReminderRepository> mockNotifyReminderRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AgencyService"/> class.
        /// </summary>
        public LookUpUnitServiceUnitTest()
        {

            this.mock_lookupRepository = new Mock<ILookupRepository>();
            this.mock_lookupRepository = new Mock<ILookupRepository>();
            this.mock_agencyRepository = new Mock<IAgencyRepository>();
            this.mock_genderRepository = new Mock<IGenderRepository>();
            this.mock_languageRepository = new Mock<ILanguageRepository>();
            this.mock_sexualityrepository = new Mock<ISexualityRepository>();
            this.mock_identificationrepository = new Mock<IIdentificationTypeRepository>();
            this.mock_supportTypeRepository = new Mock<ISupportTypeRepository>();
            this.mock_collaborationRepository = new Mock<ICollaborationRepository>();
            this.mock_helperTitleRepository = new Mock<IHelperTitleRepository>();
            this.mock_sharingPolicyRepository = new Mock<ISharingPolicyRepository>();
            this.mock_raceEthnicityRepository = new Mock<IRaceEthnicityRepository>();
            this.mockLogger = new Mock<ILogger<LookupService>>();
            this.mock_PersonRepository = new Mock<IPersonRepository>();
            this.mock_QuestionnaireRepository = new Mock<IQuestionnaireRepository>();
            this.mock_PersonQuestionnaireRepository = new Mock<IPersonQuestionnaireRepository>();
            this.mock_AssessmentReasonRepository = new Mock<IAssessmentReasonRepository>();
            this.mock_VoiceTypeRepository = new Mock<IVoiceTypeRepository>();
            this.mock_NotificationTypeRepository = new Mock<INotificationTypeRepository>();
            this.mock_mapper = new Mock<IMapper>();
            this.mockConfigRepository = new Mock<IConfigurationRepository>();
            this.httpContextAccessor = new Mock<IHttpContextAccessor>();
            this.mock_helperRepository = new Mock<IHelperRepository>();
            this.mockIdentifiedGenderRepository = new Mock<IIdentifiedGenderRepository>();
            this.mockAgencySharingPolicyRepository = new Mock<IAgencySharingPolicyRepository>();
            this.mockColloborationSharingPolicyRepository = new Mock<ICollaborationSharingPolicyRepository>();
            this.emailDetailRepository = new Mock<IEmailDetailRepository>();
            this.mockquestionnaireReminderTypeRepository = new Mock<IQuestionnaireReminderTypeRepository> ();
            this.mockAssessmentStatusRepository = new Mock<IAssessmentStatusRepository>();
            this.mocknotificationLevelRepository= new Mock<INotificationLevelRepository>();
            this.mockresponseRepository= new Mock<IResponseRepository>();
            this.mockbackgroundProcessLogRepository= new Mock<IBackgroundProcessLogRepository>();
            this.mockReminderInviteToCompleteRepository = new Mock<IReminderInviteToCompleteRepository>();
            this.mockNotifyReminderRepository = new Mock<INotifyReminderRepository>();

        }
        private void initialiseService(Mock<LocalizeService> localize)
        {
            this.lookupService = new LookupService(this.mockbackgroundProcessLogRepository.Object, this.mockresponseRepository.Object, this.mocknotificationLevelRepository.Object, this.mockquestionnaireReminderTypeRepository.Object, this.emailDetailRepository.Object, this.mockColloborationSharingPolicyRepository.Object, this.mockAgencySharingPolicyRepository.Object, this.mock_helperRepository.Object, this.mock_supportTypeRepository.Object, this.mock_collaborationRepository.Object, this.mock_identificationrepository.Object, this.mock_agencyRepository.Object, this.mock_lookupRepository.Object, this.mock_genderRepository.Object, this.mock_languageRepository.Object, this.mock_sexualityrepository.Object, this.mock_helperTitleRepository.Object, this.mock_raceEthnicityRepository.Object, this.mockLogger.Object, this.mock_sharingPolicyRepository.Object, this.mock_PersonRepository.Object, this.mock_PersonQuestionnaireRepository.Object, this.mock_QuestionnaireRepository.Object, this.mock_AssessmentReasonRepository.Object, this.mock_VoiceTypeRepository.Object, this.mock_mapper.Object, localize.Object, mockConfigRepository.Object, httpContextAccessor.Object, this.mock_NotificationTypeRepository.Object, this.mockIdentifiedGenderRepository.Object, this.mockAssessmentStatusRepository.Object, this.mockReminderInviteToCompleteRepository.Object, this.mockNotifyReminderRepository.Object);
        }
        //--------------------------------------------------------Add Agency--------------------------------------------------------
        [Fact]
        public void GetSharedPolicy_Success_ReturnsCorrectResult()
        {
            var mockSharedPolicy = GetMockSharedPolicy();
            this.mock_sharingPolicyRepository = new MockSharingPolicyRepository().MockSharingPolicy(mockSharedPolicy);
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Success);
            this.initialiseService(localize);
            var result = this.lookupService.GetSharingPolicyList();
            Assert.Equal(result.ResponseStatus, PCISEnum.StatusMessages.Success);
            //Assert.NotNull
        }

        [Fact]
        public void GetSharedPolicy_ReturnsNoResult()
        {
            var mockSharedPolicy = new List<SharingPolicyDTO>();
            this.mock_sharingPolicyRepository = new MockSharingPolicyRepository().MockSharingPolicy(mockSharedPolicy);
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Failure);
            this.initialiseService(localize);
            var result = this.lookupService.GetSharingPolicyList();
            Assert.Equal(result.ResponseStatus, PCISEnum.StatusMessages.Failure);
            //Assert.NotNull
        }

        [Fact]
        public void GetCollaborationAgency_Failure_ExceptionResult()
        {
            var mockCollaborationAgency = GetLookupCollaboration();
            this.mock_collaborationRepository = new MockCollaborationRepository().MockGetCollaboration(mockCollaborationAgency);
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Failure);
            this.initialiseService(localize); 
            var result = this.lookupService.GetAgencyCollaboration(0);
            Assert.Equal(result.ResponseStatus, PCISEnum.StatusMessages.Failure);
            //Assert.NotNull
        }

        [Fact]
        public void GetCollaborationAgency_Success_ReturnsCorrectResult()
        {
            var mockCollaborationAgency = GetLookupCollaboration();
            this.mock_collaborationRepository = new MockCollaborationRepository().MockGetCollaboration(mockCollaborationAgency);
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Success);
            this.initialiseService(localize);
            long agencyID = 1;
            var result = this.lookupService.GetAgencyCollaboration(agencyID);
            Assert.Equal(result.ResponseStatus, PCISEnum.StatusMessages.Success);
            //Assert.NotNull
        }


        private List<CollaborationLookupDTO> GetLookupCollaboration()
        {
            return new List<CollaborationLookupDTO>()
            {
                new CollaborationLookupDTO()
                {
                  CollaborationID=1,
                  Name="ABC Reporting Unit CBT"
                },
                new CollaborationLookupDTO()
                {
                   CollaborationID=2,
                  Name="Franklin Reporting Unit CBT"
                },

            };
        }

        private List<SharingPolicyDTO> GetMockSharedPolicy()
        {
            return new List<SharingPolicyDTO>()
            {
                new SharingPolicyDTO()
                {
                  SharingPolicyID=1,
                  AccessName="Read/Write"
                },
                new SharingPolicyDTO()
                {
                   SharingPolicyID=2,
                  AccessName="ReadOnly"
                },

            };
        }

        [Fact]
        public void GetAllSupportsForPerson_Success_ReturnsCorrectResult()
        {
            var mockPersonDetail = GetMockPerson();
            var mockPersonSupports = GetMockPersonSupports();
            this.mock_PersonRepository = new MockPersonRepository().MockPeopleSupport(mockPersonDetail, mockPersonSupports);
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Success);
            this.initialiseService(localize);
            Guid personIndex = Guid.NewGuid();
            var result = this.lookupService.GetAllSupportsForPerson(personIndex);
            Assert.Equal(result.ResponseStatus, PCISEnum.StatusMessages.Success);
        }

        [Fact]
        public void GetAllSupportsForPerson_ReturnsNoResult()
        {
            var mockPersonDetail = GetMockPerson();
            var mockPersonSupports = new List<PeopleSupportDTO>();
            this.mock_PersonRepository = new MockPersonRepository().MockPeopleSupport(mockPersonDetail, mockPersonSupports);
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Failure);
            this.initialiseService(localize);
            Guid personIndex = Guid.NewGuid();
            var result = this.lookupService.GetAllSupportsForPerson(personIndex);
            Assert.Null(result.PersonSupportLookup);
        }

        private PeopleDataDTO GetMockPersonDetail()
        {
            return new PeopleDataDTO()
            {
                PersonID = 1,
                PersonIndex = Guid.NewGuid(),
                FirstName = "Ben",
                MiddleName = "",
                LastName = "Jones"
            };
        }
        private PeopleDTO GetMockPerson()
        {
            return new PeopleDTO()
            {
                PersonID = 1,
                PersonIndex = Guid.NewGuid(),
                FirstName = "Ben",
                MiddleName = "",
                LastName = "Jones",
                AgencyID =1
            };
        }
        private List<PeopleSupportDTO> GetMockPersonSupports()
        {
            return new List<PeopleSupportDTO>()
            {
                new PeopleSupportDTO()
                {
                    SupportFirstName = "Billy",
                    SupportMiddleName = "Den",
                    SupportLastName = "Jones",
                    SupportPhone = "555-555-5555",
                    SupportEmail = "billy@example",
                    RelationshipID = 1,
                    Relationship = "Biological Father",
                    PersonSupportID = 1,
                    IsCurrent = true,
                    StartDate = DateTime.UtcNow,
                    EndDate = null,
                    Suffix = null,
                    PersonID = 1,
                },
                new PeopleSupportDTO()
                {
                    SupportFirstName = "Jenny",
                    SupportMiddleName = "Wan",
                    SupportLastName = "Jones",
                    SupportPhone = "555-555-5555",
                    SupportEmail = "jenny@example",
                    RelationshipID = 1,
                    Relationship = "Biological Mother",
                    PersonSupportID = 2,
                    IsCurrent = true,
                    StartDate = DateTime.UtcNow,
                    EndDate = null,
                    Suffix = null,
                    PersonID = 1,
                },
            };
        }

        #region AssessmentReason Lookup

        private List<AssessmentReason> MockAssessmentReasonList()
        {
            return new List<AssessmentReason>()
            {
                new AssessmentReason()
                {
                  AssessmentReasonID = 1,
                  Name = "Scheduled",
                  Abbrev = null,
                  Description = null,
                  ListOrder = 1,
                  IsRemoved = false,
                  UpdateDate = DateTime.UtcNow,
                  UpdateUserID = 1
                },
                new AssessmentReason()
                {
                  AssessmentReasonID = 2,
                  Name = "Triggering Event",
                  Abbrev = null,
                  Description = null,
                  ListOrder = 2,
                  IsRemoved = false,
                  UpdateDate = DateTime.UtcNow,
                  UpdateUserID = 1
                }
            };
        }

        [Fact]
        public void GetAllAssessmentReason_Success_ReturnsCorrectResult()
        {
            var mockAssessmentReasonList = MockAssessmentReasonList();

            this.mock_AssessmentReasonRepository = new MockAssessmentReasonRepository().MockGetAllAssessmentReason(mockAssessmentReasonList);

            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Success);
            var expected = new AssessmentReasonLookupDTO();
            this.mock_mapper.Setup(x => x.Map<AssessmentReason, AssessmentReasonLookupDTO>(It.IsAny<AssessmentReason>()))
            .Returns(expected);
            this.initialiseService(localize);
            var result = this.lookupService.GetAllAssessmentReason();
            Assert.Equal(result.ResponseStatus, PCISEnum.StatusMessages.Success);
        }

        [Fact]
        public void GetAllAssessmentReason_ExceptionResult()
        {
            this.mock_AssessmentReasonRepository = new MockAssessmentReasonRepository().MockGetAllAssessmentReasonException();
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Success);
            this.initialiseService(localize);
            Assert.ThrowsAny<Exception>(() => this.lookupService.GetAllAssessmentReason());
        }

        #endregion AssessmentReason Lookup


        public Mock<IMapper> MappingData()
        {
            var mappingService = new Mock<IMapper>();

            // mappingService.Setup(m => m.Map<UserDetail, AssessmentReasonLookupDTO>(It.IsAny<UserDetail>())).Returns(interview); // mapping data
            // mappingService.Setup(m => m.Map<UserDetailViewModel, UserDetail>(It.IsAny<UserDetailtViewModel>())).Returns(im); // mapping data

            return mappingService;
        }

        #region VoiceType Lookup

        private List<VoiceType> MockVoiceTypeList()
        {
            return new List<VoiceType>()
            {
                new VoiceType()
                {
                  VoiceTypeID = 1,
                  Name = "Scheduled",
                  Abbrev = null,
                  Description = null,
                  ListOrder = 1,
                  IsRemoved = false,
                  UpdateDate = DateTime.UtcNow,
                  UpdateUserID = 1
                },
                new VoiceType()
                {
                  VoiceTypeID = 2,
                  Name = "Triggering Event",
                  Abbrev = null,
                  Description = null,
                  ListOrder = 2,
                  IsRemoved = false,
                  UpdateDate = DateTime.UtcNow,
                  UpdateUserID = 1
                }
            };
        }

        [Fact]
        public void GetAllVoiceType_Success_ReturnsCorrectResult()
        {
            var mockVoiceTypeList = MockVoiceTypeList();

            this.mock_VoiceTypeRepository = new MockVoiceTypeRepository().MockGetAllVoiceType(mockVoiceTypeList);
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Success);
            this.initialiseService(localize);
            var result = this.lookupService.GetAllVoiceType();
            Assert.Equal(result.ResponseStatus, PCISEnum.StatusMessages.Success);
        }

        [Fact]
        public void GetAllVoiceType_ExceptionResult()
        {
            this.mock_VoiceTypeRepository = new MockVoiceTypeRepository().MockGetAllVoiceTypeException();
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Failure);
            this.initialiseService(localize);
            Assert.ThrowsAny<Exception>(() => this.lookupService.GetAllVoiceType());
        }

        #endregion VoiceType Lookup

        #region NotificationType Lookup

        private List<NotificationType> MockNotificationTypeList()
        {
            return new List<NotificationType>()
            {
                new NotificationType()
                {
                  NotificationTypeID = 1,
                  Name = "Danger",
                  Abbrev = null,
                  Description = null,
                  ListOrder = 1,
                  IsRemoved = false,
                  UpdateDate = DateTime.UtcNow,
                  UpdateUserID = 1
                },
                new NotificationType()
                {
                  NotificationTypeID = 2,
                  Name = "Reminder",
                  Abbrev = null,
                  Description = null,
                  ListOrder = 2,
                  IsRemoved = false,
                  UpdateDate = DateTime.UtcNow,
                  UpdateUserID = 1
                }
            };
        }

        [Fact]
        public void GetAllNotificationType_Success_ReturnsCorrectResult()
        {
            var mockNotificationTypeList = MockNotificationTypeList();

            this.mock_NotificationTypeRepository = new MockNotificationTypeRepository().MockGetAllNotificationType(mockNotificationTypeList);
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Success);
            this.initialiseService(localize);
            var result = this.lookupService.GetAllNotificationType();
            Assert.Equal(result.ResponseStatus, PCISEnum.StatusMessages.Success);
        }

        [Fact]
        public void GetAllNotificationType_ExceptionResult()
        {
            this.mock_NotificationTypeRepository = new MockNotificationTypeRepository().MockGetAllNotificationTypeException();
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Failure);
            this.initialiseService(localize);
            Assert.ThrowsAny<Exception>(() => this.lookupService.GetAllNotificationType());
        }

        #endregion NotificationType Lookup

        #region Manager Lookup

        private List<Helper> MockManagerList()
        {
            return new List<Helper>()
            {
                new Helper()
                {
                    HelperID = 1,
                    HelperIndex = Guid.NewGuid(),
                    UserID = 2,
                    FirstName = "Maxwell",
                    MiddleName = null,
                    LastName = "Romero",
                    Email = null,
                    Phone = null,
                    IsRemoved = false,
                    UpdateDate = DateTime.UtcNow,
                    UpdateUserID = 1,
                    AgencyID = 1,
                    HelperTitleID = 1,
                    Phone2 = null,
                    SupervisorHelperID = null
                }
            };
        }

        [Fact]
        public void GetAllManager_Success_ReturnsCorrectResult()
        {
            var mockManagerList = MockManagerList();

            this.mock_helperRepository = new MockHelperRepository().MockGetAllManager(mockManagerList);
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Failure);

            this.initialiseService(localize);

            long agencyID = 1;
            var result = this.lookupService.GetAllManager(agencyID);
            Assert.Equal(result.ResponseStatus, PCISEnum.StatusMessages.Success);
        }

        [Fact]
        public void GetAllManager_ExceptionResult()
        {
            this.mock_helperRepository = new MockHelperRepository().MockGetAllManagerException();
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Failure);

            this.initialiseService(localize);

            long agencyID = 1;
            Assert.ThrowsAny<Exception>(() => this.lookupService.GetAllManager(agencyID));
        }

        #endregion Manager Lookup

        #region Country Lookup

        private List<CountryLookupDTO> MockCountryList()
        {
            return new List<CountryLookupDTO>()
            {
                new CountryLookupDTO()
                {
                    Abbrev = "AF",
                    CountryCode = "93",
                    CountryID = 1,
                    Description = "Afghanistan",
                    IsRemoved = false,
                    ListOrder = 1,
                    Name = "Afghanistan",
                    UpdateDate = DateTime.Now,
                    UpdateUserID = 1
                }
            };
        }

        [Fact]
        public void GetAllContries_Success_ReturnsCorrectResult()
        {
            var mockCountries = MockCountryList();
            this.mock_lookupRepository = new MockLookupRepository().MockGetAllCountries(mockCountries);
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Success);
            this.initialiseService(localize);
            var result = this.lookupService.GetAllCountries();
            Assert.Equal(result.ResponseStatus, PCISEnum.StatusMessages.Success);
        }

        [Fact]
        public void GetAllContries_ExceptionResult()
        {
            var mockCountries = MockCountryList();
            this.mock_lookupRepository = new MockLookupRepository().MockGetAllCountriesException(mockCountries);
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Failure);

            this.initialiseService(localize);

            Assert.ThrowsAny<Exception>(() => this.lookupService.GetAllCountries());
        }

        #endregion Country Lookup

        #region TimePeriod Lookup
        private TimeFrame MockTimeFrame()
        {
            var timeFrame = new TimeFrame
            {
                DaysInService = 365,
                Timeframe_Std = "Month 12"
            };
            return timeFrame;
        }

        [Fact]
        public void GetTimeFrameDetails_Success_ReturnsCorrectResult()
        {
            var mockTimeFrame = MockTimeFrame();
            this.mock_lookupRepository = new MockLookupRepository().MockGetTimeFrameDetails(mockTimeFrame);
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Success);
            this.initialiseService(localize);
            var result = this.lookupService.GetTimeFrameDetails(365);
            Assert.Equal(result.ResponseStatus, PCISEnum.StatusMessages.Success);
        }

        [Fact]
        public void GetTimeFrameDetails_ExceptionResult()
        {
            this.mock_lookupRepository = new MockLookupRepository().MockGetTimeFrameDetailsException();
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Success);
            this.initialiseService(localize);
            Assert.ThrowsAny<Exception>(() => this.lookupService.GetTimeFrameDetails(0));
        }

        #endregion


        #region Assessment Lookup

        [Fact]

        private List<AssessmentsDTO> GetMockAssessmentDetails()
        {
            return new List<AssessmentsDTO>()
            {
                new AssessmentsDTO()
                {
                    AssessmentID= 37,
                    Time= "Time 1",
                    DateTaken= DateTime.UtcNow,
                    AssessmentReason= "Initial",
                    AssessmentReasonID= 1,
                    AssessmentStatus= "In Progress",
                    AssessmentStatusID= 1
                },
                new AssessmentsDTO()
                  {
                    AssessmentID= 38,
                    Time= "Time 2",
                    DateTaken= DateTime.UtcNow,
                    AssessmentReason= "Initial",
                    AssessmentReasonID= 1,
                    AssessmentStatus= "In Progress",
                    AssessmentStatusID= 1
                  }, new AssessmentsDTO()
                  {
                    AssessmentID= 39,
                    Time= "Time 3",
                    DateTaken= DateTime.UtcNow,
                    AssessmentReason= "Initial",
                    AssessmentReasonID= 1,
                    AssessmentStatus= "In Progress",
                    AssessmentStatusID= 1
                  }, new AssessmentsDTO()
                  {
                    AssessmentID= 40,
                    Time= "Time 4",
                    DateTaken= DateTime.UtcNow,
                    AssessmentReason= "Initial",
                    AssessmentReasonID= 1,
                    AssessmentStatus= "In Progress",
                    AssessmentStatusID= 1
                  }, new AssessmentsDTO()
                  {
                    AssessmentID= 41,
                    Time= "Time 5",
                    DateTaken= DateTime.UtcNow,
                    AssessmentReason= "Initial",
                    AssessmentReasonID= 1,
                    AssessmentStatus= "In Progress",
                    AssessmentStatusID= 1
                  }, new AssessmentsDTO()
                  {
                    AssessmentID= 42,
                    Time= "Time 6",
                    DateTaken= DateTime.UtcNow,
                    AssessmentReason= "Initial",
                    AssessmentReasonID= 1,
                    AssessmentStatus= "In Progress",
                    AssessmentStatusID= 1
                  }, new AssessmentsDTO()
                  {
                    AssessmentID= 122257,
                    Time= "Time 7",
                    DateTaken= DateTime.UtcNow,
                    AssessmentReason= "Triggering Event",
                    AssessmentReasonID= 4,
                    AssessmentStatus= "In Progress",
                    AssessmentStatusID= 1
                  }, new AssessmentsDTO()
                  {
                    AssessmentID= 122275,
                    Time= "Time 8",
                    DateTaken= DateTime.UtcNow,
                    AssessmentReason= "Discharge",
                    AssessmentReasonID= 2,
                    AssessmentStatus= "In Progress",
                    AssessmentStatusID= 1
                  }
                    };
        }

        [Fact]
        public void GetAllAssessments_Success_ReturnsCorrectResult()
        {
            var mockAssessmentList = GetMockAssessmentDetails();

            this.mock_lookupRepository = new MockLookupRepository().MockGetAllAssessments(mockAssessmentList);
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Success);

            this.initialiseService(localize);

            int personQuestionnaireID = 1;
            var result = this.lookupService.GetAllAssessments(personQuestionnaireID);
            Assert.Equal(result.ResponseStatus, PCISEnum.StatusMessages.Success);
        }

        [Fact]
        public void GetAllAssessments_ExceptionResult()
        {
            var mockAssessmentList = new List<AssessmentDetailsDTO>();

            this.mock_lookupRepository = new MockLookupRepository().MockGetAllAssessmentsException(mockAssessmentList);
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Failure);

            this.initialiseService(localize);

            int personQuestionnaireID = 1;
            Assert.ThrowsAny<Exception>(() => this.lookupService.GetAllAssessments(personQuestionnaireID));
        }

        [Fact]
        public void GetAllAssessmentsWithCollaboration_Success_ReturnsCorrectResult()
        {
            var mockAssessmentList = GetMockAssessmentDetails();
            int mockCollaborationID = 1;
            int mockVoiceTypeID = 1;

            this.mock_PersonQuestionnaireRepository = new MockPersonQuestionnaireRepository().MockGetPersonQuestionnaireByID(new PersonQuestionnaireDTO());
            this.mock_lookupRepository = new MockLookupRepository().MockGetAllAssessments(mockAssessmentList, mockCollaborationID, mockVoiceTypeID);
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Success);

            this.initialiseService(localize);

            int personQuestionnaireID = 1;
            UserTokenDetails userTokenDetails = new UserTokenDetails() { UserID = 0, AgencyID = 0 };
            var result = this.lookupService.GetAllAssessments(personQuestionnaireID, mockCollaborationID, mockVoiceTypeID, 1, userTokenDetails);
            Assert.Equal(result.ResponseStatus, PCISEnum.StatusMessages.Success);
        }

        [Fact]
        public void GetAllAssessmentsWithCollaboration_ExceptionResult()
        {
            var mockAssessmentList = new List<AssessmentDetailsDTO>();
            long mockCollaborationID = 1;
            int mockVoiceTypeID = 1;

            this.mock_lookupRepository = new MockLookupRepository().MockGetAllAssessmentsException(mockAssessmentList, mockCollaborationID, mockVoiceTypeID);
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Failure);

            this.initialiseService(localize);

            int personQuestionnaireID = 1;
            UserTokenDetails userTokenDetails = new UserTokenDetails() { UserID = 0, AgencyID = 0 };
            Assert.ThrowsAny<Exception>(() => this.lookupService.GetAllAssessments(personQuestionnaireID, mockCollaborationID, mockVoiceTypeID, 1, userTokenDetails));
        }
        #endregion Assessment Lookup

        #region VoiceTypeNew Lookup        

        [Fact]
        public void GetAllVoiceTypeInDetail_Success_ReturnsCorrectResult()
        {
            var mockVoiceTypeList = MockVoiceTypeInDetailList();
            PeopleDTO peopleDTO = new PeopleDTO();
            peopleDTO.PersonID = 1;
            this.mock_PersonRepository = new MockPersonRepository().MockGetPerson(peopleDTO);
            this.mock_VoiceTypeRepository = new MockVoiceTypeRepository().MockGetAllVoiceTypeInDetail(mockVoiceTypeList);
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Success);
            this.initialiseService(localize);
            UserTokenDetails userTokenDetails = new UserTokenDetails() { UserID = 0, AgencyID = 0 };
            var result = this.lookupService.GetAllVoiceTypeInDetail(new Guid(), new long(), new long(), userTokenDetails);
            Assert.Equal(result.ResponseStatus, PCISEnum.StatusMessages.Success);
        }

        private List<VoiceTypeDTO> MockVoiceTypeInDetailList()
        {
            return new List<VoiceTypeDTO>()
            {
               new VoiceTypeDTO()
               {
                  VoiceTypeID = 1,
                  NameInDetail = "person1",
                  FkIDValue = null,
                  VoiceTypeName = "Consumer"
               },
               new VoiceTypeDTO()
               {
                  VoiceTypeID = 1,
                  NameInDetail = "support1",
                  FkIDValue = 1,
                  VoiceTypeName = "Support"
               }
            };
        }

        [Fact]
        public void GetAllVoiceTypeInDetail_ExceptionResult()
        {
            var mockVoiceTypeList = MockVoiceTypeInDetailList();
            PeopleDTO peopleDTO = new PeopleDTO();
            peopleDTO.PersonID = 1;
            this.mock_PersonRepository = new MockPersonRepository().MockGetPerson(peopleDTO);
            this.mock_VoiceTypeRepository = new MockVoiceTypeRepository().MockGetAllVoiceTypeInDetailException();
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Failure);
            this.initialiseService(localize);
            UserTokenDetails userTokenDetails = new UserTokenDetails() { UserID = 0, AgencyID = 0 };
            Assert.ThrowsAny<Exception>(() => this.lookupService.GetAllVoiceTypeInDetail(new Guid(), new long(), new long(), userTokenDetails));
        }

        #endregion VoiceType Lookup

        #region Identified Gender

        private List<IdentifiedGender> MockIdentifiedGenderList()
        {
            return new List<IdentifiedGender>()
            {
               new IdentifiedGender()
               {
                    IdentifiedGenderID = 1,
                    Name= "Female",
                    Abbrev= "F",
                    Description = "Female",
                    ListOrder=2,
               },
               new IdentifiedGender()
               {
                    IdentifiedGenderID = 1,
                    Name= "Male",
                    Abbrev= "M",
                    Description = "Male",
                    ListOrder=1,
               }
            };
        }

        [Fact]
        public void GetAllIdentifiedGender_Success_ReturnsCorrectResult()
        {
            var mockIdentifiedGenderList = MockIdentifiedGenderList();
            this.mockIdentifiedGenderRepository = new MockIdentifiedGenderRepository().MockGetAllIdentifiedGenderList(mockIdentifiedGenderList);
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Success);
            this.initialiseService(localize);
            var result = this.lookupService.GetAllIdentifiedGender(1);
            Assert.Equal(result.ResponseStatus, PCISEnum.StatusMessages.Success);
        }

        [Fact]
        public void GetAllIdentifiedGender_ExceptionResult()
        {
            var mockIdentifiedGenderList = MockIdentifiedGenderList();
            this.mockIdentifiedGenderRepository = new MockIdentifiedGenderRepository().MockGetAllIdentifiedGenderListException();
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Success);
            this.initialiseService(localize);
            var result = this.lookupService.GetAllIdentifiedGender(1);
            Assert.Equal(result.ResponseStatus, PCISEnum.StatusMessages.Success);
        }
        #endregion Identified Gender

        #region VoiceType Filter Lookup        

        [Fact]
        public void GetVoiceTypeForFilter_Success_ReturnsCorrectResult()
        {
            var mockVoiceTypeList = MockVoiceTypeInDetailListFilter();
            PeopleDTO peopleDTO = new PeopleDTO();
            peopleDTO.PersonID = 1;
            this.mock_PersonRepository = new MockPersonRepository().MockGetPerson(peopleDTO);
            this.mock_VoiceTypeRepository = new MockVoiceTypeRepository().MockGetAllVoiceTypeInDetail(mockVoiceTypeList);

            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Success);
            this.initialiseService(localize);
            var result = this.lookupService.GetVoiceTypeForFilter(new Guid(), new long(), new long());
            Assert.Equal(result.ResponseStatus, PCISEnum.StatusMessages.Success);
        }

        private List<VoiceTypeDTO> MockVoiceTypeInDetailListFilter()
        {
            return new List<VoiceTypeDTO>()
            {
               new VoiceTypeDTO()
               {
                  VoiceTypeID = 1,
                  NameInDetail = "person1",
                  FkIDValue = null,
                  VoiceTypeName = "Consumer"
               },
               new VoiceTypeDTO()
               {
                  VoiceTypeID = 2,
                  NameInDetail = "support1",
                  FkIDValue = 1,
                  VoiceTypeName = "Support"
               }
            };
        }

        [Fact]
        public void GetVoiceTypeForFilter_ExceptionResult()
        {
            var mockVoiceTypeList = MockVoiceTypeInDetailList();
            PeopleDTO peopleDTO = new PeopleDTO();
            peopleDTO.PersonID = 1;
            this.mock_PersonRepository = new MockPersonRepository().MockGetPerson(peopleDTO);
            this.mock_VoiceTypeRepository = new MockVoiceTypeRepository().MockGetAllVoiceTypeInDetailException();

            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Failure);
            this.initialiseService(localize);
            UserTokenDetails userTokenDetails = new UserTokenDetails() { AgencyID = 0, UserID = 0 };
            Assert.ThrowsAny<Exception>(() => this.lookupService.GetAllVoiceTypeInDetail(new Guid(), new long(), new long(), userTokenDetails));
        }

        #endregion VoiceType Filter Lookup        

        #region GetConfigurationValueByName

        [Fact]
        public void GetConfigurationValueByName_Success_ReturnsCorrectResult()
        {
            var configResult = new ConfigurationParameterDTO() { Value = "value" };
            this.mockConfigRepository = new MockConfigurationRepository().GetConfigurationByName(configResult);
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Success);
            this.initialiseService(localize);
            var result = this.lookupService.GetConfigurationValueByName("key1", 1);
            Assert.Equal(result.ResponseStatus, PCISEnum.StatusMessages.Success);
        }
        [Fact]
        public void GetConfigurationValueByName_ExceptionResult()
        {
            this.mockConfigRepository = new MockConfigurationRepository().GetConfigurationByNameException();
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Success);
            this.initialiseService(localize);
            Assert.ThrowsAny<Exception>(() => this.lookupService.GetConfigurationValueByName("key",1));
        }
        #endregion

        #region GetAllConfigurationsForAgency

        [Fact]
        public void GetAllConfigurationsForAgency_Success_ReturnsCorrectResult()
        {
            this.mockConfigRepository = new MockConfigurationRepository().GetConfigurationList(new List<ConfigurationParameterDTO>());
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Success);
            this.initialiseService(localize);
            var result = this.lookupService.GetAllConfigurationsForAgency(1);
            Assert.Equal(result.ResponseStatus, PCISEnum.StatusMessages.Success);
        }
        [Fact]
        public void GetAllConfigurationsForAgency_ExceptionResult()
        {
            this.mockConfigRepository = new MockConfigurationRepository().GetConfigurationListException();
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Success);
            this.initialiseService(localize);
            Assert.ThrowsAny<Exception>(() => this.lookupService.GetAllConfigurationsForAgency(1));
        }
        #endregion
    }
}
