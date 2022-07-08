// -----------------------------------------------------------------------
// <copyright file="IVoiceTypeRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using System.Collections.Generic;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    public interface IVoiceTypeRepository
    {
        /// <summary>
        /// GetAllVoiceType
        /// </summary>
        /// <returns>VoiceType</returns>
        List<VoiceType> GetAllVoiceType();

        /// <summary>
        /// GetAll VoiceTypes InDetail For Reports
        /// </summary>
        /// <param name="personIndex">personIndex</param>
        /// <param name="personQuestionnaireID">personQuestionnaireID</param>
        /// <param name="collaborationID">collaborationID</param>
        /// <returns>VoiceTypeDTO</returns>
       List<VoiceTypeDTO> GetAllVoiceTypeInDetail(long personIndex, long personQuestionnaireID, long personCollaborationID, SharedDetailsDTO sharedIDs, SharedDetailsDTO helperColbDetails);

        /// <summary>
        /// GetActiveVoiceTypeInDetail Based on Date
        /// </summary>
        /// <param name="personIndex"></param>
        /// <returns></returns>
        List<VoiceTypeDTO> GetActiveVoiceTypeInDetail(long personIndex);


        /// <summary>
        /// GetVoiceType
        /// </summary>
        /// <returns>VoiceType</returns>        
        public VoiceType GetVoiceType(int id);


        /// <summary>
        /// GetVoiceTypeForFilter.
        /// </summary>
        /// <param name="personID">personID.</param>
        /// <param name="personQuestionaireID">personQuestionaireID.</param>
        /// <returns>VoiceTypeDTOList.</returns>
        List<VoiceTypeDTO> GetVoiceTypeForFilter(long personID, long personQuestionnaireID, string sharedAssessmentIDS);

    }
}
