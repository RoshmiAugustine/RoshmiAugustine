// -----------------------------------------------------------------------
// <copyright file="AgencySharingRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using AutoMapper;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Infrastructure.Data.Repositories.Base;
using System;
using System.Threading.Tasks;

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{
    public class AgencySharingRepository : BaseRepository<AgencySharing>, IAgencySharingRepository
    {
        private readonly IMapper mapper;
        private readonly OpeekaDBContext _dbContext;

        public AgencySharingRepository(OpeekaDBContext dbContext, IMapper mapper)
            : base(dbContext)
        {
            this.mapper = mapper;
            this._dbContext = dbContext;
        }

        public AgencySharingDTO AddAgencySharing(AgencySharingDTO agencySharingDTO)
        {
            try
            {
                AgencySharing agencySharing = new AgencySharing();
                this.mapper.Map<AgencySharingDTO, AgencySharing>(agencySharingDTO, agencySharing);
                var result = this.AddAsync(agencySharing).Result;
                this.mapper.Map<AgencySharing, AgencySharingDTO>(result, agencySharingDTO);
                return agencySharingDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// To update AgencySharing details.
        /// </summary>
        /// <param name="agencySharingDTO">id.</param>
        /// <returns>List of summaries.</returns>
        public AgencySharing UpdateAgencySharing(AgencySharing agencySharing)
        {
            try
            {
                var result = this.UpdateAsync(agencySharing).Result;
                return agencySharing;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// To get details AgencySharing.
        /// </summary>
        /// <param agencyAddressDTO="agencyAddressDTO">id.</param>
        /// <returns>.AgencyAddressDTO</returns>
        public async Task<AgencySharing> GetAgencySharing(Guid id)
        {
            try
            {
                AgencySharing agencySharing = await this.GetRowAsync(x => x.AgencySharingIndex == id);
                return agencySharing;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
