// -----------------------------------------------------------------------
// <copyright file="ICollaborationSharingRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Opeeka.PICS.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    public interface ICollaborationSharingRepository
    {
        /// <summary>
        /// AddCollaborationSharing
        /// </summary>
        /// <param name="collaborationSharingDTO"></param>
        /// <returns>CollaborationSharingDTO</returns>
        CollaborationSharingDTO AddCollaborationSharing(CollaborationSharingDTO collaborationSharingDTO);

        /// <summary>
        /// To get details CollaborationSharing.
        /// </summary>
        /// <param CollaborationSharingDTO>id.</param>
        /// <returns>.AgencyPersonCollaborationDTO</returns>
        Task<CollaborationSharingDTO> GetCollaborationSharing(Guid id);

        /// <summary>
        /// To update CollaborationSharing details.
        /// </summary>
        /// <param name="collaborationSharingDTO">id.</param>
        /// <returns>List of summaries.</returns>
        CollaborationSharingDTO UpdateCollaborationSharing(CollaborationSharingDTO collaborationSharingDTO);

        /// <summary>
        /// GetCollaborationQuestionairesList.
        /// </summary>
        /// <param name="collaborationID">collaborationID.</param>
        /// <returns>QuestionnaireDTO list.</returns>
        List<QuestionnaireDTO> GetCollaborationQuestionairesList(int collaborationID);
    }
}
