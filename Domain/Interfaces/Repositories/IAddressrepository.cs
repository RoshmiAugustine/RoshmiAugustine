// -----------------------------------------------------------------------
// <copyright file="IAddressRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    /// <summary>
    /// IAgencyRepository.
    /// </summary>
    public interface IAddressrepository : IAsyncRepository<Address>
    {
        /// <summary>
        /// To add address details.
        /// </summary>
        /// <param name="addressDTO">id.</param>
        /// <returns>Guid.</returns>
        long AddAddress(AddressDTO addressDTO);

        /// <summary>
        /// To update address details.
        /// </summary>
        /// <param name="addressDTO">id.</param>
        /// <returns>List of summaries.</returns>
        AddressDTO UpdateAddress(AddressDTO addressDTO);

        /// <summary>
        /// To get address details.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns>address details..</returns>
        Task<AddressDTO> GetAddress(long id);
        Task<List<AddressDTO>> GetAddressListByIndex(List<Guid> index);
    }
}
