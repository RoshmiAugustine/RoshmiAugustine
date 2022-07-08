// -----------------------------------------------------------------------
// <copyright file="ILanguageService.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.DTO.Output;

namespace Opeeka.PICS.Domain.Interfaces.Services
{
    /// <summary>
    /// ILanguageService.
    /// </summary>
    public interface ILanguageService
    {
        /// <summary>
        /// To add languag.
        /// </summary>
        /// <param name="languageData"></param>
        /// <returns>Object of CRUDResponseDTO</returns>
        CRUDResponseDTO AddLanguage(LanguageDetailsDTO languageData, int userID, long agencyID);

        /// <summary>
        /// UpdateLanguage
        /// </summary>
        /// <param name="languageData"></param>
        /// <returns>CRUDResponseDTO</returns>
        CRUDResponseDTO UpdateLanguage(LanguageDetailsDTO languageData, int userID, long agencyID);

        /// <summary>
        /// DeleteLanguage
        /// </summary>
        /// <param name="languageID"></param>
        /// <returns>CRUDResponseDTO</returns>
        CRUDResponseDTO DeleteLanguage(int languageID, long agencyID);

        /// <summary>
        /// GetLanguageList
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns>LanguageListResponseDTO</returns>
        LanguageListResponseDTO GetLanguageList(int pageNumber, int pageSize, long agencyID);
    }
}
