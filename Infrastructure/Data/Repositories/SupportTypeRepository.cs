// -----------------------------------------------------------------------
// <copyright file="SupportTypeRepository.cs" company="Naicoits">
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
using Opeeka.PICS.Domain.Interfaces;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Infrastructure.Data.Repositories.Base;
using Opeeka.PICS.Infrastructure.Enums;

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{
    public class SupportTypeRepository : BaseRepository<SupportType>, ISupportTypeRepository
    {
        private readonly OpeekaDBContext _dbContext;
        private readonly IMapper mapper;
        private readonly ICache _cache;

        public SupportTypeRepository(OpeekaDBContext dbContext, IMapper mapper, ICache cache)
            : base(dbContext)
        {
            this._dbContext = dbContext;
            this.mapper = mapper;
            this._cache = cache;
        }

        /// <summary>
        /// To get all SupportTypes.
        /// </summary>
        /// <returns> SupportType.</returns>
        public async Task<List<SupportTypeDTO>> GetAllSupportTypes()
        {
            try
            {
                var SupportTypes = await this.GetAsync(x => !x.IsRemoved);
                return this.mapper.Map<List<SupportTypeDTO>>(SupportTypes.OrderBy(x => x.ListOrder));
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// AddSupportType.
        /// </summary>
        /// <param name="SupportType">SupportType.</param>
        /// <returns>SupportType.</returns>
        public SupportType AddSupportType(SupportType SupportType)
        {
            try
            {
                var result = this.AddAsync(SupportType).Result;
                this._cache.Delete(PCISEnum.Caching.GetAllSupportType);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// GetSupportType.
        /// </summary>
        /// <param name="supportTypeID">supportTypeID.</param>
        /// <returns> Task<SupportType>.</returns>
        public async Task<SupportType> GetSupportType(Int64 supportTypeID)
        {
            try
            {
                var readFromCache = this._cache.Get<List<SupportType>>(PCISEnum.Caching.GetAllSupportType);
                if (readFromCache == null || readFromCache?.Count == 0)
                {
                    SupportType supportType = await this.GetRowAsync(x => x.SupportTypeID == supportTypeID);
                    return supportType;
                }
                return readFromCache.Where(x => x.SupportTypeID == supportTypeID).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// GetSupportTypeCount.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>int.</returns>
        public int GetSupportTypeCount(long agencyID)
        {
            try
            {
                var readFromCache = this._cache.Get<List<SupportType>>(PCISEnum.Caching.GetAllSupportType);
                if (readFromCache == null || readFromCache?.Count == 0)
                {
                    var count = this._dbContext.SupportTypes.Where(x => x.AgencyID == agencyID && !x.IsRemoved).Count();
                    return count;
                }
                return readFromCache.Where(x => x.AgencyID == agencyID && !x.IsRemoved).ToList().Count();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// GetSupportTypeList.
        /// </summary>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>List<SupportType>.</returns>
        public List<SupportType> GetSupportTypeList(int pageNumber, int pageSize, long agencyID)
        {
            try
            {
                var readFromCache = this._cache.Get<List<SupportType>>(PCISEnum.Caching.GetAllSupportType);
                if (readFromCache == null || readFromCache?.Count == 0)
                {
                    var supportTypeList = this._dbContext.SupportTypes.Where(x => x.AgencyID == agencyID && !x.IsRemoved).OrderBy(y => y.ListOrder).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                    return supportTypeList;
                }
                return readFromCache.Where(x => x.AgencyID == agencyID && !x.IsRemoved).OrderBy(y => y.ListOrder).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// UpdateSupportType.
        /// </summary>
        /// <param name="SupportType">SupportType</param>
        /// <returns>SupportType</returns>
        public SupportType UpdateSupportType(SupportType SupportType)
        {
            try
            {
                var result = this.UpdateAsync(SupportType).Result;
                this._cache.Delete(PCISEnum.Caching.GetAllSupportType);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetSupportTypeUsedByID.
        /// </summary>
        /// <param name="supportTypeID">levelID.</param>
        /// <returns>int.</returns>
        public int GetSupportTypeUsedByID(long supportTypeID)
        {
            int count = (from row in this._dbContext.PersonSupport
                         where (row.SupportTypeID == supportTypeID) && !row.IsRemoved
                         select row).Count();

            return count;
        }

        /// <summary>
        /// GetAgencySupportTypes.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>SupportTypeDTO</returns>
        public List<SupportTypeDTO> GetAgencySupportTypes(long agencyID)
        {
            try
            {
                List<SupportTypeDTO> supportTypeDTO = new List<SupportTypeDTO>();
                var readFromCache = this._cache.Get<List<SupportType>>(PCISEnum.Caching.GetAllSupportType);
                if (readFromCache == null || readFromCache?.Count == 0)
                {
                    var allSupportType = this._dbContext.SupportTypes.Where(x => !x.IsRemoved).ToList();
                    this._cache.Post(PCISEnum.Caching.GetAllSupportType, readFromCache = allSupportType);
                }
                readFromCache = readFromCache?.Where(x => x.AgencyID == agencyID).OrderBy(y => y.ListOrder).ToList();
                this.mapper.Map<List<SupportType>, List<SupportTypeDTO>>(readFromCache, supportTypeDTO);
                return supportTypeDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// GetAgencySupportTypesList
        /// </summary>
        /// <param name="agencyID"></param>
        /// <returns></returns>
        public List<SupportTypeDTO> GetAgencySupportTypeList(long agencyID)
        {
            try
            {
                var query = string.Empty;
                query = @$"Select AgencyID, SupportTypeID,ListOrder from info.SupportType where IsRemoved = 0 and AgencyID ={agencyID}";

                var data = ExecuteSqlQuery(query, x => new SupportTypeDTO
                {
                    AgencyID = (long)x[0],
                    SupportTypeID = x["SupportTypeID"] == DBNull.Value ? 0 : (int)x["SupportTypeID"]
                }).OrderBy(y => y.ListOrder).ToList();
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

}
