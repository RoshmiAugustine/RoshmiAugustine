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
    public class QuestionnaireDefaultResponseRuleRepository : BaseRepository<QuestionnaireDefaultResponseRule>, IQuestionnaireDefaultResponseRuleRepository
    {
        private readonly IMapper mapper;
        private readonly ILogger<QuestionnaireDefaultResponseRuleRepository> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="QuestionnaireDefaultResponseRuleRepository"/> class.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public QuestionnaireDefaultResponseRuleRepository(ILogger<QuestionnaireDefaultResponseRuleRepository> logger, OpeekaDBContext dbContext, IMapper mapper)
            : base(dbContext)
        {
            this.mapper = mapper;
            this.logger = logger;
        }

        /// <summary>
        /// CloneQuestionnaireDefaultResponseRule.
        /// </summary>
        /// <param name="objQuestionnaireDefaultResponseRuleDTO">objQuestionnaireDefaultResponseRuleDTO.</param>
        /// <returns></returns>
        public QuestionnaireDefaultResponseRule CloneQuestionnaireDefaultResponseRule(QuestionnaireDefaultResponseRuleDTO objQuestionnaireDefaultResponseRuleDTO)
        {
            try
            {
                QuestionnaireDefaultResponseRule questionnaireDefaultResponseRule = new QuestionnaireDefaultResponseRule();
                this.mapper.Map<QuestionnaireDefaultResponseRuleDTO, QuestionnaireDefaultResponseRule>(objQuestionnaireDefaultResponseRuleDTO, questionnaireDefaultResponseRule);
                return this.AddAsync(questionnaireDefaultResponseRule).Result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetQuestionnaireDefaultResponse
        /// </summary>
        /// <param name="questionnaireID">questionnaireID.</param>
        /// <returns>QuestionnaireWindowDTO.</returns>
        public List<QuestionnaireDefaultResponseRuleDTO> GetQuestionnaireDefaultResponse(int questionnaireID)
        {
            try
            {
                var query = string.Empty;
                query = @"Select 
                            SR.[QuestionnaireDefaultResponseRuleID],
                            SR.[Name] ,
                            SR.[QuestionnaireID] ,
                            SR.[IsRemoved] ,
                            SR.[UpdateDate],
                            SR.[UpdateUserID],
                            SR.[ClonedQuestionnaireDefaultResponseRuleID],
                            Q.HasDefaultResponseRule
                            from QuestionnaireDefaultResponseRule SR
                            join Questionnaire Q on Q.QuestionnaireID=SR.QuestionnaireID
                            where Q.IsRemoved=0 and SR.IsRemoved=0 and Q.QuestionnaireID=" + questionnaireID;
                var data = ExecuteSqlQuery(query, x => new QuestionnaireDefaultResponseRuleDTO
                {
                    QuestionnaireDefaultResponseRuleID = x["QuestionnaireDefaultResponseRuleID"] == DBNull.Value ? 0 : (int)x["QuestionnaireDefaultResponseRuleID"],
                    Name = x["Name"] == DBNull.Value ? string.Empty : (string)x["Name"],
                    QuestionnaireID = x["QuestionnaireID"] == DBNull.Value ? 0 : (int)x["QuestionnaireID"],
                    IsRemoved = x["IsRemoved"] == DBNull.Value ? false : (bool)x["IsRemoved"],
                    UpdateDate = x["UpdateDate"] == DBNull.Value ? DateTime.Now : (DateTime)x["UpdateDate"],
                    UpdateUserID = x["UpdateUserID"] == DBNull.Value ? 0 : (int)x["UpdateUserID"],
                    ClonedQuestionnaireDefaultResponseRuleID = x["ClonedQuestionnaireDefaultResponseRuleID"] == DBNull.Value ? 0 : (int)x["ClonedQuestionnaireDefaultResponseRuleID"],
                    HasDefaultResponseRule = x["HasDefaultResponseRule"] == DBNull.Value ? false : (bool)x["HasDefaultResponseRule"],
                });
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetQuestionnaireDefaultResponseRule.
        /// </summary>
        /// <param name="questionnaireID">questionnaireID.</param>
        /// <returns>QuestionnaireWindowDTO.</returns>
        public List<QuestionnaireDefaultResponseRuleDTO> GetQuestionnaireDefaultResponseRule(int questionnaireID)
        {
            try
            {
                var query = string.Empty;
                query = @"Select 
                            SR.[QuestionnaireDefaultResponseRuleID],
                            SR.[Name] ,
                            SR.[QuestionnaireID] ,
                            SR.[IsRemoved] ,
                            SR.[UpdateDate],
                            SR.[UpdateUserID],
                            [ClonedQuestionnaireDefaultResponseRuleID] 
                            from QuestionnaireDefaultResponseRule SR
                            join Questionnaire Q on Q.QuestionnaireID=SR.QuestionnaireID
                            where Q.IsRemoved=0 and SR.IsRemoved=0 and Q.QuestionnaireID=" + questionnaireID;
                var data = ExecuteSqlQuery(query, x => new QuestionnaireDefaultResponseRuleDTO
                {
                    QuestionnaireDefaultResponseRuleID = x["QuestionnaireDefaultResponseRuleID"] == DBNull.Value ? 0 : (int)x["QuestionnaireDefaultResponseRuleID"],
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
        /// <param name="QuestionnaireDefaultResponseRule"></param>
        /// <returns>QuestionnaireDefaultResponseRule List</returns>
        public List<QuestionnaireDefaultResponseRule> UpdateBulkQuestionnaireDefaultResponseRule(List<QuestionnaireDefaultResponseRule> questionnaireDefaultResponseRule)
        {
            try
            {
                var res = this.UpdateBulkAsync(questionnaireDefaultResponseRule);
                res.Wait();
                return questionnaireDefaultResponseRule;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// UpdateQuestionnaireDefaultResponseRule.
        /// </summary>
        /// <param name="QuestionnaireDefaultResponseRule">QuestionnaireDefaultResponseRule.</param>
        /// <returns>QuestionnaireDefaultResponseRule</returns>
        public QuestionnaireDefaultResponseRule UpdateQuestionnaireDefaultResponseRule(QuestionnaireDefaultResponseRule QuestionnaireDefaultResponseRule)
        {
            try
            {
                var result = this.UpdateAsync(QuestionnaireDefaultResponseRule).Result;
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// AddQuestionnaireDefaultResponseRule
        /// </summary>
        /// <param name="QuestionnaireDefaultResponseRule"></param>
        /// <returns>QuestionnaireDefaultResponseRule</returns>
        public QuestionnaireDefaultResponseRule AddQuestionnaireDefaultResponseRule(QuestionnaireDefaultResponseRule QuestionnaireDefaultResponseRule)
        {
            try
            {
                var result = this.AddAsync(QuestionnaireDefaultResponseRule).Result;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
