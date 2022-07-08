using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Infrastructure.Data.Repositories.Base;

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{
    public class NotifiationResolutionHistoryRepository : BaseRepository<NotificationResolutionHistory>, INotifiationResolutionHistoryRepository
    {
        private readonly IMapper mapper;
        private readonly ILogger<NotifiationResolutionHistoryRepository> logger;
        private readonly OpeekaDBContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotifiationResolutionHistoryRepository"/> class.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public NotifiationResolutionHistoryRepository(ILogger<NotifiationResolutionHistoryRepository> logger, OpeekaDBContext dbContext, IMapper mapper)
            : base(dbContext)
        {
            this.mapper = mapper;
            this.logger = logger;
            this._dbContext = dbContext;
        }

        /// <summary>
        /// AddNotificationResolutionHistory.
        /// </summary>
        /// <param name="notificationResolutionHistory">notificationResolutionHistory.</param>
        /// <returns>NotificationResolutionHistory.</returns>
        public NotificationResolutionHistory AddNotificationResolutionHistory(NotificationResolutionHistory notificationResolutionHistory)
        {
            try
            {
                var result = this.AddAsync(notificationResolutionHistory).Result;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// UpdateBulkNotificationResolutionHistory.
        /// </summary>
        /// <param name="NotificationResolutionHistory">NotificationResolutionHistory.</param>
        /// <returns>NotificationLog</returns>
        public void AddBulkNotificationResolutionHistory(List<NotificationResolutionHistory> notificationResolutionHistoryList)
        {
            try
            {
                var result = this.UpdateBulkAsync(notificationResolutionHistoryList);
                result.Wait();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
