// -----------------------------------------------------------------------
// <copyright file="ISexualityrepository.cs" company="Naicoits">
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
    /// ISupportTypeRepository.
    /// </summary>
    public interface ISupportTypeRepository
    {

        /// <summary>
        /// To get all SupportTypes.
        /// </summary>
        /// <returns> SupportType.</returns>
        Task<List<SupportTypeDTO>> GetAllSupportTypes();

        /// <summary>
        /// GetSupportTypeList.
        /// </summary>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>List of SupportType</returns>
        List<SupportType> GetSupportTypeList(int pageNumber, int pageSize, long agencyID);

        /// <summary>
        /// GetSupportTypeCount.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>int.</returns>
        int GetSupportTypeCount(long agencyID);

        /// <summary>
        /// AddSupportType.
        /// </summary>
        /// <param name="SupportType">SupportType.</param>
        /// <returns>SupportType.</returns>
        SupportType AddSupportType(SupportType SupportType);

        /// <summary>
        /// UpdateSupportType.
        /// </summary>
        /// <param name="SupportType">SupportType.</param>
        /// <returns>SupportType.</returns>
        SupportType UpdateSupportType(SupportType SupportType);

        /// <summary>.
        /// GetSupportType
        /// </summary>
        /// <param name="supportTypeID">supportTypeID.</param>
        /// <returns>Task of SupportType.</returns>
        Task<SupportType> GetSupportType(long supportTypeID);

        /// <summary>
        /// GetSupportTypeUsedByID.
        /// </summary>
        /// <param name="supportTypeID">supportTypeID.</param>
        /// <returns>count.</returns>
        int GetSupportTypeUsedByID(long supportTypeID);

        /// <summary>
        /// GetAgencySupportTypes.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>SupportTypeDTO</returns>
        List<SupportTypeDTO> GetAgencySupportTypes(long agencyID);

        List<SupportTypeDTO> GetAgencySupportTypeList(long agencyID);
    }
}
