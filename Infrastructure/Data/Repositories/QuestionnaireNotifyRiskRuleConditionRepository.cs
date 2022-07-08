// -----------------------------------------------------------------------
// <copyright file="QuestionnaireNotifyRiskRuleConditionRepository.cs" company="Naicoits">
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
    public class QuestionnaireNotifyRiskRuleConditionRepository : BaseRepository<QuestionnaireNotifyRiskRuleCondition>, IQuestionnaireNotifyRiskRuleConditionRepository
    {
        private readonly IMapper mapper;
        private readonly ILogger<QuestionnaireNotifyRiskRuleCondition> logger;
        private readonly OpeekaDBContext _dbContext;


        /// <summary>
        /// Initializes a new instance of the <see cref="QuestionnaireNotifyRiskRuleCondition"/> class.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public QuestionnaireNotifyRiskRuleConditionRepository(ILogger<QuestionnaireNotifyRiskRuleCondition> logger, OpeekaDBContext dbContext, IMapper mapper)
            : base(dbContext)
        {
            this.mapper = mapper;
            this.logger = logger;
            this._dbContext = dbContext;
        }

        /// <summary>
        /// GetQuestionnaireNotifyRiskRuleCondition.
        /// </summary>
        /// <param name="QuestionnaireNotifyRiskRuleConditionID">QuestionnaireNotifyRiskRuleConditionID.</param>
        /// <returns>QuestionnaireNotifyRiskRuleCondition</returns>
        public async Task<QuestionnaireNotifyRiskRuleCondition> GetQuestionnaireNotifyRiskRuleCondition(int questionnaireWindowID)
        {
            try
            {
                QuestionnaireNotifyRiskRuleCondition questionnaireWindow = await this.GetRowAsync(x => x.QuestionnaireNotifyRiskRuleConditionID == questionnaireWindowID);
                return questionnaireWindow;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<QuestionnaireNotifyRiskRuleCondition> GetQuestionnaireNotifyRiskRuleConditionByRuleID(int ruleID)
        {
            List<QuestionnaireNotifyRiskRuleCondition> questionnaireWindow = this._dbContext.QuestionnaireNotifyRiskRuleConditions.Where(x => x.QuestionnaireNotifyRiskRuleID == ruleID && !x.IsRemoved).ToList();
            return questionnaireWindow;
        }

        /// <summary>
        /// UpdateQuestionnaireNotifyRiskRuleCondition.
        /// </summary>
        /// <param name="QuestionnaireNotifyRiskRuleCondition">QuestionnaireNotifyRiskRuleCondition.</param>
        /// <returns>QuestionnaireNotifyRiskRuleConditionDTO.</returns>
        public QuestionnaireNotifyRiskRuleConditionDTO UpdateQuestionnaireNotifyRiskRuleCondition(QuestionnaireNotifyRiskRuleCondition questionnaireNotifyRiskRuleCondition)
        {
            try
            {
                var result = this.UpdateAsync(questionnaireNotifyRiskRuleCondition).Result;
                QuestionnaireNotifyRiskRuleConditionDTO questionnaireNotifyRiskRuleConditionDTO = new QuestionnaireNotifyRiskRuleConditionDTO();
                this.mapper.Map<QuestionnaireNotifyRiskRuleCondition, QuestionnaireNotifyRiskRuleConditionDTO>(result, questionnaireNotifyRiskRuleConditionDTO);
                return questionnaireNotifyRiskRuleConditionDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// AddQuestionnaireNotifyRiskRuleCondition.
        /// </summary>
        /// <param name="questionnaireNotifyRiskRuleConditionDTO">QuestionnaireNotifyRiskRuleConditionDTO.</param>
        /// <returns>long</returns>
        public long AddQuestionnaireNotifyRiskRuleCondition(QuestionnaireNotifyRiskRuleConditionDTO questionnaireNotifyRiskRuleConditionDTO)
        {
            try
            {
                QuestionnaireNotifyRiskRuleCondition questionnaireNotifyRiskRuleCondition = new QuestionnaireNotifyRiskRuleCondition();
                this.mapper.Map(questionnaireNotifyRiskRuleConditionDTO, questionnaireNotifyRiskRuleCondition);
                var result = this.AddAsync(questionnaireNotifyRiskRuleCondition).Result.QuestionnaireNotifyRiskRuleConditionID;
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetQuestionnaireNotifyRiskRuleConditionByRiskRule
        /// </summary>
        /// <param name="questionnaireNotifyRiskRuleID"></param>
        /// <returns>QuestionnaireNotifyRiskRuleConditionsDTO</returns>
        public List<QuestionnaireNotifyRiskRuleConditionsDTO> GetQuestionnaireNotifyRiskRuleConditionByRiskRule(int questionnaireNotifyRiskRuleID)
        {
            try
            {
                List<QuestionnaireNotifyRiskRuleConditionsDTO> questionnaireNotifyRiskRuleConditionsDTO = new List<QuestionnaireNotifyRiskRuleConditionsDTO>();
                var questionnaireNotifyRiskRuleConditions = this._dbContext.QuestionnaireNotifyRiskRuleConditions.Where(x => x.QuestionnaireNotifyRiskRuleID == questionnaireNotifyRiskRuleID).ToList();
                this.mapper.Map<List<QuestionnaireNotifyRiskRuleCondition>, List<QuestionnaireNotifyRiskRuleConditionsDTO>>(questionnaireNotifyRiskRuleConditions, questionnaireNotifyRiskRuleConditionsDTO);
                return questionnaireNotifyRiskRuleConditionsDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// CloneQuestionnaireNotifyRiskRuleCondition
        /// </summary>
        /// <param name="questionnaireNotifyRiskRuleConditionDTO"></param>
        /// <returns>QuestionnaireNotifyRiskRuleConditionsDTO</returns>
        public void CloneQuestionnaireNotifyRiskRuleCondition(List<QuestionnaireNotifyRiskRuleConditionsDTO> questionnaireNotifyRiskRuleConditionDTO)
        {
            try
            {
                List<QuestionnaireNotifyRiskRuleCondition> questionnaireNotifyRiskRuleCondition = new List<QuestionnaireNotifyRiskRuleCondition>();
                this.mapper.Map<List<QuestionnaireNotifyRiskRuleConditionsDTO>, List<QuestionnaireNotifyRiskRuleCondition>>(questionnaireNotifyRiskRuleConditionDTO, questionnaireNotifyRiskRuleCondition);
                this.AddBulkAsync(questionnaireNotifyRiskRuleCondition).Wait();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetQuestionnaireNotifyRiskRuleCondition.
        /// </summary>
        /// <param name="QuestionnaireNotifyRiskRuleConditionID">QuestionnaireNotifyRiskRuleConditionID.</param>
        /// <returns>QuestionnaireNotifyRiskRuleCondition</returns>
        public async Task<QuestionnaireNotifyRiskRuleCondition> GetQuestionnaireNotifyRiskRuleConditionByQuestionnaireItemID(int questionnaireItemID)
        {
            try
            {
                QuestionnaireNotifyRiskRuleCondition questionnaireWindow = await this.GetRowAsync(x => x.QuestionnaireItemID == questionnaireItemID);
                return questionnaireWindow;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<QuestionnaireRiskItemDetailsDTO> GetRiskItemDetails(int notificationLogID)
        {
            try
            {
                var query = string.Empty;
                query = @$"select QNRC.QuestionnaireItemID, QNRC.ComparisonOperator, QNRC.ComparisonValue, QNRC.JoiningOperator, I.ItemID, I.[Name] ItemName, NR.AssessmentID, QI.QuestionnaireID, P.PersonIndex from Notificationlog NL  
					JOIN Person P on NL.PersonID = P.PersonID
					Join NotifyRisk NR on NL.FkeyValue = NR.NotifyRiskID 
                    JOIN QuestionnaireNotifyRiskRule QNR on NR.QuestionnaireNotifyRiskRuleID= QNR.QuestionnaireNotifyRiskRuleID
                    join QuestionnaireNotifyRiskRuleCondition QNRC ON QNR.QuestionnaireNotifyRiskRuleID = QNRC.QuestionnaireNotifyRiskRuleID 
                    JOIN QuestionnaireItem QI ON QNRC.QuestionnaireItemID = QI.QuestionnaireItemID
                    JOIN Item I ON QI.ItemID = I.ItemID where NL.NotificationLogID = {notificationLogID} ";
               
                var riskItemDetails = ExecuteSqlQuery(query, x => new QuestionnaireRiskItemDetailsDTO
                {
                    QuestionnaireItemID = x["QuestionnaireItemID"] == DBNull.Value ? 0 : (int)x["QuestionnaireItemID"],
                    ComparisonOperator = x["ComparisonOperator"] == DBNull.Value ? null : (string)x["ComparisonOperator"],
                    ComparisonValue = x["ComparisonValue"] == DBNull.Value ? 0 : (decimal)x["ComparisonValue"],
                    ItemID = x["ItemID"] == DBNull.Value ? 0 : (int)x["ItemID"],
                    ItemName = x["ItemName"] == DBNull.Value ? null : (string)x["ItemName"],
                    JoiningOperator = x["JoiningOperator"] == DBNull.Value ? null : (string)x["JoiningOperator"],
                    AssessmentID = x["AssessmentID"] == DBNull.Value ? 0 : (int)x["AssessmentID"],
                    QuestionnaireID = x["QuestionnaireID"] == DBNull.Value ? 0 : (int)x["QuestionnaireID"],
                    PersonIndex = x["PersonIndex"] == DBNull.Value ? Guid.Empty : (Guid)x["PersonIndex"]
                });

                return riskItemDetails;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}