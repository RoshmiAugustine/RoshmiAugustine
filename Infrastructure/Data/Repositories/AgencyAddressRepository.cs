// -----------------------------------------------------------------------
// <copyright file="AgencyAddressRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.Threading.Tasks;
using AutoMapper;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Infrastructure.Data.Repositories.Base;

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{
    public class AgencyAddressRepository : BaseRepository<AgencyAddress>, IAgencyAddressRepository
    {
        private readonly IMapper mapper;

        public AgencyAddressRepository(OpeekaDBContext dbContext, IMapper mapper)
            : base(dbContext)
        {
            this.mapper = mapper;
        }

        /// <summary>
        /// To add agent address details.
        /// </summary>
        /// <param agencyAddressDTO="agencyAddressDTO">id.</param>
        /// <returns>Guid.</returns>
        public long AddAgencyAddress(AgencyAddressDTO agencyAddressDTO)
        {
            try
            {
                AgencyAddress agencyAddress = new AgencyAddress();
                this.mapper.Map<AgencyAddressDTO, AgencyAddress>(agencyAddressDTO, agencyAddress);
                var result = this.AddAsync(agencyAddress).Result.AgencyAddressID;
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// To get details agencyaddress.
        /// </summary>
        /// <param agencyAddressDTO="agencyAddressDTO">id.</param>
        /// <returns>.AgencyAddressDTO</returns>
        public async Task<AgencyAddressDTO> GetAgencyAddress(long id)
        {
            try
            {
                AgencyAddressDTO agencyAddressDTO = new AgencyAddressDTO();
                AgencyAddress agencyAddress = await this.GetRowAsync(x => x.AgencyID == id);
                this.mapper.Map<AgencyAddress, AgencyAddressDTO>(agencyAddress, agencyAddressDTO);

                return agencyAddressDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

}
