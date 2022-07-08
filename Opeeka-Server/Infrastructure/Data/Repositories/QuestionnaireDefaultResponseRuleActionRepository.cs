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
    public class QuestionnaireDefaultResponseRuleActionRepository : BaseRepository<QuestionnaireDefaultResponseRuleAction>, IQuestionnaireDefaultResponseRuleActionRepository
    {
        private readonly IMapper mapper;
        private readonly ILogger<QuestionnaireDefaultResponseRuleActionRepository> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="QuestionnaireDefaultResponseRuleActionRepository"/> class.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public QuestionnaireDefaultResponseRuleActionRepository(ILogger<QuestionnaireDefaultResponseRuleActionRepository> logger, OpeekaDBContext dbContext, IMapper mapper)
            : base(dbContext)
        {
            this.mapper = mapper;
            this.logger = logger;
        }

        /// <summary>
        /// CloneQuestionnaireDefaultResponseRuleAction.
        /// </summary>
        /// <param name="questionnaireDefaultResponseRuleActionDTOToClone">questionnaireDefaultResponseRuleActionDTOToClone.</param>
        public void CloneQuestionnaireDefaultResponseRuleAction(List<QuestionnaireDefaultResponseRuleActionDTO> questionnaireDefaultResponseRuleActionDTOToClone)
        {
            try
            {
                List<QuestionnaireDefaultResponseRuleAction> questionnaireDefaultResponseRuleAction = new List<QuestionnaireDefaultResponseRuleAction>();
                this.mapper.Map<List<QuestionnaireDefaultResponseRuleActionDTO>, List<QuestionnaireDefaultResponseRuleAction>>(questionnaireDefaultResponseRuleActionDTOToClone, questionnaireDefaultResponseRuleAction);
                this.AddBulkAsync(questionnaireDefaultResponseRuleAction).Wait();
            }
            catch (Exception)
            {
                throw;
            }
        }

        // <summary>
        /// To Get GetQuestionnaireDefaultResponseAction.
        /// </summary>
        /// <param name="questionnaireID">questionnaireDefaultResponseRuleID.</param>
        /// <returns>QuestionnaireDefaultResponseRuleActionDTO.</returns>
        public List<QuestionnaireDefaultResponseRuleActionDTO> GetQuestionnaireDefaultResponseAction(int questionnaireDefaultResponseRuleID)
        {
            try
            {
                var query = string.Empty;
                query = @"Select 
                            SA.[QuestionnaireDefaultResponseRuleActionID],
                            SA.[QuestionnaireDefaultResponseRuleID],
                            SA.[QuestionnaireItemID] ,
                            SA.[ListOrder] ,
                            SA.[IsRemoved] ,
                            SA.[UpdateDate],
                            SA.[UpdateUserID],
                            SA.[DefaultResponseID],
                            QI.ItemID,
							R.Value
                            from QuestionnaireDefaultResponseRuleAction SA
                            join QuestionnaireDefaultResponseRule SR on SR.QuestionnaireDefaultResponseRuleID=SA.QuestionnaireDefaultResponseRuleID
                            left join QuestionnaireItem QI on QI.QuestionnaireItemID=SA.QuestionnaireItemID
                            left join Response R on R.ResponseID=SA.DefaultResponseID
                            where SR.IsRemoved=0 and SA.IsRemoved=0 and SR.QuestionnaireDefaultResponseRuleID=" + questionnaireDefaultResponseRuleID + "order by SA.ListOrder";
                var data = ExecuteSqlQuery(query, x => new QuestionnaireDefaultResponseRuleActionDTO
                {
                    QuestionnaireDefaultResponseRuleActionID = x["QuestionnaireDefaultResponseRuleActionID"] == DBNull.Value ? 0 : (int)x["QuestionnaireDefaultResponseRuleActionID"],
                    QuestionnaireDefaultResponseRuleID = x["QuestionnaireDefaultResponseRuleID"] == DBNull.Value ? 0 : (int)x["QuestionnaireDefaultResponseRuleID"],
                    QuestionnaireItemID = x["QuestionnaireItemID"] == DBNull.Value ? null : (int?)x["QuestionnaireItemID"],
                    ListOrder = x["ListOrder"] == DBNull.Value ? 0 : (int)x["ListOrder"],
                    IsRemoved = x["IsRemoved"] == DBNull.Value ? false : (bool)x["IsRemoved"],
                    UpdateDate = x["UpdateDate"] == DBNull.Value ? DateTime.Now : (DateTime)x["UpdateDate"],
                    UpdateUserID = x["UpdateUserID"] == DBNull.Value ? 0 : (int)x["UpdateUserID"],
                    ItemID = x["ItemID"] == DBNull.Value ? 0 : (int)x["ItemID"],
                    DefaultResponseID = x["DefaultResponseID"] == DBNull.Value ? null : (int?)x["DefaultResponseID"],
                    DefaultValue = x["Value"] == DBNull.Value ? 0 : (decimal)x["Value"],
                });
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// UpdateQuestionnaireDefaultResponseRuleAction
        /// </summary>
        /// <param name="QuestionnaireDefaultResponseRuleAction"></param>
        /// <returns>QuestionnaireDefaultResponseRuleAction List</returns>
        public List<QuestionnaireDefaultResponseRuleAction> UpdateQuestionnaireDefaultResponseRuleAction(List<QuestionnaireDefaultResponseRuleAction> QuestionnaireDefaultResponseRuleAction)
        {
            try
            {
                var res = this.UpdateBulkAsync(QuestionnaireDefaultResponseRuleAction);
                res.Wait();
                return QuestionnaireDefaultResponseRuleAction;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// AddBulkQuestionnaireDefaultResponseRuleAction
        /// </summary>
        /// <param name="QuestionnaireDefaultResponseRuleAction"></param>
        /// <returns></returns>
        public void AddBulkQuestionnaireDefaultResponseRuleAction(List<QuestionnaireDefaultResponseRuleAction> QuestionnaireDefaultResponseRuleAction)
        {
            try
            {
                var result = this.AddBulkAsync(QuestionnaireDefaultResponseRuleAction);
                result.Wait();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
