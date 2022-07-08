// -----------------------------------------------------------------------
// <copyright file="PermissionService.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------


using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Opeeka.PICS.Domain.Services
{
    public class PermissionService : BaseService, IPermissionService
    {
        private IConfiguration _config;
        private readonly IApplicationObjectTypeService applicationObjectTypeService;
        private readonly IPermissionRepository _permissionRepository;
        private readonly IAgencySharingRepository _agencySharingRepository;
        private readonly ICollaborationSharingRepository _collaborationSharingRepository;

        private readonly ILogger<JWTTokenService> _logger;
        public PermissionService(IConfiguration config, IApplicationObjectTypeService applicationObjectTypeService, IPermissionRepository permissionRepository, ILogger<JWTTokenService> logger,
            IConfigurationRepository configRepo, IHttpContextAccessor httpContext, IAgencySharingRepository agencySharingRepository, ICollaborationSharingRepository collaborationSharingRepository)
        : base(configRepo, httpContext)
        {
            _config = config;
            this.applicationObjectTypeService = applicationObjectTypeService;
            _permissionRepository = permissionRepository;
            this._logger = logger;
            _agencySharingRepository = agencySharingRepository;
            _collaborationSharingRepository = collaborationSharingRepository;
        }

        // public List<PermissionModuleDTO> GetUserPermissionsByUserID(long userID, List<string> applicationObjectTypes)
        // {
        //     try
        //     {
        //         var result = _permissionRepository.GetUserPermissionsByUserID(userID, applicationObjectTypes);
        //         List<PermissionModuleDTO> permissionModuleList = new List<PermissionModuleDTO>();
        //         foreach (var item in result)
        //         {
        //             PermissionModuleDTO permissionModuleDTO = new PermissionModuleDTO();
        //             var applicationObjectNameSplit = item.Name.Split("/").ToList();
        //             int arrayLength = applicationObjectNameSplit.Count;

        //             if (permissionModuleList.Any(x => x.ModuleName == applicationObjectNameSplit[0]))
        //             {
        //                 permissionModuleDTO = permissionModuleList.Where(x => x.ModuleName == applicationObjectNameSplit[0]).FirstOrDefault();
        //                 List<PermissionDTO> permissionList = new List<PermissionDTO>();
        //                 if (arrayLength > 1)
        //                 {
        //                     PermissionDTO permission = new PermissionDTO();

        //                     permissionList = permissionModuleDTO.Permissions;
        //                     if (permissionList.Count > 0 && permissionList.Any(x => x.Name == applicationObjectNameSplit[1]))
        //                     {
        //                         permission = permissionList.Where(x => x.Name == applicationObjectNameSplit[1]).FirstOrDefault();
        //                         if (arrayLength == 2)
        //                         {
        //                             if (permission.OperationTypes != null && permission.OperationTypes.Count > 0)
        //                             {
        //                                 if (permission.OperationTypes.Any(x => x != item.OperationTypeName))
        //                                 {
        //                                     permission.OperationTypes.Add(item.OperationTypeName);
        //                                 }
        //                             }
        //                             else
        //                             {
        //                                 permission.OperationTypes = new List<string>();
        //                                 permission.OperationTypes.Add(item.OperationTypeName);
        //                             }
        //                         }
        //                         if (arrayLength > 2)
        //                         {
        //                             List<PermissionDTO> subPermissionList = new List<PermissionDTO>();
        //                             subPermissionList = permission.SubPermissions != null ? permission.SubPermissions : new List<PermissionDTO>();
        //                             PermissionDTO subPermission = new PermissionDTO();
        //                             if (subPermissionList.Count > 0 && subPermissionList.Any(x => x.Name == applicationObjectNameSplit[2]))
        //                             {
        //                                 subPermission = subPermissionList.Where(x => x.Name == applicationObjectNameSplit[2]).FirstOrDefault();
        //                                 if (arrayLength == 3)
        //                                 {
        //                                     if (subPermission.OperationTypes != null && subPermission.OperationTypes.Count > 0)
        //                                     {
        //                                         if (subPermission.OperationTypes.Any(x => x != item.OperationTypeName))
        //                                         {
        //                                             subPermission.OperationTypes.Add(item.OperationTypeName);
        //                                         }
        //                                     }
        //                                     else
        //                                     {
        //                                         subPermission.OperationTypes = new List<string>();
        //                                         subPermission.OperationTypes.Add(item.OperationTypeName);
        //                                     }
        //                                 }
        //                                 subPermissionList.Where(x => x.Name == applicationObjectNameSplit[2]).ToList().ForEach(x => x = subPermission);
        //                             }
        //                             else
        //                             {
        //                                 subPermission.Name = applicationObjectNameSplit[2];
        //                                 subPermission.ApplicationObjectType = item.ApplicationObjectTypeName;
        //                                 subPermission.OperationTypes = new List<string>();
        //                                 subPermission.OperationTypes.Add(item.OperationTypeName);
        //                                 subPermissionList.Add(subPermission);
        //                             }
        //                             permission.SubPermissions = subPermissionList;
        //                         }
        //                         permissionList.Where(x => x.Name == applicationObjectNameSplit[1]).ToList().ForEach(x => x = permission); ;
        //                     }
        //                     else
        //                     {
        //                         permission.Name = applicationObjectNameSplit[1];
        //                         permission.ApplicationObjectType = item.ApplicationObjectTypeName;
        //                         permission.OperationTypes = new List<string>();
        //                         permission.OperationTypes.Add(item.OperationTypeName);
        //                         permissionList.Add(permission);
        //                     }
        //                     permissionModuleDTO.Permissions = permissionList;
        //                 }
        //                 else
        //                 {
        //                     permissionList = permissionModuleDTO.Permissions;
        //                     if (permissionList.Count > 0 && permissionList.Any(x => x.Name == applicationObjectNameSplit[0]))
        //                     {
        //                         var permission = permissionList.Where(x => x.Name == applicationObjectNameSplit[0]).FirstOrDefault();
        //                         permission.OperationTypes = new List<string>();
        //                         permission.OperationTypes.Add(item.OperationTypeName);
        //                         permission.ApplicationObjectType = item.ApplicationObjectTypeName;
        //                         permissionList.Where(x => x.Name == applicationObjectNameSplit[0]).ToList().ForEach(x => x = permission);
        //                     }
        //                     else
        //                     {
        //                         PermissionDTO permission = new PermissionDTO();
        //                         permission.Name = applicationObjectNameSplit[0];
        //                         permission.OperationTypes = new List<string>();
        //                         permission.OperationTypes.Add(item.OperationTypeName);
        //                         permission.ApplicationObjectType = item.ApplicationObjectTypeName;
        //                         permissionList.Add(permission);
        //                     }
        //                     permissionModuleDTO.Permissions = permissionList;
        //                 }
        //                 permissionModuleList.Where(x => x.ModuleName == applicationObjectNameSplit[0]).ToList().ForEach(x => x = permissionModuleDTO);
        //             }
        //             else
        //             {
        //                 permissionModuleDTO.ModuleName = applicationObjectNameSplit[0];
        //                 List<PermissionDTO> permissionList = new List<PermissionDTO>();
        //                 if (arrayLength == 1)
        //                 {
        //                     permissionModuleDTO.Permissions = new List<PermissionDTO>();
        //                     PermissionDTO permission = new PermissionDTO();
        //                     permission.Name = applicationObjectNameSplit[0];
        //                     permission.OperationTypes = new List<string>();
        //                     permission.OperationTypes.Add(item.OperationTypeName);
        //                     permission.ApplicationObjectType = item.ApplicationObjectTypeName;
        //                     permissionModuleDTO.Permissions.Add(permission);
        //                 }
        //                 // var applicationObjectNameSplit = item.Name.Split("/").ToList();
        //                 else if (arrayLength > 1)
        //                 {
        //                     PermissionDTO permission = new PermissionDTO();
        //                     permission.Name = applicationObjectNameSplit[1];
        //                     if (arrayLength == 2)
        //                     {
        //                         permission.OperationTypes = new List<string>();
        //                         permission.OperationTypes.Add(item.OperationTypeName);
        //                         permission.ApplicationObjectType = item.ApplicationObjectTypeName;
        //                     }
        //                     if (arrayLength > 2)
        //                     {
        //                         permission.SubPermissions = new List<PermissionDTO>();
        //                         PermissionDTO subPermission = new PermissionDTO();
        //                         subPermission.Name = applicationObjectNameSplit[2];
        //                         subPermission.OperationTypes = new List<string>();
        //                         subPermission.OperationTypes.Add(item.OperationTypeName);
        //                         subPermission.ApplicationObjectType = item.ApplicationObjectTypeName;
        //                         permission.SubPermissions.Add(subPermission);
        //                     }
        //                     permissionList.Add(permission);
        //                     permissionModuleDTO.Permissions = new List<PermissionDTO>();
        //                     permissionModuleDTO.Permissions = permissionList;

        //                 }
        //                 permissionModuleList.Add(permissionModuleDTO);

        //             }


        //         }
        //         return permissionModuleList;
        //     }
        //     catch(Exception)
        //     {
        //         throw;
        //     }
        // }

        public List<PermissionResultDTO> GetUserPermissionsByUserID(long userID, List<string> applicationObjectTypes)
        {
            try
            {
                var result = _permissionRepository.GetUserPermissionsByUserID(userID, applicationObjectTypes);
                result.ForEach(x => x.Name = x.Name.Replace("/", "_"));
                var groupedResult = result.GroupBy(x => x.Name);
                List<PermissionResultDTO> permissionsList = new List<PermissionResultDTO>();
                foreach (var group in groupedResult)
                {
                    PermissionResultDTO permission = new PermissionResultDTO();
                    permission.Permission = group.Key;
                    permission.OperationType = new List<string>();
                    foreach (var item in group)
                    {
                        permission.OperationType.Add(item.OperationTypeName);
                    }
                    permissionsList.Add(permission);
                }
                return permissionsList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<PermissionResultDTO> GetUserPermissionsByRoleID(int roleID, List<string> applicationObjectTypes)
        {
            try
            {
                var result = _permissionRepository.GetUserPermissionsByRoleID(roleID, applicationObjectTypes);
                result.ForEach(x => x.Name = x.Name.Replace("/", "_"));
                var groupedResult = result.GroupBy(x => x.Name);
                List<PermissionResultDTO> permissionsList = new List<PermissionResultDTO>();
                foreach (var group in groupedResult)
                {
                    PermissionResultDTO permission = new PermissionResultDTO();
                    permission.Permission = group.Key;
                    permission.OperationType = new List<string>();
                    foreach (var item in group)
                    {
                        permission.OperationType.Add(item.OperationTypeName);
                    }
                    permissionsList.Add(permission);
                }
                return permissionsList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<PermissionResultDTO> GetSharedPersonPermissionsByRoleID(int roleID, List<string> applicationObjectTypes, string agencySharingIndex, string collaborationSharingIndex, bool isActiveForSharing)
        {
            try
            {
                var agencySharingPolicyID = _agencySharingRepository.GetAgencySharing(Guid.Parse(agencySharingIndex)).Result.AgencySharingPolicyID;
                var collaborationSharingPolicyID = _collaborationSharingRepository.GetCollaborationSharing(Guid.Parse(collaborationSharingIndex)).Result.CollaborationSharingPolicyID;
                var result = _permissionRepository.GetUserSharedPermissionsByRoleID(roleID, applicationObjectTypes, agencySharingPolicyID ?? 0, collaborationSharingPolicyID ?? 0, isActiveForSharing);
                result.ForEach(x => x.Name = x.Name.Replace("/", "_"));
                var groupedResult = result.GroupBy(x => x.Name);
                List<PermissionResultDTO> permissionsList = new List<PermissionResultDTO>();
                foreach (var group in groupedResult)
                {
                    PermissionResultDTO permission = new PermissionResultDTO();
                    permission.Permission = group.Key;
                    permission.OperationType = new List<string>();
                    foreach (var item in group)
                    {
                        permission.OperationType.Add(item.OperationTypeName);
                    }
                    permissionsList.Add(permission);
                }
                return permissionsList;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
