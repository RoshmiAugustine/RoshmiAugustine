using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.DTO.Input;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Infrastructure.Data.Repositories.Base;
using Opeeka.PICS.Infrastructure.Enums;

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{
    public class NotifyReminderRepository : BaseRepository<NotifyReminder>, INotifyReminderRepository
    {
        private readonly OpeekaDBContext _dbContext;
        private readonly IMapper mapper;

        public NotifyReminderRepository(OpeekaDBContext dbContext, IMapper mapper)
            : base(dbContext)
        {
            this._dbContext = dbContext;
            this.mapper = mapper;
        }

        /// <summary>
        /// UpdateNotificationLog.
        /// </summary>
        /// <param name="notificationLog">notificationLog.</param>
        /// <returns>NotificationLog</returns>
        public List<NotifyReminder> AddBulkNotifyReminder(List<NotifyReminderDTO> notifyReminderList)
        {
            try
            {
                List<NotifyReminder> notifyReminder = new List<NotifyReminder>();
                this.mapper.Map<List<NotifyReminderDTO>, List<NotifyReminder>>(notifyReminderList, notifyReminder);
                var result = this.AddBulkAsync(notifyReminder);
                result.Wait();
                return notifyReminder;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// UpdateNotificationLog.
        /// </summary>
        /// <param name="notificationLog">notificationLog.</param>
        /// <returns>NotificationLog</returns>
        public void UpdateBulkNotifyReminder(List<NotifyReminder> notifyReminderList)
        {
            try
            {
                if (notifyReminderList.Count > 0)
                {
                    var res = this.UpdateBulkAsync(notifyReminderList);
                    res.Wait();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetNotifyReminders.
        /// </summary>
        /// <param name="GetNotifyReminderInput">GetNotifyReminderInput.</param>
        /// <returns>NotifyReminderDetailsDTO.</returns>
        public List<NotifyReminderDTO> GetNotifyReminders(GetNotifyReminderInputDTO GetNotifyReminderInput, bool fetchIsLogAdded = true)
        {
            try
            {

                var query = string.Empty;
                var subquery = string.Empty;
                if (fetchIsLogAdded)
                {
                    subquery = "IsLogAdded=1 And";
                }
                query = @$"select  NotifyReminderID,IsLogAdded,NotifyDate,PersonQuestionnaireScheduleID, QuestionnaireReminderRuleID, InviteToCompleteSentAt, InviteToCompleteMailStatus
                            FROM NotifyReminder 
                            where {subquery}  PersonQuestionnaireScheduleID in (" + String.Join(",", GetNotifyReminderInput.personQuestionnaireScheduleIDList) + ") And IsRemoved = 0";

                var response = ExecuteSqlQuery(query, x => new NotifyReminderDTO
                {
                    NotifyReminderID = x["NotifyReminderID"] == DBNull.Value ? 0 : (int)x["NotifyReminderID"],
                    IsLogAdded = x["IsLogAdded"] == DBNull.Value ? false : (bool)x["IsLogAdded"],
                    NotifyDate = x["NotifyDate"] == DBNull.Value ? DateTime.MinValue : (DateTime)x["NotifyDate"],
                    PersonQuestionnaireScheduleID = x["PersonQuestionnaireScheduleID"] == DBNull.Value ? 0 : (long)x["PersonQuestionnaireScheduleID"],
                    QuestionnaireReminderRuleID = x["QuestionnaireReminderRuleID"] == DBNull.Value ? 0 : (int)x["QuestionnaireReminderRuleID"],
                    InviteToCompleteSentAt = x["InviteToCompleteSentAt"] == DBNull.Value ? null : (DateTime?)x["InviteToCompleteSentAt"],
                    InviteToCompleteMailStatus = x["InviteToCompleteMailStatus"] == DBNull.Value ? string.Empty : (string?)x["InviteToCompleteMailStatus"]
                }).ToList();


                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// GetReminderDetailsForInviteToComplete.
        /// Fetch all PersonQuestionnaires with corresponding reminders with mailstatus pending for today.
        /// NotifyReminders and InviteToCOmplete details are fetched as JSON
        /// </summary>
        /// <returns></returns>
        public List<RemindersToTriggerInviteToCompleteDTO> GetReminderDetailsForInviteToCompleteTrigger()
        {
            try
            {
                var query = string.Empty;
                var subquery = string.Empty;
                
                query = @$";WITH NotifyCTE AS (
						      SELECT NR.NotifyReminderID, NR.PersonQuestionnaireScheduleID, PQS.PersonQuestionnaireID, PQS.QuestionnaireWindowID, NR.NotifyDAte
							   FROM NotifyReminder NR 
						       JOIN PersonQuestionnaireSchedule PQS ON PQS.PersonQuestionnaireScheduleID = NR.PersonQuestionnaireScheduleID
						      WHERE PQS.IsRemoved = 0 AND NR.IsLogAdded =1 
                                AND NR.InviteToCompleteMailStatus = '{PCISEnum.EmailStatus.Pending}'
							    AND CAST(NR.NotifyDate AS Date) = CAST(getdate() AS Date) 
						     ) SELECT CTE.NotifyReminderId, Q.QuestionnaireID, CTE.PersonQuestionnaireScheduleID,
                          PQ.PersonQuestionnaireID, CTE.NotifyDate, CTE.QuestionnaireWindowID, QW.AssessmentReasonID,
						    InviteToCompleteReceivers = (SELECT ICR.InviteToCompleteReceiverID ,ICR.Name
						     FROM Info.InviteToCompleteReceiver ICR WHERE CAST(ICR.InviteToCompleteReceiverID AS VARCHAR(5))
                                        IN (SELECT * FROM STRING_SPLIT(ISNULL(Q.InviteToCompleteReceiverIds, '0'),',')) FOR JSON PATH)
						      FROM NotifyCTE CTE
						      JOIN PersonQuestionnaire PQ ON CTE.PersonQuestionnaireID = PQ.PersonQuestionnaireID
						      JOIN Questionnaire Q ON PQ.QuestionnaireID = Q.QuestionnaireID 
						      JOIN QuestionnaireWindow QW ON CTE.QuestionnaireWindowID = QW.QuestionnaireWindowID 
						      WHERE Q.IsRemoved = 0 AND PQ.IsRemoved = 0 
                              AND (Q.IsEmailInviteToCompleteReminders = 1 AND ISNULL(Q.InviteToCompleteReceiverIds,'0') <> '0')";

                var result = ExecuteSqlQuery(query, x => new RemindersToTriggerInviteToCompleteDTO
                {
                    PersonQuestionnaireID = x["PersonQuestionnaireID"] == DBNull.Value ? 0 : (long)x["PersonQuestionnaireID"],
                    AssessmentReasonID = x["AssessmentReasonID"] == DBNull.Value ? 0 : (int)x["AssessmentReasonID"],
                    InviteToCompleteReceivers = x["InviteToCompleteReceivers"] == DBNull.Value ? string.Empty : (string)x["InviteToCompleteReceivers"],
                    PersonQuestionnaireScheduleID = x["PersonQuestionnaireScheduleID"] == DBNull.Value ? 0 : (long)x["PersonQuestionnaireScheduleID"],
                    QuestionnaireID = x["QuestionnaireID"] == DBNull.Value ? 0 : (int)x["QuestionnaireID"],
                    QuestionnaireWindowID = x["QuestionnaireWindowID"] == DBNull.Value ? 0 : (int)x["QuestionnaireWindowID"],
                    NotifyReminderDate = (DateTime) x["NotifyDate"],
                    NotifyReminderId = (int) x["NotifyReminderId"],
                }).ToList();
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// GetNotifyReminders.
        /// </summary>
        /// <param name="GetNotifyReminderInput">GetNotifyReminderInput.</param>
        /// <returns>NotifyReminderDetailsDTO.</returns>
        public List<NotifyReminder> GetNotifyRemindersByIds(List<int> notifyReminderIds)
        {
            try
            {
                var query = string.Empty;
                query = @$"select  NotifyReminderID,IsLogAdded,NotifyDate,PersonQuestionnaireScheduleID, QuestionnaireReminderRuleID, InviteToCompleteSentAt, InviteToCompleteMailStatus, IsRemoved
                            FROM NotifyReminder 
                            WHERE NotifyReminderId IN ({String.Join(",", notifyReminderIds) }) And IsRemoved = 0";

                var response = ExecuteSqlQuery(query, x => new NotifyReminder
                {
                    NotifyReminderID = (int)x["NotifyReminderID"],
                    IsLogAdded =  (bool)x["IsLogAdded"],
                    NotifyDate =  (DateTime)x["NotifyDate"],
                    PersonQuestionnaireScheduleID =  (long?)x["PersonQuestionnaireScheduleID"],
                    QuestionnaireReminderRuleID =  (int)x["QuestionnaireReminderRuleID"],
                    InviteToCompleteSentAt = x["InviteToCompleteSentAt"] == DBNull.Value ? null : (DateTime?)x["InviteToCompleteSentAt"],
                    InviteToCompleteMailStatus = (string?)x["InviteToCompleteMailStatus"],
                    IsRemoved = (bool)x["IsRemoved"]                
                }).ToList();


                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
