// -----------------------------------------------------------------------
// <copyright file="VoiceTypeRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Infrastructure.Enums;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Infrastructure.Data.Repositories.Base;
using System.Collections.Generic;
using System.Linq;
using Opeeka.PICS.Domain.Interfaces;

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{
    public class VoiceTypeRepository : BaseRepository<VoiceType>, IVoiceTypeRepository
    {
        private readonly OpeekaDBContext _dbContext;
        private readonly ICache _cache;

        /// <summary>
        /// Initializes a new instance of the <see cref="VoiceTypeRepository"/> class.
        /// </summary>
        /// <param name="dbContext"></param>
        public VoiceTypeRepository(OpeekaDBContext dbContext, ICache cache)
            : base(dbContext)
        {
            this._dbContext = dbContext;
            this._cache = cache;
        }

        /// <summary>
        /// GetAllVoiceType
        /// </summary>
        /// <returns>list of VoiceType</returns>
        public List<VoiceType> GetAllVoiceType()
        {
            try
            {
                var readFromCache = this._cache.Get<List<VoiceType>>(PCISEnum.Caching.GetAllVoiceType);
                if (readFromCache == null || readFromCache?.Count == 0)
                {
                    var VoiceType = this._dbContext.VoiceTypes.Where(x => !x.IsRemoved).OrderBy(y => y.ListOrder).ToList();
                    this._cache.Post(PCISEnum.Caching.GetAllVoiceType, readFromCache = VoiceType);
                    return VoiceType;
                }
                return readFromCache;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetAllVoiceTypeInDetail-Reports
        /// </summary>
        /// <param name="personID"></param>
        /// <param name="personQuestionnaireID"></param>
        /// <param name="collaborationID"></param>
        /// <returns>VoiceTypeDTO</returns>
        public List<VoiceTypeDTO> GetAllVoiceTypeInDetail(long personID, long personQuestionnaireID, long personCollaborationID, SharedDetailsDTO sharedIDs, SharedDetailsDTO helperColbDetails)
        {
            try
            {
                var sharedWhereConditionQIDs = string.IsNullOrEmpty(sharedIDs.SharedQuestionnaireIDs) ? "": $@"AND PQ.QuestionnaireID IN ({sharedIDs.SharedQuestionnaireIDs})";
                var sharedWhereConditionCIDs = string.IsNullOrEmpty(sharedIDs.SharedCollaborationIDs) ? "" : $@"AND PC.CollaborationID IN ({sharedIDs.SharedCollaborationIDs})";

                var helperWhereConditionQIDs = string.IsNullOrEmpty(helperColbDetails.SharedQuestionnaireIDs) ? "" : $@"AND PQ.QuestionnaireID IN ({helperColbDetails.SharedQuestionnaireIDs})";
                var helperWhereConditionCIDs = string.IsNullOrEmpty(helperColbDetails.SharedCollaborationIDs) ? "" : $@"AND PC.CollaborationID IN ({helperColbDetails.SharedCollaborationIDs})";
                var helperpersonCollaborationID = string.IsNullOrEmpty(helperColbDetails.SharedCollaborationIDs) ? personCollaborationID : -1;
                string query = $@";WITH WindowOffsets AS
						          (
						          	SELECT
						          		q.QuestionnaireID,ar.Name [Reason],qw.WindowOpenOffsetDays,qw.WindowCloseOffsetDays
						          	FROM 
						          	PersonQuestionnaire pq
						          	JOIN Questionnaire q ON q.QuestionnaireID=pq.QuestionnaireID AND pq.PersonID = {personID}
						          	JOIN QuestionnaireWindow qw ON qw.QuestionnaireID=q.QuestionnaireID 
						          	JOIN info.AssessmentReason ar ON ar.AssessmentReasonID=qw.AssessmentReasonID
						          	WHERE (Pq.PersonQuestionnaireID = {personQuestionnaireID} OR {personQuestionnaireID}=0)
                                        AND ar.Name IN ('Initial','discharge') {sharedWhereConditionQIDs} {helperWhereConditionQIDs}
						          )
						          ,COlCTE AS
                                  (
                                  	 SELECT C.CollaborationID,C.Name AS CollaborationName,PC.EnrollDate AS CollaborationStartDate,
                                            PC.EndDate AS CollaborationEndDate, PC.PersonCollaborationID,PC.IsPrimary, PC.IsCurrent
                                  	 FROM PersonCollaboration PC 
                                  	      JOIN Collaboration C ON C.CollaborationID = PC.CollaborationID
                                  	 WHERE PC.IsRemoved=0 AND PC.PersonID = {personID}
                                     AND (PC.PersonCollaborationID = {personCollaborationID} or {personCollaborationID} = 0) {sharedWhereConditionCIDs} {helperWhereConditionCIDs}
                                  ),
                                  VoiceType AS
                                  (
                                      SELECT VT.VoiceTypeID ,VT.Name AS VoiceTypeName,cases.Name AS NameInDetail,
                                            cases.FKValue,VT.ListOrder
                                     FROM info.VoiceType VT 
                                     CROSS APPLY
                                       (
                                          SELECT P.FirstName+ ' '+isnull(P.MiddleName +' ', '')+ P.LastName As [Name],0 AS FKValue 
                                          FROM Person P where P.PersonID = {personID} AND LOWER(VT.Name) = '{PCISEnum.VoiceType.Consumer.ToLower()}'
                                          UNION ALL
                                          SELECT PS.FirstName+ ' '+isnull(PS.MiddleName+' ', '')+ PS.LastName As [Name],PS.PersonSupportID AS FKValue  
                                          FROM PersonSupport PS where PS.PersonID = {personID} AND LOWER(VT.Name) = '{PCISEnum.VoiceType.Support.ToLower()}'
                                          AND PS.IsRemoved = 0
                                          UNION ALL
                                          SELECT H.FirstName+ ' '+isnull(H.MiddleName+' ', '')+ H.LastName As [Name],PH.PersonHelperID AS FKValue
                                          FROM PersonHelper PH JOIN Helper H ON PH.helperID = H.helperID
									      WHERE PH.PersonID = {personID}  AND LOWER(VT.Name) = '{PCISEnum.VoiceType.Helper.ToLower()}' AND PH.IsRemoved = 0
                                          UNION ALL
                                          SELECT VT.Name,0 WHERE LOWER(VT.Name) = '{PCISEnum.VoiceType.Communimetric.ToLower()}'
                                       ) AS Cases 
                                  ),
                                  AssmCTE AS
                                  (
                                     SELECT a.VoiceTypeID,pq.PersonQuestionnaireID,a.DateTaken,
                                        a.VoiceTypeFKID,
                                        ar.Name [Reason],
										wo_init.WindowOpenOffsetDays,
										wo_disc.WindowCloseOffsetDays,
										(CASE WHEN ({personCollaborationID}=0 AND {helperpersonCollaborationID} = 0) THEN NULL ELSE (SELECT MIN(CAST(CollaborationStartDate AS DATE)) FROM COlCTE) END) [EnrollDate],
										(CASE WHEN ({personCollaborationID}=0 AND {helperpersonCollaborationID} = 0) THEN NULL ELSE (SELECT MAX(CAST(ISNULL(CollaborationEndDate,GETDATE()) AS DATE)) FROM COlCTE) END) [EndDate]
                                     FROM PersonQuestionnaire pq
                                          JOIN Assessment a ON a.PersonQuestionnaireID=pq.PersonQuestionnaireID AND a.IsRemoved=0 AND pq.IsRemoved=0
                                          JOIN info.AssessmentReason ar ON ar.AssessmentReasonID=a.AssessmentReasonID  
                                          JOIN info.AssessmentStatus ast ON ast.AssessmentStatusID=a.AssessmentStatusID 
                                          AND ast.Name IN ('Returned','Submitted', 'Approved')
                                          LEFT JOIN (SELECT * FROM WindowOffsets WHERE Reason='Initial')wo_init ON wo_init.QuestionnaireID=pq.QuestionnaireID 
										  LEFT JOIN (SELECT * FROM WindowOffsets WHERE Reason='Discharge')wo_disc ON wo_disc.QuestionnaireID=pq.QuestionnaireID
                                     WHERE  Pq.PersonID = {personID} {sharedWhereConditionQIDs}   {helperWhereConditionQIDs}                           
                                  )                                   
                                  SELECT distinct VT.* FROM VoiceType VT
                                        JOIN AssmCTE AT On (AT.VoiceTypeID = VT.VoiceTypeID AND ISNULL(AT.VoiceTypeFKID,0) = ISNULL(VT.FKValue,0))
                                        JOIN COlCTE CT ON 
										(
											({personCollaborationID}=0 AND {helperpersonCollaborationID} = 0) OR
											CAST(AT.DateTaken AS DATE) 
											BETWEEN
											DATEADD(DAY,0-ISNULL(AT.WindowOpenOffsetDays,0),AT.EnrollDate) 
											AND 
											DATEADD(DAY,ISNULL(AT.WindowCloseOffsetDays,0),AT.EndDate)
										)Order By VT.VoiceTypeID;";
                var data = ExecuteSqlQuery(query, x => new VoiceTypeDTO
                {
                    VoiceTypeID = x["VoiceTypeID"] == DBNull.Value ? 0 : (int)x["VoiceTypeID"],
                    VoiceTypeName = x["VoiceTypeName"] == DBNull.Value ? null : (string)x["VoiceTypeName"],
                    NameInDetail = x["NameInDetail"] == DBNull.Value ? null : (string)x["NameInDetail"],
                    FkIDValue = x["FKValue"] == DBNull.Value ? null : (long?)x["FKValue"]
                });
                return data;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// GetAllVoiceTypeWithActiveSupports Based on Dates
        /// </summary>
        /// <returns>list of VoiceType</returns>   
        public List<VoiceTypeDTO> GetActiveVoiceTypeInDetail(long personID)
        {
            try
            {
                string query = $@"SELECT VT.VoiceTypeID,VT.Name AS VoiceTypeName,cases.Name AS NameInDetail,cases.FKValue,cases.StartDate,cases.EndDate,VT.ListOrder,
                                cases.IsRemoved,cases.Email,cases.TextPermission,cases.EmailPermission,Id As HelperId
                                 FROM info.VoiceType VT 
                                 CROSS APPLY
                                   (
                                      SELECT P.FirstName+ ' '+isnull(P.MiddleName+' ', '')+ P.LastName As [Name],null AS FKValue,P.StartDate,P.EndDate,P.IsRemoved,P.Email
                                      ,P.TextPermission, P.EmailPermission,0 As Id
                                      FROM Person P where P.PersonID = {personID}  AND LOWER(VT.Name) = '{PCISEnum.VoiceType.Consumer.ToLower()}'
                                      UNION ALL
                                      SELECT PS.FirstName+ ' '+isnull(PS.MiddleName+' ', '')+ PS.LastName As [Name],PS.PersonSupportID AS FKValue,
                                      PS.StartDate,PS.EndDate,PS.IsRemoved,PS.Email,PS.TextPermission, PS.EmailPermission,0 As Id
                                      FROM PersonSupport PS where PS.PersonID = {personID}  
                                      AND LOWER(VT.Name) = '{PCISEnum.VoiceType.Support.ToLower()}' AND (PS.IsRemoved=0 OR PS.UniversalID is null)
                                      UNION ALL
                                      SELECT VT.Name,null,null,null,P.IsRemoved,P.Email,null,null,0 AS Id  FROM Person P WHERE P.PersonID = {personID} AND LOWER(VT.Name) = '{PCISEnum.VoiceType.Communimetric.ToLower()}'
                                      UNION ALL
                                      SELECT H.FirstName+ ' '+isnull(H.MiddleName+' ', '')+ H.LastName As [Name],PH.PersonHelperID AS FKValue,PH.StartDate,PH.EndDate,
                                      PH.IsRemoved,H.Email,null, null,H.HelperId AS Id
                                      FROM Helper H left join PersonHelper PH on PH.HelperID=H.HelperID
                                      where PH.PersonID ={personID}                                        
                                      AND LOWER(VT.Name) = '{PCISEnum.VoiceType.Helper.ToLower()}' AND (PH.IsRemoved=0 OR H.HelperExternalID is null)
                                    ) 
                                 AS Cases Order By VT.VoiceTypeID";
                var data = ExecuteSqlQuery(query, x => new VoiceTypeDTO
                {
                    VoiceTypeID = x["VoiceTypeID"] == DBNull.Value ? 0 : (int)x["VoiceTypeID"],
                    VoiceTypeName = x["VoiceTypeName"] == DBNull.Value ? null : (string)x["VoiceTypeName"],
                    NameInDetail = x["NameInDetail"] == DBNull.Value ? null : (string)x["NameInDetail"],
                    FkIDValue = x["FKValue"] == DBNull.Value ? null : (Int64?)x["FKValue"],
                    StartDate = x["StartDate"] == DBNull.Value ? DateTime.MinValue : (DateTime)x["StartDate"],
                    EndDate = x["EndDate"] == DBNull.Value ? null : (DateTime?)x["EndDate"],
                    IsRemoved = x["IsRemoved"] == DBNull.Value ? false : (bool)x["IsRemoved"],
                    Email= x["Email"] == DBNull.Value ? null : (string)x["Email"],
                    TextPermission= x["TextPermission"] == DBNull.Value ? false : (bool)x["TextPermission"],
                    EmailPermission= x["EmailPermission"] == DBNull.Value ? false : (bool)x["EmailPermission"],
                    HelperId = x["HelperId"] == DBNull.Value ? 0 : (int)x["HelperId"],
                });
                return data;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// GetVoiceType
        /// </summary>
        /// <returns>list of VoiceType</returns>        
        public VoiceType GetVoiceType(int id)
        {
            try
            {
                var readFromCache = this._cache.Get<List<VoiceType>>(PCISEnum.Caching.GetAllVoiceType);
                if (readFromCache == null || readFromCache?.Count == 0)
                {
                    var VoiceType = this.GetRowAsync(x => !x.IsRemoved && x.VoiceTypeID == id).Result;
                    return VoiceType;
                }
                return readFromCache.Where(x => !x.IsRemoved && x.VoiceTypeID == id).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetVoiceTypeForFilter.
        /// </summary>
        /// <param name="personID">personID.</param>
        /// <param name="personQuestionnaireID">personQuestionnaireID.</param>
        /// <returns>VoiceTypeDTO List.</returns>
        public List<VoiceTypeDTO> GetVoiceTypeForFilter(long personID, long personQuestionnaireID, string sharedAssessmentIDS)
        {
            try
            {
                var sharedCondition = string.IsNullOrEmpty(sharedAssessmentIDS) ? "" : $@"AND A.AssessmentID IN ({sharedAssessmentIDS})";
                string query = $@";with ABC as(
                           SELECT PersonSupportID ID,FirstName+' '+ LastName NameInDetail,'Support' as [Type],IsRemoved from PersonSupport
                               where PersonID = {personID}  
                           UNION ALL
                           SELECT  ph.PersonHelperID ID,FirstName+' '+ LastName NameInDetail,'Helper' as [Type],ph.IsRemoved 
                           FROM Helper h JOIN dbo.PersonHelper  ph ON h.HelperID=ph.HelperID AND ph.PersonID = {personID}  
                           UNION ALL
                           SELECT null,P.FirstName+ ' '+ P.LastName As NameInDetail,'Consumer' AS [Type],P.IsRemoved IsRemoved
                                     FROM Person P where P.PersonID = {personID}   
                            UNION ALL
                            SELECT null,'Communimetric','Communimetric' AS [Type],P.IsRemoved as IsRemoved
                                     FROM Person P where P.PersonID = {personID}  
                           ),
                           VoiceTypesFromAssessment AS
						   (
						     SELECT DISTINCT A.VoiceTypeID,VT.[Name],ISNULL(A.VoiceTypeFKID,0) AS VoiceTypeFKID 
							 from  [dbo].[Assessment] A
							 JOIN  [dbo].[PersonQuestionnaire] PQ on PQ.PersonQuestionnaireID = A.PersonQuestionnaireID
							 JOIN [info].[VoiceType] VT on VT.VoiceTypeID=A.VoiceTypeID
							   where PQ.PersonID= {personID } and PQ.QuestionnaireID= { personQuestionnaireID } AND A.IsRemoved = 0 
                               {sharedCondition}
							)
                           select distinct VoiceTypeID,[Name] VoiceTypeName,VoiceTypeFKID FKValue,NameInDetail,IsRemoved from
                           VoiceTypesFromAssessment A 
                           left join ABC AB on  A.Name = AB.Type and (A.VoiceTypeFKID= AB.ID OR AB.ID IS NULL) where NameInDetail is not null";

                var data = ExecuteSqlQuery(query, x => new VoiceTypeDTO
                {
                    VoiceTypeID = x["VoiceTypeID"] == DBNull.Value ? 0 : (int)x["VoiceTypeID"],
                    VoiceTypeName = x["VoiceTypeName"] == DBNull.Value ? null : (string)x["VoiceTypeName"],
                    NameInDetail = x["NameInDetail"] == DBNull.Value ? null : (string)x["NameInDetail"],
                    FkIDValue = x["FKValue"] == DBNull.Value ? null : (long?)x["FKValue"],
                    IsRemoved = x["IsRemoved"] == DBNull.Value ? false : (bool)x["IsRemoved"],
                });
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
