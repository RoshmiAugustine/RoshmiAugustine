using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Common;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Domain.Interfaces.Services;
using Opeeka.PICS.Domain.Services;
using Opeeka.PICS.Infrastructure.Enums;
using Opeeka.PICS.UnitTests.Domain.Mocks.Repositories;
using System;
using Xunit;

namespace Opeeka.PICS.UnitTests.Domain.Services
{
    public class HelperServiceUnitTest
    {
        private Mock<IHelperRepository> mockHelperRepository;
        private Mock<IUserRepository> mockUserRepository;
        private Mock<IUserRoleRepository> mockUserRoleRepository;
        private Mock<IAddressrepository> mockAddressRepository;
        private Mock<IHelperAddressRepository> mockHelperAddressRepository;

        /// Initializes a new instance of the <see cref="logger"/> class.
        private Mock<ILogger<HelperService>> mockLogger;

        private HelperService helperService;
        private Mock<IConfigurationRepository> mockConfigRepository;
        private Mock<IHttpContextAccessor> httpContextAccessor;
        private Mock<IUserService> mockUserService;
        private Mock<ISystemRoleRepository> mockSystemRoleRepository;
        private Mock<IMapper> mock_mapper;
        private Mock<IQueryBuilder> mockqueryBuilder;
        private Mock<IAzureADB2CService> mockAzureADB2CService;
        private Mock<ILookupRepository> mocklookupRepository;
        private Mock<ILanguageRepository> mocklanguageRepository;
        private Mock<IHelperTitleRepository> mockhelperTitleRepository;
        private Mock<IAgencyRepository> mockagencyRepository;
        private Mock<IHelperRepository> mockhelperRepository;

        public HelperServiceUnitTest()
        {
            this.mockHelperRepository = new Mock<IHelperRepository>();
            this.mockUserRepository = new Mock<IUserRepository>();
            this.mockUserRoleRepository = new Mock<IUserRoleRepository>();
            this.mockAddressRepository = new Mock<IAddressrepository>();
            this.mockHelperAddressRepository = new Mock<IHelperAddressRepository>();
            this.mockLogger = new Mock<ILogger<HelperService>>();
            this.mockConfigRepository = new Mock<IConfigurationRepository>();
            this.httpContextAccessor = new Mock<IHttpContextAccessor>();
            this.mockUserService = new Mock<IUserService>();
            this.mockSystemRoleRepository = new Mock<ISystemRoleRepository>();
            this.mock_mapper = new Mock<IMapper>();
            this.mockqueryBuilder = new Mock<IQueryBuilder>();
            this.mockAzureADB2CService = new Mock<IAzureADB2CService>();
            this.mocklookupRepository = new Mock<ILookupRepository>();
            this.mocklanguageRepository = new Mock<ILanguageRepository>();
            this.mockhelperTitleRepository = new Mock<IHelperTitleRepository>();
            this.mockagencyRepository = new Mock<IAgencyRepository>();
            this.mockhelperRepository = new Mock<IHelperRepository>();
        }

        #region InitialiseSeviceWithMockedDI
        private void InitialiseHelperService()
        {
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Success);
            this.helperService = new HelperService(this.mock_mapper.Object, this.mockUserRepository.Object, this.mockUserRoleRepository.Object,
                this.mockHelperRepository.Object, this.mockAddressRepository.Object, this.mockHelperAddressRepository.Object,
                localize.Object, mockConfigRepository.Object, httpContextAccessor.Object,
                this.mockUserService.Object, this.mockSystemRoleRepository.Object, this.mockqueryBuilder.Object, this.mockAzureADB2CService.Object,
                this.mocklookupRepository.Object, this.mocklanguageRepository.Object, this.mockhelperRepository.Object, this.mockagencyRepository.Object,
                this.mockhelperTitleRepository.Object);
        }

