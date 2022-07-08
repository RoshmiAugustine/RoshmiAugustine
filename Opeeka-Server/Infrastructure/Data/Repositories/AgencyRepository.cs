// -----------------------------------------------------------------------
// <copyright file="AgencyRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Internal;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.DTO.Input;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Infrastructure.Data.Repositories.Base;

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{
    public class AgencyRepository : BaseRepository<Agency>, IAgencyRepository
    {
        private readonly IMapper mapper;
        private readonly OpeekaDBContext _dbContext;
        public AgencyRepository(OpeekaDBContext dbContext, IMapper mapper)
            : base(dbContext)
        {
            this.mapper = mapper;
            this._dbContext = dbContext;
        }

        /// <summary>
        /// To add agent details.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns>List of summaries.</returns>
        public long AddAgency(AgencyDTO agencyDTO)
        {
            try
            {
                Agency agency = new Agency();
                this.mapper.Map<AgencyDTO, Agency>(agencyDTO, agency);
                var result = this.AddAsync(agency).Result.AgencyID;
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// To add agent details.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns>List of summaries.</returns>
        public AgencyDTO UpdateAgency(AgencyDTO agencyDTO)
        {
            try
            {
                Agency agency = new Agency();
                this.mapper.Map<AgencyDTO, Agency>(agencyDTO, agency);
                var result = this.UpdateAsync(agency).Result;
                AgencyDTO updatedAgency = new AgencyDTO();
                this.mapper.Map<Agency, AgencyDTO>(result, updatedAgency);
                return updatedAgency;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// To get details agencya.
        /// </summary>
        /// <param id="id">.</param>
        /// <returns>.AgencyDTO</returns>
        public async Task<AgencyDTO> GetAsync(Guid id)
        {
            try
            {
                AgencyDTO agencyDTO = new AgencyDTO();
                Agency agency = await this.GetRowAsync(x => x.AgencyIndex == id);
                this.mapper.Map<Agency, AgencyDTO>(agency, agencyDTO);

                return agencyDTO;
            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// GetAgencyList.
        /// </summary>
        /// <param name="agencySearchDTO"></param>
        /// <param name="queryBuilderDTO"></param>
        /// <returns>Tuple<List<AgencyDTO>,int></returns>
        public Tuple<List<AgencyDTO>, int> GetAgencyList(AgencySearchDTO agencySearchDTO, DynamicQueryBuilderDTO queryBuilderDTO)
        {
            try
            {
                List<AgencyDTO> agencyDTO = new List<AgencyDTO>();
                var query = string.Empty;
                query = @"SELECT
	                        T1.Agency as Name, 
	                        ISNULL(T1.ContactFirstName,'') + ISNULL(' ' + T1.ContactLastName,'') as [ContactFirstName], 
	                        T1.ContactLastName, T1.Phone1, T1.Phone2, T1.Email, NumberOfAddresses, NumberOfCollaboration, 
	                        T1.AgencyID, T1.AgencyIndex, T1.Abbrev,
	                        COUNT(*) OVER() AS TotalCount
                        FROM 
                        (
	                        SELECT 
		                        A.AgencyID,A.AgencyIndex,A.Name AS Agency,A.ContactFirstName,A.ContactLastName,A.Phone1,A.Phone2,A.Email,COUNT(AA.AgencyID) AS NumberOfAddresses,A.Abbrev
	                        FROM Agency A
	                        LEFT JOIN AgencyAddress AA ON AA.agencyid = A.agencyid
	                        Where A.IsRemoved=0
	                        GROUP BY A.Name, A.AgencyIndex, A.agencyid, A.ContactFirstName, A.ContactLastName, A.Phone1, A.Phone2, A.Email,A.Abbrev
                        ) T1
                        JOIN 
                        (
	                        SELECT 
		                        A.AgencyID,A.AgencyIndex,A.Name AS Agency,ISNULL(A.ContactFirstName,'') + ISNULL(' ' + A.ContactLastName,'') as [ContactFirstName],A.ContactLastName,A.Phone1,A.Phone2,A.Email,COUNT(C.CollaborationID) AS NumberOfCollaboration
	                        FROM Agency A
	                        LEFT JOIN Collaboration C ON C.AgencyID = A.AgencyID
	                        Where A.IsRemoved=0
	                        GROUP BY A.Name, A.agencyid,A.AgencyIndex, A.ContactFirstName, A.ContactLastName, A.Phone1, A.Phone2, A.Email
                        ) T2 ON T1.AgencyID = T2.AgencyID
                        WHERE 1=1 ";


                //query += @" OFFSET " + ((pageNumber - 1) * pageSize) + "ROWS FETCH NEXT " + pageSize + " ROWS ONLY";
                query += queryBuilderDTO.WhereCondition + queryBuilderDTO.OrderBy + queryBuilderDTO.Paginate;

                int totalCount = 0;
                agencyDTO = ExecuteSqlQuery(query, x =>
                {
                    totalCount = x["TotalCount"] == DBNull.Value ? 0 : (int)x["TotalCount"];
                    return new AgencyDTO
                    {
                        Name = x[0] == DBNull.Value ? null : (string)x[0],
                        ContactFirstName = x[1] == DBNull.Value ? String.Empty : (string)x[1],
                        ContactLastName = x[2] == DBNull.Value ? String.Empty : (string)x[2],
                        Phone1 = x[3] == DBNull.Value ? null : (string)x[3],
                        Phone2 = x[4] == DBNull.Value ? null : (string)x[4],
                        Email = x[5] == DBNull.Value ? null : (string)x[5],
                        NumberOfAddresses = x[6] == DBNull.Value ? 0 : (int)x[6],
                        NumberOfCollaboration = x[7] == DBNull.Value ? 0 : (int)x[7],
                        AgencyID = (long)x[8],
                        AgencyIndex = (Guid)x[9],
                        Abbrev = x[10] == DBNull.Value ? null : (string)x[10]
                    };
                }, queryBuilderDTO.QueryParameterDTO);
                return Tuple.Create(agencyDTO, totalCount);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetAgencyCount.
        /// </summary>
        /// <returns></returns>
        public int GetAgencyCount()
        {
            try
            {
                var count = this._dbContext.Agencies.Where(x => !x.IsRemoved).Count();
                return count;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetAgencyDetails.
        /// </summary>
        /// <param name="agencyIndex"></param>
        /// <returns></returns>
        public AgencyDataDTO GetAgencyDetails(Guid agencyIndex)
        {
            try
            {
                var response = (from a in this._dbContext.Agencies
                                join aa in this._dbContext.AgencyAddresses on a.AgencyID equals aa.AgencyID
                                join ad in this._dbContext.Address on aa.AddressID equals ad.AddressID
                                where a.AgencyIndex == agencyIndex
                                select new AgencyDataDTO
                                {
                                    AddressID = ad.AddressID,
                                    AgencyID = a.AgencyID,
                                    AgencyIndex = a.AgencyIndex,
                                    AddressIndex = ad.AddressIndex,
                                    ContactFirstName = a.ContactFirstName,
                                    ContactLastName = a.ContactLastName,
                                    Name = a.Name,
                                    Address1 = ad.Address1,
                                    Address2 = ad.Address2,
                                    City = ad.City,
                                    Zip = ad.Zip,
                                    Zip4 = ad.Zip4,
                                    CountryStateID = ad.CountryStateId,
                                    Phone1 = a.Phone1,
                                    Phone2 = a.Phone2,
                                    Email = a.Email,
                                    Abbrev = a.Abbrev,
                                    CountryID = ad.CountryId
                                }).FirstOrDefault();
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetAgencyDetailsByAbbrev.
        /// </summary>
        /// <param name="agencyIndex"></param>
        /// <returns></returns>
        public async Task<AgencyDTO> GetAgencyDetailsByAbbrev(string abbrev)
        {
            try
            {
                AgencyDTO agencyDTO = new AgencyDTO();
                Agency agency = await this.GetRowAsync(x => x.Abbrev == abbrev);
                this.mapper.Map<Agency, AgencyDTO>(agency, agencyDTO);
                return agencyDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// ValidateAgencyName.
        /// </summary>
        /// <param name="agencyDetailsDTO">agencyDetailsDTO.</param>
        /// <returns>bool.</returns>
        public async Task<bool> ValidateAgencyName(AgencyDetailsDTO agencyDetailsDTO)
        {
            try
            {
                Agency agency = new Agency();
                if (agencyDetailsDTO.AgencyID != 0)
                {
                    agency = await this.GetRowAsync(x => x.Name == agencyDetailsDTO.Name && x.AgencyID != agencyDetailsDTO.AgencyID);
                }
                else
                {
                    agency = await this.GetRowAsync(x => x.Name == agencyDetailsDTO.Name);
                }
                if (agency == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// ValidateAgencyAbbrev.
        /// </summary>
        /// <param name="agencyDetailsDTO">agencyDetailsDTO.</param>
        /// <returns>bool.</returns>
        public async Task<bool> ValidateAgencyAbbrev(AgencyDetailsDTO agencyDetailsDTO)
        {
            try
            {
                Agency agency = new Agency();
                if (agencyDetailsDTO.AgencyID != 0)
                {
                    agency = await this.GetRowAsync(x => x.Abbrev == agencyDetailsDTO.Abbrev && x.AgencyID != agencyDetailsDTO.AgencyID);
                }
                else
                {
                    agency = await this.GetRowAsync(x => x.Abbrev == agencyDetailsDTO.Abbrev);
                }
                if (agency == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// GetAgencyDetails.
        /// </summary>
        /// <param name="agencyIndex"></param>
        /// <returns></returns>
        public async Task<AgencyDTO> GetAgencyDetailsById(long agencyId)
        {
            try
            {
                AgencyDTO agencyDTO = new AgencyDTO();
                Agency agency = await this.GetRowAsync(x => x.AgencyID == agencyId);
                this.mapper.Map<Agency, AgencyDTO>(agency, agencyDTO);

                return agencyDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetAllAgencyLookup.
        /// </summary>
        /// <returns></returns>
        public List<AgencyLookupDTO> GetAllAgencyLookup()
        {
            try
            {
                var response = (from a in this._dbContext.Agencies
                                where !a.IsRemoved
                                select new AgencyLookupDTO
                                {
                                    AgencyID = a.AgencyID,
                                    AgencyIndex = a.AgencyIndex,
                                    Name = a.Name,
                                    Abbrev = a.Abbrev
                                }).OrderBy(x => x.Name).ToList();
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetAgencyLookupWithID.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>AgencyLookupDTO List.</returns>
        public List<AgencyLookupDTO> GetAgencyLookupWithID(long agencyID)
        {
            try
            {
                List<AgencyLookupDTO> agencyDTO = new List<AgencyLookupDTO>();
                var query = string.Empty;
                query = @"Select AgencyID, AgencyIndex, [Name], Abbrev from Agency where IsRemoved = 0 and AgencyID =" + agencyID;

                var data = ExecuteSqlQuery(query, x => new AgencyLookupDTO
                {
                    AgencyID = (long)x[0],
                    AgencyIndex = (Guid)x[1],
                    Name = x[2] == DBNull.Value ? null : (string)x[2],
                    Abbrev = x[3] == DBNull.Value ? null : (string)x[3]
                });
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetAllAgencyForSharing.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>AgencyLookupDTO List.</returns>
        public List<AgencyLookupDTO> GetAllAgencyForSharing()
        {
            try
            {
                List<AgencyLookupDTO> agencyDTO = new List<AgencyLookupDTO>();
                var query = string.Empty;
                query = @"Select AgencyID, AgencyIndex, [Name] from Agency where IsRemoved = 0 Order By  [Name]";

                var data = ExecuteSqlQuery(query, x => new AgencyLookupDTO
                {
                    AgencyID = (long)x[0],
                    AgencyIndex = (Guid)x[1],
                    Name = x[2] == DBNull.Value ? null : (string)x[2]
                });
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetAgencyListForOrgAdmin.
        /// </summary>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <param name="agencyID">agencyID.</param>
        /// <returns></returns>
        public List<AgencyDTO> GetAgencyListForOrgAdmin(int pageNumber, int pageSize, long agencyID)
        {
            try
            {
                List<AgencyDTO> agencyDTO = new List<AgencyDTO>();
                var query = string.Empty;
                query = @"SELECT T1.Agency as Name, T1.ContactFirstName, T1.ContactLastName, T1.Phone1, T1.Phone2, T1.Email, NumberOfAddresses, NumberOfCollaboration, T1.AgencyID, T1.AgencyIndex, T1.Abbrev FROM ( SELECT A.AgencyID,A.AgencyIndex,A.Name AS Agency,A.ContactFirstName,A.ContactLastName,A.Phone1,A.Phone2,A.Email,COUNT(AA.AgencyID) AS NumberOfAddresses,A.Abbrev FROM Agency A LEFT JOIN AgencyAddress AA ON AA.agencyid = A.agencyid Where A.IsRemoved=0 and A.AgencyID = " + agencyID + " GROUP BY A.Name, A.AgencyIndex, A.agencyid, A.ContactFirstName, A.ContactLastName, A.Phone1, A.Phone2, A.Email,A.Abbrev ) T1 JOIN ( SELECT A.AgencyID,A.AgencyIndex,A.Name AS Agency,A.ContactFirstName,A.ContactLastName,A.Phone1,A.Phone2,A.Email,COUNT(C.CollaborationID) AS NumberOfCollaboration FROM Agency A LEFT JOIN Collaboration C ON C.AgencyID = A.AgencyID Where A.IsRemoved=0 and A.AgencyID = " + agencyID + " GROUP BY A.Name, A.agencyid,A.AgencyIndex, A.ContactFirstName, A.ContactLastName, A.Phone1, A.Phone2, A.Email ) T2 ON T1.AgencyID = T2.AgencyID ORDER BY T1.Agency";
                query += @" OFFSET " + ((pageNumber - 1) * pageSize) + "ROWS FETCH NEXT " + pageSize + " ROWS ONLY";

                var data = ExecuteSqlQuery(query, x => new AgencyDTO
                {
                    Name = x[0] == DBNull.Value ? null : (string)x[0],
                    ContactFirstName = x[1] == DBNull.Value ? null : (string)x[1],
                    ContactLastName = x[2] == DBNull.Value ? null : (string)x[2],
                    Phone1 = x[3] == DBNull.Value ? null : (string)x[3],
                    Phone2 = x[4] == DBNull.Value ? null : (string)x[4],
                    Email = x[5] == DBNull.Value ? null : (string)x[5],
                    NumberOfAddresses = x[6] == DBNull.Value ? 0 : (int)x[6],
                    NumberOfCollaboration = x[7] == DBNull.Value ? 0 : (int)x[7],
                    AgencyID = (long)x[8],
                    AgencyIndex = (Guid)x[9],
                    Abbrev = x[10] == DBNull.Value ? null : (string)x[10]
                });
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetAgencyListForOrgAdminCount.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns></returns>
        public int GetAgencyListForOrgAdminCount(long agencyID)
        {
            try
            {
                List<AgencyDTO> agencyDTO = new List<AgencyDTO>();
                var query = string.Empty;
                query = @"SELECT count(*) FROM Agency A LEFT JOIN AgencyAddress AA ON AA.agencyid = A.agencyid Where A.IsRemoved=0 and A.AgencyID =" + agencyID;

                var data = ExecuteSqlQuery(query, x => new AgencyDTO
                {
                    TotalCount = (int)x[0]
                });

                if (data != null && data.Count > 0)
                {
                    return data[0].TotalCount;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        
    }
}
