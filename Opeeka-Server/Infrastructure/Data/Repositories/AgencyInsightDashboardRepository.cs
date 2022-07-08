using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Infrastructure.Data.Repositories.Base;

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{
    public class AgencyInsightDashboardRepository : BaseRepository<AgencyInsightDashboard>, IAgencyInsightDashboardRepository
    {
        private readonly OpeekaDBContext _dbContext;
        public AgencyInsightDashboardRepository(OpeekaDBContext dbContext)
         : base(dbContext)
        {
            this._dbContext = dbContext;
        }
        /// <summary>
        /// GetInsightDashboardDetailsByAgencyId
        /// </summary>
        /// <param name="agencyId"></param>
        /// <returns></returns>
        public List<AgencyInsightDashboard> GetInsightDashboardDetailsByAgencyId(long agencyId)
        {
            try
            {
                var query = string.Empty;
                query = @$"SELECT AgencyInsightDashboardId, DashboardId, Name, ShortDescription, LongDescription, IconURL, UpdateDate, ListOrder FROM AgencyInsightDashboard where IsRemoved = 0 and AgencyID = { agencyId } order by ListOrder";

                var result = ExecuteSqlQuery(query, x => new AgencyInsightDashboard
                {
                    AgencyInsightDashboardId = (int)x["AgencyInsightDashboardId"],
                    DashboardId = (int)x["DashboardId"],
                    Name = x["Name"] == DBNull.Value ? string.Empty : (string)x["Name"],
                    ShortDescription = x["ShortDescription"] == DBNull.Value ? string.Empty : (string)x["ShortDescription"],
                    LongDescription = x["LongDescription"] == DBNull.Value ? string.Empty : (string)x["LongDescription"],
                    IconURL = x["IconURL"] == DBNull.Value ? string.Empty : (string)x["IconURL"],
                    AgencyId = agencyId,
                    UpdateDate = (DateTime)x["UpdateDate"],
                    ListOrder = (int)x["ListOrder"],
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// GetInsightDashboardDetailsById.
        /// </summary>
        /// <param name="insightDashboardId"></param>
        /// <returns></returns>
        public AgencyInsightDashboard GetInsightDashboardDetailsById(int insightDashboardId)
        {
            try
            {
                var result = this.GetRowAsync(x => x.AgencyInsightDashboardId == insightDashboardId).Result;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
