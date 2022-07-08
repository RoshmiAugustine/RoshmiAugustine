using Microsoft.AspNetCore.Http;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Domain.Services;
using Opeeka.PICS.Infrastructure.Enums;
using Opeeka.PICS.UnitTests.Domain.Mocks.Repositories;
using System;
using System.Collections.Generic;
using Xunit;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.DTO.Input;
using Microsoft.Extensions.Configuration;
using Opeeka.PICS.Domain.Interfaces.Common;
using System.Net;
using Opeeka.PICS.UnitTests.Domain.Mocks.Common;
using Opeeka.PICS.Domain.Interfaces;
using Opeeka.PICS.Domain.Interfaces.Services;

namespace Opeeka.PICS.UnitTests.Domain.Services
{
    public class AssessmentServiceUnitTest
    {
        private Mock<IQuestionnaireRepository> mockQuestionnaireRepository;
        private Mock<IAssessmentRepository> mockAssessmentRepository;
        private Mock<IAssessmentResponseRepository> mockAssessmentResponseRepository;
        private Mock<IMapper> mockMapper;
        private Mock<INoteRepository> mockNoteRepository;
        private Mock<IAssessmentResponseNoteRepository> mockAssessmentResponseNoteRepository;
        private Mock<IAssessmentStatusRepository> mockAssessmentStatusRepository;

        private Mock<IAssessmentEmailLinkRepository> mockAssessmentEmailLinkRepository;
        private Mock<IPersonSupportRepository> mockPersonSupportRepository;
        private Mock<IEmailSender> mockEmailSender;
        private Mock<IConfiguration> mockConfiguration;
        private Mock<IDataProtection> mockDataProtector;

        /// Initializes a new instance of the <see cref="logger"/> class.
        private Mock<ILogger<AssessmentService>> mockLogger;

        private AssessmentService assessmentService;
        private Mock<IConfigurationRepository> mockConfigRepository;
        private Mock<IHttpContextAccessor> httpContextAccessor;
        private Mock<IResponseRepository> mockResponseRepository;
        private Mock<IQuestionnaireNotifyRiskRuleConditionRepository> mockQuestionnaireNotifyRiskRuleConditionRepository;
        private Mock<IPersonRepository> mockPersonRepository;
        private Mock<INotificationTypeRepository> mockNotificationTypeRepository;
        private Mock<INotificationLogRepository> mockNotificationLogRepository;
        private Mock<INotifiationResolutionStatusRepository> mockNotifiationResolutionStatusRepository;
        private Mock<IAssessmentHistoryRepository> mockAssessmentHistoryRepository;
        private Mock<IAssessmentNoteRepository> mockAssessmentNoteRepository;
        private Mock<INotifyRiskRepository> mockNotifyRiskRepository;
        private Mock<IAssessmentReasonRepository> mockAssessmentReasonRepository;
        private Mock<IUtility> mockUtility;
        private Mock<IQueue> mockQueue;
        private Mock<IVoiceTypeRepository> mockVoiceTypeRepository;
        private Mock<IAssessmentEmailOtpRepository> mockAssessmentEmailOtpRepository;
        private Mock<IAgencyRepository> mockagencyRepository;
        private Mock<IPersonQuestionnaireRepository> mockPersonQuestionnaireRepository;

        private Mock<IQuestionnaireSkipLogicRuleConditionRepository> mockQuestionnaireSkipLogicRuleConditionRepository;
        private Mock<IQuestionnaireSkipLogicRuleActionRepository> mockQuestionnaireSkipLogicRuleActionRepository;
        private Mock<IQuestionnaireSkipLogicRuleRepository> mockQuestionnaireSkipLogicRuleRepository;

        private Mock<IQuestionnaireDefaultResponseRuleConditionRepository> mockQuestionnaireDefaultResponseRuleConditionRepository;
        private Mock<IQuestionnaireDefaultResponseRuleActionRepository> mockQuestionnaireDefaultResponseRuleActionRepository;
        private Mock<IQuestionnaireDefaultResponseRuleRepository> mockQuestionnaireDefaultResponseRuleRepository;
        private Mock<IHelperRepository> mockHelperRepository;
        private Mock<IQueryBuilder> mockqueryBuilder;
        private Mock<ISMSSender> smsSender;
        private Mock<IPersonQuestionnaireScheduleRepository> personQuestionnaireScheduleRepository;
        private Mock<IPersonQuestionnaireScheduleService> personQuestionnaireScheduleService;
        private Mock<ILookupRepository> mockLookupRepository;
        private Mock<IAssessmentResponseAttachmentRepository> mockAssessmentResponseFileRepository;
        /// <summary>
        /// Initializes a new instance of the <see cref="AssessmentService"/> class.
        /// </summary>
        public AssessmentServiceUnitTest()
        {
            this.mockAssessmentRepository = new Mock<IAssessmentRepository>();
            this.mockAssessmentResponseRepository = new Mock<IAssessmentResponseRepository>();
            this.mockQuestionnaireRepository = new Mock<IQuestionnaireRepository>();
            this.mockLogger = new Mock<ILogger<AssessmentService>>();
            this.mockConfigRepository = new Mock<IConfigurationRepository>();
            this.httpContextAccessor = new Mock<IHttpContextAccessor>();
            this.mockMapper = new Mock<IMapper>();
            this.mockNoteRepository = new Mock<INoteRepository>();
            this.mockAssessmentResponseNoteRepository = new Mock<IAssessmentResponseNoteRepository>();
            this.mockAssessmentStatusRepository = new Mock<IAssessmentStatusRepository>();
            this.mockAssessmentEmailLinkRepository = new Mock<IAssessmentEmailLinkRepository>();
            this.mockPersonSupportRepository = new Mock<IPersonSupportRepository>();
            this.mockEmailSender = new Mock<IEmailSender>();
            this.mockConfiguration = new Mock<IConfiguration>();
            this.mockDataProtector = new Mock<IDataProtection>();
            this.mockResponseRepository = new Mock<IResponseRepository>();
            this.mockQuestionnaireNotifyRiskRuleConditionRepository = new Mock<IQuestionnaireNotifyRiskRuleConditionRepository>();
            this.mockPersonRepository = new Mock<IPersonRepository>();
            this.mockNotificationTypeRepository = new Mock<INotificationTypeRepository>();
            this.mockNotificationLogRepository = new Mock<INotificationLogRepository>();
            this.mockNotifiationResolutionStatusRepository = new Mock<INotifiationResolutionStatusRepository>();
            this.mockNotifyRiskRepository = new Mock<INotifyRiskRepository>();

            this.mockAssessmentHistoryRepository = new Mock<IAssessmentHistoryRepository>();
            this.mockAssessmentNoteRepository = new Mock<IAssessmentNoteRepository>();
            this.mockAssessmentReasonRepository = new Mock<IAssessmentReasonRepository>();
            var context = new DefaultHttpContext();
            context.Request.Headers[PCISEnum.TokenHeaders.timeZone] = "-330";
            this.httpContextAccessor.Setup(_ => _.HttpContext).Returns(context);
            this.mockUtility = new MockUtility().SetupUtility(DateTime.UtcNow);
            this.mockQueue = new Mock<IQueue>();
            this.mockVoiceTypeRepository = new Mock<IVoiceTypeRepository>();
            this.mockAssessmentEmailOtpRepository = new Mock<IAssessmentEmailOtpRepository>();
            this.mockagencyRepository = new Mock<IAgencyRepository>();
            this.mockPersonQuestionnaireRepository = new Mock<IPersonQuestionnaireRepository>();
            this.mockQuestionnaireSkipLogicRuleConditionRepository = new Mock<IQuestionnaireSkipLogicRuleConditionRepository>();
            this.mockQuestionnaireSkipLogicRuleActionRepository = new Mock<IQuestionnaireSkipLogicRuleActionRepository>();
            this.mockQuestionnaireSkipLogicRuleRepository = new Mock<IQuestionnaireSkipLogicRuleRepository>();
            this.mockQuestionnaireDefaultResponseRuleConditionRepository = new Mock<IQuestionnaireDefaultResponseRuleConditionRepository>();
            this.mockQuestionnaireDefaultResponseRuleActionRepository = new Mock<IQuestionnaireDefaultResponseRuleActionRepository>();
            this.mockQuestionnaireDefaultResponseRuleRepository = new Mock<IQuestionnaireDefaultResponseRuleRepository>();
            this.mockHelperRepository = new Mock<IHelperRepository>();
            this.mockqueryBuilder = new Mock<IQueryBuilder>(); 
            this.smsSender = new Mock<ISMSSender>();
            this.personQuestionnaireScheduleRepository = new Mock<IPersonQuestionnaireScheduleRepository>();
            this.personQuestionnaireScheduleService = new Mock<IPersonQuestionnaireScheduleService>();
            this.mockLookupRepository = new Mock<ILookupRepository>();
            this.mockAssessmentResponseFileRepository = new Mock<IAssessmentResponseAttachmentRepository>();
        }


        [Fact]
        public void GetQuestions_Success_ReturnsCorrectResult()
        {

            var mockQuestions = GetMockQuestions();
            this.mockQuestionnaireRepository = new MockQuestionnaireRepository().MockGetQuestions(mockQuestions);
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Success);
            InitialiseAssessmentService();


