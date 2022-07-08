using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Interfaces;
using Opeeka.PICS.Domain.Interfaces.Common;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Domain.Resources;
using Opeeka.PICS.Domain.Services;
using Opeeka.PICS.Infrastructure.Enums;
using Opeeka.PICS.UnitTests.Domain.Mocks.Common;
using Opeeka.PICS.UnitTests.Domain.Mocks.Repositories;
using System;
using System.Collections.Generic;
using Xunit;

namespace Opeeka.PICS.UnitTests.Domain.Services
{
    public class QuestionnaireServiceUnitTest
    {
        /// Initializes a new instance of the mockQuestionnaireRepository/> class.
        private Mock<IQuestionnaireRepository> mockQuestionnaireRepository;
        private Mock<IQuestionnaireItemRepository> mockquestionnaireItemRepository;
        private Mock<IQuestionnaireWindowRepository> mockquestionnaireWindowRepository;
        private Mock<IQuestionnaireReminderRuleRespository> mockquestionnaireReminderRuleRespository;
        private Mock<IQuestionnaireNotifyRiskRuleRepository> mockquestionnaireNotifyRiskRuleRespository;
        private Mock<IQuestionnaireNotifyRiskRuleConditionRepository> mockquestionnaireNotifyRiskRuleConditionRepository;
        private Mock<IQuestionnaireNotifyRiskScheduleRepository> mockquestionnaireNotifyRiskScheduleRepository;
        private Mock<ILogger<QuestionnaireService>> mockLogger;


        private QuestionnaireService questionnaireService;
        private Mock<IConfigurationRepository> mockConfigRepository;
        private Mock<IHttpContextAccessor> httpContextAccessor;
        private Mock<IUtility> mockUtility;
        private readonly Mock<IPersonQuestionnaireRepository> mockPersonQuestionnaireRepository;
        private Mock<IQueue> mockQueue;
        private Mock<IQueryBuilder> mockQueryBuilder;
        private Mock<IPersonRepository> mockPersonRepository;
        private Mock<INotifyReminderRepository> notifyReminderRepository;
        private Mock<INotificationLogRepository> mocknotificationLogRepository;
        private Mock<INotificationTypeRepository> mocknotificationTypeRepository;
        private Mock<INotifiationResolutionStatusRepository> mocknotifiationResolutionStatusRepository;
        private Mock<IMapper> mapper;
        private Mock<IQuestionnaireNotifyRiskRuleRepository> mockquestionnaireNotifyRiskRuleRepository;
        private Mock<INotifyRiskRepository> mocknotifyRiskRepository;
        private Mock<INotifyRiskValueRepository> mocknotifyRiskValueRepository;

        private Mock<IQuestionnaireSkipLogicRuleRepository> questionnaireSkipLogicRuleRepository;
        private Mock<IQuestionnaireSkipLogicRuleConditionRepository> questionnaireSkipLogicRuleConditionRepository;
        private Mock<IQuestionnaireSkipLogicRuleActionRepository> questionnaireSkipLogicRuleActionRepository;

        private Mock<IQuestionnaireDefaultResponseRuleRepository> questionnaireDefaultResponseRuleRepository;
        private Mock<IQuestionnaireDefaultResponseRuleConditionRepository> questionnaireDefaultResponseRuleConditionRepository;
        private Mock<IQuestionnaireDefaultResponseRuleActionRepository> questionnaireDefaultResponseRuleActionRepository;
        private Mock<IQuestionnaireRegularReminderRecurrenceRepository> questionnaireRegularReminderRecurrenceRepository;
        private Mock<IQuestionnaireRegularReminderTimeRuleRepository> questionnaireRegularReminderTimeRuleRepository;
        private Mock<ILookupRepository> lookupRepository;

