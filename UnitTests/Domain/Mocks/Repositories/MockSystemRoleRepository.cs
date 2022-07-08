
using Moq;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;

namespace Opeeka.PICS.UnitTests.Domain.Mocks.Repositories
{
    public class MockSystemRoleRepository : Mock<ISystemRoleRepository>
    {
        /// <summary>
        /// Mock GetAllSystemRoles
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public MockSystemRoleRepository MockGetAllSystemRoles(List<SystemRoleDTO> result)
        {
            Setup(x => x.GetAllSystemRoles(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(result);
            Setup(x => x.GetSystemRoleCount())
                .Returns(result.Count);
            return this;
        }

        /// <summary>
        /// Mock GetAllSystemRoles Exception case
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public MockSystemRoleRepository MockGetAllSystemRolesException(List<SystemRoleDTO> result)
        {
            Setup(x => x.GetAllSystemRoles(It.IsAny<int>(), It.IsAny<int>()))
                .Throws<Exception>();
            return this;
        }
    }
}
