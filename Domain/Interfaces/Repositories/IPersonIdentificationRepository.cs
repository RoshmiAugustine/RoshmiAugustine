// -----------------------------------------------------------------------
// <copyright file="IPersonIdentificationRepository.cs" company="Naicoits">
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
    public interface IPersonIdentificationRepository : IAsyncRepository<PersonIdentification>
    {
        /// <summary>
        /// To add address details.
        /// </summary>
        /// <param name="personIdentificationDTO">id.</param>
        /// <returns>Guid.</returns>
        long AddPersonIdentificationType(PersonIdentificationDTO personIdentificationDTO);

        /// <summary>
        /// To update address details.
        /// </summary>
        /// <param name="personIdentificationDTO">id.</param>
        /// <returns>List of summaries.</returns>
        PersonIdentificationDTO UpdatePersonIdentificationType(PersonIdentificationDTO personIdentificationDTO);

        /// <summary>
        /// To get address details.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns>address details..</returns>
        Task<IReadOnlyList<PersonIdentificationDTO>> GetPersonIdentificationType(long id);
        void AddBulkPersonIdentificationType(List<PersonIdentificationDTO> personIdentificationDTOList);
        List<PersonIdentification> GetPersonIdentificationsFromId(List<long> personIdentificationId);
        List<PersonIdentification> UpdateBulkPersonIdentifications(List<PersonIdentification> personIdentifications);
        List<PersonIdentification> GetPersonIdentificationTypeByDataId(long id);
    }
}
