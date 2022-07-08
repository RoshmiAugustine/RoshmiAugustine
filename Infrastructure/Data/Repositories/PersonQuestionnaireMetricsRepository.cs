// -----------------------------------------------------------------------
// <copyright file="PersonQuestionnaireMetricsRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using AutoMapper;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.DTO.Input;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Infrastructure.Data.Repositories.Base;
using Opeeka.PICS.Infrastructure.Enums;
using System.Threading.Tasks;
using System.Linq;

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{
    // WARNING: Don't add methods for Data Insertion, Updation or Deletion in this repository, using OpeekaDBContext
    public class PersonQuestionnaireMetricsRepository : BaseRepository<PersonQuestionnaireMetrics>, IPersonQuestionnaireMetricsRepository
    {
        private readonly IMapper mapper;
        private readonly OpeekaDBContext _dbContext;
		private readonly IHelperRepository helperRepository;

        public PersonQuestionnaireMetricsRepository(OpeekaDBContext dbContext, IMapper mapper, IHelperRepository helperRepository)
            : base(dbContext)
        {
            this.mapper = mapper;
            this._dbContext = dbContext;
			this.helperRepository = helperRepository;
        }

		// <summary>
		/// Get data for Dashboard Needs metrics
		/// </summary>
		/// <param name="needMetricsSearchDTO"></param>
		/// <param name="queryBuilderDTO"></param>
		/// <returns>Tuple<List<DashboardNeedMetricsDTO>, int></returns>
		public Tuple<List<DashboardNeedMetricsDTO>, int> GetDashboardNeedMetrics(NeedMetricsSearchDTO needMetricsSearchDTO, DynamicQueryBuilderDTO queryBuilderDTO)
		{
			try
			{
				List<DashboardNeedMetricsDTO> dashboardNeedMetricsDTO = new List<DashboardNeedMetricsDTO>();
				string helperColbQueryCondition = string.Empty;
				var LastassessmentsQueryForHelperInColb = string.Empty;
				if (needMetricsSearchDTO.role != PCISEnum.Roles.SuperAdmin)
				{
					List<string> personIdList = this.helperRepository.GetHelperPersonInCollaborationDetails(needMetricsSearchDTO.userID, needMetricsSearchDTO.agencyID);
					if (personIdList.Count > 0)
					{
						var personIDs = string.Join(",", personIdList.ToArray());

						helperColbQueryCondition = $@"AND Pqm.PersonID NOT IN ({personIDs})";
						List<string> assessmentsInColbratn = GetHelperMetricsInCollaboration(personIDs, needMetricsSearchDTO.agencyID, needMetricsSearchDTO.userID);
						string assessmentMetricsIDS = assessmentsInColbratn.Count > 0 ? string.Join(",", assessmentsInColbratn.ToArray()) : "0";
						LastassessmentsQueryForHelperInColb = $@"UNION
							SELECT
							pqm.PersonID,			
							pqm.ItemID,
							pqm.InstrumentID,pqm.QuestionnaireID,
							case when isnull(pqm.NeedsAddressing,0)=0 and  isnull(pqm.NeedsEver,0)>0 then 1 else isnull(pqm.NeedsAddressing,0) end as helping,
							case when isnull(pqm.NeedsAddressing,0)=0 and  isnull(pqm.NeedsEver,0)>0 and  isnull(pqm.NeedsAddressed,0)=0 then 1 else isnull(pqm.NeedsAddressed,0) end as improved
							FROM PersonAssessmentMetrics pqm WITH (NOLOCK) WHERE pqm.PersonAssessmentMetricsID IN ({assessmentMetricsIDS})";
						if(needMetricsSearchDTO.role == PCISEnum.Roles.Supervisor || needMetricsSearchDTO.role == PCISEnum.Roles.HelperRO || needMetricsSearchDTO.role == PCISEnum.Roles.HelperRW || needMetricsSearchDTO.isSameAsLoggedInUser || needMetricsSearchDTO.role == PCISEnum.Roles.Assessor)
                        {
							LastassessmentsQueryForHelperInColb += " AND pqm.NeedsEver>0";
						}
					}
				}
				string query = string.Empty;
				if (needMetricsSearchDTO.isSameAsLoggedInUser)
				{
					query = @$"WITH HelperList	AS
							(
							   SELECT
		                            h.HelperID
	                            FROM
	                            Helper h
	                            WHERE H.IsRemoved=0 AND h.AgencyID={needMetricsSearchDTO.agencyID.ToString()} 
                                AND (h.HelperID={needMetricsSearchDTO.helperID.ToString()}) --FOR HELPER only
							)
							,PersonUnderHelpers As
							(
							    SELECT 
			                        DISTINCT p.PersonID
		                        FROM 
								PersonHelper ph 
								JOIn HelperList hl on ph.HelperID = hl.HelperID
		                        JOIN Person p ON p.PersonID=ph.PersonID
		                        WHERE p.IsRemoved=0 AND p.IsActive=1 AND p.AgencyID= {needMetricsSearchDTO.agencyID.ToString()} AND ph.IsRemoved=0
                                AND CAST(GETDATE() AS DATE) BETWEEN CAST(ph.StartDate AS DATE) AND ISNULL(CAST(ph.EndDate AS DATE), CAST(GETDATE() AS DATE))
							),
							DistinctPersonQuestionnaireMetrics as
							(
								SELECT PersonQuestionnaireMetricsID FROM(
								SELECT 
								ROW_NUMBER() OVER(PARTITIon BY pqm.PersonID,pqm.ItemID ORDER BY pqm.ItemID) AS RowNumber, pqm.PersonQuestionnaireMetricsID
								from PersonQuestionnaireMetrics pqm WITH (NOLOCK)
								JOIN PersonUnderHelpers p ON pqm.PersonID=p.PersonID
									WHERE pqm.NeedsEver>0 {helperColbQueryCondition}) as A 
								WHERE A.RowNumber = 1
							)
							,CalculatedValues AS 
							(
								SELECT
									pqm.PersonID,			
									pqm.ItemID,
									pqm.InstrumentID,0 As QuestionnaireID,
									case when isnull(pqm.NeedsAddressing,0)=0 and  isnull(pqm.NeedsEver,0)>0 then 1 else isnull(pqm.NeedsAddressing,0) end as helping,
									case when isnull(pqm.NeedsAddressing,0)=0 and  isnull(pqm.NeedsEver,0)>0 and  isnull(pqm.NeedsAddressed,0)=0 then 1 else isnull(pqm.NeedsAddressed,0) end as improved
								FROM 
								PersonQuestionnaireMetrics pqm
								join DistinctPersonQuestionnaireMetrics dpqm on dpqm.PersonQuestionnaireMetricsID=pqm.PersonQuestionnaireMetricsID
								JOIN PersonUnderHelpers p ON pqm.PersonID=p.PersonID
								WHERE pqm.NeedsEver>0  {helperColbQueryCondition}
							   {LastassessmentsQueryForHelperInColb}
							), CTE AS
                            (
	                            SELECT 
		                            ItemID,
		                            InstrumentID,
		                            SUM(isnull(helping,0)) [Helping],
		                            SUM(isnull(improved,0)) [Improved]
	                            FROM 
	                            CalculatedValues
	                            GROUP BY ItemID,InstrumentID
                            )                           
                            SELECT COUNT(CTE.ItemID) OVER() AS TotalCount,
	                            ROW_NUMBER() OVER(ORDER BY Helping DESC) [Top],
	                            i.Label [Item],
	                            ins.Abbrev [Instrument],ins.Name AS [InstrumentName],
	                            CTE.*
                            FROM
                            CTE 
                            INNER JOIN Item i ON i.ItemID=CTE.ItemID
                            INNER JOIN info.Instrument ins ON ins.InstrumentID=CTE.InstrumentID
                            WHERE (Helping>0 OR Improved>0) ";
                }
                else
                {
                    if (needMetricsSearchDTO.role == PCISEnum.Roles.SuperAdmin)
                    {
                        query = @$";WITH 
							DistinctPersonQuestionnaireMetrics as
							(
								SELECT PersonQuestionnaireMetricsID FROM(
								SELECT 
								ROW_NUMBER() OVER(PARTITIon BY pqm.PersonID,pqm.ItemID ORDER BY pqm.ItemID) AS RowNumber, pqm.PersonQuestionnaireMetricsID
								from PersonQuestionnaireMetrics pqm WITH (NOLOCK)
								JOIN  dbo.Person  p ON pqm.PersonID=p.PersonID
									WHERE pqm.NeedsEver>0 and p.IsRemoved=0 AND p.IsActive=1 AND p.AgencyID={needMetricsSearchDTO.agencyID.ToString()}  ) as A 
								WHERE A.RowNumber = 1
							), CalculatedValues AS 
							(
							SELECT
							pqm.PersonID,			
							pqm.ItemID,
							pqm.InstrumentID,
							case when isnull(pqm.NeedsAddressing,0)=0 and  isnull(pqm.NeedsEver,0)>0 then 1 else isnull(pqm.NeedsAddressing,0) end as helping,
							case when isnull(pqm.NeedsAddressing,0)=0 and  isnull(pqm.NeedsEver,0)>0 and  isnull(pqm.NeedsAddressed,0)=0 then 1 else isnull(pqm.NeedsAddressed,0) end as improved
							FROM 
							PersonQuestionnaireMetrics pqm
							join DistinctPersonQuestionnaireMetrics dpqm on dpqm.PersonQuestionnaireMetricsID=pqm.PersonQuestionnaireMetricsID
							), CTE AS
                            (
	                            SELECT 
		                            ItemID,
		                            InstrumentID,
		                            SUM(isnull(helping,0)) [Helping],
		                            SUM(isnull(improved,0)) [Improved]
	                            FROM 
	                            CalculatedValues
	                            GROUP BY ItemID,InstrumentID
                            )                            
                            SELECT COUNT(CTE.ItemID) OVER() AS TotalCount,
	                            ROW_NUMBER() OVER(ORDER BY Helping DESC) [Top],
	                            i.Label [Item],
	                            ins.Abbrev [Instrument],ins.Name AS [InstrumentName],
	                            CTE.*
                            FROM
                            CTE 
                            INNER JOIN Item i ON i.ItemID=CTE.ItemID
                            INNER JOIN info.Instrument ins ON ins.InstrumentID=CTE.InstrumentID
                            WHERE (Helping>0 OR Improved>0) ";
					}
					else if (needMetricsSearchDTO.role == PCISEnum.Roles.OrgAdminRO || needMetricsSearchDTO.role == PCISEnum.Roles.OrgAdminRW)
					{
						query = @$";WITH 
							DistinctPersonQuestionnaireMetrics as
							(
								SELECT PersonQuestionnaireMetricsID FROM(
								SELECT 
								ROW_NUMBER() OVER(PARTITIon BY pqm.PersonID,pqm.ItemID ORDER BY pqm.ItemID) AS RowNumber, pqm.PersonQuestionnaireMetricsID
								from PersonQuestionnaireMetrics pqm WITH (NOLOCK)
								JOIN  dbo.Person  p ON pqm.PersonID=p.PersonID
									WHERE pqm.NeedsEver>0 and p.IsRemoved=0 AND p.IsActive=1 AND p.AgencyID={needMetricsSearchDTO.agencyID.ToString()} {helperColbQueryCondition} ) as A 
								WHERE A.RowNumber = 1
							), CalculatedValues AS 
							(
							SELECT
							pqm.PersonID,			
							pqm.ItemID,
							pqm.InstrumentID,0 As QuestionnaireID,
							case when isnull(pqm.NeedsAddressing,0)=0 and  isnull(pqm.NeedsEver,0)>0 then 1 else isnull(pqm.NeedsAddressing,0) end as helping,
							case when isnull(pqm.NeedsAddressing,0)=0 and  isnull(pqm.NeedsEver,0)>0 and  isnull(pqm.NeedsAddressed,0)=0 then 1 else isnull(pqm.NeedsAddressed,0) end as improved
							FROM 
							PersonQuestionnaireMetrics pqm
							join DistinctPersonQuestionnaireMetrics dpqm on dpqm.PersonQuestionnaireMetricsID=pqm.PersonQuestionnaireMetricsID
							WHERE 1= 1	{helperColbQueryCondition}
							   {LastassessmentsQueryForHelperInColb}
							), CTE AS
                            (
	                            SELECT 
		                            ItemID,
		                            InstrumentID,
		                            SUM(isnull(helping,0)) [Helping],
		                            SUM(isnull(improved,0)) [Improved]
	                            FROM 
	                            CalculatedValues
	                            GROUP BY ItemID,InstrumentID
                            )                            
                            SELECT COUNT(CTE.ItemID) OVER() AS TotalCount,
	                            ROW_NUMBER() OVER(ORDER BY Helping DESC) [Top],
	                            i.Label [Item],
	                            ins.Abbrev [Instrument],ins.Name AS [InstrumentName],
	                            CTE.*
                            FROM
                            CTE 
                            INNER JOIN Item i ON i.ItemID=CTE.ItemID
                            INNER JOIN info.Instrument ins ON ins.InstrumentID=CTE.InstrumentID
                            WHERE (Helping>0 OR Improved>0) ";
					}
					else if (needMetricsSearchDTO.role == PCISEnum.Roles.Supervisor)
					{
						query = @$";WITH SupervisorHierarchy AS
                            (
	                           SELECT
							     h.HelperID
							     FROM
							     Helper h	 	 
							     WHERE H.IsRemoved=0 AND h.AgencyID={needMetricsSearchDTO.agencyID.ToString()}
							        AND (h.HelperID={needMetricsSearchDTO.helperID.ToString()} OR h.SupervisorHelperID={needMetricsSearchDTO.helperID.ToString()})
							  UNION ALL
							  SELECT
								 H1.HelperID
							     FROM Helper H1 
								 INNER JOIN SupervisorHierarchy HL ON H1.SupervisorHelperID = HL.HelperID 
								 AND HL.HelperID <> {needMetricsSearchDTO.helperID.ToString()}
							     WHERE H1.IsRemoved=0 AND H1.AgencyID={needMetricsSearchDTO.agencyID.ToString()} 
                            )
                            ,HelperList	AS
							(
							  SELECT
								    h.HelperID
							    FROM Helper h WHERE h.AgencyID={needMetricsSearchDTO.agencyID.ToString()} AND h.IsRemoved=0 
	                            AND h.ReviewerID={needMetricsSearchDTO.helperID.ToString()}
								UNION
								SELECT * from SupervisorHierarchy
							)
							,PersonUnderHelpers As
							(
							    SELECT 
			                        DISTINCT p.PersonID
		                        FROM 
								PersonHelper ph 
								JOIn HelperList hl on ph.HelperID = hl.HelperID
		                        JOIN Person p ON p.PersonID=ph.PersonID
		                        WHERE p.IsRemoved=0 AND p.IsActive=1 AND p.AgencyID={needMetricsSearchDTO.agencyID.ToString()} AND ph.IsRemoved=0
                                AND CAST(GETDATE() AS DATE) BETWEEN CAST(ph.StartDate AS DATE) AND ISNULL(CAST(ph.EndDate AS DATE), CAST(GETDATE() AS DATE))
							),
							DistinctPersonQuestionnaireMetrics as
							(
								SELECT PersonQuestionnaireMetricsID FROM(
								SELECT 
								ROW_NUMBER() OVER(PARTITIon BY pqm.PersonID,pqm.ItemID ORDER BY pqm.ItemID) AS RowNumber, pqm.PersonQuestionnaireMetricsID
								from PersonQuestionnaireMetrics pqm WITH (NOLOCK)
								JOIN PersonUnderHelpers p ON pqm.PersonID=p.PersonID
									WHERE pqm.NeedsEver>0 {helperColbQueryCondition}) as A 
								WHERE A.RowNumber = 1
							),CalculatedValues AS 
							(
								SELECT
									pqm.PersonID,			
									pqm.ItemID,
									pqm.InstrumentID,0 As QuestionnaireID,
									case when isnull(pqm.NeedsAddressing,0)=0 and  isnull(pqm.NeedsEver,0)>0 then 1 else isnull(pqm.NeedsAddressing,0) end as helping,
									case when isnull(pqm.NeedsAddressing,0)=0 and  isnull(pqm.NeedsEver,0)>0 and  isnull(pqm.NeedsAddressed,0)=0 then 1 else isnull(pqm.NeedsAddressed,0) end as improved
								FROM 
								PersonQuestionnaireMetrics pqm
								join DistinctPersonQuestionnaireMetrics dpqm on dpqm.PersonQuestionnaireMetricsID=pqm.PersonQuestionnaireMetricsID
								JOIN PersonUnderHelpers p ON pqm.PersonID=p.PersonID
								WHERE pqm.NeedsEver>0  {helperColbQueryCondition}
							   {LastassessmentsQueryForHelperInColb}
							), CTE AS
                            (
	                            SELECT 
		                            ItemID,
		                            InstrumentID,
		                            SUM(isnull(helping,0)) [Helping],
		                            SUM(isnull(improved,0)) [Improved]
	                            FROM 
	                            CalculatedValues
	                            GROUP BY ItemID,InstrumentID
                            )                            
                            SELECT COUNT(CTE.ItemID) OVER() AS TotalCount,
	                            ROW_NUMBER() OVER(ORDER BY Helping DESC) [Top],
	                            i.Label [Item],
	                            ins.Abbrev [Instrument],ins.Name [InstrumentName],
	                            CTE.*
                            FROM
                            CTE 
                            INNER JOIN Item i ON i.ItemID=CTE.ItemID
                            INNER JOIN info.Instrument ins ON ins.InstrumentID=CTE.InstrumentID
                            WHERE (Helping>0 OR Improved>0) ";
					}
					else if (needMetricsSearchDTO.role == PCISEnum.Roles.HelperRO || needMetricsSearchDTO.role == PCISEnum.Roles.HelperRW || needMetricsSearchDTO.role == PCISEnum.Roles.Assessor)
					{
						query = @$"WITH HelperList	AS
							(
							   SELECT
		                            h.HelperID
	                            FROM
	                            Helper h
	                            WHERE H.IsRemoved=0 AND h.AgencyID={needMetricsSearchDTO.agencyID.ToString()} 
                                AND (h.HelperID={needMetricsSearchDTO.helperID.ToString()} OR h.ReviewerID={needMetricsSearchDTO.helperID.ToString()}) --FOR HELPER And ReviwerHelpers
							)
							,PersonUnderHelpers As
							(
							    SELECT 
			                        DISTINCT p.PersonID
		                        FROM 
								PersonHelper ph 
								JOIn HelperList hl on ph.HelperID = hl.HelperID
		                        JOIN Person p ON p.PersonID=ph.PersonID
		                        WHERE p.IsRemoved=0 AND p.IsActive=1 AND p.AgencyID= {needMetricsSearchDTO.agencyID.ToString()}  AND ph.IsRemoved=0
                                AND CAST(GETDATE() AS DATE) BETWEEN CAST(ph.StartDate AS DATE) AND ISNULL(CAST(ph.EndDate AS DATE), CAST(GETDATE() AS DATE))
							),
							DistinctPersonQuestionnaireMetrics as
							(
								SELECT PersonQuestionnaireMetricsID FROM(
								SELECT 
								ROW_NUMBER() OVER(PARTITIon BY pqm.PersonID,pqm.ItemID ORDER BY pqm.ItemID) AS RowNumber, pqm.PersonQuestionnaireMetricsID
								from PersonQuestionnaireMetrics pqm WITH (NOLOCK)
								JOIN PersonUnderHelpers p ON pqm.PersonID=p.PersonID
									WHERE pqm.NeedsEver>0 {helperColbQueryCondition}) as A 
								WHERE A.RowNumber = 1
							),CalculatedValues AS 
							(
								SELECT
									pqm.PersonID,			
									pqm.ItemID,
									pqm.InstrumentID,0 As QuestionnaireID,
									case when isnull(pqm.NeedsAddressing,0)=0 and  isnull(pqm.NeedsEver,0)>0 then 1 else isnull(pqm.NeedsAddressing,0) end as helping,
									case when isnull(pqm.NeedsAddressing,0)=0 and  isnull(pqm.NeedsEver,0)>0 and  isnull(pqm.NeedsAddressed,0)=0 then 1 else isnull(pqm.NeedsAddressed,0) end as improved
								FROM 
								PersonQuestionnaireMetrics pqm
								join DistinctPersonQuestionnaireMetrics dpqm on dpqm.PersonQuestionnaireMetricsID=pqm.PersonQuestionnaireMetricsID
								JOIN PersonUnderHelpers p ON pqm.PersonID=p.PersonID
								WHERE pqm.NeedsEver>0 {helperColbQueryCondition}
							   {LastassessmentsQueryForHelperInColb}
							), CTE AS
                            (
	                            SELECT 
		                            ItemID,
		                            InstrumentID,
		                            SUM(isnull(helping,0)) [Helping],
		                            SUM(isnull(improved,0)) [Improved]
	                            FROM 
	                            CalculatedValues
	                            GROUP BY ItemID,InstrumentID
                            )                            
                            SELECT COUNT(CTE.ItemID) OVER() AS TotalCount,
	                            ROW_NUMBER() OVER(ORDER BY Helping DESC) [Top],
	                            i.Label [Item],
	                            ins.Abbrev [Instrument],ins.Name AS [InstrumentName],
	                            CTE.*
                            FROM
                            CTE 
                            INNER JOIN Item i ON i.ItemID=CTE.ItemID
                            INNER JOIN info.Instrument ins ON ins.InstrumentID=CTE.InstrumentID
                            WHERE (Helping>0 OR Improved>0) ";
					}
				}
				query += queryBuilderDTO.WhereCondition + queryBuilderDTO.OrderBy + queryBuilderDTO.Paginate;
				int totalCount = 0;
				dashboardNeedMetricsDTO = ExecuteSqlQuery(query, x =>
				{
					totalCount = x["TotalCount"] == DBNull.Value ? 0 : (int)x["TotalCount"];
					return new DashboardNeedMetricsDTO
					{
						Top = x["Top"] == DBNull.Value ? 0 : (long)x["Top"],
						ItemID = x["ItemID"] == DBNull.Value ? 0 : (int)x["ItemID"],
						InstrumentID = x["InstrumentID"] == DBNull.Value ? 0 : (int)x["InstrumentID"],
						Item = x["Item"] == DBNull.Value ? null : (string)x["Item"],
						Instrument = x["Instrument"] == DBNull.Value ? null : (string)x["Instrument"],
						Helping = x["Helping"] == DBNull.Value ? 0 : (int)x["Helping"],
						Improved = x["Improved"] == DBNull.Value ? 0 : (int)x["Improved"],
						Instrument_title = x["InstrumentName"] == DBNull.Value ? null : (string)x["InstrumentName"],
					};
				}, queryBuilderDTO.QueryParameterDTO);
				return Tuple.Create(dashboardNeedMetricsDTO, totalCount);
			}
			catch (Exception)
			{
				throw;
			}
		}

		/// <summary>
		/// Get data for Dashboard Strengths metrics
		/// </summary>
		/// <param name="strengthMetricsSearchDTO"></param>
		/// <param name="queryBuilderDTO"></param>
		/// <returns>List<DashboardStrengthMetricsDTO></returns>
		public Tuple<List<DashboardStrengthMetricsDTO>, int> GetDashboardStrengthMetrics(StrengthMetricsSearchDTO strengthMetricsSearchDTO, DynamicQueryBuilderDTO queryBuilderDTO)
		{
			try
			{
				List<DashboardStrengthMetricsDTO> dashboardStrengthMetricsDTO = new List<DashboardStrengthMetricsDTO>();
				string query = string.Empty;
				string helperColbQueryCondition = string.Empty;
				string LastassessmentsQueryForHelperInColb = string.Empty; 
				if (strengthMetricsSearchDTO.role != PCISEnum.Roles.SuperAdmin)
				{
					var personIdList = this.helperRepository.GetHelperPersonInCollaborationDetails(strengthMetricsSearchDTO.userID, strengthMetricsSearchDTO.agencyID);
					if (personIdList.Count > 0)
					{
						string personIDs = string.Join(",", personIdList.ToArray());

						helperColbQueryCondition = $@"AND Pqm.PersonID NOT IN ({personIDs})";
						List<string> assessmentsInColbratn = GetHelperMetricsInCollaboration(personIDs, strengthMetricsSearchDTO.agencyID, strengthMetricsSearchDTO.userID);
						string assessmentMetricsIDS = assessmentsInColbratn.Count > 0 ? string.Join(",", assessmentsInColbratn.ToArray()) : "0";
						LastassessmentsQueryForHelperInColb = $@"UNION
							SELECT
							pqm.PersonID,			
							pqm.ItemID,
							pqm.InstrumentID,pqm.QuestionnaireID,
							case when isnull(pqm.StrengthsBuilding,0)=0 and isnull(pqm.StrengthsEver,0)>0 then 1 else isnull(pqm.StrengthsBuilding,0) end as helping,
							case when isnull(pqm.StrengthsBuilding,0)=0 and isnull(pqm.StrengthsEver,0)>0 and isnull(pqm.StrengthsBuilt,0)=0 then 1 else isnull(pqm.StrengthsBuilt,0) end as improved
							FROM PersonAssessmentMetrics pqm WITH (NOLOCK) WHERE pqm.PersonAssessmentMetricsID IN ({assessmentMetricsIDS})";
						if (strengthMetricsSearchDTO.role == PCISEnum.Roles.Supervisor || strengthMetricsSearchDTO.role == PCISEnum.Roles.HelperRO || strengthMetricsSearchDTO.role == PCISEnum.Roles.HelperRW || strengthMetricsSearchDTO.isSameAsLoggedInUser || strengthMetricsSearchDTO.role == PCISEnum.Roles.Assessor)
						{
							LastassessmentsQueryForHelperInColb += " AND pqm.StrengthsEver>0";
						}
					}
				}
				if (strengthMetricsSearchDTO.isSameAsLoggedInUser)
				{
					query = @$";WITH HelperList	AS
							(
							   SELECT
		                            h.HelperID
	                            FROM
	                            Helper h
	                            WHERE H.IsRemoved=0 AND h.AgencyID={strengthMetricsSearchDTO.agencyID.ToString()} 
                                AND (h.HelperID={strengthMetricsSearchDTO.helperID.ToString()}) --FOR HELPER only
							)
							,PersonUnderHelpers As
							(
							    SELECT 
			                        DISTINCT p.PersonID
		                        FROM 
								PersonHelper ph 
								JOIn HelperList hl on ph.HelperID = hl.HelperID
		                        JOIN Person p ON p.PersonID=ph.PersonID
		                        WHERE p.IsRemoved=0 AND p.IsActive=1 AND p.AgencyID={strengthMetricsSearchDTO.agencyID.ToString()}  AND ph.IsRemoved=0
                                AND CAST(GETDATE() AS DATE) BETWEEN CAST(ph.StartDate AS DATE) AND ISNULL(CAST(ph.EndDate AS DATE), CAST(GETDATE() AS DATE))
							),
							DistinctPersonQuestionnaireMetrics as
							(
								SELECT PersonQuestionnaireMetricsID FROM(
								SELECT 
								ROW_NUMBER() OVER(PARTITIon BY pqm.PersonID,pqm.ItemID ORDER BY pqm.ItemID) AS RowNumber, pqm.PersonQuestionnaireMetricsID
								from PersonQuestionnaireMetrics pqm WITH (NOLOCK)
								JOIN PersonUnderHelpers p ON pqm.PersonID=p.PersonID
									WHERE pqm.StrengthsEver>0 {helperColbQueryCondition}) as A 
								WHERE A.RowNumber = 1
							), CalculatedValues AS 
							(
								SELECT
								pqm.PersonID,			
								pqm.ItemID,
								pqm.InstrumentID,0 AS QuestionnaireID,
								case when isnull(pqm.StrengthsBuilding,0)=0 and isnull(pqm.StrengthsEver,0)>0 then 1 else isnull(pqm.StrengthsBuilding,0) end as helping,
								case when isnull(pqm.StrengthsBuilding,0)=0 and isnull(pqm.StrengthsEver,0)>0 and isnull(pqm.StrengthsBuilt,0)=0 then 1 else isnull(pqm.StrengthsBuilt,0) end as improved
								FROM 
								PersonQuestionnaireMetrics pqm
								join DistinctPersonQuestionnaireMetrics dpqm on dpqm.PersonQuestionnaireMetricsID=pqm.PersonQuestionnaireMetricsID
								JOIN PersonUnderHelpers  p ON pqm.PersonID=p.PersonID
								WHERE pqm.StrengthsEver>0 {helperColbQueryCondition}
							   {LastassessmentsQueryForHelperInColb}
							), CTE AS
                            (
								Select 
	                            ItemID,
		                        InstrumentID,
		                        SUM(isnull(helping,0)) as [Helping],
		                        SUM(isnull(improved,0)) as [Improved]
								FROM 
								CalculatedValues
	                            GROUP BY ItemID,InstrumentID
                            )                           
                            SELECT COUNT(CTE.ItemID) OVER() AS TotalCount,
	                            ROW_NUMBER() OVER(ORDER BY Helping DESC)[Top],
	                            i.Label [Item],
	                            ins.Abbrev [Instrument],ins.Name AS [InstrumentName],
	                            CTE.*
                            FROM
                            CTE 
                            INNER JOIN Item i ON i.ItemID=CTE.ItemID
                            INNER JOIN info.Instrument ins ON ins.InstrumentID=CTE.InstrumentID
                            WHERE (Helping>0 OR Improved>0) ";
                }
                else
                {
                    if (strengthMetricsSearchDTO.role == PCISEnum.Roles.SuperAdmin)
                    {
                        query = @$";WITH  
							DistinctPersonQuestionnaireMetrics as
							(
								SELECT PersonQuestionnaireMetricsID FROM(
								SELECT 
								ROW_NUMBER() OVER(PARTITIon BY pqm.PersonID,pqm.ItemID ORDER BY pqm.ItemID) AS RowNumber, pqm.PersonQuestionnaireMetricsID
								from PersonQuestionnaireMetrics pqm WITH (NOLOCK)JOIN dbo.Person  p ON pqm.PersonID=p.PersonID
								WHERE p.IsRemoved=0 AND p.IsActive=1 AND p.AgencyID={strengthMetricsSearchDTO.agencyID.ToString()} and pqm.StrengthsEver>0 ) as A 
								WHERE A.RowNumber = 1
							)
							,CalculatedValues AS 
							(
							SELECT
							pqm.PersonID,			
							pqm.ItemID,
							pqm.InstrumentID,
							case when isnull(pqm.StrengthsBuilding,0)=0 and isnull(pqm.StrengthsEver,0)>0 then 1 else isnull(pqm.StrengthsBuilding,0) end as helping,
							case when isnull(pqm.StrengthsBuilding,0)=0 and isnull(pqm.StrengthsEver,0)>0 and isnull(pqm.StrengthsBuilt,0)=0 then 1 else isnull(pqm.StrengthsBuilt,0) end as improved
							FROM 
							PersonQuestionnaireMetrics pqm
							join DistinctPersonQuestionnaireMetrics dpqm on dpqm.PersonQuestionnaireMetricsID=pqm.PersonQuestionnaireMetricsID
							), Result as
                            (
	                            SELECT
		                            ItemID,
		                            InstrumentID,
		                            SUM(isnull(helping,0)) as [Helping],
		                            SUM(isnull(improved,0)) as [Improved]
									FROM 
									CalculatedValues
	                            GROUP BY ItemID,InstrumentID
                            )                            
                            SELECT  COUNT(R.ItemID) OVER() AS TotalCount,
	                                ROW_NUMBER() OVER(ORDER BY Helping DESC)[Top],
	                                i.Label [Item],
	                                ins.Abbrev [Instrument],ins.Name AS [InstrumentName],
	                                R.*
                            FROM
                            Result R
                            INNER JOIN Item i ON i.ItemID=R.ItemID
                            INNER JOIN info.Instrument ins ON ins.InstrumentID=R.InstrumentID
                            WHERE (Helping>0 OR Improved>0) ";
					}
					else if (strengthMetricsSearchDTO.role == PCISEnum.Roles.OrgAdminRO || strengthMetricsSearchDTO.role == PCISEnum.Roles.OrgAdminRW)
					{
						query = @$";WITH  
							DistinctPersonQuestionnaireMetrics as
							(
								SELECT PersonQuestionnaireMetricsID FROM(
								SELECT 
								ROW_NUMBER() OVER(PARTITIon BY pqm.PersonID,pqm.ItemID ORDER BY pqm.ItemID) AS RowNumber, pqm.PersonQuestionnaireMetricsID
								from PersonQuestionnaireMetrics pqm WITH (NOLOCK)JOIN dbo.Person  p ON pqm.PersonID=p.PersonID
								WHERE p.IsRemoved=0 AND p.IsActive=1 AND p.AgencyID={strengthMetricsSearchDTO.agencyID.ToString()} and pqm.StrengthsEver>0 {helperColbQueryCondition} ) as A 
								WHERE A.RowNumber = 1
							)
							,CalculatedValues AS 
							(
							SELECT
							pqm.PersonID,			
							pqm.ItemID,
							pqm.InstrumentID,0 As QuestionnaireID,
							case when isnull(pqm.StrengthsBuilding,0)=0 and isnull(pqm.StrengthsEver,0)>0 then 1 else isnull(pqm.StrengthsBuilding,0) end as helping,
							case when isnull(pqm.StrengthsBuilding,0)=0 and isnull(pqm.StrengthsEver,0)>0 and isnull(pqm.StrengthsBuilt,0)=0 then 1 else isnull(pqm.StrengthsBuilt,0) end as improved
							FROM 
							PersonQuestionnaireMetrics pqm
							join DistinctPersonQuestionnaireMetrics dpqm on dpqm.PersonQuestionnaireMetricsID=pqm.PersonQuestionnaireMetricsID
							WHERE 1 = 1 {helperColbQueryCondition}
							{LastassessmentsQueryForHelperInColb}
							), Result as
                            (
	                            SELECT
		                            ItemID,
		                            InstrumentID,
		                            SUM(isnull(helping,0)) as [Helping],
		                            SUM(isnull(improved,0)) as [Improved]
									FROM 
									CalculatedValues
	                            GROUP BY ItemID,InstrumentID
                            )                            
                            SELECT  COUNT(R.ItemID) OVER() AS TotalCount,
	                                ROW_NUMBER() OVER(ORDER BY Helping DESC)[Top],
	                                i.Label [Item],
	                                ins.Abbrev [Instrument],ins.Name AS [InstrumentName],
	                                R.*
                            FROM
                            Result R
                            INNER JOIN Item i ON i.ItemID=R.ItemID
                            INNER JOIN info.Instrument ins ON ins.InstrumentID=R.InstrumentID
                            WHERE (Helping>0 OR Improved>0) ";
					}
					else if (strengthMetricsSearchDTO.role == PCISEnum.Roles.Supervisor)
					{
						query = @$";WITH SupervisorHierarchy AS
                            (
	                           SELECT
							     h.HelperID
							     FROM
							     Helper h	 	 
							     WHERE H.IsRemoved=0 AND h.AgencyID={strengthMetricsSearchDTO.agencyID.ToString()}
							        AND (h.HelperID={strengthMetricsSearchDTO.helperID.ToString()} OR h.SupervisorHelperID={strengthMetricsSearchDTO.helperID.ToString()})
							  UNION ALL
							  SELECT
								 H1.HelperID
							     FROM Helper H1 
								 INNER JOIN SupervisorHierarchy HL ON H1.SupervisorHelperID = HL.HelperID 
								 AND HL.HelperID <> {strengthMetricsSearchDTO.helperID.ToString()}
							     WHERE H1.IsRemoved=0 AND H1.AgencyID={strengthMetricsSearchDTO.agencyID.ToString()}
                            )
                            ,HelperList	AS
							(
							  SELECT
								    h.HelperID
							    FROM Helper h WHERE h.AgencyID={strengthMetricsSearchDTO.agencyID.ToString()} AND h.IsRemoved=0 
	                            AND h.ReviewerID= {strengthMetricsSearchDTO.helperID.ToString()}
								UNION
								SELECT * from SupervisorHierarchy
							)
							,PersonUnderHelpers As
							(
							    SELECT 
			                        DISTINCT p.PersonID
		                        FROM 
								PersonHelper ph 
								JOIn HelperList hl on ph.HelperID = hl.HelperID
		                        JOIN Person p ON p.PersonID=ph.PersonID
		                        WHERE p.IsRemoved=0 AND p.IsActive=1 AND p.AgencyID={strengthMetricsSearchDTO.agencyID.ToString()}  AND ph.IsRemoved=0
                                AND CAST(GETDATE() AS DATE) BETWEEN CAST(ph.StartDate AS DATE) AND ISNULL(CAST(ph.EndDate AS DATE), CAST(GETDATE() AS DATE)) 
							),
							DistinctPersonQuestionnaireMetrics as
							(
								SELECT PersonQuestionnaireMetricsID FROM(
								SELECT 
								ROW_NUMBER() OVER(PARTITIon BY pqm.PersonID,pqm.ItemID ORDER BY pqm.ItemID) AS RowNumber, pqm.PersonQuestionnaireMetricsID
								from PersonQuestionnaireMetrics pqm WITH (NOLOCK)
								JOIN PersonUnderHelpers p ON pqm.PersonID=p.PersonID
									WHERE pqm.StrengthsEver>0 {helperColbQueryCondition}) as A 
								WHERE A.RowNumber = 1
							), CalculatedValues AS 
							(
								SELECT
									pqm.PersonID,			
									pqm.ItemID,
									pqm.InstrumentID,0 As QuestionnaireID,
									case when isnull(pqm.StrengthsBuilding,0)=0 and isnull(pqm.StrengthsEver,0)>0 then 1 else isnull(pqm.StrengthsBuilding,0) end as helping,
									case when isnull(pqm.StrengthsBuilding,0)=0 and isnull(pqm.StrengthsEver,0)>0 and isnull(pqm.StrengthsBuilt,0)=0 then 1 else isnull(pqm.StrengthsBuilt,0) end as improved
								FROM 
								PersonQuestionnaireMetrics pqm
								join DistinctPersonQuestionnaireMetrics dpqm on dpqm.PersonQuestionnaireMetricsID=pqm.PersonQuestionnaireMetricsID
								JOIN PersonUnderHelpers  p ON pqm.PersonID=p.PersonID
								WHERE pqm.StrengthsEver > 0 {helperColbQueryCondition}
							    {LastassessmentsQueryForHelperInColb}
							),CTE AS
                            (
	                            SELECT
		                            ItemID,
		                            InstrumentID,
		                            SUM(isnull(helping,0)) as [Helping],
		                            SUM(isnull(improved,0)) as [Improved]
								FROM 
								CalculatedValues
	                            GROUP BY ItemID,InstrumentID
                            )                            
                            SELECT COUNT(CTE.ItemID) OVER() AS TotalCount,
	                                ROW_NUMBER() OVER(ORDER BY Helping DESC)[Top],
	                                i.Label [Item],
	                                ins.Abbrev [Instrument],ins.Name AS [InstrumentName],
	                                CTE.*
                            FROM
                            CTE 
                            INNER JOIN Item i ON i.ItemID=CTE.ItemID
                            INNER JOIN info.Instrument ins ON ins.InstrumentID=CTE.InstrumentID
                            WHERE (Helping>0 OR Improved>0) ";
					}
					else if (strengthMetricsSearchDTO.role == PCISEnum.Roles.HelperRO || strengthMetricsSearchDTO.role == PCISEnum.Roles.HelperRW || strengthMetricsSearchDTO.role == PCISEnum.Roles.Assessor)
					{
						query = @$"WITH HelperList	AS
							(
							   SELECT
		                            h.HelperID
	                            FROM
	                            Helper h
	                            WHERE H.IsRemoved=0 AND h.AgencyID={strengthMetricsSearchDTO.agencyID.ToString()}
                                AND (h.HelperID={strengthMetricsSearchDTO.helperID.ToString()} OR h.ReviewerID={strengthMetricsSearchDTO.helperID.ToString()}) --FOR HELPER And ReviwerHelpers
							)
							,PersonUnderHelpers As
							(
							    SELECT 
			                        DISTINCT p.PersonID
		                        FROM 
								PersonHelper ph 
								JOIn HelperList hl on ph.HelperID = hl.HelperID
		                        JOIN Person p ON p.PersonID=ph.PersonID
		                        WHERE p.IsRemoved=0 AND p.IsActive=1 AND p.AgencyID={strengthMetricsSearchDTO.agencyID.ToString()} AND ph.IsRemoved=0
                                AND CAST(GETDATE() AS DATE) BETWEEN CAST(ph.StartDate AS DATE) AND ISNULL(CAST(ph.EndDate AS DATE), CAST(GETDATE() AS DATE))
							),
							DistinctPersonQuestionnaireMetrics as
							(
								SELECT PersonQuestionnaireMetricsID FROM(
								SELECT 
								ROW_NUMBER() OVER(PARTITIon BY pqm.PersonID,pqm.ItemID ORDER BY pqm.ItemID) AS RowNumber, pqm.PersonQuestionnaireMetricsID
								from PersonQuestionnaireMetrics pqm WITH (NOLOCK)
								JOIN PersonUnderHelpers p ON pqm.PersonID=p.PersonID
									WHERE pqm.StrengthsEver>0 {helperColbQueryCondition}) as A 
								WHERE A.RowNumber = 1
							), CalculatedValues AS 
							(
								SELECT
									pqm.PersonID,			
									pqm.ItemID,
									pqm.InstrumentID,0 As QuestionnaireID,
									case when isnull(pqm.StrengthsBuilding,0)=0 and isnull(pqm.StrengthsEver,0)>0 then 1 else isnull(pqm.StrengthsBuilding,0) end as helping,
									case when isnull(pqm.StrengthsBuilding,0)=0 and isnull(pqm.StrengthsEver,0)>0 and isnull(pqm.StrengthsBuilt,0)=0 then 1 else isnull(pqm.StrengthsBuilt,0) end as improved
								FROM 
								PersonQuestionnaireMetrics pqm
								join DistinctPersonQuestionnaireMetrics dpqm on dpqm.PersonQuestionnaireMetricsID=pqm.PersonQuestionnaireMetricsID
								JOIN PersonUnderHelpers  p ON pqm.PersonID=p.PersonID
								WHERE pqm.StrengthsEver > 0 {helperColbQueryCondition}
							    {LastassessmentsQueryForHelperInColb}
							),CTE AS
                            (
	                            SELECT
		                            ItemID,
		                            InstrumentID,
		                            SUM(isnull(helping,0)) as [Helping],
		                            SUM(isnull(improved,0)) as [Improved]
								FROM 
								CalculatedValues
	                            GROUP BY ItemID,InstrumentID
                            )                           
                            SELECT COUNT(CTE.ItemID) OVER() AS TotalCount,
	                                ROW_NUMBER() OVER(ORDER BY Helping DESC)[Top],
	                                i.Label [Item],
	                                ins.Abbrev [Instrument],ins.Name AS [InstrumentName],
	                                CTE.*
                            FROM
                            CTE 
                            INNER JOIN Item i ON i.ItemID=CTE.ItemID
                            INNER JOIN info.Instrument ins ON ins.InstrumentID=CTE.InstrumentID
                            WHERE (Helping>0 OR Improved>0) ";
					}
				}
				query += queryBuilderDTO.WhereCondition + queryBuilderDTO.OrderBy + queryBuilderDTO.Paginate;
				int totalCount = 0;
				dashboardStrengthMetricsDTO = ExecuteSqlQuery(query, x =>
				{
					totalCount = x["TotalCount"] == DBNull.Value ? 0 : (int)x["TotalCount"];
					return new DashboardStrengthMetricsDTO
					{
						Top = x["Top"] == DBNull.Value ? 0 : (long)x["Top"],
						ItemID = x["ItemID"] == DBNull.Value ? 0 : (int)x["ItemID"],
						InstrumentID = x["InstrumentID"] == DBNull.Value ? 0 : (int)x["InstrumentID"],
						Item = x["Item"] == DBNull.Value ? null : (string)x["Item"],
						Instrument = x["Instrument"] == DBNull.Value ? null : (string)x["Instrument"],
						Helping = x["Helping"] == DBNull.Value ? 0 : (int)x["Helping"],
						Improved = x["Improved"] == DBNull.Value ? 0 : (int)x["Improved"],
						Instrument_title = x["InstrumentName"] == DBNull.Value ? null : (string)x["InstrumentName"],
					};
				}, queryBuilderDTO.QueryParameterDTO);
				return Tuple.Create(dashboardStrengthMetricsDTO, totalCount);
			}
			catch (Exception)
			{
				throw;
			}
		}

        private List<string> GetHelperMetricsInCollaboration(string personIDs, long agencyID, int userID)
        {
            try
            {
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
								AND a.IsRemoved = 0
							   	JOIN info.AssessmentStatus ast ON ast.AssessmentStatusID=a.AssessmentStatusID  AND ast.Name in	('Returned','Submitted','Approved')	
							 LEFT JOIN(SELECT * FROM WindowOffsets WHERE Reason = 'Initial')wo_init ON PHC.QuestionnaireID = wo_init.QuestionnaireID
							 LEFT JOIN(SELECT * FROM WindowOffsets WHERE Reason = 'Discharge')wo_disc ON wo_disc.QuestionnaireID = PHC.QuestionnaireID
		   	          )
				  ,AssessmentInCols AS
				     ( SELECT * FROM (
						    SELECT ROW_NUMBER() OVER (PARTITION BY SA.PersonID, sa.QuestionnaireID ORDER BY DateTaken DESC)  AS RowNumber,
							 SA.PersonID,SA.AssessmentID,SA.Datetaken,SA.Questionnaireid
						    FROM SelectedAssessments SA 
							JOIN PersonAssessmentMetrics pqm WITH (NOLOCK) ON pqm.AssessmentID = SA.AssessmentID
							WHERE 	
						    	CAST(SA.DateTaken AS Date)
						    	BETWEEN SA.EnrollDate AND SA.EndDate
					    )A WHERE RowNumber = 1
				    )
				   ,AssessmentMetricsIDs AS (
								SELECT 
								ROW_NUMBER() OVER(PARTITION BY pqm.PersonID,pqm.ItemID,pqm.QuestionnaireID ORDER BY A.DateTaken desc ,PersonAssessmentMetricsID desc) AS RowNumber,
								pqm.PersonAssessmentMetricsID,A.DateTaken, Pqm.ItemID,pqm.assessmentID
								from AssessmentInCols A  WITH (NOLOCK)
								JOIN PersonAssessmentMetrics pqm WITH (NOLOCK) ON pqm.AssessmentID = A.AssessmentID
								and pqm.PersonID=A.PersonID
					) SELECT distinct PersonAssessmentMetricsID FROM AssessmentMetricsIDs  WHERE RowNumber = 1";
				var resultIDs = ExecuteSqlQuery(query, x => new string(x["PersonAssessmentMetricsID"] == DBNull.Value ? string.Empty : x["PersonAssessmentMetricsID"].ToString())).Distinct();
				return resultIDs.ToList();
			}
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Get data for Dashboard Need pie chart
        /// </summary>
        /// <param name="role"></param>
        /// <param name="agencyID"></param>
        /// <param name="helperID"></param>
        /// <returns>DashboardNeedPieChartDTO</returns>
        public DashboardNeedPieChartDTO GetDashboardNeedPiechartData(string role, long agencyID, int? helperID, bool isSameAsLoggedInUser,int userID)
		{
			try
			{
				string query = string.Empty;
				string helperColbQueryCondition = string.Empty;
				string LastassessmentsQueryForHelperInColb = string.Empty;
				if (role != PCISEnum.Roles.SuperAdmin)
				{
					var personIdList = this.helperRepository.GetHelperPersonInCollaborationDetails(userID, agencyID);
					if (personIdList.Count > 0)
					{
						string personIDs = string.Join(",", personIdList.ToArray());

						helperColbQueryCondition = $@"AND Pqm.PersonID NOT IN ({personIDs})";
						List<string> assessmentsInColbratn = GetHelperMetricsInCollaboration(personIDs, agencyID, userID);
						string assessmentMetricsIDS = assessmentsInColbratn.Count > 0 ? string.Join(",", assessmentsInColbratn.ToArray()) : "0";
						LastassessmentsQueryForHelperInColb = $@"UNION
							SELECT
							pqm.PersonID,			
							pqm.ItemID,
							pqm.InstrumentID,
							pqm.NeedsEver,
							pqm.NeedsAddressing,
							pqm.NeedsAddressed,pqm.QuestionnaireID
							FROM PersonAssessmentMetrics pqm WITH (NOLOCK) WHERE pqm.PersonAssessmentMetricsID IN ({assessmentMetricsIDS})";
					}
				}
				if (isSameAsLoggedInUser)
				{
					query = @$";WITH HelperList	AS
							(
							   SELECT
		                            h.HelperID
	                            FROM
	                            Helper h
	                            WHERE H.IsRemoved=0 AND h.AgencyID={agencyID.ToString()} 
                                AND (h.HelperID={helperID.ToString()}) --FOR HELPER only
							)
							,PersonUnderHelpers As
							(
							    SELECT 
			                        DISTINCT p.PersonID
		                        FROM 
								PersonHelper ph 
								JOIn HelperList hl on ph.HelperID = hl.HelperID
		                        JOIN Person p ON p.PersonID=ph.PersonID
		                        WHERE p.IsRemoved=0 AND p.IsActive=1 AND p.AgencyID={agencyID.ToString()} AND ph.IsRemoved=0
                                AND CAST(GETDATE() AS DATE) BETWEEN CAST(ph.StartDate AS DATE) AND ISNULL(CAST(ph.EndDate AS DATE), CAST(GETDATE() AS DATE))
							),
							DistinctPersonQuestionnaireMetrics as
							(
								SELECT PersonQuestionnaireMetricsID FROM(
								SELECT 
								ROW_NUMBER() OVER(PARTITIon BY pqm.PersonID,pqm.ItemID ORDER BY pqm.ItemID) AS RowNumber, pqm.PersonQuestionnaireMetricsID
								from PersonQuestionnaireMetrics pqm WITH (NOLOCK)
								JOIN PersonUnderHelpers p ON pqm.PersonID=p.PersonID
									WHERE pqm.NeedsEver>0 {helperColbQueryCondition}) as A 
								WHERE A.RowNumber = 1
							),CalculatedValues AS
							(
							   SELECT
									pqm.PersonID,			
									pqm.ItemID,
									pqm.InstrumentID,
									pqm.NeedsEver,
									pqm.NeedsAddressing,
									pqm.NeedsAddressed,0 As QuestionnaireID
									FROM 
									PersonQuestionnaireMetrics pqm
									join DistinctPersonQuestionnaireMetrics dpqm on dpqm.PersonQuestionnaireMetricsID=pqm.PersonQuestionnaireMetricsID
									Where pqm.NeedsEver > 0  {helperColbQueryCondition}
									{LastassessmentsQueryForHelperInColb}
							)
                            SELECT 
	                            SUM(isnull(pqm.NeedsEver,0)) [Addressing],
	                            SUM(case when isnull(pqm.NeedsAddressed,0) = 0 and isnull(pqm.NeedsAddressing,0)=0 then isnull(pqm.NeedsEver,0) else isnull(pqm.NeedsAddressed,0) end) [Improved]
                            FROM 
	                            CalculatedValues pqm";
                }
                else
                {
                    if (role == PCISEnum.Roles.SuperAdmin)
                    {
                        query = @$";WITH DistinctPersonQuestionnaireMetrics as
							(
								SELECT * FROM(
								SELECT 
								ROW_NUMBER() OVER(PARTITIon BY pqm.PersonID,pqm.ItemID ORDER BY pqm.ItemID) AS RowNumber, pqm.PersonQuestionnaireMetricsID,pqm.PersonID,pqm.ItemID
								from PersonQuestionnaireMetrics pqm WITH (NOLOCK)
								JOIN dbo.Person  p ON pqm.PersonID=p.PersonID
								WHERE p.IsRemoved=0 AND p.IsActive=1 AND pqm.NeedsEver > 0 AND p.AgencyID={agencyID.ToString()} ) as A 
								WHERE A.RowNumber = 1
							)SELECT 
								SUM(isnull(pqm.NeedsEver,0)) [Addressing],
	                            SUM(case when isnull(pqm.NeedsAddressed,0) = 0 and isnull(pqm.NeedsAddressing,0)=0 then isnull(pqm.NeedsEver,0) else isnull(pqm.NeedsAddressed,0) end) [Improved]
                            FROM 
                            PersonQuestionnaireMetrics pqm
							join DistinctPersonQuestionnaireMetrics dpqm on dpqm.PersonQuestionnaireMetricsID=pqm.PersonQuestionnaireMetricsID";
                    }
					else if (role == PCISEnum.Roles.OrgAdminRO || role == PCISEnum.Roles.OrgAdminRW)
					{
						query = @$";WITH DistinctPersonQuestionnaireMetrics as
							(
								SELECT * FROM(
								SELECT 
								ROW_NUMBER() OVER(PARTITIon BY pqm.PersonID,pqm.ItemID ORDER BY pqm.ItemID) AS RowNumber, pqm.PersonQuestionnaireMetricsID,pqm.PersonID,pqm.ItemID
								from PersonQuestionnaireMetrics pqm WITH (NOLOCK)
								JOIN dbo.Person  p ON pqm.PersonID=p.PersonID
								WHERE p.IsRemoved=0 AND p.IsActive=1 AND pqm.NeedsEver > 0 AND p.AgencyID={agencyID.ToString()} {helperColbQueryCondition} ) as A 
								WHERE A.RowNumber = 1
							)
						  ,CalculatedVAlues AS
							(
							   SELECT
									pqm.PersonID,			
									pqm.ItemID,
									pqm.InstrumentID,
									pqm.NeedsEver,
									pqm.NeedsAddressing,
									pqm.NeedsAddressed,0 As QuestionnaireID
									FROM 
									PersonQuestionnaireMetrics pqm
									join DistinctPersonQuestionnaireMetrics dpqm on dpqm.PersonQuestionnaireMetricsID=pqm.PersonQuestionnaireMetricsID
									WHERE 1=1  {helperColbQueryCondition}
									{LastassessmentsQueryForHelperInColb}
							)
							SELECT 
								SUM(isnull(pqm.NeedsEver,0)) [Addressing],
	                            SUM(case when isnull(pqm.NeedsAddressed,0) = 0 and isnull(pqm.NeedsAddressing,0)=0 then isnull(pqm.NeedsEver,0) else isnull(pqm.NeedsAddressed,0) end) [Improved]
                            FROM 
                            CalculatedVAlues pqm";
					}
					else if (role == PCISEnum.Roles.Supervisor)
                    {
                        query = @$";WITH SupervisorHierarchy AS
                            (
	                           SELECT
							     h.HelperID
							     FROM
							     Helper h	 	 
							     WHERE H.IsRemoved=0 AND h.AgencyID={agencyID.ToString()}
							        AND (h.HelperID={helperID.ToString()} OR h.SupervisorHelperID={helperID.ToString()})
							  UNION ALL
							  SELECT
								 H1.HelperID
							     FROM Helper H1 
								 INNER JOIN SupervisorHierarchy HL ON H1.SupervisorHelperID = HL.HelperID 
								 AND HL.HelperID <> {helperID.ToString()}
							     WHERE H1.IsRemoved=0 AND H1.AgencyID={agencyID.ToString()} 
                            )
                            ,HelperList	AS
							(
							  SELECT
								    h.HelperID
							    FROM Helper h WHERE h.AgencyID={agencyID.ToString()} AND h.IsRemoved=0 
	                            AND h.ReviewerID={helperID.ToString()}
								UNION
								SELECT * from SupervisorHierarchy
							)
							,PersonUnderHelpers As
							(
							    SELECT 
			                        DISTINCT p.PersonID
		                        FROM 
								PersonHelper ph 
								JOIn HelperList hl on ph.HelperID = hl.HelperID
		                        JOIN Person p ON p.PersonID=ph.PersonID
		                        WHERE p.IsRemoved=0 AND p.IsActive=1 AND p.AgencyID={agencyID.ToString()}  AND ph.IsRemoved=0
                                AND CAST(GETDATE() AS DATE) BETWEEN CAST(ph.StartDate AS DATE) AND ISNULL(CAST(ph.EndDate AS DATE), CAST(GETDATE() AS DATE))
							),DistinctPersonQuestionnaireMetrics as
							(
								SELECT PersonQuestionnaireMetricsID FROM(
								SELECT 
								ROW_NUMBER() OVER(PARTITIon BY pqm.PersonID,pqm.ItemID ORDER BY pqm.ItemID) AS RowNumber, pqm.PersonQuestionnaireMetricsID
								from PersonQuestionnaireMetrics pqm WITH (NOLOCK)
								JOIN PersonUnderHelpers p ON pqm.PersonID=p.PersonID
									WHERE pqm.NeedsEver>0 {helperColbQueryCondition}) as A 
								WHERE A.RowNumber = 1
							),CalculatedValues AS
							(
							   SELECT
									pqm.PersonID,			
									pqm.ItemID,
									pqm.InstrumentID,
									pqm.NeedsEver,
									pqm.NeedsAddressing,
									pqm.NeedsAddressed,0 As QuestionnaireID
									FROM 
									PersonQuestionnaireMetrics pqm
									join DistinctPersonQuestionnaireMetrics dpqm on dpqm.PersonQuestionnaireMetricsID=pqm.PersonQuestionnaireMetricsID
									WHERE 1=1  {helperColbQueryCondition}
									{LastassessmentsQueryForHelperInColb}
							)
                            SELECT 
	                            SUM(isnull(pqm.NeedsEver,0)) [Addressing],
	                            SUM(case when isnull(pqm.NeedsAddressed,0) = 0 and isnull(pqm.NeedsAddressing,0)=0 then isnull(pqm.NeedsEver,0) else isnull(pqm.NeedsAddressed,0) end) [Improved]
                            FROM 
	                            CalculatedValues pqm";
                    }
                    else if (role == PCISEnum.Roles.HelperRO || role == PCISEnum.Roles.HelperRW || role == PCISEnum.Roles.Assessor)
                    {
                        query = @$";WITH HelperList	AS
							(
							   SELECT
		                            h.HelperID
	                            FROM
	                            Helper h
	                            WHERE H.IsRemoved=0 AND h.AgencyID={agencyID.ToString()} 
                                AND (h.HelperID={helperID.ToString()} OR h.ReviewerID={helperID.ToString()}) --FOR HELPER And ReviwerHelpers
							)
							,PersonUnderHelpers As
							(
							    SELECT 
			                        DISTINCT p.PersonID
		                        FROM 
								PersonHelper ph 
								JOIn HelperList hl on ph.HelperID = hl.HelperID
		                        JOIN Person p ON p.PersonID=ph.PersonID
		                        WHERE p.IsRemoved=0 AND p.IsActive=1 AND p.AgencyID={agencyID.ToString()} AND ph.IsRemoved=0
                                AND CAST(GETDATE() AS DATE) BETWEEN CAST(ph.StartDate AS DATE) AND ISNULL(CAST(ph.EndDate AS DATE), CAST(GETDATE() AS DATE))
							),DistinctPersonQuestionnaireMetrics as
							(
								SELECT PersonQuestionnaireMetricsID FROM(
								SELECT 
								ROW_NUMBER() OVER(PARTITIon BY pqm.PersonID,pqm.ItemID ORDER BY pqm.ItemID) AS RowNumber, pqm.PersonQuestionnaireMetricsID
								from PersonQuestionnaireMetrics pqm WITH (NOLOCK)
								JOIN PersonUnderHelpers p ON pqm.PersonID=p.PersonID
									WHERE pqm.NeedsEver>0 {helperColbQueryCondition}) as A 
								WHERE A.RowNumber = 1
							),CalculatedValues AS
							(
							   SELECT
									pqm.PersonID,			
									pqm.ItemID,
									pqm.InstrumentID,
									pqm.NeedsEver,
									pqm.NeedsAddressing,
									pqm.NeedsAddressed,0 As QuestionnaireID
									FROM 
									PersonQuestionnaireMetrics pqm
									join DistinctPersonQuestionnaireMetrics dpqm on dpqm.PersonQuestionnaireMetricsID=pqm.PersonQuestionnaireMetricsID
									WHERE 1=1  {helperColbQueryCondition}
									{LastassessmentsQueryForHelperInColb}
							)
                            SELECT 
								SUM(isnull(pqm.NeedsEver,0)) [Addressing],
	                            SUM(case when isnull(pqm.NeedsAddressed,0) = 0 and isnull(pqm.NeedsAddressing,0)=0 then isnull(pqm.NeedsEver,0) else isnull(pqm.NeedsAddressed,0) end) [Improved]
                            FROM 
	                           CalculatedValues pqm";
                    }
                }
                var queryResult = ExecuteSqlQuery(query, x => new DashboardNeedPieChartDTO
                {
                    Addressing = x["Addressing"] == DBNull.Value ? 0 : (int)x["Addressing"],
                    Improved = x["Improved"] == DBNull.Value ? 0 : (int)x["Improved"]
                });
                return queryResult.Count > 0 ? queryResult[0] : null;
            }
            catch (Exception)
            {
                throw;
            }
        }

		/// <summary>
		/// Get data for Dashboard Strength pie chart
		/// </summary>
		/// <param name="role"></param>
		/// <param name="agencyID"></param>
		/// <param name="helperID"></param>
		/// <returns>DashboardStrengthPieChartDTO</returns>
		public DashboardStrengthPieChartDTO GetDashboardStrengthPiechartData(string role, long agencyID, int? helperID, bool isSameAsLoggedInUser, int userID)
		{
			try
			{
				string query = string.Empty;
				string helperColbQueryCondition = string.Empty;
				string LastassessmentsQueryForHelperInColb = string.Empty;
				if (role != PCISEnum.Roles.SuperAdmin)
				{
					var personIdList = this.helperRepository.GetHelperPersonInCollaborationDetails(userID, agencyID);
					if (personIdList.Count > 0)
					{
						string personIDs = string.Join(",", personIdList.ToArray());

						helperColbQueryCondition = $@"AND Pqm.PersonID NOT IN ({personIDs})";
						List<string> assessmentsInColbratn = GetHelperMetricsInCollaboration(personIDs, agencyID, userID);
						string assessmentMetricsIDS = assessmentsInColbratn.Count > 0 ? string.Join(",", assessmentsInColbratn.ToArray()) : "0";
						LastassessmentsQueryForHelperInColb = $@"UNION
							SELECT
							pqm.PersonID,			
							pqm.ItemID,
							pqm.InstrumentID,
							pqm.StrengthsEver,
							pqm.StrengthsBuilding,
							pqm.StrengthsBuilt,pqm.QuestionnaireID
							FROM PersonAssessmentMetrics pqm WITH (NOLOCK) WHERE pqm.PersonAssessmentMetricsID IN ({assessmentMetricsIDS})";
					}
				}
				if (isSameAsLoggedInUser)
				{
					query = @$";WITH HelperList	AS
							(
							   SELECT
		                            h.HelperID
	                            FROM
	                            Helper h
	                            WHERE H.IsRemoved=0 AND h.AgencyID={agencyID.ToString()} 
                                AND (h.HelperID={helperID.ToString()}) --FOR HELPER Only
							)
							,PersonUnderHelpers As
							(
							    SELECT 
			                        DISTINCT p.PersonID
		                        FROM 
								PersonHelper ph 
								JOIn HelperList hl on ph.HelperID = hl.HelperID
		                        JOIN Person p ON p.PersonID=ph.PersonID
		                        WHERE p.IsRemoved=0 AND p.IsActive=1 AND p.AgencyID={agencyID.ToString()} AND ph.IsRemoved=0
                                AND CAST(GETDATE() AS DATE) BETWEEN CAST(ph.StartDate AS DATE) AND ISNULL(CAST(ph.EndDate AS DATE), CAST(GETDATE() AS DATE))
							),DistinctPersonQuestionnaireMetrics as
							(
								SELECT * FROM(
								SELECT 
								ROW_NUMBER() OVER(PARTITIon BY pqm.PersonID,pqm.ItemID ORDER BY pqm.ItemID) AS RowNumber, pqm.PersonQuestionnaireMetricsID,pqm.PersonID,pqm.ItemID
								from PersonQuestionnaireMetrics pqm WITH (NOLOCK)
								JOIN PersonUnderHelpers p ON pqm.PersonID=p.PersonID
								where pqm.StrengthsEver > 0) as A 
								WHERE A.RowNumber = 1
							),CalculatedValues AS
							(
							   SELECT
									pqm.PersonID,			
									pqm.ItemID,
									pqm.InstrumentID,
									pqm.StrengthsEver,
									pqm.StrengthsBuilding,
									pqm.StrengthsBuilt,0 As QuestionnaireID
									FROM 
									PersonQuestionnaireMetrics pqm
									join DistinctPersonQuestionnaireMetrics dpqm on dpqm.PersonQuestionnaireMetricsID=pqm.PersonQuestionnaireMetricsID
									WHERE 1=1  {helperColbQueryCondition}
									{LastassessmentsQueryForHelperInColb}
							)
							SELECT 
	                            SUM(isnull(pqm.StrengthsEver,0)) [Addressing],
	                            SUM(case when isnull(pqm.StrengthsBuilt,0) = 0 and isnull(pqm.StrengthsBuilding,0)=0 then isnull(pqm.StrengthsEver,0) else isnull(pqm.StrengthsBuilt,0) end) [Improved]
                            FROM 
                             CalculatedValues pqm";
                }
                else
                {
                    if (role == PCISEnum.Roles.SuperAdmin)
                    {
                        query = @$"WITH DistinctPersonQuestionnaireMetrics as
							(
								SELECT * FROM(
								SELECT 
								ROW_NUMBER() OVER(PARTITIon BY pqm.PersonID,pqm.ItemID ORDER BY pqm.ItemID) AS RowNumber, pqm.PersonQuestionnaireMetricsID,pqm.PersonID,pqm.ItemID
								from PersonQuestionnaireMetrics pqm WITH (NOLOCK)
								JOIN dbo.Person  p ON pqm.PersonID=p.PersonID
								WHERE p.IsRemoved=0 AND p.IsActive=1 AND pqm.StrengthsEver > 0 AND p.AgencyID={agencyID.ToString()} ) as A 
								WHERE A.RowNumber = 1
							)SELECT 
	                            SUM(isnull(pqm.StrengthsEver,0)) [Addressing],
	                            SUM(case when isnull(pqm.StrengthsBuilt,0) = 0 and isnull(pqm.StrengthsBuilding,0)=0 then isnull(pqm.StrengthsEver,0) else isnull(pqm.StrengthsBuilt,0) end) [Improved]
                            FROM 
                            PersonQuestionnaireMetrics pqm
							join DistinctPersonQuestionnaireMetrics dpqm on dpqm.PersonQuestionnaireMetricsID=pqm.PersonQuestionnaireMetricsID";
					}
					if (role == PCISEnum.Roles.OrgAdminRO || role == PCISEnum.Roles.OrgAdminRW)
					{
						query = @$"WITH DistinctPersonQuestionnaireMetrics as
							(
								SELECT * FROM(
								SELECT 
								ROW_NUMBER() OVER(PARTITIon BY pqm.PersonID,pqm.ItemID ORDER BY pqm.ItemID) AS RowNumber, pqm.PersonQuestionnaireMetricsID,pqm.PersonID,pqm.ItemID
								from PersonQuestionnaireMetrics pqm WITH (NOLOCK)
								JOIN dbo.Person  p ON pqm.PersonID=p.PersonID
								WHERE p.IsRemoved=0 AND p.IsActive=1 AND pqm.StrengthsEver > 0 AND p.AgencyID={agencyID.ToString()} {helperColbQueryCondition} ) as A 
								WHERE A.RowNumber = 1
							),CalculatedValues AS
							(
							   SELECT
									pqm.PersonID,			
									pqm.ItemID,
									pqm.InstrumentID,
									pqm.StrengthsEver,
									pqm.StrengthsBuilding,
									pqm.StrengthsBuilt,0 As QuestionnaireID
									FROM 
									PersonQuestionnaireMetrics pqm
									join DistinctPersonQuestionnaireMetrics dpqm on dpqm.PersonQuestionnaireMetricsID=pqm.PersonQuestionnaireMetricsID
									WHERE 1=1  {helperColbQueryCondition}
									{LastassessmentsQueryForHelperInColb}
							)SELECT 
	                            SUM(isnull(pqm.StrengthsEver,0)) [Addressing],
	                            SUM(case when isnull(pqm.StrengthsBuilt,0) = 0 and isnull(pqm.StrengthsBuilding,0)=0 then isnull(pqm.StrengthsEver,0) else isnull(pqm.StrengthsBuilt,0) end) [Improved]
                            FROM 
                            CalculatedValues pqm";
					}
					else if (role == PCISEnum.Roles.Supervisor)
                    {
                        query = @$";WITH SupervisorHierarchy AS
                            (
	                           SELECT
							     h.HelperID
							     FROM
							     Helper h	 	 
							     WHERE H.IsRemoved=0 AND h.AgencyID={agencyID.ToString()}
							        AND (h.HelperID={helperID.ToString()} OR h.SupervisorHelperID={helperID.ToString()})
							  UNION ALL
							  SELECT
								 H1.HelperID
							     FROM Helper H1 
								 INNER JOIN SupervisorHierarchy HL ON H1.SupervisorHelperID = HL.HelperID 
								 AND HL.HelperID <> {helperID.ToString()}
							     WHERE H1.IsRemoved=0 AND H1.AgencyID={agencyID.ToString()} 
                            )
                            ,HelperList	AS
							(
							  SELECT
								    h.HelperID
							    FROM Helper h WHERE h.AgencyID={agencyID.ToString()} AND h.IsRemoved=0 
	                            AND h.ReviewerID={helperID.ToString()}
								UNION
								SELECT * from SupervisorHierarchy
							)
							,PersonUnderHelpers As
							(
							    SELECT 
			                        DISTINCT p.PersonID
		                        FROM 
								PersonHelper ph 
								JOIn HelperList hl on ph.HelperID = hl.HelperID
		                        JOIN Person p ON p.PersonID=ph.PersonID
		                        WHERE p.IsRemoved=0 AND p.IsActive=1 AND p.AgencyID={agencyID.ToString()}  AND ph.IsRemoved=0
                                AND CAST(GETDATE() AS DATE) BETWEEN CAST(ph.StartDate AS DATE) AND ISNULL(CAST(ph.EndDate AS DATE), CAST(GETDATE() AS DATE))
							),DistinctPersonQuestionnaireMetrics as
							(
								SELECT * FROM(
								SELECT 
								ROW_NUMBER() OVER(PARTITIon BY pqm.PersonID,pqm.ItemID ORDER BY pqm.ItemID) AS RowNumber, pqm.PersonQuestionnaireMetricsID,pqm.PersonID,pqm.ItemID
								from PersonQuestionnaireMetrics pqm WITH (NOLOCK)
								JOIN PersonUnderHelpers p ON pqm.PersonID=p.PersonID
								where pqm.StrengthsEver > 0 {helperColbQueryCondition} ) as A 
								WHERE A.RowNumber = 1
							),CalculatedValues AS
							(
							   SELECT
									pqm.PersonID,			
									pqm.ItemID,
									pqm.InstrumentID,
									pqm.StrengthsEver,
									pqm.StrengthsBuilding,
									pqm.StrengthsBuilt,0 As QuestionnaireID
									FROM 
									PersonQuestionnaireMetrics pqm
									join DistinctPersonQuestionnaireMetrics dpqm on dpqm.PersonQuestionnaireMetricsID=pqm.PersonQuestionnaireMetricsID
									WHERE 1=1  {helperColbQueryCondition}
									{LastassessmentsQueryForHelperInColb}
							)
							SELECT 
	                            SUM(isnull(pqm.StrengthsEver,0)) [Addressing],
	                            SUM(case when isnull(pqm.StrengthsBuilt,0) = 0 and isnull(pqm.StrengthsBuilding,0)=0 then isnull(pqm.StrengthsEver,0) else isnull(pqm.StrengthsBuilt,0) end) [Improved]
                            FROM 
                            CalculatedValues pqm";
                    }
                    else if (role == PCISEnum.Roles.HelperRO || role == PCISEnum.Roles.HelperRW || role == PCISEnum.Roles.Assessor)
                    {
                        query = @$";WITH HelperList	AS
							(
							   SELECT
		                            h.HelperID
	                            FROM
	                            Helper h
	                            WHERE H.IsRemoved=0 AND h.AgencyID={agencyID.ToString()} 
                                AND (h.HelperID={helperID.ToString()} OR h.ReviewerID={helperID.ToString()}) --FOR HELPER And ReviwerHelpers
							)
							,PersonUnderHelpers As
							(
							    SELECT 
			                        DISTINCT p.PersonID
		                        FROM 
								PersonHelper ph 
								JOIn HelperList hl on ph.HelperID = hl.HelperID
		                        JOIN Person p ON p.PersonID=ph.PersonID
		                        WHERE p.IsRemoved=0 AND p.IsActive=1 AND p.AgencyID={agencyID.ToString()} AND ph.IsRemoved=0
                                AND CAST(GETDATE() AS DATE) BETWEEN CAST(ph.StartDate AS DATE) AND ISNULL(CAST(ph.EndDate AS DATE), CAST(GETDATE() AS DATE))
							),DistinctPersonQuestionnaireMetrics as
							(
								SELECT * FROM(
								SELECT 
								ROW_NUMBER() OVER(PARTITIon BY pqm.PersonID,pqm.ItemID ORDER BY pqm.ItemID) AS RowNumber, pqm.PersonQuestionnaireMetricsID,pqm.PersonID,pqm.ItemID
								from PersonQuestionnaireMetrics pqm WITH (NOLOCK)
								JOIN PersonUnderHelpers p ON pqm.PersonID=p.PersonID
								where pqm.StrengthsEver > 0 {helperColbQueryCondition} ) as A 
								WHERE A.RowNumber = 1
							),CalculatedValues AS
							(
							   SELECT
									pqm.PersonID,			
									pqm.ItemID,
									pqm.InstrumentID,
									pqm.StrengthsEver,
									pqm.StrengthsBuilding,
									pqm.StrengthsBuilt,0 As QuestionnaireID
									FROM 
									PersonQuestionnaireMetrics pqm
									join DistinctPersonQuestionnaireMetrics dpqm on dpqm.PersonQuestionnaireMetricsID=pqm.PersonQuestionnaireMetricsID
									WHERE 1=1  {helperColbQueryCondition}
									{LastassessmentsQueryForHelperInColb}
							)
							SELECT 
	                            SUM(isnull(pqm.StrengthsEver,0)) [Addressing],
	                            SUM(case when isnull(pqm.StrengthsBuilt,0) = 0 and isnull(pqm.StrengthsBuilding,0)=0 then isnull(pqm.StrengthsEver,0) else isnull(pqm.StrengthsBuilt,0) end) [Improved]
                            FROM CalculatedValues pqm";
                    }
                }
                var queryResult = ExecuteSqlQuery(query, x => new DashboardStrengthPieChartDTO
                {
                    ToBeBuilt = x["Addressing"] == DBNull.Value ? 0 : (int)x["Addressing"],
                    Built = x["Improved"] == DBNull.Value ? 0 : (int)x["Improved"]
                });
                return queryResult.Count > 0 ? queryResult[0] : null;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// To get details Questionnaire.
        /// </summary>
        /// <param QuestionnaireDTO="QuestionnaireDTO">id.</param>
        /// <returns>.QuestionnaireDTO</returns>
        public List<PersonQuestionnaireMetrics> GetPersonQuestionnaireMetrics(DashboardMetricsInputDTO metricsInput)
        {
            try
            {
                List<PersonQuestionnaireMetrics> personQuestionnaireMetrics = new List<PersonQuestionnaireMetrics>();
                personQuestionnaireMetrics = this.GetAsync(x => x.PersonID == metricsInput.personId && metricsInput.itemIds.Contains(x.ItemID)).Result.ToList();


                var query = @$"select [PersonQuestionnaireMetricsID]
							  ,[PersonID]
							  ,[InstrumentID]
							  ,[PersonQuestionnaireID]
							  ,[ItemID]
							  ,[NeedsEver]
							  ,[NeedsIdentified]
							  ,[NeedsAddressed]
							  ,[NeedsAddressing]
							  ,[NeedsImproved]
							  ,[StrengthsEver]
							  ,[StrengthsIdentified]
							  ,[StrengthsBuilt]
							  ,[StrengthsBuilding]
							  ,[StrengthsImproved] from PersonQuestionnaireMetrics 
							where personId=" + metricsInput.personId + @" and ItemId in (" + String.Join(",", metricsInput.itemIds) + @")";
                var queryResult = ExecuteSqlQuery(query, x => new PersonQuestionnaireMetrics
                {
                    PersonQuestionnaireMetricsID = x["PersonQuestionnaireMetricsID"] == DBNull.Value ? 0 : (long)x["PersonQuestionnaireMetricsID"],
					PersonID = x["PersonID"] == DBNull.Value ? 0 : (long)x["PersonID"],
					InstrumentID = x["InstrumentID"] == DBNull.Value ? 0 : (int)x["InstrumentID"],
					PersonQuestionnaireID = x["PersonQuestionnaireID"] == DBNull.Value ? 0 : (int)x["PersonQuestionnaireID"],
					ItemID = x["ItemID"] == DBNull.Value ? 0 : (int)x["ItemID"],
					NeedsEver = x["NeedsEver"] == DBNull.Value ? 0 : (int)x["NeedsEver"],
					NeedsIdentified = x["NeedsIdentified"] == DBNull.Value ? 0 : (int)x["NeedsIdentified"],
					NeedsAddressed = x["NeedsAddressed"] == DBNull.Value ? 0 : (int)x["NeedsAddressed"],
					NeedsAddressing = x["NeedsAddressing"] == DBNull.Value ? 0 : (int)x["NeedsAddressing"],
					NeedsImproved = x["NeedsImproved"] == DBNull.Value ? 0 : (int)x["NeedsImproved"],
					StrengthsEver = x["StrengthsEver"] == DBNull.Value ? 0 : (int)x["StrengthsEver"],
					StrengthsIdentified = x["StrengthsIdentified"] == DBNull.Value ? 0 : (int)x["StrengthsIdentified"],
					StrengthsBuilt = x["StrengthsBuilt"] == DBNull.Value ? 0 : (int)x["StrengthsBuilt"],
					StrengthsBuilding = x["StrengthsBuilding"] == DBNull.Value ? 0 : (int)x["StrengthsBuilding"],
					StrengthsImproved = x["NeedsImproved"] == DBNull.Value ? 0 : (int)x["StrengthsImproved"],
				});

                return personQuestionnaireMetrics;
            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// Add PersonQuestionnaireMetrics
        /// </summary>
        /// <param name="personQuestionnaireMetrics"></param>
        /// <returns>PersonQuestionnaireMetrics</returns>
        public PersonQuestionnaireMetrics AddPersonQuestionnaireMetrics(PersonQuestionnaireMetrics personQuestionnaireMetrics)
        {
            try
            {
                var result = this.AddAsync(personQuestionnaireMetrics).Result;
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// To update PersonQuestionnaire details.
        /// </summary>
        /// <param name="personQuestionnaireDTO">id.</param>
        /// <returns>List of summaries.</returns>
        public PersonQuestionnaireMetrics UpdatePersonQuestionnaireMetrics(PersonQuestionnaireMetrics personQuestionnaireMetrics)
        {
            try
            {
                var result = this.UpdateAsync(personQuestionnaireMetrics).Result;
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<PersonQuestionnaireMetrics> UpdateBulkPersonQuestionnaireMetrics(List<PersonQuestionnaireMetrics> personQuestionnaireMetrics)
        {
            try
            {
                var res = this.UpdateBulkAsync(personQuestionnaireMetrics);
                res.Wait();
                return personQuestionnaireMetrics;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<PersonQuestionnaireMetrics> AddBulkPersonQuestionnaireMetrics(List<PersonQuestionnaireMetrics> personQuestionnaireMetrics)
        {
            try
            {
                var res = this.AddBulkAsync(personQuestionnaireMetrics);
                res.Wait();
                return personQuestionnaireMetrics;
            }
            catch (Exception)
            {
                throw;
            }
        }
	}
}
