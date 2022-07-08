// -----------------------------------------------------------------------
// <copyright file="ISexualityrepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    /// <summary>
    /// IIdentificationrepository.
    /// </summary>
    public interface IIdentificationTypeRepository
    {

        /// <summary>
        ///  To get all identificationTypes.
        /// </summary>
        /// <returns> IdentificationTypeDTO.</returns>
        Task<List<IdentificationTypeDTO>> GetAllIdentificationTypes();

        /// <summary>
        /// GetIdentificationTypeList.
        /// </summary>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <returns>List of IdentificationType</returns>
        List<IdentificationType> GetIdentificationTypeList(int pageNumber, int pageSize, long agencyID);

        /// <summary>
        /// GetIdentificationTypeCount.
        /// </summary>
        /// <returns>int.</returns>
        int GetIdentificationTypeCount(long agencyID);

        /// <summary>
        /// AddIdentificationType.
        /// </summary>
        /// <param name="IdentificationType">IdentificationType.</param>
        /// <returns>IdentificationType.</returns>
        IdentificationType AddIdentificationType(IdentificationType IdentificationType);

        /// <summary>
        /// UpdateIdentificationType.
        /// </summary>
        /// <param name="IdentificationType">IdentificationType.</param>
        /// <returns>IdentificationType.</returns>
        IdentificationType UpdateIdentificationType(IdentificationType IdentificationType);

        /// <summary>.
        /// GetIdentificationType
        /// </summary>
        /// <param name="identificationTypeID">identificationTypeID.</param>
        /// <returns>Task of IdentificationType.</returns>
        Task<IdentificationType> GetIdentificationType(Int64 identificationTypeID);

        /// <summary>
        /// GetIdentificationTypeUsedByID.
        /// </summary>
        /// <param name="identificationTypeID">identificationTypeID.</param>
        /// <returns>count.</returns>
        int GetIdentificationTypeUsedByID(Int64 identificationTypeID);

        /// <summary>
        /// GetAgencyIdentificationTypeList
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>IdentificationTypeDTO List</returns>
        List<IdentificationTypeDTO> GetAgencyIdentificationTypeList(long agencyID);

        /// <summary>
        /// GetIdentificationTypeDetailsByName.
        /// </summary>
        /// <param name="nameCSV">nameCSV.</param>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>IdentificationTypeDTO.</returns>
        List<IdentificationTypeDTO> GetIdentificationTypeDetailsByName(string nameCSV, long agencyID);
    }
}
