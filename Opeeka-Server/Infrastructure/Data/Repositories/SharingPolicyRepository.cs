// -----------------------------------------------------------------------
// <copyright file="SharingPolicyRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Infrastructure.Data.Repositories.Base;

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{
    public class SharingPolicyRepository : BaseRepository<SharingPolicy>, ISharingPolicyRepository
    {
        private readonly IMapper mapper;
        private readonly OpeekaDBContext _dbContext;

        public SharingPolicyRepository(OpeekaDBContext dbContext, IMapper mapper)
            : base(dbContext)
        {
            this.mapper = mapper;
            this._dbContext = dbContext;
        }

        /// <summary>
        /// To get all languages.
        /// </summary>
        /// <returns> LanguageDTO.</returns>
        public async Task<List<SharingPolicyDTO>> GetAllSharingPolicy()
        {
            try
            {
                var sharingPolicy = await this.GetAllAsync();
                return this.mapper.Map<List<SharingPolicyDTO>>(sharingPolicy);
            }
            catch (Exception)
            {
                throw;
            }
        }


    }

}
