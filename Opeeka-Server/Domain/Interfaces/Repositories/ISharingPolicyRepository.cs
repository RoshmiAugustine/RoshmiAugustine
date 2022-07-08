// -----------------------------------------------------------------------
// <copyright file="ISharingPolicyRepository.cs" company="Naicoits">
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
    /// ISharingPolicyRepository.
    /// </summary>
    public interface ISharingPolicyRepository : IAsyncRepository<SharingPolicy>
    {

        /// <summary>
        /// To get all languages.
        /// </summary>
        /// <returns> LanguageDTO.</returns>
        Task<List<SharingPolicyDTO>> GetAllSharingPolicy();
    }
}
