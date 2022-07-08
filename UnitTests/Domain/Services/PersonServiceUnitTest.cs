using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Domain.Services;
using Opeeka.PICS.Infrastructure.Enums;
using Opeeka.PICS.UnitTests.Domain.Mocks.Repositories;
using Opeeka.PICS.Domain.Interfaces.Common;
using Xunit;
using Opeeka.PICS.UnitTests.Domain.Mocks.Common;
using Opeeka.PICS.Domain.Interfaces;
using Opeeka.PICS.Domain.Interfaces.Services;

namespace Opeeka.PICS.UnitTests.Domain.Services
{
    public class PersonServiceUnitTest
    {
        /// Initializes a new instance of the PersonRepository/> class.
        private Mock<IPersonRepository> mockPersonRepository;

        /// Initializes a new instance of the <see cref="logger"/> class.
        private Mock<ILogger<PersonService>> mockLogger;

        /// Initializes a new instance of the <see cref="addressRepository"/> class.
        private Mock<IAddressrepository> mockAddressRepository;

        /// Initializes a new instance of the <see cref="personAddressRepository"/> class.
        private Mock<IPersonAddressRepository> mockPersonAddressRepository;

        /// Initializes a new instance of the <see cref="personSupportRepository"/> class.
        private Mock<IPersonSupportRepository> mockPersonSupportRepository;

        /// Initializes a new instance of the <see cref="personRaceEthnicityRepository"/> class.
        private Mock<IPersonRaceEthnicityRepository> mockPersonRaceEthnicityRepository;

        /// Initializes a new instance of the <see cref="personLanguageRepository"/> class.
        private Mock<IPersonLanguageRepository> mockPersonLanguageRepository;

        /// Initializes a new instance of the <see cref="personIdentificationRepository"/> class.
        private Mock<IPersonIdentificationRepository> mockPersonIdentificationRepository;

        /// Initializes a new instance of the <see cref="personCollaborationRepository"/> class.
        private Mock<IPersonCollaborationRepository> mockPersonCollaborationRepository;

        /// Initializes a new instance of the <see cref="personHelperRepository"/> class.
        private Mock<IPersonHelperRepository> mockPersonHelperRepository;

        /// Initializes a new instance of the <see cref="questionnaireRepository"/> class.
        private Mock<IQuestionnaireRepository> mockQuestionnaireRepository;

        /// Initializes a new instance of the <see cref="personQuestionnaireRepository"/> class.
        private Mock<IPersonQuestionnaireRepository> mockPersonQuestionnaireRepository;

        /// Initializes a new instance of the <see cref="NotifiationResolutionStatusRepository"/> class.
        private Mock<INotifiationResolutionStatusRepository> mockNotifiationResolutionStatusRepository;

        /// Initializes a new instance of the <see cref="NotificationLogRepository"/> class.
        private Mock<INotificationLogRepository> mockNotificationLogRepository;

        /// Initializes a new instance of the <see cref="NotifiationResolutionHistoryRepository"/> class.
        private Mock<INotifiationResolutionHistoryRepository> mockNotifiationResolutionHistoryRepository;

        /// Initializes a new instance of the <see cref="NoteRepository"/> class.
        private Mock<INoteRepository> mockNoteRepository;

        /// Initializes a new instance of the <see cref="NotifiationResolutionNoteRepository"/> class.
        private Mock<INotifiationResolutionNoteRepository> mockNotifiationResolutionNoteRepository;

        /// Initializes a new instance of the <see cref="Mapper"/> class.
        private Mock<IMapper> mockMapper;
        private Mock<IConfigurationRepository> mockConfigRepository;
        private Mock<IHttpContextAccessor> httpContextAccessor;
        private Mock<IUtility> mockUtility;

        private PersonService personService;
        private Mock<ICollaborationQuestionnaireRepository> mockcollaborationQuestionnaireRepository;
        private Mock<IQueryBuilder> mockQueryBuilder;
        private Mock<IQueue> mockQueue;
        private readonly Mock<IPersonQuestionnaireScheduleRepository> mockPersonQuestionnaireScheduleRepository;
        private readonly Mock<IPersonQuestionnaireScheduleService> mockPersonQuestionnaireScheduleService;
        private Mock<IQuestionnaireNotifyRiskRuleConditionRepository> mockQuestionnaireNotifyRiskRuleConditionRepository;
        private Mock<IAuditPersonProfileRepository> mockAuditPersonProfileRepository;
        private Mock<ISMSSender> mockSMSSender;
        private Mock<ILookupRepository> mockLookupRepository;
        private Mock<ILanguageRepository> mockLanguageRepository;
        private Mock<IGenderRepository> mockGenderRepository;
        private Mock<ISexualityRepository> mockSexualityrepository;
        private Mock<IRaceEthnicityRepository> mockRaceEthnicityRepository;
        private Mock<IIdentificationTypeRepository> mockIdentificationrepository;
        private Mock<IIdentifiedGenderRepository> mockIdentifiedGenderRepository;
        private Mock<ISupportTypeRepository> mockSupportTypeRepository;
        private Mock<INotifyReminderRepository> mockNotifyReminderRepository;
        private Mock<INotificationTypeRepository> mockNotificationTypeRepository;
        /// <summary>
        /// Initializes a new instance of the <see cref="PersonService"/> class.
        /// </summary>
        public PersonServiceUnitTest()
        {
            this.mockSMSSender = new Mock<ISMSSender>();
            this.mockPersonRepository = new Mock<IPersonRepository>();
            this.mockLogger = new Mock<ILogger<PersonService>>();
            this.mockAddressRepository = new Mock<IAddressrepository>();
            this.mockPersonAddressRepository = new Mock<IPersonAddressRepository>();
            this.mockPersonSupportRepository = new Mock<IPersonSupportRepository>();
            this.mockPersonRaceEthnicityRepository = new Mock<IPersonRaceEthnicityRepository>();
            this.mockPersonLanguageRepository = new Mock<IPersonLanguageRepository>();
            this.mockPersonIdentificationRepository = new Mock<IPersonIdentificationRepository>();
            this.mockPersonCollaborationRepository = new Mock<IPersonCollaborationRepository>();
            this.mockPersonHelperRepository = new Mock<IPersonHelperRepository>();
            this.mockQuestionnaireRepository = new Mock<IQuestionnaireRepository>();
            this.mockPersonQuestionnaireRepository = new Mock<IPersonQuestionnaireRepository>();
            this.mockConfigRepository = new Mock<IConfigurationRepository>();
            this.httpContextAccessor = new Mock<IHttpContextAccessor>();
            this.mockcollaborationQuestionnaireRepository = new Mock<ICollaborationQuestionnaireRepository>();
            this.mockNotifiationResolutionStatusRepository = new Mock<INotifiationResolutionStatusRepository>();
            this.mockNotificationLogRepository = new Mock<INotificationLogRepository>();
            this.mockMapper = new Mock<IMapper>();
            this.mockNotifiationResolutionHistoryRepository = new Mock<INotifiationResolutionHistoryRepository>();
            this.mockNoteRepository = new Mock<INoteRepository>();
            this.mockNotifiationResolutionNoteRepository = new Mock<INotifiationResolutionNoteRepository>();
            this.mockUtility = new MockUtility().SetupUtility(DateTime.UtcNow);
            var context = new DefaultHttpContext();
            context.Request.Headers[PCISEnum.TokenHeaders.timeZone] = "-330";
            this.httpContextAccessor.Setup(_ => _.HttpContext).Returns(context);
            this.mockQueryBuilder = new MockQueryBuilder().SetupQueryBuilder(new DynamicQueryBuilderDTO());
            this.mockQueue = new Mock<IQueue>();
            this.mockPersonQuestionnaireScheduleRepository = new Mock<IPersonQuestionnaireScheduleRepository>();
            this.mockPersonQuestionnaireScheduleService = new Mock<IPersonQuestionnaireScheduleService>();
            this.mockQuestionnaireNotifyRiskRuleConditionRepository = new Mock<IQuestionnaireNotifyRiskRuleConditionRepository>();
            this.mockAuditPersonProfileRepository = new Mock<IAuditPersonProfileRepository>();
            this.mockLookupRepository = new Mock<ILookupRepository>();
            this.mockLanguageRepository = new Mock<ILanguageRepository>();
            this.mockGenderRepository = new Mock<IGenderRepository>();
            this.mockSexualityrepository = new Mock<ISexualityRepository>();
            this.mockRaceEthnicityRepository = new Mock<IRaceEthnicityRepository>();
            this.mockIdentificationrepository = new Mock<IIdentificationTypeRepository>();
            this.mockIdentifiedGenderRepository = new Mock<IIdentifiedGenderRepository>();
            this.mockSupportTypeRepository = new Mock<ISupportTypeRepository>();
            this.mockNotifyReminderRepository = new Mock<INotifyReminderRepository>();
            this.mockNotificationTypeRepository = new Mock<INotificationTypeRepository>();
        }

        #region InitialiseSeviceWithMockedDI
        private void InitialisePersonService()
        {
            var localize = new MockLocalize().Localize();
            this.personService = new PersonService(this.mockSMSSender.Object, this.mockcollaborationQuestionnaireRepository.Object, this.mockPersonQuestionnaireRepository.Object, this.mockPersonRepository.Object, this.mockLogger.Object, this.mockAddressRepository.Object, this.mockPersonAddressRepository.Object, this.mockPersonIdentificationRepository.Object, this.mockPersonCollaborationRepository.Object, this.mockPersonLanguageRepository.Object, this.mockPersonSupportRepository.Object, this.mockPersonRaceEthnicityRepository.Object, this.mockPersonHelperRepository.Object, this.mockQuestionnaireRepository.Object, this.mockPersonQuestionnaireRepository.Object, this.mockNotifiationResolutionStatusRepository.Object, this.mockNotificationLogRepository.Object, this.mockMapper.Object, localize.Object, mockConfigRepository.Object, httpContextAccessor.Object, this.mockNotifiationResolutionHistoryRepository.Object, this.mockNoteRepository.Object, this.mockNotifiationResolutionNoteRepository.Object, this.mockUtility.Object, this.mockQueryBuilder.Object, this.mockQueue.Object, this.mockPersonQuestionnaireScheduleRepository.Object, this.mockPersonQuestionnaireScheduleService.Object, this.mockQuestionnaireNotifyRiskRuleConditionRepository.Object, this.mockAuditPersonProfileRepository.Object,
              this.mockLookupRepository.Object, this.mockLanguageRepository.Object, this.mockGenderRepository.Object,
              this.mockSexualityrepository.Object, this.mockIdentificationrepository.Object, this.mockRaceEthnicityRepository.Object, this.mockIdentifiedGenderRepository.Object,this.mockSupportTypeRepository.Object,this.mockNotifyReminderRepository.Object, this.mockNotificationTypeRepository.Object);
        }

