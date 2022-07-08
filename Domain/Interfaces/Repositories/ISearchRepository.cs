using System.Collections.Generic;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.DTO.Input;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    public interface ISearchRepository
    {
        /// <summary>
        /// UpperPaneSearch.
        /// </summary>
        /// <param name="userID">userID.</param>
        /// <param name="agencyID">agencyID.</param>
        /// <param name="search">search</param>
        /// <returns>UpperPaneSearchResponseDTO.</returns>
        List<UpperpaneSearchDTO> GetUpperpaneSearchResults(UpperpaneSearchKeyDTO upperpaneSearchKeyDTO, string role, long agencyID, int helperID, string sharedPersonQuery);
    }
}
