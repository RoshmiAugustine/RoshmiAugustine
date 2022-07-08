// -----------------------------------------------------------------------
// <copyright file="IGenderRepository.cs" company="Naicoits">
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
    public interface IGenderRepository : IAsyncRepository<Gender>
    {
        /// <summary>
        /// To add gender details.
        /// </summary>
        /// <returns> CountryStateDTO.</returns>
        Task<List<GenderDTO>> GetAllGenders();

        /// <summary>
        /// GetGenderList.
        /// </summary>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>List of GenderDTO</returns>
        List<GenderDTO> GetGenderList(int pageNumber, int pageSize, long agencyID);

        /// <summary>
        /// GetNotificationLevelCountByNotificationLevelID
        /// </summary>
        /// <param name="notificationLevelID">notificationLevelID</param>
        /// <returns>int</returns>
        int GetGenderCountByGenderID(int genderID);

        /// <summary>
        /// GetGenderCount.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>int.</returns>
        int GetGenderCount(long agencyID);

        /// <summary>
        /// AddGender.
        /// </summary>
        /// <param name="GenderDTO">GenderDTO.</param>
        /// <returns>GenderDTO.</returns>
        GenderDTO AddGender(GenderDTO GenderDTO);

        /// <summary>
        /// UpdateGender.
        /// </summary>
        /// <param name="GenderDTO">GenderDTO.</param>
        /// <returns>GenderDTO.</returns>
        GenderDTO UpdateGender(GenderDTO GenderDTO);

        /// <summary>.
        /// GetGender
        /// </summary>
        /// <param name="collaborationLevelID">collaborationLevelID.</param>
        /// <returns>Task of GenderDTO.</returns>
        Task<GenderDTO> GetGender(Int64 collaborationLevelID);

        /// <summary>
        /// GetAgencyGenderList.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>List of GenderDTO.</returns>
        List<GenderDTO> GetAgencyGenderList(long agencyID);
    }
}
