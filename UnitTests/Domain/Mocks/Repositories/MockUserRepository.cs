using System;
using System.Threading.Tasks;
using Moq;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Interfaces.Repositories;

namespace Opeeka.PICS.UnitTests.Domain.Mocks.Repositories
{
    public class MockUserRepository : Mock<IUserRepository>
    {
        internal Mock<IUserRepository> MockUser(UsersDTO mockUserDTO)
        {
            Setup(x => x.UpdateUser(It.IsAny<UsersDTO>())).Returns(mockUserDTO);
            Setup(x => x.GetUsersByUsersIDAsync(It.IsAny<int>())).Returns(Task.FromResult(mockUserDTO));
            return this;
        }

        public MockUserRepository MockUserProfile(UserProfileDTO mockUserProfile)
        {
            Setup(x => x.GetUserProfile(It.IsAny<int>(), It.IsAny<bool>()))
                  .Returns(mockUserProfile);
            return this;
        }

        public MockUserRepository MockUserProfileException()
        {
            Setup(x => x.GetUserProfile(It.IsAny<int>(), It.IsAny<bool>()))
                .Throws<Exception>();
            return this;
        }
    }
}