        #endregion
        /// <summary>
        /// The AddPersonDetails_Success_ReturnsCorrectResult.
        /// </summary>
        /// 
        [Fact]
        public void AddPersonDetails_Success_ReturnsCorrectResult()
        {
            var mockPersonDetails = GetMockPersonDetails();
            var mockperson = GetMockPerson();
            this.mockPersonRepository = new MockPersonRepository().MockPeopleAdd(mockperson);
            this.mockAddressRepository = new MockAddressRepository().MockAddAddress(1);
            this.mockPersonAddressRepository = new MockPersonAddressRepository().MockPersonAddressAdd(1);
            this.mockPersonRaceEthnicityRepository = new MockpersonRaceEthnicityRepository().MockpersonRaceEthnicityADD(1);
            this.mockPersonIdentificationRepository = new MockpersonIdentificationRepository().MockpersonIdentificationAdd(1);
            this.mockPersonSupportRepository = new MockPersonSupportRepository().MockPeopleSupport(1);
            this.mockPersonCollaborationRepository = new MockpersonCollaborationRepository().MockpersonCollaborationAdd(1);
            this.mockPersonLanguageRepository = new MockpersonLanguageRepository().MockpersonLanguageAdd(1);
            this.mockPersonHelperRepository = new MockpersonHelperRepository().MockpersonHelperAdd(1);
            this.mockcollaborationQuestionnaireRepository = new MockcollaborationQuestionnaireRepository().MockcollaborationQuestionnaireAdd(1);
            this.mockPersonQuestionnaireRepository = new MockPersonQuestionnaireRepository().MockPersonQuestionnaireAdd(1);
            InitialisePersonService();
             var result = this.personService.AddPeopleDetails(mockPersonDetails);
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.InsertionSuccess);
        }

        /// <summary>
        /// The EditPersonDetails_Success_ReturnsCorrectResult.
        /// </summary>
        //[Fact]
        //public void EditPersonDetails_Success_ReturnsCorrectResult()
        //{
        //    var mockPersonEditDetails = GetMockPersonEditDetails();
        //    var mockPersonDetails = GetMockPersonDetails();
        //    var mockPersonEditRaceEthnicity = GetMockPersonEditRaceEthnicity();
        //    var MockPeopleData = GetMockPeopleData();
        //    var MockPersonEditIdentification = GetMockPersonEditIdentification();
        //    var MockPersonEditIdentificationDTO = GetMockPersonEditIdentificationDTO();
        //    var mockPersonraceList = GetMockPersonEditRaceEthnicityList();
        //    var MockPersonhelperList = GetMockPersonhelperList();
        //    var MockPersonhelper = GetMockPersonhelper();
        //    var MockPersonSupport = GetMockPersonSupport();
        //    var MockPersonSupportList = GetPersonSupportList();
        //    var MockPersonCollaborationList = GetMockPersonCollaborationList();
        //    var MockPersonCollaboration = GetMockPersonCollaboration();
        //    var MockPersonAddressDTO = GetMockPersonAddressDTO();
        //    var MockAddressDTO = GetMockAddressDTO();
        //    var mockperson = GetMockPerson();
        //    this.mockPersonRepository = new MockPersonRepository().MockPeopleEditDetails(mockperson);
        //    this.mockAddressRepository = new MockAddressRepository().MockEditAddress(MockAddressDTO);
        //    this.mockPersonAddressRepository = new MockPersonAddressRepository().MockPersonAddressEdit(MockPersonAddressDTO);
        //    this.mockPersonRaceEthnicityRepository = new MockpersonRaceEthnicityRepository().MockpersonRaceEthnicityEdit(mockPersonEditRaceEthnicity, mockPersonraceList);
        //    this.mockPersonIdentificationRepository = new MockpersonIdentificationRepository().MockpersonIdentificationEdit(MockPersonEditIdentification, MockPersonEditIdentificationDTO);
        //    this.mockPersonSupportRepository = new MockPersonSupportRepository().MockPeopleSupportEdit(MockPersonSupport, MockPersonSupportList);
        //    this.mockPersonCollaborationRepository = new MockpersonCollaborationRepository().MockpersonCollaborationEdit(MockPersonCollaboration, MockPersonCollaborationList);
        //    this.mockPersonHelperRepository = new MockpersonHelperRepository().MockpersonHelperEdit(MockPersonhelper, MockPersonhelperList);
        //    this.mockcollaborationQuestionnaireRepository = new MockcollaborationQuestionnaireRepository().MockcollaborationQuestionnaireAdd(1);
        //    this.mockPersonQuestionnaireRepository = new MockPersonQuestionnaireRepository().MockPersonQuestionnaireAdd(1);
        //    var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.UpdationSuccess);
        //    this.personService = new PersonService(this.mockcollaborationQuestionnaireRepository.Object, this.mockPersonQuestionnaireRepository.Object, this.mockPersonRepository.Object, this.mockLogger.Object, this.mockAddressRepository.Object, this.mockPersonAddressRepository.Object, this.mockPersonIdentificationRepository.Object, this.mockPersonCollaborationRepository.Object, this.mockPersonLanguageRepository.Object, this.mockPersonSupportRepository.Object, this.mockPersonRaceEthnicityRepository.Object, this.mockPersonHelperRepository.Object, this.mockQuestionnaireRepository.Object, this.mockPersonQuestionnaireRepository.Object, this.mockNotifiationResolutionStatusRepository.Object, this.mockNotificationLogRepository.Object, this.mockMapper.Object, localize.Object, mockConfigRepository.Object, httpContextAccessor.Object, this.mockNotifiationResolutionHistoryRepository.Object, this.mockNoteRepository.Object, this.mockNotifiationResolutionNoteRepository.Object, this.mockUtility.Object, this.mockQueryBuilder.Object, this.mockQueue.Object, this.mockPersonQuestionnaireScheduleRepository.Object, this.mockPersonQuestionnaireScheduleService.Object);

        //    var result = this.personService.EditPeopleDetails(mockPersonEditDetails);
        //    Assert.Equal(result.ResponseStatus, PCISEnum.StatusMessages.UpdationSuccess);
        //}

        /// <summary>
        /// The AddPersonDetails_Failure_InvalidParameterResult.
        /// </summary>
        //[Fact]
        //public void AddPersonDetails_Failure_InvalidParameterResult()
        //{
        //    var mockPersonDetails = new PeopleDetailsDTO();
        //    var mockperson = GetMockPerson();
        //    this.mockPersonRepository = new MockPersonRepository().MockPeopleAdd(mockperson);
        //    this.mockAddressRepository = new MockAddressRepository().MockAddAddress(1);
        //    this.mockPersonAddressRepository = new MockPersonAddressRepository().MockPersonAddressAdd(1);
        //    this.mockPersonRaceEthnicityRepository = new MockpersonRaceEthnicityRepository().MockpersonRaceEthnicityADD(1);
        //    this.mockPersonIdentificationRepository = new MockpersonIdentificationRepository().MockpersonIdentificationAdd(1);
        //    this.mockPersonSupportRepository = new MockPersonSupportRepository().MockPeopleSupport(1);
        //    this.mockPersonCollaborationRepository = new MockpersonCollaborationRepository().MockpersonCollaborationAdd(1);
        //    this.mockPersonLanguageRepository = new MockpersonLanguageRepository().MockpersonLanguageAdd(1);
        //    this.mockPersonHelperRepository = new MockpersonHelperRepository().MockpersonHelperAdd(1);
        //    this.mockcollaborationQuestionnaireRepository = new MockcollaborationQuestionnaireRepository().MockcollaborationQuestionnaireAdd(1);
        //    this.mockPersonQuestionnaireRepository = new MockPersonQuestionnaireRepository().MockPersonQuestionnaireAdd(1);
        //    var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.insertionFailed);
        //    this.personService = new PersonService(this.mockcollaborationQuestionnaireRepository.Object, this.mockPersonQuestionnaireRepository.Object, this.mockPersonRepository.Object, this.mockLogger.Object, this.mockAddressRepository.Object, this.mockPersonAddressRepository.Object, this.mockPersonIdentificationRepository.Object, this.mockPersonCollaborationRepository.Object, this.mockPersonLanguageRepository.Object, this.mockPersonSupportRepository.Object, this.mockPersonRaceEthnicityRepository.Object, this.mockPersonHelperRepository.Object, this.mockQuestionnaireRepository.Object, this.mockPersonQuestionnaireRepository.Object, this.mockNotifiationResolutionStatusRepository.Object, this.mockNotificationLogRepository.Object, this.mockMapper.Object, localize.Object, mockConfigRepository.Object, httpContextAccessor.Object, this.mockNotifiationResolutionHistoryRepository.Object, this.mockNoteRepository.Object, this.mockNotifiationResolutionNoteRepository.Object, this.mockUtility.Object, this.mockQueryBuilder.Object, this.mockQueue.Object, this.mockPersonQuestionnaireScheduleRepository.Object, this.mockPersonQuestionnaireScheduleService.Object);

        //    var result = this.personService.AddPeopleDetails(mockPersonDetails);
        //    Assert.Equal(result.ResponseStatus, PCISEnum.StatusMessages.insertionFailed);
        //}


