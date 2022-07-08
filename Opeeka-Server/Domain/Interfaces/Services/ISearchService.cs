// -----------------------------------------------------------------------
// <copyright file="ISearchService.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System.Collections.Generic;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.DTO.Input;

namespace Opeeka.PICS.Domain.Interfaces.Services
{
    public interface ISearchService
    {
        /// <summary>
        /// UpperPaneSearch.
        /// </summary>
        /// <param name="helperID">helperID.</param>
        /// <param name="agencyID">agencyID.</param>
        /// <param name="searchKey">searchKey</param>
        /// <param name="pageNo">pageNo.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <param name="roles">roles.</param>
        /// <returns>UpperPaneSearchResponseDTO.</returns>
        UpperpaneSearchResponseDTO GetUpperpaneSearchResults(UpperpaneSearchKeyDTO upperpaneSearchKeyDTO, long agencyID, List<string> roles, int helperID);
    }
}
