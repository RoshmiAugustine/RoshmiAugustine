// -----------------------------------------------------------------------
// <copyright file="AgencySharingPolicyRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using AutoMapper;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Infrastructure.Data.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{
    public class AgencySharingPolicyRepository : BaseRepository<AgencySharingPolicy>, IAgencySharingPolicyRepository
    {
        private readonly IMapper mapper;
        private readonly OpeekaDBContext _dbContext;

        public AgencySharingPolicyRepository(OpeekaDBContext dbContext, IMapper mapper)
            : base(dbContext)
        {
            this.mapper = mapper;
            this._dbContext = dbContext;
        }

        public AgencySharingPolicyDTO AddAgencySharingPolicy(AgencySharingPolicyDTO agencySharingPolicyDTO)
        {
            try
            {
                AgencySharingPolicy agencySharingPolicy = new AgencySharingPolicy();
                this.mapper.Map<AgencySharingPolicyDTO, AgencySharingPolicy>(agencySharingPolicyDTO, agencySharingPolicy);
                var result = this.AddAsync(agencySharingPolicy).Result;
                this.mapper.Map<AgencySharingPolicy, AgencySharingPolicyDTO>(result, agencySharingPolicyDTO);
                return agencySharingPolicyDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// To update AgencySharing details.
        /// </summary>
        /// <param name="agencySharingPolicyDTO">id.</param>
        /// <returns>List of summaries.</returns>
        public AgencySharingPolicyDTO UpdateAgencySharingPolicy(AgencySharingPolicyDTO agencySharingPolicyDTO)
        {
            try
            {
                AgencySharingPolicy agencySharingPolicy = new AgencySharingPolicy();
                this.mapper.Map<AgencySharingPolicyDTO, AgencySharingPolicy>(agencySharingPolicyDTO, agencySharingPolicy);
                var result = this.UpdateAsync(agencySharingPolicy).Result;
                AgencySharingPolicyDTO updated = new AgencySharingPolicyDTO();
                this.mapper.Map<AgencySharingPolicy, AgencySharingPolicyDTO>(result, updated);
                return updated;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// To get details AgencySharingPolicy.
        /// </summary>
        /// <param AgencySharingPolicyDTO="AgencySharingPolicyDTO">id.</param>
        /// <returns>.AgencyAddressDTO</returns>
        public async Task<AgencySharingPolicyDTO> GetAgencySharingPolicy(long id)
        {
            try
            {
                AgencySharingPolicyDTO agencySharingPolicyDTO = new AgencySharingPolicyDTO();
                AgencySharingPolicy agencySharingPolicy = await this.GetRowAsync(x => x.AgencySharingPolicyID == id);
                this.mapper.Map<AgencySharingPolicy, AgencySharingPolicyDTO>(agencySharingPolicy, agencySharingPolicyDTO);

                return agencySharingPolicyDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// GetAgencySharingPolicy
        /// </summary>
        /// <returns></returns>
        public async Task<List<AgencySharingPolicyDTO>> GetAgencySharingPolicy()
        {
            try
            {
                var AgencySharingPolicyList = await this.GetAllAsync();
                return this.mapper.Map<List<AgencySharingPolicyDTO>>(AgencySharingPolicyList);
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
