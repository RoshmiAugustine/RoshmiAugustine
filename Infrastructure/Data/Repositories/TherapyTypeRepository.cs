// -----------------------------------------------------------------------
// <copyright file="TherapyTypeRepository.cs" company="Naicoits">
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
using Opeeka.PICS.Infrastructure.Enums;
using Opeeka.PICS.Domain.Interfaces;

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{
    /// <summary>
    /// TherapyTypeRepository.
    /// </summary>
    public class TherapyTypeRepository : BaseRepository<TherapyType>, ITherapyTypeRepository
    {
        private readonly IMapper mapper;
        private readonly OpeekaDBContext _dbContext;
        private readonly ICache _cache;

        public TherapyTypeRepository(OpeekaDBContext dbContext, IMapper mapper, ICache cache)
            : base(dbContext)
        {
            this.mapper = mapper;
            this._dbContext = dbContext;
            this._cache = cache;
        }

        /// <summary>
        /// To get all Therapy Types.
        /// </summary>
        /// <returns> TherapyType.</returns>
        public async Task<List<TherapyTypeDTO>> GetAllTherapyType()
        {
            try
            {
                var therapyType = await this.GetAsync(x => !x.IsRemoved);
                return this.mapper.Map<List<TherapyTypeDTO>>(therapyType.OrderBy(x => x.ListOrder));
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get the Therapy Types list
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>TherapyTypeDTO</returns>
        public List<TherapyTypeDTO> GetTherapyTypeList(int pageNumber, int pageSize, long agencyID)
        {
            try
            {
                List<TherapyTypeDTO> therapyTypeDTO = new List<TherapyTypeDTO>();
                var therapyType = this._dbContext.TherapyType.Where(x => x.AgencyID == agencyID && !x.IsRemoved).OrderBy(y => y.ListOrder).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                this.mapper.Map<List<TherapyType>, List<TherapyTypeDTO>>(therapyType, therapyTypeDTO);
                return therapyTypeDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get the Therapy Types list agencywise
        /// </summary>
        /// <param name="agencyId"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns>TherapyTypeDTO</returns>
        public List<TherapyTypeDTO> GetTherapyTypeList(int agencyId, int pageNumber, int pageSize)
        {
            try
            {
                List<TherapyTypeDTO> therapyTypeDTO = new List<TherapyTypeDTO>();
                var therapyType = this._dbContext.TherapyType.Where(x => x.AgencyID == agencyId && !x.IsRemoved).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                this.mapper.Map<List<TherapyType>, List<TherapyTypeDTO>>(therapyType, therapyTypeDTO);
                return therapyTypeDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get the total count of Therapy Types
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns></returns>
        public int GetTherapyTypeCount(long agencyId)
        {
            try
            {
                var count = this._dbContext.TherapyType.Where(x => x.AgencyID == agencyId && !x.IsRemoved).Count();
                return count;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Add new Therapy Type
        /// </summary>
        /// <param name="therapyTypeDetailsDTO"></param>
        /// <returns>TherapyTypeDetailsDTO</returns>
        public TherapyTypeDTO AddTherapyType(TherapyTypeDTO therapyTypeDetailsDTO)
        {
            try
            {
                TherapyType therapyType = new TherapyType();
                this.mapper.Map<TherapyTypeDTO, TherapyType>(therapyTypeDetailsDTO, therapyType);
                var result = this.AddAsync(therapyType).Result;
                this.mapper.Map<TherapyType, TherapyTypeDTO>(result, therapyTypeDetailsDTO);
                this._cache.DeleteAll(new List<string>() { PCISEnum.Caching.GetAllTherapyTypes, PCISEnum.Caching.GetAgencyTherapyTypeList });
                return therapyTypeDetailsDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Update existing Therapy Type
        /// </summary>
        /// <param name="therapyTypeDetailsDTO"></param>
        /// <returns>TherapyTypeDetailsDTO</returns>
        public TherapyTypeDTO UpdateTherapyType(TherapyTypeDTO therapyTypeDetailsDTO)
        {
            try
            {
                TherapyType therapyType = new TherapyType();
                this.mapper.Map<TherapyTypeDTO, TherapyType>(therapyTypeDetailsDTO, therapyType);
                var result = this.UpdateAsync(therapyType).Result;
                this.mapper.Map<TherapyType, TherapyTypeDTO>(result, therapyTypeDetailsDTO);
                this._cache.DeleteAll(new List<string>() { PCISEnum.Caching.GetAllTherapyTypes, PCISEnum.Caching.GetAgencyTherapyTypeList });
                return therapyTypeDetailsDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get one Therapy Type by Id
        /// </summary>
        /// <param name="therapyTypeID"></param>
        /// <returns>TherapyTypeDTO</returns>
        public async Task<TherapyTypeDTO> GetTherapyType(int therapyTypeID)
        {
            try
            {
                TherapyTypeDTO therapyTypeDTO = new TherapyTypeDTO();
                TherapyType therapyType = await this.GetRowAsync(x => x.TherapyTypeID == therapyTypeID);
                this.mapper.Map<TherapyType, TherapyTypeDTO>(therapyType, therapyTypeDTO);
                return therapyTypeDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
