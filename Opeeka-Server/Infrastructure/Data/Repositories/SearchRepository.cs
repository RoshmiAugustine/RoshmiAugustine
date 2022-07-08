// -----------------------------------------------------------------------
// <copyright file="SearchRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.DTO.Input;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Infrastructure.Data.Repositories.Base;
using Opeeka.PICS.Infrastructure.Enums;

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{
    public class SearchRepository : BaseRepository<Person>, ISearchRepository
    {

        private readonly OpeekaDBContext dbContext;

        /// <summary>
        /// SearchRepository.
        /// </summary>
        public SearchRepository(OpeekaDBContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// GetUpperpaneSearchResults
        /// </summary>
        /// <param name="upperpaneSearchKeyDTO"></param>
        /// <param name="role"></param>
        /// <param name="agencyID"></param>
        /// <param name="helperID"></param>
        /// <param name="sharedPersonQuery"></param>
        /// <returns></returns>
        public List<UpperpaneSearchDTO> GetUpperpaneSearchResults(UpperpaneSearchKeyDTO upperpaneSearchKeyDTO, string role, long agencyID, int helperID, string sharedPersonIDs)
        {
            try
            {
                var sharedPersonQuery = string.Empty;
                List <UpperpaneSearchDTO> responseDTO = new List<UpperpaneSearchDTO>();
                var query = string.Empty;
                if (upperpaneSearchKeyDTO.searchKey.Any(ch => !Char.IsLetterOrDigit(ch)))
                {
                    upperpaneSearchKeyDTO.searchKey = upperpaneSearchKeyDTO.searchKey.Replace(@"\", @"\\");
                    upperpaneSearchKeyDTO.searchKey = upperpaneSearchKeyDTO.searchKey.Replace(@"%", @"\%");
                    upperpaneSearchKeyDTO.searchKey = upperpaneSearchKeyDTO.searchKey.Replace(@"[", @"\[");
                    upperpaneSearchKeyDTO.searchKey = upperpaneSearchKeyDTO.searchKey.Replace(@"]", @"\]");
                    upperpaneSearchKeyDTO.searchKey = upperpaneSearchKeyDTO.searchKey.Replace(@"_", @"\_");
                    upperpaneSearchKeyDTO.searchKey = upperpaneSearchKeyDTO.searchKey.Replace(@"'", @"''");
                }

                if (role == PCISEnum.Roles.SuperAdmin)
                {
                    if (!string.IsNullOrEmpty(sharedPersonIDs))
                    {
                        sharedPersonQuery = $@"UNION ALL
                                     SELECT DISTINCT P.PersonID as Id,[PersonIndex] as [Index], FirstName + ISNULL(' ' + MiddleName, '') + ISNULL(' ' + LastName, '') as [Name], FirstName  + ISNULL(' ' + LastName, '') as [FLName], 'Person' as [Type], [IsActive] as [IsActive], P.[IsRemoved] as [IsRemoved], 1 AS [IsShared], A.Name as [AgencyName],P.Email as [Email] FROM Person P JOIN Agency A ON P.AgencyID = A.AgencyID WHERE P.PersonID IN ({sharedPersonIDs})";
                    }
                    query = @$";WITH CTE AS
                            (select * from
                            (
                            select[HelperID] as Id,[HelperIndex] as [Index], FirstName + ISNULL(' ' + MiddleName, '') + ISNULL(' ' + LastName, '') as [Name], FirstName  + ISNULL(' ' + LastName, '') as [FLName], 'Helper' as [Type], -1 as [IsActive], -1 as [IsRemoved], 0 AS [IsShared], '' as [AgencyName], Email as [Email]
                            from[dbo].[Helper] H  WHERE IsRemoved = 0 and AgencyID = {agencyID}
                            union ALL
                            select[PersonID] as Id,[PersonIndex] as [Index], FirstName + ISNULL(' ' + MiddleName, '') + ISNULL(' ' + LastName, '') as [Name], FirstName  + ISNULL(' ' + LastName, '') as [FLName], 'Person' as [Type], [IsActive] as [IsActive], [IsRemoved] as [IsRemoved], 0 AS [IsShared], '' as [AgencyName], Email as [Email]
                            from[dbo].[Person] P  WHERE AgencyID = {agencyID}
                            {sharedPersonQuery}
                            )Result where Result.Name like '%{upperpaneSearchKeyDTO.searchKey}%' ESCAPE  '\'   or Result.[FLName] like '%{upperpaneSearchKeyDTO.searchKey}%' ESCAPE  '\'  or Result.[Email] like '%{upperpaneSearchKeyDTO.searchKey}%' ESCAPE  '\'  
                            )
                            SELECT COUNT(*) OVER() AS TotalCount,* from CTE
                            Order by[Name] ";
                }
                else if (role == PCISEnum.Roles.OrgAdminRO || role == PCISEnum.Roles.OrgAdminRW)
                {
                    if (!string.IsNullOrEmpty(sharedPersonIDs))
                    {
                        sharedPersonQuery = $@"UNION ALL
                                     SELECT DISTINCT P.PersonID as Id,[PersonIndex] as [Index], FirstName + ISNULL(' ' + MiddleName, '') + ISNULL(' ' + LastName, '') as [Name], FirstName  + ISNULL(' ' + LastName, '') as [FLName], 'Person' as [Type], [IsActive] as [IsActive], P.[IsRemoved] as [IsRemoved], 1 AS [IsShared], A.Name as [AgencyName],P.Email as [Email] FROM Person P JOIN Agency A ON P.AgencyID = A.AgencyID WHERE P.PersonID IN ({sharedPersonIDs})";
                    }
                    query = @$";WITH CTE AS
                            (select * from
                            (
                            select[HelperID] as Id,[HelperIndex] as [Index], FirstName + ISNULL(' ' + MiddleName, '') + ISNULL(' ' + LastName, '') as [Name], FirstName  + ISNULL(' ' + LastName, '') as [FLName], 'Helper' as [Type], -1 as [IsActive], -1 as [IsRemoved], 0 AS [IsShared], '' as [AgencyName] , Email as [Email]
                            from[dbo].[Helper] H  where IsRemoved = 0 and AgencyID = {agencyID}
                            union ALL
                            select[PersonID] as Id,[PersonIndex]  as [Index], FirstName + ISNULL(' ' + MiddleName, '') + ISNULL(' ' + LastName, '') as [Name], FirstName  + ISNULL(' ' + LastName, '') as [FLName], 'Person' as [Type], [IsActive] as [IsActive], [IsRemoved] as [IsRemoved], 0 AS [IsShared], '' as [AgencyName] , Email as [Email]
                            from[dbo].[Person] P  where P.IsRemoved = 0 and AgencyID = {agencyID}
                            {sharedPersonQuery}
                            )Result where Result.Name like '%{upperpaneSearchKeyDTO.searchKey}%' ESCAPE  '\'   or Result.[FLName] like '%{upperpaneSearchKeyDTO.searchKey}%' ESCAPE  '\'   or Result.[Email] like '%{upperpaneSearchKeyDTO.searchKey}%' ESCAPE  '\'
                            )
                            SELECT COUNT(*) OVER() AS TotalCount,* from CTE
                            Order by[Name] ";
                }
                else if (role == PCISEnum.Roles.Supervisor)
                {
                    if (!string.IsNullOrEmpty(sharedPersonIDs))
                    {
                        sharedPersonQuery = $@"UNION ALL
                                SELECT SharedPersons.* FROM (
                                     SELECT DISTINCT P.PersonID as Id,[PersonIndex] as [Index], FirstName + ISNULL(' ' + MiddleName, '') + ISNULL(' ' + LastName, '') as [Name], FirstName  + ISNULL(' ' + LastName, '') as [FLName], 'Person' as [Type], [IsActive] as [IsActive], P.[IsRemoved] as [IsRemoved], 1 AS [IsShared], A.Name as [AgencyName],P.Email as [Email] FROM Person P JOIN Agency A ON P.AgencyID = A.AgencyID WHERE P.PersonID IN ({sharedPersonIDs})
                                )SharedPersons
                                LEFT JOIN PersonHelper ph ON ph.PersonID=SharedPersons.Id 
	                            JOIN HelperList h ON h.HelperID=ph.helperID  
	                            AND ph.IsRemoved=0
	                            AND ph.StartDate<=CAST(GETDATE() AS DATE) AND ISNULL(ph.EndDate, CAST(GETDATE() AS DATE))>=CAST(GETDATE() AS DATE)";
                    }
                    query = @$";WITH HelperList AS
                            (
	                            SELECT  * FROM
	                            Helper h WHERE h.AgencyID={agencyID} AND h.IsRemoved=0 
	                            AND (h.HelperID={helperID} OR h.SupervisorHelperID={helperID})  
                            )
                            ,PersonHelped AS
                            (
	                            SELECT  DISTINCT p.PersonID    FROM     Person P 
	                            LEFT JOIN PersonHelper ph ON ph.PersonID=p.PersonID 
	                            JOIN HelperList h ON h.HelperID=ph.helperID
	                            WHERE P.IsRemoved=0 AND p.AgencyID={agencyID} 
	                            AND ph.IsRemoved=0 AND P.IsActive = 1
	                            AND ph.StartDate<=CAST(GETDATE() AS DATE) AND ISNULL(ph.EndDate, CAST(GETDATE() AS DATE))>=CAST(GETDATE() AS DATE)
                            )
                            ,PersonInCollaboration AS
                            (
	                            SELECT  DISTINCT p.PersonID FROM HelperList h 
	                            JOIN PersonHelper ph ON ph.HelperID=h.HelperID
	                            AND ph.StartDate<=CAST(GETDATE() AS DATE) AND ISNULL(ph.EndDate, CAST(GETDATE() AS DATE))>=CAST(GETDATE() AS DATE)
	                            JOIN CollaborationLeadHistory clh ON clh.LeadUserID=h.HelperID AND clh.StartDate<=CAST(GETDATE() AS DATE) AND ISNULL(clh.EndDate, CAST(GETDATE() AS DATE))>=CAST(GETDATE() AS DATE)
	                            JOIN PersonCollaboration pc ON clh.CollaborationID=pc.CollaborationID AND pc.EnrollDate<=CAST(GETDATE() AS DATE) AND ISNULL(pc.EndDate, CAST(GETDATE() AS DATE))>=CAST(GETDATE() AS DATE) 
	                            JOIN Person p ON pc.PersonID=p.PersonID AND p.IsRemoved=0 AND P.IsActive = 1
                            )
                            SELECT
	                            COUNT(*) OVER() AS TotalCount,
	                            *
                            FROM
                            (
	                            SELECT
		                            [HelperID]  as Id,[HelperIndex] as [Index],FirstName+ ISNULL(' ' +MiddleName,'') + ISNULL(' ' + LastName,'') as [Name], FirstName  + ISNULL(' ' + LastName, '') as [FLName],'Helper' as [Type], -1 as [IsActive], -1 as [IsRemoved], 0 AS [IsShared], '' as [AgencyName] ,Email as [Email]
	                            FROM   HelperList

	                            UNION ALL

	                            SELECT
		                            P.[PersonID] as Id,[PersonIndex] as [Index],P.FirstName + ISNULL(' ' + P.MiddleName, '') + ISNULL(' ' + P.LastName, '') as [Name], FirstName  + ISNULL(' ' + LastName, '') as [FLName],'Person' as [Type], [IsActive] as [IsActive], [IsRemoved] as [IsRemoved], 0 AS [IsShared], '' as [AgencyName] ,Email as [Email]
	                            FROM    Person p
	                            JOIN 
	                            (
		                            SELECT PersonID FROM PersonHelped
		                            UNION
		                            SELECT PersonID FROM PersonInCollaboration
	                            )PersonToList ON p.PersonID=PersonToList.PersonID                                
                                {sharedPersonQuery}
                            )Result 
                            WHERE Result.Name LIKE  '%{upperpaneSearchKeyDTO.searchKey}%' ESCAPE  '\'     or Result.[FLName] like '%{upperpaneSearchKeyDTO.searchKey}%' ESCAPE  '\' 
                            or Result.[Email] like '%{upperpaneSearchKeyDTO.searchKey}%' ESCAPE  '\'  Order by [Name] ";
                }
                else if (role == PCISEnum.Roles.HelperRO || role == PCISEnum.Roles.HelperRW)
                {
                    if (!string.IsNullOrEmpty(sharedPersonIDs))
                    {
                        sharedPersonQuery = $@"UNION ALL
                                SELECT SharedPersons.* FROM (
                                     SELECT DISTINCT P.PersonID as Id,[PersonIndex] as [Index], FirstName + ISNULL(' ' + MiddleName, '') + ISNULL(' ' + LastName, '') as [Name], FirstName  + ISNULL(' ' + LastName, '') as [FLName], 'Person' as [Type], [IsActive] as [IsActive], P.[IsRemoved] as [IsRemoved], 1 AS [IsShared], A.Name as [AgencyName],P.Email as [Email] FROM Person P JOIN Agency A ON P.AgencyID = A.AgencyID WHERE P.PersonID IN ({sharedPersonIDs})
                                )SharedPersons
                                LEFT JOIN PersonHelper ph ON ph.PersonID=SharedPersons.Id 
	                            JOIN HelperList h ON h.HelperID=ph.helperID  
	                            AND ph.IsRemoved=0
	                            AND ph.StartDate<=CAST(GETDATE() AS DATE) AND ISNULL(ph.EndDate, CAST(GETDATE() AS DATE))>=CAST(GETDATE() AS DATE)";
                    }
                    query = @$";WITH HelperList AS
                            (
	                            SELECT *  FROM
	                            Helper h WHERE h.AgencyID={agencyID} AND h.IsRemoved=0 
	                            AND h.HelperID={helperID}
                            )
                            ,PersonHelped AS
                            (
	                            SELECT    DISTINCT p.PersonID  FROM    Person P 
	                            LEFT JOIN PersonHelper ph ON ph.PersonID=p.PersonID 
	                            JOIN HelperList h ON h.HelperID=ph.helperID
	                            WHERE P.IsRemoved=0 AND p.AgencyID={agencyID} 
	                            AND ph.IsRemoved=0 AND P.IsActive = 1
	                            AND ph.StartDate<=CAST(GETDATE() AS DATE) AND ISNULL(ph.EndDate, CAST(GETDATE() AS DATE))>=CAST(GETDATE() AS DATE)
                            )
                            ,PersonInCollaboration AS
                            (
	                            SELECT  DISTINCT p.PersonID   FROM    HelperList h 
	                            JOIN PersonHelper ph ON ph.HelperID=h.HelperID
	                            AND ph.StartDate<=CAST(GETDATE() AS DATE) AND ISNULL(ph.EndDate, CAST(GETDATE() AS DATE))>=CAST(GETDATE() AS DATE)
	                            JOIN CollaborationLeadHistory clh ON clh.LeadUserID=h.HelperID AND clh.StartDate<=CAST(GETDATE() AS DATE) AND ISNULL(clh.EndDate, CAST(GETDATE() AS DATE))>=CAST(GETDATE() AS DATE)
	                            JOIN PersonCollaboration pc ON clh.CollaborationID=pc.CollaborationID AND pc.EnrollDate<=CAST(GETDATE() AS DATE) AND ISNULL(pc.EndDate, CAST(GETDATE() AS DATE))>=CAST(GETDATE() AS DATE) 
	                            JOIN Person p ON pc.PersonID=p.PersonID AND p.IsRemoved=0 AND P.IsActive = 1
                            )
                            SELECT
	                            COUNT(*) OVER() AS TotalCount,
	                            *
                            FROM
                            (
	                            SELECT
		                            [HelperID]  as Id,[HelperIndex] as [Index],FirstName+ ISNULL(' ' +MiddleName,'') + ISNULL(' ' + LastName,'') as [Name], FirstName  + ISNULL(' ' + LastName, '') as [FLName],'Helper' as [Type], -1 as [IsActive], -1 as [IsRemoved], 0 AS [IsShared], '' as [AgencyName]  ,Email as [Email]
	                            FROM
	                            HelperList

	                            UNION ALL

	                            SELECT
		                            P.[PersonID] as Id,[PersonIndex] as [Index],P.FirstName + ISNULL(' ' + P.MiddleName, '') + ISNULL(' ' + P.LastName, '') as [Name], FirstName  + ISNULL(' ' + LastName, '') as [FLName],'Person' as [Type], [IsActive] as [IsActive], [IsRemoved] as [IsRemoved], 0 AS [IsShared], '' as [AgencyName] ,Email as [Email]
	                            FROM 
	                            Person p
	                            JOIN 
	                            (
		                            SELECT PersonID FROM PersonHelped
		                            UNION
		                            SELECT PersonID FROM PersonInCollaboration
	                            )PersonToList ON p.PersonID=PersonToList.PersonID
                                {sharedPersonQuery}
                            )Result 
                            WHERE Result.Name LIKE  '%{upperpaneSearchKeyDTO.searchKey}%' ESCAPE  '\'  or Result.[FLName] like '%{upperpaneSearchKeyDTO.searchKey}%' ESCAPE  '\'
                            or Result.[Email] like '%{upperpaneSearchKeyDTO.searchKey}%' ESCAPE  '\'  Order by [Name] ";
                }
                else if (role == PCISEnum.Roles.Assessor)
                {
                    if (!string.IsNullOrEmpty(sharedPersonIDs))
                    {
                        sharedPersonQuery = $@"UNION ALL
                                SELECT SharedPersons.* FROM (
                                     SELECT DISTINCT P.PersonID as Id,[PersonIndex] as [Index], FirstName + ISNULL(' ' + MiddleName, '') + ISNULL(' ' + LastName, '') as [Name], FirstName  + ISNULL(' ' + LastName, '') as [FLName], 'Person' as [Type], [IsActive] as [IsActive], P.[IsRemoved] as [IsRemoved], 1 AS [IsShared], A.Name as [AgencyName],P.Email as [Email] FROM Person P JOIN Agency A ON P.AgencyID = A.AgencyID WHERE P.PersonID IN ({sharedPersonIDs})
                                )SharedPersons
                                LEFT JOIN PersonHelper ph ON ph.PersonID=SharedPersons.Id 
	                            JOIN HelperList h ON h.HelperID=ph.helperID  
	                            AND ph.IsRemoved=0
	                            AND ph.StartDate<=CAST(GETDATE() AS DATE) AND ISNULL(ph.EndDate, CAST(GETDATE() AS DATE))>=CAST(GETDATE() AS DATE)";
                    }
                    query = @$";WITH HelperList AS
                            (
	                            SELECT *  FROM
	                            Helper h WHERE h.AgencyID={agencyID} AND h.IsRemoved=0 
	                            AND h.HelperID={helperID}
                            )
                            ,PersonHelped AS
                            (
	                            SELECT    DISTINCT p.PersonID  FROM    Person P 
	                            LEFT JOIN PersonHelper ph ON ph.PersonID=p.PersonID 
	                            JOIN HelperList h ON h.HelperID=ph.helperID
	                            WHERE P.IsRemoved=0 AND p.AgencyID={agencyID} 
	                            AND ph.IsRemoved=0 AND P.IsActive = 1
	                            AND ph.StartDate<=CAST(GETDATE() AS DATE) AND ISNULL(ph.EndDate, CAST(GETDATE() AS DATE))>=CAST(GETDATE() AS DATE)
                            )
                            ,PersonInCollaboration AS
                            (
	                            SELECT  DISTINCT p.PersonID   FROM    HelperList h 
	                            JOIN PersonHelper ph ON ph.HelperID=h.HelperID
	                            AND ph.StartDate<=CAST(GETDATE() AS DATE) AND ISNULL(ph.EndDate, CAST(GETDATE() AS DATE))>=CAST(GETDATE() AS DATE)
	                            JOIN CollaborationLeadHistory clh ON clh.LeadUserID=h.HelperID AND clh.StartDate<=CAST(GETDATE() AS DATE) AND ISNULL(clh.EndDate, CAST(GETDATE() AS DATE))>=CAST(GETDATE() AS DATE)
	                            JOIN PersonCollaboration pc ON clh.CollaborationID=pc.CollaborationID AND pc.EnrollDate<=CAST(GETDATE() AS DATE) AND ISNULL(pc.EndDate, CAST(GETDATE() AS DATE))>=CAST(GETDATE() AS DATE) 
	                            JOIN Person p ON pc.PersonID=p.PersonID AND p.IsRemoved=0 AND P.IsActive = 1
                            )
                            SELECT
	                            COUNT(*) OVER() AS TotalCount,
	                            *
                            FROM
                            (
	                            SELECT
		                            P.[PersonID] as Id,[PersonIndex] as [Index],P.FirstName + ISNULL(' ' + P.MiddleName, '') + ISNULL(' ' + P.LastName, '') as [Name], FirstName  + ISNULL(' ' + LastName, '') as [FLName],'Person' as [Type], [IsActive] as [IsActive], [IsRemoved] as [IsRemoved], 0 AS [IsShared], '' as [AgencyName] ,Email as [Email]
	                            FROM 
	                            Person p
	                            JOIN 
	                            (
		                            SELECT PersonID FROM PersonHelped
		                            UNION
		                            SELECT PersonID FROM PersonInCollaboration
	                            )PersonToList ON p.PersonID=PersonToList.PersonID
                                {sharedPersonQuery}
                            )Result 
                            WHERE Result.Name LIKE  '%{upperpaneSearchKeyDTO.searchKey}%' ESCAPE  '\'  or Result.[FLName] like '%{upperpaneSearchKeyDTO.searchKey}%' ESCAPE  '\'
                            or Result.[Email] like '%{upperpaneSearchKeyDTO.searchKey}%' ESCAPE  '\'  Order by [Name] ";
                }

                query += @" OFFSET " + ((upperpaneSearchKeyDTO.pageNo - 1) * upperpaneSearchKeyDTO.pageSize) + " ROWS FETCH NEXT " + upperpaneSearchKeyDTO.pageSize + " ROWS ONLY";

                responseDTO = ExecuteSqlQuery(query, x => new UpperpaneSearchDTO
                {
                    Name = x["Name"] == DBNull.Value ? null : (string)x["Name"],
                    Id = x["Id"] == DBNull.Value ? 0 : (Int64)x["Id"],
                    Index = x["Index"] == DBNull.Value ? Guid.Empty : (Guid)x["Index"],
                    Type = x["Type"] == DBNull.Value ? null : (string)x["Type"],
                    IsSharedPerson = (int)x["IsShared"] == 0 ? false : true,                    
                    TotalCount = x["TotalCount"] == DBNull.Value ? 0 : (int)x["TotalCount"],
                    IsActive = x["IsActive"] == DBNull.Value ? 0 : (int)x["IsActive"],
                    IsRemoved = x["IsRemoved"] == DBNull.Value ? 0 : (int)x["IsRemoved"],
                    AgencyName = x["AgencyName"] == DBNull.Value ? null : (string)x["AgencyName"],
                    EmailId = x["Email"] == DBNull.Value ? null : (string)x["Email"],
                });
                return responseDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
