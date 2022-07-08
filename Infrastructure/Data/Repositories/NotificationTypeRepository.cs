// -----------------------------------------------------------------------
// <copyright file="NotificationTypeRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Infrastructure.Data.Repositories.Base;
using Opeeka.PICS.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{
    public class NotificationTypeRepository : BaseRepository<NotificationType>, INotificationTypeRepository
    {
        private readonly OpeekaDBContext _dbContext;

        private readonly ICache _cache;
        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationTypeRepository"/> class.
        /// </summary>
        /// <param name="dbContext"></param>
        public NotificationTypeRepository(OpeekaDBContext dbContext, ICache cache)
            : base(dbContext)
        {
            this._dbContext = dbContext;
            this._cache = cache;
        }

        /// <summary>
        /// GetAllNotificationType
        /// </summary>
        /// <returns>list of NotificationType</returns>
        public List<NotificationType> GetAllNotificationType()
        {
            try
            {
                var readFromCache = this._cache.Get<List<NotificationType>>(PCISEnum.Caching.GetAllNotificationType);
                if (readFromCache == null || readFromCache?.Count == 0)
                {
                    var NotificationType = this._dbContext.NotificationTypes.Where(x => !x.IsRemoved).OrderBy(y => y.ListOrder).ToList();
                    this._cache.Post(PCISEnum.Caching.GetAllNotificationType, readFromCache = NotificationType);
                }
                return readFromCache;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetAllNotificationType
        /// </summary>
        /// <returns>list of NotificationType</returns>
        public async Task<NotificationType> GetNotificationType(string name)
        {
            try
            {
                var readFromCache = this._cache.Get<List<NotificationType>>(PCISEnum.Caching.GetAllNotificationType);
                if (readFromCache == null || readFromCache?.Count == 0)
                {
                    var NotificationType = await this.GetRowAsync(x => !x.IsRemoved && x.Name == name);
                    return NotificationType;
                }
                return readFromCache.Where(x => !x.IsRemoved && x.Name == name).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