        #endregion
        #region Get User Details
        [Fact]
        public void GetUserDetailsList_Success_ReturnsCorrectResult()
        {
            var mockUserDetails = new UserDetailsDTO();//GetMockUserDetails();
            mockUserDetails.Name = "Gibin";
            mockUserDetails.Title = "Dr.";
            this.mockHelperRepository = new MockHelperRepository().MockUserDetails(mockUserDetails);
            InitialiseHelperService();
            var UserId = 1;
            var result = this.helperService.GetUserDetails(UserId);
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.Success);
        }

        [Fact]
        public void GetUserDetailsList_Success_ReturnsNoResult()
        {
            var mockUserDetails = new UserDetailsDTO();//GetMockUserDetails();
            this.mockHelperRepository = new MockHelperRepository().MockUserDetails(mockUserDetails);
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Success);
            InitialiseHelperService();
            var UserId = -1;
            var result = this.helperService.GetUserDetails(UserId);
            Assert.Null(result.UserDetails.Name);
        }

        [Fact]
        public void GetUserDetailsList_Failure_ExceptionResult()
        {
            UserDetailsDTO mockUserDetails = new UserDetailsDTO();
            this.mockHelperRepository = new MockHelperRepository().MockUserDetailsException(mockUserDetails);

            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Failure);
            InitialiseHelperService();
            int UserId = 1;

            Assert.ThrowsAny<Exception>(() => this.helperService.GetUserDetails(UserId));
        }
        #endregion


        #region Delete Helper 
        [Fact]
        public void RemoveHelperDetails_Success_ReturnsCorrectResult()
        {
            Helper mockHelper = MockHelper();
            HelperDTO mockHelperDTO = MockHelperDTO();
            this.mockHelperRepository = new MockHelperRepository().MockRemoveHelper(mockHelper, mockHelperDTO);
            AddressDTO mockAddressDTO = GetMockAddressDTO();
            this.mockAddressRepository = new MockAddressRepository().MockEditAddress(mockAddressDTO);
            UsersDTO mockUsersDTO = GetMockUsersDTO();
            this.mockUserRepository = new MockUserRepository().MockUser(mockUsersDTO);
            HelperAddressDTO mockHelperAddressDTO = GetMockHelperAddressDTO();
            this.mockHelperAddressRepository = new MockHelperAddressRespository().MockHelperAddress(mockHelperAddressDTO);

            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.DeletionSuccess);
            InitialiseHelperService();
            Guid helperIndex = new Guid("CA67BF4B-7A9E-4227-8A45-069868379FA7");
            var result = this.helperService.RemoveHelperDetails(helperIndex, 1);

            Assert.Equal(result.ResponseStatus, PCISEnum.StatusMessages.DeletionSuccess);
        }

        [Fact]
        public void RemoveHelperDetails_Failure_ExceptionResult()
        {
            Helper mockHelper = MockHelper();
            HelperDTO mockHelperDTO = MockHelperDTO();
            this.mockHelperRepository = new MockHelperRepository().MockRemoveHelperException(mockHelper, mockHelperDTO);
            AddressDTO mockAddressDTO = GetMockAddressDTO();
            this.mockAddressRepository = new MockAddressRepository().MockEditAddress(mockAddressDTO);
            UsersDTO mockUsersDTO = GetMockUsersDTO();
            this.mockUserRepository = new MockUserRepository().MockUser(mockUsersDTO);
            HelperAddressDTO mockHelperAddressDTO = GetMockHelperAddressDTO();
            this.mockHelperAddressRepository = new MockHelperAddressRespository().MockHelperAddress(mockHelperAddressDTO);

            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.DeletionSuccess);
            InitialiseHelperService();

            Guid helperIndex = new Guid("CA67BF4B-7A9E-4227-8A45-069868379FA7");
            Assert.ThrowsAny<Exception>(() => this.helperService.RemoveHelperDetails(helperIndex, 1));
        }

        private UsersDTO GetMockUsersDTO()
        {
            return new UsersDTO()
            {
                UserID = 1,
                Name = "asddf",
                IsActive = true,
            };
        }
        private HelperAddressDTO GetMockHelperAddressDTO()
        {
            return new HelperAddressDTO()
            {
                HelperAddressIndex = new Guid("B6EA0154-95AD-4C4C-A198-25945A45A5F7"),
                HelperAddressID = 1,
                AddressID = 1,
                HelperID = 1,
                IsPrimary = true,
            };
        }
        private AddressDTO GetMockAddressDTO()
        {
            AddressDTO AddressDTO = new AddressDTO()
            {
                AddressIndex = new Guid("B6EA0154-95AD-4C4C-A198-25945A45A5F7"),
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
        private Helper MockHelper()
        {
            return new Helper()
            {
                HelperID = 1,
                AgencyID = 1,
                FirstName = "Helper 1",
                LastName = "California",
                MiddleName = "Q1",
                Email = "test@gmail.com",
                StartDate = Convert.ToDateTime("2020 -01-01 00:00:00.000"),
                EndDate = null,
                IsRemoved = false,
                UpdateUserID = 2,
                UpdateDate = DateTime.UtcNow
            };
        }
        private HelperDTO MockHelperDTO()
        {
            return new HelperDTO()
            {
                HelperID = 1,
                FirstName = "Helper 1",
                LastName = "California",
                MiddleName = "Q1",
                Email = "test@gmail.com",
                StartDate = Convert.ToDateTime("2020 -01-01 00:00:00.000"),
                EndDate = null,
                IsRemoved = false,
                UpdateUserID = 2,
                UpdateDate = DateTime.UtcNow
            };
        }

        #endregion Delete Helper 

    }
}
