using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.DTO.Input;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Domain.Services;
using Opeeka.PICS.Infrastructure.Enums;
using Opeeka.PICS.UnitTests.Domain.Mocks.Repositories;
using Xunit;

namespace Opeeka.PICS.UnitTests.Domain.Services
{
    public class OptionsServiceUnitTest
    {
        /// Initializes a new instance of the Collaboration Level Repository/> class.
        private Mock<ICollaborationLevelRepository> mockCollaborationLevelRepository;
        /// Initializes a new instance of the Collaboration Repository/> class.
        private Mock<ICollaborationRepository> mockCollaborationRepository;
        private Mock<ICollaborationTagTypeRepository> mockCollaborationTagTypeRepository;
        private Mock<ICollaborationTagRepository> mockCollaborationTagRepository;
        private Mock<ITherapyTypeRepository> mockTherapyTypeRepository;
        private Mock<IHelperTitleRepository> mockHelperTitleRepository;
        private Mock<IHelperRepository> mockHelperRepository;
        private Mock<IOptionsRepository> mockOptionsRepository;

        private Mock<INotificationLevelRepository> mockNotificationLevelRepository;
        private Mock<IGenderRepository> mockGenderRepository;
        private Mock<IIdentifiedGenderRepository> mockIdentifiedGenderRepository;
        private Mock<IIdentificationTypeRepository> mockIdentificationTypeRepository;
        private Mock<IRaceEthnicityRepository> mockRaceEthnicityRepository;
        private Mock<ISupportTypeRepository> mockSupportTypeRepository;
        private Mock<INotificationTypeRepository> mockNotificationTypeRepository;

        /// Initializes a new instance of the <see cref="logger"/> class.
        private Mock<ILogger<OptionsService>> mockLogger;
        /// Initializes a new instance of the mock_SexualityRepository"/> class.
        private Mock<ISexualityRepository> mock_SexualityRepository;
        private Mock<IMapper> mockMapper;
        private Mock<ISexualityRepository> mockSexualityRepository;

        private OptionsService optionsService;
        private Mock<IConfigurationRepository> mockConfigRepository;
        private Mock<IHttpContextAccessor> httpContextAccessor;
        public OptionsServiceUnitTest()
        {
            this.mockCollaborationLevelRepository = new Mock<ICollaborationLevelRepository>();
            this.mockCollaborationRepository = new Mock<ICollaborationRepository>();
            this.mockCollaborationTagRepository = new Mock<ICollaborationTagRepository>();
            this.mockCollaborationTagTypeRepository = new Mock<ICollaborationTagTypeRepository>();
            this.mockTherapyTypeRepository = new Mock<ITherapyTypeRepository>();
            this.mockHelperRepository = new Mock<IHelperRepository>();
            this.mockHelperTitleRepository = new Mock<IHelperTitleRepository>();
            this.mockNotificationLevelRepository = new Mock<INotificationLevelRepository>();
            this.mockGenderRepository = new Mock<IGenderRepository>();
            this.mockIdentificationTypeRepository = new Mock<IIdentificationTypeRepository>();
            this.mockRaceEthnicityRepository = new Mock<IRaceEthnicityRepository>();
            this.mockSupportTypeRepository = new Mock<ISupportTypeRepository>();
            this.mockNotificationTypeRepository = new Mock<INotificationTypeRepository>();
            this.mockLogger = new Mock<ILogger<OptionsService>>();
            this.mockMapper = new Mock<IMapper>();
            this.mockConfigRepository = new Mock<IConfigurationRepository>();
            this.httpContextAccessor = new Mock<IHttpContextAccessor>();
            this.mock_SexualityRepository = new Mock<ISexualityRepository>();
            this.mockIdentifiedGenderRepository = new Mock<IIdentifiedGenderRepository>();
            this.mockOptionsRepository = new Mock<IOptionsRepository>();
        }

