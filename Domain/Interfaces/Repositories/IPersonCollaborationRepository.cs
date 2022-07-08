// -----------------------------------------------------------------------
// <copyright file="IPersonCollaborationRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    /// <summary>
    /// IAgencyRepository.
    /// </summary>
    public interface IPersonCollaborationRepository : IAsyncRepository<PersonCollaboration>
    {
        /// <summary>
        /// To add address details.
        /// </summary>
        /// <param name="personCollaborationDTO">id.</param>
        /// <returns>Guid.</returns>
        long AddPersonCollaboration(PersonCollaborationDTO personCollaborationDTO);

        /// <summary>
        /// To update address details.
        /// </summary>
        /// 
        /// 
        /// <param name="personCollaborationDTO">id.</param>
        /// <returns>List of summaries.</returns>
        PersonCollaborationDTO UpdatePersonCollaboration(PersonCollaborationDTO personCollaborationDTO);

        /// <summary>
        /// GetPersonCollaboration.
        /// </summary>
        /// <param name="personId"></param>
        /// <returns>PeopleCollaborationDTO.</returns>
        List<PeopleCollaborationDTO> GetPersonCollaboration(long personId);


        Task<IReadOnlyList<PersonCollaborationDTO>> GetPersonCollaborationByCollaborationID(long collaborationID);
        void AddBulkPersonCollaboration(List<PersonCollaborationDTO> personCollaborationDTO);
        /// <summary>
        /// GetPersonCollaborationByPersonIdAndCollaborationId.
        /// </summary>
        /// <param name="personId">personId.</param>
        /// <param name="collaborationId">collaborationId.</param>
        /// <returns>PersonCollaboration.</returns>
        PersonCollaboration GetPersonCollaborationByPersonIdAndCollaborationId(long personId, int? collaborationId);

        List<PersonCollaboration> GetPersonCollaborationFromId(List<long> personCollaborationId);
        List<PersonCollaboration> UpdateBulkPersonCollaboration(List<PersonCollaboration> personCollaboration);
        List<PersonCollaboration> GetPersonCollaborationByDataId(long personId);
        /// <summary>
        /// GetPersonCollaborationByPersonCollaborationId
        /// </summary>
        /// <param name="personCollaborationId"></param>
        /// <returns></returns>
        PersonCollaboration GetPersonCollaborationByPersonCollaborationId(long personCollaborationId);
    }
}
