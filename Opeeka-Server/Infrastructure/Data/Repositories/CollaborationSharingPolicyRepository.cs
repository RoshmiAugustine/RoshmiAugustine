// -----------------------------------------------------------------------
// <copyright file="AgencySharingPolicyRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using AutoMapper;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Infrastructure.Data.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{
    public class CollaborationSharingPolicyRepository : BaseRepository<CollaborationSharingPolicy>, ICollaborationSharingPolicyRepository
    {
        private readonly IMapper mapper;
        private readonly OpeekaDBContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="CollaborationSharingPolicyRepository"/> class.
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public CollaborationSharingPolicyRepository(OpeekaDBContext dbContext, IMapper mapper)
            : base(dbContext)
        {
            this.mapper = mapper;
            this._dbContext = dbContext;
        }

        /// <summary>
        /// AddCollaborationSharingPolicy
        /// </summary>
        /// <param name="collaborationSharingPolicyDTO"></param>
        /// <returns>CollaborationSharingPolicyDTO</returns>
        public CollaborationSharingPolicyDTO AddCollaborationSharingPolicy(CollaborationSharingPolicyDTO collaborationSharingPolicyDTO)
        {
            try
            {
                CollaborationSharingPolicy collaborationSharingPolicy = new CollaborationSharingPolicy();
                this.mapper.Map<CollaborationSharingPolicyDTO, CollaborationSharingPolicy>(collaborationSharingPolicyDTO, collaborationSharingPolicy);
                var result = this.AddAsync(collaborationSharingPolicy).Result;
                this.mapper.Map<CollaborationSharingPolicy, CollaborationSharingPolicyDTO>(result, collaborationSharingPolicyDTO);
                return collaborationSharingPolicyDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// To update CollaborationSharingPolicy details.
        /// </summary>
        /// <param name="collaborationSharingPolicyDTO">id.</param>
        /// <returns>List of summaries.</returns>
        public CollaborationSharingPolicyDTO UpdateCollaborationSharingPolicy(CollaborationSharingPolicyDTO collaborationSharingPolicyDTO)
        {
            try
            {
                CollaborationSharingPolicy collaborationSharingPolicy = new CollaborationSharingPolicy();
                this.mapper.Map<CollaborationSharingPolicyDTO, CollaborationSharingPolicy>(collaborationSharingPolicyDTO, collaborationSharingPolicy);
                var result = this.UpdateAsync(collaborationSharingPolicy).Result;
                CollaborationSharingPolicyDTO updated = new CollaborationSharingPolicyDTO();
                return this.mapper.Map<CollaborationSharingPolicy, CollaborationSharingPolicyDTO>(result, updated);

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// To get details CollaborationSharingPolicy.
        /// </summary>
        /// <param "Guid>id.</param>
        /// <returns>.AgencyPersonCollaborationDTO</returns>
        public async Task<CollaborationSharingPolicyDTO> GetCollaborationSharingPolicy(int id)
        {
            try
            {
                CollaborationSharingPolicyDTO collaborationSharingPolicyDTO = new CollaborationSharingPolicyDTO();
                CollaborationSharingPolicy collaborationSharingPolicy = await this.GetRowAsync(x => x.CollaborationSharingPolicyID == id);
                this.mapper.Map<CollaborationSharingPolicy, CollaborationSharingPolicyDTO>(collaborationSharingPolicy, collaborationSharingPolicyDTO);

                return collaborationSharingPolicyDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetAllCollaborationSharingPolicy.
        /// </summary>
        /// <returns>CollaborationSharingPolicyDTO.</returns>
        public async Task<List<CollaborationSharingPolicyDTO>> GetAllCollaborationSharingPolicy()
        {
            try
            {
                var collaborationSharingPolicyList = await this.GetAllAsync();
                return this.mapper.Map<List<CollaborationSharingPolicyDTO>>(collaborationSharingPolicyList);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