            int id = 1;
            var result = this.assessmentService.GetQuestions(id);
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.Success);
            //Assert.NotNull
        }


        [Fact]
        public void GetQuestions_Success_ReturnsnullResult()
        {

            //var mockQuestions = null;
            this.mockQuestionnaireRepository = new MockQuestionnaireRepository().MockGetQuestions(null);
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Success);
            InitialiseAssessmentService();
            int id = 1;
            var result = this.assessmentService.GetQuestions(id);
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.Success);
            //Assert.NotNull
        }

        [Fact]
        public void GetQuestions_Success_Failure_ExceptionResult()
        {

            var mockQuestions = new QuestionsDTO();
            this.mockQuestionnaireRepository = new MockQuestionnaireRepository().MockGetQuestionsException(mockQuestions);
            InitialiseAssessmentService();
            int id = 1;
            Assert.ThrowsAny<Exception>(() => this.assessmentService.GetQuestions(id));
            // Assert.NotNull
        }

        [Fact]
        public void GetAssessmentDetails_Success_ReturnsCorrectResult()
        {

            var mockQuestions = GetMockAssessmentDetails();
            this.mockAssessmentRepository = new MockAssessmentRepository().MockGetAssessmentDetails(mockQuestions);
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Success);
            InitialiseAssessmentService();

            Guid PersonIndex = new Guid();
            int QuestionnaireID = 1;
            DateTime? date = DateTime.Now;
            UserTokenDetails userTokenDetails = new UserTokenDetails() { UserID = 0, AgencyID = 0 };
            int pageNumber = 0;
            long totalCount = 0;
            var result = this.assessmentService.GetAssessmentDetails(PersonIndex, QuestionnaireID, date, userTokenDetails,pageNumber, totalCount, 0);
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.Success);
            //Assert.NotNull
        }


        [Fact]
        public void GetAssessmentDetails_Success_ReturnsnullResult()
        {

            //var mockQuestions = null;
            this.mockAssessmentRepository = new MockAssessmentRepository().MockGetAssessmentDetails(null);
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Success);
            InitialiseAssessmentService();

            Guid PersonIndex = new Guid();
            int QuestionnaireID = 1;
            DateTime? date = DateTime.Now;
            UserTokenDetails userTokenDetails = new UserTokenDetails() { UserID = 0, AgencyID = 0 };
            int pageNumber = 0;
            long totalCount = 0;
            var result = this.assessmentService.GetAssessmentDetails(PersonIndex, QuestionnaireID, date, userTokenDetails,pageNumber, totalCount, 0);
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.Success);
            //Assert.NotNull
        }

        [Fact]
        public void GetAssessmentDetails_Success_Failure_ExceptionResult()
        {

            var mockQuestions = new List<AssessmentDetailsDTO>();
            this.mockAssessmentRepository = new MockAssessmentRepository().MockGetAssessmentDetailsException(mockQuestions);
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Success);
            InitialiseAssessmentService();

            Guid PersonIndex = new Guid();
            int QuestionnaireID = 1;
            DateTime? date = DateTime.Now;
            UserTokenDetails userTokenDetails = new UserTokenDetails() { UserID = 0, AgencyID = 0 };
            int pageNumber = 0;
            long totalCount = 0;
            Assert.ThrowsAny<Exception>(() => this.assessmentService.GetAssessmentDetails(PersonIndex, QuestionnaireID, date, userTokenDetails,pageNumber,totalCount, 0));
            // Assert.NotNull
        }


        [Fact]
        public void GetAssessmentValues_Success_ReturnsCorrectResult()
        {

            var mockAssessmentValues = GetMockAssessmentValues();
            this.mockAssessmentResponseRepository = new MockAssessmentResponseRepository().MockGetAssessmentValues(mockAssessmentValues);
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Success);
            InitialiseAssessmentService();

            Guid PersonIndex = new Guid();
            int QuestionnaireID = 1;
            UserTokenDetails userTokenDetails = new UserTokenDetails() { UserID = 0, AgencyID = 1 };
            string assessmentIDs = string.Empty;
            var result = this.assessmentService.GetAssessmentValues(PersonIndex, QuestionnaireID, userTokenDetails, assessmentIDs);
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.Success);
            //Assert.NotNull
        }


        [Fact]
        public void GetAssessmentValues_Success_ReturnsnullResult()
        {

            //var mockQuestions = null;
            this.mockAssessmentResponseRepository = new MockAssessmentResponseRepository().MockGetAssessmentValues(null);
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Success);
            InitialiseAssessmentService();


            Guid PersonIndex = new Guid();
            int QuestionnaireID = 1;
            UserTokenDetails userTokenDetails = new UserTokenDetails() { UserID = 0, AgencyID = 0 };
            string assessmentIDs = string.Empty;
            var result = this.assessmentService.GetAssessmentValues(PersonIndex, QuestionnaireID, userTokenDetails, assessmentIDs);
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.Success);
            //Assert.NotNull
        }

        [Fact]
        public void GetAssessmentValues_Success_Failure_ExceptionResult()
        {

            var mockAssessmentValues = new List<AssessmentValuesDTO>();
            this.mockAssessmentResponseRepository = new MockAssessmentResponseRepository().MockGetAssessmentValuesException(mockAssessmentValues);
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Success);
            InitialiseAssessmentService();

            Guid PersonIndex = new Guid();
            int QuestionnaireID = 1;
            UserTokenDetails userTokenDetails = new UserTokenDetails() { UserID = 0, AgencyID = 0 };
            string assessmentIDs = "1";
            Assert.ThrowsAny<Exception>(() => this.assessmentService.GetAssessmentValues(PersonIndex, QuestionnaireID, userTokenDetails, assessmentIDs));
            // Assert.NotNull
        }
        private QuestionsDTO GetMockQuestions()
        {
            return new QuestionsDTO()
            {
                QuestionnaireID = 1,
                QuestionnaireName = "QA1",
                Categories = @"string"

            };
        }

        private List<AssessmentValuesDTO> GetMockAssessmentValues()
        {
            return new List<AssessmentValuesDTO>()
            {
                new AssessmentValuesDTO()
                {
                    AssessmentID = 1,
                    AssessmentResponseID = 1,
                   BehaviorName="test",
                   IsRequiredConfidential=false,
                   ItemId=1,
                   ItemResponseBehaviorID=1,
                   KeyCodes="1",
                   Note="Note",
                   PersonID=1,
                   PersonSupportID=0,
                   ResponseId=1,
                   Value=1
                },
                new AssessmentValuesDTO()
                {
                    AssessmentID = 1,
                    AssessmentResponseID = 2,
                   BehaviorName="test",
                   IsRequiredConfidential=false,
                   ItemId=1,
                   ItemResponseBehaviorID=1,
                   KeyCodes="1",
                   Note="Note",
                   PersonID=1,
                   PersonSupportID=0,
                   ResponseId=1,
                   Value=1
                }
            };
        }

        private List<AssessmentDetailsDTO> GetMockAssessmentDetails()
        {
            return new List<AssessmentDetailsDTO>()
            {
                new AssessmentDetailsDTO()
                {
                    AssessmentID=1,
                    AssessmentReasonID=1,
                    AssessmentStatusID=1,
                    Date=DateTime.Now,
                    DaysInProgram=0,
                    PersonID=1,
                    PersonQuestionnaireID=1,
                    TimePeriod=""
                },
                new AssessmentDetailsDTO()
                {
                    AssessmentID=2,
                    AssessmentReasonID=1,
                    AssessmentStatusID=1,
                    Date=DateTime.Now,
                    DaysInProgram=0,
                    PersonID=1,
                    PersonQuestionnaireID=1,
                    TimePeriod=""
                }
            };
        }

        #region AddAssessmentProgress

        private AssessmentStatus MockAssessmentStatusInProgress()
        {
            return new AssessmentStatus()
            {
                AssessmentStatusID = 1,
                Name = "In Progress",
                Abbrev = null,
                Description = null,
                ListOrder = 1,
                IsRemoved = false,
                UpdateDate = DateTime.UtcNow,
                UpdateUserID = 2
            };
        }

        private AssessmentStatus MockAssessmentStatusSubmitted()
        {
            return new AssessmentStatus()
            {
                AssessmentStatusID = 2,
                Name = "Submitted",
                Abbrev = null,
                Description = null,
                ListOrder = 1,
                IsRemoved = false,
                UpdateDate = DateTime.UtcNow,
                UpdateUserID = 2
            };
        }

        private Assessment MockAssessment()
        {
            return new Assessment()
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
            };
        }

        private AssessmentResponse MockAssessmentResponse()
        {
            return new AssessmentResponse()
            {
                AssessmentResponseID = 1,
                AssessmentID = 1,
                PersonSupportID = null,
                ResponseID = 2,
                ItemResponseBehaviorID = 1,
                IsRequiredConfidential = false,
                IsPersonRequestedConfidential = null,
                IsOtherConfidential = false,
                IsRemoved = false,
                UpdateDate = DateTime.UtcNow,
                UpdateUserID = 2,
                QuestionnaireItemID = 1,
                IsCloned = false
            };
        }

        private Note MockNote()
        {
            return new Note()
            {
                NoteID = 1,
                NoteText = "nec tempus mauris erat eget ipsum. Suspendisse sagittis.",
                IsConfidential = false,
                IsRemoved = false,
                UpdateDate = DateTime.UtcNow,
                UpdateUserID = 1
            };
        }

        private AssessmentResponseNote MockAssessmentResponseNote()
        {
            return new AssessmentResponseNote()
            {
                AssessmentResponseNoteID = 1,
                AssessmentResponseID = 1,
                NoteID = 1
            };
        }

        private List<AssessmentProgressInputDTO> MockAssessmentProgressSaveInput()
        {
            return new List<AssessmentProgressInputDTO>()
            {
                new AssessmentProgressInputDTO()
                {
                    PersonIndex = new Guid(),
                    QuestionnaireID = 1,
                    VoiceTypeID = 1,
                    DateTaken = DateTime.UtcNow,
                    ReasoningText = "reasoningText1",
                    AssessmentReasonID = 2,
                    AssessmentStatus = "Save",
                    CloseDate = DateTime.UtcNow,
                    AssessmentResponses = MockAssessmentResponseInput()
                }
            };
        }

        private List<AssessmentResponseInputDTO> MockAssessmentResponseInput()
        {
            return new List<AssessmentResponseInputDTO>()
            {
                new AssessmentResponseInputDTO()
                {
                    PersonSupportID = 11,
                    ResponseID = 17,
                    ItemResponseBehaviorID = 4,
                    IsRequiredConfidential = true,
                    IsPersonRequestedConfidential = true,
                    IsOtherConfidential = true,
                    QuestionnaireItemID = 5,
                    IsCloned = true,
                    AssessmentResponseNotes = MockAssessmentResponseNoteInput()
                }
            };
        }

        private List<AssessmentResponseNoteInputDTO> MockAssessmentResponseNoteInput()
        {
            return new List<AssessmentResponseNoteInputDTO>()
            {
                new AssessmentResponseNoteInputDTO()
                {
                    NoteText = "Note1",
                    IsConfidential = true
                }
            };
        }


        [Fact]
        public void AddAssessmentProgress_OnSave_Success_ReturnsCorrectResult()
        {
            var mockAssessmentStatus = MockAssessmentStatusInProgress();
            var mockAssessment = MockAssessment();
            var mockAssessmentResponse = MockAssessmentResponse();
            var mockNote = MockNote();
            var mockAssessmentResponseNote = MockAssessmentResponseNote();
            var mockAssessmentProgressInput = MockAssessmentProgressSaveInput();
            var mockResponse = MockResponse();
            var mockQuestionnaireNotifyRiskRuleCondition = MockQuestionnaireNotifyRiskRuleCondition();
            var mockPerson = GetMockPerson();
            var mockAssessmentResponselist = MockAssessmentResponseList();
            var mockAssessmentResponselist1 = MockAssessmentResponseList1();
            var mockNotificationStatus = GetMockNotificationStatus();
            var mockNotificationType = GetMockNotificationType();
            var mockGetNeedforFocusValues = GetNeedforFocusValues();
            var mockNoteList = MockNoteList();

            this.mockAssessmentStatusRepository = new MockAssessmentStatusRepository().MockGetAllAssessmentStatus(MockAssessmentStatusList());
            var mockAssessmentReason = MockAssessmentReasonList();
            this.mockAssessmentReasonRepository = new MockAssessmentReasonRepository().MockGetAllAssessmentReason(mockAssessmentReason);
            this.mockAssessmentRepository = new MockAssessmentRepository().MockAddAssessmentProgress(mockAssessment);
            this.mockAssessmentResponseRepository = new MockAssessmentResponseRepository().MockAddAssessmentProgress(mockAssessmentResponse, mockGetNeedforFocusValues, mockAssessmentResponselist, mockAssessmentResponselist1, mockAssessmentResponselist1);
            this.mockNoteRepository = new MockNoteRepository().MockAddNote(mockNote, mockNoteList);
            this.mockAssessmentResponseNoteRepository = new MockAssessmentResponseNoteRepository().MockAddAssessmentProgress(mockAssessmentResponseNote);
            this.mockResponseRepository = new MockResponseRepository().MockGetResponse(mockResponse);
            this.mockQuestionnaireNotifyRiskRuleConditionRepository = new MockQuestionnaireNotifyRiskRuleConditionRepository().MockGetQuestionnaireNotifyRiskRuleConditionByQuestionnaireItemID(mockQuestionnaireNotifyRiskRuleCondition);
            this.mockPersonRepository = new MockPersonRepository().MockGetPerson(mockPerson);
            this.mockNotifiationResolutionStatusRepository = new MockNotificationResolutionStatusRepository().MockGetNotificationStatus(mockNotificationStatus);
            this.mockNotificationTypeRepository = new MockNotificationTypeRepository().MockGetNotificationType(mockNotificationType);

            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.SaveSuccess);
            InitialiseAssessmentService();
            this.assessmentService.GetTimeZoneFromHeader();
            int updateUserID = 2;
            var result = this.assessmentService.AddAssessmentProgress(mockAssessmentProgressInput, updateUserID, false, 1);
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.SaveSuccess);
        }

        private AssessmentReason GetMockAssessmentReason()
        {
            return new AssessmentReason()
            {
                Name = "Triggering Event"
            };
        }

        //[Fact]
        //public void AddAssessmentProgress_OnSave_Exception_OnInvalidResponse()
        //{
        //    var mockAssessmentStatus = MockAssessmentStatusInProgress();
        //    var mockAssessment = MockAssessment();
        //    var mockAssessmentResponse = MockAssessmentResponse();
        //    var mockNote = MockNote();
        //    var mockAssessmentResponseNote = MockAssessmentResponseNote();
        //    var mockAssessmentProgressInput = MockAssessmentProgressSaveInput();
        //    var mockResponse = MockResponse();
        //    var mockQuestionnaireNotifyRiskRuleCondition = MockQuestionnaireNotifyRiskRuleCondition();
        //    var mockPerson = GetMockPerson();
        //    var mockNotificationStatus = GetMockNotificationStatus();
        //    var mockNotificationType = GetMockNotificationType();

        //    this.mockAssessmentStatusRepository = new MockAssessmentStatusRepository().MockAddAssessmentProgress(mockAssessmentStatus);
        //    this.mockAssessmentRepository = new MockAssessmentRepository().MockAddAssessmentProgress(mockAssessment);
        //    this.mockAssessmentResponseRepository = new MockAssessmentResponseRepository().MockAddAssessmentProgress(mockAssessmentResponse);
        //    this.mockNoteRepository = new MockNoteRepository().MockAddNote(mockNote);
        //    this.mockAssessmentResponseNoteRepository = new MockAssessmentResponseNoteRepository().MockAddAssessmentProgress(mockAssessmentResponseNote);
        //    this.mockResponseRepository = new MockResponseRepository().MockGetResponseException(mockResponse);
        //    //this.mockQuestionnaireNotifyRiskRuleConditionRepository = new MockQuestionnaireNotifyRiskRuleConditionRepository().MockGetQuestionnaireNotifyRiskRuleConditionByQuestionnaireItemID(mockQuestionnaireNotifyRiskRuleCondition);
        //    //this.mockPersonRepository = new MockPersonRepository().MockGetPerson(mockPerson);
        //    //this.mockNotifiationResolutionStatusRepository = new MockNotificationResolutionStatusRepository().MockGetNotificationStatus(mockNotificationStatus);
        //    //this.mockNotificationTypeRepository = new MockNotificationTypeRepository().MockGetNotificationType(mockNotificationType);

        //    var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.SaveSuccess);
        //    this.assessmentService = new AssessmentService(this.mockAssessmentResponseRepository.Object, this.mockAssessmentRepository.Object, this.mockQuestionnaireRepository.Object, this.mockLogger.Object,
        //                                               localize.Object, mockConfigRepository.Object, httpContextAccessor.Object, this.mockMapper.Object, this.mockNoteRepository.Object, this.mockAssessmentResponseNoteRepository.Object,
        //                                               this.mockAssessmentStatusRepository.Object, mockAssessmentEmailLinkRepository.Object, mockPersonSupportRepository.Object, mockEmailSender.Object, mockConfiguration.Object, mockDataProtector.Object,
        //                                               this.mockResponseRepository.Object, mockQuestionnaireNotifyRiskRuleConditionRepository.Object, mockPersonRepository.Object, mockNotificationTypeRepository.Object, mockNotificationLogRepository.Object,
        //                                               this.mockNotifiationResolutionStatusRepository.Object, this.mockNotifyRiskRepository.Object);

        //    int updateUserID = 2;
        //    //var result = this.assessmentService.AddAssessmentProgress(mockAssessmentProgressInput, updateUserID);
        //    Assert.ThrowsAny<Exception>(() => this.assessmentService.AddAssessmentProgress(mockAssessmentProgressInput, updateUserID, false));
        //}


        [Fact]
        public void AddAssessmentProgress_ExceptionResult()
        {
            var mockAssessmentProgressInput = MockAssessmentProgressSaveInput();
            var mockAssessmentStatus = MockAssessmentStatusInProgress();
            var mockAssessment = new Assessment();

            this.mockAssessmentStatusRepository = new MockAssessmentStatusRepository().MockAddAssessmentProgress(mockAssessmentStatus);
            this.mockAssessmentRepository = new MockAssessmentRepository().MockAddAssessmentProgressException(mockAssessment);

            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.InsertionSuccess);
            InitialiseAssessmentService();

            int updateUserID = 2;
            Assert.ThrowsAny<Exception>(() => this.assessmentService.AddAssessmentProgress(mockAssessmentProgressInput, updateUserID, false, 1));
        }

        private AssessmentStatus MockAssessmentStatusSubmit()
        {
            return new AssessmentStatus()
            {
                AssessmentStatusID = 1,
                Name = "Submitted",
                Abbrev = null,
                Description = null,
                ListOrder = 1,
                IsRemoved = false,
                UpdateDate = DateTime.UtcNow,
                UpdateUserID = 2
            };
        }

        private List<AssessmentProgressInputDTO> MockAssessmentProgressSubmitInput()
        {
            return new List<AssessmentProgressInputDTO>()
            {
                new AssessmentProgressInputDTO()
                {
                    PersonIndex = new Guid(),
                    QuestionnaireID = 1,
                    VoiceTypeID = 1,
                    DateTaken = DateTime.UtcNow,
                    ReasoningText = "reasoningText1",
                    AssessmentReasonID = 2,
                    AssessmentStatus = "Submit",
                    CloseDate = DateTime.UtcNow,
                    AssessmentResponses = MockAssessmentResponseInput()
                }
            };
        }

        private ResponseDTO MockResponse()
        {
            return new ResponseDTO()
            {
                ResponseID = 1,
                BackgroundColorPaletteID = 4,
                DefaultItemResponseBehaviorID = 3,
                Description = "Identified need requires monitoring, watchful waiting, or preventive activities.   Evidence of disruption in thought processes or content. Child/youth may be somewhat tangential in speech or evidence somewhat illogical thinking (age-inappropriate). This also includes child/youth with a history of hallucinations but none currently. Use this category for child/youth who are below the threshold for one of the DSM diagnoses listed above.",
                IsItemResponseBehaviorDisabled = false,
                ItemID = 1,
                KeyCodes = "1",
                Label = "1",
                ListOrder = 1,
                MaxRangeValue = null,
                Value = 1
            };
        }
        private QuestionnaireNotifyRiskRuleCondition MockQuestionnaireNotifyRiskRuleCondition()
        {
            return new QuestionnaireNotifyRiskRuleCondition()
            {
                ComparisonOperator = "==",
                ComparisonValue = 1,
                IsRemoved = false,
                QuestionnaireItemID = 1,
                QuestionnaireNotifyRiskRuleConditionID = 1,
                UpdateDate = DateTime.UtcNow,
                UpdateUserID = 1,
                QuestionnaireNotifyRiskRuleID = 1,
                ListOrder = 1
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

        private NotificationType GetMockNotificationType()
        {
            return new NotificationType()
            {
                NotificationTypeID = 1,
                Name = "Danger",
                ListOrder = 2,
                IsRemoved = false,
                UpdateDate = DateTime.UtcNow,
                UpdateUserID = 1
            };
        }

        private List<AssessmentValuesDTO> GetNeedforFocusValues()
        {
            return new List<AssessmentValuesDTO>()
            {
                new AssessmentValuesDTO()
                {
                    AssessmentResponseID = 1,
                    Value = 3
                },
                new AssessmentValuesDTO()
                {
                    AssessmentResponseID = 2,
                    Value = 1
                }
            };
        }

        //[Fact]
        //public void AddAssessmentProgress_OnSubmit_Success_ReturnsCorrectResult()
        //{
        //    var mockAssessmentStatus = MockAssessmentStatusSubmit();
        //    var mockAssessment = MockAssessment();
        //    var mockAssessmentResponse = MockAssessmentResponse();
        //    var mockNote = MockNote();
        //    var mockAssessmentResponseNote = MockAssessmentResponseNote();
        //    var mockAssessmentProgressInput = MockAssessmentProgressSubmitInput();
        //    var mockResponse = MockResponse();
        //    var mockQuestionnaireNotifyRiskRuleCondition = MockQuestionnaireNotifyRiskRuleCondition();
        //    var mockPerson = GetMockPerson();
        //    var mockNotificationStatus = GetMockNotificationStatus();
        //    var mockNotificationType = GetMockNotificationType();
        //    var mockGetNeedforFocusValues = GetNeedforFocusValues();
        //    var mockNoteList = MockNoteList();
        //    var mockAssessmentResponselist = MockAssessmentResponseList();
        //    var mockAssessmentResponselist1 = MockAssessmentResponseList1();

        //    this.mockAssessmentStatusRepository = new MockAssessmentStatusRepository().MockAddAssessmentProgress(mockAssessmentStatus);
        //    this.mockAssessmentRepository = new MockAssessmentRepository().MockAddAssessmentProgress(mockAssessment);
        //    this.mockAssessmentResponseRepository = new MockAssessmentResponseRepository().MockAddAssessmentProgress(mockAssessmentResponse, mockGetNeedforFocusValues, mockAssessmentResponselist, mockAssessmentResponselist1, mockAssessmentResponselist1);
        //    this.mockNoteRepository = new MockNoteRepository().MockAddNote(mockNote, mockNoteList);
        //    this.mockAssessmentResponseNoteRepository = new MockAssessmentResponseNoteRepository().MockAddAssessmentProgress(mockAssessmentResponseNote);
        //    this.mockResponseRepository = new MockResponseRepository().MockGetResponse(mockResponse);
        //    this.mockQuestionnaireNotifyRiskRuleConditionRepository = new MockQuestionnaireNotifyRiskRuleConditionRepository().MockGetQuestionnaireNotifyRiskRuleConditionByQuestionnaireItemID(mockQuestionnaireNotifyRiskRuleCondition);
        //    this.mockPersonRepository = new MockPersonRepository().MockGetPerson(mockPerson);
        //    this.mockNotifiationResolutionStatusRepository = new MockNotificationResolutionStatusRepository().MockGetNotificationStatus(mockNotificationStatus);
        //    this.mockNotificationTypeRepository = new MockNotificationTypeRepository().MockGetNotificationType(mockNotificationType);


        //    var mocknotificationType = new NotificationType();
        //    mocknotificationType.NotificationTypeID = 1;
        //    var mockNotificationLog = new NotificationLog();
        //    mockNotificationLog.NotificationLogID = 1;
        //    var mockNotificationResolutionStatus = new NotificationResolutionStatus();
        //    mockNotificationResolutionStatus.NotificationResolutionStatusID = 1;
        //    this.mockNotificationTypeRepository = new MockNotificationTypeRepository().MockGetNotificationType(mocknotificationType);
        //    this.mockNotificationLogRepository = new MockNotificationLogRepository().MockAddNotificationLog(mockNotificationLog);
        //    this.mockNotifiationResolutionStatusRepository = new MockNotificationResolutionStatusRepository().MockGetNotificationStatus(mockNotificationResolutionStatus);

        //    var mockAssessmentReason = GetMockAssessmentReason();
        //    this.mockAssessmentReasonRepository = new MockAssessmentReasonRepository().MockGetAssessmentReason(mockAssessmentReason);

        //    var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.SubmitSuccess);
        //    this.assessmentService = new AssessmentService(this.mockAssessmentResponseRepository.Object, this.mockAssessmentRepository.Object, this.mockQuestionnaireRepository.Object, this.mockLogger.Object,
        //                                           localize.Object, mockConfigRepository.Object, httpContextAccessor.Object, this.mockMapper.Object, this.mockNoteRepository.Object, this.mockAssessmentResponseNoteRepository.Object,
        //                                            this.mockAssessmentStatusRepository.Object, mockAssessmentEmailLinkRepository.Object, mockPersonSupportRepository.Object, mockEmailSender.Object, mockConfiguration.Object, mockDataProtector.Object,
        //                                           this.mockResponseRepository.Object, mockQuestionnaireNotifyRiskRuleConditionRepository.Object, mockPersonRepository.Object, mockNotificationTypeRepository.Object, mockNotificationLogRepository.Object,
        //                                           this.mockNotifiationResolutionStatusRepository.Object, this.mockNotifyRiskRepository.Object, this.mockAssessmentHistoryRepository.Object, this.mockAssessmentNoteRepository.Object,
        //                                           this.mockAssessmentReasonRepository.Object, this.mockUtility.Object, this.mockQueue.Object, this.mockVoiceTypeRepository.Object, this.mockAssessmentEmailOtpRepository.Object,
        //                   this.mockagencyRepository.Object, this.mockPersonQuestionnaireRepository.Object, this.mockHelperRepository.Object);
        //    this.assessmentService.GetTimeZoneFromHeader();
        //    int updateUserID = 2;
        //    var result = this.assessmentService.AddAssessmentProgress(mockAssessmentProgressInput, updateUserID, false);
        //    Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.SubmitSuccess);
        //}

        //[Fact]
        //public void AddAssessmentProgress_OnSubmit_Success_SendCorrectNotificationOnEmailSubmit()
        //{
        //    var mockAssessmentStatus = MockAssessmentStatusSubmit();
        //    var mockAssessment = MockAssessment();
        //    var mockAssessmentResponse = MockAssessmentResponse();
        //    var mockNote = MockNote();
        //    var mockAssessmentResponseNote = MockAssessmentResponseNote();
        //    var mockAssessmentProgressInput = MockAssessmentProgressSubmitInput();
        //    var mockResponse = MockResponse();
        //    var mockQuestionnaireNotifyRiskRuleCondition = MockQuestionnaireNotifyRiskRuleCondition();
        //    var mockPerson = GetMockPerson();
        //    var mockNotificationStatus = GetMockNotificationStatus();
        //    var mockNotificationType = GetMockNotificationType();
        //    var mockGetNeedforFocusValues = GetNeedforFocusValues();
        //    var mockNoteList = MockNoteList();
        //    var mockAssessmentResponselist = MockAssessmentResponseList();
        //    var mockAssessmentResponselist1 = MockAssessmentResponseList1();

        //    this.mockAssessmentStatusRepository = new MockAssessmentStatusRepository().MockAddAssessmentProgress(mockAssessmentStatus);
        //    this.mockAssessmentRepository = new MockAssessmentRepository().MockAddAssessmentProgress(mockAssessment);
        //    this.mockAssessmentResponseRepository = new MockAssessmentResponseRepository().MockAddAssessmentProgress(mockAssessmentResponse, mockGetNeedforFocusValues, mockAssessmentResponselist, mockAssessmentResponselist1, mockAssessmentResponselist1);
        //    this.mockNoteRepository = new MockNoteRepository().MockAddNote(mockNote, mockNoteList);
        //    this.mockAssessmentResponseNoteRepository = new MockAssessmentResponseNoteRepository().MockAddAssessmentProgress(mockAssessmentResponseNote);
        //    this.mockResponseRepository = new MockResponseRepository().MockGetResponse(mockResponse);
        //    this.mockQuestionnaireNotifyRiskRuleConditionRepository = new MockQuestionnaireNotifyRiskRuleConditionRepository().MockGetQuestionnaireNotifyRiskRuleConditionByQuestionnaireItemID(mockQuestionnaireNotifyRiskRuleCondition);
        //    this.mockPersonRepository = new MockPersonRepository().MockGetPerson(mockPerson);
        //    this.mockNotifiationResolutionStatusRepository = new MockNotificationResolutionStatusRepository().MockGetNotificationStatus(mockNotificationStatus);
        //    this.mockNotificationTypeRepository = new MockNotificationTypeRepository().MockGetNotificationType(mockNotificationType);

        //    var mocknotificationType = new NotificationType();
        //    mocknotificationType.NotificationTypeID = 1;
        //    var mockNotificationLog = new NotificationLog();
        //    mockNotificationLog.NotificationLogID = 1;
        //    var mockNotificationResolutionStatus = new NotificationResolutionStatus();
        //    mockNotificationResolutionStatus.NotificationResolutionStatusID = 1;
        //    this.mockNotificationTypeRepository = new MockNotificationTypeRepository().MockGetNotificationType(mocknotificationType);
        //    this.mockNotificationLogRepository = new MockNotificationLogRepository().MockAddNotificationLog(mockNotificationLog);
        //    this.mockNotifiationResolutionStatusRepository = new MockNotificationResolutionStatusRepository().MockGetNotificationStatus(mockNotificationResolutionStatus);

        //    var mockAssessmentReason = GetMockAssessmentReason();
        //    this.mockAssessmentReasonRepository = new MockAssessmentReasonRepository().MockGetAssessmentReason(mockAssessmentReason);

        //    var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.SubmitSuccess);
        //    this.assessmentService = new AssessmentService(this.mockAssessmentResponseRepository.Object, this.mockAssessmentRepository.Object, this.mockQuestionnaireRepository.Object, this.mockLogger.Object,
        //                                           localize.Object, mockConfigRepository.Object, httpContextAccessor.Object, this.mockMapper.Object, this.mockNoteRepository.Object, this.mockAssessmentResponseNoteRepository.Object,
        //                                            this.mockAssessmentStatusRepository.Object, mockAssessmentEmailLinkRepository.Object, mockPersonSupportRepository.Object, mockEmailSender.Object, mockConfiguration.Object, mockDataProtector.Object,
        //                                           this.mockResponseRepository.Object, mockQuestionnaireNotifyRiskRuleConditionRepository.Object, mockPersonRepository.Object, mockNotificationTypeRepository.Object, mockNotificationLogRepository.Object,
        //                                           this.mockNotifiationResolutionStatusRepository.Object, this.mockNotifyRiskRepository.Object, this.mockAssessmentHistoryRepository.Object, this.mockAssessmentNoteRepository.Object,
        //                                           this.mockAssessmentReasonRepository.Object, this.mockUtility.Object, this.mockQueue.Object, this.mockVoiceTypeRepository.Object, this.mockAssessmentEmailOtpRepository.Object,
        //                   this.mockagencyRepository.Object, this.mockPersonQuestionnaireRepository.Object, this.mockHelperRepository.Object);
        //    this.assessmentService.GetTimeZoneFromHeader();
        //    int updateUserID = 2;
        //    var result = this.assessmentService.AddAssessmentProgress(mockAssessmentProgressInput, updateUserID, true);
        //    Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.SubmitSuccess);
        //}

        private List<AssessmentProgressInputDTO> MockUpdateAssessmentProgressSaveInput()
        {
            return new List<AssessmentProgressInputDTO>()
            {
                new AssessmentProgressInputDTO()
                {
                    AssessmentID = 1,
                    PersonIndex = new Guid(),
                    QuestionnaireID = 1,
                    VoiceTypeID = 1,
                    DateTaken = DateTime.UtcNow,
                    ReasoningText = "reasoningText1",
                    AssessmentReasonID = 2,
                    AssessmentStatus = "Save",
                    CloseDate = DateTime.UtcNow,
                    AssessmentResponses = MockUpdateAssessmentResponseInput()
                }
            };
        }

        private List<Note> MockNoteList()
        {
            return new List<Note>()
            {
                new Note()
                {
                    NoteID = 1,
                    UpdateDate = DateTime.UtcNow,
                    NoteText = "reasoningText1",
                    IsConfidential=false,
                    IsRemoved=false,
                    UpdateUserID=1
                }
            };
        }



        private List<AssessmentResponseInputDTO> MockUpdateAssessmentResponseInput()
        {
            return new List<AssessmentResponseInputDTO>()
            {
                new AssessmentResponseInputDTO()
                {
                    AssessmentResponseID = 1,
                    PersonSupportID = 11,
                    ResponseID = 17,
                    ItemResponseBehaviorID = 4,
                    IsRequiredConfidential = true,
                    IsPersonRequestedConfidential = true,
                    IsOtherConfidential = true,
                    QuestionnaireItemID = 5,
                    IsCloned = true,
                    AssessmentResponseNotes = MockUpdateAssessmentResponseNoteInput()
                }
            };
        }

        private List<AssessmentResponseNoteInputDTO> MockUpdateAssessmentResponseNoteInput()
        {
            return new List<AssessmentResponseNoteInputDTO>()
            {
                new AssessmentResponseNoteInputDTO()
                {
                    NoteID = 1,
                    NoteText = "Note1",
                    IsConfidential = true
                }
            };
        }

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
                },
                new AssessmentReason()
                {
                  AssessmentReasonID = 3,
                  Name = "Initial",
                  Abbrev = null,
                  Description = null,
                  ListOrder = 2,
                  IsRemoved = false,
                  UpdateDate = DateTime.UtcNow,
                  UpdateUserID = 1
                },
                new AssessmentReason()
                {
                  AssessmentReasonID = 4,
                  Name = "Discharge",
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
        public void UpdateAssessmentProgress_OnSave_Success_ReturnsCorrectResult()
        {
            var mockAssessmentStatus = MockAssessmentStatusInProgress();
            var mockAssessment = MockAssessment();
            var mockAssessmentResponse = MockAssessmentResponse();
            var mockNote = MockNote();
            var mockAssessmentResponseNote = MockAssessmentResponseNote();
            var mockAssessmentProgressInput = MockUpdateAssessmentProgressSaveInput();
            var mockNoteList = MockNoteList();
            var mockAssessmentResponseList = MockAssessmentResponseList();
            var mockAssessmentResponselist1 = MockAssessmentResponseList1();
            var mockPerson = GetMockPerson();

            this.mockPersonRepository = new MockPersonRepository().MockGetPerson(mockPerson);
            this.mockAssessmentRepository = new MockAssessmentRepository().MockGetAssessment(mockAssessment, mockAssessment);
            this.mockAssessmentResponseRepository = new MockAssessmentResponseRepository().MockUpdateAssessmentProgress(mockAssessmentResponseList, mockAssessmentResponse, mockAssessmentResponselist1);
            this.mockNoteRepository = new MockNoteRepository().MockUpdateNote(mockNote, mockNoteList);
            this.mockAssessmentStatusRepository = new MockAssessmentStatusRepository().MockGetAllAssessmentStatus(MockAssessmentStatusList());
            this.mockAssessmentReasonRepository = new MockAssessmentReasonRepository().MockGetAllAssessmentReason(MockAssessmentReasonList());

            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.SaveSuccess);
            this.InitialiseAssessmentService();

            int updateUserID = 2;
            var result = this.assessmentService.AddAssessmentProgress(mockAssessmentProgressInput, updateUserID, false, 1);
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.SaveSuccess);
        }

        #endregion AddAssessmentProgress

        #region AddAssessmentForEmail

        private AssessmentStatus MockAssessmentStatusEmailSent()
        {
            return new AssessmentStatus()
            {
                AssessmentStatusID = 4,
                Name = "Email Sent",
                Abbrev = null,
                Description = null,
                ListOrder = 4,
                IsRemoved = false,
                UpdateDate = DateTime.UtcNow,
                UpdateUserID = 2
            };
        }

        private AssessmentInputDTO MockAssessmentInput()
        {
            return new AssessmentInputDTO()
            {
                PersonIndex = Guid.NewGuid(),
                QuestionnaireID = 1,
                VoiceTypeID = 1,
                DateTaken = DateTime.UtcNow,
                Note = "reasoningText1",
                AssessmentReasonID = 2,
                CloseDate = DateTime.UtcNow,
                HelperID = 1,
                PersonSupportID = 1
            };
        }

        [Fact]
        public void AddAssessmentForEmail_Success_ReturnsCorrectResultBySendingEmail()
        {
            var mockAssessmentStatus = MockAssessmentStatusEmailSent();
            var mockAssessment = MockAssessment();
            var mockAssessmentInput = MockAssessmentInput();
            this.mockAssessmentStatusRepository = new MockAssessmentStatusRepository().MockAddAssessmentProgress(mockAssessmentStatus);
            this.mockAssessmentRepository = new MockAssessmentRepository().MockAddAssessmentProgress(mockAssessment);
            MockSendAssessmentEmailLink(HttpStatusCode.Accepted);
            InitialiseAssessmentService();
            int updateUserID = 2;
            var result = this.assessmentService.AddAssessmentForEmail(mockAssessmentInput, updateUserID, 1, "agency1");
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.EmailSendSuccess);
        }

        [Fact]
        public void AddAssessmentForEmail_Failure_ErrorInSendingEmail()
        {
            var mockAssessmentStatus = MockAssessmentStatusEmailSent();
            var mockAssessment = MockAssessment();
            var mockAssessmentInput = MockAssessmentInput();
            this.mockAssessmentRepository = new MockAssessmentRepository().MockAddAssessmentProgress(mockAssessment);
            this.mockAssessmentStatusRepository = new MockAssessmentStatusRepository().MockAddAssessmentProgress(mockAssessmentStatus);
            this.mockAssessmentRepository = new MockAssessmentRepository().MockAddAssessmentProgress(mockAssessment);
            MockSendAssessmentEmailLink(HttpStatusCode.BadRequest);
            InitialiseAssessmentService();
            int updateUserID = 2;
            var result = this.assessmentService.AddAssessmentForEmail(mockAssessmentInput, updateUserID, 1, "agency1");
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.EmailSendFailed);
        }
        [Fact]
        public void AddAssessmentForEmail_Exception_ExceptionInEmailSending()
        {
            var mockAssessmentStatus = MockAssessmentStatusEmailSent();
            var mockAssessment = MockAssessment();
            var mockAssessmentInput = MockAssessmentInput();
            this.mockAssessmentStatusRepository = new MockAssessmentStatusRepository().MockAddAssessmentProgress(mockAssessmentStatus);
            this.mockAssessmentRepository = new MockAssessmentRepository().MockAddAssessmentProgress(mockAssessment);
            MockSendAssessmentEmailLink(HttpStatusCode.BadRequest);
            this.mockDataProtector = new MockDataProtection().MockProtectException();
            InitialiseAssessmentService();
            int updateUserID = 2;
            Assert.ThrowsAny<Exception>(() => this.assessmentService.AddAssessmentForEmail(mockAssessmentInput, updateUserID, 1, "agency1"));

        }

        [Fact]
        public void AddAssessmentForEmail_ExceptionResult()
        {
            var mockAssessmentInput = MockAssessmentInput();
            var mockAssessmentStatus = MockAssessmentStatusEmailSent();
            var mockAssessment = new Assessment();

            this.mockAssessmentStatusRepository = new MockAssessmentStatusRepository().MockAddAssessmentProgress(mockAssessmentStatus);
            this.mockAssessmentRepository = new MockAssessmentRepository().MockAddAssessmentProgressException(mockAssessment);

            InitialiseAssessmentService();
            int updateUserID = 2;
            Assert.ThrowsAny<Exception>(() => this.assessmentService.AddAssessmentForEmail(mockAssessmentInput, updateUserID, 1, "agency1"));
        }

        #endregion

        #region SendAssessmentEmailLink   
        private void MockSendAssessmentEmailLink(HttpStatusCode httpStatusCode)
        {
            var mockAssessmentEmailLink = GetMockAssessmentEmailLinkInputsValues();
            var mockPersonSupportDetails = GetmockPersonSupportDetails();
            var mockAssessmentEmailLinkDetails = GetMockAssessmentEmailLinkDetails();
            var mockConfigParameter = MockConfigurationParameter();
            var mockPerson = MockPerson();
            var mockVoiceType = MockVoiceType();
            this.mockPersonRepository = new MockPersonRepository().MockGetPersonDetailsShared(mockPerson);
            this.mockVoiceTypeRepository = new MockVoiceTypeRepository().MockGetVoiceType(mockVoiceType);
            this.mockConfigRepository = new MockConfigurationRepository().GetConfigurationByName(mockConfigParameter);
            this.mockPersonSupportRepository = new MockPersonSupportRepository().MockPeopleSupportDetails(mockPersonSupportDetails);
            this.mockAssessmentEmailLinkRepository = new MockAssessmentEmailLinkRepository().MockAddEmailLinkData(mockAssessmentEmailLinkDetails);
            this.mockDataProtector = new MockDataProtection().MockProtect("protectedQueryParameter");
            this.mockConfiguration.Setup(x => x[It.IsAny<string>()]).Returns("salt");
            this.mockEmailSender = new MockEmailSender().MockSendEmailAsync(httpStatusCode);
        }

        private Person MockPerson()
        {
            var personDetails = new Person()
            {
                Email = "geethu.joseph@naicoits.com",
                FirstName = "aaa",
                MiddleName = "",
                LastName = "zzz",
                PersonID = 1,
                AgencyID = 1,
                EmailPermission =true
        };
            return personDetails;
        }

        private VoiceType MockVoiceType()
        {
            var voiceType = new VoiceType()
            {
                Name = "Consumer",
                VoiceTypeID = 1
            };
            return voiceType;
        }

        [Fact]
        public void SendAssessmentEmailLink_Success_ReturnsCorrectResult()
        {
            ////Arrange
            var mockAssessmentEmailLink = GetMockAssessmentEmailLinkInputsValues();
            MockSendAssessmentEmailLink(HttpStatusCode.Accepted);
            InitialiseAssessmentService();
            //Act
            var result = this.assessmentService.SendAssessmentEmailLink(mockAssessmentEmailLink, 1, "agency1", false);
            //Assert
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.EmailSendSuccess);
        }
        [Fact]
        public void SendAssessmentEmailLink_Success_ReturnsFailureResult()
        {
            //Arrange
            var mockAssessmentEmailLink = GetMockAssessmentEmailLinkInputsValues();
            MockSendAssessmentEmailLink(HttpStatusCode.BadRequest);
            InitialiseAssessmentService();
            //Act
            var result = this.assessmentService.SendAssessmentEmailLink(mockAssessmentEmailLink, 1, "agency1", false);
            //Assert
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.EmailSendFailed);
        }

        [Fact]
        public void SendAssessmentEmailLink_Failure_OnInvalidBaseUrl()
        {
            //Arrange
            var mockAssessmentEmailLink = GetMockAssessmentEmailLinkInputsValues();
            var mockPersonSupportDetails = GetmockPersonSupportDetails();
            var mockAssessmentEmailLinkDetails = GetMockAssessmentEmailLinkDetails();
            var mockPerson = MockPerson();
            var mockVoiceType = MockVoiceType();

            this.mockPersonRepository = new MockPersonRepository().MockGetPersonRow(mockPerson);
            this.mockVoiceTypeRepository = new MockVoiceTypeRepository().MockGetVoiceType(mockVoiceType);
            this.mockPersonSupportRepository = new MockPersonSupportRepository().MockPeopleSupportDetails(mockPersonSupportDetails);
            this.mockAssessmentEmailLinkRepository = new MockAssessmentEmailLinkRepository().MockAddEmailLinkData(mockAssessmentEmailLinkDetails);
            this.mockDataProtector = new MockDataProtection().MockProtect("protectedQueryParameter");
            this.mockConfiguration.Setup(x => x[It.IsAny<string>()]).Returns("WrongUrl");
            InitialiseAssessmentService();
            //Act
            var result = this.assessmentService.SendAssessmentEmailLink(mockAssessmentEmailLink, 1, "agency1", false);

            //Assert
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.InvalidEmailAssessmentURL);
        }

        [Fact]
        public void SendAssessmentEmailLink_Exception_OnGettingSupportDetails()
        {
            //Arrange
            var mockAssessmentEmailLink = GetMockAssessmentEmailLinkInputsValues();
            var mockPersonSupportDetails = GetmockPersonSupportDetails();
            this.mockPersonSupportRepository = new MockPersonSupportRepository().MockPeopleSupportDetailsException(mockPersonSupportDetails);
            InitialiseAssessmentService();
            //Act AND Assert
            Assert.ThrowsAny<Exception>(() => this.assessmentService.SendAssessmentEmailLink(mockAssessmentEmailLink, 1, "agency1", false));
        }

        [Fact]
        public void SendAssessmentEmailLink_Exception_OnAddingEmailLinkDetails()
        {
            //Arrange
            var mockAssessmentEmailLink = GetMockAssessmentEmailLinkInputsValues();
            var mockPersonSupportDetails = GetmockPersonSupportDetails();
            var mockAssessmentEmailLinkDetails = GetMockAssessmentEmailLinkDetails();
            this.mockPersonSupportRepository = new MockPersonSupportRepository().MockPeopleSupportDetails(mockPersonSupportDetails);
            this.mockAssessmentEmailLinkRepository = new MockAssessmentEmailLinkRepository().MockAddEmailLinkDataException(mockAssessmentEmailLinkDetails);
            InitialiseAssessmentService();
            //Act AND Assert
            Assert.ThrowsAny<Exception>(() => this.assessmentService.SendAssessmentEmailLink(mockAssessmentEmailLink, 1, "agency1", false));
        }
        [Fact]
        public void SendAssessmentEmailLink_Exception_IfKeyForEncryptIsMissing()
        {
            //Arrange
            var mockAssessmentEmailLink = GetMockAssessmentEmailLinkInputsValues();
            var mockPersonSupportDetails = GetmockPersonSupportDetails();
            var mockAssessmentEmailLinkDetails = GetMockAssessmentEmailLinkDetails();
            this.mockPersonSupportRepository = new MockPersonSupportRepository().MockPeopleSupportDetails(mockPersonSupportDetails);
            this.mockAssessmentEmailLinkRepository = new MockAssessmentEmailLinkRepository().MockAddEmailLinkData(mockAssessmentEmailLinkDetails);
            this.mockConfiguration.Setup(x => x["AssessmentEmailLink-Key"]).Throws<Exception>();
            this.mockDataProtector = new MockDataProtection().MockProtect("protectedQueryParameter");
            InitialiseAssessmentService();
            //Act AND Assert
            Assert.ThrowsAny<Exception>(() => this.assessmentService.SendAssessmentEmailLink(mockAssessmentEmailLink, 1, "agency1", false));
        }
        private ConfigurationParameterDTO MockConfigurationParameter()
        {
            ConfigurationParameterDTO mockConfigParameter = new ConfigurationParameterDTO()
            {
                ConfigurationParameterID = 1,
                Name = "Config1",
                Value = "https://www.sample.com/ExternalAssessment"
            };
            return mockConfigParameter;
        }
        private ConfigurationParameterDTO MockIntConfigurationParameter()
        {
            ConfigurationParameterDTO mockConfigParameter = new ConfigurationParameterDTO()
            {
                ConfigurationParameterID = 1,
                Name = "Config1",
                Value = "72"
            };
            return mockConfigParameter;
        }

        private AssessmentEmailLinkDetails GetMockAssessmentEmailLinkDetails()
        {
            var assessmentEmailLinkDetails = new AssessmentEmailLinkDetails()
            {
                EmailLinkDetailsIndex = new Guid("ff52d58a-7edd-43ab-ac9d-36e05eeefa54"),
                PersonIndex = new Guid("ff52d58a-7edd-43ab-ac9d-36e05eeefa54"),
                PersonSupportID = 1,
                QuestionnaireID = 1,
                AssessmentEmailLinkDetailsID = 1

            };
            return assessmentEmailLinkDetails;
        }
        private AssessmentEmailLinkInputDTO GetMockAssessmentEmailLinkInputsValues()
        {
            var assessmentEmailLinkDTO = new AssessmentEmailLinkInputDTO()
            {
                PersonIndex = new Guid("ff52d58a-7edd-43ab-ac9d-36e05eeefa54"),
                PersonSupportID = 1,
                QuestionnaireID = 1
            };
            return assessmentEmailLinkDTO;
        }
        private PersonSupport GetmockPersonSupportDetails()
        {
            var personSupport = new PersonSupport()
            {
                PersonSupportID = 1,
                FirstName = "xxx",
                MiddleName = "",
                LastName = "yyy",
                Email = "geethu.joseph@naicoits.com",
                EmailPermission = true
            };
            return personSupport;
        }
        private AssessmentEmailOtp GetMockAssesmentEmailOtp()
        {
            var assessmentEmailOtp = new AssessmentEmailOtp()
            {
                AssessmentEmailLinkDetailsID = 1,
                Otp = "L01LVX0L4FII",
                ExpiryTime = DateTime.UtcNow.AddMinutes(10),
                UpdateDate = DateTime.UtcNow

            };
            return assessmentEmailOtp;
        }

        #endregion

        #region DecryptAssessmentEmailLink
        [Fact]
        public void GetDetailsFromAssessmentEmailLink_Success_ReturnsCorrectResult()
        {
            //Arrange
            var mockAssessment = MockAssessment();
            var mockAssessmentEmailLinkDetails = GetMockAssessmentEmailLinkDetails();
            var mockAssessmentEmailOtp = GetMockAssesmentEmailOtp();
            var mockPerson = GetMockPerson();
            var mockConfigParameter = MockIntConfigurationParameter();
            var mockPersonInitials = new PersonInitialsDTO();
            mockPersonInitials.PersonInitials = "a d b";

            this.mockPersonRepository = new MockPersonRepository().MockGetPersonWithInitials(mockPerson, mockPersonInitials);
            this.mockConfiguration.Setup(x => x[It.IsAny<string>()]).Returns("purposestring");
            this.mockDataProtector = new MockDataProtection().MockUnProtect(new Guid().ToString());
            this.mockAssessmentEmailLinkRepository = new MockAssessmentEmailLinkRepository().MockGetEmailLinkData(mockAssessmentEmailLinkDetails);
            this.mockAssessmentRepository = new MockAssessmentRepository().MockGetAssessmentByID(mockAssessment);
            this.mockAssessmentEmailOtpRepository = new MockAssessmentEmailOtpRepository().MockFindIsEmailOtpValid(mockAssessmentEmailOtp);
            this.mockConfigRepository = new MockConfigurationRepository().GetConfigurationByName(mockConfigParameter);
            InitialiseAssessmentService();
            //Act
            var result = this.assessmentService.GetDetailsFromAssessmentEmailLink("L01LVX0L4FII", "EncryptedQueryParameter", 1);
            //Assert
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.Success);
        }
        [Fact]
        public void GetDetailsFromAssessmentEmailLink_Failure_OnInvalidQueryIdIndex()
        {
            //Arrange
            var mockAssessmentEmailLinkDetails = GetMockAssessmentEmailLinkDetails();
            var mockConfigParameter = MockIntConfigurationParameter();
            this.mockConfiguration.Setup(x => x[It.IsAny<string>()]).Returns("purposestring");
            this.mockDataProtector = new MockDataProtection().MockUnProtect(new Guid().ToString());
            this.mockAssessmentEmailLinkRepository = new MockAssessmentEmailLinkRepository().MockGetEmailLinkData(null);
            this.mockAssessmentEmailOtpRepository = new MockAssessmentEmailOtpRepository().MockFindIsEmailOtpValid(null);
            this.mockConfigRepository = new MockConfigurationRepository().GetConfigurationByName(mockConfigParameter);
            InitialiseAssessmentService();
            //Act
            var result = this.assessmentService.GetDetailsFromAssessmentEmailLink("L01LVX0L4FII", "EncryptedQueryParameter", 1);
            //Assert
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.Failure);
        }
        [Fact]
        public void GetDetailsFromAssessmentEmailLink_Exception_IfKeyForDecryptIsMissing()
        {
            //Arrange
            var mockAssessmentEmailLinkDetails = GetMockAssessmentEmailLinkDetails();
            var mockConfigParameter = MockIntConfigurationParameter();
            this.mockConfigRepository = new MockConfigurationRepository().GetConfigurationByName(mockConfigParameter);
            this.mockConfiguration.Setup(x => x["AssessmentEmailLink-Key"]).Throws<Exception>();
            InitialiseAssessmentService();
            //Act AND Assert
            Assert.ThrowsAny<Exception>(() => this.assessmentService.GetDetailsFromAssessmentEmailLink("L01LVX0L4FII", "test", 1));
        }
        #endregion

        #region InitialiseSeviceWithMockedDI
        private void InitialiseAssessmentService()
        {
            var localize = new MockLocalize().Localize();
            this.assessmentService = new AssessmentService(this.mockQuestionnaireDefaultResponseRuleActionRepository.Object, this.mockQuestionnaireDefaultResponseRuleConditionRepository.Object, this.mockQuestionnaireDefaultResponseRuleRepository.Object, this.smsSender.Object, this.mockQuestionnaireSkipLogicRuleConditionRepository.Object, this.mockQuestionnaireSkipLogicRuleActionRepository.Object, this.mockQuestionnaireSkipLogicRuleRepository.Object, this.mockAssessmentResponseRepository.Object, this.mockAssessmentRepository.Object, this.mockQuestionnaireRepository.Object, this.mockLogger.Object,
                                                    localize.Object, mockConfigRepository.Object, httpContextAccessor.Object, this.mockMapper.Object, this.mockNoteRepository.Object, this.mockAssessmentResponseNoteRepository.Object,
                                                     this.mockAssessmentStatusRepository.Object, mockAssessmentEmailLinkRepository.Object, mockPersonSupportRepository.Object, mockEmailSender.Object, mockConfiguration.Object, mockDataProtector.Object,
                                                    this.mockResponseRepository.Object, mockQuestionnaireNotifyRiskRuleConditionRepository.Object, mockPersonRepository.Object, mockNotificationTypeRepository.Object, mockNotificationLogRepository.Object,
                                                    this.mockNotifiationResolutionStatusRepository.Object, this.mockNotifyRiskRepository.Object, this.mockAssessmentHistoryRepository.Object, this.mockAssessmentNoteRepository.Object,
                                                    this.mockAssessmentReasonRepository.Object, this.mockUtility.Object, this.mockQueue.Object, this.mockVoiceTypeRepository.Object, this.mockAssessmentEmailOtpRepository.Object,
                            this.mockagencyRepository.Object, this.mockPersonQuestionnaireRepository.Object, this.mockHelperRepository.Object, this.mockqueryBuilder.Object,
                               this.personQuestionnaireScheduleRepository.Object,
            this.personQuestionnaireScheduleService.Object,this.mockLookupRepository.Object, this.mockAssessmentResponseFileRepository.Object);
        }

        #endregion

        #region AddAssessmentNotification
        [Fact]
        public void AddNotificationOnAssessment_Success_ReturnsCorrectResult()
        {
            //Arrange
            var mockNotificationInputValues = GetMockassesmentNotificationInputValues();
            var mocknotificationType = new NotificationType();
            mocknotificationType.NotificationTypeID = 1;
            var mockNotificationLog = new NotificationLog();
            mockNotificationLog.NotificationLogID = 1;
            var mockNotificationResolutionStatus = new NotificationResolutionStatus();
            mockNotificationResolutionStatus.NotificationResolutionStatusID = 1;
            this.mockNotificationTypeRepository = new MockNotificationTypeRepository().MockGetNotificationType(mocknotificationType);
            this.mockNotificationLogRepository = new MockNotificationLogRepository().MockAddNotificationLog(mockNotificationLog);
            this.mockAssessmentRepository = new MockAssessmentRepository().MockGetPersonIDFromAssessment(1);
            this.mockNotifiationResolutionStatusRepository = new MockNotificationResolutionStatusRepository().MockGetNotificationStatus(mockNotificationResolutionStatus);
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.NotificationSuccess);
            InitialiseAssessmentService();

            var result = this.assessmentService.AddNotificationOnAssessment(mockNotificationInputValues, null);
            //Assert
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.NotificationSuccess);

        }

        [Fact]
        public void AddNotificationOnAssessment_Failure_OnIvalidAssessmetID()
        {
            //Arrange
            var mockNotificationInputValues = GetMockassesmentNotificationInputValues();
            mockNotificationInputValues.AssessmentID = 0;
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.NotificationFailed);
            InitialiseAssessmentService();
            //Act
            var result = this.assessmentService.AddNotificationOnAssessment(mockNotificationInputValues, null);
            //Assert
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.NotificationFailed);

        }

        //[Fact]
        //public void AddNotificationOnAssessment_Failure_ReturnsNullOnInvalidNotificationType()
        //{
        //    //Arrange
        //    var mockNotificationInputValues = GetMockassesmentNotificationInputValues();
        //    var mocknotificationType = new NotificationType();
        //    mocknotificationType.Name = "InvalidNotification";
        //    mocknotificationType.NotificationTypeID = 0;
        //    this.mockNotificationTypeRepository = new MockNotificationTypeRepository().MockGetNotificationType(mocknotificationType);
        //    var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.NotificationFailed);
        //    this.assessmentService = new AssessmentService(this.mockAssessmentResponseRepository.Object, this.mockAssessmentRepository.Object, this.mockQuestionnaireRepository.Object, this.mockLogger.Object,
        //                                           localize.Object, mockConfigRepository.Object, httpContextAccessor.Object, this.mockMapper.Object, this.mockNoteRepository.Object, this.mockAssessmentResponseNoteRepository.Object,
        //                                            this.mockAssessmentStatusRepository.Object, mockAssessmentEmailLinkRepository.Object, mockPersonSupportRepository.Object, mockEmailSender.Object, mockConfiguration.Object, mockDataProtector.Object,
        //                                           this.mockResponseRepository.Object, mockQuestionnaireNotifyRiskRuleConditionRepository.Object, mockPersonRepository.Object, mockNotificationTypeRepository.Object, mockNotificationLogRepository.Object,
        //                                           this.mockNotifiationResolutionStatusRepository.Object, this.mockNotifyRiskRepository.Object, this.mockAssessmentHistoryRepository.Object, this.mockAssessmentNoteRepository.Object,
        //                                           this.mockAssessmentReasonRepository.Object, this.mockUtility.Object, this.mockQueue.Object, this.mockVoiceTypeRepository.Object, this.mockAssessmentEmailOtpRepository.Object,
        //                   this.mockagencyRepository.Object, this.mockPersonQuestionnaireRepository.Object, this.mockHelperRepository.Object);

        //    var result = this.assessmentService.AddNotificationOnAssessment(mockNotificationInputValues);
        //    Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.NotificationFailed);

        //}
        [Fact]
        public void AddNotificationOnAssessment_Exception_OnInvalidNotificationType()
        {
            //Arrange
            var mockNotificationInputValues = GetMockassesmentNotificationInputValues();
            this.mockNotificationTypeRepository = new MockNotificationTypeRepository().MockGetNotificationTypeException();
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.insertionFailed);
            InitialiseAssessmentService();

            Assert.ThrowsAny<Exception>(() => this.assessmentService.AddNotificationOnAssessment(mockNotificationInputValues, null));

        }

        private AssesmentNotificationInputDTO GetMockassesmentNotificationInputValues()
        {
            var assesmentNotificationInputDTO = new AssesmentNotificationInputDTO
            {
                AssesmentNotificationType = "AssessmentSubmit",
                AssessmentID = 1,
                NotificationDate = DateTime.Now,
                UpdateUserId = 1

            };
            return assesmentNotificationInputDTO;
        }
        #endregion
        [Fact]
        public void RemoveAssessment_Success_ReturnsCorrectResult()
        {
            var mockAssessment = MockAssessmentData();
            var mockAssessmentStatus = MockAssessmentStatusSubmitted();
            var mockUpdatedAssessment = MockUpdatedAssessment();
            var mockAssessmentResponse = MockAssessmentResponseList();
            var mockUpdatedAssessmentResponse = MockUpdatedAssessmentResponse();
            var mockNotificationLog = MockNotificationLog();
            var mockNotificationTypeList = MockNotificationTypeList();
            var mockAssessmentResponseList = MockAssessmentResponseList1();
            IReadOnlyList<NotificationLog> mocknotificationLogList = new List<NotificationLog>();

            this.mockAssessmentRepository = new MockAssessmentRepository().MockGetAssessment(mockAssessment, mockUpdatedAssessment);
            this.mockAssessmentStatusRepository = new MockAssessmentStatusRepository().MockGetAssessmentStatus(mockAssessmentStatus);
            this.mockAssessmentResponseRepository = new MockAssessmentResponseRepository().MockgetAssessmentResponse1(mockAssessmentResponse, mockUpdatedAssessmentResponse, mockAssessmentResponseList);
           this.mockAssessmentHistoryRepository = new MockAssessmentHistroyRepository().GetHistoryForAssessment();

            this.mockNotificationTypeRepository = new MockNotificationTypeRepository().MockGetAllNotificationType(mockNotificationTypeList);
            this.mockNotificationLogRepository = new MockNotificationLogRepository().MockGetNotificationLog(mockNotificationLog, mocknotificationLogList);

            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Success);
            MockValidAssessmentofPersonInAgency();
            MockValidPersonInAgency();
            InitialiseAssessmentService();
            int assessmentID = 1;
            List<string> roles = new List<string>();
            roles.Add("Helper RW");
            var result = this.assessmentService.RemoveAssessment(assessmentID, roles, 1);
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.DeletionFailed);
        }

        [Fact]
        public void RemoveAssessment_Success_ReturnsPermissionFailure()
        {
            var mockAssessment = MockAssessmentData();
            var mockAssessmentStatus = MockAssessmentStatusSubmit();
            var mockUpdatedAssessment = MockUpdatedAssessment();
            var mockAssessmentResponse = MockAssessmentResponseList();
            var mockUpdatedAssessmentResponse = new AssessmentResponse();
            this.mockAssessmentRepository = new MockAssessmentRepository().MockGetAssessment(mockAssessment, mockUpdatedAssessment);
            this.mockAssessmentStatusRepository = new MockAssessmentStatusRepository().MockGetAssessmentStatus(mockAssessmentStatus);
            this.mockAssessmentResponseRepository = new MockAssessmentResponseRepository().MockgetAssessmentResponse(mockAssessmentResponse, mockUpdatedAssessmentResponse);
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.DeletionFailed);
            MockValidAssessmentofPersonInAgency();
            MockValidPersonInAgency();
            InitialiseAssessmentService();
            int assessmentID = 1;
            List<string> roles = new List<string>();
            roles.Add("Helper RW");
            var result = this.assessmentService.RemoveAssessment(assessmentID, roles, 1);
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.DeletionFailed);
        }

        [Fact]
        public void RemoveAssessment_Failure_ExceptionResult()
        {

            var mockAssessment = MockAssessmentData();
            var mockAssessmentStatus = MockAssessmentStatusInProgress();
            var mockUpdatedAssessment = new Assessment();
            var mockAssessmentResponse = MockAssessmentResponseList();
            var mockUpdatedAssessmentResponse = new AssessmentResponse();
            this.mockAssessmentRepository = new MockAssessmentRepository().MockUpdateAssessmentException(mockAssessment, mockUpdatedAssessment);
            this.mockAssessmentStatusRepository = new MockAssessmentStatusRepository().MockGetAssessmentStatus(mockAssessmentStatus);
            this.mockAssessmentResponseRepository = new MockAssessmentResponseRepository().MockgetAssessmentResponse(mockAssessmentResponse, mockUpdatedAssessmentResponse);
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Success);
            MockValidAssessmentofPersonInAgency();
            MockValidPersonInAgency();
            InitialiseAssessmentService();
            int assessmentID = 1;
            List<string> roles = new List<string>();
            roles.Add("Helper RW");
            Assert.ThrowsAny<Exception>(() => this.assessmentService.RemoveAssessment(assessmentID, roles, 1));
        }

        private Assessment MockAssessmentData()
        {
            return new Assessment()
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
            };
        }

        private Assessment MockUpdatedAssessment()
        {
            return new Assessment()
            {
                AssessmentID = 1,
                PersonQuestionnaireID = 1,
                VoiceTypeID = 1,
                DateTaken = DateTime.UtcNow,
                ReasoningText = null,
                AssessmentReasonID = 2,
                AssessmentStatusID = 1,
                PersonQuestionnaireScheduleID = null,
                IsUpdate = true,
                Approved = null,
                CloseDate = DateTime.UtcNow,
                IsRemoved = true,
                UpdateDate = DateTime.UtcNow,
                UpdateUserID = 2
            };
        }

        private List<AssessmentResponsesDTO> MockAssessmentResponseList()
        {
            return new List<AssessmentResponsesDTO>()
            {
                new AssessmentResponsesDTO()
                {
                    AssessmentResponseID = 1,
                    AssessmentID = 1,
                    PersonSupportID = null,
                    ResponseID = 2,
                    ItemResponseBehaviorID = 3,
                    IsRequiredConfidential = false,
                    IsPersonRequestedConfidential = null,
                    IsOtherConfidential = true,
                    IsRemoved = false,
                    UpdateDate = DateTime.UtcNow,
                    UpdateUserID = 2,
                    QuestionnaireItemID = 1,
                    IsCloned = false
                }
            };
        }
        private List<AssessmentResponse> MockAssessmentResponseList1()
        {
            return new List<AssessmentResponse>()
            {
                new AssessmentResponse()
                {
                    AssessmentResponseID = 1,
                    AssessmentID = 1,
                    PersonSupportID = null,
                    ResponseID = 2,
                    ItemResponseBehaviorID = 1,
                    IsRequiredConfidential = false,
                    IsPersonRequestedConfidential = null,
                    IsOtherConfidential = false,
                    IsRemoved = false,
                    UpdateDate = DateTime.UtcNow,
                    UpdateUserID = 2,
                    QuestionnaireItemID = 1,
                    IsCloned = false,
                    Value=string.Empty
                }
            };
        }




        private AssessmentResponse MockUpdatedAssessmentResponse()
        {
            return new AssessmentResponse()
            {
                AssessmentResponseID = 1,
                AssessmentID = 1,
                PersonSupportID = null,
                ResponseID = 2,
                ItemResponseBehaviorID = 3,
                IsRequiredConfidential = false,
                IsPersonRequestedConfidential = null,
                IsOtherConfidential = false,
                IsRemoved = true,
                UpdateDate = DateTime.UtcNow,
                UpdateUserID = 2,
                QuestionnaireItemID = 1,
                IsCloned = false,
                Value = string.Empty
            };
        }

        private NotificationLog MockNotificationLog()
        {
            return new NotificationLog()
            {
                FKeyValue = 1,
                IsRemoved = false,
                NotificationData = "test",
                NotificationDate = DateTime.UtcNow,
                NotificationLogID = 1,
                NotificationResolutionStatusID = 1,
                NotificationTypeID = 1,
                PersonID = 1,
                UpdateUserID = 1
            };
        }

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

        #region ChangeReviewStatus
        private AssessmentReviewStatusDTO mockAssessmentReviewStatusInput()
        {
            return new AssessmentReviewStatusDTO
            {
                AssessmentID = 1,
                ReviewUserID = 1,
                AssessmentStatus = "Approved",
                ReviewNote = "Review Note Testing"
            };

        }
        private AssessmentStatus mockAssessmentStatus()
        {
            return new AssessmentStatus
            {
                AssessmentStatusID = 7,
                IsRemoved = false,
                ListOrder = 5,
                Name = "Approved",
                UpdateUserID = 1,
                Description = null

            };
        }
        private ReviewerHistory mockReviewerHistory()
        {
            return new ReviewerHistory
            {
                AssessmentReviewHistoryID = 1,
                RecordedDate = DateTime.UtcNow,
                StatusFrom = 2,
                StatusTo = 7,
                IsRemoved = false,
                UpdateDate = DateTime.UtcNow,
                UpdateUserID = 1
            };
        }
        private AssessmentNote MockAssessmentNote()
        {
            return new AssessmentNote
            {
                AssessmentID = 1,
                NoteID = 1,
            };
        }

        //[Fact]
        //public void ChangeReviewStatus_Success_ReturnsCorrectResult()
        //{
        //    var mockAssessment = MockAssessment();
        //    var mockAssessmentNote = MockAssessmentNote();
        //    var mockNote = MockNote();
        //    var mockAssessmentReviewStatusDataInput = mockAssessmentReviewStatusInput();
        //    var mockAssessmentStatus = this.mockAssessmentStatus();
        //    var mockAssessmentHistory = this.mockReviewerHistory();

        //    this.mockAssessmentRepository = new MockAssessmentRepository().MockGetAssessment(mockAssessment, mockAssessment);
        //    this.mockAssessmentNoteRepository = new MockAssessmentNoteRepository().MockAddAssessmentNote(mockAssessmentNote);
        //    this.mockNoteRepository = new MockNoteRepository().MockAddNote(mockNote);
        //    this.mockAssessmentStatusRepository = new MockAssessmentStatusRepository().GetAssessmentStatus(mockAssessmentStatus);
        //    this.mockAssessmentHistoryRepository = new MockAssessmentHistroyRepository().AddAssessmentHistory(mockAssessmentHistory);

        //    var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.SaveSuccess);
        //    InitialiseAssessmentService();
        //    var result = this.assessmentService.ChangeReviewStatus(mockAssessmentReviewStatusDataInput);
        //    Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.InsertionSuccess);
        //}

        [Fact]
        public void ChangeReviewStatus_Execption_Result()
        {
            var mockAssessment = MockAssessment();
            var mockAssessmentNote = MockAssessmentNote();
            var mockNote = MockNote();
            var mockAssessmentReviewStatusDataInput = mockAssessmentReviewStatusInput();
            var mockAssessmentStatus = this.mockAssessmentStatus();
            var mockAssessmentHistory = this.mockReviewerHistory();

            this.mockAssessmentRepository = new MockAssessmentRepository().MockGetAssessment(mockAssessment, mockAssessment);
            this.mockAssessmentNoteRepository = new MockAssessmentNoteRepository().MockAddAssessmentNote(mockAssessmentNote);
            this.mockNoteRepository = new MockNoteRepository().MockAddNote(mockNote);
            this.mockAssessmentStatusRepository = new MockAssessmentStatusRepository().GetAssessmentStatus(mockAssessmentStatus);
            this.mockAssessmentHistoryRepository = new MockAssessmentHistroyRepository().AddAssessmentHistoryException();

            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Failure);
            MockValidAssessmentofPersonInAgency();
            MockValidPersonInAgency();
            InitialiseAssessmentService();
            Assert.ThrowsAny<Exception>(() => this.assessmentService.ChangeReviewStatus(mockAssessmentReviewStatusDataInput, 1));
        }


        #endregion ChangeReviewStatus

        #region AssessmentPriority
        [Fact]
        public void AssessmentPriority_Success_ReturnsCorrectResult()
        {
            var mockAssessmentPriorityInputDTO = MockAssessmentPriorityInputDTO();
            var mockGetAssessmentResponses = MockUpdatedAssessmentResponse();
            this.mockAssessmentResponseRepository = new MockAssessmentResponseRepository().GetAssessmentResponses(mockGetAssessmentResponses);
            InitialiseAssessmentService();
            int updateUserID = 2;
            var result = this.assessmentService.AssessmentPriority(mockAssessmentPriorityInputDTO, updateUserID);
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.Success);
        }

        [Fact]
        public void AssessmentPriority_ExceptionResult()
        {
            AssessmentPriorityInputDTO assessmentPriorityInputDTO = new AssessmentPriorityInputDTO();
            var mockGetAssessmentResponses = MockUpdatedAssessmentResponse();
            this.mockAssessmentResponseRepository = new MockAssessmentResponseRepository().GetAssessmentResponses(mockGetAssessmentResponses);
            InitialiseAssessmentService();
            int updateUserID = 2;
            Assert.ThrowsAny<Exception>(() => this.assessmentService.AssessmentPriority(assessmentPriorityInputDTO, updateUserID));
        }

        private AssessmentPriorityInputDTO MockAssessmentPriorityInputDTO()
        {
            return new AssessmentPriorityInputDTO()
            {
                AssessmentPriorityList = this.MockAssessmentPriorityDTO()
            };
        }
        private List<AssessmentPriorityDTO> MockAssessmentPriorityDTO()
        {
            return new List<AssessmentPriorityDTO>()
            {
                new AssessmentPriorityDTO()
                {
                    AssessmentResponseID = 1,
                    Priority=11
                },
                  new AssessmentPriorityDTO()
                {
                    AssessmentResponseID = 21,
                    Priority=13
                },
            };
        }
        #endregion AssessmentPriority

        #region AssessmentEmailOtp

        [Fact]
        public void SendEmailAssessmentOtp_Success_ReturnsCorrectResult()
        {
            //Arrange
            var mockAssessment = MockAssessment();
            var mockAgencyDetails = GetMockAgency();
            var mockAssessmentEmailLinkDetails = GetMockAssessmentEmailLinkDetails();
            var mockAssessmentEmailOtp = GetMockAssesmentEmailOtp();
            var mockConfigParameter = MockIntConfigurationParameter();
            var mockAssessmentStatus = MockAssessmentStatusInProgress();

            this.mockagencyRepository = new MockAgencyRepository().MockGetAgency(mockAgencyDetails);
            this.mockAssessmentRepository = new MockAssessmentRepository().MockGetAssessment(mockAssessment, mockAssessment);
            this.mockAssessmentStatusRepository = new MockAssessmentStatusRepository().GetAssessmentStatus(mockAssessmentStatus);
            MockSendAssessmentEmailLink(HttpStatusCode.Accepted);
            this.mockConfiguration.Setup(x => x[It.IsAny<string>()]).Returns("purposestring");
            this.mockDataProtector = new MockDataProtection().MockUnProtect(new Guid().ToString());
            this.mockAssessmentEmailLinkRepository = new MockAssessmentEmailLinkRepository().MockGetEmailLinkData(mockAssessmentEmailLinkDetails);
            this.mockAssessmentEmailOtpRepository = new MockAssessmentEmailOtpRepository().MockAddOrUpdateEmailOtp(mockAssessmentEmailOtp);
            this.mockConfigRepository = new MockConfigurationRepository().GetConfigurationByName(mockConfigParameter);
            MockValidAssessmentofPersonInAgency();
            InitialiseAssessmentService();
            //Act
            var result = this.assessmentService.SendEmailAssessmentOtp("EncryptedQueryParameter", 1, "");
            //Assert
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.EmailSendSuccess);
        }

        private void MockValidAssessmentofPersonInAgency()
        {
            Person mockperson = MockPerson();
            PersonQuestionnaireDTO mockQuestionnaire = new PersonQuestionnaireDTO();
            mockQuestionnaire.QuestionnaireID = 1;
            this.mockPersonQuestionnaireRepository = new MockPersonQuestionnaireRepository().MockGetPersonAndQuestionnaireForAgency(mockQuestionnaire, mockperson);

        }
        private void MockValidPersonInAgency()
        {
            this.mockPersonRepository = new MockPersonRepository().MockGetIsValidPersonInAgency(true);
        }

        [Fact]
        public void SendEmailAssessmentOtp_Failure_OnInvalidQueryIdIndex()
        {
            var mockAssessment = MockAssessment();
            var mockAgencyDetails = GetMockAgency();
            var mockAssessmentEmailLinkDetails = GetMockAssessmentEmailLinkDetails();
            var mockAssessmentEmailOtp = GetMockAssesmentEmailOtp();
            var mockConfigParameter = MockIntConfigurationParameter();
            var mockAssessmentStatus = MockAssessmentStatusInProgress();
            var mockPerson = MockPerson();

            MockSendAssessmentEmailLink(HttpStatusCode.BadRequest);
            this.mockagencyRepository = new MockAgencyRepository().MockGetAgency(mockAgencyDetails);
            this.mockAssessmentRepository = new MockAssessmentRepository().MockGetAssessment(mockAssessment, mockAssessment);
            this.mockAssessmentStatusRepository = new MockAssessmentStatusRepository().GetAssessmentStatus(mockAssessmentStatus);
            this.mockConfiguration.Setup(x => x[It.IsAny<string>()]).Returns("purposestring");
            this.mockDataProtector = new MockDataProtection().MockUnProtect(new Guid().ToString());
            this.mockAssessmentEmailLinkRepository = new MockAssessmentEmailLinkRepository().MockGetEmailLinkData(mockAssessmentEmailLinkDetails);
            this.mockAssessmentEmailOtpRepository = new MockAssessmentEmailOtpRepository().MockAddOrUpdateEmailOtp(mockAssessmentEmailOtp);
            this.mockConfigRepository = new MockConfigurationRepository().GetConfigurationByName(mockConfigParameter);
            MockValidAssessmentofPersonInAgency();
            InitialiseAssessmentService();
            //Act
            var result = this.assessmentService.SendEmailAssessmentOtp("EncryptedQueryParameter", 1, "");
            //Assert
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.EmailSendFailed);
        }
        [Fact]
        public void SendEmailAssessmentOtp_Exception_IfOtpIsMissing()
        {
            //Arrange
            var mockAssessmentEmailLinkDetails = GetMockAssessmentEmailLinkDetails();
            this.mockConfiguration.Setup(x => x["AssessmentEmailLink-Key"]).Throws<Exception>();
            InitialiseAssessmentService();
            //Act AND Assert
            Assert.ThrowsAny<Exception>(() => this.assessmentService.SendEmailAssessmentOtp("EncryptedQueryParameter", 1, ""));
        }
        #endregion AssessmentEmailOtp

        #region GetLastAssessmentByPerson
        [Fact]
        public void GetLastAssessmentByPerson_Success_ReturnsCorrectResult()
        {
            var mockPersonDetails = GetMockPerson();
            var assessment = new List<Assessment> { MockAssessment() };

            this.mockPersonRepository = new MockPersonRepository().MockGetPerson(mockPersonDetails);
            this.mockAssessmentRepository = new MockAssessmentRepository().MockGetLastAssessmentByPerson(assessment);
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Success);
            this.InitialiseAssessmentService();
            UserTokenDetails userTokenDetails = new UserTokenDetails() { AgencyID = 0, UserID = 0 };
            var result = this.assessmentService.GetLastAssessmentByPerson(mockPersonDetails.PersonIndex, 1, 0, 0, 0, userTokenDetails);
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.Success);
        }
        [Fact]
        public void GetLastAssessmentByPerson_Success_ReturnsnullResult()
        {
            var mockPersonDetails = GetMockPerson();

            this.mockPersonRepository = new MockPersonRepository().MockGetPerson(mockPersonDetails);
            this.mockAssessmentRepository = new MockAssessmentRepository().MockGetLastAssessmentByPerson(null);
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Success);
            this.InitialiseAssessmentService();

            UserTokenDetails userTokenDetails = new UserTokenDetails() { AgencyID = 0, UserID = 0 };
            var result = this.assessmentService.GetLastAssessmentByPerson(mockPersonDetails.PersonIndex, 1, 0, 0, 0, userTokenDetails);
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.Success);
        }

        [Fact]
        public void GetLastAssessmentByPerson_Failure_OnInvalidPerson()
        {
            var mockPersonDetails = GetMockPerson();
            this.mockPersonRepository = new MockPersonRepository().MockGetPerson(null);
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Failure);
            this.InitialiseAssessmentService();

            UserTokenDetails userTokenDetails = new UserTokenDetails() { AgencyID = 0, UserID = 0 };
            var result = this.assessmentService.GetLastAssessmentByPerson(mockPersonDetails.PersonIndex, 1, 0, 0, 0, userTokenDetails);
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.Failure);
        }

        [Fact]
        public void GetLastAssessmentByPerson_Exception()
        {
            var mockPersonDetails = GetMockPerson();

            this.mockPersonRepository = new MockPersonRepository().MockGetPerson(mockPersonDetails);
            this.mockAssessmentRepository = new MockAssessmentRepository().MockGetLastAssessmentByPersonException();
            this.InitialiseAssessmentService();

            UserTokenDetails userTokenDetails = new UserTokenDetails() { AgencyID = 0, UserID = 0 };
            Assert.ThrowsAny<Exception>(() => this.assessmentService.GetLastAssessmentByPerson(mockPersonDetails.PersonIndex, 1, 0, 0, 0, userTokenDetails));
        }
        #endregion

        private Agency GetMockAgency()
        {
            Agency AgencyDTO = new Agency()
            {
                Name = "Test333",
                ContactFirstName = "ContactFirstName1",
                ContactLastName = "ContactLastName2",
                Phone1 = "Phone1",
                Phone2 = "Phone2",
                Email = "abc@xyz.com",
                AgencyID = 1,
                AgencyIndex = Guid.NewGuid(),
                Abbrev = "abbrev12"
            };
            return AgencyDTO;
        }

        #region GetAssessmentById
        [Fact]
        public void GetAssessmentById_Success_ReturnsCorrectResult()
        {
            var mockAssessment = MockAssessment();
            this.mockAssessmentRepository = new MockAssessmentRepository().MockGetAssessment(mockAssessment, null);
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Success);
            this.InitialiseAssessmentService();
            var result = this.assessmentService.GetAssessmentById(1);
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.Success);
        }

        [Fact]
        public void GetAssessmentById_Exception()
        {
            this.mockAssessmentRepository = new MockAssessmentRepository().MockGetAssessmentException();
            this.InitialiseAssessmentService();
            Assert.ThrowsAny<Exception>(() => this.assessmentService.GetAssessmentById(1));
        }
        #endregion GetAssessmentById

        #region GetAssessmentByPersonQuestionaireIdAndStatus
        [Fact]
        public void GetAssessmentByPersonQuestionaireIdAndStatus_Success_ReturnsCorrectResult()
        {
            var mockAssessmentStatus = MockAssessmentStatusApproved();
            var mockAssessment = MockAssessment();
            this.mockAssessmentStatusRepository = new MockAssessmentStatusRepository().GetAssessmentStatus(mockAssessmentStatus);
            this.mockAssessmentRepository = new MockAssessmentRepository().MockGetAssessmentByPersonQuestionaireID(mockAssessment);
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Success);
            this.InitialiseAssessmentService();
            var result = this.assessmentService.GetAssessmentByPersonQuestionaireIdAndStatus(1, "Approved");
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.Success);
        }

        [Fact]
        public void GetAssessmentByPersonQuestionaireIdAndStatus_Exception()
        {
            var mockAssessmentStatus = MockAssessmentStatusApproved();
            this.mockAssessmentStatusRepository = new MockAssessmentStatusRepository().GetAssessmentStatus(mockAssessmentStatus);
            this.mockAssessmentRepository = new MockAssessmentRepository().MockGetAssessmentByPersonQuestionaireIDException();
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Failure);
            this.InitialiseAssessmentService();
            Assert.ThrowsAny<Exception>(() => this.assessmentService.GetAssessmentByPersonQuestionaireIdAndStatus(1, "Approved"));
        }

        private AssessmentStatus MockAssessmentStatusApproved()
        {
            return new AssessmentStatus()
            {
                AssessmentStatusID = 2,
                Name = "Approved",
                Abbrev = null,
                Description = null,
                ListOrder = 1,
                IsRemoved = false,
                UpdateDate = DateTime.UtcNow,
                UpdateUserID = 2
            };
        }
        #endregion GetAssessmentByPersonQuestionaireIdAndStatus

        #region GetAssessmentResponses
        [Fact]
        public void GetAssessmentResponses_Success_ReturnsCorrectResult()
        {
            var mockAssessmentResponse = MockAssessmentResponseList();
            this.mockAssessmentResponseRepository = new MockAssessmentResponseRepository().MockgetAssessmentAllResponse(mockAssessmentResponse, null);
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Success);
            this.InitialiseAssessmentService();
            var result = this.assessmentService.GetAssessmentResponses(1);
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.Success);
        }

        [Fact]
        public void GetAssessmentResponses_Exception()
        {
            this.mockAssessmentResponseRepository = new MockAssessmentResponseRepository().MockgetAssessmentResponseException();
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Failure);
            this.InitialiseAssessmentService();
            Assert.ThrowsAny<Exception>(() => this.assessmentService.GetAssessmentResponses(1));
        }
        #endregion GetAssessmentResponses


        #region GetAssessmentsByPersonQuestionaireID
        [Fact]
        public void GetAssessmentsByPersonQuestionaireID_Success_ReturnsCorrectResult()
        {
            var mockAssessmentStatus = MockAssessmentStatusApproved();
            var assessment = new List<Assessment> { MockAssessment() };
            this.mockAssessmentStatusRepository = new MockAssessmentStatusRepository().GetAssessmentStatus(mockAssessmentStatus);
            this.mockPersonQuestionnaireRepository = new MockPersonQuestionnaireRepository().MockGetAssessmentsByPersonQuestionaireID(assessment);
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Success);
            this.InitialiseAssessmentService();
            var result = this.assessmentService.GetAssessmentsByPersonQuestionaireID(1);
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.Success);
        }

        [Fact]
        public void GetAssessmentsByPersonQuestionaireID_Exception()
        {
            var mockAssessmentStatus = MockAssessmentStatusApproved();
            this.mockAssessmentStatusRepository = new MockAssessmentStatusRepository().GetAssessmentStatus(mockAssessmentStatus);
            this.mockPersonQuestionnaireRepository = new MockPersonQuestionnaireRepository().MockGetAssessmentsByPersonQuestionaireIDException();
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Failure);
            this.InitialiseAssessmentService();
            Assert.ThrowsAny<Exception>(() => this.assessmentService.GetAssessmentsByPersonQuestionaireID(1));
        }
        #endregion GetAssessmentResponses


        #region GetAssessmentResponseFOrDashboardCalculation
        [Fact]
        public void GetAssessmentResponseFOrDashboardCalculation_Success_ReturnsCorrectResult()
        {
            var mockAssessmentResponselist = MockAssessmentResponseList();

            this.mockAssessmentStatusRepository = new MockAssessmentStatusRepository().MockGetAllAssessmentStatus(MockAssessmentStatusList());
            this.mockAssessmentResponseRepository = new MockAssessmentResponseRepository().MockGetAssessmentResponseFOrDashboardCalculation(mockAssessmentResponselist);
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Success);
            this.InitialiseAssessmentService();
            var result = this.assessmentService.GetAssessmentResponseFOrDashboardCalculation(1, 1);
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.Success);
        }

        [Fact]
        public void GetAssessmentResponseFOrDashboardCalculation_Exception()
        {

            this.mockAssessmentResponseRepository = new MockAssessmentResponseRepository().MockGetAssessmentResponseFOrDashboardCalculationException();
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Failure);
            this.InitialiseAssessmentService();
            Assert.ThrowsAny<Exception>(() => this.assessmentService.GetAssessmentResponseFOrDashboardCalculation(1, 1));
        }
        #endregion GetAssessmentResponseFOrDashboardCalculation

        #region BatchUploadAssessments
        [Fact]
        public void BatchUploadAssessments_Success_ReturnsCorrectResult()
        {
            UploadAssessmentDTO uploadAssessmentDTO = MockUploadAssessmentDTO();
            List<PersonQuestionnaire> personQuestionnaireList = MockPersonQuestionnaire();
            List<Assessment> assessmentList = new List<Assessment> { MockAssessment() };
            IReadOnlyList<AssessmentResponse> mockAssessmentResponselist1 = MockAssessmentResponse12();

            var mockAssessmentResponselist = MockAssessmentResponseList1();
            this.mockAssessmentRepository = new MockAssessmentRepository().MockGetPersonQuestionnaireID(1, assessmentList);
            this.mockAssessmentStatusRepository = new MockAssessmentStatusRepository().MockGetAllAssessmentStatus(MockAssessmentStatusList());
            this.mockPersonQuestionnaireRepository = new MockPersonQuestionnaireRepository().MockGetAllpersonQuestionnaire(personQuestionnaireList);
            this.mockAssessmentResponseRepository = new MockAssessmentResponseRepository().MockAddBulkAssessmentResponse(mockAssessmentResponselist, mockAssessmentResponselist1);

            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.InsertionSuccess);
            this.InitialiseAssessmentService();
            var result = this.assessmentService.BatchUploadAssessments(uploadAssessmentDTO);
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.InsertionSuccess);
        }

        [Fact]
        public void BatchUploadAssessments_Exception()
        {
            this.mockAssessmentRepository = new MockAssessmentRepository().MockGetPersonQuestionnaireIDException();
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Failure);
            this.InitialiseAssessmentService();
            Assert.ThrowsAny<Exception>(() => this.assessmentService.BatchUploadAssessments(MockUploadAssessmentDTO()));
        }

        private UploadAssessmentDTO MockUploadAssessmentDTO()
        {
            AssessmentProgressDTO sd = new AssessmentProgressDTO();
            sd.PersonID = 1;
            sd.AssessmentStatusID = 1;
            sd.AssessmentGUID = Guid.NewGuid();
            sd.PersonQuestionnaireID = 1;
            return new UploadAssessmentDTO
            {
                AgencyID = 1,
                UpdateUserID = 1,
                QuestionnaireID = 1,
                AssessmentsToUpload = new List<AssessmentProgressDTO> { sd }
            };
        }

        private IReadOnlyList<AssessmentResponse> MockAssessmentResponse12()
        {
            return new List<AssessmentResponse>()
            {
                new AssessmentResponse()
                {
                     AssessmentResponseID = 1,
                    AssessmentID = 1,
                    PersonSupportID = null,
                    ResponseID = 2,
                    ItemResponseBehaviorID = 3,
                    IsRequiredConfidential = false,
                    IsPersonRequestedConfidential = null,
                    IsOtherConfidential = false,
                    IsRemoved = false,
                    UpdateDate = DateTime.UtcNow,
                    UpdateUserID = 2,
                    QuestionnaireItemID = 1,
                    IsCloned = false
                }
            };
        }

        private List<PersonQuestionnaire> MockPersonQuestionnaire()
        {
            return new List<PersonQuestionnaire>()
            {
                new PersonQuestionnaire()
                {
                    PersonID=1,
                    CollaborationID=1,
                    IsActive=true,
                    PersonQuestionnaireID=1,
                    StartDate= DateTime.UtcNow,
                    IsRemoved = false,
                    UpdateDate = DateTime.UtcNow,
                    UpdateUserID = 2,
                    QuestionnaireID=1,
                    EndDueDate=DateTime.UtcNow.AddDays(10)
                }
            };
        }

        private List<AssessmentStatus> MockAssessmentStatusList()
        {
            return new List<AssessmentStatus>()
            {
                new AssessmentStatus()
            {
                AssessmentStatusID = 1,
                Name = "Approved",
                Abbrev = null,
                Description = null,
                ListOrder = 1,
                IsRemoved = false,
                UpdateDate = DateTime.UtcNow,
                UpdateUserID = 2
            },
                    new AssessmentStatus()
            {
                AssessmentStatusID = 2,
                Name = "Submitted",
                Abbrev = null,
                Description = null,
                ListOrder = 1,
                IsRemoved = false,
                UpdateDate = DateTime.UtcNow,
                UpdateUserID = 2
            },
                        new AssessmentStatus()
            {
                AssessmentStatusID = 3,
                Name = "Returned",
                Abbrev = null,
                Description = null,
                ListOrder = 1,
                IsRemoved = false,
                UpdateDate = DateTime.UtcNow,
                UpdateUserID = 2
            },
            new AssessmentStatus()
            {
                AssessmentStatusID = 4,
                Name = "In Progress",
                Abbrev = null,
                Description = null,
                ListOrder = 1,
                IsRemoved = false,
                UpdateDate = DateTime.UtcNow,
                UpdateUserID = 2
            },
            new AssessmentStatus()
            {
                AssessmentStatusID = 5,
                Name = "Email Sent",
                Abbrev = null,
                Description = null,
                ListOrder = 1,
                IsRemoved = false,
                UpdateDate = DateTime.UtcNow,
                UpdateUserID = 2
            }
        };
        }

        #endregion BatchUploadAssessments
    }
}
