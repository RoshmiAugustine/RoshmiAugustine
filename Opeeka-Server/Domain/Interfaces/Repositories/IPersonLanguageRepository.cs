// -----------------------------------------------------------------------
// <copyright file="IPersonLanguageRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Opeeka.PICS.Domain.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    /// <summary>
    /// IAgencyRepository.
    /// </summary>
    public interface IPersonLanguageRepository
    {
        /// <summary>
        /// To add address details.
        /// </summary>
        /// <param name="addressDTO">id.</param>
        /// <returns>Guid.</returns>
        long AddPersonLanguage(PersonLanguageDTO personLanguageDTO);

        /// <summary>
        /// To update address details.
        /// </summary>
        /// <param name="addressDTO">id.</param>
        /// <returns>List of summaries.</returns>
        PersonLanguageDTO UpdatePersonLanguage(PersonLanguageDTO personLanguageDTO);

        /// <summary>
        /// To get address details.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns>address details..</returns>
        Task<IReadOnlyList<PersonLanguageDTO>> GetPersonLanguage(long id);
    }
}
