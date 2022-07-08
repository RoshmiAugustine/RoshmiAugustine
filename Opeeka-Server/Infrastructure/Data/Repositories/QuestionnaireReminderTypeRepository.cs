// -----------------------------------------------------------------------
// <copyright file="QuestionnaireReminderTypeRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using AutoMapper;
using Microsoft.Extensions.Logging;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Infrastructure.Data.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{
    public class QuestionnaireReminderTypeRepository : BaseRepository<QuestionnaireReminderType>, IQuestionnaireReminderTypeRepository
    {
        private readonly IMapper mapper;
        private readonly ILogger<QuestionnaireReminderTypeRepository> logger;
        private readonly OpeekaDBContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="QuestionnaireReminderRule  "/> class.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public QuestionnaireReminderTypeRepository(ILogger<QuestionnaireReminderTypeRepository> logger, OpeekaDBContext dbContext, IMapper mapper)
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
        public async Task<QuestionnaireReminderType> GetQuestionnaireReminderType(int questionnaireReminderTypeID)
        {

            try
            {
                QuestionnaireReminderType questionnaireReminderType = await this.GetRowAsync(x => x.QuestionnaireReminderTypeID == questionnaireReminderTypeID);
                return questionnaireReminderType;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetAllQuestionnaireReminderType.
        /// </summary>
        /// <returns>QuestionnaireReminderType</returns>
        public List<QuestionnaireReminderType> GetAllQuestionnaireReminderType()
        {

            try
            {
                List<QuestionnaireReminderType> questionnaireReminderType = this.GetAllAsync().Result.ToList();
                return questionnaireReminderType;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}