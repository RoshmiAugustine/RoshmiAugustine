// -----------------------------------------------------------------------
// <copyright file="ReportingUnitRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using AutoMapper;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Infrastructure.Data.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{
    public class ReportingUnitRepository : BaseRepository<ReportingUnit>, IReportingUnitRepository
    {
        private readonly IMapper mapper;
        private readonly OpeekaDBContext _dbContext;

        public ReportingUnitRepository(OpeekaDBContext dbContext, IMapper mapper)
            : base(dbContext)
        {
            this.mapper = mapper;
            this._dbContext = dbContext;
        }

        public ReportingUnitDTO AddReportingUnit(ReportingUnitDTO reportingUnitDTO)
        {
            try
            {
                ReportingUnit reportingUnit = new ReportingUnit();
                this.mapper.Map<ReportingUnitDTO, ReportingUnit>(reportingUnitDTO, reportingUnit);
                var result = this.AddAsync(reportingUnit).Result;
                this.mapper.Map<ReportingUnit, ReportingUnitDTO>(result, reportingUnitDTO);
                return reportingUnitDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetPartnerAgencyList.
        /// </summary>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <param name="reportingUnitID">reportingUnitID.</param>
        /// <returns>PartnerAgency List.</returns>
        public List<PartnerAgencyDataDTO> GetPartnerAgencyList(int reportingUnitID, int pageNumber, int pageSize)
        {
            try
            {
                List<PartnerAgencyDataDTO> partnerAgencyDataDTO = new List<PartnerAgencyDataDTO>();
                var query = string.Empty;

                    query = @"select Ash.AgencySharingID, Ash.ReportingUnitID, A.[Name] as Agency, Ap.AgencySharingPolicyID, Ash.StartDate, Ash.EndDate, Ash.HistoricalView,
                               Ap.[Name] as AccessName, Ap.AgencySharingPolicyID, Ash.AgencyID , Ash.AgencySharingIndex,Ash.IsSharing
                               from AgencySharing Ash
                               left join Agency A on A.AgencyID = Ash.AgencyID
                               left join AgencySharingPolicy  Ap on Ap.AgencySharingPolicyID = Ash.AgencySharingPolicyID
                            where Ash.ReportingUnitID = " + reportingUnitID + " Order by A.[Name]";
                query += @" OFFSET " + ((pageNumber - 1) * pageSize) + "ROWS FETCH NEXT " + pageSize + " ROWS ONLY";

                partnerAgencyDataDTO = ExecuteSqlQuery(query, x => new PartnerAgencyDataDTO
                {
                    AgencySharingID = x[0] == DBNull.Value ? 0 : (int)x[0],
                    ReportingUnitID = x[1] == DBNull.Value ? 0 : (int)x[1],
                    Agency = x[2] == DBNull.Value ? null : (string)x[2],
                    AgencySharingPolicyID = x[3] == DBNull.Value ? 0 : (int)x[3],
                    StartDate = x[4] == DBNull.Value ? null : (DateTime?)x[4],
                    EndDate = x[5] == DBNull.Value ? null : (DateTime?)x[5],
                    HistoricalView = x[6] == DBNull.Value ? false : (bool)x[6],
                    AccessName = x[7] == DBNull.Value ? null : (string)x[7],
                    SharingPolicyID = x[8] == DBNull.Value ? 0 : (int)x[8],
                    AgencyID = x[9] == DBNull.Value ? 0 : (long)x[9],
                    AgencySharingIndex = x[10] == DBNull.Value ? new Guid() : (Guid)x[10],
                    IsSharing = x["IsSharing"] == DBNull.Value ? false : (bool)x["IsSharing"],
                });
                return partnerAgencyDataDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetReportingUnitList.
        /// </summary>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>ReportingUnit List.</returns>
        public List<ReportingUnitDataDTO> GetReportingUnitList(long agencyID, int pageNumber, int pageSize)
        {
            try
            {
                List<ReportingUnitDataDTO> reportingUnitDataDTO = new List<ReportingUnitDataDTO>();
                var query = string.Empty;

                query = @"SELECT R.ReportingUnitID, R.ReportingUnitIndex, R.[Name], R.Abbrev, R.StartDate, R.EndDate, A.[Name] as Agency ,A.AgencyID,
                        R.IsSharing,R.Description
                        FROM ReportingUnit R 
                        LEFT JOIN AgencySharing ASH ON ASH.ReportingUnitID = R.ReportingUnitID AND ASH.AgencyID = " + agencyID + " " +
                        @"LEFT JOIN Agency A ON A.AgencyID = R.ParentAgencyID  
                        WHERE ASH.AgencyID = " + agencyID + " AND R.IsRemoved = 0 Order by [Name]";
                query += @" OFFSET " + ((pageNumber - 1) * pageSize) + "ROWS FETCH NEXT " + pageSize + " ROWS ONLY";

                reportingUnitDataDTO = ExecuteSqlQuery(query, x => new ReportingUnitDataDTO
                {
                    ReportingUnitID = x[0] == DBNull.Value ? 0 : (int)x[0],
                    ReportingUnitIndex = x[1] == DBNull.Value ? Guid.Empty : (Guid)x[1],
                    Name = x[2] == DBNull.Value ? null : (string)x[2],
                    Abbrev = x[3] == DBNull.Value ? null : (string)x[3],
                    StartDate = x[4] == DBNull.Value ? DateTime.MinValue : (DateTime)x[4],
                    EndDate = x[5] == DBNull.Value ? null : (DateTime?)x[5],
                    Agency = x[6] == DBNull.Value ? null : (string)x[6],
                    AgencyID = x[7] == DBNull.Value ? 0 : (long)x[7],
                    IsSharing = x["IsSharing"] == DBNull.Value ? false : (bool)x["IsSharing"],
                    Description = x["Description"] == DBNull.Value ? null : (string)x["Description"],
                });
                return reportingUnitDataDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetReportingUnitCount.
        /// </summary>
        /// <returns>Collaboration Count.</returns>
        public int GetReportingUnitCount(long agencyID)
        {
            try
            {
                List<ReportingUnitDataDTO> reportingUnitDataDTO = new List<ReportingUnitDataDTO>();
                var query = string.Empty;
                query = @"SELECT Count(R.ReportingUnitID)
                        FROM ReportingUnit R 
                        LEFT JOIN AgencySharing ASH ON ASH.ReportingUnitID = R.ReportingUnitID AND ASH.AgencyID = " + agencyID + @"
                        LEFT JOIN Agency A ON A.AgencyID = R.ParentAgencyID  
                        WHERE ASH.AgencyID = " + agencyID + " AND R.IsRemoved = 0 ";

                reportingUnitDataDTO = ExecuteSqlQuery(query, x => new ReportingUnitDataDTO
                {
                    totalCount = x[0] == DBNull.Value ? 0 : (int)x[0],
                });

                return reportingUnitDataDTO.FirstOrDefault().totalCount;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetRUCollaborationList.
        /// </summary>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <param name="agencyID">collaborationID.</param>
        /// <param name="reportingUnitID">reportingUnitID.</param>
        /// <returns>Collaboration List.</returns>
        public List<RUCollaborationDataDTO> GetRUCollaborationList(long agencyID, int reportingUnitID, int pageNumber, int pageSize)
        {
            try
            {
                List<RUCollaborationDataDTO> collaborationDataDTO = new List<RUCollaborationDataDTO>();
                var query = string.Empty;

                query = @"select Csh.CollaborationSharingID, Csh.CollaborationSharingIndex, Csh.CollaborationSharingPolicyID, Csh.HistoricalView, Csh.StartDate, 
                             Csh.EndDate, Csh.CollaborationID, C.[Name] as Collaboration, CP.[Name] AccessName , Csh.AgencyID,
							 A.[Name] as Agency , CP.CollaborationSharingPolicyID,Csh.IsSharing 
                             from CollaborationSharing Csh
                             left join Agency A on A.AgencyID = Csh.AgencyID 
                             left join Collaboration C on C.CollaborationID = Csh.CollaborationID
                             left join CollaborationSharingPolicy  CP on CP.CollaborationSharingPolicyID = Csh.CollaborationSharingPolicyID
                             where Csh.ReportingUnitID = " + reportingUnitID + " and Csh.AgencyID = " + agencyID + " Order by C.[Name]";
                query += @" OFFSET " + ((pageNumber - 1) * pageSize) + "ROWS FETCH NEXT " + pageSize + " ROWS ONLY";

                collaborationDataDTO = ExecuteSqlQuery(query, x => new RUCollaborationDataDTO
                {
                    CollaborationSharingID = x["CollaborationSharingID"] == DBNull.Value ? 0 : (int)x["CollaborationSharingID"],
                    CollaborationSharingIndex = x["CollaborationSharingIndex"] == DBNull.Value ? Guid.Empty : (Guid)x["CollaborationSharingIndex"],
                    CollaborationSharingPolicyID = x["CollaborationSharingPolicyID"] == DBNull.Value ? 0 : (int)x["CollaborationSharingPolicyID"],
                    HistoricalView = x["HistoricalView"] == DBNull.Value ? false : (bool)x["HistoricalView"],
                    StartDate = x["StartDate"] == DBNull.Value ? null : (DateTime?)x["StartDate"],
                    EndDate = x["EndDate"] == DBNull.Value ?null : (DateTime?)x["EndDate"],
                    CollaborationID = x["CollaborationID"] == DBNull.Value ? 0 : (int)x["CollaborationID"],
                    Collaboration = x["Collaboration"] == DBNull.Value ? null : (string)x["Collaboration"],
                    AccessName = x["AccessName"] == DBNull.Value ? null : (string)x["AccessName"],
                    AgencyID = x["AgencyID"] == DBNull.Value ? 0 : (long)x["AgencyID"],
                    Agency = x["Agency"] == DBNull.Value ? null : (string)x["Agency"],
                    SharingPolicy = x["CollaborationSharingPolicyID"] == DBNull.Value ? 0 : (int)x["CollaborationSharingPolicyID"],
                    IsSharing = x["IsSharing"] == DBNull.Value ? false : (bool)x["IsSharing"]
                });
                return collaborationDataDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// To update ReportingUnit details.
        /// </summary>
        /// <param name="reportingUnitDTO">id.</param>
        /// <returns>List of summaries.</returns>
        public ReportingUnit UpdateReportingUnit(ReportingUnit reportingUnit)
        {
            try
            {
                var result = this.UpdateAsync(reportingUnit).Result;
                return reportingUnit;

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// To get details ReportingUnit.
        /// </summary>
        /// <param agencyPersonCollaborationDTO="agencyPersonCollaborationDTO">id.</param>
        /// <returns>.AgencyPersonCollaborationDTO</returns>
        public async Task<ReportingUnit> GetReportingUnit(Guid id)
        {
            try
            {
                ReportingUnit reportingUnit = await this.GetRowAsync(x => x.ReportingUnitIndex == id);

                return reportingUnit;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
