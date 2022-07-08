// -----------------------------------------------------------------------
// <copyright file="ISexualityrepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.DTO.ExternalAPI;
using Opeeka.PICS.Domain.DTO.Input;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    /// <summary>
    /// ICollaborationRepository.
    /// </summary>
    public interface ICollaborationRepository
    {

        /// <summary>
        /// To add collaboration details.
        /// </summary>
        /// <param name="collaborationDetailsDTO">collaborationDetailsDTO.</param>
        /// <returns>id.</returns>
        int AddCollaboration(CollaborationDTO collaborationDTO);

        /// <summary>
        /// To get collaboration details.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns>CollaborationDTO.</returns>
        Task<CollaborationDTO> GetAsync(Guid id);

        /// <summary>
        /// To update collaboration details.
        /// </summary>
        /// <param name="CollaborationDTO">CollaborationDTO.</param>
        /// <returns>CollaborationDTO.</returns>
        CollaborationDTO UpdateCollaboration(CollaborationDTO collaborationDTO);

        /// <summary>
        /// GetCollaborationList
        /// </summary>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageNumber.</param>
        /// <returns>CollaborationDataDTO List.</returns>
        List<CollaborationDataDTO> GetCollaborationList(int pageNumber, int pageSize);

        /// <summary>
        /// GetCollaborationCount.
        /// </summary>
        /// <returns>Collaboration Count</returns>
        int GetCollaborationCount();

        /// <summary>
        /// GetCollaborationDetails.
        /// </summary>
        /// <param name="peopleIndex">peopleIndex.</param>
        /// <returns>CollaborationInfoDTO.</returns>
        CollaborationInfoDTO GetCollaborationDetails(Guid collaborationIndex, long agencyID);

        /// <summary>
        /// GetCollaborationQuestionnaireList.
        /// </summary>
        /// <param name="collaborationID">collaborationID.</param>
        /// <returns>Questionnaire List.</returns>
        List<QuestionnaireDataDTO> GetCollaborationQuestionnaireList(int collaborationID);

        /// <summary>
        /// GetCollaborationLeads.
        /// </summary>
        /// <param name="collaborationID">collaborationID.</param>
        /// <returns>CollaborationLead List.</returns>
        List<CollaborationLeadDTO> GetCollaborationLeads(int collaborationID);

        /// <summary>
        /// GetCollaborationCategories.
        /// </summary>
        /// <param name="collaborationID">collaborationID.</param>
        /// <returns>Category List.</returns>
        List<CategoryDataDTO> GetCollaborationCategories(int collaborationID);
        /// <summary>
        /// To get all Agency collaborations.
        /// </summary>
        /// <returns> CollaborationDTO.</returns>
        List<CollaborationLookupDTO> GetAllAgencycollaborations(long id);
        /// <summary>
        /// GetCollaborationCountByLevel.
        /// </summary>
        /// <param name="levelID">levelID.</param>
        /// <returns>count.</returns>
        int GetCollaborationCountByLevel(Int64 levelID);

        /// <summary>
        /// Get the count of Collaborations having a specific therapy type
        /// </summary>
        /// <param name="therapyTypeID"></param>
        /// <returns>int</returns>
        int GetCollaborationCountByTherapy(int therapyTypeID);

        /// <summary>
        /// GetCollaborationListForOrgAdmin.
        /// </summary>
        /// <param name="collaborationSearchDTO">collaborationSearchDTO.</param>
        /// <param name="queryBuilderDTO">queryBuilderDTO.</param>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>Tuple<List<CollaborationDataDTO>,int>.</returns>
        Tuple<List<CollaborationDataDTO>, int> GetCollaborationListForOrgAdmin(CollaborationSearchDTO collaborationSearchDTO, DynamicQueryBuilderDTO queryBuilderDTO, long agencyID);

        /// <summary>
        /// GetCollaborationListForOrgAdminCount.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>CollaborationDataDTO List.</returns>
        int GetCollaborationListForOrgAdminCount(long agencyID);
        Task<CollaborationDTO> GetCollaborationAsync(int id);

        /// <summary>
        /// GetHelperDetailsByHelperEmail.
        /// </summary>
        /// <param name="nameCSV">nameCSV.</param>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>CollaborationInfoDTO List.</returns>
        List<CollaborationInfoDTO> GetCollaborationDetailsByName(string nameCSV, long agencyID);

        /// <summary>
        /// GetPCollaborationDetailsListForExternal
        /// </summary>
        /// <param name="loggedInUserDTO"></param>
        /// <param name="queryBuilderDTO"></param>
        /// <param name="collaborationSearchInputDTO"></param>
        /// <returns>CollaborationDetailsListDTO</returns>
        List<CollaborationDetailsListDTO> GetPCollaborationDetailsListForExternal(LoggedInUserDTO loggedInUserDTO, DynamicQueryBuilderDTO queryBuilderDTO, CollaborationSearchInputDTO collaborationSearchInputDTO);
    }
}