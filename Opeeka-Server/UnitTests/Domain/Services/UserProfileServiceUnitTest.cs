using System;
using Moq;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Domain.Interfaces.Services;
using Opeeka.PICS.Domain.Services;
using Opeeka.PICS.Infrastructure.Enums;
using Opeeka.PICS.UnitTests.Domain.Mocks.Repositories;
using Xunit;

namespace Opeeka.PICS.UnitTests.Domain.Services
{
    public class UserProfileServiceUnitTest
    {
        private UserProfileService UserProfileService;
        private Mock<IUserProfileRepository> mockUserProfileRepository;
        public UserProfileServiceUnitTest()
        {
            this.mockUserProfileRepository = new Mock<IUserProfileRepository>();
        }

        private void InitialiseUserService()
        {
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.InsertionSuccess);
            this.UserProfileService = new UserProfileService(localize.Object, null, null, this.mockUserProfileRepository.Object,null,null,null);
        }


        #region Get File Report
        [Fact]
        public void AddUserProfile_Success_ReturnsCorrectResult()
        {
            this.mockUserProfileRepository = new MockUserProfileRepository().MockAddUserProfile(1);
            InitialiseUserService();
            var result = this.UserProfileService.AddUserProfile(AddMockUserProfile());
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.InsertionSuccess);
        }

        [Fact]
        public void AddUserProfile_Failure_ExceptionResult()
        {
            this.mockUserProfileRepository = new MockUserProfileRepository().MockAddUserProfileException();
            InitialiseUserService();
            Assert.ThrowsAny<Exception>(() => this.UserProfileService.AddUserProfile(AddMockUserProfile()));
        }

        private UserProfileDTO AddMockUserProfile()
        {
            return new UserProfileDTO()
            {
                ImageFileID = 1,
                UserID = 1,
                UserProfileID = 1
            };
        }
        #endregion  Get File Report 
    }
}
