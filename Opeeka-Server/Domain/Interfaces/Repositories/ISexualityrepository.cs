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
    /// ISexualityrepository.
    /// </summary>
    public interface ISexualityRepository
    {

        /// <summary>
        /// To get all types of sexuality.
        /// </summary>
        /// <returns> SexualityDTO.</returns>
        Task<List<SexualityDTO>> GetAllSexuality();

        /// <summary>
        /// To UpdateSexuality.
        /// </summary>
        /// <param name="Sexuality">id.</param>
        /// <returns> Sexuality.</returns>
        Sexuality UpdateSexuality(Sexuality sexuality);

        /// <summary>
        /// To  GetSexuality.
        /// </summary>
        /// <param Sexuality="Sexuality">id.</param>
        /// <returns>.Sexuality</returns>
        Task<Sexuality> GetSexuality(long id);


        /// <summary>
        /// Get the count of helpers having a specific title
        /// </summary>
        /// <param name="helperTitleID"></param>
        /// <returns>int</returns>
        public int GetSexualityCount(int SexualityID);

        /// <summary>
        /// AddSexuality
        /// </summary>
        /// <param name="sexuality"></param>
        /// <returns>Sexuality</returns>
        Sexuality AddSexuality(Sexuality sexuality);

        /// <summary>
        /// GetSexualityList
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns>Sexuality</returns>
        List<Sexuality> GetSexualityList(int pageNumber, int pageSize, long agencyID);

        /// <summary>
        /// GetSexualityCount
        /// </summary>
        /// <returns></returns>
        int GetAgencySexualityCount(long agencyID);

        /// <summary>
        /// GetAgencySexuality
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>SexualityDTO</returns>
        List<SexualityDTO> GetAgencySexuality(long agencyID);
    }
}
