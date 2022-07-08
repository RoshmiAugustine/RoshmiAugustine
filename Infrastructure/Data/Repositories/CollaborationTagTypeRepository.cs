// -----------------------------------------------------------------------
// <copyright file="CollaborationTagTypeRepository.cs" company="Naicoits">
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
    /// CollaborationTagTypeRepository.
    /// </summary>
    public class CollaborationTagTypeRepository : BaseRepository<CollaborationTagType>, ICollaborationTagTypeRepository
    {
        private readonly IMapper mapper;
        private readonly OpeekaDBContext _dbContext;
        private readonly ICache _cache;

        public CollaborationTagTypeRepository(OpeekaDBContext dbContext, IMapper mapper, ICache cache)
            : base(dbContext)
        {
            this.mapper = mapper;
            this._dbContext = dbContext;
            this._cache = cache;
        }

        /// <summary>
        /// To get all Collaboration Tag Types.
        /// </summary>
        /// <returns> CollaborationTagType.</returns>
        public async Task<List<CollaborationTagTypeDTO>> GetAllCollaborationTagType()
        {
            try
            {
                var collaborationTagType = await this.GetAsync(x => !x.IsRemoved);
                return this.mapper.Map<List<CollaborationTagTypeDTO>>(collaborationTagType.OrderBy(x => x.ListOrder));
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get the Collaboration Tag Types list
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>CollaborationTagTypeDTO</returns>
        public List<CollaborationTagTypeDTO> GetCollaborationTagTypeList(int pageNumber, int pageSize, long agencyID)
        {
            try
            {
                List<CollaborationTagTypeDTO> collaborationTagTypeDTO = new List<CollaborationTagTypeDTO>();
                var collaborationTagType = this._dbContext.CollaborationTagType.Where(x => x.AgencyID == agencyID && !x.IsRemoved).OrderBy(y => y.ListOrder).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                this.mapper.Map<List<CollaborationTagType>, List<CollaborationTagTypeDTO>>(collaborationTagType, collaborationTagTypeDTO);
                return collaborationTagTypeDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get the total count of Collaboration Tag Types
        /// </summary>
        /// <returns></returns>
        public int GetCollaborationTagTypeCount(long agencyID)
        {
            try
            {
                var count = this._dbContext.CollaborationTagType.Where(x => !x.IsRemoved && x.AgencyID == agencyID).Count();
                return count;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Add new Collaboration Tag Type
        /// </summary>
        /// <param name="collaborationTagTypeDetailsDTO"></param>
        /// <returns>CollaborationTagTypeDetailsDTO</returns>
        public CollaborationTagTypeDTO AddCollaborationTagType(CollaborationTagTypeDTO collaborationTagTypeDetailsDTO)
        {
            try
            {
                CollaborationTagType collaborationTagType = new CollaborationTagType();
                this.mapper.Map<CollaborationTagTypeDTO, CollaborationTagType>(collaborationTagTypeDetailsDTO, collaborationTagType);
                var result = this.AddAsync(collaborationTagType).Result;
                this.mapper.Map<CollaborationTagType, CollaborationTagTypeDTO>(result, collaborationTagTypeDetailsDTO);
                this._cache.Delete(PCISEnum.Caching.GetAgencyTagTypeList);
                return collaborationTagTypeDetailsDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Update existing Collaboration Tag Type
        /// </summary>
        /// <param name="collaborationTagTypeDetailsDTO"></param>
        /// <returns>CollaborationTagTypeDetailsDTO</returns>
        public CollaborationTagTypeDTO UpdateCollaborationTagType(CollaborationTagTypeDTO collaborationTagTypeDetailsDTO)
        {
            try
            {
                CollaborationTagType collaborationTagType = new CollaborationTagType();
                this.mapper.Map<CollaborationTagTypeDTO, CollaborationTagType>(collaborationTagTypeDetailsDTO, collaborationTagType);
                var result = this.UpdateAsync(collaborationTagType).Result;
                this.mapper.Map<CollaborationTagType, CollaborationTagTypeDTO>(result, collaborationTagTypeDetailsDTO);
                this._cache.Delete(PCISEnum.Caching.GetAgencyTagTypeList);
                return collaborationTagTypeDetailsDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get one Collaboration Tag Type by Id
        /// </summary>
        /// <param name="collaborationTagTypeID"></param>
        /// <returns>CollaborationTagTypeDTO</returns>
        public async Task<CollaborationTagTypeDTO> GetCollaborationTagType(int collaborationTagTypeID)
        {
            try
            {
                CollaborationTagTypeDTO collaborationTagTypeDTO = new CollaborationTagTypeDTO();
                CollaborationTagType collaborationTagType = await this.GetRowAsync(x => x.CollaborationTagTypeID == collaborationTagTypeID);
                this.mapper.Map<CollaborationTagType, CollaborationTagTypeDTO>(collaborationTagType, collaborationTagTypeDTO);
                return collaborationTagTypeDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
