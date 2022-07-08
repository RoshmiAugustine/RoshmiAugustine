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
    public class RolePermissionRepository : BaseRepository<RolePermission>, IRolePermissionRepository
    {
        private readonly IMapper mapper;
        private readonly ILogger<RolePermissionRepository> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRoleRepository"/> class.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public RolePermissionRepository(ILogger<RolePermissionRepository> logger, OpeekaDBContext dbContext, IMapper mapper)
            : base(dbContext)
        {
            this.mapper = mapper;
            this.logger = logger;
        }

        /// <summary>
        /// Save bulk user role details.
        /// </summary>
        /// <param name="userRoledto"></param>
        /// <param name="UserID"></param>
        public async Task CreateBulkRolePermissions(List<RolePermissionDTO> rolePermissionDto)
        {
            try
            {
                List<RolePermission> rolePermissions = new List<RolePermission>();
                this.mapper.Map<List<RolePermissionDTO>, List<RolePermission>>(rolePermissionDto, rolePermissions);
                if (rolePermissionDto != null)
                {
                    await this.AddBulkAsync(rolePermissions);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Save bulk user role details.
        /// </summary>
        /// <param name="userRoledto"></param>
        /// <param name="UserID"></param>
        public async Task DeleteBulkRolePermissions(List<RolePermissionDTO> rolePermissionDto)
        {
            try
            {
                List<RolePermission> rolePermissions = new List<RolePermission>();
                this.mapper.Map<List<RolePermissionDTO>, List<RolePermission>>(rolePermissionDto, rolePermissions);
                if (rolePermissionDto != null)
                {
                    await this.DeleteBulkAsync(rolePermissions);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<RolePermissionDTO>> GetRolePermissionsAsync(List<int> userRoleIds)
        {
            try
            {
                var rolePermissions = await this.GetAsync(x => userRoleIds.Contains(x.UserRoleID));
                return this.mapper.Map<List<RolePermissionDTO>>(rolePermissions);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
