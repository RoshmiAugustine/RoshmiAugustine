using System;
using System.Collections.Generic;
using AutoMapper;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Infrastructure.Data.Repositories.Base;

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{
    public class NotifyRiskValueRepository : BaseRepository<NotifyRiskValue>, INotifyRiskValueRepository
    {
        private readonly OpeekaDBContext _dbContext;
        private readonly IMapper mapper;

        public NotifyRiskValueRepository(OpeekaDBContext dbContext, IMapper mapper)
            : base(dbContext)
        {
            this._dbContext = dbContext;
            this.mapper = mapper;
        }

        /// <summary>
        /// UpdateNotificationLog.
        /// </summary>
        /// <param name="notificationLog">notificationLog.</param>
        /// <returns>NotificationLog</returns>
        public void AddBulkNotifyRiskValue(List<NotifyRiskValue> notifyRiskValue)
        {
            try
            {
                var result = this.AddBulkAsync(notifyRiskValue);
                result.Wait();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
