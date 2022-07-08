using Microsoft.AspNetCore.Http;
using Moq;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Domain.Services;
using Opeeka.PICS.Infrastructure.Enums;
using Opeeka.PICS.UnitTests.Domain.Mocks.Repositories;
using System;
using System.Collections.Generic;
using Xunit;

namespace Opeeka.PICS.UnitTests.Domain.Services
{
    public class SystemRoleServiceUnitTest
    {
        /// Initializes a new instance of the SystemRoleRepository class.
        private Mock<ISystemRoleRepository> mockSystemRoleRepository;
        private Mock<IConfigurationRepository> mockConfigRepository;
        private Mock<IHttpContextAccessor> httpContextAccessor;
        private SystemRoleService systemRoleService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AgencyService"/> class.
        /// </summary>
        public SystemRoleServiceUnitTest()
        {
            this.mockSystemRoleRepository = new Mock<ISystemRoleRepository>();
            this.mockConfigRepository = new Mock<IConfigurationRepository>();
            this.httpContextAccessor = new Mock<IHttpContextAccessor>();
        }

        /// <summary>
        /// Get System Role List Success
        /// </summary>
        /// 
        [Fact]
        public void GetSystemRoleList_Success_ReturnsCorrectResult()
        {
            var mockSystemRoles = GetMockSystemRoles();
            this.mockSystemRoleRepository = new MockSystemRoleRepository().MockGetAllSystemRoles(mockSystemRoles);
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Success);
            this.systemRoleService = new SystemRoleService(this.mockSystemRoleRepository.Object, localize.Object, mockConfigRepository.Object, httpContextAccessor.Object);
            int pageNumber = 1;
            int pageSize = 10;
            var result = this.systemRoleService.GetSystemRoleList(pageNumber, pageSize);
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.Success);
        }

        /// <summary>
        /// GetSystemRoleList Returns No Result
        /// </summary>
        [Fact]
        public void GetSystemRoleList_ReturnsNoResult()
        {
            var mockSystemRoles = new List<SystemRoleDTO>();
            this.mockSystemRoleRepository = new MockSystemRoleRepository().MockGetAllSystemRoles(mockSystemRoles);
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Success);
            this.systemRoleService = new SystemRoleService(this.mockSystemRoleRepository.Object, localize.Object, mockConfigRepository.Object, httpContextAccessor.Object);
            int pageNumber = 1;
            int pageSize = 10;
            var result = this.systemRoleService.GetSystemRoleList(pageNumber, pageSize);
            Assert.Equal(0, result.TotalCount);
        }

        /// <summary>
        /// GetSystemRoleList with Invalid Parameter Result
        /// </summary>
        [Fact]
        public void GetSystemRoleList_Failure_InvalidParameterResult()
        {
            var mockSystemRoles = new List<SystemRoleDTO>();
            this.mockSystemRoleRepository = new MockSystemRoleRepository().MockGetAllSystemRoles(mockSystemRoles);
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Failure);
            this.systemRoleService = new SystemRoleService(this.mockSystemRoleRepository.Object, localize.Object, mockConfigRepository.Object, httpContextAccessor.Object);
            int pageNumber = 0;
            int pageSize = 10;
            var result = this.systemRoleService.GetSystemRoleList(pageNumber, pageSize);
            Assert.Null(result.SystemRoleList);
        }

        /// <summary>
        /// GetSystemRoles Exception Result
        /// </summary>
        [Fact]
        public void GetSystemRoles_Failure_ExceptionResult()
        {
            var mockSystemRoles = new List<SystemRoleDTO>();
            this.mockSystemRoleRepository = new MockSystemRoleRepository().MockGetAllSystemRolesException(mockSystemRoles);
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Failure);
            this.systemRoleService = new SystemRoleService(this.mockSystemRoleRepository.Object, localize.Object, mockConfigRepository.Object, httpContextAccessor.Object);
            int pageNumber = 1;
            int pageSize = 10;
            Assert.ThrowsAny<Exception>(() => this.systemRoleService.GetSystemRoleList(pageNumber, pageSize));
        }

        /// <summary>
        /// Get Mock data for SystemRoles
        /// </summary>
        /// <returns></returns>
        private List<SystemRoleDTO> GetMockSystemRoles()
        {
            return new List<SystemRoleDTO>()
            {
                new SystemRoleDTO()
                {
                    SystemRoleID = 1,
                    Name = "Helper-RW",
                    Abbrev = "H-RW",
                    ListOrder = 1,
                    IsExternal = true,
                    Weight = 1,
                    IsRemoved = false,
                    UpdateDate = DateTime.UtcNow,
                    UpdateUserID = 1
                },
                new SystemRoleDTO()
                {
                    SystemRoleID = 2,
                    Name = "Supervisor",
                    Abbrev = "SUP",
                    ListOrder = 1,
                    IsExternal = true,
                    Weight = 1,
                    IsRemoved = false,
                    UpdateDate = DateTime.UtcNow,
                    UpdateUserID = 1
                }
            };
        }
    }
}
