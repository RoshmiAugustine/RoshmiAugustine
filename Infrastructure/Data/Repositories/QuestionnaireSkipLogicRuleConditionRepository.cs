using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Infrastructure.Data.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{
    public class QuestionnaireSkipLogicRuleConditionRepository : BaseRepository<QuestionnaireSkipLogicRuleCondition>, IQuestionnaireSkipLogicRuleConditionRepository
    {
        private readonly IMapper mapper;
        private readonly ILogger<QuestionnaireSkipLogicRuleConditionRepository> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="QuestionnaireSkipLogicRuleConditionRepository"/> class.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public QuestionnaireSkipLogicRuleConditionRepository(ILogger<QuestionnaireSkipLogicRuleConditionRepository> logger, OpeekaDBContext dbContext, IMapper mapper)
            : base(dbContext)
        {
            this.mapper = mapper;
            this.logger = logger;
        }

        /// <summary>
        /// CloneQuestionnaireSkipLogicRuleCondition.
        /// </summary>
        /// <param name="questionnaireSkipLogicRuleConditionDTOToClone">questionnaireSkipLogicRuleConditionDTOToClone.</param>
        public void CloneQuestionnaireSkipLogicRuleCondition(List<QuestionnaireSkipLogicRuleConditionDTO> questionnaireSkipLogicRuleConditionDTOToClone)
        {
            List<QuestionnaireSkipLogicRuleCondition> questionnaireSkipLogicRuleCondition = new List<QuestionnaireSkipLogicRuleCondition>();
            this.mapper.Map<List<QuestionnaireSkipLogicRuleConditionDTO>, List<QuestionnaireSkipLogicRuleCondition>>(questionnaireSkipLogicRuleConditionDTOToClone, questionnaireSkipLogicRuleCondition);
            this.AddBulkAsync(questionnaireSkipLogicRuleCondition).Wait();
        }

        /// <summary>
        /// To Get GetQuestionnaireSkipLogicCondition.
        /// </summary>
        /// <param name="QuestionnaireSkipLogicRuleID">QuestionnaireSkipLogicRuleID.</param>
        /// <returns>QuestionnaireSkipLogicRuleConditionDTO.</returns>
        public List<QuestionnaireSkipLogicRuleConditionDTO> GetQuestionnaireSkipLogicCondition(int QuestionnaireSkipLogicRuleID)
        {
            try
            {
                var query = string.Empty;
                query = @"Select 
                            SC.[QuestionnaireSkipLogicRuleConditionID],
                            SC.[QuestionnaireItemID],
                            SC.[ComparisonOperator] ,
                            SC.[ComparisonValue] ,
                            SC.[QuestionnaireSkipLogicRuleID],
                            SC.[ListOrder],
                            SC.[JoiningOperator] ,
                            SC.[IsRemoved] ,
                            SC.[UpdateDate],
                            SC.[UpdateUserID],
                            QI.ItemID
                            from QuestionnaireSkipLogicRuleCondition SC
                            join QuestionnaireSkipLogicRule SR on SR.QuestionnaireSkipLogicRuleID=SC.QuestionnaireSkipLogicRuleID
                            left join QuestionnaireItem QI on QI.QuestionnaireItemID=SC.QuestionnaireItemID
                            where SR.IsRemoved=0 and SC.IsRemoved=0 and SR.QuestionnaireSkipLogicRuleID=" + QuestionnaireSkipLogicRuleID + "order by SC.ListOrder";
                var data = ExecuteSqlQuery(query, x => new QuestionnaireSkipLogicRuleConditionDTO
                {
                    QuestionnaireSkipLogicRuleConditionID = x["QuestionnaireSkipLogicRuleConditionID"] == DBNull.Value ? 0 : (int)x["QuestionnaireSkipLogicRuleConditionID"],
                    QuestionnaireSkipLogicRuleID = x["QuestionnaireSkipLogicRuleID"] == DBNull.Value ? 0 : (int)x["QuestionnaireSkipLogicRuleID"],
                    QuestionnaireItemID = x["QuestionnaireItemID"] == DBNull.Value ? 0 : (int)x["QuestionnaireItemID"],
                    ComparisonOperator = x["ComparisonOperator"] == DBNull.Value ? string.Empty : (string)x["ComparisonOperator"],
                    ComparisonValue = x["ComparisonValue"] == DBNull.Value ? 0 : (decimal)x["ComparisonValue"],
                    JoiningOperator = x["JoiningOperator"] == DBNull.Value ?string.Empty : (string)x["JoiningOperator"],
                    ListOrder = x["ListOrder"] == DBNull.Value ? 0 : (int)x["ListOrder"],
                    IsRemoved = x["IsRemoved"] == DBNull.Value ? false : (bool)x["IsRemoved"],
                    UpdateDate = x["UpdateDate"] == DBNull.Value ? DateTime.Now : (DateTime)x["UpdateDate"],
                    UpdateUserID = x["UpdateUserID"] == DBNull.Value ? 0 : (int)x["UpdateUserID"],
                    ItemID = x["ItemID"] == DBNull.Value ? 0 : (int)x["ItemID"],
                });
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Update Questionnaire Skip Logic Rule
        /// </summary>
        /// <param name="QuestionnaireSkipLogicRule"></param>
        /// <returns>QuestionnaireSkipLogicRule List</returns>
        public List<QuestionnaireSkipLogicRuleCondition> UpdateQuestionnaireSkipLogicRuleCondition(List<QuestionnaireSkipLogicRuleCondition> QuestionnaireSkipLogicRuleCondition)
        {
            try
            {
                var res = this.UpdateBulkAsync(QuestionnaireSkipLogicRuleCondition);
                res.Wait();
                return QuestionnaireSkipLogicRuleCondition;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// AddBulkQuestionnaireSkipLogicRuleCondition
        /// </summary>
        /// <param name="QuestionnaireSkipLogicRuleCondition"></param>
        /// <returns></returns>
        public void AddBulkQuestionnaireSkipLogicRuleCondition(List<QuestionnaireSkipLogicRuleCondition> QuestionnaireSkipLogicRuleCondition)
        {
            try
            {
                var result = this.AddBulkAsync(QuestionnaireSkipLogicRuleCondition);
                result.Wait();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
