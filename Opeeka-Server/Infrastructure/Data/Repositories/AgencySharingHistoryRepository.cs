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
    public class AgencySharingHistoryRepository: BaseRepository<AgencySharingHistory>, IAgencySharingHistoryRepository
    {
        private readonly IMapper mapper;
        private readonly OpeekaDBContext _dbContext;

        public AgencySharingHistoryRepository(OpeekaDBContext dbContext, IMapper mapper)
            : base(dbContext)
        {
            this.mapper = mapper;
            this._dbContext = dbContext;
        }

        public AgencySharingHistoryDTO AddAgencySharingHistroy(AgencySharingHistoryDTO agencySharingHistoryDTO)
        {
            try
            {
                AgencySharingHistory agencySharingHistory = new AgencySharingHistory();
                this.mapper.Map<AgencySharingHistoryDTO, AgencySharingHistory>(agencySharingHistoryDTO, agencySharingHistory);
                var result = this.AddAsync(agencySharingHistory).Result;
                AgencySharingHistoryDTO updated = new AgencySharingHistoryDTO();
                this.mapper.Map<AgencySharingHistory, AgencySharingHistoryDTO>(result, updated);
                return updated;
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
