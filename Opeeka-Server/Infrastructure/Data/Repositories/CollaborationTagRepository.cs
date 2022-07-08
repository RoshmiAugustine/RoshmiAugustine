// -----------------------------------------------------------------------
// <copyright file="CollaborationTagRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Infrastructure.Data.Repositories.Base;

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{
    public class CollaborationTagRepository : BaseRepository<CollaborationTag>, ICollaborationTagRepository
    {
        private readonly IMapper mapper;
        private readonly OpeekaDBContext _dbContext;
        public CollaborationTagRepository(OpeekaDBContext dbContext, IMapper mapper)
            : base(dbContext)
        {
            this.mapper = mapper;
            this._dbContext = dbContext;
        }

        /// <summary>
        /// To add collaboration tag.
        /// </summary>
        /// <param name="collaborationTagData">collaborationTagData.</param>
        /// <returns>id.</returns>
        public Int64 AddCollaborationTag(CollaborationTagDTO collaborationTagDTO)
        {
            try
            {
                CollaborationTag collaborationtag = new CollaborationTag();
                this.mapper.Map<CollaborationTagDTO, CollaborationTag>(collaborationTagDTO, collaborationtag);
                var result = (this.AddAsync(collaborationtag).Result.CollaborationTagID);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// To update collaboration tag details.
        /// </summary>
        /// <param name="CollaborationTagDTO">CollaborationTagDTO.</param>
        /// <returns>CollaborationTagDTO.</returns>
        public CollaborationTagDTO UpdateCollaborationTag(CollaborationTagDTO collaborationTagDTO)
        {
            try
            {
                CollaborationTag collaborationTag = new CollaborationTag();
                this.mapper.Map<CollaborationTagDTO, CollaborationTag>(collaborationTagDTO, collaborationTag);
                var result = this.UpdateAsync(collaborationTag).Result;
                CollaborationTagDTO updatedCollaborationTag = new CollaborationTagDTO();
                this.mapper.Map<CollaborationTag, CollaborationTagDTO>(result, updatedCollaborationTag);
                return updatedCollaborationTag;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetCollaborationCategories.
        /// </summary>
        /// <param name="collaborationID">collaborationID</param>
        /// <returns>CollaborationTagDTO List.</returns>
        public List<CollaborationTagDTO> GetCollaborationCategories(int collaborationID)
        {
            try
            {
                var query = string.Empty;
                query = @"Select CollaborationTagID, CollaborationID, CollaborationTagTypeID from CollaborationTag
                            WHERE IsRemoved = 0 and CollaborationID = " + collaborationID;

                var Collaborationcategories = ExecuteSqlQuery(query, x => new CollaborationTagDTO
                {
                    CollaborationTagID = x[0] == DBNull.Value ? 0 : (int)x[0],
                    CollaborationID = x[1] == DBNull.Value ? 0 : (int)x[1],
                    CollaborationTagTypeID = x[2] == DBNull.Value ? 0 : (int)x[2]
                });

                return Collaborationcategories;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get the count of Collaboration tags having a specific tag type
        /// </summary>
        /// <param name="collaborationTagTypeID"></param>
        /// <returns>int</returns>
        public int GetCollaborationTagTypeCountByCollaborationTag(int collaborationTagTypeID)
        {
            int count = (
                            from row
                            in this._dbContext.CollaborationTag
                            where
                                row.CollaborationTagTypeID == collaborationTagTypeID && !row.IsRemoved
                            select
                                row
                        ).Count();

            return count;
        }
    }
}
