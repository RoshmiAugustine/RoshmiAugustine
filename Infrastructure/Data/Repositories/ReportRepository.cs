// -----------------------------------------------------------------------
// <copyright file="ReportRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.DTO.Input;
using Opeeka.PICS.Domain.DTO.Output;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Infrastructure.Data.Repositories.Base;
using Opeeka.PICS.Infrastructure.Enums;

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{
    public class ReportRepository : BaseRepository<Person>, IReportRepository
    {
        private readonly OpeekaDBContext dbContext;
        public ReportRepository(OpeekaDBContext dbContext)
            : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// GetItemReportData.
        /// </summary>
        /// <param name="reportInputDTO">reportInputDTO.</param>
        /// <returns>ItemDetailsList.</returns>
        public List<ItemDetailsDTO> GetItemReportData(ReportInputDTO reportInputDTO, SharedDetailsDTO sharedIDs, SharedDetailsDTO helperColbIDs)
        {
            try
            {                
                var sharedWhereConditionQIDs = string.IsNullOrEmpty(sharedIDs.SharedQuestionnaireIDs) ? "" : $@"AND PQ.QuestionnaireID IN ({sharedIDs.SharedQuestionnaireIDs})";
                var sharedWhereConditionCIDs = string.IsNullOrEmpty(sharedIDs.SharedCollaborationIDs) ? "" : $@"AND PC.CollaborationID IN ({sharedIDs.SharedCollaborationIDs})";


                var helperAssessmentIDs = string.IsNullOrEmpty(helperColbIDs.SharedAssessmentIDs) ? "" : $@"AND A.AssessmentID IN ({helperColbIDs.SharedAssessmentIDs})";
                var helperWhereConditionQIDs = string.IsNullOrEmpty(helperColbIDs.SharedQuestionnaireIDs) ? "" : $@"AND PQ.QuestionnaireID IN ({helperColbIDs.SharedQuestionnaireIDs})";
                var helperWhereConditionCIDs = string.IsNullOrEmpty(helperColbIDs.SharedCollaborationIDs) ? "" : $@"AND PC.CollaborationID IN ({helperColbIDs.SharedCollaborationIDs})";

                var helperpersonCollaborationID = string.IsNullOrEmpty(helperColbIDs.SharedCollaborationIDs) ? reportInputDTO.PersonCollaborationID : -1;
                var confidentialItemsHideInSharing = string.Empty;
                var confidentialCTE = string.Empty;
                if (!string.IsNullOrEmpty(sharedWhereConditionQIDs) || !string.IsNullOrEmpty(sharedWhereConditionCIDs))
                {
                    confidentialItemsHideInSharing = $@"AND CAST(AR.QuestionnaireItemID AS NVARCHAR(MAX)) +'-' + CAST(ISNULL(AR.PersonSupportID,'0') AS NVARCHAR(MAX)) NOT IN (SELECT * FROM ConfidentialItems)";
                    confidentialCTE = $@",ConfidentialItems AS(
						    SELECT DISTINCT CAST(AR.QuestionnaireItemID AS NVARCHAR(MAX)) +'-' + CAST(ISNULL(AR.PersonSupportID,'0') AS NVARCHAR(MAX))     
							ConfidentialsItems 
						    FROM AssessmentResponse AR
								JOIN Assessment A ON A.AssessmentID = AR.AssessmentID 
                                JOIN PersonQuestionnaire PQ ON PQ.PersonQuestionnaireID = A.PersonQuestionnaireID 
								JOIN Person P ON P.PersonID = PQ.PersonID AND P.PersonIndex = '{reportInputDTO.PersonIndex}'
                                WHERE A.IsRemoved = 0 AND AR.IsRemoved = 0 
                                AND (PQ.QuestionnaireID = (SELECT QuestionnaireID FROM PersonQuestionnaire WHERE PersonQuestionnaireID = {reportInputDTO.PersonQuestionnaireID}) 
                                OR PQ.QuestionnaireID = 0) AND (AR.IsRequiredConfidential = 1 
								      OR  AR.IsPersonRequestedConfidential = 1 OR AR.IsOtherConfidential = 1))";
                }
                List<ItemDetailsDTO> itemDetailsList = new List<ItemDetailsDTO>();
                var query = string.Empty;
                int supportVoiceTypeID = dbContext.VoiceTypes.Where(x => x.Name == PCISEnum.VoiceType.Support).FirstOrDefault().VoiceTypeID;
                int personVoiceTypeID = dbContext.VoiceTypes.Where(x => x.Name == PCISEnum.VoiceType.Consumer).FirstOrDefault().VoiceTypeID;
                query = @$";WITH WindowOffsets AS
						(
							SELECT
								q.QuestionnaireID,ar.Name [Reason],qw.WindowOpenOffsetDays,qw.WindowCloseOffsetDays
							FROM 
							PersonQuestionnaire pq
							JOIN Questionnaire q ON q.QuestionnaireID=pq.QuestionnaireID 
                            AND pq.PersonQuestionnaireID={reportInputDTO.PersonQuestionnaireID}
							JOIN QuestionnaireWindow qw ON qw.QuestionnaireID=q.QuestionnaireID 
							JOIN info.AssessmentReason ar ON ar.AssessmentReasonID=qw.AssessmentReasonID
						)
						,SelectedCollaboration AS
						( 
                            SELECT-- C.CollaborationID,C.Name AS CollaborationName,
							MIN(PC.EnrollDate) EnrollDate,PC.PersonID,
                                            MAX(PC.EndDate) EndDate--, PC.PersonCollaborationID,PC.IsPrimary, PC.IsCurrent
                                  	 FROM PersonCollaboration PC 
                                  	      JOIN Collaboration C ON C.CollaborationID = PC.CollaborationID
                                  	 WHERE PC.IsRemoved=0 AND PC.PersonID = {sharedIDs.PersonID}
                                     AND (PC.PersonCollaborationID = {reportInputDTO.PersonCollaborationID} or { reportInputDTO.PersonCollaborationID} = 0) {sharedWhereConditionCIDs} {helperWhereConditionCIDs}
                                     GRoup by personid
						)
						,AssessmentsForPerson AS
						(
							SELECT
								ROW_NUMBER() OVER (ORDER BY a.DateTaken) [Time],
								a.PersonQuestionnaireID,a.AssessmentReasonID,a.AssessmentStatusID,
                                a.AssessmentId,a.VoiceTypeFKID,a.VoiceTypeID,a.DateTaken,
								pq.QuestionnaireID,
								pq.PersonID
							FROM
							Person p
							JOIN PersonQuestionnaire pq ON pq.PersonID=p.PersonID AND p.PersonIndex='{reportInputDTO.PersonIndex}' 
                            AND pq.QuestionnaireID = (SELECT QuestionnaireID FROM PersonQuestionnaire WHERE PersonQuestionnaireID={reportInputDTO.PersonQuestionnaireID}) AND pq.Isactive=1 AND pq.IsRemoved=0 {sharedWhereConditionQIDs} {helperWhereConditionQIDs}
							JOIN Assessment a ON a.PersonQuestionnaireID=pq.PersonQuestionnaireID AND a.IsRemoved=0  
                            WHERE 1=1 {helperAssessmentIDs}
						)
						,SelectedAssessments AS
						(
							SELECT
								a.*,
								ar.Name [Reason],								
								wo_init.WindowOpenOffsetDays,
								wo_disc.WindowCloseOffsetDays,
								DATEDIFF(DAY,sc.EnrollDate,a.DateTaken) [DaysInEpisode],
								(CASE WHEN ({reportInputDTO.PersonCollaborationID}=0 AND {helperpersonCollaborationID} = 0) THEN NULL ELSE (SELECT MIN(CAST(EnrollDate AS DATE)) FROM SelectedCollaboration) END) [EnrollDate],
								(CASE WHEN ({reportInputDTO.PersonCollaborationID}=0 AND {helperpersonCollaborationID} = 0) THEN NULL ELSE (SELECT MAX(CAST(ISNULL(EndDate,GETDATE()) AS DATE)) FROM SelectedCollaboration) END) [EndDate]
							FROM
							AssessmentsForPerson a				
							JOIN info.AssessmentReason ar ON ar.AssessmentReasonID=a.AssessmentReasonID AND ({reportInputDTO.VoiceTypeID}=0 OR a.VoiceTypeID={reportInputDTO.VoiceTypeID}) 
							JOIN info.AssessmentStatus ast ON ast.AssessmentStatusID=a.AssessmentStatusID AND ast.Name in ('Returned','Submitted','Approved')
							JOIN SelectedCollaboration sc ON sc.PersonID=a.PersonID
							LEFT JOIN (SELECT * FROM WindowOffsets WHERE Reason='Initial')wo_init ON wo_init.QuestionnaireID=a.QuestionnaireID 
							LEFT JOIN (SELECT * FROM WindowOffsets WHERE Reason='Discharge')wo_disc ON wo_disc.QuestionnaireID=a.QuestionnaireID
							WHERE (ISNULL(A.VoiceTypeFKID,0) = {reportInputDTO.VoiceTypeFKID} OR {reportInputDTO.VoiceTypeFKID} = 0) 
						){confidentialCTE}
                   		SELECT distinct
							sa.Time,
							sa.AssessmentID,
							sa.DateTaken,
							tf.Timeframe_Std [Period],
							cat.CategoryID,
							cat.Name [Category],
							i.ItemID,
							i.Name [Item],
							r.Value,
							cp.RGB [Rgb],
							r.label [ResponseLable],
							ar.ItemResponseBehaviorID,
							i.ResponseValueTypeID,
							r.Value as minRange,
							r.MaxRangeValue as maxRange,
							ar.Value as AssessmentResponseValue,
                           Notes = (SELECT
						            n.NoteId, 
						            n.NoteText,
						            n.UpdateDate,
						            CASE WHEN n.VoiceTypeFKID is null THEN u.Name
	                                     WHEN n.VoiceTypeFKID is not null AND n.AddedByVoiceTypeID = {personVoiceTypeID} THEN	p.FirstName+ ' ' + p.MiddleName + ' ' + p.LastName
			                             WHEN n.VoiceTypeFKID is not null AND n.AddedByVoiceTypeID = {supportVoiceTypeID} THEN	pS.FirstName+ ' ' + pS.MiddleName + ' ' + pS.LastName END as Author                                    
			                        FROM AssessmentResponseNote qrn
						            INNER JOIN Note n ON qrn.NoteId = n.NoteId and n.IsRemoved=0
						            LEFT JOIN [User] U ON n.UpdateUserId = u.UserId
									LEFT JOIN Helper h ON h.UserID=u.UserID 
									LEFT JOIN info.HelperTitle ht ON ht.HelperTitleID=h.HelperTitleID
		                            LEFT JOIN Person p ON n.VoiceTypeFKID = p.PersonID	AND VoiceTypeFKID is not null
		                            LEFT JOIN PersonSupport ps ON n.VoiceTypeFKID = ps.PersonSupportID AND VoiceTypeFKID is not null
					            WHERE qrn.AssessmentResponseID = ar.AssessmentResponseID
					            FOR JSON PATH)					
						FROM
						SelectedAssessments sa
						CROSS JOIN
						(
							SELECT qqi.* FROM Questionnaire q JOIN QuestionnaireItem qqi ON qqi.QuestionnaireID=q.QuestionnaireID AND q.QuestionnaireID={reportInputDTO.QuestionnaireId}
						)qi
						LEFT JOIN Item i ON i.ItemID=qi.ItemID
						LEFT JOIN AssessmentResponse ar ON ar.AssessmentID=sa.AssessmentID AND ar.QuestionnaireItemID=qi.QuestionnaireItemID
						LEFT JOIN Response r ON r.ResponseID=ar.ResponseID
						LEFT JOIN info.Category cat ON cat.CategoryID=qi.CategoryID
						LEFT JOIN info.TimeFrame tf ON tf.DaysInService=sa.DaysInEpisode
						LEFT JOIN info.ColorPalette cp ON cp.ColorPaletteID=r.BackgroundColorPaletteID
						WHERE i.ResponseValueTypeID < 4  AND
						(
							({reportInputDTO.PersonCollaborationID}=0 AND {helperpersonCollaborationID} = 0) OR
							CAST(sa.DateTaken AS DATE) 
							BETWEEN
							DATEADD(DAY,0-ISNULL(sa.WindowOpenOffsetDays,0),CAST(sa.EnrollDate AS DATE)) 
							AND 
							DATEADD(DAY,ISNULL(sa.WindowCloseOffsetDays,0),ISNULL(sa.EndDate, CAST(GETDATE() AS DATE)))
						) {confidentialItemsHideInSharing} ORDER BY sa.DateTaken,cat.CategoryID,i.ItemID";

                itemDetailsList = ExecuteSqlQuery(query, x => new ItemDetailsDTO
                {
                    Time = x["Time"] == DBNull.Value ? 0 : (Int64)x["Time"],
                    DateTaken = x["DateTaken"] == DBNull.Value ? DateTime.MinValue : (DateTime)x["DateTaken"],
                    Category = x["Category"] == DBNull.Value ? null : (string)x["Category"],
                    CategoryId = x["CategoryId"] == DBNull.Value ? 0 : (int)x["CategoryId"],
                    AssessmentId = x["AssessmentId"] == DBNull.Value ? 0 : (int)x["AssessmentId"],
                    ItemId = x["ItemId"] == DBNull.Value ? 0 : (int)x["ItemId"],
                    Item = x["Item"] == DBNull.Value ? null : (string)x["Item"],
                    Notes = x["Notes"] == DBNull.Value ? null : (string)x["Notes"],
                    Period = x["Period"] == DBNull.Value ? null : (string)x["Period"],
                    Rgb = x["Rgb"] == DBNull.Value ? null : (string)x["Rgb"],
                    ResponseLable=x["ResponseLable"] == DBNull.Value ? null : (string)x["ResponseLable"],
                    ItemResponseBehaviorID = x["ItemResponseBehaviorID"] == DBNull.Value ? 0 : (int)x["ItemResponseBehaviorID"],
                    ResponseValueTypeID = x["ResponseValueTypeID"] == DBNull.Value ? 0 : (int)x["ResponseValueTypeID"],
                    minRange = x["minRange"] == DBNull.Value ? 0 : (decimal)x["minRange"],
                    maxRange = x["maxRange"] == DBNull.Value ? 0 : (int)x["maxRange"],
                    AssessmentResponseValue = x["AssessmentResponseValue"] == DBNull.Value ? null : (string)x["AssessmentResponseValue"],
                    Value = x["Value"] == DBNull.Value ? null : (decimal?)x["Value"]
                });
                return itemDetailsList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
     
        public List<StoryMapDTO> GetStoryMapReportData(ReportInputDTO reportInputDTO, SharedDetailsDTO sharedIDs, SharedDetailsDTO helperColbIDs)
        {
            try
            {
                var sharedWhereConditionQIDs = string.IsNullOrEmpty(sharedIDs.SharedQuestionnaireIDs) ? "" : $@"AND PQ.QuestionnaireID IN ({sharedIDs.SharedQuestionnaireIDs})";
                var sharedWhereConditionCIDs = string.IsNullOrEmpty(sharedIDs.SharedCollaborationIDs) ? "" : $@"AND PC.CollaborationID IN ({sharedIDs.SharedCollaborationIDs})";

                var helperAssessmentIDs = string.IsNullOrEmpty(helperColbIDs.SharedAssessmentIDs) ? "" : $@"AND A.AssessmentID IN ({helperColbIDs.SharedAssessmentIDs})";
                var helperWhereConditionQIDs = string.IsNullOrEmpty(helperColbIDs.SharedQuestionnaireIDs) ? "" : $@"AND PQ.QuestionnaireID IN ({helperColbIDs.SharedQuestionnaireIDs})";
                var helperWhereConditionCIDs = string.IsNullOrEmpty(helperColbIDs.SharedCollaborationIDs) ? "" : $@"AND PC.CollaborationID IN ({helperColbIDs.SharedCollaborationIDs})";
                var confidentialItemsHideInSharing = string.Empty;
                var confidentialCTE = string.Empty;
                if (!string.IsNullOrEmpty(sharedWhereConditionQIDs))
                {
                    confidentialItemsHideInSharing = $@"WHERE CAST(sar.QuestionnaireItemID AS NVARCHAR(MAX)) +'-' + CAST(ISNULL(sar.PersonSupportID,'0') AS NVARCHAR(MAX)) NOT IN (SELECT * FROM ConfidentialItems)";
                    confidentialCTE = $@",ConfidentialItems AS (
						  SELECT DISTINCT CAST(AR.QuestionnaireItemID AS NVARCHAR(MAX)) +'-' + CAST(ISNULL(AR.PersonSupportID,'0') AS NVARCHAR(MAX)) 
                          AS ConfidentialsItems 
						  FROM AssessmentResponse AR
						  JOIN Assessment A ON A.AssessmentID = AR.AssessmentID 
                          JOIN PersonQuestionnaire PQ ON PQ.PersonQuestionnaireID = A.PersonQuestionnaireID 
						  JOIN Person P ON P.PersonID = PQ.PersonID AND P.PersonIndex = '{reportInputDTO.PersonIndex}'
                          WHERE A.IsRemoved = 0 AND AR.IsRemoved = 0 
                              AND (PQ.QuestionnaireID = (SELECT QuestionnaireID FROM PersonQuestionnaire 
                                  WHERE PersonQuestionnaireID = {reportInputDTO.PersonQuestionnaireID}) 
                              OR PQ.QuestionnaireID = 0) AND (AR.IsRequiredConfidential = 1 
						      OR  AR.IsPersonRequestedConfidential = 1 OR AR.IsOtherConfidential = 1)
					)";
                }
                List<StoryMapDTO> storyMapList = new List<StoryMapDTO>();
                var query = string.Empty;
                query = @$";WITH SelectedAssessments AS
					(
						SELECT
							ROW_NUMBER() OVER (ORDER BY a.DateTaken) [Time],
                            a.AssessmentId,
							pq.QuestionnaireID														
						FROM
						Person p
						JOIN PersonQuestionnaire pq ON pq.PersonID=p.PersonID AND p.PersonIndex='{reportInputDTO.PersonIndex}'AND pq.QuestionnaireId={reportInputDTO.QuestionnaireId} AND pq.Isactive=1 AND pq.IsRemoved=0 {sharedWhereConditionQIDs} 
						JOIN Assessment a ON a.AssessmentID={reportInputDTO.AssessmentID} AND a.PersonQuestionnaireID=pq.PersonQuestionnaireID AND a.VoiceTypeID={reportInputDTO.VoiceTypeID} AND a.IsRemoved=0 
						AND ISNULL(a.VoiceTypeFKID,0)={reportInputDTO.VoiceTypeFKID} WHERE 1=1 {helperAssessmentIDs}
					)
					,SelectedAssessmentResponse AS
					(
						SELECT
							ar.*
						FROM
						SelectedAssessments sa
						JOIN AssessmentResponse ar ON ar.AssessmentID=sa.AssessmentID AND ar.IsRemoved=0
					){confidentialCTE}
             		SELECT
						irt.Name [Type],
						qi.ItemID [ItemID],
						c.Abbrev + '-' + i.Label + ISNULL('-' + ps.FirstName, '') [Item],
						LEFT(r.label, 3) [Label],
						r.description [LabelDescription],
						r.Value [Score],
						(CASE WHEN sar.Priority IS NULL THEN ROW_NUMBER() OVER(PARTITION BY irt.Name,irb.Name ORDER BY r.Value DESC,i.Name ASC) ELSE sar.Priority END) [Priority],
						irb.Name [ToDo],sar.AssessmentResponseID,
						cp.RGB [Rgb],
						i.ResponseValueTypeID,
						r.Value as minRange,
						r.MaxRangeValue as maxRange,
						sar.Value as AssessmentResponseValue
					FROM
					SelectedAssessmentResponse sar
					JOIN QuestionnaireItem qi ON qi.QuestionnaireItemID=sar.QuestionnaireItemID AND sar.IsRemoved=0 AND qi.IsRemoved=0
					JOIN Item i ON i.ItemID=qi.ItemID
					JOIN info.Category c ON c.CategoryID=qi.CategoryID
					JOIN Response r ON r.ResponseID=sar.ResponseID
					JOIN info.ItemResponseBehavior irb ON irb.ItemResponseBehaviorID=sar.ItemResponseBehaviorID AND irb.Name<>'None'
					JOIN info.ItemResponseType irt ON irt.ItemResponseTypeID=i.ItemResponseTypeID
					LEFT JOIN PersonSupport ps ON ps.PersonSupportID=sar.PersonSupportID
					LEFT JOIN info.ColorPalette cp ON cp.ColorPaletteID=r.BackgroundColorPaletteID
                    {confidentialItemsHideInSharing}
					ORDER BY irt.Name,irb.Name,[Priority]";

                storyMapList = ExecuteSqlQuery(query, x => new StoryMapDTO
                {
                    Type = x["Type"] == DBNull.Value ? null : (string)x["Type"],
                    ItemID = x["ItemID"] == DBNull.Value ? 0 : (int)x["ItemID"],
                    Item = x["Item"] == DBNull.Value ? null : (string)x["Item"],
                    Label = x["Label"] == DBNull.Value ? null : (string)x["Label"],
                    LabelDescription = x["LabelDescription"] == DBNull.Value ? null : (string)x["LabelDescription"],
                    Score = x["Score"] == DBNull.Value ? null : (decimal?)x["Score"],
                    Priority = x["Priority"] == DBNull.Value ? null : (Int64?)x["Priority"],
                    ToDo = x["ToDo"] == DBNull.Value ? null : (string)x["ToDo"],
                    AssessmentResponseID = x["AssessmentResponseID"] == DBNull.Value ? 0 : (int)x["AssessmentResponseID"],
                    Rgb = x["Rgb"] == DBNull.Value ? null : (string)x["Rgb"],
                    ResponseValueTypeID = x["ResponseValueTypeID"] == DBNull.Value ? 0 : (int)x["ResponseValueTypeID"],
                    minRange = x["minRange"] == DBNull.Value ? 0 : (decimal)x["minRange"],
                    maxRange = x["maxRange"] == DBNull.Value ? 0 : (int)x["maxRange"],
                    AssessmentResponseValue = x["AssessmentResponseValue"] == DBNull.Value ? null : (string)x["AssessmentResponseValue"],
                });
                return storyMapList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region PersonStrengthFamilyReport

        public PersonStrengthReportDTO GetPersonStrengthFamilyReportData(long personID, FamilyReportInputDTO familyReportInputDTO, List<int> latestAssessmentIDs)
        {
            try
            {
                PersonStrengthReportDTO personStrengthReportDTO = new PersonStrengthReportDTO();
                personStrengthReportDTO.ReportDetails = new StrengthReportDetailsDTO();
                personStrengthReportDTO.PersonDetails = GetPersonDetails(familyReportInputDTO.PersonIndex);
                personStrengthReportDTO.QuestionnaireDetails = GetQuestionnaireDetails(personID, familyReportInputDTO);
                personStrengthReportDTO.ReportPeriodDate = GetReportPeriodDate(personID, familyReportInputDTO);
                var confidentialItems = GetConfidentialItems(familyReportInputDTO, latestAssessmentIDs, PCISEnum.ItemResponseType.Strength);
                var ProgressForLast2Assessments = GetStrengthProgressForLast2Assessments(familyReportInputDTO, latestAssessmentIDs.Take(2).ToList(), confidentialItems);
                if (ProgressForLast2Assessments.Count > 0)
                {
                    personStrengthReportDTO.ReportDetails.LatestProgressPerItem = GetLatestStrengthProgressPerItem(ProgressForLast2Assessments, latestAssessmentIDs.Take(2).ToList());
                    personStrengthReportDTO.ReportSummary = GetStrengthReportSummaryDetails(ProgressForLast2Assessments, latestAssessmentIDs);
                }
                personStrengthReportDTO.ReportDetails.StrengthToUsePerAssessmentforGraph = GetStrengthToUsePerAssessmentForGraph(personID, familyReportInputDTO, latestAssessmentIDs, confidentialItems);
                personStrengthReportDTO.ReportDetails.ItemNotes = GetItemNotes(personID, familyReportInputDTO, latestAssessmentIDs, confidentialItems, PCISEnum.ItemResponseType.Strength);
                return personStrengthReportDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get All StrengthProgress ForLast2Assessments.
        /// </summary>
        /// <param name="personID"></param>
        /// <param name="familyReportInputDTO"></param>
        /// <param name="latestAssessmentIDs"></param>
        /// <returns></returns>
        private List<LatestStrengthDTO> GetStrengthProgressForLast2Assessments(FamilyReportInputDTO familyReportInputDTO, List<int> latestAssessmentIDs, List<string> confidentialItems)
        {
            try
            {
                var query = string.Empty;
                query = @$"
						SELECT A.AssessmentID,A.PersonQuestionnaireID,A.DateTaken,
							AR.ResponseID,AR.ItemResponseBehaviorID,AR.QuestionnaireItemID,
							I.Name + ISNULL('-' + ps.FirstName, '') AS ItemName,ISNULL(AR.PersonSupportID,0) As PersonSupportID,
							I.ItemID,I.Abbreviation AS ItemAbbrev,I.Description AS ItemDescription,I.Label AS ItemLabel,
							IRB.Name AS ResponseBehaviour,IRB.ItemResponseBehaviorID,
							IRT.Name AS ResponseBehaviourType,IRT.ItemResponseTypeID,
							R.[Value] AS ResponseScore,
							CASE WHEN IRB.Name = '{PCISEnum.ToDo.Build}' THEN 1 
							ELSE 0 END AS StrengthToBuild,
							CASE WHEN IRB.Name = '{PCISEnum.ToDo.Use}' THEN 1 
							ELSE 0 END AS StrengthToUse		
							FROM AssessmentResponse AR
							JOIN Response R ON AR.ResponseID = R.ResponseID
							JOIN info.ItemResponseBehavior IRB ON IRB.ItemResponseBehaviorID = AR.ItemResponseBehaviorID 
							JOIN info.ItemResponseType IRT ON IRT.ItemResponseTypeID = IRB.ItemResponseTypeID
							JOIN Assessment A ON  A.AssessmentID = AR.AssessmentID  AND A.PersonQuestionnaireID = {familyReportInputDTO.PersonQuestionnaireID}
							JOIN QuestionnaireItem QI ON QI.QuestionnaireItemID = AR.QuestionnaireItemID
							JOIN Item I ON I.ItemID = QI.ItemID 
							LEFT JOIN PersonSupport ps ON ps.PersonSupportID=AR.PersonSupportID
							WHERE A.IsRemoved = 0 AND AR.IsRemoved = 0  
							     AND (IRT.Name = '{PCISEnum.ItemResponseType.Strength}' AND (IRB.Name = '{PCISEnum.ToDo.Build}' OR IRB.Name = '{PCISEnum.ToDo.Use}'))
							     AND (A.AssessmentID in ({string.Join(',', latestAssessmentIDs.ToArray())})) AND CAST(AR.QuestionnaireItemID AS NVARCHAR(MAX)) +'-' + CAST(ISNULL(AR.PersonSupportID,'0') AS NVARCHAR(MAX)) NOT IN ('{string.Join("','", confidentialItems.ToArray())}')";
                var latestProgressDTO = ExecuteSqlQuery(query, x => new LatestStrengthDTO
                {
                    ItemID = x["ItemID"] == DBNull.Value ? 0 : (int)x["ItemID"],
                    ItemLabel = x["ItemLabel"] == DBNull.Value ? string.Empty : (string)x["ItemLabel"],
                    ItemDescription = x["ItemDescription"] == DBNull.Value ? string.Empty : (string)x["ItemDescription"],
                    ItemName = x["ItemName"] == DBNull.Value ? string.Empty : (string)x["ItemName"],
                    AssessmentID = x["AssessmentID"] == DBNull.Value ? 0 : (int)x["AssessmentID"],
                    AssessmentDate = x["DateTaken"] == DBNull.Value ? DateTime.MinValue : (DateTime)x["DateTaken"],
                    ResponseID = x["ResponseID"] == DBNull.Value ? 0 : (int)x["ResponseID"],
                    ResponseScore = x["ResponseScore"] == DBNull.Value ? 0 : (decimal)x["ResponseScore"],
                    StrengthToBuild = x["StrengthToBuild"] == DBNull.Value ? false : (int)x["StrengthToBuild"] == 0 ? false : true,
                    StrengthToUse = x["StrengthToUse"] == DBNull.Value ? false : (int)x["StrengthToUse"] == 0 ? false : true,
                    PersonCareGiverID = x["PersonSupportID"] == DBNull.Value ? 0 : (int)x["PersonSupportID"]
                });

                return latestProgressDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get Last and previous Assessment StrengthProgress for each Item.(Arranging for OutputDTO).
        /// </summary>
        /// <param name="ProgressForLast2Assessments"></param>
        /// <param name="latestAssessmentIDs"></param>
        /// <returns></returns>
        private List<LatestStrengthPerItemDTO> GetLatestStrengthProgressPerItem(List<LatestStrengthDTO> ProgressForLast2Assessments, List<int> latestAssessmentIDs)
        {
            try
            {
                List<LatestStrengthPerItemDTO> latestProgressPerItem = new List<LatestStrengthPerItemDTO>();
                var query = string.Empty;
                var lastAssessmentID = latestAssessmentIDs[0];
                var prevAssessmentID = latestAssessmentIDs.Count == 2 ? latestAssessmentIDs[1] : 0;
                if (ProgressForLast2Assessments.Count > 0)
                {
                    var progressPerItem = ProgressForLast2Assessments.Select(x => new LatestStrengthPerItemDTO
                    {
                        ItemID = x.ItemID,
                        ItemDescription = x.ItemDescription,
                        ItemLabel = x.ItemLabel,
                        ItemName = x.ItemName,
                        PersonCareGiverID = x.PersonCareGiverID,
                        StrengthInLatestAssessment = new AssessmentStrengthDetailsDTO(),
                        StrengthInPreviousAssessment = new AssessmentStrengthDetailsDTO()
                    }).ToList();
                    latestProgressPerItem = progressPerItem.GroupBy(x => new { x.ItemID, x.PersonCareGiverID }).Select(g => g.First()).ToList();
                    foreach (var item in latestProgressPerItem)
                    {
                        var prevAssessmentResponseOnItem = ProgressForLast2Assessments.Where(x => x.ItemID == item.ItemID && x.PersonCareGiverID == item.PersonCareGiverID && x.AssessmentID == prevAssessmentID).ToList();
                        if (prevAssessmentResponseOnItem.Count() > 0)
                        {
                            item.StrengthInPreviousAssessment.AssessmentID = prevAssessmentResponseOnItem[0].AssessmentID;
                            item.StrengthInPreviousAssessment.AssessmentDate = prevAssessmentResponseOnItem[0].AssessmentDate;
                            item.StrengthInPreviousAssessment.StrengthToBuild = prevAssessmentResponseOnItem[0].StrengthToBuild;
                            item.StrengthInPreviousAssessment.StrengthToUse = prevAssessmentResponseOnItem[0].StrengthToUse;
                            item.StrengthInPreviousAssessment.ResponseScore = prevAssessmentResponseOnItem[0].ResponseScore;
                            item.StrengthInPreviousAssessment.ResponseID = prevAssessmentResponseOnItem[0].ResponseID;
                        }
                        var lastAssessmentResponseOnItem = ProgressForLast2Assessments.Where(x => x.ItemID == item.ItemID && x.PersonCareGiverID == item.PersonCareGiverID && x.AssessmentID == lastAssessmentID).ToList();
                        if (lastAssessmentResponseOnItem.Count() > 0)
                        {
                            item.StrengthInLatestAssessment.AssessmentID = lastAssessmentResponseOnItem[0].AssessmentID;
                            item.StrengthInLatestAssessment.AssessmentDate = lastAssessmentResponseOnItem[0].AssessmentDate;
                            item.StrengthInLatestAssessment.StrengthToBuild = lastAssessmentResponseOnItem[0].StrengthToBuild;
                            item.StrengthInLatestAssessment.StrengthToUse = lastAssessmentResponseOnItem[0].StrengthToUse;
                            item.StrengthInLatestAssessment.ResponseScore = lastAssessmentResponseOnItem[0].ResponseScore;
                            item.StrengthInLatestAssessment.ResponseID = lastAssessmentResponseOnItem[0].ResponseID;
                        }
                    }
                }
                return latestProgressPerItem;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get StrengthToUse PerAssessment(All) ForGraphPlotting.
        /// </summary>
        /// <param name="personID"></param>
        /// <param name="familyReportInputDTO"></param>
        /// <param name="latestAssessmentIDs"></param>
        /// <returns></returns>
        private List<StrengthToUsePerAssessmentDTO> GetStrengthToUsePerAssessmentForGraph(long personID, FamilyReportInputDTO familyReportInputDTO, List<int> latestAssessmentIDs, List<string> confidentialItems)
        {
            try
            {
                List<StrengthToUsePerAssessmentDTO> strengthToUsePerAssessment = new List<StrengthToUsePerAssessmentDTO>();
                var query = string.Empty;

                query = @$";WITH SelectedCollaboration AS
						( 
							SELECT TOP 1* FROM (
							SELECT
								*,1 AS RowNo
							FROM
							PersonCollaboration pc 
							WHERE pc.PersonCollaborationID = {familyReportInputDTO.PersonCollaborationID} AND pc.IsRemoved=0
							UNION 
							SELECT TOP 1 pc.*,2 AS RowNo
								    FROM PersonCollaboration pc WHERE
								    PC.personID = {personID} ORDER BY pc.EnrollDate ASC
								  ) A order by RowNo asc
						),
					   SelectedAssessments AS
					   (
					       SELECT DISTINCT A.AssessmentID,A.DateTaken,personID = {personID},
								  IRT.Name AS ResponseType,--IRB.Name AS ResponseBehaviour,
								  CASE WHEN IRB.Name = '{PCISEnum.ToDo.Use}' THEN COUNT(AR.ResponseID) 
								  ELSE 0 END AS strengthCount   FROM Assessment A
						   JOIN AssessmentResponse AR ON A.AssessmentID = AR.AssessmentID
						   JOIN Response R ON R.ResponseID = AR.ResponseID 
						   JOIN info.ItemResponseBehavior IRB ON IRB.ItemResponseBehaviorID = AR.ItemResponseBehaviorID 
						   JOIN info.ItemResponseType IRT ON IRT.ItemResponseTypeID = IRB.ItemResponseTypeID
						   WHERE A.AssessmentID IN ({string.Join(',', latestAssessmentIDs.ToArray())}) AND A.PersonQuestionnaireID = {familyReportInputDTO.PersonQuestionnaireID}
						   AND CAST(AR.QuestionnaireItemID AS NVARCHAR(MAX)) +'-' + CAST(ISNULL(AR.PersonSupportID,'0') AS NVARCHAR(MAX)) NOT IN ('{string.Join("','", confidentialItems.ToArray())}')
						   GROUP BY A.AssessmentID,A.DateTaken,IRT.Name,IRB.Name HAVING IRT.Name = '{PCISEnum.ItemResponseType.Strength}'
					   )						
						SELECT SA.AssessmentID,Max(strengthCount) AS StrengthToUse,SA.DateTaken,
						       DATEDIFF(DAY,sc.EnrollDate,SA.DateTaken) AS DaysInEpisode,TF.Timeframe_Std AS TimePeriod
						FROM SelectedAssessments SA 
						JOIN SelectedCollaboration sc ON sc.PersonID=SA.PersonID
						LEFT JOIN info.Timeframe TF on TF.DaysInService = DATEDIFF(DAY,sc.EnrollDate,SA.DateTaken)
						GROUP BY SA.AssessmentID,SA.DateTaken,sc.EnrollDate,Timeframe_Std order by SA.DateTaken asc";
                strengthToUsePerAssessment = ExecuteSqlQuery(query, x => new StrengthToUsePerAssessmentDTO
                {
                    AssessmentID = x["AssessmentID"] == DBNull.Value ? 0 : (int)x["AssessmentID"],
                    StrengthsToUse = x["StrengthToUse"] == DBNull.Value ? 0 : (int)x["StrengthToUse"],
                    AssessmentDate = x["DateTaken"] == DBNull.Value ? DateTime.MinValue : (DateTime)x["DateTaken"],
                    DaysInEpisode = x["DaysInEpisode"] == DBNull.Value ? 0 : (int)x["DaysInEpisode"],
                    TimePeriod = x["TimePeriod"] == DBNull.Value ? string.Empty : (string)x["TimePeriod"],
                });
                return strengthToUsePerAssessment;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get StrengthReport Summary(Last Assesment StrengthToUse and StrengthToBuild) for displaying on badge.
        /// </summary>
        /// <param name="ProgressForLast2Assessments"></param>
        /// <param name="latestAssessmentIDs"></param>
        /// <returns></returns>
        private StrengthReportSummaryDTO GetStrengthReportSummaryDetails(List<LatestStrengthDTO> ProgressForLast2Assessments, List<int> latestAssessmentIDs)
        {
            try
            {
                StrengthReportSummaryDTO reportStrengthSummaryDetails = new StrengthReportSummaryDTO();
                var strengthToBuildCount = ProgressForLast2Assessments.Where(x => x.AssessmentID == latestAssessmentIDs[0] && x.StrengthToBuild == true).ToList().Count();
                var strengthToUseCount = ProgressForLast2Assessments.Where(x => x.AssessmentID == latestAssessmentIDs[0] && x.StrengthToUse == true).Count();
                reportStrengthSummaryDetails.StrengthsToBuild = strengthToBuildCount;
                reportStrengthSummaryDetails.StrengthsToUse = strengthToUseCount;
                return reportStrengthSummaryDetails;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region FamilyReport CommonMethods

        /// <summary>
        /// Get Range or Period Date for the Report.
        /// </summary>
        /// <param name="personID"></param>
        /// <param name="familyReportInputDTO"></param>
        /// <returns></returns>
        private ReportPeriodDateDTO GetReportPeriodDate(long personID, FamilyReportInputDTO familyReportInputDTO)
        {
            try
            {
                ReportPeriodDateDTO reportPeriodDateDTO = new ReportPeriodDateDTO();
                var query = string.Empty;
                query = $@";WITH CTE AS
							(
							  SELECT  COUNT(*) OVER () as totalcount,
							    ROW_NUMBER() OVER (ORDER BY PC.EnrollDate) AS ROWNO,
							    CAST(PC.EnrollDate AS DATE) AS EnrollDate,
							  ISNULL(CAST(PC.EndDate AS DATE),CAST(GETDATE() AS DATE)) AS EndDate
			                     FROM PersonCollaboration PC where (PC.PersonCollaborationID = {familyReportInputDTO.PersonCollaborationID} OR {familyReportInputDTO.PersonCollaborationID} = 0)
				              AND PC.PersonID = {personID} AND PC.IsRemoved = 0 
				            )
						   SELECT (SELECT T.EnrollDate FROM CTE T where t.ROWNO = 1)  AS StartDate,
						   (SELECT ISNULL(T.EndDate,GETDATE()) AS EndDate FROM CTE T  where t.ROWNO = t.totalcount) AS EndDate;";

                reportPeriodDateDTO = ExecuteSqlQuery(query, x => new ReportPeriodDateDTO
                {
                    StartDate = x["StartDate"] == DBNull.Value ? DateTime.MinValue : (DateTime)x["StartDate"],
                    EndDate = x["EndDate"] == DBNull.Value ? DateTime.MinValue : (DateTime)x["EndDate"],
                }).FirstOrDefault();

                return reportPeriodDateDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get Strength Item and their Notes.
        /// </summary>
        /// <param name="personID"></param>
        /// <param name="familyReportInputDTO"></param>
        /// <param name="latestAssessmentIDs"></param>
        /// <returns></returns>

        private List<ItemNoteDTO> GetItemNotes(long personID, FamilyReportInputDTO familyReportInputDTO, List<int> latestAssessmentIDs, List<string> confidentialItems, string reportType, int personSupportID = 0, List<string> goalsReached = null)
        {
            try
            {
                var query = string.Empty; var condition = string.Empty;
                if (reportType == PCISEnum.ItemResponseType.Strength)
                {
                    condition = @$"AND (IRT.Name = '{PCISEnum.ItemResponseType.Strength}') AND (IRB.Name = '{PCISEnum.ToDo.Build}' OR IRB.Name = '{PCISEnum.ToDo.Use}')";
                }
                else if (reportType == PCISEnum.ItemResponseType.Need)
                {
                    condition = @$"AND (IRT.Name = '{PCISEnum.ItemResponseType.Need}' AND ((IRB.Name = '{PCISEnum.ToDo.Focus}' OR IRB.Name = '{PCISEnum.ToDo.Background}') OR (IRB.Name ='{PCISEnum.ToDo.None}' AND qi.Name + ISNULL(CAST(AR.PersonSupportID AS nvarchar(100)), '0') IN('{string.Join("','", goalsReached.ToArray())}')))) ";
                }
                else if (reportType == PCISEnum.ItemResponseType.SupportResource)
                {
                    condition = @$"AND (IRT.Name = '{PCISEnum.ItemResponseType.SupportResource}') AND (IRB.Name = '{PCISEnum.ToDo.Build}' OR IRB.Name = '{PCISEnum.ToDo.Use}') AND AR.personSupportID = {personSupportID}";
                }
                else if (reportType == PCISEnum.ItemResponseType.SupportNeed)
                {
                    condition = @$"AND (IRT.Name = '{PCISEnum.ItemResponseType.SupportNeed}' AND ((IRB.Name = '{PCISEnum.ToDo.Focus}' OR IRB.Name = '{PCISEnum.ToDo.Background}') OR (IRB.Name ='{PCISEnum.ToDo.None}' AND qi.Name + ISNULL(CAST(AR.PersonSupportID AS nvarchar(100)), '0') IN('{string.Join("','", goalsReached.ToArray())}'))) AND AR.personSupportID = {personSupportID} ) ";
                }
                query = @$";WITH SelectedCollaboration AS
						( 
							SELECT TOP 1* FROM (
							SELECT
								*,1 AS RowNo
							FROM
							PersonCollaboration pc 
							WHERE pc.PersonCollaborationID = 0 AND pc.IsRemoved=0
							UNION 
							SELECT TOP 1 pc.*,2 AS RowNo
								    FROM PersonCollaboration pc WHERE
								    PC.personID = {personID} ORDER BY pc.EnrollDate ASC
								  ) A order by RowNo asc
						),
						voiceTypeForPerson AS(
						        SELECT '{PCISEnum.VoiceType.Consumer}' AS VoiceType,P.FirstName+ ' '+isnull(P.MiddleName+' ', '')+ P.LastName As [Name],0 AS FKValue
                                FROM Person P where P.PersonID = {personID} 
                                UNION ALL
                                SELECT '{PCISEnum.VoiceType.Support}' AS VoiceType,PS.FirstName+ ' '+isnull(PS.MiddleName+' ', '')+ PS.LastName As [Name],PS.PersonSupportID AS Value
                                FROM PersonSupport PS where PS.PersonID = {personID}  AND PS.IsRemoved = 0 
                                UNION ALL
                                SELECT '{PCISEnum.VoiceType.Communimetric}' AS VoiceType,'Communimetric',0
                                UNION ALL
                                SELECT '{PCISEnum.VoiceType.Helper}' AS VoiceType,H.FirstName+ ' '+isnull(H.MiddleName+' ', '')+ H.LastName As [Name],PH.PersonHelperID AS FKValue
                                FROM Helper H left join PersonHelper PH on PH.HelperID=H.HelperID
                                WHERE PH.PersonID = {personID} AND PH.IsRemoved = 0                                      
						),						
						SelectedAssessments AS
						(
							SELECT A.AssessmentID,A.DateTaken,a.PersonQuestionnaireID,
							A.VoiceTypeID,VT.Name AS VoiceType,A.VoiceTypeFKID,VTP.Name VoiceTypeName,
							DATEDIFF(DAY,sc.EnrollDate,A.DateTaken) [DaysInEpisode],
							TF.Timeframe_Std 
							FROM Assessment A
							JOIN SelectedCollaboration sc ON sc.PersonID={personID}
							LEFT JOIN info.Timeframe TF on TF.DaysInService = DATEDIFF(DAY,sc.EnrollDate,A.DateTaken)
							JOIN info.VoiceType VT ON VT.VoiceTypeID = A.VoiceTypeID
							JOIN voiceTypeForPerson VTP ON VTP.VoiceType = VT.Name AND VTP.FKValue = ISNULL(A.VoiceTypeFKID,0)	
							WHERE AssessmentID IN ({string.Join(',', latestAssessmentIDs.ToArray())}) AND A.IsRemoved = 0
						)
						SELECT
							SA.AssessmentID,
							SA.DateTaken,
							SA.VoiceTypeName,
							SA.Timeframe_Std [TimePeriod],
							QI.ItemID,qi.Name + ISNULL('-' + ps.FirstName, '') AS ItemName,ISNULL(AR.PersonSupportID,0) As PersonSupportID,
							QI.Name AS ItemName,AR.AssessmentResponseID,
							R.Value AS ResponseScore,ARN.AssessmentResponseID,
							N.NoteID,N.NoteText,N.UpdateDate AS NoteDate,N.UpdateUserID,u.Name AS Author,
							(CASE WHEN h.HelperID IS NULL THEN 'Super Admin' ELSE ht.Name END) AuthorTitle
						FROM
						SelectedAssessments sa						
						CROSS JOIN
						(
						    SELECT QQI.QuestionnaireItemID,i.ItemID,i.Name FROM PersonQuestionnaire PQ
							LEFT JOIN QuestionnaireItem QQI ON QQI.QuestionnaireID = PQ.QuestionnaireID 
							LEFT JOIN Item i ON i.ItemID=qqi.ItemID
							WHERE PQ.PersonQuestionnaireID = {familyReportInputDTO.PersonQuestionnaireID} AND PQ.IsRemoved = 0 AND QQI.IsRemoved = 0 
						)qi						
						LEFT JOIN AssessmentResponse ar ON ar.AssessmentID=sa.AssessmentID AND ar.QuestionnaireItemID=qi.QuestionnaireItemID
						JOIN AssessmentResponseNote ARN ON ARN.AssessmentResponseID = AR.AssessmentResponseID
						JOIN Note N ON N.NoteID = ARN.NoteID and N.IsRemoved=0
						JOIN [User] u ON n.UpdateUserId = u.UserId
						LEFT JOIN Helper h ON h.UserID=u.UserID 
						LEFT JOIN info.HelperTitle ht ON ht.HelperTitleID=h.HelperTitleID
						LEFT JOIN Response r ON r.ResponseID=ar.ResponseID 
						JOIN info.ItemResponseBehavior IRB ON IRB.ItemResponseBehaviorID = AR.ItemResponseBehaviorID 
						JOIN info.ItemResponseType IRT ON IRT.ItemResponseTypeID = IRB.ItemResponseTypeID
						LEFT JOIN PersonSupport ps ON ps.PersonSupportID=AR.PersonSupportID
						WHERE AR.IsRemoved = 0 {condition} 
					    AND CAST(AR.QuestionnaireItemID AS NVARCHAR(MAX)) +'-' + CAST(ISNULL(AR.PersonSupportID,'0') AS NVARCHAR(MAX)) NOT IN ('{string.Join("','", confidentialItems.ToArray())}') ORDER BY sa.DateTaken,qi.ItemID;";
                var itemNotes = ExecuteSqlQuery(query, x => new ItemNoteDTO
                {
                    AssessmentID = x["AssessmentID"] == DBNull.Value ? 0 : (int)x["AssessmentID"],
                    AssessmentDate = x["DateTaken"] == DBNull.Value ? DateTime.MinValue : (DateTime)x["DateTaken"],
                    VoiceTypeFKName = x["VoiceTypeName"] == DBNull.Value ? string.Empty : (string)x["VoiceTypeName"],
                    ItemID = x["ItemID"] == DBNull.Value ? 0 : (int)x["ItemID"],
                    ItemName = x["ItemName"] == DBNull.Value ? string.Empty : (string)x["ItemName"],
                    TimePeriod = x["TimePeriod"] == DBNull.Value ? string.Empty : (string)x["TimePeriod"],
                    Author = x["Author"] == DBNull.Value ? string.Empty : (string)x["Author"],
                    Title = x["AuthorTitle"] == DBNull.Value ? string.Empty : (string)x["AuthorTitle"],
                    Note = x["NoteText"] == DBNull.Value ? string.Empty : (string)x["NoteText"],
                    NoteDate = x["NoteDate"] == DBNull.Value ? DateTime.MinValue : (DateTime)x["NoteDate"],
                    NoteID = x["NoteID"] == DBNull.Value ? 0 : (int)x["NoteID"]
                });

                return itemNotes;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get QuestionnaireDetails.
        /// </summary>
        /// <param name="personID"></param>
        /// <param name="familyReportInputDTO"></param>
        /// <returns></returns>
        private QuestionnaireDetailsInReportDTO GetQuestionnaireDetails(long personID, FamilyReportInputDTO familyReportInputDTO)
        {
            try
            {
                QuestionnaireDetailsInReportDTO questionnaireDetailsInReportDTO = new QuestionnaireDetailsInReportDTO();
                var query = string.Empty;

                query = @$"SELECT 
								I.InstrumentID,I.Name AS InstrumentName,I.Abbrev AS InstrumentAbbrev,
								Q.QuestionnaireID,Q.Name AS QuestionnaireName,Q.Abbrev AS QuestionnaireAbbrev
								FROM PersonQuestionnaire PQ  
								JOIN Questionnaire Q ON PQ.QuestionnaireID = Q.QuestionnaireID AND PQ.PersonQuestionnaireID = {familyReportInputDTO.PersonQuestionnaireID}
								JOIN info.Instrument I ON I.InstrumentID = Q.InstrumentID 
						  WHERE PQ.PersonID = {personID} ";
                questionnaireDetailsInReportDTO = ExecuteSqlQuery(query, x => new QuestionnaireDetailsInReportDTO
                {
                    InstrumentID = x["InstrumentID"] == DBNull.Value ? 0 : (int)x["InstrumentID"],
                    InstrumentName = x["InstrumentName"] == DBNull.Value ? null : (string)x["InstrumentName"],
                    InstrumentAbbrevation = x["InstrumentAbbrev"] == DBNull.Value ? string.Empty : (string)x["InstrumentAbbrev"],

                    QuestionnaireID = x["QuestionnaireID"] == DBNull.Value ? 0 : (int)x["QuestionnaireID"],
                    QuestionnaireName = x["QuestionnaireName"] == DBNull.Value ? string.Empty : (string)x["QuestionnaireName"],
                    QuestionnaireAbbrevation = x["QuestionnaireAbbrev"] == DBNull.Value ? null : (string)x["QuestionnaireAbbrev"],

                }).FirstOrDefault();
                return questionnaireDetailsInReportDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get PersonDetails.
        /// </summary>
        /// <param name="personIndex"></param>
        /// <returns></returns>
        private PersonDetailsInReportDTO GetPersonDetails(Guid personIndex)
        {
            try
            {
                PersonDetailsInReportDTO personDetailsInReportDTO = new PersonDetailsInReportDTO();
                var query = string.Empty;

                query = @$"SELECT 
								P.FirstName,
								P.MiddleName,
								P.LastName,
								P.PersonID,
								P.PersonIndex FROM Person P 
						   WHERE P.PersonIndex = '{personIndex}' AND P.IsRemoved = 0;";
                personDetailsInReportDTO = ExecuteSqlQuery(query, x => new PersonDetailsInReportDTO
                {
                    FirstName = x["FirstName"] == DBNull.Value ? string.Empty : (string)x["FirstName"],
                    LastName = x["MiddleName"] == DBNull.Value ? string.Empty : (string)x["MiddleName"],
                    MiddleName = x["LastName"] == DBNull.Value ? string.Empty : (string)x["LastName"],
                    PersonID = (long)x["PersonID"],
                    PersonIndex = (Guid)x["PersonIndex"],
                }).FirstOrDefault();

                return personDetailsInReportDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion 

        #region PersonNeedsFamilyReport
        public PersonNeedsReportDTO GetPersonNeedsFamilyReportData(long personID, FamilyReportInputDTO familyReportInputDTO, List<int> latestAssessmentIDs)
        {
            try
            {
                PersonNeedsReportDTO personNeedsReportDTO = new PersonNeedsReportDTO();
                personNeedsReportDTO.ReportDetails = new NeedsReportDetailsDTO();
                personNeedsReportDTO.PersonDetails = GetPersonDetails(familyReportInputDTO.PersonIndex);
                personNeedsReportDTO.QuestionnaireDetails = GetQuestionnaireDetails(personID, familyReportInputDTO);
                personNeedsReportDTO.ReportPeriodDate = GetReportPeriodDate(personID, familyReportInputDTO);
                var confidentialItems = GetConfidentialItems(familyReportInputDTO, latestAssessmentIDs, PCISEnum.ItemResponseType.Need);
                var goalsReachedForAllAssessments = GetGoalReachedNeedsPerItem(familyReportInputDTO, latestAssessmentIDs, confidentialItems, PCISEnum.ItemResponseType.Need);
                personNeedsReportDTO.ReportDetails.GoalsReachedPerItem = GetGoalReachedNeeds(goalsReachedForAllAssessments, latestAssessmentIDs, PCISEnum.ItemResponseType.Need);
                var lastAssessmentID = latestAssessmentIDs[0];
                var prevAssessmentID = latestAssessmentIDs.Count >= 2 ? latestAssessmentIDs[1] : 0;
                List<LatestNeedsDTO> NeedsForLast2Assessments = goalsReachedForAllAssessments.Where(x => x.AssessmentID == lastAssessmentID || x.AssessmentID == prevAssessmentID).ToList();
                if (NeedsForLast2Assessments.Count > 0)
                {
                    personNeedsReportDTO.ReportDetails.InProgressFocusPerItem = GetInProgressNeedsPerItem(NeedsForLast2Assessments, latestAssessmentIDs.Take(2).ToList());
                    personNeedsReportDTO.ReportDetails.BackgroundNeedsPerItem = GetBackgroundNeedsPerItem(NeedsForLast2Assessments, latestAssessmentIDs.Take(2).ToList());
                    personNeedsReportDTO.ReportSummary = GetNeedsReportSummaryDetails(NeedsForLast2Assessments, latestAssessmentIDs);
                    personNeedsReportDTO.ReportSummary.GoalsReached = personNeedsReportDTO.ReportDetails.GoalsReachedPerItem.Count;
                }
                var goalsReached = personNeedsReportDTO.ReportDetails.GoalsReachedPerItem?.Select(x => x.ItemName + x.PersonCareGiverID).ToList();
                personNeedsReportDTO.ReportDetails.ItemNotes = GetItemNotes(personID, familyReportInputDTO, latestAssessmentIDs, confidentialItems, PCISEnum.ItemResponseType.Need, 0, goalsReached);
                return personNeedsReportDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private NeedsReportSummaryDTO GetNeedsReportSummaryDetails(List<LatestNeedsDTO> needsForLast2Assessments, List<int> latestAssessmentIDs)
        {
            try
            {
                NeedsReportSummaryDTO reportNeedsSummaryDetails = new NeedsReportSummaryDTO();
                var InProgressFocusCount = needsForLast2Assessments.Where(x => x.AssessmentID == latestAssessmentIDs[0] && x.NeedForFocus == true).Count();
                reportNeedsSummaryDetails.GoalsReached = 0;
                reportNeedsSummaryDetails.InProgress = InProgressFocusCount;
                return reportNeedsSummaryDetails;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private List<LatestNeedsPerItemDTO> GetBackgroundNeedsPerItem(List<LatestNeedsDTO> needsForLast2Assessments, List<int> latestAssessmentIDs)
        {

            try
            {
                List<LatestNeedsPerItemDTO> latestProgressPerItem = new List<LatestNeedsPerItemDTO>();
                var query = string.Empty;
                var lastAssessmentID = latestAssessmentIDs[0];
                var prevAssessmentID = latestAssessmentIDs.Count == 2 ? latestAssessmentIDs[1] : 0;
                if (needsForLast2Assessments.Count > 0)
                {
                    var backgroundItems = needsForLast2Assessments.Where(x => x.NeedInBackground == true && x.AssessmentID == lastAssessmentID).ToList();
                    latestProgressPerItem = backgroundItems.Select(x => new LatestNeedsPerItemDTO
                    {
                        ItemID = x.ItemID,
                        ItemDescription = x.ItemDescription,
                        ItemLabel = x.ItemLabel,
                        ItemName = x.ItemName,
                        PersonCareGiverID = x.PersonCareGiverID,
                        NeedsInLatestAssessment = new AssessmentNeedsDetailsDTO(),
                        NeedsInPreviousAssessment = new AssessmentNeedsDetailsDTO()
                    }).ToList();
                    foreach (var item in latestProgressPerItem)
                    {
                        var prevAssessmentResponseOnItem = needsForLast2Assessments.Where(x => x.ItemID == item.ItemID && x.PersonCareGiverID == item.PersonCareGiverID && x.AssessmentID == prevAssessmentID).ToList();
                        if (prevAssessmentResponseOnItem.Count() > 0)
                        {
                            item.NeedsInPreviousAssessment.AssessmentID = prevAssessmentResponseOnItem[0].AssessmentID;
                            item.NeedsInPreviousAssessment.AssessmentDate = prevAssessmentResponseOnItem[0].AssessmentDate;
                            item.NeedsInPreviousAssessment.NeedForFocus = prevAssessmentResponseOnItem[0].NeedForFocus;
                            item.NeedsInPreviousAssessment.NeedInBackground = prevAssessmentResponseOnItem[0].NeedInBackground;
                            item.NeedsInPreviousAssessment.NeedNone = prevAssessmentResponseOnItem[0].NeedNone;
                            item.NeedsInPreviousAssessment.ResponseScore = prevAssessmentResponseOnItem[0].ResponseScore;
                            item.NeedsInPreviousAssessment.ResponseID = prevAssessmentResponseOnItem[0].ResponseID;
                        }
                        var lastAssessmentResponseOnItem = needsForLast2Assessments.Where(x => x.ItemID == item.ItemID && x.PersonCareGiverID == item.PersonCareGiverID && x.AssessmentID == lastAssessmentID).ToList();
                        if (lastAssessmentResponseOnItem.Count() > 0)
                        {
                            item.NeedsInLatestAssessment.AssessmentID = lastAssessmentResponseOnItem[0].AssessmentID;
                            item.NeedsInLatestAssessment.AssessmentDate = lastAssessmentResponseOnItem[0].AssessmentDate;
                            item.NeedsInLatestAssessment.NeedForFocus = lastAssessmentResponseOnItem[0].NeedForFocus;
                            item.NeedsInLatestAssessment.NeedInBackground = lastAssessmentResponseOnItem[0].NeedInBackground;
                            item.NeedsInLatestAssessment.NeedNone = lastAssessmentResponseOnItem[0].NeedNone;
                            item.NeedsInLatestAssessment.ResponseScore = lastAssessmentResponseOnItem[0].ResponseScore;
                            item.NeedsInLatestAssessment.ResponseID = lastAssessmentResponseOnItem[0].ResponseID;
                        }
                    }
                }
                return latestProgressPerItem;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private List<LatestNeedsPerItemDTO> GetInProgressNeedsPerItem(List<LatestNeedsDTO> needsForLast2Assessments, List<int> latestAssessmentIDs)
        {
            try
            {
                List<LatestNeedsPerItemDTO> latestProgressPerItem = new List<LatestNeedsPerItemDTO>();
                var query = string.Empty;
                var lastAssessmentID = latestAssessmentIDs[0];
                var prevAssessmentID = latestAssessmentIDs.Count == 2 ? latestAssessmentIDs[1] : 0;
                if (needsForLast2Assessments.Count > 0)
                {
                    var focusItems = needsForLast2Assessments.Where(x => x.NeedForFocus == true && x.AssessmentID == lastAssessmentID).ToList();
                    latestProgressPerItem = focusItems.Select(x => new LatestNeedsPerItemDTO
                    {
                        ItemID = x.ItemID,
                        ItemDescription = x.ItemDescription,
                        ItemLabel = x.ItemLabel,
                        ItemName = x.ItemName,
                        IsNewFocus = false,
                        PersonCareGiverID = x.PersonCareGiverID,
                        NeedsInLatestAssessment = new AssessmentNeedsDetailsDTO(),
                        NeedsInPreviousAssessment = new AssessmentNeedsDetailsDTO()
                    }).ToList();
                    foreach (var item in latestProgressPerItem)
                    {
                        var IsNew = false;
                        var prevAssessmentResponseOnItem = needsForLast2Assessments.Where(x => x.ItemID == item.ItemID && x.PersonCareGiverID == item.PersonCareGiverID && x.AssessmentID == prevAssessmentID).ToList();
                        if (prevAssessmentResponseOnItem.Count() > 0)
                        {
                            item.NeedsInPreviousAssessment.AssessmentID = prevAssessmentResponseOnItem[0].AssessmentID;
                            item.NeedsInPreviousAssessment.AssessmentDate = prevAssessmentResponseOnItem[0].AssessmentDate;
                            item.NeedsInPreviousAssessment.NeedForFocus = prevAssessmentResponseOnItem[0].NeedForFocus;
                            item.NeedsInPreviousAssessment.NeedInBackground = prevAssessmentResponseOnItem[0].NeedInBackground;
                            item.NeedsInPreviousAssessment.NeedNone = prevAssessmentResponseOnItem[0].NeedNone;
                            item.NeedsInPreviousAssessment.ResponseScore = prevAssessmentResponseOnItem[0].ResponseScore;
                            item.NeedsInPreviousAssessment.ResponseID = prevAssessmentResponseOnItem[0].ResponseID;
                            IsNew = prevAssessmentResponseOnItem[0].NeedForFocus;
                        }
                        var lastAssessmentResponseOnItem = needsForLast2Assessments.Where(x => x.ItemID == item.ItemID && x.PersonCareGiverID == item.PersonCareGiverID && x.AssessmentID == lastAssessmentID).ToList();
                        if (lastAssessmentResponseOnItem.Count() > 0)
                        {
                            item.NeedsInLatestAssessment.AssessmentID = lastAssessmentResponseOnItem[0].AssessmentID;
                            item.NeedsInLatestAssessment.AssessmentDate = lastAssessmentResponseOnItem[0].AssessmentDate;
                            item.NeedsInLatestAssessment.NeedForFocus = lastAssessmentResponseOnItem[0].NeedForFocus;
                            item.NeedsInLatestAssessment.NeedInBackground = lastAssessmentResponseOnItem[0].NeedInBackground;
                            item.NeedsInLatestAssessment.NeedNone = lastAssessmentResponseOnItem[0].NeedNone;
                            item.NeedsInLatestAssessment.ResponseScore = lastAssessmentResponseOnItem[0].ResponseScore;
                            item.NeedsInLatestAssessment.ResponseID = lastAssessmentResponseOnItem[0].ResponseID;
                            IsNew = (prevAssessmentResponseOnItem.Count >= 0 && !IsNew) && lastAssessmentResponseOnItem[0].NeedForFocus == true ? true : false;
                        }
                        item.IsNewFocus = IsNew;
                    }
                }
                return latestProgressPerItem;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private List<LatestNeedsDTO> GetGoalReachedNeedsPerItem(FamilyReportInputDTO familyReportInputDTO, List<int> latestAssessmentIDs, List<string> confidentialItems, string reportType = "", int personSupportID = 0)
        {
            try
            {
                var condition = string.Empty;
                if (reportType == PCISEnum.ItemResponseType.SupportNeed)
                {
                    condition = @$"AND AR.PersonSupportID = { personSupportID}";
                }
                var query = string.Empty;
                query = @$"SELECT A.AssessmentID,A.PersonQuestionnaireID,A.DateTaken,
								AR.ResponseID,AR.ItemResponseBehaviorID,AR.QuestionnaireItemID,							
								I.ItemID,I.Abbreviation AS ItemAbbrev,AR.IsRequiredConfidential,
								I.Name + ISNULL('-' + ps.FirstName, '') AS ItemName,ISNULL(AR.PersonSupportID,0) As PersonSupportID,
								I.Description AS ItemDescription,I.Label AS ItemLabel,
								IRB.Name AS ResponseBehaviour,IRB.ItemResponseBehaviorID,
								IRT.Name AS ResponseBehaviourType,IRT.ItemResponseTypeID,
								R.[Value] AS ResponseScore,								
								CASE WHEN IRB.Name = '{PCISEnum.ToDo.Focus}' THEN 1 
								ELSE 0 END AS NeedForFocus,
								CASE WHEN IRB.Name = '{PCISEnum.ToDo.Background}' THEN 1 
								ELSE 0 END AS NeedInBackground,	
								CASE WHEN IRB.Name = '{PCISEnum.ToDo.None}' THEN 1 
								ELSE 0 END AS NeedsNone	
								FROM AssessmentResponse AR
								JOIN Response R ON AR.ResponseID = R.ResponseID
								JOIN info.ItemResponseBehavior IRB ON IRB.ItemResponseBehaviorID = AR.ItemResponseBehaviorID 
								JOIN info.ItemResponseType IRT ON IRT.ItemResponseTypeID = IRB.ItemResponseTypeID
								JOIN Assessment A ON  A.AssessmentID = AR.AssessmentID  AND A.PersonQuestionnaireID = {familyReportInputDTO.PersonQuestionnaireID}
								JOIN QuestionnaireItem QI ON QI.QuestionnaireItemID = AR.QuestionnaireItemID
								JOIN Item I ON I.ItemID = QI.ItemID 
								LEFT JOIN PersonSupport ps ON ps.PersonSupportID=AR.PersonSupportID
								WHERE A.IsRemoved = 0 AND AR.IsRemoved = 0
								     AND IRT.Name = '{reportType}' {condition} AND (A.AssessmentID IN ({string.Join(',', latestAssessmentIDs.ToArray())})) 
									 AND CAST(AR.QuestionnaireItemID AS NVARCHAR(MAX)) +'-' + CAST(ISNULL(AR.PersonSupportID,'0') AS NVARCHAR(MAX)) NOT IN ('{string.Join("','", confidentialItems.ToArray())}')";

                var latestProgressDTO = ExecuteSqlQuery(query, x => new LatestNeedsDTO
                {
                    ItemID = x["ItemID"] == DBNull.Value ? 0 : (int)x["ItemID"],
                    ItemLabel = x["ItemLabel"] == DBNull.Value ? string.Empty : (string)x["ItemLabel"],
                    ItemDescription = x["ItemDescription"] == DBNull.Value ? string.Empty : (string)x["ItemDescription"],
                    ItemName = x["ItemName"] == DBNull.Value ? string.Empty : (string)x["ItemName"],
                    AssessmentID = x["AssessmentID"] == DBNull.Value ? 0 : (int)x["AssessmentID"],
                    AssessmentDate = x["DateTaken"] == DBNull.Value ? DateTime.MinValue : (DateTime)x["DateTaken"],
                    ResponseID = x["ResponseID"] == DBNull.Value ? 0 : (int)x["ResponseID"],
                    ResponseScore = x["ResponseScore"] == DBNull.Value ? 0 : (decimal)x["ResponseScore"],
                    NeedForFocus = x["NeedForFocus"] == DBNull.Value ? false : (int)x["NeedForFocus"] == 0 ? false : true,
                    NeedInBackground = x["NeedInBackground"] == DBNull.Value ? false : (int)x["NeedInBackground"] == 0 ? false : true,
                    NeedNone = x["NeedsNone"] == DBNull.Value ? false : (int)x["NeedsNone"] == 0 ? false : true,
                    PersonCareGiverID = x["PersonSupportID"] == DBNull.Value ? 0 : (int)x["PersonSupportID"]
                });
                return latestProgressDTO;
            }
            catch (Exception)
            {

                throw;
            }
        }
        private List<LatestNeedsPerItemDTO> GetGoalReachedNeeds(List<LatestNeedsDTO> goalsReachedForAllAssessments, List<int> latestAssessmentIDs, string reportType, int personSupportID = 0)
        {
            try
            {
                List<LatestNeedsPerItemDTO> latestProgressPerItem = new List<LatestNeedsPerItemDTO>();
                var lastAssessmentID = latestAssessmentIDs[0];
                var prevAssessmentID = latestAssessmentIDs.Count >= 2 ? latestAssessmentIDs[1] : 0;
                if (goalsReachedForAllAssessments.Count > 0)
                {
                    var noneItems = goalsReachedForAllAssessments.Where(x => x.NeedNone == true && x.AssessmentID == lastAssessmentID).ToList();
                    foreach (var item in noneItems)
                    {
                        var value = goalsReachedForAllAssessments.Where(x => x.ItemID == item.ItemID && x.PersonCareGiverID == item.PersonCareGiverID && (x.NeedForFocus == true || x.NeedInBackground == true) && x.AssessmentID != lastAssessmentID).ToList();
                        if (value?.Count > 0)
                        {
                            var latestItem = new LatestNeedsPerItemDTO()
                            {
                                ItemID = item.ItemID,
                                ItemDescription = item.ItemDescription,
                                ItemLabel = item.ItemLabel,
                                ItemName = item.ItemName,
                                PersonCareGiverID = item.PersonCareGiverID,
                                NeedsInLatestAssessment = new AssessmentNeedsDetailsDTO(),
                                NeedsInPreviousAssessment = new AssessmentNeedsDetailsDTO()
                            };
                            latestProgressPerItem.Add(latestItem);
                        }
                    }
                    foreach (var item in latestProgressPerItem)
                    {
                        var prevAssessmentResponseOnItem = goalsReachedForAllAssessments.Where(x => x.ItemID == item.ItemID && x.PersonCareGiverID == item.PersonCareGiverID && x.AssessmentID == prevAssessmentID).ToList();
                        if (prevAssessmentResponseOnItem.Count() > 0)
                        {
                            item.NeedsInPreviousAssessment.AssessmentID = prevAssessmentResponseOnItem[0].AssessmentID;
                            item.NeedsInPreviousAssessment.AssessmentDate = prevAssessmentResponseOnItem[0].AssessmentDate;
                            item.NeedsInPreviousAssessment.NeedForFocus = prevAssessmentResponseOnItem[0].NeedForFocus;
                            item.NeedsInPreviousAssessment.NeedInBackground = prevAssessmentResponseOnItem[0].NeedInBackground;
                            item.NeedsInPreviousAssessment.NeedNone = prevAssessmentResponseOnItem[0].NeedNone;
                            item.NeedsInPreviousAssessment.ResponseScore = prevAssessmentResponseOnItem[0].ResponseScore;
                            item.NeedsInPreviousAssessment.ResponseID = prevAssessmentResponseOnItem[0].ResponseID;
                        }
                        var lastAssessmentResponseOnItem = goalsReachedForAllAssessments.Where(x => x.ItemID == item.ItemID && x.PersonCareGiverID == item.PersonCareGiverID && x.AssessmentID == lastAssessmentID).ToList();
                        if (lastAssessmentResponseOnItem.Count() > 0)
                        {
                            item.NeedsInLatestAssessment.AssessmentID = lastAssessmentResponseOnItem[0].AssessmentID;
                            item.NeedsInLatestAssessment.AssessmentDate = lastAssessmentResponseOnItem[0].AssessmentDate;
                            item.NeedsInLatestAssessment.NeedForFocus = lastAssessmentResponseOnItem[0].NeedForFocus;
                            item.NeedsInLatestAssessment.NeedInBackground = lastAssessmentResponseOnItem[0].NeedInBackground;
                            item.NeedsInLatestAssessment.NeedNone = lastAssessmentResponseOnItem[0].NeedNone;
                            item.NeedsInLatestAssessment.ResponseScore = lastAssessmentResponseOnItem[0].ResponseScore;
                            item.NeedsInLatestAssessment.ResponseID = lastAssessmentResponseOnItem[0].ResponseID;
                        }
                    }
                }
                return latestProgressPerItem;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private List<LatestNeedsDTO> GetNeedProgressForLast2Assessments(long personID, FamilyReportInputDTO familyReportInputDTO, List<int> latestAssessmentIDs, string reportType, int personSupportID = 0)
        {
            try
            {
                string condition = "";
                if (reportType == PCISEnum.ItemResponseType.Need)
                {
                    condition = @$"AND (IRT.Name = '{PCISEnum.ItemResponseType.Need}') ";
                }
                else if (reportType == PCISEnum.ItemResponseType.SupportNeed)
                {
                    condition = @$"AND (IRT.Name = '{PCISEnum.ItemResponseType.SupportNeed}') AND AR.PersonSupportID = {personSupportID} ";
                }
                var query = string.Empty;
                var lastAssessmentID = latestAssessmentIDs[0];
                var prevAssessmentID = latestAssessmentIDs.Count == 2 ? latestAssessmentIDs[1] : 0;
                query = @$"SELECT A.AssessmentID,A.PersonQuestionnaireID,A.DateTaken,
							AR.ResponseID,AR.ItemResponseBehaviorID,AR.QuestionnaireItemID,							
							I.ItemID,I.Abbreviation AS ItemAbbrev,
							I.Name + ISNULL('-' + ps.FirstName, '') AS ItemName,ISNULL(AR.PersonSupportID,0) As PersonSupportID,
							I.Description AS ItemDescription,I.Label AS ItemLabel,
							IRB.Name AS ResponseBehaviour,IRB.ItemResponseBehaviorID,
							IRT.Name AS ResponseBehaviourType,IRT.ItemResponseTypeID,
							R.[Value] AS ResponseScore,
							CASE WHEN IRB.Name = '{PCISEnum.ToDo.Focus}' THEN 1 
							ELSE 0 END AS NeedForFocus,
							CASE WHEN IRB.Name = '{PCISEnum.ToDo.Background}' THEN 1 
							ELSE 0 END AS NeedInBackground,	
							CASE WHEN IRB.Name = '{PCISEnum.ToDo.None}' THEN 1 
							ELSE 0 END AS NeedsNone	
							FROM AssessmentResponse AR
							JOIN Response R ON AR.ResponseID = R.ResponseID
							JOIN info.ItemResponseBehavior IRB ON IRB.ItemResponseBehaviorID = AR.ItemResponseBehaviorID 
							JOIN info.ItemResponseType IRT ON IRT.ItemResponseTypeID = IRB.ItemResponseTypeID
							JOIN Assessment A ON  A.AssessmentID = AR.AssessmentID  AND A.PersonQuestionnaireID = {familyReportInputDTO.PersonQuestionnaireID}
							JOIN QuestionnaireItem QI ON QI.QuestionnaireItemID = AR.QuestionnaireItemID
							JOIN Item I ON I.ItemID = QI.ItemID 
							LEFT JOIN PersonSupport ps ON ps.PersonSupportID=AR.PersonSupportID
							WHERE A.IsRemoved = 0 AND AR.IsRemoved = 0 {condition}							     
							     AND (A.AssessmentID = {lastAssessmentID} OR A.AssessmentID = {prevAssessmentID})";
                var latestProgressDTO = ExecuteSqlQuery(query, x => new LatestNeedsDTO
                {
                    ItemID = x["ItemID"] == DBNull.Value ? 0 : (int)x["ItemID"],
                    ItemLabel = x["ItemLabel"] == DBNull.Value ? string.Empty : (string)x["ItemLabel"],
                    ItemDescription = x["ItemDescription"] == DBNull.Value ? string.Empty : (string)x["ItemDescription"],
                    ItemName = x["ItemName"] == DBNull.Value ? string.Empty : (string)x["ItemName"],
                    AssessmentID = x["AssessmentID"] == DBNull.Value ? 0 : (int)x["AssessmentID"],
                    AssessmentDate = x["DateTaken"] == DBNull.Value ? DateTime.MinValue : (DateTime)x["DateTaken"],
                    ResponseID = x["ResponseID"] == DBNull.Value ? 0 : (int)x["ResponseID"],
                    ResponseScore = x["ResponseScore"] == DBNull.Value ? 0 : (decimal)x["ResponseScore"],
                    NeedForFocus = x["NeedForFocus"] == DBNull.Value ? false : (int)x["NeedForFocus"] == 0 ? false : true,
                    NeedInBackground = x["NeedInBackground"] == DBNull.Value ? false : (int)x["NeedInBackground"] == 0 ? false : true,
                    NeedNone = x["NeedsNone"] == DBNull.Value ? false : (int)x["NeedsNone"] == 0 ? false : true,
                    PersonCareGiverID = x["PersonSupportID"] == DBNull.Value ? 0 : (int)x["PersonSupportID"],
                });

                return latestProgressDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region SupportResourcesFamilyReport
        public SupportResourceReportDTO GetSupportResourcesFamilyReportData(long personID, FamilyReportInputDTO familyReportInputDTO, List<int> latestAssessmentIDs)
        {
            try
            {
                List<SupportResourceDTO> supportTobeRemoved = new List<SupportResourceDTO>();
                SupportResourceReportDTO supportResourceReportDTO = new SupportResourceReportDTO();
                supportResourceReportDTO.PersonDetails = GetPersonDetails(familyReportInputDTO.PersonIndex);
                supportResourceReportDTO.QuestionnaireDetails = GetQuestionnaireDetails(personID, familyReportInputDTO);
                supportResourceReportDTO.ReportPeriodDate = GetReportPeriodDate(personID, familyReportInputDTO);
                supportResourceReportDTO.SupportDetails = (List<SupportResourceDTO>)GetSupportReportDetails(personID, latestAssessmentIDs, PCISEnum.ItemResponseType.SupportResource);
                var confidentialItems = GetConfidentialItems(familyReportInputDTO, latestAssessmentIDs, PCISEnum.ItemResponseType.SupportResource);
                foreach (var support in supportResourceReportDTO.SupportDetails)
                {
                    support.ReportDetails = new ResourceReportDetailsDTO();
                    support.ReportSummary = new SupportResourceReportSummaryDTO();
                    List<LatestSupportResourceDTO> supportRescourceForLast2Assessments = GetSupportResourceProgressForLast2Assessments(familyReportInputDTO, latestAssessmentIDs.Take(2).ToList(), confidentialItems, support.PersonSupportID);
                    if (supportRescourceForLast2Assessments.Count > 0)
                    {
                        support.ReportDetails.LatestProgressPerItem = GetLatestSupportResourceProgressPerItem(supportRescourceForLast2Assessments, latestAssessmentIDs.Take(2).ToList());
                        support.ReportSummary = GetSupportResourceReportSummaryDetails(supportRescourceForLast2Assessments, latestAssessmentIDs);
                    }
                    else
                    {
                        supportTobeRemoved.Add(support);
                        continue;

                    }
                    support.ReportDetails.ResourceToUsePerAssessmentforGraph = GetSupportResourceToUsePerAssessmentForGraph(personID, familyReportInputDTO, latestAssessmentIDs, confidentialItems, support.PersonSupportID);
                    support.ReportDetails.ItemNotes = GetItemNotes(personID, familyReportInputDTO, latestAssessmentIDs, confidentialItems, PCISEnum.ItemResponseType.SupportResource, support.PersonSupportID);
                }
                foreach (var support in supportTobeRemoved)
                {
                    supportResourceReportDTO?.SupportDetails?.Remove(support);
                }
                return supportResourceReportDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private object GetSupportReportDetails(long personID, List<int> latestAssessmentIDs, string reportType)
        {
            try
            {
                List<SupportResourceDTO> supportReportsDetailsDTOs = new List<SupportResourceDTO>();
                var query = "";
                query = @$" WITH CTE AS (SELECT distinct PersonSupportID AS PersonSupportID 
											FROM AssessmentResponse 
											WHERE AssessmentID in({string.Join(',', latestAssessmentIDs.ToArray())}))
										 SELECT PS.PersonSupportID,PS.FirstName,ps.MiddleName,ps.LastName,ST.Name AS Relation
											FROM CTE 
											JOIN PersonSupport PS ON CTE.PersonSupportID = PS.PersonSupportID
											JOIN info.SupportType ST ON PS.SupportTypeID = ST.SupportTypeID
										    WHERE personid = {personID}";
                if (reportType == PCISEnum.ItemResponseType.SupportResource)
                {
                    var supportResourceDTO = ExecuteSqlQuery(query, x => new SupportResourceDTO
                    {
                        PersonSupportID = x["PersonSupportID"] == DBNull.Value ? 0 : (int)x["PersonSupportID"],
                        FirstName = x["FirstName"] == DBNull.Value ? string.Empty : (string)x["FirstName"],
                        MiddleName = x["MiddleName"] == DBNull.Value ? string.Empty : (string)x["MiddleName"],
                        LastName = x["LastName"] == DBNull.Value ? string.Empty : (string)x["LastName"],
                        Relation = x["Relation"] == DBNull.Value ? string.Empty : (string)x["Relation"]
                    });

                    return supportResourceDTO;
                }
                if (reportType == PCISEnum.ItemResponseType.SupportNeed)
                {
                    var supportNeedsDTO = ExecuteSqlQuery(query, x => new SupportNeedsDTO
                    {
                        PersonSupportID = x["PersonSupportID"] == DBNull.Value ? 0 : (int)x["PersonSupportID"],
                        FirstName = x["FirstName"] == DBNull.Value ? string.Empty : (string)x["FirstName"],
                        MiddleName = x["MiddleName"] == DBNull.Value ? string.Empty : (string)x["MiddleName"],
                        LastName = x["LastName"] == DBNull.Value ? string.Empty : (string)x["LastName"],
                        Relation = x["Relation"] == DBNull.Value ? string.Empty : (string)x["Relation"]
                    });

                    return supportNeedsDTO;
                }
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private List<SupportResourceToUsePerAssessmentDTO> GetSupportResourceToUsePerAssessmentForGraph(long personID, FamilyReportInputDTO familyReportInputDTO, List<int> latestAssessmentIDs, List<string> confidentialItems, int personSupportID)
        {

            try
            {
                var query = string.Empty;

                query = @$";WITH SelectedCollaboration AS
						( 
							SELECT TOP 1* FROM (
							SELECT
								*,1 AS RowNo
							FROM
							PersonCollaboration pc 
							WHERE pc.PersonCollaborationID = {familyReportInputDTO.PersonCollaborationID} AND pc.IsRemoved=0
							UNION 
							SELECT TOP 1 pc.*,2 AS RowNo
								    FROM PersonCollaboration pc WHERE
								    PC.personID = {personID} ORDER BY pc.EnrollDate ASC
								  ) A order by RowNo asc
						),
					   SelectedAssessments AS
					   (
					       SELECT DISTINCT A.AssessmentID,A.DateTaken,personID = {personID},
								  IRT.Name AS ResponseType,--IRB.Name AS ResponseBehaviour,
								  CASE WHEN IRB.Name = '{PCISEnum.ToDo.Use}' THEN COUNT(AR.ResponseID) 
								  ELSE 0 END AS strengthCount   FROM Assessment A
						   JOIN AssessmentResponse AR ON A.AssessmentID = AR.AssessmentID  						   
						   JOIN Response R ON R.ResponseID = AR.ResponseID 
						   JOIN info.ItemResponseBehavior IRB ON IRB.ItemResponseBehaviorID = AR.ItemResponseBehaviorID 
						   JOIN info.ItemResponseType IRT ON IRT.ItemResponseTypeID = IRB.ItemResponseTypeID
						   WHERE A.AssessmentID IN ({string.Join(',', latestAssessmentIDs.ToArray())}) AND A.PersonQuestionnaireID = {familyReportInputDTO.PersonQuestionnaireID}
						      AND AR.personSupportID = {personSupportID} AND CAST(AR.QuestionnaireItemID AS NVARCHAR(MAX)) +'-' + CAST(ISNULL(AR.PersonSupportID,'0') AS NVARCHAR(MAX)) NOT IN ('{string.Join("','", confidentialItems.ToArray())}')
						   GROUP BY A.AssessmentID,A.DateTaken,IRT.Name,IRB.Name HAVING IRT.Name = '{PCISEnum.ItemResponseType.SupportResource}'  
					   )						
						SELECT SA.AssessmentID,Max(strengthCount) AS ResourceToUse,SA.DateTaken,
						       DATEDIFF(DAY,sc.EnrollDate,SA.DateTaken) AS DaysInEpisode,TF.Timeframe_Std AS TimePeriod
						FROM SelectedAssessments SA 
						JOIN SelectedCollaboration sc ON sc.PersonID=SA.PersonID
						LEFT JOIN info.Timeframe TF on TF.DaysInService = DATEDIFF(DAY,sc.EnrollDate,SA.DateTaken)
						GROUP BY SA.AssessmentID,SA.DateTaken,sc.EnrollDate,Timeframe_Std order by SA.DateTaken asc";
                var strengthToUsePerAssessment = ExecuteSqlQuery(query, x => new SupportResourceToUsePerAssessmentDTO
                {
                    AssessmentID = x["AssessmentID"] == DBNull.Value ? 0 : (int)x["AssessmentID"],
                    ResourceToUse = x["ResourceToUse"] == DBNull.Value ? 0 : (int)x["ResourceToUse"],
                    AssessmentDate = x["DateTaken"] == DBNull.Value ? DateTime.MinValue : (DateTime)x["DateTaken"],
                    DaysInEpisode = x["DaysInEpisode"] == DBNull.Value ? 0 : (int)x["DaysInEpisode"],
                    TimePeriod = x["TimePeriod"] == DBNull.Value ? string.Empty : (string)x["TimePeriod"],
                });
                return strengthToUsePerAssessment;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private SupportDetailsInReportDTO GetSupportDetails(long personID, FamilyReportInputDTO familyReportInputDTO)
        {
            try
            {
                var query = string.Empty;
                query = @$"SELECT PS.PersonSupportID,PS.FirstName,ps.MiddleName,ps.LastName,ST.Name AS Relation FROM PersonSupport PS
							JOIN info.SupportType ST ON PS.SupportTypeID = ST.SupportTypeID
							WHERE personid = {personID} AND PersonSupportID = {familyReportInputDTO.VoiceTypeFKID}";
                var supportDetailsDTO = ExecuteSqlQuery(query, x => new SupportDetailsInReportDTO
                {
                    PersonSupportID = x["PersonSupportID"] == DBNull.Value ? 0 : (int)x["PersonSupportID"],
                    FirstName = x["FirstName"] == DBNull.Value ? string.Empty : (string)x["FirstName"],
                    MiddleName = x["MiddleName"] == DBNull.Value ? string.Empty : (string)x["MiddleName"],
                    LastName = x["LastName"] == DBNull.Value ? string.Empty : (string)x["LastName"],
                    Relation = x["Relation"] == DBNull.Value ? string.Empty : (string)x["Relation"]
                }).FirstOrDefault();

                return supportDetailsDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private SupportResourceReportSummaryDTO GetSupportResourceReportSummaryDetails(List<LatestSupportResourceDTO> supportRescourceForLast2Assessments, List<int> latestAssessmentIDs)
        {
            try
            {
                SupportResourceReportSummaryDTO reportStrengthSummaryDetails = new SupportResourceReportSummaryDTO();
                var resourceToBuildCount = supportRescourceForLast2Assessments.Where(x => x.AssessmentID == latestAssessmentIDs[0] && x.ResourceToBuild == true).ToList().Count();
                var resourceToUseCount = supportRescourceForLast2Assessments.Where(x => x.AssessmentID == latestAssessmentIDs[0] && x.ResourceToUse == true).Count();
                reportStrengthSummaryDetails.ResourceToBuild = resourceToBuildCount;
                reportStrengthSummaryDetails.ResourceToUse = resourceToUseCount;
                return reportStrengthSummaryDetails;
            }
            catch (Exception)
            {
                throw;
            }

        }

        private List<LatestSupportResourcePerItemDTO> GetLatestSupportResourceProgressPerItem(List<LatestSupportResourceDTO> supportRescourceForLast2Assessments, List<int> latestAssessmentIDs)
        {

            try
            {
                List<LatestSupportResourcePerItemDTO> latestProgressPerItem = new List<LatestSupportResourcePerItemDTO>();
                var query = string.Empty;
                var lastAssessmentID = latestAssessmentIDs[0];
                var prevAssessmentID = latestAssessmentIDs.Count == 2 ? latestAssessmentIDs[1] : 0;
                if (supportRescourceForLast2Assessments.Count > 0)
                {
                    var progressPerItem = supportRescourceForLast2Assessments.Select(x => new LatestSupportResourcePerItemDTO
                    {
                        ItemID = x.ItemID,
                        ItemDescription = x.ItemDescription,
                        ItemLabel = x.ItemLabel,
                        ItemName = x.ItemName,
                        PersonCareGiverID = x.PersonCareGiverID,
                        ResourceInLatestAssessment = new AssessmentSupportResourceDetailsDTO(),
                        ResourceInPreviousAssessment = new AssessmentSupportResourceDetailsDTO()
                    }).ToList();
                    latestProgressPerItem = progressPerItem.GroupBy(x => new { x.ItemID, x.PersonCareGiverID }).Select(g => g.First()).ToList();
                    foreach (var item in latestProgressPerItem)
                    {
                        var prevAssessmentResponseOnItem = supportRescourceForLast2Assessments.Where(x => x.ItemID == item.ItemID && x.PersonCareGiverID == item.PersonCareGiverID && x.AssessmentID == prevAssessmentID).ToList();
                        if (prevAssessmentResponseOnItem.Count() > 0)
                        {
                            item.ResourceInPreviousAssessment.AssessmentID = prevAssessmentResponseOnItem[0].AssessmentID;
                            item.ResourceInPreviousAssessment.AssessmentDate = prevAssessmentResponseOnItem[0].AssessmentDate;
                            item.ResourceInPreviousAssessment.ResourceToBuild = prevAssessmentResponseOnItem[0].ResourceToBuild;
                            item.ResourceInPreviousAssessment.ResourceToUse = prevAssessmentResponseOnItem[0].ResourceToUse;
                            item.ResourceInPreviousAssessment.ResponseScore = prevAssessmentResponseOnItem[0].ResponseScore;
                            item.ResourceInPreviousAssessment.ResponseID = prevAssessmentResponseOnItem[0].ResponseID;
                        }
                        var lastAssessmentResponseOnItem = supportRescourceForLast2Assessments.Where(x => x.ItemID == item.ItemID && x.PersonCareGiverID == item.PersonCareGiverID && x.AssessmentID == lastAssessmentID).ToList();
                        if (lastAssessmentResponseOnItem.Count() > 0)
                        {
                            item.ResourceInLatestAssessment.AssessmentID = lastAssessmentResponseOnItem[0].AssessmentID;
                            item.ResourceInLatestAssessment.AssessmentDate = lastAssessmentResponseOnItem[0].AssessmentDate;
                            item.ResourceInLatestAssessment.ResourceToBuild = lastAssessmentResponseOnItem[0].ResourceToBuild;
                            item.ResourceInLatestAssessment.ResourceToUse = lastAssessmentResponseOnItem[0].ResourceToUse;
                            item.ResourceInLatestAssessment.ResponseScore = lastAssessmentResponseOnItem[0].ResponseScore;
                            item.ResourceInLatestAssessment.ResponseID = lastAssessmentResponseOnItem[0].ResponseID;
                        }
                    }
                }
                return latestProgressPerItem;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private List<LatestSupportResourceDTO> GetSupportResourceProgressForLast2Assessments(FamilyReportInputDTO familyReportInputDTO, List<int> latestAssessmentIDs, List<string> confidentialItems, int personSupportID)
        {
            try
            {
                var query = string.Empty;
                query = @$"SELECT A.AssessmentID,A.PersonQuestionnaireID,A.DateTaken,
								AR.ResponseID,AR.ItemResponseBehaviorID,AR.QuestionnaireItemID,
								I.ItemID,I.Abbreviation AS ItemAbbrev,
								I.Name + ISNULL('-' + ps.FirstName, '') AS ItemName,ISNULL(AR.PersonSupportID,0) As PersonSupportID,
								I.Description AS ItemDescription,I.Label AS ItemLabel,
								IRB.Name AS ResponseBehaviour,IRB.ItemResponseBehaviorID,
								IRT.Name AS ResponseBehaviourType,IRT.ItemResponseTypeID,
								R.[Value] AS ResponseScore,
								CASE WHEN IRB.Name = '{PCISEnum.ToDo.Build}' THEN 1 
								ELSE 0 END AS StrengthToBuild,
								CASE WHEN IRB.Name = '{PCISEnum.ToDo.Use}' THEN 1 
								ELSE 0 END AS StrengthToUse		
								FROM AssessmentResponse AR
								JOIN Response R ON AR.ResponseID = R.ResponseID
								JOIN info.ItemResponseBehavior IRB ON IRB.ItemResponseBehaviorID = AR.ItemResponseBehaviorID 
								JOIN info.ItemResponseType IRT ON IRT.ItemResponseTypeID = IRB.ItemResponseTypeID
								JOIN Assessment A ON  A.AssessmentID = AR.AssessmentID  AND A.PersonQuestionnaireID = {familyReportInputDTO.PersonQuestionnaireID}
								JOIN QuestionnaireItem QI ON QI.QuestionnaireItemID = AR.QuestionnaireItemID
								JOIN Item I ON I.ItemID = QI.ItemID 
								LEFT JOIN PersonSupport ps ON ps.PersonSupportID=AR.PersonSupportID
								WHERE A.IsRemoved = 0 AND AR.IsRemoved = 0
								     AND (IRT.Name = '{PCISEnum.ItemResponseType.SupportResource}') AND (IRB.Name = '{PCISEnum.ToDo.Build}' OR IRB.Name = '{PCISEnum.ToDo.Use}')
								     AND (A.AssessmentID in ({string.Join(',', latestAssessmentIDs.ToArray())})) AND AR.PersonSupportID = {personSupportID} 
									 AND CAST(AR.QuestionnaireItemID AS NVARCHAR(MAX)) +'-' + CAST(ISNULL(AR.PersonSupportID,'0') AS NVARCHAR(MAX)) NOT IN ('{string.Join("','", confidentialItems.ToArray())}')";
                var latestProgressDTO = ExecuteSqlQuery(query, x => new LatestSupportResourceDTO
                {
                    ItemID = x["ItemID"] == DBNull.Value ? 0 : (int)x["ItemID"],
                    ItemLabel = x["ItemLabel"] == DBNull.Value ? string.Empty : (string)x["ItemLabel"],
                    ItemDescription = x["ItemDescription"] == DBNull.Value ? string.Empty : (string)x["ItemDescription"],
                    ItemName = x["ItemName"] == DBNull.Value ? string.Empty : (string)x["ItemName"],
                    AssessmentID = x["AssessmentID"] == DBNull.Value ? 0 : (int)x["AssessmentID"],
                    AssessmentDate = x["DateTaken"] == DBNull.Value ? DateTime.MinValue : (DateTime)x["DateTaken"],
                    ResponseID = x["ResponseID"] == DBNull.Value ? 0 : (int)x["ResponseID"],
                    ResponseScore = x["ResponseScore"] == DBNull.Value ? 0 : (decimal)x["ResponseScore"],
                    ResourceToBuild = x["StrengthToBuild"] == DBNull.Value ? false : (int)x["StrengthToBuild"] == 0 ? false : true,
                    ResourceToUse = x["StrengthToUse"] == DBNull.Value ? false : (int)x["StrengthToUse"] == 0 ? false : true,
                    PersonCareGiverID = x["PersonSupportID"] == DBNull.Value ? 0 : (int)x["PersonSupportID"],
                });

                return latestProgressDTO;
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region SupportNeedsFamilyReport
        public SupportNeedsReportDTO GetSupportNeedsFamilyReportData(long personID, FamilyReportInputDTO familyReportInputDTO, List<int> latestAssessmentIDs)
        {
            try
            {
                List<SupportNeedsDTO> supportTobeRemoved = new List<SupportNeedsDTO>();
                SupportNeedsReportDTO supportNeedsReportDTO = new SupportNeedsReportDTO();
                supportNeedsReportDTO.PersonDetails = GetPersonDetails(familyReportInputDTO.PersonIndex);
                supportNeedsReportDTO.QuestionnaireDetails = GetQuestionnaireDetails(personID, familyReportInputDTO);
                supportNeedsReportDTO.ReportPeriodDate = GetReportPeriodDate(personID, familyReportInputDTO);
                supportNeedsReportDTO.SupportDetails = (List<SupportNeedsDTO>)GetSupportReportDetails(personID, latestAssessmentIDs, PCISEnum.ItemResponseType.SupportNeed);
                var confidentialItems = GetConfidentialItems(familyReportInputDTO, latestAssessmentIDs, PCISEnum.ItemResponseType.SupportNeed);
                foreach (var support in supportNeedsReportDTO.SupportDetails)
                {
                    support.ReportDetails = new NeedsReportDetailsDTO();
                    var goalsReachedForAllAssessments = GetGoalReachedNeedsPerItem(familyReportInputDTO, latestAssessmentIDs, confidentialItems, PCISEnum.ItemResponseType.SupportNeed, support.PersonSupportID);
                    support.ReportDetails.GoalsReachedPerItem = GetGoalReachedNeeds(goalsReachedForAllAssessments, latestAssessmentIDs, PCISEnum.ItemResponseType.SupportNeed, support.PersonSupportID);
                    var lastAssessmentID = latestAssessmentIDs[0];
                    var prevAssessmentID = latestAssessmentIDs.Count >= 2 ? latestAssessmentIDs[1] : 0;
                    List<LatestNeedsDTO> supportNeedsForLast2Assessments = goalsReachedForAllAssessments.Where(x => x.AssessmentID == lastAssessmentID || x.AssessmentID == prevAssessmentID).ToList();
                    if (supportNeedsForLast2Assessments.Count > 0)
                    {
                        support.ReportDetails.InProgressFocusPerItem = GetInProgressNeedsPerItem(supportNeedsForLast2Assessments, latestAssessmentIDs.Take(2).ToList());
                        support.ReportDetails.BackgroundNeedsPerItem = GetBackgroundNeedsPerItem(supportNeedsForLast2Assessments, latestAssessmentIDs.Take(2).ToList());
                        support.ReportSummary = GetNeedsReportSummaryDetails(supportNeedsForLast2Assessments, latestAssessmentIDs);
                        support.ReportSummary.GoalsReached = support.ReportDetails.GoalsReachedPerItem.Count;
                    }
                    else
                    {
                        supportTobeRemoved.Add(support);
                        continue;
                    }
                    var goalsReached = support.ReportDetails.GoalsReachedPerItem?.Select(x => x.ItemName + x.PersonCareGiverID).ToList();
                    support.ReportDetails.ItemNotes = GetItemNotes(personID, familyReportInputDTO, latestAssessmentIDs, confidentialItems, PCISEnum.ItemResponseType.SupportNeed, support.PersonSupportID, goalsReached);
                }
                foreach (var support in supportTobeRemoved)
                {
                    supportNeedsReportDTO?.SupportDetails?.Remove(support);
                }
                return supportNeedsReportDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        public FamilyReportStatusDTO GetFamilyReportStatus(long personID, FamilyReportInputDTO familyReportInputDTO, List<int> latestAssessmentIDs)
        {
            try
            {
                var query = string.Empty;
                var confidentialItems = GetConfidentialItems(familyReportInputDTO, latestAssessmentIDs);
                query = @$";WITH CTE AS(
							SELECT A.AssessmentID,AR.AssessmentResponseID,				
										IRT.Name AS ResponseBehaviourType,
										IRB.Name AS ResponseBehaviour,							
										CASE WHEN IRT.Name = '{PCISEnum.ItemResponseType.Strength}' THEN 1
										ELSE 0 END AS PersonStrengthReport,
										CASE WHEN IRT.Name = '{PCISEnum.ItemResponseType.Need}' THEN 1
										ELSE 0 END AS PersonNeedReport,
										CASE WHEN IRT.Name = '{PCISEnum.ItemResponseType.SupportResource}' THEN 1
										ELSE 0 END AS SupportResourceReport,
										CASE WHEN IRT.Name = '{PCISEnum.ItemResponseType.SupportNeed}' THEN 1
										ELSE 0 END AS SupportNeedReport
										FROM AssessmentResponse AR
										JOIN Response R ON AR.ResponseID = R.ResponseID
										JOIN info.ItemResponseBehavior IRB ON IRB.ItemResponseBehaviorID = AR.ItemResponseBehaviorID 
										JOIN info.ItemResponseType IRT ON IRT.ItemResponseTypeID = IRB.ItemResponseTypeID
										JOIN Assessment A ON  A.AssessmentID = AR.AssessmentID AND (A.AssessmentID IN ({string.Join(',', latestAssessmentIDs.ToArray())}))
										WHERE A.IsRemoved = 0 AND AR.IsRemoved = 0  
										AND CAST(AR.QuestionnaireItemID AS NVARCHAR(MAX)) +'-' + CAST(ISNULL(AR.PersonSupportID,'0') AS NVARCHAR(MAX)) NOT IN ('{string.Join("','", confidentialItems.ToArray())}')
										AND (A.AssessmentID IN ({string.Join(',', latestAssessmentIDs.ToArray())}) AND
										((IRT.Name = '{PCISEnum.ItemResponseType.Strength}' OR IRT.Name = '{PCISEnum.ItemResponseType.SupportResource}') 
										   AND (IRB.Name = '{PCISEnum.ToDo.Build}' OR IRB.Name = '{PCISEnum.ToDo.Use}'))											
										OR (IRT.Name = '{PCISEnum.ItemResponseType.Need}' OR IRT.Name = '{PCISEnum.ItemResponseType.SupportNeed}') 		
										  AND (IRB.Name = '{PCISEnum.ToDo.Focus}' OR IRB.Name = '{PCISEnum.ToDo.Background}'))
							)
							SELECT MAX(PersonStrengthReport) PersonStrengthReport ,MAX(PersonNeedReport) PersonNeedReport,
							MAX(SupportResourceReport) SupportResourceReport,MAX(SupportNeedReport)  SupportNeedReport
							FROM CTE ";
                var latestProgressDTO = ExecuteSqlQuery(query, x => new FamilyReportStatusDTO
                {
                    PersonStrengthReportStatus = x["PersonStrengthReport"] == DBNull.Value ? false : (int)x["PersonStrengthReport"] == 0 ? false : true,
                    PersonNeedsReportStatus = x["PersonNeedReport"] == DBNull.Value ? false : (int)x["PersonNeedReport"] == 0 ? false : true,
                    SupportNeedsReportStatus = x["SupportNeedReport"] == DBNull.Value ? false : (int)x["SupportNeedReport"] == 0 ? false : true,
                    SupportResourceReportStatus = x["SupportResourceReport"] == DBNull.Value ? false : (int)x["SupportResourceReport"] == 0 ? false : true,
                }).FirstOrDefault();

                return latestProgressDTO;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private List<string> GetConfidentialItems(FamilyReportInputDTO familyReportInputDTO, List<int> latestAssessmentIDs, string reportType = "")
        {
            try
            {
                var query = string.Empty; var condition = string.Empty;
                if (reportType == PCISEnum.ItemResponseType.Strength || reportType == PCISEnum.ItemResponseType.SupportResource)
                {
                    condition = @$"AND ((IRT.Name = '{reportType}') AND (IRB.Name = 'Build' OR IRB.Name = 'Use'))";
                }
                else if (reportType == PCISEnum.ItemResponseType.Need || reportType == PCISEnum.ItemResponseType.SupportNeed)
                {
                    condition = @$"AND ((IRT.Name = '{reportType}') AND (IRB.Name = 'Focus' OR IRB.Name = 'Background'))";
                }
                else
                {
                    condition = @$"AND ((IRT.Name = '{PCISEnum.ItemResponseType.Strength}' OR IRT.Name = '{PCISEnum.ItemResponseType.SupportResource}') AND (IRB.Name = '{PCISEnum.ToDo.Build}' OR IRB.Name = '{PCISEnum.ToDo.Use}')) OR 									
					 ((IRT.Name = '{PCISEnum.ItemResponseType.Need}' OR IRT.Name = '{PCISEnum.ItemResponseType.SupportNeed}') AND (IRB.Name = '{PCISEnum.ToDo.Focus}' OR IRB.Name = '{PCISEnum.ToDo.Background}' OR IRB.Name ='{PCISEnum.ToDo.None}'))";
                }
                query = $@"SELECT DISTINCT CAST(AR.QuestionnaireItemID AS NVARCHAR(MAX)) +'-' + CAST(ISNULL(AR.PersonSupportID,'0') AS NVARCHAR(MAX)) ConfidentialsItems 
						    FROM AssessmentResponse AR
								JOIN Assessment A ON A.AssessmentID = AR.AssessmentID 
                                INNER JOIN QuestionnaireItem QI ON AR.QuestionnaireItemID = QI.QuestionnaireItemID
                                INNER JOIN Item I on I.itemID = QI.itemID
                                AND A.PersonQuestionnaireID = {familyReportInputDTO.PersonQuestionnaireID} 
								JOIN info.ItemResponseBehavior IRB ON IRB.ItemResponseBehaviorID = AR.ItemResponseBehaviorID 
								JOIN info.ItemResponseType IRT ON IRT.ItemResponseTypeID = IRB.ItemResponseTypeID
								WHERE A.IsRemoved = 0 AND AR.IsRemoved =0 AND ( (I.UseRequiredConfidentiality = 1 AND AR.IsRequiredConfidential = 1)
								  OR  (I.UsePersonRequestedConfidentiality = 1 AND AR.IsPersonRequestedConfidential = 1) 
								  OR (I.UseOtherConfidentiality = 1 AND AR.IsOtherConfidential = 1))";
                var confidentialItems = ExecuteSqlQuery(query, x => new string(x["ConfidentialsItems"] == DBNull.Value ? null : (string)x["ConfidentialsItems"]));
                return confidentialItems;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// GetAllQuestionnairesForSuperStoryMap.
        /// </summary>
        /// <param name="personID">personID.</param>
        /// <param name="personCollaborationID">personCollaborationID.</param>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <param name="sharedIDs">sharedIDs.</param>
        /// <returns></returns>
        public Tuple<List<AssessmentQuestionnaireDataDTO>, int> GetAllQuestionnairesForSuperStoryMap(long personID,long personCollaborationID, int pageNumber, int pageSize,  SharedDetailsDTO sharedIDs, SharedDetailsDTO helperColbIDs)
        {
            try
            {
                var sharedWhereConditionQIDs = string.IsNullOrEmpty(sharedIDs.SharedQuestionnaireIDs) ? "" : $@"AND PQ.QuestionnaireID IN ({sharedIDs.SharedQuestionnaireIDs})";
                var sharedWhereConditionCIDs = string.IsNullOrEmpty(sharedIDs.SharedCollaborationIDs) ? "" : $@"AND PC.CollaborationID IN ({sharedIDs.SharedCollaborationIDs})";

                var helperAssessmentIDs = string.IsNullOrEmpty(helperColbIDs.SharedAssessmentIDs) ? "" : $@"AND A.AssessmentID IN ({helperColbIDs.SharedAssessmentIDs})";
                var helperWhereConditionQIDs = string.IsNullOrEmpty(helperColbIDs.SharedQuestionnaireIDs) ? "" : $@"AND PQ.QuestionnaireID IN ({helperColbIDs.SharedQuestionnaireIDs})";
                var helperWhereConditionCIDs = string.IsNullOrEmpty(helperColbIDs.SharedCollaborationIDs) ? "" : $@"AND PC.CollaborationID IN ({helperColbIDs.SharedCollaborationIDs})";
                 var helperpersonCollaborationID = string.IsNullOrEmpty(helperColbIDs.SharedCollaborationIDs) ? personCollaborationID : -1;
                string query = @$";WITH SelectedAssessments AS (
                                   SELECT ROW_NUMBER() OVER (PARTITION BY PQ.QuestionnaireID ORDER BY A.DateTAken desc) rowNum,
								   A.personQuestionnaireID,A.AssessmentID,A.DateTaken,PQ.QuestionnaireID,PQ.PersonID
                                   FROM assessment A
                                	JOIN personQuestionnaire PQ ON PQ.personQuestionnaireID = A.personQuestionnaireID
									JOIN Questionnaire q ON Pq.QuestionnaireID=q.QuestionnaireID 
                                    JOIN  [info].[AssessmentStatus] ast on ast.[AssessmentStatusID]=A.[AssessmentStatusID]	
                                	WHERE PQ.personId = {personID} And pq.IsRemoved = 0 AND Q.IsRemoved = 0
                                	AND ast.Name in ('Returned','Submitted','Approved') AND A.Isremoved = 0  {sharedWhereConditionQIDs} {helperWhereConditionQIDs} {helperAssessmentIDs}
                                ) ,
                                LatestAssessments AS
                                (
                                	SELECT * FROM SelectedAssessments WHERE rowNum =1 
                                ),
                                QuestionnaireCTE AS
                                (
                                    SELECT
                                      	PQ.QuestionnaireID,PQ.PersonQuestionnaireID,ar.Name [Reason],qw.WindowOpenOffsetDays,qw.WindowCloseOffsetDays
                                      FROM 
                                      LatestAssessments pq 
                                      LEFT JOIN QuestionnaireWindow qw ON qw.QuestionnaireID=Pq.QuestionnaireID 
                                      LEFT JOIN info.AssessmentReason ar ON ar.AssessmentReasonID=qw.AssessmentReasonID 
                                      WHERE ar.Name IN ('Initial','Discharge') {sharedWhereConditionQIDs} {helperWhereConditionQIDs}
                                ), 
                                COlCTE AS
                                (
                                    SELECT C.CollaborationID,C.Name AS CollaborationName,PC.EnrollDate AS CollaborationStartDate,PC.EndDate AS CollaborationEndDate,                    PC.PersonCollaborationID,
                                	  PC.IsPrimary, PC.IsCurrent
                                	  FROM PersonCollaboration PC 
                                	  JOIN Collaboration C ON C.CollaborationID = PC.CollaborationID
                                	  WHERE PC.IsRemoved=0 AND PC.PersonID = {personID}	 AND
                                               (PC.PersonCollaborationID = {personCollaborationID} or {personCollaborationID} = 0) {sharedWhereConditionCIDs}  {helperWhereConditionCIDs}                    		  
                                ),
                                NotifyRiskSchedule AS
                                (
                                    SELECT
                                        *
                                    FROM
                                    (
                                        SELECT
                                            ROW_NUMBER() OVER(PARTITION BY qn.QuestionnaireID ORDER BY qn.QuestionnaireNotifyRiskScheduleID DESC) [RNo],
                                            qn.*
                                        FROM 
                                        LatestAssessments CTE
                                        JOIN QuestionnaireNotifyRiskSchedule qn ON QN.QuestionnaireID=CTE.QuestionnaireID and QN.IsRemoved=0
                                    )T
                                    WHERE RNo=1
                                ),
                                CompletedAssessments AS
                                (
                                     SELECT A.PersonQuestionnaireID,A.DateTaken,A.QuestionnaireID,A.AssessmentID,A.PersonID,
                                    	  wo_init.WindowOpenOffsetDays,
                                    	  wo_disc.WindowCloseOffsetDays,
                                    	  (CASE WHEN ({personCollaborationID}=0 AND {helperpersonCollaborationID} = 0) THEN NULL ELSE (SELECT MIN(CAST(CollaborationStartDate AS DATE)) FROM COlCTE) END) [EnrollDate],
                                    	  (CASE WHEN ({personCollaborationID}=0 AND {helperpersonCollaborationID} = 0) THEN NULL ELSE (SELECT MAX(CAST(ISNULL(CollaborationEndDate,GETDATE()) AS DATE)) FROM COlCTE) END) [EndDate]
                                        FROM
                                        LatestAssessments A
                                    	LEFT JOIN (SELECT * FROM QuestionnaireCTE WHERE Reason='Initial')wo_init ON wo_init.QuestionnaireID=A.QuestionnaireID 
                                        LEFT JOIN (SELECT * FROM QuestionnaireCTE WHERE Reason='Discharge')wo_disc ON wo_disc.QuestionnaireID=A.QuestionnaireID		
                                ),
                                selectedQuestionaires AS
                                (
                                    SELECT distinct CA.QuestionnaireID, str(Q.QuestionnaireID) + '-' + I.Abbrev + '-' + Q.Abbrev as QuestionnaireName,
									Q.AgencyID, Q.Abbrev as QuestionnaireAbbrev,I.InstrumentID, Q.StartDate, Q.EndDate, Q.ReminderScheduleName, Q.IsBaseQuestionnaire, 
                                        nrs.[Name] as NotificationScheduleName,CA.DateTaken,CA.AssessmentID,
                                        I.[Name] as InstrumentName,I.Abbrev as InstrumentAbbrev, Q.[Name],CA.PersonQuestionnaireID, CA.PersonID
                                    FROM CompletedAssessments CA
                                    LEFT JOIN Questionnaire Q ON CA.QuestionnaireID = Q.QuestionnaireID
									LEFT JOIN info.Instrument I on  Q.InstrumentID = I.InstrumentID
                                    LEFT JOIN NotifyRiskSchedule nrs ON nrs.QuestionnaireID=Q.QuestionnaireID
                                    JOIN COlCTE CT ON 
                                		( 
                                			({personCollaborationID}=0 AND {helperpersonCollaborationID} = 0) OR
                                			CAST(CA.DateTaken AS DATE) 
                                			BETWEEN
                                			DATEADD(DAY,0-ISNULL(CA.WindowOpenOffsetDays,0),CA.EnrollDate) 
                                			AND 
                                			DATEADD(DAY,ISNULL(CA.WindowCloseOffsetDays,0),CA.EndDate)
                                		)
                                ) SELECT COUNT(QuestionnaireID) OVER() [TotalCount],*
                                 from selectedQuestionaires SQ ORDER BY DateTaken DESC,SQ.QuestionnaireID ASC";
                //query += @" OFFSET " + ((pageNumber - 1) * pageSize) + "ROWS FETCH NEXT " + pageSize + " ROWS ONLY";

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
                        AssessmentID = x["AssessmentID"] == DBNull.Value ? 0 : (int)x["AssessmentID"],
                    };
                });
                return Tuple.Create(data.OrderBy(y => y.QuestionnaireID).ToList(), totalCount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SuperStoryMapDTO> GetAllDetailsForSuperStoryMap(SuperStoryInputDTO reportInputDTO, SharedDetailsDTO sharedIDs)
        {
            try
            {
                var sharedWhereConditionQIDs = string.IsNullOrEmpty(sharedIDs.SharedQuestionnaireIDs) ? "" : $@"AND PQ.QuestionnaireID IN ({sharedIDs.SharedQuestionnaireIDs})";
                var sharedWhereConditionCIDs = string.IsNullOrEmpty(sharedIDs.SharedCollaborationIDs) ? "" : $@"AND PC.CollaborationID IN ({sharedIDs.SharedCollaborationIDs})";

                var confidentialItemsHideInSharing = string.Empty;
                var confidentialCTE = string.Empty;
                if (!string.IsNullOrEmpty(sharedWhereConditionQIDs))
                {
                    confidentialItemsHideInSharing = $@"WHERE CAST(sar.QuestionnaireItemID AS NVARCHAR(MAX)) +'-' + CAST(ISNULL(sar.PersonSupportID,'0') AS NVARCHAR(MAX)) NOT IN (SELECT * FROM ConfidentialItems)";
                    confidentialCTE = $@",ConfidentialItems AS (
						  SELECT DISTINCT CAST(AR.QuestionnaireItemID AS NVARCHAR(MAX)) +'-' + CAST(ISNULL(AR.PersonSupportID,'0') AS NVARCHAR(MAX)) 
                          AS ConfidentialsItems 
						  FROM AssessmentResponse AR
						  JOIN Assessment A ON A.AssessmentID = AR.AssessmentID 
                          JOIN PersonQuestionnaire PQ ON PQ.PersonQuestionnaireID = A.PersonQuestionnaireID 
						  JOIN Person P ON P.PersonID = PQ.PersonID AND P.PersonIndex = '{reportInputDTO.PersonIndex}'
                          WHERE A.IsRemoved = 0 AND AR.IsRemoved = 0 
                              AND (PQ.QuestionnaireID = (SELECT QuestionnaireID FROM Questionnaire 
                                  WHERE QuestionnaireID in {reportInputDTO.QuestionnairesIDs}) 
                              OR PQ.QuestionnaireID = 0) AND (AR.IsRequiredConfidential = 1 
						      OR  AR.IsPersonRequestedConfidential = 1 OR AR.IsOtherConfidential = 1)
					)";
                }                
                List<SuperStoryMapDTO> storyMapList = new List<SuperStoryMapDTO>();
                var query = @$";WITH SelectedAssessment AS ( 
                                  SELECT AVT.*,Cases.Name as NameInDetail FROM
                                  (
                                      SELECT A.AssessmentID,A.voiceTypeID,VT.Name As VoiceType,ISNULL(A.VoiceTypeFKID,0) AS VoiceTypeFKID FROM 
                                      Assessment A
                                      JOIN info.voiceType VT ON A.VoiceTYpeID = VT.VoiceTYPEID
                                      WHERE assessmentId  in ({reportInputDTO.AssessmentIDs})
                                  ) AS AVT
                                  CROSS APPLY
                                  (
                                        SELECT P.FirstName+ ' '+isnull(P.MiddleName+' ', '')+ P.LastName As [Name],null AS FKValue,P.StartDate,P.EndDate,P.IsRemoved
                                        FROM Person P where P.PersonID = {reportInputDTO.PersonID}  AND LOWER(AVT.VoiceType) = '{PCISEnum.VoiceType.Consumer.ToLower()}'
                                        UNION ALL
                                        SELECT PS.FirstName+ ' '+isnull(PS.MiddleName+' ', '')+ PS.LastName As [Name],PS.PersonSupportID AS FKValue,
                                        PS.StartDate,PS.EndDate,PS.IsRemoved
                                        FROM PersonSupport PS where PS.PersonID = {reportInputDTO.PersonID}  
                                        AND (LOWER(AVT.VoiceType) = '{PCISEnum.VoiceType.Support.ToLower()}' AND AVT.VoiceTypeFKID = PS.PersonSupportID) 
                                        UNION ALL
                                        SELECT AVT.VoiceType,null,null,null,P.IsRemoved  FROM Person P WHERE P.PersonID = {reportInputDTO.PersonID} AND LOWER(AVT.VoiceType) = '{PCISEnum.VoiceType.Communimetric.ToLower()}'
                                        UNION ALL
                                        SELECT H.FirstName+ ' '+isnull(H.MiddleName+' ', '')+ H.LastName As [Name],PH.PersonHelperID AS FKValue,PH.StartDate,PH.EndDate,PH.IsRemoved
                                        FROM Helper H left join PersonHelper PH on PH.HelperID=H.HelperID
                                        where PH.PersonID ={reportInputDTO.PersonID}  AND (AVT.VoiceTypeFKID = PH.PersonHelperID                                     
                                        AND LOWER(AVT.VoiceType) = '{PCISEnum.VoiceType.Helper.ToLower()}') 
                                  ) AS Cases 
                                 ),
                                 SelectedAssessmentResponse AS
                				 (
                					SELECT
                						ar.*,SA.voiceTypeID,SA.VoiceType,SA.VoiceTypeFKID,SA.NameInDetail
                					FROM
                					AssessmentResponse ar 
                					JOIN SelectedAssessment SA on ar.AssessmentID = SA.AssessmentID
                					where ar.IsRemoved=0
                				 )
                             	 SELECT
                				    sar.AssessmentID,
                				    Ins.Abbrev,Q.QuestionnaireID,Q.Name As QuestionnaireName,
                					SAR.voiceTypeID,SAR.VoiceType,SAR.VoiceTypeFKID,SAR.NameInDetail,
                					irt.Name [Type],
                					qi.ItemID [ItemID],
                					c.Abbrev + '-' + i.Label + ISNULL('-' + ps.FirstName, '') [Item],
                					LEFT(r.label, 3) [Label],
                					r.description [LabelDescription],
                					r.Value [Score],
                					(CASE WHEN sar.Priority IS NULL THEN ROW_NUMBER() OVER(PARTITION BY irt.Name,irb.Name ORDER BY r.Value  DESC,i.Name  ASC) 
                                    	ELSE sar.Priority END) [Priority],
                                    	irb.Name [ToDo],sar.AssessmentResponseID,
                                    	cp.RGB [Rgb],
                                    	i.ResponseValueTypeID,
                                    	r.Value as minRange,
                                    	r.MaxRangeValue as maxRange,
                                    	sar.Value as AssessmentResponseValue
                                    FROM
                                    SelectedAssessmentResponse sar
                                    JOIN QuestionnaireItem qi ON qi.QuestionnaireItemID=sar.QuestionnaireItemID AND qi.IsRemoved=0
                                    JOIN Questionnaire Q On Q.QuestionnaireID = qi.QuestionnaireID
                                    JOIN Info.instrument Ins on Q.InstrumentID = Ins.InstrumentID
                                    JOIN Item i ON i.ItemID=qi.ItemID
                                    JOIN info.Category c ON c.CategoryID=qi.CategoryID
                                    JOIN Response r ON r.ResponseID=sar.ResponseID
                                    JOIN info.ItemResponseBehavior irb ON irb.ItemResponseBehaviorID=sar.ItemResponseBehaviorID AND irb.Name<>'None'
                                    JOIN info.ItemResponseType irt ON irt.ItemResponseTypeID=i.ItemResponseTypeID
                                    LEFT JOIN PersonSupport ps ON ps.PersonSupportID=sar.PersonSupportID
                                    LEFT JOIN info.ColorPalette cp ON cp.ColorPaletteID=r.BackgroundColorPaletteID
                                    ORDER BY sar.AssessmentID";

                storyMapList = ExecuteSqlQuery(query, x => new SuperStoryMapDTO
                {
                    Type = x["Type"] == DBNull.Value ? null : (string)x["Type"],
                    ItemID = x["ItemID"] == DBNull.Value ? 0 : (int)x["ItemID"],
                    Item = x["Item"] == DBNull.Value ? null : (string)x["Item"],
                    Label = x["Label"] == DBNull.Value ? null : (string)x["Label"],
                    LabelDescription = x["LabelDescription"] == DBNull.Value ? null : (string)x["LabelDescription"],
                    Score = x["Score"] == DBNull.Value ? null : (decimal?)x["Score"],
                    Priority = x["Priority"] == DBNull.Value ? null : (Int64?)x["Priority"],
                    ToDo = x["ToDo"] == DBNull.Value ? null : (string)x["ToDo"],
                    AssessmentResponseID = x["AssessmentResponseID"] == DBNull.Value ? 0 : (int)x["AssessmentResponseID"],
                    Rgb = x["Rgb"] == DBNull.Value ? null : (string)x["Rgb"],
                    ResponseValueTypeID = x["ResponseValueTypeID"] == DBNull.Value ? 0 : (int)x["ResponseValueTypeID"],
                    minRange = x["minRange"] == DBNull.Value ? 0 : (decimal)x["minRange"],
                    maxRange = x["maxRange"] == DBNull.Value ? 0 : (int)x["maxRange"],
                    AssessmentResponseValue = x["AssessmentResponseValue"] == DBNull.Value ? null : (string)x["AssessmentResponseValue"],
                    InstrumentAbbrev = x["Abbrev"] == DBNull.Value ? null : (string)x["Abbrev"],
                    VoiceType = x["VoiceType"] == DBNull.Value ? null : (string)x["VoiceType"],
                    VoiceTypeFKID = x["voiceTypeFKID"] == DBNull.Value ? 0 : (long?)x["voiceTypeFKID"],
                    VoiceTypeID = x["VoiceTypeID"] == DBNull.Value ? 0 : (int)x["VoiceTypeID"],
                    VoiceTypeInDetail = x["NameInDetail"] == DBNull.Value ? null : (string)x["NameInDetail"],
                    QuestionnaireID = x["QuestionnaireID"] == DBNull.Value ? 0 : (int)x["QuestionnaireID"],
                    QuestionnaireName = x["QuestionnaireName"] == DBNull.Value ? string.Empty : (string)x["QuestionnaireName"]
                });
                return storyMapList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}