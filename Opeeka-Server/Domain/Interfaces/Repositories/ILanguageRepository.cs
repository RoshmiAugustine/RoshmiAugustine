// -----------------------------------------------------------------------
// <copyright file="ILanguageRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Opeeka.PICS.Domain.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    /// <summary>
    /// ILanguageRepository.
    /// </summary>
    public interface ILanguageRepository
    {
        /// <summary>
        /// To get all languages.
        /// </summary>
        /// <returns> RaceEthnicityDTO.</returns>
        Task<List<LanguageDTO>> GetAllLanguages();

        /// <summary>
        /// AddLanguage
        /// </summary>
        /// <param name="languageDetailsDTO"></param>
        /// <returns>LanguageDTO</returns>
        LanguageDTO AddLanguage(LanguageDTO languageDetailsDTO);

        /// <summary>
        /// UpdateLanguage
        /// </summary>
        /// <param name="languageDetailsDTO"></param>
        /// <returns>LanguageDTO</returns>
        LanguageDTO UpdateLanguage(LanguageDTO languageDetailsDTO);

        /// <summary>
        /// GetLanguage
        /// </summary>
        /// <param name="languageID"></param>
        /// <returns>Task<LanguageDTO></returns>
        Task<LanguageDTO> GetLanguage(int languageID);

        /// <summary>
        /// GetLanguageList
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns>LanguageDTO</returns>
        List<LanguageDTO> GetLanguageList(int pageNumber, int pageSize, long agencyID);

        /// <summary>
        /// GetLanguageCount
        /// </summary>
        /// <returns>int</returns>
        int GetLanguageCount(long agencyID);

        /// <summary>
        /// GetAgencyLanguageList
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>LanguageDTO</returns>
        List<LanguageDTO> GetAgencyLanguageList(long agencyID);
    }
}
