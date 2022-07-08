// -----------------------------------------------------------------------
// <copyright file="UserRepository.cs" company="Naicoits">
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
using Opeeka.PICS.Infrastructure.Enums;
using Opeeka.PICS.Domain.Interfaces;

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{
    public class ApplicationObjectTypeRepository : BaseRepository<ApplicationObjectType>, IApplicationObjectTypeRepository
    {
        private readonly IMapper mapper;
        private readonly ILogger<UserRepository> logger;
        private readonly ICache _cache;
        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public ApplicationObjectTypeRepository(ILogger<UserRepository> logger, OpeekaDBContext dbContext, IMapper mapper, ICache cache)
            : base(dbContext)
        {
            this.mapper = mapper;
            this.logger = logger;
            this._cache = cache;
        }

        /// <summary>
        /// Save user details.
        /// </summary>
        /// <param name="userdto"></param>
        /// <returns>Object of Users</returns>
        public async Task<List<ApplicationObjectTypeDTO>> GetApplicationObjectTypes()
        {
            try
            {
                List<ApplicationObjectTypeDTO> applicationObjectTypeList = new List<ApplicationObjectTypeDTO>();
                applicationObjectTypeList = this._cache.Get<List<ApplicationObjectTypeDTO>>(PCISEnum.Caching.GetApplicationObjectTypeList);
                if (applicationObjectTypeList == null || applicationObjectTypeList?.Count == 0)
                {
                    var applicationObjectTypes = await this.GetAllAsync();
                    applicationObjectTypeList = this.mapper.Map<List<ApplicationObjectTypeDTO>>(applicationObjectTypes);
                    TimeSpan span = new TimeSpan(1, 00, 0);
                    this._cache.Post(PCISEnum.Caching.GetApplicationObjectTypeList, applicationObjectTypeList, span);
                }
                return applicationObjectTypeList;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
