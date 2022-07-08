using System;
using System.Linq;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Infrastructure.Data.Repositories.Base;

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{
    public class OptionsRepository : BaseRepository<Person>, IOptionsRepository
    {
        private readonly OpeekaDBContext _dbContext;
        public OptionsRepository(OpeekaDBContext dbContext)
            : base(dbContext)
        {
            this._dbContext = dbContext;
        }

        /// <summary>
        /// IsValidListOrder.
        /// </summary>
        /// <param name="listOrder">listOrder.</param>
        /// <param name="tableName">tableName.</param>
        /// <param name="agencyID">agencyID.</param>
        /// <param name="id">id.</param>
        /// <returns>bool</returns>
        public bool IsValidListOrder(int listOrder, string tableName, long agencyID, long id = 0)
        {
            try
            {
                var query = string.Empty;
                if (id != 0)
                {
                    query = @"Select count (*) from [info].[" + tableName + "] WHERE " + tableName + "ID != " + id + " and  ListOrder = " + listOrder + " and IsRemoved=0 AND AgencyID =" + agencyID;
                }
                else
                {
                    query = @"Select count (*) from [info].[" + tableName + "] WHERE  ListOrder = " + listOrder + " and IsRemoved=0 AND AgencyID =" + agencyID;
                }

                OptionsResponseDTO data = ExecuteSqlQuery(query, x => new OptionsResponseDTO
                {
                    TotalCount = (int)x[0]
                }).FirstOrDefault();

                if (data != null && data.TotalCount > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
