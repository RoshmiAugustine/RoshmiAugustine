// -----------------------------------------------------------------------
// <copyright file="IHelperRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.DTO.ExternalAPI;
using Opeeka.PICS.Domain.DTO.Input;
using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    /// <summary>
    /// IHelperRepository.
    /// </summary>
    public interface IHelperRepository : IAsyncRepository<Helper>
    {
        /// <summary>
        /// To post helpers details.
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="UserID"></param>
        /// <returns>Object of helper</returns>
        Helper CreateHelper(HelperDTO helper, int UserID);
        /// <summary>
        /// GetHelperDetails.
        /// </summary>
        /// <param name="helperData"></param>
        /// <param name="from"></param>
        /// <param name="tenantID"></param>
        /// <returns></returns>
        List<HelperDataDTO> GetHelperDetails(int userId, int pageNumber, int pageSize, string from, long tenantID);
        /// <summary>
        /// GetHelperDetailsCount.
        /// </summary>
        /// <param name="helperData"></param>
        /// <param name="from"></param>
        /// <param name="tenantID"></param>
        /// <returns></returns>
        int GetHelperDetailsCount(int userId, string from, long tenantID);

        /// <summary>
        /// Get Gelper details.
        /// </summary>
        /// <param name="helperID"></param>
        /// <returns></returns>
        HelperInfoDTO GetHelperInfo(Guid helperIndex,long agencyID);

        /// <summary>
        /// To get Helper details by Helper Index.
        /// </summary>
        /// <param name="HelperIndex"></param>
        /// <returns>HelperDTO</returns>
        Task<Helper> GetHelperByIndexAsync(Guid? HelperIndex);

        /// <summary>
        /// To update Helper details.
        /// </summary>
        /// <param name="helperDTO"></param>
        /// <returns>HelperDTO</returns>
        HelperDTO UpdateHelper(Helper helper);

        /// <summary>
        /// To get helpers count having a specific Helper title
        /// </summary>
        /// <param name="helperTitleID"></param>
        /// <returns>int</returns>
        int GetHelperCountByHelperTitle(int helperTitleID);

        /// <summary>
        /// GetUserDetails.
        /// </summary>
        /// <param name="userID">userID.</param>
        /// <returns>UserDetailsResponseDTO.</returns>
        UserDetailsDTO GetUserDetails(Int64 userID);

        /// <summary>
        /// GetAllManager
        /// </summary>
        /// <returns>Helper</returns>
        List<Helper> GetAllManager(long agencyID, string OrgAdminRO, string OrgAdminRW, string Supervisor, bool activeHelpers = true);

        /// <summary>
        /// GetHelperDetailsByHelperID.
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="role"></param>
        /// <param name="agencyID"></param>
        /// <param name="helperID"></param>
        /// <returns></returns>
        Tuple<List<HelperDataDTO>, int> GetHelperDetailsByHelperID(HelperSearchDTO helperSearchDTO, DynamicQueryBuilderDTO queryBuilderDTO);

        /// <summary>
        /// GetHelperDetailsByHelperIDCount.
        /// </summary>
        /// <param name="role"></param>
        /// <param name="agencyID">agencyID</param>
        /// <param name="helperID">helperID</param>
        /// <returns></returns>
        int GetHelperDetailsByHelperIDCount(string role, Int64 agencyID, int? helperID = null);

        /// <summary>
        /// GetHelperUsedCount.
        /// </summary>
        /// <param name="helperID">helperID.</param>
        /// <returns>int.</returns>
        int GetHelperUsedCount(int helperID);

        Task<bool> ValidateHelperExternalID(HelperDetailsDTO helperDetailsDTO, long agencyID);

        /// <summary>
        /// GetAllExternalHelpersForAgency.
        /// </summary>
        /// <param name="agencyID"></param>
        /// <returns></returns>
        List<Helper> GetAllExternalHelpersForAgency(long agencyID);
        /// <summary>
        /// GetHelperDetailsByHelperEmail.
        /// </summary>
        /// <param name="helperEmailCSV">helperEmailCSV.</param>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>HelperInfoDTO List.</returns>
        List<HelperInfoDTO> GetHelperDetailsByHelperEmail(string helperEmailCSV, long agencyID);

        /// <summary>
        /// AddHelperBulk.
        /// </summary>
        /// <param name="helperDTOList">helperDTOList.</param>
        /// <returns>HelperDTO.</returns>
        List<HelperDTO> AddHelperBulk(List<HelperDTO> helperDTOList);

        /// <summary>
        /// GetHelperListByGUID.
        /// </summary>
        /// <param name="helperIndexGuids">helperIndexGuids.</param>
        /// <returns>Helper.</returns>
        Task<IReadOnlyList<Helper>> GetHelperListByGUID(List<Guid> helperIndexGuids);

        /// <summary>
        /// updateHelperDTOList.
        /// </summary>
        /// <param name="updateHelperDTOList">updateHelperDTOList.</param>
        void UpdateHelperBulk(List<HelperDTO> updateHelperDTOList);

        /// <summary>
        /// GetHelpersDetailsListForExternal
        /// </summary>
        /// <param name="loggedInUserDTO"></param>
        /// <param name="queryBuilderDTO"></param>
        /// <param name="helperSearchInputDTO"></param>
        /// <returns>HelperDetailsListDTO</returns>
        List<HelperDetailsListDTO> GetHelpersDetailsListForExternal(LoggedInUserDTO loggedInUserDTO, DynamicQueryBuilderDTO queryBuilderDTO, HelperSearchInputDTO helperSearchInputDTO);
        /// <summary>
        /// GetHelperPersonCollaborationData as Rows.
        /// </summary>
        /// <param name="helperID"></param>
        /// <param name="loggedInAgencyID"></param>
        /// <returns></returns>
        List<HelperPersonCollaborationDTO> GetHelperPersonCollaborationData(int helperID, long loggedInAgencyID);
        /// <summary>
        /// GetHelperPersonCollaborationDetails in a single object.
        /// </summary>
        /// <param name="helperID"></param>
        /// <param name="loggedInAgencyID"></param>
        /// <returns></returns>
        HelperPersonCollaborationDetailsDTO GetHelperPersonCollaborationDetails(int helperID, long loggedInAgencyID);
        /// <summary>
        /// GetHelperPersonInCollaborationDetails
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="loggedInAgencyID"></param>
        /// <returns></returns>
        List<string> GetHelperPersonInCollaborationDetails(int userID, long loggedInAgencyID);
    }
}
