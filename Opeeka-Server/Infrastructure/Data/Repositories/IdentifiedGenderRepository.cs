using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Infrastructure.Data.Repositories.Base;

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{
    public class IdentifiedGenderRepository : BaseRepository<IdentifiedGender>, IIdentifiedGenderRepository
    {
        private readonly OpeekaDBContext _dbContext;

        public IdentifiedGenderRepository(OpeekaDBContext dbContext)
          : base(dbContext)
        {
            this._dbContext = dbContext;
        }

        /// <summary>
        /// AddIdentifiedGender
        /// </summary>
        /// <param name="identifiedGender">identifiedGender.</param>
        /// <returns>IdentifiedGender.</returns>
        public IdentifiedGender AddIdentifiedGender(IdentifiedGender identifiedGender)
        {
            try
            {
                var result = this.AddAsync(identifiedGender).Result;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<IdentifiedGender> GetIdentifiedGenderList(long agencyID)
        {
            try
            {
                List<IdentifiedGender> genderList = new List<IdentifiedGender>();
                genderList = this._dbContext.IdentifiedGender.Where(x => x.AgencyID == agencyID && !x.IsRemoved).OrderBy(y => y.ListOrder).ToList();
                return genderList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// GetIdentifiedGender.
        /// </summary>
        /// <param name="identifiedGenderID">identifiedGenderID.</param>
        /// <returns>IdentifiedGender.</returns>
        public async Task<IdentifiedGender> GetIdentifiedGender(int identifiedGenderID)
        {
            try
            {
                IdentifiedGender identifiedGender = await this.GetRowAsync(x => x.IdentifiedGenderID == identifiedGenderID);
                return identifiedGender;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// GetIdentifiedGenderCount.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>int.</returns>
        public int GetIdentifiedGenderCount(long agencyID)
        {
            try
            {
                var count = this._dbContext.IdentifiedGender.Where(x => x.AgencyID == agencyID && !x.IsRemoved).Count();
                return count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// GetIdentifiedGenderList.
        /// </summary>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>IdentifiedGender.</returns>
        public List<IdentifiedGender> GetIdentifiedGenderList(int pageNumber, int pageSize, long agencyID)
        {
            try
            {
                List<IdentifiedGender> genderList = this._dbContext.IdentifiedGender.Where(x => x.AgencyID == agencyID && !x.IsRemoved).OrderBy(y => y.ListOrder).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                return genderList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// UpdateIdentifiedGender.
        /// </summary>
        /// <param name="identifiedGender">identifiedGender.</param>
        /// <returns>IdentifiedGender.</returns>
        public IdentifiedGender UpdateIdentifiedGender(IdentifiedGender identifiedGender)
        {
            try
            {
                var result = this.UpdateAsync(identifiedGender).Result;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get count of people by Identified Gender
        /// </summary>
        /// <param name="identifiedGenderID">identifiedGenderID</param>
        /// <returns>int</returns>
        public int GetPeopleCountByIdentifiedGenderID(int identifiedGenderID)
        {
            int count = (
                            from row
                            in this._dbContext.Person
                            where
                                row.BiologicalSexID == identifiedGenderID && !row.IsRemoved
                            select
                                row
                        ).Count();
            return count;
        }


        /// <summary>
        /// GetIdentifiedGenderDetailsByName.
        /// </summary>
        /// <param name="nameCSV">nameCSV.</param>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>IdentifiedGenderDTO.</returns>
        public List<IdentifiedGenderDTO> GetIdentifiedGenderDetailsByName(string nameCSV, long agencyID)
        {
            List<IdentifiedGenderDTO> dataList = new List<IdentifiedGenderDTO>();
            try
            {
                var query = string.Empty;
                query = @$"SELECT IdentifiedGenderID,[Name]
                        FROM    [info].[IdentifiedGender] where [Name] in ({nameCSV})
                        and  AgencyID = {agencyID}";

                dataList = ExecuteSqlQuery(query, x => new IdentifiedGenderDTO
                {
                    IdentifiedGenderID = (int)x["IdentifiedGenderID"],
                    Name = x["Name"] == DBNull.Value ? null : (string)x["Name"],
                });
                return dataList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
