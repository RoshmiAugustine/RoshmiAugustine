// -----------------------------------------------------------------------
// <copyright file="UserProfileRepository.cs" company="Naicoits">
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
    public class UserProfileRepository : BaseRepository<UserProfile>, IUserProfileRepository
    {
        private readonly IMapper mapper;
        private readonly ILogger<UserProfileRepository> logger;
        private readonly OpeekaDBContext _dbContext;
        /// <summary>
        /// Initializes a new instance of the <see cref="FileRepository"/> class.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public UserProfileRepository(ILogger<UserProfileRepository> logger, OpeekaDBContext dbContext, IMapper mapper)
            : base(dbContext)
        {
            this.mapper = mapper;
            this.logger = logger;
            _dbContext = dbContext;
        }

        /// <summary>
        /// AddUserProfile.
        /// </summary>
        /// <param name="userProfileDTO">userProfileDTO.</param>
        /// <returns>int</returns>
        public int AddUserProfile(UserProfileDTO userProfileDTO)
        {
            try
            {
                UserProfile userProfile = new UserProfile();
                this.mapper.Map<UserProfileDTO, UserProfile>(userProfileDTO, userProfile);
                var result = this.AddAsync(userProfile).Result.UserProfileID;
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteUserProfile(UserProfile userProfile)
        {
            try
            {
                this.DeleteAsync(userProfile).Wait();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<UserProfile> GetUserProfileByID(int userID)
        {
            return this.GetRowAsync(x => x.UserID == userID);
        }
    }
}
