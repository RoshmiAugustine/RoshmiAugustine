// -----------------------------------------------------------------------
// <copyright file="UserRoleRepository.cs" company="Naicoits">
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
using System.Collections.Generic;
namespace Opeeka.PICS.Infrastructure.Data.Repositories
{
    public class UserRoleRepository : BaseRepository<UserRole>, IUserRoleRepository
    {
        private readonly IMapper mapper;
        private readonly ILogger<UserRoleRepository> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRoleRepository"/> class.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public UserRoleRepository(ILogger<UserRoleRepository> logger, OpeekaDBContext dbContext, IMapper mapper)
            : base(dbContext)
        {
            this.mapper = mapper;
            this.logger = logger;
        }

        /// <summary>
        /// Save user role details.
        /// </summary>
        /// <param name="userRoledto"></param>
        /// <param name="UserID"></param>
        public int CreateUserRole(UserRoleDTO userRoledto, int UserID)
        {
            try
            {
                int userRoleId = 0;
                UserRole userRole = new UserRole();
                this.mapper.Map<UserRoleDTO, UserRole>(userRoledto, userRole);
                if (userRoledto != null)
                {
                    userRole.UserID = UserID;
                    userRole = this.AddAsync(userRole).Result;
                    userRoleId = userRole.UserRoleID;
                }
                return userRoleId;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Save bulk user role details.
        /// </summary>
        /// <param name="userRoledto"></param>
        /// <param name="UserID"></param>
        public void CreateBulkUserRole(List<UserRoleDTO> userRoledto)
        {
            try
            {
                List<UserRole> userRole = new List<UserRole>();
                this.mapper.Map<List<UserRoleDTO>, List<UserRole>>(userRoledto, userRole);
                if (userRoledto != null)
                {
                    var sss = this.AddBulkAsync(userRole).IsCompleted;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// To get User Role details by UserID
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns>UserRoleDTO</returns>
        public async Task<UserRoleDTO> GetUserRoleByUserIDAsync(int UserID)
        {
            try
            {
                UserRoleDTO userRoleDTO = new UserRoleDTO();
                UserRole userRole = await this.GetRowAsync(x => x.UserID == UserID);
                this.mapper.Map<UserRole, UserRoleDTO>(userRole, userRoleDTO);
                return userRoleDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<UserRoleDTO>> GetUserRoleList(int userId)
        {
            try
            {
                var userRole = await this.GetAsync(x => x.UserID == userId);
                return this.mapper.Map<List<UserRoleDTO>>(userRole);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// To update User Role details.
        /// </summary>
        /// <param name="userRoleDTO"></param>
        /// <returns>UserRoleDTO</returns>
        public UserRoleDTO UpdateUserRole(UserRoleDTO userRoleDTO)
        {
            try
            {
                UserRole userRole = new UserRole();
                this.mapper.Map<UserRoleDTO, UserRole>(userRoleDTO, userRole);
                var result = this.UpdateAsync(userRole).Result;
                this.mapper.Map<UserRole, UserRoleDTO>(result, userRoleDTO);
                return userRoleDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Save bulk user role details.
        /// </summary>
        /// <param name="userRoledto"></param>
        /// <param name="UserID"></param>
        public async Task DeleteBulkUserRole(List<UserRoleDTO> userRoleDto)
        {
            try
            {
                List<UserRole> userRole = new List<UserRole>();
                this.mapper.Map<List<UserRoleDTO>, List<UserRole>>(userRoleDto, userRole);
                if (userRoleDto != null)
                {
                    await this.DeleteBulkAsync(userRole);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
