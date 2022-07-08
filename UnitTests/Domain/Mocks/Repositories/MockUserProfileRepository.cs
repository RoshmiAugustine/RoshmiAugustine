using System;
using Moq;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Interfaces.Repositories;

namespace Opeeka.PICS.UnitTests.Domain.Mocks.Repositories
{
    public class MockUserProfileRepository : Mock<IUserProfileRepository>
    {
        public MockUserProfileRepository MockAddUserProfile(int result)
        {
            Setup(x => x.AddUserProfile(It.IsAny<UserProfileDTO>()))
                .Returns(result);
            return this;
        }
        public MockUserProfileRepository MockAddUserProfileException()
        {
            Setup(x => x.AddUserProfile(It.IsAny<UserProfileDTO>()))
           .Throws<Exception>();
            return this;
        }
    }
}
