// -----------------------------------------------------------------------
// <copyright file="AddressRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Infrastructure.Data.Repositories.Base;
using System.Collections.Generic;
using System.Linq;
using Opeeka.PICS.Infrastructure.Enums;

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{
    public class PersonQuestionnaireScheduleRepository : BaseRepository<PersonQuestionnaireSchedule>, IPersonQuestionnaireScheduleRepository
    {
        private readonly IMapper mapper;
        private readonly ILogger<PersonQuestionnaireScheduleRepository> logger;
        private readonly OpeekaDBContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonQuestionnaire"/> class.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public PersonQuestionnaireScheduleRepository(ILogger<PersonQuestionnaireScheduleRepository> logger, OpeekaDBContext _dbContext, IMapper mapper)
            : base(_dbContext)
        {
            this.mapper = mapper;
            this.logger = logger;
            this._dbContext = _dbContext;
        }

        /// To add personQuestionnaire details.
        /// </summary>
        /// <param name="personQuestionnaireDTO"></param>
        /// <returns>Guid.</returns>
        public long AddPersonQuestionnaireSchedule(PersonQuestionnaireSchedule personQuestionnaireSchedule)
        {
            try
            {
                personQuestionnaireSchedule.UpdateDate = DateTime.Now;
                var result = this.AddAsync(personQuestionnaireSchedule).Result.PersonQuestionnaireScheduleID;
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// UpdateBulkPersonQuestionnaireSchedule.
        /// </summary>
        /// <param name="notes">List of personQuestionnaireSchedule</param>
        /// <returns>List of personQuestionnaireSchedule</returns>
        public List<PersonQuestionnaireSchedule> UpdateBulkPersonQuestionnaireSchedule(List<PersonQuestionnaireSchedule> personQuestionnaireSchedule)
        {
            try
            {
                if (personQuestionnaireSchedule.Count > 0)
                {
                    personQuestionnaireSchedule?.ForEach(x => x.UpdateDate = DateTime.Now);
                    var res = this.UpdateBulkAsync(personQuestionnaireSchedule);
                    res.Wait();
                }
                return personQuestionnaireSchedule;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To get details PersonQuestionnaire.
        /// </summary>
        /// <param id="int">id.</param>
        /// <returns>.PersonQuestionnaireDTO</returns>
        public async Task<List<PersonQuestionnaireSchedule>> GetPersonQuestionnaireSchedule(long personQuestionnaireID, int questionnaireWindowID)
        {
            try
            {
                var personQuestionnaireSchedule = await this.GetAsync(x => x.PersonQuestionnaireID == personQuestionnaireID && x.QuestionnaireWindowID == questionnaireWindowID && !x.IsRemoved);
                return personQuestionnaireSchedule.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// UpdatePersonQuestionnaireSchedule.
        /// </summary>
        /// <param name="notes">List of personQuestionnaireSchedule</param>
        /// <returns>List of personQuestionnaireSchedule</returns>
        public PersonQuestionnaireSchedule UpdatePersonQuestionnaireSchedule(PersonQuestionnaireSchedule personQuestionnaireSchedule)
        {
            try
            {
                personQuestionnaireSchedule.UpdateDate = DateTime.Now;
                var res = this.UpdateAsync(personQuestionnaireSchedule);
                res.Wait();
                return personQuestionnaireSchedule;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To get details of NotifyReminder that scheduled for today for ReminderTrigger.
        /// </summary>
        /// <returns>.ReminderNotificationsListDTO</returns>
        public List<ReminderNotificationsListDTO> GetNotifyReminderScheduledForToday(DateTime? lastRunTime = null, DateTime? currentRunTime = null)
        {
            try
            {
                var query = string.Empty;

                query = $@"select PQ.PersonID,NR.NotifyReminderID, PQS.PersonQuestionnaireScheduleID,PQ.QuestionnaireID,Q.Name,Q.ReminderScheduleName,I.Abbrev,AR.Name as AssessmentReasonName,PQS.WindowDueDate,PH.HelperID,H.FirstName+' '+IsNull(H.MiddleName,'')+' ' +H.LastName AS LeadHelperName,PQS.PersonQuestionnaireID, Q.IsEmailInviteToCompleteReminders,Q.IsEmailRemindersHelpers
                          from PersonQuestionnaireSchedule PQS
                          join NotifyReminder NR on PQS.PersonQuestionnaireScheduleID = NR.PersonQuestionnaireScheduleID
                          join PersonQuestionnaire PQ on PQS.PersonQuestionnaireID = PQ.PersonQuestionnaireID
                          join Questionnaire Q on PQ.QuestionnaireID = Q.QuestionnaireID 
                          join info.Instrument I on Q.InstrumentID = I.InstrumentID
                          join QuestionnaireWindow QW on PQS.QuestionnaireWindowID = QW.QuestionnaireWindowID
                          join info.AssessmentReason AR on QW.AssessmentReasonID = AR.AssessmentReasonID
						  LEFT JOIN PersonHelper PH ON PQ.PersonID = PH.PersonID AND PH.IsLead = 1 AND PH.IsRemoved = 0
						  LEFT JOIN Helper H ON PH.HelperID = H.HelperID AND PH.IsRemoved = 0
                          where PQS.IsRemoved =0 AND  NR.IsLogAdded = 0 AND NR.NotifyDate 
                          BETWEEN CAST('{lastRunTime}' AS datetime2) and CAST('{currentRunTime}' AS datetime2) ";

                var data = ExecuteSqlQuery(query, x => new ReminderNotificationsListDTO
                {
                    PersonID = x["PersonID"] == DBNull.Value ? 0 : (long)x["PersonID"],
                    NotifyReminderID = x["NotifyReminderID"] == DBNull.Value ? 0 : (int)x["NotifyReminderID"],
                    PersonQuestionnaireScheduleID = x["PersonQuestionnaireScheduleID"] == DBNull.Value ? 0 : (long)x["PersonQuestionnaireScheduleID"],
                    QuestionnaireID = x["QuestionnaireID"] == DBNull.Value ? 0 : (int)x["QuestionnaireID"],
                    QuestionnaireName = x["Name"] == DBNull.Value ? string.Empty : (string)x["Name"],
                    ReminderScheduleName = x["ReminderScheduleName"] == DBNull.Value ? string.Empty : (string)x["ReminderScheduleName"],
                    InstrumentAbbrev = x["Abbrev"] == DBNull.Value ? string.Empty : (string)x["Abbrev"],
                    AssessmentReasonName = x["AssessmentReasonName"] == DBNull.Value ? string.Empty : (string)x["AssessmentReasonName"],
                    DueDate = x["WindowDueDate"] == DBNull.Value ? DateTime.MinValue : (DateTime)x["WindowDueDate"],
                    LeadHelperName = x["LeadHelperName"] == DBNull.Value ? string.Empty : (string)x["LeadHelperName"],
                    IsEmailInviteToCompleteReminders = x["IsEmailInviteToCompleteReminders"] == DBNull.Value ? false : (bool)x["IsEmailInviteToCompleteReminders"],
                    IsEmailRemindersHelpers = x["IsEmailRemindersHelpers"] == DBNull.Value ? false : (bool)x["IsEmailRemindersHelpers"],
                    PersonQuestionnaireID = x["PersonQuestionnaireID"] == DBNull.Value ? 0 : (long)x["PersonQuestionnaireID"],
                }).ToList();
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// To get details of NotifyReminder that scheduled for today.
        /// </summary>
        /// <returns>.ReminderNotificationsListDTO</returns>
        public int GetNotifyReminderScheduledCountForToday(DateTime lastRunTime, DateTime currentRunTime)
        {
            try
            {
                var query = string.Empty;

                query = $@"select count(NR.NotifyReminderID) as Count from PersonQuestionnaireSchedule PQS
                          join NotifyReminder NR on PQS.PersonQuestionnaireScheduleID = NR.PersonQuestionnaireScheduleID
                          join PersonQuestionnaire PQ on PQS.PersonQuestionnaireID = PQ.PersonQuestionnaireID
                          join Questionnaire Q on PQ.QuestionnaireID = Q.QuestionnaireID 
                          join info.Instrument I on Q.InstrumentID = I.InstrumentID
                          join QuestionnaireWindow QW on PQS.QuestionnaireWindowID = QW.QuestionnaireWindowID
                          join info.AssessmentReason AR on QW.AssessmentReasonID = AR.AssessmentReasonID
                          where PQS.IsRemoved =0 AND NR.IsLogAdded = 0 AND NR.NotifyDate 
                          BETWEEN CAST('{lastRunTime}' AS datetime2) and CAST('{currentRunTime}' AS datetime2) ";

                var data = ExecuteSqlQuery(query, x => new ReminderNotificationsListDTO
                {
                    Count = x["Count"] == DBNull.Value ? 0 : (int)x["Count"],
                }).FirstOrDefault();
                return data.Count;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// To get details PersonQuestionnaire.
        /// </summary>
        /// <param id="int">id.</param>
        /// <returns>.PersonQuestionnaireDTO</returns>
        public async Task<List<PersonQuestionnaireSchedule>> GetPersonQuestionnaireScheduleByWindowList(long personQuestionnaireID, List<int> questionnaireWindowID)
        {
            try
            {
                var personQuestionnaireSchedule = await this.GetAsync(x => x.PersonQuestionnaireID == personQuestionnaireID && questionnaireWindowID.Contains(x.QuestionnaireWindowID) && !x.IsRemoved);
                return personQuestionnaireSchedule.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// To get details of NotifyReminder that scheduled for today.
        /// </summary>
        /// <param id="int">id.</param>
        /// <returns>.PersonQuestionnaireDTO</returns>
        public async Task<List<PersonQuestionnaireSchedule>> GetPersonQuestionnaireScheduleList(DateTime dateTaken, List<int> questionnaireWindowIDs, long personQuestionnaireID)
        {
            try
            {
                var res = from PQS in this._dbContext.PersonQuestionnaireSchedules
                          join NR in this._dbContext.NotifyReminder on PQS.PersonQuestionnaireScheduleID equals NR.PersonQuestionnaireScheduleID
                          join PQ in this._dbContext.PersonQuestionnaires on PQS.PersonQuestionnaireID equals PQ.PersonQuestionnaireID
                          where questionnaireWindowIDs.Contains(PQS.QuestionnaireWindowID) && PQS.PersonQuestionnaireID == personQuestionnaireID
                          select PQS;
                return res.Distinct().ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// To get  all person questionnaire schedules.
        /// </summary>
        /// <returns>.PersonQuestionnaireRegularScheduleDetailsDTO</returns>
        public List<PersonQuestionnaireRegularScheduleDetailsDTO> GetAllPersonQuestionnairesRegularSchedule(string reminderLimtForFuture)
        {
            try
            {
                var query = string.Empty;
                reminderLimtForFuture = string.IsNullOrWhiteSpace(reminderLimtForFuture) ? "0" : reminderLimtForFuture;
                var reasonQuery = @$"Select AssessmentReasonID from info.AssessmentReason where Name ='{PCISEnum.AssessmentReason.Scheduled}'";
                query = @$"WITH CTE AS (select PQS.PersonQuestionnaireID,PQS.QuestionnaireWindowID , MAX(WindowDueDate) CurrentlyScheduled , 
                          DATEADD(DD, MAX(QW.repeatIntervalDays) , MAX(WindowDueDate))  ToBeScheduled , DATEADD(MM,{reminderLimtForFuture},getdate()) 
                          as TwelveMonthsFromToday,QW.WindowOpenOffsetDays,QW.WindowCloseOffsetDays,QW.RepeatIntervalDays,QW.IsSelected,PQ.QuestionnaireID
                          from personquestionnaireschedule  PQS
                          join PersonQuestionnaire PQ ON PQS.PersonQuestionnaireID=PQ.PersonQuestionnaireID  and  PQ.IsRemoved=0
                          join PersonCollaboration PC ON PQ.PersonID=PC.PersonID and PQ.CollaborationID=PC.CollaborationID
                          JOIn QuestionnaireWindow QW ON PQS.QuestionnaireWindowID = QW.QuestionnaireWindowID and Qw.IsSelected= 1
                          where PC.EndDate IS NULL and QW.AssessmentReasonID In ({reasonQuery}) and PQS.IsRemoved=0  group by PQS.PersonQuestionnaireID, PQS.QuestionnaireWindowID ,QW.WindowOpenOffsetDays , QW.WindowCloseOffsetDays,QW.RepeatIntervalDays,QW.IsSelected,PQ.QuestionnaireID) SELECT * FROM CTE WHERE  ToBeScheduled < TwelveMonthsFromToday AND CAST(ToBeScheduled AS DATE) > CAST(GETDATE() AS DATE)";

                var data = ExecuteSqlQuery(query, x => new PersonQuestionnaireRegularScheduleDetailsDTO
                {
                    PersonQuestionnaireID = x["PersonQuestionnaireID"] == DBNull.Value ? 0 : (long)x["PersonQuestionnaireID"],
                    QuestionnaireWindowID = x["QuestionnaireWindowID"] == DBNull.Value ? 0 : (int)x["QuestionnaireWindowID"],
                    QuestionnaireID = x["QuestionnaireID"] == DBNull.Value ? 0 : (int)x["QuestionnaireID"],
                    CurrentlyScheduled = x["CurrentlyScheduled"] == DBNull.Value ? DateTime.MinValue : (DateTime)x["CurrentlyScheduled"],
                    ToBeScheduled = x["ToBeScheduled"] == DBNull.Value ? DateTime.MinValue : (DateTime)x["ToBeScheduled"],
                    TwelveMonthsFromToday = x["TwelveMonthsFromToday"] == DBNull.Value ? DateTime.MinValue : (DateTime)x["TwelveMonthsFromToday"],
                    WindowOpenOffsetDays= x["WindowOpenOffsetDays"] == DBNull.Value ? 0 : (int)x["WindowOpenOffsetDays"],
                    WindowCloseOffsetDays=x["WindowCloseOffsetDays"] == DBNull.Value ? 0 : (int)x["WindowCloseOffsetDays"],
                    RepeatIntervalDays=x["RepeatIntervalDays"] == DBNull.Value ? 0 : (int)x["RepeatIntervalDays"],
                    IsSelected = x["IsSelected"] == DBNull.Value ? false : (bool)x["IsSelected"],  
                }).ToList();
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// AddBulkPersonQuestionnaireSchedule
        /// </summary>
        /// <param name="personQuestionnaireSchedule"></param>
        /// <returns></returns>
        public List<PersonQuestionnaireSchedule> AddBulkPersonQuestionnaireSchedule(List<PersonQuestionnaireSchedule> personQuestionnaireSchedule)
        {
            try
            {
                if (personQuestionnaireSchedule.Count > 0)
                {
                    personQuestionnaireSchedule?.ForEach(x => x.UpdateDate = DateTime.Now);
                    var res = this.AddBulkAsync(personQuestionnaireSchedule);
                    res.Wait();
                }
                return personQuestionnaireSchedule;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<PersonQuestionnaireSchedule> GetAllPersonQuestionnaireScheduleID(List<Guid?> personQuestionnaireScheduleIndex)
        {
            try
            {
                var query = @$"SELECT PersonQuestionnaireScheduleID, PersonQuestionnaireScheduleIndex FROM PersonQuestionnaireSchedule WHERE PersonQuestionnaireScheduleIndex in ('{string.Join("','", personQuestionnaireScheduleIndex)}')";

                var data = ExecuteSqlQuery(query, x => new PersonQuestionnaireSchedule
                {
                    PersonQuestionnaireScheduleID = x["PersonQuestionnaireScheduleID"] == DBNull.Value ? 0 : (long)x["PersonQuestionnaireScheduleID"],
                    PersonQuestionnaireScheduleIndex = x["PersonQuestionnaireScheduleIndex"] == DBNull.Value ? null : (Guid?)x["PersonQuestionnaireScheduleIndex"],
                }).ToList();
                return data;
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// GetAllRegularSchedulesWithWindowOpenToday.
        /// Fetch the schedules for FutureReminder Process to do next schedule.
        /// Fetch  the last occurrence schedule for each personQuestinnaireID with its min notifydate and max duedate.
        /// </summary>
        /// <returns>.PersonQuestionnaireRegularScheduleDetailsDTO</returns>
        public List<PersonQuestionnaireRegularScheduleDetailsDTO> GetAllRegularSchedulesWithWindowOpenToday()
        {
            try
            {
                var query = string.Empty;
                var reasonQuery = @$"Select AssessmentReasonID from info.AssessmentReason where Name ='{PCISEnum.AssessmentReason.Scheduled}'";                
                query = @$";WITH CTE AS(
                                SELECT ROW_NUMBER() OVER(PARTITION BY PQS.PersonQuestionnaireID ORDER BY PQS.OccurrenceCounter DESC) AS ROWNO,PQS.PersonQuestionnaireID, MIN(NR.NotifyDate) AS NotifyDate,  OccurrenceCounter,MIN(NR.NotifyReminderID) NotifyReminderID,MAX(PQS.WindowDueDate)      WindowDueDate,QW.QuestionnaireID,QW.CloseOffsetTypeID,QW.OpenOffsetTypeID,                  QW.QuestionnaireWindowID, QW.WindowOpenOffsetDays, QW.WindowCloseOffsetDays,QW.IsSelected,QRR.RecurrenceRangeEndTypeID, 
                                    QRR.RecurrenceRangeEndInNumber, QRR.RecurrenceRangeEndDate
			                 FROM PersonQuestionnaireSchedule PQS
			                      JOIN NotifyReminder NR ON PQS.PersonQuestionnaireScheduleID = NR.PersonQuestionnaireScheduleID
					              JOIN QuestionnaireWindow QW ON PQS.QuestionnaireWindowID = QW.QuestionnaireWindowID and Qw.IsSelected = 1 
					              JOIN QuestionnaireRegularReminderRecurrence QRR ON QW.QuestionnaireID = QRR.QuestionnaireID AND QRR.IsRemoved = 0
			                WHERE PQS.IsRemoved = 0 AND NR.IsRemoved =0 
					              AND CAST(NR.NotifyDate AS DATE) >= CAST(GETDATE() AS DATE) 
					              AND QW.AssessmentReasonID In({reasonQuery})
					              GROUP BY PQS.PersonQuestionnaireID, PQS.OccurrenceCounter,QW.QuestionnaireID,
					              QRR.RecurrenceRangeEndTypeID, QRR.RecurrenceRangeEndInNumber, QRR.RecurrenceRangeEndDate, QW.QuestionnaireID,QW.CloseOffsetTypeID,QW.OpenOffsetTypeID,QW.QuestionnaireWindowID,
                                  QW.WindowOpenOffsetDays, QW.WindowCloseOffsetDays,QW.IsSelected
		                     )SELECT CTE.PersonQuestionnaireID,CTE.QuestionnaireWindowID,CTE.QuestionnaireID,CTE.WindowOpenOffsetDays,      CTE.WindowCloseOffsetDays,CTE.IsSelected, CTE.NotifyDate, CTE.WindowDueDate, CTE.OccurrenceCounter,  CTE.NotifyReminderID,CTE.CloseOffsetTypeID,CTE.OpenOffsetTypeID
		                       FROM CTE
			                    JOIN info.RecurrenceEndType RE ON CTE.RecurrenceRangeEndTypeID = RE.RecurrenceEndTypeID
			                   WHERE ((RE.Name = '{PCISEnum.RecurrenceEndType.EndByEndate}' AND CTE.WindowDueDate <= CTE.RecurrenceRangeEndDate) 
					            OR  (RE.Name = '{PCISEnum.RecurrenceEndType.EndByNumberOfOccurences}' AND CTE.OccurrenceCounter + 1 <= CTE.RecurrenceRangeEndInNumber) 
                                OR (RE.Name = '{PCISEnum.RecurrenceEndType.EndByNoEndate}' )) AND RowNo = 1;";
                var data = ExecuteSqlQuery(query, x => new PersonQuestionnaireRegularScheduleDetailsDTO
                {
                    PersonQuestionnaireID = x["PersonQuestionnaireID"] == DBNull.Value ? 0 : (long)x["PersonQuestionnaireID"],
                    QuestionnaireWindowID = x["QuestionnaireWindowID"] == DBNull.Value ? 0 : (int)x["QuestionnaireWindowID"],
                    QuestionnaireID = x["QuestionnaireID"] == DBNull.Value ? 0 : (int)x["QuestionnaireID"],
                    WindowOpenOffsetDays = x["WindowOpenOffsetDays"] == DBNull.Value ? 0 : (int)x["WindowOpenOffsetDays"],
                    WindowCloseOffsetDays = x["WindowCloseOffsetDays"] == DBNull.Value ? 0 : (int)x["WindowCloseOffsetDays"],
                    IsSelected = x["IsSelected"] == DBNull.Value ? false : (bool)x["IsSelected"],
                    CloseOffsetTypeID = char.Parse((string)x["CloseOffsetTypeID"]),
                    OpenOffsetTypeID = char.Parse((string)x["OpenOffsetTypeID"]),
                    OccurrenceCounter = x["OccurrenceCounter"] == DBNull.Value ? 0 : (int)x["OccurrenceCounter"],
                    WindowDueDate = (DateTime)x["WindowDueDate"]
                }).ToList();
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetAllLatestSchedulesWithMaXDueDate.
        /// Fetch the schedules for FutureReminder Process to do next schedule.
        /// Fetch  the last occurrence schedule for each personQuestinnaireID with its max duedate for a set of personQuestionnaireIDs.
        /// </summary>
        /// <returns>.PersonQuestionnaireRegularScheduleDetailsDTO</returns>
        public List<PersonQuestionnaireRegularScheduleDetailsDTO> GetAllLatestSchedulesWithMaXDueDate(List<long> lst_PersonQuestionnaireIds)
        {
            try
            {
                var query = string.Empty;
                var reasonQuery = @$"Select AssessmentReasonID from info.AssessmentReason where Name ='{PCISEnum.AssessmentReason.Scheduled}'";
                query = @$"WITH MAxDueDateWithMAXOcurrence AS(
			                SELECT  PQS.PersonQuestionnaireID, MAX(PQS.OccurrenceCounter) OccurrenceCounter,PQ.QuestionnaireID,
					        MAX(WindowDueDate) AS WindowDueDate,MAX(QW.WindowOpenOffsetDays) WindowOpenOffsetDays, 
					        		 MAX(QW.WindowCloseOffsetDays) WindowCloseOffsetDays,QW.IsSelected, MAX(QW.QuestionnaireWindowID) QuestionnaireWindowID,
					        		MAX(QW.CloseOffsetTypeID) CloseOffsetTypeID,MAX(QW.OpenOffsetTypeID)OpenOffsetTypeID
					        FROM PersonQuestionnaireSchedule PQS
					        JOIN PersonQuestionnaire PQ ON PQS.PersonQuestionnaireID = PQ.PersonQuestionnaireID
					        JOIN QuestionnaireWindow QW ON PQS.QuestionnaireWindowID = QW.QuestionnaireWindowID and Qw.IsSelected = 1					 
					        WHERE  PQS.IsRemoved = 0 AND PQ.IsRemoved = 0 
					        AND QW.AssessmentReasonID In({reasonQuery}) 
					        AND Pq.PersonQuestionnaireID in ({string.Join(',', lst_PersonQuestionnaireIds)}) 
					        GROUP BY PQS.PersonQuestionnaireID,QW.IsSelected,PQ.QuestionnaireID
                          ) SELECT CTE.PersonQuestionnaireID,CTE.QuestionnaireWindowID,CTE.QuestionnaireID,CTE.WindowOpenOffsetDays,   
							 CTE.WindowCloseOffsetDays,CTE.IsSelected, CTE.WindowDueDate, CTE.OccurrenceCounter, 
							 CTE.CloseOffsetTypeID,CTE.OpenOffsetTypeID,CTE.IsSelected,
							 QRR.RecurrenceRangeEndTypeID,QRR.RecurrenceRangeEndInNumber,QRR.RecurrenceRangeEndDate
		                       FROM MAxDueDateWithMAXOcurrence CTE 							   
						     JOIN QuestionnaireRegularReminderRecurrence QRR ON CTE.QuestionnaireID = QRR.QuestionnaireID AND QRR.IsRemoved = 0
			                 JOIN info.RecurrenceEndType RE ON QRR.RecurrenceRangeEndTypeID = RE.RecurrenceEndTypeID
			                 WHERE ((RE.Name = '{PCISEnum.RecurrenceEndType.EndByEndate}' 
                                        AND CAST(CTE.WindowDueDate AS DATE) <= CAST(QRR.RecurrenceRangeEndDate AS DATE))  
					            OR  (RE.Name = '{PCISEnum.RecurrenceEndType.EndByNumberOfOccurences}' 
                                            AND (CTE.OccurrenceCounter + 1) <= QRR.RecurrenceRangeEndInNumber) 
                                OR (RE.Name = '{PCISEnum.RecurrenceEndType.EndByNoEndate}' ))";
                var data = ExecuteSqlQuery(query, x => new PersonQuestionnaireRegularScheduleDetailsDTO
                {
                    PersonQuestionnaireID = x["PersonQuestionnaireID"] == DBNull.Value ? 0 : (long)x["PersonQuestionnaireID"],
                    QuestionnaireWindowID = x["QuestionnaireWindowID"] == DBNull.Value ? 0 : (int)x["QuestionnaireWindowID"],
                    QuestionnaireID = x["QuestionnaireID"] == DBNull.Value ? 0 : (int)x["QuestionnaireID"],
                    WindowOpenOffsetDays = x["WindowOpenOffsetDays"] == DBNull.Value ? 0 : (int)x["WindowOpenOffsetDays"],
                    WindowCloseOffsetDays = x["WindowCloseOffsetDays"] == DBNull.Value ? 0 : (int)x["WindowCloseOffsetDays"],
                    IsSelected = x["IsSelected"] == DBNull.Value ? false : (bool)x["IsSelected"],
                    CloseOffsetTypeID = char.Parse((string)x["CloseOffsetTypeID"]),
                    OpenOffsetTypeID = char.Parse((string)x["OpenOffsetTypeID"]),
                    OccurrenceCounter = x["OccurrenceCounter"] == DBNull.Value ? 0 : (int)x["OccurrenceCounter"],
                    WindowDueDate = (DateTime)x["WindowDueDate"]
                }).ToList();
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<PersonQuestionnaireDTO> GetAllPersonQuestionnairesToBeScheduled()
        {
            try
            {
                var query = string.Empty;
                var reasonQuery = @$"Select AssessmentReasonID from info.AssessmentReason where Name ='{PCISEnum.AssessmentReason.Scheduled}'";
                query = @$"SELECT * INTO #PersonQuestionnireWindowCTE FROM (
                            SELECT distinct PQ.PersonQuestionnaireID, PQ.QuestionnaireID,QW.QuestionnaireWindowID
                            from Person P
                            JOIN PersonQuestionnaire PQ ON PQ.personID = P.personID
                            JOIN questionnaire Q ON PQ.QuestionnaireID = Q.QuestionnaireID
                            JOIN QuestionnaireWindow QW ON Q.QuestionnaireID = QW.QuestionnaireID 
                                            AND QW.AssessmentReasonID IN ({reasonQuery})  AND QW.IsSelected = 1 
                            JOIN QuestionnaireRegularReminderRecurrence QRR ON Q.QuestionnaireID = QRR.QuestionnaireID 
                                            AND QRR.IsRemoved = 0 AND Q.IsBaseQuestionnaire = 0
                            WHERE P.IsRemoved = 0 AND PQ.IsRemoved = 0 AND Q.IsRemoved = 0 
                          )AS A 
                          SELECT * INTO #PersonQuestionnaireScheduleFiltered FROM (
                            SELECT distinct PQS.PersonQuestionnaireID
                                FROM PersonQuestionnaireSchedule PQS 
                            	 WHERE PQS.IsRemoved = 0 
                            	 ) AS B
                          SELECT distinct PQS.PersonQuestionnaireID,PQW.QuestionnaireID
                                FROM #PersonQuestionnireWindowCTE PQW
                            	JOIN #PersonQuestionnaireScheduleFiltered PQS ON PQW.PersonQuestionnaireID = PQS.PersonQuestionnaireID";

               var data = ExecuteSqlQuery(query, x => new PersonQuestionnaireDTO
                {
                    PersonQuestionnaireID = x["PersonQuestionnaireID"] == DBNull.Value ? 0 : (long)x["PersonQuestionnaireID"],
                    QuestionnaireID = x["QuestionnaireID"] == DBNull.Value ? 0 : (int)x["QuestionnaireID"],
                }).ToList();
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<PersonQuestionnaireSchedule> GetPersonQuestionnaireSchedule(List<long> personQuestionnaireIDs)
        {
            try
            {
                var personQuestionnaireSchedule = this.GetAsync(x => personQuestionnaireIDs.Contains(x.PersonQuestionnaireID) && !x.IsRemoved).Result;
                return personQuestionnaireSchedule.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
