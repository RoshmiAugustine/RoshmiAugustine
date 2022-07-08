// -----------------------------------------------------------------------
// <copyright file="RaceEthnicityRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Infrastructure.Data.Repositories.Base;

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{
    public class RaceEthnicityRepository : BaseRepository<RaceEthnicity>, IRaceEthnicityRepository
    {
        private readonly OpeekaDBContext _dbContext;
        private readonly IMapper mapper;

        public RaceEthnicityRepository(OpeekaDBContext dbContext, IMapper mapper)
            : base(dbContext)
        {
            this._dbContext = dbContext;
            this.mapper = mapper;
        }

        /// <summary>
        /// To add agent details.
        /// </summary>
        /// <returns> CountryStateDTO.</returns>
        public async Task<List<RaceEthnicityDTO>> GetAllRaceEthnicity()
        {
            try
            {
                var raceEthnicity = await this.GetAsync(x => !x.IsRemoved);
                return this.mapper.Map<List<RaceEthnicityDTO>>(raceEthnicity.OrderBy(x => x.ListOrder));
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// AddRaceEthnicity.
        /// </summary>
        /// <param name="RaceEthnicity">RaceEthnicity.</param>
        /// <returns>RaceEthnicity.</returns>
        public RaceEthnicity AddRaceEthnicity(RaceEthnicity RaceEthnicity)
        {
            try
            {
                var result = this.AddAsync(RaceEthnicity).Result;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// GetRaceEthnicity.
        /// </summary>
        /// <param name="raceEthnicityID">raceEthnicityID.</param>
        /// <returns> Task<RaceEthnicity>.</returns>
        public async Task<RaceEthnicity> GetRaceEthnicity(Int64 raceEthnicityID)
        {
            try
            {
                RaceEthnicity raceEthnicity = await this.GetRowAsync(x => x.RaceEthnicityID == raceEthnicityID);
                return raceEthnicity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// GetRaceEthnicityCount.
        /// </summary>
        /// <returns>int.</returns>
        public int GetRaceEthnicityCount(long agencyID)
        {
            try
            {
                var count = this._dbContext.RaceEthnicities.Where(x => !x.IsRemoved && x.AgencyID == agencyID).Count();
                return count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// GetRaceEthnicityList.
        /// </summary>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>List<RaceEthnicity>.</returns>
        public List<RaceEthnicity> GetRaceEthnicityList(int pageNumber, int pageSize, long agencyID)
        {
            try
            {
                var raceEthnicity = this._dbContext.RaceEthnicities.Where(x => x.AgencyID == agencyID && !x.IsRemoved).OrderBy(y => y.ListOrder).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                return raceEthnicity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// UpdateRaceEthnicity.
        /// </summary>
        /// <param name="RaceEthnicity">RaceEthnicity</param>
        /// <returns>RaceEthnicity</returns>
        public RaceEthnicity UpdateRaceEthnicity(RaceEthnicity RaceEthnicity)
        {
            try
            {
                var result = this.UpdateAsync(RaceEthnicity).Result;
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetRaceEthnicityUsedByID.
        /// </summary>
        /// <param name="raceEthnicityID">levelID.</param>
        /// <returns>int.</returns>
        public int GetRaceEthnicityUsedByID(long raceEthnicityID)
        {
            int count = (from row in this._dbContext.PersonRaceEthnicity
                         where (row.RaceEthnicityID == raceEthnicityID) && !row.IsRemoved
                         select row).Count();

            return count;
        }

        /// <summary>
        /// GetAgencyRaceEthnicityList
        /// </summary>
        /// <param name="personID"></param>
        /// <returns>List of PeopleRaceEthnicityDTO</returns>
        public List<RaceEthnicityDTO> GetAgencyRaceEthnicityList(long agencyID)
        {
            try
            {
                var query = string.Empty;
                query = @"Select RaceEthnicityID, [Name], Abbrev, Description, ListOrder, IsRemoved, UpdateDate, UpdateUserID, AgencyID  From info.RaceEthnicity where AgencyID = " + agencyID + " and IsRemoved = 0 order by ListOrder";

                var raceEthnicity = ExecuteSqlQuery(query, x => new RaceEthnicityDTO
                {
                    RaceEthnicityID = (int)x[0],
                    Name = x[1] == DBNull.Value ? null : (string)x[1],
                    Abbrev = x[2] == DBNull.Value ? null : (string)x[2],
                    Description = x[3] == DBNull.Value ? null : (string)x[3],
                    ListOrder = x[4] == DBNull.Value ? 0 : (int)x[4],
                    IsRemoved = x[5] == DBNull.Value ? false : (bool)x[5],
                    UpdateDate = (DateTime)x[6],
                    UpdateUserID = x[7] == DBNull.Value ? 0 : (int)x[7],
                    AgencyID = (long)x[8]
                });

                return raceEthnicity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<RaceEthnicityDTO> GetRaceEthnicityDetailsByName(string nameCSV, long agencyID)
        {
            List<RaceEthnicityDTO> dataList = new List<RaceEthnicityDTO>();
            try
            {
                nameCSV = string.IsNullOrEmpty(nameCSV) ? null : nameCSV.ToLower();
                var query = string.Empty;
                query = @$"SELECT RaceEthnicityID,[Name]
                        FROM    [info].[RaceEthnicity] where LOWER( [Name]) in({nameCSV})
                        and  AgencyID = {agencyID}";

                dataList = ExecuteSqlQuery(query, x => new RaceEthnicityDTO
                {
                    RaceEthnicityID = (int)x["RaceEthnicityID"],
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