        public QuestionnaireServiceUnitTest()
        {
            this.mockQuestionnaireRepository = new Mock<IQuestionnaireRepository>();
            this.mockquestionnaireItemRepository = new Mock<IQuestionnaireItemRepository>();
            this.mockquestionnaireNotifyRiskScheduleRepository = new Mock<IQuestionnaireNotifyRiskScheduleRepository>();
            this.mockquestionnaireReminderRuleRespository = new Mock<IQuestionnaireReminderRuleRespository>();
            this.mockquestionnaireNotifyRiskRuleRespository = new Mock<IQuestionnaireNotifyRiskRuleRepository>();
            this.mockquestionnaireNotifyRiskRuleConditionRepository = new Mock<IQuestionnaireNotifyRiskRuleConditionRepository>();
            this.mockquestionnaireWindowRepository = new Mock<IQuestionnaireWindowRepository>();
            this.mockLogger = new Mock<ILogger<QuestionnaireService>>();
            this.mockConfigRepository = new Mock<IConfigurationRepository>();
            this.httpContextAccessor = new Mock<IHttpContextAccessor>();
            this.mockUtility = new MockUtility().SetupUtility(DateTime.UtcNow);
            this.mockPersonQuestionnaireRepository = new Mock<IPersonQuestionnaireRepository>();
            this.mockQueue = new Mock<IQueue>();
            this.mockQueryBuilder = new MockQueryBuilder().SetupQueryBuilder(new DynamicQueryBuilderDTO());
            this.mockPersonRepository = new Mock<IPersonRepository>();
            this.notifyReminderRepository = new Mock<INotifyReminderRepository>();
            this.mocknotificationLogRepository = new Mock<INotificationLogRepository>();
            this.mocknotificationTypeRepository = new Mock<INotificationTypeRepository>();
            this.mocknotifiationResolutionStatusRepository = new Mock<INotifiationResolutionStatusRepository>();

            this.mapper = new Mock<IMapper>();
            this.mockquestionnaireNotifyRiskRuleRepository = new Mock<IQuestionnaireNotifyRiskRuleRepository>();
            this.mocknotifyRiskRepository = new Mock<INotifyRiskRepository>();
            this.mocknotifyRiskValueRepository = new Mock<INotifyRiskValueRepository>();
            this.questionnaireSkipLogicRuleRepository = new Mock<IQuestionnaireSkipLogicRuleRepository>();
            this.questionnaireSkipLogicRuleConditionRepository = new Mock<IQuestionnaireSkipLogicRuleConditionRepository>();
            this.questionnaireSkipLogicRuleActionRepository = new Mock<IQuestionnaireSkipLogicRuleActionRepository>();

            this.questionnaireDefaultResponseRuleRepository = new Mock<IQuestionnaireDefaultResponseRuleRepository>();
            this.questionnaireDefaultResponseRuleConditionRepository = new Mock<IQuestionnaireDefaultResponseRuleConditionRepository>();
            this.questionnaireDefaultResponseRuleActionRepository = new Mock<IQuestionnaireDefaultResponseRuleActionRepository>();
            this.questionnaireRegularReminderRecurrenceRepository = new Mock<IQuestionnaireRegularReminderRecurrenceRepository>();
            this.questionnaireRegularReminderTimeRuleRepository = new Mock<IQuestionnaireRegularReminderTimeRuleRepository>();
            this.lookupRepository = new Mock<ILookupRepository>();
        }

