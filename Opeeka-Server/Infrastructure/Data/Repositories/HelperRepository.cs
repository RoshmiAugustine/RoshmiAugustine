// -----------------------------------------------------------------------
// <copyright file="HelperRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.DTO.Input;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Infrastructure.Data.Repositories.Base;
using Opeeka.PICS.Infrastructure.Enums;
using Opeeka.PICS.Domain.Interfaces;
using Opeeka.PICS.Domain.DTO.ExternalAPI;

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{
    public class HelperRepository : BaseRepository<Helper>, IHelperRepository
    {
        private readonly IMapper mapper;
        private readonly ILogger<HelperRepository> logger;
        private readonly OpeekaDBContext _dbContext;
        private readonly ICache _cache;
        private readonly IAssessmentStatusRepository _assessmentStatusRepository;
        private readonly ISystemRoleRepository _systemRoleRepository;
        /// <summary>
        /// Initializes a new instance of the <see cref="HelperRepository"/> class.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public HelperRepository(ISystemRoleRepository systemRoleRepository, ILogger<HelperRepository> logger, OpeekaDBContext dbContext, IMapper mapper, ICache cache, IAssessmentStatusRepository assessmentStatusRepository)
            : base(dbContext)
        {
            this.mapper = mapper;
            this.logger = logger;
            this._dbContext = dbContext;
            this._cache = cache;
            this._assessmentStatusRepository = assessmentStatusRepository;
            this._systemRoleRepository = systemRoleRepository;
        }

        /// <summary>
        /// Save helper details.
        /// </summary>
        /// <param name="helperdto"></param>
        /// <param name="UserID"></param>
        /// <returns>Object of Helper</returns>
        public Helper CreateHelper(HelperDTO helperdto, int UserID)
        {
            try
            {
                Helper helper = new Helper();
                this.mapper.Map<HelperDTO, Helper>(helperdto, helper);
                if (helperdto != null)
                {
                    helper.UserID = UserID;
                    helper.IsRemoved = false;
                    helper.UpdateDate = DateTime.UtcNow;
                    helper = this.AddAsync(helper).Result;
                    this._cache.DeleteAll(new List<string>() { PCISEnum.Caching.GetAllHelperLookup, PCISEnum.Caching.GetAllLeads });
                }
                return helper;
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// GetHelperDetails.
        /// </summary>
        /// <param name="helperData"></param>
        /// <returns></returns>
        public List<HelperDataDTO> GetHelperDetails(int userId, int pageNumber, int pageSize, string from, long TenentID)
        {
            try
            {
                var query = string.Empty;
                if (from == PCISEnum.Roles.SuperAdmin)
                {
                    query = @"select H.HelperID as HelperID,H1.HelperID as LeadID,H.HelperIndex as HelperIndex,H1.HelperIndex as LeadIndex,
                                H.FirstName+' '+ISNULL(H.MiddleName,'')+' ' +H.LastName as HelperName,
                                H1.FirstName+' '+ISNULL(H1.MiddleName,'')+' ' +H1.LastName from Helper H
                                left join Helper H1 on H.SupervisorHelperID=H1.HelperID 
                                where H.IsRemoved=0 AND H.AgencyID= " + TenentID + " ORDER BY HelperName ASC";
                }
                else if (from == PCISEnum.Roles.OrgAdmin)
                {
                    query = @"select H.HelperID as HelperID,H1.HelperID as LeadID,H.HelperIndex as HelperIndex,H1.HelperIndex as LeadIndex,
                                H.FirstName+' '+ISNULL(H.MiddleName,'')+' ' +H.LastName as HelperName,
                                H1.FirstName+' '+ISNULL(H1.MiddleName,'')+' ' +H1.LastName from Helper H
                            left join Helper H1 on H.SupervisorHelperID=H1.HelperID
                            where H.IsRemoved=0 AND H.AgencyID= " + TenentID + " ORDER BY HelperName ASC";
                }
                else
                {
                    query = @"select H.HelperID as HelperID,H1.HelperID as LeadID,H.HelperIndex as HelperIndex,H1.HelperIndex as LeadIndex,
                                H.FirstName+' '+ISNULL(H.MiddleName,'')+' ' +H.LastName as HelperName,
                                H1.FirstName+' '+ISNULL(H1.MiddleName,'')+' ' +H1.LastName from Helper H
                            left  Helper H1 on H.SupervisorHelperID=H1.HelperID
                            where H.IsRemoved=0 AND H.UserID= " + userId + " ORDER BY HelperName ASC";
                }

                query += @" OFFSET " + ((pageNumber - 1) * pageSize) + "ROWS FETCH NEXT " + pageSize + " ROWS ONLY";

                var data = ExecuteSqlQuery(query, x => new HelperDataDTO
                {
                    HelperID = x[0] == DBNull.Value ? 0 : (int)x[0],
                    LeadHelerID = x[1] == DBNull.Value ? 0 : (int)x[1],
                    HelperIndex = x[2] == DBNull.Value ? null : (Guid?)x[2],
                    LeadHelperIndex = x[3] == DBNull.Value ? null : (Guid?)x[3],
                    HelperName = x[4] == DBNull.Value ? null : (string)x[4],
                    LeadName = x[5] == DBNull.Value ? null : (string)x[5]
                });
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetHelperDetailsCount.
        /// </summary>
        /// <param name="helperData"></param>
        /// <param name="from"></param>
        /// <param name="TenentID"></param>
        /// <returns></returns>
        public int GetHelperDetailsCount(int userId, string from, long TenentID)
        {
            try
            {
                var query = string.Empty;
                if (from == PCISEnum.Roles.SuperAdmin)
                {
                    query = @"select Count(*)  from Helper H
                            left join Helper H1 on H.SupervisorHelperID=H1.HelperID
                            where H.IsRemoved=0 AND  H.AgencyID= " + TenentID;
                }
                else if (from == PCISEnum.Roles.OrgAdmin)
                {
                    query = @"select Count(*)  from Helper H
                            left join Helper H1 on H.SupervisorHelperID=H1.HelperID
                            where H.IsRemoved=0 AND  H.AgencyID= " + TenentID;
                }
                else
                {
                    query = @"select Count(*)  from Helper H
                            left join Helper H1 on H.SupervisorHelperID=H1.HelperID
                            where H.IsRemoved=0 AND  H.UserID= " + userId;
                }

                var data = ExecuteSqlQuery(query, x => new HelperDataDTO
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

        /// <summary>
        /// Get Helper details.
        /// </summary>
        /// <param name="helperIndex"></param>
        /// <returns></returns>
        public HelperInfoDTO GetHelperInfo(Guid helperIndex, long agencyId = 0)
        {
            HelperInfoDTO helperDetails = new HelperInfoDTO();
            try
            {
                string agencyCondition = string.Empty;
                if (agencyId > 0)
                    agencyCondition = " H.AgencyID=" + agencyId + " AND ";

                var query = string.Empty;
                query = @"select H.HelperID, H.HelperIndex, H.FirstName, H.LastName, H.MiddleName, HT.HelperTitleID, HT.Name as HelperTitle, H.Email, H.Phone, H.Phone2,
                            H1.FirstName+' '+IsNull(H1.MiddleName,'')+' ' +H1.LastName as Supervisor, A.[Name] as Agency, AD.Address1 as [Address], 
                            AD.Address2 as Address2, AD.City, AD.Zip, C.[Name] as State, S.[Name] as [Role], H1.HelperID as SupervisorID, 
                            H1.HelperIndex as SupervisorIndex, C.CountryStateId as CountryStateId , S.SystemRoleID,A.AgencyID,H.ReviewerID,H.StartDate,H.EndDate,H.HelperExternalID, H.UserId,H.IsEmailReminderAlerts, Con.CountryID as CountryId,Con.Name AS Country, S.SystemRoleId as RoleId   from helper H  
                            left join Helper H1 on H1.HelperId = H.SupervisorHelperId
                            left join Agency A on A.AgencyId = H.AgencyId
                            left join HelperAddress HD on HD.HelperId = H.HelperId
                            left join Address AD on AD.AddressId = HD.AddressId
                            left join info.CountryState C on AD.CountryStateId = C.CountryStateId 
                            left join UserRole U on U.UserId = H.UserId
                            left join info.SystemRole S on S.SystemRoleId = U.SystemRoleId
                            left join info.HelperTitle HT on HT.HelperTitleID = H.HelperTitleID
                            left join [info].Country Con ON Con.CountryID = AD.CountryId
                            where " + agencyCondition + " H.HelperIndex= '" + helperIndex + "'";
                helperDetails = ExecuteSqlQuery(query, x => new HelperInfoDTO
                {
                    HelperID = (int)x[0],
                    HelperIndex = (Guid)x[1],
                    FirstName = x[2] == DBNull.Value ? null : (string)x[2],
                    LastName = x[3] == DBNull.Value ? null : (string)x[3],
                    MiddleName = x[4] == DBNull.Value ? null : (string)x[4],
                    HelperTitleID = x[5] == DBNull.Value ? null : (int?)x[5],
                    HelperTitle = x[6] == DBNull.Value ? null : (string)x[6],
                    Email = x[7] == DBNull.Value ? null : (string)x[7],
                    Phone1 = x[8] == DBNull.Value ? null : (string)x[8],
                    Phone2 = x[9] == DBNull.Value ? null : (string)x[9],
                    Supervisor = x[10] == DBNull.Value ? null : (string)x[10],
                    Agency = x[11] == DBNull.Value ? null : (string)x[11],
                    Address = x[12] == DBNull.Value ? null : (string)x[12],
                    Address2 = x[13] == DBNull.Value ? null : (string)x[13],
                    City = x[14] == DBNull.Value ? null : (string)x[14],
                    Zip = x[15] == DBNull.Value ? null : (string)x[15],
                    State = x[16] == DBNull.Value ? null : (string)x[16],
                    Role = x[17] == DBNull.Value ? null : (string)x[17],
                    SupervisorHelperID = x[18] == DBNull.Value ? null : (int?)x[18],
                    SupervisorIndex = x[19] == DBNull.Value ? null : (Guid?)x[19],
                    CountryStateId = x[20] == DBNull.Value ? null : (int?)x[20],
                    SystemRoleID = x[21] == DBNull.Value ? null : (int?)x[21],
                    AgencyId = x[22] == DBNull.Value ? 0 : (long)x[22],
                    ReviewerID = x[23] == DBNull.Value ? null : (int?)x[23],
                    StartDate = x["StartDate"] == DBNull.Value ? DateTime.MinValue : (DateTime)x["StartDate"],
                    EndDate = x["EndDate"] == DBNull.Value ? null : (DateTime?)x["EndDate"],
                    HelperExternalID = x["HelperExternalID"] == DBNull.Value ? null : (string)x["HelperExternalID"],
                    UserId = x["UserId"] == DBNull.Value ? 0 : (int)x["UserId"],
                    IsEmailReminderAlerts = x["IsEmailReminderAlerts"] == DBNull.Value ? false : (bool)x["IsEmailReminderAlerts"],
                    CountryID = x["CountryId"] == DBNull.Value ? null : (int?)x["CountryId"],
                    Country = x["Country"] == DBNull.Value ? null : (string)x["Country"],
                    RoleId = x["RoleId"] == DBNull.Value ? 0 : (int)x["RoleId"]
                }).FirstOrDefault();

                return helperDetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To get helper details.
        /// </summary>
        /// <param name="HelperIndex"></param>
        /// <returns>HelperDTO</returns>
        public async Task<Helper> GetHelperByIndexAsync(Guid? HelperIndex)
        {
            try
            {
                Helper helper = await this.GetRowAsync(x => x.HelperIndex == HelperIndex);
                return helper;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// To update helper details.
        /// </summary>
        /// <param name="helperDTO"></param>
        /// <returns></returns>
        public HelperDTO UpdateHelper(Helper helper)
        {
            try
            {
                HelperDTO helperDTO = new HelperDTO();
                var result = this.UpdateAsync(helper).Result;
                this.mapper.Map<Helper, HelperDTO>(result, helperDTO);
                this._cache.DeleteAll(new List<string>() { PCISEnum.Caching.GetAllHelperLookup, PCISEnum.Caching.GetAllLeads });
                return helperDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get the count of helpers having a specific title
        /// </summary>
        /// <param name="helperTitleID"></param>
        /// <returns>int</returns>
        public int GetHelperCountByHelperTitle(int helperTitleID)
        {
            int count = (
                            from row
                            in this._dbContext.Helper
                            where
                                row.HelperTitleID == helperTitleID && !row.IsRemoved
                            select
                                row
                        ).Count();

            return count;
        }

        /// <summary>
        /// GetAllManager
        /// </summary>
        /// <returns>Helper</returns>
        public List<Helper> GetAllManager(long agencyID, string OrgAdminRO, string OrgAdminRW, string Supervisor, bool activeHelpers = true)
        {
            try
            {
                var activeHelperCondition = activeHelpers ? "AND ISNULL(EndDate, CAST(GETDATE() AS DATE))>=CAST(GETDATE() AS DATE)" : "";
                var query = string.Empty;
                query = @$"select H.HelperID,H.HelperIndex,H.UserID,H.FirstName,H.MiddleName,H.LastName,H.Email,H.Phone,H.UpdateDate,H.UpdateUserID,H.AgencyId
                                ,H.HelperTitleID,h.Phone2,h.SupervisorHelperID
                            FROM Helper H
                            JOIN UserRole U on H.UserID = U.UserID
                            JOIN info.SystemRole S on S.SystemRoleID = U.SystemRoleId AND S.IsRemoved=0
                            WHERE H.AgencyId = { agencyID } AND S.Name IN ('{OrgAdminRO}','{OrgAdminRW}', '{Supervisor}') AND H.IsRemoved=0 {activeHelperCondition} ORDER BY FirstName ASC";

                var data = ExecuteSqlQuery(query, x => new Helper
                {
                    HelperID = (int)x[0],
                    HelperIndex = (Guid)x[1],
                    UserID = (int)x[2],
                    FirstName = (string)x[3],
                    MiddleName = x[4] == DBNull.Value ? null : (string)x[4],
                    LastName = (string)x[5],
                    Email = x[6] == DBNull.Value ? null : (string)x[6],
                    Phone = x[7] == DBNull.Value ? null : (string)x[7],
                    UpdateDate = (DateTime)x[8],
                    UpdateUserID = (int)x[9],
                    AgencyID = (long)x[10],
                    HelperTitleID = x[11] == DBNull.Value ? null : (int?)x[11],
                    Phone2 = x[12] == DBNull.Value ? null : (string)x[12],
                    SupervisorHelperID = x[13] == DBNull.Value ? null : (int?)x[13],
                });
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetUserDetails.
        /// </summary>
        /// <param name="userID">userID.</param>
        /// <returns>UserDetailsDTO.</returns>
        public UserDetailsDTO GetUserDetails(long userID)
        {
            try
            {
                var query = string.Empty;
                query = @"select case when A.Name is null then U.Name else A.Name end  as Name,
		                         case when  A.Title is null then NULL else A.Title end as Title,A.HelperID,   
                                 case when A.Email is null then U.UserNAme else A.Email end  as Email,
                                 case when A.HelperExternalID is null then NULL else A.HelperExternalID end  as HelperExternalID
                          from   [dbo].[User] U 
                        left join (select  H.UserID,H.FirstName+' '+H.LastName as Name,HT.Name Title, H.HelperID, H.Email,H.HelperExternalID
                                   from [dbo].[Helper] H  
                                   Left JOIN [info].[HelperTitle] HT ON HT.HelperTitleID = H.HelperTitleID
                                   ) A ON U.UserID=A.UserID 
                        where U.UserID = " + userID;

                var data = ExecuteSqlQuery(query, x => new UserDetailsDTO
                {
                    Name = x["Name"] == DBNull.Value ? null : (string)x["Name"],
                    Title = x["Title"] == DBNull.Value ? null : (string)x["Title"],
                    HelperID = x["HelperID"] == DBNull.Value ? 0 : (int)x["HelperID"],
                    HelperEmail = x["Email"] == DBNull.Value ? string.Empty : (string)x["Email"],
                    HelperExternalID = x["HelperExternalID"] == DBNull.Value ? string.Empty : (string)x["HelperExternalID"],
                }).FirstOrDefault();
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetHelperDetailsByHelperID.
        /// </summary>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <param name="role">role.</param>
        /// <param name="agencyID">agencyID.</param>
        /// <param name="helperID">helperID.</param>
        /// <returns>HelperDataDTO.</returns>
        public Tuple<List<HelperDataDTO>, int> GetHelperDetailsByHelperID(HelperSearchDTO helperSearchDTO, DynamicQueryBuilderDTO queryBuilderDTO)
        {
            try
            {
                var query = string.Empty;
                string activeFilter = string.Empty;
                List<AssessmentStatus> assessmentStatus = this._assessmentStatusRepository.GetAllAssessmentStatus();
                int assessmentApproveStatusID = assessmentStatus.Where(x => x.Name == PCISEnum.AssessmentStatus.Approved).ToList()[0].AssessmentStatusID;
                int assessmentRejectStatusID = assessmentStatus.Where(x => x.Name == PCISEnum.AssessmentStatus.Returned).ToList()[0].AssessmentStatusID;
                int assessmentSubmitStatusID = assessmentStatus.Where(x => x.Name == PCISEnum.AssessmentStatus.Submitted).ToList()[0].AssessmentStatusID;
                if (helperSearchDTO.activeFilter == PCISEnum.ActiveFilter.Active)
                {
                    activeFilter = @$" AND CAST(h.StartDate AS DATE)<=CAST(GETDATE() AS DATE) 
                        AND ISNULL(h.EndDate, CAST(GETDATE() AS DATE))>= CAST(GETDATE() AS DATE)";
                }
                else if (helperSearchDTO.activeFilter == PCISEnum.ActiveFilter.Inactive)
                {
                    activeFilter = @$" AND (CAST(h.StartDate AS DATE)>CAST(GETDATE() AS DATE) 
                        OR ISNULL(h.EndDate, CAST(GETDATE() AS DATE)) < CAST(GETDATE() AS DATE))";
                }
                string helperColbQueryCondition = string.Empty;
                var helpermetricsQuery = string.Empty;
                var helperAssessmentQueryQuery = string.Empty;
                if (helperSearchDTO.role != PCISEnum.Roles.SuperAdmin)
                {
                    var personIdList = this.GetHelperPersonInCollaborationDetails(helperSearchDTO.userID, helperSearchDTO.agencyID);
                    if (personIdList.Count > 0)
                    {
                        string personIDs = string.Join(",", personIdList.ToArray());

                        helperColbQueryCondition = $@"AND {{0}}.PersonID NOT IN ({personIDs})";
                        var assessmentsIDsColbratn = this.GetHelperAllAssessmentsInCollaboration(personIDs, helperSearchDTO.agencyID, helperSearchDTO.userID);
                        string assessmentsIDs = assessmentsIDsColbratn.Count > 0 ? string.Join(",", assessmentsIDsColbratn.ToArray()) : "0";
                        helperAssessmentQueryQuery = $@"AND(PH.PersonID NOT IN({ personIDs}) OR (PH.PersonID IN({ personIDs}) AND A.AssessmentID IN({ assessmentsIDs})))";

                        List<string> assessmentsInColbratn = this.GetHelperAllMetricsInCollaboration(assessmentsIDs);
                        string assessmentMetricsIDS = assessmentsInColbratn.Count > 0 ? string.Join(",", assessmentsInColbratn.ToArray()) : "0";
                        helpermetricsQuery = @$"UNION ALL
								SELECT ph.PersonID,ph.HelperID,
							          [NeedsIdentified]
		                            , [NeedsAddressed]
		                            , [StrengthsIdentified]
		                            , [StrengthsBuilt]
                                    , [NeedsEver]
                                    , [NeedsAddressing]
                                    , [StrengthsEver]
		                            , [StrengthsBuilding],1 As PersonMetrics
									FROM #SelectedPersonswithDays ph
									LEFT JOIN [dbo].[PersonAssessmentMetrics] pm ON ph.PersonID=pm.[PersonID] WHERE 1 = 1
									AND pm.PersonAssessmentMetricsID IN ({assessmentMetricsIDS})";

                    }
                }
                if (helperSearchDTO.role == PCISEnum.Roles.SuperAdmin)
                {
                    query = @$";WITH HelperList AS
                            (
	                            SELECT
		                            H.HelperID HelperID,
	                                H.HelperIndex HelperIndex,
	                                H.FirstName +COALESCE(CASE H.MiddleName WHEN '' THEN '' ELSE ' '+H.MiddleName END, ' '+H.MiddleName, '')+ ' ' +H.LastName [HelperName]
	                            FROM
	                            Helper h
	                            WHERE H.IsRemoved=0 AND h.AgencyID={helperSearchDTO.agencyID.ToString()} { activeFilter}
                            )
                            ,PeopleHelped AS
                            (
	                            SELECT
		                            h.HelperID,
		                            p.PersonID
	                            FROM
	                            HelperList h
	                            JOIN dbo.PersonHelper  ph ON h.HelperID=ph.HelperID AND ph.IsRemoved=0
                                AND ph.StartDate<=CAST(GETDATE() AS DATE) AND ISNULL(ph.EndDate, CAST(GETDATE() AS DATE))>=CAST(GETDATE() AS DATE)
	                            JOIN dbo.Person  p ON ph.PersonID=p.PersonID AND p.IsRemoved=0 AND p.IsActive=1 AND P.AgencyId = {helperSearchDTO.agencyID.ToString()}
                            )
							,SelectedPersonswithDays AS(
							select ROW_NUMBER() OVER (PARTITION BY p.HelperID,p.PersonID ORDER BY pc.PersonCollaborationID) rownum,
                            p.HelperID,p.PersonID,		pc.EnrollDate	,pc.EndDate	,	
						    CASE WHEN ISNULL(pc.EndDate,GETDATE())>GETDATE()
                                               THEN DATEDIFF(DAY, ISNULL(pc.EnrollDate, GETDATE()),GETDATE())+1
                                    ELSE DATEDIFF(DAY, ISNULL(pc.EnrollDate, GETDATE()),ISNULL(pc.EndDate, GETDATE()))+1 END [Days]
							 FROM  PeopleHelped P 
									LEFT JOIN PersonCollaboration pc ON pc.PersonID=p.PersonID AND pc.IsPrimary=1 AND pc.IsRemoved=0
									AND IsCurrent = 1 
							),
                            PersonAssessed AS 
                            (
	                             SELECT 
		                          PH.helperID , COUNT(A.AssessmentID) as AssessmentsCount--,PH.PersonID 
	                            FROM 
	                            SelectedPersonswithDays PH
								LEFT JOIN PersonQuestionnaire PQ ON Pq.PersonID = PH.PersonID and pq.IsRemoved = 0 AND pq.IsActive=1
								JOIN Assessment a ON a.PersonQuestionnaireID=pq.PersonQuestionnaireID AND a.IsRemoved=0
								where a.AssessmentStatusID in ({assessmentRejectStatusID},{assessmentSubmitStatusID},{assessmentApproveStatusID})
	                           GROUP BY PH.helperID
                            )
                            ,CTE AS
                            (
	                            SELECT
		                            ph.HelperID,COUNT(Distinct ph.PersonID) Helping,AVG(ph.Days) [Days]
		                            ,SUM([NeedsIdentified]) [TotalNeedsIdentified]
		                            ,SUM([NeedsAddressed]) [TotalNeedsAddressed]
		                            ,SUM([StrengthsIdentified]) [TotalStrengthsIdentified]
		                            ,SUM([StrengthsBuilt]) [TotalStrengthsBuilt]
                                    ,SUM([NeedsEver]) [TotalNeedsEver]
                                    ,SUM([NeedsAddressing]) [TotalNeedsAddressing]
                                    ,SUM([StrengthsEver]) [TotalStrengthsEver]
		                            ,SUM([StrengthsBuilding]) [TotalStrengthsBuilding]
                                    ,MAX(AssessmentsCount) [Completed] 
	                            FROM SelectedPersonswithDays ph
	                            LEFT JOIN [dbo].[PersonQuestionnaireMetrics] pm ON ph.PersonID=pm.[PersonID]
                                LEFT JOIN [PersonAssessed] pa ON pa.helperID=ph.[helperID]
	                            GROUP BY ph.HelperID,rownum HAving  rownum = 1
                            )
                            select 
	                            COUNT(H.HelperID) OVER() AS TotalCount,
	                            H.HelperID HelperID
	                            ,H.HelperIndex HelperIndex
	                            ,H.HelperName
	                            ,CTE.Helping
	                            ,CTE.Days Days
	                            ,null AS Due
	                            ,Completed
                                ,CTE.TotalNeedsIdentified
	                            ,CTE.TotalNeedsAddressed
                                ,CTE.TotalStrengthsIdentified
	                            ,CTE.TotalStrengthsBuilt
                                ,CTE.TotalNeedsEver
	                            ,CTE.TotalNeedsAddressing
                                ,CTE.TotalStrengthsEver
	                            ,CTE.TotalStrengthsBuilding
                            from HelperList H
                            LEFT JOIN CTE on CTE.HelperID=H.HelperID
                            WHERE 1=1
                            ";
                }
                if (helperSearchDTO.IsSameUser)
                {
                    query = @$"SELECT * INTO #HelperList FROM
							(
							  SELECT
								    H.HelperID HelperID,
	                                H.HelperIndex HelperIndex,
	                                H.FirstName +COALESCE(CASE H.MiddleName WHEN '' THEN '' ELSE ' '+H.MiddleName END, ' '+H.MiddleName, '')+ ' ' +H.LastName           [HelperName]
							    FROM Helper h WHERE h.AgencyID={helperSearchDTO.agencyID.ToString()} AND h.IsRemoved=0 
	                            AND h.HelperID={helperSearchDTO.helperID}   {activeFilter}
							)AS A
                            SELECT * INTO #PeopleHelped FROM
                            (
	                            SELECT
		                            h.HelperID,
		                            p.PersonID
	                            FROM
	                            #HelperList h
	                            JOIN dbo.PersonHelper  ph ON h.HelperID=ph.HelperID AND ph.IsRemoved=0
                                AND ph.StartDate<=CAST(GETDATE() AS DATE) AND ISNULL(ph.EndDate, CAST(GETDATE() AS DATE))>=CAST(GETDATE() AS DATE)
	                            JOIN dbo.Person  p ON ph.PersonID=p.PersonID AND p.IsRemoved=0 AND p.IsActive=1 AND P.AgencyId = {helperSearchDTO.agencyID}
                            )AS B
							SELECT * INTO #SelectedPersonswithDays FROM(
							select ROW_NUMBER() OVER (PARTITION BY p.HelperID,p.PersonID ORDER BY pc.PersonCollaborationID) rownum, 
							p.HelperID,p.PersonID,		pc.EnrollDate	,pc.EndDate	,	
						    CASE WHEN ISNULL(pc.EndDate,GETDATE())>GETDATE()
                                               THEN DATEDIFF(DAY, ISNULL(pc.EnrollDate, GETDATE()),GETDATE())+1
                                    ELSE DATEDIFF(DAY, ISNULL(pc.EnrollDate, GETDATE()),ISNULL(pc.EndDate, GETDATE()))+1 END [Days]
							 FROM  #PeopleHelped P 
									LEFT JOIN PersonCollaboration pc ON pc.PersonID=p.PersonID AND pc.IsPrimary=1 AND pc.IsRemoved=0
									AND IsCurrent = 1 
							)AS C
                            SELECT * INTO #PersonAssessed FROM 
                            (
	                             SELECT 
		                          PH.helperID , COUNT(A.AssessmentID) as AssessmentsCount--,PH.PersonID 
	                            FROM 
	                            #SelectedPersonswithDays PH
								LEFT JOIN PersonQuestionnaire PQ ON Pq.PersonID = PH.PersonID and pq.IsRemoved = 0 AND pq.IsActive=1
								JOIN Assessment a ON a.PersonQuestionnaireID=pq.PersonQuestionnaireID AND a.IsRemoved=0
								where a.AssessmentStatusID in ({assessmentRejectStatusID},{assessmentSubmitStatusID},{assessmentApproveStatusID})
                                {helperAssessmentQueryQuery}
	                           GROUP BY PH.helperID
                            )AS D
                            SELECT * INTO #DistinctPersonQuestionnaireMetrics FROM
								(
								SELECT PersonQuestionnaireMetricsID FROM(
								SELECT 
								ROW_NUMBER() OVER(PARTITIon BY pqm.PersonID,pqm.ItemID ORDER BY pqm.ItemID) AS RowNumber, pqm.PersonQuestionnaireMetricsID
								from PersonQuestionnaireMetrics pqm WITH (NOLOCK)
								JOIN #PeopleHelped p ON pqm.PersonID=p.PersonID
									 ) as A 
								WHERE A.RowNumber = 1
							)AS E
                            SELECT * INTO #MetricsForPersons FROM( 
								SELECT ph.PersonID,ph.HelperID,
							           [NeedsIdentified]
		                            , [NeedsAddressed]
		                            , [StrengthsIdentified]
		                            , [StrengthsBuilt]
                                    , [NeedsEver]
                                    , [NeedsAddressing]
                                    , [StrengthsEver]
		                            , [StrengthsBuilding],0 As PersonMetrics
									FROM #PeopleHelped ph
									LEFT JOIN [dbo].[PersonQuestionnaireMetrics] pm ON ph.PersonID=pm.[PersonID] 
                                    JOIN  #DistinctPersonQuestionnaireMetrics pm1 on pm1.[PersonQuestionnaireMetricsID]=pm.[PersonQuestionnaireMetricsId]
									WHERE 1= 1 {string.Format(helperColbQueryCondition, "PM")} {helpermetricsQuery}
                             )AS F 
                            SELECT * INTO #CTE FROM
                            (
	                            SELECT
		                            ph.HelperID,COUNT(Distinct ph.PersonID) Helping,AVG(ph.Days) [Days]
		                            ,SUM([NeedsIdentified]) [TotalNeedsIdentified]
		                            ,SUM([NeedsAddressed]) [TotalNeedsAddressed]
		                            ,SUM([StrengthsIdentified]) [TotalStrengthsIdentified]
		                            ,SUM([StrengthsBuilt]) [TotalStrengthsBuilt]
                                    ,SUM([NeedsEver]) [TotalNeedsEver]
                                    ,SUM([NeedsAddressing]) [TotalNeedsAddressing]
                                    ,SUM([StrengthsEver]) [TotalStrengthsEver]
		                            ,SUM([StrengthsBuilding]) [TotalStrengthsBuilding]
                                    ,MAX(AssessmentsCount) [Completed] 
	                            FROM #SelectedPersonswithDays ph
	                            LEFT JOIN #MetricsForPersons pm ON ph.PersonID=pm.[PersonID]
                                LEFT JOIN [#PersonAssessed] pa ON pa.helperID=ph.[helperID]
                                WHERE 1= 1 
	                            GROUP BY ph.HelperID,rownum HAving  rownum = 1
                            )AS G
                            select 
	                            COUNT(H.HelperID) OVER() AS TotalCount,
	                            H.HelperID HelperID
	                            ,H.HelperIndex HelperIndex
	                            ,H.HelperName
	                            ,CTE.Helping
	                            ,CTE.Days Days
	                            ,null AS Due
	                            ,Completed
                                ,CTE.TotalNeedsIdentified
	                            ,CTE.TotalNeedsAddressed
                                ,CTE.TotalStrengthsIdentified
	                            ,CTE.TotalStrengthsBuilt
                                ,CTE.TotalNeedsEver
	                            ,CTE.TotalNeedsAddressing
                                ,CTE.TotalStrengthsEver
	                            ,CTE.TotalStrengthsBuilding
                            from #HelperList H
                            LEFT JOIN #CTE CTE on CTE.HelperID=H.HelperID
                            WHERE 1=1
                            ";
                }
                else
                {
                    if (helperSearchDTO.role == PCISEnum.Roles.OrgAdminRO || helperSearchDTO.role == PCISEnum.Roles.OrgAdminRW)
                    {
                        query = @$"SELECT * INTO #HelperList FROM
                            (
	                            SELECT
		                            H.HelperID HelperID,
	                                H.HelperIndex HelperIndex,
	                                H.FirstName +COALESCE(CASE H.MiddleName WHEN '' THEN '' ELSE ' '+H.MiddleName END, ' '+H.MiddleName, '')+ ' ' +H.LastName [HelperName]
	                            FROM
	                            Helper h
	                            WHERE H.IsRemoved=0 AND h.AgencyID={helperSearchDTO.agencyID.ToString()} { activeFilter}
                            )AS A
                            SELECT * INTO #PeopleHelped FROM
                            (
	                            SELECT
		                            h.HelperID,
		                            p.PersonID
	                            FROM
	                            #HelperList h
	                            JOIN dbo.PersonHelper  ph ON h.HelperID=ph.HelperID AND ph.IsRemoved=0
                                AND ph.StartDate<=CAST(GETDATE() AS DATE) AND ISNULL(ph.EndDate, CAST(GETDATE() AS DATE))>=CAST(GETDATE() AS DATE)
	                            JOIN dbo.Person  p ON ph.PersonID=p.PersonID AND p.IsRemoved=0 AND p.IsActive=1 AND P.AgencyId = {helperSearchDTO.agencyID.ToString()}
                            )AS B
							SELECT * INTO #SelectedPersonswithDays FROM(
							select ROW_NUMBER() OVER (PARTITION BY p.HelperID,p.PersonID ORDER BY pc.PersonCollaborationID) rownum,
                            p.HelperID,p.PersonID,		pc.EnrollDate	,pc.EndDate	,	
						    CASE WHEN ISNULL(pc.EndDate,GETDATE())>GETDATE()
                                               THEN DATEDIFF(DAY, ISNULL(pc.EnrollDate, GETDATE()),GETDATE())+1
                                    ELSE DATEDIFF(DAY, ISNULL(pc.EnrollDate, GETDATE()),ISNULL(pc.EndDate, GETDATE()))+1 END [Days]
							 FROM  #PeopleHelped P 
									LEFT JOIN PersonCollaboration pc ON pc.PersonID=p.PersonID AND pc.IsPrimary=1 AND pc.IsRemoved=0
									AND IsCurrent = 1 
							)AS C
                           SELECT * INTO #PersonAssessed FROM 
                            (
	                             SELECT 
		                          PH.helperID , COUNT(A.AssessmentID) as AssessmentsCount--,PH.PersonID 
	                            FROM 
	                            #SelectedPersonswithDays PH
								LEFT JOIN PersonQuestionnaire PQ ON Pq.PersonID = PH.PersonID and pq.IsRemoved = 0 AND pq.IsActive=1
								JOIN Assessment a ON a.PersonQuestionnaireID=pq.PersonQuestionnaireID AND a.IsRemoved=0
								where a.AssessmentStatusID in ({assessmentRejectStatusID},{assessmentSubmitStatusID},{assessmentApproveStatusID})
                                {helperAssessmentQueryQuery}
	                           GROUP BY PH.helperID
                            )AS D
                           SELECT * INTO #DistinctPersonQuestionnaireMetrics FROM
								(
								SELECT PersonQuestionnaireMetricsID FROM(
								SELECT 
								ROW_NUMBER() OVER(PARTITIon BY pqm.PersonID,pqm.ItemID ORDER BY pqm.ItemID) AS RowNumber, pqm.PersonQuestionnaireMetricsID
								from PersonQuestionnaireMetrics pqm WITH (NOLOCK)
								JOIN #PeopleHelped p ON pqm.PersonID=p.PersonID
									 ) as A 
								WHERE A.RowNumber = 1
							)AS E 
                           SELECT * INTO #MetricsForPersons FROM( 
								SELECT ph.PersonID,ph.HelperID,
							           [NeedsIdentified]
		                            , [NeedsAddressed]
		                            , [StrengthsIdentified]
		                            , [StrengthsBuilt]
                                    , [NeedsEver]
                                    , [NeedsAddressing]
                                    , [StrengthsEver]
		                            , [StrengthsBuilding],0 As PersonMetrics
									FROM #PeopleHelped ph
									LEFT JOIN [dbo].[PersonQuestionnaireMetrics] pm ON ph.PersonID=pm.[PersonID] 
                                    JOIN  #DistinctPersonQuestionnaireMetrics pm1 on pm1.[PersonQuestionnaireMetricsID]=pm.[PersonQuestionnaireMetricsId]
									WHERE 1= 1 {string.Format(helperColbQueryCondition, "PM")} {helpermetricsQuery}
                             )AS F
                            SELECT * INTO #CTE FROM
                            (
	                            SELECT
		                            ph.HelperID,COUNT(Distinct ph.PersonID) Helping,AVG(ph.Days) [Days]
		                            ,SUM([NeedsIdentified]) [TotalNeedsIdentified]
		                            ,SUM([NeedsAddressed]) [TotalNeedsAddressed]
		                            ,SUM([StrengthsIdentified]) [TotalStrengthsIdentified]
		                            ,SUM([StrengthsBuilt]) [TotalStrengthsBuilt]
                                    ,SUM([NeedsEver]) [TotalNeedsEver]
                                    ,SUM([NeedsAddressing]) [TotalNeedsAddressing]
                                    ,SUM([StrengthsEver]) [TotalStrengthsEver]
		                            ,SUM([StrengthsBuilding]) [TotalStrengthsBuilding]
                                    ,MAX(AssessmentsCount) [Completed] 
	                            FROM #SelectedPersonswithDays ph
	                            LEFT JOIN #MetricsForPersons pm ON ph.PersonID=pm.[PersonID]
                                LEFT JOIN [#PersonAssessed] pa ON pa.helperID=ph.[helperID]
                                WHERE 1= 1 
	                            GROUP BY ph.HelperID,rownum HAving  rownum = 1
                            )AS G
                            select 
	                            COUNT(H.HelperID) OVER() AS TotalCount,
	                            H.HelperID HelperID
	                            ,H.HelperIndex HelperIndex
	                            ,H.HelperName
	                            ,CTE.Helping
	                            ,CTE.Days Days
	                            ,null AS Due
	                            ,Completed
                                ,CTE.TotalNeedsIdentified
	                            ,CTE.TotalNeedsAddressed
                                ,CTE.TotalStrengthsIdentified
	                            ,CTE.TotalStrengthsBuilt
                                ,CTE.TotalNeedsEver
	                            ,CTE.TotalNeedsAddressing
                                ,CTE.TotalStrengthsEver
	                            ,CTE.TotalStrengthsBuilding
                            from #HelperList H
                            LEFT JOIN #CTE CTE on CTE.HelperID=H.HelperID
                            WHERE 1=1
                            ";
                    }
                    else if (helperSearchDTO.role == PCISEnum.Roles.Supervisor)
                    {
                        query = @$";WITH SupervisorHierarchy AS
                            (
	                           SELECT
							        H.HelperID HelperID,
	                                H.HelperIndex HelperIndex,
	                                H.FirstName +COALESCE(CASE H.MiddleName WHEN '' THEN '' ELSE ' '+H.MiddleName END, ' '+H.MiddleName, '')+ ' ' +H.LastName [HelperName]
							     FROM
							     Helper h	 	 
							     WHERE H.IsRemoved=0 AND h.AgencyID={helperSearchDTO.agencyID.ToString()}
							        AND (h.HelperID={helperSearchDTO.helperID.ToString()} OR h.SupervisorHelperID={helperSearchDTO.helperID.ToString()}) {activeFilter}
							  UNION ALL
							  SELECT
								    H1.HelperID HelperID,
	                                H1.HelperIndex HelperIndex,
	                                H1.FirstName +COALESCE(CASE H1.MiddleName WHEN '' THEN '' ELSE ' '+H1.MiddleName END, ' '+H1.MiddleName, '')+ ' ' +H1.LastName [HelperName]
							     FROM Helper H1 
								 INNER JOIN SupervisorHierarchy HL ON H1.SupervisorHelperID = HL.HelperID 
								 AND HL.HelperID <> {helperSearchDTO.helperID.ToString()}
							     WHERE H1.IsRemoved=0 AND H1.AgencyID={helperSearchDTO.agencyID.ToString()} 
							)
                            SELECT * INTO #HelperList	FROM
							(
							  SELECT
								    H.HelperID HelperID,
	                                H.HelperIndex HelperIndex,
	                                H.FirstName +COALESCE(CASE H.MiddleName WHEN '' THEN '' ELSE ' '+H.MiddleName END, ' '+H.MiddleName, '')+ ' ' +H.LastName [HelperName]
							    FROM Helper h WHERE h.AgencyID={helperSearchDTO.agencyID.ToString()} AND h.IsRemoved=0 
	                            AND h.ReviewerID={helperSearchDTO.helperID.ToString()} {activeFilter}
								UNION
								select * from SupervisorHierarchy
							)AS A
                            SELECT * INTO #PeopleHelped FROM
                            (
	                            SELECT
		                            h.HelperID,
		                            p.PersonID
	                            FROM
	                            #HelperList h
	                            JOIN dbo.PersonHelper  ph ON h.HelperID=ph.HelperID AND ph.IsRemoved=0
                                AND ph.StartDate<=CAST(GETDATE() AS DATE) AND ISNULL(ph.EndDate, CAST(GETDATE() AS DATE))>=CAST(GETDATE() AS DATE)
	                            JOIN dbo.Person  p ON ph.PersonID=p.PersonID AND p.IsRemoved=0 AND p.IsActive=1 AND P.AgencyId = {helperSearchDTO.agencyID.ToString()}
                            )AS B
							SELECT * INTO #SelectedPersonswithDays FROM(
							select ROW_NUMBER() OVER (PARTITION BY p.HelperID,p.PersonID ORDER BY pc.PersonCollaborationID) rownum, 
							p.HelperID,p.PersonID,		pc.EnrollDate	,pc.EndDate	,	
						    CASE WHEN ISNULL(pc.EndDate,GETDATE())>GETDATE()
                                               THEN DATEDIFF(DAY, ISNULL(pc.EnrollDate, GETDATE()),GETDATE())+1
                                    ELSE DATEDIFF(DAY, ISNULL(pc.EnrollDate, GETDATE()),ISNULL(pc.EndDate, GETDATE()))+1 END [Days]
							 FROM  #PeopleHelped P 
									LEFT JOIN PersonCollaboration pc ON pc.PersonID=p.PersonID AND pc.IsPrimary=1 AND pc.IsRemoved=0
									AND IsCurrent = 1 
							)AS C
                            SELECT * INTO #PersonAssessed FROM 
                            (
	                             SELECT 
		                          PH.helperID , COUNT(A.AssessmentID) as AssessmentsCount--,PH.PersonID 
	                            FROM 
	                            #SelectedPersonswithDays PH
								LEFT JOIN PersonQuestionnaire PQ ON Pq.PersonID = PH.PersonID and pq.IsRemoved = 0 AND pq.IsActive=1
								JOIN Assessment a ON a.PersonQuestionnaireID=pq.PersonQuestionnaireID AND a.IsRemoved=0
								where a.AssessmentStatusID in ({assessmentRejectStatusID},{assessmentSubmitStatusID},{assessmentApproveStatusID})
                                {helperAssessmentQueryQuery}
	                           GROUP BY PH.helperID
                            )AS D
                            SELECT * INTO #DistinctPersonQuestionnaireMetrics FROM
								(
								SELECT PersonQuestionnaireMetricsID FROM(
								SELECT 
								ROW_NUMBER() OVER(PARTITIon BY pqm.PersonID,pqm.ItemID ORDER BY pqm.ItemID) AS RowNumber, pqm.PersonQuestionnaireMetricsID
								from PersonQuestionnaireMetrics pqm WITH (NOLOCK)
								JOIN #PeopleHelped p ON pqm.PersonID=p.PersonID
									 ) as A 
								WHERE A.RowNumber = 1
							)AS E
                            SELECT * INTO #MetricsForPersons FROM( 
								SELECT ph.PersonID,ph.HelperID,
							           [NeedsIdentified]
		                            , [NeedsAddressed]
		                            , [StrengthsIdentified]
		                            , [StrengthsBuilt]
                                    , [NeedsEver]
                                    , [NeedsAddressing]
                                    , [StrengthsEver]
		                            , [StrengthsBuilding],0 As PersonMetrics
									FROM #PeopleHelped ph
									LEFT JOIN [dbo].[PersonQuestionnaireMetrics] pm ON ph.PersonID=pm.[PersonID] 
                                    JOIN #DistinctPersonQuestionnaireMetrics pm1 on pm1.[PersonQuestionnaireMetricsID]=pm.[PersonQuestionnaireMetricsId]
									WHERE 1= 1 {string.Format(helperColbQueryCondition, "PM")} {helpermetricsQuery}
                             )AS F
                            SELECT * INTO #CTE FROM
                            (
	                            SELECT
		                            ph.HelperID,COUNT(Distinct ph.PersonID) Helping,AVG(ph.Days) [Days]
		                            ,SUM([NeedsIdentified]) [TotalNeedsIdentified]
		                            ,SUM([NeedsAddressed]) [TotalNeedsAddressed]
		                            ,SUM([StrengthsIdentified]) [TotalStrengthsIdentified]
		                            ,SUM([StrengthsBuilt]) [TotalStrengthsBuilt]
                                    ,SUM([NeedsEver]) [TotalNeedsEver]
                                    ,SUM([NeedsAddressing]) [TotalNeedsAddressing]
                                    ,SUM([StrengthsEver]) [TotalStrengthsEver]
		                            ,SUM([StrengthsBuilding]) [TotalStrengthsBuilding]
                                    ,MAX(AssessmentsCount) [Completed] 
	                            FROM #SelectedPersonswithDays ph
	                            LEFT JOIN #MetricsForPersons pm ON ph.PersonID=pm.[PersonID]
                                LEFT JOIN [#PersonAssessed] pa ON pa.helperID=ph.[helperID]
                                WHERE 1= 1 
	                            GROUP BY ph.HelperID,rownum HAving  rownum = 1
                            )AS G
                            select 
	                            COUNT(H.HelperID) OVER() AS TotalCount,
	                            H.HelperID HelperID
	                            ,H.HelperIndex HelperIndex
	                            ,H.[HelperName]
	                            ,CTE.Helping
	                            ,CTE.Days Days
	                            ,null AS Due
	                            ,Completed
                                ,CTE.TotalNeedsIdentified
	                            ,CTE.TotalNeedsAddressed
                                ,CTE.TotalStrengthsIdentified
	                            ,CTE.TotalStrengthsBuilt
                                ,CTE.TotalNeedsEver
	                            ,CTE.TotalNeedsAddressing
                                ,CTE.TotalStrengthsEver
	                            ,CTE.TotalStrengthsBuilding
                            from #HelperList H
                            LEFT JOIN #CTE CTE on CTE.HelperID=H.HelperID
                            WHERE 1=1
                            ";
                    }
                    else if (helperSearchDTO.role == PCISEnum.Roles.HelperRO || helperSearchDTO.role == PCISEnum.Roles.HelperRW || helperSearchDTO.role == PCISEnum.Roles.Assessor)
                    {
                        query = @$"SELECT * INTO #HelperList FROM
                            (
	                            SELECT
		                            H.HelperID HelperID,
	                                H.HelperIndex HelperIndex,
	                                H.FirstName +COALESCE(CASE H.MiddleName WHEN '' THEN '' ELSE ' '+H.MiddleName END, ' '+H.MiddleName, '')+ ' ' +H.LastName [HelperName]
	                            FROM
	                            Helper h
	                            WHERE H.IsRemoved=0 AND h.AgencyID={helperSearchDTO.agencyID.ToString()} 
                                AND (h.HelperID={helperSearchDTO.helperID.ToString()} OR h.ReviewerID={helperSearchDTO.helperID.ToString()}) --FOR HELPER And ReviwerHelpers
                            )AS A
                            SELECT * INTO #PeopleHelped FROM
                            (
	                            SELECT
		                            h.HelperID,
		                            p.PersonID
	                            FROM
	                            #HelperList h
	                            JOIN dbo.PersonHelper  ph ON h.HelperID=ph.HelperID AND ph.IsRemoved=0
                                AND ph.StartDate<=CAST(GETDATE() AS DATE) AND ISNULL(ph.EndDate, CAST(GETDATE() AS DATE))>=CAST(GETDATE() AS DATE)
	                            JOIN dbo.Person  p ON ph.PersonID=p.PersonID AND p.IsRemoved=0 AND p.IsActive=1 AND P.AgencyId = {helperSearchDTO.agencyID.ToString()}
                            )AS B
							SELECT * INTO #SelectedPersonswithDays FROM(
							select ROW_NUMBER() OVER (PARTITION BY p.HelperID,p.PersonID ORDER BY pc.PersonCollaborationID) rownum, 
							p.HelperID,p.PersonID,		pc.EnrollDate	,pc.EndDate	,	
						    CASE WHEN ISNULL(pc.EndDate,GETDATE())>GETDATE()
                                               THEN DATEDIFF(DAY, ISNULL(pc.EnrollDate, GETDATE()),GETDATE())+1
                                    ELSE DATEDIFF(DAY, ISNULL(pc.EnrollDate, GETDATE()),ISNULL(pc.EndDate, GETDATE()))+1 END [Days]
							 FROM  #PeopleHelped P 
									LEFT JOIN PersonCollaboration pc ON pc.PersonID=p.PersonID AND pc.IsPrimary=1 AND pc.IsRemoved=0
									AND IsCurrent = 1 
							)AS C
                            SELECT * INTO #PersonAssessed FROM 
                            (
	                             SELECT 
		                          PH.helperID , COUNT(A.AssessmentID) as AssessmentsCount--,PH.PersonID 
	                            FROM 
	                            #SelectedPersonswithDays PH
								LEFT JOIN PersonQuestionnaire PQ ON Pq.PersonID = PH.PersonID and pq.IsRemoved = 0 AND pq.IsActive=1
								JOIN Assessment a ON a.PersonQuestionnaireID=pq.PersonQuestionnaireID AND a.IsRemoved=0
								where a.AssessmentStatusID in ({assessmentRejectStatusID},{assessmentSubmitStatusID},{assessmentApproveStatusID})
                                {helperAssessmentQueryQuery}
	                           GROUP BY PH.helperID
                            )AS D
                            SELECT * INTO #DistinctPersonQuestionnaireMetrics FROM
								(
								SELECT PersonQuestionnaireMetricsID FROM(
								SELECT 
								ROW_NUMBER() OVER(PARTITIon BY pqm.PersonID,pqm.ItemID ORDER BY pqm.ItemID) AS RowNumber, pqm.PersonQuestionnaireMetricsID
								from PersonQuestionnaireMetrics pqm WITH (NOLOCK)
								JOIN #PeopleHelped p ON pqm.PersonID=p.PersonID
									 ) as A 
								WHERE A.RowNumber = 1
							)AS E
                            SELECT * INTO #MetricsForPersons FROM( 
								SELECT ph.PersonID,ph.HelperID,
							           [NeedsIdentified]
		                            , [NeedsAddressed]
		                            , [StrengthsIdentified]
		                            , [StrengthsBuilt]
                                    , [NeedsEver]
                                    , [NeedsAddressing]
                                    , [StrengthsEver]
		                            , [StrengthsBuilding],0 As PersonMetrics
									FROM #PeopleHelped ph
									LEFT JOIN [dbo].[PersonQuestionnaireMetrics] pm ON ph.PersonID=pm.[PersonID] 
                                    JOIN #DistinctPersonQuestionnaireMetrics pm1 on pm1.[PersonQuestionnaireMetricsID]=pm.[PersonQuestionnaireMetricsId]
									WHERE 1= 1 {string.Format(helperColbQueryCondition, "PM")} {helpermetricsQuery}
                             )AS F
                            SELECT * INTO #CTE FROM
                            (
	                            SELECT
		                            ph.HelperID,COUNT(Distinct ph.PersonID) Helping,AVG(ph.Days) [Days]
		                            ,SUM([NeedsIdentified]) [TotalNeedsIdentified]
		                            ,SUM([NeedsAddressed]) [TotalNeedsAddressed]
		                            ,SUM([StrengthsIdentified]) [TotalStrengthsIdentified]
		                            ,SUM([StrengthsBuilt]) [TotalStrengthsBuilt]
                                    ,SUM([NeedsEver]) [TotalNeedsEver]
                                    ,SUM([NeedsAddressing]) [TotalNeedsAddressing]
                                    ,SUM([StrengthsEver]) [TotalStrengthsEver]
		                            ,SUM([StrengthsBuilding]) [TotalStrengthsBuilding]
                                    ,MAX(AssessmentsCount) [Completed] 
	                            FROM #SelectedPersonswithDays ph
	                            LEFT JOIN #MetricsForPersons pm ON ph.PersonID=pm.[PersonID]
                                LEFT JOIN [#PersonAssessed] pa ON pa.helperID=ph.[helperID]
                                WHERE 1= 1
	                            GROUP BY ph.HelperID,rownum HAving  rownum = 1
                            )AS G
                            select 
	                            COUNT(H.HelperID) OVER() AS TotalCount,
	                            H.HelperID HelperID
	                            ,H.HelperIndex HelperIndex
	                            ,H.HelperName
	                            ,CTE.Helping
	                            ,CTE.Days Days
	                            ,null AS Due
	                            ,Completed
                                ,CTE.TotalNeedsIdentified
	                            ,CTE.TotalNeedsAddressed
                                ,CTE.TotalStrengthsIdentified
	                            ,CTE.TotalStrengthsBuilt
                                ,CTE.TotalNeedsEver
	                            ,CTE.TotalNeedsAddressing
                                ,CTE.TotalStrengthsEver
	                            ,CTE.TotalStrengthsBuilding
                            from #HelperList H
                            LEFT JOIN #CTE CTE on CTE.HelperID=H.HelperID
                            WHERE 1=1 
                            ";
                    }
                }
                query += queryBuilderDTO.WhereCondition + queryBuilderDTO.OrderBy + queryBuilderDTO.Paginate;
                int totalCount = 0;
                List<HelperDataDTO> data = ExecuteSqlQuery(query, x =>
                {
                    totalCount = x["TotalCount"] == DBNull.Value ? 0 : (int)x["TotalCount"];
                    return new HelperDataDTO
                    {
                        HelperID = x["HelperID"] == DBNull.Value ? 0 : (int)x["HelperID"],
                        HelperIndex = x["HelperIndex"] == DBNull.Value ? null : (Guid?)x["HelperIndex"],
                        HelperName = x["HelperName"] == DBNull.Value ? null : (string)x["HelperName"],
                        Helping = x["Helping"] == DBNull.Value ? 0 : (int)x["Helping"],
                        Days = x["Days"] == DBNull.Value ? 0 : (int)x["Days"],
                        Due = x["Due"] == DBNull.Value ? null : (int?)x["Due"],
                        Completed = x["Completed"] == DBNull.Value ? 0 : (int?)x["Completed"],
                        NeedIdentified = x["TotalNeedsIdentified"] == DBNull.Value ? 0 : (int)x["TotalNeedsIdentified"],
                        NeedAddressed = x["TotalNeedsAddressed"] == DBNull.Value ? 0 : (int)x["TotalNeedsAddressed"],
                        StrengthIdentified = x["TotalStrengthsIdentified"] == DBNull.Value ? 0 : (int)x["TotalStrengthsIdentified"],
                        StrengthBuilt = x["TotalStrengthsBuilt"] == DBNull.Value ? 0 : (int)x["TotalStrengthsBuilt"],
                        NeedsEver = x["TotalNeedsEver"] == DBNull.Value ? 0 : (int)x["TotalNeedsEver"],
                        NeedsAddressing = x["TotalNeedsAddressing"] == DBNull.Value ? 0 : (int)x["TotalNeedsAddressing"],
                        StrengthsEver = x["TotalStrengthsEver"] == DBNull.Value ? 0 : (int)x["TotalStrengthsEver"],
                        StrengthsBuilding = x["TotalStrengthsBuilding"] == DBNull.Value ? 0 : (int)x["TotalStrengthsBuilding"]
                    };
                }, queryBuilderDTO.QueryParameterDTO);

                return Tuple.Create(data, totalCount);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetHelperDetailsByHelperIDCount.
        /// </summary>
        /// <param name="role">role.</param>
        /// <param name="agencyID">agencyID.</param>
        /// <param name="helperID">helperID.</param>
        /// <returns>int.</returns>
        public int GetHelperDetailsByHelperIDCount(string role, long agencyID, int? helperID = null)
        {
            try
            {
                var query = string.Empty;
                if (role == PCISEnum.Roles.SuperAdmin)
                {
                    query = @$"WITH CTE AS
                            (
	                            SELECT
		                            h.HelperID
		                            ,COUNT(DISTINCT pm.PersonID) Helping
		                            ,AVG(DATEDIFF(DAY, ISNULL(pc.EnrollDate, GETDATE()), ISNULL(pc.EndDate, GETDATE()))) [Days]
		                            ,SUM([NeedsIdentified]) [TotalNeedsIdentified]
		                            ,SUM([NeedsAddressed]) [TotalNeedsAddressed]
		                            ,SUM([StrengthsIdentified]) [TotalStrengthsIdentified]
		                            ,SUM([StrengthsBuilt]) [TotalStrengthsBuilt]
	                            FROM dbo.Helper h
	                            JOIN dbo.PersonHelper  ph ON h.HelperID=ph.HelperID AND h.AgencyID={agencyID.ToString()}
                                AND ph.StartDate<=CAST(GETDATE() AS DATETIME2) AND ISNULL(ph.EndDate, CAST(GETDATE() AS DATETIME2))>=CAST(GETDATE() AS DATETIME2)
	                            JOIN [dbo].[PersonQuestionnaireMetrics] pm ON ph.PersonID=pm.[PersonID]
	                            JOIN dbo.Person  p ON pm.PersonID=p.PersonID
	                            JOIN PersonCollaboration pc ON pc.PersonID=p.PersonID AND pc.IsPrimary=1 AND pc.IsRemoved=0
	                            WHERE ph.IsRemoved=0 AND p.IsRemoved=0 AND p.IsActive=1 
	                            GROUP BY h.HelperID
                            )
                            select 
	                            COUNT(*)
                            from Helper H
                            left join CTE on CTE.HelperID=H.HelperID
                            WHERE H.IsRemoved=0 AND h.AgencyID={agencyID.ToString()}";
                }
                else if (role == PCISEnum.Roles.OrgAdminRO || role == PCISEnum.Roles.OrgAdminRW)
                {
                    query = @$"WITH CTE AS
                            (
	                            SELECT
		                            h.HelperID
		                            ,COUNT(DISTINCT pm.PersonID) Helping
		                            ,AVG(DATEDIFF(DAY, ISNULL(pc.EnrollDate, GETDATE()), ISNULL(pc.EndDate, GETDATE()))) [Days]
		                            ,SUM([NeedsIdentified]) [TotalNeedsIdentified]
		                            ,SUM([NeedsAddressed]) [TotalNeedsAddressed]
		                            ,SUM([StrengthsIdentified]) [TotalStrengthsIdentified]
		                            ,SUM([StrengthsBuilt]) [TotalStrengthsBuilt]
	                            FROM dbo.Helper h
	                            JOIN dbo.PersonHelper  ph ON h.HelperID=ph.HelperID AND h.AgencyID={agencyID.ToString()}
                                AND ph.StartDate<=CAST(GETDATE() AS DATETIME2) AND ISNULL(ph.EndDate, CAST(GETDATE() AS DATETIME2))>=CAST(GETDATE() AS DATETIME2)
	                            JOIN [dbo].[PersonQuestionnaireMetrics] pm ON ph.PersonID=pm.[PersonID]
	                            JOIN dbo.Person  p ON pm.PersonID=p.PersonID
	                            JOIN PersonCollaboration pc ON pc.PersonID=p.PersonID AND pc.IsPrimary=1 AND pc.IsRemoved=0
	                            WHERE ph.IsRemoved=0 AND p.IsRemoved=0 AND p.IsActive=1 
	                            GROUP BY h.HelperID
                            )
                            select 
	                            COUNT(*)
                            from Helper H
                            left join CTE on CTE.HelperID=H.HelperID
                            WHERE H.IsRemoved=0 AND h.AgencyID={agencyID.ToString()} AND h.HelperID<>{helperID.ToString()}";
                }
                else if (role == PCISEnum.Roles.Supervisor)
                {
                    query = @$"WITH CTE AS
                            (
	                            SELECT
		                            h.HelperID
		                            ,COUNT(DISTINCT pm.PersonID) Helping
		                            ,AVG(DATEDIFF(DAY, ISNULL(pc.EnrollDate, GETDATE()), ISNULL(pc.EndDate, GETDATE()))) [Days]
		                            ,SUM([NeedsIdentified]) [TotalNeedsIdentified]
		                            ,SUM([NeedsAddressed]) [TotalNeedsAddressed]
		                            ,SUM([StrengthsIdentified]) [TotalStrengthsIdentified]
		                            ,SUM([StrengthsBuilt]) [TotalStrengthsBuilt]
	                            FROM dbo.Helper h
	                            JOIN dbo.PersonHelper  ph ON h.HelperID=ph.HelperID AND h.AgencyID={agencyID.ToString()}
                                AND ph.StartDate<=CAST(GETDATE() AS DATETIME2) AND ISNULL(ph.EndDate, CAST(GETDATE() AS DATETIME2))>=CAST(GETDATE() AS DATETIME2)
	                            JOIN [dbo].[PersonQuestionnaireMetrics] pm ON ph.PersonID=pm.[PersonID]
	                            JOIN dbo.Person  p ON pm.PersonID=p.PersonID
	                            JOIN PersonCollaboration pc ON pc.PersonID=p.PersonID AND pc.IsPrimary=1 AND pc.IsRemoved=0
	                            WHERE ph.IsRemoved=0 AND p.IsRemoved=0 AND p.IsActive=1 
	                            GROUP BY h.HelperID
                            )
                            select 
	                            COUNT(*)
                            from Helper H
                            left join CTE on CTE.HelperID=H.HelperID
                            WHERE H.IsRemoved=0 AND h.AgencyID={agencyID.ToString()} AND h.SupervisorHelperID={helperID.ToString()}";
                }
                else if (role == PCISEnum.Roles.HelperRO || role == PCISEnum.Roles.HelperRW || role == PCISEnum.Roles.Assessor)
                {
                    query = @$"WITH CTE AS
                            (
	                            SELECT
		                            h.HelperID
		                            ,COUNT(DISTINCT pm.PersonID) Helping
		                            ,AVG(DATEDIFF(DAY, ISNULL(pc.EnrollDate, GETDATE()), ISNULL(pc.EndDate, GETDATE()))) [Days]
		                            ,SUM([NeedsIdentified]) [TotalNeedsIdentified]
		                            ,SUM([NeedsAddressed]) [TotalNeedsAddressed]
		                            ,SUM([StrengthsIdentified]) [TotalStrengthsIdentified]
		                            ,SUM([StrengthsBuilt]) [TotalStrengthsBuilt]
	                            FROM dbo.Helper h
	                            JOIN dbo.PersonHelper  ph ON h.HelperID=ph.HelperID AND h.AgencyID={agencyID.ToString()}
                                AND ph.StartDate<=CAST(GETDATE() AS DATETIME2) AND ISNULL(ph.EndDate, CAST(GETDATE() AS DATETIME2))>=CAST(GETDATE() AS DATETIME2)
	                            JOIN [dbo].[PersonQuestionnaireMetrics] pm ON ph.PersonID=pm.[PersonID]
	                            JOIN dbo.Person  p ON pm.PersonID=p.PersonID
	                            JOIN PersonCollaboration pc ON pc.PersonID=p.PersonID AND pc.IsPrimary=1 AND pc.IsRemoved=0
	                            WHERE ph.IsRemoved=0 AND p.IsRemoved=0 AND p.IsActive=1 
	                            GROUP BY h.HelperID
                            )
                            select 
	                            COUNT(*)
                            from Helper H
                            left join CTE on CTE.HelperID=H.HelperID
                            WHERE H.IsRemoved=0 AND h.AgencyID={agencyID.ToString()} AND h.HelperID={helperID.ToString()}";
                }


                var data = ExecuteSqlQuery(query, x => new HelperDataDTO
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

        /// <summary>
        /// GetHelperUsedCount.
        /// </summary>
        /// <param name="helperID">helperID.</param>
        /// <returns>int.</returns>
        public int GetHelperUsedCount(int helperID)
        {
            try
            {
                int usedcount = 0;
                string queryHelper = @"SELECT COUNT(*) FROM Helper WHERE IsRemoved=0 AND (SupervisorHelperID=" + helperID + " OR (ReviewerID=" + helperID + " and HelperID <> " + helperID + "))";
                string queryPerson = @"SELECT COUNT(*) FROM Person P JOIN PersonHelper ph ON p.PersonID=ph.PersonID AND p.IsRemoved=0 AND p.IsActive=1 AND ph.IsRemoved=0 AND ph.HelperID=" + helperID;

                var dataHelper = ExecuteSqlQuery(queryHelper, x => new HelperDataDTO
                {
                    TotalCount = (int)x[0]
                });
                if (dataHelper != null && dataHelper.Count > 0)
                {
                    usedcount += dataHelper[0].TotalCount;
                }

                if (usedcount <= 0)
                {
                    var dataPerson = ExecuteSqlQuery(queryPerson, x => new HelperDataDTO
                    {
                        TotalCount = (int)x[0]
                    });
                    if (dataPerson != null && dataPerson.Count > 0)
                    {
                        usedcount += dataPerson[0].TotalCount;
                    }
                }

                return usedcount;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// ValidateAgencyName.
        /// </summary>
        /// <param name="agencyDetailsDTO">agencyDetailsDTO.</param>
        /// <returns>bool.</returns>
        public async Task<bool> ValidateHelperExternalID(HelperDetailsDTO helperDetailsDTO, long agencyID)
        {
            try
            {
                Helper helper = new Helper();
                if (helperDetailsDTO.HelperIndex != Guid.Empty && helperDetailsDTO.HelperExternalID != null)
                {
                    helper = await this.GetRowAsync(x => x.HelperExternalID == helperDetailsDTO.HelperExternalID && x.HelperIndex != helperDetailsDTO.HelperIndex && x.AgencyID == agencyID);
                }
                else if (helperDetailsDTO.HelperExternalID != null)
                {
                    helper = await this.GetRowAsync(x => x.HelperExternalID == helperDetailsDTO.HelperExternalID && x.AgencyID == agencyID);
                }
                if (helper == null || helper.HelperID == 0)
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

        public List<Helper> GetAllExternalHelpersForAgency(long agencyID)
        {
            try
            {
                var helper = _dbContext.Helper.Where(x => x.IsRemoved == false && x.AgencyID == agencyID && !string.IsNullOrEmpty(x.HelperExternalID)).ToList();
                return helper;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetHelperDetailsByHelperEmail.
        /// </summary>
        /// <param name="helperEmailCSV">helperEmailCSV.</param>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>HelperInfoDTO.</returns>
        public List<HelperInfoDTO> GetHelperDetailsByHelperEmail(string helperEmailCSV, long agencyID)
        {
            List<HelperInfoDTO> helperDataList = new List<HelperInfoDTO>();

            try
            {
                helperEmailCSV = string.IsNullOrEmpty(helperEmailCSV) ? null : helperEmailCSV.ToLower();
                var query = string.Empty;
                query = @$"SELECT HelperID,HelperIndex,FirstName,MiddleName,LastName,Email,StartDate, EndDate
                        FROM    [dbo].[Helper] where LOWER(Email) in ({helperEmailCSV})
                        and  AgencyID = {agencyID}";

                helperDataList = ExecuteSqlQuery(query, x => new HelperInfoDTO
                {
                    HelperID = (int)x["HelperID"],
                    HelperIndex = (Guid)x["HelperIndex"],
                    FirstName = x["FirstName"] == DBNull.Value ? null : (string)x["FirstName"],
                    MiddleName = x["MiddleName"] == DBNull.Value ? null : (string)x["MiddleName"],
                    LastName = x["LastName"] == DBNull.Value ? null : (string)x["LastName"],
                    Email = x["Email"] == DBNull.Value ? null : (string)x["Email"],
                    StartDate = x["StartDate"] == DBNull.Value ? DateTime.Now : (DateTime)x["StartDate"],
                    EndDate = x["EndDate"] == DBNull.Value ? null : (DateTime?)x["EndDate"],
                });
                return helperDataList;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// AddHelperBulk.
        /// </summary>
        /// <param name="helperDTOList">helperDTOList.</param>
        /// <returns>HelperDTO.</returns>
        public List<HelperDTO> AddHelperBulk(List<HelperDTO> helperDTOList)
        {
            try
            {
                List<Helper> helper = new List<Helper>();
                this.mapper.Map<List<HelperDTO>, List<Helper>>(helperDTOList, helper);
                var res = this.AddBulkAsync(helper);
                res.Wait();
                return helperDTOList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// GetHelperListByGUID.
        /// </summary>
        /// <param name="helperIndexGuids">helperIndexGuids.</param>
        /// <returns>Helper.</returns>
        public async Task<IReadOnlyList<Helper>> GetHelperListByGUID(List<Guid> helperIndexGuids)
        {
            try
            {
                var response = await this.GetAsync(x => helperIndexGuids.Contains(x.HelperIndex));
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// UpdateHelperBulk.
        /// </summary>
        /// <param name="updateHelperDTOList">updateHelperDTOList.</param>
        public void UpdateHelperBulk(List<HelperDTO> updateHelperDTOList)
        {
            try
            {
                List<Helper> helper = new List<Helper>();
                this.mapper.Map<List<HelperDTO>, List<Helper>>(updateHelperDTOList, helper);
                var res = this.UpdateBulkAsync(helper);
                res.Wait();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// GetPersonsDetailsListForExternal
        /// </summary>
        /// <param name="loggedInUserDTO"></param>
        /// <param name="queryBuilderDTO"></param>
        /// <returns>HelperDetailsListDTO</returns>
        public List<HelperDetailsListDTO> GetHelpersDetailsListForExternal(LoggedInUserDTO loggedInUserDTO, DynamicQueryBuilderDTO queryBuilderDTO, HelperSearchInputDTO helperSearchInputDTO)
        {
            try
            {
                var query = string.Empty;
                string joinQuery = string.Empty;
                string columnQuery = string.Empty;
                var superAdminID = this._systemRoleRepository.GetSystemRoleByRoleName(PCISEnum.Roles.SuperAdmin)?.SystemRoleID;

                if (helperSearchInputDTO.SearchFields?.PersonIndex != null)
                {
                    joinQuery = $@"left join PersonHelper Ph on Ph.HelperID = H.HelperID
                                   left join Person P on P.PersonID = ph.PersonID";
                    columnQuery = $@",P.PersonIndex";
                }
                query = @$";WITH HelperList AS
                            (
	                        select H.HelperID, H.HelperIndex, H.FirstName, H.LastName, H.MiddleName, HT.HelperTitleID, HT.Name as HelperTitle, H.Email, H.Phone, H.Phone2,
                            H1.FirstName+' '+IsNull(H1.MiddleName,'')+' ' +H1.LastName as Supervisor, A.[Name] as Agency, AD.Address1 as [Address], 
                            AD.Address2 as Address2, AD.City, AD.Zip, C.[Name] as State, S.[Name] as [Role], H1.HelperID as SupervisorID, 
                            H1.HelperIndex as SupervisorIndex, C.CountryStateId as CountryStateId , S.SystemRoleID,A.AgencyID,H.ReviewerID,H.StartDate,H.EndDate,H.HelperExternalID, H.UserId,H.IsEmailReminderAlerts, Con.CountryID as CountryId,Con.Name AS Country,
	                        H.FirstName +COALESCE(CASE H.MiddleName WHEN '' THEN '' ELSE ' '+H.MiddleName END, ' '+H.MiddleName, '')+ ' ' +H.LastName [FullName]{columnQuery}
				            from helper H  
                            {joinQuery}
                            left join Helper H1 on H1.HelperId = H.SupervisorHelperId
                            left join Agency A on A.AgencyId = H.AgencyId
                            left join HelperAddress HD on HD.HelperId = H.HelperId
                            left join Address AD on AD.AddressId = HD.AddressId
                            left join info.CountryState C on AD.CountryStateId = C.CountryStateId 
                            left join UserRole U on U.UserId = H.UserId
                            left join info.SystemRole S on S.SystemRoleId = U.SystemRoleId
                            left join info.HelperTitle HT on HT.HelperTitleID = H.HelperTitleID
                            left join [info].Country Con ON Con.CountryID = AD.CountryId
                            WHERE S.SystemRoleID <> {superAdminID} AND H.IsRemoved=0 AND h.AgencyID={loggedInUserDTO.AgencyId})
                            select H.* , COUNT(H.HelperID) OVER() AS TotalCount
	                        from HelperList H
                            WHERE 1=1
                            ";

                query += queryBuilderDTO.WhereCondition + queryBuilderDTO.OrderBy + queryBuilderDTO.Paginate;
                var helperDetailsListDTO = ExecuteSqlQuery(query, x => new HelperDetailsListDTO
                {
                    HelperID = (int)x[0],
                    HelperIndex = (Guid)x[1],
                    FirstName = x[2] == DBNull.Value ? null : (string)x[2],
                    LastName = x[3] == DBNull.Value ? null : (string)x[3],
                    MiddleName = x[4] == DBNull.Value ? null : (string)x[4],
                    HelperTitleID = x[5] == DBNull.Value ? null : (int?)x[5],
                    HelperTitle = x[6] == DBNull.Value ? null : (string)x[6],
                    Email = x[7] == DBNull.Value ? null : (string)x[7],
                    Phone1 = x[8] == DBNull.Value ? null : (string)x[8],
                    Phone2 = x[9] == DBNull.Value ? null : (string)x[9],
                    Supervisor = x[10] == DBNull.Value ? null : (string)x[10],
                    Agency = x[11] == DBNull.Value ? null : (string)x[11],
                    Address = x[12] == DBNull.Value ? null : (string)x[12],
                    Address2 = x[13] == DBNull.Value ? null : (string)x[13],
                    City = x[14] == DBNull.Value ? null : (string)x[14],
                    Zip = x[15] == DBNull.Value ? null : (string)x[15],
                    State = x[16] == DBNull.Value ? null : (string)x[16],
                    Role = x[17] == DBNull.Value ? null : (string)x[17],
                    SupervisorHelperID = x[18] == DBNull.Value ? null : (int?)x[18],
                    SupervisorIndex = x[19] == DBNull.Value ? null : (Guid?)x[19],
                    CountryStateId = x[20] == DBNull.Value ? null : (int?)x[20],
                    SystemRoleID = x[21] == DBNull.Value ? null : (int?)x[21],
                    AgencyId = x[22] == DBNull.Value ? 0 : (long)x[22],
                    ReviewerID = x[23] == DBNull.Value ? null : (int?)x[23],
                    StartDate = x["StartDate"] == DBNull.Value ? DateTime.MinValue : (DateTime)x["StartDate"],
                    EndDate = x["EndDate"] == DBNull.Value ? null : (DateTime?)x["EndDate"],
                    HelperExternalID = x["HelperExternalID"] == DBNull.Value ? null : (string)x["HelperExternalID"],
                    UserId = x["UserId"] == DBNull.Value ? 0 : (int)x["UserId"],
                    IsEmailReminderAlerts = x["IsEmailReminderAlerts"] == DBNull.Value ? false : (bool)x["IsEmailReminderAlerts"],
                    CountryID = x["CountryId"] == DBNull.Value ? null : (int?)x["CountryId"],
                    Country = x["Country"] == DBNull.Value ? null : (string)x["Country"],
                    TotalCount = x["TotalCount"] == DBNull.Value ? 0 : (int)x["TotalCount"],
                }, queryBuilderDTO.QueryParameterDTO);
                return helperDetailsListDTO;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<HelperPersonCollaborationDTO> GetHelperPersonCollaborationData(int helperID, long loggedInAgencyID)
        {
            try
            {
                var query = $@";With PersonHelperCollab AS ( 
									SELECT DISTINCT PH.CollaborationID , PH.PersonID
									FROM PersonHelper PH
									JOIN PersonCollaboration PC ON PC.personID = PH.Personid
									    AND PC.CollaborationID = PH.CollaborationID
									WHERE PH.helperID = {helperID}
									    AND PH.IsRemoved = 0 AND PC.IsRemoved = 0
							   )
							 SELECT DISTINCT PHC.CollaborationID, CQ.QuestionnaireID,PQ.PersonQuestionnaireID, PHC.Personid
									FROM PersonHelperCollab PHC
								    JOIN Collaboration C ON PHC.CollaborationId = C.CollaborationId
									LEFT JOIN CollaborationQuestionnaire CQ on CQ.CollaborationID = C.CollaborationID
								    JOIN PersonQuestionnaire PQ ON PQ.PersonID = PHC.PersonID AND PQ.CollaborationId = PHC.CollaborationId
								          AND CQ.QuestionnaireID = PQ.QuestionnaireID
									WHERE C.IsRemoved = 0 AND CQ.IsRemoved = 0 AND PQ.Isremoved = 0 AND C.AgencyID = {loggedInAgencyID}
									      AND ISNULL(PQ.CollaborationID, 0) <> 0 ";
                var helperDataList = ExecuteSqlQuery(query, x => new HelperPersonCollaborationDTO
                {
                    CollaborationID = x["CollaborationID"] == DBNull.Value ? 0 : (int)x["CollaborationID"],
                    QuestionnaireID = x["QuestionnaireID"] == DBNull.Value ? 0 : (int)x["QuestionnaireID"],
                    PersonQuestionnaireID = x["PersonQuestionnaireID"] == DBNull.Value ? 0 : (long)x["PersonQuestionnaireID"],
                    PersonID = x["Personid"] == DBNull.Value ? 0 : (long)x["Personid"],
                });
                return helperDataList;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public HelperPersonCollaborationDetailsDTO GetHelperPersonCollaborationDetails(int helperID, long loggedInAgencyID)
        {
            try
            {
                var helperColbData = this.GetHelperPersonCollaborationData(helperID, loggedInAgencyID);

                HelperPersonCollaborationDetailsDTO result = new HelperPersonCollaborationDetailsDTO();
                if (helperColbData.Count > 0)
                {
                    result.CollaborationIDs = helperColbData.Select(x => x.CollaborationID).Distinct().ToList();
                    result.PersonIDs = helperColbData.Select(x => x.PersonID).Distinct().ToList();
                    result.PersonQuestionnaireIDs = helperColbData.Select(x => x.PersonQuestionnaireID).Distinct().ToList();
                    result.QuestionnaireIDs = helperColbData.Select(x => x.QuestionnaireID).Distinct().ToList();
                }
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<string> GetHelperPersonInCollaborationDetails(int userID, long loggedInAgencyID)
        {
            try
            {
                HelperPersonCollaborationDetailsDTO result = new HelperPersonCollaborationDetailsDTO();
                var query = $@"SELECT DISTINCT PH.CollaborationID, PH.PersonID 
                                     FROM Helper H
                                     JOIN PersonHelper  PH ON PH.HelperId = H.HelperID
                                	 WHERE H.UserID = {userID}
                                     AND H.AgencyID = {loggedInAgencyID} AND PH.IsRemoved = 0 AND CollaborationID IS NOT NULL;";

                var personIDs = ExecuteSqlQuery(query, x => new string(
                    x["Personid"] == DBNull.Value ? string.Empty : x["Personid"].ToString()
                )).Distinct().ToList();
                return personIDs;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<string> GetHelperAllAssessmentsInCollaboration(string personIDs, long agencyID, int userID, string statusType = "", bool lastAssessmentsOnly = true)
        {
            try
            {
                var assessmentStatus = this._assessmentStatusRepository.GetAllAssessmentStatus();
                var submittedStatus = assessmentStatus.Where(x => x.Name == PCISEnum.AssessmentStatus.Submitted).ToList()[0].AssessmentStatusID;
                var approvedStatus = assessmentStatus.Where(x => x.Name == PCISEnum.AssessmentStatus.Approved).ToList()[0].AssessmentStatusID;
                var returnedStatus = assessmentStatus.Where(x => x.Name == PCISEnum.AssessmentStatus.Returned).ToList()[0].AssessmentStatusID;
                var assessmentStatusCondition = string.Empty;
                var lastAssessmentsOnlyCondition = string.Empty;
                if (string.IsNullOrWhiteSpace(statusType))
                {
                    assessmentStatusCondition = $@"AND a.AssessmentStatusID in ({returnedStatus},{submittedStatus},{approvedStatus})";
                }
                if (lastAssessmentsOnly)
                {
                    lastAssessmentsOnlyCondition = $@"WHERE RowNumber = 1";
                }
                var query = @$";WITH PersonInHelp AS ( 
                        SELECT distinct personid
                        FROM Person
                         WHERE personid in ({ personIDs }) AND IsRemoved=0 
							   AND IsActive=1 AND agencyID = {agencyID}
                    )
                   ,PersonInHelpColb AS (
		            	SELECT DISTINCT PH.CollaborationID, PH.PersonID,PC.EnrollDate,PC.EndDate,CQ.QuestionnaireID,PQ.PersonQuestionnaireID,CQ.Isremoved
                            FROM Helper H
                             JOIN PersonHelper  PH ON PH.HelperId = H.HelperID AND H.UserID = {userID}
		            		 JOIN PersonInHelp  P ON PH.PersonID = P.PersonID
		            		 JOIN PersonCollaboration PC ON PC.CollaborationId = PH.CollaborationId 
		            							       AND PC.PersonID = PH.PersonID				
		            		 JOIN Collaboration C ON PC.CollaborationId = C.CollaborationId
                             LEFT JOIN CollaborationQuestionnaire CQ on CQ.CollaborationID = PC.CollaborationID
		            		 LEFT JOIN PersonQuestionnaire PQ ON PQ.PersonID = PC.PersonID 
		            					AND PQ.QuestionnaireID = CQ.QuestionnaireID
                             WHERE  H.AgencyID = {agencyID} AND PH.IsRemoved = 0 AND PC.IsRemoved = 0  AND C.IsRemoved = 0 
		            				AND ISNULL(CQ.IsRemoved,0) = 0 AND ISNULL(PQ.Isremoved,0) = 0
		            				AND PH.CollaborationID IS NOT NULL 
		            		UNION 
		            	SELECT '' AS CollaborationID, PH.PersonID , null AS EnrollDate,null AS EndDate,
                            PQ.QuestionnaireID,PQ.PersonQuestionnaireID,0 AS         Isremoved
		            	    FROM PersonInHelp P 
                             JOIN PersonHelper  PH ON P.personid =PH.Personid
		            		 JOIN PersonQuestionnaire PQ ON PH.Personid = PQ.Personid
		            		 WHERE ISNULL(PQ.CollaborationID, 0) = 0 AND PH.IsRemoved = 0 AND PQ.IsRemoved = 0 
		            						AND PQ.UpdateUserID = {userID}
					)
                  ,PersonColb AS(
                                SELECT MIN(CAST(EnrollDate AS DATE)) AS CollaborationStartDate,
                                    MAX(ISNULL(EndDate, CAST(GETDATE() AS DATE))) AS CollaborationEndDate,PersonID
                               FROM PersonInHelpColb PHC GROUP BY PersonID
                    		  )
                  ,WindowOffsets AS(
                              SELECT
                                  qw.QuestionnaireID, qw.WindowOpenOffsetDays, qw.WindowCloseOffsetDays, ar.Name as Reason
                              FROM
                              QuestionnaireWindow qw
                              JOIN info.AssessmentReason ar ON ar.AssessmentReasonID = qw.AssessmentReasonID
                              WHERE ar.Name IN('Initial','discharge') AND qw.QuestionnaireID IN(select distinct QuestionnaireID from PersonInHelpColb)
                       )
				  ,SelectedAssessments AS
                       (
							SELECT PC.*, wo_init.WindowOpenOffsetDays,wo_disc.WindowCloseOffsetDays ,A.assessmentID,A.DateTaken,PHC.QuestionnaireID,
							DATEADD(DAY, 0 - ISNULL (wo_init.WindowOpenOffsetDays, 0), CAST(PC.CollaborationStartDate AS DATE)) AS Enrolldate,
							DATEADD(DAY, ISNULL (wo_disc.WindowCloseOffsetDays, 0), ISNULL(CAST(PC.CollaborationEndDate AS DATE), CAST(GETDATE() AS DATE)))	 AS Enddate
							   FROM PersonInHelpColb PHC
							    JOIn Assessment A ON A.PersonQuestionnaireID = PHC.PersonQuestionnaireID 
							    JOIn PersonColb PC ON PC.PersonID = PHC.PersonID
								AND a.IsRemoved = 0 {assessmentStatusCondition}
							 LEFT JOIN(SELECT * FROM WindowOffsets WHERE Reason = 'Initial')wo_init ON PHC.QuestionnaireID = wo_init.QuestionnaireID
							 LEFT JOIN(SELECT * FROM WindowOffsets WHERE Reason = 'Discharge')wo_disc ON wo_disc.QuestionnaireID = PHC.QuestionnaireID
		   	          ) SELECT distinct SA.AssessmentID
							 --SA.PersonID,SA.AssessmentID,SA.Datetaken,SA.Questionnaireid
						    FROM SelectedAssessments SA 
							join PersonAssessmentMetrics pqm WITH (NOLOCK) ON pqm.AssessmentID = SA.AssessmentID
							WHERE 	
						    	CAST(SA.DateTaken AS Date)
						    	BETWEEN SA.EnrollDate AND SA.EndDate";
                var resultIDs = ExecuteSqlQuery(query, x => new string(x["AssessmentID"].ToString())).Distinct();
                return resultIDs.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<string> GetHelperAllMetricsInCollaboration(string assessmentIDs)
        {
            try
            {
                var assessmentStatus = this._assessmentStatusRepository.GetAllAssessmentStatus();
                var submittedStatus = assessmentStatus.Where(x => x.Name == PCISEnum.AssessmentStatus.Submitted).ToList()[0].AssessmentStatusID;
                var approvedStatus = assessmentStatus.Where(x => x.Name == PCISEnum.AssessmentStatus.Approved).ToList()[0].AssessmentStatusID;
                var returnedStatus = assessmentStatus.Where(x => x.Name == PCISEnum.AssessmentStatus.Returned).ToList()[0].AssessmentStatusID;
                var assessmentStatusCondition = $@"AND SA.AssessmentStatusID in ({returnedStatus},{submittedStatus},{approvedStatus})";
                var query = @$";WITH AssessmentInCols AS
				     ( SELECT * FROM (
						    SELECT ROW_NUMBER() OVER (PARTITION BY pqm.PersonID, pqm.QuestionnaireID ORDER BY SA.DateTaken DESC)  AS RowNumber,
							 pqm.PersonID,pqm.AssessmentID,SA.Datetaken,pqm.Questionnaireid
						    FROM Assessment SA 
							join PersonAssessmentMetrics pqm WITH (NOLOCK) ON pqm.AssessmentID = SA.AssessmentID
							WHERE 	1=1 {assessmentStatusCondition} AND SA.AssessmentID IN ({assessmentIDs})
					    )A WHERE RowNumber = 1
				    )
				   ,AssessmentMetricsIDs AS (
								SELECT 
								ROW_NUMBER() OVER(PARTITION BY pqm.PersonID,pqm.ItemID,pqm.QuestionnaireID ORDER BY A.DateTaken desc ,PersonAssessmentMetricsID desc) AS RowNumber,
								pqm.PersonAssessmentMetricsID,A.DateTaken, Pqm.ItemID,pqm.assessmentID
								from AssessmentInCols A  WITH (NOLOCK)
								JOIN PersonAssessmentMetrics pqm ON pqm.AssessmentID = A.AssessmentID
								and pqm.PersonID=A.PersonID
					) SELECT distinct PersonAssessmentMetricsID FROM AssessmentMetricsIDs  WHERE RowNumber = 1";
                var resultIDs = ExecuteSqlQuery(query, x => new string(x["PersonAssessmentMetricsID"].ToString())).Distinct();
                return resultIDs.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
