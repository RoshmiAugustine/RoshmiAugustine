using System.Collections.Generic;
using System.Threading.Tasks;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    public interface IIdentifiedGenderRepository
    {
        /// <summary>
        /// AddIdentifiedGender.
        /// </summary>
        /// <param name="identifiedGender">identifiedGender.</param>
        /// <returns>IdentifiedGender.</returns>
        IdentifiedGender AddIdentifiedGender(IdentifiedGender identifiedGender);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="identifiedGenderID">identifiedGenderID.</param>
        /// <returns>IdentifiedGender.</returns>
        Task<IdentifiedGender> GetIdentifiedGender(int identifiedGenderID);

        /// <summary>
        /// UpdateIdentifiedGender.
        /// </summary>
        /// <param name="identifiedGender">identifiedGender.</param>
        /// <returns>IdentifiedGender.</returns>
        IdentifiedGender UpdateIdentifiedGender(IdentifiedGender identifiedGender);

        /// <summary>
        /// GetIdentifiedGenderList.
        /// </summary>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <param name="agencyID">agencyID</param>
        /// <returns>IdentifiedGender</returns>
        List<IdentifiedGender> GetIdentifiedGenderList(int pageNumber, int pageSize, long agencyID);

        /// <summary>
        /// GetIdentifiedGenderCount.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>int.</returns>
        int GetIdentifiedGenderCount(long agencyID);

        /// <summary>
        /// GetAgencyIdentifiedGenderList.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>IdentifiedGenderDTO.</returns>
        List<IdentifiedGender> GetIdentifiedGenderList(long agencyID);

        /// <summary>
        /// Get count of people by Identified Gender
        /// </summary>
        /// <param name="identifiedGenderID">identifiedGenderID</param>
        /// <returns>int</returns>
        int GetPeopleCountByIdentifiedGenderID(int identifiedGenderID);

        /// <summary>
        /// GetIdentifiedGenderDetailsByName.
        /// </summary>
        /// <param name="nameCSV">nameCSV.</param>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>IdentifiedGenderDTO.</returns>
        List<IdentifiedGenderDTO> GetIdentifiedGenderDetailsByName(string nameCSV, long agencyID);
    }
}
