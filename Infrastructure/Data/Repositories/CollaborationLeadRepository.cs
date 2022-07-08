// -----------------------------------------------------------------------
// <copyright file="CollaborationLeadRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using AutoMapper;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Infrastructure.Data.Repositories.Base;

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{
    public class CollaborationLeadRepository : BaseRepository<CollaborationLeadHistory>, ICollaborationLeadRepository
    {
        private readonly IMapper mapper;
        private readonly OpeekaDBContext _dbContext;
        public CollaborationLeadRepository(OpeekaDBContext dbContext, IMapper mapper)
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
        public Int64 AddCollaborationLead(CollaborationLeadHistoryDTO collaborationLeadDTO)
        {
            try
            {
                CollaborationLeadHistory collaborationlead = new CollaborationLeadHistory();
                this.mapper.Map<CollaborationLeadHistoryDTO, CollaborationLeadHistory>(collaborationLeadDTO, collaborationlead);
                var result = (this.AddAsync(collaborationlead).Result.CollaborationLeadHistoryID);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// To update collaboration lead history details.
        /// </summary>
        /// <param name="CollaborationLeadHistoryDTO">CollaborationLeadHistoryDTO.</param>
        /// <returns>CollaborationLeadHistoryDTO.</returns>
        public CollaborationLeadHistoryDTO UpdateCollaborationLeadHistory(CollaborationLeadHistoryDTO collaborationLeadHistoryDTO)
        {
            try
            {
                CollaborationLeadHistory collaborationLeadHistory = new CollaborationLeadHistory();
                this.mapper.Map<CollaborationLeadHistoryDTO, CollaborationLeadHistory>(collaborationLeadHistoryDTO, collaborationLeadHistory);
                var result = this.UpdateAsync(collaborationLeadHistory).Result;
                CollaborationLeadHistoryDTO updatedCollaborationLead = new CollaborationLeadHistoryDTO();
                this.mapper.Map<CollaborationLeadHistory, CollaborationLeadHistoryDTO>(result, updatedCollaborationLead);
                return updatedCollaborationLead;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetCollaborationLeads.
        /// </summary>
        /// <param name="collaborationID">collaborationID</param>
        /// <returns>CollaborationLeadHistoryDTO List.</returns>
        public List<CollaborationLeadHistoryDTO> GetCollaborationLeads(int collaborationID)
        {
            try
            {
                var query = string.Empty;
                query = @"Select CollaborationLeadHistoryID, CollaborationID, LeadUserID, RemovedUserID, StartDate, EndDate, IsRemoved from CollaborationLeadHistory
                            WHERE IsRemoved = 0 AND CollaborationID = " + collaborationID;

                var collaborationLeadHistory = ExecuteSqlQuery(query, x => new CollaborationLeadHistoryDTO
                {
                    CollaborationLeadHistoryID = x[0] == DBNull.Value ? 0 : (int)x[0],
                    CollaborationID = x[1] == DBNull.Value ? 0 : (int)x[1],
                    LeadUserID = x[2] == DBNull.Value ? 0 : (int)x[2],
                    RemovedUserID = x[3] == DBNull.Value ? 0 : (int)x[3],
                    StartDate = x[4] == DBNull.Value ? DateTime.MinValue : (DateTime?)x[4],
                    EndDate = x[5] == DBNull.Value ? DateTime.MinValue : (DateTime?)x[5],
                    IsRemoved = x[6] == DBNull.Value ? false : (bool)x[6]
                });

                return collaborationLeadHistory;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
