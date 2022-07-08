// -----------------------------------------------------------------------
// <copyright file="IRaceEthnicityRepository.cs" company="Naicoits">
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
    /// IRaceEthnicityRepository.
    /// </summary>
    public interface IRaceEthnicityRepository
    {
        /// <summary>
        /// To get all race details.
        /// </summary>
        /// <returns> RaceEthnicityDTO.</returns>
        Task<List<RaceEthnicityDTO>> GetAllRaceEthnicity();

        /// <summary>
        /// GetRaceEthnicityList.
        /// </summary>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <returns>List of RaceEthnicity</returns>
        List<RaceEthnicity> GetRaceEthnicityList(int pageNumber, int pageSize, long agencyID);

        /// <summary>
        /// GetRaceEthnicityCount.
        /// </summary>
        /// <returns>int.</returns>
        int GetRaceEthnicityCount(long agencyID);

        /// <summary>
        /// AddRaceEthnicity.
        /// </summary>
        /// <param name="RaceEthnicity">RaceEthnicity.</param>
        /// <returns>RaceEthnicity.</returns>
        RaceEthnicity AddRaceEthnicity(RaceEthnicity RaceEthnicity);

        /// <summary>
        /// UpdateRaceEthnicity.
        /// </summary>
        /// <param name="RaceEthnicity">RaceEthnicity.</param>
        /// <returns>RaceEthnicity.</returns>
        RaceEthnicity UpdateRaceEthnicity(RaceEthnicity RaceEthnicity);

        /// <summary>.
        /// GetRaceEthnicity
        /// </summary>
        /// <param name="identificationTypeID">identificationTypeID.</param>
        /// <returns>Task of RaceEthnicity.</returns>
        Task<RaceEthnicity> GetRaceEthnicity(long identificationTypeID);

        /// <summary>
        /// GetRaceEthnicityUsedByID.
        /// </summary>
        /// <param name="identificationTypeID">identificationTypeID.</param>
        /// <returns>count.</returns>
        int GetRaceEthnicityUsedByID(long identificationTypeID);

        /// <summary>
        /// GetAgencyRaceEthnicityList
        /// </summary>
        /// <param name="personID"></param>
        /// <returns>List of PeopleRaceEthnicityDTO</returns>
        List<RaceEthnicityDTO> GetAgencyRaceEthnicityList(long agencyID);

        /// <summary>
        /// GetRaceEthnicityDetailsByName.
        /// </summary>
        /// <param name="nameCSV">nameCSV.</param>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>RaceEthnicityDTO.</returns>
        List<RaceEthnicityDTO> GetRaceEthnicityDetailsByName(string nameCSV, long agencyID);
    }
}
