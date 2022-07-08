// -----------------------------------------------------------------------
// <copyright file="IAddressRepository.cs" company="Naicoits">
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
    /// IAgencyRepository.
    /// </summary>
    public interface IPersonHelperRepository : IAsyncRepository<PersonHelper>
    {
        /// <summary>
        /// To add address details.
        /// </summary>
        /// <param name="personHelperDTO">id.</param>
        /// <returns>Guid.</returns>
        long AddPersonHelper(PersonHelperDTO personHelperDTO);

        /// <summary>
        /// To update address details.
        /// </summary>
        /// <param name="personHelperDTO">id.</param>
        /// <returns>List of summaries.</returns>
        PersonHelperDTO UpdatePersonHelper(PersonHelperDTO personHelperDTO);

        /// <summary>
        /// To get address details.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns>address details..</returns>
        Task<IReadOnlyList<PersonHelperDTO>> GetPersonHelper(long id);
        void AddBulkPersonHelper(List<PersonHelperDTO> personHelperDTO);

        List<PersonHelper> GetPersonHelperFromId(List<long> personHelperId);
        List<PersonHelper> UpdateBulkPersonHelper(List<PersonHelper> personHelper);

        List<PersonHelper> GetPersonHelperByDataId(long id);

        List<PersonHelperDetailsDTO> GetPersonHelperDetails(long personID);
        /// <summary>
        /// GetPersonHelperByPersonHelperId
        /// </summary>
        /// <param name="personHelperId"></param>
        /// <returns></returns>
        PersonHelper GetPersonHelperByPersonHelperId(long personHelperId);

    }
}
