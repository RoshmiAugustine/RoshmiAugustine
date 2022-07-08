// -----------------------------------------------------------------------
// <copyright file="IPersonRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    /// <summary>
    /// IPersonRepository.
    /// </summary>
    public interface IPersonAddressRepository : IAsyncRepository<PersonAddress>
    {

        /// <summary>
        /// To add person address details.
        /// </summary>
        /// <param agencyAddressDTO="personAddressDTO">id.</param>
        /// <returns>Guid.</returns>
        long AddPersonAddress(PersonAddressDTO personAddressDTO);


        /// <summary>
        /// To get address details.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns>address details..</returns>
        Task<PersonAddressDTO> GetPersonAddress(long id);

        void AddBulkPersonAddress(List<PersonAddressDTO> personAddressDTOList);
    }
}
