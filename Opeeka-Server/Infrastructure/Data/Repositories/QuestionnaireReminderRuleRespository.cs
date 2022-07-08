// -----------------------------------------------------------------------
// <copyright file="QuestionnaireReminderRuleRespository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using AutoMapper;
using Microsoft.Extensions.Logging;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Infrastructure.Data.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{
    public class QuestionnaireReminderRuleRespository : BaseRepository<QuestionnaireReminderRule>, IQuestionnaireReminderRuleRespository
    {
        private readonly IMapper mapper;
        private readonly ILogger<QuestionnaireReminderRule> logger;
        private readonly OpeekaDBContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="QuestionnaireReminderRule  "/> class.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public QuestionnaireReminderRuleRespository(ILogger<QuestionnaireReminderRule> logger, OpeekaDBContext dbContext, IMapper mapper)
            : base(dbContext)
        {
            this.mapper = mapper;
            this.logger = logger;
            this._dbContext = dbContext;
        }

        /// <summary>
        /// GetQuestionnaireReminderRule.
        /// </summary>
        /// <param name="QuestionnaireReminderRuleID">questionnaireReminderRuleID.</param>
        /// <returns>QuestionnaireReminderRuleDTO</returns>
        public async Task<QuestionnaireReminderRule> GetQuestionnaireReminderRule(int questionnaireWindowID)
        {

            try
            {
                QuestionnaireReminderRule questionnaireWindow = await this.GetRowAsync(x => x.QuestionnaireReminderRuleID == questionnaireWindowID);
                return questionnaireWindow;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// UpdateQuestionnaireReminderRule
        /// </summary>
        /// <param name="questionnaireWindow">questionnaireReminderRule.</param>
        /// <returns>questionnaireReminderRuleDTO.</returns>
        public QuestionnaireReminderRuleDTO UpdateQuestionnaireReminderRule(QuestionnaireReminderRule questionnaireReminderRule)
        {
            try
            {
                var result = this.UpdateAsync(questionnaireReminderRule).Result;
                QuestionnaireReminderRuleDTO QuestionnaireReminderRuleDTO = new QuestionnaireReminderRuleDTO();
                this.mapper.Map<QuestionnaireReminderRule, QuestionnaireReminderRuleDTO>(result, QuestionnaireReminderRuleDTO);
                return QuestionnaireReminderRuleDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// AddQuestionnaireReminderRule.
        /// </summary>
        /// <param name="QuestionnaireReminderRuleDTO">QuestionnaireReminderRuleDTO.</param>
        /// <returns>long</returns>
        public long AddQuestionnaireReminderRule(QuestionnaireReminderRuleDTO QuestionnaireReminderRuleDTO)
        {
            try
            {
                QuestionnaireReminderRule QuestionnaireReminderRule = new QuestionnaireReminderRule();
                this.mapper.Map(QuestionnaireReminderRuleDTO, QuestionnaireReminderRule);
                var result = this.AddAsync(QuestionnaireReminderRule).Result.QuestionnaireReminderRuleID;
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// AddQuestionnaireReminderRule.
        /// </summary>
        /// <param name="QuestionnaireReminderRuleDTO">QuestionnaireReminderRuleDTO.</param>
        /// <returns>long</returns>
        public void AddBulkQuestionnaireReminderRule(List<QuestionnaireReminderRuleDTO> QuestionnaireReminderRuleDTO)
        {
            try
            {
                List<QuestionnaireReminderRule> QuestionnaireReminderRule = new List<QuestionnaireReminderRule>();
                this.mapper.Map(QuestionnaireReminderRuleDTO, QuestionnaireReminderRule);
                var result = this.AddBulkAsync(QuestionnaireReminderRule);
                result.Wait();
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// AddQuestionnaireReminderRule.
        /// </summary>
        /// <param name="QuestionnaireReminderRuleDTO">QuestionnaireReminderRuleDTO.</param>
        /// <returns>long</returns>
        public void UpdateBulkQuestionnaireReminderRule(List<QuestionnaireReminderRule> QuestionnaireReminderRuleDTO)
        {
            try
            {
                var result = this.UpdateBulkAsync(QuestionnaireReminderRuleDTO);
                result.Wait();
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// GetQuestionnaireReminderRulesByQuestionnaire
        /// </summary>
        /// <param name="questionnaireID"></param>
        /// <returns>QuestionnaireReminderRulesDTO</returns>
        public List<QuestionnaireReminderRulesDTO> GetQuestionnaireReminderRulesByQuestionnaire(int questionnaireID)
        {
            try
            {
                List<QuestionnaireReminderRulesDTO> questionnaireReminderRulesDTO = new List<QuestionnaireReminderRulesDTO>();
                var questionnaireReminderRules = this._dbContext.QuestionnaireReminderRules.Where(x => x.QuestionnaireID == questionnaireID).ToList();
                this.mapper.Map<List<QuestionnaireReminderRule>, List<QuestionnaireReminderRulesDTO>>(questionnaireReminderRules, questionnaireReminderRulesDTO);
                return questionnaireReminderRulesDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// CloneQuestionnaireReminderRule
        /// </summary>
        /// <param name="questionnaireReminderRuleDTO"></param>
        /// <returns>QuestionnaireReminderRulesDTO</returns>
        public void CloneQuestionnaireReminderRule(List<QuestionnaireReminderRulesDTO> questionnaireReminderRuleDTO)
        {
            try
            {
                List<QuestionnaireReminderRule> questionnaireReminderRule = new List<QuestionnaireReminderRule>();
                this.mapper.Map<List<QuestionnaireReminderRulesDTO>, List<QuestionnaireReminderRule>>(questionnaireReminderRuleDTO, questionnaireReminderRule);
                var result = this.AddBulkAsync(questionnaireReminderRule);
                result.Wait();
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// GetAllReminderRulesByQuestionnaires
        /// </summary>
        /// <param name="list_questionnaireIds"></param>
        /// <returns></returns>
        public List<QuestionnaireReminderRulesDTO> GetAllReminderRulesByQuestionnaires(List<int> list_questionnaireIds)
        {
            try
            {
                List<QuestionnaireReminderRulesDTO> questionnaireReminderRulesDTO = new List<QuestionnaireReminderRulesDTO>();
                var questionnaireReminderRules = this._dbContext.QuestionnaireReminderRules.Where(x => list_questionnaireIds.Contains(x.QuestionnaireID)).ToList();
                this.mapper.Map<List<QuestionnaireReminderRule>, List<QuestionnaireReminderRulesDTO>>(questionnaireReminderRules, questionnaireReminderRulesDTO);
                return questionnaireReminderRulesDTO;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// GetQuestionnaireReminderRulesByQuestionnaire
        /// </summary>
        /// <param name="questionnaireID"></param>
        /// <returns>QuestionnaireReminderRulesDTO</returns>
        public List<QuestionnaireReminderRule> GetAllQuestionnaireReminderRules(int questionnaireID)
        {
            try
            {
                var questionnaireReminderRules = this._dbContext.QuestionnaireReminderRules.Where(x => x.QuestionnaireID == questionnaireID).ToList();
                return questionnaireReminderRules;
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}