        private void InitialiseUserService_InsertionSuccess()
        {
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.InsertionSuccess);
            this.optionsService = new OptionsService(this.mockMapper.Object, this.mockCollaborationLevelRepository.Object, this.mockCollaborationRepository.Object, this.mockCollaborationTagTypeRepository.Object, this.mockCollaborationTagRepository.Object, this.mockTherapyTypeRepository.Object, this.mockHelperTitleRepository.Object, this.mockHelperRepository.Object, this.mockNotificationLevelRepository.Object, this.mockGenderRepository.Object, this.mockIdentificationTypeRepository.Object, this.mockRaceEthnicityRepository.Object, this.mockSupportTypeRepository.Object,
                localize.Object, mockConfigRepository.Object, httpContextAccessor.Object, mock_SexualityRepository.Object, this.mockNotificationTypeRepository.Object, this.mockIdentifiedGenderRepository.Object, this.mockOptionsRepository.Object);
        }
        private void InitialiseUserService_Success()
        {
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Success);
            this.optionsService = new OptionsService(this.mockMapper.Object, this.mockCollaborationLevelRepository.Object, this.mockCollaborationRepository.Object, this.mockCollaborationTagTypeRepository.Object, this.mockCollaborationTagRepository.Object, this.mockTherapyTypeRepository.Object, this.mockHelperTitleRepository.Object, this.mockHelperRepository.Object, this.mockNotificationLevelRepository.Object, this.mockGenderRepository.Object, this.mockIdentificationTypeRepository.Object, this.mockRaceEthnicityRepository.Object, this.mockSupportTypeRepository.Object,
                localize.Object, mockConfigRepository.Object, httpContextAccessor.Object, mock_SexualityRepository.Object, this.mockNotificationTypeRepository.Object, this.mockIdentifiedGenderRepository.Object, this.mockOptionsRepository.Object);
        }
        private void InitialiseUserService_Failed()
        {
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Failure);
            this.optionsService = new OptionsService(this.mockMapper.Object, this.mockCollaborationLevelRepository.Object, this.mockCollaborationRepository.Object, this.mockCollaborationTagTypeRepository.Object, this.mockCollaborationTagRepository.Object, this.mockTherapyTypeRepository.Object, this.mockHelperTitleRepository.Object, this.mockHelperRepository.Object, this.mockNotificationLevelRepository.Object, this.mockGenderRepository.Object, this.mockIdentificationTypeRepository.Object, this.mockRaceEthnicityRepository.Object, this.mockSupportTypeRepository.Object,
                localize.Object, mockConfigRepository.Object, httpContextAccessor.Object, mock_SexualityRepository.Object, this.mockNotificationTypeRepository.Object, this.mockIdentifiedGenderRepository.Object, this.mockOptionsRepository.Object);
        }

        private void InitialiseUserService_DeletionSuccess()
        {
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.DeletionSuccess);
            this.optionsService = new OptionsService(this.mockMapper.Object, this.mockCollaborationLevelRepository.Object, this.mockCollaborationRepository.Object, this.mockCollaborationTagTypeRepository.Object, this.mockCollaborationTagRepository.Object, this.mockTherapyTypeRepository.Object, this.mockHelperTitleRepository.Object, this.mockHelperRepository.Object, this.mockNotificationLevelRepository.Object, this.mockGenderRepository.Object, this.mockIdentificationTypeRepository.Object, this.mockRaceEthnicityRepository.Object, this.mockSupportTypeRepository.Object,
                localize.Object, mockConfigRepository.Object, httpContextAccessor.Object, mock_SexualityRepository.Object, this.mockNotificationTypeRepository.Object, this.mockIdentifiedGenderRepository.Object, this.mockOptionsRepository.Object);
        }
        private void InitialiseUserService_DeletionFailed()
        {
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.DeletionFailed);
            this.optionsService = new OptionsService(this.mockMapper.Object, this.mockCollaborationLevelRepository.Object, this.mockCollaborationRepository.Object, this.mockCollaborationTagTypeRepository.Object, this.mockCollaborationTagRepository.Object, this.mockTherapyTypeRepository.Object, this.mockHelperTitleRepository.Object, this.mockHelperRepository.Object, this.mockNotificationLevelRepository.Object, this.mockGenderRepository.Object, this.mockIdentificationTypeRepository.Object, this.mockRaceEthnicityRepository.Object, this.mockSupportTypeRepository.Object,
                localize.Object, mockConfigRepository.Object, httpContextAccessor.Object, mock_SexualityRepository.Object, this.mockNotificationTypeRepository.Object, this.mockIdentifiedGenderRepository.Object, this.mockOptionsRepository.Object);
        }
        private void InitialiseUserService_DeleteRecordInUse()
        {
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.DeleteRecordInUse);
            this.optionsService = new OptionsService(this.mockMapper.Object, this.mockCollaborationLevelRepository.Object, this.mockCollaborationRepository.Object, this.mockCollaborationTagTypeRepository.Object, this.mockCollaborationTagRepository.Object, this.mockTherapyTypeRepository.Object, this.mockHelperTitleRepository.Object, this.mockHelperRepository.Object, this.mockNotificationLevelRepository.Object, this.mockGenderRepository.Object, this.mockIdentificationTypeRepository.Object, this.mockRaceEthnicityRepository.Object, this.mockSupportTypeRepository.Object,
                localize.Object, mockConfigRepository.Object, httpContextAccessor.Object, mock_SexualityRepository.Object, this.mockNotificationTypeRepository.Object, this.mockIdentifiedGenderRepository.Object, this.mockOptionsRepository.Object);
        }
        private void InitialiseUserService_InsertionFailed()
        {
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.insertionFailed);
            this.optionsService = new OptionsService(this.mockMapper.Object, this.mockCollaborationLevelRepository.Object, this.mockCollaborationRepository.Object, this.mockCollaborationTagTypeRepository.Object, this.mockCollaborationTagRepository.Object, this.mockTherapyTypeRepository.Object, this.mockHelperTitleRepository.Object, this.mockHelperRepository.Object, this.mockNotificationLevelRepository.Object, this.mockGenderRepository.Object, this.mockIdentificationTypeRepository.Object, this.mockRaceEthnicityRepository.Object, this.mockSupportTypeRepository.Object,
                localize.Object, mockConfigRepository.Object, httpContextAccessor.Object, mock_SexualityRepository.Object, this.mockNotificationTypeRepository.Object, this.mockIdentifiedGenderRepository.Object, this.mockOptionsRepository.Object);
        }
        private void InitialiseUserService_UpdationSuccess()
        {
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.UpdationSuccess);
            this.optionsService = new OptionsService(this.mockMapper.Object, this.mockCollaborationLevelRepository.Object, this.mockCollaborationRepository.Object, this.mockCollaborationTagTypeRepository.Object, this.mockCollaborationTagRepository.Object, this.mockTherapyTypeRepository.Object, this.mockHelperTitleRepository.Object, this.mockHelperRepository.Object, this.mockNotificationLevelRepository.Object, this.mockGenderRepository.Object, this.mockIdentificationTypeRepository.Object, this.mockRaceEthnicityRepository.Object, this.mockSupportTypeRepository.Object,
                localize.Object, mockConfigRepository.Object, httpContextAccessor.Object, mock_SexualityRepository.Object, this.mockNotificationTypeRepository.Object, this.mockIdentifiedGenderRepository.Object, this.mockOptionsRepository.Object);
        }
        private void InitialiseUserService_UpdationFailed()
        {
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.UpdationFailed);
            this.optionsService = new OptionsService(this.mockMapper.Object, this.mockCollaborationLevelRepository.Object, this.mockCollaborationRepository.Object, this.mockCollaborationTagTypeRepository.Object, this.mockCollaborationTagRepository.Object, this.mockTherapyTypeRepository.Object, this.mockHelperTitleRepository.Object, this.mockHelperRepository.Object, this.mockNotificationLevelRepository.Object, this.mockGenderRepository.Object, this.mockIdentificationTypeRepository.Object, this.mockRaceEthnicityRepository.Object, this.mockSupportTypeRepository.Object,
                localize.Object, mockConfigRepository.Object, httpContextAccessor.Object, mock_SexualityRepository.Object, this.mockNotificationTypeRepository.Object, this.mockIdentifiedGenderRepository.Object, this.mockOptionsRepository.Object);
        }

        #region Get Collaboration Level
        [Fact]
        public void GetCollaborationLevelList_Success_ReturnsCorrectResult()
        {

            var mockCollaborationLevel = GetMockCollaborationLevelList();
            this.mockCollaborationLevelRepository = new MockCollaborationLevelRepository().MockCollaborationLevelList(mockCollaborationLevel);
            InitialiseUserService_Success();
            int pageNumber = 1;
            int pageSize = 10;
            long agencyID = 1;
            var result = this.optionsService.GetCollaborationLevelList(pageNumber, pageSize, agencyID);
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.Success);
        }

        [Fact]
        public void GetCollaborationLevelList_Success_ReturnsNoResult()
        {
            var mockCollaborationLevels = new List<CollaborationLevelDTO>();
            this.mockCollaborationLevelRepository = new MockCollaborationLevelRepository().MockCollaborationLevelList(mockCollaborationLevels);
            InitialiseUserService_Success();
            int pageNumber = 1;
            int pageSize = 10;
            long agencyID = 1;
            var result = this.optionsService.GetCollaborationLevelList(pageNumber, pageSize, agencyID);
            Assert.Equal(0, result.TotalCount);
            //Assert.NotNull
        }

        [Fact]
        public void GetCollaborationLevelList_Failure_InvalidParameterResult()
        {
            var mockCollaborationLevels = new List<CollaborationLevelDTO>();
            this.mockCollaborationLevelRepository = new MockCollaborationLevelRepository().MockCollaborationLevelList(mockCollaborationLevels);
            InitialiseUserService_Success();
            int pageNumber = 0;
            int pageSize = 10;
            long agencyID = 1;
            var result = this.optionsService.GetCollaborationLevelList(pageNumber, pageSize, agencyID);
            Assert.Equal(0, result.TotalCount);
            //Assert.NotNull
        }
        [Fact]
        public void GetCollaborationLevelList_Failure_ExceptionResult()
        {
            List<CollaborationLevelDTO> mockCollaborationLevels = new List<CollaborationLevelDTO>();
            this.mockCollaborationLevelRepository = new MockCollaborationLevelRepository().MockCollaborationLevelListException(mockCollaborationLevels);
            InitialiseUserService_Success();
            int pageNumber = 1;
            int pageSize = 10;
            long agencyID = 1;
            Assert.ThrowsAny<Exception>(() => this.optionsService.GetCollaborationLevelList(pageNumber, pageSize, agencyID));
        }
        #endregion

        #region Add Collaboration Level
        [Fact]
        public void AddCollaborationLevel_Success_ReturnsCorrectResult()
        {
            var mockCollaborationLevel = GetMockCollaborationLevel();
            this.mockCollaborationLevelRepository = new MockCollaborationLevelRepository().MockAddCollaborationLevel(mockCollaborationLevel);
            InitialiseUserService_InsertionSuccess();
            int userID = 1;
            long agencyID = 1;
            var result = this.optionsService.AddCollaborationLevel(mockCollaborationLevel, userID, agencyID);
            Assert.Equal(result.ResponseStatus, PCISEnum.StatusMessages.InsertionSuccess);
        }

        [Fact]
        public void AddCollaborationLevel_Failure_ExceptionResult()
        {
            CollaborationLevelDTO mockCollaborationLevel = new CollaborationLevelDTO();
            this.mockOptionsRepository = new MockOptionsRepository().MockIsValidListOrder(true);
            this.mockCollaborationLevelRepository = new MockCollaborationLevelRepository().MockAddCollaborationLevelException(mockCollaborationLevel);
            InitialiseUserService_InsertionFailed();
            int userID = 1;
            long agencyID = 1;
            Assert.ThrowsAny<Exception>(() => this.optionsService.AddCollaborationLevel(mockCollaborationLevel, userID, agencyID));
        }
        #endregion

        #region Edit Collaboration Level
        [Fact]
        public void UpdateCollaborationLevel_Success_ReturnsCorrectResult()
        {
            var mockCollaborationLevelUpdateInput = GetMockCollaborationLevelUpdateInput();
            var mockCollaborationLevel = GetMockCollaborationLevel();
            this.mockCollaborationLevelRepository = new MockCollaborationLevelRepository().MockUpdateCollaborationLevel(mockCollaborationLevel);
            InitialiseUserService_UpdationSuccess();
            int userID = 1;
            long agencyID = 1;
            var result = this.optionsService.UpdateCollaborationLevel(mockCollaborationLevelUpdateInput, userID, agencyID);
            Assert.Equal(result.ResponseStatus, PCISEnum.StatusMessages.UpdationSuccess);
        }

        [Fact]
        public void UpdateCollaborationLevel_Failure_InvalidParameterResult()
        {
            var mockCollaborationLevelInput = GetMockCollaborationLevelUpdateInvalidInput(); //new CollaborationLevelDTO();
            var mockCollaborationLevel = new CollaborationLevelDTO();
            this.mockCollaborationLevelRepository = new MockCollaborationLevelRepository().MockUpdateCollaborationLevel(mockCollaborationLevelInput);
            InitialiseUserService_UpdationFailed();
            int userID = 1;
            long agencyID = 1;
            var result = this.optionsService.UpdateCollaborationLevel(mockCollaborationLevel, userID, agencyID);
            Assert.Equal(result.ResponseStatus, PCISEnum.StatusMessages.UpdationFailed);
        }

        [Fact]
        public void UpdateCollaborationLevel_Failure_ExceptionResult()
        {
            CollaborationLevelDTO mockCollaborationLevel = GetMockCollaborationLevel();
            this.mockCollaborationLevelRepository = new MockCollaborationLevelRepository().MockUpdateCollaborationLevelException(mockCollaborationLevel);
            this.mockOptionsRepository = new MockOptionsRepository().MockIsValidListOrder(true);
            InitialiseUserService_UpdationFailed();
            int userID = 1;
            long agencyID = 1;
            Assert.ThrowsAny<Exception>(() => this.optionsService.UpdateCollaborationLevel(mockCollaborationLevel, userID, agencyID));
        }
        #endregion

        #region Delete Collaboration Level
        [Fact]
        public void DeleteCollaborationLevel_Success_ReturnsCorrectResult()
        {
            var mockCollaborationLevelUpdateInput = GetMockCollaborationLevelDelete();
            var CollaborationLevelID = 1;
            this.mockCollaborationLevelRepository = new MockCollaborationLevelRepository().MockdeleteCollaborationLevel(mockCollaborationLevelUpdateInput);
            InitialiseUserService_DeletionSuccess();
            var result = this.optionsService.DeleteCollaborationLevel(CollaborationLevelID,1);
            Assert.Equal(result.ResponseStatus, PCISEnum.StatusMessages.DeletionSuccess);
        }

        [Fact]
        public void DeleteCollaborationLevel_Failure_ExceptionResult()
        {
            CollaborationLevelDTO mockCollaborationLevel = GetMockCollaborationLevelDelete();
            var CollaborationLevelID = 1;
            this.mockCollaborationLevelRepository = new MockCollaborationLevelRepository().MockDeleteCollaborationLevelException(mockCollaborationLevel);
            InitialiseUserService_DeletionFailed();
            Assert.ThrowsAny<Exception>(() => this.optionsService.DeleteCollaborationLevel(CollaborationLevelID,1));
        }
        #endregion

        #region Add Collaboration Tag Type
        [Fact]
        public void AddCollaborationTagType_Success_ReturnsCorrectResult()
        {
            var mockCollaborationTagType = GetMockCollaborationTagType();
            this.mockCollaborationTagTypeRepository = new MockCollaborationTagTypeRepository().MockAddCollaborationTagType(GetMockCollaborationTagTypes()[0]);
            InitialiseUserService_InsertionSuccess();
            int userID = 1;
            long agencyID = 1;
            var result = this.optionsService.AddCollaborationTagType(mockCollaborationTagType, userID, agencyID);
            Assert.Equal(PCISEnum.StatusMessages.InsertionSuccess, result.ResponseStatus);
        }

        [Fact]
        public void AddCollaborationTagType_Failure_ReturnsCorrectResult()
        {
            var mockCollaborationTagType = GetMockCollaborationTagType();
            this.mockCollaborationTagTypeRepository = new MockCollaborationTagTypeRepository().MockAddCollaborationTagTypeFailure(GetMockCollaborationTagTypes()[0]);
            InitialiseUserService_InsertionFailed();
            int userID = 1;
            long agencyID = 1;
            var result = this.optionsService.AddCollaborationTagType(mockCollaborationTagType, userID, agencyID);
            Assert.Equal(PCISEnum.StatusMessages.insertionFailed, result.ResponseStatus);
        }

        [Fact]
        public void AddCollaborationTagType_Failure_InvalidParameterResult()
        {
            var mockCollaborationTagType = new CollaborationTagTypeDTO();
            this.mockCollaborationTagTypeRepository = new MockCollaborationTagTypeRepository().MockAddCollaborationTagType(mockCollaborationTagType);
            InitialiseUserService_InsertionFailed();
            int userID = 1;
            long agencyID = 1;
            var result = this.optionsService.AddCollaborationTagType(null, userID, agencyID);
            Assert.NotNull(result);
            Assert.Null(result.ResponseStatus);
        }

        [Fact]
        public void AddCollaborationTagType_Failure_ExceptionResult()
        {
            CollaborationTagTypeDTO mockCollaborationTagTypes = new CollaborationTagTypeDTO();
            var mockCollaborationTagTypeDetail = new CollaborationTagTypeDetailsDTO();
            this.mockOptionsRepository = new MockOptionsRepository().MockIsValidListOrder(true);
            this.mockCollaborationTagTypeRepository = new MockCollaborationTagTypeRepository().MockAddCollaborationTagTypeException(mockCollaborationTagTypes);
            InitialiseUserService_InsertionFailed();
            int userID = 1;
            long agencyID = 1;
            Assert.ThrowsAny<Exception>(() => this.optionsService.AddCollaborationTagType(mockCollaborationTagTypeDetail, userID, agencyID));
        }
        #endregion

        #region Edit existing Collaboration Tag Type
        [Fact]
        public void UpdateCollaborationTagType_Success_ReturnsCorrectResult()
        {
            var mockCollaborationTagType = GetMockCollaborationTagType();
            this.mockCollaborationTagTypeRepository = new MockCollaborationTagTypeRepository().MockUpdateCollaborationTagType(GetMockCollaborationTagTypes()[0]);
            InitialiseUserService_UpdationSuccess();
            int userID = 1;
            long agencyID = 1;
            var result = this.optionsService.UpdateCollaborationTagType(mockCollaborationTagType, userID, agencyID);
            Assert.Equal(result.ResponseStatus, PCISEnum.StatusMessages.UpdationSuccess);
        }

        [Fact]
        public void UpdateCollaborationTagType_Failure_ReturnsCorrectResult()
        {
            var mockCollaborationTagType = GetMockCollaborationTagType();
            this.mockCollaborationTagTypeRepository = new MockCollaborationTagTypeRepository().MockUpdateCollaborationTagTypeFailure(GetMockCollaborationTagTypes()[0]);
            InitialiseUserService_UpdationFailed();
            int userID = 1;
            long agencyID = 1;
            var result = this.optionsService.UpdateCollaborationTagType(mockCollaborationTagType, userID, agencyID);

            Assert.Equal(PCISEnum.StatusMessages.UpdationFailed, result.ResponseStatus);
        }

        [Fact]
        public void UpdateCollaborationTagType_Failure_InvalidParameterResult()
        {
            var mockCollaborationTagType = new CollaborationTagTypeDTO();
            this.mockCollaborationTagTypeRepository = new MockCollaborationTagTypeRepository().MockUpdateCollaborationTagType(mockCollaborationTagType);
            InitialiseUserService_UpdationFailed();
            int userID = 1;
            long agencyID = 1;
            var result = this.optionsService.UpdateCollaborationTagType(null, userID, agencyID);
            Assert.NotNull(result);
            Assert.Null(result.ResponseStatus);
        }

        [Fact]
        public void UpdateCollaborationTagType_Failure_ExceptionResult()
        {
            CollaborationTagTypeDTO mockCollaborationTagTypes = new CollaborationTagTypeDTO();
            var mockCollaborationTagTypeDetail = new CollaborationTagTypeDetailsDTO();
            this.mockOptionsRepository = new MockOptionsRepository().MockIsValidListOrder(true);
            this.mockCollaborationTagTypeRepository = new MockCollaborationTagTypeRepository().MockUpdateCollaborationTagTypeException(mockCollaborationTagTypes);
            InitialiseUserService_UpdationFailed();
            int userID = 1;
            long agencyID = 1;
            Assert.ThrowsAny<Exception>(() => this.optionsService.UpdateCollaborationTagType(mockCollaborationTagTypeDetail, userID, agencyID));
        }
        #endregion

        #region Delete existing Collaboration Tag Type
        [Fact]
        public void DeleteCollaborationTagType_Success_ReturnsCorrectResult()
        {
            var mockCollaborationTagType = GetMockCollaborationTagTypes()[0];
            this.mockCollaborationTagTypeRepository = new MockCollaborationTagTypeRepository().MockUpdateCollaborationTagType(mockCollaborationTagType).MockGetCollaborationTagType(mockCollaborationTagType);
            this.mockCollaborationTagRepository = new MockCollaborationTagRepository().MockGetCollaborationTagTypeCountByCollaborationTag(mockCollaborationTagType.CollaborationTagTypeID, 0);
            InitialiseUserService_DeletionSuccess();
            var result = this.optionsService.DeleteCollaborationTagType(mockCollaborationTagType.CollaborationTagTypeID,1);
            Assert.Equal(PCISEnum.StatusMessages.DeletionSuccess, result.ResponseStatus);
        }

        [Fact]
        public void DeleteCollaborationTagType_Failure_ReturnsCorrectResult()
        {
            var mockCollaborationTagType = GetMockCollaborationTagTypes()[0];
            this.mockCollaborationTagTypeRepository = new MockCollaborationTagTypeRepository().MockUpdateCollaborationTagTypeFailure(mockCollaborationTagType).MockGetCollaborationTagType(mockCollaborationTagType);
            this.mockCollaborationTagRepository = new MockCollaborationTagRepository().MockGetCollaborationTagTypeCountByCollaborationTag(mockCollaborationTagType.CollaborationTagTypeID, 0);
            InitialiseUserService_DeletionFailed();
            var result = this.optionsService.DeleteCollaborationTagType(mockCollaborationTagType.CollaborationTagTypeID,1);
            Assert.Equal(PCISEnum.StatusMessages.DeletionFailed, result.ResponseStatus);
        }

        [Fact]
        public void DeleteCollaborationTagType_Failure_ConflictResult()
        {
            var mockCollaborationTagType = GetMockCollaborationTagTypes()[0];
            this.mockCollaborationTagTypeRepository = new MockCollaborationTagTypeRepository().MockUpdateCollaborationTagType(mockCollaborationTagType).MockGetCollaborationTagType(mockCollaborationTagType);
            this.mockCollaborationTagRepository = new MockCollaborationTagRepository().MockGetCollaborationTagTypeCountByCollaborationTag(mockCollaborationTagType.CollaborationTagTypeID, 1);
            InitialiseUserService_DeleteRecordInUse();
            var result = this.optionsService.DeleteCollaborationTagType(mockCollaborationTagType.CollaborationTagTypeID,0);
            Assert.Equal(PCISEnum.StatusMessages.DeleteRecordInUse, result.ResponseStatus);
        }

        [Fact]
        public void DeleteCollaborationTagType_Failure_InvalidParameterResult()
        {
            var mockCollaborationTagType = GetMockCollaborationTagTypes()[0];
            this.mockCollaborationTagTypeRepository = new MockCollaborationTagTypeRepository().MockUpdateCollaborationTagType(mockCollaborationTagType).MockGetCollaborationTagType(null);
            InitialiseUserService_DeletionFailed();
            var result = this.optionsService.DeleteCollaborationTagType(mockCollaborationTagType.CollaborationTagTypeID,0);
            Assert.Equal(PCISEnum.StatusMessages.DeletionFailed, result.ResponseStatus);
        }

        [Fact]
        public void DeleteCollaborationTagType_Failure_ExceptionResult()
        {
            var mockCollaborationTagTypeDetail = GetMockCollaborationTagType();
            var mockCollaborationTagType = GetMockCollaborationTagTypes()[0];
            this.mockCollaborationTagTypeRepository = new MockCollaborationTagTypeRepository().MockUpdateCollaborationTagTypeException(mockCollaborationTagType).MockGetCollaborationTagType(mockCollaborationTagType);
            this.mockCollaborationTagRepository = new MockCollaborationTagRepository().MockGetCollaborationTagTypeCountByCollaborationTag(mockCollaborationTagType.CollaborationTagTypeID, 0);
            InitialiseUserService_DeletionFailed();
            Assert.ThrowsAny<Exception>(() => this.optionsService.DeleteCollaborationTagType(mockCollaborationTagTypeDetail.CollaborationTagTypeID,1));
        }
        #endregion

        #region Get Collaboration Tag Type List
        [Fact]
        public void GetCollaborationTagTypeList_Success_ReturnsCorrectResult()
        {
            var mockCollaborationTagTypes = GetMockCollaborationTagTypes();
            this.mockCollaborationTagTypeRepository = new MockCollaborationTagTypeRepository().MockGetCollaborationTagTypeList(mockCollaborationTagTypes);
            InitialiseUserService_Success();
            int pageNumber = 1;
            int pageSize = 10;
            long agencyID = 1;
            var result = this.optionsService.GetCollaborationTagTypeList(pageNumber, pageSize, agencyID);
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.Success);
        }

        [Fact]
        public void GetCollaborationTagTypeList_Success_ReturnsNoResult()
        {
            var mockCollaborationTagTypes = new List<CollaborationTagTypeDTO>();
            this.mockCollaborationTagTypeRepository = new MockCollaborationTagTypeRepository().MockGetCollaborationTagTypeList(mockCollaborationTagTypes);
            InitialiseUserService_Success();
            int pageNumber = 1;
            int pageSize = 10;
            long agencyID = 1;
            var result = this.optionsService.GetCollaborationTagTypeList(pageNumber, pageSize, agencyID);
            Assert.Equal(0, result.TotalCount);
        }

        [Fact]
        public void GetCollaborationTagTypeList_Failure_InvalidParameterResult()
        {
            var mockCollaborationTagTypes = new List<CollaborationTagTypeDTO>();
            this.mockCollaborationTagTypeRepository = new MockCollaborationTagTypeRepository().MockGetCollaborationTagTypeList(mockCollaborationTagTypes);
            InitialiseUserService_Success();
            int pageNumber = 0;
            int pageSize = 10;
            long agencyID = 1;
            var result = this.optionsService.GetCollaborationTagTypeList(pageNumber, pageSize, agencyID);
            Assert.Equal(0, result.TotalCount);
            pageNumber = 1;
            pageSize = 0;
            result = this.optionsService.GetCollaborationTagTypeList(pageNumber, pageSize, agencyID);
            Assert.Equal(0, result.TotalCount);
        }

        [Fact]
        public void GetCollaborationTagTypeList_Failure_ExceptionResult()
        {
            List<CollaborationTagTypeDTO> mockCollaborationTagTypes = new List<CollaborationTagTypeDTO>();
            this.mockCollaborationTagTypeRepository = new MockCollaborationTagTypeRepository().MockCollaborationTagTypeListException(mockCollaborationTagTypes);
            InitialiseUserService_Success();
            int pageNumber = 1;
            int pageSize = 10;
            long agencyID = 1;
            Assert.ThrowsAny<Exception>(() => this.optionsService.GetCollaborationTagTypeList(pageNumber, pageSize, agencyID));
        }
        #endregion

        #region Get Collaboration Tag Type List Agencywise
        [Fact]
        public void GetAgencyCollaborationTagTypeList_Success_ReturnsCorrectResult()
        {
            var mockCollaborationTagTypes = GetMockCollaborationTagTypes();
            this.mockCollaborationTagTypeRepository = new MockCollaborationTagTypeRepository().MockGetCollaborationTagTypeList(mockCollaborationTagTypes, 1);
            InitialiseUserService_Success();
            int pageNumber = 1;
            int pageSize = 10;
            int agencyId = 1;
            var result = this.optionsService.GetCollaborationTagTypeList(agencyId, pageNumber, pageSize);
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.Success);
        }

        [Fact]
        public void GetAgencyCollaborationTagTypeList_Success_ReturnsNoResult()
        {
            var mockCollaborationTagTypes = new List<CollaborationTagTypeDTO>();
            this.mockCollaborationTagTypeRepository = new MockCollaborationTagTypeRepository().MockGetCollaborationTagTypeList(mockCollaborationTagTypes, 1);
            InitialiseUserService_Success();
            int pageNumber = 1;
            int pageSize = 10;
            int agencyId = 1;
            var result = this.optionsService.GetCollaborationTagTypeList(agencyId, pageNumber, pageSize);
            Assert.Equal(0, result.TotalCount);
        }

        [Fact]
        public void GetAgencyCollaborationTagTypeList_Failure_InvalidParameterResult()
        {
            var mockCollaborationTagTypes = new List<CollaborationTagTypeDTO>();
            this.mockCollaborationTagTypeRepository = new MockCollaborationTagTypeRepository().MockGetCollaborationTagTypeList(mockCollaborationTagTypes, 1);
            InitialiseUserService_Success();
            int pageNumber = 0;
            int pageSize = 10;
            int agencyId = 1;
            var result = this.optionsService.GetCollaborationTagTypeList(agencyId, pageNumber, pageSize);
            Assert.Equal(0, result.TotalCount);
            pageNumber = 1;
            pageSize = 0;
            agencyId = 1;
            result = this.optionsService.GetCollaborationTagTypeList(agencyId, pageNumber, pageSize);
            Assert.Equal(0, result.TotalCount);
        }

        #endregion

        #region Add Helper title
        [Fact]
        public void AddTherapyType_Success_ReturnsCorrectResult()
        {
            var mockTherapyType = GetMockTherapyType();
            this.mockTherapyTypeRepository = new MockTherapyTypeRepository().MockAddTherapyType(GetMockTherapyTypes()[0]);
            InitialiseUserService_InsertionSuccess();
            int userID = 1;
            long agencyID = 1;
            var result = this.optionsService.AddTherapyType(mockTherapyType, userID, agencyID);
            Assert.Equal(PCISEnum.StatusMessages.InsertionSuccess, result.ResponseStatus);
        }

        [Fact]
        public void AddTherapyType_Failure_ReturnsCorrectResult()
        {
            var mockTherapyType = GetMockTherapyType();
            this.mockTherapyTypeRepository = new MockTherapyTypeRepository().MockAddTherapyTypeFailure(GetMockTherapyTypes()[0]);
            InitialiseUserService_InsertionFailed();
            int userID = 1;
            long agencyID = 1;
            var result = this.optionsService.AddTherapyType(mockTherapyType, userID, agencyID);
            Assert.Equal(PCISEnum.StatusMessages.insertionFailed, result.ResponseStatus);
        }

        [Fact]
        public void AddTherapyType_Failure_InvalidParameterResult()
        {
            var mockTherapyType = new TherapyTypeDTO();
            this.mockTherapyTypeRepository = new MockTherapyTypeRepository().MockAddTherapyType(mockTherapyType);
            InitialiseUserService_InsertionFailed();
            int userID = 1;
            long agencyID = 1;
            var result = this.optionsService.AddTherapyType(null, userID, agencyID);
            Assert.NotNull(result);
            Assert.Null(result.ResponseStatus);
        }

        [Fact]
        public void AddTherapyType_Failure_ExceptionResult()
        {
            TherapyTypeDTO mockTherapyTypes = new TherapyTypeDTO();
            var mockTherapyTypeDetail = new TherapyTypeDetailsDTO();
            this.mockOptionsRepository = new MockOptionsRepository().MockIsValidListOrder(true);
            this.mockTherapyTypeRepository = new MockTherapyTypeRepository().MockAddTherapyTypeException(mockTherapyTypes);
            InitialiseUserService_InsertionFailed();
            int userID = 1;
            long agencyID = 1;
            Assert.ThrowsAny<Exception>(() => this.optionsService.AddTherapyType(mockTherapyTypeDetail, userID, agencyID));
        }
        #endregion

        #region Edit existing Helper Title
        [Fact]
        public void UpdateTherapyType_Success_ReturnsCorrectResult()
        {
            var mockTherapyType = GetMockTherapyType();
            this.mockTherapyTypeRepository = new MockTherapyTypeRepository().MockUpdateTherapyType(GetMockTherapyTypes()[0]);
            InitialiseUserService_UpdationSuccess();
            int userID = 1;
            long agencyID = 1;
            var result = this.optionsService.UpdateTherapyType(mockTherapyType, userID, agencyID);
            Assert.Equal(result.ResponseStatus, PCISEnum.StatusMessages.UpdationSuccess);
        }

        [Fact]
        public void UpdateTherapyType_Failure_ReturnsCorrectResult()
        {
            var mockTherapyType = GetMockTherapyType();
            this.mockTherapyTypeRepository = new MockTherapyTypeRepository().MockUpdateTherapyTypeFailure(GetMockTherapyTypes()[0]);
            InitialiseUserService_UpdationFailed();
            int userID = 1;
            long agencyID = 1;
            var result = this.optionsService.UpdateTherapyType(mockTherapyType, userID, agencyID);
            Assert.Equal(PCISEnum.StatusMessages.UpdationFailed, result.ResponseStatus);
        }

        [Fact]
        public void UpdateTherapyType_Failure_InvalidParameterResult()
        {
            var mockTherapyType = new TherapyTypeDTO();
            this.mockTherapyTypeRepository = new MockTherapyTypeRepository().MockUpdateTherapyType(mockTherapyType);
            InitialiseUserService_UpdationFailed();
            int userID = 1;
            long agencyID = 1;
            var result = this.optionsService.UpdateTherapyType(null, userID, agencyID);
            Assert.NotNull(result);
            Assert.Null(result.ResponseStatus);
        }

        [Fact]
        public void UpdateTherapyType_Failure_ExceptionResult()
        {
            TherapyTypeDTO mockTherapyTypes = new TherapyTypeDTO();
            var mockTherapyTypeDetail = new TherapyTypeDetailsDTO();
            this.mockOptionsRepository = new MockOptionsRepository().MockIsValidListOrder(true);
            this.mockTherapyTypeRepository = new MockTherapyTypeRepository().MockUpdateTherapyTypeException(mockTherapyTypes);
            InitialiseUserService_UpdationFailed();
            int userID = 1;
            long agencyID = 1;
            Assert.ThrowsAny<Exception>(() => this.optionsService.UpdateTherapyType(mockTherapyTypeDetail, userID, agencyID));
        }
        #endregion

        #region Delete existing Helper Title
        [Fact]
        public void DeleteTherapyType_Success_ReturnsCorrectResult()
        {
            var mockTherapyType = GetMockTherapyTypes()[0];
            this.mockTherapyTypeRepository = new MockTherapyTypeRepository().MockUpdateTherapyType(mockTherapyType).MockGetTherapyType(mockTherapyType);
            this.mockCollaborationRepository = new MockCollaborationRepository().MockGetCollaborationCountByTherapy(mockTherapyType.TherapyTypeID, 0);
            InitialiseUserService_DeletionSuccess();
            var result = this.optionsService.DeleteTherapyType(mockTherapyType.TherapyTypeID,1);
            Assert.Equal(PCISEnum.StatusMessages.DeletionSuccess, result.ResponseStatus);
        }

        [Fact]
        public void DeleteTherapyType_Failure_ReturnsCorrectResult()
        {
            var mockTherapyType = GetMockTherapyTypes()[0];
            this.mockTherapyTypeRepository = new MockTherapyTypeRepository().MockUpdateTherapyTypeFailure(mockTherapyType).MockGetTherapyType(mockTherapyType);
            this.mockCollaborationRepository = new MockCollaborationRepository().MockGetCollaborationCountByTherapy(mockTherapyType.TherapyTypeID, 0);
            InitialiseUserService_DeletionFailed();
            var result = this.optionsService.DeleteTherapyType(mockTherapyType.TherapyTypeID,1);
            Assert.Equal(PCISEnum.StatusMessages.DeletionFailed, result.ResponseStatus);
        }

        [Fact]
        public void DeleteTherapyType_Failure_ConflictResult()
        {
            var mockTherapyType = GetMockTherapyTypes()[0];
            this.mockTherapyTypeRepository = new MockTherapyTypeRepository().MockUpdateTherapyType(mockTherapyType).MockGetTherapyType(mockTherapyType);
            this.mockCollaborationRepository = new MockCollaborationRepository().MockGetCollaborationCountByTherapy(mockTherapyType.TherapyTypeID, 1);
            InitialiseUserService_DeleteRecordInUse();
            var result = this.optionsService.DeleteTherapyType(mockTherapyType.TherapyTypeID,1);
            Assert.Equal(PCISEnum.StatusMessages.DeleteRecordInUse, result.ResponseStatus);
        }

        [Fact]
        public void DeleteTherapyType_Failure_InvalidParameterResult()
        {
            var mockTherapyType = GetMockTherapyTypes()[0];
            this.mockTherapyTypeRepository = new MockTherapyTypeRepository().MockUpdateTherapyType(mockTherapyType).MockGetTherapyType(null);
            InitialiseUserService_DeletionFailed();
            var result = this.optionsService.DeleteTherapyType(mockTherapyType.TherapyTypeID,1);
            Assert.Equal(PCISEnum.StatusMessages.DeletionFailed, result.ResponseStatus);
        }

        [Fact]
        public void DeleteTherapyType_Failure_ExceptionResult()
        {
            var mockTherapyTypeDetail = GetMockTherapyType();
            var mockTherapyType = GetMockTherapyTypes()[0];
            this.mockTherapyTypeRepository = new MockTherapyTypeRepository().MockUpdateTherapyTypeException(mockTherapyType).MockGetTherapyType(mockTherapyType);
            this.mockCollaborationRepository = new MockCollaborationRepository().MockGetCollaborationCountByTherapy(mockTherapyType.TherapyTypeID, 0);
            InitialiseUserService_DeletionFailed();
            Assert.ThrowsAny<Exception>(() => this.optionsService.DeleteTherapyType(mockTherapyTypeDetail.TherapyTypeID,1));
        }
        #endregion

        #region Get Helper Title List
        [Fact]
        public void GetTherapyTypeList_Success_ReturnsCorrectResult()
        {
            var mockTherapyTypes = GetMockTherapyTypes();
            this.mockTherapyTypeRepository = new MockTherapyTypeRepository().MockGetTherapyTypeList(mockTherapyTypes);
            InitialiseUserService_Success();
            int pageNumber = 1;
            int pageSize = 10;
            long agencyID = 1;
            var result = this.optionsService.GetTherapyTypeList(pageNumber, pageSize, agencyID);
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.Success);
        }

        [Fact]
        public void GetTherapyTypeList_Success_ReturnsNoResult()
        {
            var mockTherapyTypes = new List<TherapyTypeDTO>();
            this.mockTherapyTypeRepository = new MockTherapyTypeRepository().MockGetTherapyTypeList(mockTherapyTypes);
            InitialiseUserService_Success();
            int pageNumber = 1;
            int pageSize = 10;
            long agencyID = 1;
            var result = this.optionsService.GetTherapyTypeList(pageNumber, pageSize, agencyID);
            Assert.Equal(0, result.TotalCount);
        }

        [Fact]
        public void GetTherapyTypeList_Failure_InvalidParameterResult()
        {
            var mockTherapyTypes = new List<TherapyTypeDTO>();
            this.mockTherapyTypeRepository = new MockTherapyTypeRepository().MockGetTherapyTypeList(mockTherapyTypes);
            InitialiseUserService_Success();
            int pageNumber = 0;
            int pageSize = 10;
            long agencyID = 1;
            var result = this.optionsService.GetTherapyTypeList(pageNumber, pageSize, agencyID);
            Assert.Equal(0, result.TotalCount);
            pageNumber = 1;
            pageSize = 0;
            result = this.optionsService.GetTherapyTypeList(pageNumber, pageSize, agencyID);
            Assert.Equal(0, result.TotalCount);
        }

        [Fact]
        public void GetTherapyTypeList_Failure_ExceptionResult()
        {
            List<TherapyTypeDTO> mockTherapyTypes = new List<TherapyTypeDTO>();
            this.mockTherapyTypeRepository = new MockTherapyTypeRepository().MockTherapyTypeListException(mockTherapyTypes);
            InitialiseUserService_Success();
            int pageNumber = 1;
            int pageSize = 10;
            long agencyID = 1;
            Assert.ThrowsAny<Exception>(() => this.optionsService.GetTherapyTypeList(pageNumber, pageSize, agencyID));
        }
        #endregion

        #region Get Helper Title List Agencywise
        [Fact]
        public void GetAgencyTherapyTypeList_Success_ReturnsCorrectResult()
        {
            var mockTherapyTypes = GetMockTherapyTypes();
            this.mockTherapyTypeRepository = new MockTherapyTypeRepository().MockGetTherapyTypeList(mockTherapyTypes, 1);
            InitialiseUserService_Success();
            int pageNumber = 1;
            int pageSize = 10;
            int agencyId = 1;
            var result = this.optionsService.GetTherapyTypeList(agencyId, pageNumber, pageSize);
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.Success);
        }

        [Fact]
        public void GetAgencyTherapyTypeList_Success_ReturnsNoResult()
        {
            var mockTherapyTypes = new List<TherapyTypeDTO>();
            this.mockTherapyTypeRepository = new MockTherapyTypeRepository().MockGetTherapyTypeList(mockTherapyTypes, 1);
            InitialiseUserService_Success();
            int pageNumber = 1;
            int pageSize = 10;
            int agencyId = 1;
            var result = this.optionsService.GetTherapyTypeList(agencyId, pageNumber, pageSize);
            Assert.Equal(0, result.TotalCount);
        }

        [Fact]
        public void GetAgencyTherapyTypeList_Failure_InvalidParameterResult()
        {
            var mockTherapyTypes = new List<TherapyTypeDTO>();
            this.mockTherapyTypeRepository = new MockTherapyTypeRepository().MockGetTherapyTypeList(mockTherapyTypes, 1);
            InitialiseUserService_Success();

            int pageNumber = 0;
            int pageSize = 10;
            int agencyId = 1;
            var result = this.optionsService.GetTherapyTypeList(agencyId, pageNumber, pageSize);
            Assert.Equal(0, result.TotalCount);
            pageNumber = 1;
            pageSize = 0;
            agencyId = 1;
            result = this.optionsService.GetTherapyTypeList(agencyId, pageNumber, pageSize);
            Assert.Equal(0, result.TotalCount);
        }

        [Fact]
        public void GetAgencyTherapyTypeList_Failure_ExceptionResult()
        {
            List<TherapyTypeDTO> mockTherapyTypes = new List<TherapyTypeDTO>();
            this.mockTherapyTypeRepository = new MockTherapyTypeRepository().MockTherapyTypeListException(mockTherapyTypes, 1);
            InitialiseUserService_Success();
            int pageNumber = 1;
            int pageSize = 10;
            int agencyId = 1;
            Assert.ThrowsAny<Exception>(() => this.optionsService.GetTherapyTypeList(agencyId, pageNumber, pageSize));
        }
        #endregion

        #region Add Helper title
        [Fact]
        public void AddHelperTitle_Success_ReturnsCorrectResult()
        {
            var mockHelperTitle = GetMockHelperTitle();
            this.mockHelperTitleRepository = new MockHelperTitleRepository().MockAddHelperTitle(GetMockHelperTitles()[0]);
            InitialiseUserService_InsertionSuccess();
            int userID = 1;
            long agencyID = 1;
            var result = this.optionsService.AddHelperTitle(mockHelperTitle, userID, agencyID);
            Assert.Equal(PCISEnum.StatusMessages.InsertionSuccess, result.ResponseStatus);
        }

        [Fact]
        public void AddHelperTitle_Failure_ReturnsCorrectResult()
        {
            var mockHelperTitle = GetMockHelperTitle();
            this.mockHelperTitleRepository = new MockHelperTitleRepository().MockAddHelperTitleFailure(GetMockHelperTitles()[0]);
            InitialiseUserService_InsertionFailed();
            int userID = 1;
            long agencyID = 1;
            var result = this.optionsService.AddHelperTitle(mockHelperTitle, userID, agencyID);
            Assert.Equal(PCISEnum.StatusMessages.insertionFailed, result.ResponseStatus);
        }

        [Fact]
        public void AddHelperTitle_Failure_InvalidParameterResult()
        {
            var mockHelperTitle = new HelperTitleDTO();
            this.mockHelperTitleRepository = new MockHelperTitleRepository().MockAddHelperTitle(mockHelperTitle);
            InitialiseUserService_InsertionFailed();
            int userID = 1;
            long agencyID = 1;
            var result = this.optionsService.AddHelperTitle(null, userID, agencyID);
            Assert.NotNull(result);
            Assert.Null(result.ResponseStatus);
        }

        [Fact]
        public void AddHelperTitle_Failure_ExceptionResult()
        {
            HelperTitleDTO mockHelperTitles = new HelperTitleDTO();
            var mockHelperTitleDetail = new HelperTitleDetailsDTO();
            this.mockOptionsRepository = new MockOptionsRepository().MockIsValidListOrder(true);
            this.mockHelperTitleRepository = new MockHelperTitleRepository().MockAddHelperTitleException(mockHelperTitles);
            InitialiseUserService_InsertionFailed();
            int userID = 1;
            long agencyID = 1;
            Assert.ThrowsAny<Exception>(() => this.optionsService.AddHelperTitle(mockHelperTitleDetail, userID, agencyID));
        }
        #endregion

        #region Edit existing Helper Title
        [Fact]
        public void UpdateHelperTitle_Success_ReturnsCorrectResult()
        {
            var mockHelperTitle = GetMockHelperTitle();
            this.mockHelperTitleRepository = new MockHelperTitleRepository().MockUpdateHelperTitle(GetMockHelperTitles()[0]);
            InitialiseUserService_UpdationSuccess();
            int userID = 1;
            long agencyID = 1;
            var result = this.optionsService.UpdateHelperTitle(mockHelperTitle, userID, agencyID);
            Assert.Equal(result.ResponseStatus, PCISEnum.StatusMessages.UpdationSuccess);
        }

        [Fact]
        public void UpdateHelperTitle_Failure_ReturnsCorrectResult()
        {
            var mockHelperTitle = GetMockHelperTitle();
            this.mockHelperTitleRepository = new MockHelperTitleRepository().MockUpdateHelperTitleFailure(GetMockHelperTitles()[0]);
            InitialiseUserService_UpdationFailed();
            int userID = 1;
            long agencyID = 1;
            var result = this.optionsService.UpdateHelperTitle(mockHelperTitle, userID, agencyID);
            Assert.Equal(PCISEnum.StatusMessages.UpdationFailed, result.ResponseStatus);
        }

        [Fact]
        public void UpdateHelperTitle_Failure_InvalidParameterResult()
        {
            var mockHelperTitle = new HelperTitleDTO();
            this.mockHelperTitleRepository = new MockHelperTitleRepository().MockUpdateHelperTitle(mockHelperTitle);
            InitialiseUserService_UpdationFailed();
            int userID = 1;
            long agencyID = 1;
            var result = this.optionsService.UpdateHelperTitle(null, userID, agencyID);
            Assert.NotNull(result);
            Assert.Null(result.ResponseStatus);
        }

        [Fact]
        public void UpdateHelperTitle_Failure_ExceptionResult()
        {
            HelperTitleDTO mockHelperTitles = new HelperTitleDTO();
            var mockHelperTitleDetail = new HelperTitleDetailsDTO();
            this.mockOptionsRepository = new MockOptionsRepository().MockIsValidListOrder(true);
            this.mockHelperTitleRepository = new MockHelperTitleRepository().MockUpdateHelperTitleException(mockHelperTitles);
            InitialiseUserService_UpdationFailed();
            int userID = 1;
            long agencyID = 1;
            Assert.ThrowsAny<Exception>(() => this.optionsService.UpdateHelperTitle(mockHelperTitleDetail, userID, agencyID));
        }
        #endregion

        #region Delete existing Helper Title
        [Fact]
        public void DeleteHelperTitle_Success_ReturnsCorrectResult()
        {
            var mockHelperTitle = GetMockHelperTitles()[0];
            this.mockHelperTitleRepository = new MockHelperTitleRepository().MockUpdateHelperTitle(mockHelperTitle).MockGetHelperTitle(mockHelperTitle);
            this.mockHelperRepository = new MockHelperRepository().MockGetHelperCountByHelperTitle(mockHelperTitle.HelperTitleID, 0);
            InitialiseUserService_DeletionSuccess();
            var result = this.optionsService.DeleteHelperTitle(mockHelperTitle.HelperTitleID,1);
            Assert.Equal(PCISEnum.StatusMessages.DeletionSuccess, result.ResponseStatus);
        }

        [Fact]
        public void DeleteHelperTitle_Failure_ReturnsCorrectResult()
        {
            var mockHelperTitle = GetMockHelperTitles()[0];
            this.mockHelperTitleRepository = new MockHelperTitleRepository().MockUpdateHelperTitleFailure(mockHelperTitle).MockGetHelperTitle(mockHelperTitle);
            this.mockHelperRepository = new MockHelperRepository().MockGetHelperCountByHelperTitle(mockHelperTitle.HelperTitleID, 0);
            InitialiseUserService_DeletionFailed();
            var result = this.optionsService.DeleteHelperTitle(mockHelperTitle.HelperTitleID,1);
            Assert.Equal(PCISEnum.StatusMessages.DeletionFailed, result.ResponseStatus);
        }

        [Fact]
        public void DeleteHelperTitle_Failure_ConflictResult()
        {
            var mockHelperTitle = GetMockHelperTitles()[0];
            this.mockHelperTitleRepository = new MockHelperTitleRepository().MockUpdateHelperTitle(mockHelperTitle).MockGetHelperTitle(mockHelperTitle);
            this.mockHelperRepository = new MockHelperRepository().MockGetHelperCountByHelperTitle(mockHelperTitle.HelperTitleID, 1);
            InitialiseUserService_DeleteRecordInUse();
            var result = this.optionsService.DeleteHelperTitle(mockHelperTitle.HelperTitleID,1);
            Assert.Equal(PCISEnum.StatusMessages.DeleteRecordInUse, result.ResponseStatus);
        }

        [Fact]
        public void DeleteHelperTitle_Failure_InvalidParameterResult()
        {
            var mockHelperTitle = GetMockHelperTitles()[0];
            this.mockHelperTitleRepository = new MockHelperTitleRepository().MockUpdateHelperTitle(mockHelperTitle).MockGetHelperTitle(null);
            InitialiseUserService_DeletionFailed();
            var result = this.optionsService.DeleteHelperTitle(mockHelperTitle.HelperTitleID,1);
            Assert.Equal(PCISEnum.StatusMessages.DeletionFailed, result.ResponseStatus);
        }

        [Fact]
        public void DeleteHelperTitle_Failure_ExceptionResult()
        {
            var mockHelperTitleDetail = GetMockHelperTitle();
            var mockHelperTitle = GetMockHelperTitles()[0];
            this.mockHelperTitleRepository = new MockHelperTitleRepository().MockUpdateHelperTitleException(mockHelperTitle).MockGetHelperTitle(mockHelperTitle);
            this.mockHelperRepository = new MockHelperRepository().MockGetHelperCountByHelperTitle(mockHelperTitle.HelperTitleID, 0);
            InitialiseUserService_DeletionFailed();
            Assert.ThrowsAny<Exception>(() => this.optionsService.DeleteHelperTitle(mockHelperTitleDetail.HelperTitleID,1));
        }
        #endregion

        #region Get Helper Title List
        [Fact]
        public void GetHelperTitleList_Success_ReturnsCorrectResult()
        {
            var mockHelperTitles = GetMockHelperTitles();
            this.mockHelperTitleRepository = new MockHelperTitleRepository().MockGetHelperTitleList(mockHelperTitles);
            InitialiseUserService_Success();
            int pageNumber = 1;
            int pageSize = 10;
            long agencyID = 1;
            var result = this.optionsService.GetHelperTitleList(pageNumber, pageSize, agencyID);
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.Success);
        }

        [Fact]
        public void GetHelperTitleList_Success_ReturnsNoResult()
        {
            var mockHelperTitles = new List<HelperTitleDTO>();
            this.mockHelperTitleRepository = new MockHelperTitleRepository().MockGetHelperTitleList(mockHelperTitles);
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Success);
            InitialiseUserService_Success();
            int pageNumber = 1;
            int pageSize = 10;
            long agencyID = 1;
            var result = this.optionsService.GetHelperTitleList(pageNumber, pageSize, agencyID);
            Assert.Equal(0, result.TotalCount);
        }

        [Fact]
        public void GetHelperTitleList_Failure_InvalidParameterResult()
        {
            var mockHelperTitles = new List<HelperTitleDTO>();
            this.mockHelperTitleRepository = new MockHelperTitleRepository().MockGetHelperTitleList(mockHelperTitles);
            InitialiseUserService_Success();
            int pageNumber = 0;
            int pageSize = 10;
            long agencyID = 1;
            var result = this.optionsService.GetHelperTitleList(pageNumber, pageSize, agencyID);
            Assert.Equal(0, result.TotalCount);
        }

        [Fact]
        public void GetHelperTitleList_Failure_ExceptionResult()
        {
            List<HelperTitleDTO> mockHelperTitles = new List<HelperTitleDTO>();
            this.mockHelperTitleRepository = new MockHelperTitleRepository().MockHelperTitleListException(mockHelperTitles);
            InitialiseUserService_Success();
            int pageNumber = 1;
            int pageSize = 10;
            long agencyID = 1;
            Assert.ThrowsAny<Exception>(() => this.optionsService.GetHelperTitleList(pageNumber, pageSize, agencyID));
        }
        #endregion

        #region Get Helper Title List Agencywise
        [Fact]
        public void GetAgencyHelperTitleList_Success_ReturnsCorrectResult()
        {
            var mockHelperTitles = GetMockHelperTitles();
            this.mockHelperTitleRepository = new MockHelperTitleRepository().MockGetHelperTitleList(mockHelperTitles, 1);
            InitialiseUserService_Success();
            int pageNumber = 1;
            int pageSize = 10;
            long agencyId = 1;
            var result = this.optionsService.GetHelperTitleList(agencyId, pageNumber, pageSize);
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.Success);
        }

        [Fact]
        public void GetAgencyHelperTitleList_Success_ReturnsNoResult()
        {
            var mockHelperTitles = new List<HelperTitleDTO>();
            this.mockHelperTitleRepository = new MockHelperTitleRepository().MockGetHelperTitleList(mockHelperTitles, 1);
            InitialiseUserService_Success();
            int pageNumber = 1;
            int pageSize = 10;
            long agencyId = 1;
            var result = this.optionsService.GetHelperTitleList(agencyId, pageNumber, pageSize);
            Assert.Equal(0, result.TotalCount);
        }

        [Fact]
        public void GetAgencyHelperTitleList_Failure_InvalidParameterResult()
        {
            var mockHelperTitles = new List<HelperTitleDTO>();
            this.mockHelperTitleRepository = new MockHelperTitleRepository().MockGetHelperTitleList(mockHelperTitles, 1);
            InitialiseUserService_Success();
            int pageNumber = 0;
            int pageSize = 10;
            long agencyId = 1;
            var result = this.optionsService.GetHelperTitleList(agencyId, pageNumber, pageSize);
            Assert.Equal(0, result.TotalCount);
        }

        [Fact]
        public void GetAgencyHelperTitleList_Failure_ExceptionResult()
        {
            List<HelperTitleDTO> mockHelperTitles = new List<HelperTitleDTO>();
            this.mockHelperTitleRepository = new MockHelperTitleRepository().MockHelperTitleListException(mockHelperTitles, 1);
            InitialiseUserService_Success();
            int pageNumber = 1;
            int pageSize = 10;
            long agencyId = 1;
            Assert.ThrowsAny<Exception>(() => this.optionsService.GetHelperTitleList(agencyId, pageNumber, pageSize));
        }
        #endregion

        #region Get Notification Level List
        //[Fact]
        //public void getnotificationlevellist_success_returnscorrectresult()
        //{

        //    var mocknotificationlevel = getmocknotificationlevellist();
        //    this.mocknotificationlevelrepository = new mocknotificationlevelrepository().mocknotificationlevellist(mocknotificationlevel);
        //    InitialiseUserService_Success();
        //    int pagenumber = 1;
        //    int pagesize = 10;
        //    var result = this.optionsservice.getnotificationlevellist(pagenumber, pagesize);
        //    assert.equal(result.responsestatuscode, pcisenum.statuscodes.success);
        //    //assert.notnull
        //}

        [Fact]
        public void GetNotificationLevelList_Success_ReturnsNoResult()
        {
            var mockNotificationLevels = new List<NotificationLevelDTO>();
            this.mockNotificationLevelRepository = new MockNotificationLevelRepository().MockNotificationLevelList(mockNotificationLevels);
            InitialiseUserService_Success();
            int pageNumber = 1;
            int pageSize = 10;
            long agencyID = 1;
            var result = this.optionsService.GetNotificationLevelList(pageNumber, pageSize, agencyID);
            Assert.Equal(0, result.TotalCount);
            //Assert.NotNull
        }

        [Fact]
        public void GetNotificationLevelList_Failure_InvalidParameterResult()
        {
            var mockNotificationLevels = new List<NotificationLevelDTO>();
            this.mockNotificationLevelRepository = new MockNotificationLevelRepository().MockNotificationLevelList(mockNotificationLevels);
            InitialiseUserService_Success();
            int pageNumber = 0;
            int pageSize = 10;
            long agencyID = 1;
            var result = this.optionsService.GetNotificationLevelList(pageNumber, pageSize, agencyID);
            Assert.Equal(0, result.TotalCount);
            //Assert.NotNull
        }
        [Fact]
        public void GetNotificationLevelList_Failure_ExceptionResult()
        {
            List<NotificationLevelDTO> mockNotificationLevels = new List<NotificationLevelDTO>();
            this.mockNotificationLevelRepository = new MockNotificationLevelRepository().MockNotificationLevelListException(mockNotificationLevels);
            InitialiseUserService_Success();
            int pageNumber = 1;
            int pageSize = 10;
            long agencyID = 1;
            Assert.ThrowsAny<Exception>(() => this.optionsService.GetNotificationLevelList(pageNumber, pageSize, agencyID));
        }
        #endregion Get Notification Level List

        #region Add Notification Level
        [Fact]
        public void AddNotificationLevel_Success_ReturnsCorrectResult()
        {
            var mockNotificationLevel = GetMockNotificationLevel();
            this.mockNotificationLevelRepository = new MockNotificationLevelRepository().MockAddNotificationLevel(mockNotificationLevel);

            InitialiseUserService_InsertionSuccess();
            int userID = 1;
            long agencyID = 1;
            var result = this.optionsService.AddNotificationLevel(mockNotificationLevel, userID, agencyID);
            Assert.Equal(result.ResponseStatus, PCISEnum.StatusMessages.InsertionSuccess);
            //Assert.NotNull
        }

        [Fact]
        public void AddNotificationLevel_Failure_ExceptionResult()
        {
            NotificationLevelDTO mockNotificationLevel = new NotificationLevelDTO();
            this.mockNotificationLevelRepository = new MockNotificationLevelRepository().MockAddNotificationLevelException(mockNotificationLevel);
            this.mockOptionsRepository = new MockOptionsRepository().MockIsValidListOrder(true);
            InitialiseUserService_InsertionFailed();
            int userID = 1;
            long agencyID = 1;
            Assert.ThrowsAny<Exception>(() => this.optionsService.AddNotificationLevel(mockNotificationLevel, userID, agencyID));
        }
        #endregion Add Notification Level

        #region Update Notification Level
        [Fact]
        public void UpdateNotificationLevel_Success_ReturnsCorrectResult()
        {
            var mockNotificationLevelUpdateInput = GetMockNotificationLevel();
            var mockNotificationLevel = GetMockNotificationLevel();
            this.mockNotificationLevelRepository = new MockNotificationLevelRepository().MockUpdateNotificationLevel(mockNotificationLevel);
            InitialiseUserService_UpdationSuccess();
            int userID = 1;
            long agencyID = 1;
            var result = this.optionsService.UpdateNotificationLevel(mockNotificationLevelUpdateInput, userID, agencyID);
            Assert.Equal(result.ResponseStatus, PCISEnum.StatusMessages.UpdationSuccess);
        }

        [Fact]
        public void UpdateNotificationLevel_Failure_InvalidParameterResult()
        {
            var mockNotificationLevelInput = new NotificationLevelDTO();
            var mockNotificationLevel = new NotificationLevelDTO();
            this.mockNotificationLevelRepository = new MockNotificationLevelRepository().MockUpdateNotificationLevel(mockNotificationLevelInput);
            InitialiseUserService_UpdationFailed();
            int userID = 1;
            long agencyID = 1;
            var result = this.optionsService.UpdateNotificationLevel(mockNotificationLevel, userID, agencyID);
            Assert.Equal(result.ResponseStatus, PCISEnum.StatusMessages.UpdationFailed);
        }

        [Fact]
        public void UpdateNotificationLevel_Failure_ExceptionResult()
        {
            NotificationLevelDTO mockNotificationLevel = GetMockNotificationLevel();
            this.mockOptionsRepository = new MockOptionsRepository().MockIsValidListOrder(true);
            this.mockNotificationLevelRepository = new MockNotificationLevelRepository().MockUpdateNotificationLevelException(mockNotificationLevel);
            InitialiseUserService_UpdationFailed();
            int userID = 1;
            long agencyID = 1;
            Assert.ThrowsAny<Exception>(() => this.optionsService.UpdateNotificationLevel(mockNotificationLevel, userID, agencyID));
        }
        #endregion Update Notification Level

        #region Delete Notification Level
        [Fact]
        public void DeleteNotificationLevel_Success_ReturnsCorrectResult()
        {
            var mockNotificationLevelUpdateInput = GetMockNotificationLevel(); //GetMockNotificationLevelDelete();
            var NotificationLevelID = 1;
            this.mockNotificationLevelRepository = new MockNotificationLevelRepository().MockdeleteNotificationLevel(mockNotificationLevelUpdateInput);
            InitialiseUserService_DeletionSuccess();
            var result = this.optionsService.DeleteNotificationLevel(NotificationLevelID,1);
            Assert.Equal(result.ResponseStatus, PCISEnum.StatusMessages.DeletionSuccess);
        }

        [Fact]
        public void DeleteNotificationLevel_Failure_ExceptionResult()
        {
            NotificationLevelDTO mockNotificationLevel = GetMockNotificationLevel();
            var NotificationLevelID = 1;
            this.mockNotificationLevelRepository = new MockNotificationLevelRepository().MockDeleteNotificationLevelException(mockNotificationLevel);
            InitialiseUserService_DeletionFailed();
            Assert.ThrowsAny<Exception>(() => this.optionsService.DeleteNotificationLevel(NotificationLevelID,1));
        }
        #endregion Delete Notification Level

        #region Get Gender  List
        [Fact]
        public void GetGenderList_Success_ReturnsCorrectResult()
        {

            var mockGender = GetMockGenderList();
            this.mockGenderRepository = new MockGenderRepository().MockGenderList(mockGender);
            InitialiseUserService_Success();
            int pageNumber = 1;
            int pageSize = 10;
            long agencyID = 1;
            var result = this.optionsService.GetGenderList(pageNumber, pageSize, agencyID);
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.Success);
            //Assert.NotNull
        }

        [Fact]
        public void GetGenderList_Success_ReturnsNoResult()
        {
            var mockGenders = new List<GenderDTO>();
            this.mockGenderRepository = new MockGenderRepository().MockGenderList(mockGenders);
            InitialiseUserService_Success();
            int pageNumber = 1;
            int pageSize = 10;
            long agencyID = 1;
            var result = this.optionsService.GetGenderList(pageNumber, pageSize, agencyID);
            Assert.Equal(0, result.TotalCount);
            //Assert.NotNull
        }

        [Fact]
        public void GetGenderList_Failure_InvalidParameterResult()
        {
            var mockGenders = new List<GenderDTO>();
            this.mockGenderRepository = new MockGenderRepository().MockGenderList(mockGenders);
            InitialiseUserService_Success();
            int pageNumber = 0;
            int pageSize = 10;
            long agencyID = 1;
            var result = this.optionsService.GetGenderList(pageNumber, pageSize, agencyID);
            Assert.Equal(0, result.TotalCount);
            //Assert.NotNull
        }
        [Fact]
        public void GetGenderList_Failure_ExceptionResult()
        {
            List<GenderDTO> mockGenders = new List<GenderDTO>();
            this.mockGenderRepository = new MockGenderRepository().MockGenderListException(mockGenders);
            InitialiseUserService_Success();
            int pageNumber = 1;
            int pageSize = 10;
            long agencyID = 1;
            Assert.ThrowsAny<Exception>(() => this.optionsService.GetGenderList(pageNumber, pageSize, agencyID));
        }
        #endregion Get Gender  List

        #region Add Gender 
        [Fact]
        public void AddGender_Success_ReturnsCorrectResult()
        {
            var mockGender = GetMockGender();
            this.mockGenderRepository = new MockGenderRepository().MockAddGender(mockGender);
            InitialiseUserService_InsertionSuccess();
            int userID = 1;
            long agencyID = 1;
            var result = this.optionsService.AddGender(mockGender, userID, agencyID);
            Assert.Equal(result.ResponseStatus, PCISEnum.StatusMessages.InsertionSuccess);
        }

        [Fact]
        public void AddGender_Failure_ExceptionResult()
        {
            GenderDTO mockGender = new GenderDTO();
            this.mockOptionsRepository = new MockOptionsRepository().MockIsValidListOrder(true);
            this.mockGenderRepository = new MockGenderRepository().MockAddGenderException(mockGender);
            InitialiseUserService_InsertionFailed();
            int userID = 1;
            long agencyID = 1;
            Assert.ThrowsAny<Exception>(() => this.optionsService.AddGender(mockGender, userID, agencyID));
        }
        #endregion Add Gender 

        #region Update Gender 
        [Fact]
        public void UpdateGender_Success_ReturnsCorrectResult()
        {
            var mockGenderUpdateInput = GetMockGender();
            var mockGender = GetMockGender();
            this.mockGenderRepository = new MockGenderRepository().MockUpdateGender(mockGender);
            InitialiseUserService_UpdationSuccess();
            int userID = 1;
            long agencyID = 1;
            var result = this.optionsService.UpdateGender(mockGenderUpdateInput, userID, agencyID);
            Assert.Equal(result.ResponseStatus, PCISEnum.StatusMessages.UpdationSuccess);
        }

        [Fact]
        public void UpdateGender_Failure_InvalidParameterResult()
        {
            var mockGenderInput = new GenderDTO();
            var mockGender = new GenderDTO();
            this.mockGenderRepository = new MockGenderRepository().MockUpdateGender(mockGenderInput);
            InitialiseUserService_UpdationFailed();
            int userID = 1;
            long agencyID = 1;
            var result = this.optionsService.UpdateGender(mockGender, userID, agencyID);
            Assert.Equal(result.ResponseStatus, PCISEnum.StatusMessages.UpdationFailed);
        }

        [Fact]
        public void UpdateGender_Failure_ExceptionResult()
        {
            GenderDTO mockGender = GetMockGender();
            this.mockOptionsRepository = new MockOptionsRepository().MockIsValidListOrder(true);
            this.mockGenderRepository = new MockGenderRepository().MockUpdateGenderException(mockGender);
            InitialiseUserService_UpdationFailed();
            int userID = 1;
            long agencyID = 1;
            Assert.ThrowsAny<Exception>(() => this.optionsService.UpdateGender(mockGender, userID, agencyID));
        }
        #endregion Update Gender 

        #region Delete Gender 
        [Fact]
        public void DeleteGender_Success_ReturnsCorrectResult()
        {
            var mockGenderUpdateInput = GetMockGender(); //GetMockGenderDelete();
            var GenderID = 1;
            this.mockGenderRepository = new MockGenderRepository().MockdeleteGender(mockGenderUpdateInput);
            InitialiseUserService_DeletionSuccess();
            var result = this.optionsService.DeleteGender(GenderID,1);
            Assert.Equal(result.ResponseStatus, PCISEnum.StatusMessages.DeletionSuccess);
        }

        [Fact]
        public void DeleteGender_Failure_ExceptionResult()
        {
            GenderDTO mockGender = GetMockGender();
            var GenderID = 1;
            this.mockGenderRepository = new MockGenderRepository().MockDeleteGenderException(mockGender);
            InitialiseUserService_DeletionFailed();
            Assert.ThrowsAny<Exception>(() => this.optionsService.DeleteGender(GenderID,1));
        }
        #endregion Delete Gender 

        #region Get Identification Type  List
        [Fact]
        public void GetIdentificationTypeList_Success_ReturnsCorrectResult()
        {
            var mockIdentificationType = GetMockIdentificationTypeList();
            List<IdentificationType> identificationType = new List<IdentificationType>();
            this.mockMapper.Object.Map<List<IdentificationType>, List<IdentificationTypeDTO>>(identificationType, mockIdentificationType);
            this.mockIdentificationTypeRepository = new MockIdentificationTypeRepository().MockIdentificationTypeList(identificationType);
            InitialiseUserService_Success();
            int pageNumber = 1;
            int pageSize = 10;
            long agencyID = 1;
            var result = this.optionsService.GetIdentificationTypeList(pageNumber, pageSize, agencyID);
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.Success);
        }

        [Fact]
        public void GetIdentificationTypeList_Success_ReturnsNoResult()
        {
            var mockIdentificationTypes = new List<IdentificationType>();
            this.mockIdentificationTypeRepository = new MockIdentificationTypeRepository().MockIdentificationTypeList(mockIdentificationTypes);
            InitialiseUserService_Success();
            int pageNumber = 1;
            int pageSize = 10;
            long agencyID = 1;
            var result = this.optionsService.GetIdentificationTypeList(pageNumber, pageSize, agencyID);
            Assert.Equal(0, result.TotalCount);
        }

        [Fact]
        public void GetIdentificationTypeList_Failure_InvalidParameterResult()
        {
            var mockIdentificationTypes = new List<IdentificationType>();
            this.mockIdentificationTypeRepository = new MockIdentificationTypeRepository().MockIdentificationTypeList(mockIdentificationTypes);
            InitialiseUserService_Success();
            int pageNumber = 0;
            int pageSize = 10;
            long agencyID = 1;
            var result = this.optionsService.GetIdentificationTypeList(pageNumber, pageSize, agencyID);
            Assert.Equal(0, result.TotalCount);
        }
        [Fact]
        public void GetIdentificationTypeList_Failure_ExceptionResult()
        {
            List<IdentificationTypeDTO> mockIdentificationTypes = new List<IdentificationTypeDTO>();
            List<IdentificationType> identificationType = new List<IdentificationType>();
            this.mockMapper.Object.Map<List<IdentificationType>, List<IdentificationTypeDTO>>(identificationType, mockIdentificationTypes);
            this.mockIdentificationTypeRepository = new MockIdentificationTypeRepository().MockIdentificationTypeListException(identificationType);
            InitialiseUserService_Success();
            int pageNumber = 1;
            int pageSize = 10;
            long agencyID = 1;
            Assert.ThrowsAny<Exception>(() => this.optionsService.GetIdentificationTypeList(pageNumber, pageSize, agencyID));
        }
        #endregion Get Identification Type  List

        #region Add Identification Type 
        [Fact]
        public void AddIdentificationType_Success_ReturnsCorrectResult()
        {
            var mockIdentificationType = GetMockIdentificationTypeDTO();
            IdentificationType identificationType = GetMockIdentificationType();

            this.mockIdentificationTypeRepository = new MockIdentificationTypeRepository().MockAddIdentificationType(identificationType);
            InitialiseUserService_InsertionSuccess();
            int userID = 1;
            long agencyID = 1;
            var result = this.optionsService.AddIdentificationType(mockIdentificationType, userID, agencyID);
            Assert.Equal(result.ResponseStatus, PCISEnum.StatusMessages.InsertionSuccess);
            //Assert.NotNull
        }

        [Fact]
        public void AddIdentificationType_Failure_ExceptionResult()
        {
            IdentificationTypeDTO mockIdentificationType = new IdentificationTypeDTO();
            this.mockOptionsRepository = new MockOptionsRepository().MockIsValidListOrder(true);
            this.mockIdentificationTypeRepository = new MockIdentificationTypeRepository().MockAddIdentificationTypeException();
            InitialiseUserService_InsertionFailed();
            int userID = 1;
            long agencyID = 1;
            Assert.ThrowsAny<Exception>(() => this.optionsService.AddIdentificationType(mockIdentificationType, userID, agencyID));
        }
        #endregion Add Identification Type 

        #region Update Identification Type 
        [Fact]
        public void UpdateIdentificationType_Success_ReturnsCorrectResult()
        {
            var mockIdentificationType = GetMockIdentificationTypeDTO();
            IdentificationType identificationType = GetMockIdentificationType();
            this.mockIdentificationTypeRepository = new MockIdentificationTypeRepository().MockUpdateIdentificationType(identificationType);
            InitialiseUserService_UpdationSuccess();
            int userID = 1;
            long agencyID = 1;
            var result = this.optionsService.UpdateIdentificationType(mockIdentificationType, userID, agencyID);
            Assert.Equal(result.ResponseStatus, PCISEnum.StatusMessages.UpdationSuccess);
        }

        [Fact]
        public void UpdateIdentificationType_Failure_InvalidParameterResult()
        {
            var mockIdentificationTypeInput = new IdentificationType();
            var mockIdentificationType = new IdentificationTypeDTO();
            this.mockIdentificationTypeRepository = new MockIdentificationTypeRepository().MockUpdateIdentificationType(mockIdentificationTypeInput);
            InitialiseUserService_UpdationFailed();
            int userID = 1;
            long agencyID = 1;
            var result = this.optionsService.UpdateIdentificationType(mockIdentificationType, userID, agencyID);
            Assert.Equal(result.ResponseStatus, PCISEnum.StatusMessages.UpdationFailed);
        }

        [Fact]
        public void UpdateIdentificationType_Failure_ExceptionResult()
        {
            IdentificationTypeDTO mockIdentificationType = GetMockIdentificationTypeDTO();
            this.mockOptionsRepository = new MockOptionsRepository().MockIsValidListOrder(true);
            this.mockIdentificationTypeRepository = new MockIdentificationTypeRepository().MockUpdateIdentificationTypeException();
            InitialiseUserService_UpdationFailed();
            int userID = 1;
            long agencyID = 1;
            Assert.ThrowsAny<Exception>(() => this.optionsService.UpdateIdentificationType(mockIdentificationType, userID, agencyID));
        }
        #endregion Update Identification Type 

        #region Delete Identification Type 
        [Fact]
        public void DeleteIdentificationType_Success_ReturnsCorrectResult()
        {
            var IdentificationTypeID = 1;
            IdentificationType identificationType = GetMockIdentificationType();
            this.mockIdentificationTypeRepository = new MockIdentificationTypeRepository().MockdeleteIdentificationType(identificationType);
            InitialiseUserService_DeletionSuccess();
            var result = this.optionsService.DeleteIdentificationType(IdentificationTypeID,1);
            Assert.Equal(result.ResponseStatus, PCISEnum.StatusMessages.DeletionSuccess);
        }

        [Fact]
        public void DeleteIdentificationType_Failure_ExceptionResult()
        {
            var IdentificationTypeID = 1;
            this.mockIdentificationTypeRepository = new MockIdentificationTypeRepository().MockDeleteIdentificationTypeException();
            InitialiseUserService_DeletionFailed();
            Assert.ThrowsAny<Exception>(() => this.optionsService.DeleteIdentificationType(IdentificationTypeID,1));
        }
        #endregion Delete Identification Type 

        #region Get Race Ethnicity  List
        [Fact]
        public void GetRaceEthnicityList_Success_ReturnsCorrectResult()
        {
            var mockRaceEthnicity = GetMockRaceEthnicityList();
            List<RaceEthnicity> identificationType = new List<RaceEthnicity>();
            this.mockMapper.Object.Map<List<RaceEthnicity>, List<RaceEthnicityDTO>>(identificationType, mockRaceEthnicity);
            this.mockRaceEthnicityRepository = new MockRaceEthnicityRepository().MockRaceEthnicityList(identificationType);
            InitialiseUserService_Success();
            int pageNumber = 1;
            int pageSize = 10;
            long agencyID = 1;
            var result = this.optionsService.GetRaceEthnicityList(pageNumber, pageSize, agencyID);
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.Success);
        }

        [Fact]
        public void GetRaceEthnicityList_Success_ReturnsNoResult()
        {
            var mockRaceEthnicitys = new List<RaceEthnicity>();
            this.mockRaceEthnicityRepository = new MockRaceEthnicityRepository().MockRaceEthnicityList(mockRaceEthnicitys);
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Success);
            InitialiseUserService_Success();
            int pageNumber = 1;
            int pageSize = 10;
            long agencyID = 1;
            var result = this.optionsService.GetRaceEthnicityList(pageNumber, pageSize, agencyID);
            Assert.Equal(0, result.TotalCount);
        }

        [Fact]
        public void GetRaceEthnicityList_Failure_InvalidParameterResult()
        {
            var mockRaceEthnicitys = new List<RaceEthnicity>();
            this.mockRaceEthnicityRepository = new MockRaceEthnicityRepository().MockRaceEthnicityList(mockRaceEthnicitys);
            InitialiseUserService_Success();
            int pageNumber = 0;
            int pageSize = 10;
            long agencyID = 1;
            var result = this.optionsService.GetRaceEthnicityList(pageNumber, pageSize, agencyID);
            Assert.Equal(0, result.TotalCount);
        }
        [Fact]
        public void GetRaceEthnicityList_Failure_ExceptionResult()
        {
            List<RaceEthnicityDTO> mockRaceEthnicitys = new List<RaceEthnicityDTO>();
            List<RaceEthnicity> identificationType = new List<RaceEthnicity>();
            this.mockMapper.Object.Map<List<RaceEthnicity>, List<RaceEthnicityDTO>>(identificationType, mockRaceEthnicitys);
            this.mockRaceEthnicityRepository = new MockRaceEthnicityRepository().MockRaceEthnicityListException(identificationType);
            InitialiseUserService_Success();
            int pageNumber = 1;
            int pageSize = 10;
            long agencyID = 1;
            Assert.ThrowsAny<Exception>(() => this.optionsService.GetRaceEthnicityList(pageNumber, pageSize, agencyID));
        }
        #endregion Get Race Ethnicity  List

        #region Add Race Ethnicity 
        [Fact]
        public void AddRaceEthnicity_Success_ReturnsCorrectResult()
        {
            var mockRaceEthnicity = GetMockRaceEthnicityDTO();
            RaceEthnicity identificationType = GetMockRaceEthnicity();
            this.mockRaceEthnicityRepository = new MockRaceEthnicityRepository().MockAddRaceEthnicity(identificationType);
            InitialiseUserService_InsertionSuccess();
            int userID = 1;
            long agencyID = 1;
            var result = this.optionsService.AddRaceEthnicity(mockRaceEthnicity, userID, agencyID);
            Assert.Equal(result.ResponseStatus, PCISEnum.StatusMessages.InsertionSuccess);
        }

        [Fact]
        public void AddRaceEthnicity_Failure_ExceptionResult()
        {
            RaceEthnicityDTO mockRaceEthnicity = new RaceEthnicityDTO();
            this.mockRaceEthnicityRepository = new MockRaceEthnicityRepository().MockAddRaceEthnicityException();
            this.mockOptionsRepository = new MockOptionsRepository().MockIsValidListOrder(true);
            InitialiseUserService_InsertionFailed();
            int userID = 1;
            long agencyID = 1;
            Assert.ThrowsAny<Exception>(() => this.optionsService.AddRaceEthnicity(mockRaceEthnicity, userID, agencyID));
        }
        #endregion Add Race Ethnicity 

        #region Update Race Ethnicity 
        [Fact]
        public void UpdateRaceEthnicity_Success_ReturnsCorrectResult()
        {
            var mockRaceEthnicity = GetMockRaceEthnicityDTO();
            RaceEthnicity identificationType = GetMockRaceEthnicity();
            this.mockRaceEthnicityRepository = new MockRaceEthnicityRepository().MockUpdateRaceEthnicity(identificationType);
            InitialiseUserService_UpdationSuccess();
            int userID = 1;
            long agencyID = 1;
            var result = this.optionsService.UpdateRaceEthnicity(mockRaceEthnicity, userID, agencyID);
            Assert.Equal(result.ResponseStatus, PCISEnum.StatusMessages.UpdationSuccess);
        }

        [Fact]
        public void UpdateRaceEthnicity_Failure_InvalidParameterResult()
        {
            var mockRaceEthnicityInput = new RaceEthnicity();
            var mockRaceEthnicity = new RaceEthnicityDTO();
            this.mockRaceEthnicityRepository = new MockRaceEthnicityRepository().MockUpdateRaceEthnicity(mockRaceEthnicityInput);
            InitialiseUserService_UpdationFailed();
            int userID = 1;
            long agencyID = 1;
            var result = this.optionsService.UpdateRaceEthnicity(mockRaceEthnicity, userID, agencyID);
            Assert.Equal(result.ResponseStatus, PCISEnum.StatusMessages.UpdationFailed);
        }

        [Fact]
        public void UpdateRaceEthnicity_Failure_ExceptionResult()
        {
            RaceEthnicityDTO mockRaceEthnicity = GetMockRaceEthnicityDTO();
            this.mockOptionsRepository = new MockOptionsRepository().MockIsValidListOrder(true);
            this.mockRaceEthnicityRepository = new MockRaceEthnicityRepository().MockUpdateRaceEthnicityException();
            InitialiseUserService_UpdationFailed();
            int userID = 1;
            long agencyID = 1;
            Assert.ThrowsAny<Exception>(() => this.optionsService.UpdateRaceEthnicity(mockRaceEthnicity, userID, agencyID));
        }
        #endregion Update Race Ethnicity 

        #region Delete Race Ethnicity 
        [Fact]
        public void DeleteRaceEthnicity_Success_ReturnsCorrectResult()
        {
            var RaceEthnicityID = 1;
            RaceEthnicity identificationType = GetMockRaceEthnicity();
            this.mockRaceEthnicityRepository = new MockRaceEthnicityRepository().MockdeleteRaceEthnicity(identificationType);
            InitialiseUserService_DeletionSuccess();
            var result = this.optionsService.DeleteRaceEthnicity(RaceEthnicityID,1);
            Assert.Equal(result.ResponseStatus, PCISEnum.StatusMessages.DeletionSuccess);
        }

        [Fact]
        public void DeleteRaceEthnicity_Failure_ExceptionResult()
        {
            var RaceEthnicityID = 1;
            this.mockRaceEthnicityRepository = new MockRaceEthnicityRepository().MockDeleteRaceEthnicityException();
            InitialiseUserService_DeletionFailed();
            Assert.ThrowsAny<Exception>(() => this.optionsService.DeleteRaceEthnicity(RaceEthnicityID,1));
        }
        #endregion Delete Race Ethnicity 

        #region Get Support Type  List
        [Fact]
        public void GetSupportTypeList_Success_ReturnsCorrectResult()
        {
            var mockSupportType = GetMockSupportTypeList();
            List<SupportType> supportType = new List<SupportType>();
            this.mockMapper.Object.Map<List<SupportType>, List<SupportTypeDTO>>(supportType, mockSupportType);
            this.mockSupportTypeRepository = new MockSupportTypeRepository().MockSupportTypeList(supportType);
            InitialiseUserService_Success();
            int pageNumber = 1;
            int pageSize = 10;
            long agencyID = 1;
            var result = this.optionsService.GetSupportTypeList(pageNumber, pageSize, agencyID);
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.Success);
        }

        [Fact]
        public void GetSupportTypeList_Success_ReturnsNoResult()
        {
            var mockSupportTypes = new List<SupportType>();
            this.mockSupportTypeRepository = new MockSupportTypeRepository().MockSupportTypeList(mockSupportTypes);
            InitialiseUserService_Success();
            int pageNumber = 1;
            int pageSize = 10;
            long agencyID = 1;
            var result = this.optionsService.GetSupportTypeList(pageNumber, pageSize, agencyID);
            Assert.Equal(0, result.TotalCount);
        }

        [Fact]
        public void GetSupportTypeList_Failure_InvalidParameterResult()
        {
            var mockSupportTypes = new List<SupportType>();
            this.mockSupportTypeRepository = new MockSupportTypeRepository().MockSupportTypeList(mockSupportTypes);
            InitialiseUserService_Success();
            int pageNumber = 0;
            int pageSize = 10;
            long agencyID = 1;
            var result = this.optionsService.GetSupportTypeList(pageNumber, pageSize, agencyID);
            Assert.Equal(0, result.TotalCount);
            //Assert.NotNull
        }
        [Fact]
        public void GetSupportTypeList_Failure_ExceptionResult()
        {
            List<SupportTypeDTO> mockSupportTypes = new List<SupportTypeDTO>();
            List<SupportType> supportType = new List<SupportType>();
            this.mockMapper.Object.Map<List<SupportType>, List<SupportTypeDTO>>(supportType, mockSupportTypes);
            this.mockSupportTypeRepository = new MockSupportTypeRepository().MockSupportTypeListException(supportType);
            InitialiseUserService_Success();
            int pageNumber = 1;
            int pageSize = 10;
            long agencyID = 1;
            Assert.ThrowsAny<Exception>(() => this.optionsService.GetSupportTypeList(pageNumber, pageSize, agencyID));
        }
        #endregion Get Support Type  List

        #region Add Support Type 
        [Fact]
        public void AddSupportType_Success_ReturnsCorrectResult()
        {
            var mockSupportType = GetMockSupportTypeDTO();
            SupportType supportType = GetMockSupportType();
            this.mockSupportTypeRepository = new MockSupportTypeRepository().MockAddSupportType(supportType);
            InitialiseUserService_InsertionSuccess();
            int userID = 1;
            long agencyID = 1;
            var result = this.optionsService.AddSupportType(mockSupportType, userID, agencyID);
            Assert.Equal(result.ResponseStatus, PCISEnum.StatusMessages.InsertionSuccess);
        }

        [Fact]
        public void AddSupportType_Failure_ExceptionResult()
        {
            SupportTypeDTO mockSupportType = new SupportTypeDTO();
            this.mockSupportTypeRepository = new MockSupportTypeRepository().MockAddSupportTypeException();
            this.mockOptionsRepository = new MockOptionsRepository().MockIsValidListOrder(true);
            InitialiseUserService_InsertionFailed();
            int userID = 1;
            long agencyID = 1;
            Assert.ThrowsAny<Exception>(() => this.optionsService.AddSupportType(mockSupportType, userID, agencyID));
        }
        #endregion Add Support Type 

        #region Update Support Type 
        [Fact]
        public void UpdateSupportType_Success_ReturnsCorrectResult()
        {
            var mockSupportType = GetMockSupportTypeDTO();

            SupportType supportType = GetMockSupportType();

            this.mockSupportTypeRepository = new MockSupportTypeRepository().MockUpdateSupportType(supportType);
            InitialiseUserService_UpdationSuccess();
            int userID = 1;
            long agencyID = 1;
            var result = this.optionsService.UpdateSupportType(mockSupportType, userID, agencyID);
            Assert.Equal(result.ResponseStatus, PCISEnum.StatusMessages.UpdationSuccess);
        }

        [Fact]
        public void UpdateSupportType_Failure_InvalidParameterResult()
        {
            var mockSupportTypeInput = new SupportType();
            var mockSupportType = new SupportTypeDTO();

            this.mockSupportTypeRepository = new MockSupportTypeRepository().MockUpdateSupportType(mockSupportTypeInput);
            InitialiseUserService_UpdationFailed();
            int userID = 1;
            long agencyID = 1;

            var result = this.optionsService.UpdateSupportType(mockSupportType, userID, agencyID);
            Assert.Equal(result.ResponseStatus, PCISEnum.StatusMessages.UpdationFailed);
        }

        [Fact]
        public void UpdateSupportType_Failure_ExceptionResult()
        {
            SupportTypeDTO mockSupportType = GetMockSupportTypeDTO();
            this.mockOptionsRepository = new MockOptionsRepository().MockIsValidListOrder(true);
            this.mockSupportTypeRepository = new MockSupportTypeRepository().MockUpdateSupportTypeException();
            InitialiseUserService_UpdationFailed();
            int userID = 1;
            long agencyID = 1;
            Assert.ThrowsAny<Exception>(() => this.optionsService.UpdateSupportType(mockSupportType, userID, agencyID));
        }
        #endregion Update Support Type 

        #region Delete Support Type 
        [Fact]
        public void DeleteSupportType_Success_ReturnsCorrectResult()
        {
            var SupportTypeID = 1;
            SupportType supportType = GetMockSupportType();
            this.mockSupportTypeRepository = new MockSupportTypeRepository().MockdeleteSupportType(supportType);
            InitialiseUserService_DeletionSuccess();
            var result = this.optionsService.DeleteSupportType(SupportTypeID,1);
            Assert.Equal(result.ResponseStatus, PCISEnum.StatusMessages.DeletionSuccess);
        }

        [Fact]
        public void DeleteSupportType_Failure_ExceptionResult()
        {
            var SupportTypeID = 1;
            this.mockSupportTypeRepository = new MockSupportTypeRepository().MockDeleteSupportTypeException();
            InitialiseUserService_DeletionFailed();
            Assert.ThrowsAny<Exception>(() => this.optionsService.DeleteSupportType(SupportTypeID,1));
        }
        #endregion Delete Support Type 

        #region Update Sexuality
        [Fact]
        public void UpdateSexuality_Failure_ExceptionResult()
        {
            var mockeditSexuality = GetMockEditSexuality();
            this.mockOptionsRepository = new MockOptionsRepository().MockIsValidListOrder(true);
            this.mock_SexualityRepository = new MockSexualityRepository().MockEditSexualityException();
            InitialiseUserService_UpdationFailed();
            Assert.ThrowsAny<Exception>(() => this.optionsService.UpdateSexuality(mockeditSexuality, 0, 0));
        }

        [Fact]
        public void UpdateSexuality_Success()
        {
            var mockSexuality = GetMockSexuality();
            var mockeditSexuality = GetMockEditSexuality();
            this.mock_SexualityRepository = new MockSexualityRepository().MockEditSexuality(mockSexuality);
            InitialiseUserService_UpdationSuccess();
            var result = this.optionsService.UpdateSexuality(mockeditSexuality, 1, 1);
            Assert.Equal(result.ResponseStatus, PCISEnum.StatusMessages.UpdationSuccess);
        }
        #endregion Update Sexuality

        #region Delete Sexuality
        [Fact]
        public void RemoveSexuality_Failure_ExceptionResult()
        {
            var mockSexuality = GetMockSexuality();
            this.mock_SexualityRepository = new MockSexualityRepository().MockEditSexuality(mockSexuality);
            InitialiseUserService_DeletionFailed();
            var result = this.optionsService.RemoveSexuality(0,1);
            Assert.Equal(result.ResponseStatus, PCISEnum.StatusMessages.DeletionFailed);
        }

        [Fact]
        public void RemoveSexuality_Success()
        {
            var mockSexuality = GetMockSexuality();
            this.mock_SexualityRepository = new MockSexualityRepository().MockEditSexuality(mockSexuality);
            InitialiseUserService_DeletionSuccess();
            var result = this.optionsService.RemoveSexuality(1,1);
            Assert.Equal(result.ResponseStatus, PCISEnum.StatusMessages.DeletionSuccess);
        }
        #endregion Delete Sexuality

        #region Add Sexuality        

        [Fact]
        public void AddSexuality_Success_ReturnsCorrectResult()
        {
            var mockSexualityInputDTO = MockSexualityInputDTO();
            var mockSexuality = MockSexuality();

            this.mock_SexualityRepository = new MockSexualityRepository().MockAddSexuality(mockSexuality);
            InitialiseUserService_InsertionSuccess();
            long agencyID = 1;
            int updateUserID = 2;
            var result = this.optionsService.AddSexuality(mockSexualityInputDTO, agencyID, updateUserID);
            Assert.Equal(result.ResponseStatus, PCISEnum.StatusMessages.InsertionSuccess);
        }

        [Fact]
        public void AddSexuality_Failure()
        {
            var mockSexualityInputDTO = MockSexualityInputDTO();
            var mockSexuality = new Sexuality();
            this.mock_SexualityRepository = new MockSexualityRepository().MockAddSexuality(mockSexuality);
            InitialiseUserService_InsertionFailed();
            long agencyID = 1;
            int updateUserID = 2;
            var result = this.optionsService.AddSexuality(mockSexualityInputDTO, agencyID, updateUserID);
            Assert.Equal(result.ResponseStatus, PCISEnum.StatusMessages.insertionFailed);
        }

        [Fact]
        public void AddSexuality_ExceptionResult()
        {
            var mockSexualityInputDTO = new SexualityInputDTO();
            this.mock_SexualityRepository = new MockSexualityRepository().MockAddSexualityException();
            this.mockOptionsRepository = new MockOptionsRepository().MockIsValidListOrder(true);
            InitialiseUserService_InsertionFailed();
            long agencyID = 1;
            int updateUserID = 2;
            Assert.ThrowsAny<Exception>(() => this.optionsService.AddSexuality(mockSexualityInputDTO, agencyID, updateUserID));
        }
        #endregion Add Sexuality 

        #region Get Sexuality List
        [Fact]
        public void GetSexualityList_Success_ReturnsCorrectResult()
        {
            var mockSexuality = GetMockSexualityList();
            List<Sexuality> sexuality = new List<Sexuality>();
            this.mockMapper.Object.Map<List<Sexuality>, List<SexualityDTO>>(sexuality, mockSexuality);
            this.mockSexualityRepository = new MockSexualityRepository().MockSexualityList(sexuality);
            InitialiseUserService_Success();
            int pageNumber = 1;
            int pageSize = 10;
            long agencyID = 1;
            var result = this.optionsService.GetSexualityList(pageNumber, pageSize, agencyID);
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.Success);
        }

        [Fact]
        public void GetSexualityList_Success_ReturnsNoResult()
        {
            var mockSexuality = new List<Sexuality>();
            this.mock_SexualityRepository = new MockSexualityRepository().MockSexualityList(mockSexuality);
            InitialiseUserService_Success();
            int pageNumber = 1;
            int pageSize = 10;
            long agencyID = 1;
            var result = this.optionsService.GetSexualityList(pageNumber, pageSize, agencyID);
            Assert.Equal(0, result.TotalCount);
        }

        [Fact]
        public void GetSexualityList_Failure_InvalidParameterResult()
        {
            var mockSexuality = new List<Sexuality>();
            this.mock_SexualityRepository = new MockSexualityRepository().MockSexualityList(mockSexuality);
            InitialiseUserService_Success();
            int pageNumber = 0;
            int pageSize = 10;
            long agencyID = 1;
            var result = this.optionsService.GetSexualityList(pageNumber, pageSize, agencyID);
            Assert.Equal(0, result.TotalCount);
        }
        [Fact]
        public void GetSexualityList_Failure_ExceptionResult()
        {
            List<SexualityDTO> mockSexuality = new List<SexualityDTO>();
            List<Sexuality> sexuality = new List<Sexuality>();
            this.mockMapper.Object.Map<List<Sexuality>, List<SexualityDTO>>(sexuality, mockSexuality);
            this.mock_SexualityRepository = new MockSexualityRepository().MockSexualityListException(sexuality);
            InitialiseUserService_Success();
            int pageNumber = 1;
            int pageSize = 10;
            long agencyID = 1;
            Assert.ThrowsAny<Exception>(() => this.optionsService.GetSexualityList(pageNumber, pageSize, agencyID));
        }
        #endregion Get Sexuality  List

        #region Get Identified Gender  List
        [Fact]
        public void GetIdentifiedGenderList_Success_ReturnsCorrectResult()
        {
            var mockIdentifiedGender = GetMockIdentifiedGenderList();
            this.mockIdentifiedGenderRepository = new MockIdentifiedGenderRepository().MockIdentifiedGenderList(mockIdentifiedGender);
            InitialiseUserService_Success();
            int pageNumber = 1;
            int pageSize = 10;
            long agencyID = 1;
            var result = this.optionsService.GetIdentifiedGenderList(pageNumber, pageSize, agencyID);
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.Success);
        }

        [Fact]
        public void GetIdentifiedGenderList_Success_ReturnsNoResult()
        {
            var mockIdentifiedGenders = new List<IdentifiedGender>();
            this.mockIdentifiedGenderRepository = new MockIdentifiedGenderRepository().MockIdentifiedGenderList(mockIdentifiedGenders);
            InitialiseUserService_Success();
            int pageNumber = 1;
            int pageSize = 10;
            long agencyID = 1;
            var result = this.optionsService.GetIdentifiedGenderList(pageNumber, pageSize, agencyID);
            Assert.Equal(0, result.TotalCount);
        }

        [Fact]
        public void GetIdentifiedGenderList_Failure_InvalidParameterResult()
        {
            var mockIdentifiedGenders = new List<IdentifiedGender>();
            this.mockIdentifiedGenderRepository = new MockIdentifiedGenderRepository().MockIdentifiedGenderList(mockIdentifiedGenders);
            InitialiseUserService_Success();
            int pageNumber = 0;
            int pageSize = 10;
            long agencyID = 1;
            var result = this.optionsService.GetIdentifiedGenderList(pageNumber, pageSize, agencyID);
            Assert.Equal(0, result.TotalCount);
            //Assert.NotNull
        }
        [Fact]
        public void GetIdentifiedGenderList_Failure_ExceptionResult()
        {
            this.mockIdentifiedGenderRepository = new MockIdentifiedGenderRepository().MockIdentifiedGenderListException();
            InitialiseUserService_Success();
            int pageNumber = 1;
            int pageSize = 10;
            long agencyID = 1;
            Assert.ThrowsAny<Exception>(() => this.optionsService.GetIdentifiedGenderList(pageNumber, pageSize, agencyID));
        }
        #endregion Get Gender  List

        #region Add Identified Gender 
        [Fact]
        public void AddIdentifiedGender_Success_ReturnsCorrectResult()
        {
            var mockIdentifiedGender = GetMockIdentifiedGender();
            var mockIdentifiedGenderDTO = GetMockIdentifiedGenderDTO();
            this.mockIdentifiedGenderRepository = new MockIdentifiedGenderRepository().MockAddIdentifiedGender(mockIdentifiedGender);
            InitialiseUserService_InsertionSuccess();
            int userID = 1;
            long agencyID = 1;
            var result = this.optionsService.AddIdentifiedGender(mockIdentifiedGenderDTO, userID, agencyID);
            Assert.Equal(result.ResponseStatus, PCISEnum.StatusMessages.InsertionSuccess);
        }

        [Fact]
        public void AddIdentifiedGender_Failure_ExceptionResult()
        {
            IdentifiedGenderDTO mockIdentifiedGender = new IdentifiedGenderDTO();
            this.mockIdentifiedGenderRepository = new MockIdentifiedGenderRepository().MockAddIdentifiedGenderException();
            this.mockOptionsRepository = new MockOptionsRepository().MockIsValidListOrder(true);
            InitialiseUserService_InsertionFailed();
            int userID = 1;
            long agencyID = 1;
            Assert.ThrowsAny<Exception>(() => this.optionsService.AddIdentifiedGender(mockIdentifiedGender, userID, agencyID));
        }
        #endregion Add Identified Gender 

        #region Update Identified Gender 
        [Fact]
        public void UpdateIdentifiedGender_Success_ReturnsCorrectResult()
        {
            var mockIdentifiedGenderUpdateInput = GetMockIdentifiedGenderDTO();
            var mockIdentifiedGender = GetMockIdentifiedGender();
            this.mockIdentifiedGenderRepository = new MockIdentifiedGenderRepository().MockUpdateIdentifiedGender(mockIdentifiedGender);
            InitialiseUserService_UpdationSuccess();
            int userID = 1;
            long agencyID = 1;
            var result = this.optionsService.UpdateIdentifiedGender(mockIdentifiedGenderUpdateInput, userID, agencyID);
            Assert.Equal(result.ResponseStatus, PCISEnum.StatusMessages.UpdationSuccess);
        }

        [Fact]
        public void UpdateIdentifiedGender_Failure_InvalidParameterResult()
        {
            var mockIdentifiedGenderInput = new IdentifiedGender();
            var mockIdentifiedGender = new IdentifiedGenderDTO();
            this.mockIdentifiedGenderRepository = new MockIdentifiedGenderRepository().MockUpdateIdentifiedGender(mockIdentifiedGenderInput);
            InitialiseUserService_UpdationFailed();
            int userID = 1;
            long agencyID = 1;
            var result = this.optionsService.UpdateIdentifiedGender(mockIdentifiedGender, userID, agencyID);
            Assert.Equal(result.ResponseStatus, PCISEnum.StatusMessages.UpdationFailed);
        }

        [Fact]
        public void UpdateIdentifiedGender_Failure_ExceptionResult()
        {
            IdentifiedGenderDTO mockIdentifiedGender = GetMockIdentifiedGenderDTO();
            this.mockIdentifiedGenderRepository = new MockIdentifiedGenderRepository().MockUpdateIdentifiedGenderException();
            this.mockOptionsRepository = new MockOptionsRepository().MockIsValidListOrder(true);
            InitialiseUserService_UpdationFailed();
            int userID = 1;
            long agencyID = 1;
            Assert.ThrowsAny<Exception>(() => this.optionsService.UpdateIdentifiedGender(mockIdentifiedGender, userID, agencyID));
        }
        #endregion Update Identified Gender 

        #region Delete IdentifiedGender 
        [Fact]
        public void DeleteIdentifiedGender_Success_ReturnsCorrectResult()
        {
            var mockIdentifiedGenderUpdateInput = GetMockIdentifiedGender(); //GetMockGenderDelete();
            var identifiedGenderID = 1;
            this.mockIdentifiedGenderRepository = new MockIdentifiedGenderRepository().MockDeleteIdentifiedGender(mockIdentifiedGenderUpdateInput);
            InitialiseUserService_DeletionSuccess();
            var result = this.optionsService.DeleteIdentifiedGender(identifiedGenderID,1);
            Assert.Equal(result.ResponseStatus, PCISEnum.StatusMessages.DeletionSuccess);
        }

        [Fact]
        public void DeleteIdentifiedGender_Failure_ExceptionResult()
        {
            var mockIdentifiedGender = GetMockIdentifiedGender();
            var identifiedGenderID = 1;
            this.mockIdentifiedGenderRepository = new MockIdentifiedGenderRepository().MockDeleteIdentifiedGenderException(mockIdentifiedGender);
            InitialiseUserService_DeletionFailed();
            Assert.ThrowsAny<Exception>(() => this.optionsService.DeleteIdentifiedGender(identifiedGenderID,1));
        }
        #endregion Delete IdentifiedGender 

        #region Mock Data
        private List<CollaborationTagTypeDTO> GetMockCollaborationTagTypes()
        {
            return new List<CollaborationTagTypeDTO>()
            {
                new CollaborationTagTypeDTO()
                {
                    CollaborationTagTypeID=1,
                    Name="Medicaid",
                    Abbrev="MED",
                    Description="",
                    ListOrder=1,
                    IsRemoved=false,
                    UpdateDate=DateTime.UtcNow,
                    UpdateUserID=1,
                    AgencyID=1
                },
                new CollaborationTagTypeDTO()
                {
                    CollaborationTagTypeID=2,
                    Name="Child Welfare",
                    Abbrev="CHW",
                    Description="",
                    ListOrder=2,
                    IsRemoved=false,
                    UpdateDate=DateTime.UtcNow,
                    UpdateUserID=1,
                    AgencyID=1
                }
            };
        }

        private CollaborationTagTypeDetailsDTO GetMockCollaborationTagType()
        {
            CollaborationTagTypeDetailsDTO collaborationTagTypeDetailsDTO = new CollaborationTagTypeDetailsDTO()
            {
                CollaborationTagTypeID = 2,
                Name = "Probation",
                Abbrev = "PRB",
                Description = "",
                ListOrder = 2,
                UpdateUserID = 1,
                AgencyID = 1
            };
            return collaborationTagTypeDetailsDTO;
        }

        private List<CollaborationLevelDTO> GetMockCollaborationLevelList()
        {
            return new List<CollaborationLevelDTO>()
            {
                new CollaborationLevelDTO()
                {
                    CollaborationLevelID= 1,
                    Name= "Low Intensity",
                    Abbrev= null,
                    ListOrder= 1,
                    AgencyID= 1,
                    Description= null,
                    UpdateUserID= 1,
                    IsRemoved= false,
                    UpdateDate= DateTime.Now
                },

                new CollaborationLevelDTO()
                    {
                    CollaborationLevelID= 2,
                    Name= "Residential",
                    Abbrev= null,
                    ListOrder= 10,
                    AgencyID= 1,
                    Description= null,
                    UpdateUserID= 1,
                    IsRemoved= false,
                    UpdateDate=  DateTime.Now
                },
                new CollaborationLevelDTO()
                    {
                    CollaborationLevelID= 3,
                    Name= "Community Based Programs",
                    Abbrev= null,
                    ListOrder= 100,
                    AgencyID= 1,
                    Description= null,
                    UpdateUserID= 1,
                    IsRemoved= false,
                    UpdateDate=  DateTime.Now
                },
                new CollaborationLevelDTO()
                    {
                    CollaborationLevelID= 4,
                    Name= "Foster Care",
                    Abbrev= null,
                    ListOrder= 101,
                    AgencyID= 1,
                    Description= null,
                    UpdateUserID= 1,
                    IsRemoved= false,
                    UpdateDate= DateTime.Now
                },
                new CollaborationLevelDTO()
                    {
                    CollaborationLevelID= 5,
                    Name= "Mental Health",
                    Abbrev= null,
                    ListOrder= 102,
                    AgencyID= 1,
                    Description= null,
                    UpdateUserID= 1,
                    IsRemoved= false,
                    UpdateDate= DateTime.Now
                },
                new CollaborationLevelDTO()
                    {
                    CollaborationLevelID= 6,
                    Name= "sABA",
                    Abbrev= null,
                    ListOrder= 103,
                    AgencyID= 1,
                    Description= null,
                    UpdateUserID= 1,
                    IsRemoved= false,
                    UpdateDate=  DateTime.Now
                }
            };
        }
        private CollaborationLevelDTO GetMockCollaborationLevel()
        {
            CollaborationLevelDTO collaborationLevelDTO = new CollaborationLevelDTO()
            {
                CollaborationLevelID = 6,
                Name = "sABA",
                Abbrev = null,
                ListOrder = 103,
                AgencyID = 1,
                Description = null,
                UpdateUserID = 1,
                IsRemoved = false,
                UpdateDate = DateTime.Now
            };
            return collaborationLevelDTO;
        }

        private CollaborationLevelDTO GetMockCollaborationLevelUpdateInput()
        {
            CollaborationLevelDTO collaborationLevelDTO = new CollaborationLevelDTO()
            {
                CollaborationLevelID = 6,
                Name = "Test 7",
                Abbrev = null,
                ListOrder = 103,
                AgencyID = 1,
                Description = null,
                UpdateUserID = 1,
                IsRemoved = false,
                UpdateDate = DateTime.Now
            };
            return collaborationLevelDTO;
        }
        private CollaborationLevelDTO GetMockCollaborationLevelUpdateInvalidInput()
        {
            CollaborationLevelDTO collaborationLevelDTO = new CollaborationLevelDTO()
            {
                CollaborationLevelID = 0,
                Name = "Test 7",
                Abbrev = null,
                ListOrder = 0,
                AgencyID = 0,
                Description = null,
                IsRemoved = false,
                UpdateDate = DateTime.Now
            };
            return collaborationLevelDTO;
        }

        private CollaborationLevelDTO GetMockCollaborationLevelDelete()
        {
            CollaborationLevelDTO collaborationLevelDTO = new CollaborationLevelDTO()
            {
                CollaborationLevelID = 7,
                UpdateUserID = 1,
                IsRemoved = false,
                UpdateDate = DateTime.Now
            };
            return collaborationLevelDTO;
        }

        private List<TherapyTypeDTO> GetMockTherapyTypes()
        {
            return new List<TherapyTypeDTO>()
            {
                new TherapyTypeDTO()
                {
                    TherapyTypeID=1,
                    Name = "Cognitive Behaviorial Therapy",
                    Abbrev = "CBT",
                    Description = "Cognitive behavioral therapy (CBT) is a short-term form of psychotherapy directed at present-time issues and based on the idea that the way an individual thinks and feels affects the way he or she behaves. The focus is on problem solving, and the goal is to change clients' thought patterns in order to change their responses to difficult situations. A CBT approach can be applied to a wide range of mental health issues and conditions.",
                    ListOrder = 1,
                    IsResidential = false,
                    IsRemoved=false,
                    UpdateDate=DateTime.UtcNow,
                    UpdateUserID=1,
                    AgencyID=1
                },
                new TherapyTypeDTO()
                {
                    TherapyTypeID=2,
                    Name = "Biofeedback",
                    ListOrder = 2,
                    IsResidential = false,
                    IsRemoved=false,
                    UpdateDate=DateTime.UtcNow,
                    UpdateUserID=1,
                    AgencyID=1
                }
            };
        }

        private TherapyTypeDetailsDTO GetMockTherapyType()
        {
            TherapyTypeDetailsDTO therapyTypeDetailsDTO = new TherapyTypeDetailsDTO()
            {
                TherapyTypeID = 1,
                Name = "Cognitive Behaviorial Therapy",
                Abbrev = "CBT",
                Description = "Cognitive behavioral therapy (CBT) is a short-term form of psychotherapy directed at present-time issues and based on the idea that the way an individual thinks and feels affects the way he or she behaves. The focus is on problem solving, and the goal is to change clients' thought patterns in order to change their responses to difficult situations. A CBT approach can be applied to a wide range of mental health issues and conditions.",
                ListOrder = 1,
                IsResidential = false,
                IsRemoved = false,
                UpdateDate = DateTime.UtcNow,
                UpdateUserID = 1,
                AgencyID = 1
            };
            return therapyTypeDetailsDTO;
        }

        private List<HelperTitleDTO> GetMockHelperTitles()
        {
            return new List<HelperTitleDTO>()
            {
                new HelperTitleDTO()
                {
                    HelperTitleID=1,
                    Name="Doctor",
                    Abbrev="Dr.",
                    Description="",
                    ListOrder=1,
                    IsRemoved=false,
                    UpdateDate=DateTime.UtcNow,
                    UpdateUserID=1,
                    AgencyID=1
                },
                new HelperTitleDTO()
                {
                    HelperTitleID=2,
                    Name="Mister",
                    Abbrev="Mr.",
                    Description="",
                    ListOrder=2,
                    IsRemoved=false,
                    UpdateDate=DateTime.UtcNow,
                    UpdateUserID=1,
                    AgencyID=1
                }
            };
        }

        private HelperTitleDetailsDTO GetMockHelperTitle()
        {
            HelperTitleDetailsDTO helperTitleDetailsDTO = new HelperTitleDetailsDTO()
            {
                HelperTitleID = 2,
                Name = "Mister",
                Abbrev = "Mr.",
                Description = "",
                ListOrder = 2,
                UpdateUserID = 1,
                AgencyID = 1
            };
            return helperTitleDetailsDTO;
        }

        private List<NotificationLevelDTO> GetMockNotificationLevelList()
        {
            return new List<NotificationLevelDTO>()
            {
                new NotificationLevelDTO()
                {
                    NotificationLevelID= 1,
                    NotificationTypeID=1,
                    Name= "Safety Supervision",
                    Abbrev= null,
                    ListOrder= 1,
                    AgencyID= 1,
                    Description= null,
                    UpdateUserID= 1,
                    IsRemoved= false,
                    UpdateDate= DateTime.Now
                },

                new NotificationLevelDTO()
                    {
                    NotificationLevelID= 2,
                    NotificationTypeID=1,
                    Name= "Danger to Others",
                    Abbrev= null,
                    ListOrder= 10,
                    AgencyID= 1,
                    Description= null,
                    UpdateUserID= 1,
                    IsRemoved= false,
                    UpdateDate=  DateTime.Now
                },
                new NotificationLevelDTO()
                    {
                    NotificationLevelID= 3,
                    NotificationTypeID=1,
                    Name= "Danger to Self",
                    Abbrev= null,
                    ListOrder= 100,
                    AgencyID= 1,
                    Description= null,
                    UpdateUserID= 1,
                    IsRemoved= false,
                    UpdateDate=  DateTime.Now
                },
                new NotificationLevelDTO()
                    {
                    NotificationLevelID= 4,
                    NotificationTypeID=1,
                    Name= "Questionnaire Due",
                    Abbrev= null,
                    ListOrder= 101,
                    AgencyID= 1,
                    Description= null,
                    UpdateUserID= 1,
                    IsRemoved= false,
                    UpdateDate= DateTime.Now
                },
                new NotificationLevelDTO()
                    {
                    NotificationLevelID= 5,
                    NotificationTypeID=1,
                    Name= "Questionnaire Overdue (O)",
                    Abbrev= null,
                    ListOrder= 102,
                    AgencyID= 1,
                    Description= null,
                    UpdateUserID= 1,
                    IsRemoved= false,
                    UpdateDate= DateTime.Now
                },
                new NotificationLevelDTO()
                    {
                    NotificationLevelID= 6,
                    NotificationTypeID=1,
                    Name= "Window Closing",
                    Abbrev= null,
                    ListOrder= 103,
                    AgencyID= 1,
                    Description= null,
                    UpdateUserID= 1,
                    IsRemoved= false,
                    UpdateDate=  DateTime.Now
                }
            };
        }
        private NotificationLevelDTO GetMockNotificationLevel()
        {
            NotificationLevelDTO notificationLevelDTO = new NotificationLevelDTO()
            {
                NotificationLevelID = 7,
                NotificationTypeID = 1,
                Name = "Test 7",
                Abbrev = null,
                ListOrder = 103,
                AgencyID = 1,
                Description = null,
                UpdateUserID = 1,
                IsRemoved = false,
                RequireResolution = false,
                UpdateDate = DateTime.Now
            };
            return notificationLevelDTO;
        }

        private List<GenderDTO> GetMockGenderList()
        {
            return new List<GenderDTO>()
            {
                new GenderDTO()
                {
                    GenderID= 1,
                    Name= "Male",
                    Abbrev= null,
                    ListOrder= 1,
                    AgencyID= 1,
                    Description= null,
                    UpdateUserID= 1,
                    IsRemoved= false,
                    UpdateDate= DateTime.Now
                },

                new GenderDTO()
                    {
                    GenderID= 2,
                    Name= "Female",
                    Abbrev= null,
                    ListOrder= 10,
                    AgencyID= 1,
                    Description= null,
                    UpdateUserID= 1,
                    IsRemoved= false,
                    UpdateDate=  DateTime.Now
                },
                new GenderDTO()
                    {
                    GenderID= 3,
                    Name= "Trans-Gender",
                    Abbrev= null,
                    ListOrder= 100,
                    AgencyID= 1,
                    Description= null,
                    UpdateUserID= 1,
                    IsRemoved= false,
                    UpdateDate=  DateTime.Now
                }
            };
        }
        private List<IdentifiedGender> GetMockIdentifiedGenderList()
        {
            return new List<IdentifiedGender>()
            {
                new IdentifiedGender()
                {
                    IdentifiedGenderID= 1,
                    Name= "Male",
                    Abbrev= null,
                    ListOrder= 1,
                    AgencyID= 1,
                    Description= null,
                    UpdateUserID= 1,
                    IsRemoved= false,
                    UpdateDate= DateTime.Now
                },

                new IdentifiedGender()
                    {
                    IdentifiedGenderID= 2,
                    Name= "Female",
                    Abbrev= null,
                    ListOrder= 10,
                    AgencyID= 1,
                    Description= null,
                    UpdateUserID= 1,
                    IsRemoved= false,
                    UpdateDate=  DateTime.Now
                },
                new IdentifiedGender()
                    {
                    IdentifiedGenderID= 3,
                    Name= "Not Reported",
                    Abbrev= null,
                    ListOrder= 100,
                    AgencyID= 1,
                    Description= null,
                    UpdateUserID= 1,
                    IsRemoved= false,
                    UpdateDate=  DateTime.Now
                }
            };
        }
        private IdentifiedGender GetMockIdentifiedGender()
        {
            IdentifiedGender identifiedender = new IdentifiedGender()
            {
                IdentifiedGenderID = 1,
                Name = "Male",
                Abbrev = null,
                ListOrder = 1,
                AgencyID = 1,
                Description = null,
                UpdateUserID = 1,
                IsRemoved = false,
                UpdateDate = DateTime.Now
            };
            return identifiedender;
        }
        private IdentifiedGenderDTO GetMockIdentifiedGenderDTO()
        {
            IdentifiedGenderDTO genderDTO = new IdentifiedGenderDTO()
            {
                IdentifiedGenderID = 1,
                Name = "Male",
                Abbrev = null,
                ListOrder = 1,
                AgencyID = 1,
                Description = null,
                UpdateUserID = 1,
                IsRemoved = false,
                UpdateDate = DateTime.Now
            };
            return genderDTO;
        }
        private GenderDTO GetMockGender()
        {
            GenderDTO genderDTO = new GenderDTO()
            {
                GenderID = 1,
                Name = "Male",
                Abbrev = null,
                ListOrder = 1,
                AgencyID = 1,
                Description = null,
                UpdateUserID = 1,
                IsRemoved = false,
                UpdateDate = DateTime.Now
            };
            return genderDTO;
        }

        private List<IdentificationTypeDTO> GetMockIdentificationTypeList()
        {
            return new List<IdentificationTypeDTO>()
            {
                new IdentificationTypeDTO()
                {
                    IdentificationTypeID= 1,
                    Name= "Behavorial Health ID",
                    Abbrev= null,
                    ListOrder= 1,
                    AgencyID= 1,
                    Description= null,
                    UpdateUserID= 1,
                    IsRemoved= false,
                    UpdateDate= DateTime.Now
                },

                new IdentificationTypeDTO()
                    {
                    IdentificationTypeID= 2,
                    Name= "Behavorial Health ID 2",
                    Abbrev= null,
                    ListOrder= 2,
                    AgencyID= 1,
                    Description= null,
                    UpdateUserID= 1,
                    IsRemoved= false,
                    UpdateDate=  DateTime.Now
                },
                new IdentificationTypeDTO()
                    {
                    IdentificationTypeID= 3,
                    Name= "Behavorial Health ID 3",
                    Abbrev= null,
                    ListOrder= 3,
                    AgencyID= 1,
                    Description= null,
                    UpdateUserID= 1,
                    IsRemoved= false,
                    UpdateDate=  DateTime.Now
                }
            };

        }

        private IdentificationType GetMockIdentificationType()
        {
            IdentificationType identificationType = new IdentificationType()
            {
                IdentificationTypeID = 1,
                Name = "Behavorial Health ID",
                Abbrev = null,
                ListOrder = 1,
                AgencyID = 1,
                Description = null,
                UpdateUserID = 1,
                IsRemoved = false,
                UpdateDate = DateTime.Now
            };
            return identificationType;
        }
        private IdentificationTypeDTO GetMockIdentificationTypeDTO()
        {
            IdentificationTypeDTO identificationType = new IdentificationTypeDTO()
            {
                IdentificationTypeID = 1,
                Name = "Behavorial Health ID",
                Abbrev = null,
                ListOrder = 1,
                AgencyID = 1,
                Description = null,
                UpdateUserID = 1,
                IsRemoved = false,
                UpdateDate = DateTime.Now
            };
            return identificationType;
        }


        private List<RaceEthnicityDTO> GetMockRaceEthnicityList()
        {
            return new List<RaceEthnicityDTO>()
            {
                new RaceEthnicityDTO()
                {
                    RaceEthnicityID= 1,
                    Name= "American Indian or Alaska Native",
                    Abbrev= null,
                    ListOrder= 1,
                    AgencyID= 1,
                    Description= "A person having origins in any of the original peoples of North and South America (including Central America), and who maintains tribal affiliation or community attachment.",
                    UpdateUserID= 1,
                    IsRemoved= false,
                    UpdateDate= DateTime.Now
                },

                new RaceEthnicityDTO()
                    {
                    RaceEthnicityID= 2,
                    Name= "American Indian",
                    Abbrev= null,
                    ListOrder= 2,
                    AgencyID= 1,
                    Description= "A person having origins in any of the original peoples of North and South America (including Central America), and who maintains tribal affiliation or community attachment.",
                    UpdateUserID= 1,
                    IsRemoved= false,
                    UpdateDate=  DateTime.Now
                }
            };

        }
        private RaceEthnicity GetMockRaceEthnicity()
        {
            RaceEthnicity identificationType = new RaceEthnicity()
            {
                RaceEthnicityID = 1,
                Name = "American Indian or Alaska Native",
                Abbrev = null,
                ListOrder = 1,
                AgencyID = 1,
                Description = "A person having origins in any of the original peoples of North and South America (including Central America), and who maintains tribal affiliation or community attachment.",
                UpdateUserID = 1,
                IsRemoved = false,
                UpdateDate = DateTime.Now
            };
            return identificationType;
        }
        private RaceEthnicityDTO GetMockRaceEthnicityDTO()
        {
            RaceEthnicityDTO identificationType = new RaceEthnicityDTO()
            {
                RaceEthnicityID = 1,
                Name = "American Indian or Alaska Native",
                Abbrev = null,
                ListOrder = 1,
                AgencyID = 1,
                Description = "A person having origins in any of the original peoples of North and South America (including Central America), and who maintains tribal affiliation or community attachment.",
                UpdateUserID = 1,
                IsRemoved = false,
                UpdateDate = DateTime.Now
            };
            return identificationType;
        }

        private List<SupportTypeDTO> GetMockSupportTypeList()
        {
            return new List<SupportTypeDTO>()
            {
                new SupportTypeDTO()
                {
                    SupportTypeID= 1,
                    Name= "Aunt",
                    Abbrev= null,
                    ListOrder= 1,
                    AgencyID= 1,
                    Description= null,
                    UpdateUserID= 1,
                    IsRemoved= false,
                    UpdateDate= DateTime.Now
                },

                new SupportTypeDTO()
                    {
                    SupportTypeID= 2,
                    Name= "Uncle",
                    Abbrev= null,
                    ListOrder= 2,
                    AgencyID= 1,
                    Description= null,
                    UpdateUserID= 1,
                    IsRemoved= false,
                    UpdateDate=  DateTime.Now
                },
                new SupportTypeDTO()
                    {
                    SupportTypeID= 3,
                    Name= "Grand Parent",
                    Abbrev= null,
                    ListOrder= 3,
                    AgencyID= 1,
                    Description= null,
                    UpdateUserID= 1,
                    IsRemoved= false,
                    UpdateDate=  DateTime.Now
                }
            };

        }
        private SupportType GetMockSupportType()
        {
            SupportType identificationType = new SupportType()
            {
                SupportTypeID = 1,
                Name = "Aunt",
                Abbrev = null,
                ListOrder = 1,
                AgencyID = 1,
                Description = null,
                UpdateUserID = 1,
                IsRemoved = false,
                UpdateDate = DateTime.Now
            };
            return identificationType;
        }
        private SupportTypeDTO GetMockSupportTypeDTO()
        {
            SupportTypeDTO identificationType = new SupportTypeDTO()
            {
                SupportTypeID = 1,
                Name = "Aunt",
                Abbrev = null,
                ListOrder = 1,
                AgencyID = 1,
                Description = null,
                UpdateUserID = 1,
                IsRemoved = false,
                UpdateDate = DateTime.Now
            };
            return identificationType;
        }

        private Sexuality MockSexuality()
        {
            return new Sexuality()
            {
                SexualityID = 1,
                Name = "Not Reported",
                Abbrev = null,
                Description = "No stated attraction",
                ListOrder = 1,
                IsRemoved = false,
                UpdateDate = DateTime.UtcNow,
                UpdateUserID = 2,
                AgencyID = 1
            };
        }
        private SexualityInputDTO MockSexualityInputDTO()
        {
            return new SexualityInputDTO()
            {
                Name = "Not Reported",
                Abbrev = null,
                Description = "No stated attraction",
                ListOrder = 1
            };
        }

        private List<SexualityDTO> GetMockSexualityList()
        {
            return new List<SexualityDTO>()
            {
                new SexualityDTO()
                {
                    SexualityID = 1,
                    Name = "Not Reported",
                    Abbrev = null,
                    Description = "No stated attraction",
                    ListOrder = 1,
                    IsRemoved = false,
                    UpdateDate = DateTime.UtcNow,
                    UpdateUserID = 2,
                    AgencyID = 1
                }
            };
        }
        private List<NotificationType> GetMockNotificationTypeList()
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

        private EditSexualityDTO GetMockEditSexuality()
        {
            return new EditSexualityDTO()
            {
                SexualityID = 1,
                Abbrev = "SexualityAbbrev",
                Name = "Sexualityname",
                ListOrder = 1,
                Description = "SexualityDesc"
            };
        }
        private Sexuality GetMockSexuality()
        {
            return new Sexuality()
            {
                SexualityID = 1,
                Abbrev = "SexualityAbbrev",
                Name = "Sexualityname",
                ListOrder = 1,
                Description = "SexualityDesc"
            };
        }
        #endregion
    }
}