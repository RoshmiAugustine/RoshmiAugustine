// -----------------------------------------------------------------------
// <copyright file="IPersonRaceEthnicityRepository.cs" company="Naicoits">
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
    public interface IPersonRaceEthnicityRepository : IAsyncRepository<PersonRaceEthnicity>
    {
        /// <summary>
        /// To add address details.
        /// </summary>
        /// <param name="personRaceEthnicityDTO">id.</param>
        /// <returns>Guid.</returns>
        long AddRaceEthnicity(PersonRaceEthnicityDTO personRaceEthnicityDTO);

        /// <summary>
        /// To update address details.
        /// </summary>
        /// <param name="personRaceEthnicityDTO">id.</param>
        /// <returns>List of summaries.</returns>
        PersonRaceEthnicityDTO UpdateRaceEthnicity(PersonRaceEthnicityDTO personRaceEthnicityDTO);

        /// <summary>
        /// To get address details.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns>address details..</returns>
        Task<IReadOnlyList<PersonRaceEthnicityDTO>> GetRaceEthnicity(long id);

        /// <summary>
        /// DeleteRaceEthnicity.
        /// </summary>
        /// <param name="PersonRaceEthnicityDTO">PersonRaceEthnicityDTO</param>
        void DeleteRaceEthnicity(PersonRaceEthnicityDTO PersonRaceEthnicityDTO);

        void AddBulkRaceEthnicity(List<PersonRaceEthnicityDTO> personRaceEthnicityDTO);
        List<PersonRaceEthnicity> GetRaceEthnicityByid(long id);
        List<PersonRaceEthnicity> UpdateBulkPersonRaceEthnicity(List<PersonRaceEthnicity> personRaceEthnicity);
        List<PersonRaceEthnicity> GetPersonRaceEthnicityFromId(List<long> personRaceEthnicityId);
    }
}
