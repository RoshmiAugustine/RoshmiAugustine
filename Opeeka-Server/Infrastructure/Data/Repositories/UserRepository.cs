// -----------------------------------------------------------------------
// <copyright file="UserRepository.cs" company="Naicoits">
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
using System.Linq;

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly IMapper mapper;
        private readonly ILogger<UserRepository> logger;
        private readonly OpeekaDBContext dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public UserRepository(ILogger<UserRepository> logger, OpeekaDBContext dbContext, IMapper mapper)
            : base(dbContext)
        {
            this.mapper = mapper;
            this.logger = logger;
            this.dbContext = dbContext;
        }

        /// <summary>
        /// Save user details.
        /// </summary>
        /// <param name="userdto"></param>
        /// <returns>Object of Users</returns>
        public int CreateUser(UsersDTO userdto)
        {
            int UserID = 0;
            try
            {
                User user = new User();
                this.mapper.Map<UsersDTO, User>(userdto, user);
                if (userdto != null)
                {
                    user.IsActive = true;
                    UserID = this.AddAsync(user).Result.UserID;
                    return UserID;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return UserID;
        }

        /// <summary>
        /// To get Users details by UsersID
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public async Task<UsersDTO> GetUsersByUsersIDAsync(int UserID)
        {
            try
            {
                UsersDTO userDTO = new UsersDTO();
                User user = await this.GetRowAsync(x => x.UserID == UserID);
                this.mapper.Map<User, UsersDTO>(user, userDTO);
                return userDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// To get Users details by UsersID
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public async Task<User> GetUsersDetailsByUsersIDAsync(int UserID)
        {
            try
            {
                User user = await this.GetRowAsync(x => x.UserID == UserID);
                return user;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// To update user details.
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns>UsersDTO</returns>
        public UsersDTO UpdateUser(UsersDTO userDTO)
        {
            try
            {
                User user = new User();
                this.mapper.Map<UsersDTO, User>(userDTO, user);
                var result = this.UpdateAsync(user).Result;
                this.mapper.Map<User, UsersDTO>(result, userDTO);
                return userDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<ApplicationObjectDTO> GetUserPermissionsByUserID(long userID, string applicationObjectTypes)
        {
            try
            {
                // var applicationObjectTypeString = String.Join("','", applicationObjectTypes.ToArray());
                string sql = @"
                select AO.Name, AOT.Name as ApplicationObjectTypeName, P.OperationTypeID, OT.Name from ApplicationObject AO 
                JOIN info.Permission P ON AO.ApplicationObjectId = P.ApplicationObjectId
                JOIN info.OperationType OT ON P.OperationTypeID = OT.OperationTypeID
                JOIN info.RolePermission RP ON RP.PermissionId = p.PermissionId
                JOIN UserRole UR ON UR.UserRoleId = RP.UserRoleId
                JOIN info.SystemRole SR ON UR.SystemRoleID = SR.SystemRoleID 
                JOIN [User] U ON U.UserId = UR.UserId
                JOIN ApplicationObjectType AOT ON AO.ApplicationObjectTypeId = AOT.ApplicationObjectTypeId WHERE  U.UserId =" + userID + " AND AOT.Name = '" + applicationObjectTypes + "'";
                //  where AOT.Name = '"+"APIEndPoint"+"'"

                var result = ExecuteSqlQuery(sql, x => new ApplicationObjectDTO { Name = (string)x[0], ApplicationObjectTypeName = (string)x[1], OperationTypeID = (int)x[2], OperationTypeName = (string)x[3] });
                return result;
            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// To get Users details by UsersID
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public async Task<UsersDTO> GetUserByUserNameAndAgencyIdAsync(string email, long agencyId)
        {
            try
            {
                UsersDTO userDTO = new UsersDTO();
                User user = await this.GetRowAsync(x => x.UserName == email && (x.AgencyID == agencyId || x.AgencyID == 0 || x.AgencyID == null));
                this.mapper.Map<User, UsersDTO>(user, userDTO);
                return userDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// To get Users roles by UsersID
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public async Task<List<SystemRole>> GetUserRoleList(long userId)
        {
            try
            {
                string sql = @"
            select SR.SystemRoleID, SR.Name from info.SystemRole SR 
                    JOIN UserRole UR ON SR.SystemRoleID = UR.SystemRoleId 
                    where UR.UserID = " + userId;
                var result = ExecuteSqlQuery(sql, x => new SystemRole { SystemRoleID = (int)x[0], Name = (string)x[1] });
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetUserProfile.
        /// </summary>
        /// <param name="userID">userID.</param>
        /// <param name="IsSuperAdmin">IsSuperAdmin.</param>
        /// <returns>UserProfileDTO.</returns>
        public UserProfileDTO GetUserProfile(int userID, bool IsSuperAdmin)
        {
            try
            {
                var query = string.Empty;
                if (IsSuperAdmin)
                {
                    query = @"select top 1 case when A.Name is null then U.Name else A.Name end  as Name,case when  A.Title is null then 'Super Admin' else A.Title end as Title,A.Phone,A.Phone2,U.UserName as Email,NULL Address1,AzureFileName ,0 as HelperTitleID,'' AS FirstName ,'' AS MiddleName,'' AS LastName, null AS HelperIndex, 0 as CountryID,null as CountryName, U.AgencyID, AG.Name AS AgencyName,AG.Abbrev AS AgencyAbbrev
                        from   [dbo].[User] U 
                        left join (select  H.UserID,H.FirstName+' '+H.LastName as Name,HT.Name Title,H.Phone,H.Phone2,H.Email
                                   from [dbo].[Helper] H  
									Left JOIN [info].[HelperTitle] HT ON HT.HelperTitleID = H.HelperTitleID
                                   ) A ON U.UserID=A.UserID 
						left join [UserProfile] UP  on UP.UserID = U.UserID 
                        Left join Agency AG on U.AgencyID = AG.AgencyID
						left join  [File] F on UP.ImageFileID=f.FileID 
                        where U.UserID =" + userID + " order by F.FileID desc";
                }
                else
                {
                    query = @"Select top 1 H.FirstName+' '+ISNULL(H.MiddleName,'')+' ' +H.LastName as Name,H.Phone,H.Phone2,H.Email,F.AzureFileName,A.Address1 Address1,HT.[Name] Title ,HT.HelperTitleID,H.FirstName ,H.MiddleName,H.LastName,H.HelperIndex,C.CountryID,C.Name as CountryName,H.AgencyID, AG.Name AS AgencyName,AG.Abbrev AS AgencyAbbrev
                            from [User] U 
                            join Helper H on U.UserID = H.UserID 
                            Left JOIN [info].[HelperTitle] HT ON HT.HelperTitleID = H.HelperTitleID
                            left join [HelperAddress] HA on HA.HelperID=H.HelperID
                            left join [Address] A on A.AddressID=HA.AddressID
                            left join [UserProfile] UP  on UP.UserID = H.UserID 
                            left join  [File] F on UP.ImageFileID=f.FileID 
                            left join [info].[Country] C on C.CountryID = A.CountryId
                            Left join Agency AG on H.AgencyID = AG.AgencyID
                            where U.UserID = " + userID + " order by F.FileID desc";
                }
                var data = ExecuteSqlQuery(query, x => new UserProfileDTO
                {
                    Name = x["Name"] == DBNull.Value ? null : (string)x["Name"],
                    Email = x["Email"] == DBNull.Value ? null : (string)x["Email"],
                    Phone1 = x["Phone"] == DBNull.Value ? null : (string)x["Phone"],
                    Phone2 = x["Phone2"] == DBNull.Value ? null : (string)x["Phone2"],
                    Address = x["Address1"] == DBNull.Value ? null : (string)x["Address1"],
                    Title = x["Title"] == DBNull.Value ? null : (string)x["Title"],
                    AzureFileName = x["AzureFileName"] == DBNull.Value ? null : (string)x["AzureFileName"],
                    HelperTitleID = x["HelperTitleID"] == DBNull.Value ? 0 : (int)x["HelperTitleID"],
                    FirstName = x["FirstName"] == DBNull.Value ? null : (string)x["FirstName"],
                    MiddleName = x["MiddleName"] == DBNull.Value ? null : (string)x["MiddleName"],
                    LastName = x["LastName"] == DBNull.Value ? null : (string)x["LastName"],
                    HelperIndex = x["HelperIndex"] == DBNull.Value ? Guid.Empty : (Guid)x["HelperIndex"],
                    CountryName = x["CountryName"] == DBNull.Value ? null : (string)x["CountryName"],
                    CountryID = x["CountryID"] == DBNull.Value ? 0 : (int)x["CountryID"],
                    AgencyID = x["AgencyID"] == DBNull.Value ? 0 : (long)x["AgencyID"],
                    AgencyName = x["AgencyName"] == DBNull.Value ? string.Empty : (string)x["AgencyName"],
                    AgencyAbbrev = x["AgencyAbbrev"] == DBNull.Value ? string.Empty : (string)x["AgencyAbbrev"],
                }).LastOrDefault();
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public UserProfileDTO GetUserProfilePicDetails(int userID)
        {
            try
            {
                var query = string.Empty;

                query = @"Select F.AzureFileName,f.FileID,UserProfileID from  [UserProfile] UP  left join  [File] F on UP.ImageFileID=f.FileID where UP.UserID=" + userID;

                var data = ExecuteSqlQuery(query, x => new UserProfileDTO
                {
                    AzureFileName = x["AzureFileName"] == DBNull.Value ? null : (string)x["AzureFileName"],
                    ImageFileID = x["FileID"] == DBNull.Value ? 0 : (long)x["FileID"],
                    UserProfileID = x["UserProfileID"] == DBNull.Value ? 0 : (int)x["UserProfileID"],
                }).LastOrDefault();
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// CreateUserBulk.
        /// </summary>
        /// <param name="userList">userList.</param>
        /// <returns>UsersDTO.</returns>
        public List<UsersDTO> CreateUserBulk(List<UsersDTO> userList)
        {
            try
            {
                List<User> user = new List<User>();
                this.mapper.Map<List<UsersDTO>, List<User>>(userList, user);
                var res = this.UpdateBulkAsync(user);
                res.Wait();
                return userList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IReadOnlyList<User>> GetUserListByGUID(List<Guid> UserIndexGuids)
        {
            try
            {
                var response = await this.GetAsync(x => UserIndexGuids.Contains(x.UserIndex));
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       public List<User> UpdateUserBulk(List<User> userList)
        {
            try
            {
                var res = this.UpdateBulkAsync(userList);
                res.Wait();
                return userList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// GetUserEmail.
        /// </summary>
        /// <param name="userID">userID.</param>
        /// <returns>UsersDTO.</returns>
        public UsersDTO GetUserEmail(int userID)
        {
            try
            {
                var query = string.Empty;
                query = @"SELECT [UserID],[UserIndex],[UserName],[LastLogin],[Name],[IsActive],[AgencyID],[AspNetUserID]  FROM [dbo].[User] where UserID=" + userID;
                var data = ExecuteSqlQuery(query, x => new UsersDTO
                {
                    Email = x["UserName"] == DBNull.Value ? null : (string)x["UserName"],
                    Name = x["Name"] == DBNull.Value ? null : (string)x["Name"],
                    UserID = x["UserID"] == DBNull.Value ? 0 : (int)x["UserID"],
                }).LastOrDefault();
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// ValidateHelperEmail.
        /// </summary>
        /// <param name="nameCSV">nameCSV.</param>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>UsersDTO.</returns>
        public List<UsersDTO> ValidateHelperEmail(string nameCSV, long agencyID)
        {
            List<UsersDTO> usersDTO = new List<UsersDTO>();
            try
            {
                nameCSV = string.IsNullOrEmpty(nameCSV) ? null : nameCSV.ToLower();
                var query = string.Empty;
                query = @$"SELECT  [UserID],[UserIndex],[UserName]
                        FROM    [dbo].[User] where LOWER([UserName]) in ({nameCSV})
                        and  AgencyID = {agencyID}";
                usersDTO = ExecuteSqlQuery(query, x => new UsersDTO
                {
                    UserIndex = (Guid)x["UserIndex"],
                    UserName = x["UserName"] == DBNull.Value ? null : (string)x["UserName"]
                });
                return usersDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// UserLastLoginTime
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public UsersDTO UserLastLoginTime(int userId)
        {
            UsersDTO usersDTO = new UsersDTO();
            try
            {
                var query = string.Empty;
                query = @"SELECT [LastLogin],[NotificationViewedOn] FROM [dbo].[User] where UserID=" + userId;
                var data = ExecuteSqlQuery(query, x => new UsersDTO
                {
                    LastLogin = x[0] == DBNull.Value ? DateTime.UtcNow : (DateTime)x["LastLogin"],
                    NotificationViewedOn = x[0] == DBNull.Value ? DateTime.UtcNow : (DateTime)x["NotificationViewedOn"]
                }).LastOrDefault();
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
