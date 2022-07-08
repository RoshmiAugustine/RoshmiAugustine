// -----------------------------------------------------------------------
// <copyright file="IHelperTitleRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Opeeka.PICS.Domain.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    public interface IHelperTitleRepository
    {
        /// <summary>
        /// Get all Helper titles
        /// </summary>
        /// <returns>HelperTitleDTO</returns>
        Task<List<HelperTitleDTO>> GetAllHelperTitle();

        /// <summary>
        /// Get Helper Title list paginated
        /// </summary>
        /// <param name="pageNumber">pageNumber</param>
        /// <param name="pageSize">pageSize</param>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>HelperTitleDTO</returns> 
        List<HelperTitleDTO> GetHelperTitleList(int pageNumber, int pageSize, long agencyID);

        /// <summary>
        /// Get Helper Title list for an agency paginated
        /// </summary>
        /// <param name="agencyId"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns>HelperTitleDTO</returns>
        List<HelperTitleDTO> GetHelperTitleList(long agencyId, int pageNumber, int pageSize);

        /// <summary>
        /// Get the total count of Helper Titles
        /// </summary>
        /// <returns>int</returns>
        int GetHelperTitleCount(long agencyID);

        /// <summary>
        /// Add a new Helper title
        /// </summary>
        /// <param name="helperTitleDetailsDTO"></param>
        /// <returns>HelperTitleDTO</returns>
        HelperTitleDTO AddHelperTitle(HelperTitleDTO helperTitleDetailsDTO);

        /// <summary>
        /// Update an existing Helper title
        /// </summary>
        /// <param name="helperTitleDetailsDTO"></param>
        /// <returns>HelperTitleDTO</returns>
        HelperTitleDTO UpdateHelperTitle(HelperTitleDTO helperTitleDetailsDTO);

        /// <summary>
        /// Get one Helper title by Id
        /// </summary>
        /// <param name="helperTitleID"></param>
        /// <returns>Task<HelperTitleDTO></returns>
        Task<HelperTitleDTO> GetHelperTitle(int helperTitleID);

        /// <summary>
        /// Get the Helper Title for an agency.
        /// </summary>
        /// <param name="agencyId"></param>
        /// <returns>HelperTitleDTO</returns>
        List<HelperTitleDTO> GetAgencyHelperTitleList(long agencyId);
    }
}
