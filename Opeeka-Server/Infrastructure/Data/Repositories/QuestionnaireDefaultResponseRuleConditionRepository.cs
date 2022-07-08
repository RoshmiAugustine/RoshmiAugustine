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
    public class QuestionnaireDefaultResponseRuleConditionRepository : BaseRepository<QuestionnaireDefaultResponseRuleCondition>, IQuestionnaireDefaultResponseRuleConditionRepository
    {
        private readonly IMapper mapper;
        private readonly ILogger<QuestionnaireDefaultResponseRuleConditionRepository> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="QuestionnaireDefaultResponseRuleConditionRepository"/> class.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public QuestionnaireDefaultResponseRuleConditionRepository(ILogger<QuestionnaireDefaultResponseRuleConditionRepository> logger, OpeekaDBContext dbContext, IMapper mapper)
            : base(dbContext)
        {
            this.mapper = mapper;
            this.logger = logger;
        }

        /// <summary>
        /// CloneQuestionnaireDefaultResponseRuleCondition.
        /// </summary>
        /// <param name="questionnaireDefaultResponseRuleConditionDTOToClone">questionnaireDefaultResponseRuleConditionDTOToClone.</param>
        public void CloneQuestionnaireDefaultResponseRuleCondition(List<QuestionnaireDefaultResponseRuleConditionDTO> questionnaireDefaultResponseRuleConditionDTOToClone)
        {
            List<QuestionnaireDefaultResponseRuleCondition> questionnaireDefaultResponseRuleCondition = new List<QuestionnaireDefaultResponseRuleCondition>();
            this.mapper.Map<List<QuestionnaireDefaultResponseRuleConditionDTO>, List<QuestionnaireDefaultResponseRuleCondition>>(questionnaireDefaultResponseRuleConditionDTOToClone, questionnaireDefaultResponseRuleCondition);
            this.AddBulkAsync(questionnaireDefaultResponseRuleCondition).Wait();
        }

        /// <summary>
        /// To Get GetQuestionnaireDefaultResponseCondition.
        /// </summary>
        /// <param name="QuestionnaireDefaultResponseRuleID">QuestionnaireDefaultResponseRuleID.</param>
        /// <returns>QuestionnaireDefaultResponseRuleConditionDTO.</returns>
        public List<QuestionnaireDefaultResponseRuleConditionDTO> GetQuestionnaireDefaultResponseCondition(int QuestionnaireDefaultResponseRuleID)
        {
            try
            {
                var query = string.Empty;
                query = @"Select 
                            SC.[QuestionnaireDefaultResponseRuleConditionID],
                            SC.[QuestionnaireItemID],
                            SC.[QuestionnaireID],
                            SC.[ComparisonOperator] ,
                            SC.[ComparisonValue] ,
                            SC.[QuestionnaireDefaultResponseRuleID],
                            SC.[ListOrder],
                            SC.[JoiningOperator] ,
                            SC.[IsRemoved] ,
                            SC.[UpdateDate],
                            SC.[UpdateUserID],
                            QI.ItemID
                            from QuestionnaireDefaultResponseRuleCondition SC
                            join QuestionnaireDefaultResponseRule SR on SR.QuestionnaireDefaultResponseRuleID=SC.QuestionnaireDefaultResponseRuleID
                            left join QuestionnaireItem QI on QI.QuestionnaireItemID=SC.QuestionnaireItemID
                            where SR.IsRemoved=0 and SC.IsRemoved=0 and SR.QuestionnaireDefaultResponseRuleID=" + QuestionnaireDefaultResponseRuleID + "order by SC.ListOrder";
                var data = ExecuteSqlQuery(query, x => new QuestionnaireDefaultResponseRuleConditionDTO
                {
                    QuestionnaireDefaultResponseRuleConditionID = x["QuestionnaireDefaultResponseRuleConditionID"] == DBNull.Value ? 0 : (int)x["QuestionnaireDefaultResponseRuleConditionID"],
                    QuestionnaireDefaultResponseRuleID = x["QuestionnaireDefaultResponseRuleID"] == DBNull.Value ? 0 : (int)x["QuestionnaireDefaultResponseRuleID"],
                    QuestionnaireItemID = x["QuestionnaireItemID"] == DBNull.Value ? 0 : (int)x["QuestionnaireItemID"],
                    ComparisonOperator = x["ComparisonOperator"] == DBNull.Value ? string.Empty : (string)x["ComparisonOperator"],
                    ComparisonValue = x["ComparisonValue"] == DBNull.Value ? 0 : (decimal)x["ComparisonValue"],
                    JoiningOperator = x["JoiningOperator"] == DBNull.Value ?string.Empty : (string)x["JoiningOperator"],
                    ListOrder = x["ListOrder"] == DBNull.Value ? 0 : (int)x["ListOrder"],
                    IsRemoved = x["IsRemoved"] == DBNull.Value ? false : (bool)x["IsRemoved"],
                    UpdateDate = x["UpdateDate"] == DBNull.Value ? DateTime.Now : (DateTime)x["UpdateDate"],
                    UpdateUserID = x["UpdateUserID"] == DBNull.Value ? 0 : (int)x["UpdateUserID"],
                    ItemID = x["ItemID"] == DBNull.Value ? 0 : (int)x["ItemID"],
                    QuestionnaireID = x["QuestionnaireID"] == DBNull.Value ? 0 : (int)x["QuestionnaireID"],
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
        /// <param name="QuestionnaireDefaultResponseRule"></param>
        /// <returns>QuestionnaireDefaultResponseRule List</returns>
        public List<QuestionnaireDefaultResponseRuleCondition> UpdateQuestionnaireDefaultResponseRuleCondition(List<QuestionnaireDefaultResponseRuleCondition> QuestionnaireDefaultResponseRuleCondition)
        {
            try
            {
                var res = this.UpdateBulkAsync(QuestionnaireDefaultResponseRuleCondition);
                res.Wait();
                return QuestionnaireDefaultResponseRuleCondition;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// AddBulkQuestionnaireDefaultResponseRuleCondition
        /// </summary>
        /// <param name="QuestionnaireDefaultResponseRuleCondition"></param>
        /// <returns></returns>
        public void AddBulkQuestionnaireDefaultResponseRuleCondition(List<QuestionnaireDefaultResponseRuleCondition> QuestionnaireDefaultResponseRuleCondition)
        {
            try
            {
                var result = this.AddBulkAsync(QuestionnaireDefaultResponseRuleCondition);
                result.Wait();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
