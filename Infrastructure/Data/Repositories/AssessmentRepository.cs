// -----------------------------------------------------------------------
// <copyright file="AssessmentRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.DTO.ExternalAPI;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Infrastructure.Data.Repositories.Base;
using Opeeka.PICS.Infrastructure.Enums;

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{
    public class AssessmentRepository : BaseRepository<Assessment>, IAssessmentRepository
    {
        private readonly IMapper mapper;
        private readonly ILogger<AssessmentRepository> logger;
        private readonly OpeekaDBContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="AssessmentRepository"/> class.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public AssessmentRepository(ILogger<AssessmentRepository> logger, OpeekaDBContext dbContext, IMapper mapper)
            : base(dbContext)
        {
            this.mapper = mapper;
            this.logger = logger;
            this._dbContext = dbContext;
        }

        /// <summary>
        /// GetAssessmentDetails.
        /// </summary>
        /// <param name="personIndex">Index of a person.</param>
        /// <param name="questionnaireId">ID of a Questionnaire.</param>
        /// <param name="date">date.</param>
        /// <returns>AssessmentDetailsResponseDTO.</returns>
        public List<AssessmentDetailsDTO> GetAssessmentDetails(Guid personIndex, int questionnaireId, DateTime? date, string sharedAssessmentIDs, long loggedInAgencyID, string helpersAssessmentIDs,int pageNumber, int pageSize)
        {
            try
            {
                var sharedWhereCondition = string.Empty;
                var personAgencyCondition = $@"AND P.AgencyID = {loggedInAgencyID}";
                if (!string.IsNullOrEmpty(sharedAssessmentIDs))
                {
                    sharedWhereCondition = $@"AND Q.AssessmentID IN ({sharedAssessmentIDs})";
                    personAgencyCondition = string.Empty;
                }
                var helperColbWhereCondition = string.Empty;
                if (!string.IsNullOrEmpty(helpersAssessmentIDs))
                {
                    helperColbWhereCondition = $@"AND Q.AssessmentID IN ({helpersAssessmentIDs})";
                }
                List<AssessmentDetailsDTO> AssessmentDetailsDTO = new List<AssessmentDetailsDTO>();
                var query = string.Empty;
                var queryDaysInService = $@"DATEDIFF(day, CAST((select top 1 MIN(EnrollDate) from PersonCollaboration join Person on PersonCollaboration.PersonID = Person.PersonID where Person.PersonIndex = '{ personIndex }' and PersonCollaboration.isremoved = 0) AS DATE), CAST(q.[DateTaken] AS Date)) AS DaysInService";
                if (date != null)
                {
                    queryDaysInService = $@"DATEDIFF(day, CAST('{ date }' AS Date), CAST(q.[DateTaken] AS Date)) AS DaysInService";
                }

                query = $@";WITH CTE AS (
                          SELECT q.AssessmentID,
                                 pq.PersonID, 
	                             q.AssessmentStatusID,
                                 qs.[Name] AS [QuestionnaireStatus], 
	                             q.[DateTaken] AS [Assementdate],
                                 q.[ReasoningText] AS Note,
	                             q.AssessmentReasonID AS AssessmentReasonID,	
                                 q.[CloseDate] AS SubmittedDate,
	                             q.PersonQuestionnaireID,
                                 qs.[Name] AS [AssessmentStatus],
	                             q.ReasoningText,
                                 q.Approved,	
                                 q.VoiceTypeID,
                                 q.VoiceTypeFKID,
	                             q.PersonQuestionnaireScheduleID,
                                 q.IsUpdate,
                                 q.EventDate,q.EventNotes,
	                             {queryDaysInService} 
                          	FROM Assessment q 
                          	LEFT JOIN info.AssessmentStatus qs ON q.AssessmentStatusID = qs.AssessmentStatusID AND qs.IsRemoved=0
                          	LEFT JOIN PersonQuestionnaire pq ON q.PersonQuestionnaireID=pq.PersonQuestionnaireID
                          	LEFT JOIN Person p ON p.PersonID=pq.PersonID {personAgencyCondition}
                          	WHERE q.IsRemoved=0 AND p.PersonIndex = '{personIndex}' AND pq.QuestionnaireID = {questionnaireId}
                            {sharedWhereCondition} {helperColbWhereCondition}
                          ) 
                          SELECT COUNT(*) OVER() AS TotalCount,
                                 CTE.AssessmentID,CTE.PersonID, 
	                             CTE.AssessmentStatusID,CTE.[QuestionnaireStatus], 
	                             CTE.[Assementdate],CTE.Note,
	                             CTE.AssessmentReasonID,CTE.SubmittedDate,
	                             CTE.PersonQuestionnaireID,CTE.[AssessmentStatus],
	                             CTE.ReasoningText,CTE.Approved,CTE.VoiceTypeID,CTE.VoiceTypeFKID,
	                             CTE.PersonQuestionnaireScheduleID,CTE.IsUpdate,
                                 CTE.EventDate,CTE.EventNotes,
                                 CTE.DaysInService,TF.Timeframe_Std AS TimePeriod
                          FROM CTE 
                          LEFT JOIN info.Timeframe TF on TF.DaysInService = CTE.DaysInService
                          ORDER BY CTE.[Assementdate] desc  OFFSET {((pageNumber - 1) * pageSize)} ROWS FETCH NEXT { pageSize} ROWS ONLY";


                AssessmentDetailsDTO = ExecuteSqlQuery(query, x => new AssessmentDetailsDTO
                {
                    TotalCount = x["TotalCount"] == DBNull.Value ? 0 : (int)x["TotalCount"],
                    AssessmentID = x["AssessmentID"] == DBNull.Value ? 0 : (int)x["AssessmentID"],
                    PersonID = x["PersonID"] == DBNull.Value ? 0 : (long)x["PersonID"],
                    AssessmentStatusID = x["AssessmentStatusID"] == DBNull.Value ? 0 : (int)x["AssessmentStatusID"],
                    QuestionnaireStatus = x["QuestionnaireStatus"] == DBNull.Value ? null : (string)x["QuestionnaireStatus"],
                    Date = x["Assementdate"] == DBNull.Value ? DateTime.MinValue : (DateTime)x["Assementdate"],
                    Note = x["Note"] == DBNull.Value ? null : (string)x["Note"],
                    AssessmentReasonID = x["AssessmentReasonID"] == DBNull.Value ? 0 : (int)x["AssessmentReasonID"],
                    SubmittedDate = x["SubmittedDate"] == DBNull.Value ? null : (DateTime?)x["SubmittedDate"],
                    PersonQuestionnaireID = x["PersonQuestionnaireID"] == DBNull.Value ? 0 : (long)x["PersonQuestionnaireID"],
                    Status = x["AssessmentStatus"] == DBNull.Value ? null : (string)x["AssessmentStatus"],
                    ReasoningText = x["ReasoningText"] == DBNull.Value ? null : (string)x["ReasoningText"],
                    Approved = x["Approved"] == DBNull.Value ? null : (int?)x["Approved"],
                    VoiceTypeID = x["VoiceTypeID"] == DBNull.Value ? 0 : (int)x["VoiceTypeID"],
                    PersonQuestionnaireScheduleID = x["PersonQuestionnaireScheduleID"] == DBNull.Value ? null : (long?)x["PersonQuestionnaireScheduleID"],
                    IsUpdate = x["IsUpdate"] == DBNull.Value ? false : (bool)x["IsUpdate"],
                    EventDate = x["EventDate"] == DBNull.Value ? null : (DateTime?)x["EventDate"],
                    EventNotes = x["EventNotes"] == DBNull.Value ? null : (string)x["EventNotes"],
                    DaysInProgram = x["DaysInService"] == DBNull.Value ? null : (int?)x["DaysInService"],
                    TimePeriod = x["TimePeriod"] == DBNull.Value ? null : (string)x["TimePeriod"],
                    VoiceTypeFKID = x["VoiceTypeFKID"] == DBNull.Value ? 0 : (long)x["VoiceTypeFKID"]
                });
                return AssessmentDetailsDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// AddAssessment
        /// </summary>
        /// <param name="assessment"></param>
        /// <returns>Assessment</returns>
        public Assessment AddAssessment(Assessment assessment)
        {
            try
            {
                var result = this.AddAsync(assessment).Result;
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// To get Assessment details.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns>Assessment.</returns>
        public async Task<Assessment> GetAssessment(int id)
        {
            try
            {
                Assessment assessment = await this.GetRowAsync(x => x.AssessmentID == id);
                return assessment;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// To Update Assessment.
        /// </summary>
        /// <param name="assessment">assessment.</param>
        /// <returns>List of summaries.</returns>
        public Assessment UpdateAssessment(Assessment assessment)
        {
            try
            {
                Assessment result = this.UpdateAsync(assessment).Result;
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetPersonQuestionnaireID
        /// </summary>
        /// <param name="personIndex"></param>
        /// <param name="questionnaireID"></param>
        /// <returns>long</returns>
        public long GetPersonQuestionnaireID(Guid personIndex, int questionnaireID)
        {
            try
            {
                return (from P in this._dbContext.Person
                        join PQ in this._dbContext.PersonQuestionnaires on P.PersonID equals PQ.PersonID
                        where P.PersonIndex == personIndex && PQ.QuestionnaireID == questionnaireID && PQ.IsRemoved == false
                        select PQ.PersonQuestionnaireID).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetPersonQuestionnaireID
        /// </summary>
        /// <param name="personIndex"></param>
        /// <param name="questionnaireID"></param>
        /// <returns>long</returns>
        public Assessment GetAssessmentByPersonQuestionaireID(long personQuestionnaireID, int assessmentStatusID)
        {
            try
            {
                var data = (from P in this._dbContext.PersonQuestionnaires
                            where P.PersonQuestionnaireID == personQuestionnaireID
                            select P).FirstOrDefault();
                if (data != null)
                {
                    return (from P in this._dbContext.PersonQuestionnaires
                            join A in this._dbContext.Assessments on P.PersonQuestionnaireID equals A.PersonQuestionnaireID
                            where P.PersonID == data.PersonID && P.QuestionnaireID == data.QuestionnaireID && A.AssessmentStatusID == assessmentStatusID
                            orderby A.DateTaken descending
                            select A).FirstOrDefault();
                }
                else
                {
                    return new Assessment();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetPersonIdFromAssessment
        /// </summary>
        /// <param name="assessmentId"></param>
        /// <returns>long PersonId</returns>
        public long GetPersonIdFromAssessment(int assessmentId)
        {
            var PersonId = this._dbContext.Assessments
                 .Where(x => x.AssessmentID == assessmentId && x.IsRemoved == false)
                 .Include(x => x.PersonQuestionnaire)
                 .Select(x => x.PersonQuestionnaire.PersonID).FirstOrDefault();
            return PersonId;
        }

        /// <summary>
        /// GetLastAssessmentByPerson On Reports filterChange
        /// </summary>
        /// <param name="personID">personID</param>
        /// <param name="questionnaireID">questionnaireID</param>
        /// <returns></returns>
        public List<Assessment> GetLastAssessmentByPerson(long personID, long personQuestionnaireID, long personCollaborationID, int voiceTypeID, long voiceTypeFKID, SharedDetailsDTO sharedIDs, long loggedInAgencyID, SharedDetailsDTO helperColbDTO, int rowCount = 1)
        {
            try
            {
                var personAgencyCondition = string.Empty;
                var sharedWhereConditionQIDs = string.IsNullOrEmpty(sharedIDs.SharedQuestionnaireIDs) ? "" : $@"AND PQ.QuestionnaireID IN ({sharedIDs.SharedQuestionnaireIDs})";
                var sharedWhereConditionCIDs = string.IsNullOrEmpty(sharedIDs.SharedCollaborationIDs) ? "" : $@"AND PC.CollaborationID IN ({sharedIDs.SharedCollaborationIDs})";

                var helperWhereConditionQIDs = string.IsNullOrEmpty(helperColbDTO.SharedQuestionnaireIDs) ? "" : $@"AND PQ.QuestionnaireID IN ({helperColbDTO.SharedQuestionnaireIDs})";
                var helperWhereConditionCIDs = string.IsNullOrEmpty(helperColbDTO.SharedCollaborationIDs) ? "" : $@"AND PC.CollaborationID IN ({helperColbDTO.SharedCollaborationIDs})";
                var helperpersonCollaborationID = string.IsNullOrEmpty(helperColbDTO.SharedCollaborationIDs) ? personCollaborationID : -1;
                if (string.IsNullOrEmpty(sharedIDs.SharedQuestionnaireIDs)) 
                {
                    personAgencyCondition = $@"AND P.AgencyID = {loggedInAgencyID}";
                }
                List<Assessment> assessment = new List<Assessment>();
                var query = string.Empty;
                var selectTop = rowCount == 0 ? "" : "TOP " + rowCount;
                query = @$";WITH WindowOffsets AS
						    (
						    	SELECT
						    		q.QuestionnaireID,ar.Name [Reason],qw.WindowOpenOffsetDays,qw.WindowCloseOffsetDays,PQ.PersonQuestionnaireID
						    	FROM 
						    	PersonQuestionnaire pq
						    	JOIN Questionnaire q ON q.QuestionnaireID=pq.QuestionnaireID AND pq.PersonID = {personID}
						    	JOIN QuestionnaireWindow qw ON qw.QuestionnaireID=q.QuestionnaireID 
						    	JOIN info.AssessmentReason ar ON ar.AssessmentReasonID=qw.AssessmentReasonID
						    	WHERE ar.Name IN ('Initial','discharge') AND (PQ.PersonQuestionnaireID = {personQuestionnaireID} OR {personQuestionnaireID}=0) 
						    ) 
						    ,SelectedCollaboration AS
						    ( 
						    	 SELECT PC.EnrollDate AS CollaborationStartDate,
                                                PC.EndDate AS CollaborationEndDate, PC.PersonCollaborationID,PC.IsPrimary, PC.IsCurrent
                                      	 FROM PersonCollaboration PC                                  	      
                                      	 WHERE PC.IsRemoved=0 AND PC.PersonID = {personID} {sharedWhereConditionCIDs} {helperWhereConditionCIDs}
                                         AND (PC.PersonCollaborationID = {personCollaborationID} or {personCollaborationID} = 0) 
						    )
						    ,SelectedAssessments AS
						    (
						    	SELECT
						    		a.*,					
						    		wo_init.WindowOpenOffsetDays,
						    		wo_disc.WindowCloseOffsetDays,
						    		(CASE WHEN ({personCollaborationID}=0 AND {helperpersonCollaborationID} = 0) THEN NULL ELSE (SELECT MIN(CAST(CollaborationStartDate AS DATE)) FROM SelectedCollaboration) END) [EnrollDate],
						    		(CASE WHEN ({personCollaborationID}=0 AND {helperpersonCollaborationID} = 0) THEN NULL ELSE (SELECT MAX(CAST(ISNULL(CollaborationEndDate,GETDATE()) AS DATE)) FROM SelectedCollaboration) END) [EndDate]
						    	FROM
						    	Assessment a	
						    	JOIn PersonQuestionnaire PQ ON PQ.PersonQuestionnaireID = a.PersonQuestionnaireID AND PQ.PersonID = {personID} AND pq.IsRemoved=0
                                AND (PQ.QuestionnaireID = (SELECT QuestionnaireID FROM PersonQuestionnaire WHERE PersonQuestionnaireID = {personQuestionnaireID}) OR {personQuestionnaireID}=0) 
                                AND a.IsRemoved = 0 {sharedWhereConditionQIDs} {helperWhereConditionQIDs}
                                JOIN Person P ON PQ.PersonID = P.PersonID {personAgencyCondition}
						    	JOIN info.AssessmentStatus ast ON ast.AssessmentStatusID=a.AssessmentStatusID AND ast.Name in ('Returned','Submitted','Approved')  
                                LEFT JOIN (SELECT * FROM WindowOffsets WHERE Reason='Initial')wo_init ON wo_init.PersonQuestionnaireID=a.PersonQuestionnaireID 
						    	LEFT JOIN (SELECT * FROM WindowOffsets WHERE Reason='Discharge')wo_disc ON wo_disc.PersonQuestionnaireID=a.PersonQuestionnaireID
						    	WHERE (ISNULL(A.VoiceTypeFKID,0) = {voiceTypeFKID} OR {voiceTypeFKID} = 0)  AND ({voiceTypeID}=0 OR a.VoiceTypeID={voiceTypeID}) 
						    )
						    SELECT DISTINCT {selectTop} SA.AssessmentID,SA.PersonQuestionnaireID,SA.VoiceTypeID,SA.VoiceTypeFKID,SA.DateTaken,SA.ReasoningText,
						          SA.AssessmentReasonID,SA.AssessmentStatusID,SA.PersonQuestionnaireScheduleID, SA.IsUpdate,
						    	  SA.Approved,SA.CloseDate,SA.IsRemoved,SA.UpdateDate,SA.UpdateUserID,SA.EventDate,SA.EventNotes
						    FROM SelectedAssessments SA
						    JOIN SelectedCollaboration CT ON 
						    (					
						    	({personCollaborationID}=0 AND {helperpersonCollaborationID} = 0) OR
						    	CAST(SA.DateTaken AS DATE) 
						    	BETWEEN
						    	DATEADD(DAY,0-ISNULL(SA.WindowOpenOffsetDays,0),CAST(SA.EnrollDate AS DATE)) 
						    	AND 
						    	DATEADD(DAY,ISNULL(SA.WindowCloseOffsetDays,0),ISNULL(SA.EndDate, CAST(GETDATE() AS DATE)))	)					
						    ORDER BY sa.DateTaken desc";
                assessment = ExecuteSqlQuery(query, x => new Assessment
                {
                    AssessmentID = x["AssessmentID"] == DBNull.Value ? 0 : (int)x["AssessmentID"],
                    PersonQuestionnaireID = x["PersonQuestionnaireID"] == DBNull.Value ? 0 : (long)x["PersonQuestionnaireID"],
                    VoiceTypeID = x["VoiceTypeID"] == DBNull.Value ? 0 : (int)x["VoiceTypeID"],
                    VoiceTypeFKID = x["VoiceTypeFKID"] == DBNull.Value ? 0 : (long)x["VoiceTypeFKID"],
                    DateTaken = x["DateTaken"] == DBNull.Value ? DateTime.MinValue : (DateTime)x["DateTaken"],
                    ReasoningText = x["ReasoningText"] == DBNull.Value ? null : (string)x["ReasoningText"],
                    AssessmentReasonID = x["AssessmentReasonID"] == DBNull.Value ? 0 : (int)x["AssessmentReasonID"],
                    AssessmentStatusID = x["AssessmentStatusID"] == DBNull.Value ? 0 : (int)x["AssessmentStatusID"],
                    PersonQuestionnaireScheduleID = x["PersonQuestionnaireScheduleID"] == DBNull.Value ? null : (long?)x["PersonQuestionnaireScheduleID"],
                    IsUpdate = x["IsUpdate"] == DBNull.Value ? false : (bool)x["IsUpdate"],
                    Approved = x["Approved"] == DBNull.Value ? null : (int?)x["Approved"],
                    CloseDate = x["CloseDate"] == DBNull.Value ? null : (DateTime?)x["CloseDate"],
                    IsRemoved = x["IsRemoved"] == DBNull.Value ? false : (bool)x["IsRemoved"],
                    UpdateDate = x["UpdateDate"] == DBNull.Value ? DateTime.MinValue : (DateTime)x["UpdateDate"],
                    EventDate = x["EventDate"] == DBNull.Value ? null : (DateTime?)x["EventDate"],
                    EventNotes = x["EventNotes"] == DBNull.Value ? null : (string)x["EventNotes"]
                });

                return assessment;
            }
            catch (Exception)
            {

                throw;
            }

        }


        public Questionnaire GetQuestionDetailsFromAssessment(int assessmentId)
        {
            var questionDetails = this._dbContext.Assessments
                 .Where(x => x.AssessmentID == assessmentId && x.IsRemoved == false)
                 .Include(x => x.PersonQuestionnaire)
                 .ThenInclude(x=>x.Questionnaire)
                 .Select(x => x.PersonQuestionnaire.Questionnaire).FirstOrDefault();
            return questionDetails;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assessmentResponse"></param>
        /// <returns></returns>
        public List<Assessment> AddBulkAssessments(List<Assessment> assessments)
        {
            try
            {
                var res = this.AddBulkAsync(assessments);
                res.Wait();
                return assessments;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// GetAssessmentListByGUID.
        /// </summary>
        /// <param name="AssessmentResponseIDList"></param>
        /// <returns></returns>
        public List<Assessment> GetAssessmentListByGUID(List<Guid?> assessmentGUIDList)
        {
            try
            {
                var query = @$"SELECT AssessmentID, AssessmentGuid FROM Assessment WHERE AssessmentGUID in ('{string.Join("','", assessmentGUIDList)}')";

                var data = ExecuteSqlQuery(query, x => new Assessment
                {
                    AssessmentID = x["AssessmentID"] == DBNull.Value ? 0 : (int)x["AssessmentID"],
                    AssessmentGUID = x["AssessmentGUID"] == DBNull.Value ? null : (Guid?)x["AssessmentGUID"],
                }).ToList();
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// GetAssessmentListByIDs.
        /// </summary>
        /// <param name="AssessmentResponseIDList"></param>
        /// <returns></returns>
        public List<Assessment> GetAssessmentListByID(List<int> assessmentIDList)
        {
            try
            {
                var assessments = this._dbContext.Assessments.Where(x => assessmentIDList.Contains(x.AssessmentID)).ToList();
                return assessments;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// GetAssessmentDetailsForEHRUpdate.
        /// </summary>
        /// <param name="ehrAssessmentIDs"></param>
        /// <returns></returns>
        public List<EHRAssessmentDTO> GetAssessmentDetailsForEHRUpdate(string ehrAssessmentIDs)
        {
            try
            {
                var query = $@";WITH AssessmentCTE AS 
                                (
                        	        SELECT A.AssessmentID,A.UpdateUserID,A.UpdateDate,A.PersonQuestionnaireID FROM Assessment  A
                        	        WHERE A.AssessmentID IN ({ehrAssessmentIDs}) AND A.IsRemoved = 0
                                ) SELECT A.AssessmentID,
                                       PQ.PersonQuestionnaireID,
                                	   H.HelperID,
                                       A.UpdateDate AS ApprovedDate,
                                	   H.HelperExternalID,
                                	   PQ.QuestionnaireID,
                                	   P.AgencyID,
                                	   p.UniversalID AS PersonUniversalID,
                                	   I.Abbrev AS InstrumentAbbrev,
                                	   A.UpdateUserID
                                FROM AssessmentCTE  A
                                JOIN PersonQuestionnaire PQ on A.PersonQuestionnaireID = PQ.PersonQuestionnaireID 
                                JOIN Questionnaire Q ON Q.QuestionnaireID = PQ.QuestionnaireID
                                JOIN Info.Instrument I ON I.InstrumentID = Q.InstrumentID
                                JOIN Person P ON PQ.PersonID = P.PersonID AND ISNULL(p.UniversalID,'') <> ''
                                JOIN personHelper PH ON P.PersonID = PH.PersonID AND ph.IsLead = 1 AND PH.IsRemoved = 0
                                         JOIN Helper H ON H.HelperID = PH.HelperID
                                WHERE ISNULL(H.HelperExternalID,'') <> ''";
                var result = ExecuteSqlQuery(query, x => new EHRAssessmentDTO
                {
                    AssessmentID = x["AssessmentID"] == DBNull.Value ? 0 : (int)x["AssessmentID"],
                    PersonQuestionnaireID = x["PersonQuestionnaireID"] == DBNull.Value ? 0 : (long)x["PersonQuestionnaireID"],
                    HelperID = x["HelperID"] == DBNull.Value ? 0 : (int)x["HelperID"],
                    ApprovedDate = x["ApprovedDate"] == DBNull.Value ? DateTime.UtcNow : (DateTime)x["ApprovedDate"],
                    AgencyID = x["AgencyID"] == DBNull.Value ? 0 : (long)x["AgencyID"],
                    HelperExternalID = x["HelperExternalID"] == DBNull.Value ? string.Empty : (string)x["HelperExternalID"],
                    InstrumentAbbrev = x["InstrumentAbbrev"] == DBNull.Value ? string.Empty : (string)x["InstrumentAbbrev"],
                    PersonUniversalID = x["PersonUniversalID"] == DBNull.Value ? string.Empty : (string)x["PersonUniversalID"],
                    QuestionnaireID = x["QuestionnaireID"] == DBNull.Value ? 0 : (int)x["QuestionnaireID"]
                });
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// GetAssessmentIDsForEHRUpdate.
        /// </summary>
        /// <param name="approvedStatusID"></param>
        /// <param name="agencyId"></param>
        /// <param name="EHRUpdateStatus"></param>
        /// <returns></returns>
        public List<string> GetAssessmentIDsForEHRUpdate(int approvedStatusID, long agencyId, string EHRUpdateStatus)
        {
            try
            {
                var query = $@"SELECT A.AssessmentID FROM Assessment  A
                                         JOIN PersonQuestionnaire PQ on A.PersonQuestionnaireID = PQ.PersonQuestionnaireID 
										 JOIN Person P ON PQ.PersonID = P.PersonID AND ISNULL(p.UniversalID,'') <> '' AND P.agencyID = {agencyId}
                                         JOIN personHelper PH ON P.PersonID = PH.PersonID AND ph.IsLead = 1 AND PH.IsRemoved = 0
                                         JOIN Helper H ON H.HelperID = PH.HelperID
                                          WHERE A.AssessmentStatusID = {approvedStatusID} AND A.IsRemoved = 0 AND A.EHRUpdateStatus = '{EHRUpdateStatus}'
                                          AND ISNULL(H.HelperExternalID,'') <> ''";
                var result = ExecuteSqlQuery(query, x => new string(x["AssessmentID"] == DBNull.Value ? null : x["AssessmentID"].ToString()));
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// UpdateBulkAssessments.
        /// </summary>
        /// <param name="assessments"></param>
        /// <returns></returns>
        public List<Assessment> UpdateBulkAssessments(List<Assessment> assessments)
        {
            try
            {
                var res = this.UpdateBulkAsync(assessments);
                res.Wait();
                return assessments;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<AssessmentDetailsListDTO> GetAssessmentDetailsListsForExternal(LoggedInUserDTO loggedInUserDTO, DynamicQueryBuilderDTO queryBuilderDTO, Guid? helperIndex)
        {

            try
            {
                var query = string.Empty;
                var helperJoinCondition = string.Empty;
                if(helperIndex != null)
                {
                    helperJoinCondition = $@"JOIN personHelper PH ON P.PersonID = PH.PersonID AND PH.IsRemoved = 0
                                             JOIN Helper H ON H.HelperID = PH.HelperID AND H.IsRemoved = 0";
                }
                var queryBuilder = queryBuilderDTO.WhereCondition + queryBuilderDTO.OrderBy + queryBuilderDTO.Paginate;
                int supportVoiceTypeID = _dbContext.VoiceTypes.Where(x => x.Name == PCISEnum.VoiceType.Support).FirstOrDefault().VoiceTypeID;
                int personVoiceTypeID = _dbContext.VoiceTypes.Where(x => x.Name == PCISEnum.VoiceType.Consumer).FirstOrDefault().VoiceTypeID;
                query = $@";WITH PersonCTE AS(
                        	    SELECT P.PersonID,P.PersonIndex,P.FirstName+ COALESCE(CASE P.MiddleName WHEN '' THEN '' ELSE ' '+P.MiddleName END, ' '+P.MiddleName, '') 
                        	    				 + COALESCE(CASE P.LastName WHEN '' THEN '' ELSE ' '+P.LastName END, ' '+P.LastName, '') [PersonName] 
								FROM Person P with (nolock)
                        	    Where P.AgencyID = {loggedInUserDTO.AgencyId} AND P.IsRemoved = 0 AND P.IsActive = 1
                            ),
							AssessmnetChildResponses AS
                            ( 
                              SELECT b.AssessmentResponseID, I.Name AS ChildItemName, I.ResponseValueTypeID, b.ItemID AS ChildItemID, b.Value, b.GroupNumber, b.ParentAssessmentResponseID
                            						  FROM AssessmentResponse b 
													  INNER JOIN Item I ON B.ItemID = I.ItemID AND I.ParentItemID IS NOT NULL
                            						  WHERE  ParentAssessmentResponseID is not null AND b.IsRemoved = 0
                            						  AND b.Groupnumber is not null AND I.IsRemoved = 0
                            ),AssessmentsCTE AS (
                            SELECT COUNT(A.AssessmentID) OVER() TotalCount, A.AssessmentID,
	                                A.[DateTaken] AS [Assementdate],
                                    P.PersonIndex, P.PersonName,
	                                PQ.QuestionnaireID,A.PersonQuestionnaireID,A.PersonQuestionnaireScheduleID,
	                                A.AssessmentStatusID,
	                                A.AssessmentReasonID AS AssessmentReasonID,	
	                                A.ReasoningText AS ReasonNote,
                                    A.EventDate,A.EventNotes,
                                    A.[CloseDate] AS SubmittedDate,
                                    A.Approved,	
                                    A.VoiceTypeID,
                                    A.VoiceTypeFKID,
                                    A.IsUpdate,A.UpdateUserID,A.UpdateDate,
							        DATEDIFF(day, CAST((SELECT TOP 1 MIN(EnrollDate) FROM PersonCollaboration PC
					         WHERE PC.PersonID = P.PersonID AND PC.IsRemoved = 0) AS DATE), 
					        	 CAST(A.[DateTaken] AS Date)) 
					        	  AS DaysInService								 
					         FROM Assessment A with (nolock)
                               	JOIN PersonQuestionnaire pq with (nolock) ON A.PersonQuestionnaireID=pq.PersonQuestionnaireID
								JOIn Questionnaire Q with (nolock)  on PQ.QuestionnaireID = Q.QuestionnaireID
                               	JOIN PersonCTE p ON p.PersonID=pq.PersonID
						        {helperJoinCondition}  
								WHERE A.IsRemoved = 0 AND Q.IsBaseQuestionnaire = 0  
                                {queryBuilder}					         
                            )
                           SELECT CTE.TotalCount, CTE.AssessmentID,CTE.[Assementdate],CTE.QuestionnaireID,
                                  CTE.PersonIndex, CTE.PersonName,
	                              CTE.AssessmentStatusID,AST.[Name] AS [AssessmentStatus], 
	                              CTE.ReasonNote,
	                              CTE.AssessmentReasonID,ASR.Name AS AssessmentReason, CTE.SubmittedDate,
	                              CTE.PersonQuestionnaireID,CTE.PersonQuestionnaireScheduleID,
	                              CTE.Approved,CTE.VoiceTypeID,CTE.VoiceTypeFKID,V.Name AS VoiceType,
	                              CTE.IsUpdate,CTE.EventDate,CTE.EventNotes,
                                  CTE.DaysInService,TF.Timeframe_Std AS TimePeriod,CTE.UpdateUserID,CTE.UpdateDate,
					              AssessmResponses = (SELECT Count(*) OVER() AS TotalCount,
									 CTE.AssessmentID,
									 AR.AssessmentResponseID,
									 AR.QuestionnaireItemID,I.ItemID,I.name AS ItemName, QI.categoryID, C.Name AS Category,
									 AR.ItemResponseBehaviorID , IRB.Name AS ItemResponseBehavior, I.ItemResponseTypeID, IRT.Name AS ItemResponseType,
									 AR.[ResponseId], R.KeyCodes,I.ResponseValueTypeID, R.[Value] ResponseValue, AR.Value as AssessmentResponseValue,
									 ISNULL(AR.PersonSupportID, 0) PersonSupportID, AR.IsCloned,
									 CASE WHEN I.UseRequiredConfidentiality = 1 THEN AR.IsRequiredConfidential
									 ELSE I.UseRequiredConfidentiality END  AS IsRequiredConfidential,
									 CASE WHEN I.UsePersonRequestedConfidentiality = 1 THEN AR.IsPersonRequestedConfidential
									 ELSE I.UsePersonRequestedConfidentiality END  AS IsPersonRequestedConfidential, 
									 CASE WHEN I.UseOtherConfidentiality = 1 THEN AR.IsOtherConfidential
									 ELSE I.UseOtherConfidentiality END  AS IsOtherConfidential, 
					                 ChildItemResponses =  CASE WHEN R.displaychilditem = 1 THEN
										(SELECT b.AssessmentResponseID, b.ResponseValueTypeID, b.ChildItemName, b.ChildItemID, b.Value AS ChildResponseValue, b.GroupNumber, b.ParentAssessmentResponseID
											FROM AssessmnetChildResponses b 
										 WHERE  b.ParentAssessmentResponseID = AR.AssessmentResponseID Order by b.GroupNumber FOR JSON PATH)
										 ELSE null END,
									 AssessmResponseNotes = (SELECT
								         n.NoteId,  
								         n.NoteText,
								         n.UpdateDate,
								         CASE WHEN n.VoiceTypeFKID is null THEN u.Name
	                                     WHEN n.VoiceTypeFKID is not null AND n.AddedByVoiceTypeID = {personVoiceTypeID} THEN	p.FirstName+ ' ' + p.MiddleName + ' ' + p.LastName
			                             WHEN n.VoiceTypeFKID is not null AND n.AddedByVoiceTypeID = {supportVoiceTypeID} THEN	pS.FirstName+ ' ' + pS.MiddleName + ' ' + pS.LastName END as Author
								              FROM AssessmentResponseNote qrn
								         INNER JOIN Note n ON qrn.NoteId = n.NoteId and n.IsRemoved=0
								         LEFT JOIN [User] U ON n.UpdateUserId = u.UserId
								         LEFT JOIN Person p ON n.VoiceTypeFKID = p.PersonID	AND VoiceTypeFKID is not null
								         LEFT JOIN PersonSupport ps ON n.VoiceTypeFKID = ps.PersonSupportID AND VoiceTypeFKID is not null
								         WHERE qrn.AssessmentResponseID = AR.AssessmentResponseID   FOR JSON PATH)
									 FROM AssessmentResponse AR with (nolock)
									 INNER JOIN Response R with (nolock) ON AR.ResponseId = R.ResponseId AND R.IsRemoved = 0
									 INNER JOIN QuestionnaireItem QI with (nolock) ON AR.QuestionnaireItemID = QI.QuestionnaireItemID and QI.IsRemoved=0
									 INNER JOIN Item I with (nolock) on QI.itemID = I.itemID
									 INNER JOIN info.Category C with (nolock) on QI.CategoryID = C.CategoryID
									 LEFT join info.ItemResponseBehavior IRB on IRB.ItemResponseBehaviorID=AR.ItemResponseBehaviorID and IRB.IsRemoved=0
									 INNER join info.ItemResponseType IRT on I.ItemResponseTypeID=IRT.ItemResponseTypeID  
									 WHERE AR.ParentAssessmentResponseID IS NULL AND AR.IsRemoved = 0  AND AR.AssessmentID = CTE.AssessmentID order by AR.AssessmentResponseID  FOR JSON PATH)
                           FROM AssessmentsCTE CTE
                           JOIN info.AssessmentStatus AST ON CTE.AssessmentStatusID = AST.AssessmentStatusID
						   JOIN info.AssessmentReason ASR ON CTE.AssessmentReasonID = ASR.AssessmentReasonID
						   JOIN info.VoiceType V ON CTE.VoiceTypeID = V.VoiceTypeID
                           LEFT JOIN info.Timeframe TF on TF.DaysInService = CTE.DaysInService";
                
                var assessmentDetails = ExecuteSqlQuery(query, x => new AssessmentDetailsListDTO
                {
                    AssessmentID = (int)x["AssessmentID"],                    
                    PersonIndex = (Guid)x["PersonIndex"],
                    PersonName = x["PersonName"] == DBNull.Value ? null : (string)x["PersonName"],
                    AssessmentStatusID = x["AssessmentStatusID"] == DBNull.Value ? 0 : (int)x["AssessmentStatusID"],
                    DateTaken = x["Assementdate"] == DBNull.Value ? DateTime.MinValue : (DateTime)x["Assementdate"],
                    ReasonNote = x["ReasonNote"] == DBNull.Value ? null : (string)x["ReasonNote"],
                    AssessmentReasonID = x["AssessmentReasonID"] == DBNull.Value ? 0 : (int)x["AssessmentReasonID"],
                    SubmittedDate = x["SubmittedDate"] == DBNull.Value ? null : (DateTime?)x["SubmittedDate"],
                    PersonQuestionnaireID = x["PersonQuestionnaireID"] == DBNull.Value ? 0 : (long)x["PersonQuestionnaireID"],
                    QuestionnaireID = x["QuestionnaireID"] == DBNull.Value ? 0 : (int)x["QuestionnaireID"],
                    EventDate = x["EventDate"] == DBNull.Value ? null : (DateTime?)x["EventDate"],
                    EventNotes = x["EventNotes"] == DBNull.Value ? null : (string)x["EventNotes"],
                    Approved = x["Approved"] == DBNull.Value ? null : (int?)x["Approved"],
                    VoiceTypeID = x["VoiceTypeID"] == DBNull.Value ? 0 : (int)x["VoiceTypeID"],
                    PersonQuestionnaireScheduleID = x["PersonQuestionnaireScheduleID"] == DBNull.Value ? null : (long?)x["PersonQuestionnaireScheduleID"],
                    IsUpdate = x["IsUpdate"] == DBNull.Value ? false : (bool)x["IsUpdate"],
                    DaysInProgram = x["DaysInService"] == DBNull.Value ? null : (int?)x["DaysInService"],
                    TimePeriod = x["TimePeriod"] == DBNull.Value ? null : (string)x["TimePeriod"],
                    VoiceTypeFKID = x["VoiceTypeFKID"] == DBNull.Value ? 0 : (long)x["VoiceTypeFKID"],
                    AssessmentStatus = x["AssessmentStatus"] == DBNull.Value ? null : (string)x["AssessmentStatus"],
                    AssessmentReason = x["AssessmentReason"] == DBNull.Value ? null : (string)x["AssessmentReason"],
                    VoiceType = x["VoiceType"] == DBNull.Value ? null : (string)x["VoiceType"],
                    UpdateDate = x["UpdateDate"] == DBNull.Value ? DateTime.MinValue : (DateTime)x["UpdateDate"],
                    UpdateUserID = x["UpdateUserID"] == DBNull.Value ? 0 : (int)x["UpdateUserID"],
                    AssessmentResponses = x["AssessmResponses"] == DBNull.Value ? string.Empty : (string)x["AssessmResponses"],
                    TotalCount = x["TotalCount"] == DBNull.Value ? 0 : (int)x["TotalCount"],
                }, queryBuilderDTO.QueryParameterDTO);
                return assessmentDetails;
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// GetDetailsForSendingInviteToComplete.
        /// </summary>
        /// <param name="personQuestionnaireIds"></param>
        /// <param name="typeOfInvite"></param>
        /// <returns></returns>
        public List<InviteReceiversInDetailDTO> GetReceiversDetailsForReminderInvite(List<long> personQuestionnaireIds, string voiceTypeOfInvite)
        {
            try
            {
                //TO-DO -Fetch agencyID and also write logic for j=helpers and leadhelpers
                var query = string.Empty;
                var leadSubquery = voiceTypeOfInvite == PCISEnum.InviteToCompleteReceivers.LeadHelper ? "AND PH.IsLead = 1" : "";
                if (voiceTypeOfInvite == PCISEnum.InviteToCompleteReceivers.Person)
                {
                    query = $@"SELECT P.Firstname,P.LastName,P.MiddleName,P.Email,
                                ISNULL(P.Phone1, P.Phone2) AS Phone,
                                CASE P.Phone1 WHEN  NULL THEN P.Phone2Code
                                ELSE Phone1Code END AS PhoneCode,
                                p.TextPermission,P.EmailPermission,null AS VoiceTypeFKID,P.AgencyID,
                                PQ.PersonQuestionnaireID,null AS HelperId,P.PersonIndex,P.PersonId
                                FROM Person P
                                JOIN PersonQuestionnaire PQ ON P.PersonID = PQ.PersonID 
								--JOIN PersonHelper PH ON P.PersonID = PH.PersonID AND PH.IsLead = 1 AND PH.IsRemoved	=0
                                Where PQ.PersonQuestionnaireID IN ({string.Join(",", personQuestionnaireIds)}) 
                                    and PQ.IsRemoved=0 and P.IsRemoved = 0 AND (P.TextPermission = 1 OR P.EmailPermission=1)";
                }
                if (voiceTypeOfInvite == PCISEnum.InviteToCompleteReceivers.Supports)
                {
                    query = $@"SELECT PS.Firstname,PS.LastName,PS.MiddleName,PS.Email,null AS HelperId, PS.Phone,pS.PhoneCode,
                                PS.TextPermission,PS.EmailPermission,PQ.PersonQuestionnaireID,          
                                CAST(PS.PersonSupportId AS BIGINT) AS VoiceTypeFKID,P.PersonIndex,P.AgencyID,P.PersonId
                                FROM PersonSupport PS
                                JOIN Person P ON P.PersonID  = PS.PersonID
                                JOIN PersonQuestionnaire PQ ON P.personid = PQ.PersonID 
								--JOIN PersonHelper PH ON P.PersonID = PH.PersonID AND PH.IsRemoved = 0
                                Where PQ.PersonQuestionnaireID IN ({string.Join(",", personQuestionnaireIds)}) 
                                AND P.IsRemoved = 0  AND PS.Isremoved =0 AND PQ.IsRemoved =0 AND PS.IsCurrent = 1
                                AND (PS.TextPermission = 1 OR PS.EmailPermission=1)";
                }
                if (voiceTypeOfInvite == PCISEnum.InviteToCompleteReceivers.Helpers || voiceTypeOfInvite == PCISEnum.InviteToCompleteReceivers.LeadHelper)
                {
                    query = $@"SELECT H.Firstname,H.LastName,H.MiddleName,H.Email,H.HelperID AS HelperId,
                        '' AS Phonecode, ISNULL(H.Phone, H.Phone2) As Phone,null AS TextPermission,H.IsEmailReminderAlerts AS EmailPermission,
                        PQ.PersonQuestionnaireID, PH.PersonHelperID AS VoiceTypeFKID,P.PersonIndex,P.AgencyID,P.PersonId
                                FROM PersonHelper PH 
                                JOIN PersonQuestionnaire PQ ON PH.PersonID = PQ.PersonID 			
                                JOIN Person P ON PQ.PersonID = P.PersonID 					
                                JOIN Helper H ON H.HelperID  = PH.HelperID 
                                Where PQ.PersonQuestionnaireID IN ({string.Join(",", personQuestionnaireIds)}) {leadSubquery}
								AND P.IsRemoved = 0 AND H.IsRemoved	= 0 AND PH.IsCurrent = 1 AND PQ.IsRemoved = 0 AND PH.IsRemoved = 0  
                                AND (H.IsEmailReminderAlerts = 1)";
                }

                var dataResult = ExecuteSqlQuery(query, x => new InviteReceiversInDetailDTO
                {
                    FirstName = x["FirstName"] == DBNull.Value ? string.Empty : (string)x["FirstName"],
                    LastName = x["LastName"] == DBNull.Value ? string.Empty : (string)x["LastName"],
                    MiddleName = x["MiddleName"] == DBNull.Value ? string.Empty : (string)x["MiddleName"],
                    EmailID = x["Email"] == DBNull.Value ? string.Empty : (string)x["Email"],
                    TextPermission = x["TextPermission"] == DBNull.Value ? false : (bool)x["TextPermission"],
                    EmailPermission = x["EmailPermission"] == DBNull.Value ? false : (bool)x["EmailPermission"],
                    Phone = x["Phone"] == DBNull.Value ? string.Empty : (string)x["Phone"],
                    PhoneCode = x["PhoneCode"] == DBNull.Value ? string.Empty : (string)x["PhoneCode"],
                    PersonQuestionnaireID = x["PersonQuestionnaireID"] == DBNull.Value ? 0 : (long)x["PersonQuestionnaireID"],
                    VoiceTypeFKID = x["VoiceTypeFKID"] == DBNull.Value ? null : (long?)x["VoiceTypeFKID"],
                    HelperId = x["HelperId"] == DBNull.Value ? 0 : (int?)x["HelperId"],
                    PersonIndex = (Guid)x["PersonIndex"],
                    PersonId = (long)x["PersonId"],
                    AgencyID = (long)x["AgencyID"],
                    InviteReceiverType = voiceTypeOfInvite
                });
                return dataResult;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// GetAssessmentPageNumberByAssessmentID.
        /// Please Note that the rownumber is taken as starting From 0. So that to retrieve exact pageNumber.
        /// For example if Pagesize = 7 and Actual rownumber is 7 
        /// But we assume as Rownumber = ActualRowNumber -1 = 7-1 = 6 
        /// so that 6/Pagesize(7) should be in pageNumber = 0.
        /// </summary>
        /// <param name="personIndex"></param>
        /// <param name="questionnaireId"></param>
        /// <param name="assessmentId"></param>
        /// <param name="loggedInAgencyID"></param>
        /// <returns></returns>
        public AssessmentDetailsDTO GetAssessmentPageNumberByAssessmentID(Guid personIndex, int questionnaireId, int assessmentId, long loggedInAgencyID)
        {
            try
            {
                var personAgencyCondition = $@"AND P.AgencyID = {loggedInAgencyID}";
                var query = $@"WITH CTE AS ( 
                                SELECT COUNT(*) over() AS TotalCount,
                                      ROW_NUMBER() over(order by q.[DateTaken] desc) AS ActualRowNumber,q.AssessmentID
                          	    FROM Assessment q 
                          	    LEFT JOIN PersonQuestionnaire pq ON q.PersonQuestionnaireID=pq.PersonQuestionnaireID
                          	    LEFT JOIN Person p ON p.PersonID=pq.PersonID  {personAgencyCondition}
                          	    WHERE q.IsRemoved=0 AND p.PersonIndex = '{personIndex}' AND pq.QuestionnaireID = {questionnaireId}
                              )
							  SELECT *,CAST(ActualRowNumber-1 AS INT) AS RowNumber FROM CTE WHERE AssessmentId = {assessmentId}";

                var assessmentDetailsDTO = ExecuteSqlQuery(query, x => new AssessmentDetailsDTO
                {
                    TotalCount = x["TotalCount"] == DBNull.Value ? 0 : (int)x["TotalCount"],
                    AssessmentID = x["AssessmentID"] == DBNull.Value ? 0 : (int)x["AssessmentID"],
                    RowNumber = x["RowNumber"] == DBNull.Value ? 0 : (int)x["RowNumber"]
                }).FirstOrDefault();
                return assessmentDetailsDTO;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
