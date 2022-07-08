// -----------------------------------------------------------------------
// <copyright file="AssessmentResponseRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Logging;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Infrastructure.Data.Repositories.Base;
using Opeeka.PICS.Infrastructure.Enums;

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{
    public class AssessmentResponseRepository : BaseRepository<AssessmentResponse>, IAssessmentResponseRepository
    {
        private readonly IMapper mapper;
        private readonly ILogger<AssessmentResponseRepository> logger;
        private readonly OpeekaDBContext _dbContext;

        private IAssessmentStatusRepository AssessmentStatusRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AssessmentResponseRepository"/> class.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public AssessmentResponseRepository(IAssessmentStatusRepository AssessmentStatusRepository, ILogger<AssessmentResponseRepository> logger, OpeekaDBContext dbContext, IMapper mapper)
            : base(dbContext)
        {
            this.mapper = mapper;
            this.logger = logger;
            _dbContext = dbContext;
            this.AssessmentStatusRepository = AssessmentStatusRepository;
        }


        /// <summary>
        /// GetAssessmentValues.
        /// </summary>
        /// <param name="personIndex">Index of a person.</param>
        /// <param name="questionnaireId">Id of a Questionnaire.</param>
        /// <returns>AssessmentResponseDTO.</returns>
        public List<AssessmentValuesDTO> GetAssessmentValues(Guid personIndex, int questionnaireId, string sharedAssessmentIDs, long loggedInAgencyID, string helpersAssessmentIDs, string assessmentIDs)
        {
            try
            {
                var sharedWhereCondition = string.Empty;
                var assesmentWhereCondition = string.Empty;
                var personAgencyCondition = $@"AND P.AgencyID = {loggedInAgencyID}";
                if(!string.IsNullOrEmpty(assessmentIDs))
                {
                    assesmentWhereCondition = $@"AND A.AssessmentID IN ({assessmentIDs})";
                }
                if (!string.IsNullOrEmpty(sharedAssessmentIDs))
                {
                    sharedWhereCondition = $@"AND A.AssessmentID IN ({sharedAssessmentIDs})";
                    personAgencyCondition = string.Empty;
                }
                var helperColbWhereCondition = string.Empty;
                if (!string.IsNullOrEmpty(helpersAssessmentIDs))
                {
                    helperColbWhereCondition = $@"AND A.AssessmentID IN ({helpersAssessmentIDs})";
                }
                List<AssessmentValuesDTO> AssessmentValuesDTO = new List<AssessmentValuesDTO>();
                var query = string.Empty;
                int supportVoiceTypeID = _dbContext.VoiceTypes.Where(x => x.Name == PCISEnum.VoiceType.Support).FirstOrDefault().VoiceTypeID;
                int personVoiceTypeID = _dbContext.VoiceTypes.Where(x => x.Name == PCISEnum.VoiceType.Consumer).FirstOrDefault().VoiceTypeID;

                query = $@";With AssessmnetChildResponses AS
                            ( 
                              SELECT b.AssessmentResponseID, b.ItemID AS ChildItemID, b.Value, b.GroupNumber, b.ParentAssessmentResponseID, b.ResponseID
                            						  FROM AssessmentResponse b 
                            						  WHERE  ParentAssessmentResponseID is not null AND b.IsRemoved = 0
                            						  AND b.Groupnumber is not null
                            )
                          SELECT a.assessmentID, qr.AssessmentResponseID as id, ISNULL(qr.PersonSupportID, 0) AS PersonSupportID, p.PersonID, qr.[ResponseId] as response, 
				          qr.ItemResponseBehaviorID , ib.Name, CASE WHEN I.UseRequiredConfidentiality = 1 THEN qr.IsRequiredConfidential
						  ELSE I.UseRequiredConfidentiality END  AS IsRequiredConfidential, r.ItemId , r.KeyCodes, r.value,qs.[Name] AS [status],
                          CASE WHEN I.UsePersonRequestedConfidentiality = 1 THEN qr.IsPersonRequestedConfidential
						  ELSE I.UsePersonRequestedConfidentiality END  AS IsPersonRequestedConfidential, CASE WHEN I.UseOtherConfidentiality = 1 THEN qr.IsOtherConfidential
						  ELSE I.UseOtherConfidentiality END  AS IsOtherConfidential, qr.QuestionnaireItemID, qr.IsCloned,vi.categoryID,a.[DateTaken] as [date],
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
		                            LEFT JOIN Person p ON n.VoiceTypeFKID = p.PersonID	AND VoiceTypeFKID is not null
		                            LEFT JOIN PersonSupport ps ON n.VoiceTypeFKID = ps.PersonSupportID AND VoiceTypeFKID is not null
					            WHERE qrn.AssessmentResponseID = qr.AssessmentResponseID
					            FOR JSON PATH),
                          Attachments = (SELECT
						            arf.AssessmentResponseAttachmentID, 
                                    arf.FileName,
                                    arf.FileURL,
						            arf.UpdateDate,
						            CASE WHEN arf.VoiceTypeFKID is null THEN u.Name
	                                     WHEN arf.VoiceTypeFKID is not null AND arf.AddedByVoiceTypeID = {personVoiceTypeID} THEN	p.FirstName+ ' ' + p.MiddleName + ' ' + p.LastName
			                             WHEN arf.VoiceTypeFKID is not null AND arf.AddedByVoiceTypeID = {supportVoiceTypeID} THEN pS.FirstName+ ' ' + pS.MiddleName + ' ' + pS.LastName END as Author
			                        FROM AssessmentResponseAttachment arf
						            LEFT JOIN [User] U ON arf.UpdateUserId = u.UserId
		                            LEFT JOIN Person p ON arf.VoiceTypeFKID = p.PersonID AND VoiceTypeFKID is not null
		                            LEFT JOIN PersonSupport ps ON arf.VoiceTypeFKID = ps.PersonSupportID AND VoiceTypeFKID is not null
					            WHERE arf.AssessmentResponseID = qr.AssessmentResponseID AND arf.IsRemoved =0 
					            FOR JSON PATH),
                          qr.CaregiverCategory,
                          a.voicetypeID,
                          cp.RGB As ColorPalette,
                          qr.Value as AssessmentResponseValue,
                          I.ResponseValueTypeId,
						  r.displayChildItem,
                          ChildItemResponses =  CASE WHEN r.displaychilditem =1 THEN
						  (SELECT b.AssessmentResponseID, b.ChildItemID, b.Value, b.GroupNumber, b.ParentAssessmentResponseID,b.ResponseID, i.ResponseValueTypeID
						  FROM AssessmnetChildResponses b 
                          join Item i on i.itemID=b.ChildItemID
						  WHERE  b.ParentAssessmentResponseID = qr.AssessmentResponseID  Order by b.GroupNumber FOR JSON PATH)
                          ELSE null END
			            FROM AssessmentResponse qr 
			            INNER join Assessment a on a.assessmentID=qr.assessmentID and a.IsRemoved=0
                        INNER JOIN info.AssessmentStatus qs ON a.AssessmentStatusID = qs.AssessmentStatusID 
			            INNER join personQuestionnaire pq on pq.PersonQuestionnaireID=a.PersonQuestionnaireID 
			            INNER join Person p on p.personID=pq.personID {personAgencyCondition}
			            INNER JOIN Response r ON qr.ResponseId = r.ResponseId AND r.IsRemoved = 0
                        LEFT JOIN info.ColorPalette cp ON cp.ColorPaletteID = r.BackgroundColorPaletteID 
			            INNER JOIN QuestionnaireItem vi ON r.ItemId = vi.ItemId AND vi.QuestionnaireID = {questionnaireId} and vi.IsRemoved=0
                        INNER JOIN Item I on I.itemID = Vi.itemID
                        LEFT join info.ItemResponseBehavior ib on ib.ItemResponseBehaviorID=qr.ItemResponseBehaviorID and ib.IsRemoved=0
	                    WHERE qr.ParentAssessmentResponseID IS NULL AND qr.IsRemoved = 0 and p.PersonIndex = '{personIndex}' AND pq.QuestionnaireID = { questionnaireId} {sharedWhereCondition} {helperColbWhereCondition} {assesmentWhereCondition}
                       ORDER BY vi.itemID";

                AssessmentValuesDTO = ExecuteSqlQuery(query, x => new AssessmentValuesDTO
                {
                    AssessmentID = x["assessmentID"] == DBNull.Value ? 0 : (int)x["assessmentID"],
                    AssessmentResponseID = x["Id"] == DBNull.Value ? 0 : (int)x["Id"],
                    PersonSupportID = x["PersonSupportID"] == DBNull.Value ? null : (int?)x["PersonSupportID"],
                    PersonID = x["PersonID"] == DBNull.Value ? 0 : (long)x["PersonID"],
                    ResponseId = x["response"] == DBNull.Value ? 0 : (int)x["response"],
                    ItemResponseBehaviorID = x["ItemResponseBehaviorID"] == DBNull.Value ? 0 : (int)x["ItemResponseBehaviorID"],
                    BehaviorName = x["Name"] == DBNull.Value ? null : (string)x["Name"],
                    IsRequiredConfidential = x["IsRequiredConfidential"] == DBNull.Value ? false : (bool)x["IsRequiredConfidential"],
                    ItemId = x["ItemId"] == DBNull.Value ? 0 : (int)x["ItemId"],
                    KeyCodes = x["KeyCodes"] == DBNull.Value ? null : (string)x["KeyCodes"],
                    Value = x["Value"] == DBNull.Value ? 0 : (decimal)x["Value"],
                    Status = x["status"] == DBNull.Value ? null : (string)x["status"],
                    IsPersonRequestedConfidential = x["IsPersonRequestedConfidential"] == DBNull.Value ? false : (bool)x["IsPersonRequestedConfidential"],
                    IsOtherConfidential = x["IsOtherConfidential"] == DBNull.Value ? false : (bool)x["IsOtherConfidential"],
                    QuestionnaireItemID = x["QuestionnaireItemID"] == DBNull.Value ? 0 : (int)x["QuestionnaireItemID"],
                    IsCloned = x["IsCloned"] == DBNull.Value ? false : (bool)x["IsCloned"],
                    CategoryID = x["categoryID"] == DBNull.Value ? 0 : (int)x["categoryID"],
                    Date = x["date"] == DBNull.Value ? DateTime.Now : (DateTime)x["date"],
                    Note = x["Notes"] == DBNull.Value ? null : (string)x["Notes"],
                    CaregiverCategory = x["CaregiverCategory"] == DBNull.Value ? null : (string)x["CaregiverCategory"],
                    VoicetypeID = x["voicetypeID"] == DBNull.Value ? 0 : (int)x["voicetypeID"],
                    ColorPalette = x["ColorPalette"] == DBNull.Value ? null : (string)x["ColorPalette"],
                    AssessmentResponseValue = x["AssessmentResponseValue"] == DBNull.Value ? null : (string)x["AssessmentResponseValue"],
                    ChildItemResponses = x["ChildItemResponses"] == DBNull.Value ? null : (string)x["ChildItemResponses"],
                    ResponseValueTypeId = x["ResponseValueTypeId"] == DBNull.Value ? 0 : (int)x["ResponseValueTypeId"],
                    Attachments = x["Attachments"] == DBNull.Value ? null : (string)x["Attachments"],
                });
                return AssessmentValuesDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetAssessmentValuesByAssessmentID.
        /// </summary>
        /// <param name="assessmentID">assessmentID.</param>
        /// <returns>AssessmentResponseDTO.</returns>
        public List<AssessmentValuesDTO> GetAssessmentValuesByAssessmentID(int assessmentID)
        {
            try
            {
                List<AssessmentValuesDTO> AssessmentValuesDTO = new List<AssessmentValuesDTO>();
                var query = string.Empty;
                int supportVoiceTypeID = _dbContext.VoiceTypes.Where(x => x.Name == PCISEnum.VoiceType.Support).FirstOrDefault().VoiceTypeID;
                int personVoiceTypeID = _dbContext.VoiceTypes.Where(x => x.Name == PCISEnum.VoiceType.Consumer).FirstOrDefault().VoiceTypeID;

                query = $@"SELECT a.assessmentID, qr.AssessmentResponseID as id, ISNULL(qr.PersonSupportID, 0), p.PersonID, qr.[ResponseId] as response,                        
				          qr.ItemResponseBehaviorID , ib.Name, qr.IsRequiredConfidential, r.ItemId , r.KeyCodes, r.value,qs.[Name] AS [status],
                          qr.IsPersonRequestedConfidential, qr.IsOtherConfidential, qr.QuestionnaireItemID, qr.IsCloned,vi.categoryID,a.[DateTaken] as [date],
				          Notes =CASE WHEN a.VoiceTypeID = {personVoiceTypeID} THEN
						  (SELECT
						            n.NoteId, 
						            n.NoteText,
						            n.UpdateDate,	
                                    p.FirstName+ ' ' + p.MiddleName + ' ' + p.LastName as Author
					             FROM AssessmentResponseNote qrn
						            INNER JOIN Note n ON qrn.NoteId = n.NoteId and n.IsRemoved=0
						            LEFT JOIN Person p ON n.VoiceTypeFKID = p.PersonID
					            WHERE qrn.AssessmentResponseID = qr.AssessmentResponseID
					            FOR JSON PATH)
								WHEN a.VoiceTypeID = { supportVoiceTypeID } THEN
						  (SELECT
						            n.NoteId, 
						            n.NoteText,
						            n.UpdateDate,						          
                                    ps.FirstName+ ' ' + ps.MiddleName + ' ' + ps.LastName as Author
					             FROM AssessmentResponseNote qrn
						            INNER JOIN Note n ON qrn.NoteId = n.NoteId and n.IsRemoved=0
						            LEFT JOIN PersonSupport ps ON n.VoiceTypeFKID = ps.PersonSupportID
					            WHERE qrn.AssessmentResponseID = qr.AssessmentResponseID
					            FOR JSON PATH)
								ELSE
						   (SELECT
						            n.NoteId, 
						            n.NoteText,
						            n.UpdateDate,
						            h.Name AS Author
					             FROM AssessmentResponseNote qrn
						            INNER JOIN Note n ON qrn.NoteId = n.NoteId and n.IsRemoved=0
						            LEFT JOIN [User] h ON n.UpdateUserId = h.UserId
					            WHERE qrn.AssessmentResponseID = qr.AssessmentResponseID
					            FOR JSON PATH) END,
                          qr.CaregiverCategory,
                          a.voicetypeID,
                          qr.Value,
                          r.displayChildItem,
                          ChildItemResponses =  CASE WHEN r.displaychilditem =1 THEN
						  (SELECT b.AssessmentResponseID, b.ItemID AS childItemID, b.GroupNumber, b.value
						  from AssessmentResponse b 
						  where  b.ParentAssessmentResponseID = qr.AssessmentResponseID AND b.IsRemoved = 0
						  AND b.Groupnumber is not null FOR JSON PATH)
                          ELSE null END,
                          Attachments = (SELECT
						            arf.AssessmentResponseAttachmentID, 
                                    arf.FileName,
                                    arf.FileURL,
						            arf.UpdateDate,
						            CASE WHEN arf.VoiceTypeFKID is null THEN u.Name
	                                     WHEN arf.VoiceTypeFKID is not null AND arf.AddedByVoiceTypeID = {personVoiceTypeID} THEN	p.FirstName+ ' ' + p.MiddleName + ' ' + p.LastName
			                             WHEN arf.VoiceTypeFKID is not null AND arf.AddedByVoiceTypeID = {supportVoiceTypeID} THEN pS.FirstName+ ' ' + pS.MiddleName + ' ' + pS.LastName END as Author
			                        FROM AssessmentResponseAttachment arf
						            LEFT JOIN [User] U ON arf.UpdateUserId = u.UserId
		                            LEFT JOIN Person p ON arf.VoiceTypeFKID = p.PersonID AND VoiceTypeFKID is not null
		                            LEFT JOIN PersonSupport ps ON arf.VoiceTypeFKID = ps.PersonSupportID AND VoiceTypeFKID is not null
					            WHERE arf.AssessmentResponseID = qr.AssessmentResponseID AND arf.IsRemoved =0 
					            FOR JSON PATH)
			            FROM AssessmentResponse qr 
			            INNER join Assessment a on a.assessmentID=qr.assessmentID and a.IsRemoved=0
                        INNER JOIN info.AssessmentStatus qs ON a.AssessmentStatusID = qs.AssessmentStatusID
						INNER join personQuestionnaire pq on pq.PersonQuestionnaireID=a.PersonQuestionnaireID and pq.IsRemoved=0
			            INNER join Person p on p.personID=pq.personID and p.IsRemoved=0 and p.IsActive=1
			            INNER JOIN Response r ON qr.ResponseId = r.ResponseId AND r.IsRemoved = 0
			            INNER JOIN QuestionnaireItem vi ON r.ItemId = vi.ItemId and vi.QuestionnaireID=pq.QuestionnaireID and vi.IsRemoved=0
                        LEFT join info.ItemResponseBehavior ib on ib.ItemResponseBehaviorID=qr.ItemResponseBehaviorID and ib.IsRemoved=0
	                    WHERE qr.IsRemoved = 0 and a.assessmentID = " + assessmentID + "	ORDER BY vi.itemID";

                AssessmentValuesDTO = ExecuteSqlQuery(query, x => new AssessmentValuesDTO
                {
                    AssessmentID = x[0] == DBNull.Value ? 0 : (int)x[0],
                    AssessmentResponseID = x[1] == DBNull.Value ? 0 : (int)x[1],
                    PersonSupportID = x[2] == DBNull.Value ? null : (int?)x[2],
                    PersonID = x[3] == DBNull.Value ? 0 : (long)x[3],
                    ResponseId = x[4] == DBNull.Value ? 0 : (int)x[4],
                    ItemResponseBehaviorID = x[5] == DBNull.Value ? 0 : (int)x[5],
                    BehaviorName = x[6] == DBNull.Value ? null : (string)x[6],
                    IsRequiredConfidential = x[7] == DBNull.Value ? false : (bool)x[7],
                    ItemId = x[8] == DBNull.Value ? 0 : (int)x[8],
                    KeyCodes = x[9] == DBNull.Value ? null : (string)x[9],
                    Value = x[10] == DBNull.Value ? 0 : (decimal)x[10],
                    Status = x[11] == DBNull.Value ? null : (string)x[11],
                    IsPersonRequestedConfidential = x[12] == DBNull.Value ? false : (bool)x[12],
                    IsOtherConfidential = x[13] == DBNull.Value ? false : (bool)x[13],
                    QuestionnaireItemID = x[14] == DBNull.Value ? 0 : (int)x[14],
                    IsCloned = x[15] == DBNull.Value ? false : (bool)x[15],
                    CategoryID = x[16] == DBNull.Value ? 0 : (int)x[16],
                    Date = x[17] == DBNull.Value ? DateTime.Now : (DateTime)x[17],
                    Note = x[18] == DBNull.Value ? null : (string)x[18],
                    CaregiverCategory = x[19] == DBNull.Value ? null : (string)x[19],
                    VoicetypeID = x[20] == DBNull.Value ? 0 : (int)x[20],
                    AssessmentResponseValue = x[21] == DBNull.Value ? null : (string)x[21],
                    ChildItemResponses = x[23] == DBNull.Value ? null : (string)x[23],
                    Attachments = x[24] == DBNull.Value ? null : (string)x[24],
                });
                return AssessmentValuesDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public List<AssessmentValuesDTO> GetNeedforFocusValues(List<string> assessmentResponseIds)
        {

            try
            {
                List<AssessmentValuesDTO> AssessmentValuesDTO = new List<AssessmentValuesDTO>();
                var query = string.Empty;
                var assessmentResponseIdsString = String.Join(",", assessmentResponseIds.ToArray());

                query = $@"WITH CTE AS 
					      (
					      SELECT AR.AssessmentResponseID,AR.ResponseID,AR.QuestionnaireItemID FROM AssessmentResponse AR 
					           JOIN info.ItemResponseBehavior IRB on AR.ItemResponseBehaviorID = IRB.ItemResponseBehaviorID
					      	 WHERE IRB.Name = '{PCISEnum.ToDo.Focus}' AND AR.AssessmentResponseID IN ({assessmentResponseIdsString})
					      )
					      SELECT AR.AssessmentResponseID,R.Value,C.Abbrev,I.Label
					              from CTE AR
                                  join Response R on R.ResponseID=AR.ResponseID
                                  join QuestionnaireItem QI on QI.QuestionnaireItemID=AR.QuestionnaireItemID
                                  join Item I on I.ItemID=QI.ItemID
								  join info.Category C on QI.CategoryID = C.CategoryID 
								  order by R.Value desc, C.Abbrev asc, I.Label asc";

                AssessmentValuesDTO = ExecuteSqlQuery(query, x => new AssessmentValuesDTO
                {
                    AssessmentResponseID = x[0] == DBNull.Value ? 0 : (int)x[0],
                    Value = x[1] == DBNull.Value ? 0 : (decimal)x[1],
                });
                return AssessmentValuesDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }



        /// <summary>
        /// AddAssessmentResponse
        /// </summary>
        /// <param name="assessmentResponse"></param>
        /// <returns>AssessmentResponse</returns>
        public AssessmentResponse AddAssessmentResponse(AssessmentResponse assessmentResponse)
        {
            try
            {
                var result = this.AddAsync(assessmentResponse).Result;
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// To get AssessmentResponse details.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns>AssessmentResponse.</returns>
        public async Task<IReadOnlyList<AssessmentResponsesDTO>> GetAssessmentResponse(int id)
        {
            try
            {
                IReadOnlyList<AssessmentResponsesDTO> assessmentResponseDTO = new List<AssessmentResponsesDTO>();
                IReadOnlyList<AssessmentResponse> assessmentResponse = await this.GetAsync(x => x.AssessmentID == id && !x.IsRemoved);
                this.mapper.Map<IReadOnlyList<AssessmentResponse>, IReadOnlyList<AssessmentResponsesDTO>>(assessmentResponse, assessmentResponseDTO);

                return assessmentResponseDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// To Update AssessmentResponse.
        /// </summary>
        /// <param name="assessmentResponse">assessmentResponse.</param>
        /// <returns>AssessmentResponse.</returns>
        public AssessmentResponse UpdateAssessmentResponse(AssessmentResponse assessmentResponse, bool isTracked = true)
        {
            try
            {
                if (!isTracked)
                {
                    _dbContext.AssessmentResponses.Attach(assessmentResponse);
                }
                AssessmentResponse result = this.UpdateAsync(assessmentResponse).Result;
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetAssessmentResponses.
        /// </summary>
        /// <param name="AssessmentResponseID">AssessmentResponseID.</param>
        /// <returns>AssessmentResponse.</returns>
        public async Task<AssessmentResponse> GetAssessmentResponses(int AssessmentResponseID)
        {
            try
            {
                AssessmentResponse assessmentResponse = await this.GetRowAsync(x => x.AssessmentResponseID == AssessmentResponseID);
                return assessmentResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// AssessmentPriority.
        /// </summary>
        /// <param name="assessmentResponse">assessmentResponse.</param>
        /// <returns>AssessmentResponse list.</returns>
        public List<AssessmentResponse> UpdateBulkAssessmentResponse(List<AssessmentResponse> assessmentResponse)
        {
            try
            {
                var res = this.UpdateBulkAsync(assessmentResponse);
                res.Wait();
                return assessmentResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// AssessmentPriority.
        /// </summary>
        /// <param name="assessmentResponse">assessmentResponse.</param>
        /// <returns>AssessmentResponse list.</returns>
        public List<AssessmentResponse> UpdatePriority(List<AssessmentResponse> assessmentResponse)
        {
            try
            {
                foreach (var item in assessmentResponse)
                {
                    string sql = @" Update AssessmentResponse set Priority=" + item.Priority + " where AssessmentResponseID=" + item.AssessmentResponseID;

                    var result = ExecuteSqlQuery(sql, x => new AssessmentResponse
                    {
                        AssessmentResponseID = x[0] == DBNull.Value ? 0 : (int)x[0]
                    });
                }
                return assessmentResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// GetConfidentialItems with caregiverID
        /// </summary>
        /// <param name="personIndex"></param>
        /// <param name="questionnaireID"></param>
        /// <returns></returns>
        private List<string> GetConfidentialItems(Guid personIndex, int questionnaireID)
        {
            try
            {
                var query = $@"SELECT DISTINCT CAST(AR.QuestionnaireItemID AS NVARCHAR(MAX)) +'-' + CAST(ISNULL(AR.PersonSupportID,'0') AS NVARCHAR(MAX))               ConfidentialsItems 
						    FROM AssessmentResponse AR
								JOIN Assessment A ON A.AssessmentID = AR.AssessmentID 
                                JOIN PersonQuestionnaire PQ ON PQ.PersonQuestionnaireID = A.PersonQuestionnaireID  
                                JOIN Person P ON P.PersonID = PQ.PersonID AND P.PersonIndex = '{personIndex}'
                                WHERE A.IsRemoved = 0 AND AR.IsRemoved = 0 
                                AND PQ.QuestionnaireID = {questionnaireID} AND (AR.IsRequiredConfidential = 1 
								      OR  AR.IsPersonRequestedConfidential = 1 OR AR.IsOtherConfidential = 1)";
                var confidentialItems = ExecuteSqlQuery(query, x => new string(x["ConfidentialsItems"] == DBNull.Value ? null : (string)x["ConfidentialsItems"]));
                return confidentialItems;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// AssessmentPriority.
        /// </summary>
        /// <param name="assessmentResponse">assessmentResponse.</param>
        /// <returns>AssessmentResponse list.</returns>
        public List<AssessmentResponse> AddBulkAssessmentResponse(List<AssessmentResponse> assessmentResponse)
        {
            try
            {
                var res = this.AddBulkAsync(assessmentResponse);
                res.Wait();
                return assessmentResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// GetAssessmentResponses.
        /// </summary>
        /// <param name="AssessmentResponseID">AssessmentResponseID.</param>
        /// <returns>AssessmentResponse.</returns>
        public async Task<AssessmentResponse> GetAssessmentResponseByGUID(Guid AssessmentResponseGuid)
        {
            try
            {
                AssessmentResponse assessmentResponse = await this.GetRowAsync(x => x.AssessmentResponseGuid == AssessmentResponseGuid);
                return assessmentResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// GetAssessmentResponses.
        /// </summary>
        /// <param name="AssessmentResponseID">AssessmentResponseID.</param>
        /// <returns>AssessmentResponse.</returns>
        public async Task<IReadOnlyList<AssessmentResponse>> GetAssessmentResponseList(List<int> AssessmentResponseIDList)
        {
            try
            {
                var assessmentResponse = await this.GetAsync(x => AssessmentResponseIDList.Contains(x.AssessmentResponseID));
                return assessmentResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// GetAssessmentResponses.
        /// </summary>
        /// <param name="AssessmentResponseID">AssessmentResponseID.</param>
        /// <returns>AssessmentResponse.</returns>
        public async Task<IReadOnlyList<AssessmentResponse>> GetAssessmentResponseListByGUID(List<Guid> AssessmentResponseGUIDList)
        {
            try
            {
                var assessmentResponse = await this.GetAsync(x => AssessmentResponseGUIDList.Contains(x.AssessmentResponseGuid));
                return assessmentResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// GetAssessmentResponseListByAssessmentId.
        /// </summary>
        /// <param name="AssessmentId">AssessmentId.</param>
        /// <returns>AssessmentResponse.</returns>
        public async Task<IReadOnlyList<AssessmentResponse>> GetAssessmentResponseListByAssessmentId(int AssessmentId)
        {
            try
            {
                var assessmentResponse = await this.GetAsync(x => x.AssessmentID == AssessmentId && !x.IsRemoved);
                return assessmentResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AssessmentResponsesDTO> GetAssessmentResponseFOrDashboardCalculation(long personId, int AssessmentId, int submittedStatusID, int approvedStatusID, int returnedStatusID)
        {
            try
            {
                List<AssessmentResponsesDTO> AssessmentValuesDTO = new List<AssessmentResponsesDTO>();
                var query = string.Empty;

                query = @";WITH ItemList	AS
							(
							   select I.ItemID from Assessment A
								join AssessmentResponse AR on A.AssessmentID=AR.AssessmentID
								join QuestionnaireItem QI on QI.QuestionnaireItemID=AR.QuestionnaireItemID
								join Item I on I.ItemID=QI.ItemID
								where A.AssessmentID=" + AssessmentId + @"
							)
							select AR.AssessmentResponseID, AR.AssessmentID,AR.ResponseID,AR.ItemResponseBehaviorID,AR.QuestionnaireItemID,I.itemID, A.DateTaken,Q.instrumentID
							from AssessmentResponse AR
							join Assessment A on A.AssessmentID=AR.AssessmentID and A.IsRemoved = 0
							join PersonQuestionnaire PQ on A.PersonQuestionnaireID=PQ.PersonQuestionnaireID
							inner join QuestionnaireItem QI on QI.QuestionnaireItemID=AR.QuestionnaireItemID and QI.QuestionnaireID=PQ.QuestionnaireID
							inner join Questionnaire Q on Q.QuestionnaireID=PQ.QuestionnaireID
							join Item I on I.ItemID=QI.ItemID
							join ItemList IL on I.ItemID=IL.ItemID
							where PQ.PersonID=" + personId + @" and AR.IsRemoved = 0 and A.AssessmentStatusID in (" + submittedStatusID + "," + approvedStatusID + "," + returnedStatusID +
                            @") order by  I.itemID asc, A.DateTaken desc";

                AssessmentValuesDTO = ExecuteSqlQuery(query, x => new AssessmentResponsesDTO
                {
                    AssessmentResponseID = x[0] == DBNull.Value ? 0 : (int)x[0],
                    AssessmentID = x[1] == DBNull.Value ? 0 : (int)x[1],
                    ResponseID = x[2] == DBNull.Value ? 0 : (int)x[2],
                    ItemResponseBehaviorID = x[3] == DBNull.Value ? 0 : (int)x[3],
                    QuestionnaireItemID = x[4] == DBNull.Value ? 0 : (int)x[4],
                    ItemId = x[5] == DBNull.Value ? 0 : (int)x[5],
                    DateTaken = x[6] == DBNull.Value ? DateTime.MinValue : (DateTime)x[6],
                    InstrumentID = x[7] == DBNull.Value ? 0 : (int)x[7],
                });
                return AssessmentValuesDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<AssessmentResponsesDTO> GetConfidentialQuestionnaireItemID(long personID, int questionnaireID, int assessmentID)
        {
            try
            {
                List<AssessmentResponsesDTO> AssessmentValuesDTO = new List<AssessmentResponsesDTO>();
                var query = $@"SELECT DIstinct AR.QuestionnaireItemID, AR.AssessmentResponseID
                            FROM PersonQuestionnaire PQ 
                            JOIN Assessment A ON A.PersonQuestionnaireID = PQ.PersonQuestionnaireID
                            JOIN AssessmentResponse AR ON AR.AssessmentID = A.AssessmentID
                            WHERE PQ.personID = {personID} AND PQ.questionnaireID = {questionnaireID} AND AR.IsRemoved = 0
                            AND A.IsRemoved =0 AND A.AssessmentID <> {assessmentID} AND
                            (AR.IsRequiredConfidential = 1 OR IsPersonRequestedConfidential = 1 OR IsOtherConfidential = 1)";

                //var confidentialQItemIDs = ExecuteSqlQuery(query, x => x["QuestionnaireItemID"] == DBNull.Value ? 0 : (int)x["QuestionnaireItemID"]);
                var confidentialQItemIDs = ExecuteSqlQuery(query, x => new AssessmentResponsesDTO
                {
                    QuestionnaireItemID = x["QuestionnaireItemID"] == DBNull.Value ? 0 : (int)x["QuestionnaireItemID"],
                    AssessmentResponseID = x["AssessmentResponseID"] == DBNull.Value ? 0 : (int)x["AssessmentResponseID"],
                });
                return confidentialQItemIDs;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<AssessmentResponse> GetAssessmentResponsesByID(List<int> assessmentResponseIDs)
        {
            try
            {
                return this.GetAsync(x => assessmentResponseIDs.Contains(x.AssessmentResponseID)).Result.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<AssessmentResponsesDTO> GetAssessmentResponseForDefualtResponseValue(int questionnaireID, Guid personIndex, List<int> questionnaireItemID)
        {
            try
            {
                var assessmentStatus = this.AssessmentStatusRepository.GetAllAssessmentStatus();
                var submittedStatus = assessmentStatus.Where(x => x.Name == PCISEnum.AssessmentStatus.Submitted).ToList()[0].AssessmentStatusID;
                var approvedStatus = assessmentStatus.Where(x => x.Name == PCISEnum.AssessmentStatus.Approved).ToList()[0].AssessmentStatusID;
                var returnedStatus = assessmentStatus.Where(x => x.Name == PCISEnum.AssessmentStatus.Returned).ToList()[0].AssessmentStatusID;
                List<AssessmentResponsesDTO> AssessmentValuesDTO = new List<AssessmentResponsesDTO>();
                var query = $@"; WITH TopAssessment AS
                             (
                                SELECT Top 1 A.AssessmentID 
                                FROM PersonQuestionnaire PQ
                                join Person P on PQ.PersonID = P.PersonID
                                JOIN Assessment A ON A.PersonQuestionnaireID = PQ.PersonQuestionnaireID 
                                WHERE P.PersonIndex = '{personIndex}' AND PQ.questionnaireID = {questionnaireID} AND PQ.ISRemoved = 0 
                                AND A.IsRemoved = 0 AND A.AssessmentStatusID in ({submittedStatus},{approvedStatus},{returnedStatus}) Order by DateTaken Desc
                            )
                            select AR.QuestionnaireItemID, AR.AssessmentResponseID, AR.ResponseID
                            from AssessmentResponse AR
                            join TopAssessment A on A.AssessmentID = AR.AssessmentID
                            Where AR.IsRemoved = 0 AND AR.QuestionnaireItemID in ({ String.Join(",", questionnaireItemID) })";

                AssessmentValuesDTO = ExecuteSqlQuery(query, x => new AssessmentResponsesDTO
                {
                    QuestionnaireItemID = x["QuestionnaireItemID"] == DBNull.Value ? 0 : (int)x["QuestionnaireItemID"],
                    AssessmentResponseID = x["AssessmentResponseID"] == DBNull.Value ? 0 : (int)x["AssessmentResponseID"],
                    ResponseID = x["ResponseID"] == DBNull.Value ? 0 : (int)x["ResponseID"],
                });
                return AssessmentValuesDTO;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// To get AssessmentResponse details.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns>AssessmentResponse.</returns>
        public async Task<IReadOnlyList<AssessmentResponsesDTO>> GetAllAssessmentResponses(int id)
        {
            try
            {
                IReadOnlyList<AssessmentResponsesDTO> assessmentResponseDTO = new List<AssessmentResponsesDTO>();
                IReadOnlyList<AssessmentResponse> assessmentResponse = await this.GetAsync(x => x.AssessmentID == id);
                this.mapper.Map<IReadOnlyList<AssessmentResponse>, IReadOnlyList<AssessmentResponsesDTO>>(assessmentResponse, assessmentResponseDTO);

                return assessmentResponseDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Function to fetch the child response aassesments based on the parent id
        /// </summary>
        /// <param name="AssessmentId"></param>
        /// <returns></returns>
        public async Task<List<AssessmentResponse>> GetAssessmentResponseListByParentId(int AssessmentResponseId)
        {
            try
            {
                var assessmentResponse = await this.GetAsync(x => x.ParentAssessmentResponseID == AssessmentResponseId && !x.IsRemoved);
                return assessmentResponse.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
