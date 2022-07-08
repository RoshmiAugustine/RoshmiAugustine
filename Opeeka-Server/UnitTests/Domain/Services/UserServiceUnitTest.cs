using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Moq;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Interfaces.Providers.Contract;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Domain.Interfaces.Services;
using Opeeka.PICS.Domain.Services;
using Opeeka.PICS.Infrastructure.Enums;
using Opeeka.PICS.UnitTests.Domain.Mocks.Repositories;
using Xunit;

namespace Opeeka.PICS.UnitTests.Domain.Services
{
    public class UserServiceUnitTest
    {
        private Mock<IUserRepository> mockUserRepository;
        private Mock<IMapper> mockMapper;
        private Mock<IUserRoleRepository> mockUserRoleRepository;
        private Mock<ISystemRoleRepository> mockSystemRoleRepository;
        private Mock<ISystemRolePermissionRepository> mockSystemRolePermissionRepository;
        private Mock<IRolePermissionRepository> mockRolePermissionRepository;
        private Mock<IAgencyRepository> mockAgencyRepository;
        private Mock<IConfigurationRepository> mockConfigurationRepository;
        private Mock<ITenantProvider> mockTenantProvider;
        private Mock<IHttpContextAccessor> mockaccessor;
        private UserService userService;
        private Mock<IFileService> mockFileService;

        public UserServiceUnitTest()
        {
            this.mockUserRepository = new Mock<IUserRepository>();
            this.mockMapper = new Mock<IMapper>();
            this.mockTenantProvider = new Mock<ITenantProvider>();
            this.mockSystemRoleRepository = new Mock<ISystemRoleRepository>();
            this.mockUserRoleRepository = new Mock<IUserRoleRepository>();
            this.mockSystemRolePermissionRepository = new Mock<ISystemRolePermissionRepository>();
            this.mockRolePermissionRepository = new Mock<IRolePermissionRepository>();
            this.mockAgencyRepository = new Mock<IAgencyRepository>();
            this.mockaccessor = new Mock<IHttpContextAccessor>();
            this.mockFileService = new Mock<IFileService>();
            this.mockConfigurationRepository = new Mock<IConfigurationRepository>();
        }


        #region Get User Profile
        [Fact]
        public void GetUserProfileList_Success_ReturnsCorrectResult()
        {

            UserProfileDTO mockUserProfile = GetMockUserProfile();
            this.mockUserRepository = new MockUserRepository().MockUserProfile(mockUserProfile);

            InitialiseUserService();

            int userID = 1;
            long agencyID = 1;
            List<string> userRole = new List<string>();
            userRole.Add(PCISEnum.Roles.SuperAdmin);
            var result = this.userService.GetUserProfile(userID, userRole, agencyID);
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.Success);
        }

        [Fact]
        public void GetUserProfileList_Failure_ExceptionResult()
        {
            this.mockUserRepository = new MockUserRepository().MockUserProfileException();
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.InsertionSuccess);
            InitialiseUserService();
            int userID = 1;
            long agencyID = 1;
            List<string> userRole = new List<string>();
            userRole.Add(PCISEnum.Roles.SuperAdmin);

            Assert.ThrowsAny<Exception>(() => this.userService.GetUserProfile(userID, userRole, agencyID));
        }

        private UserProfileDTO GetMockUserProfile()
        {
            UserProfileDTO userProfile = new UserProfileDTO()
            {
                Name = "Job",
                Email = "gibin.job@naicoits.com",
                Phone1 = "9878456512",
                Phone2 = "9878545621",
                AzureFileName = "f4d3a8ce-df54-4eb9-8022-2ee22f6a66d7.jpg",
            };
            return userProfile;
        }

        private void InitialiseUserService()
        {
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.InsertionSuccess);
            this.userService = new UserService(this.mockConfigurationRepository.Object, this.mockUserRepository.Object, this.mockMapper.Object, this.mockTenantProvider.Object, null, null, this.mockSystemRoleRepository.Object, this.mockUserRoleRepository.Object, this.mockAgencyRepository.Object, this.mockSystemRolePermissionRepository.Object, this.mockRolePermissionRepository.Object, this.mockaccessor.Object, localize.Object, this.mockFileService.Object);
        }
        #endregion
    }
}
