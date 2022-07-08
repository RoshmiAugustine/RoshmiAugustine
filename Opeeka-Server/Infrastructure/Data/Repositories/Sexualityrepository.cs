// -----------------------------------------------------------------------
// <copyright file="Sexualityrepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Infrastructure.Data.Repositories.Base;

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{
    public class Sexualityrepository : BaseRepository<Sexuality>, ISexualityRepository
    {
        private readonly IMapper mapper;
        private readonly OpeekaDBContext _dbContext;

        public Sexualityrepository(OpeekaDBContext _dbContext, OpeekaDBContext dbContext, IMapper mapper)
            : base(dbContext)
        {
            this.mapper = mapper;
            this._dbContext = _dbContext;
        }

        /// <summary>
        /// To get all types of sexuality.
        /// </summary>
        /// <returns> SexualityDTO.</returns>
        public async Task<List<SexualityDTO>> GetAllSexuality()
        {
            try
            {
                var sexuality = await this.GetAsync(x => !x.IsRemoved);
                return this.mapper.Map<List<SexualityDTO>>(sexuality.OrderBy(x => x.ListOrder));
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// To Update Sexuality
        /// </summary>
        /// <param name="Sexuality">id.</param>
        /// <returns>List of summaries.</returns>
        public Sexuality UpdateSexuality(Sexuality sexuality)
        {
            try
            {
                Sexuality result = this.UpdateAsync(sexuality).Result;
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// To get details agencyaddress.
        /// </summary>
        /// <param agencyAddressDTO="agencyAddressDTO">id.</param>
        /// <returns>.AgencyAddressDTO</returns>
        public async Task<Sexuality> GetSexuality(long id)
        {
            try
            {
                Sexuality sexuality = await this.GetRowAsync(x => x.SexualityID == id);
                return sexuality;
            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// Get the count of helpers having a specific title
        /// </summary>
        /// <param name="helperTitleID"></param>
        /// <returns>int</returns>
        public int GetSexualityCount(int sexualityID)
        {
            int count = (
                            from row
                            in this._dbContext.Person
                            where
                                row.SexualityID == sexualityID && !row.IsRemoved
                            select
                                row
                        ).Count();

            return count;
        }

        /// <summary>
        /// AddSexuality
        /// </summary>
        /// <param name="sexuality"></param>
        /// <returns>Sexuality</returns>
        public Sexuality AddSexuality(Sexuality sexuality)
        {
            try
            {
                var result = this.AddAsync(sexuality).Result;
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetSexualityList
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>Sexuality</returns>
        public List<Sexuality> GetSexualityList(int pageNumber, int pageSize, long agencyID)
        {
            try
            {
                var sexualityList = this._dbContext.Sexualities.Where(x => x.AgencyID == agencyID && !x.IsRemoved).OrderBy(y => y.ListOrder).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                return sexualityList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// GetSexualityCount
        /// </summary>
        /// <returns>int</returns>
        public int GetAgencySexualityCount(long agencyID)
        {
            try
            {
                var count = this._dbContext.Sexualities.Where(x => !x.IsRemoved && x.AgencyID == agencyID).Count();
                return count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// GetAgencySexuality
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>SexualityDTO</returns>
        public List<SexualityDTO> GetAgencySexuality(long agencyID)
        {
            try
            {
                List<SexualityDTO> sexualityDTO = new List<SexualityDTO>();
                var sexuality = this._dbContext.Sexualities.Where(x => x.AgencyID == agencyID && !x.IsRemoved).OrderBy(y => y.ListOrder).ToList();
                this.mapper.Map<List<Sexuality>, List<SexualityDTO>>(sexuality, sexualityDTO);
                return sexualityDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

}
