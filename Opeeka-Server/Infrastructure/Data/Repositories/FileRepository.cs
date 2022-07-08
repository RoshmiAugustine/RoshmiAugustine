// -----------------------------------------------------------------------
// <copyright file="FileRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Infrastructure.Data.Repositories.Base;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Opeeka.PICS.Domain.DTO;

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{
    public class FileRepository : BaseRepository<File>, IFileRepository
    {
        private readonly IMapper mapper;
        private readonly ILogger<FileRepository> logger;
        private readonly OpeekaDBContext _dbContext;
        /// <summary>
        /// Initializes a new instance of the <see cref="FileRepository"/> class.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public FileRepository(ILogger<FileRepository> logger, OpeekaDBContext dbContext, IMapper mapper)
            : base(dbContext)
        {
            this.mapper = mapper;
            this.logger = logger;
            _dbContext = dbContext;
        }

        /// <summary>
        /// AddFile.
        /// </summary>
        /// <param name="fileDTO">fileDTO.</param>
        /// <returns>long.</returns>
        public long AddFile(FileDTO fileDTO)
        {
            try
            {
                File file = new File();
                this.mapper.Map<FileDTO, File>(fileDTO, file);
                var result = this.AddAsync(file).Result.FileID;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteFile(File file)
        {
            try
            {
                this.DeleteAsync(file).Wait();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public File GetFile(long fileID)
        {
            try
            {
                return this.GetRowAsync(x => x.FileID == fileID).Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
