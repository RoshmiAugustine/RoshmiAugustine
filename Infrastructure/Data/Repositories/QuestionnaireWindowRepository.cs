// -----------------------------------------------------------------------
// <copyright file="QuestionnaireWindowRepository.cs" company="Naicoits">
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
    public class QuestionnaireWindowRepository : BaseRepository<QuestionnaireWindow>, IQuestionnaireWindowRepository
    {
        private readonly IMapper mapper;
        private readonly ILogger<QuestionnaireWindow> logger;
        private readonly OpeekaDBContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="QuestionnaireWindow"/> class.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public QuestionnaireWindowRepository(ILogger<QuestionnaireWindow> logger, OpeekaDBContext dbContext, IMapper mapper)
            : base(dbContext)
        {
            this.mapper = mapper;
            this.logger = logger;
            this._dbContext = dbContext;
        }

        /// <summary>
        /// GetQuestionnaireWindow.
        /// </summary>
        /// <param name="questionnaireWindowID">questionnaireWindowID.</param>
        /// <returns>QuestionnaireWindowDTO</returns>
        public async Task<QuestionnaireWindow> GetQuestionnaireWindow(int questionnaireWindowID)
        {

            try
            {
                QuestionnaireWindow questionnaireWindow = await this.GetRowAsync(x => x.QuestionnaireWindowID == questionnaireWindowID);
                return questionnaireWindow;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// UpdateQuestionnaireWindow.
        /// </summary>
        /// <param name="questionnaireWindow">QuestionnaireWindow.</param>
        /// <returns>QuestionnaireWindowDTO.</returns>
        public QuestionnaireWindowDTO UpdateQuestionnaireWindow(QuestionnaireWindow questionnaireWindow)
        {
            try
            {
                var result = this.UpdateAsync(questionnaireWindow).Result;
                QuestionnaireWindowDTO QuestionnaireWindowDTO = new QuestionnaireWindowDTO();
                this.mapper.Map<QuestionnaireWindow, QuestionnaireWindowDTO>(result, QuestionnaireWindowDTO);
                return QuestionnaireWindowDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// AddQuestionnaireWindow.
        /// </summary>
        /// <param name="QuestionnaireWindowDTO">QuestionnaireWindowDTO.</param>
        /// <returns>long</returns>
        public int AddQuestionnaireWindow(QuestionnaireWindowDTO QuestionnaireWindowDTO)
        {
            try
            {
                QuestionnaireWindow QuestionnaireWindow = new QuestionnaireWindow();
                this.mapper.Map(QuestionnaireWindowDTO, QuestionnaireWindow);
                var result = this.AddAsync(QuestionnaireWindow).Result.QuestionnaireWindowID;
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// GetQuestionnaireWindowsByQuestionnaire
        /// </summary>
        /// <param name="questionnaireID"></param>
        /// <returns>QuestionnaireWindowsDTO</returns>
        public List<QuestionnaireWindowsDTO> GetQuestionnaireWindowsByQuestionnaire(int questionnaireID)
        {
            try
            {
                List<QuestionnaireWindowsDTO> questionnaireWindowsDTO = new List<QuestionnaireWindowsDTO>();
                var questionnaireWindows = this._dbContext.QuestionnaireWindows.Where(x => x.QuestionnaireID == questionnaireID).ToList();
                this.mapper.Map<List<QuestionnaireWindow>, List<QuestionnaireWindowsDTO>>(questionnaireWindows, questionnaireWindowsDTO);
                return questionnaireWindowsDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// CloneQuestionnaireWindow
        /// </summary>
        /// <param name="QuestionnaireWindowDTO"></param>
        /// <returns>QuestionnaireWindowsDTO</returns>
        public void CloneQuestionnaireWindow(List<QuestionnaireWindowsDTO> QuestionnaireWindowDTO)
        {
            try
            {
                List<QuestionnaireWindow> questionnaireWindow = new List<QuestionnaireWindow>();
                this.mapper.Map<List<QuestionnaireWindowsDTO>, List<QuestionnaireWindow>>(QuestionnaireWindowDTO, questionnaireWindow);
                this.AddBulkAsync(questionnaireWindow);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetAllQuestionnaireWindows.
        /// </summary>
        /// <param name="questionnaireID"></param>
        /// <returns>QuestionnaireWindowsDTO</returns>
        public List<QuestionnaireWindow> GetAllQuestionnaireWindows(int questionnaireID)
        {
            try
            {
                var questionnaireWindows = this._dbContext.QuestionnaireWindows.Where(x => x.QuestionnaireID == questionnaireID).ToList();
                return questionnaireWindows;
            }
            catch (Exception)
            {
                throw;
            }
        }                
    }
}