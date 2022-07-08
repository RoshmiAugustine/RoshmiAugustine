using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Infrastructure.Data.Repositories.Base;

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{
    public class CollaborationSharingHistoryRepository : BaseRepository<CollaborationSharingHistory>, ICollaborationSharingHistoryRepository
    {

        private readonly IMapper mapper;
        private readonly OpeekaDBContext _dbContext;

        public CollaborationSharingHistoryRepository(OpeekaDBContext dbContext, IMapper mapper)
            : base(dbContext)
        {
            this.mapper = mapper;
            this._dbContext = dbContext;
        }
 

        public CollaborationSharingHistoryDTO AddCollaborationSharingHistroy(CollaborationSharingHistoryDTO collaborationHistoryDTO)
        {
            try
            {
                CollaborationSharingHistory colloaborationHistory = new CollaborationSharingHistory();
                this.mapper.Map<CollaborationSharingHistoryDTO, CollaborationSharingHistory>(collaborationHistoryDTO, colloaborationHistory);
                var result = this.AddAsync(colloaborationHistory).Result;
                CollaborationSharingHistoryDTO updated = new CollaborationSharingHistoryDTO();
                this.mapper.Map<CollaborationSharingHistory, CollaborationSharingHistoryDTO>(result, updated);
                return updated;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
