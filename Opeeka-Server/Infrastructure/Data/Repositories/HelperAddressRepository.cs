// -----------------------------------------------------------------------
// <copyright file="HelperAddressRepository.cs" company="Naicoits">
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

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{
    public class HelperAddressRepository : BaseRepository<HelperAddress>, IHelperAddressRepository
    {
        private readonly IMapper mapper;
        private readonly ILogger<HelperAddressRepository> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddressRepository"/> class.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public HelperAddressRepository(ILogger<HelperAddressRepository> logger, OpeekaDBContext dbContext, IMapper mapper)
            : base(dbContext)
        {
            this.mapper = mapper;
            this.logger = logger;
        }

        /// <summary>
        /// Save helper address details.
        /// </summary>
        /// <param name="HelperID"></param>
        /// <param name="AddressID"></param>
        public void CreateHelperAddress(int HelperID, long AddressID)
        {
            try
            {
                HelperAddress helperAddress = new HelperAddress();
                helperAddress.HelperID = HelperID;
                helperAddress.AddressID = AddressID;
                helperAddress.IsPrimary = true;
                helperAddress = this.AddAsync(helperAddress).Result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// to get helper address by HelperID
        /// </summary>
        /// <param name="HelperID"></param>
        /// <returns>HelperAddressDTO</returns>
        public async Task<HelperAddressDTO> GetHelperAddressByHelperIDAsync(long HelperID)
        {
            try
            {
                HelperAddressDTO helperAddressDTO = new HelperAddressDTO();
                HelperAddress helperAddress = await this.GetRowAsync(x => x.HelperID == HelperID);
                this.mapper.Map<HelperAddress, HelperAddressDTO>(helperAddress, helperAddressDTO);
                return helperAddressDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