        [Fact]
        public void GetPersonQuestionnaireList_Success_ReturnsCorrectResult()
        {

            var mockPersonQuestionnaire = GetMockPersonQuestionnaire();
            this.mockPersonRepository = new MockPersonRepository().MockGetPersonIDS(new PeopleDTO(), new SharedDetailsDTO() { PersonID = 0, SharedCollaborationIDs = "test", SharedQuestionnaireIDs = "test" });
            this.mockQuestionnaireRepository = new MockQuestionnaireRepository().MockPersonQuestionnaireList(mockPersonQuestionnaire);
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Success);
            this.initialiseService(localize);
            Guid personIndex = new Guid("CA67BF4B-7A9E-4227-8A45-069868379FA7");
            int pageNumber = 1;
            int pageSize = 10;
            UserTokenDetails userTokenDetails = new UserTokenDetails() { AgencyID = 0, UserID = 0 };
            var result = this.questionnaireService.GetPersonQuestionnaireList(personIndex, 0, pageNumber, pageSize, userTokenDetails);
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.Success);
        }

        private void initialiseService(Mock<LocalizeService> localize)
        {
            this.questionnaireService = new QuestionnaireService(
            this.questionnaireDefaultResponseRuleActionRepository.Object, this.questionnaireDefaultResponseRuleConditionRepository.Object,
            this.questionnaireDefaultResponseRuleRepository.Object, this.questionnaireSkipLogicRuleConditionRepository.Object,
            this.questionnaireSkipLogicRuleActionRepository.Object,
            this.questionnaireSkipLogicRuleRepository.Object, this.mocknotifyRiskValueRepository.Object, this.mocknotifyRiskRepository.Object, this.mapper.Object, this.mockquestionnaireNotifyRiskRuleRepository.Object, this.mocknotifiationResolutionStatusRepository.Object, this.mocknotificationTypeRepository.Object, this.mocknotificationLogRepository.Object, this.notifyReminderRepository.Object, this.mockPersonRepository.Object, this.mockquestionnaireNotifyRiskRuleConditionRepository.Object, this.mockquestionnaireNotifyRiskRuleRespository.Object,
                this.mockquestionnaireReminderRuleRespository.Object, this.mockquestionnaireWindowRepository.Object, this.mockquestionnaireNotifyRiskScheduleRepository.Object
                , this.mockquestionnaireItemRepository.Object, this.mockQuestionnaireRepository.Object, this.mockLogger.Object,
                localize.Object, mockConfigRepository.Object, httpContextAccessor.Object, this.mockUtility.Object, this.mockPersonQuestionnaireRepository.Object, this.mockQueue.Object, this.mockQueryBuilder.Object, this.questionnaireRegularReminderRecurrenceRepository.Object, this.questionnaireRegularReminderTimeRuleRepository.Object, this.lookupRepository.Object);
        }

        [Fact]
        public void GetPersonQuestionnaireList_Success_ReturnsNoResult()
        {
            var mockPersonQuestionnaire = new List<PersonQuestionnaireListDTO>();
            this.mockPersonRepository = new MockPersonRepository().MockGetPersonIDS(new PeopleDTO(), new SharedDetailsDTO() { PersonID = 0, SharedCollaborationIDs = "test", SharedQuestionnaireIDs = "test" });
            this.mockQuestionnaireRepository = new MockQuestionnaireRepository().MockPersonQuestionnaireList(mockPersonQuestionnaire);
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Success);
            this.initialiseService(localize);
            Guid personIndex = new Guid("CA67BF4B-7A9E-4227-8A45-069868379FA7");
            int pageNumber = 1;
            int pageSize = 10;
            UserTokenDetails userTokenDetails = new UserTokenDetails() { AgencyID = 0, UserID = 0 };
            var result = this.questionnaireService.GetPersonQuestionnaireList(personIndex, 0, pageNumber, pageSize, userTokenDetails);
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.Success);
        }

        [Fact]
        public void GetPersonQuestionnaireList_Failure_InvalidParameterResult_IfPageNumberIsZero()
        {
            var mockPersonQuestionnaire = new List<PersonQuestionnaireListDTO>();
            this.mockQuestionnaireRepository = new MockQuestionnaireRepository().MockPersonQuestionnaireList(mockPersonQuestionnaire);
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Success);
            this.initialiseService(localize);
            Guid personIndex = new Guid("CA67BF4B-7A9E-4227-8A45-069868379FA7");
            int pageNumber = 0;
            int pageSize = 10;
            UserTokenDetails userTokenDetails = new UserTokenDetails() { UserID = 0, AgencyID = 0 };
            var result = this.questionnaireService.GetPersonQuestionnaireList(personIndex, 0, pageNumber, pageSize, userTokenDetails);
            Assert.Null(result.PersonQuestionnaireListDTO);
        }

        [Fact]
        public void GetPersonQuestionnaireList_Failure_InvalidParameterResult_IfPageSizeIsZero()
        {
            var mockPersonQuestionnaire = new List<PersonQuestionnaireListDTO>();
            this.mockQuestionnaireRepository = new MockQuestionnaireRepository().MockPersonQuestionnaireList(mockPersonQuestionnaire);
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Success);
            this.initialiseService(localize);
            Guid personIndex = new Guid("CA67BF4B-7A9E-4227-8A45-069868379FA7");
            int pageNumber = 1;
            int pageSize = 0;
            UserTokenDetails userTokenDetails = new UserTokenDetails() { UserID = 0, AgencyID = 0 };
            var result = this.questionnaireService.GetPersonQuestionnaireList(personIndex, 0, pageNumber, pageSize, userTokenDetails);
            Assert.Null(result.PersonQuestionnaireListDTO);
        }

        [Fact]
        public void GetPersonQuestionnaireList_ExceptionResult()
        {
            List<PersonQuestionnaireListDTO> mockPersonQuestionnaire = new List<PersonQuestionnaireListDTO>();
            this.mockQuestionnaireRepository = new MockQuestionnaireRepository().MockPersonQuestionnaireListException(mockPersonQuestionnaire);
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Success);
            this.initialiseService(localize);
            Guid personIndex = new Guid("CA67BF4B-7A9E-4227-8A45-069868379FA7");
            int pageNumber = 1;
            int pageSize = 10;
            UserTokenDetails userTokenDetails = new UserTokenDetails() { UserID = 0, AgencyID = 0 };
            Assert.ThrowsAny<Exception>(() => this.questionnaireService.GetPersonQuestionnaireList(personIndex, 0, pageNumber, pageSize, userTokenDetails));
        }

        private List<PersonQuestionnaireListDTO> GetMockPersonQuestionnaire()
        {
            return new List<PersonQuestionnaireListDTO>()
            {
                new PersonQuestionnaireListDTO()
                {
                    QuestionnaireID = 1,
                    InstrumentID = 1,
                    AgencyID = 1,
                    QuestionnaireName = "Questionnaire 1",
                    QuestionnaireAbbrev = "Q1",
                    NotificationScheduleName = "Self Harm Only",
                    ReminderScheduleName = "Reminder Name",
                    IsBaseQuestionnaire = false,
                    InstrumentName = "Adult Needs and Strengths Assessment",
                    InstrumentAbbrev = "ANSA",
                    TotalCount = 0,
                    Status = null
                },

                new PersonQuestionnaireListDTO()
                {
                    QuestionnaireID = 1,
                    InstrumentID = 1,
                    AgencyID = 1,
                    QuestionnaireName = "Questionnaire 1",
                    QuestionnaireAbbrev = "Q1",
                    NotificationScheduleName = "Self Harm Only",
                    ReminderScheduleName = "Reminder Name",
                    IsBaseQuestionnaire = false,
                    InstrumentName = "Adult Needs and Strengths Assessment",
                    InstrumentAbbrev = "ANSA",
                    TotalCount = 0,
                    Status = null
                }
            };
        }

        #region CloneQuestionnaires

        private QuestionnairesDTO MockQuestionnaireDTO()
        {
            return new QuestionnairesDTO()
            {
                QuestionnaireID = 1,
                InstrumentID = 1,
                AgencyID = 1,
                Name = "Questionnaire 1",
                Description = "The California CANS Core 50 for Child Welfare and Behavioral Health plus 12 Trauma items.",
                Abbrev = "Q1",
                ReminderScheduleName = "Reminder Name",
                RequiredConfidentialityLanguage = "Signed Release Required",
                PersonRequestedConfidentialityLanguage = "Person Requested Confidential",
                OtherConfidentialityLanguage = "Person Reaction Sensitive",
                IsPubllished = true,
                ParentQuestionnaireID = 0,
                IsBaseQuestionnaire = false,
                StartDate = Convert.ToDateTime("2020 -01-01 00:00:00.000"),
                EndDate = null,
                IsRemoved = false,
                UpdateUserID = 2,
                UpdateDate = DateTime.UtcNow
            };
        }

        private List<QuestionnaireItemsDTO> MockQuestionnaireItemListDTO()
        {
            return new List<QuestionnaireItemsDTO>()
            {
                new QuestionnaireItemsDTO()
                {
                    QuestionnaireItemID = 1,
                    QuestionnaireItemIndex = Guid.NewGuid(),
                    QuestionnaireID = 1,
                    CategoryID = 1,
                    ItemID = 1,
                    IsOptional = false,
                    CanOverrideLowerResponseBehavior = true,
                    CanOverrideMedianResponseBehavior = true,
                    CanOverrideUpperResponseBehavior = true,
                    LowerItemResponseBehaviorID = 3,
                    MedianItemResponseBehaviorID = 2,
                    UpperItemResponseBehaviorID = 1,
                    IsActive = true,
                    IsRemoved = false,
                    UpdateDate = DateTime.UtcNow,
                    UpdateUserID = 2,
                    LowerResponseValue = 1,
                    UpperResponseValue = 2,
                    StartDate = null,
                    EndDate = null,
                    ClonedQuestionnaireItemId = 1
                }
            };
        }
        private QuestionnaireItemsDTO MockQuestionnaireItemDTO()
        {
            return new QuestionnaireItemsDTO()
            {
                QuestionnaireItemID = 1,
                QuestionnaireItemIndex = Guid.NewGuid(),
                QuestionnaireID = 1,
                CategoryID = 1,
                ItemID = 1,
                IsOptional = false,
                CanOverrideLowerResponseBehavior = true,
                CanOverrideMedianResponseBehavior = true,
                CanOverrideUpperResponseBehavior = true,
                LowerItemResponseBehaviorID = 3,
                MedianItemResponseBehaviorID = 2,
                UpperItemResponseBehaviorID = 1,
                IsActive = true,
                IsRemoved = false,
                UpdateDate = DateTime.UtcNow,
                UpdateUserID = 2,
                LowerResponseValue = 1,
                UpperResponseValue = 2,
                StartDate = null,
                EndDate = null
            };
        }

        private List<QuestionnaireReminderRulesDTO> MockQuestionnaireReminderRuleListDTO()
        {
            return new List<QuestionnaireReminderRulesDTO>()
            {
                new QuestionnaireReminderRulesDTO()
                {
                    QuestionnaireReminderRuleID = 1,
                    QuestionnaireID = 1,
                    QuestionnaireReminderTypeID = 1,
                    ReminderOffsetDays = 1,
                    CanRepeat = false,
                    RepeatInterval = null,
                    IsSelected = true
                }
            };
        }
        private QuestionnaireReminderRulesDTO MockQuestionnaireReminderRuleDTO()
        {
            return new QuestionnaireReminderRulesDTO()
            {
                QuestionnaireReminderRuleID = 1,
                QuestionnaireID = 1,
                QuestionnaireReminderTypeID = 1,
                ReminderOffsetDays = 1,
                CanRepeat = false,
                RepeatInterval = null,
                IsSelected = true
            };
        }

        private List<QuestionnaireWindowsDTO> MockQuestionnaireWindowListDTO()
        {
            return new List<QuestionnaireWindowsDTO>()
            {
                new QuestionnaireWindowsDTO()
                {
                    QuestionnaireWindowID = 1,
                    QuestionnaireID = 1,
                    AssessmentReasonID = 1,
                    DueDateOffsetDays = 0,
                    WindowOpenOffsetDays = 30,
                    WindowCloseOffsetDays = 30,
                    CanRepeat = false,
                    RepeatIntervalDays = null,
                    IsSelected = true
                }
            };
        }
        private QuestionnaireWindowsDTO MockQuestionnaireWindowDTO()
        {
            return new QuestionnaireWindowsDTO()
            {
                QuestionnaireWindowID = 1,
                QuestionnaireID = 1,
                AssessmentReasonID = 1,
                DueDateOffsetDays = 0,
                WindowOpenOffsetDays = 30,
                WindowCloseOffsetDays = 30,
                CanRepeat = false,
                RepeatIntervalDays = null,
                IsSelected = true
            };
        }

        private QuestionnaireNotifyRiskSchedulesDTO MockQuestionnaireNotifyRiskScheduleDTO()
        {
            return new QuestionnaireNotifyRiskSchedulesDTO()
            {
                QuestionnaireNotifyRiskScheduleID = 1,
                Name = "Self Harm Only",
                QuestionnaireID = 1,
                IsRemoved = false,
                UpdateDate = DateTime.UtcNow,
                UpdateUserID = 2
            };
        }

        private List<QuestionnaireNotifyRiskRulesDTO> MockQuestionnaireNotifyRiskRuleListDTO()
        {
            return new List<QuestionnaireNotifyRiskRulesDTO>()
            {
                new QuestionnaireNotifyRiskRulesDTO()
                {
                    QuestionnaireNotifyRiskRuleID = 1,
                    Name = "Self Harm Only 1",
                    QuestionnaireNotifyRiskScheduleID = 1,
                    NotificationLevelID = 1,
                    IsRemoved = false,
                    UpdateDate = DateTime.UtcNow,
                    UpdateUserID = 2,
                    ClonedQuestionnaireNotifyRiskRuleID = 1
                }
            };
        }
        private QuestionnaireNotifyRiskRulesDTO MockQuestionnaireNotifyRiskRuleDTO()
        {
            return new QuestionnaireNotifyRiskRulesDTO()
            {
                QuestionnaireNotifyRiskRuleID = 1,
                Name = "Self Harm Only 1",
                QuestionnaireNotifyRiskScheduleID = 1,
                NotificationLevelID = 1,
                IsRemoved = false,
                UpdateDate = DateTime.UtcNow,
                UpdateUserID = 2
            };
        }

        private List<QuestionnaireNotifyRiskRuleConditionsDTO> MockQuestionnaireNotifyRiskRuleConditionListDTO()
        {
            return new List<QuestionnaireNotifyRiskRuleConditionsDTO>()
            {
                new QuestionnaireNotifyRiskRuleConditionsDTO()
                {
                    QuestionnaireNotifyRiskRuleConditionID = 1,
                    QuestionnaireItemId = 1,
                    ComparisonOperator = ">=",
                    ComparisonValue = 1,
                    QuestionnaireNotifyRiskRuleID = 1,
                    ListOrder = 1,
                    JoiningOperator = "AND",
                    IsRemoved = false,
                    UpdateDate = DateTime.UtcNow,
                    UpdateUserID = 2
                }
            };
        }
        private QuestionnaireNotifyRiskRuleConditionsDTO MockQuestionnaireNotifyRiskRuleConditionDTO()
        {
            return new QuestionnaireNotifyRiskRuleConditionsDTO()
            {
                QuestionnaireNotifyRiskRuleConditionID = 1,
                QuestionnaireItemId = 1,
                ComparisonOperator = ">=",
                ComparisonValue = 1,
                QuestionnaireNotifyRiskRuleID = 1,
                ListOrder = 1,
                JoiningOperator = "AND",
                IsRemoved = false,
                UpdateDate = DateTime.UtcNow,
                UpdateUserID = 2
            };
        }

        [Fact]
        public void CloneQuestionnaire_Success_ReturnsCorrectResult()
        {
            var mockQuestionnaire = MockQuestionnaireDTO();
            var mockQuestionnaireItemList = MockQuestionnaireItemListDTO();
            var mockQuestionnaireItem = MockQuestionnaireItemDTO();
            var mockQuestionnaireReminderRuleList = MockQuestionnaireReminderRuleListDTO();
            var mockQuestionnaireReminderRule = MockQuestionnaireReminderRuleDTO();
            var mockQuestionnaireWindowList = MockQuestionnaireWindowListDTO();
            var mockQuestionnaireWindow = MockQuestionnaireWindowDTO();
            var mockQuestionnaireNotifyRiskSchedule = MockQuestionnaireNotifyRiskScheduleDTO();
            var mockQuestionnaireNotifyRiskRuleList = MockQuestionnaireNotifyRiskRuleListDTO();
            var mockQuestionnaireNotifyRiskRule = MockQuestionnaireNotifyRiskRuleDTO();
            var mockQuestionnaireNotifyRiskRuleConditionList = MockQuestionnaireNotifyRiskRuleConditionListDTO();
            var mockQuestionnaireNotifyRiskRuleCondition = MockQuestionnaireNotifyRiskRuleConditionDTO();

            this.mockQuestionnaireRepository = new MockQuestionnaireRepository().MockCloneQuestionnaire(mockQuestionnaire);
            this.mockquestionnaireItemRepository = new MockQuestionnaireItemRepository().MockCloneQuestionnaire(mockQuestionnaireItem, mockQuestionnaireItemList);
            this.mockquestionnaireReminderRuleRespository = new MockQuestionnaireReminderRuleRepository().MockCloneQuestionnaire(mockQuestionnaireReminderRule, mockQuestionnaireReminderRuleList);
            this.mockquestionnaireWindowRepository = new MockQuestionnaireWindowRepository().MockCloneQuestionnaire(mockQuestionnaireWindow, mockQuestionnaireWindowList);
            this.mockquestionnaireNotifyRiskScheduleRepository = new MockQuestionnaireNotifyRiskScheduleRepository().MockCloneQuestionnaire(mockQuestionnaireNotifyRiskSchedule);
            this.mockquestionnaireNotifyRiskRuleRespository = new MockQuestionnaireNotifyRiskRuleRepository().MockCloneQuestionnaire(mockQuestionnaireNotifyRiskRule, mockQuestionnaireNotifyRiskRuleList);
            this.mockquestionnaireNotifyRiskRuleConditionRepository = new MockQuestionnaireNotifyRiskRuleConditionRepository().MockCloneQuestionnaire(mockQuestionnaireNotifyRiskRuleCondition, mockQuestionnaireNotifyRiskRuleConditionList);

            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.CloneSuccess);
            this.initialiseService(localize);
            int questionnaireID = 1;
            long agencyID = 1;
            int updateUserID = 2;
            var result = this.questionnaireService.CloneQuestionnaire(questionnaireID, agencyID, updateUserID);
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.CloneSuccess);
        }

        [Fact]
        public void CloneQuestionnaire_Failure()
        {
            var mockQuestionnaire = new QuestionnairesDTO();

            this.mockQuestionnaireRepository = new MockQuestionnaireRepository().MockCloneQuestionnaire(mockQuestionnaire);

            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.CloneSuccess);
            this.initialiseService(localize);
            int questionnaireID = 1;
            long agencyID = 1;
            int updateUserID = 2;
            var result = this.questionnaireService.CloneQuestionnaire(questionnaireID, agencyID, updateUserID);
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.CloneSuccess);
        }

        [Fact]
        public void CloneQuestionnaire_ExceptionResult()
        {
            var mockQuestionnaire = new QuestionnairesDTO();

            this.mockQuestionnaireRepository = new MockQuestionnaireRepository().MockCloneQuestionnaireException(mockQuestionnaire);

            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Success);
            this.initialiseService(localize);
            int questionnaireID = 1;
            long agencyID = 1;
            int updateUserID = 2;
            Assert.ThrowsAny<Exception>(() => this.questionnaireService.CloneQuestionnaire(questionnaireID, agencyID, updateUserID));
        }

        #endregion CloneQuestionnaires

        #region Delete Questionnaire 
        [Fact]
        public void DeleteQuestionnaire_Success_ReturnsCorrectResult()
        {
            var mockQuestionnaire = MockQuestionnaireDTO();
            var QuestionnaireID = 1;
            this.mockQuestionnaireRepository = new MockQuestionnaireRepository().MockdeleteQuestionnaire(mockQuestionnaire);
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.DeletionSuccess);
            this.initialiseService(localize);
            var result = this.questionnaireService.DeleteQuestionnaire(QuestionnaireID, 1);
            Assert.Equal(result.ResponseStatus, PCISEnum.StatusMessages.DeletionSuccess);
        }

        [Fact]
        public void DeleteQuestionnaire_Failure_ExceptionResult()
        {
            var QuestionnaireID = 1;
            this.mockQuestionnaireRepository = new MockQuestionnaireRepository().MockDeleteQuestionnaireException();
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Success);
            this.initialiseService(localize);
            Assert.ThrowsAny<Exception>(() => this.questionnaireService.DeleteQuestionnaire(QuestionnaireID, 1));
        }
        #endregion Delete Questionnaire 
    }
}
