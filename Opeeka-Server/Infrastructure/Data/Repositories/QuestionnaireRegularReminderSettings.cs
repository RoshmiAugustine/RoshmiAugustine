using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Infrastructure.Data.Repositories.Base;

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{
    public class QuestionnaireRegularReminderRecurrenceRepository : BaseRepository<QuestionnaireRegularReminderRecurrence>, IQuestionnaireRegularReminderRecurrenceRepository
    {
        private readonly IMapper mapper;
        private readonly ILogger<QuestionnaireRegularReminderRecurrenceRepository> logger;
        private readonly OpeekaDBContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="QuestionnaireReminderRule  "/> class.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public QuestionnaireRegularReminderRecurrenceRepository(ILogger<QuestionnaireRegularReminderRecurrenceRepository> logger, OpeekaDBContext dbContext, IMapper mapper)
            : base(dbContext)
        {
            this.mapper = mapper;
            this.logger = logger;
            this._dbContext = dbContext;
        }

        public int AddQuestionnaireReminderRecurrence(QuestionnaireRegularReminderRecurrenceDTO regularReminderRecurrenceDTO)
        {
            try
            {
                QuestionnaireRegularReminderRecurrence regularReminderRecurrence = new QuestionnaireRegularReminderRecurrence();
                this.mapper.Map<QuestionnaireRegularReminderRecurrenceDTO, QuestionnaireRegularReminderRecurrence>(regularReminderRecurrenceDTO, regularReminderRecurrence);
                var result = this.AddAsync(regularReminderRecurrence).Result.QuestionnaireRegularReminderRecurrenceID;
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public int UpdateQuestionnaireReminderRecurrence(QuestionnaireRegularReminderRecurrenceDTO regularReminderRecurrenceDTO)
        {
            try
            {
                QuestionnaireRegularReminderRecurrence regularReminderRecurrence = new QuestionnaireRegularReminderRecurrence();
                this.mapper.Map<QuestionnaireRegularReminderRecurrenceDTO, QuestionnaireRegularReminderRecurrence>(regularReminderRecurrenceDTO, regularReminderRecurrence);
                var result = this.UpdateAsync(regularReminderRecurrence).Result.QuestionnaireRegularReminderRecurrenceID;
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public QuestionnaireRegularReminderRecurrenceDTO GetQuestionnaireRegularReminderRecurrence(int questionnaireId)
        {
            try
            {
                var query = string.Empty;
                query = $@"SELECT 
		                        QR.QuestionnaireRegularReminderRecurrenceID,QR.RecurrencePatternID,
                                QR.RecurrenceInterval, QR.RecurrenceOrdinalIDs,QR.RecurrenceDayNameIDs,
		                        QR.RecurrenceDayNoOfMonth,QR.RecurrenceMonthIDs, QR.RecurrenceRangeStartDate,
                                QR.RecurrenceRangeEndTypeID,QR.RecurrenceRangeStartDate, QR.RecurrenceRangeEndDate,
		                        QR.RecurrenceRangeStartDate,QR.RecurrenceRangeEndInNumber,RP.GroupName PatternGroup,
                                QR.QuestionnaireWindowID
	                        FROM QuestionnaireRegularReminderRecurrence QR
							JOIN INFO.RecurrencePattern RP ON RP.RecurrencePatternID = QR.RecurrencePatternID
	                        WHERE QR.QuestionnaireID = {questionnaireId} AND QR.IsRemoved = 0";
                var data = ExecuteSqlQuery(query, x => new QuestionnaireRegularReminderRecurrenceDTO
                {
                    QuestionnaireRegularReminderRecurrenceID = (int)x["QuestionnaireRegularReminderRecurrenceID"],
                    QuestionnaireID = questionnaireId,
                    QuestionnaireWindowID = x["QuestionnaireWindowID"] == DBNull.Value ? null : (int?)x["QuestionnaireWindowID"],
                    RecurrenceDayNameIDs = x["RecurrenceDayNameIDs"] == DBNull.Value ? null : (string)x["RecurrenceDayNameIDs"],
                    RecurrenceDayNoOfMonth = x["RecurrenceDayNoOfMonth"] == DBNull.Value ? null : (int?)x["RecurrenceDayNoOfMonth"],
                    RecurrenceMonthIDs = x["RecurrenceMonthIDs"] == DBNull.Value ? null : (string)x["RecurrenceMonthIDs"],
                    RecurrenceOrdinalIDs = x["RecurrenceOrdinalIDs"] == DBNull.Value ? null : (string)x["RecurrenceOrdinalIDs"],
                    RecurrencePatternID = x["RecurrencePatternID"] == DBNull.Value ? 0 : (int)x["RecurrencePatternID"],
                    RecurrenceRangeEndDate = x["RecurrenceRangeEndDate"] == DBNull.Value ? null : (DateTime?)x["RecurrenceRangeEndDate"],
                    RecurrenceRangeEndInNumber = x["RecurrenceRangeEndInNumber"] == DBNull.Value ? 0 : (int?)x["RecurrenceRangeEndInNumber"],
                    RecurrenceRangeEndTypeID = x["RecurrenceRangeEndTypeID"] == DBNull.Value ? 0 : (int)x["RecurrenceRangeEndTypeID"],
                    RecurrenceRangeStartDate = (DateTime)x["RecurrenceRangeStartDate"],
                    RecurrenceInterval = x["RecurrenceInterval"] == DBNull.Value ? 0 : (int)x["RecurrenceInterval"],
                    PatternGroup = x["PatternGroup"] == DBNull.Value ? string.Empty : (string)x["PatternGroup"],
                }).FirstOrDefault();
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<QuestionnaireRegularReminderRecurrenceDTO> GetRegularReminderSettingsForQuestionnaires(List<int> questionnaireIds)
        {
            try
            {
                var query = string.Empty;
                query = $@"SELECT 
		                        QR.QuestionnaireID, QR.QuestionnaireRegularReminderRecurrenceID,QR.RecurrencePatternID,QR.RecurrenceInterval,QR.RecurrenceOrdinalIDs,QR.RecurrenceDayNameIDs,QR.RecurrenceDayNoOfMonth,QR.RecurrenceMonthIDs, QR.RecurrenceRangeStartDate,QR.RecurrenceRangeEndTypeID,QR.RecurrenceRangeStartDate, QR.RecurrenceRangeEndDate, QR.RecurrenceRangeStartDate,QR.RecurrenceRangeEndInNumber, RP.Name AS RecurrencePattern, RP.GroupName AS PatternGroup,RE.Name AS RecurrenceRangeEndType,
                                  RecurrenceDayNames = (SELECT Name FROM info.RecurrenceDay where RecurrenceDayID IN (select CONVERT(int,value) from string_split(RecurrenceDayNameIDs, ',')) order by listorder FOR JSON PATH),
                                  RecurrenceMonths = (SELECT Name FROM info.RecurrenceMonth where RecurrenceMonthID IN (select CONVERT(int,value) from string_split(RecurrenceMonthIDs, ',')) order by listorder FOR JSON PATH),
                                  RecurrenceOrdinals = (SELECT Name FROM info.RecurrenceOrdinal where RecurrenceOrdinalID IN (select CONVERT(int,value) from string_split(RecurrenceOrdinalIDs, ',')) order by listorder FOR JSON PATH)
	                        FROM QuestionnaireRegularReminderRecurrence QR
                                 JOIN info.RecurrencePattern RP ON QR.RecurrencePatternID = RP.RecurrencePatternID
                                 JOIN info.RecurrenceEndType RE ON QR.RecurrenceRangeEndTypeID = RE.RecurrenceEndTypeID
	                        WHERE QR.QuestionnaireID in ({string.Join(",", questionnaireIds)}) AND QR.IsRemoved = 0";
                var data = ExecuteSqlQuery(query, x => new QuestionnaireRegularReminderRecurrenceDTO
                {
                    QuestionnaireID = x["QuestionnaireID"] == DBNull.Value ? 0 : (int)x["QuestionnaireID"],
                    RecurrenceDayNameIDs = x["RecurrenceDayNameIDs"] == DBNull.Value ? null : (string)x["RecurrenceDayNameIDs"],
                    RecurrenceDayNoOfMonth = x["RecurrenceDayNoOfMonth"] == DBNull.Value ? 0 : (int?)x["RecurrenceDayNoOfMonth"],
                    RecurrenceMonthIDs = x["RecurrenceMonthIDs"] == DBNull.Value ? null : (string)x["RecurrenceMonthIDs"],
                    RecurrenceOrdinalIDs = x["RecurrenceOrdinalIDs"] == DBNull.Value ? null : (string)x["RecurrenceOrdinalIDs"],
                    RecurrencePatternID = x["RecurrencePatternID"] == DBNull.Value ? 0 : (int)x["RecurrencePatternID"],
                    RecurrenceRangeEndDate = x["RecurrenceRangeEndDate"] == DBNull.Value ? null : (DateTime?)x["RecurrenceRangeEndDate"],
                    RecurrenceRangeEndInNumber = x["RecurrenceRangeEndInNumber"] == DBNull.Value ? 0 : (int?)x["RecurrenceRangeEndInNumber"],
                    RecurrenceRangeEndTypeID = x["RecurrenceRangeEndTypeID"] == DBNull.Value ? 0 : (int)x["RecurrenceRangeEndTypeID"],
                    RecurrenceRangeStartDate = (DateTime)x["RecurrenceRangeStartDate"],
                    RecurrencePattern = x["RecurrencePattern"] == DBNull.Value ? "" : (string)x["RecurrencePattern"],
                    PatternGroup = x["PatternGroup"] == DBNull.Value ? "" : (string)x["PatternGroup"],
                    RecurrenceRangeEndType = x["RecurrenceRangeEndType"] == DBNull.Value ? "" : (string)x["RecurrenceRangeEndType"],
                    RecurrenceInterval = x["RecurrenceInterval"] == DBNull.Value ? 0 : (int)x["RecurrenceInterval"],
                    RecurrenceDayNames = x["RecurrenceDayNames"] == DBNull.Value ? "" : (string)x["RecurrenceDayNames"],
                    RecurrenceMonths = x["RecurrenceMonths"] == DBNull.Value ? "" : (string)x["RecurrenceMonths"],
                    RecurrenceOrdinals = x["RecurrenceOrdinals"] == DBNull.Value ? "" : (string)x["RecurrenceOrdinals"],
                });
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
    public class QuestionnaireRegularReminderTimeRuleRepository : BaseRepository<QuestionnaireRegularReminderTimeRule>, IQuestionnaireRegularReminderTimeRuleRepository
    {
        private readonly IMapper mapper;
        private readonly ILogger<QuestionnaireRegularReminderTimeRuleRepository> logger;
        private readonly OpeekaDBContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="QuestionnaireReminderRule  "/> class.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public QuestionnaireRegularReminderTimeRuleRepository(ILogger<QuestionnaireRegularReminderTimeRuleRepository> logger, OpeekaDBContext dbContext, IMapper mapper)
            : base(dbContext)
        {
            this.mapper = mapper;
            this.logger = logger;
            this._dbContext = dbContext;
        }

        public void AddReminderTimeRule(List<QuestionnaireRegularReminderTimeRuleDTO> regularReminderTimeRuleDTO)
        {
            try
            {
                if (regularReminderTimeRuleDTO.Count > 0)
                {
                    List<QuestionnaireRegularReminderTimeRule> regularReminderTimeRule = new List<QuestionnaireRegularReminderTimeRule>();
                    this.mapper.Map<List<QuestionnaireRegularReminderTimeRuleDTO>, List<QuestionnaireRegularReminderTimeRule>>(regularReminderTimeRuleDTO, regularReminderTimeRule);
                    var result = this.AddBulkAsync(regularReminderTimeRule);
                    result.Wait();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateReminderTimeRule(List<QuestionnaireRegularReminderTimeRuleDTO> regularRminderTimeRuleDTO)
        {
            try
            {
                if (regularRminderTimeRuleDTO.Count > 0)
                {
                    List<QuestionnaireRegularReminderTimeRule> regularRminderTimeRule = new List<QuestionnaireRegularReminderTimeRule>();
                    this.mapper.Map<List<QuestionnaireRegularReminderTimeRuleDTO>, List<QuestionnaireRegularReminderTimeRule>>(regularRminderTimeRuleDTO, regularRminderTimeRule);
                    var result = this.UpdateBulkAsync(regularRminderTimeRule);
                    result.Wait();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<QuestionnaireRegularReminderTimeRuleDTO> GetQuestionnaireRegularReminderTimeRule(int questionnaireId)
        {
            try
            {
                var query = string.Empty;
                query = $@"SELECT 
		                        QR.QuestionnaireRegularReminderTimeRuleID,
		                        QR.[Hour] Hour,QR.[Minute] Minute,QR.AMorPM,QR.TimeZonesID, T.Name AS TimeZoneName
	                        FROM QuestionnaireRegularReminderTimeRule QR
							JOIN info.TimeZones T on QR.TimeZonesID=T.TimeZonesID
	                        WHERE QR.QuestionnaireID = {questionnaireId} AND QR.IsRemoved = 0";
                var data = ExecuteSqlQuery(query, x => new QuestionnaireRegularReminderTimeRuleDTO
                {
                    QuestionnaireID = questionnaireId,
                    QuestionnaireRegularReminderTimeRuleID = x["QuestionnaireRegularReminderTimeRuleID"] == DBNull.Value ? 0 : (int)x["QuestionnaireRegularReminderTimeRuleID"],
                    Hour = x["Hour"] == DBNull.Value ? "" : (string)x["Hour"],
                    Minute = x["Minute"] == DBNull.Value ? "" : (string)x["Minute"],
                    AMorPM = x["AMorPM"] == DBNull.Value ? "" : (string)x["AMorPM"],
                    TimeZonesID = x["TimeZonesID"] == DBNull.Value ? 0 : (int)x["TimeZonesID"],
                });
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<QuestionnaireRegularReminderTimeRuleDTO> GetRegularReminderTimeRuleForQuestionnaires(List<int> questionnaireIds)
        {
            try
            {
                var query = string.Empty;
                query = $@"SELECT 
		                        QR.QuestionnaireRegularReminderTimeRuleID,QR.QuestionnaireID,
		                        QR.[Hour] Hour,QR.[Minute] Minute,QR.AMorPM,QR.TimeZonesID,T.Name AS TimeZoneName
	                        FROM QuestionnaireRegularReminderTimeRule QR
							JOIN info.TimeZones T on QR.TimeZonesID=T.TimeZonesID
	                        WHERE QR.QuestionnaireID IN ({string.Join(",", questionnaireIds)}) AND QR.IsRemoved = 0";
                var data = ExecuteSqlQuery(query, x => new QuestionnaireRegularReminderTimeRuleDTO
                {
                    QuestionnaireID = x["QuestionnaireID"] == DBNull.Value ? 0 : (int)x["QuestionnaireID"],
                    QuestionnaireRegularReminderTimeRuleID = x["QuestionnaireRegularReminderTimeRuleID"] == DBNull.Value ? 0 : (int)x["QuestionnaireRegularReminderTimeRuleID"],
                    Hour = (string)x["Hour"],
                    Minute = (string)x["Minute"],
                    AMorPM = (string)x["AMorPM"],
                    TimeZonesID = x["TimeZonesID"] == DBNull.Value ? 0 : (int)x["TimeZonesID"],
                    TimeZoneName = x["TimeZoneName"] == DBNull.Value ? null : (string)x["TimeZoneName"]
                });
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}