// -----------------------------------------------------------------------
// <copyright file="IPersonSupportRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.DTO.ExternalAPI;
using Opeeka.PICS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    /// <summary>
    /// IAgencyRepository.
    /// </summary>
    public interface IPersonSupportRepository : IAsyncRepository<PersonSupport>
    {
        /// <summary>
        /// To add address details.
        /// </summary>
        /// <param name="personSupportDTO">id.</param>
        /// <returns>Guid.</returns>
        int AddPersonSupport(PersonSupportDTO personSupportDTO);

        /// <summary>
        /// To update address details.
        /// </summary>
        /// <param name="personSupportDTO">id.</param>
        /// <returns>List of summaries.</returns>
        PersonSupportDTO UpdatePersonSupport(PersonSupportDTO personSupportDTO);

        /// <summary>
        /// To get address details.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns>address details..</returns>
        List<PersonSupportDTO> GetPersonSupport(long id);

        void AddBulkPersonSupport(List<PersonSupportDTO> personSupportDTO);
        List<PersonSupport> GetPersonSupportFromId(List<int> personSupportId);
        List<PersonSupport> UpdateBulkPersonSupport(List<PersonSupport> personSupport);
        List<PersonSupport> GetPersonSupportByDataId(long id);
        PersonSupportDTO GetPersonSupportDetails(int id);

        /// <summary>
        /// GetPeopleSupportListForExternal.
        /// </summary>
        /// <param name="loggedInUserDTO"></param>
        /// <param name="queryBuilderDTO"></param>
        /// <param name="personSearchInputDTO"></param>
        /// <returns></returns>
        List<SupportDetailsListDTO> GetPersonSupportListForExternal(LoggedInUserDTO loggedInUserDTO, DynamicQueryBuilderDTO queryBuilderDTO);
    }
}
