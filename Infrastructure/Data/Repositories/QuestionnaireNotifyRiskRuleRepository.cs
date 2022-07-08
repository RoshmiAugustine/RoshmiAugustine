// -----------------------------------------------------------------------
// <copyright file="QuestionnaireNotifyRiskRuleRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Infrastructure.Data.Repositories.Base;

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{
    public class QuestionnaireNotifyRiskRuleRepository : BaseRepository<QuestionnaireNotifyRiskRule>, IQuestionnaireNotifyRiskRuleRepository
    {
        private readonly IMapper mapper;
        private readonly OpeekaDBContext _dbContext;
        private readonly ILogger<QuestionnaireNotifyRiskRule> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="QuestionnaireNotifyRiskRule"/> class.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public QuestionnaireNotifyRiskRuleRepository(ILogger<QuestionnaireNotifyRiskRule> logger, OpeekaDBContext dbContext, IMapper mapper)
            : base(dbContext)
        {
            this.mapper = mapper;
            this.logger = logger;
            this._dbContext = dbContext;
        }

        /// <summary>
        /// GetQuestionnaireNotifyRiskRule.
        /// </summary>
        /// <param name="QuestionnaireNotifyRiskRuleID">QuestionnaireNotifyRiskRuleID.</param>
        /// <returns>QuestionnaireNotifyRiskRule</returns>
        public async Task<QuestionnaireNotifyRiskRule> GetQuestionnaireNotifyRiskRule(int questionnaireNotifyRiskRuleID)
        {

            try
            {
                QuestionnaireNotifyRiskRule questionnaireNotifyRiskRule = await this.GetRowAsync(x => x.QuestionnaireNotifyRiskRuleID == questionnaireNotifyRiskRuleID);
                return questionnaireNotifyRiskRule;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<QuestionnaireNotifyRiskRule> GetQuestionnaireNotifyRiskRuleByScheduleID(int scheduleID)
        {
            try
            {
                List<QuestionnaireNotifyRiskRule> questionnaireNotifyRiskRule = this._dbContext.QuestionnaireNotifyRiskRules.Where(x => x.QuestionnaireNotifyRiskScheduleID == scheduleID && !x.IsRemoved).ToList();
                return questionnaireNotifyRiskRule;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// UpdateQuestionnaireNotifyRiskRule.
        /// </summary>
        /// <param name="QuestionnaireNotifyRiskRule">QuestionnaireNotifyRiskRule.</param>
        /// <returns>QuestionnaireNotifyRiskRuleDTO.</returns>
        public QuestionnaireNotifyRiskRuleDTO UpdateQuestionnaireNotifyRiskRule(QuestionnaireNotifyRiskRule questionnaireWindow)
        {
            try
            {
                var result = this.UpdateAsync(questionnaireWindow).Result;
                QuestionnaireNotifyRiskRuleDTO questionnaireNotifyRiskRuleDTO = new QuestionnaireNotifyRiskRuleDTO();
                this.mapper.Map<QuestionnaireNotifyRiskRule, QuestionnaireNotifyRiskRuleDTO>(result, questionnaireNotifyRiskRuleDTO);
                return questionnaireNotifyRiskRuleDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// AddQuestionnaireNotifyRiskRule.
        /// </summary>
        /// <param name="questionnaireNotifyRiskRuleDTO">QuestionnaireNotifyRiskRuleDTO.</param>
        /// <returns>long</returns>
        public int AddQuestionnaireNotifyRiskRule(QuestionnaireNotifyRiskRuleDTO questionnaireNotifyRiskRuleDTO)
        {
            try
            {
                QuestionnaireNotifyRiskRule questionnaireNotifyRiskRule = new QuestionnaireNotifyRiskRule();
                this.mapper.Map(questionnaireNotifyRiskRuleDTO, questionnaireNotifyRiskRule);
                var result = this.AddAsync(questionnaireNotifyRiskRule).Result.QuestionnaireNotifyRiskRuleID;
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetQuestionnaireNotifyRiskRuleBySchedule
        /// </summary>
        /// <param name="questionnaireNotifyRiskScheduleID"></param>
        /// <returns>QuestionnaireNotifyRiskRulesDTO</returns>
        public List<QuestionnaireNotifyRiskRulesDTO> GetQuestionnaireNotifyRiskRuleBySchedule(int questionnaireNotifyRiskScheduleID)
        {
            try
            {
                List<QuestionnaireNotifyRiskRulesDTO> questionnaireNotifyRiskRulesDTO = new List<QuestionnaireNotifyRiskRulesDTO>();
                var questionnaireNotifyRiskRules = this._dbContext.QuestionnaireNotifyRiskRules.Where(x => x.QuestionnaireNotifyRiskScheduleID == questionnaireNotifyRiskScheduleID && !x.IsRemoved).ToList();
                this.mapper.Map<List<QuestionnaireNotifyRiskRule>, List<QuestionnaireNotifyRiskRulesDTO>>(questionnaireNotifyRiskRules, questionnaireNotifyRiskRulesDTO);
                return questionnaireNotifyRiskRulesDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// CloneQuestionnaireNotifyRiskRule
        /// </summary>
        /// <param name="questionnaireNotifyRiskRuleDTO"></param>
        /// <returns>QuestionnaireNotifyRiskRulesDTO</returns>
        public void CloneQuestionnaireNotifyRiskRule(List<QuestionnaireNotifyRiskRulesDTO> questionnaireNotifyRiskRuleDTO)
        {
            try
            {
                List<QuestionnaireNotifyRiskRule> questionnaireNotifyRiskRule = new List<QuestionnaireNotifyRiskRule>();
                this.mapper.Map<List<QuestionnaireNotifyRiskRulesDTO>, List<QuestionnaireNotifyRiskRule>>(questionnaireNotifyRiskRuleDTO, questionnaireNotifyRiskRule);
                this.AddBulkAsync(questionnaireNotifyRiskRule).Wait();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}