        /// <summary>
        /// The AddPersonQuestionaire_Failure_InvalidParameterResult.
        /// </summary>
        [Fact]
        public void AddPersonQuestionaire_Failure_InvalidParameterResult()
        {
            var mockPersonQuestionaire = new PersonQuestionnaireDetailsDTO();
            var mockperson = GetMockPerson();
            this.mockPersonQuestionnaireRepository = new MockPersonQuestionnaireRepository().MockPersonQuestionnaireAdd(1);
            this.mockPersonRepository = new MockPersonRepository().MockPeopleEditDetails(mockperson);
            InitialisePersonService();

            var result = this.personService.AddPersonQuestionaire(mockPersonQuestionaire, 0, 1);
            Assert.Equal(result.ResponseStatus, PCISEnum.StatusMessages.insertionFailed);
        }

        /// <summary>
        /// The AddPersonQuestionaire_Success.
        /// </summary>
        [Fact]
        public void AddPersonQuestionaire_Success()
        {
            var mockperson = GetMockPerson();
            var mockPersonQuestionaire = GetMockPersonQuestionaire();
            this.mockPersonRepository = new MockPersonRepository().MockGetPerson(mockperson);
            this.mockPersonQuestionnaireRepository = new MockPersonQuestionnaireRepository().MockPersonQuestionnaireAdd(1);
            this.mockNotifiationResolutionStatusRepository = new Mock<INotifiationResolutionStatusRepository>();
            this.mockNotificationLogRepository = new Mock<INotificationLogRepository>();
            this.mockMapper = new Mock<IMapper>();
            this.mockConfigRepository = new Mock<IConfigurationRepository>();
            this.httpContextAccessor = new Mock<IHttpContextAccessor>();
            this.mockNotifiationResolutionHistoryRepository = new Mock<INotifiationResolutionHistoryRepository>();
            this.mockNoteRepository = new Mock<INoteRepository>();
            this.mockNotifiationResolutionNoteRepository = new Mock<INotifiationResolutionNoteRepository>();
            this.mockUtility = new Mock<IUtility>();
            InitialisePersonService();

            var result = this.personService.AddPersonQuestionaire(mockPersonQuestionaire, 1, 1);
            Assert.Equal(result.ResponseStatus, PCISEnum.StatusMessages.InsertionSuccess);
        }

