// -----------------------------------------------------------------------
// <copyright file="BackgroundProcessLogRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using AutoMapper;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Infrastructure.Data.Repositories.Base;

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{
    public class BackgroundProcessLogRepository : BaseRepository<BackgroundProcessLog>, IBackgroundProcessLogRepository
    {
        private readonly OpeekaDBContext _dbContext;
        private readonly IMapper mapper;

        public BackgroundProcessLogRepository(OpeekaDBContext dbContext, IMapper mapper)
            : base(dbContext)
        {
            this._dbContext = dbContext;
            this.mapper = mapper;
        }

        /// <summary>
        /// GetBackgroundProcessLog.
        /// </summary>
        /// <param name="processName">processName.</param>
        /// <returns>BackgroundProcessLog</returns>
        public BackgroundProcessLog GetBackgroundProcessLog(string processName)
        {
            try
            {
                var result = this.GetRowAsync(x => x.ProcessName == processName).Result;
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// AddBackgroundProcessLog.
        /// </summary>
        /// <param name="backgroundProcessLog">backgroundProcessLog.</param>
        /// <returns>backgroundProcessLog</returns>
        public BackgroundProcessLog AddBackgroundProcessLog(BackgroundProcessLog backgroundProcessLog)
        {
            try
            {
                var result = this.AddAsync(backgroundProcessLog).Result;
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// AddBackgroundProcessLog.
        /// </summary>
        /// <param name="backgroundProcessLog">backgroundProcessLog.</param>
        /// <returns>backgroundProcessLog</returns>
        public BackgroundProcessLog UpdateBackgroundProcessLog(BackgroundProcessLog backgroundProcessLog)
        {
            try
            {
                var result = this.UpdateAsync(backgroundProcessLog).Result;
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
