// -----------------------------------------------------------------------
// <copyright file="UserRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Infrastructure.Data.Repositories.Base;
using System.Collections.Generic;
using Opeeka.PICS.Domain.Interfaces;
using Opeeka.PICS.Infrastructure.Enums;
using System.Linq;

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{
    public class PermissionRepository : BaseRepository<Permission>, IPermissionRepository
    {
        private readonly IMapper mapper;
        private readonly ILogger<PermissionRepository> logger;
        private readonly ICache cache;
        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public PermissionRepository(ILogger<PermissionRepository> logger, OpeekaDBContext dbContext, IMapper mapper, ICache caching)
            : base(dbContext)
        {
            this.mapper = mapper;
            this.logger = logger;
            this.cache = caching;
        }

        /// <summary>
        /// GetUserPermissionsByUserID
        /// </summary>
        /// <param name="userid">
        /// <param name = "applicationObjectType"></param>
        /// <returns></returns>
        public List<ApplicationObjectDTO> GetUserPermissionsByUserID(long userID, List<string> applicationObjectTypes)
        {
            try
            {
                var applicationObjectTypeString = String.Join("','", applicationObjectTypes.ToArray());
                string sql = @"
                select AO.Name, AOT.Name as ApplicationObjectTypeName, P.OperationTypeID, OT.Name from ApplicationObject AO 
                JOIN info.Permission P ON AO.ApplicationObjectId = P.ApplicationObjectId
                JOIN info.OperationType OT ON P.OperationTypeID = OT.OperationTypeID
                JOIN info.RolePermission RP ON RP.PermissionId = p.PermissionId
                JOIN UserRole UR ON UR.UserRoleId = RP.UserRoleId
                JOIN info.SystemRole SR ON UR.SystemRoleID = SR.SystemRoleID 
                JOIN [User] U ON U.UserId = UR.UserId
                JOIN ApplicationObjectType AOT ON AO.ApplicationObjectTypeId = AOT.ApplicationObjectTypeId WHERE  U.UserId =" + userID + " AND AOT.Name IN('" + applicationObjectTypeString + "')";
                //  where AOT.Name = '"+"APIEndPoint"+"'"

                var result = ExecuteSqlQuery(sql, x => new ApplicationObjectDTO { Name = (string)x[0], ApplicationObjectTypeName = (string)x[1], OperationTypeID = (int)x[2], OperationTypeName = (string)x[3] });
                return result;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public List<ApplicationObjectDTO> GetUserPermissionsByRoleID(int roleID, List<string> applicationObjectTypes)
        {
            try
            {
                List<ApplicationObjectDTO> permissionList = new List<ApplicationObjectDTO>();
                string cachingKey = PCISEnum.Caching.APIPermissions;
                applicationObjectTypes.ForEach(
                    x => cachingKey += x.Substring(0, 1)
                );
                var applicationObjectTypeString = String.Join("','", applicationObjectTypes.ToArray());
                permissionList = this.cache.Get<List<ApplicationObjectDTO>>(cachingKey);
                if (permissionList == null || permissionList?.Count == 0)
                {
                    logger.LogWarning("Data from DB");
                    // var applicationObjectTypeString = String.Join("','", applicationObjectTypes.ToArray());
                    string sql = @"
                select AO.Name, AOT.Name as ApplicationObjectTypeName, P.OperationTypeID, 
                OT.Name, SRP.SystemRoleID
				from info.SystemRolePermission SRP
				JOIN info.Permission P ON SRP.PermissionID = P.PermissionID
				JOIN ApplicationObject AO ON AO.ApplicationObjectID = P.ApplicationObjectID
				JOIN info.OperationType OT ON P.OperationTypeID = OT.OperationTypeID
                JOIN ApplicationObjectType AOT ON AO.ApplicationObjectTypeId = AOT.ApplicationObjectTypeId
                WHERE " +
                    "AOT.Name IN('" + applicationObjectTypeString + "')";

                    permissionList = ExecuteSqlQuery(sql, x => new ApplicationObjectDTO
                    {
                        Name = (string)x[0],
                        ApplicationObjectTypeName = (string)x[1],
                        OperationTypeID = (int)x[2],
                        OperationTypeName = (string)x[3],
                        SystemRoleID = (int)x[4]
                    });
                    TimeSpan span = new TimeSpan(1, 00, 0);
                    this.cache.Post(cachingKey, permissionList, span);
                }
                permissionList = permissionList.Where(x => x.SystemRoleID == roleID).ToList();
                return permissionList;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public List<ApplicationObjectDTO> GetUserSharedPermissionsByRoleID(int roleID, List<string> applicationObjectTypes, int agencySharingPolicyID = 0, int collaborationSharingPolicyID = 0, bool isActivePerson = true)
        {
            try
            {
                List<ApplicationObjectDTO> permissionList = new List<ApplicationObjectDTO>();
                var applicationObjectTypeString = String.Join("','", applicationObjectTypes.ToArray());
                
                // var applicationObjectTypeString = String.Join("','", applicationObjectTypes.ToArray());
                string sql = @"
            select AO.Name, AOT.Name as ApplicationObjectTypeName, P.OperationTypeID, 
            OT.Name, SRP.SystemRoleID
			from info.SharingRolePermission SHRP 
            JOIN info.SystemRolePermission SRP ON SHRP.SystemRolePermissionID = SRP.SystemRolePermissionID
			JOIN info.Permission P ON SRP.PermissionID = P.PermissionID
			JOIN ApplicationObject AO ON AO.ApplicationObjectID = P.ApplicationObjectID
			JOIN info.OperationType OT ON P.OperationTypeID = OT.OperationTypeID
            JOIN ApplicationObjectType AOT ON AO.ApplicationObjectTypeId = AOT.ApplicationObjectTypeId
            WHERE " +
                "AOT.Name IN('" + applicationObjectTypeString + "') ";
                if (agencySharingPolicyID != 0)
                {
                    sql = sql + "AND SHRP.AgencySharingPolicyID = " + agencySharingPolicyID;
                }
                if (collaborationSharingPolicyID != 0)
                {
                    sql = sql + " AND SHRP.CollaborationSharingPolicyID =" + collaborationSharingPolicyID;
                }
                if (!isActivePerson)
                {
                    sql = sql + " AND SHRP.AllowInactiveAccess = 1";
                }

                permissionList = ExecuteSqlQuery(sql, x => new ApplicationObjectDTO
                {
                    Name = (string)x[0],
                    ApplicationObjectTypeName = (string)x[1],
                    OperationTypeID = (int)x[2],
                    OperationTypeName = (string)x[3],
                    SystemRoleID = (int)x[4]
                });
                   
                permissionList = permissionList.Where(x => x.SystemRoleID == roleID).ToList();
                return permissionList;
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