        /// <summary>
        /// The EditPersonDetails_Failure_InvalidParameterResult.
        /// </summary>
        [Fact]
        public void EditPersonDetails_Failure_InvalidParameterResult()
        {
            var mockPersonDetails = new PeopleEditDetailsDTO();
            this.mockAddressRepository = new MockAddressRepository().MockAddAddress(1);
            this.mockPersonAddressRepository = new MockPersonAddressRepository().MockPersonAddressAdd(1);
            this.mockPersonRaceEthnicityRepository = new MockpersonRaceEthnicityRepository().MockpersonRaceEthnicityADD(1);
            this.mockPersonIdentificationRepository = new MockpersonIdentificationRepository().MockpersonIdentificationAdd(1);
            this.mockPersonSupportRepository = new MockPersonSupportRepository().MockPeopleSupport(1);
            this.mockPersonCollaborationRepository = new MockpersonCollaborationRepository().MockpersonCollaborationAdd(1);
            this.mockPersonLanguageRepository = new MockpersonLanguageRepository().MockpersonLanguageAdd(1);
            this.mockPersonHelperRepository = new MockpersonHelperRepository().MockpersonHelperAdd(1);
            this.mockcollaborationQuestionnaireRepository = new MockcollaborationQuestionnaireRepository().MockcollaborationQuestionnaireAdd(1);
            this.mockPersonQuestionnaireRepository = new MockPersonQuestionnaireRepository().MockPersonQuestionnaireAdd(1);
            InitialisePersonService();

            var result = this.personService.EditPeopleDetails(mockPersonDetails, 1);
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.UpdationFailed);
        }

        [Fact]
        public void GetAllRiskNotification_Success_ReturnsCorrectResult()
        {
            var mockPersonDetail = GetMockPerson();
            var mockRiskNotifications = GetMockRiskNotifications();
            var mockReminderNotifications = GetMockReminderNotifications();
            this.mockPersonRepository = new MockPersonRepository().MockGetNotifications(mockPersonDetail, mockRiskNotifications, mockReminderNotifications);
            InitialisePersonService();
            Guid personIndex = Guid.NewGuid();
            var result = this.personService.GetRiskNotificationList(personIndex);
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.Success);
        }

        [Fact]
        public void GetAllRiskNotification_ReturnsNoResult()
        {
            var mockPersonDetail = GetMockPerson();
            var mockRiskNotifications = new List<RiskNotificationsListDTO>();
            var mockReminderNotifications = new List<ReminderNotificationsListDTO>();
            this.mockPersonRepository = new MockPersonRepository().MockGetNotifications(mockPersonDetail, mockRiskNotifications, mockReminderNotifications);
            InitialisePersonService();
            Guid personIndex = Guid.NewGuid();
            var result = this.personService.GetRiskNotificationList(personIndex);
            Assert.Null(result.RiskNotifications);
            Assert.Null(result.ReminderNotifications);
        }

        [Fact]
        public void GetAllRiskNotification_ExceptionResult()
        {
            var mockPersonDetail = GetMockPersonDetail();
            var mockRiskNotifications = new List<RiskNotificationsListDTO>();
            var mockReminderNotifications = new List<ReminderNotificationsListDTO>();
            this.mockPersonRepository = new MockPersonRepository().MockGetNotificationsException(mockPersonDetail, mockRiskNotifications, mockReminderNotifications);
            InitialisePersonService();
            Guid personIndex = Guid.NewGuid();
            Assert.ThrowsAny<Exception>(() => this.personService.GetRiskNotificationList(personIndex));
        }

        [Fact]
        public void GetAllpastNotification_Success_ReturnsCorrectResult()
        {
            var mockPersonDetail = GetMockPerson();
            var mockPastNotifications = GetMockPastNotifications();
            this.mockPersonRepository = new MockPersonRepository().MockGetPastNotifications(mockPersonDetail, mockPastNotifications);
            InitialisePersonService();
            Guid personIndex = Guid.NewGuid();
            var result = this.personService.GetPastNotificationList(personIndex);
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.Success);
        }

        [Fact]
        public void GetAllPastNotification_ReturnsNoResult()
        {
            var mockPersonDetail = GetMockPerson();
            var mockPastNotifications = new List<PastNotificationsListDTO>();
            this.mockPersonRepository = new MockPersonRepository().MockGetPastNotifications(mockPersonDetail, mockPastNotifications);
            InitialisePersonService();
            Guid personIndex = Guid.NewGuid();
            var result = this.personService.GetPastNotificationList(personIndex);
            Assert.Null(result.PastNotifications);
        }

        [Fact]
        public void GetAllPastNotification_ExceptionResult()
        {
            var mockPersonDetail = GetMockPersonDetail();
            var mockPastNotifications = new List<PastNotificationsListDTO>();
            this.mockPersonRepository = new MockPersonRepository().MockGetPastNotificationsException(mockPersonDetail, mockPastNotifications);
            InitialisePersonService();
            Guid personIndex = Guid.NewGuid();
            Assert.ThrowsAny<Exception>(() => this.personService.GetPastNotificationList(personIndex));
        }

        [Fact]
        public void GetAllQuestionnairesForPerson_Success_ReturnsCorrectResult()
        {
            var mockPersonData = GetMockPerson();
            var mockquestionnaireData = GetMockQuestionnaires();
            var mockPersonQuestionnaireData = GetMockPersonQuestionnaires();
            this.mockPersonRepository = new MockPersonRepository().MockPersonData(mockPersonData);
            this.mockQuestionnaireRepository = new MockQuestionnaireRepository().MockQuestionnaireData(mockquestionnaireData);
            this.mockPersonQuestionnaireRepository = new MockPersonQuestionnaireRepository().MockPersonQuestionnaireData(mockPersonQuestionnaireData);
            InitialisePersonService();
            Guid personIndex = Guid.NewGuid();
            long agencyID = 1;
            int pageNumber = 1;
            int pageSize = 10;
            var result = this.personService.GetAllQuestionnaireForPerson(agencyID, personIndex, pageNumber, pageSize);
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.Success);
        }

        [Fact]
        public void GetAllQuestionnairesForPerson_ReturnsNoResult()
        {
            var mockPersonDetail = GetMockPerson();
            var mockquestionnaireData = new List<PersonQuestionnaireDataDTO>();
            var mockPersonQuestionnaireData = GetMockPersonQuestionnaires();
            this.mockPersonRepository = new MockPersonRepository().MockPersonData(mockPersonDetail);
            this.mockQuestionnaireRepository = new MockQuestionnaireRepository().MockQuestionnaireData(mockquestionnaireData);
            this.mockPersonQuestionnaireRepository = new MockPersonQuestionnaireRepository().MockPersonQuestionnaireData(mockPersonQuestionnaireData);
            InitialisePersonService();
            Guid personIndex = Guid.NewGuid();
            long agencyID = 1;
            int pageNumber = 1;
            int pageSize = 10;
            var result = this.personService.GetAllQuestionnaireForPerson(agencyID, personIndex, pageNumber, pageSize);
            Assert.Null(result.PersonQuestionnaireDataDTO);
        }

        [Fact]
        public void GetAllQuestionnairesForPerson_Failure_InvalidParameterResult()
        {
            var mockPersonDetail = GetMockPerson();
            var mockquestionnaireData = new List<PersonQuestionnaireDataDTO>();
            var mockPersonQuestionnaireData = GetMockPersonQuestionnaires();
            this.mockPersonRepository = new MockPersonRepository().MockPersonData(mockPersonDetail);
            this.mockQuestionnaireRepository = new MockQuestionnaireRepository().MockQuestionnaireData(mockquestionnaireData);
            this.mockPersonQuestionnaireRepository = new MockPersonQuestionnaireRepository().MockPersonQuestionnaireData(mockPersonQuestionnaireData);
            InitialisePersonService();
            Guid personIndex = Guid.NewGuid();
            long agencyID = 1;
            int pageNumber = 0;
            int pageSize = 10;
            var result = this.personService.GetAllQuestionnaireForPerson(agencyID, personIndex, pageNumber, pageSize);
            Assert.Null(result.PersonQuestionnaireDataDTO);
        }

        [Fact]
        public void GetAllQuestionnairesForPerson_ExceptionResult()
        {
            var mockPersonDetail = GetMockPerson();
            var mockquestionnaireData = new List<PersonQuestionnaireDataDTO>();
            var mockPersonQuestionnaireData = GetMockPersonQuestionnaires();
            this.mockPersonRepository = new MockPersonRepository().MockPersonData(mockPersonDetail);
            this.mockQuestionnaireRepository = new MockQuestionnaireRepository().MockQuestionnaireDataException(mockquestionnaireData);
            this.mockPersonQuestionnaireRepository = new MockPersonQuestionnaireRepository().MockPersonQuestionnaireData(mockPersonQuestionnaireData);
            InitialisePersonService();
            Guid personIndex = Guid.NewGuid();
            long agencyID = 1;
            int pageNumber = 1;
            int pageSize = 10;
            Assert.ThrowsAny<Exception>(() => this.personService.GetAllQuestionnaireForPerson(agencyID, personIndex, pageNumber, pageSize));
        }

        private List<PersonQuestionnaireDataDTO> GetMockQuestionnaires()
        {
            return new List<PersonQuestionnaireDataDTO>()
            {
                new PersonQuestionnaireDataDTO()
                {
                    QuestionnaireID = 1,
                    InstrumentID = 1,
                    AgencyID = 1,
                    Name = "Questionnaire 1",
                    QuestionnaireName = "1-ANSA-Q1",
                    QuestionnaireAbbrev = "Q1",
                    ReminderScheduleName = "Reminder Name",
                    NotificationScheduleName = "Self Harm Only",
                    InstrumentName = "Adult Needs and Strengths Assessment",
                    InstrumentAbbrev = "ANSA",
                    IsBaseQuestionnaire = false,
                    PersonID = 1,
                    StartDate = DateTime.UtcNow,
                    EndDate = null
                },
                new PersonQuestionnaireDataDTO()
                {
                    QuestionnaireID = 2,
                    InstrumentID = 1,
                    AgencyID = 1,
                    Name = "1Questionnaire 2",
                    QuestionnaireName = "2-ANSA-Q2",
                    QuestionnaireAbbrev = "Q2",
                    ReminderScheduleName = "Reminder Name",
                    NotificationScheduleName = "Self Harm Only",
                    InstrumentName = "Adult Needs and Strengths Assessment",
                    InstrumentAbbrev = "ANSA",
                    IsBaseQuestionnaire = false,
                    PersonID = 1,
                    StartDate = DateTime.UtcNow,
                    EndDate = null
                },
            };
        }

        private List<PersonQuestionnaireDTO> GetMockPersonQuestionnaires()
        {
            return new List<PersonQuestionnaireDTO>()
            {
                new PersonQuestionnaireDTO()
                {
                    PersonQuestionnaireID = 1,
                    PersonID = 1,
                    QuestionnaireID = 1,
                    CollaborationID = 1,
                    StartDate = DateTime.UtcNow,
                    EndDueDate = null,
                    IsActive = true,
                    IsRemoved = false,
                    UpdateDate = DateTime.UtcNow,
                    UpdateUserID = 1,
                }
            };
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

        private List<RiskNotificationsListDTO> GetMockRiskNotifications()
        {
            return new List<RiskNotificationsListDTO>()
            {
                new RiskNotificationsListDTO()
                {
                    NotificationLogID = 1,
                    NotificationType = "Danger",
                    NotificationDate = DateTime.UtcNow,
                    Status = "Unresolved",
                    Note = null,
                    PersonID = 1,
                    IsRemoved = false,
                    NotificationTypeID = 1,
                    NotifyRiskID = 1,
                    QuestionnaireNotifyRiskRuleID = 1,
                    AssessmentID = 1,
                    NotifyDate = null,
                    CloseDate = null,
                    RiskIsRemoved = false,
                },
                new RiskNotificationsListDTO()
                {
                    NotificationLogID = 2,
                    NotificationType = "Danger",
                    NotificationDate = DateTime.UtcNow,
                    Status = "Unresolved",
                    Note = null,
                    PersonID = 1,
                    IsRemoved = false,
                    NotificationTypeID = 1,
                    NotifyRiskID = 1,
                    QuestionnaireNotifyRiskRuleID = 1,
                    AssessmentID = 1,
                    NotifyDate = null,
                    CloseDate = null,
                    RiskIsRemoved = false,
                },
            };
        }

        private List<ReminderNotificationsListDTO> GetMockReminderNotifications()
        {
            return new List<ReminderNotificationsListDTO>()
            {
                new ReminderNotificationsListDTO()
                {
                    NotificationLogID = 3,
                    NotificationType = "Reminder",
                    NotificationDate = DateTime.UtcNow,
                    Status = "Unresolved",
                    Note = null,
                    PersonID = 1,
                    IsRemoved = false,
                    NotificationTypeID = 2,
                    NotifyReminderID = 1,
                    NotifyDate = DateTime.UtcNow,
                    PersonQuestionnaireScheduleID = 1,
                    QuestionnaireReminderRuleID = 1,
                },
                new ReminderNotificationsListDTO()
                {
                    NotificationLogID = 4,
                    NotificationType = "Reminder",
                    NotificationDate = DateTime.UtcNow,
                    Status = "Unresolved",
                    Note = null,
                    PersonID = 1,
                    IsRemoved = false,
                    NotificationTypeID = 2,
                    NotifyReminderID = 2,
                    NotifyDate = DateTime.UtcNow,
                    PersonQuestionnaireScheduleID = 1,
                    QuestionnaireReminderRuleID = 1,
                },
            };
        }

        private List<PastNotificationsListDTO> GetMockPastNotifications()
        {
            return new List<PastNotificationsListDTO>()
            {
                new PastNotificationsListDTO()
                {
                    NotificationLogID = 1,
                    NotificationType = "Danger",
                    NotificationDate = DateTime.UtcNow,
                    Status = "Resolved",
                    Note = null,
                    PersonID = 1,
                    IsRemoved = false,
                    NotificationTypeID = 1,
                    FKeyValue = 1,
                    NotificationResolutionStatusID = 2
                },
                new PastNotificationsListDTO()
                {
                    NotificationLogID = 2,
                    NotificationType = "Reminder",
                    NotificationDate = DateTime.UtcNow,
                    Status = "Resolved",
                    Note = null,
                    PersonID = 1,
                    IsRemoved = false,
                    NotificationTypeID = 1,
                    FKeyValue = 1,
                    NotificationResolutionStatusID = 2
                },
            };
        }

        //[Fact]
        //public void UpdateNotificationStatus_Success_ReturnsCorrectResult()
        //{
        //    var mockNotificationData = GetMockNotificationLogDetails();
        //    var mockNotificationStatus = GetMockNotificationStatus();
        //    var mockNotificationLog = GetMockNotificationLog();
        //    var mockNotificationResolutionHistory = GetMockNotificationResolutionHistory();
        //    this.mockNotifiationResolutionStatusRepository = new MockNotificationResolutionStatusRepository().MockGetNotificationStatus(mockNotificationStatus);
        //    this.mockNotificationLogRepository = new MockNotificationLogRepository().MockUpdateNotificationLog(mockNotificationData, mockNotificationLog);
        //    this.mockNotifiationResolutionHistoryRepository = new MockNotificationResolutionHistoryRepository().MockAddNotificationResolutionHistory(mockNotificationResolutionHistory);
        //    var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.UpdationSuccess);
        //    this.personService = new PersonService(this.mockcollaborationQuestionnaireRepository.Object, this.mockPersonQuestionnaireRepository.Object, this.mockPersonRepository.Object, this.mockLogger.Object, this.mockAddressRepository.Object, this.mockPersonAddressRepository.Object, this.mockPersonIdentificationRepository.Object, this.mockPersonCollaborationRepository.Object, this.mockPersonLanguageRepository.Object, this.mockPersonSupportRepository.Object, this.mockPersonRaceEthnicityRepository.Object, this.mockPersonHelperRepository.Object, this.mockQuestionnaireRepository.Object, this.mockPersonQuestionnaireRepository.Object, this.mockNotifiationResolutionStatusRepository.Object, this.mockNotificationLogRepository.Object, this.mockMapper.Object, localize.Object, mockConfigRepository.Object, httpContextAccessor.Object, this.mockNotifiationResolutionHistoryRepository.Object, this.mockNoteRepository.Object, this.mockNotifiationResolutionNoteRepository.Object, this.mockUtility.Object, this.mockQueryBuilder.Object, this.mockQueue.Object, this.mockPersonQuestionnaireScheduleRepository.Object, this.mockPersonQuestionnaireScheduleService.Object, this.mockQuestionnaireNotifyRiskRuleConditionRepository.Object, this.mockAuditPersonProfileRepository.Object);
        //    int notificationlogID = 1;
        //    string status = "Unresolved";
        //    var result = this.personService.UpdateNotificationStatus(notificationlogID, status);
        //    Assert.Equal(result.ResponseStatus, PCISEnum.StatusMessages.UpdationSuccess);
        //}

        //[Fact]
        //public void UpdateNotificationStatus_Returns_Exception()
        //{
        //    var mockNotificationData = GetMockNotificationLogDetails();
        //    var mockNotificationStatus = GetMockNotificationStatus();
        //    var mockNotificationLog = new NotificationLog();
        //    var mockNotificationResolutionHistory = GetMockNotificationResolutionHistory();
        //    this.mockNotifiationResolutionStatusRepository = new MockNotificationResolutionStatusRepository().MockGetNotificationStatus(mockNotificationStatus);
        //    this.mockNotificationLogRepository = new MockNotificationLogRepository().MockUpdateNotificationLogException(mockNotificationData, mockNotificationLog);
        //    this.mockNotifiationResolutionHistoryRepository = new MockNotificationResolutionHistoryRepository().MockAddNotificationResolutionHistory(mockNotificationResolutionHistory);
        //    var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Failure);
        //    this.personService = new PersonService(this.mockcollaborationQuestionnaireRepository.Object, this.mockPersonQuestionnaireRepository.Object, this.mockPersonRepository.Object, this.mockLogger.Object, this.mockAddressRepository.Object, this.mockPersonAddressRepository.Object, this.mockPersonIdentificationRepository.Object, this.mockPersonCollaborationRepository.Object, this.mockPersonLanguageRepository.Object, this.mockPersonSupportRepository.Object, this.mockPersonRaceEthnicityRepository.Object, this.mockPersonHelperRepository.Object, this.mockQuestionnaireRepository.Object, this.mockPersonQuestionnaireRepository.Object, this.mockNotifiationResolutionStatusRepository.Object, this.mockNotificationLogRepository.Object, this.mockMapper.Object, localize.Object, mockConfigRepository.Object, httpContextAccessor.Object, this.mockNotifiationResolutionHistoryRepository.Object, this.mockNoteRepository.Object, this.mockNotifiationResolutionNoteRepository.Object, this.mockUtility.Object, this.mockQueryBuilder.Object, this.mockQueue.Object, this.mockPersonQuestionnaireScheduleRepository.Object, this.mockPersonQuestionnaireScheduleService.Object, this.mockQuestionnaireNotifyRiskRuleConditionRepository.Object, this.mockAuditPersonProfileRepository.Object);
        //    int notificationlogID = 1;
        //    string status = "Unresolved";
        //    Assert.ThrowsAny<Exception>(() => this.personService.UpdateNotificationStatus(notificationlogID, status));
        //}

        private NotificationResolutionStatus GetMockNotificationStatus()
        {
            return new NotificationResolutionStatus()
            {
                NotificationResolutionStatusID = 1,
                Name = "Resolved",
                ListOrder = 2,
                IsRemoved = false,
                UpdateDate = DateTime.UtcNow,
                UpdateUserID = 1
            };
        }

        private NotificationLogDTO GetMockNotificationLogDetails()
        {
            return new NotificationLogDTO()
            {
                NotificationLogID = 1,
                NotificationDate = DateTime.UtcNow,
                PersonID = 1,
                IsRemoved = false,
                NotificationTypeID = 1,
                FKeyValue = 1,
                NotificationResolutionStatusID = 1
            };
        }

        private NotificationLog GetMockNotificationLog()
        {
            return new NotificationLog()
            {
                NotificationLogID = 1,
                NotificationDate = DateTime.UtcNow,
                PersonID = 1,
                IsRemoved = false,
                NotificationTypeID = 1,
                FKeyValue = 1,
                NotificationResolutionStatusID = 2
            };
        }

        private NotificationResolutionHistory GetMockNotificationResolutionHistory()
        {
            return new NotificationResolutionHistory()
            {
                NotificationResolutionHistoryID = 1,
                NotificationLogID = 1,
                StatusFrom = 1,
                StatusTo = 2,
                IsRemoved = false,
                UpdateDate = DateTime.UtcNow,
                UpdateUserID = 1
            };
        }

        [Fact]
        public void GetAllPastNotes_Success_ReturnsCorrectResult()
        {
            var mockPastNotes = GetMockPastNotes();
            this.mockPersonRepository = new MockPersonRepository().MockGetPastNotes(mockPastNotes);
            var questionnaireRiskItemDetailsDTO = new List<QuestionnaireRiskItemDetailsDTO>();
            this.mockQuestionnaireNotifyRiskRuleConditionRepository = new MockQuestionnaireNotifyRiskRuleConditionRepository().MockGetRiskItemDetails(questionnaireRiskItemDetailsDTO);

            InitialisePersonService();
            int notificationLogID = 1;
            int pageNumber = 1;
            int pageSize = 10;
            var result = this.personService.GetAllPastNotes(notificationLogID, pageNumber, pageSize);
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.Success);
        }

        [Fact]
        public void GetAllPastNotes_ReturnsNoResult()
        {
            var mockPastNotes = new List<NotificationNotesDTO>();
            this.mockPersonRepository = new MockPersonRepository().MockGetPastNotes(mockPastNotes);
            var questionnaireRiskItemDetailsDTO = new List<QuestionnaireRiskItemDetailsDTO>();
            this.mockQuestionnaireNotifyRiskRuleConditionRepository = new MockQuestionnaireNotifyRiskRuleConditionRepository().MockGetRiskItemDetails(questionnaireRiskItemDetailsDTO);

            InitialisePersonService();
            int notificationLogID = 1;
            int pageNumber = 1;
            int pageSize = 10;
            var result = this.personService.GetAllPastNotes(notificationLogID, pageNumber, pageSize);
            Assert.Null(result.NotificationNotes);
        }

        [Fact]
        public void GetAllPastNotes_Failure_InvalidParameterResult()
        {
            var mockPastNotes = new List<NotificationNotesDTO>();
            this.mockPersonRepository = new MockPersonRepository().MockGetPastNotes(mockPastNotes);
            var questionnaireRiskItemDetailsDTO = new List<QuestionnaireRiskItemDetailsDTO>();
            this.mockQuestionnaireNotifyRiskRuleConditionRepository = new MockQuestionnaireNotifyRiskRuleConditionRepository().MockGetRiskItemDetails(questionnaireRiskItemDetailsDTO);

            InitialisePersonService();
            int notificationLogID = 1;
            int pageNumber = 0;
            int pageSize = 10;
            var result = this.personService.GetAllPastNotes(notificationLogID, pageNumber, pageSize);
            Assert.Null(result.NotificationNotes);
        }

        [Fact]
        public void GetAllPastNotes_ExceptionResult()
        {
            var mockPastNotes = new List<NotificationNotesDTO>();
            this.mockPersonRepository = new MockPersonRepository().MockGetPastNotesException(mockPastNotes);
            InitialisePersonService();
            int notificationLogID = 1;
            int pageNumber = 1;
            int pageSize = 10;
            Assert.ThrowsAny<Exception>(() => this.personService.GetAllPastNotes(notificationLogID, pageNumber, pageSize));
        }

        private List<NotificationNotesDTO> GetMockPastNotes()
        {
            return new List<NotificationNotesDTO>()
            {
                new NotificationNotesDTO()
                {
                    NotificationLogID = 1,
                    NotificationResolutionNoteID = 1,
                    NotificationResolutionHistoryID = 1,
                    NoteID = 1,
                    NoteText = "nec tempus mauris erat eget ipsum. Suspendisse sagittis.",
                    UpdateDate = DateTime.UtcNow,
                    HelperID = 1,
                    UpdateUserID = 1,
                    User = "Mark Smith",
                    HelperTitleID = 1,
                    HelperTitle = "Doctor",
                },
                new NotificationNotesDTO()
                {
                    NotificationLogID = 1,
                    NotificationResolutionNoteID = 1,
                    NotificationResolutionHistoryID = 1,
                    NoteID = 2,
                    NoteText = "egestas. Aliquam nec enim. Nunc.",
                    UpdateDate = DateTime.UtcNow,
                    HelperID = 1,
                    UpdateUserID = 1,
                    User = "Mark Smith",
                    HelperTitleID = 1,
                    HelperTitle = "Doctor",
                },
            };
        }

        [Fact]
        public void AddNotificationNote_Success_ReturnsCorrectResult()
        {
            var mockNoteData = GetMockotificationNoteData();
            var mockNotes = GetMockNotes();
            var mockNotificationResolutionNotes = GetMockNotificationResolutionNotes();
            this.mockNoteRepository = new MockNoteRepository().MockAddNote(mockNotes);
            this.mockNotifiationResolutionNoteRepository = new MockNotificationResolutionNoteRepository().MockAddNotificationResolutionNote(mockNotificationResolutionNotes);
            InitialisePersonService();
            int userID = 1;
            var result = this.personService.AddNotificationNote(mockNoteData, userID);
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.InsertionSuccess);
        }

        [Fact]
        public void AddNotificationNote_Returns_Exception()
        {
            var mockNoteData = GetMockotificationNoteData();
            var mockNotes = GetMockNotes();
            var mockNotificationResolutionNotes = new NotificationResolutionNote();
            this.mockNoteRepository = new MockNoteRepository().MockAddNote(mockNotes);
            this.mockNotifiationResolutionNoteRepository = new MockNotificationResolutionNoteRepository().MockAddNotificationResolutionNoteException(mockNotificationResolutionNotes);
            InitialisePersonService();
            int userID = 1;
            Assert.ThrowsAny<Exception>(() => this.personService.AddNotificationNote(mockNoteData, userID));
        }

        private NotificationNoteDataDTO GetMockotificationNoteData()
        {
            return new NotificationNoteDataDTO()
            {
                NotificationLogID = 1,
                NoteText = "egestas",
                IsConfidential = true
            };
        }

        private Note GetMockNotes()
        {
            return new Note()
            {
                NoteID = 1,
                NoteText = "egestas",
                IsConfidential = true,
                IsRemoved = false,
                UpdateDate = DateTime.UtcNow,
                UpdateUserID = 1
            };
        }

        private NotificationResolutionNote GetMockNotificationResolutionNotes()
        {
            return new NotificationResolutionNote()
            {
                NotificationResolutionNoteID = 1,
                NotificationLogID = 1,
                Note_NoteID = 1,
                NotificationResolutionHistoryID = 0
            };
        }

        private PeopleDTO GetMockPerson()
        {

            PeopleDTO peopleDetailsDTO = new PeopleDTO()
            {

                PersonID = 1,

                PersonIndex = new Guid("F1001DCE-3078-420B-96C6-5AD2DB381EA7"),
                FirstName = "Saliha",
                LastName = "nazar",
                PreferredLanguageID = 1,
                DateOfBirth = DateTime.UtcNow,
                GenderID = 1,
                SexualityID = 1,
                BiologicalSexID = 1,
                Email = "salu@gmail.com",
                Phone1 = "9037337619",
                IsActive = true,
                PersonScreeningStatusID = 1,
                StartDate = DateTime.UtcNow,
                EndDate = null,
                IsRemoved = false,
                UpdateDate = DateTime.UtcNow,
                UpdateUserID = 1,
                AgencyID = 1,
            };
            return peopleDetailsDTO;
        }

        /// <summary>
        /// The GetMockPerson.
        /// </summary>
        /// <returns>The <see cref="PeopleDetailsDTO"/>.</returns>
        private PeopleDataDTO GetMockPeopleData()
        {

            PeopleDataDTO peopleDetailsDTO = new PeopleDataDTO()
            {

                PersonID = 1,
                PersonIndex = new Guid("2CA5AA77-BE13-4AF4-90DB-5D8B32C55A05"),
                FirstName = "Saliha",
                LastName = "nazar",
                DateOfBirth = DateTime.UtcNow,
                SexualityID = 1,
                Email = "salu@gmail.com",
                BioGenderID = 1,
                CountryStateID = 1,
                Phone1 = "9037337619",
                PersonScreeningStatusID = 1,
                AddressID = 1,
                PersonAddressID = 1,
                peopleIdentifier = new List<PeopleIdentifierDTO>()
                {
                    new PeopleIdentifierDTO() {
                        IdentifierID = "ff",
                        PersonIdentificationID = 1,
                        IdentifierType ="dfg",
                        IdentificationTypeID = 1,
                    }
                },

                peopleRaceEthnicity = new List<PeopleRaceEthnicityDTO>()
                {
                    new PeopleRaceEthnicityDTO() {
                        PersonRaceEthnicityID = 1,
                        RaceEthnicityID = 1,
                    }
                },
                peopleHelper = new List<PeopleHelperDTO>()
                {
                    new PeopleHelperDTO() {
                        HelperID = 1,
                        PersonHelperID = 1,
                        IsCurrent = true,
                        IsLead = true,
                    }
                },
                peopleSupport = new List<PeopleSupportDTO>()
                {
                    new PeopleSupportDTO() {
                        PersonID = 1,
                        PersonSupportID = 1,
                        SupportEmail = "salu@gmail.com",
                        IsCurrent = true,
                        StartDate = DateTime.UtcNow,
                        EndDate = null,
                    }
                },
                peopleCollaboration = new List<PeopleCollaborationDTO>()
                {
                    new PeopleCollaborationDTO() {
                        CollaborationID = 3,
                        PersonCollaborationID = 1,
                        IsCurrent = true,
                        IsPrimary = true,
                    }
                }
            };
            return peopleDetailsDTO;
        }


        /// <summary>
        /// The GetMockPersonDetails.
        /// </summary>
        /// <returns>The <see cref="PeopleDetailsDTO"/>.</returns>
        private PeopleDetailsDTO GetMockPersonDetails()
        {

            PeopleDetailsDTO peopleDetailsDTO = new PeopleDetailsDTO()
            {

                PersonID = 1,
                PersonIndex = new Guid("2CA5AA77-BE13-4AF4-90DB-5D8B32C55A05"),
                FirstName = "Saliha",
                LastName = "nazar",
                PreferredLanguageID = 1,
                IsPreferred = true,
                DateOfBirth = DateTime.UtcNow,
                GenderID = 1,
                SexualityID = 1,
                BiologicalSexID = 1,
                Email = "salu@gmail.com",
                Phone1 = "9037337619",
                IsActive = true,
                LanguageID = 1,
                PersonScreeningStatusID = 1,
                StartDate = DateTime.UtcNow,
                EndDate = null,
                IsRemoved = false,
                UpdateDate = DateTime.UtcNow,
                UpdateUserID = 1,
                AddressID = 1,
                AgencyID = 1,
                PersonAddressID = 1,
                IsPrimary = true,
                CountryStateId = 1,
                Address1 = "",
                Address2 = "",
                City = "",
                PrimaryLanguageID = 0,
                Zip = "",

                PersonIdentifications = new List<PersonIdentificationDTO>()
                {
                    new PersonIdentificationDTO() {
                        PersonID = 1,
                        PersonIdentificationID = 1,
                        UpdateDate = DateTime.UtcNow,
                        UpdateUserID = 1,
                        IsRemoved = false,
                    }
                },

                PersonRaceEthnicities = new List<PersonRaceEthnicityDTO>()
                {
                    new PersonRaceEthnicityDTO() {
                        PersonID = 1,
                        PersonRaceEthnicityID = 1,
                        RaceEthnicityID = 1,
                        IsRemoved = false,
                    }
                },
                PersonHelpers = new List<PersonHelperDTO>()
                {
                    new PersonHelperDTO() {
                        PersonID = 1,
                        HelperID = 1,
                        PersonHelperID = 1,
                        IsRemoved = false,
                        UpdateDate = DateTime.UtcNow,
                        UpdateUserID = 1,
                        IsCurrent = true,
                        IsLead = true,
                        StartDate = DateTime.UtcNow,
                        EndDate = null,
                    }
                },
                PersonSupports = new List<PersonSupportDTO>()
                {
                    new PersonSupportDTO() {
                        PersonID = 1,
                        PersonSupportID = 1,
                        SupportTypeID = 1,
                        IsRemoved = false,
                        UpdateDate = DateTime.UtcNow,
                        UpdateUserID = 1,
                        IsCurrent = true,
                        FirstName = "Saliha",
                        StartDate = DateTime.UtcNow,
                        EndDate = null,
                        Email = "salu@gmail.com",
                        Phone = "9037337619",
                    }
                },
                PersonCollaborations = new List<PersonCollaborationDTO>()
                {
                    new PersonCollaborationDTO() {
                        PersonID = 1,
                        CollaborationID = 3,
                        PersonCollaborationID = 1,
                        IsRemoved = false,
                        UpdateDate = DateTime.UtcNow,
                        UpdateUserID = 1,
                        IsCurrent = true,
                        IsPrimary = true,
                        EnrollDate = DateTime.UtcNow,
                        EndDate = null,

                    }
                }
            };
            return peopleDetailsDTO;
        }
        /// <summary>
        /// The GetMockPersonEditIdentificationDTO.
        /// </summary>
        /// <returns>The <see cref="PersonIdentificationDTO"/>.</returns>
        private IReadOnlyList<PersonIdentificationDTO> GetMockPersonEditIdentificationDTO()
        {

            IReadOnlyList<PersonIdentificationDTO> PersonEditIdentificationDTO = new List<PersonIdentificationDTO>()
                {
                    new PersonIdentificationDTO()
        {
                    PersonID = 1,
                        PersonIdentificationID = 1,
                        UpdateDate = DateTime.UtcNow,
                        UpdateUserID = 1,
                        IsRemoved = false,
                    }
    };
            return PersonEditIdentificationDTO;
        }

        /// <summary>
        /// The GetMockPersonEditIdentification.
        /// </summary>
        /// <returns>The <see cref="PersonIdentificationDTO"/>.</returns>
        private PersonIdentificationDTO GetMockPersonEditIdentification()
        {

            PersonIdentificationDTO PersonEditIdentificationDTO = new PersonIdentificationDTO()

            {
                PersonID = 1,
                PersonIdentificationID = 1,
                UpdateDate = DateTime.UtcNow,
                UpdateUserID = 1,
                IsRemoved = false,
            };
            return PersonEditIdentificationDTO;
        }
        /// <summary>
        /// The GetMockPersonEditIdentificationDTO.
        /// </summary>
        /// <returns>The <see cref="PersonRaceEthnicityDTO"/>.</returns>
        private IReadOnlyList<PersonRaceEthnicityDTO> GetMockPersonEditRaceEthnicityList()
        {

            IReadOnlyList<PersonRaceEthnicityDTO> PersonEditRaceEthnicityDTO = new List<PersonRaceEthnicityDTO>()
            {

                    new PersonRaceEthnicityDTO() {
                        PersonID = 1,
                        PersonRaceEthnicityID = 1,
                        RaceEthnicityID = 1
                    }

            };
            return PersonEditRaceEthnicityDTO;
        }
        /// <summary>
        /// The GetMockPersonEditRaceEthnicity.
        /// </summary>
        /// <returns>The <see cref="PersonRaceEthnicityDTO"/>.</returns>
        private PersonRaceEthnicityDTO GetMockPersonEditRaceEthnicity()
        {

            PersonRaceEthnicityDTO PersonEditRaceEthnicityDTO = new PersonRaceEthnicityDTO()
            {

                PersonID = 1,
                PersonRaceEthnicityID = 1,
                RaceEthnicityID = 1
            };


            return PersonEditRaceEthnicityDTO;
        }

        /// <summary>
        /// The GetMockPersonhelperList.
        /// </summaryPersonHelperDTO
        /// <returns>The <see cref="PersonHelperDTO"/>.</returns>
        private IReadOnlyList<PersonHelperDTO> GetMockPersonhelperList()
        {

            IReadOnlyList<PersonHelperDTO> PersonHelperDTO = new List<PersonHelperDTO>()
                {
                    new PersonHelperDTO()
        {
                     PersonID = 1,
                        HelperID = 1,
                        PersonHelperID = 1,
                        IsRemoved = false,
                        UpdateDate = DateTime.UtcNow,
                        UpdateUserID = 1,
                        IsCurrent = true,
                        IsLead = true,
                        StartDate = DateTime.UtcNow,
                        EndDate = null,
                    }
    };
            return PersonHelperDTO;
        }

        /// <summary>
        /// The GetMockPersonhelper.
        /// </summary>
        /// <returns>The <see cref="PersonHelperDTO"/>.</returns>
        private PersonHelperDTO GetMockPersonhelper()
        {

            PersonHelperDTO PersonEditIdentificationDTO = new PersonHelperDTO()

            {
                PersonID = 1,
                HelperID = 1,
                PersonHelperID = 1,
                IsRemoved = false,
                UpdateDate = DateTime.UtcNow,
                UpdateUserID = 1,
                IsCurrent = true,
                IsLead = true,
                StartDate = DateTime.UtcNow,
                EndDate = null,
            };
            return PersonEditIdentificationDTO;
        }

        /// <summary>
        /// The GetMockPersonQuestionaire.
        /// </summary>
        /// <returns>The <see cref="PersonHelperDTO"/>.</returns>
        private PersonQuestionnaireDetailsDTO GetMockPersonQuestionaire()
        {

            PersonQuestionnaireDetailsDTO personQuestionnaireDetailsDTO = new PersonQuestionnaireDetailsDTO()

            {
                PersonIndex = new Guid("2CA5AA77-BE13-4AF4-90DB-5D8B32C55A05"),
                QuestionnaireID = 1,

            };
            return personQuestionnaireDetailsDTO;
        }

        /// <summary>
        /// The GetPersonSupportList.
        /// </summary
        /// <returns>The <see cref="PersonSupportDTO"/>.</returns>
        private IReadOnlyList<PersonSupportDTO> GetPersonSupportList()
        {

            IReadOnlyList<PersonSupportDTO> PersonSupportDTO = new List<PersonSupportDTO>()
                {
                    new PersonSupportDTO()
        {
                     PersonID = 1,
                        PersonSupportID = 1,
                        SupportTypeID = 1,
                        IsRemoved = false,
                        UpdateDate = DateTime.UtcNow,
                        UpdateUserID = 1,
                        IsCurrent = true,
                        FirstName = "Saliha",
                        StartDate = DateTime.UtcNow,
                        EndDate = null,
                        Email = "salu@gmail.com",
                        Phone = "9037337619",
                    }
    };
            return PersonSupportDTO;
        }

        /// <summary>
        /// The GetMockPersonSupport.
        /// </summary>
        /// <returns>The <see cref="PersonSupportDTO"/>.</returns>
        private PersonSupportDTO GetMockPersonSupport()
        {

            PersonSupportDTO PersonSupportDTO = new PersonSupportDTO()

            {
                PersonID = 1,
                PersonSupportID = 1,
                SupportTypeID = 1,
                IsRemoved = false,
                UpdateDate = DateTime.UtcNow,
                UpdateUserID = 1,
                IsCurrent = true,
                FirstName = "Saliha",
                StartDate = DateTime.UtcNow,
                EndDate = null,
                Email = "salu@gmail.com",
                Phone = "9037337619",
            };
            return PersonSupportDTO;
        }

        /// <summary>
        /// The PersonSupportDTO.
        /// </summary
        /// <returns>The <see cref="PersonSupportDTO"/>.</returns>
        private IReadOnlyList<PersonCollaborationDTO> GetMockPersonCollaborationList()
        {

            IReadOnlyList<PersonCollaborationDTO> PersonCollaborationDTO = new List<PersonCollaborationDTO>()
                {
                    new PersonCollaborationDTO()
        {  PersonID = 1,
                        CollaborationID = 2,
                        PersonCollaborationID = 1,
                        IsRemoved = false,
                        UpdateDate = DateTime.UtcNow,
                        UpdateUserID = 1,
                        IsCurrent = true,
                        IsPrimary = true,
                        EnrollDate = DateTime.UtcNow,
                        EndDate = null,
                    }
    };
            return PersonCollaborationDTO;
        }

        /// <summary>
        /// The GetMockPersonSupport.
        /// </summary>
        /// <returns>The <see cref="PersonSupportDTO"/>.</returns>
        private PersonCollaborationDTO GetMockPersonCollaboration()
        {

            PersonCollaborationDTO PersonCollaborationDTO = new PersonCollaborationDTO()

            {
                PersonID = 1,
                CollaborationID = 2,
                PersonCollaborationID = 1,
                IsRemoved = false,
                UpdateDate = DateTime.UtcNow,
                UpdateUserID = 1,
                IsCurrent = true,
                IsPrimary = true,
                EnrollDate = DateTime.UtcNow,
                EndDate = null,
            };
            return PersonCollaborationDTO;
        }

        /// <summary>
        /// The GetMockPersonSupport.
        /// </summary>
        /// <returns>The <see cref="PersonSupportDTO"/>.</returns>
        private AddressDTO GetMockAddressDTO()
        {

            AddressDTO AddressDTO = new AddressDTO()

            {
                AddressID = 1,
                Address1 = "asddf",
                IsRemoved = false,
                UpdateDate = DateTime.UtcNow,
                UpdateUserID = 1,
                IsPrimary = true,
                CountryStateID = 1,
                Zip = "65677"
            };
            return AddressDTO;
        }
        /// <summary>
        /// The GetMockPersonSupport.
        /// </summary>
        /// <returns>The <see cref="PersonSupportDTO"/>.</returns>
        private PersonAddressDTO GetMockPersonAddressDTO()
        {

            PersonAddressDTO PersonAddressDTO = new PersonAddressDTO()

            {
                PersonID = 1,
                AddressID = 1,
                PersonAddressID = 1,
                IsPrimary = true
            };
            return PersonAddressDTO;
        }

        /// <summary>
        /// The GetMockPersonDetails.
        /// </summary>
        /// <returns>The <see cref="PeopleDetailsDTO"/>.</returns>
        private PeopleEditDetailsDTO GetMockPersonEditDetails()
        {

            PeopleEditDetailsDTO peopleDetailsDTO = new PeopleEditDetailsDTO()
            {

                PersonID = 1,
                PersonIndex = new Guid("2CA5AA77-BE13-4AF4-90DB-5D8B32C55A05"),
                FirstName = "Saliha",
                LastName = "nazar",
                PreferredLanguageID = 1,
                IsPreferred = true,
                DateOfBirth = DateTime.UtcNow,
                GenderID = 1,
                SexualityID = 1,
                BiologicalSexID = 1,
                Email = "salu@gmail.com",
                Phone1 = "9037337619",
                IsActive = true,
                LanguageID = 1,
                PersonScreeningStatusID = 1,
                StartDate = DateTime.UtcNow,
                EndDate = null,
                IsRemoved = false,
                UpdateDate = DateTime.UtcNow,
                UpdateUserID = 1,
                AddressID = 1,
                AgencyID = 1,
                PersonAddressID = 1,
                IsPrimary = true,
                CountryStateId = 1,

                PersonEditIdentificationDTO = new List<PersonEditIdentificationDTO>()
                {
                    new PersonEditIdentificationDTO() {
                        PersonID = 1,
                        PersonIdentificationID = 1,
                        UpdateDate = DateTime.UtcNow,
                        UpdateUserID = 1,
                        IsRemoved = false,
                    }
                },

                PersonEditRaceEthnicityDTO = new List<PersonEditRaceEthnicityDTO>()
                {
                    new PersonEditRaceEthnicityDTO() {
                        PersonID = 1,
                        PersonRaceEthnicityID = 1,
                        RaceEthnicityID = 1
                    }
                },
                PersonEditHelperDTO = new List<PersonEditHelperDTO>()
                {
                    new PersonEditHelperDTO() {
                        PersonID = 1,
                        HelperID = 1,
                        PersonHelperID = 1,
                        IsRemoved = false,
                        UpdateDate = DateTime.UtcNow,
                        UpdateUserID = 1,
                        IsCurrent = true,
                        IsLead = true,
                        StartDate = DateTime.UtcNow,
                        EndDate = null,
                    }
                },
                PersonEditSupportDTO = new List<PersonEditSupportDTO>()
                {
                    new PersonEditSupportDTO() {
                        PersonID = 1,
                        PersonSupportID = 1,
                        SupportTypeID = 1,
                        IsRemoved = false,
                        UpdateDate = DateTime.UtcNow,
                        UpdateUserID = 1,
                        IsCurrent = true,
                        FirstName = "Saliha",
                        StartDate = DateTime.UtcNow,
                        EndDate = null,
                        Email = "salu@gmail.com",
                        Phone = "9037337619",
                    }
                },
                PersonEditCollaborationDTO = new List<PersonEditCollaborationDTO>()
                {
                    new PersonEditCollaborationDTO() {
                        PersonID = 1,
                        CollaborationID = 2,
                        PersonCollaborationID = 1,
                        IsRemoved = false,
                        UpdateDate = DateTime.UtcNow,
                        UpdateUserID = 1,
                        IsCurrent = true,
                        IsPrimary = true,
                        EnrollDate = DateTime.UtcNow,
                        EndDate = null,

                    }
                }
            };
            return peopleDetailsDTO;
        }

        [Fact]
        public void GetPersonHelpingCount_Success_ReturnsCorrectResult()
        {
            List<PersonDTO> response = GetMockPersonHelpingCount();
            this.mockPersonRepository = new MockPersonRepository().MockGetPersonHelpingCount(response);
            InitialisePersonService();
            int helperID = 1;
            long agencyID = 1;
            List<string> roles = new List<string>();
            roles.Add("Helper RW");
            var result = this.personService.GetPersonHelpingCount(helperID, agencyID, roles, false);
            Assert.Equal(result.ResponseStatus, PCISEnum.StatusMessages.Success);
        }

        [Fact]
        public void GetPersonHelpingCount_Success_ReturnsNoResult()
        {
            List<PersonDTO> response = new List<PersonDTO>();
            this.mockPersonRepository = new MockPersonRepository().MockGetPersonHelpingCount(response);
            InitialisePersonService();
            int helperID = 1;
            long agencyID = 1;
            List<string> roles = new List<string>();
            roles.Add("Helper RW");
            var result = this.personService.GetPersonHelpingCount(helperID, agencyID, roles, false);
            Assert.Equal(0, result.PersonHelpingCount);
        }

        [Fact]
        public void GetPersonHelpingCount_Returns_Exception()
        {
            List<PersonDTO> response = GetMockPersonHelpingCount();
            this.mockPersonRepository = new MockPersonRepository().MockGetPersonHelpingCountException();
            InitialisePersonService();
            int helperID = 1;
            long agencyID = 1;
            List<string> roles = new List<string>();
            roles.Add("Helper RW");
            Assert.ThrowsAny<Exception>(() => this.personService.GetPersonHelpingCount(helperID, agencyID, roles, false));
        }



        [Fact]
        public void GetAllQuestionnairesWithCompletedAssessment_Success_ReturnsCorrectResult()
        {
            var mockPersonData = GetMockPerson();
            var mockquestionnaireData = GetMockAssessedQuestionnaires();
            this.mockPersonRepository = new MockPersonRepository().MockPersonData(mockPersonData);
            this.mockQuestionnaireRepository = new MockQuestionnaireRepository().MockAssessedQuestionnaireData(mockquestionnaireData);
            InitialisePersonService();
            Guid personIndex = Guid.NewGuid();
            UserTokenDetails userTokenDetails = new UserTokenDetails();
            userTokenDetails.AgencyID = 1;
            int pageNumber = 1;
            int pageSize = 10;
            int collaborationID = 1;
            int voiceTypeID = 1;
            var result = this.personService.GetAllQuestionnaireWithCompletedAssessment(userTokenDetails, personIndex, collaborationID, voiceTypeID, pageNumber, pageSize, 1);
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.Success);
        }

        [Fact]
        public void GetAllQuestionnairesWithCompletedAssessment_ReturnsNoResult()
        {
            var mockPersonData = GetMockPerson();
            var mockquestionnaireData = new List<AssessmentQuestionnaireDataDTO>();
            this.mockPersonRepository = new MockPersonRepository().MockPersonData(mockPersonData);
            this.mockQuestionnaireRepository = new MockQuestionnaireRepository().MockAssessedQuestionnaireData(mockquestionnaireData);
            InitialisePersonService();
            Guid personIndex = Guid.NewGuid();
            UserTokenDetails userTokenDetails = new UserTokenDetails();
            userTokenDetails.AgencyID = 1;
            int pageNumber = 1;
            int pageSize = 10;
            var result = this.personService.GetAllQuestionnaireWithCompletedAssessment(userTokenDetails, personIndex, pageNumber, pageSize, 1, 1, 1);
            Assert.Null(result.QuestionnaireData);
        }

        [Fact]
        public void GetAllQuestionnairesWithCompletedAssessment_Failure_InvalidParameterResult()
        {
            var mockPersonData = GetMockPerson();
            var mockquestionnaireData = new List<AssessmentQuestionnaireDataDTO>();
            this.mockPersonRepository = new MockPersonRepository().MockPersonData(mockPersonData);
            this.mockQuestionnaireRepository = new MockQuestionnaireRepository().MockAssessedQuestionnaireData(mockquestionnaireData);
            InitialisePersonService();
            Guid personIndex = Guid.NewGuid();
            UserTokenDetails userTokenDetails = new UserTokenDetails();
            userTokenDetails.AgencyID = 1;
            int pageNumber = 0;
            int pageSize = 10;
            var result = this.personService.GetAllQuestionnaireWithCompletedAssessment(userTokenDetails, personIndex, pageNumber, pageSize, 1, 1, 1);
            Assert.Null(result.QuestionnaireData);
        }

        [Fact]
        public void GetAllQuestionnairesWithCompletedAssessment_ExceptionResult()
        {
            var mockPersonData = GetMockPerson();
            var mockquestionnaireData = new List<AssessmentQuestionnaireDataDTO>();
            this.mockPersonRepository = new MockPersonRepository().MockPersonData(mockPersonData);
            this.mockQuestionnaireRepository = new MockQuestionnaireRepository().MockAssessedQuestionnaireDataException(mockquestionnaireData);
            InitialisePersonService();
            Guid personIndex = Guid.NewGuid();
            UserTokenDetails userTokenDetails = new UserTokenDetails();
            userTokenDetails.AgencyID = 1;
            int pageNumber = 1;
            int pageSize = 10;
            Assert.ThrowsAny<Exception>(() => this.personService.GetAllQuestionnaireWithCompletedAssessment(userTokenDetails, personIndex, pageNumber, pageSize, 1, 1, 1));
        }

        private List<AssessmentQuestionnaireDataDTO> GetMockAssessedQuestionnaires()
        {
            return new List<AssessmentQuestionnaireDataDTO>()
            {
                new AssessmentQuestionnaireDataDTO()
                {
                    QuestionnaireID = 1,
                    InstrumentID = 1,
                    AgencyID = 1,
                    Name = "Questionnaire 1",
                    QuestionnaireName = "1-ANSA-Q1",
                    QuestionnaireAbbrev = "Q1",
                    ReminderScheduleName = "Reminder Name",
                    NotificationScheduleName = "Self Harm Only",
                    InstrumentName = "Adult Needs and Strengths Assessment",
                    InstrumentAbbrev = "ANSA",
                    IsBaseQuestionnaire = false,
                    PersonID = 1,
                    StartDate = DateTime.UtcNow,
                    EndDate = null,
                    PersonQuestionnaireID = 1,
                    AssessmentID = 1,
                    AssessmentStatusID = 2,
                    Status = "Submitted"
                },
                new AssessmentQuestionnaireDataDTO()
                {
                    QuestionnaireID = 2,
                    InstrumentID = 1,
                    AgencyID = 1,
                    Name = "1Questionnaire 2",
                    QuestionnaireName = "2-ANSA-Q2",
                    QuestionnaireAbbrev = "Q2",
                    ReminderScheduleName = "Reminder Name",
                    NotificationScheduleName = "Self Harm Only",
                    InstrumentName = "Adult Needs and Strengths Assessment",
                    InstrumentAbbrev = "ANSA",
                    IsBaseQuestionnaire = false,
                    PersonID = 1,
                    StartDate = DateTime.UtcNow,
                    EndDate = null,
                    PersonQuestionnaireID = 1,
                    AssessmentID = 1,
                    AssessmentStatusID = 3,
                    Status = "Complete"
                },
            };
        }

        #region GetPersonInitials

        private PersonInitialsDTO MockPersonInitials()
        {
            return new PersonInitialsDTO()
            {
                PersonIndex = new Guid("87BB9401-6142-474C-B473-D96E2B5CAE8F"),
                PersonInitials = "LD"
            };
        }

        [Fact]
        public void GetPersonInitials_Success_ReturnsCorrectResult()
        {
            var mockPersonInitials = MockPersonInitials();
            this.mockPersonRepository = new MockPersonRepository().MockGetPersonInitials(mockPersonInitials);

            InitialisePersonService();

            Guid personIndex = new Guid("87BB9401-6142-474C-B473-D96E2B5CAE8F");
            var result = this.personService.GetPersonInitials(personIndex);
            Assert.Equal(result.ResponseStatus, PCISEnum.StatusMessages.Success);
        }

        [Fact]
        public void GetPersonInitials_Returns_Exception()
        {
            var mockPersonInitials = MockPersonInitials();
            this.mockPersonRepository = new MockPersonRepository().MockGetPersonInitialsException(mockPersonInitials);

            InitialisePersonService();

            Guid personIndex = new Guid("87BB9401-6142-474C-B473-D96E2B5CAE8F");
            Assert.ThrowsAny<Exception>(() => this.personService.GetPersonInitials(personIndex));
        }

        #endregion GetPersonInitials

        #region GetPeopleCollaborationList

        private List<PeopleCollaborationDTO> MockPeopleCollaborationList()
        {
            return new List<PeopleCollaborationDTO>()
            {
                new PeopleCollaborationDTO()
                {
                    CollaborationID=1,
                    CollaborationName="Test",
                    PersonCollaborationID=1,
                    CollaborationStartDate=DateTime.Now,
                    CollaborationEndDate=null,
                    IsPrimary=true,
                    IsCurrent=true
                }
            };
        }

        [Fact]
        public void GetPeopleCollaborationList_Success_ReturnsCorrectResult()
        {
            var mockPeopleCollaborationList = MockPeopleCollaborationList();
            var mockPersonData = GetMockPerson();
            this.mockPersonRepository = new MockPersonRepository().MockGetPeopleCollaborationList(mockPeopleCollaborationList).MockPersonData(mockPersonData);

            InitialisePersonService();

            Guid personIndex = new Guid("87BB9401-6142-474C-B473-D96E2B5CAE8F");
            UserTokenDetails userTokenDetails = new UserTokenDetails();
            userTokenDetails.AgencyID = 1;
            var result = this.personService.GetPeopleCollaborationList(personIndex, userTokenDetails, 1);
            Assert.Equal(result.ResponseStatus, PCISEnum.StatusMessages.Success);
        }

        [Fact]
        public void GetPeopleCollaborationList_Returns_Exception()
        {
            var mockPeopleCollaborationList = MockPeopleCollaborationList();
            var mockPersonData = GetMockPerson();
            this.mockPersonRepository = new MockPersonRepository().MockGetPeopleCollaborationListException(mockPeopleCollaborationList).MockPersonData(mockPersonData);

            InitialisePersonService();

            Guid personIndex = new Guid("87BB9401-6142-474C-B473-D96E2B5CAE8F");
            UserTokenDetails userTokenDetails = new UserTokenDetails();
            userTokenDetails.AgencyID = 1;
            Assert.ThrowsAny<Exception>(() => this.personService.GetPeopleCollaborationList(personIndex, userTokenDetails,1));
        }

        #endregion GetPeopleCollaborationList

        private List<PersonDTO> GetMockPersonHelpingCount()
        {
            return new List<PersonDTO>()
            {
                new PersonDTO()
                {
                    PersonID = 10000117209,
                    PersonIndex=Guid.NewGuid(),
                    Name= "aa i sa",
                    Collaboration= null,
                    Lead= "test2",
                    StartDate= DateTime.UtcNow,
                    EndDate= null,
                    CollaborationID= 0,
                    Days= 0,
                    Assessed= 2,
                    NeedsEver= 0,
                    NeedsAddressing=0,
                    StrengthEver= 0,
                    StrengthBuilding= 0,
                    TotalCount= 63,
                    PersonStartDate= DateTime.UtcNow,
                    PersonEndDate= DateTime.UtcNow
                },
                new PersonDTO()
                {
                    PersonID = 10000117208,
                    Name= "aab",
                    Collaboration= null,
                    Lead= "test1",
                    StartDate= DateTime.UtcNow,
                    EndDate= null,
                    CollaborationID= 0,
                    Days= 0,
                    Assessed= 2,
                    NeedsEver= 0,
                    NeedsAddressing=0,
                    StrengthEver= 0,
                    StrengthBuilding= 0,
                    TotalCount= 63,
                    PersonStartDate= DateTime.UtcNow,
                    PersonEndDate= DateTime.UtcNow
                }
            };
        }
    }
}
