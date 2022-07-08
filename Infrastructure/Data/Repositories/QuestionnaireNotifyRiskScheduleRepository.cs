// -----------------------------------------------------------------------
// <copyright file="QuestionnaireNotifyRiskScheduleRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Infrastructure.Data.Repositories.Base;

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{
    public class QuestionnaireNotifyRiskScheduleRepository : BaseRepository<QuestionnaireNotifyRiskSchedule>, IQuestionnaireNotifyRiskScheduleRepository
    {
        private readonly IMapper mapper;
        private readonly OpeekaDBContext _dbContext;
        public QuestionnaireNotifyRiskScheduleRepository(OpeekaDBContext dbContext, IMapper mapper)
            : base(dbContext)
        {
            this.mapper = mapper;
            this._dbContext = dbContext;
        }

        /// <summary>
        /// GetNotificationDetails.
        /// </summary>
        /// <param name="id">questionnaireid.</param>
        /// <returns>NotificationDetailsResponseDTO.</returns>
        public List<NotificationDetailsDTO> GetNotificationDetails(int id)
        {
            try
            {

                var query = @"select Q1.QuestionnaireNotifyRiskScheduleID, Q1.Name, Q1.QuestionnaireID, 
                            Q2.QuestionnaireNotifyRiskRuleID, Q2.Name AS RuleName, Q2.NotificationLevelID , 
                            Q3.QuestionnaireNotifyRiskRuleConditionID, Q3.QuestionnaireItemID, Q3.ComparisonOperator,
                            Q3.ComparisonValue, Q3.ListOrder, Q3.JoiningOperator,Q.IsAlertsHelpersManagers
                            from QuestionnaireNotifyRiskSchedule Q1
                            join Questionnaire Q on  Q.QuestionnaireID=Q1.QuestionnaireID
                            join QuestionnaireNotifyRiskRule Q2 on Q1.QuestionnaireNotifyRiskScheduleID=Q2.QuestionnaireNotifyRiskScheduleID and Q2.IsRemoved=0
                            join QuestionnaireNotifyRiskRuleCondition Q3 on Q2.QuestionnaireNotifyRiskRuleID=Q3.QuestionnaireNotifyRiskRuleID and Q3.IsRemoved=0
                            where  Q1.QuestionnaireID=" + id + @" and Q1.IsRemoved=0
                            Order by Q3.ListOrder";

                var data = ExecuteSqlQuery(query, x => new NotificationDetailsDTO
                {
                    QuestionnaireNotifyRiskScheduleID = x["QuestionnaireNotifyRiskScheduleID"] == DBNull.Value ? 0 : (int)x["QuestionnaireNotifyRiskScheduleID"],
                    NotificationName = x["Name"] == DBNull.Value ? null : (string)x["Name"],
                    QuestionnaireID = x["QuestionnaireID"] == DBNull.Value ? 0 : (int)x["QuestionnaireID"],
                    QuestionnaireNotifyRiskRuleID = x["QuestionnaireNotifyRiskRuleID"] == DBNull.Value ? 0 : (int)x["QuestionnaireNotifyRiskRuleID"],
                    RuleName = x["RuleName"] == DBNull.Value ? null : (string)x["RuleName"],
                    NotificationLevelID = x["NotificationLevelID"] == DBNull.Value ? 0 : (int)x["NotificationLevelID"],
                    QuestionnaireNotifyRiskRuleConditionID = x["QuestionnaireNotifyRiskRuleConditionID"] == DBNull.Value ? 0 : (int)x["QuestionnaireNotifyRiskRuleConditionID"],
                    QuestionnaireItemID = x["QuestionnaireItemID"] == DBNull.Value ? 0 : (int)x["QuestionnaireItemID"],
                    ComparisonOperator = x["ComparisonOperator"] == DBNull.Value ? null : (string)x["ComparisonOperator"],
                    ComparisonValue = x["ComparisonValue"] == DBNull.Value ? 0 : (decimal)x["ComparisonValue"],
                    ListOrder = x["ListOrder"] == DBNull.Value ? 0 : (int)x["ListOrder"],
                    JoiningOperator = x["JoiningOperator"] == DBNull.Value ? null : (string)x["JoiningOperator"],
                    IsAlertsHelpersManagers = x["IsAlertsHelpersManagers"] == DBNull.Value ? false : (bool)x["IsAlertsHelpersManagers"],
                });

                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetQuestionnaireNotifyRiskSchedule.
        /// </summary>
        /// <param name="questionnaireNotifyRiskScheduleID">QuestionnaireNotifyRiskScheduleID.</param>
        /// <returns>QuestionnaireNotifyRiskSchedule</returns>
        public async Task<QuestionnaireNotifyRiskSchedule> GetQuestionnaireNotifyRiskSchedule(int questionnaireNotifyRiskScheduleID)
        {

            try
            {
                QuestionnaireNotifyRiskSchedule questionnaireNotifyRiskSchedule = await this.GetRowAsync(x => x.QuestionnaireNotifyRiskScheduleID == questionnaireNotifyRiskScheduleID && !x.IsRemoved);
                return questionnaireNotifyRiskSchedule;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// UpdateQuestionnaireNotifyRiskSchedule.
        /// </summary>
        /// <param name="questionnaireNotifyRiskSchedule">QuestionnaireNotifyRiskSchedule.</param>
        /// <returns>QuestionnaireNotifyRiskScheduleDTO.</returns>
        public QuestionnaireNotifyRiskScheduleDTO UpdateQuestionnaireNotifyRiskSchedule(QuestionnaireNotifyRiskSchedule questionnaireNotifyRiskSchedule)
        {
            try
            {
                var result = this.UpdateAsync(questionnaireNotifyRiskSchedule).Result;
                QuestionnaireNotifyRiskScheduleDTO questionnaireNotifyRiskScheduleDTO = new QuestionnaireNotifyRiskScheduleDTO();
                this.mapper.Map<QuestionnaireNotifyRiskSchedule, QuestionnaireNotifyRiskScheduleDTO>(result, questionnaireNotifyRiskScheduleDTO);
                return questionnaireNotifyRiskScheduleDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// AddQuestionnaireNotifyRiskSchedule.
        /// </summary>
        /// <param name="QuestionnaireNotifyRiskScheduleDTO">QuestionnaireNotifyRiskScheduleDTO.</param>
        /// <returns>long</returns>
        public long AddQuestionnaireNotifyRiskSchedule(QuestionnaireNotifyRiskScheduleDTO QuestionnaireNotifyRiskScheduleDTO)
        {
            try
            {
                QuestionnaireNotifyRiskSchedule QuestionnaireNotifyRiskSchedule = new QuestionnaireNotifyRiskSchedule();
                this.mapper.Map(QuestionnaireNotifyRiskScheduleDTO, QuestionnaireNotifyRiskSchedule);
                var result = this.AddAsync(QuestionnaireNotifyRiskSchedule).Result.QuestionnaireNotifyRiskScheduleID;
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetQuestionnaireNotifyRiskScheduleByQuestionnaireID
        /// </summary>
        /// <param name="questionnaireID"></param>
        /// <returns>QuestionnaireNotifyRiskSchedule</returns>
        public async Task<QuestionnaireNotifyRiskSchedule> GetQuestionnaireNotifyRiskScheduleByQuestionnaireID(int questionnaireID)
        {
            try
            {
                QuestionnaireNotifyRiskSchedule questionnaireNotifyRiskSchedules = await this.GetRowAsync(x => x.QuestionnaireID == questionnaireID && !x.IsRemoved);
                return questionnaireNotifyRiskSchedules;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetQuestionnaireNotifyRiskScheduleByQuestionnaire
        /// </summary>
        /// <param name="questionnaireID"></param>
        /// <returns>QuestionnaireNotifyRiskSchedulesDTO</returns>
        public async Task<QuestionnaireNotifyRiskSchedulesDTO> GetQuestionnaireNotifyRiskScheduleByQuestionnaire(int questionnaireID)
        {
            try
            {
                QuestionnaireNotifyRiskSchedulesDTO questionnaireNotifyRiskSchedulesDTO = new QuestionnaireNotifyRiskSchedulesDTO();
                QuestionnaireNotifyRiskSchedule questionnaireNotifyRiskSchedules = await this.GetRowAsync(x => x.QuestionnaireID == questionnaireID && !x.IsRemoved);
                this.mapper.Map<QuestionnaireNotifyRiskSchedule, QuestionnaireNotifyRiskSchedulesDTO>(questionnaireNotifyRiskSchedules, questionnaireNotifyRiskSchedulesDTO);
                return questionnaireNotifyRiskSchedulesDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// CloneQuestionnaireNotifyRiskSchedule
        /// </summary>
        /// <param name="QuestionnaireNotifyRiskScheduleDTO"></param>
        /// <returns>QuestionnaireNotifyRiskSchedulesDTO</returns>
        public QuestionnaireNotifyRiskSchedulesDTO CloneQuestionnaireNotifyRiskSchedule(QuestionnaireNotifyRiskSchedulesDTO QuestionnaireNotifyRiskScheduleDTO)
        {
            try
            {
                QuestionnaireNotifyRiskSchedule questionnaireNotifyRiskSchedule = new QuestionnaireNotifyRiskSchedule();
                this.mapper.Map<QuestionnaireNotifyRiskSchedulesDTO, QuestionnaireNotifyRiskSchedule>(QuestionnaireNotifyRiskScheduleDTO, questionnaireNotifyRiskSchedule);
                var result = this.AddAsync(questionnaireNotifyRiskSchedule).Result;
                this.mapper.Map<QuestionnaireNotifyRiskSchedule, QuestionnaireNotifyRiskSchedulesDTO>(result, QuestionnaireNotifyRiskScheduleDTO);
                return QuestionnaireNotifyRiskScheduleDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
