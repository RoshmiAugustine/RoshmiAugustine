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
    public class NotificationlevelRepository : BaseRepository<NotificationLevel>, INotificationLevelRepository
    {
        private readonly IMapper mapper;
        private readonly OpeekaDBContext _dbContext;
        private readonly ICache _cache;

        public NotificationlevelRepository(OpeekaDBContext dbContext, IMapper mapper, ICache cache)
            : base(dbContext)
        {
            this.mapper = mapper;
            this._dbContext = dbContext;
            this._cache = cache;
        }

        /// <summary>
        /// AddNotificationLevel.
        /// </summary>
        /// <param name="NotificationLevelDTO">NotificationLevelDTO.</param>
        /// <returns>NotificationLevelDTO.</returns>
        public NotificationLevelDTO AddNotificationLevel(NotificationLevelDTO NotificationLevelDTO)
        {
            try
            {
                NotificationLevel notificationLevel = new NotificationLevel();
                this.mapper.Map<NotificationLevelDTO, NotificationLevel>(NotificationLevelDTO, notificationLevel);
                var result = this.AddAsync(notificationLevel).Result;
                this.mapper.Map<NotificationLevel, NotificationLevelDTO>(result, NotificationLevelDTO);
                this._cache.DeleteAll(new List<string>() { PCISEnum.Caching.GetAllNotificationLevel, PCISEnum.Caching.GetAgencyNotificationLevelList });
                return NotificationLevelDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetNotificationLevelCount.
        /// </summary>
        /// <returns>int.</returns>
        public int GetNotificationLevelCount(long agencyID)
        {
            try
            {
                var count = this._dbContext.NotificationLevels.Where(x => !x.IsRemoved && x.AgencyID == agencyID).Count();
                return count;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetNotificationLevel.
        /// </summary>
        /// <param name="notificationLevelID">notificationLevelID.</param>
        /// <returns>NotificationLevelDTO.</returns>
        public async Task<NotificationLevelDTO> GetNotificationLevel(int notificationLevelID)
        {
            try
            {
                NotificationLevelDTO notificationLevelDTO = new NotificationLevelDTO();
                NotificationLevel notificationLevel = await this.GetRowAsync(x => x.NotificationLevelID == notificationLevelID);
                this.mapper.Map<NotificationLevel, NotificationLevelDTO>(notificationLevel, notificationLevelDTO);
                return notificationLevelDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetNotificationLevelList.
        /// </summary>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>List of NotificationLevelDTO.</returns>
        public List<NotificationLevelDTO> GetNotificationLevelList(int pageNumber, int pageSize, long agencyID)
        {
            try
            {
                List<NotificationLevelDTO> NotificationLevelDTO = new List<NotificationLevelDTO>();
                var NotificationLevel = this._dbContext.NotificationLevels.Where(x => x.AgencyID == agencyID && !x.IsRemoved).OrderBy(y => y.ListOrder).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                this.mapper.Map<List<NotificationLevel>, List<NotificationLevelDTO>>(NotificationLevel, NotificationLevelDTO);
                return NotificationLevelDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// UpdateNotificationLevel.
        /// </summary>
        /// <param name="NotificationLevelDTO">NotificationLevelDTO.</param>
        /// <returns>NotificationLevelDTO.</returns>
        public NotificationLevelDTO UpdateNotificationLevel(NotificationLevelDTO NotificationLevelDTO)
        {
            try
            {
                NotificationLevel notificationLevel = new NotificationLevel();
                this.mapper.Map<NotificationLevelDTO, NotificationLevel>(NotificationLevelDTO, notificationLevel);
                var result = this.UpdateAsync(notificationLevel).Result;
                this.mapper.Map<NotificationLevel, NotificationLevelDTO>(result, NotificationLevelDTO);
                this._cache.DeleteAll(new List<string>() { PCISEnum.Caching.GetAllNotificationLevel, PCISEnum.Caching.GetAgencyNotificationLevelList });
                return NotificationLevelDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetNotificationLevelCountByNotificationLevelID
        /// </summary>
        /// <param name="notificationLevelID">notificationLevelID</param>
        /// <returns>int</returns>
        public int GetNotificationLevelCountByNotificationLevelID(int notificationLevelID)
        {
            int questionnaireNotifyRiskRulescount = (
                            from row
                            in this._dbContext.QuestionnaireNotifyRiskRules
                            where
                                row.NotificationLevelID == notificationLevelID && !row.IsRemoved
                            select
                                row
                        ).Count();

            int questionnaireReminderTypescount = (
                            from row
                            in this._dbContext.QuestionnaireReminderTypes
                            where
                                row.NotificationLevelID == notificationLevelID && !row.IsRemoved
                            select
                                row
                        ).Count();

            return questionnaireNotifyRiskRulescount + questionnaireReminderTypescount;
        }

    }
}
