// -----------------------------------------------------------------------
// <copyright file="ResponseRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Infrastructure.Data.Repositories.Base;

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{
    public class ResponseRepository : BaseRepository<Response>, IResponseRepository
    {
        private readonly IMapper mapper;
        private readonly ILogger<ResponseRepository> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AssessmentResponseRepository"/> class.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public ResponseRepository(ILogger<ResponseRepository> logger, OpeekaDBContext dbContext, IMapper mapper)
            : base(dbContext)
        {
            this.mapper = mapper;
            this.logger = logger;
        }


        /// <summary>
        /// To get AssessmentResponse details.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns>AssessmentResponse.</returns>
        public async Task<ResponseDTO> GetResponse(int id)
        {
            try
            {
                ResponseDTO responseDTO = new ResponseDTO();
                var response = await this.GetRowAsync(x => x.ResponseID == id);
                responseDTO = this.mapper.Map<ResponseDTO>(response);

                return responseDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
