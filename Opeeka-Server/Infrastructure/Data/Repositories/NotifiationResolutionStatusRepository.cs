using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Infrastructure.Data.Repositories.Base;
using Opeeka.PICS.Infrastructure.Enums;

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{
    public class NotifiationResolutionStatusRepository : BaseRepository<NotificationResolutionStatus>, INotifiationResolutionStatusRepository
    {
        private readonly IMapper mapper;
        private readonly ILogger<NotifiationResolutionStatusRepository> logger;
        private readonly OpeekaDBContext _dbContext;
        private readonly ICache _cache;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotifiationResolutionStatusRepository"/> class.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public NotifiationResolutionStatusRepository(ILogger<NotifiationResolutionStatusRepository> logger, OpeekaDBContext dbContext, IMapper mapper, ICache cache)
            : base(dbContext)
        {
            this.mapper = mapper;
            this.logger = logger;
            this._dbContext = dbContext;
            this._cache = cache;
        }


        /// <summary>
        /// GetNotificationStatus
        /// </summary>
        /// <returns>NotificationResolutionStatusDTO</returns>
        public NotificationResolutionStatus GetNotificationStatus(string status)
        {
            try
            {
                var allnotificationStatus = GetAllNotificationStatus();
                NotificationResolutionStatus notificationResolutionStatus = allnotificationStatus.Where(x => x.Name == status && !x.IsRemoved).FirstOrDefault();
                return notificationResolutionStatus;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetNotificationStatusById
        /// </summary>
        /// <returns>NotificationResolutionStatusDTO</returns>
        public NotificationResolutionStatus GetNotificationStatusById(int statusId)
        {
            try
            {
                var allnotificationStatus = GetAllNotificationStatus();
                NotificationResolutionStatus notificationResolutionStatus = allnotificationStatus.Where(x => x.NotificationResolutionStatusID == statusId && !x.IsRemoved).FirstOrDefault();
                return notificationResolutionStatus;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get Notification Status For Unresolved
        /// </summary>
        /// <returns></returns>
        public NotificationResolutionStatusDTO GetNotificationStatusForUnResolved()
        {
            try
            {
                var allnotificationStatus = GetAllNotificationStatus();
                NotificationResolutionStatusDTO notificationResolutionStatusDTO = new NotificationResolutionStatusDTO();
                NotificationResolutionStatus notificationResolutionStatus = allnotificationStatus.Where(x => x.Name == "Unresolved").FirstOrDefault();
                this.mapper.Map<NotificationResolutionStatus, NotificationResolutionStatusDTO>(notificationResolutionStatus, notificationResolutionStatusDTO);
                return notificationResolutionStatusDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get Notification Status For Resolved
        /// </summary>
        /// <returns></returns>
        public NotificationResolutionStatusDTO GetNotificationStatusForResolved()
        {
            try
            {
                var allnotificationStatus = GetAllNotificationStatus();
                NotificationResolutionStatusDTO notificationResolutionStatusDTO = new NotificationResolutionStatusDTO();
                NotificationResolutionStatus notificationResolutionStatus = allnotificationStatus.Where(x => x.Name == "Resolved").FirstOrDefault();
                this.mapper.Map<NotificationResolutionStatus, NotificationResolutionStatusDTO>(notificationResolutionStatus, notificationResolutionStatusDTO);
                return notificationResolutionStatusDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private List<NotificationResolutionStatus> GetAllNotificationStatus()
        {
            var readFromCache = this._cache.Get<List<NotificationResolutionStatus>>(PCISEnum.Caching.GetAllNotificationStatus);
            if (readFromCache == null || readFromCache?.Count == 0)
            {
                var notificationStatus = this.GetAsync(x => !x.IsRemoved).Result.ToList();
                this._cache.Post(PCISEnum.Caching.GetAllNotificationStatus, readFromCache = notificationStatus);
            }
            return readFromCache;
        }
    }
}
