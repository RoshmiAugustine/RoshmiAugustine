// -----------------------------------------------------------------------
// <copyright file="ICollaborationService.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.DTO.ExternalAPI;
using Opeeka.PICS.Domain.DTO.Input;
using Opeeka.PICS.Domain.DTO.Output;
using System;

namespace Opeeka.PICS.Domain.Interfaces.Services
{
    /// <summary>
    /// ICollaborationService.
    /// </summary>
    public interface ICollaborationService
    {
        /// <summary>
        /// Add Collaboration Details.
        /// </summary>
        /// <param name="AddCollaborationDetailsDTO">id.</param>
        /// <returns>AddCollaborationDetailsDTO.</returns>
        CollaborationResponseDTO AddCollaborationDetails(CollaborationDetailsDTO collaborationDetailsDTO);

        /// <summary>
        /// Update Collaboration Details.
        /// </summary>
        /// <param name="collaborationDetailsDTO">id.</param>
        /// <returns>AddCollaborationDetailsDTO.</returns>
        CollaborationResponseDTO UpdateCollaborationDetails(CollaborationDetailsDTO collaborationDetailsDTO, long updateUserID, string callingType = "");

        /// <summary>
        /// Get CollaborationList.
        /// </summary>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>Collaboration List</returns>
        GetCollaborationListDTO GetCollaborationList(CollaborationSearchDTO collaborationSearchDTO, long agencyID);

        /// <summary>
        /// GetCollaborationDetails
        /// </summary>
        /// <param name="collaborationIndex"></param>
        /// <returns>CollaborationDetailsResponseDTO.</returns>
        CollaborationDetailsResponseDTO GetCollaborationDetails(Guid collaborationIndex,long agencyID);

        /// <summary>
        /// GetCollaborationDetailsByName.
        /// </summary>
        /// <param name="nameCSV">nameCSV.</param>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>CollaborationDetailsResponseDTO.</returns>
        CollaborationDetailsResponseDTO GetCollaborationDetailsByName(string nameCSV, long agencyID);

        /// <summary>
        /// GetCollaborationDetailsListForExternal
        /// </summary>
        /// <param name="collaborationSearchInputDTO"></param>
        /// <param name="loggedInUserDTO"></param>
        /// <returns>CollaborationResponseDTOForExternal</returns>
        CollaborationResponseDTOForExternal GetCollaborationDetailsListForExternal(CollaborationSearchInputDTO collaborationSearchInputDTO, LoggedInUserDTO loggedInUserDTO);

        /// <summary>
        /// add collabration list from external
        /// </summary>
        /// <param name="collaborationDetailsDTO"></param>
        /// <returns></returns>
        AddCollaborationResponseDTOForExternal AddCollabrationForExternal(CollabrationAddDTOForExternal collaborationDetailsDTO,long agencyID,int updateUserID);

        /// <summary>
        /// Function to update collabration module for external
        /// </summary>
        /// <param name="collaborationDetailsDTOForExternal"></param>
        /// <returns></returns>
        CRUDCollaborationResponseDTOForExternal UpdateCollabrationForExternal(CollabrationUpdateDTOForExternal collaborationDetailsDTOForExternal,long updateUserID,long agencyID);

        /// <summary>
        /// Function to delete collabratiom details for external
        /// </summary>
        /// <param name="collabrationIndex"></param>
        /// <param name="updateUserID"></param>
        /// <param name="loggedInAgencyID"></param>
        /// <returns></returns>
        CRUDCollaborationResponseDTOForExternal DeleteCollaborationDetail(Guid collabrationIndex, int updateUserID, long loggedInAgencyID);
    }
}
