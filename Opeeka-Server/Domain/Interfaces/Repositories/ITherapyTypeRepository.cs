// -----------------------------------------------------------------------
// <copyright file="ITherapyTypeRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Opeeka.PICS.Domain.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    /// <summary>
    /// ITherapyTypeRepository.
    /// </summary>
    public interface ITherapyTypeRepository
    {
        /// <summary>
        /// Get all Collaboration Tag Types
        /// </summary>
        /// <returns>TherapyTypeDTO</returns>
        Task<List<TherapyTypeDTO>> GetAllTherapyType();

        /// <summary>
        /// Get Collaboration Tag Type list paginated
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>TherapyTypeDTO</returns>
        List<TherapyTypeDTO> GetTherapyTypeList(int pageNumber, int pageSize, long agencyID);

        /// <summary>
        /// Get Collaboration Tag Type list of an agency paginated
        /// </summary>
        /// <param name="agencyId"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns>TherapyTypeDTO</returns>
        List<TherapyTypeDTO> GetTherapyTypeList(int agencyId, int pageNumber, int pageSize);

        /// <summary>
        /// Get the total count of Collaboration Tag Types
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>int</returns>
        int GetTherapyTypeCount(long agencyID);

        /// <summary>
        /// Add a new Collaboration Tag Type
        /// </summary>
        /// <param name="therapyTypeDetailsDTO"></param>
        /// <returns>TherapyTypeDTO</returns>
        TherapyTypeDTO AddTherapyType(TherapyTypeDTO therapyTypeDetailsDTO);

        /// <summary>
        /// Update an existing Collaboration Tag Type
        /// </summary>
        /// <param name="therapyTypeDetailsDTO"></param>
        /// <returns>therapyTypeDTO</returns>
        TherapyTypeDTO UpdateTherapyType(TherapyTypeDTO therapyTypeDetailsDTO);

        /// <summary>
        /// Get one Collaboration Tag Type by Id
        /// </summary>
        /// <param name="therapyTypeID"></param>
        /// <returns>Task<TherapyTypeDTO></returns>
        Task<TherapyTypeDTO> GetTherapyType(int therapyTypeID);
    }
}
