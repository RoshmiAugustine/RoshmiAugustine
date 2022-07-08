// -----------------------------------------------------------------------
// <copyright file="UserRoleRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Infrastructure.Data.Repositories.Base;
using System.Collections.Generic;

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{
    public class SystemRolePermissionRepository : BaseRepository<SystemRolePermission>, ISystemRolePermissionRepository
    {
        private readonly IMapper mapper;
        private readonly ILogger<SystemRolePermissionRepository> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRoleRepository"/> class.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public SystemRolePermissionRepository(ILogger<SystemRolePermissionRepository> logger, OpeekaDBContext dbContext, IMapper mapper)
            : base(dbContext)
        {
            this.mapper = mapper;
            this.logger = logger;
        }

        /// <summary>
        /// To get system Role details by role name
        /// </summary>
        /// <param name="role"></param>
        /// <returns>SystemRoleDTO</returns>
        public async Task<List<SystemRolePermissionDTO>> GetSystemRolePermissionsAsync(int systemRoleId)
        {
            try
            {
                var systemRolePermissions = await this.GetAsync(x => x.SystemRoleID == systemRoleId);
                return this.mapper.Map<List<SystemRolePermissionDTO>>(systemRolePermissions);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
