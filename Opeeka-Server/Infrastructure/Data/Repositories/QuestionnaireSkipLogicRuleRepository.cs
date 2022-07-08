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
    public class QuestionnaireSkipLogicRuleRepository : BaseRepository<QuestionnaireSkipLogicRule>, IQuestionnaireSkipLogicRuleRepository
    {
        private readonly IMapper mapper;
        private readonly ILogger<QuestionnaireSkipLogicRuleRepository> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="QuestionnaireSkipLogicRuleRepository"/> class.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public QuestionnaireSkipLogicRuleRepository(ILogger<QuestionnaireSkipLogicRuleRepository> logger, OpeekaDBContext dbContext, IMapper mapper)
            : base(dbContext)
        {
            this.mapper = mapper;
            this.logger = logger;
        }

        /// <summary>
        /// CloneQuestionnaireSkipLogicRule.
        /// </summary>
        /// <param name="objQuestionnaireSkipLogicRuleDTO">objQuestionnaireSkipLogicRuleDTO.</param>
        /// <returns></returns>
        public QuestionnaireSkipLogicRule CloneQuestionnaireSkipLogicRule(QuestionnaireSkipLogicRuleDTO objQuestionnaireSkipLogicRuleDTO)
        {
            try
            {
                QuestionnaireSkipLogicRule questionnaireSkipLogicRule = new QuestionnaireSkipLogicRule();
                this.mapper.Map<QuestionnaireSkipLogicRuleDTO, QuestionnaireSkipLogicRule>(objQuestionnaireSkipLogicRuleDTO, questionnaireSkipLogicRule);
                return this.AddAsync(questionnaireSkipLogicRule).Result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetQuestionnaireSkipLogic
        /// </summary>
        /// <param name="questionnaireID">questionnaireID.</param>
        /// <returns>QuestionnaireWindowDTO.</returns>
        public List<QuestionnaireSkipLogicRuleDTO> GetQuestionnaireSkipLogic(int questionnaireID)
        {
            try
            {
                var query = string.Empty;
                query = @"Select 
                            SR.[QuestionnaireSkipLogicRuleID],
                            SR.[Name] ,
                            SR.[QuestionnaireID] ,
                            SR.[IsRemoved] ,
                            SR.[UpdateDate],
                            SR.[UpdateUserID],
                            SR.[ClonedQuestionnaireSkipLogicRuleID],
                            Q.HasSkipLogic
                            from QuestionnaireSkipLogicRule SR
                            join Questionnaire Q on Q.QuestionnaireID=SR.QuestionnaireID
                            where Q.IsRemoved=0 and SR.IsRemoved=0 and Q.QuestionnaireID=" + questionnaireID;
                var data = ExecuteSqlQuery(query, x => new QuestionnaireSkipLogicRuleDTO
                {
                    QuestionnaireSkipLogicRuleID = x["QuestionnaireSkipLogicRuleID"] == DBNull.Value ? 0 : (int)x["QuestionnaireSkipLogicRuleID"],
                    Name = x["Name"] == DBNull.Value ? string.Empty : (string)x["Name"],
                    QuestionnaireID = x["QuestionnaireID"] == DBNull.Value ? 0 : (int)x["QuestionnaireID"],
                    IsRemoved = x["IsRemoved"] == DBNull.Value ? false : (bool)x["IsRemoved"],
                    UpdateDate = x["UpdateDate"] == DBNull.Value ? DateTime.Now : (DateTime)x["UpdateDate"],
                    UpdateUserID = x["UpdateUserID"] == DBNull.Value ? 0 : (int)x["UpdateUserID"],
                    ClonedQuestionnaireSkipLogicRuleID = x["ClonedQuestionnaireSkipLogicRuleID"] == DBNull.Value ? 0 : (int)x["ClonedQuestionnaireSkipLogicRuleID"],
                    HasSkipLogic = x["HasSkipLogic"] == DBNull.Value ? false : (bool)x["HasSkipLogic"],
                });
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetQuestionnaireSkipLogicRule.
        /// </summary>
        /// <param name="questionnaireID">questionnaireID.</param>
        /// <returns>QuestionnaireWindowDTO.</returns>
        public List<QuestionnaireSkipLogicRuleDTO> GetQuestionnaireSkipLogicRule(int questionnaireID)
        {
            try
            {
                var query = string.Empty;
                query = @"Select 
                            SR.[QuestionnaireSkipLogicRuleID],
                            SR.[Name] ,
                            SR.[QuestionnaireID] ,
                            SR.[IsRemoved] ,
                            SR.[UpdateDate],
                            SR.[UpdateUserID],
                            [ClonedQuestionnaireSkipLogicRuleID] 
                            from QuestionnaireSkipLogicRule SR
                            join Questionnaire Q on Q.QuestionnaireID=SR.QuestionnaireID
                            where Q.IsRemoved=0 and SR.IsRemoved=0 and Q.QuestionnaireID=" + questionnaireID;
                var data = ExecuteSqlQuery(query, x => new QuestionnaireSkipLogicRuleDTO
                {
                    QuestionnaireSkipLogicRuleID = x["QuestionnaireSkipLogicRuleID"] == DBNull.Value ? 0 : (int)x["QuestionnaireSkipLogicRuleID"],
                    Name = x["Name"] == DBNull.Value ? string.Empty : (string)x["Name"],
                    QuestionnaireID = x["QuestionnaireID"] == DBNull.Value ? 0 : (int)x["QuestionnaireID"],
                    IsRemoved = x["IsRemoved"] == DBNull.Value ? false : (bool)x["IsRemoved"],
                    UpdateDate = x["UpdateDate"] == DBNull.Value ? DateTime.Now : (DateTime)x["UpdateDate"],
                    UpdateUserID = x["UpdateUserID"] == DBNull.Value ? 0 : (int)x["UpdateUserID"],
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
        public List<QuestionnaireSkipLogicRule> UpdateBulkQuestionnaireSkipLogicRule(List<QuestionnaireSkipLogicRule> questionnaireSkipLogicRule)
        {
            try
            {
                var res = this.UpdateBulkAsync(questionnaireSkipLogicRule);
                res.Wait();
                return questionnaireSkipLogicRule;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// UpdateQuestionnaireSkipLogicRule.
        /// </summary>
        /// <param name="QuestionnaireSkipLogicRule">QuestionnaireSkipLogicRule.</param>
        /// <returns>QuestionnaireSkipLogicRule</returns>
        public QuestionnaireSkipLogicRule UpdateQuestionnaireSkipLogicRule(QuestionnaireSkipLogicRule QuestionnaireSkipLogicRule)
        {
            try
            {
                var result = this.UpdateAsync(QuestionnaireSkipLogicRule).Result;
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// AddQuestionnaireSkipLogicRule
        /// </summary>
        /// <param name="QuestionnaireSkipLogicRule"></param>
        /// <returns>QuestionnaireSkipLogicRule</returns>
        public QuestionnaireSkipLogicRule AddQuestionnaireSkipLogicRule(QuestionnaireSkipLogicRule QuestionnaireSkipLogicRule)
        {
            try
            {
                var result = this.AddAsync(QuestionnaireSkipLogicRule).Result;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
