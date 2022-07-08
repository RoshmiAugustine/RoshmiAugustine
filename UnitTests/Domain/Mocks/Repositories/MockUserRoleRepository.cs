
using Moq;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using System.Threading.Tasks;

namespace Opeeka.PICS.UnitTests.Domain.Mocks.Repositories
{
    public class MockUserRoleRepository : Mock<IUserRoleRepository>
    {
        public MockUserRoleRepository MockUserRole(int result)
        {
            Setup(x => x.CreateUserRole(It.IsAny<UserRoleDTO>(), It.IsAny<int>()))
                .Returns(result);
            return this;
        }
        public MockUserRoleRepository MockEditUserRole(UserRoleDTO result)
        {
            Setup(x => x.UpdateUserRole(It.IsAny<UserRoleDTO>()))
               .Returns(result);

            Setup(x => x.GetUserRoleByUserIDAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(result));
            return this;
        }

    }
}
