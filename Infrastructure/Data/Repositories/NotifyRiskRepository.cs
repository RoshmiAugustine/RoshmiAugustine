using System;
using System.Linq;
using AutoMapper;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Infrastructure.Data.Repositories.Base;

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{
    public class NotifyRiskRepository : BaseRepository<NotifyRisk>, INotifyRiskRepository
    {
        private readonly OpeekaDBContext _dbContext;
        private readonly IMapper mapper;

        public NotifyRiskRepository(OpeekaDBContext dbContext, IMapper mapper)
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
        public NotifyRisk AddNotifyRisk(NotifyRisk notifyRisk)
        {
            try
            {
                var result = this.AddAsync(notifyRisk).Result;
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetPersonQuestionnaireID
        /// </summary>
        /// <param name="personIndex"></param>
        /// <param name="questionnaireID"></param>
        /// <returns>long</returns>
        public int GetNotifyRiskCount(int assessmentID)
        {
            try
            {
                var query = string.Empty;
                query = @"select  COUNT(1)  FROM NotifyRisk where 
                IsRemoved=0 And AssessmentID=" + assessmentID;
                var count = ExecuteSqlQuery(query, x => Convert.ToInt32(x[0])).FirstOrDefault();
                return count;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
