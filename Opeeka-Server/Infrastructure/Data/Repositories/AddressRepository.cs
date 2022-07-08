// -----------------------------------------------------------------------
// <copyright file="AddressRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Infrastructure.Data.Repositories.Base;

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{
    public class AddressRepository : BaseRepository<Address>, IAddressrepository
    {
        private readonly IMapper mapper;
        private readonly ILogger<AddressRepository> logger;
        private readonly OpeekaDBContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddressRepository"/> class.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public AddressRepository(ILogger<AddressRepository> logger, OpeekaDBContext dbContext, IMapper mapper)
            : base(dbContext)
        {
            this.mapper = mapper;
            this.logger = logger;
            _dbContext = dbContext;
        }

        /// <summary>
        /// To add address details.
        /// </summary>
        /// <param name="addressDTO"></param>
        /// <returns>Guid.</returns>
        public long AddAddress(AddressDTO addressDTO)
        {
            try
            {
                Address address = new Address();
                this.mapper.Map<AddressDTO, Address>(addressDTO, address);
                var result = this.AddAsync(address).Result.AddressID;
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// To update agent details.
        /// </summary>
        /// <param name="addressDTO">id.</param>
        /// <returns>List of summaries.</returns>
        public AddressDTO UpdateAddress(AddressDTO addressDTO)
        {
            try
            {
                // Address address = _dbContext.Address.Where(x => x.AddressID == addressDTO.AddressID).FirstOrDefault();
                Address address = new Address();
                this.mapper.Map<AddressDTO, Address>(addressDTO, address);
                var result = this.UpdateAsync(address).Result;
                AddressDTO updatedAddress = new AddressDTO();
                this.mapper.Map<Address, AddressDTO>(result, updatedAddress);
                return updatedAddress;
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
        public async Task<AddressDTO> GetAddress(long id)
        {
            try
            {
                AddressDTO addressDTO = new AddressDTO();
                Address address = await this.GetRowAsync(x => x.AddressID == id);
                this.mapper.Map<Address, AddressDTO>(address, addressDTO);

                return addressDTO;
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
        public async Task<List<AddressDTO>> GetAddressListByIndex(List<Guid> index)
        {
            try
            {
                List<AddressDTO> addressDTO = new List<AddressDTO>();
                var address = this.GetAsync(x => index.Contains(x.AddressIndex)).Result;
                this.mapper.Map<IReadOnlyList<Address>, List<AddressDTO>>(address, addressDTO);

                return addressDTO;
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
