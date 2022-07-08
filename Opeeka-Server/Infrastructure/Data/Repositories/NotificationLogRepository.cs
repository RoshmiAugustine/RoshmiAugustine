// -----------------------------------------------------------------------
// <copyright file="NotificationLogRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.DTO.Output;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Infrastructure.Data.Repositories.Base;
using Opeeka.PICS.Infrastructure.Enums;

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{
    public class NotificationLogRepository : BaseRepository<NotificationLog>, INotificationLogRepository
    {
        private readonly OpeekaDBContext _dbContext;
        private readonly IMapper mapper;

        /// <summary>
        /// Defines the NotifiationResolutionStatusRepository.
        /// </summary>
        private readonly INotifiationResolutionStatusRepository notifiationResolutionStatusRepository;
        private readonly INotificationTypeRepository notificationTypeRepository;
        private readonly IPersonRepository personRepository;

        public NotificationLogRepository(OpeekaDBContext dbContext, IMapper mapper, INotifiationResolutionStatusRepository notifiationResolutionStatusRepository, INotificationTypeRepository notificationTypeRepository, IPersonRepository personRepository)
            : base(dbContext)
        {
            this.notifiationResolutionStatusRepository = notifiationResolutionStatusRepository;
            this._dbContext = dbContext;
            this.mapper = mapper;
            this.notificationTypeRepository = notificationTypeRepository;
            this.personRepository = personRepository;
        }

        /// <summary>
        /// UpdateNotificationLog.
        /// </summary>
        /// <param name="notificationLog">notificationLog.</param>
        /// <returns>NotificationLog</returns>
        public NotificationLog UpdateNotificationLog(NotificationLog notificationLog)
        {
            try
            {
                var result = this.UpdateAsync(notificationLog).Result;
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// GetNotificationLog.
        /// </summary>
        /// <param name="notificationLogID">notificationLogID.</param>
        /// <returns>NotificationLogDTO.</returns>
        public async Task<NotificationLogDTO> GetNotificationLog(int notificationLogID)
        {
            try
            {
                NotificationLogDTO notificationLogDTO = new NotificationLogDTO();
                NotificationLog notificationLog = await this.GetRowAsync(x => x.NotificationLogID == notificationLogID);
                this.mapper.Map<NotificationLog, NotificationLogDTO>(notificationLog, notificationLogDTO);
                return notificationLogDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetNotificationLogByID.
        /// </summary>
        /// <param name="notificationLogID">notificationLogID.</param>
        /// <returns>NotificationLogDTO.</returns>
        public NotificationLogDTO GetNotificationLogByID(int notificationLogID)
        {
            try
            {
                var query = @"select NL.*,NT.Name as TypeName from NotificationLog NL
                            join info.NotificationType NT on NL.NotificationTypeID = NT.NotificationTypeID
                            where NotificationLogID = " + notificationLogID;

                var notificationLogDTO = ExecuteSqlQuery(query, x => new NotificationLogDTO
                {
                    NotificationLogID = x["NotificationLogID"] == DBNull.Value ? 0 : (int)x["NotificationLogID"],
                    NotificationDate = (DateTime)(x["NotificationDate"] == DBNull.Value ? null : (DateTime?)x["NotificationDate"]),
                    PersonID = x["PersonID"] == DBNull.Value ? 0 : (long)x["PersonID"],
                    NotificationTypeID = x["NotificationTypeID"] == DBNull.Value ? 0 : (int)x["NotificationTypeID"],
                    FKeyValue = x["FKeyValue"] == DBNull.Value ? 0 : (int)x["FKeyValue"],
                    NotificationData = x["NotificationData"] == DBNull.Value ? null : (string)x["NotificationData"],
                    NotificationResolutionStatusID = x["NotificationResolutionStatusID"] == DBNull.Value ? 0 : (int)x["NotificationResolutionStatusID"],
                    StatusDate = x["StatusDate"] == DBNull.Value ? null : (DateTime?)x["StatusDate"],
                    IsRemoved = (bool)x["IsRemoved"],
                    UpdateUserID = x["UpdateUserID"] == DBNull.Value ? 0 : (int)x["UpdateUserID"],
                    NotificationTypeName = x["TypeName"] == DBNull.Value ? string.Empty : (string)x["TypeName"],
                    Details = x["Details"] == DBNull.Value ? string.Empty : (string)x["Details"],
                    QuestionnaireID = x["QuestionnaireID"] == DBNull.Value ? 0 : (int)x["QuestionnaireID"],
                    AssessmentID = x["AssessmentID"] == DBNull.Value ? 0 : (int)x["AssessmentID"],
                    AssessmentNoteID = x["AssessmentNoteID"] == DBNull.Value ? null : (int?)x["AssessmentNoteID"],
                    HelperName = x["HelperName"] == DBNull.Value ? string.Empty : (string?)x["HelperName"],
                }).FirstOrDefault();
                return notificationLogDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetReminderNotifications.
        /// </summary>
        /// <param name="notifyReminderId">notifyReminderId.</param>
        /// <returns>NotificationLogDTO.</returns>
        public List<NotificationLogDTO> GetReminderNotifications(int notifyReminderId)
        {
            try
            {
                var query = @";WITH PersonQuestionnaireScheduleData AS
                            (
	                            select PersonQuestionnaireScheduleID from NotifyReminder NR
								where NR.NotifyReminderID=" + notifyReminderId + @"
                            ), 
							NotifyReminderData AS
                            (
	                             select NR.NotifyReminderID from NotifyReminder NR
								join PersonQuestionnaireScheduleData NR1 on NR1.PersonQuestionnaireScheduleID=NR.PersonQuestionnaireScheduleID and NR.IsLogAdded=1
                            ) 
							select * from NotificationLog  NL
							join NotifyReminderData NRD on NL.FKeyValue=NRD.NotifyReminderID";

                var notificationLogDTO = ExecuteSqlQuery(query, x => new NotificationLogDTO
                {
                    NotificationLogID = x["NotificationLogID"] == DBNull.Value ? 0 : (int)x["NotificationLogID"],
                    NotificationDate = (DateTime)(x["NotificationDate"] == DBNull.Value ? null : (DateTime?)x["NotificationDate"]),
                    PersonID = x["PersonID"] == DBNull.Value ? 0 : (long)x["PersonID"],
                    NotificationTypeID = x["NotificationTypeID"] == DBNull.Value ? 0 : (int)x["NotificationTypeID"],
                    FKeyValue = x["FKeyValue"] == DBNull.Value ? 0 : (int)x["FKeyValue"],
                    NotificationData = x["NotificationData"] == DBNull.Value ? null : (string)x["NotificationData"],
                    NotificationResolutionStatusID = x["NotificationResolutionStatusID"] == DBNull.Value ? 0 : (int)x["NotificationResolutionStatusID"],
                    StatusDate = x["StatusDate"] == DBNull.Value ? null : (DateTime?)x["StatusDate"],
                    IsRemoved = (bool)x["IsRemoved"],
                    UpdateUserID = x["UpdateUserID"] == DBNull.Value ? 0 : (int)x["UpdateUserID"],
                    Details = x["Details"] == DBNull.Value ? string.Empty : (string)x["Details"],
                    QuestionnaireID = x["QuestionnaireID"] == DBNull.Value ? 0 : (int)x["QuestionnaireID"],
                    AssessmentID = x["AssessmentID"] == DBNull.Value ? 0 : (int)x["AssessmentID"],
                    AssessmentNoteID = x["AssessmentNoteID"] == DBNull.Value ? null : (int?)x["AssessmentNoteID"],
                    HelperName = x["HelperName"] == DBNull.Value ? null : (string)x["HelperName"],
                }).ToList();
                return notificationLogDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Get Notification Log List
        /// </summary>
        /// <param name="notificationLogSearchDTO"></param>
        /// <param name="queryBuilderDTO"></param>
        /// <returns>Tuple<List<NotificationLogDTO>, int></returns>
        public Tuple<List<NotificationLogDTO>, int> GetNotificationLogList(NotificationLogSearchDTO notificationLogSearchDTO, DynamicQueryBuilderDTO queryBuilderDTO)
        {
            try
            {
                List<NotificationLogDTO> notificationLogDTO = new List<NotificationLogDTO>();
                string query = string.Empty;

                NotificationResolutionStatusDTO notifiationResolutionStatus = this.notifiationResolutionStatusRepository.GetNotificationStatusForUnResolved();
                List<NotificationType> notification = this.notificationTypeRepository.GetAllNotificationType();
                int reminderTypeID = notification.Where(x => x.Name == PCISEnum.NotificationType.Reminder).ToList()[0].NotificationTypeID;
                int alertTypeID = notification.Where(x => x.Name == PCISEnum.NotificationType.Alert).ToList()[0].NotificationTypeID;
                int assessmentApproveTypeID = notification.Where(x => x.Name == PCISEnum.AssessmentNotificationType.Approve).ToList()[0].NotificationTypeID;
                int assessmentRejectTypeID = notification.Where(x => x.Name == PCISEnum.AssessmentNotificationType.Reject).ToList()[0].NotificationTypeID;
                int emailAssessmentSubmitTypeID = notification.Where(x => x.Name == PCISEnum.AssessmentNotificationType.EmailSubmit).ToList()[0].NotificationTypeID;
                int assessmentSubmitTypeID = notification.Where(x => x.Name == PCISEnum.AssessmentNotificationType.Submit).ToList()[0].NotificationTypeID;
                string helperColbQueryCondition = string.Empty;
                if (notificationLogSearchDTO.role != PCISEnum.Roles.SuperAdmin)
                {
                    List<string> personIdList = GetHelperPersonInCollaborationDetails(notifiationResolutionStatus.NotificationResolutionStatusID, notificationLogSearchDTO.UserID, notificationLogSearchDTO.agencyID);
                    if (personIdList.Count > 0)
                    {
                        string personIDs = string.Join(",", personIdList.ToArray());
                        List<string> notifcatnsInColbratn = GetHelperNotificationsInCollaboration(notificationLogSearchDTO, personIDs, notifiationResolutionStatus.NotificationResolutionStatusID, reminderTypeID);
                        string notifcatnsInColbratnIds = notifcatnsInColbratn.Count > 0 ? string.Join(",", notifcatnsInColbratn.ToArray()) : "0";
                        helperColbQueryCondition = $@"AND (P.PersonID NOT IN ({personIDs}) OR (P.PersonID IN ({personIDs}) AND NH.NotificationLogID IN ({notifcatnsInColbratnIds})))";
                         
                    }
                }
                if (notificationLogSearchDTO.role == PCISEnum.Roles.SuperAdmin)
                {
                    query = $@"select * from (select COUNT(NH.NotificationLogID) OVER() AS TotalCount,
		                                NT.[Name],
		                                NH.NotificationLogID
		                                ,NH.NotificationDate
		                                ,NH.PersonID,NH.NotificationTypeID,
		                                NH.AssessmentID as FKeyValue,
		                                NH.NotificationData,NH.NotificationResolutionStatusID,NH.StatusDate, 
		                                P.FirstName+ COALESCE(CASE P.MiddleName WHEN '' THEN '' ELSE ' '+P.MiddleName END, ' '+P.MiddleName, '') + COALESCE(CASE P.LastName WHEN '' THEN '' ELSE ' '+P.LastName END, ' '+P.LastName, '') as PersonName, 
		                                CASE NT.Name 
		                                WHEN 'AssessmentSubmit'  THEN 'Submitted'
		                                WHEN 'EmailAssessmentSubmit'  THEN 'Email Assessment Submit'
		                                WHEN 'AssessmentApproved'  THEN 'Approved'
		                                WHEN 'AssessmentRejected'  THEN 'Returned'
		                                ELSE NT.Name
		                                END as NotificationType,
		                                P.PersonIndex,
		                                NH.Details, 
		                                --NR.[Name] as Status,
		                                NH.QuestionnaireID,
		                                NH.AssessmentID  as AssessmentID,
                                        ISNULL(NH.HelperName,'') AS HelperName
		                                from NotificationLog NH  WITH (NOLOCK)
		                                join Person P WITH (NOLOCK) on P.PersonID=NH.PersonID AND P.AgencyID= {notificationLogSearchDTO.agencyID} AND  NH.IsRemoved=0 AND NH.NotificationResolutionStatusID= {notifiationResolutionStatus.NotificationResolutionStatusID} 
		                                join info.NotificationType NT WITH (NOLOCK) on NT.NotificationTypeID=NH.NotificationTypeID 
                            WHERE  NH.[NotificationTypeID] <> {reminderTypeID} ) NH WHERE 1=1";
                }
                else if (notificationLogSearchDTO.role == PCISEnum.Roles.OrgAdminRO || notificationLogSearchDTO.role == PCISEnum.Roles.OrgAdminRW || notificationLogSearchDTO.role == PCISEnum.Roles.OrgAdmin)
                {
                    query = $@"; WITH PersonHelped AS
			                                        (
				                                        SELECT DISTINCT
					                                        p.PersonID, p.AgencyID
				                                        FROM
				                                        Person P
				                                        JOIN PersonHelper ph ON ph.PersonID = p.PersonID
				                                        JOIN Helper h ON h.HelperID = ph.helperID
				                                        WHERE P.IsRemoved = 0 AND p.AgencyID = {notificationLogSearchDTO.agencyID} AND ph.IsRemoved = 0 AND h.AgencyID = {notificationLogSearchDTO.agencyID} AND h.IsRemoved = 0 AND h.HelperID = {notificationLogSearchDTO.helperID}
				                                        AND ph.StartDate <= CAST(GETDATE() AS DATE) AND ISNULL(ph.EndDate, CAST(GETDATE() AS DATE)) >= CAST(GETDATE() AS DATE)
			                                        ),   
                                        tempNotifications as (
		                                        select
		                                        NT.[Name],
		                                        NH.NotificationLogID
		                                        ,NH.NotificationDate
		                                        ,NH.PersonID,NH.NotificationTypeID,
		                                        NH.AssessmentID as FKeyValue,
		                                        NH.NotificationData,NH.NotificationResolutionStatusID,NH.StatusDate, 
		                                        P.FirstName+ COALESCE(CASE P.MiddleName WHEN '' THEN '' ELSE ' '+P.MiddleName END, ' '+P.MiddleName, '') + COALESCE(CASE P.LastName WHEN '' THEN '' ELSE ' '+P.LastName END, ' '+P.LastName, '') as PersonName, 
		                                        CASE NT.Name 
		                                        WHEN 'AssessmentSubmit'  THEN 'Submitted'
		                                        WHEN 'EmailAssessmentSubmit'  THEN 'Email Assessment Submit'
		                                        WHEN 'AssessmentApproved'  THEN 'Approved'
		                                        WHEN 'AssessmentRejected'  THEN 'Returned'
		                                        ELSE NT.Name
		                                        END as NotificationType,
		                                        P.PersonIndex,
		                                        NH.Details, 
		                                        --NR.[Name] as Status,
		                                        NH.QuestionnaireID,
		                                        NH.AssessmentID  as AssessmentID,
                                                ISNULL(NH.HelperName, '') AS HelperName                              
		                                        from NotificationLog NH WITH (NOLOCK)
		                                        join Person P WITH (NOLOCK) on P.PersonID=NH.PersonID
		                                        join info.NotificationType NT on NT.NotificationTypeID=NH.NotificationTypeID  
		                                        LEFT JOIN PersonHelped PH on NH.PersonID = PH.PersonID  and  PH.AgencyID= {notificationLogSearchDTO.agencyID}
		                                        WHERE NH.IsRemoved=0 AND NH.NotificationResolutionStatusID={notifiationResolutionStatus.NotificationResolutionStatusID} AND P.AgencyID= {notificationLogSearchDTO.agencyID} AND P.IsRemoved = 0  {helperColbQueryCondition}
                                        )
	                                        select COUNT(1) OVER() AS TotalCount,* 
	                                        from 
	                                        (
		                                        select
		                                        temp.NotificationLogID
		                                        ,temp.NotificationDate
		                                        ,temp.PersonID,temp.NotificationTypeID,
		                                        temp.FKeyValue,
		                                        temp.NotificationData,temp.NotificationResolutionStatusID,temp.StatusDate, 
		                                        temp.PersonName, 
		                                        temp.NotificationType,
		                                        temp.PersonIndex,
		                                        temp.Details, 
		                                        temp.QuestionnaireID,
		                                        temp.AssessmentID,     
		                                        temp.HelperName                          
		                                        from tempNotifications temp
		                                        LEFT JOIN PersonHelped PH on temp.PersonID = PH.PersonID  and  PH.AgencyID= {notificationLogSearchDTO.agencyID}
		                                        WHERE PH.PersonID = temp.PersonID
                                        union
		                                        select
		                                        temp.NotificationLogID
		                                        ,temp.NotificationDate
		                                        ,temp.PersonID,temp.NotificationTypeID,
		                                        temp.FKeyValue,
		                                        temp.NotificationData,temp.NotificationResolutionStatusID,temp.StatusDate, 
		                                        temp.PersonName, 
		                                        temp.NotificationType,
		                                        temp.PersonIndex,
		                                        temp.Details, 
		                                        temp.QuestionnaireID,
		                                        temp.AssessmentID,       
		                                        temp.HelperName                               
		                                        from tempNotifications temp
		                                        WHERE temp.[NotificationTypeID] <> ({reminderTypeID})
		                                        ) NH 
	                                        WHERE 1=1 ";
                }
                else if (notificationLogSearchDTO.role == PCISEnum.Roles.Supervisor)
                {
                    query = @$";WITH HelperList AS
                            (
                                SELECT
                                    h.helperID
                                FROM
                                Helper h
                                WHERE h.AgencyID = {notificationLogSearchDTO.agencyID} AND h.IsRemoved = 0 AND h.HelperID = {notificationLogSearchDTO.helperID}
                            )
                            ,PersonHelped AS
                            (
	                            SELECT DISTINCT
		                            p.PersonID
	                            FROM
	                            Person P
	                            JOIN PersonHelper ph ON ph.PersonID = p.PersonID
	                            JOIN HelperList h ON h.HelperID = ph.helperID
	                            WHERE P.IsRemoved = 0 AND p.AgencyID = {notificationLogSearchDTO.agencyID} AND ph.IsRemoved = 0
	                            AND ph.StartDate <= CAST(GETDATE() AS DATE) AND ISNULL(ph.EndDate, CAST(GETDATE() AS DATE)) >= CAST(GETDATE() AS DATE)
                            )
                            ,HelpersSupervised AS
                            (
	                            SELECT
                                    h.helperID
                                FROM
                                Helper h
                                WHERE h.AgencyID = {notificationLogSearchDTO.agencyID} AND h.IsRemoved = 0 AND h.SupervisorHelperID = {notificationLogSearchDTO.helperID}
                            )
                            ,PersonSupervised AS
                            (
	                            SELECT DISTINCT
		                            p.PersonID
	                            FROM
	                            Person P
	                            JOIN PersonHelper ph ON ph.PersonID = p.PersonID
	                            JOIN HelpersSupervised h ON h.HelperID = ph.helperID
	                            WHERE P.IsRemoved = 0 AND p.AgencyID = {notificationLogSearchDTO.agencyID} AND ph.IsRemoved = 0
	                            AND ph.StartDate <= CAST(GETDATE() AS DATE) AND ISNULL(ph.EndDate, CAST(GETDATE() AS DATE)) >= CAST(GETDATE() AS DATE)
                            )
                            ,HelpersReviewed AS
                            (
	                            SELECT
		                            h.helperID
	                            FROM
	                            Helper h
	                            WHERE h.AgencyID = {notificationLogSearchDTO.agencyID} AND h.IsRemoved = 0 AND 
	                            (h.ReviewerID = {notificationLogSearchDTO.helperID} --OR h.ReviewerID IN (SELECT HelperID FROM HelpersSupervised)
                                )
                            )
                            ,PersonToReview AS
                            (
	                            SELECT DISTINCT
		                            p.PersonID
	                            FROM
	                            Person P
	                            JOIN PersonHelper ph ON ph.PersonID = p.PersonID
	                            JOIN HelpersReviewed h ON h.HelperID = ph.helperID
	                            WHERE P.IsRemoved = 0 AND p.AgencyID = {notificationLogSearchDTO.agencyID} AND ph.IsRemoved = 0
	                            AND ph.StartDate <= CAST(GETDATE() AS DATE) AND ISNULL(ph.EndDate, CAST(GETDATE() AS DATE)) >= CAST(GETDATE() AS DATE)
                            )
                            ,PersonInCollaboration AS
                            (
	                            SELECT DISTINCT
		                            p.PersonID
	                            FROM
	                            HelperList h 
	                            JOIN PersonHelper ph ON ph.HelperID = h.HelperID
	                            AND ph.StartDate <= CAST(GETDATE() AS DATE) AND ISNULL(ph.EndDate, CAST(GETDATE() AS DATE)) >= CAST(GETDATE() AS DATE)
	                            JOIN CollaborationLeadHistory clh ON clh.LeadUserID = h.HelperID
	                            AND clh.StartDate <= CAST(GETDATE() AS DATE) AND ISNULL(clh.EndDate, CAST(GETDATE() AS DATE)) >= CAST(GETDATE() AS DATE)
	                            JOIN PersonCollaboration pc ON clh.CollaborationID = pc.CollaborationID
	                            AND pc.EnrollDate <= CAST(GETDATE() AS DATE) AND ISNULL(pc.EndDate, CAST(GETDATE() AS DATE)) >= CAST(GETDATE() AS DATE)
	                            JOIN Person p ON pc.PersonID = p.PersonID AND P.IsRemoved = 0
                            )
                            ,RecCountAlert As
                            (
	                            SELECT
		                            NH.NotificationLogID, p.PersonID
	                            FROM
	                            Person p
	                            JOIN
	                            (
		                            SELECT PersonID FROM PersonHelped
                                    UNION
		                            SELECT PersonID FROM PersonSupervised
		                            UNION
		                            SELECT PersonID FROM PersonInCollaboration
	                            )
	                            PersonToList ON p.PersonID = PersonToList.PersonID
	                            JOIN NotificationLog NH ON NH.PersonID = p.PersonID AND NH.IsRemoved = 0 AND NH.NotificationResolutionStatusID = {notifiationResolutionStatus.NotificationResolutionStatusID} AND NH.NotificationTypeID IN ({alertTypeID},{assessmentApproveTypeID},{assessmentRejectTypeID})
								union
								SELECT NotificationLogID, PersonID FROM(
									SELECT 
									ROW_NUMBER() OVER(PARTITIon BY PersonQuestionnaireScheduleID ORDER BY NR.NotifyDate) AS RowNumber, NH.NotificationLogID,P.PersonID
									from 
									Person p
									JOIN
									(
										SELECT PersonID FROM PersonHelped
                                        UNION
		                                SELECT PersonID FROM PersonSupervised
		                                UNION
		                                SELECT PersonID FROM PersonInCollaboration
									)
									PersonToList ON p.PersonID = PersonToList.PersonID
									join NotificationLog NH WITH (NOLOCK) 
									on P.PersonID=NH.PersonID
									join Notifyreminder NR on NR.NotifyReminderID=NH.FKeyValue
									WHERE NH.IsRemoved=0 AND  NH.NotificationtypeId={reminderTypeID} AND NH.NotificationResolutionStatusID= {notifiationResolutionStatus.NotificationResolutionStatusID} AND  nR.IsLogAdded=1
									) as A 
									WHERE A.RowNumber = 1
                            )
                            ,RecCountToReview As
                            (
	                            SELECT
		                            NH.NotificationLogID, p.PersonID
	                            FROM
	                            Person p
	                            JOIN PersonToReview ON p.PersonID = PersonToReview.PersonID
	                            JOIN NotificationLog NH ON NH.PersonID = p.PersonID AND NH.IsRemoved = 0 AND NH.NotificationResolutionStatusID = {notifiationResolutionStatus.NotificationResolutionStatusID} AND NH.NotificationTypeID IN ({alertTypeID},{assessmentSubmitTypeID},{emailAssessmentSubmitTypeID})
                            )
                            ,RecCount AS
                            (
	                            SELECT * FROM RecCountAlert
	                            UNION 
	                            SELECT * FROM RecCountToReview
                            )
                            SELECT 
                                COUNT(NotificationLogID) OVER() AS TotalCount,* 
                            FROM 
                            (
                               SELECT
                                    NH.NotificationLogID,
                                    NH.NotificationDate,
                                    NH.PersonID,
                                    NH.NotificationTypeID,
                                   NH.AssessmentID AS FKeyValue,
                                    NH.NotificationData,
                                    NH.NotificationResolutionStatusID,
                                    NH.StatusDate,
                                    P.FirstName+ COALESCE(CASE P.MiddleName WHEN '' THEN '' ELSE ' '+P.MiddleName END, ' '+P.MiddleName, '') 
		                            + COALESCE(CASE P.LastName WHEN '' THEN '' ELSE ' '+P.LastName END, ' '+P.LastName, '') as PersonName,
                                    CASE NT.Name 
                                        WHEN 'AssessmentSubmit'  THEN 'Submitted'
                                        WHEN 'EmailAssessmentSubmit'  THEN 'Email Assessment Submit'
                                        WHEN 'AssessmentApproved'  THEN 'Approved'
                                        WHEN 'AssessmentRejected'  THEN 'Returned'
                                    ELSE NT.Name
                                    END as NotificationType,
                                    P.PersonIndex,
                                    NH.Details, --NR.[Name] as Status,
                                    NH.QuestionnaireID,
                                   NH.AssessmentID,
                                  ISNULL(NH.HelperName, '') AS HelperName  
                                FROM
                                Person p
                                JOIN RecCount rec on p.PersonID=rec.PersonID
                                JOIN NotificationLog NH WITH (NOLOCK) ON NH.NotificationLogID=rec.NotificationLogID 
                                JOIN info.NotificationType NT ON NT.NotificationTypeID = NH.NotificationTypeID
                                LEFT JOIN PersonSupervised PLS on NH.PersonID = PLS.PersonID 
                                LEFT JOIN PersonHelped PH on NH.PersonID = PH.PersonID where  P.IsRemoved = 0 and P.IsActive = 1 {helperColbQueryCondition}
                            ) NH WHERE 1=1";
                }
                else if (notificationLogSearchDTO.role == PCISEnum.Roles.HelperRO || notificationLogSearchDTO.role == PCISEnum.Roles.HelperRW || notificationLogSearchDTO.role == PCISEnum.Roles.Helper || notificationLogSearchDTO.role == PCISEnum.Roles.Assessor)
                {
                    query = @$";WITH HelperList AS
                            (
                                SELECT
                                    h.HelperID
                                FROM
                                Helper h
                                WHERE h.AgencyID = {notificationLogSearchDTO.agencyID} AND h.IsRemoved = 0 AND h.HelperID = {notificationLogSearchDTO.helperID}
                            )
                            ,PersonHelped AS
                            (
	                            SELECT DISTINCT
		                            p.PersonID
	                            FROM
	                            Person P
	                            JOIN PersonHelper ph ON ph.PersonID = p.PersonID
	                            JOIN HelperList h ON h.HelperID = ph.helperID
	                            WHERE P.IsRemoved = 0 AND p.AgencyID = {notificationLogSearchDTO.agencyID} AND ph.IsRemoved = 0
	                            AND ph.StartDate <= CAST(GETDATE() AS DATE) AND ISNULL(ph.EndDate, CAST(GETDATE() AS DATE)) >= CAST(GETDATE() AS DATE)
                            )
                            ,HelpersReviewed AS
                            (
	                            SELECT
		                             h.HelperID
	                            FROM
	                            Helper h
	                            WHERE h.AgencyID = {notificationLogSearchDTO.agencyID} AND h.IsRemoved = 0 AND 
	                            h.ReviewerID = {notificationLogSearchDTO.helperID}
                            )
                            ,PersonToReview AS
                            (
	                            SELECT DISTINCT
		                            p.PersonID
	                            FROM
	                            Person P
	                            JOIN PersonHelper ph ON ph.PersonID = p.PersonID
	                            JOIN HelpersReviewed h ON h.HelperID = ph.helperID
	                            WHERE P.IsRemoved = 0 AND p.AgencyID = {notificationLogSearchDTO.agencyID} AND ph.IsRemoved = 0
	                            AND ph.StartDate <= CAST(GETDATE() AS DATE) AND ISNULL(ph.EndDate, CAST(GETDATE() AS DATE)) >= CAST(GETDATE() AS DATE)
                            )
                            ,PersonInCollaboration AS
                            (
	                            SELECT DISTINCT
		                            p.PersonID
	                            FROM
	                            HelperList h 
	                            JOIN PersonHelper ph ON ph.HelperID = h.HelperID
	                            AND ph.StartDate <= CAST(GETDATE() AS DATE) AND ISNULL(ph.EndDate, CAST(GETDATE() AS DATE)) >= CAST(GETDATE() AS DATE)
	                            JOIN CollaborationLeadHistory clh ON clh.LeadUserID = h.HelperID
	                            AND clh.StartDate <= CAST(GETDATE() AS DATE) AND ISNULL(clh.EndDate, CAST(GETDATE() AS DATE)) >= CAST(GETDATE() AS DATE)
	                            JOIN PersonCollaboration pc ON clh.CollaborationID = pc.CollaborationID
	                            AND pc.EnrollDate <= CAST(GETDATE() AS DATE) AND ISNULL(pc.EndDate, CAST(GETDATE() AS DATE)) >= CAST(GETDATE() AS DATE)
	                            JOIN Person p ON pc.PersonID = p.PersonID AND P.IsRemoved = 0
                            )
                            ,RecCountAlert As
                            (
	                            SELECT
		                            NH.NotificationLogID, p.PersonID
	                            FROM
	                            Person p
	                            JOIN
	                            (
		                            SELECT PersonID FROM PersonHelped
                                    UNION
		                            SELECT PersonID FROM PersonInCollaboration
	                            )
	                            PersonToList ON p.PersonID = PersonToList.PersonID
	                            JOIN NotificationLog NH ON NH.PersonID = p.PersonID AND NH.IsRemoved = 0 AND NH.NotificationResolutionStatusID = {notifiationResolutionStatus.NotificationResolutionStatusID} AND NH.NotificationTypeID IN ({alertTypeID},{assessmentApproveTypeID},{assessmentRejectTypeID})
								union
								SELECT NotificationLogID, PersonID FROM(
									SELECT 
									ROW_NUMBER() OVER(PARTITIon BY PersonQuestionnaireScheduleID ORDER BY NR.NotifyDate) AS RowNumber, NH.NotificationLogID,P.PersonID
									from 
									Person p
									JOIN
									(
										SELECT PersonID FROM PersonHelped
                                        UNION
		                                SELECT PersonID FROM PersonInCollaboration
									)
									PersonToList ON p.PersonID = PersonToList.PersonID
									join NotificationLog NH WITH (NOLOCK) 
									on P.PersonID=NH.PersonID
									join Notifyreminder NR on NR.NotifyReminderID=NH.FKeyValue
									WHERE NH.IsRemoved=0 AND  NH.NotificationtypeId={reminderTypeID} AND NH.NotificationResolutionStatusID= {notifiationResolutionStatus.NotificationResolutionStatusID} AND  nR.IsLogAdded=1
									) as A 
									WHERE A.RowNumber = 1
                            )
                            ,RecCountToReview As
                            (
	                            SELECT
		                            NH.NotificationLogID, p.PersonID
	                            FROM
	                            Person p
	                            JOIN PersonToReview ON p.PersonID = PersonToReview.PersonID
	                            JOIN NotificationLog NH ON NH.PersonID = p.PersonID AND NH.IsRemoved = 0 AND NH.NotificationResolutionStatusID = {notifiationResolutionStatus.NotificationResolutionStatusID} AND NH.NotificationTypeID IN ({alertTypeID},{assessmentSubmitTypeID},{emailAssessmentSubmitTypeID})
                            )
                            ,RecCount AS
                            (
	                            SELECT * FROM RecCountAlert
	                            UNION 
	                            SELECT * FROM RecCountToReview
                            )
                            SELECT 
                                COUNT(NotificationLogID) OVER() AS TotalCount,* 
                            FROM 
                            (
                                SELECT
                                    NH.NotificationLogID,
                                    NH.NotificationDate,
                                    NH.PersonID,
                                    NH.NotificationTypeID,
                                    NH.AssessmentID AS FKeyValue,
                                    NH.NotificationData,
                                    NH.NotificationResolutionStatusID,
                                    NH.StatusDate,
                                    P.FirstName+ COALESCE(CASE P.MiddleName WHEN '' THEN '' ELSE ' '+P.MiddleName END, ' '+P.MiddleName, '') 
		                            + COALESCE(CASE P.LastName WHEN '' THEN '' ELSE ' '+P.LastName END, ' '+P.LastName, '') as PersonName,
                                    CASE NT.Name 
                                        WHEN 'AssessmentSubmit'  THEN 'Submitted'
                                        WHEN 'EmailAssessmentSubmit'  THEN 'Email Assessment Submit'
                                        WHEN 'AssessmentApproved'  THEN 'Approved'
                                        WHEN 'AssessmentRejected'  THEN 'Returned'
                                    ELSE NT.Name
                                    END as NotificationType,
                                    P.PersonIndex,
                                    NH.Details, --NR.[Name] as Status,
                                   NH.QuestionnaireID,
                                    NH.AssessmentID,
                                   ISNULL(NH.HelperName, '') AS HelperName  
                                FROM
                                Person p
                                JOIN RecCount rec on p.PersonID=rec.PersonID
                                JOIN NotificationLog NH WITH (NOLOCK) ON NH.NotificationLogID=rec.NotificationLogID 
                                JOIN info.NotificationType NT ON NT.NotificationTypeID = NH.NotificationTypeID
                                LEFT JOIN PersonHelped PH ON PH.PersonID = NH.PersonID where  P.IsRemoved = 0 and P.IsActive = 1 {helperColbQueryCondition}
                            ) NH WHERE 1=1";
                }
                query += queryBuilderDTO.WhereCondition + queryBuilderDTO.OrderBy.Replace("DATE", "DATETIME") + queryBuilderDTO.Paginate;
                int? totalCount = 0;
                notificationLogDTO = ExecuteSqlQuery(query, x =>
                {
                    totalCount = x["TotalCount"] == DBNull.Value ? 0 : (int?)x["TotalCount"];
                    return new NotificationLogDTO
                    {
                        NotificationLogID = x["NotificationLogID"] == DBNull.Value ? 0 : (int)x["NotificationLogID"],
                        NotificationDate = (DateTime)(x["NotificationDate"] == DBNull.Value ? null : (DateTime?)x["NotificationDate"]),
                        PersonID = x["PersonID"] == DBNull.Value ? 0 : (long)x["PersonID"],
                        NotificationTypeID = x["NotificationTypeID"] == DBNull.Value ? 0 : (int)x["NotificationTypeID"],
                        FKeyValue = x["FKeyValue"] == DBNull.Value ? 0 : (int)x["FKeyValue"],
                        NotificationData = x["NotificationData"] == DBNull.Value ? null : (string)x["NotificationData"],
                        NotificationResolutionStatusID = x["NotificationResolutionStatusID"] == DBNull.Value ? 0 : (int)x["NotificationResolutionStatusID"],
                        StatusDate = x["StatusDate"] == DBNull.Value ? null : (DateTime?)x["StatusDate"],
                        PersonName = x["PersonName"] == DBNull.Value ? null : (string)x["PersonName"],
                        NotificationType = x["NotificationType"] == DBNull.Value ? null : (string)x["NotificationType"],
                        PersonIndex = x["PersonIndex"] == DBNull.Value ? Guid.Empty : (Guid)x["PersonIndex"],
                        Details = x["Details"] == DBNull.Value ? null : ((string)x["Details"]).Trim(),
                        Status = notifiationResolutionStatus.Name, //x["Status"] == DBNull.Value ? null : (string)x["Status"],
                        QuestionnaireID = x["QuestionnaireID"] == DBNull.Value ? 0 : (int)x["QuestionnaireID"],
                        AssessmentID = x["AssessmentID"] == DBNull.Value ? 0 : (int)x["AssessmentID"],
                        HelperName = x["HelperName"] == DBNull.Value ? string.Empty : (string)x["HelperName"]
                    };
                }, queryBuilderDTO.QueryParameterDTO);
                return Tuple.Create(notificationLogDTO, totalCount == null ? 0 : totalCount.Value);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private List<string> GetHelperNotificationsInCollaboration(NotificationLogSearchDTO notificationLogSearchDTO, string personIDs, int notificationResolutionStatusID, int reminderTypeID)
        {
            try
            {
                var query = $@";WITH PersonInHelp AS ( 
                        SELECT distinct personid
                        FROM Person
                         WHERE personid in ({ personIDs }) AND agencyID = {notificationLogSearchDTO.agencyID}
                    )
                   ,PersonInHelpColb AS (
		            	SELECT DISTINCT PH.CollaborationID, PH.PersonID,PC.EnrollDate,PC.EndDate,CQ.QuestionnaireID,PQ.PersonQuestionnaireID,CQ.Isremoved
                            FROM Helper H
                             JOIN PersonHelper  PH ON PH.HelperId = H.HelperID AND H.UserID = {notificationLogSearchDTO.UserID}
		            		 JOIN PersonInHelp  P ON PH.PersonID = P.PersonID
		            		 JOIN PersonCollaboration PC ON PC.CollaborationId = PH.CollaborationId 
		            							       AND PC.PersonID = PH.PersonID				
		            		 JOIN Collaboration C ON PC.CollaborationId = C.CollaborationId
                             LEFT JOIN CollaborationQuestionnaire CQ on CQ.CollaborationID = PC.CollaborationID
		            		 LEFT JOIN PersonQuestionnaire PQ ON PQ.PersonID = PC.PersonID 
		            					AND PQ.QuestionnaireID = CQ.QuestionnaireID
                             WHERE  H.AgencyID = {notificationLogSearchDTO.agencyID} AND PH.IsRemoved = 0 AND PC.IsRemoved = 0  AND C.IsRemoved = 0 
		            				AND ISNULL(CQ.IsRemoved,0) = 0 AND ISNULL(PQ.Isremoved,0) = 0
		            				AND PH.CollaborationID IS NOT NULL 
		            		UNION 
		            	SELECT '' AS CollaborationID, PH.PersonID , null AS EnrollDate,null AS EndDate,
                            PQ.QuestionnaireID,PQ.PersonQuestionnaireID,0 AS         Isremoved
		            	    FROM PersonInHelp P 
                             JOIN PersonHelper  PH ON P.personid =PH.Personid
		            		 JOIN PersonQuestionnaire PQ ON PH.Personid = PQ.Personid
		            		 WHERE ISNULL(PQ.CollaborationID, 0) = 0 AND PH.IsRemoved = 0 AND PQ.IsRemoved = 0 
		            						AND PQ.UpdateUserID = {notificationLogSearchDTO.UserID}
		          )--select * from PersonInHelpColb
                 ,PersonColb AS(
                                SELECT MIN(CAST(EnrollDate AS DATE)) AS CollaborationStartDate,--QuestionnaireID,--CollaborationID,IsRemoved,
                                    MAX(ISNULL(EndDate, CAST(GETDATE() AS DATE))) AS CollaborationEndDate,PersonID--,PersonQuestionnaireID
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
                 ,COLBQuestion AS(
                             SELECT distinct PC.Personid, PHC.QuestionnaireID, wo_init.WindowOpenOffsetDays, wo_disc.WindowCloseOffsetDays ,
                             DATEADD(DAY, 0 - ISNULL(wo_init.WindowOpenOffsetDays, 0), CAST(PC.CollaborationStartDate AS DATE)) AS  Enrolldate,DATEADD(DAY, ISNULL(wo_disc.WindowCloseOffsetDays, 0), ISNULL(PC.CollaborationEndDate, CAST(GETDATE() AS DATE))) AS         Enddate
                             FROM PersonInHelpColb PHC
                             JOIN PersonColb PC ON PC.personid = PHC.personid
                             JOIN(SELECT * FROM WindowOffsets WHERE Reason = 'Initial')wo_init ON wo_init.QuestionnaireID = PHC.QuestionnaireID
                             JOIN(SELECT * FROM WindowOffsets WHERE Reason = 'Discharge')wo_disc ON wo_disc.QuestionnaireID = PHC.QuestionnaireID
                    	)
                 ,AllNotifications AS
                      (  
                          SELECT NH.NotificationLogID,NH.NotificationTypeID,NH.QuestionnaireID,
                            NH.NotificationDate,NH.FKeyValue,NH.AssessmentID,NH.PersonID
                           FROM NotificationLog NH
                      	 JOIN PersonInHelp PH ON PH.PersonID = NH.PersonID 
                      	 WHERE NH.NotificationResolutionStatusID = {notificationResolutionStatusID} AND NH.IsRemoved = 0
                      )
                    SELECT
                          NH.NotificationLogID, p.PersonID
                              FROM
                          COLBQuestion P 
                          JOIN AllNotifications NH ON NH.PersonID = p.PersonID AND P.QuestionnaireID = NH.QuestionnaireID 
                              AND NH.NotificationTypeID <> {reminderTypeID}
                          JOIN Assessment A ON NH.AssessmentID = A.AssessmentID AND A.IsRemoved = 0
                          JOIN PersonInHelpColb PQ ON A.PersonQuestionnaireID = PQ.PersonQuestionnaireID
                          WHERE CAST(A.DateTaken AS DATE) BETWEEN P.Enrolldate AND P.Enddate
                         UNION ALL
                   SELECT NotificationLogID, PersonID FROM(
                         SELECT
                              ROW_NUMBER() OVER(PARTITIon BY PersonQuestionnaireScheduleID ORDER BY NR.NotifyDate) AS RowNumber,
                                NH.NotificationLogID,P.PersonID
                          FROM COLBQuestion p
                          join AllNotifications NH WITH(NOLOCK)
                          on P.PersonID = NH.PersonID AND P.QuestionnaireID = NH.QuestionnaireID
                          join Notifyreminder NR on NR.NotifyReminderID = NH.FKeyValue
                          WHERE NH.NotificationtypeId = {reminderTypeID}
                          AND nR.IsLogAdded  = 1 AND NH.NotificationDate BETWEEN P.Enrolldate AND P.Enddate
                    ) as A  WHERE A.RowNumber = 1";
                var resultIDs = ExecuteSqlQuery(query, x => new string(x["NotificationLogID"] == DBNull.Value ? string.Empty : x["NotificationLogID"].ToString())).Distinct();
                return resultIDs.ToList();
            
            }
            catch (Exception)
            {

                throw;
            }
        }


        /// <summary>
        /// AddNotificationLog
        /// </summary>
        /// <param name="notificationLog"></param>
        /// <returns>NotificationLog</returns>
        public NotificationLog AddNotificationLog(NotificationLog notificationLog)
        {
            try
            {
                var result = this.AddAsync(notificationLog).Result;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// AddNotificationLog
        /// </summary>
        /// <param name="notificationLog"></param>
        /// <returns>NotificationLog</returns>
        public void AddBulkNotificationLog(List<NotificationLog> notificationLog)
        {
            try
            {
                var result = this.AddBulkAsync(notificationLog);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// AddNotificationLog
        /// </summary>
        /// <param name="notificationLog"></param>
        /// <returns>NotificationLog</returns>
        public IReadOnlyList<NotificationLog> GetAssessmentNotificationLog(List<int> notificationTypeIDs, int assessmentId)
        {
            try
            {
                IReadOnlyList<NotificationLogDTO> notificationLogDTO = new List<NotificationLogDTO>();
                var result = this.GetAsync(x => notificationTypeIDs.Contains(x.NotificationTypeID) && x.FKeyValue == assessmentId && x.IsRemoved == false).Result;

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get Past Notification Log List
        /// </summary>
        /// <param name="notificationLogSearchDTO"></param>
        /// <param name="queryBuilderDTO"></param>
        /// <returns>Tuple<List<NotificationLogDTO>, int></returns>
        public Tuple<List<NotificationLogDTO>, int> GetPastNotificationLogList(NotificationLogSearchDTO notificationLogSearchDTO, DynamicQueryBuilderDTO queryBuilderDTO)
        {
            try
            {
                List<NotificationLogDTO> notificationLogDTO = new List<NotificationLogDTO>();
                var query = string.Empty;

                var notifiationResolutionStatus = this.notifiationResolutionStatusRepository.GetNotificationStatusForResolved();
                var notification = this.notificationTypeRepository.GetAllNotificationType();
                int reminderTypeID = notification.Where(x => x.Name == PCISEnum.NotificationType.Reminder).ToList()[0].NotificationTypeID;
                int alertTypeID = notification.Where(x => x.Name == PCISEnum.NotificationType.Alert).ToList()[0].NotificationTypeID;
                int assessmentApproveTypeID = notification.Where(x => x.Name == PCISEnum.AssessmentNotificationType.Approve).ToList()[0].NotificationTypeID;
                int assessmentRejectTypeID = notification.Where(x => x.Name == PCISEnum.AssessmentNotificationType.Reject).ToList()[0].NotificationTypeID;
                int emailAssessmentSubmitTypeID = notification.Where(x => x.Name == PCISEnum.AssessmentNotificationType.EmailSubmit).ToList()[0].NotificationTypeID;
                int assessmentSubmitTypeID = notification.Where(x => x.Name == PCISEnum.AssessmentNotificationType.Submit).ToList()[0].NotificationTypeID;
                string helperColbQueryCondition = string.Empty;
                if (notificationLogSearchDTO.role != PCISEnum.Roles.SuperAdmin)
                {
                    var personIdList = GetHelperPersonInCollaborationDetails(notifiationResolutionStatus.NotificationResolutionStatusID, notificationLogSearchDTO.UserID, notificationLogSearchDTO.agencyID);
                    if (personIdList.Count > 0)
                    {
                        string personIDs = string.Join(",", personIdList.ToArray());
                        List<string> notifcatnsInColbratn = GetHelperNotificationsInCollaboration(notificationLogSearchDTO, personIDs, notifiationResolutionStatus.NotificationResolutionStatusID, reminderTypeID);
                        string notifcatnsInColbratnIds = notifcatnsInColbratn.Count > 0 ? string.Join(",", notifcatnsInColbratn.ToArray()) : "0";
                        helperColbQueryCondition = $@"AND (P.PersonID NOT IN ({personIDs}) OR (P.PersonID IN ({personIDs}) AND NH.NotificationLogID IN ({notifcatnsInColbratnIds})))";
                    }
                }
                if (notificationLogSearchDTO.role == PCISEnum.Roles.SuperAdmin)
                {
                    query = $@"select * from (select COUNT(*) OVER() AS TotalCount,
		                                NT.[Name],
		                                NH.NotificationLogID
		                                ,NH.NotificationDate
		                                ,NH.PersonID,NH.NotificationTypeID,
		                                NH.AssessmentID as FKeyValue,
		                                NH.NotificationData,NH.NotificationResolutionStatusID,NH.StatusDate, 
		                                P.FirstName+ COALESCE(CASE P.MiddleName WHEN '' THEN '' ELSE ' '+P.MiddleName END, ' '+P.MiddleName, '') + COALESCE(CASE P.LastName WHEN '' THEN '' ELSE ' '+P.LastName END, ' '+P.LastName, '') as PersonName, 
		                                CASE NT.Name 
		                                WHEN 'AssessmentSubmit'  THEN 'Submitted'
		                                WHEN 'EmailAssessmentSubmit'  THEN 'Email Assessment Submit'
		                                WHEN 'AssessmentApproved'  THEN 'Approved'
		                                WHEN 'AssessmentRejected'  THEN 'Returned'
		                                ELSE NT.Name
		                                END as NotificationType,
		                                P.PersonIndex,
		                                NH.Details, 
		                                --NR.[Name] as Status,
		                                NH.QuestionnaireID,
		                                NH.AssessmentID  as AssessmentID,    
                                        ISNULL(NH.HelperName, '') AS HelperName                            
		                                from NotificationLog NH WITH (NOLOCK)
		                                join Person P WITH (NOLOCK) on P.PersonID=NH.PersonID
		                                join info.NotificationType NT on NT.NotificationTypeID=NH.NotificationTypeID  
                            WHERE NH.IsRemoved=0 AND NH.NotificationResolutionStatusID={notifiationResolutionStatus.NotificationResolutionStatusID} AND P.AgencyID= { notificationLogSearchDTO.agencyID } AND NH.[NotificationTypeID] NOT IN({reminderTypeID}) ) NH where 1=1";
                }
                else if (notificationLogSearchDTO.role == PCISEnum.Roles.OrgAdminRO || notificationLogSearchDTO.role == PCISEnum.Roles.OrgAdminRW || notificationLogSearchDTO.role == PCISEnum.Roles.OrgAdmin)
                {
                    query = $@";WITH PersonHelped AS
                        			(
                        				SELECT DISTINCT
                        					p.PersonID, p.AgencyID
                        				FROM
                        				Person P
                        				JOIN PersonHelper ph ON ph.PersonID = p.PersonID
                        				JOIN Helper h ON h.HelperID = ph.helperID
                        				WHERE P.IsRemoved = 0 AND p.AgencyID = {notificationLogSearchDTO.agencyID} AND ph.IsRemoved = 0 AND h.AgencyID = {notificationLogSearchDTO.agencyID} AND h.IsRemoved = 0 AND h.HelperID = {notificationLogSearchDTO.helperID}
                        				AND ph.StartDate <= CAST(GETDATE() AS DATE) AND ISNULL(ph.EndDate, CAST(GETDATE() AS DATE)) >= CAST(GETDATE() AS DATE)
                        			), 
                                tempNotifications as 
                                    (
                        		        SELECT
                        		            NT.[Name],
                        		            NH.NotificationLogID
                        		            ,NH.NotificationDate
                        		            ,NH.PersonID,NH.NotificationTypeID,
		                                    NH.AssessmentID as FKeyValue,
                        		            NH.NotificationData,NH.NotificationResolutionStatusID,NH.StatusDate, 
                        		            P.FirstName+ COALESCE(CASE P.MiddleName WHEN '' THEN '' ELSE ' '+P.MiddleName END, ' '+P.MiddleName, '') + COALESCE(CASE    P.LastName     WHEN     ''     THEN     ''     ELSE     '      '+P.LastName END, ' '+P.LastName, '') as PersonName, 
                        		            CASE NT.Name 
                        		            WHEN 'AssessmentSubmit'  THEN 'Submitted'
                        		            WHEN 'EmailAssessmentSubmit'  THEN 'Email Assessment Submit'
                        		            WHEN 'AssessmentApproved'  THEN 'Approved'
                        		            WHEN 'AssessmentRejected'  THEN 'Returned'
                        		            ELSE NT.Name
                        		            END as NotificationType,
                        		            P.PersonIndex,
                        		            NH.Details,                        		            
                        		            NH.QuestionnaireID,
                        		            NH.AssessmentID  as AssessmentID,
                                            ISNULL(NH.HelperName, '') AS HelperName                                
                        		        from NotificationLog NH WITH (NOLOCK)
                        		        join Person P WITH (NOLOCK) on P.PersonID=NH.PersonID
                        		        join info.NotificationType NT on NT.NotificationTypeID=NH.NotificationTypeID  
                        		        LEFT JOIN PersonHelped PH on NH.PersonID = PH.PersonID  and  PH.AgencyID= {notificationLogSearchDTO.agencyID}
                        		        WHERE NH.IsRemoved=0 AND NH.NotificationResolutionStatusID={notifiationResolutionStatus.NotificationResolutionStatusID} AND P.AgencyID= {notificationLogSearchDTO.agencyID} AND P.IsRemoved = 0   {helperColbQueryCondition}
                                  )
                        	SELECT COUNT(1) OVER() AS TotalCount,* 
                        	    FROM 
                        	     (
                        		    SELECT
                        		        temp.NotificationLogID
                        		        ,temp.NotificationDate
                        		        ,temp.PersonID,temp.NotificationTypeID,
                        		        temp.FKeyValue,
                        		        temp.NotificationData,temp.NotificationResolutionStatusID,temp.StatusDate, 
                        		        temp.PersonName, 
                        		        temp.NotificationType,
                        		        temp.PersonIndex,
                        		        temp.Details, 
                        		        temp.QuestionnaireID,
                        		        temp.AssessmentID,       
                        		        temp.HelperName                             
                        		        FROM tempNotifications temp
                                        LEFT JOIN PersonHelped PH on temp.PersonID = PH.PersonID  and  PH.AgencyID= {notificationLogSearchDTO.agencyID}
                        		        WHERE PH.PersonID = temp.PersonID
                                    UNION
                        		    SELECT
                        		        temp.NotificationLogID
                        		        ,temp.NotificationDate
                        		        ,temp.PersonID,temp.NotificationTypeID,
                        		        temp.FKeyValue,
                        		        temp.NotificationData,temp.NotificationResolutionStatusID,temp.StatusDate, 
                        		        temp.PersonName, 
                        		        temp.NotificationType,
                        		        temp.PersonIndex,
                        		        temp.Details, 
                        		        temp.QuestionnaireID,
                        		        temp.AssessmentID,       
                        		        temp.HelperName                             
                        		        FROM tempNotifications temp
                        		        WHERE temp.[NotificationTypeID] <> ({reminderTypeID})
                        		 ) NH
                        	WHERE 1=1 ";
                }
                else if (notificationLogSearchDTO.role == PCISEnum.Roles.Supervisor)
                {
                    query = @$";WITH HelperList AS
                            (
                                SELECT
                                    *
                                FROM
                                Helper h
                                WHERE h.AgencyID = {notificationLogSearchDTO.agencyID} AND h.IsRemoved = 0 AND h.HelperID = {notificationLogSearchDTO.helperID}
                            )
                            ,PersonHelped AS
                            (
	                            SELECT DISTINCT
		                            p.PersonID
	                            FROM
	                            Person P
	                            JOIN PersonHelper ph ON ph.PersonID = p.PersonID
	                            JOIN HelperList h ON h.HelperID = ph.helperID
	                            WHERE P.IsRemoved = 0 AND p.AgencyID = {notificationLogSearchDTO.agencyID} AND ph.IsRemoved = 0
	                            AND ph.StartDate <= CAST(GETDATE() AS DATE) AND ISNULL(ph.EndDate, CAST(GETDATE() AS DATE)) >= CAST(GETDATE() AS DATE)
                            )
                            ,HelpersSupervised AS
                            (
	                            SELECT
                                    *
                                FROM
                                Helper h
                                WHERE h.AgencyID = {notificationLogSearchDTO.agencyID} AND h.IsRemoved = 0 AND h.SupervisorHelperID = {notificationLogSearchDTO.helperID}
                            )
                            ,PersonSupervised AS
                            (
	                            SELECT DISTINCT
		                            p.PersonID
	                            FROM
	                            Person P
	                            JOIN PersonHelper ph ON ph.PersonID = p.PersonID
	                            JOIN HelpersSupervised h ON h.HelperID = ph.helperID
	                            WHERE P.IsRemoved = 0 AND p.AgencyID = {notificationLogSearchDTO.agencyID} AND ph.IsRemoved = 0
	                            AND ph.StartDate <= CAST(GETDATE() AS DATE) AND ISNULL(ph.EndDate, CAST(GETDATE() AS DATE)) >= CAST(GETDATE() AS DATE)
                            )                           
                            ,HelpersReviewed AS
                            (
	                            SELECT
		                            *
	                            FROM
	                            Helper h
	                            WHERE h.AgencyID = {notificationLogSearchDTO.agencyID} AND h.IsRemoved = 0 AND 
	                            (h.ReviewerID = {notificationLogSearchDTO.helperID} --OR h.ReviewerID IN (SELECT HelperID FROM HelpersSupervised)
                                )
                            )
                            ,PersonToReview AS
                            (
	                            SELECT DISTINCT
		                            p.PersonID
	                            FROM
	                            Person P
	                            JOIN PersonHelper ph ON ph.PersonID = p.PersonID
	                            JOIN HelpersReviewed h ON h.HelperID = ph.helperID
	                            WHERE P.IsRemoved = 0 AND p.AgencyID = {notificationLogSearchDTO.agencyID} AND ph.IsRemoved = 0
	                            AND ph.StartDate <= CAST(GETDATE() AS DATE) AND ISNULL(ph.EndDate, CAST(GETDATE() AS DATE)) >= CAST(GETDATE() AS DATE)
                            )
                            ,PersonInCollaboration AS
                            (
	                            SELECT DISTINCT
		                            p.PersonID
	                            FROM
	                            HelperList h 
	                            JOIN PersonHelper ph ON ph.HelperID = h.HelperID
	                            AND ph.StartDate <= CAST(GETDATE() AS DATE) AND ISNULL(ph.EndDate, CAST(GETDATE() AS DATE)) >= CAST(GETDATE() AS DATE)
	                            JOIN CollaborationLeadHistory clh ON clh.LeadUserID = h.HelperID
	                            AND clh.StartDate <= CAST(GETDATE() AS DATE) AND ISNULL(clh.EndDate, CAST(GETDATE() AS DATE)) >= CAST(GETDATE() AS DATE)
	                            JOIN PersonCollaboration pc ON clh.CollaborationID = pc.CollaborationID
	                            AND pc.EnrollDate <= CAST(GETDATE() AS DATE) AND ISNULL(pc.EndDate, CAST(GETDATE() AS DATE)) >= CAST(GETDATE() AS DATE)
	                            JOIN Person p ON pc.PersonID = p.PersonID AND P.IsRemoved = 0
                            )
                            ,RecCountAlert As
                            (
	                            SELECT
		                            NH.NotificationLogID, p.PersonID
	                            FROM
	                            Person p
	                            JOIN
	                            (
		                            SELECT PersonID FROM PersonHelped
                                    UNION
		                            SELECT PersonID FROM PersonSupervised
		                            UNION
		                            SELECT PersonID FROM PersonInCollaboration
	                            )
	                            PersonToList ON p.PersonID = PersonToList.PersonID
	                            JOIN NotificationLog NH ON NH.PersonID = p.PersonID AND NH.IsRemoved = 0 AND NH.NotificationResolutionStatusID = {notifiationResolutionStatus.NotificationResolutionStatusID} AND NH.NotificationTypeID IN ({alertTypeID},{assessmentApproveTypeID},{assessmentRejectTypeID})
								union
								SELECT NotificationLogID, PersonID FROM(
									SELECT 
									ROW_NUMBER() OVER(PARTITIon BY PersonQuestionnaireScheduleID ORDER BY NR.NotifyDate) AS RowNumber, NH.NotificationLogID,P.PersonID
									from 
									Person p
									JOIN
									(
										SELECT PersonID FROM PersonHelped
                                        UNION
		                                SELECT PersonID FROM PersonSupervised
		                                UNION
		                                SELECT PersonID FROM PersonInCollaboration
									)
									PersonToList ON p.PersonID = PersonToList.PersonID
									join NotificationLog NH WITH (NOLOCK) 
									on P.PersonID=NH.PersonID
									join Notifyreminder NR on NR.NotifyReminderID=NH.FKeyValue
									WHERE NH.IsRemoved=0 AND  NH.NotificationtypeId={reminderTypeID} AND NH.NotificationResolutionStatusID= {notifiationResolutionStatus.NotificationResolutionStatusID} AND  nR.IsLogAdded=1
									) as A 
									WHERE A.RowNumber = 1
                            )
                            ,RecCountToReview As
                            (
	                            SELECT
		                            NH.NotificationLogID, p.PersonID
	                            FROM
	                            Person p
	                            JOIN PersonToReview ON p.PersonID = PersonToReview.PersonID
	                            JOIN NotificationLog NH ON NH.PersonID = p.PersonID AND NH.IsRemoved = 0 AND NH.NotificationResolutionStatusID = {notifiationResolutionStatus.NotificationResolutionStatusID} AND NH.NotificationTypeID IN ({alertTypeID},{assessmentSubmitTypeID},{emailAssessmentSubmitTypeID})
                            )
                            ,RecCount AS
                            (
	                            SELECT * FROM RecCountAlert
	                            UNION 
	                            SELECT * FROM RecCountToReview
                            )
                            SELECT 
                                COUNT(*) OVER() AS TotalCount,* 
                            FROM 
                            (
                                SELECT
                                    NH.NotificationLogID,
                                    NH.NotificationDate,
                                    NH.PersonID,
                                    NH.NotificationTypeID,
                                    NH.AssessmentID AS FKeyValue,
                                    NH.NotificationData,
                                    NH.NotificationResolutionStatusID,
                                    NH.StatusDate,
                                    P.FirstName+ COALESCE(CASE P.MiddleName WHEN '' THEN '' ELSE ' '+P.MiddleName END, ' '+P.MiddleName, '') 
		                            + COALESCE(CASE P.LastName WHEN '' THEN '' ELSE ' '+P.LastName END, ' '+P.LastName, '') as PersonName,
                                    CASE NT.Name 
                                        WHEN 'AssessmentSubmit'  THEN 'Submitted'
                                        WHEN 'EmailAssessmentSubmit'  THEN 'Email Assessment Submit'
                                        WHEN 'AssessmentApproved'  THEN 'Approved'
                                        WHEN 'AssessmentRejected'  THEN 'Returned'
                                    ELSE NT.Name
                                    END as NotificationType,
                                    P.PersonIndex,
                                    NH.Details AS Details, --NR.[Name] as Status,
                                    NH.QuestionnaireID as QuestionnaireID,
                                    NH.AssessmentID as AssessmentID,
                                    ISNULL(NH.HelperName, '') AS HelperName  
                                FROM
                                Person p
                                JOIN RecCount rec on p.PersonID=rec.PersonID
                                JOIN NotificationLog NH WITH (NOLOCK) ON NH.NotificationLogID=rec.NotificationLogID 
                                JOIN info.NotificationType NT ON NT.NotificationTypeID = NH.NotificationTypeID
                                 LEFT JOIN PersonSupervised PLS on NH.PersonID = PLS.PersonID 
                                LEFT JOIN PersonHelped PH on NH.PersonID = PH.PersonID where  P.IsRemoved = 0 and P.IsActive = 1{helperColbQueryCondition}
                            ) NH WHERE 1=1 ";
                }
                else if (notificationLogSearchDTO.role == PCISEnum.Roles.HelperRO || notificationLogSearchDTO.role == PCISEnum.Roles.HelperRW || notificationLogSearchDTO.role == PCISEnum.Roles.Helper || notificationLogSearchDTO.role == PCISEnum.Roles.Assessor)
                {
                    query = @$";WITH HelperList AS
                            (
                                SELECT
                                    *
                                FROM
                                Helper h
                                WHERE h.AgencyID = {notificationLogSearchDTO.agencyID} AND h.IsRemoved = 0 AND h.HelperID = {notificationLogSearchDTO.helperID}
                            )
                            ,PersonHelped AS
                            (
	                            SELECT DISTINCT
		                            p.PersonID
	                            FROM
	                            Person P
	                            JOIN PersonHelper ph ON ph.PersonID = p.PersonID
	                            JOIN HelperList h ON h.HelperID = ph.helperID
	                            WHERE P.IsRemoved = 0 AND p.AgencyID = {notificationLogSearchDTO.agencyID} AND ph.IsRemoved = 0
	                            AND ph.StartDate <= CAST(GETDATE() AS DATE) AND ISNULL(ph.EndDate, CAST(GETDATE() AS DATE)) >= CAST(GETDATE() AS DATE)
                            )
                            ,HelpersReviewed AS
                            (
	                            SELECT
		                            *
	                            FROM
	                            Helper h
	                            WHERE h.AgencyID = {notificationLogSearchDTO.agencyID} AND h.IsRemoved = 0 AND 
	                            h.ReviewerID = {notificationLogSearchDTO.helperID}
                            )
                            ,PersonToReview AS
                            (
	                            SELECT DISTINCT
		                            p.PersonID
	                            FROM
	                            Person P
	                            JOIN PersonHelper ph ON ph.PersonID = p.PersonID
	                            JOIN HelpersReviewed h ON h.HelperID = ph.helperID
	                            WHERE P.IsRemoved = 0 AND p.AgencyID = {notificationLogSearchDTO.agencyID} AND ph.IsRemoved = 0
	                            AND ph.StartDate <= CAST(GETDATE() AS DATE) AND ISNULL(ph.EndDate, CAST(GETDATE() AS DATE)) >= CAST(GETDATE() AS DATE)
                            )
                            ,PersonInCollaboration AS
                            (
	                            SELECT DISTINCT
		                            p.PersonID
	                            FROM
	                            HelperList h 
	                            JOIN PersonHelper ph ON ph.HelperID = h.HelperID
	                            AND ph.StartDate <= CAST(GETDATE() AS DATE) AND ISNULL(ph.EndDate, CAST(GETDATE() AS DATE)) >= CAST(GETDATE() AS DATE)
	                            JOIN CollaborationLeadHistory clh ON clh.LeadUserID = h.HelperID
	                            AND clh.StartDate <= CAST(GETDATE() AS DATE) AND ISNULL(clh.EndDate, CAST(GETDATE() AS DATE)) >= CAST(GETDATE() AS DATE)
	                            JOIN PersonCollaboration pc ON clh.CollaborationID = pc.CollaborationID
	                            AND pc.EnrollDate <= CAST(GETDATE() AS DATE) AND ISNULL(pc.EndDate, CAST(GETDATE() AS DATE)) >= CAST(GETDATE() AS DATE)
	                            JOIN Person p ON pc.PersonID = p.PersonID AND P.IsRemoved = 0
                            )
                            ,RecCountAlert As
                            (
	                            SELECT
		                            NH.NotificationLogID, p.PersonID
	                            FROM
	                            Person p
	                            JOIN
	                            (
		                            SELECT PersonID FROM PersonHelped
                                    UNION
		                            SELECT PersonID FROM PersonInCollaboration
	                            )
	                            PersonToList ON p.PersonID = PersonToList.PersonID
	                            JOIN NotificationLog NH ON NH.PersonID = p.PersonID AND NH.IsRemoved = 0 AND NH.NotificationResolutionStatusID = {notifiationResolutionStatus.NotificationResolutionStatusID} AND NH.NotificationTypeID IN ({alertTypeID},{assessmentApproveTypeID},{assessmentRejectTypeID})
								union
								SELECT NotificationLogID, PersonID FROM(
									SELECT 
									ROW_NUMBER() OVER(PARTITIon BY PersonQuestionnaireScheduleID ORDER BY NR.NotifyDate) AS RowNumber, NH.NotificationLogID,P.PersonID
									from 
									Person p
									JOIN
									(
										SELECT PersonID FROM PersonHelped
                                        UNION
		                                SELECT PersonID FROM PersonInCollaboration
									)
									PersonToList ON p.PersonID = PersonToList.PersonID
									join NotificationLog NH WITH (NOLOCK) 
									on P.PersonID=NH.PersonID
									join Notifyreminder NR on NR.NotifyReminderID=NH.FKeyValue
									WHERE NH.IsRemoved=0 AND  NH.NotificationtypeId={reminderTypeID} AND NH.NotificationResolutionStatusID= {notifiationResolutionStatus.NotificationResolutionStatusID} AND  nR.IsLogAdded=1
									) as A 
									WHERE A.RowNumber = 1
                            )
                            ,RecCountToReview As
                            (
	                            SELECT
		                            NH.NotificationLogID, p.PersonID
	                            FROM
	                            Person p
	                            JOIN PersonToReview ON p.PersonID = PersonToReview.PersonID
	                            JOIN NotificationLog NH ON NH.PersonID = p.PersonID AND NH.IsRemoved = 0 AND NH.NotificationResolutionStatusID = {notifiationResolutionStatus.NotificationResolutionStatusID} AND NH.NotificationTypeID IN ({alertTypeID},{assessmentSubmitTypeID},{emailAssessmentSubmitTypeID})
                            )
                            ,RecCount AS
                            (
	                            SELECT * FROM RecCountAlert
	                            UNION 
	                            SELECT * FROM RecCountToReview
                            )
                            SELECT 
                                COUNT(*) OVER() AS TotalCount,* 
                            FROM 
                            (
                                SELECT
                                    NH.NotificationLogID,
                                    NH.NotificationDate,
                                    NH.PersonID,
                                    NH.NotificationTypeID,
                                    NH.AssessmentID AS FKeyValue,
                                    NH.NotificationData,
                                    NH.NotificationResolutionStatusID,
                                    NH.StatusDate,
                                    P.FirstName+ COALESCE(CASE P.MiddleName WHEN '' THEN '' ELSE ' '+P.MiddleName END, ' '+P.MiddleName, '') 
		                            + COALESCE(CASE P.LastName WHEN '' THEN '' ELSE ' '+P.LastName END, ' '+P.LastName, '') as PersonName,
                                    CASE NT.Name 
                                        WHEN 'AssessmentSubmit'  THEN 'Submitted'
                                        WHEN 'EmailAssessmentSubmit'  THEN 'Email Assessment Submit'
                                        WHEN 'AssessmentApproved'  THEN 'Approved'
                                        WHEN 'AssessmentRejected'  THEN 'Returned'
                                    ELSE NT.Name
                                    END as NotificationType,
                                    P.PersonIndex, 
                                    NH.Details as Details, --NR.[Name] as Status,
                                    NH.QuestionnaireID as QuestionnaireID,
                                    NH.AssessmentID as AssessmentID,
                                    ISNULL(NH.HelperName, '') AS HelperName  
                                FROM
                                Person p
                                JOIN RecCount rec on p.PersonID=rec.PersonID
                                JOIN NotificationLog NH WITH (NOLOCK) ON NH.NotificationLogID=rec.NotificationLogID 
                                JOIN info.NotificationType NT ON NT.NotificationTypeID = NH.NotificationTypeID
                                LEFT JOIN PersonHelped PH ON PH.PersonID = NH.PersonID  where  P.IsRemoved = 0 and P.IsActive = 1{helperColbQueryCondition}
                            ) NH WHERE 1=1";
                }
                query += queryBuilderDTO.WhereCondition + queryBuilderDTO.OrderBy.Replace("DATE", "DATETIME") + queryBuilderDTO.Paginate;
                int? totalCount = 0;
                notificationLogDTO = ExecuteSqlQuery(query, x =>
                {
                    totalCount = x["TotalCount"] == DBNull.Value ? 0 : (int?)x["TotalCount"];
                    return new NotificationLogDTO
                    {
                        NotificationLogID = x["NotificationLogID"] == DBNull.Value ? 0 : (int)x["NotificationLogID"],
                        NotificationDate = (DateTime)(x["NotificationDate"] == DBNull.Value ? null : (DateTime?)x["NotificationDate"]),
                        PersonID = x["PersonID"] == DBNull.Value ? 0 : (long)x["PersonID"],
                        NotificationTypeID = x["NotificationTypeID"] == DBNull.Value ? 0 : (int)x["NotificationTypeID"],
                        FKeyValue = x["FKeyValue"] == DBNull.Value ? 0 : (int)x["FKeyValue"],
                        NotificationData = x["NotificationData"] == DBNull.Value ? null : (string)x["NotificationData"],
                        NotificationResolutionStatusID = x["NotificationResolutionStatusID"] == DBNull.Value ? 0 : (int)x["NotificationResolutionStatusID"],
                        StatusDate = x["StatusDate"] == DBNull.Value ? null : (DateTime?)x["StatusDate"],
                        PersonName = x["PersonName"] == DBNull.Value ? null : (string)x["PersonName"],
                        NotificationType = x["NotificationType"] == DBNull.Value ? null : (string)x["NotificationType"],
                        PersonIndex = x["PersonIndex"] == DBNull.Value ? Guid.Empty : (Guid)x["PersonIndex"],
                        Details = x["Details"] == DBNull.Value ? null : ((string)x["Details"]).Trim(),
                        Status = notifiationResolutionStatus.Name,//x["Status"] == DBNull.Value ? null : (string)x["Status"],
                        QuestionnaireID = x["QuestionnaireID"] == DBNull.Value ? 0 : (int)x["QuestionnaireID"],
                        AssessmentID = x["AssessmentID"] == DBNull.Value ? 0 : (int)x["AssessmentID"],
                        HelperName = x["HelperName"] == DBNull.Value ? string.Empty : (string)x["HelperName"]
                    };
                }, queryBuilderDTO.QueryParameterDTO);
                return Tuple.Create(notificationLogDTO, totalCount == null ? 0 : totalCount.Value);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Get NotificationLog For Notification ResolveAlert
        /// </summary>
        /// <param name="personID"></param>
        /// <returns></returns>
        public List<NotificationLog> GetNotificationLogForNotificationResolveAlert(long personID)
        {
            List<NotificationLog> notificationLogDTO = new List<NotificationLog>();
            try
            {
                int statusID = this.notifiationResolutionStatusRepository.GetNotificationStatusForUnResolved().NotificationResolutionStatusID;
                var query = "select * from NotificationLog NL join NotifyRisk NR on NR.NotifyRiskID = NL.FKeyValue and NR.PersonID = " + personID + " join QuestionnaireNotifyRiskRule QNR on QNR.QuestionnaireNotifyRiskRuleID = NR.QuestionnaireNotifyRiskRuleID where QNR.IsRemoved = 1 and NL.NotificationResolutionStatusID = " + statusID;

                notificationLogDTO = ExecuteSqlQuery(query, x => new NotificationLog
                {
                    NotificationLogID = x["NotificationLogID"] == DBNull.Value ? 0 : (int)x["NotificationLogID"],
                    NotificationDate = (DateTime)(x["NotificationDate"] == DBNull.Value ? null : (DateTime?)x["NotificationDate"]),
                    PersonID = x["PersonID"] == DBNull.Value ? 0 : (long)x["PersonID"],
                    NotificationTypeID = x["NotificationTypeID"] == DBNull.Value ? 0 : (int)x["NotificationTypeID"],
                    FKeyValue = x["FKeyValue"] == DBNull.Value ? 0 : (int)x["FKeyValue"],
                    NotificationData = x["NotificationData"] == DBNull.Value ? null : (string)x["NotificationData"],
                    NotificationResolutionStatusID = x["NotificationResolutionStatusID"] == DBNull.Value ? 0 : (int)x["NotificationResolutionStatusID"],
                    StatusDate = x["StatusDate"] == DBNull.Value ? null : (DateTime?)x["StatusDate"],
                    IsRemoved = (bool)x["IsRemoved"],
                    UpdateDate = (DateTime)(x["UpdateDate"] == DBNull.Value ? null : (DateTime?)x["UpdateDate"]),
                    UpdateUserID = x["UpdateUserID"] == DBNull.Value ? 0 : (int)x["UpdateUserID"],
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return notificationLogDTO;
        }

        /// <summary>
        /// Get UnResolved NotificationLog For Notification Resolve Alert
        /// </summary>
        /// <param name="personID"></param>
        /// <returns></returns>
        public List<RiskNotificationsListDTO> GetUnResolvedNotificationLogForNotificationResolveAlert(long personID)
        {
            List<RiskNotificationsListDTO> notificationLogDTO = new List<RiskNotificationsListDTO>();
            try
            {
                int statusID = this.notifiationResolutionStatusRepository.GetNotificationStatusForUnResolved().NotificationResolutionStatusID;
                var query = "select * from NotificationLog NL join NotifyRisk NR on NR.NotifyRiskID=NL.FKeyValue and NR.PersonID=" + personID + " join QuestionnaireNotifyRiskRule QNR on QNR.QuestionnaireNotifyRiskRuleID=NR.QuestionnaireNotifyRiskRuleID where NL.NotificationResolutionStatusID=" + statusID + " and QNR.IsRemoved=0";

                notificationLogDTO = ExecuteSqlQuery(query, x => new RiskNotificationsListDTO
                {
                    NotificationLogID = x["NotificationLogID"] == DBNull.Value ? 0 : (int)x["NotificationLogID"],
                    NotificationDate = (DateTime)(x["NotificationDate"] == DBNull.Value ? null : (DateTime?)x["NotificationDate"]),
                    PersonID = x["PersonID"] == DBNull.Value ? 0 : (long)x["PersonID"],
                    NotificationTypeID = x["NotificationTypeID"] == DBNull.Value ? 0 : (int)x["NotificationTypeID"],
                    FKeyValue = x["FKeyValue"] == DBNull.Value ? 0 : (int)x["FKeyValue"],
                    NotificationData = x["NotificationData"] == DBNull.Value ? null : (string)x["NotificationData"],
                    NotificationResolutionStatusID = x["NotificationResolutionStatusID"] == DBNull.Value ? 0 : (int)x["NotificationResolutionStatusID"],
                    StatusDate = x["StatusDate"] == DBNull.Value ? null : (DateTime?)x["StatusDate"],
                    IsRemoved = (bool)x["IsRemoved"],
                    UpdateUserID = x["UpdateUserID"] == DBNull.Value ? 0 : (int)x["UpdateUserID"],
                    QuestionnaireNotifyRiskRuleID = x["QuestionnaireNotifyRiskRuleID"] == DBNull.Value ? 0 : (int)x["QuestionnaireNotifyRiskRuleID"],
                    NotifyRiskID = x["NotifyRiskID"] == DBNull.Value ? 0 : (int)x["NotifyRiskID"],
                    Details = x["Details"] == DBNull.Value ? string.Empty : (string)x["Details"],
                    HelperName = x["HelperName"] == DBNull.Value ? string.Empty : (string)x["HelperName"],
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return notificationLogDTO;
        }

        /// <summary>
        /// Update Notification Log By Id
        /// </summary>
        /// <param name="NotificationLog"></param>
        /// <returns></returns>
        public List<NotificationLog> UpdateNotificationLog(List<NotificationLog> NotificationLog)
        {
            try
            {
                var res = this.UpdateBulkAsync(NotificationLog);
                res.Wait();
                return NotificationLog;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get Past Notifications
        /// </summary>
        /// <param name="notificationLogSearchDTO"></param>
        /// <param name="queryBuilderDTO"></param>
        /// <param name="personID"></param>
        /// <returns>Tuple<List<NotificationLogDTO>, int></returns>
        public Tuple<List<NotificationLogDTO>, int> GetPastNotifications(NotificationLogSearchDTO notificationLogSearchDTO, DynamicQueryBuilderDTO queryBuilderDTO, long personID, SharedDetailsDTO sharedIDs, long personAgencyID, SharedDetailsDTO helperColbIDs)
        {
            try
            {
                List<NotificationLogDTO> notificationLogDTO = new List<NotificationLogDTO>();
                var query = string.Empty;
                string sharedQuestionnaires = "0";
                var notifiationResolutionStatus = this.notifiationResolutionStatusRepository.GetNotificationStatusForResolved();
                var notification = this.notificationTypeRepository.GetAllNotificationType();
                int reminderTypeID = notification.Where(x => x.Name == PCISEnum.NotificationType.Reminder).ToList()[0].NotificationTypeID;
                var helperColbQueryCondition = string.Empty;
                if (sharedIDs != null && sharedIDs.SharedQuestionnaireIDs != null)
                {
                    sharedQuestionnaires = sharedIDs.SharedQuestionnaireIDs;
                }
                if (!string.IsNullOrWhiteSpace(helperColbIDs.SharedCollaborationIDs))
                {
                    query = GetHelperCollaborationNotificationQuery(notificationLogSearchDTO, personID, notifiationResolutionStatus.NotificationResolutionStatusID, reminderTypeID);
                }
                else
                {
                    query = $@";WITH NotificationLogIdList AS (select NH.NotificationLogID
                            from NotificationLog NH WITH (NOLOCK)
                            join Person P WITH (NOLOCK) on P.PersonID=NH.PersonID
                            join info.NotificationType NT on NT.NotificationTypeID=NH.NotificationTypeID  
                            WHERE NH.IsRemoved=0 AND NH.NotificationResolutionStatusID={notifiationResolutionStatus.NotificationResolutionStatusID} AND P.AgencyID={personAgencyID} AND NH.NotificationTypeID<>{reminderTypeID} AND P.PersonID={personID}
                            union all
                            SELECT NotificationLogID FROM(
                            SELECT 
                            ROW_NUMBER() OVER(PARTITIon BY PersonQuestionnaireScheduleID ORDER BY NR.NotifyDate) AS RowNumber, NH.NotificationLogID
                            from NotificationLog NH WITH (NOLOCK)
                            join Person P WITH (NOLOCK) 
                            on P.PersonID=NH.PersonID
                            join Notifyreminder NR on NR.NotifyReminderID=NH.FKeyValue
                            WHERE NH.IsRemoved=0 AND NH.NotificationtypeId={reminderTypeID}  AND NH.NotificationResolutionStatusID= {notifiationResolutionStatus.NotificationResolutionStatusID} AND P.AgencyID={personAgencyID} AND P.PersonID={personID} AND nR.IsLogAdded=1
                            ) as A 
                            WHERE A.RowNumber = 1)
                            select COUNT(*) OVER() AS TotalCount,* from (select NH.NotificationLogID,NH.NotificationDate,NH.PersonID,NH.NotificationTypeID,NH.AssessmentID AS FKeyValue,NH.NotificationData,NH.NotificationResolutionStatusID,NH.StatusDate, 
                            P.FirstName+ COALESCE(CASE P.MiddleName WHEN '' THEN '' ELSE ' '+P.MiddleName END, ' '+P.MiddleName, '') + COALESCE(CASE P.LastName WHEN '' THEN '' ELSE ' '+P.LastName END, ' '+P.LastName, '') as PersonName, 
                            CASE NT.Name 
                                WHEN 'AssessmentSubmit'  THEN 'Submitted'
                                WHEN 'EmailAssessmentSubmit'  THEN 'Email Assessment Submit'
                                WHEN 'AssessmentApproved'  THEN 'Approved'
                                WHEN 'AssessmentRejected'  THEN 'Returned'
                            ELSE NT.Name
                            END as NotificationType,
                            P.PersonIndex, 
                            NH.Details, --NR.[Name] as Status,
							NH.AssessmentID, NH.QuestionnaireID,
                            ISNULL(NH.HelperName, '') AS HelperName 
                            from NotificationLog NH WITH (NOLOCK)
							JOIN NotificationLogIdList NL on NL.NotificationLogID = NH.NotificationLogID 
		                    join Person P WITH (NOLOCK) on P.PersonID=NH.PersonID
		                    join info.NotificationType NT on NT.NotificationTypeID=NH.NotificationTypeID
                            WHERE 1=1
                            ) T WHERE 1=1 ";
                }
                query += queryBuilderDTO.WhereCondition + " AND ('" + sharedQuestionnaires + "' = '0' OR QuestionnaireID IN (" + sharedQuestionnaires + ")) " + queryBuilderDTO.OrderBy.Replace("DATE", "DATETIME") + queryBuilderDTO.Paginate;
                int? totalCount = 0;
                notificationLogDTO = ExecuteSqlQuery(query, x =>
                {
                    totalCount = x["TotalCount"] == DBNull.Value ? 0 : (int?)x["TotalCount"];
                    return new NotificationLogDTO
                    {
                        NotificationLogID = x["NotificationLogID"] == DBNull.Value ? 0 : (int)x["NotificationLogID"],
                        NotificationDate = (DateTime)(x["NotificationDate"] == DBNull.Value ? null : (DateTime?)x["NotificationDate"]),
                        PersonID = x["PersonID"] == DBNull.Value ? 0 : (long)x["PersonID"],
                        NotificationTypeID = x["NotificationTypeID"] == DBNull.Value ? 0 : (int)x["NotificationTypeID"],
                        FKeyValue = x["FKeyValue"] == DBNull.Value ? 0 : (int)x["FKeyValue"],
                        NotificationData = x["NotificationData"] == DBNull.Value ? null : (string)x["NotificationData"],
                        NotificationResolutionStatusID = x["NotificationResolutionStatusID"] == DBNull.Value ? 0 : (int)x["NotificationResolutionStatusID"],
                        StatusDate = x["StatusDate"] == DBNull.Value ? null : (DateTime?)x["StatusDate"],
                        PersonName = x["PersonName"] == DBNull.Value ? null : (string)x["PersonName"],
                        NotificationType = x["NotificationType"] == DBNull.Value ? null : (string)x["NotificationType"],
                        PersonIndex = x["PersonIndex"] == DBNull.Value ? Guid.Empty : (Guid)x["PersonIndex"],
                        Details = x["Details"] == DBNull.Value ? null : ((string)x["Details"]).Trim(),
                        Status = notifiationResolutionStatus.Name, //x["Status"] == DBNull.Value ? null : (string)x["Status"],
                        QuestionnaireID = x["QuestionnaireID"] == DBNull.Value ? 0 : (int)x["QuestionnaireID"],
                        AssessmentID = x["AssessmentID"] == DBNull.Value ? 0 : (int)x["AssessmentID"],
                        HelperName = x["HelperName"] == DBNull.Value ? string.Empty : (string)x["HelperName"]
                    };
                }, queryBuilderDTO.QueryParameterDTO);
                return Tuple.Create(notificationLogDTO, totalCount == null ? 0 : totalCount.Value);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string GetHelperCollaborationNotificationQuery(NotificationLogSearchDTO notificationLogSearchDTO, long personID, int notificationResolutionStatusID, int reminderTypeID)
        {
            var query = $@";WITH PersonInHelp AS ( 
                                    SELECT distinct personid
                                    FROM Person
                                     WHERE personid in ({personID}) AND AgencyID = {notificationLogSearchDTO.agencyID}
                                )
                               ,PersonInHelpColb AS (
		                        	SELECT DISTINCT PH.CollaborationID,         PH.PersonID,PC.EnrollDate,PC.EndDate,CQ.QuestionnaireID,PQ.PersonQuestionnaireID,CQ.Isremoved
                                        FROM Helper H
                                         JOIN PersonHelper  PH ON PH.HelperId = H.HelperID AND H.UserID = {notificationLogSearchDTO.UserID}
		                        		 JOIN PersonInHelp  P ON PH.PersonID = P.PersonID
		                        		 JOIN PersonCollaboration PC ON PC.CollaborationId = PH.CollaborationId 
		                        							       AND PC.PersonID = PH.PersonID				
		                        		 JOIN Collaboration C ON PC.CollaborationId = C.CollaborationId
                                         LEFT JOIN CollaborationQuestionnaire CQ on CQ.CollaborationID = PC.CollaborationID
		                        		 LEFT JOIN PersonQuestionnaire PQ ON PQ.PersonID = PC.PersonID 
		                        					AND PQ.QuestionnaireID = CQ.QuestionnaireID
                                         WHERE  H.AgencyID = {notificationLogSearchDTO.agencyID} AND PH.IsRemoved = 0 
                                                AND PC.IsRemoved = 0  AND C.IsRemoved = 0 
		                        				AND ISNULL(CQ.IsRemoved,0) = 0 AND ISNULL(PQ.Isremoved,0) = 0
		                        				AND PH.CollaborationID IS NOT NULL 
		                        		UNION 
		                        	SELECT '' AS CollaborationID, PH.PersonID , null AS EnrollDate,null AS EndDate,
                                        PQ.QuestionnaireID,PQ.PersonQuestionnaireID,0 AS  Isremoved
		                        	    FROM PersonInHelp P 
                                         JOIN PersonHelper  PH ON P.personid =PH.Personid
		                        		 JOIN PersonQuestionnaire PQ ON PH.Personid = PQ.Personid
		                        		 WHERE ISNULL(PQ.CollaborationID, 0) = 0 AND PH.IsRemoved = 0 AND PQ.IsRemoved = 0 
		                        						AND PQ.UpdateUserID =  {notificationLogSearchDTO.UserID}
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
                                          WHERE ar.Name IN('Initial','discharge') AND qw.QuestionnaireID IN(select distinct QuestionnaireID from    PersonInHelpColb)
                                   )
                             ,COLBQuestion AS(
                                         SELECT distinct PC.Personid, PHC.QuestionnaireID, wo_init.WindowOpenOffsetDays, wo_disc.WindowCloseOffsetDays ,
                                         DATEADD(DAY, 0 - ISNULL(wo_init.WindowOpenOffsetDays, 0), CAST(PC.CollaborationStartDate AS DATE)) AS      Enrolldate,DATEADD  (DAY,  ISNULL(wo_disc.WindowCloseOffsetDays, 0), ISNULL(PC.CollaborationEndDate, CAST(GETDATE()     AS DATE))) AS             Enddate
                                         FROM PersonInHelpColb PHC
                                         JOIN PersonColb PC ON PC.personid = PHC.personid
                                         JOIN(SELECT * FROM WindowOffsets WHERE Reason = 'Initial')wo_init ON wo_init.QuestionnaireID = PHC.QuestionnaireID
                                         JOIN(SELECT * FROM WindowOffsets WHERE Reason = 'Discharge')wo_disc ON wo_disc.QuestionnaireID = PHC.QuestionnaireID
                                	)
                             ,AllNotifications AS
                                  (  
                                      SELECT NH.NotificationLogID,NH.NotificationTypeID,NH.QuestionnaireID,NH.Details,
                                        NH.NotificationDate,NH.FKeyValue,NH.AssessmentID,NH.PersonID,NH.HelperName,
										NH.NotificationData,NH.NotificationResolutionStatusID,NH.StatusDate
                                       FROM NotificationLog NH
                                  	 JOIN PersonInHelp PH ON PH.PersonID = NH.PersonID 
                                  	 WHERE NH.NotificationResolutionStatusID = {notificationResolutionStatusID} AND NH.IsRemoved = 0
                                  )
                              ,NotificationLogIdList AS
				                (  
				                 SELECT
                                      NH.NotificationLogID, p.PersonID
                                          FROM
                                      COLBQuestion P 
                                      JOIN AllNotifications NH ON NH.PersonID = p.PersonID AND P.QuestionnaireID = NH.QuestionnaireID 
                                          AND NH.NotificationTypeID <> {reminderTypeID}
                                      JOIN Assessment A ON NH.AssessmentID = A.AssessmentID AND A.IsRemoved = 0
                                      JOIN PersonInHelpColb PQ ON A.PersonQuestionnaireID = PQ.PersonQuestionnaireID
                                      WHERE CAST(A.DateTaken AS DATE) BETWEEN P.Enrolldate AND P.Enddate
                                     UNION 
                               SELECT NotificationLogID, PersonID FROM(
                                     SELECT
                                          ROW_NUMBER() OVER(PARTITIon BY PersonQuestionnaireScheduleID ORDER BY NR.NotifyDate) AS RowNumber,
                                            NH.NotificationLogID,P.PersonID
                                      FROM COLBQuestion p
                                      join AllNotifications NH WITH(NOLOCK)
                                      on P.PersonID = NH.PersonID AND P.QuestionnaireID = NH.QuestionnaireID
                                      join Notifyreminder NR on NR.NotifyReminderID = NH.FKeyValue
                                      WHERE NH.NotificationtypeId = {reminderTypeID}
                                      AND nR.IsLogAdded  = 1 AND NH.NotificationDate BETWEEN P.Enrolldate AND P.Enddate
                                ) as A  WHERE A.RowNumber = 1 )
                            select COUNT(*) OVER() AS TotalCount,* from (select NH.NotificationLogID,NH.NotificationDate,NH.PersonID,NH.NotificationTypeID,NH.AssessmentID AS FKeyValue,NH.NotificationData,NH.NotificationResolutionStatusID,NH.StatusDate, 
                            P.FirstName+ COALESCE(CASE P.MiddleName WHEN '' THEN '' ELSE ' '+P.MiddleName END, ' '+P.MiddleName, '') + COALESCE(CASE P.LastName WHEN '' THEN '' ELSE ' '+P.LastName END, ' '+P.LastName, '') as PersonName, 
                            CASE NT.Name 
                                WHEN 'AssessmentSubmit'  THEN 'Submitted'
                                WHEN 'EmailAssessmentSubmit'  THEN 'Email Assessment Submit'
                                WHEN 'AssessmentApproved'  THEN 'Approved'
                                WHEN 'AssessmentRejected'  THEN 'Returned'
                            ELSE NT.Name
                            END as NotificationType,
                            P.PersonIndex, 
                            NH.Details, --NR.[Name] as Status,
							NH.AssessmentID, NH.QuestionnaireID,
                            ISNULL(NH.HelperName, '') AS HelperName 
                            from AllNotifications NH WITH (NOLOCK)
							JOIN NotificationLogIdList NL on NL.NotificationLogID = NH.NotificationLogID 
		                    join Person P WITH (NOLOCK) on P.PersonID=NH.PersonID
		                    join info.NotificationType NT on NT.NotificationTypeID=NH.NotificationTypeID
                            ) T WHERE 1=1 ";
            return query;
        }

        private string GetReminderQueryForHelperNotification(long personID, SharedDetailsDTO helperColbIDs)
        {
            try
            {
                var query = string.Empty;
                if (!string.IsNullOrEmpty(helperColbIDs.SharedCollaborationIDs))
                {
                    query = $@",PersonCollab AS (
                                     SELECT MIN(CAST(PC.EnrollDate AS DATE)) AS CollaborationStartDate, CQ.QuestionnaireID,PC.CollaborationID,
                                               MAX(ISNULL(PC.EndDate, CAST(GETDATE() AS DATE))) AS CollaborationEndDate, PC.PersonID
                                          FROM PersonCollaboration PC
                                          JOIN Collaboration C ON PC.CollaborationId = C.CollaborationId
                                          LEFT JOIN CollaborationQuestionnaire CQ on CQ.CollaborationID = PC.CollaborationID
                                          WHERE PC.IsRemoved = 0 AND PC.PersonID = {personID} 
                                           AND(PC.CollaborationID IN ({helperColbIDs.SharedCollaborationIDs})) 
                                           GROUP BY CQ.QuestionnaireID,PC.PersonID,PC.CollaborationID
									   )
						         ,WindowOffsets AS
                                        (
                                             SELECT
                                                 qw.QuestionnaireID, qw.WindowOpenOffsetDays, qw.WindowCloseOffsetDays, ar.Name as Reason
                                             
                                             FROM
                                             QuestionnaireWindow qw
                                             JOIN info.AssessmentReason ar ON ar.AssessmentReasonID = qw.AssessmentReasonID
                                             WHERE ar.Name IN('Initial','discharge') AND qw.QuestionnaireID IN ({helperColbIDs.SharedQuestionnaireIDs})
						                )
						        ,COLBQuestion AS
                                   (
                                     SELECT PC.*, wo_init.WindowOpenOffsetDays,wo_disc.WindowCloseOffsetDays , DATEADD(DAY, 0 - ISNULL (wo_init.WindowOpenOffsetDays, 0), CAST(PC.CollaborationStartDate AS DATE)) AS Enrolldate, DATEADD(DAY, ISNULL   (wo_disc.WindowCloseOffsetDays, 0), ISNULL(PC.CollaborationEndDate, CAST(GETDATE() AS DATE))) AS Enddate
                                        FROM PersonCollab PC
                                      JOIN(SELECT * FROM WindowOffsets WHERE Reason = 'Initial')wo_init ON PC.QuestionnaireID = wo_init.QuestionnaireID
                                      JOIN(SELECT * FROM WindowOffsets WHERE Reason = 'Discharge')wo_disc ON wo_disc.QuestionnaireID = PC.QuestionnaireID
						    	   )";
                }
                return query;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Get Present Notifications
        /// </summary>
        /// <param name="notificationLogSearchDTO"></param>
        /// <param name="queryBuilderDTO"></param>
        /// <param name="personID"></param>
        /// <returns>Tuple<List<NotificationLogDTO>, int></returns>
        public Tuple<List<NotificationLogDTO>, int> GetPresentNotifications(NotificationLogSearchDTO notificationLogSearchDTO, DynamicQueryBuilderDTO queryBuilderDTO, long personID, SharedDetailsDTO sharedIDs, long personAgencyId, SharedDetailsDTO helperColbIDs)
        {
            try
            {
                List<NotificationLogDTO> notificationLogDTO = new List<NotificationLogDTO>();
                var query = string.Empty;
                string sharedQuestionnaires = "0";
                var notifiationResolutionStatus = this.notifiationResolutionStatusRepository.GetNotificationStatusForUnResolved();
                var notification = this.notificationTypeRepository.GetAllNotificationType();
                int reminderTypeID = notification.Where(x => x.Name == PCISEnum.NotificationType.Reminder).ToList()[0].NotificationTypeID;
                var helperColbQueryCondition = string.Empty;
                if (sharedIDs != null && sharedIDs.SharedQuestionnaireIDs != null)
                {
                    sharedQuestionnaires = sharedIDs.SharedQuestionnaireIDs;
                }
                if (!string.IsNullOrWhiteSpace(helperColbIDs.SharedCollaborationIDs))
                {
                    query = GetHelperCollaborationNotificationQuery(notificationLogSearchDTO, personID, notifiationResolutionStatus.NotificationResolutionStatusID, reminderTypeID);
                }
                else
                {
                    query = $@";WITH NotificationLogIdList AS (select NH.NotificationLogID
                            from NotificationLog NH WITH (NOLOCK)
                            join Person P WITH (NOLOCK) on P.PersonID=NH.PersonID
                            join info.NotificationType NT on NT.NotificationTypeID=NH.NotificationTypeID  
                            WHERE NH.IsRemoved=0 AND NH.NotificationResolutionStatusID={notifiationResolutionStatus.NotificationResolutionStatusID} AND P.AgencyID={personAgencyId} AND NH.NotificationTypeID<>{reminderTypeID} AND P.PersonID={personID}
                            union all
                            SELECT NotificationLogID FROM(
                            SELECT 
                            ROW_NUMBER() OVER(PARTITIon BY PersonQuestionnaireScheduleID ORDER BY NR.NotifyDate) AS RowNumber, NH.NotificationLogID
                            from NotificationLog NH WITH (NOLOCK)
                            join Person P WITH (NOLOCK) 
                            on P.PersonID=NH.PersonID
                            join Notifyreminder NR on NR.NotifyReminderID=NH.FKeyValue
                            WHERE NH.IsRemoved=0 AND NH.NotificationtypeId={reminderTypeID}  AND NH.NotificationResolutionStatusID= {notifiationResolutionStatus.NotificationResolutionStatusID} AND P.AgencyID={personAgencyId} AND P.PersonID={personID} AND nR.IsLogAdded=1
                            ) as A 
                            WHERE A.RowNumber = 1)                           
                            select COUNT(*) OVER() AS TotalCount,* from (select NH.NotificationLogID,NH.NotificationDate,NH.PersonID,NH.NotificationTypeID,NH.AssessmentID AS FKeyValue,NH.NotificationData,NH.NotificationResolutionStatusID,NH.StatusDate, 
                            P.FirstName+ COALESCE(CASE P.MiddleName WHEN '' THEN '' ELSE ' '+P.MiddleName END, ' '+P.MiddleName, '') + COALESCE(CASE P.LastName WHEN '' THEN '' ELSE ' '+P.LastName END, ' '+P.LastName, '') as PersonName, 
                            CASE NT.Name 
                                WHEN 'AssessmentSubmit'  THEN 'Submitted'
                                WHEN 'EmailAssessmentSubmit'  THEN 'Email Assessment Submit'
                                WHEN 'AssessmentApproved'  THEN 'Approved'
                                WHEN 'AssessmentRejected'  THEN 'Returned'
                            ELSE NT.Name
                            END as NotificationType,
                            P.PersonIndex, 
                            NH.Details, --NR.[Name] as Status,
							NH.AssessmentID, NH.QuestionnaireID,
                            ISNULL(NH.HelperName, '') AS HelperName 
                            from NotificationLog NH WITH (NOLOCK)
							JOIN NotificationLogIdList NL on NL.NotificationLogID = NH.NotificationLogID 
		                    join Person P WITH (NOLOCK) on P.PersonID=NH.PersonID
		                    join info.NotificationType NT on NT.NotificationTypeID=NH.NotificationTypeID
                            WHERE 1=1 {helperColbQueryCondition}
                            ) T WHERE 1=1";
                }
                query += queryBuilderDTO.WhereCondition + " AND ('" + sharedQuestionnaires + "' = '0' OR QuestionnaireID IN (" + sharedQuestionnaires + ")) " + queryBuilderDTO.OrderBy.Replace("DATE", "DATETIME") + queryBuilderDTO.Paginate;
                int? totalCount = 0;
                notificationLogDTO = ExecuteSqlQuery(query, x =>
                {
                    totalCount = x["TotalCount"] == DBNull.Value ? 0 : (int?)x["TotalCount"];
                    return new NotificationLogDTO
                    {
                        NotificationLogID = x["NotificationLogID"] == DBNull.Value ? 0 : (int)x["NotificationLogID"],
                        NotificationDate = (DateTime)(x["NotificationDate"] == DBNull.Value ? null : (DateTime?)x["NotificationDate"]),
                        PersonID = x["PersonID"] == DBNull.Value ? 0 : (long)x["PersonID"],
                        NotificationTypeID = x["NotificationTypeID"] == DBNull.Value ? 0 : (int)x["NotificationTypeID"],
                        FKeyValue = x["FKeyValue"] == DBNull.Value ? 0 : (int)x["FKeyValue"],
                        NotificationData = x["NotificationData"] == DBNull.Value ? null : (string)x["NotificationData"],
                        NotificationResolutionStatusID = x["NotificationResolutionStatusID"] == DBNull.Value ? 0 : (int)x["NotificationResolutionStatusID"],
                        StatusDate = x["StatusDate"] == DBNull.Value ? null : (DateTime?)x["StatusDate"],
                        PersonName = x["PersonName"] == DBNull.Value ? null : (string)x["PersonName"],
                        NotificationType = x["NotificationType"] == DBNull.Value ? null : (string)x["NotificationType"],
                        PersonIndex = x["PersonIndex"] == DBNull.Value ? Guid.Empty : (Guid)x["PersonIndex"],
                        Details = x["Details"] == DBNull.Value ? null : ((string)x["Details"]).Trim(),
                        Status = notifiationResolutionStatus.Name, //x["Status"] == DBNull.Value ? null : (string)x["Status"],
                        QuestionnaireID = x["QuestionnaireID"] == DBNull.Value ? 0 : (int)x["QuestionnaireID"],
                        AssessmentID = x["AssessmentID"] == DBNull.Value ? 0 : (int)x["AssessmentID"],
                        HelperName = x["HelperName"] == DBNull.Value ? string.Empty : (string)x["HelperName"]
                    };
                }, queryBuilderDTO.QueryParameterDTO);

                return Tuple.Create(notificationLogDTO, totalCount == null ? 0 : totalCount.Value);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// UpdateBulkNotificationLog.
        /// </summary>
        /// <param name="notificationLog">notificationLog.</param>
        /// <returns>NotificationLog</returns>
        public List<NotificationLog> UpdateBulkNotificationLog(List<NotificationLog> notificationLogList)
        {
            try
            {
                if (notificationLogList.Count > 0)
                {
                    var result = this.UpdateBulkAsync(notificationLogList);
                    result.Wait();
                }
                return notificationLogList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Tuple<List<NotificationLogDTO>, int> GetDashboardNotificationLogList(NotificationLogSearchDTO notificationLogSearchDTO, DynamicQueryBuilderDTO queryBuilderDTO)
        {
            try
            {
                List<NotificationLogDTO> notificationLogDTO = new List<NotificationLogDTO>();
                string query = string.Empty;
                var countquery = string.Empty;
                NotificationResolutionStatusDTO notifiationResolutionStatus = this.notifiationResolutionStatusRepository.GetNotificationStatusForUnResolved();
                List<NotificationType> notification = this.notificationTypeRepository.GetAllNotificationType();
                int reminderTypeID = notification.Where(x => x.Name == PCISEnum.NotificationType.Reminder).ToList()[0].NotificationTypeID;
                int alertTypeID = notification.Where(x => x.Name == PCISEnum.NotificationType.Alert).ToList()[0].NotificationTypeID;
                int assessmentApproveTypeID = notification.Where(x => x.Name == PCISEnum.AssessmentNotificationType.Approve).ToList()[0].NotificationTypeID;
                int assessmentRejectTypeID = notification.Where(x => x.Name == PCISEnum.AssessmentNotificationType.Reject).ToList()[0].NotificationTypeID;
                int emailAssessmentSubmitTypeID = notification.Where(x => x.Name == PCISEnum.AssessmentNotificationType.EmailSubmit).ToList()[0].NotificationTypeID;
                int assessmentSubmitTypeID = notification.Where(x => x.Name == PCISEnum.AssessmentNotificationType.Submit).ToList()[0].NotificationTypeID;
                var helperColbQueryCondition = string.Empty;
                if (notificationLogSearchDTO.role != PCISEnum.Roles.SuperAdmin)
                {
                    var personIdList = GetHelperPersonInCollaborationDetails(notifiationResolutionStatus.NotificationResolutionStatusID, notificationLogSearchDTO.UserID, notificationLogSearchDTO.agencyID);
                    if (personIdList.Count > 0)
                    {
                        string personIDs = string.Join(",", personIdList.ToArray());
                        List<string> notifcatnsInColbratn = GetHelperNotificationsInCollaboration(notificationLogSearchDTO, personIDs, notifiationResolutionStatus.NotificationResolutionStatusID, reminderTypeID);
                        string notifcatnsInColbratnIds = notifcatnsInColbratn.Count > 0 ? string.Join(",", notifcatnsInColbratn.ToArray()) : "0";
                        helperColbQueryCondition = $@"AND (P.PersonID NOT IN ({personIDs}) OR (P.PersonID IN ({personIDs}) AND NH.NotificationLogID IN ({notifcatnsInColbratnIds})))";
                    }
                }
                if (notificationLogSearchDTO.isSameAsLoggedInUser)
                {
                    query = @$";WITH HelperList AS
                            (
                                SELECT
                                    *
                                FROM
                                Helper h
                                WHERE h.AgencyID = {notificationLogSearchDTO.agencyID} AND h.IsRemoved = 0 AND h.HelperID = {notificationLogSearchDTO.helperID}
                            )
                            ,PersonHelped AS
                            (
	                            SELECT DISTINCT
		                            p.PersonID
	                            FROM
	                            Person P
	                            JOIN PersonHelper ph ON ph.PersonID = p.PersonID
	                            JOIN HelperList h ON h.HelperID = ph.helperID
	                            WHERE P.IsRemoved = 0 AND p.AgencyID = {notificationLogSearchDTO.agencyID} AND ph.IsRemoved = 0
	                            AND ph.StartDate <= CAST(GETDATE() AS DATE) AND ISNULL(ph.EndDate, CAST(GETDATE() AS DATE)) >= CAST(GETDATE() AS DATE)
                            )                            
                            ,RecCountAlert As
                            (
	                            SELECT
		                            NH.NotificationLogID, p.PersonID
	                            FROM
	                            Person p
	                            JOIN
	                            (
		                            SELECT PersonID FROM PersonHelped
	                            )
	                            PersonToList ON p.PersonID = PersonToList.PersonID
	                            JOIN NotificationLog NH ON NH.PersonID = p.PersonID AND NH.IsRemoved = 0 AND NH.NotificationResolutionStatusID = {notifiationResolutionStatus.NotificationResolutionStatusID} AND NH.NotificationTypeID IN ({alertTypeID},{assessmentApproveTypeID},{assessmentRejectTypeID})
								union
								SELECT NotificationLogID, PersonID FROM(
									SELECT 
									ROW_NUMBER() OVER(PARTITIon BY PersonQuestionnaireScheduleID ORDER BY NR.NotifyDate) AS RowNumber, NH.NotificationLogID,P.PersonID
									from 
									Person p
									JOIN
									(
										SELECT PersonID FROM PersonHelped
									)
									PersonToList ON p.PersonID = PersonToList.PersonID
									join NotificationLog NH WITH (NOLOCK) 
									on P.PersonID=NH.PersonID
									join Notifyreminder NR on NR.NotifyReminderID=NH.FKeyValue
									WHERE NH.IsRemoved=0 AND  NH.NotificationtypeId={reminderTypeID} AND NH.NotificationResolutionStatusID= {notifiationResolutionStatus.NotificationResolutionStatusID} AND  nR.IsLogAdded=1
									) as A 
									WHERE A.RowNumber = 1
                            )                            
                            ,RecCount AS
                            (
	                            SELECT * FROM RecCountAlert
                            )                            
                            SELECT TOP 5
                                    NH.NotificationLogID,
                                    NH.NotificationDate,
                                    NH.PersonID,
                                    NH.NotificationTypeID,
                                    NH.AssessmentID AS FKeyValue,
                                    NH.NotificationData,
                                    NH.NotificationResolutionStatusID,
                                    NH.StatusDate,
                                    P.FirstName+ COALESCE(CASE P.MiddleName WHEN '' THEN '' ELSE ' '+P.MiddleName END, ' '+P.MiddleName, '') 
		                            + COALESCE(CASE P.LastName WHEN '' THEN '' ELSE ' '+P.LastName END, ' '+P.LastName, '') as PersonName,
                                    CASE NT.Name 
                                        WHEN 'AssessmentSubmit'  THEN 'Submitted'
                                        WHEN 'EmailAssessmentSubmit'  THEN 'Email Assessment Submit'
                                        WHEN 'AssessmentApproved'  THEN 'Approved'
                                        WHEN 'AssessmentRejected'  THEN 'Returned'
                                    ELSE NT.Name
                                    END as NotificationType,
                                    P.PersonIndex,
                                    NH.Details,
                                   NH.QuestionnaireID,
                                    NH.AssessmentID
                                FROM
                                Person p
                                JOIN RecCount rec on p.PersonID=rec.PersonID
                                JOIN NotificationLog NH WITH (NOLOCK) ON NH.NotificationLogID=rec.NotificationLogID 
                                JOIN info.NotificationType NT ON NT.NotificationTypeID = NH.NotificationTypeID
                                LEFT JOIN PersonHelped PH ON PH.PersonID = NH.PersonID where  P.IsRemoved = 0 and P.IsActive = 1 {helperColbQueryCondition}";

                    countquery = @$";WITH HelperList AS
                            (
                                SELECT
                                    *
                                FROM
                                Helper h
                                WHERE h.AgencyID = {notificationLogSearchDTO.agencyID} AND h.IsRemoved = 0 AND h.HelperID = {notificationLogSearchDTO.helperID}
                            )
                            ,PersonHelped AS
                            (
	                            SELECT DISTINCT
		                            p.PersonID
	                            FROM
	                            Person P
	                            JOIN PersonHelper ph ON ph.PersonID = p.PersonID
	                            JOIN HelperList h ON h.HelperID = ph.helperID
	                            WHERE P.IsRemoved = 0 AND p.AgencyID = {notificationLogSearchDTO.agencyID} AND ph.IsRemoved = 0
	                            AND ph.StartDate <= CAST(GETDATE() AS DATE) AND ISNULL(ph.EndDate, CAST(GETDATE() AS DATE)) >= CAST(GETDATE() AS DATE)
                            )
                            ,RecCountAlert As
                            (
	                            SELECT
		                            NH.NotificationLogID, p.PersonID
	                            FROM
	                            Person p
	                            JOIN
	                            (
		                            SELECT PersonID FROM PersonHelped
	                            )
	                            PersonToList ON p.PersonID = PersonToList.PersonID
	                            JOIN NotificationLog NH ON NH.PersonID = p.PersonID AND NH.IsRemoved = 0 AND NH.NotificationResolutionStatusID = {notifiationResolutionStatus.NotificationResolutionStatusID} AND NH.NotificationTypeID IN ({alertTypeID},{assessmentApproveTypeID},{assessmentRejectTypeID})
								union
								SELECT NotificationLogID, PersonID FROM(
									SELECT 
									ROW_NUMBER() OVER(PARTITIon BY PersonQuestionnaireScheduleID ORDER BY NR.NotifyDate) AS RowNumber, NH.NotificationLogID,P.PersonID
									from 
									Person p
									JOIN
									(
										SELECT PersonID FROM PersonHelped
									)
									PersonToList ON p.PersonID = PersonToList.PersonID
									join NotificationLog NH WITH (NOLOCK) 
									on P.PersonID=NH.PersonID									
									join Notifyreminder NR on NR.NotifyReminderID=NH.FKeyValue
									WHERE NH.IsRemoved=0 AND  NH.NotificationtypeId={reminderTypeID} AND NH.NotificationResolutionStatusID= {notifiationResolutionStatus.NotificationResolutionStatusID} AND  nR.IsLogAdded=1
									) as A 
									WHERE A.RowNumber = 1
                            )
                            ,RecCount AS
                            (
	                            SELECT * FROM RecCountAlert
                            )
                            SELECT top 1
                                    Count(1) AS TotalCount
                                FROM
                                Person p
                                JOIN RecCount rec on p.PersonID=rec.PersonID
                                JOIN NotificationLog NH WITH (NOLOCK) ON NH.NotificationLogID=rec.NotificationLogID 
                                JOIN info.NotificationType NT ON NT.NotificationTypeID = NH.NotificationTypeID
                                LEFT JOIN PersonHelped PH ON PH.PersonID = NH.PersonID where  P.IsRemoved = 0 and P.IsActive = 1 {helperColbQueryCondition}";
                }
                else
                {
                    if (notificationLogSearchDTO.role == PCISEnum.Roles.SuperAdmin)
                    {
                        query = $@"select top 5
		                                NT.[Name],
		                                NH.NotificationLogID
		                                ,NH.NotificationDate
		                                ,NH.PersonID,NH.NotificationTypeID,
		                                NH.AssessmentID as FKeyValue,
		                                NH.NotificationData,NH.NotificationResolutionStatusID,NH.StatusDate, 
		                                P.FirstName+ COALESCE(CASE P.MiddleName WHEN '' THEN '' ELSE ' '+P.MiddleName END, ' '+P.MiddleName, '') + COALESCE(CASE P.LastName WHEN '' THEN '' ELSE ' '+P.LastName END, ' '+P.LastName, '') as PersonName, 
		                                CASE NT.Name 
		                                WHEN 'AssessmentSubmit'  THEN 'Submitted'
		                                WHEN 'EmailAssessmentSubmit'  THEN 'Email Assessment Submit'
		                                WHEN 'AssessmentApproved'  THEN 'Approved'
		                                WHEN 'AssessmentRejected'  THEN 'Returned'
		                                ELSE NT.Name
		                                END as NotificationType,
		                                P.PersonIndex,
		                                NH.Details, 
		                                NH.QuestionnaireID,
		                                NH.AssessmentID  as AssessmentID                              
		                                from NotificationLog NH  WITH (NOLOCK)
		                                join Person P WITH (NOLOCK) on P.PersonID=NH.PersonID AND P.AgencyID= {notificationLogSearchDTO.agencyID} AND  NH.IsRemoved=0 AND NH.NotificationResolutionStatusID= {notifiationResolutionStatus.NotificationResolutionStatusID} 
		                                join info.NotificationType NT WITH (NOLOCK) on NT.NotificationTypeID=NH.NotificationTypeID 									
                         WHERE  NH.[NotificationTypeID] <> {reminderTypeID} ";

                        countquery = $@"select top 1
		                               COUNT(1) AS TotalCount                           
		                                from NotificationLog NH
		                                join Person P  on P.PersonID=NH.PersonID AND P.AgencyID= {notificationLogSearchDTO.agencyID}  
		                                join info.NotificationType NT on NT.NotificationTypeID=NH.NotificationTypeID 									   
                            WHERE  NH.IsRemoved=0 AND NH.NotificationResolutionStatusID= {notifiationResolutionStatus.NotificationResolutionStatusID}  AND P.AgencyID= {notificationLogSearchDTO.agencyID} AND NH.[NotificationTypeID] <> {reminderTypeID}";
                    }
                    else if (notificationLogSearchDTO.role == PCISEnum.Roles.OrgAdminRO || notificationLogSearchDTO.role == PCISEnum.Roles.OrgAdminRW || notificationLogSearchDTO.role == PCISEnum.Roles.OrgAdmin)
                    {
                        query = $@"; WITH PersonHelped AS
			                                        (
				                                        SELECT DISTINCT
					                                        p.PersonID, p.AgencyID
				                                        FROM
				                                        Person P
				                                        JOIN PersonHelper ph ON ph.PersonID = p.PersonID
				                                        JOIN Helper h ON h.HelperID = ph.helperID
				                                        WHERE P.IsRemoved = 0 AND p.AgencyID = {notificationLogSearchDTO.agencyID} AND ph.IsRemoved = 0 AND h.AgencyID = {notificationLogSearchDTO.agencyID} AND h.IsRemoved = 0 AND h.HelperID = {notificationLogSearchDTO.helperID}
				                                        AND ph.StartDate <= CAST(GETDATE() AS DATE) AND ISNULL(ph.EndDate, CAST(GETDATE() AS DATE)) >= CAST(GETDATE() AS DATE)
			                                        ),   
                                        tempNotifications as (
		                                        select
		                                        NT.[Name],
		                                        NH.NotificationLogID
		                                        ,NH.NotificationDate
		                                        ,NH.PersonID,NH.NotificationTypeID,
		                                        NH.AssessmentID as FKeyValue,
		                                        NH.NotificationData,NH.NotificationResolutionStatusID,NH.StatusDate, 
		                                        P.FirstName+ COALESCE(CASE P.MiddleName WHEN '' THEN '' ELSE ' '+P.MiddleName END, ' '+P.MiddleName, '') + COALESCE(CASE P.LastName WHEN '' THEN '' ELSE ' '+P.LastName END, ' '+P.LastName, '') as PersonName, 
		                                        CASE NT.Name 
		                                        WHEN 'AssessmentSubmit'  THEN 'Submitted'
		                                        WHEN 'EmailAssessmentSubmit'  THEN 'Email Assessment Submit'
		                                        WHEN 'AssessmentApproved'  THEN 'Approved'
		                                        WHEN 'AssessmentRejected'  THEN 'Returned'
		                                        ELSE NT.Name
		                                        END as NotificationType,
		                                        P.PersonIndex,
		                                        NH.Details, 
		                                        NH.QuestionnaireID,
		                                        NH.AssessmentID  as AssessmentID                              
		                                        from NotificationLog NH WITH (NOLOCK)
		                                        join Person P WITH (NOLOCK) on P.PersonID=NH.PersonID
		                                        join info.NotificationType NT on NT.NotificationTypeID=NH.NotificationTypeID  
		                                        LEFT JOIN PersonHelped PH on NH.PersonID = PH.PersonID  and  PH.AgencyID= {notificationLogSearchDTO.agencyID}
		                                        WHERE NH.IsRemoved=0 AND NH.NotificationResolutionStatusID={notifiationResolutionStatus.NotificationResolutionStatusID} AND P.AgencyID= {notificationLogSearchDTO.agencyID} AND P.IsRemoved = 0  {helperColbQueryCondition}
                                        )
	                                        select top 5 * 
	                                        from 
	                                        (
		                                        select 
		                                        temp.NotificationLogID
		                                        ,temp.NotificationDate
		                                        ,temp.PersonID,temp.NotificationTypeID,
		                                        temp.FKeyValue,
		                                        temp.NotificationData,temp.NotificationResolutionStatusID,temp.StatusDate, 
		                                        temp.PersonName, 
		                                        temp.NotificationType,
		                                        temp.PersonIndex,
		                                        temp.Details, 
		                                        temp.QuestionnaireID,
		                                        temp.AssessmentID                              
		                                        from tempNotifications temp
		                                        LEFT JOIN PersonHelped PH on temp.PersonID = PH.PersonID  and  PH.AgencyID= {notificationLogSearchDTO.agencyID}
		                                        WHERE PH.PersonID = temp.PersonID
                                        union
		                                        select
		                                        temp.NotificationLogID
		                                        ,temp.NotificationDate
		                                        ,temp.PersonID,temp.NotificationTypeID,
		                                        temp.FKeyValue,
		                                        temp.NotificationData,temp.NotificationResolutionStatusID,temp.StatusDate, 
		                                        temp.PersonName, 
		                                        temp.NotificationType,
		                                        temp.PersonIndex,
		                                        temp.Details, 
		                                        temp.QuestionnaireID,
		                                        temp.AssessmentID                              
		                                        from tempNotifications temp
		                                        WHERE temp.[NotificationTypeID] <> {reminderTypeID}
		                                        ) T 
	                                        WHERE 1=1 ";
                        countquery = $@"; WITH PersonHelped AS
			                                        (
				                                        SELECT DISTINCT
					                                        p.PersonID, p.AgencyID
				                                        FROM
				                                        Person P
				                                        JOIN PersonHelper ph ON ph.PersonID = p.PersonID
				                                        JOIN Helper h ON h.HelperID = ph.helperID
				                                        WHERE P.IsRemoved = 0 AND p.AgencyID = {notificationLogSearchDTO.agencyID} AND ph.IsRemoved = 0 AND h.AgencyID = {notificationLogSearchDTO.agencyID} AND h.IsRemoved = 0 AND h.HelperID = {notificationLogSearchDTO.helperID}
				                                        AND ph.StartDate <= CAST(GETDATE() AS DATE) AND ISNULL(ph.EndDate, CAST(GETDATE() AS DATE)) >= CAST(GETDATE() AS DATE)
			                                        ),   
                                        tempNotifications as (
		                                        select
		                                        NT.[Name],
		                                        NH.NotificationLogID,
                                                NH.[NotificationTypeID],
		                                        NH.PersonID												                             
		                                        from NotificationLog NH WITH (NOLOCK)
		                                        join Person P WITH (NOLOCK) on P.PersonID=NH.PersonID
		                                        join info.NotificationType NT on NT.NotificationTypeID=NH.NotificationTypeID  
		                                        join info.NotificationResolutionStatus NR on NR.NotificationResolutionStatusID = NH.NotificationResolutionStatusID
		                                        LEFT JOIN PersonHelped PH on NH.PersonID = PH.PersonID  and  PH.AgencyID= {notificationLogSearchDTO.agencyID}									            
		                                        WHERE NH.IsRemoved=0 AND NH.NotificationResolutionStatusID= {notifiationResolutionStatus.NotificationResolutionStatusID} AND P.AgencyID= {notificationLogSearchDTO.agencyID} AND P.IsRemoved = 0  {helperColbQueryCondition}
                                        )
	                                        select top 1 COUNT(*) OVER() AS TotalCount
	                                        from 
	                                        (
		                                        select 
												temp.NotificationLogID		                                                                    
		                                        from tempNotifications temp  
		                                        LEFT JOIN PersonHelped PH on temp.PersonID = PH.PersonID  and  PH.AgencyID= {notificationLogSearchDTO.agencyID}
		                                        WHERE PH.PersonID = temp.PersonID
                                        union
		                                        select temp.NotificationLogID		                                                                  
		                                        from tempNotifications temp  
		                                        WHERE temp.[NotificationTypeID] <> {reminderTypeID}
		                                        ) T
	                                        WHERE 1=1";
                    }
                    else if (notificationLogSearchDTO.role == PCISEnum.Roles.Supervisor)
                    {
                        query = @$";WITH HelperList AS
                            (
                                SELECT
                                    *
                                FROM
                                Helper h
                                WHERE h.AgencyID = {notificationLogSearchDTO.agencyID} AND h.IsRemoved = 0 AND h.HelperID = {notificationLogSearchDTO.helperID}
                            )
                            ,PersonHelped AS
                            (
	                            SELECT DISTINCT
		                            p.PersonID
	                            FROM
	                            Person P
	                            JOIN PersonHelper ph ON ph.PersonID = p.PersonID
	                            JOIN HelperList h ON h.HelperID = ph.helperID
	                            WHERE P.IsRemoved = 0 AND p.AgencyID = {notificationLogSearchDTO.agencyID} AND ph.IsRemoved = 0
	                            AND ph.StartDate <= CAST(GETDATE() AS DATE) AND ISNULL(ph.EndDate, CAST(GETDATE() AS DATE)) >= CAST(GETDATE() AS DATE)
                            )
                            ,HelpersSupervised AS
                            (
	                            SELECT
                                    *
                                FROM
                                Helper h
                                WHERE h.AgencyID = {notificationLogSearchDTO.agencyID} AND h.IsRemoved = 0 AND h.SupervisorHelperID = {notificationLogSearchDTO.helperID}
                            )
                            ,PersonSupervised AS
                            (
	                            SELECT DISTINCT
		                            p.PersonID
	                            FROM
	                            Person P
	                            JOIN PersonHelper ph ON ph.PersonID = p.PersonID
	                            JOIN HelpersSupervised h ON h.HelperID = ph.helperID
	                            WHERE P.IsRemoved = 0 AND p.AgencyID = {notificationLogSearchDTO.agencyID} AND ph.IsRemoved = 0
	                            AND ph.StartDate <= CAST(GETDATE() AS DATE) AND ISNULL(ph.EndDate, CAST(GETDATE() AS DATE)) >= CAST(GETDATE() AS DATE)
                            )
                            ,HelpersReviewed AS
                            (
	                            SELECT
		                            *
	                            FROM
	                            Helper h
	                            WHERE h.AgencyID = {notificationLogSearchDTO.agencyID} AND h.IsRemoved = 0 AND 
	                            (h.ReviewerID = {notificationLogSearchDTO.helperID} --OR h.ReviewerID IN (SELECT HelperID FROM HelpersSupervised)
                                )
                            )
                            ,PersonToReview AS
                            (
	                            SELECT DISTINCT
		                            p.PersonID
	                            FROM
	                            Person P
	                            JOIN PersonHelper ph ON ph.PersonID = p.PersonID
	                            JOIN HelpersReviewed h ON h.HelperID = ph.helperID
	                            WHERE P.IsRemoved = 0 AND p.AgencyID = {notificationLogSearchDTO.agencyID} AND ph.IsRemoved = 0
	                            AND ph.StartDate <= CAST(GETDATE() AS DATE) AND ISNULL(ph.EndDate, CAST(GETDATE() AS DATE)) >= CAST(GETDATE() AS DATE)
                            )
                            ,PersonInCollaboration AS
                            (
	                            SELECT DISTINCT
		                            p.PersonID
	                            FROM
	                            HelperList h 
	                            JOIN PersonHelper ph ON ph.HelperID = h.HelperID
	                            AND ph.StartDate <= CAST(GETDATE() AS DATE) AND ISNULL(ph.EndDate, CAST(GETDATE() AS DATE)) >= CAST(GETDATE() AS DATE)
	                            JOIN CollaborationLeadHistory clh ON clh.LeadUserID = h.HelperID
	                            AND clh.StartDate <= CAST(GETDATE() AS DATE) AND ISNULL(clh.EndDate, CAST(GETDATE() AS DATE)) >= CAST(GETDATE() AS DATE)
	                            JOIN PersonCollaboration pc ON clh.CollaborationID = pc.CollaborationID
	                            AND pc.EnrollDate <= CAST(GETDATE() AS DATE) AND ISNULL(pc.EndDate, CAST(GETDATE() AS DATE)) >= CAST(GETDATE() AS DATE)
	                            JOIN Person p ON pc.PersonID = p.PersonID AND P.IsRemoved = 0
                            )
                            ,RecCountAlert As
                            (
	                             SELECT
		                            NH.NotificationLogID, p.PersonID
	                            FROM
	                            Person p
	                            JOIN
	                            (
		                            SELECT PersonID FROM PersonHelped
                                    UNION
		                            SELECT PersonID FROM PersonSupervised
		                            UNION
		                            SELECT PersonID FROM PersonInCollaboration
	                            )
	                            PersonToList ON p.PersonID = PersonToList.PersonID
	                            JOIN NotificationLog NH ON NH.PersonID = p.PersonID AND NH.IsRemoved = 0 AND NH.NotificationResolutionStatusID = {notifiationResolutionStatus.NotificationResolutionStatusID} AND NH.NotificationTypeID IN ({alertTypeID},{assessmentApproveTypeID},{assessmentRejectTypeID})
								union
								SELECT NotificationLogID, PersonID FROM(
									SELECT 
									ROW_NUMBER() OVER(PARTITIon BY PersonQuestionnaireScheduleID ORDER BY NR.NotifyDate) AS RowNumber, NH.NotificationLogID,P.PersonID
									from 
									Person p
									JOIN
									(
										SELECT PersonID FROM PersonHelped
                                        UNION
		                                SELECT PersonID FROM PersonSupervised
		                                UNION
		                                SELECT PersonID FROM PersonInCollaboration
									)
									PersonToList ON p.PersonID = PersonToList.PersonID
									join NotificationLog NH WITH (NOLOCK) 
									on P.PersonID=NH.PersonID
									join Notifyreminder NR on NR.NotifyReminderID=NH.FKeyValue
									WHERE NH.IsRemoved=0 AND  NH.NotificationtypeId={reminderTypeID} AND NH.NotificationResolutionStatusID= {notifiationResolutionStatus.NotificationResolutionStatusID} AND  nR.IsLogAdded=1
									) as A 
									WHERE A.RowNumber = 1
                            )
                            ,RecCountToReview As
                            (
	                            SELECT
		                            NH.NotificationLogID, p.PersonID
	                            FROM
	                            Person p
	                            JOIN PersonToReview ON p.PersonID = PersonToReview.PersonID
	                            JOIN NotificationLog NH ON NH.PersonID = p.PersonID AND NH.IsRemoved = 0 AND NH.NotificationResolutionStatusID = {notifiationResolutionStatus.NotificationResolutionStatusID} AND NH.NotificationTypeID IN ({alertTypeID},{assessmentSubmitTypeID},{emailAssessmentSubmitTypeID})
                            )
                            ,RecCount AS
                            (
	                            SELECT * FROM RecCountAlert
	                            UNION 
	                            SELECT * FROM RecCountToReview
                            )
                            SELECT top 5
                                    NH.NotificationLogID,
                                    NH.NotificationDate,
                                    NH.PersonID,
                                    NH.NotificationTypeID,
                                   NH.AssessmentID AS FKeyValue,
                                    NH.NotificationData,
                                    NH.NotificationResolutionStatusID,
                                    NH.StatusDate,
                                    P.FirstName+ COALESCE(CASE P.MiddleName WHEN '' THEN '' ELSE ' '+P.MiddleName END, ' '+P.MiddleName, '') 
		                            + COALESCE(CASE P.LastName WHEN '' THEN '' ELSE ' '+P.LastName END, ' '+P.LastName, '') as PersonName,
                                    CASE NT.Name 
                                        WHEN 'AssessmentSubmit'  THEN 'Submitted'
                                        WHEN 'EmailAssessmentSubmit'  THEN 'Email Assessment Submit'
                                        WHEN 'AssessmentApproved'  THEN 'Approved'
                                        WHEN 'AssessmentRejected'  THEN 'Returned'
                                    ELSE NT.Name
                                    END as NotificationType,
                                    P.PersonIndex,
                                     NH.Details, 
                                        NH.QuestionnaireID,
                                   NH.AssessmentID
                                FROM
                                Person p
                                JOIN RecCount rec on p.PersonID=rec.PersonID
                                JOIN NotificationLog NH WITH (NOLOCK) ON NH.NotificationLogID=rec.NotificationLogID 
                                JOIN info.NotificationType NT ON NT.NotificationTypeID = NH.NotificationTypeID
                                LEFT JOIN PersonSupervised PLS on NH.PersonID = PLS.PersonID 
                                LEFT JOIN PersonHelped PH on NH.PersonID = PH.PersonID WHERE P.IsRemoved = 0 and P.IsActive = 1 {helperColbQueryCondition}";

                        countquery = $@";WITH HelperList AS
                            (
                                SELECT
                                    *
                                FROM
                                Helper h
                                WHERE h.AgencyID = {notificationLogSearchDTO.agencyID} AND h.IsRemoved = 0 AND h.HelperID = {notificationLogSearchDTO.helperID}
                            )
                            ,PersonHelped AS
                            (
	                            SELECT DISTINCT
		                            p.PersonID
	                            FROM
	                            Person P
	                            JOIN PersonHelper ph ON ph.PersonID = p.PersonID
	                            JOIN HelperList h ON h.HelperID = ph.helperID
	                            WHERE P.IsRemoved = 0 AND p.AgencyID = {notificationLogSearchDTO.agencyID} AND ph.IsRemoved = 0
	                            AND ph.StartDate <= CAST(GETDATE() AS DATE) AND ISNULL(ph.EndDate, CAST(GETDATE() AS DATE)) >= CAST(GETDATE() AS DATE)
                            )
                            ,HelpersSupervised AS
                            (
	                            SELECT
                                    *
                                FROM
                                Helper h
                                WHERE h.AgencyID = {notificationLogSearchDTO.agencyID} AND h.IsRemoved = 0 AND h.SupervisorHelperID = {notificationLogSearchDTO.helperID}
                            )
                            ,PersonSupervised AS
                            (
	                            SELECT DISTINCT
		                            p.PersonID
	                            FROM
	                            Person P
	                            JOIN PersonHelper ph ON ph.PersonID = p.PersonID
	                            JOIN HelpersSupervised h ON h.HelperID = ph.helperID
	                            WHERE P.IsRemoved = 0 AND p.AgencyID = {notificationLogSearchDTO.agencyID} AND ph.IsRemoved = 0
	                            AND ph.StartDate <= CAST(GETDATE() AS DATE) AND ISNULL(ph.EndDate, CAST(GETDATE() AS DATE)) >= CAST(GETDATE() AS DATE)
                            )
                            ,HelpersReviewed AS
                            (
	                            SELECT
		                            *
	                            FROM
	                            Helper h
	                            WHERE h.AgencyID = {notificationLogSearchDTO.agencyID} AND h.IsRemoved = 0 AND 
	                            (h.ReviewerID = {notificationLogSearchDTO.helperID} -- OR h.ReviewerID IN (SELECT HelperID FROM HelpersSupervised)
                                )
                            )
                            ,PersonToReview AS
                            (
	                            SELECT DISTINCT
		                            p.PersonID
	                            FROM
	                            Person P
	                            JOIN PersonHelper ph ON ph.PersonID = p.PersonID
	                            JOIN HelpersReviewed h ON h.HelperID = ph.helperID
	                            WHERE P.IsRemoved = 0 AND p.AgencyID = {notificationLogSearchDTO.agencyID}  AND ph.IsRemoved = 0
	                            AND ph.StartDate <= CAST(GETDATE() AS DATE) AND ISNULL(ph.EndDate, CAST(GETDATE() AS DATE)) >= CAST(GETDATE() AS DATE)
                            )
                            ,PersonInCollaboration AS
                            (
	                            SELECT DISTINCT
		                            p.PersonID
	                            FROM
	                            HelperList h 
	                            JOIN PersonHelper ph ON ph.HelperID = h.HelperID
	                            AND ph.StartDate <= CAST(GETDATE() AS DATE) AND ISNULL(ph.EndDate, CAST(GETDATE() AS DATE)) >= CAST(GETDATE() AS DATE)
	                            JOIN CollaborationLeadHistory clh ON clh.LeadUserID = h.HelperID
	                            AND clh.StartDate <= CAST(GETDATE() AS DATE) AND ISNULL(clh.EndDate, CAST(GETDATE() AS DATE)) >= CAST(GETDATE() AS DATE)
	                            JOIN PersonCollaboration pc ON clh.CollaborationID = pc.CollaborationID
	                            AND pc.EnrollDate <= CAST(GETDATE() AS DATE) AND ISNULL(pc.EndDate, CAST(GETDATE() AS DATE)) >= CAST(GETDATE() AS DATE)
	                            JOIN Person p ON pc.PersonID = p.PersonID AND P.IsRemoved = 0
                            )
                            ,RecCountAlert As
                            (
	                            SELECT
		                            NH.NotificationLogID, p.PersonID
	                            FROM
	                            Person p
	                            JOIN
	                            (
		                            SELECT PersonID FROM PersonHelped
                                    UNION
		                            SELECT PersonID FROM PersonSupervised
		                            UNION
		                            SELECT PersonID FROM PersonInCollaboration
	                            )
	                            PersonToList ON p.PersonID = PersonToList.PersonID
	                            JOIN NotificationLog NH ON NH.PersonID = p.PersonID AND NH.IsRemoved = 0 AND NH.NotificationResolutionStatusID = {notifiationResolutionStatus.NotificationResolutionStatusID} AND NH.NotificationTypeID IN ({alertTypeID},{assessmentApproveTypeID},{assessmentRejectTypeID})
								union
								SELECT NotificationLogID, PersonID FROM(
									SELECT 
									ROW_NUMBER() OVER(PARTITIon BY PersonQuestionnaireScheduleID ORDER BY NR.NotifyDate) AS RowNumber, NH.NotificationLogID,P.PersonID
									from 
									Person p
									JOIN
									(
										SELECT PersonID FROM PersonHelped
                                        UNION
		                                SELECT PersonID FROM PersonSupervised
		                                UNION
		                                SELECT PersonID FROM PersonInCollaboration
									)
									PersonToList ON p.PersonID = PersonToList.PersonID
									join NotificationLog NH WITH (NOLOCK) 
									on P.PersonID=NH.PersonID
									join Notifyreminder NR on NR.NotifyReminderID=NH.FKeyValue
									WHERE NH.IsRemoved=0 AND  NH.NotificationtypeId={reminderTypeID} AND NH.NotificationResolutionStatusID= {notifiationResolutionStatus.NotificationResolutionStatusID} AND  nR.IsLogAdded=1
									) as A 
									WHERE A.RowNumber = 1
                            )
                            ,RecCountToReview As
                            (
	                            SELECT
		                            NH.NotificationLogID, p.PersonID
	                            FROM
	                            Person p
	                            JOIN PersonToReview ON p.PersonID = PersonToReview.PersonID
	                            JOIN NotificationLog NH ON NH.PersonID = p.PersonID AND NH.IsRemoved = 0 AND NH.NotificationResolutionStatusID = {notifiationResolutionStatus.NotificationResolutionStatusID} AND NH.NotificationTypeID IN ({alertTypeID},{assessmentSubmitTypeID},{emailAssessmentSubmitTypeID})
                            )
                            ,RecCount AS
                            (
	                            SELECT * FROM RecCountAlert
	                            UNION 
	                            SELECT * FROM RecCountToReview
                            )                            
                               SELECT top 1 Count (1) AS totalCount                                    
                                FROM
                                Person p
                                JOIN RecCount rec on p.PersonID=rec.PersonID
                                JOIN NotificationLog NH WITH (NOLOCK) ON NH.NotificationLogID=rec.NotificationLogID 
                                JOIN info.NotificationType NT ON NT.NotificationTypeID = NH.NotificationTypeID
                                LEFT JOIN PersonSupervised PLS on NH.PersonID = PLS.PersonID 
                                LEFT JOIN PersonHelped PH on NH.PersonID = PH.PersonID where  P.IsRemoved = 0 and P.IsActive = 1 {helperColbQueryCondition}";
                    }
                    else if (notificationLogSearchDTO.role == PCISEnum.Roles.HelperRO || notificationLogSearchDTO.role == PCISEnum.Roles.HelperRW || notificationLogSearchDTO.role == PCISEnum.Roles.Helper || notificationLogSearchDTO.role == PCISEnum.Roles.Assessor)
                    {
                        query = @$";WITH HelperList AS
                            (
                                SELECT
                                    *
                                FROM
                                Helper h
                                WHERE h.AgencyID = {notificationLogSearchDTO.agencyID} AND h.IsRemoved = 0 AND h.HelperID = {notificationLogSearchDTO.helperID}
                            )
                            ,PersonHelped AS
                            (
	                            SELECT DISTINCT
		                            p.PersonID
	                            FROM
	                            Person P
	                            JOIN PersonHelper ph ON ph.PersonID = p.PersonID
	                            JOIN HelperList h ON h.HelperID = ph.helperID
	                            WHERE P.IsRemoved = 0 AND p.AgencyID = {notificationLogSearchDTO.agencyID} AND ph.IsRemoved = 0
	                            AND ph.StartDate <= CAST(GETDATE() AS DATE) AND ISNULL(ph.EndDate, CAST(GETDATE() AS DATE)) >= CAST(GETDATE() AS DATE)
                            )
                            ,HelpersReviewed AS
                            (
	                            SELECT
		                            *
	                            FROM
	                            Helper h
	                            WHERE h.AgencyID = {notificationLogSearchDTO.agencyID} AND h.IsRemoved = 0 AND 
	                            h.ReviewerID = {notificationLogSearchDTO.helperID}
                            )
                            ,PersonToReview AS
                            (
	                            SELECT DISTINCT
		                            p.PersonID
	                            FROM
	                            Person P
	                            JOIN PersonHelper ph ON ph.PersonID = p.PersonID
	                            JOIN HelpersReviewed h ON h.HelperID = ph.helperID
	                            WHERE P.IsRemoved = 0 AND p.AgencyID = {notificationLogSearchDTO.agencyID} AND ph.IsRemoved = 0
	                            AND ph.StartDate <= CAST(GETDATE() AS DATE) AND ISNULL(ph.EndDate, CAST(GETDATE() AS DATE)) >= CAST(GETDATE() AS DATE)
                            )
                            ,PersonInCollaboration AS
                            (
	                            SELECT DISTINCT
		                            p.PersonID
	                            FROM
	                            HelperList h 
	                            JOIN PersonHelper ph ON ph.HelperID = h.HelperID
	                            AND ph.StartDate <= CAST(GETDATE() AS DATE) AND ISNULL(ph.EndDate, CAST(GETDATE() AS DATE)) >= CAST(GETDATE() AS DATE)
	                            JOIN CollaborationLeadHistory clh ON clh.LeadUserID = h.HelperID
	                            AND clh.StartDate <= CAST(GETDATE() AS DATE) AND ISNULL(clh.EndDate, CAST(GETDATE() AS DATE)) >= CAST(GETDATE() AS DATE)
	                            JOIN PersonCollaboration pc ON clh.CollaborationID = pc.CollaborationID
	                            AND pc.EnrollDate <= CAST(GETDATE() AS DATE) AND ISNULL(pc.EndDate, CAST(GETDATE() AS DATE)) >= CAST(GETDATE() AS DATE)
	                            JOIN Person p ON pc.PersonID = p.PersonID AND P.IsRemoved = 0
                            )
                            ,RecCountAlert As
                            (
	                           SELECT
		                            NH.NotificationLogID, p.PersonID
	                            FROM
	                            Person p
	                            JOIN
	                            (
		                            SELECT PersonID FROM PersonHelped
		                            UNION
		                            SELECT PersonID FROM PersonInCollaboration
	                            )
	                            PersonToList ON p.PersonID = PersonToList.PersonID
	                            JOIN NotificationLog NH ON NH.PersonID = p.PersonID AND NH.IsRemoved = 0 AND NH.NotificationResolutionStatusID = {notifiationResolutionStatus.NotificationResolutionStatusID} AND NH.NotificationTypeID IN ({alertTypeID},{assessmentApproveTypeID},{assessmentRejectTypeID})
								union
								SELECT NotificationLogID, PersonID FROM(
									SELECT 
									ROW_NUMBER() OVER(PARTITIon BY PersonQuestionnaireScheduleID ORDER BY NR.NotifyDate) AS RowNumber, NH.NotificationLogID,P.PersonID
									from 
									Person p
									JOIN
									(
										SELECT PersonID FROM PersonHelped
		                                UNION
		                                SELECT PersonID FROM PersonInCollaboration
									)
									PersonToList ON p.PersonID = PersonToList.PersonID
									join NotificationLog NH WITH (NOLOCK) 
									on P.PersonID=NH.PersonID
									join Notifyreminder NR on NR.NotifyReminderID=NH.FKeyValue
									WHERE NH.IsRemoved=0 AND  NH.NotificationtypeId={reminderTypeID} AND NH.NotificationResolutionStatusID= {notifiationResolutionStatus.NotificationResolutionStatusID} AND  nR.IsLogAdded=1
									) as A 
									WHERE A.RowNumber = 1
                            )
                            ,RecCountToReview As
                            (
	                            SELECT
		                            NH.NotificationLogID, p.PersonID
	                            FROM
	                            Person p
	                            JOIN PersonToReview ON p.PersonID = PersonToReview.PersonID
	                            JOIN NotificationLog NH ON NH.PersonID = p.PersonID AND NH.IsRemoved = 0 AND NH.NotificationResolutionStatusID = {notifiationResolutionStatus.NotificationResolutionStatusID} AND NH.NotificationTypeID IN ({alertTypeID},{assessmentSubmitTypeID},{emailAssessmentSubmitTypeID})
                            )
                            ,RecCount AS
                            (
	                            SELECT * FROM RecCountAlert
	                            UNION 
	                            SELECT * FROM RecCountToReview
                            )                            
                            SELECT TOP 5
                                    NH.NotificationLogID,
                                    NH.NotificationDate,
                                    NH.PersonID,
                                    NH.NotificationTypeID,
                                    NH.AssessmentID AS FKeyValue,
                                    NH.NotificationData,
                                    NH.NotificationResolutionStatusID,
                                    NH.StatusDate,
                                    P.FirstName+ COALESCE(CASE P.MiddleName WHEN '' THEN '' ELSE ' '+P.MiddleName END, ' '+P.MiddleName, '') 
		                            + COALESCE(CASE P.LastName WHEN '' THEN '' ELSE ' '+P.LastName END, ' '+P.LastName, '') as PersonName,
                                    CASE NT.Name 
                                        WHEN 'AssessmentSubmit'  THEN 'Submitted'
                                        WHEN 'EmailAssessmentSubmit'  THEN 'Email Assessment Submit'
                                        WHEN 'AssessmentApproved'  THEN 'Approved'
                                        WHEN 'AssessmentRejected'  THEN 'Returned'
                                    ELSE NT.Name
                                    END as NotificationType,
                                    P.PersonIndex,
                                    NH.Details,
                                   NH.QuestionnaireID,
                                    NH.AssessmentID
                                FROM
                                Person p
                                JOIN RecCount rec on p.PersonID=rec.PersonID
                                JOIN NotificationLog NH WITH (NOLOCK) ON NH.NotificationLogID=rec.NotificationLogID 
                                JOIN info.NotificationType NT ON NT.NotificationTypeID = NH.NotificationTypeID
                                --JOIN info.NotificationResolutionStatus NR on NR.NotificationResolutionStatusID = NH.NotificationResolutionStatusID
                                LEFT JOIN PersonHelped PH ON PH.PersonID = NH.PersonID where  P.IsRemoved = 0 and P.IsActive = 1 {helperColbQueryCondition}";

                        countquery = @$";WITH HelperList AS
                            (
                                SELECT
                                    *
                                FROM
                                Helper h
                                WHERE h.AgencyID = {notificationLogSearchDTO.agencyID} AND h.IsRemoved = 0 AND h.HelperID = {notificationLogSearchDTO.helperID}
                            )
                            ,PersonHelped AS
                            (
	                            SELECT DISTINCT
		                            p.PersonID
	                            FROM
	                            Person P
	                            JOIN PersonHelper ph ON ph.PersonID = p.PersonID
	                            JOIN HelperList h ON h.HelperID = ph.helperID
	                            WHERE P.IsRemoved = 0 AND p.AgencyID = {notificationLogSearchDTO.agencyID} AND ph.IsRemoved = 0
	                            AND ph.StartDate <= CAST(GETDATE() AS DATE) AND ISNULL(ph.EndDate, CAST(GETDATE() AS DATE)) >= CAST(GETDATE() AS DATE)
                            )
                            ,HelpersReviewed AS
                            (
	                            SELECT
		                            *
	                            FROM
	                            Helper h
	                            WHERE h.AgencyID = {notificationLogSearchDTO.agencyID} AND h.IsRemoved = 0 AND 
	                            h.ReviewerID = {notificationLogSearchDTO.helperID}
                            )
                            ,PersonToReview AS
                            (
	                            SELECT DISTINCT
		                            p.PersonID
	                            FROM
	                            Person P
	                            JOIN PersonHelper ph ON ph.PersonID = p.PersonID
	                            JOIN HelpersReviewed h ON h.HelperID = ph.helperID
	                            WHERE P.IsRemoved = 0 AND p.AgencyID = {notificationLogSearchDTO.agencyID} AND ph.IsRemoved = 0
	                            AND ph.StartDate <= CAST(GETDATE() AS DATE) AND ISNULL(ph.EndDate, CAST(GETDATE() AS DATE)) >= CAST(GETDATE() AS DATE)
                            )
                            ,PersonInCollaboration AS
                            (
	                            SELECT DISTINCT
		                            p.PersonID
	                            FROM
	                            HelperList h 
	                            JOIN PersonHelper ph ON ph.HelperID = h.HelperID
	                            AND ph.StartDate <= CAST(GETDATE() AS DATE) AND ISNULL(ph.EndDate, CAST(GETDATE() AS DATE)) >= CAST(GETDATE() AS DATE)
	                            JOIN CollaborationLeadHistory clh ON clh.LeadUserID = h.HelperID
	                            AND clh.StartDate <= CAST(GETDATE() AS DATE) AND ISNULL(clh.EndDate, CAST(GETDATE() AS DATE)) >= CAST(GETDATE() AS DATE)
	                            JOIN PersonCollaboration pc ON clh.CollaborationID = pc.CollaborationID
	                            AND pc.EnrollDate <= CAST(GETDATE() AS DATE) AND ISNULL(pc.EndDate, CAST(GETDATE() AS DATE)) >= CAST(GETDATE() AS DATE)
	                            JOIN Person p ON pc.PersonID = p.PersonID AND P.IsRemoved = 0
                            )
                            ,RecCountAlert As
                            (
	                            SELECT
		                            NH.NotificationLogID, p.PersonID
	                            FROM
	                            Person p
	                            JOIN
	                            (
		                            SELECT PersonID FROM PersonHelped
		                            UNION
		                            SELECT PersonID FROM PersonInCollaboration
	                            )
	                            PersonToList ON p.PersonID = PersonToList.PersonID
	                            JOIN NotificationLog NH ON NH.PersonID = p.PersonID AND NH.IsRemoved = 0 AND NH.NotificationResolutionStatusID = {notifiationResolutionStatus.NotificationResolutionStatusID} AND NH.NotificationTypeID IN ({alertTypeID},{assessmentApproveTypeID},{assessmentRejectTypeID})
								union
								SELECT NotificationLogID, PersonID FROM(
									SELECT 
									ROW_NUMBER() OVER(PARTITIon BY PersonQuestionnaireScheduleID ORDER BY NR.NotifyDate) AS RowNumber, NH.NotificationLogID,P.PersonID
									from 
									Person p
									JOIN
									(
										SELECT PersonID FROM PersonHelped
		                                UNION
		                                SELECT PersonID FROM PersonInCollaboration
									)
									PersonToList ON p.PersonID = PersonToList.PersonID
									join NotificationLog NH WITH (NOLOCK) 
									on P.PersonID=NH.PersonID
									join Notifyreminder NR on NR.NotifyReminderID=NH.FKeyValue
									WHERE NH.IsRemoved=0 AND  NH.NotificationtypeId={reminderTypeID} AND NH.NotificationResolutionStatusID= {notifiationResolutionStatus.NotificationResolutionStatusID} AND  nR.IsLogAdded=1
									) as A 
									WHERE A.RowNumber = 1
                            )
                            ,RecCountToReview As
                            (
	                            SELECT
		                            NH.NotificationLogID, p.PersonID
	                            FROM
	                            Person p
	                            JOIN PersonToReview ON p.PersonID = PersonToReview.PersonID
	                            JOIN NotificationLog NH ON NH.PersonID = p.PersonID AND NH.IsRemoved = 0 AND NH.NotificationResolutionStatusID = {notifiationResolutionStatus.NotificationResolutionStatusID} AND NH.NotificationTypeID IN ({alertTypeID},{assessmentSubmitTypeID},{emailAssessmentSubmitTypeID})
                            )
                            ,RecCount AS
                            (
	                            SELECT * FROM RecCountAlert
	                            UNION 
	                            SELECT * FROM RecCountToReview
                            )
                            SELECT top 1
                                    Count(1) AS TotalCount
                                FROM
                                Person p
                                JOIN RecCount rec on p.PersonID=rec.PersonID
                                JOIN NotificationLog NH WITH (NOLOCK) ON NH.NotificationLogID=rec.NotificationLogID 
                                JOIN info.NotificationType NT ON NT.NotificationTypeID = NH.NotificationTypeID
                                LEFT JOIN PersonHelped PH ON PH.PersonID = NH.PersonID where  P.IsRemoved = 0 and P.IsActive = 1 {helperColbQueryCondition}";
                    }
                }
                query += " ORDER BY  CAST(DateAdd(MINUTE,0,NotificationDate) AS DATETIME) DESC";
                int? totalCount = ExecuteSqlQuery(countquery, x =>
                {
                    return x["TotalCount"] == DBNull.Value ? 0 : (int?)x["TotalCount"];
                }).FirstOrDefault();
                notificationLogDTO = ExecuteSqlQuery(query, x =>
                {
                    // totalCount = x["TotalCount"] == DBNull.Value ? 0 : (int)x["TotalCount"];
                    return new NotificationLogDTO
                    {
                        NotificationLogID = x["NotificationLogID"] == DBNull.Value ? 0 : (int)x["NotificationLogID"],
                        NotificationDate = (DateTime)(x["NotificationDate"] == DBNull.Value ? null : (DateTime?)x["NotificationDate"]),
                        PersonID = x["PersonID"] == DBNull.Value ? 0 : (long)x["PersonID"],
                        NotificationTypeID = x["NotificationTypeID"] == DBNull.Value ? 0 : (int)x["NotificationTypeID"],
                        FKeyValue = x["FKeyValue"] == DBNull.Value ? 0 : (int)x["FKeyValue"],
                        NotificationData = x["NotificationData"] == DBNull.Value ? null : (string)x["NotificationData"],
                        NotificationResolutionStatusID = x["NotificationResolutionStatusID"] == DBNull.Value ? 0 : (int)x["NotificationResolutionStatusID"],
                        StatusDate = x["StatusDate"] == DBNull.Value ? null : (DateTime?)x["StatusDate"],
                        PersonName = x["PersonName"] == DBNull.Value ? null : (string)x["PersonName"],
                        NotificationType = x["NotificationType"] == DBNull.Value ? null : (string)x["NotificationType"],
                        PersonIndex = x["PersonIndex"] == DBNull.Value ? Guid.Empty : (Guid)x["PersonIndex"],
                        Details = x["Details"] == DBNull.Value ? null : ((string)x["Details"]).Trim(),
                        Status = notifiationResolutionStatus.Name, //x["Status"] == DBNull.Value ? null : (string)x["Status"],
                        QuestionnaireID = x["QuestionnaireID"] == DBNull.Value ? 0 : (int)x["QuestionnaireID"],
                        AssessmentID = x["AssessmentID"] == DBNull.Value ? 0 : (int)x["AssessmentID"]
                    };
                }, queryBuilderDTO.QueryParameterDTO);

                return Tuple.Create(notificationLogDTO, totalCount == null ? 0 : totalCount.Value);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// UpdateBulkNotificationLog.
        /// </summary>
        /// <param name="notificationLog">notificationLog.</param>
        /// <returns>NotificationLog</returns>
        public List<NotificationLog> GetNotifcationLogForReminder(List<int> notifyReminderIDList, int notificationTypeID)
        {
            try
            {
                var query = string.Empty;
                query = @"select  *  FROM NotificationLog 
                            where NotificationTypeID=" + notificationTypeID + " And FKeyValue in (" + String.Join(",", notifyReminderIDList) + ")";

                var response = ExecuteSqlQuery(query, x => new NotificationLog
                {
                    NotificationLogID = x["NotificationLogID"] == DBNull.Value ? 0 : (int)x["NotificationLogID"],
                    NotificationDate = (DateTime)(x["NotificationDate"] == DBNull.Value ? null : (DateTime?)x["NotificationDate"]),
                    PersonID = x["PersonID"] == DBNull.Value ? 0 : (long)x["PersonID"],
                    NotificationTypeID = x["NotificationTypeID"] == DBNull.Value ? 0 : (int)x["NotificationTypeID"],
                    FKeyValue = x["FKeyValue"] == DBNull.Value ? 0 : (int)x["FKeyValue"],
                    NotificationData = x["NotificationData"] == DBNull.Value ? null : (string)x["NotificationData"],
                    NotificationResolutionStatusID = x["NotificationResolutionStatusID"] == DBNull.Value ? 0 : (int)x["NotificationResolutionStatusID"],
                    StatusDate = x["StatusDate"] == DBNull.Value ? null : (DateTime?)x["StatusDate"],
                    Details = x["Details"] == DBNull.Value ? null : ((string)x["Details"]).Trim(),
                    QuestionnaireID = x["QuestionnaireID"] == DBNull.Value ? 0 : (int)x["QuestionnaireID"],
                    AssessmentID = x["AssessmentID"] == DBNull.Value ? 0 : (int)x["AssessmentID"],
                    IsRemoved = x["IsRemoved"] == DBNull.Value ? false : (bool)x["IsRemoved"],
                    UpdateDate = x["UpdateDate"] == DBNull.Value ? DateTime.MinValue : (DateTime)x["UpdateDate"],
                    UpdateUserID = x["UpdateUserID"] == DBNull.Value ? 0 : (int)x["UpdateUserID"],
                    HelperName = x["HelperName"] == DBNull.Value ? string.Empty : (string)x["HelperName"],

                }).ToList();
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetNotificationCount
        /// </summary>
        /// <param name="notificationLogSearchDTO"></param>
        /// <param name="notificationViewedOn"></param>
        /// <returns></returns>
        public int GetNotificationCount(NotificationLogSearchDTO notificationLogSearchDTO, DateTime notificationViewedOn)
        {
            try
            {
                // List<NotificationLogDTO> notificationLogDTO = new List<NotificationLogDTO>();
                var query = string.Empty;

                NotificationResolutionStatusDTO notifiationResolutionStatus = this.notifiationResolutionStatusRepository.GetNotificationStatusForUnResolved();
                List<NotificationType> notification = this.notificationTypeRepository.GetAllNotificationType();
                int reminderTypeID = notification.Where(x => x.Name == PCISEnum.NotificationType.Reminder).ToList()[0].NotificationTypeID;
                int alertTypeID = notification.Where(x => x.Name == PCISEnum.NotificationType.Alert).ToList()[0].NotificationTypeID;
                int assessmentApproveTypeID = notification.Where(x => x.Name == PCISEnum.AssessmentNotificationType.Approve).ToList()[0].NotificationTypeID;
                int assessmentRejectTypeID = notification.Where(x => x.Name == PCISEnum.AssessmentNotificationType.Reject).ToList()[0].NotificationTypeID;
                int emailAssessmentSubmitTypeID = notification.Where(x => x.Name == PCISEnum.AssessmentNotificationType.EmailSubmit).ToList()[0].NotificationTypeID;
                int assessmentSubmitTypeID = notification.Where(x => x.Name == PCISEnum.AssessmentNotificationType.Submit).ToList()[0].NotificationTypeID;
                // var sqlLastLogin=string.Format("{ 0: hh: mm: ffff}", lastLogin);
                string sqlNotificationViewedOn = String.Format("{0:yyyy-MM-dd HH:mm:ss.fffffff}", notificationViewedOn);
                string lastNotifyCheck = $@"AND NH.NotificationDate >= '{sqlNotificationViewedOn}'";
                var helperColbQueryCondition = string.Empty;
                if (notificationLogSearchDTO.role != PCISEnum.Roles.SuperAdmin)
                {
                    var personIdList = GetHelperPersonInCollaborationDetails(notifiationResolutionStatus.NotificationResolutionStatusID, notificationLogSearchDTO.UserID, notificationLogSearchDTO.agencyID);
                    if (personIdList.Count > 0)
                    {
                        string personIDs = string.Join(",", personIdList.ToArray());
                        List<string> notifcatnsInColbratn = GetHelperNotificationsInCollaboration(notificationLogSearchDTO, personIDs, notifiationResolutionStatus.NotificationResolutionStatusID, reminderTypeID);
                        string notifcatnsInColbratnIds = notifcatnsInColbratn.Count > 0 ? string.Join(",", notifcatnsInColbratn.ToArray()) : "0";
                        helperColbQueryCondition = $@"AND (P.PersonID NOT IN ({personIDs}) OR (P.PersonID IN ({personIDs}) AND NH.NotificationLogID IN ({notifcatnsInColbratnIds})))";
                    }
                }
                if (notificationLogSearchDTO.role == PCISEnum.Roles.SuperAdmin)
                {
                    query = $@"select COUNT(NH.NotificationLogID) OVER() AS TotalCount                          
		                                from NotificationLog NH WITH (NOLOCK)
		                                join Person P WITH (NOLOCK) on P.PersonID=NH.PersonID
		                                join info.NotificationType NT on NT.NotificationTypeID=NH.NotificationTypeID  
                            WHERE NH.IsRemoved=0 AND NH.NotificationResolutionStatusID={notifiationResolutionStatus.NotificationResolutionStatusID} AND P.AgencyID= { notificationLogSearchDTO.agencyID } AND NH.[NotificationTypeID] NOT IN({reminderTypeID}) {lastNotifyCheck}";
                }
                else if (notificationLogSearchDTO.role == PCISEnum.Roles.OrgAdminRO || notificationLogSearchDTO.role == PCISEnum.Roles.OrgAdminRW || notificationLogSearchDTO.role == PCISEnum.Roles.OrgAdmin)
                {
                    query = $@";WITH PersonHelped AS
                        			(
                        				SELECT DISTINCT
                        					p.PersonID, p.AgencyID
                        				FROM
                        				Person P
                        				JOIN PersonHelper ph ON ph.PersonID = p.PersonID
                        				JOIN Helper h ON h.HelperID = ph.helperID
                        				WHERE P.IsRemoved = 0 AND p.AgencyID = {notificationLogSearchDTO.agencyID} AND ph.IsRemoved = 0 AND h.AgencyID = {notificationLogSearchDTO.agencyID} AND h.IsRemoved = 0 AND h.HelperID = {notificationLogSearchDTO.helperID}
                        				AND ph.StartDate <= CAST(GETDATE() AS DATE) AND ISNULL(ph.EndDate, CAST(GETDATE() AS DATE)) >= CAST(GETDATE() AS DATE)
                        			), 
                                tempNotifications as 
                                    (
                        		        SELECT
                                            	NH.NotificationLogID,
                                            NH.NotificationDate
                        		            ,NH.PersonID,NH.NotificationTypeID                             
                        		        from NotificationLog NH WITH (NOLOCK)
                        		        join Person P WITH (NOLOCK) on P.PersonID=NH.PersonID
                        		        join info.NotificationType NT on NT.NotificationTypeID=NH.NotificationTypeID  
                        		        LEFT JOIN PersonHelped PH on NH.PersonID = PH.PersonID  and  PH.AgencyID= {notificationLogSearchDTO.agencyID}
                        		        WHERE NH.IsRemoved=0 AND NH.NotificationResolutionStatusID={notifiationResolutionStatus.NotificationResolutionStatusID} AND P.AgencyID= {notificationLogSearchDTO.agencyID} AND P.IsRemoved = 0 {lastNotifyCheck}  {helperColbQueryCondition} 
                                  )
                        	SELECT COUNT(1) OVER() AS TotalCount,* 
                        	    FROM 
                        	     (
                        		    SELECT
                        		         temp.NotificationLogID,
                        		       temp.NotificationDate
                        		        ,temp.PersonID,temp.NotificationTypeID               
                        		        FROM tempNotifications temp
                                        LEFT JOIN PersonHelped PH on temp.PersonID = PH.PersonID  and  PH.AgencyID= {notificationLogSearchDTO.agencyID}
                        		        WHERE PH.PersonID = temp.PersonID
                                    UNION
                        		    SELECT
                        		        temp.NotificationLogID,
                        		        temp.NotificationDate
                        		        ,temp.PersonID,temp.NotificationTypeID                              
                        		        FROM tempNotifications temp
                        		        WHERE temp.[NotificationTypeID] <> ({reminderTypeID})
                        		 ) T 
                        	WHERE 1=1 ";
                }
                else if (notificationLogSearchDTO.role == PCISEnum.Roles.Supervisor)
                {
                    query = @$";WITH HelperList AS
                            (
                                SELECT
                                    *
                                FROM
                                Helper h
                                WHERE h.AgencyID = {notificationLogSearchDTO.agencyID} AND h.IsRemoved = 0 AND h.HelperID = {notificationLogSearchDTO.helperID}
                            )
                            ,PersonHelped AS
                            (
	                            SELECT DISTINCT
		                            p.PersonID
	                            FROM
	                            Person P
	                            JOIN PersonHelper ph ON ph.PersonID = p.PersonID
	                            JOIN HelperList h ON h.HelperID = ph.helperID
	                            WHERE P.IsRemoved = 0 AND p.AgencyID = {notificationLogSearchDTO.agencyID} AND ph.IsRemoved = 0
	                            AND ph.StartDate <= CAST(GETDATE() AS DATE) AND ISNULL(ph.EndDate, CAST(GETDATE() AS DATE)) >= CAST(GETDATE() AS DATE)
                            )
                            ,HelpersSupervised AS
                            (
	                            SELECT
                                    *
                                FROM
                                Helper h
                                WHERE h.AgencyID = {notificationLogSearchDTO.agencyID} AND h.IsRemoved = 0 AND h.SupervisorHelperID = {notificationLogSearchDTO.helperID}
                            )
                            ,PersonSupervised AS
                            (
	                            SELECT DISTINCT
		                            p.PersonID
	                            FROM
	                            Person P
	                            JOIN PersonHelper ph ON ph.PersonID = p.PersonID
	                            JOIN HelpersSupervised h ON h.HelperID = ph.helperID
	                            WHERE P.IsRemoved = 0 AND p.AgencyID = {notificationLogSearchDTO.agencyID} AND ph.IsRemoved = 0
	                            AND ph.StartDate <= CAST(GETDATE() AS DATE) AND ISNULL(ph.EndDate, CAST(GETDATE() AS DATE)) >= CAST(GETDATE() AS DATE)
                            )                           
                            ,HelpersReviewed AS
                            (
	                            SELECT
		                            *
	                            FROM
	                            Helper h
	                            WHERE h.AgencyID = {notificationLogSearchDTO.agencyID} AND h.IsRemoved = 0 AND 
	                            (h.ReviewerID = {notificationLogSearchDTO.helperID} --OR h.ReviewerID IN (SELECT HelperID FROM HelpersSupervised)
                                )
                            )
                            ,PersonToReview AS
                            (
	                            SELECT DISTINCT
		                            p.PersonID
	                            FROM
	                            Person P
	                            JOIN PersonHelper ph ON ph.PersonID = p.PersonID
	                            JOIN HelpersReviewed h ON h.HelperID = ph.helperID
	                            WHERE P.IsRemoved = 0 AND p.AgencyID = {notificationLogSearchDTO.agencyID} AND ph.IsRemoved = 0
	                            AND ph.StartDate <= CAST(GETDATE() AS DATE) AND ISNULL(ph.EndDate, CAST(GETDATE() AS DATE)) >= CAST(GETDATE() AS DATE)
                            )
                            ,PersonInCollaboration AS
                            (
	                            SELECT DISTINCT
		                            p.PersonID
	                            FROM
	                            HelperList h 
	                            JOIN PersonHelper ph ON ph.HelperID = h.HelperID
	                            AND ph.StartDate <= CAST(GETDATE() AS DATE) AND ISNULL(ph.EndDate, CAST(GETDATE() AS DATE)) >= CAST(GETDATE() AS DATE)
	                            JOIN CollaborationLeadHistory clh ON clh.LeadUserID = h.HelperID
	                            AND clh.StartDate <= CAST(GETDATE() AS DATE) AND ISNULL(clh.EndDate, CAST(GETDATE() AS DATE)) >= CAST(GETDATE() AS DATE)
	                            JOIN PersonCollaboration pc ON clh.CollaborationID = pc.CollaborationID
	                            AND pc.EnrollDate <= CAST(GETDATE() AS DATE) AND ISNULL(pc.EndDate, CAST(GETDATE() AS DATE)) >= CAST(GETDATE() AS DATE)
	                            JOIN Person p ON pc.PersonID = p.PersonID AND P.IsRemoved = 0
                            )
                            ,RecCountAlert As
                            (
	                            SELECT
		                            NH.NotificationLogID, p.PersonID
	                            FROM
	                            Person p
	                            JOIN
	                            (
		                            SELECT PersonID FROM PersonHelped
		                            UNION
		                            SELECT PersonID FROM PersonSupervised
		                            UNION
		                            SELECT PersonID FROM PersonInCollaboration
	                            )
	                            PersonToList ON p.PersonID = PersonToList.PersonID
	                            JOIN NotificationLog NH ON NH.PersonID = p.PersonID AND NH.IsRemoved = 0 {lastNotifyCheck}  AND NH.NotificationResolutionStatusID = {notifiationResolutionStatus.NotificationResolutionStatusID} AND NH.NotificationTypeID IN ({alertTypeID},{reminderTypeID},{assessmentApproveTypeID},{assessmentRejectTypeID})
                            )
                            ,RecCountToReview As
                            (
	                            SELECT
		                            NH.NotificationLogID, p.PersonID
	                            FROM
	                            Person p
	                            JOIN PersonToReview ON p.PersonID = PersonToReview.PersonID
	                            JOIN NotificationLog NH ON NH.PersonID = p.PersonID AND NH.IsRemoved = 0 {lastNotifyCheck} AND NH.NotificationResolutionStatusID = {notifiationResolutionStatus.NotificationResolutionStatusID} AND NH.NotificationTypeID IN ({alertTypeID},{assessmentSubmitTypeID},{emailAssessmentSubmitTypeID})
                            )
                            ,RecCount AS
                            (
	                            SELECT * FROM RecCountAlert
	                            UNION 
	                            SELECT * FROM RecCountToReview
                            )
                           SELECT 
                                COUNT(NotificationLogID) OVER() AS TotalCount,* 
                            FROM 
                            (
                                SELECT
                                    NH.NotificationLogID
                                  
                                    
                                FROM
                                Person p
                                JOIN RecCount rec on p.PersonID=rec.PersonID
                                JOIN NotificationLog NH WITH (NOLOCK) ON NH.NotificationLogID=rec.NotificationLogID 
                                JOIN info.NotificationType NT ON NT.NotificationTypeID = NH.NotificationTypeID
                                 LEFT JOIN PersonSupervised PLS on NH.PersonID = PLS.PersonID 
                                LEFT JOIN PersonHelped PH on NH.PersonID = PH.PersonID where  P.IsRemoved = 0 and P.IsActive = 1 {lastNotifyCheck} 
                            {helperColbQueryCondition} ) T WHERE 1=1 ";
                }
                else if (notificationLogSearchDTO.role == PCISEnum.Roles.HelperRO || notificationLogSearchDTO.role == PCISEnum.Roles.HelperRW || notificationLogSearchDTO.role == PCISEnum.Roles.Helper || notificationLogSearchDTO.role == PCISEnum.Roles.Assessor)
                {
                    query = @$";WITH HelperList AS
                            (
                                SELECT
                                    *
                                FROM
                                Helper h
                                WHERE h.AgencyID = {notificationLogSearchDTO.agencyID} AND h.IsRemoved = 0 AND h.HelperID = {notificationLogSearchDTO.helperID}
                            )
                            ,PersonHelped AS
                            (
	                            SELECT DISTINCT
		                            p.PersonID
	                            FROM
	                            Person P
	                            JOIN PersonHelper ph ON ph.PersonID = p.PersonID
	                            JOIN HelperList h ON h.HelperID = ph.helperID
	                            WHERE P.IsRemoved = 0 AND p.AgencyID = {notificationLogSearchDTO.agencyID} AND ph.IsRemoved = 0
	                            AND ph.StartDate <= CAST(GETDATE() AS DATE) AND ISNULL(ph.EndDate, CAST(GETDATE() AS DATE)) >= CAST(GETDATE() AS DATE)
                            )
                            ,HelpersReviewed AS
                            (
	                            SELECT
		                            *
	                            FROM
	                            Helper h
	                            WHERE h.AgencyID = {notificationLogSearchDTO.agencyID} AND h.IsRemoved = 0 AND 
	                            h.ReviewerID = {notificationLogSearchDTO.helperID}
                            )
                            ,PersonToReview AS
                            (
	                            SELECT DISTINCT
		                            p.PersonID
	                            FROM
	                            Person P
	                            JOIN PersonHelper ph ON ph.PersonID = p.PersonID
	                            JOIN HelpersReviewed h ON h.HelperID = ph.helperID
	                            WHERE P.IsRemoved = 0 AND p.AgencyID = {notificationLogSearchDTO.agencyID} AND ph.IsRemoved = 0
	                            AND ph.StartDate <= CAST(GETDATE() AS DATE) AND ISNULL(ph.EndDate, CAST(GETDATE() AS DATE)) >= CAST(GETDATE() AS DATE)
                            )
                            ,PersonInCollaboration AS
                            (
	                            SELECT DISTINCT
		                            p.PersonID
	                            FROM
	                            HelperList h 
	                            JOIN PersonHelper ph ON ph.HelperID = h.HelperID
	                            AND ph.StartDate <= CAST(GETDATE() AS DATE) AND ISNULL(ph.EndDate, CAST(GETDATE() AS DATE)) >= CAST(GETDATE() AS DATE)
	                            JOIN CollaborationLeadHistory clh ON clh.LeadUserID = h.HelperID
	                            AND clh.StartDate <= CAST(GETDATE() AS DATE) AND ISNULL(clh.EndDate, CAST(GETDATE() AS DATE)) >= CAST(GETDATE() AS DATE)
	                            JOIN PersonCollaboration pc ON clh.CollaborationID = pc.CollaborationID
	                            AND pc.EnrollDate <= CAST(GETDATE() AS DATE) AND ISNULL(pc.EndDate, CAST(GETDATE() AS DATE)) >= CAST(GETDATE() AS DATE)
	                            JOIN Person p ON pc.PersonID = p.PersonID AND P.IsRemoved = 0
                            )
                            ,RecCountAlert As
                            (
	                            SELECT
		                            NH.NotificationLogID, p.PersonID
	                            FROM
	                            Person p
	                            JOIN
	                            (
		                            SELECT PersonID FROM PersonHelped
		                            UNION
		                            SELECT PersonID FROM PersonInCollaboration
	                            )
	                            PersonToList ON p.PersonID = PersonToList.PersonID
	                            JOIN NotificationLog NH ON NH.PersonID = p.PersonID AND NH.IsRemoved = 0 {lastNotifyCheck} AND NH.NotificationResolutionStatusID = {notifiationResolutionStatus.NotificationResolutionStatusID} AND NH.NotificationTypeID IN ({alertTypeID},{reminderTypeID},{assessmentApproveTypeID},{assessmentRejectTypeID})
                            )
                            ,RecCountToReview As
                            (
	                            SELECT
		                            NH.NotificationLogID, p.PersonID
	                            FROM
	                            Person p
	                            JOIN PersonToReview ON p.PersonID = PersonToReview.PersonID
	                            JOIN NotificationLog NH ON NH.PersonID = p.PersonID AND NH.IsRemoved = 0 {lastNotifyCheck} AND NH.NotificationResolutionStatusID = {notifiationResolutionStatus.NotificationResolutionStatusID} AND NH.NotificationTypeID IN ({alertTypeID},{assessmentSubmitTypeID},{emailAssessmentSubmitTypeID})
                            )
                            ,RecCount AS
                            (
	                            SELECT * FROM RecCountAlert
	                            UNION 
	                            SELECT * FROM RecCountToReview
                            )
                            SELECT 
                                COUNT(NotificationLogID) OVER() AS TotalCount,* 
                            FROM 
                            (
                                SELECT
                                    NH.NotificationLogID
                                    
                                FROM
                                Person p
                                JOIN RecCount rec on p.PersonID=rec.PersonID
                                JOIN NotificationLog NH WITH (NOLOCK) ON NH.NotificationLogID=rec.NotificationLogID 
                                JOIN info.NotificationType NT ON NT.NotificationTypeID = NH.NotificationTypeID
                                LEFT JOIN PersonHelped PH ON PH.PersonID = NH.PersonID  where  P.IsRemoved = 0 and P.IsActive = 1 {lastNotifyCheck}
                            {helperColbQueryCondition} ) T WHERE 1=1";
                }
                var data = ExecuteSqlQuery(query, x => new NotificationLogResponseDTO
                {
                    TotalCount = x["TotalCount"] == DBNull.Value ? 0 : (int)x["TotalCount"]
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
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<string> GetHelperPersonInCollaborationDetails(int NotificationResolutionStatusID, int userID, long loggedInAgencyID)
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
        /// <summary>
        /// GetAssessmentAlertNotificationLog
        /// </summary>
        /// <param name="notificationTypeID"></param>
        /// <param name="assessmentId"></param>
        /// <returns></returns>
        public IReadOnlyList<NotificationLog> GetAssessmentAlertNotificationLog(int notificationTypeID, int assessmentId)
        {
            try
            {
                IReadOnlyList<NotificationLogDTO> notificationLogDTO = new List<NotificationLogDTO>();
                var result = this.GetAsync(x => notificationTypeID.Equals(x.NotificationTypeID) && x.AssessmentID == assessmentId && x.IsRemoved == false).Result;

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
