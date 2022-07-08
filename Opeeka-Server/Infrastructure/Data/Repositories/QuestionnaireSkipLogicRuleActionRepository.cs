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
    public class QuestionnaireSkipLogicRuleActionRepository : BaseRepository<QuestionnaireSkipLogicRuleAction>, IQuestionnaireSkipLogicRuleActionRepository
    {
        private readonly IMapper mapper;
        private readonly ILogger<QuestionnaireSkipLogicRuleActionRepository> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="QuestionnaireSkipLogicRuleActionRepository"/> class.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public QuestionnaireSkipLogicRuleActionRepository(ILogger<QuestionnaireSkipLogicRuleActionRepository> logger, OpeekaDBContext dbContext, IMapper mapper)
            : base(dbContext)
        {
            this.mapper = mapper;
            this.logger = logger;
        }

        /// <summary>
        /// CloneQuestionnaireSkipLogicRuleAction.
        /// </summary>
        /// <param name="questionnaireSkipLogicRuleActionDTOToClone">questionnaireSkipLogicRuleActionDTOToClone.</param>
        public void CloneQuestionnaireSkipLogicRuleAction(List<QuestionnaireSkipLogicRuleActionDTO> questionnaireSkipLogicRuleActionDTOToClone)
        {
            try
            {
                List<QuestionnaireSkipLogicRuleAction> questionnaireSkipLogicRuleAction = new List<QuestionnaireSkipLogicRuleAction>();
                this.mapper.Map<List<QuestionnaireSkipLogicRuleActionDTO>, List<QuestionnaireSkipLogicRuleAction>>(questionnaireSkipLogicRuleActionDTOToClone, questionnaireSkipLogicRuleAction);
                this.AddBulkAsync(questionnaireSkipLogicRuleAction).Wait();
            }
            catch (Exception)
            {
                throw;
            }
        }

        // <summary>
        /// To Get GetQuestionnaireSkipLogicAction.
        /// </summary>
        /// <param name="questionnaireID">questionnaireSkipLogicRuleID.</param>
        /// <returns>QuestionnaireSkipLogicRuleActionDTO.</returns>
        public List<QuestionnaireSkipLogicRuleActionDTO> GetQuestionnaireSkipLogicAction(int questionnaireSkipLogicRuleID)
        {
            try
            {
                var query = string.Empty;
                query = @"Select 
                            SA.[QuestionnaireSkipLogicRuleActionID],
                            SA.[QuestionnaireSkipLogicRuleID],
                            SA.[ActionLevelID],
                            SA.[QuestionnaireItemID] ,
                            SA.[CategoryID] ,
                            SA.[ActionTypeID] ,
                            SA.[ListOrder] ,
                            SA.[IsRemoved] ,
                            SA.[UpdateDate],
                            SA.[UpdateUserID],
                            AL.Name as ActionLevelName,
                            AT.Name as ActionTypeName,
                            QI.ItemID,
                            SA.ChildItemID,
                            SA.ParentItemID
                            from QuestionnaireSkipLogicRuleAction SA
                            join QuestionnaireSkipLogicRule SR on SR.QuestionnaireSkipLogicRuleID=SA.QuestionnaireSkipLogicRuleID
                            left join QuestionnaireItem QI on QI.QuestionnaireItemID=SA.QuestionnaireItemID
                            join info.ActionLevel AL on AL.ActionLevelID=SA.ActionLevelID
                            join info.ActionType AT on AT.ActionTypeID=SA.ActionTypeID
                            where SR.IsRemoved=0 and SA.IsRemoved=0 and SR.QuestionnaireSkipLogicRuleID=" + questionnaireSkipLogicRuleID + "order by SA.ListOrder";
                var data = ExecuteSqlQuery(query, x => new QuestionnaireSkipLogicRuleActionDTO
                {
                    QuestionnaireSkipLogicRuleActionID = x["QuestionnaireSkipLogicRuleActionID"] == DBNull.Value ? 0 : (int)x["QuestionnaireSkipLogicRuleActionID"],
                    QuestionnaireSkipLogicRuleID = x["QuestionnaireSkipLogicRuleID"] == DBNull.Value ? 0 : (int)x["QuestionnaireSkipLogicRuleID"],
                    ActionLevelID = x["ActionLevelID"] == DBNull.Value ? 0 : (int)x["ActionLevelID"],
                    QuestionnaireItemID = x["QuestionnaireItemID"] == DBNull.Value ? null : (int?)x["QuestionnaireItemID"],
                    CategoryID = x["CategoryID"] == DBNull.Value ? null: (int?)x["CategoryID"],
                    ActionTypeID = x["ActionTypeID"] == DBNull.Value ? 0 : (int)x["ActionTypeID"],
                    ListOrder = x["ListOrder"] == DBNull.Value ? 0 : (int)x["ListOrder"],
                    IsRemoved = x["IsRemoved"] == DBNull.Value ? false : (bool)x["IsRemoved"],
                    UpdateDate = x["UpdateDate"] == DBNull.Value ? DateTime.Now : (DateTime)x["UpdateDate"],
                    UpdateUserID = x["UpdateUserID"] == DBNull.Value ? 0 : (int)x["UpdateUserID"],
                    ActionLevelName = x["ActionLevelName"] == DBNull.Value ? string.Empty : (string)x["ActionLevelName"],
                    ActionTypeName = x["ActionTypeName"] == DBNull.Value ? string.Empty : (string)x["ActionTypeName"],
                    ItemID = x["ItemID"] == DBNull.Value ? 0 : (int)x["ItemID"],
                    ChildItemID = x["ChildItemID"] == DBNull.Value ? null : (int?)x["ChildItemID"],
                    ParentItemID = x["ParentItemID"] == DBNull.Value ? null : (int?)x["ParentItemID"],
                });
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// UpdateQuestionnaireSkipLogicRuleAction
        /// </summary>
        /// <param name="QuestionnaireSkipLogicRuleAction"></param>
        /// <returns>QuestionnaireSkipLogicRuleAction List</returns>
        public List<QuestionnaireSkipLogicRuleAction> UpdateQuestionnaireSkipLogicRuleAction(List<QuestionnaireSkipLogicRuleAction> QuestionnaireSkipLogicRuleAction)
        {
            try
            {
                var res = this.UpdateBulkAsync(QuestionnaireSkipLogicRuleAction);
                res.Wait();
                return QuestionnaireSkipLogicRuleAction;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// AddBulkQuestionnaireSkipLogicRuleAction
        /// </summary>
        /// <param name="QuestionnaireSkipLogicRuleAction"></param>
        /// <returns></returns>
        public void AddBulkQuestionnaireSkipLogicRuleAction(List<QuestionnaireSkipLogicRuleAction> QuestionnaireSkipLogicRuleAction)
        {
            try
            {
                var result = this.AddBulkAsync(QuestionnaireSkipLogicRuleAction);
                result.Wait();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
