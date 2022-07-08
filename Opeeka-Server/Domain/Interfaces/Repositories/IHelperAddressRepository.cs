// -----------------------------------------------------------------------
// <copyright file="IHelperAddressRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System.Threading.Tasks;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    public interface IHelperAddressRepository : IAsyncRepository<HelperAddress>
    {
        /// <summary>
        /// To post helperAddress details.
        /// </summary>
        /// <param name="HelperID"></param>
        /// <param name="AddressID"></param>
        void CreateHelperAddress(int HelperID, long AddressID);

        /// <summary>
        /// To get Helper Address details by HelperID
        /// </summary>
        /// <param name="HelperID"></param>
        /// <returns></returns>
        Task<HelperAddressDTO> GetHelperAddressByHelperIDAsync(long HelperID);
    }
}
