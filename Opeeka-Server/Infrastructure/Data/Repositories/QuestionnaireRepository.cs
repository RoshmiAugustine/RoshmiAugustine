// -----------------------------------------------------------------------
// <copyright file="QuestionnaireRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using AutoMapper;
using Microsoft.Extensions.Logging;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Infrastructure.Data.Repositories.Base;
using Opeeka.PICS.Infrastructure.Enums;
using Opeeka.PICS.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Opeeka.PICS.Domain.DTO.Input;

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{
    public class QuestionnaireRepository : BaseRepository<Questionnaire>, IQuestionnaireRepository
    {
        private readonly IMapper mapper;
        private readonly ILogger<QuestionnaireRepository> logger;
        private readonly ICache _cache;

        /// <summary>
        /// Initializes a new instance of the <see cref="QuestionnaireRepository"/> class.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public QuestionnaireRepository(ILogger<QuestionnaireRepository> logger, OpeekaDBContext dbContext, IMapper mapper, ICache cache)
            : base(dbContext)
        {
            this.mapper = mapper;
            this.logger = logger;
            this._cache = cache;
        }

        /// <summary>
        /// GetQuestionnaireList.
        /// </summary>
        /// <param name="QuestionnaireSearchDTO">questionnaireSearchDTO</param>
        /// <returns>QuestionnaireDTO</returns>
        public List<QuestionnaireDTO> GetQuestionnaireList(QuestionnaireSearchDTO questionnaireSearchDTO, DynamicQueryBuilderDTO queryBuilderDTO)
        {
            try
            {
                var countQuery = @$"Select count(Q.QuestionnaireID) TotalCount 
	                        from Questionnaire Q
	                        inner join info.Instrument I on Q.InstrumentID=I.InstrumentID and I.IsRemoved=0
	                        left join QuestionnaireNotifyRiskSchedule QN on QN.QuestionnaireID=Q.QuestionnaireID
	                        and  QN.QuestionnaireNotifyRiskScheduleID in 
	                        (        
		                        select 
			                        top 1 max(QuestionnaireNotifyRiskScheduleID) from QuestionnaireNotifyRiskSchedule qn1  
		                        where qn1.IsRemoved=0 and qn1.QuestionnaireID=Q.QuestionnaireID
		                        group by QuestionnaireNotifyRiskScheduleID
		                        order by 1 desc
	                        )
	                        where Q.IsRemoved=0 AND Q.AgencyID={questionnaireSearchDTO.AgencyId} 
                            { queryBuilderDTO.WhereCondition }";

                var count = ExecuteSqlQuery(countQuery, x => Convert.ToInt32(x[0]), queryBuilderDTO.QueryParameterDTO).FirstOrDefault();
                var query = string.Empty;

                query = @$";WITH SelectedQuestionnaires AS
                        (
	                        Select 
		                        Distinct Q.QuestionnaireID,Q.InstrumentID,Q.Name As QuestionaireName,Q.Abbrev As QuestionAbbrev,
		                        I.Name As InstrumentName, I.Abbrev As InstrumentAbbrev, Q.IsBaseQuestionnaire, Q.AgencyID, QN.Name AS NotifyName, 
		                        Q.ReminderScheduleName ,Q.Description,Q.UpdateDate, Q.StartDate, Q.EndDate, Q.HasSkipLogic, Q.HasDefaultResponseRule,Q.HasFormView
	                        from Questionnaire Q
	                        inner join info.Instrument I on Q.InstrumentID=I.InstrumentID and I.IsRemoved=0
	                        left join QuestionnaireNotifyRiskSchedule QN on QN.QuestionnaireID=Q.QuestionnaireID
	                        and  QN.QuestionnaireNotifyRiskScheduleID in 
	                        (        
		                        select 
			                        top 1 max(QuestionnaireNotifyRiskScheduleID) from QuestionnaireNotifyRiskSchedule qn1  
		                        where qn1.IsRemoved=0 and qn1.QuestionnaireID=Q.QuestionnaireID
		                        group by QuestionnaireNotifyRiskScheduleID
		                        order by 1 desc
	                        )
	                        where Q.IsRemoved=0 AND Q.AgencyID={questionnaireSearchDTO.AgencyId} 
                            { queryBuilderDTO.WhereCondition } {queryBuilderDTO.OrderBy } { queryBuilderDTO.Paginate}
                        )
                        ,AssignedToCollaboration AS
                        (
	                        SELECT
		                        QuestionnaireID, COUNT(QuestionnaireID) [Count]
	                        FROM
	                        CollaborationQuestionnaire CQ where CQ.IsRemoved=0 and CQ.QuestionnaireID IN (SELECT DISTINCT QuestionnaireID FROM SelectedQuestionnaires)
	                        GROUP BY QuestionnaireID
                        )
                        ,AssignedToPerson AS
                        (
	                        SELECT
		                        QuestionnaireID, COUNT(QuestionnaireID) [Count]
	                        FROM
	                        PersonQuestionnaire PQ where PQ.IsRemoved=0 and PQ.QuestionnaireID IN (SELECT DISTINCT QuestionnaireID FROM SelectedQuestionnaires)
	                        GROUP BY QuestionnaireID
                        )
                        SELECT *,
	                        CASE WHEN (ISNULL(ac.Count,0) + ISNULL(ap.Count,0))>0 THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END [IsUsed]
                        FROM
                        SelectedQuestionnaires Q
                        LEFT JOIN AssignedToCollaboration ac ON ac.QuestionnaireID=Q.QuestionnaireID
                        LEFT JOIN AssignedToPerson ap ON ap.QuestionnaireID=Q.QuestionnaireID
                        WHERE 1=1 ";
                var data = ExecuteSqlQuery(query, x => new QuestionnaireDTO
                {
                    TotalCount = count,
                    QuestionnaireID = x["QuestionnaireID"] == DBNull.Value ? 0 : (int)x["QuestionnaireID"],
                    InstrumentID = x["InstrumentID"] == DBNull.Value ? 0 : (int)x["InstrumentID"],
                    QuestionnaireName = x["QuestionaireName"] == DBNull.Value ? null : (string)x["QuestionaireName"],
                    QuestionnaireAbbrev = x["QuestionAbbrev"] == DBNull.Value ? null : (string)x["QuestionAbbrev"],
                    InstrumentName = x["InstrumentName"] == DBNull.Value ? null : (string)x["InstrumentName"],
                    InstrumentAbbrev = x["InstrumentAbbrev"] == DBNull.Value ? null : (string)x["InstrumentAbbrev"],
                    IsBaseQuestionnaire = x["IsBaseQuestionnaire"] == DBNull.Value ? false : (bool)x["IsBaseQuestionnaire"],
                    AgencyID = x["AgencyID"] == DBNull.Value ? 0 : (long)x["AgencyID"],
                    NotificationScheduleName = x["NotifyName"] == DBNull.Value ? null : (string)x["NotifyName"],
                    ReminderScheduleName = x["ReminderScheduleName"] == DBNull.Value ? null : (string)x["ReminderScheduleName"],
                    Description = x["Description"] == DBNull.Value ? null : (string)x["Description"],
                    UpdateDate = x["UpdateDate"] == DBNull.Value ? null : (DateTime?)x["UpdateDate"],
                    StartDate = x["StartDate"] == DBNull.Value ? null : (DateTime?)x["StartDate"],
                    EndDate = x["EndDate"] == DBNull.Value ? null : (DateTime?)x["EndDate"],
                    IsUsed = x["IsUsed"] == DBNull.Value ? false : (bool)x["IsUsed"],
                    HasSkipLogic = x["HasSkipLogic"] == DBNull.Value ? false : (bool)x["HasSkipLogic"],
                    HasDefaultResponseRule = x["HasDefaultResponseRule"] == DBNull.Value ? false : (bool)x["HasDefaultResponseRule"],
                    HasFormView = x["HasFormView"] == DBNull.Value ? false : (bool)x["HasFormView"],

                }, queryBuilderDTO.QueryParameterDTO);
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetQuestionnaireCount.
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="from"></param>
        /// <param name="TenentID"></param>
        /// <returns></returns>
        public int GetQuestionnaireCount(long tenantID)
        {
            try
            {
                var query = string.Empty;

                query = @"Select count(*)
                            from Questionnaire Q
                            inner join info.Instrument I on Q.InstrumentID=I.InstrumentID and I.IsRemoved=0
                            left join QuestionnaireNotifyRiskSchedule QN on QN.QuestionnaireID=Q.QuestionnaireID
                            and  QN.QuestionnaireNotifyRiskScheduleID in (        
                            select top 1 max(QuestionnaireNotifyRiskScheduleID) from QuestionnaireNotifyRiskSchedule qn1  
		                            where qn1.IsRemoved=0 and qn1.QuestionnaireID=Q.QuestionnaireID
		                            group by QuestionnaireNotifyRiskScheduleID
		                            order by 1 desc
                                )
                            where Q.IsRemoved=0 AND Q.AgencyID=" + tenantID;

                var data = ExecuteSqlQuery(query, x => new QuestionnaireDTO
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
        /// To update Questionnaire.
        /// </summary>
        /// <param name="questionnaireDTO">id.</param>
        /// <returns>List of summaries.</returns>
        public QuestionnairesDTO UpdateQuestionnaire(QuestionnairesDTO questionnaireDTO)
        {
            try
            {
                Questionnaire questionnaire = new Questionnaire();
                this.mapper.Map<QuestionnairesDTO, Questionnaire>(questionnaireDTO, questionnaire);
                var result = this.UpdateAsync(questionnaire).Result;
                QuestionnairesDTO updatedQuestionnaire = new QuestionnairesDTO();
                this.mapper.Map<Questionnaire, QuestionnairesDTO>(result, updatedQuestionnaire);
                this._cache.Delete(PCISEnum.Caching.GetAllQuestionnaire);
                return updatedQuestionnaire;
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
        public async Task<QuestionnairesDTO> GetQuestionnaire(int id)
        {
            try
            {
                QuestionnairesDTO questionnaireDTO = new QuestionnairesDTO();
                Questionnaire questionnaire = await this.GetRowAsync(x => x.QuestionnaireID == id);
                this.mapper.Map<Questionnaire, QuestionnairesDTO>(questionnaire, questionnaireDTO);

                return questionnaireDTO;
            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// To Get QuestionnaireWindow.
        /// </summary>
        /// <param name="questionnaireID">questionnaireID.</param>
        /// <returns>QuestionnaireWindowDTO.</returns>
        public List<QuestionnaireWindowDTO> GetQuestionnaireWindow(int questionnaireID)
        {
            try
            {
                var query = string.Empty;
                query = @"Select QuestionnaireWindowID, Q.AssessmentReasonID, DueDateOffsetDays, WindowOpenOffsetDays, WindowCloseOffsetDays, CanRepeat, RepeatIntervalDays, IsSelected ,
                        A.Name As AssessmentReason, A.ListOrder, Q.CloseOffsetTypeID, Q.OpenOffsetTypeID
                        from QuestionnaireWindow Q
						join info.AssessmentReason A on A.AssessmentReasonID=Q.AssessmentReasonID
                        WHERE QuestionnaireID = " + questionnaireID + "  Order By A.ListOrder";
                var data = ExecuteSqlQuery(query, x => new QuestionnaireWindowDTO
                {
                    QuestionnaireWindowID = x["QuestionnaireWindowID"] == DBNull.Value ? 0 : (int)x["QuestionnaireWindowID"],
                    AssessmentReasonID = x["AssessmentReasonID"] == DBNull.Value ? 0 : (int)x["AssessmentReasonID"],
                    DueDateOffsetDays = x["DueDateOffsetDays"] == DBNull.Value ? 0 : (int)x["DueDateOffsetDays"],
                    WindowOpenOffsetDays = x["WindowOpenOffsetDays"] == DBNull.Value ? 0 : (int)x["WindowOpenOffsetDays"],
                    WindowCloseOffsetDays = x["WindowCloseOffsetDays"] == DBNull.Value ? 0 : (int)x["WindowCloseOffsetDays"],
                    CanRepeat = x["CanRepeat"] == DBNull.Value ? false : (bool)x["CanRepeat"],
                    //RepeatIntervalDays = x[6] == DBNull.Value ? 0 : (int)x[6],
                    IsSelected = x["IsSelected"] == DBNull.Value ? false : (bool)x["IsSelected"],
                    AssessmentName = x["AssessmentReason"] == DBNull.Value ? null : (string)x["AssessmentReason"],
                    ListOrder = x["ListOrder"] == DBNull.Value ? 0 : (int)x["ListOrder"],
                    CloseOffsetTypeID = char.Parse((string)x["CloseOffsetTypeID"]),
                    OpenOffsetTypeID = char.Parse((string)x["OpenOffsetTypeID"]),
                });
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// To Get QuestionnaireReminderRule.
        /// </summary>
        /// <param name="questionnaireID">questionnaireID.</param>
        /// <returns>QuestionnaireReminderRuleDTO.</returns>
        public List<QuestionnaireReminderRuleDTO> GetQuestionnaireReminderRule(int questionnaireID)
        {
            try
            {
                var query = string.Empty;
                query = @"Select QuestionnaireReminderRuleID, Q.QuestionnaireReminderTypeID, ReminderOffsetDays, CanRepeat, RepeatInterval, IsSelected , R.Name As ReminderTypeName, R.ListOrder, Q.ReminderOffsetTypeID
                        from QuestionnaireReminderRule Q
                        join info.QuestionnaireReminderType R on R.QuestionnaireReminderTypeID=Q.QuestionnaireReminderTypeID
                        WHERE QuestionnaireID = " + questionnaireID + "  Order By R.ListOrder";
                var data = ExecuteSqlQuery(query, x => new QuestionnaireReminderRuleDTO
                {
                    QuestionnaireReminderRuleID = x["QuestionnaireReminderRuleID"] == DBNull.Value ? 0 : (int)x["QuestionnaireReminderRuleID"],
                    QuestionnaireReminderTypeID = x["QuestionnaireReminderTypeID"] == DBNull.Value ? 0 : (int)x["QuestionnaireReminderTypeID"],
                    ReminderOffsetDays = x["ReminderOffsetDays"] == DBNull.Value ? 0 : (int)x["ReminderOffsetDays"],
                    CanRepeat = x["CanRepeat"] == DBNull.Value ? false : (bool)x["CanRepeat"],
                    RepeatInterval = x["RepeatInterval"] == DBNull.Value ? 0 : (int)x["RepeatInterval"],
                    IsSelected = x["IsSelected"] == DBNull.Value ? false : (bool)x["IsSelected"],
                    ReminderTypeName = x["ReminderTypeName"] == DBNull.Value ? null : (string)x["ReminderTypeName"],
                    ListOrder = x["ListOrder"] == DBNull.Value ? 0 : (int)x["ListOrder"],
                    ReminderOffsetTypeID = char.Parse((string)x["ReminderOffsetTypeID"])
                });
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetPersonQuestionnaireList
        /// </summary>
        /// <param name="personIndex"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns>PersonQuestionnaireListDTO</returns>        
        public List<PersonQuestionnaireListDTO> GetPersonQuestionnaireList(Guid personIndex, int? questionnaireID, int pageNumber, int pageSize, string sharedQuestionIDS, string helperColabQuestionIDs)
        {
            try
            {
                var query = string.Empty;
                var ConditionWithSharedQuestionIDs = string.IsNullOrEmpty(sharedQuestionIDS) ? string.Empty : $@"AND Q.QuestionnaireID IN ({sharedQuestionIDS})";
                var helperCollaborationCondition = string.IsNullOrEmpty(helperColabQuestionIDs) ? string.Empty : $@"AND Q.QuestionnaireID IN ({helperColabQuestionIDs})";
                query = $@"SELECT * INTO #QuestionsWithAssessments FROM (
                            	 SELECT DISTINCT PQ.QuestionnaireID
                            	 FROM Person P
	                             JOIN PersonQuestionnaire PQ ON PQ.PersonID = p.PersonID  
                                 AND P.PersonIndex = '{personIndex}'  
	                             JOIN Assessment A ON PQ.PersonQuestionnaireID = A.PersonQuestionnaireID 
	                             WHERE A.IsRemoved = 0 
                            )A
                            SELECT * INTO #CTE FROM(
                       	         SELECT distinct Q.QuestionnaireID,Q.InstrumentID,Q.Name AS QuestionnaireName,Q.Abbrev as QuestionnaireAbbrev, 
                       	         I.Name AS InstrumentName, I.Abbrev as InstrumentAbbrev,I.InstrumentUrl, Q.IsBaseQuestionnaire, Q.AgencyID, 
                       	         QN.Name AS NotificationSchedule, Q.ReminderScheduleName, null AS Status, Q.EndDate
                       	         FROM Person P
                       	         join PersonQuestionnaire PQ ON PQ.PersonID = P.PersonID and PQ.IsRemoved=0
                                 left join CollaborationQuestionnaire CQ ON CQ.CollaborationID = PQ.CollaborationID AND CQ.QuestionnaireID = PQ.QuestionnaireID
                       	         join Questionnaire Q ON Q.QuestionnaireID = PQ.QuestionnaireID and Q.IsRemoved=0
                       	         left join info.Instrument I ON Q.InstrumentID=I.InstrumentID and I.IsRemoved=0
                       	         left join QuestionnaireNotifyRiskSchedule QN ON QN.QuestionnaireID=Q.QuestionnaireID
                       	         and  QN.QuestionnaireNotifyRiskScheduleID in 
                       	         (SELECT TOP 1 max(QuestionnaireNotifyRiskScheduleID) FROM QuestionnaireNotifyRiskSchedule qn1  
                       	           WHERE qn1.IsRemoved=0 and qn1.QuestionnaireID=Q.QuestionnaireID
                       	           GROUP BY QuestionnaireNotifyRiskScheduleID
                       	           ORDER BY 1 DESC)
                       	         WHERE Q.IsBaseQuestionnaire=0 AND (PQ.EndDueDate is null or PQ.EndDueDate > getDate())
                       	         AND P.PersonIndex = '{personIndex}' and Q.IsRemoved=0 
							     AND ((((CAST(CQ.StartDate AS DATE)<=CAST(GETDATE() AS DATE) AND ISNULL(CAST(CQ.EndDate AS DATE), CAST(GETDATE() AS DATE))>=CAST(GETDATE() AS DATE)
						         AND CQ.IsRemoved = 0) OR (CQ.CollaborationID is null OR CQ.CollaborationID =0 )) 
							     AND ((CAST(Q.StartDate AS DATE)<=CAST(GETDATE() AS DATE) AND ISNULL(CAST(Q.EndDate AS DATE), CAST(GETDATE() AS DATE))>=CAST(GETDATE() AS DATE)
						         AND Q.IsRemoved = 0))) OR Q.QuestionnaireID IN (SELECT * FROM #QuestionsWithAssessments))
                                 AND ({questionnaireID ?? 0} = 0 OR Q.QuestionnaireID = {questionnaireID ?? 0}) {ConditionWithSharedQuestionIDs}
                                 {helperCollaborationCondition}
							)B
                            SELECT * FROM
                            (
                                SELECT COUNT(*) OVER() AS TotalCount,
                            		   QuestionnaireID,InstrumentID,QuestionnaireName,QuestionnaireAbbrev,InstrumentName,
                            		   InstrumentAbbrev,IsBaseQuestionnaire,AgencyID,NotificationSchedule,ReminderScheduleName,Status,InstrumentUrl,EndDate
                                FROM #CTE ORDER BY QuestionnaireID asc 	
	                            OFFSET {((pageNumber - 1) * pageSize)} ROWS FETCH NEXT { pageSize} ROWS ONLY
                            )A;";

                var data = ExecuteSqlQuery(query, x => new PersonQuestionnaireListDTO
                {
                    TotalCount = x["TotalCount"] == DBNull.Value ? 0 : (int)x["TotalCount"],
                    QuestionnaireID = x["QuestionnaireID"] == DBNull.Value ? 0 : (int)x["QuestionnaireID"],
                    InstrumentID = x["InstrumentID"] == DBNull.Value ? 0 : (int)x["InstrumentID"],
                    QuestionnaireName = x["QuestionnaireName"] == DBNull.Value ? null : (string)x["QuestionnaireName"],
                    QuestionnaireAbbrev = x["QuestionnaireAbbrev"] == DBNull.Value ? null : (string)x["QuestionnaireAbbrev"],
                    InstrumentName = x["InstrumentName"] == DBNull.Value ? null : (string)x["InstrumentName"],
                    InstrumentAbbrev = x["InstrumentAbbrev"] == DBNull.Value ? null : (string)x["InstrumentAbbrev"],
                    IsBaseQuestionnaire = x["IsBaseQuestionnaire"] == DBNull.Value ? false : (bool)x["IsBaseQuestionnaire"],
                    AgencyID = x["AgencyID"] == DBNull.Value ? 0 : (long)x["AgencyID"],
                    NotificationScheduleName = x["NotificationSchedule"] == DBNull.Value ? null : (string)x["NotificationSchedule"],
                    ReminderScheduleName = x["ReminderScheduleName"] == DBNull.Value ? null : (string)x["ReminderScheduleName"],
                    Status = x["Status"] == DBNull.Value ? null : (string)x["Status"],
                    InstrumentUrl = x["InstrumentUrl"] == DBNull.Value ? null : (string)x["InstrumentUrl"],
                    QuestionnaireEndDate = x["EndDate"] == DBNull.Value ? null : (DateTime?)x["EndDate"],
                });
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetPersonQuestionnaireCount
        /// </summary>
        /// <returns>int</returns>
        public int GetPersonQuestionnaireCount(Guid personIndex)
        {
            try
            {
                var query = string.Empty;

                query = @"Select count(*)
                        from Person P
                        join PersonQuestionnaire PQ ON PQ.PersonID = P.PersonID and PQ.IsRemoved=0
                        join Questionnaire Q ON Q.QuestionnaireID = PQ.QuestionnaireID and Q.IsRemoved=0
                        left join info.Instrument I on Q.InstrumentID=I.InstrumentID and I.IsRemoved=0
						left join QuestionnaireNotifyRiskSchedule QN on QN.QuestionnaireID=Q.QuestionnaireID
                            and  QN.QuestionnaireNotifyRiskScheduleID in (        
                            select top 1 max(QuestionnaireNotifyRiskScheduleID) from QuestionnaireNotifyRiskSchedule qn1  
		                            where qn1.IsRemoved=0 and qn1.QuestionnaireID=Q.QuestionnaireID
		                            group by QuestionnaireNotifyRiskScheduleID
		                            order by 1 desc
                                )
                        WHERE P.PersonIndex='" + personIndex + "' and Q.IsRemoved=0";

                var data = ExecuteSqlQuery(query, x => new QuestionnaireDTO
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
        /// GetQuestions.
        /// </summary>
        /// <param name="id">Questionnaire ID</param>
        /// <returns>QuestionsResponseDTO.</returns>
        public QuestionsDTO GetQuestions(int id)
        {
            try
            {
                var query = string.Empty;
                query = @";WITH ChildItems AS
                           (
                            SELECT i.ItemID AS ChildItemID,  i.Name as ChildItemTitle,i.Label As ChildItemLabel, i.[Description] As ChildItemDescription,ISNULL(ChildItemGroupNumber, 0) AS ChildItemGroupNumber,
                           		i.ListOrder, i.ResponseValueTypeID, i.ParentItemID,
                                Responses = (SELECT	
								                        r.ResponseId,
								                        r.Label,
								                        UPPER(LEFT(r.[Label],1)) as dropdownChar,
								                        r.[Description], 
								                        r.[Value], 
								                        r.ListOrder, 
								                        r.KeyCodes,
								                        cp.RGB AS colorPalette,
                                                        r.MaxRangeValue,
														r.DisplayChildItem,
                                                        i.AllowMultipleGroups														
							                        FROM Response r 
								                        LEFT JOIN info.ColorPalette cp ON r.BackgroundColorPaletteId = cp.ColorPaletteId
							                        WHERE r.ItemId = i.ItemId AND r.IsRemoved = 0 Order By r.ListOrder
						                        FOR JSON PATH)
                           		FROM Item i									
                           		WHERE ParentItemID IS NOT NULL AND IsRemoved =0 
                           )
                           SELECT 
		                        v.QuestionnaireID,
		                        v.[Name] AS QuestionnaireName,
                                I.Abbrev AS InstrumentAbbrev,
                                I.InstrumentUrl AS InstrumentUrl,
								v.Abbrev AS QuestionnaireAbbrev,
								v.RequiredConfidentialityLanguage,
								v.PersonRequestedConfidentialityLanguage,
								v.OtherConfidentialityLanguage,
                                v.HasFormview,
                                v.Description AS InstrumentDescription,
                                I.Description AS QuestionnaireDescription,
		                        Categories = (SELECT c.CategoryId, 
				                        c.[Description] AS CategoryDescription,
				                        c.[Name] AS CategoryName,
                                        cf.CategoryFocusID FocusId,
										cf.Name as FocusName,
                                        c.ShowAverage,
				                        VersionItems = (
					                        SELECT vi.QuestionnaireItemID,
						                        i.ItemId, 
						                        i.[Name] as Title, 
                                                i.[Label],
						                        i.ListOrder, 
						                        i.[Description],
                                                i.[IsExpandable],
                                                ISNULL(i.[Considerations],'') as considerations,
                 								ISNULL(i.SupplementalDescription,'') as supplementalDescription,
						                        i.ResponseValueTypeID,
						                        vi.[CanOverrideLowerResponseBehavior] as editableMin, 
						                        vi.[CanOverrideMedianResponseBehavior] as editable, 
						                        vi.[CanOverrideUpperResponseBehavior] as editableMax, 		
						                        lowerIrb.[Name] AS minTypeInfo,
						                        medianIrb.[Name] AS defaultTypeInfo,
						                        upperIrb.[Name] AS maxTypeInfo,
												lowerIrb.ItemResponseBehaviorID AS minItemResponseBehaviorID,
						                        medianIrb.ItemResponseBehaviorID AS defaultItemResponseBehaviorID,
						                        upperIrb.ItemResponseBehaviorID AS maxItemResponseBehaviorID,
						                        lowerResp.ListOrder AS minThreshold,
						                        upperResp.ListOrder AS maxThreshold,
						                        irt.[Name] AS property,
						                        vi.IsOptional,
                                                Vi.EndDate,
												Vi.LowerResponseValue,
												Vi.UpperResponseValue,
                                                i.UseRequiredConfidentiality,
                                                i.UsePersonRequestedConfidentiality,
                                                i.UseOtherConfidentiality,
                                                i.AutoExpand,
                                                i.AutoExpand As IsChildAutoExpand,
                                                i.ResponseRequired,
												i.NotesRequired,
												i.ShowNotes,
                                                i.GridLayoutInFormView,
                                                CASE WHEN ((vi.DefaultRequiredConfidentiality = 1) OR
												          (vi.DefaultRequiredConfidentiality IS NULL AND i.DefaultRequiredConfidentiality = 1)) THEN CAST(1 AS BIT)
												ELSE CAST(0 AS BIT) END AS DefaultRequiredConfidentiality,
												CASE WHEN ((vi.DefaultPersonRequestedConfidentiality = 1) OR
												          (vi.DefaultPersonRequestedConfidentiality IS NULL AND i.DefaultPersonRequestedConfidentiality = 1)) THEN CAST(1 AS BIT)
												ELSE CAST(0 AS BIT) END AS DefaultPersonRequestedConfidentiality,
												CASE WHEN ((vi.DefaultOtherConfidentiality = 1) OR
												          (vi.DefaultOtherConfidentiality IS NULL AND i.DefaultOtherConfidentiality = 1)) THEN CAST(1 AS BIT)
												ELSE CAST(0 AS BIT) END AS DefaultOtherConfidentiality,
												ISNULL(CAST(I.DefaultResponseID AS NVARCHAR(50)), '-') AS DefaultResponseID,
												ISNULL(CAST(defaultResp.Value AS NVARCHAR(5)), '-') AS DefaultResponseValue,
                                                ISNULL(defaultResp.Description,'-') AS DefaultResponseDescription,
												ChildItems = (SELECT COUNT(*) OVER() AS TotalCount, ChildItemID, 
                                                            ChildItemTitle,ChildItemLabel,ChildItemDescription,
												            ListOrder, ChildItemGroupNumber, ResponseValueTypeID, ParentItemID, Responses
												    FROM ChildItems 										
												 WHERE ParentItemID = i.itemid
												 ORDER BY ListOrder FOR JSON PATH),
						                        Responses = (SELECT	
								                        r.ResponseId,
								                        r.Label,
								                        UPPER(LEFT(r.[Label],1)) as dropdownChar,
								                        r.[Description], 
								                        r.[Value], 
								                        r.ListOrder, 
								                        r.KeyCodes,
								                        cp.RGB AS colorPalette,
                                                        r.MaxRangeValue,
														r.DisplayChildItem,
                                                        i.AllowMultipleGroups														
							                        FROM Response r 
								                        LEFT JOIN info.ColorPalette cp ON r.BackgroundColorPaletteId = cp.ColorPaletteId
							                        WHERE r.ItemId = i.ItemId AND r.IsRemoved = 0 Order By r.ListOrder
						                        FOR JSON PATH)
					                        FROM QuestionnaireItem vi
						                        INNER JOIN Item i ON vi.ItemId = i.ItemId
						                        LEFT JOIN info.ItemResponseBehavior lowerIrb on lowerIrb.ItemResponseBehaviorId = vi.LowerItemResponseBehaviorId 
						                        LEFT JOIN info.ItemResponseBehavior medianIrb on medianIrb.ItemResponseBehaviorId = vi.MedianItemResponseBehaviorId 
						                        LEFT JOIN info.ItemResponseBehavior upperIrb on upperIrb.ItemResponseBehaviorId = vi.UpperItemResponseBehaviorId 
						                        LEFT JOIN info.ItemResponseType irt on i.ItemResponseTypeId = irt.ItemResponseTypeId 
						                        LEFT JOIN Response lowerResp on vi.LowerResponseValue = lowerResp.ResponseId 
						                        LEFT JOIN Response upperResp on vi.UpperResponsevalue = upperResp.ResponseId 
												LEFT JOIN Response defaultResp on i.DefaultResponseID = defaultResp.ResponseId
					                        WHERE QuestionnaireID = " + id + @"  AND vi.IsRemoved = 0 AND vi.CategoryId = c.CategoryId Order by i.ListOrder
				                        FOR JSON PATH)
			                        FROM info.Category c
									left join info.CategoryFocus cf on c.CategoryFocusID=cf.CategoryFocusID
			                        WHERE CategoryId IN (SELECT DISTINCT CategoryId FROM QuestionnaireItem WHERE QuestionnaireItem.QuestionnaireID = " + id + @")
                                    ORDER BY c.ListOrder 
                                    FOR JSON PATH),
                                v.StartDate,
                                v.EndDate,
                                I.Instructions AS Instructions
	                        FROM Questionnaire v
							join info.Instrument I on I.InstrumentID=v.InstrumentID
	                        WHERE v.QuestionnaireID = " + id;
                var data = ExecuteSqlQuery(query, x => new QuestionsDTO
                {
                    QuestionnaireID = x["QuestionnaireID"] == DBNull.Value ? 0 : (int)x["QuestionnaireID"],
                    QuestionnaireName = x["QuestionnaireName"] == DBNull.Value ? null : (string)x["QuestionnaireName"],
                    InstrumentAbbrev = x["InstrumentAbbrev"] == DBNull.Value ? null : (string)x["InstrumentAbbrev"],
                    InstrumentUrl = x["InstrumentUrl"] == DBNull.Value ? null : (string)x["InstrumentUrl"],
                    QuestionnaireAbbrev = x["QuestionnaireAbbrev"] == DBNull.Value ? null : (string)x["QuestionnaireAbbrev"],
                    RequiredConfidentialityLanguage = x["RequiredConfidentialityLanguage"] == DBNull.Value ? null : (string)x["RequiredConfidentialityLanguage"],
                    PersonRequestedConfidentialityLanguage = x["PersonRequestedConfidentialityLanguage"] == DBNull.Value ? null : (string)x["PersonRequestedConfidentialityLanguage"],
                    OtherConfidentialityLanguage = x["OtherConfidentialityLanguage"] == DBNull.Value ? null : (string)x["OtherConfidentialityLanguage"],
                    Categories = x["Categories"] == DBNull.Value ? null : (string)x["Categories"],
                    StartDate = x["StartDate"] == DBNull.Value ? DateTime.MinValue : (DateTime)x["StartDate"],
                    EndDate = x["EndDate"] == DBNull.Value ? null : (DateTime?)x["EndDate"],
                    Instruction = x["Instructions"] == DBNull.Value ? null : (string)x["Instructions"],
                    HasFormView = x["HasFormView"] == DBNull.Value ? false : (bool)x["HasFormView"],
                    InstrumentDescription = x["InstrumentDescription"] == DBNull.Value ? null : (string)x["InstrumentDescription"],
                    QuestionnaireDescription = x["QuestionnaireDescription"] == DBNull.Value ? null : (string)x["QuestionnaireDescription"]
                }).FirstOrDefault();
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetAllQuestionnairesWithAgency.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <returns>PersonQuestionnaireDataDTO list.</returns>
        public List<PersonQuestionnaireDataDTO> GetAllQuestionnairesWithAgency(long agencyID)
        {
            try
            {
                string query = @"Select Q.QuestionnaireID,  Q.Name AS QuestionnaireName, Q.AgencyID, Q.Abbrev as QuestionnaireAbbrev, I.InstrumentID, Q.StartDate, Q.EndDate, Q.ReminderScheduleName, Q.IsBaseQuestionnaire, QN.[Name] as NotificationScheduleName, I.[Name] as InstrumentName, I.Abbrev as InstrumentAbbrev, Q.[Name] from Questionnaire Q
                                    left join info.Instrument I on  I.InstrumentID = Q.InstrumentID
                                    left join QuestionnaireNotifyRiskSchedule QN on QN.QuestionnaireID=Q.QuestionnaireID
                                    and  QN.QuestionnaireNotifyRiskScheduleID in (        
                                    select top 1 max(QuestionnaireNotifyRiskScheduleID) from QuestionnaireNotifyRiskSchedule qn1  
		                                    where qn1.IsRemoved=0 and qn1.QuestionnaireID=Q.QuestionnaireID
		                                    group by QuestionnaireNotifyRiskScheduleID
		                                    order by 1 desc
                                        )
                                    where Q.IsRemoved = 0 and Q.IsBaseQuestionnaire = 0 and Q.AgencyID = " + agencyID + " and ISNULL(CAST( Q.EndDate AS DATE), CAST(GETDATE() AS DATE))>=CAST(GETDATE() AS DATE)  ORDER BY Q.IsBaseQuestionnaire DESC, Q.QuestionnaireID ASC";
                //query += @" OFFSET " + ((pageNumber - 1) * pageSize) + "ROWS FETCH NEXT " + pageSize + " ROWS ONLY";

                var data = ExecuteSqlQuery(query, x => new PersonQuestionnaireDataDTO
                {
                    QuestionnaireID = x[0] == DBNull.Value ? 0 : (int)x[0],
                    QuestionnaireName = x[1] == DBNull.Value ? null : ((string)x[1]).Trim(),
                    AgencyID = x[2] == DBNull.Value ? 0 : (long)x[2],
                    QuestionnaireAbbrev = x[3] == DBNull.Value ? null : ((string)x[3]).Trim(),
                    InstrumentID = x[4] == DBNull.Value ? 0 : (int)x[4],
                    StartDate = x[5] == DBNull.Value ? DateTime.MinValue : (DateTime)x[5],
                    EndDate = x[6] == DBNull.Value ? null : (DateTime?)x[6],
                    ReminderScheduleName = x[7] == DBNull.Value ? null : ((string)x[7]).Trim(),
                    IsBaseQuestionnaire = x[8] == DBNull.Value ? false : (bool)x[8],
                    NotificationScheduleName = x[9] == DBNull.Value ? null : (string)x[9],
                    InstrumentName = x[10] == DBNull.Value ? null : (string)x[10],
                    InstrumentAbbrev = x[11] == DBNull.Value ? null : (string)x[11],
                    Name = x[12] == DBNull.Value ? null : (string)x[12]
                }).OrderBy(y => y.QuestionnaireID).ToList();

                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// CloneQuestionnaire
        /// </summary>
        /// <param name="questionnaireDTO"></param>
        /// <returns>QuestionnairesDTO</returns>
        public QuestionnairesDTO CloneQuestionnaire(QuestionnairesDTO questionnaireDTO)
        {
            try
            {
                Questionnaire questionnaire = new Questionnaire();
                this.mapper.Map<QuestionnairesDTO, Questionnaire>(questionnaireDTO, questionnaire);
                var result = this.AddAsync(questionnaire).Result;
                this.mapper.Map<Questionnaire, QuestionnairesDTO>(result, questionnaireDTO);
                return questionnaireDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetAllQuestionnaireWithCompletedAssessment-Reports.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <param name="personID">personID.</param>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <returns>PersonQuestionnaireDataDTO list.</returns>
        public Tuple<List<AssessmentQuestionnaireDataDTO>, int> GetAllQuestionnaireWithCompletedAssessment(long agencyID, long personID, int pageNumber, int pageSize, long personCollaborationID, int voicetypeID, long voiceTypeFKID, SharedDetailsDTO sharedIDs, SharedDetailsDTO helperColbIDs)
        {
            try
            {
                var personAgencyCondition = string.Empty;
                var sharedWhereConditionQIDs = string.IsNullOrEmpty(sharedIDs.SharedQuestionnaireIDs) ? "" : $@"AND PQ.QuestionnaireID IN ({sharedIDs.SharedQuestionnaireIDs})";
                var sharedWhereConditionCIDs = string.IsNullOrEmpty(sharedIDs.SharedCollaborationIDs) ? "" : $@"AND PC.CollaborationID IN ({sharedIDs.SharedCollaborationIDs})";

                var helperWhereConditionQIDs = string.Empty;
                var helperWhereConditionCIDs = string.Empty;
                if (string.IsNullOrEmpty(sharedIDs.SharedCollaborationIDs))
                {
                    personAgencyCondition = $@"AND P.AgencyID = {agencyID}";
                    helperWhereConditionQIDs = string.IsNullOrEmpty(helperColbIDs.SharedQuestionnaireIDs) ? "" : $@"AND PQ.QuestionnaireID IN ({helperColbIDs.SharedQuestionnaireIDs})";
                    helperWhereConditionCIDs = string.IsNullOrEmpty(helperColbIDs.SharedCollaborationIDs) ? "" : $@"AND PC.CollaborationID IN ({helperColbIDs.SharedCollaborationIDs})";                   
                }
                var helperpersonCollaborationID = string.IsNullOrEmpty(helperColbIDs?.SharedCollaborationIDs) ? personCollaborationID : -1;
                string query = @$";WITH WindowOffsets AS
						           (
						           	  SELECT
						           	    	q.QuestionnaireID,ar.Name [Reason],qw.WindowOpenOffsetDays,qw.WindowCloseOffsetDays
						           	    FROM 
						           	    PersonQuestionnaire pq
						           	    JOIN Questionnaire q ON q.QuestionnaireID=pq.QuestionnaireID AND pq.PersonID = {personID}
						           	    JOIN QuestionnaireWindow qw ON qw.QuestionnaireID=q.QuestionnaireID 
						           	    JOIN info.AssessmentReason ar ON ar.AssessmentReasonID=qw.AssessmentReasonID
						           	    WHERE ar.Name IN ('Initial','discharge') AND pq.IsRemoved=0 {sharedWhereConditionQIDs} {helperWhereConditionQIDs}
						           )
						           , COlCTE AS
	                               (
	                                  SELECT C.CollaborationID,C.Name AS CollaborationName,PC.EnrollDate AS CollaborationStartDate,PC.EndDate AS CollaborationEndDate, PC.PersonCollaborationID,
	                               		  PC.IsPrimary, PC.IsCurrent
	                               		  FROM PersonCollaboration PC 
	                               		  JOIN Collaboration C ON C.CollaborationID = PC.CollaborationID
	                               		  WHERE PC.IsRemoved=0 AND PC.PersonID = {personID}	 AND
                                                (PC.PersonCollaborationID = {personCollaborationID} or {personCollaborationID} = 0) {sharedWhereConditionCIDs} {helperWhereConditionCIDs}           		  
	                               ),
                                   CTE AS
                                   (
                                     Select Q.QuestionnaireID,  str(Q.QuestionnaireID) + '-' + I.Abbrev + '-' + Q.Abbrev as QuestionnaireName, Q.AgencyID, Q.Abbrev as QuestionnaireAbbrev,
                                            I.InstrumentID, Q.StartDate, Q.EndDate, Q.ReminderScheduleName, Q.IsBaseQuestionnaire, 
                                            I.[Name] as InstrumentName,I.Abbrev as InstrumentAbbrev, Q.[Name], PQ.PersonQuestionnaireID, 
	                             	        PQ.PersonID
                                     from Person P
                                     left join PersonQuestionnaire PQ on P.PersonID = PQ.PersonID AND pq.IsRemoved=0
                                     left join Questionnaire Q ON PQ.QuestionnaireID = Q.QuestionnaireID
                                     left join info.Instrument I on  I.InstrumentID = Q.InstrumentID			 
                                     Where PQ.PersonID= {personID} and Q.IsRemoved=0 {sharedWhereConditionQIDs}	 {personAgencyCondition} {helperWhereConditionQIDs}	 
                                   )
                                   ,NotifyRiskSchedule AS
                                   (
                                       SELECT
                                           *
                                       FROM
                                       (
                                           SELECT
                                               ROW_NUMBER() OVER(PARTITION BY qn.QuestionnaireID ORDER BY qn.QuestionnaireNotifyRiskScheduleID DESC) [RNo],
                                               qn.*
                                           FROM 
                                           CTE
                                           JOIN QuestionnaireNotifyRiskSchedule qn ON QN.QuestionnaireID=CTE.QuestionnaireID and QN.IsRemoved=0
                                       )T
                                       WHERE RNo=1
                                   ),
                                   CompletedAssessments AS
                                    (
                                        SELECT CTE.PersonQuestionnaireID,a.DateTaken,a.VoiceTypeID,                                        
                                        ar.Name [Reason],a.VoiceTypeFKID,
										wo_init.WindowOpenOffsetDays,
										wo_disc.WindowCloseOffsetDays,
										(CASE WHEN ({personCollaborationID}=0 AND {helperpersonCollaborationID} = 0) THEN NULL ELSE (SELECT MIN(CAST(CollaborationStartDate AS DATE)) FROM COlCTE) END) [EnrollDate],
										(CASE WHEN ({personCollaborationID}=0 AND {helperpersonCollaborationID} = 0) THEN NULL ELSE (SELECT MAX(CAST(ISNULL(CollaborationEndDate,GETDATE()) AS DATE)) FROM COlCTE) END) [EndDate]
                                        FROM
                                        CTE
                                        JOIN Assessment a ON a.PersonQuestionnaireID=CTE.PersonQuestionnaireID AND a.IsRemoved=0 
                                        LEFT JOIN info.AssessmentReason ar ON ar.AssessmentReasonID=a.AssessmentReasonID  
                                        LEFT JOIN  [info].[AssessmentStatus] ast on ast.[AssessmentStatusID]=A.[AssessmentStatusID]	
										LEFT JOIN (SELECT * FROM WindowOffsets WHERE Reason='Initial')wo_init ON wo_init.QuestionnaireID=cte.QuestionnaireID 
									    LEFT JOIN (SELECT * FROM WindowOffsets WHERE Reason='Discharge')wo_disc ON wo_disc.QuestionnaireID=cte.QuestionnaireID		  
                                        WHERE ast.Name in ('Returned','Submitted','Approved') 
		                                      AND ((A.VoiceTypeID = {voicetypeID} OR {voicetypeID} = 0) 
                                              AND (ISNULL(A.VoiceTypeFKID,0) = {voiceTypeFKID} OR {voiceTypeFKID} = 0)) 
                                    ), 
		                            selectedQuestionres AS
		                            (
		                                SELECT distinct CTE.QuestionnaireID,  QuestionnaireName, AgencyID, QuestionnaireAbbrev,
		                                    InstrumentID, CTE.StartDate, CTE.EndDate, ReminderScheduleName, IsBaseQuestionnaire, 
		                                    nrs.[Name] as NotificationScheduleName,
		                                    InstrumentName,MAX(CA.DateTaken) as DateTaken,
		                                    InstrumentAbbrev, CTE.[Name], CTE.PersonQuestionnaireID, PersonID
		                                FROM CompletedAssessments CA
			                            LEFT JOIN CTE ON CTE.PersonQuestionnaireID = CA.PersonQuestionnaireID 
			                            LEFT JOIN NotifyRiskSchedule nrs ON nrs.QuestionnaireID=CTE.QuestionnaireID
		                                JOIN COlCTE CT ON 
										( 
											({personCollaborationID}=0 AND {helperpersonCollaborationID} = 0) OR
											CAST(CA.DateTaken AS DATE) 
											BETWEEN
											DATEADD(DAY,0-ISNULL(CA.WindowOpenOffsetDays,0),CA.EnrollDate) 
											AND 
											DATEADD(DAY,ISNULL(CA.WindowCloseOffsetDays,0),CA.EndDate)
										)GROUP BY CTE.QuestionnaireID,  QuestionnaireName, AgencyID, QuestionnaireAbbrev,
		                                    InstrumentID, CTE.StartDate, CTE.EndDate, ReminderScheduleName, IsBaseQuestionnaire, 
		                                    nrs.[Name],InstrumentName,InstrumentAbbrev, CTE.[Name], CTE.PersonQuestionnaireID,PersonID 
		                            ) SELECT COUNT(QuestionnaireID) OVER() [TotalCount],* from selectedQuestionres SQ ORDER BY DateTaken DESC,SQ.QuestionnaireID ASC";
                query += @" OFFSET " + ((pageNumber - 1) * pageSize) + "ROWS FETCH NEXT " + pageSize + " ROWS ONLY";

                int totalCount = 0;
                var data = ExecuteSqlQuery(query, x =>
                {
                    totalCount = x["TotalCount"] == DBNull.Value ? 0 : (int)x["TotalCount"];
                    return new AssessmentQuestionnaireDataDTO
                    {
                        QuestionnaireID = x["QuestionnaireID"] == DBNull.Value ? 0 : (int)x["QuestionnaireID"],
                        QuestionnaireName = x["QuestionnaireName"] == DBNull.Value ? null : ((string)x["QuestionnaireName"]).Trim(),
                        AgencyID = x["AgencyID"] == DBNull.Value ? 0 : (long)x["AgencyID"],
                        QuestionnaireAbbrev = x["QuestionnaireAbbrev"] == DBNull.Value ? null : ((string)x["QuestionnaireAbbrev"]).Trim(),
                        InstrumentID = x["InstrumentID"] == DBNull.Value ? 0 : (int)x["InstrumentID"],
                        StartDate = x["StartDate"] == DBNull.Value ? DateTime.MinValue : (DateTime)x["StartDate"],
                        EndDate = x["EndDate"] == DBNull.Value ? DateTime.MinValue : (DateTime)x["EndDate"],
                        ReminderScheduleName = x["ReminderScheduleName"] == DBNull.Value ? null : ((string)x["ReminderScheduleName"]).Trim(),
                        IsBaseQuestionnaire = x["IsBaseQuestionnaire"] == DBNull.Value ? false : (bool)x["IsBaseQuestionnaire"],
                        NotificationScheduleName = x["NotificationScheduleName"] == DBNull.Value ? null : (string)x["NotificationScheduleName"],
                        InstrumentName = x["InstrumentName"] == DBNull.Value ? null : (string)x["InstrumentName"],
                        InstrumentAbbrev = x["InstrumentAbbrev"] == DBNull.Value ? null : (string)x["InstrumentAbbrev"],
                        Name = x["Name"] == DBNull.Value ? null : (string)x["Name"],
                        PersonQuestionnaireID = x["PersonQuestionnaireID"] == DBNull.Value ? 0 : (long)x["PersonQuestionnaireID"],
                        PersonID = x["PersonID"] == DBNull.Value ? 0 : (long)x["PersonID"],
                    };
                });
                return Tuple.Create(data.OrderBy(y => y.QuestionnaireID).ToList(), totalCount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// GetAllQuestionnaireWithCompletedAssessmentCount.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <param name="personID">personID.</param>
        /// <returns>Count.</returns>
        public int GetAllQuestionnaireWithCompletedAssessmentCount(long agencyID, long personID)
        {
            try
            {
                string query = @"Select count(*) as TotalCount from Questionnaire Q
                                    left join info.Instrument I on  I.InstrumentID = Q.InstrumentID
                                    left join QuestionnaireNotifyRiskSchedule QN on QN.QuestionnaireID=Q.QuestionnaireID
	                                left join PersonQuestionnaire PQ on PQ.QuestionnaireID = Q.QuestionnaireID
	                                left join Person P on P.PersonID = PQ.PersonID
	                                left join Assessment A on A.PersonQuestionnaireID = PQ.PersonQuestionnaireID	
	                                left join info.AssessmentStatus ASS on ASS.AssessmentStatusID = A.AssessmentStatusID
	                                Where PQ.PersonID = " + personID + " and ASS.[Name] != 'In Progress' and Q.IsRemoved = 0 and Q.AgencyID = " + agencyID;
                var data = ExecuteSqlQuery(query, x => new AssessmentQuestionnaireDataDTO
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
        /// GetAllQuestionnairesWithAgencyCount.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>QuestionnaireDataDTO Count.</returns>
        public int GetAllQuestionnairesWithAgencyCount(long agencyID)
        {
            try
            {
                string query = @"Select count(*) from Questionnaire Q
                                    left join info.Instrument I on  I.InstrumentID = Q.InstrumentID
                                    left join QuestionnaireNotifyRiskSchedule QN on QN.QuestionnaireID=Q.QuestionnaireID
                                    where Q.IsRemoved = 0 and Q.AgencyID = " + agencyID;

                var data = ExecuteSqlQuery(query, x => new PersonQuestionnaireDataDTO
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
        /// QuestionnaireByAssessmentID.
        /// </summary>
        /// <param name="assessmentID">assessmentID.</param>
        /// <returns>QuestionsResponseDTO.</returns>
        public int QuestionnaireByAssessmentID(int assessmentID)
        {
            try
            {
                string query = @"select Q.QuestionnaireID from Questionnaire Q
                                join PersonQuestionnaire PQ on PQ.QuestionnaireID=Q.QuestionnaireID
                                join Assessment A on A.PersonQuestionnaireID=PQ.PersonQuestionnaireID
                                where A.AssessmentID= " + assessmentID;

                var data = ExecuteSqlQuery(query, x => new Questionnaire
                {
                    QuestionnaireID = (int)x[0]
                });

                if (data != null && data.Count > 0)
                {
                    return data[0].QuestionnaireID;
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
        /// Get reference count for a questionnaire.
        /// </summary>
        /// <param name="questionnaireID">questionnaireID.</param>
        /// <returns>int.</returns>
        public int GetQuestionnaireUsedCountByID(int questionnaireID)
        {
            try
            {
                int usedcount = 0;
                string queryCollaborationQuestionnaire = @" Select count(CQ.CollaborationQuestionnaireID) from CollaborationQuestionnaire CQ where CQ.IsRemoved=0 and CQ.QuestionnaireID = " + questionnaireID;
                string queryPersonQuestionnaire = @" Select count(PQ.PersonQuestionnaireID) from PersonQuestionnaire PQ where PQ.IsRemoved=0 and PQ.QuestionnaireID = " + questionnaireID;

                var dataCollaborationQuestionnaire = ExecuteSqlQuery(queryCollaborationQuestionnaire, x => new QuestionnaireDTO
                {
                    TotalCount = (int)x[0]
                });
                if (dataCollaborationQuestionnaire != null && dataCollaborationQuestionnaire.Count > 0)
                {
                    usedcount += dataCollaborationQuestionnaire[0].TotalCount;
                }

                if (usedcount <= 0)
                {
                    var dataPersonQuestionnaire = ExecuteSqlQuery(queryPersonQuestionnaire, x => new QuestionnaireDTO
                    {
                        TotalCount = (int)x[0]
                    });
                    if (dataPersonQuestionnaire != null && dataPersonQuestionnaire.Count > 0)
                    {
                        usedcount += dataPersonQuestionnaire[0].TotalCount;
                    }
                }

                return usedcount;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<QuestionnaireItemsForImportDTO> GetAllQuestionnaireItemsWithResponses(int questionnaireID)
        {
            try
            {
                string query = $@"SELECT vi.QuestionnaireItemID,
						                        i.ItemId, 
						                        i.[Name] as QuestionnaireItemName, 
						                        i.ListOrder, 
						                        vi.[CanOverrideLowerResponseBehavior] as EditableMin, 
						                        vi.[CanOverrideMedianResponseBehavior] as Editable, 
						                        vi.[CanOverrideUpperResponseBehavior] as EditableMax, 		
						                        lowerIrb.[Name] AS MinTypeInfo,
						                        medianIrb.[Name] AS DefaultTypeInfo,
						                        upperIrb.[Name] AS MaxTypeInfo,
												lowerIrb.ItemResponseBehaviorID AS MinItemResponseBehaviorID,
						                        medianIrb.ItemResponseBehaviorID AS DefaultItemResponseBehaviorID,
						                        upperIrb.ItemResponseBehaviorID AS MaxItemResponseBehaviorID,
						                        lowerResp.ListOrder AS MinThreshold,
						                        upperResp.ListOrder AS MaxThreshold,
						                        irt.[Name] AS Property,
						                        vi.IsOptional,
						                        Responses = (SELECT	
								                        r.ResponseId,
								                        r.Label,
								                        UPPER(LEFT(r.[Label],1)) as DropdownChar,
								                        r.[Value], 
                                                        r.KeyCodes,
								                        r.ListOrder
														FROM Response r 
							                        WHERE r.ItemId = i.ItemId Order By r.ListOrder
						                        FOR JSON PATH)
					                        FROM QuestionnaireItem vi
						                        INNER JOIN Item i ON vi.ItemId = i.ItemId
						                        LEFT JOIN info.ItemResponseBehavior lowerIrb on lowerIrb.ItemResponseBehaviorId = vi.LowerItemResponseBehaviorId 
						                        LEFT JOIN info.ItemResponseBehavior medianIrb on medianIrb.ItemResponseBehaviorId = vi.MedianItemResponseBehaviorId 
						                        LEFT JOIN info.ItemResponseBehavior upperIrb on upperIrb.ItemResponseBehaviorId = vi.UpperItemResponseBehaviorId 
						                        LEFT JOIN info.ItemResponseType irt on i.ItemResponseTypeId = irt.ItemResponseTypeId 
						                        LEFT JOIN Response lowerResp on vi.LowerResponseValue = lowerResp.ResponseId 
						                        LEFT JOIN Response upperResp on vi.UpperResponsevalue = upperResp.ResponseId 
					                        WHERE QuestionnaireID = {questionnaireID} AND vi.IsRemoved = 0  Order by i.ListOrder ";

                var data = ExecuteSqlQuery(query, x => new QuestionnaireItemsForImportDTO
                {
                    QuestionnaireItemID = x["QuestionnaireItemID"] == DBNull.Value ? 0 : (int)x["QuestionnaireItemID"],
                    ItemId = x["ItemId"] == DBNull.Value ? 0 : (int)x["ItemId"],
                    QuestionnaireItemName = x["QuestionnaireItemName"] == DBNull.Value ? string.Empty : (string)x["QuestionnaireItemName"],
                    ListOrder = x["ListOrder"] == DBNull.Value ? 0 : (int)x["ListOrder"],
                    EditableMin = x["EditableMin"] == DBNull.Value ? false : (bool)x["EditableMin"],
                    Editable = x["Editable"] == DBNull.Value ? false : (bool)x["Editable"],
                    EditableMax = x["EditableMax"] == DBNull.Value ? false : (bool)x["EditableMax"],
                    MinTypeInfo = x["MinTypeInfo"] == DBNull.Value ? string.Empty : (string)x["MinTypeInfo"],
                    DefaultTypeInfo = x["DefaultTypeInfo"] == DBNull.Value ? string.Empty : (string)x["DefaultTypeInfo"],
                    MaxTypeInfo = x["MaxTypeInfo"] == DBNull.Value ? string.Empty : (string)x["MaxTypeInfo"],
                    MinItemResponseBehaviorID = x["MinItemResponseBehaviorID"] == DBNull.Value ? 0 : (int)x["MinItemResponseBehaviorID"],
                    DefaultItemResponseBehaviorID = x["DefaultItemResponseBehaviorID"] == DBNull.Value ? 0 : (int)x["DefaultItemResponseBehaviorID"],
                    MaxItemResponseBehaviorID = x["MaxItemResponseBehaviorID"] == DBNull.Value ? 0 : (int)x["MaxItemResponseBehaviorID"],
                    MinThreshold = x["MinThreshold"] == DBNull.Value ? 0 : (int)x["MinThreshold"],
                    MaxThreshold = x["MaxThreshold"] == DBNull.Value ? 0 : (int)x["MaxThreshold"],
                    Property = x["Property"] == DBNull.Value ? string.Empty : (string)x["Property"],
                     
                    IsOptional = x["IsOptional"] == DBNull.Value ? false : (bool)x["IsOptional"],
                    Responses = x["Responses"] == DBNull.Value ? string.Empty : (string)x["Responses"]
                }) ;
                return data;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        /// <summary>
        /// GetQuestionnaireDetailsbyIds.
        /// </summary>
        /// <param name="questionnaireIds">questionnaireIds.</param>
        /// <returns>QuestionnairesDTO.</returns>
        public List<QuestionnairesDTO> GetQuestionnaireDetailsbyIds(List<int> questionnaireIds)
        {
            try
            {
                var response =  this.GetAsync(x => questionnaireIds.Contains(x.QuestionnaireID)).Result.ToList();
                List<QuestionnairesDTO> questionnaireDTO = new List<QuestionnairesDTO>();
                this.mapper.Map<List<Questionnaire>, List<QuestionnairesDTO>>(response, questionnaireDTO);
                return questionnaireDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// GetQuestions for EHR Update.
        /// </summary>
        /// <param name="id">Questionnaire ID</param>
        /// <returns>QuestionsResponseDTO.</returns>
        public QuestionsDTO GetQuestionDetails(int id)
        {
            try
            {
                var query = string.Empty;
                query = $@"SELECT 
		                        v.QuestionnaireID,
		                        v.[Name] AS QuestionnaireName,
                                I.Abbrev AS InstrumentAbbrev
	                        FROM Questionnaire v
							join info.Instrument I on I.InstrumentID=v.InstrumentID
	                        WHERE v.QuestionnaireID = {id}";
                var data = ExecuteSqlQuery(query, x => new QuestionsDTO
                {
                    QuestionnaireID = x["QuestionnaireID"] == DBNull.Value ? 0 : (int)x["QuestionnaireID"],
                    QuestionnaireName = x["QuestionnaireName"] == DBNull.Value ? null : (string)x["QuestionnaireName"],
                    InstrumentAbbrev = x["InstrumentAbbrev"] == DBNull.Value ? null : (string)x["InstrumentAbbrev"]                    
                }).FirstOrDefault();
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetQuestionsSkippedActionDetails.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public QuestionnaireSkipActionDetailsDTO GetQuestionsSkippedActionDetails(int id)
        {
            try
            {
                var query = string.Empty;
                query = $@"Select 
                            Q.Questionnaireid,                          
							SkippedItems = (SELECT SA.QuestionnaireItemID FROM QuestionnaireSkipLogicRuleAction SA 
                                join QuestionnaireSkipLogicRule SR on SR.QuestionnaireSkipLogicRuleID=SA.QuestionnaireSkipLogicRuleID
								join QuestionnaireItem QI on QI.QuestionnaireItemID=SA.QuestionnaireItemID
                                join info.ActionType AT on AT.ActionTypeID=SA.ActionTypeID
								WHERE SA.QuestionnaireSkipLogicRuleID = SR.QuestionnaireSkipLogicRuleID 
								AND AT.Name = 'Item' and SA.IsRemoved=0 AND SR.ISremoved = 0 AND SR.Questionnaireid = Q.Questionnaireid FOR JSON PATH),
							SkippedCategories = (SELECT SA.CategoryID FROM QuestionnaireSkipLogicRuleAction SA 
                                join QuestionnaireSkipLogicRule SR on SR.QuestionnaireSkipLogicRuleID=SA.QuestionnaireSkipLogicRuleID
								join info.category C on SA.categoryID = C.categoryID
                                join info.ActionType AT on AT.ActionTypeID=SA.ActionTypeID
							WHERE SA.QuestionnaireSkipLogicRuleID = SR.QuestionnaireSkipLogicRuleID 
							AND AT.Name = 'Category'and SA.IsRemoved=0 AND SR.ISremoved = 0  AND SR.Questionnaireid = Q.Questionnaireid FOR JSON PATH),
							SkippedChildItems = (SELECT ParentItemID, ChildItemID FROM QuestionnaireSkipLogicRuleAction SA 
                                join QuestionnaireSkipLogicRule SR on SR.QuestionnaireSkipLogicRuleID=SA.QuestionnaireSkipLogicRuleID
							    join info.ActionType AT on AT.ActionTypeID=SA.ActionTypeID
							WHERE SA.QuestionnaireSkipLogicRuleID = SR.QuestionnaireSkipLogicRuleID 
							AND AT.Name = 'Child Item'and SA.IsRemoved=0 AND SR.ISremoved = 0 AND SR.Questionnaireid = Q.Questionnaireid FOR JSON PATH)
                            from   Questionnaire Q 
                            where Q.Questionnaireid = {id} AND Q.Hasskiplogic = 1";
                var data = ExecuteSqlQuery(query, x => new QuestionnaireSkipActionDetailsDTO
                {
                    QuestionnaireID = x["QuestionnaireID"] == DBNull.Value ? 0 : (int)x["QuestionnaireID"],
                    SkippedItems = x["SkippedItems"] == DBNull.Value ? null : (string)x["SkippedItems"],
                    SkippedCategories = x["SkippedCategories"] == DBNull.Value ? null : (string)x["SkippedCategories"],
                    SkippedChildItems = x["SkippedChildItems"] == DBNull.Value ? null : (string)x["SkippedChildItems"],
                }).FirstOrDefault();
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
