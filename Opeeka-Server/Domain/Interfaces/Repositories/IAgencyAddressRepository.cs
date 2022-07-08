// -----------------------------------------------------------------------
// <copyright file="IAgencyRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using System.Threading.Tasks;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    /// <summary>
    /// IAgencyRepository.
    /// </summary>
    public interface IAgencyAddressRepository : IAsyncRepository<AgencyAddress>
    {
        /// <summary>
        /// To add agent address details.
        /// </summary>
        /// <param agencyAddressDTO="agencyAddressDTO">id.</param>
        /// <returns>Guid.</returns>
        long AddAgencyAddress(AgencyAddressDTO agencyAddressDTO);

        /// <summary>
        /// To get details agencyaddress.
        /// </summary>
        /// <param agencyAddressDTO="agencyAddressDTO">id.</param>
        /// <returns>.AgencyAddressDTO</returns>
        Task<AgencyAddressDTO> GetAgencyAddress(long id);
    }
}
