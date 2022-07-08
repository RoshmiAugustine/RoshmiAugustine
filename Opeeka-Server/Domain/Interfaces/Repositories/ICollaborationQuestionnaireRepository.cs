// -----------------------------------------------------------------------
// <copyright file="ICollaborationQuestionnaire.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    /// <summary>
    /// ICollaborationQuestionnaire.
    /// </summary>
    public interface ICollaborationQuestionnaireRepository : IAsyncRepository<CollaborationQuestionnaire>
    {
        /// <summary>
        /// To add collaboration details.
        /// </summary>
        /// <param name="collaborationDetailsDTO">collaborationDetailsDTO.</param>
        /// <returns>id.</returns>
        Int64 AddCollaborationQuestionnaire(CollaborationQuestionnaireDTO collaborationQuestionnaireDTO);

        /// <summary>
        /// To update collaboration questionnaire details.
        /// </summary>
        /// <param name="CollaborationQuestionnaireDTO">CollaborationQuestionnaireDTO.</param>
        /// <returns>CollaborationQuestionnaireDTO.</returns>
        CollaborationQuestionnaireDTO UpdateCollaborationQuestionnaire(CollaborationQuestionnaireDTO collaborationQuestionnaireDTO);

        /// <summary>
        /// GetCollaborationQuestionnaireData.
        /// </summary>
        /// <param name="collaborationID">collaborationID</param>
        /// <returns>CollaborationQuestionnaireDTO List.</returns>
        List<CollaborationQuestionnaireDTO> GetCollaborationQuestionnaireData(int collaborationID);

        CollaborationQuestionnaireDTO GetCollaborationQuestionnaireByCollaborationAndQuestionnaire(long collaborationID, long questionnaireID);
        List<PeopleCollaborationDTO> GetAllPersonCollaborationForReminders(List<long> list_personCollaborationIds);
        /// <summary>
        /// GetCollaborationQuestionnaireData.
        /// </summary>
        /// <param name="collaborationIDs"></param>
        /// <returns></returns>
        List<CollaborationQuestionnaireDTO> GetCollaborationQuestionnaireData(List<int> collaborationIDs);
    }
}
