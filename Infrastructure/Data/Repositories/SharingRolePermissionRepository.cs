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
    public class SharingRolePermissionRepository : BaseRepository<SharingRolePermission>, ISharingRolePermissionRepository
    {
        private readonly IMapper mapper;
        private readonly ILogger<SharingRolePermissionRepository> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRoleRepository"/> class.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public SharingRolePermissionRepository(ILogger<SharingRolePermissionRepository> logger, OpeekaDBContext dbContext, IMapper mapper)
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
        public async Task<List<SharingRolePermission>> GetSharingRolePermissionsAsync(int agencySharingPolicyID, List<int> collaborationSharingPolicyIDs)
        {
            try
            {
                var sharingRolePermissions = await this.GetAsync(x => x.AgencySharingPolicyID == agencySharingPolicyID && collaborationSharingPolicyIDs.Contains(x.CollaborationSharingPolicyID));
                return this.mapper.Map<List<SharingRolePermission>>(sharingRolePermissions);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
