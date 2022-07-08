// -----------------------------------------------------------------------
// <copyright file="CollaborationLevelRepository.cs" company="Naicoits">
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
    public class CollaborationLevelRepository : BaseRepository<CollaborationLevel>, ICollaborationLevelRepository
    {
        private readonly IMapper mapper;
        private readonly OpeekaDBContext _dbContext;
        private readonly ICache _cache;

        public CollaborationLevelRepository(OpeekaDBContext dbContext, IMapper mapper, ICache cache)
            : base(dbContext)
        {
            this.mapper = mapper;
            this._dbContext = dbContext;
            this._cache = cache;
        }

        /// <summary>
        /// AddCollaborationLevel.
        /// </summary>
        /// <param name="CollaborationLevelDTO">CollaborationLevelDTO.</param>
        /// <returns>CollaborationLevelDTO.</returns>
        public CollaborationLevelDTO AddCollaborationLevel(CollaborationLevelDTO CollaborationLevelDTO)
        {
            try
            {
                CollaborationLevel collaborationLevel = new CollaborationLevel();
                this.mapper.Map<CollaborationLevelDTO, CollaborationLevel>(CollaborationLevelDTO, collaborationLevel);
                var result = this.AddAsync(collaborationLevel).Result;
                this.mapper.Map<CollaborationLevel, CollaborationLevelDTO>(result, CollaborationLevelDTO);
                this._cache.DeleteAll(new List<string>() { PCISEnum.Caching.GetAllLevels, PCISEnum.Caching.GetCollaborationLevelLookup });
                return CollaborationLevelDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetCollaborationLevelCount.
        /// </summary>
        /// <returns>int.</returns>
        public int GetCollaborationLevelCount(long agencyID)
        {
            try
            {
                var count = this._dbContext.CollaborationLevel.Where(x => x.AgencyID == agencyID && !x.IsRemoved).Count();
                return count;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetCollaborationLevel.
        /// </summary>
        /// <param name="collaborationLevelID">collaborationLevelID.</param>
        /// <returns>CollaborationLevelDTO.</returns>
        public async Task<CollaborationLevelDTO> GetCollaborationLevel(Int64 collaborationLevelID)
        {
            try
            {
                CollaborationLevelDTO collaborationLevelDTO = new CollaborationLevelDTO();
                CollaborationLevel collaborationLevel = await this.GetRowAsync(x => x.CollaborationLevelID == collaborationLevelID);
                this.mapper.Map<CollaborationLevel, CollaborationLevelDTO>(collaborationLevel, collaborationLevelDTO);
                return collaborationLevelDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetCollaborationLevelList.
        /// </summary>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>List of CollaborationLevelDTO.</returns>
        public List<CollaborationLevelDTO> GetCollaborationLevelList(int pageNumber, int pageSize, long agencyID)
        {
            try
            {
                List<CollaborationLevelDTO> CollaborationLevelDTO = new List<CollaborationLevelDTO>();
                var CollaborationLevel = this._dbContext.CollaborationLevel.Where(x => x.AgencyID == agencyID && !x.IsRemoved).OrderBy(y => y.ListOrder).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                this.mapper.Map<List<CollaborationLevel>, List<CollaborationLevelDTO>>(CollaborationLevel, CollaborationLevelDTO);
                return CollaborationLevelDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// UpdateCollaborationLevel.
        /// </summary>
        /// <param name="CollaborationLevelDTO">CollaborationLevelDTO.</param>
        /// <returns>CollaborationLevelDTO.</returns>
        public CollaborationLevelDTO UpdateCollaborationLevel(CollaborationLevelDTO CollaborationLevelDTO)
        {
            try
            {
                CollaborationLevel collaborationLevel = new CollaborationLevel();
                this.mapper.Map<CollaborationLevelDTO, CollaborationLevel>(CollaborationLevelDTO, collaborationLevel);
                var result = this.UpdateAsync(collaborationLevel).Result;
                this.mapper.Map<CollaborationLevel, CollaborationLevelDTO>(result, CollaborationLevelDTO);
                this._cache.DeleteAll(new List<string>() { PCISEnum.Caching.GetAllLevels, PCISEnum.Caching.GetCollaborationLevelLookup });
                return CollaborationLevelDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
