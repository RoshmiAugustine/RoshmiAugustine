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
using System.Linq;
using Opeeka.PICS.Domain.Interfaces;
using Opeeka.PICS.Infrastructure.Enums;

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{
    public class SystemRoleRepository : BaseRepository<SystemRole>, ISystemRoleRepository
    {
        private readonly IMapper mapper;
        private readonly ILogger<SystemRoleRepository> logger;
        private readonly OpeekaDBContext dbContext;
        private readonly ICache _cache;
        /// <summary>
        /// Initializes a new instance of the <see cref="UserRoleRepository"/> class.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public SystemRoleRepository(ILogger<SystemRoleRepository> logger, OpeekaDBContext dbContext, IMapper mapper, ICache cache)
            : base(dbContext)
        {
            this.mapper = mapper;
            this.logger = logger;
            this.dbContext = dbContext;
            this._cache = cache;
        }

        /// <summary>
        /// To get system Role details by role name
        /// </summary>
        /// <param name="role"></param>
        /// <returns>SystemRoleDTO</returns>
        public List<SystemRoleDTO> GetSystemRoleByRoleNameAsync(List<string> roles)
        {
            try
            {
                var systemRoles = this._cache.Get<List<SystemRoleDTO>>(PCISEnum.Caching.GetAllSystemRoles);
                if (systemRoles == null || systemRoles?.Count == 0)
                {
                    var systemRolesDB = this.dbContext.SystemRoles.Where(x => !x.IsRemoved).ToList();
                    systemRoles = this.mapper.Map<List<SystemRoleDTO>>(systemRolesDB);
                    this._cache.Post(PCISEnum.Caching.GetAllSystemRoles, systemRoles);
                }
                systemRoles = systemRoles.Where(x => roles.Contains(x.Name)).ToList();
                return systemRoles;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// To get all System Roles
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns>List<SystemRoleDTO></returns>
        public List<SystemRoleDTO> GetAllSystemRoles(int pageNumber, int pageSize)
        {
            try
            {
                var systemRoles = this._cache.Get<List<SystemRoleDTO>>(PCISEnum.Caching.GetAllSystemRoles);
                if (systemRoles == null || systemRoles?.Count == 0)
                {
                    var systemRolesDB = this.dbContext.SystemRoles.Where(x => !x.IsRemoved && !x.IsExternal).ToList();
                    systemRoles = this.mapper.Map<List<SystemRoleDTO>>(systemRolesDB);
                    this._cache.Post(PCISEnum.Caching.GetAllSystemRoles, systemRoles);
                }
                systemRoles = systemRoles.OrderBy(y => y.ListOrder).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                return systemRoles;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get the total count of System Roles
        /// </summary>
        /// <returns>int</returns>
        public int GetSystemRoleCount()
        {
            try
            {
                var systemRoles = this._cache.Get<List<SystemRoleDTO>>(PCISEnum.Caching.GetAllSystemRoles);
                if (systemRoles == null || systemRoles?.Count == 0)
                {
                    var systemRolesDB = this.dbContext.SystemRoles.Where(x => !x.IsRemoved && !x.IsExternal).ToList();
                    systemRoles = this.mapper.Map<List<SystemRoleDTO>>(systemRolesDB);
                    this._cache.Post(PCISEnum.Caching.GetAllSystemRoles, systemRoles);
                }
                return systemRoles.Count();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetSystemRoleByRoleID.
        /// </summary>
        /// <param name="systemRoleID">systemRoleID</param>
        /// <returns>SystemRoleDTO.</returns>
        public SystemRoleDTO GetSystemRoleByRoleID(int systemRoleID)
        {
            try
            {
                var systemRoles = this._cache.Get<List<SystemRoleDTO>>(PCISEnum.Caching.GetAllSystemRoles);
                if (systemRoles == null || systemRoles?.Count == 0)
                {
                    var systemRolesDB = this.dbContext.SystemRoles.Where(x => !x.IsRemoved).ToList();
                    systemRoles = this.mapper.Map<List<SystemRoleDTO>>(systemRolesDB);
                    this._cache.Post(PCISEnum.Caching.GetAllSystemRoles, systemRoles);
                }
                return systemRoles.Where(x => x.SystemRoleID == systemRoleID).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// GetSystemRoleByRoleName.
        /// </summary>
        /// <param name="systemRoleName">systemRoleName</param>
        /// <returns>SystemRoleDTO.</returns>
        public SystemRoleDTO GetSystemRoleByRoleName(string systemRoleName)
        {
            try
            {
                var systemRoles = this._cache.Get<List<SystemRoleDTO>>(PCISEnum.Caching.GetAllSystemRoles);
                if (systemRoles == null || systemRoles?.Count == 0)
                {
                    var systemRolesDB = this.dbContext.SystemRoles.Where(x => !x.IsRemoved).ToList();
                    systemRoles = this.mapper.Map<List<SystemRoleDTO>>(systemRolesDB);
                    this._cache.Post(PCISEnum.Caching.GetAllSystemRoles, systemRoles);
                }
                return systemRoles.Where(x => x.Name == systemRoleName).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
