// -----------------------------------------------------------------------
// <copyright file="HelperService.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Graph;
using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.DTO.ExternalAPI;
using Opeeka.PICS.Domain.DTO.Input;
using Opeeka.PICS.Domain.DTO.Output;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Common;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Domain.Interfaces.Services;
using Opeeka.PICS.Domain.Resources;
using Opeeka.PICS.Infrastructure.Enums;

namespace Opeeka.PICS.Domain.Services
{
    public class HelperService : BaseService, IHelperService
    {
        private readonly IUserRepository userRepository;
        private readonly IUserRoleRepository userRoleRepository;
        private readonly IHelperRepository helperRepository;
        private readonly IAddressrepository addressRepository;
        private readonly IHelperAddressRepository helperAddressRepository;
        readonly CRUDResponseDTO response = new CRUDResponseDTO();
        private readonly ISystemRoleRepository systemRoleRepository;
        private readonly IUserService userService;
        private readonly IAzureADB2CService azureADB2CService;

        private readonly IMapper mapper;

        /// Initializes a new instance of the LocalizeService/> class.
        private readonly LocalizeService localize;
        private readonly IQueryBuilder queryBuilder;
        private readonly ILookupRepository lookupRepository;
        private readonly ILanguageRepository languageRepository;
        private readonly IHelperTitleRepository _helperTitleRepository;
        private readonly IAgencyRepository _agencyRepository;
        private readonly IHelperRepository _helperRepository;
        /// <summary>
        /// Initializes a new instance of the <see cref="HelperService"/> class.
        /// </summary>
        /// <param name="userRepository"></param>
        /// <param name="userRoleRepository"></param>
        /// <param name="helperRepository"></param>
        /// <param name="addressRepository"></param>
        /// <param name="helperAddressRepository"></param>
        public HelperService(IMapper mapper, IUserRepository userRepository, IUserRoleRepository userRoleRepository, IHelperRepository helperRepository, IAddressrepository addressRepository, IHelperAddressRepository helperAddressRepository, LocalizeService localizeService, IConfigurationRepository configRepo, IHttpContextAccessor httpContext, IUserService userService, ISystemRoleRepository systemRoleRepository, IQueryBuilder querybuild, IAzureADB2CService azureADB2CService, ILookupRepository lookupRepository, ILanguageRepository languageRepository, IHelperRepository _helperRepository,
          IAgencyRepository _agencyRepository, IHelperTitleRepository _helperTitleRepository)
        : base(configRepo, httpContext)
        {
            this.userRepository = userRepository;
            this.userRoleRepository = userRoleRepository;
            this.helperRepository = helperRepository;
            this.addressRepository = addressRepository;
            this.helperAddressRepository = helperAddressRepository;
            this.localize = localizeService;
            this.userService = userService;
            this.systemRoleRepository = systemRoleRepository;
            this.mapper = mapper;
            this.queryBuilder = querybuild;
            this.azureADB2CService = azureADB2CService;
            this.lookupRepository = lookupRepository;
            this.languageRepository = languageRepository;
            this._helperRepository = _helperRepository;
            this._agencyRepository = _agencyRepository;
            this._helperTitleRepository = _helperTitleRepository;
        }

        /// <summary>
        /// To save Helper details.
        /// </summary>
        /// <param name="helperData"></param>
        /// <returns>Object of StatusResponse.</returns>
        public CRUDResponseDTO SaveHelperDetails(HelperDetailsDTO helperData, long agencyID)
        {
            try
            {
                int UserID = 0;
                int HelperID = 0;
                long AddressID = 0;

                if (helperData != null)
                {
                    bool helperValid = this.helperRepository.ValidateHelperExternalID(helperData, agencyID).Result;
                    if (!helperValid)
                    {
                        response.ResponseStatusCode = PCISEnum.StatusCodes.InvalidName;
                        response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.InvalidHelperExternalID);
                        return response;
                    }
                    var systemRoleName = this.systemRoleRepository.GetSystemRoleByRoleID(helperData.RoleId).Name;
                    List<string> RolesList = new List<string>
                    {
                        systemRoleName
                    };

                    var user = new UsersDTO
                    {
                        UserName = helperData.Email,
                        Name = helperData.FirstName + (string.IsNullOrEmpty(helperData.MiddleName) ? "" : " " + helperData.MiddleName) + (string.IsNullOrEmpty(helperData.LastName) ? "" : " " + helperData.LastName),
                        Email = helperData.Email,
                        AgencyID = helperData.AgencyID,
                        Roles = RolesList,
                        //AspNetUserID = b2cUserId
                    };

                    TokenResultDTO tokenResult = userService.Register(user).Result;
                    if (tokenResult.AlreadyExists)
                    {
                        response.ResponseStatusCode = PCISEnum.StatusCodes.UserExists;
                        response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UserAlreadyExists);
                        return response;
                    }
                    UserID = Convert.ToInt32(tokenResult.UserID);
                    if (UserID == 0)
                    {
                        response.ResponseStatusCode = PCISEnum.StatusCodes.insertionFailed;
                        response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.insertionFailed);
                        return response;
                    }

                    //sign up in b2c
                    var b2cResult = this.azureADB2CService.BuildUserSignUpToken(helperData, systemRoleName, UserID);
                    var b2cUserId = b2cResult.Item1;
                    var b2cMailStatus = b2cResult.Item2;
                    if (!string.IsNullOrEmpty(b2cUserId))
                    {
                        var updateUser = this.userRepository.GetUsersByUsersIDAsync(UserID).GetAwaiter().GetResult();
                        updateUser.AspNetUserID = b2cUserId;
                        var updatedUser = this.userRepository.UpdateUser(updateUser);

                        var helper = new HelperDTO
                        {
                            FirstName = helperData.FirstName,
                            MiddleName = helperData.MiddleName,
                            LastName = helperData.LastName,
                            Email = helperData.Email,
                            Phone = helperData.Phone1,
                            AgencyId = helperData.AgencyID,
                            HelperTitleID = helperData.HelperTitleID,
                            Phone2 = helperData.Phone2,
                            SupervisorHelperID = helperData.SupervisorHelperID,
                            UpdateUserID = helperData.UpdateUserID,
                            ReviewerID = helperData.ReviewerID == 0 ? null : helperData.ReviewerID,
                            StartDate = helperData.StartDate,
                            EndDate = helperData.EndDate,
                            HelperExternalID = helperData.HelperExternalID,
                            IsEmailReminderAlerts = helperData.IsEmailReminderAlerts
                        };

                        var address = new AddressDTO
                        {
                            Address1 = helperData.Address,
                            Address2 = helperData.Address2,
                            City = helperData.City,
                            CountryStateID = helperData.StateId,
                            Zip = helperData.Zip,
                            IsPrimary = true,
                            IsRemoved = false,
                            UpdateDate = DateTime.UtcNow,
                            UpdateUserID = helperData.UpdateUserID,
                            CountryID = helperData.CountryId
                        };

                        if (user != null)
                        {
                            if (UserID != 0)
                            {
                                if (helper != null)
                                {
                                    if (helperData.ReviewerID == 0)
                                    {
                                        var newHelper = this.helperRepository.CreateHelper(helper, UserID);
                                        newHelper.ReviewerID = newHelper.HelperID;
                                        newHelper.UserID = UserID;
                                        HelperID = newHelper.HelperID;
                                        var result = this.helperRepository.UpdateHelper(newHelper);
                                    }
                                    else
                                    {
                                        var newHelper = this.helperRepository.CreateHelper(helper, UserID);
                                        HelperID = newHelper.HelperID;
                                    }
                                }
                                if (address != null)
                                {
                                    AddressID = this.addressRepository.AddAddress(address);
                                }
                                if (HelperID != 0 && AddressID != 0)
                                {
                                    this.helperAddressRepository.CreateHelperAddress(HelperID, AddressID);
                                }
                                response.ResponseStatusCode = PCISEnum.StatusCodes.InsertionSuccess;
                                response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.InsertionSuccess);
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(b2cUserId) && !b2cMailStatus)
                    {
                        response.ResponseStatusCode = PCISEnum.StatusCodes.EmailSendFailed;
                        response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.EmailSendFailed);
                        return response;
                    }
                }
                if (response.ResponseStatusCode != PCISEnum.StatusCodes.InsertionSuccess)
                {
                    response.ResponseStatusCode = PCISEnum.StatusCodes.insertionFailed;
                    response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.insertionFailed);
                }
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// To update Helper details.
        /// </summary>
        /// <param name="HelperID"></param>
        /// <returns></returns>
        public async Task<CRUDResponseDTO> UpdateHelperDetails(HelperDetailsDTO helperData, long agencyID)
        {
            try
            {
                var isSuccess = false;
                if (helperData.HelperIndex != Guid.Empty)
                {
                    bool helperValid = this.helperRepository.ValidateHelperExternalID(helperData, agencyID).Result;
                    if (!helperValid)
                    {
                        response.ResponseStatusCode = PCISEnum.StatusCodes.InvalidName;
                        response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.InvalidHelperExternalID);
                        return response;
                    }
                    var systemRoleName = this.systemRoleRepository.GetSystemRoleByRoleID(helperData.RoleId).Name;
                    List<string> RolesList = new List<string>
                    {
                        systemRoleName
                    };

                    var user = new UsersDTO
                    {
                        UserName = helperData.Email,
                        Email = helperData.Email,
                        AgencyID = helperData.AgencyID,
                        Roles = RolesList,
                        IsActive = true
                    };

                    var helper = new HelperDTO
                    {
                        FirstName = helperData.FirstName,
                        MiddleName = helperData.MiddleName,
                        LastName = helperData.LastName,
                        Email = helperData.Email,
                        Phone = helperData.Phone1,
                        AgencyId = helperData.AgencyID,
                        HelperTitleID = helperData.HelperTitleID,
                        Phone2 = helperData.Phone2,
                        SupervisorHelperID = helperData.SupervisorHelperID,
                        UpdateDate = DateTime.UtcNow,
                        IsRemoved = false,
                        UpdateUserID = helperData.UpdateUserID,
                        ReviewerID = helperData.ReviewerID == 0 ? null : helperData.ReviewerID,
                        StartDate = helperData.StartDate,
                        EndDate = helperData.EndDate,
                        HelperExternalID = helperData.HelperExternalID,
                        IsEmailReminderAlerts = helperData.IsEmailReminderAlerts
                    };

                    var address = new AddressDTO
                    {
                        Address1 = helperData.Address,
                        Address2 = helperData.Address2,
                        City = helperData.City,
                        CountryStateID = helperData.StateId,
                        Zip = helperData.Zip,
                        IsPrimary = true,
                        IsRemoved = false,
                        UpdateDate = DateTime.UtcNow,
                        UpdateUserID = helperData.UpdateUserID,
                        CountryID = helperData.CountryId
                    };

                    if (helper != null)
                    {
                        var helperResponse = this.helperRepository.GetHelperByIndexAsync(helperData.HelperIndex).Result;
                        if (helperResponse != null)
                        {
                            helper.HelperID = helperResponse.HelperID;
                            helper.UserID = helperResponse.UserID;
                            helper.HelperIndex = helperResponse.HelperIndex;
                            user.UserID = helper.UserID;
                            user.ExistingEmail = helperResponse.Email;
                            var tokenResult = await userService.UpdateUser(user);
                            if (tokenResult.AlreadyExists)
                            {
                                response.ResponseStatusCode = PCISEnum.StatusCodes.UserExists;
                                response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UserAlreadyExists);
                                return response;
                            }
                            helperResponse.FirstName = helper.FirstName;
                            helperResponse.LastName = helper.LastName;
                            helperResponse.MiddleName = helper.MiddleName;
                            helperResponse.Phone = helper.Phone;
                            helperResponse.Phone2 = helper.Phone2;
                            helperResponse.HelperTitleID = helper.HelperTitleID;
                            helperResponse.SupervisorHelperID = helper.SupervisorHelperID;
                            helperResponse.UpdateDate = helper.UpdateDate;
                            helperResponse.Email = helper.Email;
                            helperResponse.ReviewerID = helper.ReviewerID;
                            helperResponse.StartDate = helper.StartDate;
                            helperResponse.EndDate = helper.EndDate;
                            helperResponse.HelperExternalID = helper.HelperExternalID;
                            helperResponse.IsEmailReminderAlerts = helper.IsEmailReminderAlerts;

                            var helperresult = this.helperRepository.UpdateHelper(helperResponse);
                            if (helperresult != null)
                            {
                                isSuccess = true;
                                //user.UserIndex = this.userRepository.GetUsersByUsersIDAsync(user.UserID).Result.UserIndex;
                                if (user.UserID != 0)
                                {
                                    address.AddressID = this.helperAddressRepository.GetHelperAddressByHelperIDAsync(helper.HelperID).Result.AddressID;
                                    address.AddressIndex = this.addressRepository.GetAddress(address.AddressID).Result.AddressIndex;
                                    if (address.AddressID != 0)
                                    {
                                        var addressResponse = this.addressRepository.UpdateAddress(address);
                                    }
                                }
                            }
                            this.azureADB2CService.UpdateUserExtensions(helper, systemRoleName).GetAwaiter().GetResult();
                        }
                    }
                }
                if (isSuccess)
                {
                    response.ResponseStatusCode = PCISEnum.StatusCodes.UpdationSuccess;
                    response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationSuccess);
                }
                else
                {
                    response.ResponseStatusCode = PCISEnum.StatusCodes.UpdationFailed;
                    response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationFailed);
                }
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// GetHelperDetails.
        /// </summary>
        /// <param name="helperData"></param>
        /// <returns></returns>
        public GetHelperResponseDTO GetHelperDetails(int userId, int pageNumber, int pageSize)
        {
            try
            {
                GetHelperResponseDTO HelperResponse = new GetHelperResponseDTO();
                List<HelperDataDTO> response = new List<HelperDataDTO>();
                int totalCount = 0;
                long agencyid = 1;
                if (userId == 1)
                {
                    response = this.helperRepository.GetHelperDetails(userId, pageNumber, pageSize, PCISEnum.Roles.SuperAdmin, agencyid);
                    totalCount = this.helperRepository.GetHelperDetailsCount(userId, PCISEnum.Roles.SuperAdmin, agencyid);
                }
                else if (userId == 2)
                {
                    response = this.helperRepository.GetHelperDetails(userId, pageNumber, pageSize, PCISEnum.Roles.OrgAdmin, agencyid);
                    totalCount = this.helperRepository.GetHelperDetailsCount(userId, PCISEnum.Roles.OrgAdmin, agencyid);
                }
                else if (userId == 3)
                {
                    response = this.helperRepository.GetHelperDetails(userId, pageNumber, pageSize, PCISEnum.Roles.Helper, agencyid);
                    totalCount = this.helperRepository.GetHelperDetailsCount(userId, PCISEnum.Roles.Helper, agencyid);
                }

                HelperResponse.HelperData = response;
                HelperResponse.TotalCount = totalCount;
                HelperResponse.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                HelperResponse.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                return HelperResponse;

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get helper details.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Helper details</returns>
        public HelperViewResponseDTO GetHelperInfo(Guid helperIndex, long agencyID = 0)
        {
            HelperViewResponseDTO helperDetailsDto = new HelperViewResponseDTO();
            try
            {
                if (helperIndex != Guid.Empty)
                {
                    var response = this.helperRepository.GetHelperInfo(helperIndex, agencyID);
                    helperDetailsDto.HelperDetails = response;
                    helperDetailsDto.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                    helperDetailsDto.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                    return helperDetailsDto;
                }
                else
                {
                    helperDetailsDto.ResponseStatus = String.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.HelperIndex));
                    helperDetailsDto.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return helperDetailsDto;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// to remove helper.
        /// </summary>
        /// <param name="helperIndex"></param>
        /// <returns>StatusResponseDTO</returns>
        public CRUDResponseDTO RemoveHelperDetails(Guid helperIndex, long agencyID)
        {
            try
            {
                if (helperIndex != Guid.Empty)
                {
                    var helperResponse = this.helperRepository.GetHelperByIndexAsync(helperIndex).Result;
                    if (helperResponse.AgencyID == agencyID)
                    {
                        int usedHerlperCount = this.helperRepository.GetHelperUsedCount(helperResponse.HelperID);
                        if (usedHerlperCount == 0)
                        {
                            helperResponse.IsRemoved = true;
                            var helperresult = this.helperRepository.UpdateHelper(helperResponse);

                            if (helperresult != null)
                            {
                                var user = this.userRepository.GetUsersByUsersIDAsync(helperresult.UserID).Result;
                                var isDeletionFailed = false;
                                if (user != null)
                                {
                                    user.IsActive = false;
                                    var userresult = this.userRepository.UpdateUser(user);
                                    if (userresult != null)
                                    {
                                        var helperaddress = this.helperAddressRepository.GetHelperAddressByHelperIDAsync(helperResponse.HelperID).Result;
                                        var helper = this.addressRepository.GetAddress(helperaddress.AddressID).Result;
                                        if (helper != null)
                                        {
                                            helper.IsRemoved = true;
                                            var addressResponse = this.addressRepository.UpdateAddress(helper);
                                            if (addressResponse != null)
                                            {
                                                response.ResponseStatusCode = PCISEnum.StatusCodes.DeletionSuccess;
                                                response.ResponseStatus = PCISEnum.StatusMessages.DeletionSuccess;
                                            }
                                            else
                                            {
                                                isDeletionFailed = true;
                                            }
                                        }
                                        if (user.AspNetUserID != null)
                                        {
                                            var b2cResult = this.azureADB2CService.DeleteUserById(user.AspNetUserID).GetAwaiter().GetResult();
                                            if (b2cResult == "-1")
                                            {
                                                isDeletionFailed = true;
                                            }
                                            else
                                            {
                                                response.ResponseStatusCode = PCISEnum.StatusCodes.DeletionSuccess;
                                                response.ResponseStatus = PCISEnum.StatusMessages.DeletionSuccess;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    isDeletionFailed = true;
                                }
                                if (isDeletionFailed)
                                {
                                    response.ResponseStatusCode = PCISEnum.StatusCodes.DeletionFailed;
                                    response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.DeletionFailed);
                                }
                            }
                        }
                        else
                        {
                            response.ResponseStatusCode = PCISEnum.StatusCodes.DeleteRecordInUse;
                            response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.DeleteRecordInUse);
                        }
                    }
                }
                else
                {
                    response.ResponseStatusCode = PCISEnum.StatusCodes.DeletionFailed;
                    response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.DeletionFailed);
                }
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetUserDetails.
        /// </summary>
        /// <param name="userID">userID.</param>
        /// <returns>UserDetailsResponseDTO.</returns>
        public UserDetailsResponseDTO GetUserDetails(long userID)
        {
            try
            {
                UserDetailsResponseDTO userDetails = new UserDetailsResponseDTO();
                if (userID != 0)
                {
                    var response = this.helperRepository.GetUserDetails(userID);
                    userDetails.UserDetails = response;
                    userDetails.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                    userDetails.ResponseStatusCode = PCISEnum.StatusCodes.Success;

                    return userDetails;
                }
                else
                {
                    userDetails.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), PCISEnum.Parameters.UserId);
                    userDetails.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;

                    return userDetails;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public GetHelperResponseDTO GetHelperDetailsByHelperID(HelperSearchDTO helperSearchDTO)
        {
            try
            {
                List<QueryFieldMappingDTO> fieldMappingList = GetHelperListConfiguration();
                DynamicQueryBuilderDTO queryBuilderDTO = this.queryBuilder.BuildQuery(helperSearchDTO.SearchFilter, fieldMappingList);
                GetHelperResponseDTO HelperResponse = new GetHelperResponseDTO();
                int totalCount = 0;
                helperSearchDTO.role = this.GetRoleName(helperSearchDTO.userRole);

                Tuple<List<HelperDataDTO>, int> queryResponse = this.helperRepository.GetHelperDetailsByHelperID(helperSearchDTO, queryBuilderDTO);
                totalCount = queryResponse.Item2;
                HelperResponse.HelperData = queryResponse.Item1;
                HelperResponse.TotalCount = totalCount;
                HelperResponse.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                HelperResponse.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                return HelperResponse;

            }
            catch (Exception)
            {
                throw;
            }
        }

        private List<QueryFieldMappingDTO> GetHelperListConfiguration()
        {
            List<QueryFieldMappingDTO> fieldMappingList = new List<QueryFieldMappingDTO>();
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "H.HelperName",
                fieldAlias = "helperName",
                fieldTable = "",
                fieldType = "string"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "ISNULL(Helping,0)",
                fieldAlias = "helping",
                fieldTable = "",
                fieldType = "number"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "ISNULL(Days,0)",
                fieldAlias = "days",
                fieldTable = "",
                fieldType = "number"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "ISNULL(Due,0)",
                fieldAlias = "due",
                fieldTable = "",
                fieldType = "number"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "ISNULL(completed,0)",
                fieldAlias = "completed",
                fieldTable = "",
                fieldType = "number"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "ISNULL(CTE.TotalNeedsIdentified,0)",
                fieldAlias = "needIdentified",
                fieldTable = "",
                fieldType = "number"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "ISNULL(CTE.TotalNeedsAddressed,0)",
                fieldAlias = "needAddressed",
                fieldTable = "",
                fieldType = "number"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "ISNULL(CTE.TotalStrengthsIdentified,0)",
                fieldAlias = "strengthIdentified",
                fieldTable = "",
                fieldType = "number"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "ISNULL(CTE.TotalStrengthsBuilt,0)",
                fieldAlias = "strengthBuilt",
                fieldTable = "",
                fieldType = "number"
            });

            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "ISNULL(CTE.TotalNeedsEver,0)",
                fieldAlias = "needsEver",
                fieldTable = "",
                fieldType = "number"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "ISNULL(CTE.TotalNeedsAddressing,0)",
                fieldAlias = "needsAddressing",
                fieldTable = "",
                fieldType = "number"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "ISNULL(CTE.TotalStrengthsEver,0)",
                fieldAlias = "strengthsEver",
                fieldTable = "",
                fieldType = "number"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "ISNULL(CTE.TotalStrengthsBuilding,0)",
                fieldAlias = "strengthsBuilding",
                fieldTable = "",
                fieldType = "number"
            });
            return fieldMappingList;
        }

        /// <summary>
        /// Set Super Admin Default Agency
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="agencyId"></param>
        /// <returns></returns>
        public CRUDResponseDTO SetSuperAdminDefaultAgency(int userId, long agencyId)
        {
            try
            {
                var user = this.userRepository.GetUsersByUsersIDAsync(userId).GetAwaiter().GetResult();
                user.AgencyID = agencyId;
                var updatedUser = this.userRepository.UpdateUser(user);
                CRUDResponseDTO resultDTO = new CRUDResponseDTO();
                if (updatedUser != null)
                {
                    this.azureADB2CService.UpdateUserAgencyExtension(updatedUser).GetAwaiter().GetResult();
                    resultDTO.ResponseStatusCode = PCISEnum.StatusCodes.UpdationSuccess;
                    resultDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationSuccess);
                }
                else
                {

                    resultDTO.ResponseStatusCode = PCISEnum.StatusCodes.UpdationFailed;
                    resultDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationFailed);
                }
                return resultDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Reset Helper Password
        /// </summary>
        /// <param name="helperData"></param>
        /// <returns>CRUDResponseDTO</returns>        
        public CRUDResponseDTO ResendHelperPassword(Guid helperIndex, long agencyId)
        {
            try
            {
                var isSuccess = false;
                if (helperIndex != Guid.Empty)
                {
                    var helperData = this.helperRepository.GetHelperByIndexAsync(helperIndex).Result;
                    if (helperData.AgencyID == agencyId)
                    {
                        var helper = new HelperDetailsDTO
                        {
                            FirstName = helperData.FirstName,
                            MiddleName = helperData.MiddleName,
                            LastName = helperData.LastName,
                            Email = helperData.Email,
                            AgencyID = helperData.AgencyID,
                            HelperTitleID = helperData.HelperTitleID,
                            SupervisorHelperID = helperData.SupervisorHelperID
                        };
                        var b2cUserId = this.azureADB2CService.ResetPasswordAndSendMail(helper, helperData.UserID);
                        if (b2cUserId == "-1")
                        {
                            isSuccess = false;
                        }
                        else if (b2cUserId == "-2")
                        {
                            response.ResponseStatusCode = PCISEnum.StatusCodes.EmailSendFailed;
                            response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.EmailSendFailed);
                            return response;
                        }
                        else
                        {
                            isSuccess = true;
                        }
                    }
                }
                if (isSuccess)
                {
                    response.ResponseStatusCode = PCISEnum.StatusCodes.ResetPasswordSuccess;
                    response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.ResetPasswordSuccess);
                }
                else
                {
                    response.ResponseStatusCode = PCISEnum.StatusCodes.ResetPasswordFailed;
                    response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.ResetPasswordFailed);
                }
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public CRUDResponseDTO ImportHelper(List<HelperDetailsDTO> helperDetailsDTOList, long agencyID)
        {
            try
            {
                List<UsersDTO> userList = new List<UsersDTO>();
                List<Guid> helperIndexGuids = new List<Guid>();

                foreach (HelperDetailsDTO helperData in helperDetailsDTOList)
                {
                    Guid newhelperGuid = Guid.NewGuid();
                    helperData.HelperIndex = newhelperGuid;
                    var systemRoleName = this.systemRoleRepository.GetSystemRoleByRoleID(helperData.RoleId).Name;
                    List<string> RolesList = new List<string>
                    {
                        systemRoleName
                    };
                    var user = new UsersDTO
                    {
                        UserName = helperData.Email,
                        Name = helperData.FirstName + (string.IsNullOrEmpty(helperData.MiddleName) ? "" : " " + helperData.MiddleName) + (string.IsNullOrEmpty(helperData.LastName) ? "" : " " + helperData.LastName),
                        Email = helperData.Email,
                        AgencyID = helperData.AgencyID,
                        Roles = RolesList,
                        helperIndex = newhelperGuid
                    };
                    userList.Add(user);
                }

                List<TokenResultDTO> tokenResultList = userService.RegisterBulk(userList).Result;
                List<HelperDTO> helperDTOList = new List<HelperDTO>();
                List<HelperDTO> updateHelperDTOList = new List<HelperDTO>();
                List<Entities.User> updateUsersList = new List<Entities.User>();

                foreach (TokenResultDTO tokenResult in tokenResultList)
                {
                    HelperDetailsDTO helperData = new HelperDetailsDTO();
                    helperData = helperDetailsDTOList.Where(x => x.HelperIndex == tokenResult.helperIndex).FirstOrDefault();
                    var systemRoleName = this.systemRoleRepository.GetSystemRoleByRoleID(helperData.RoleId).Name;
                    int UserID = Convert.ToInt32(tokenResult.UserID);
                    //sign up in b2c
                    var b2cResult = this.azureADB2CService.BuildUserSignUpToken(helperData, systemRoleName, UserID);
                    var b2cUserId = b2cResult.Item1;
                    var b2cMailStatus = b2cResult.Item2;
                    if (!string.IsNullOrEmpty(b2cUserId))
                    {
                        var updateUser = this.userRepository.GetUsersDetailsByUsersIDAsync(UserID).GetAwaiter().GetResult();
                        updateUser.AspNetUserID = b2cUserId;
                        updateUsersList.Add(updateUser);
                    }
                    var helper = new HelperDTO
                    {
                        HelperIndex = (Guid)helperData.HelperIndex,
                        FirstName = helperData.FirstName,
                        MiddleName = helperData.MiddleName,
                        LastName = helperData.LastName,
                        Email = helperData.Email,
                        Phone = helperData.Phone1,
                        AgencyId = helperData.AgencyID,
                        HelperTitleID = helperData.HelperTitleID,
                        Phone2 = helperData.Phone2,
                        SupervisorHelperID = helperData.SupervisorHelperID,
                        UpdateUserID = helperData.UpdateUserID,
                        ReviewerID = helperData.ReviewerID == 0 ? null : helperData.ReviewerID,
                        StartDate = helperData.StartDate,
                        EndDate = helperData.EndDate,
                        HelperExternalID = helperData.HelperExternalID,
                        IsEmailReminderAlerts = helperData.IsEmailReminderAlerts,
                        UserID = UserID,
                    };
                    if (UserID != 0 && helper != null)
                    {
                        if (helperData.ReviewerID == 0)
                        {
                            helperDTOList.Add(helper);
                            updateHelperDTOList.Add(helper);
                            helperIndexGuids.Add(helper.HelperIndex);
                        }
                        else
                        {
                            helperDTOList.Add(helper);
                        }
                    }
                }
                List<HelperDTO> updateResult = helperRepository.AddHelperBulk(helperDTOList);
                List<Entities.User> userResult = userRepository.UpdateUserBulk(updateUsersList);

                if (updateResult.Count > 0 && updateHelperDTOList.Count > 0)
                {
                    var helperListByGuid = helperRepository.GetHelperListByGUID(helperIndexGuids).Result;
                    foreach (var item in updateHelperDTOList)
                    {
                        item.ReviewerID = helperListByGuid.Where(x => x.HelperIndex == item.HelperIndex).FirstOrDefault().HelperID;
                        item.HelperID = helperListByGuid.Where(x => x.HelperIndex == item.HelperIndex).FirstOrDefault().HelperID;
                        item.SupervisorHelperID = item.SupervisorHelperID == 0 ? null : item.SupervisorHelperID;
                        item.HelperTitleID = item.HelperTitleID == 0 ? null : item.HelperTitleID;
                    }
                    helperRepository.UpdateHelperBulk(updateHelperDTOList);
                }
                response.ResponseStatusCode = PCISEnum.StatusCodes.InsertionSuccess;
                response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.InsertionSuccess);
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public HelperViewResponseDTO GetHelperDetailsByHelperEmail(string helperEmailCSV, long agencyID)
        {
            try
            {
                HelperViewResponseDTO response = new HelperViewResponseDTO();

                string EmailCSV = ConvertJsonKeyValuestoCSV(helperEmailCSV);
                response.HelperList = this.helperRepository.GetHelperDetailsByHelperEmail(EmailCSV, agencyID);

                response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                response.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ConvertJsonKeyValuestoCSV(string helperEmailCSV)
        {
            dynamic helperEmailObj = JsonConvert.DeserializeObject(helperEmailCSV);
            string CSV = "";
            foreach (var data in helperEmailObj)
            {
                foreach (var dataItem in data)
                {
                    if (CSV == "")
                        CSV = "'" + dataItem.Value.ToString() + "'";
                    else
                        CSV = CSV + ",'" + dataItem.Value.ToString() + "'";
                }
            }
            return CSV;
        }

        /// <summary>
        /// ValidateHelperEmail.
        /// </summary>
        /// <param name="jsonData">jsonData.</param>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>EmailValidationResponseDTO.</returns>
        public EmailValidationResponseDTO ValidateHelperEmail(string jsonData, long agencyID)
        {
            try
            {
                EmailValidationResponseDTO response = new EmailValidationResponseDTO();
                string nameCSV = ConvertJsonKeyValuestoCSV(jsonData);
                response.existingEmails = this.userRepository.ValidateHelperEmail(nameCSV, agencyID);

                if (response.existingEmails.Count > 0)
                {
                    response.isVaildEmails = false;
                }
                else
                {
                    response.isVaildEmails = true;
                }
                response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                response.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public GetHelperResponseDTOForExternal GetAllHelperDetailsForExternal(HelperSearchInputDTO helperSearchInputDTO, LoggedInUserDTO loggedInUserDTO)
        {
            try
            {
                GetHelperResponseDTOForExternal helperResponse = new GetHelperResponseDTOForExternal();
                helperResponse.HelperDetailsList = null;
                helperResponse.TotalCount = 0;
                if (loggedInUserDTO.AgencyId != 0)
                {
                    List<QueryFieldMappingDTO> fieldMappingList = GetHelperListConfigurationForExternal();
                    var queryBuilderDTO = this.queryBuilder.BuildQueryForExternalAPI(helperSearchInputDTO, fieldMappingList);

                    List<PeopleProfileDetailsDTO> personList = new List<PeopleProfileDetailsDTO>();
                    if (queryBuilderDTO.Page <= 0)
                    {
                        helperResponse.HelperDetailsList = null;
                        response.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PageNumber));
                        helperResponse.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                        return helperResponse;
                    }
                    else if (queryBuilderDTO.PageSize <= 0)
                    {
                        helperResponse.HelperDetailsList = null;
                        helperResponse.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PageSize));
                        helperResponse.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                        return helperResponse;
                    }
                    else
                    {
                        helperResponse.HelperDetailsList = this.helperRepository.GetHelpersDetailsListForExternal(loggedInUserDTO, queryBuilderDTO, helperSearchInputDTO);
                        if (helperResponse.HelperDetailsList.Count > 0)
                        {
                            helperResponse.TotalCount = helperResponse.HelperDetailsList[0].TotalCount;
                        }
                    }
                }
                helperResponse.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                helperResponse.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                return helperResponse;

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetHelperListtConfigurationForExternal.
        /// Always the First item to the list should be the column deatils for order by (With fieldTable as OrderBy for just user identification).
        /// And the next item should be the fieldMapping for order by Column specified above.
        /// </summary>
        /// <returns></returns>
        private List<QueryFieldMappingDTO> GetHelperListConfigurationForExternal()
        {
            List<QueryFieldMappingDTO> fieldMappingList = new List<QueryFieldMappingDTO>();
            fieldMappingList.Add(new QueryFieldMappingDTO
            {   //Exclusively For Order By
                fieldName = "FullName",
                fieldAlias = "Name",
                fieldTable = "OrderBy",
                fieldType = "asc"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "FullName",
                fieldAlias = "Name",
                fieldTable = "H",
                fieldType = "string"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "Email",
                fieldAlias = "Email",
                fieldTable = "H",
                fieldType = "string"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "HelperExternalID",
                fieldAlias = "ExternalId",
                fieldTable = "H",
                fieldType = "string"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "PersonIndex",
                fieldAlias = "PersonIndex",
                fieldTable = "H",
                fieldType = "string"
            });
            fieldMappingList.Add(new QueryFieldMappingDTO
            {
                fieldName = "HelperID",
                fieldAlias = "HelperID",
                fieldTable = "H",
                fieldType = "int"
            });
            return fieldMappingList;
        }

        /// <summary>
        /// SaveHelperDetailsForExternal
        /// </summary>
        /// <param name="helperInputData">helperInputData</param>
        /// <returns>CRUDResponseDTOForExternal</returns>
        public CRUDResponseDTOForExternal SaveHelperDetailsForExternal(HelperDetailsInputDTO helperInputData)
        {
            try
            {
                int UserID = 0;
                int HelperID = 0;
                long AddressID = 0;
                CRUDResponseDTOForExternal response = new CRUDResponseDTOForExternal();
                if (helperInputData != null)
                {
                    HelperDetailsDTO helperData = new HelperDetailsDTO();
                    this.mapper.Map<HelperDetailsInputDTO, HelperDetailsDTO>(helperInputData, helperData);
                    string ValidationResult = ValidateHelperDataForExternal(helperData);
                    if (!string.IsNullOrEmpty(ValidationResult))
                    {
                        response.ResponseStatusCode = PCISEnum.StatusCodes.insertionFailed;
                        response.ResponseStatus = ValidationResult;
                        return response;
                    }
                    bool helperValid = this.helperRepository.ValidateHelperExternalID(helperData, helperData.AgencyID).Result;
                    if (!helperValid)
                    {
                        response.ResponseStatusCode = PCISEnum.StatusCodes.InvalidName;
                        response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.InvalidHelperExternalID);
                        return response;
                    }
                    var systemRoleName = this.systemRoleRepository.GetSystemRoleByRoleID(helperInputData.RoleId).Name;
                    List<string> RolesList = new List<string>
                    {
                        systemRoleName
                    };

                    var user = new UsersDTO
                    {
                        UserName = helperInputData.Email,
                        Name = helperInputData.FirstName + (string.IsNullOrEmpty(helperInputData.MiddleName) ? "" : " " + helperInputData.MiddleName) + (string.IsNullOrEmpty(helperInputData.LastName) ? "" : " " + helperInputData.LastName),
                        Email = helperInputData.Email,
                        AgencyID = helperInputData.AgencyID,
                        Roles = RolesList,
                    };

                    TokenResultDTO tokenResult = userService.Register(user).Result;
                    if (tokenResult.AlreadyExists)
                    {
                        response.ResponseStatusCode = PCISEnum.StatusCodes.UserExists;
                        response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UserAlreadyExists);
                        return response;
                    }
                    UserID = Convert.ToInt32(tokenResult.UserID);
                    if (UserID == 0)
                    {
                        response.ResponseStatusCode = PCISEnum.StatusCodes.insertionFailed;
                        response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.insertionFailed);
                        return response;
                    }

                    //sign up in b2c
                    var b2cResult = this.azureADB2CService.BuildUserSignUpToken(helperData, systemRoleName, UserID);
                    var b2cUserId = b2cResult.Item1;
                    var b2cMailStatus = b2cResult.Item2;
                    if (!string.IsNullOrEmpty(b2cUserId))
                    {
                        var updateUser = this.userRepository.GetUsersByUsersIDAsync(UserID).GetAwaiter().GetResult();
                        updateUser.AspNetUserID = b2cUserId;
                        var updatedUser = this.userRepository.UpdateUser(updateUser);

                        var helper = new HelperDTO
                        {
                            FirstName = helperInputData.FirstName,
                            MiddleName = helperInputData.MiddleName,
                            LastName = helperInputData.LastName,
                            Email = helperInputData.Email,
                            Phone = helperInputData.Phone1,
                            AgencyId = helperInputData.AgencyID,
                            HelperTitleID = helperInputData.HelperTitleID <= 0 ? null : helperInputData.HelperTitleID,
                            Phone2 = helperInputData.Phone2,
                            SupervisorHelperID = helperInputData.SupervisorHelperID <= 0 ? null : helperInputData.SupervisorHelperID,
                            UpdateUserID = helperInputData.UpdateUserID,
                            ReviewerID = helperInputData.ReviewerID == 0 ? null : helperInputData.ReviewerID,
                            StartDate = helperInputData.StartDate,
                            EndDate = helperInputData.EndDate,
                            HelperExternalID = helperInputData.HelperExternalID,
                            IsEmailReminderAlerts = helperInputData.IsEmailReminderAlerts
                        };

                        var address = new AddressDTO
                        {
                            Address1 = helperInputData.Address,
                            Address2 = helperInputData.Address2,
                            City = helperInputData.City,
                            CountryStateID = helperInputData.StateId <= 0 ? null : helperInputData.StateId,
                            Zip = helperInputData.Zip,
                            IsPrimary = true,
                            IsRemoved = false,
                            UpdateDate = DateTime.UtcNow,
                            UpdateUserID = helperInputData.UpdateUserID,
                            CountryID = helperInputData.CountryId <= 0 ? null : helperInputData.CountryId,
                        };

                        if (user != null)
                        {
                            if (UserID != 0)
                            {
                                if (helper != null)
                                {
                                    if (helperInputData.ReviewerID == 0)
                                    {
                                        var newHelper = this.helperRepository.CreateHelper(helper, UserID);
                                        newHelper.ReviewerID = newHelper.HelperID;
                                        newHelper.UserID = UserID;
                                        HelperID = newHelper.HelperID;
                                        helper.HelperIndex = newHelper.HelperIndex;
                                        var result = this.helperRepository.UpdateHelper(newHelper);
                                    }
                                    else
                                    {
                                        var newHelper = this.helperRepository.CreateHelper(helper, UserID);
                                        HelperID = newHelper.HelperID;
                                        helper.HelperIndex = newHelper.HelperIndex;
                                    }
                                }
                                if (address != null)
                                {
                                    AddressID = this.addressRepository.AddAddress(address);
                                }
                                if (HelperID != 0 && AddressID != 0)
                                {
                                    this.helperAddressRepository.CreateHelperAddress(HelperID, AddressID);
                                }
                                response.ResponseStatusCode = PCISEnum.StatusCodes.InsertionSuccess;
                                response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.InsertionSuccess);
                                response.Index = helper.HelperIndex;
                                response.Id = HelperID;
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(b2cUserId) && !b2cMailStatus)
                    {
                        response.ResponseStatusCode = PCISEnum.StatusCodes.EmailSendFailed;
                        response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.EmailSendFailed);
                        return response;
                    }
                }
                if (response.ResponseStatusCode != PCISEnum.StatusCodes.InsertionSuccess)
                {
                    response.ResponseStatusCode = PCISEnum.StatusCodes.insertionFailed;
                    response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.insertionFailed);
                }
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// UpdateHelperDetailsForExternal
        /// </summary>
        /// <param name="helperInputData">helperInputData</param>
        /// <returns>CRUDResponseDTOForExternal</returns>
        public async Task<CRUDResponseDTOForExternal> UpdateHelperDetailsForExternal(HelperDetailsEditInputDTO helperInputData)
        {
            try
            {
                int helperID = 0;
                var isSuccess = false;
                CRUDResponseDTOForExternal response = new CRUDResponseDTOForExternal();
                if (helperInputData.HelperIndex != Guid.Empty && helperInputData.HelperIndex != null)
                {
                    var helperDetails = this.helperRepository.GetHelperByIndexAsync(helperInputData.HelperIndex).Result;
                    HelperDetailsDTO helperData = new HelperDetailsDTO();
                    this.mapper.Map<HelperDetailsEditInputDTO, HelperDetailsDTO>(helperInputData, helperData);
                    string ValidationResult = ValidateHelperDataForExternal(helperData);
                    if (!string.IsNullOrEmpty(ValidationResult))
                    {
                        response.ResponseStatusCode = PCISEnum.StatusCodes.insertionFailed;
                        response.ResponseStatus = ValidationResult;
                        return response;
                    }
                    bool helperValid = this.helperRepository.ValidateHelperExternalID(helperData, helperData.AgencyID).Result;
                    if (!helperValid)
                    {
                        response.ResponseStatusCode = PCISEnum.StatusCodes.InvalidName;
                        response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.InvalidHelperExternalID);
                        return response;
                    }
                    var systemRoleName = this.systemRoleRepository.GetSystemRoleByRoleID(helperInputData.RoleId).Name;
                    List<string> RolesList = new List<string>
                    {
                        systemRoleName
                    };

                    var user = new UsersDTO
                    {
                        UserName= helperDetails.Email,
                        Email= helperDetails.Email,
                        AgencyID = helperInputData.AgencyID,
                        Roles = RolesList,
                        IsActive = true
                    };

                    var helper = new HelperDTO
                    {
                        FirstName = helperInputData.FirstName,
                        MiddleName = helperInputData.MiddleName,
                        LastName = helperInputData.LastName,
                        Phone = helperInputData.Phone1,
                        Email= helperDetails.Email,
                        AgencyId = helperInputData.AgencyID,
                        HelperTitleID = helperInputData.HelperTitleID <= 0 ? null : helperInputData.HelperTitleID,
                        Phone2 = helperInputData.Phone2,
                        SupervisorHelperID = helperInputData.SupervisorHelperID <= 0 ? null : helperInputData.SupervisorHelperID,
                        UpdateDate = DateTime.UtcNow,
                        IsRemoved = false,
                        UpdateUserID = helperInputData.UpdateUserID,
                        ReviewerID = helperInputData.ReviewerID == 0 ? null : helperInputData.ReviewerID,
                        StartDate = helperInputData.StartDate,
                        EndDate = helperInputData.EndDate,
                        HelperExternalID = helperInputData.HelperExternalID,
                        IsEmailReminderAlerts = helperInputData.IsEmailReminderAlerts
                    };

                    var address = new AddressDTO
                    {
                        Address1 = helperInputData.Address,
                        Address2 = helperInputData.Address2,
                        City = helperInputData.City,
                        CountryStateID = helperInputData.StateId <= 0 ? null : helperInputData.StateId,
                        Zip = helperInputData.Zip,
                        IsPrimary = true,
                        IsRemoved = false,
                        UpdateDate = DateTime.UtcNow,
                        UpdateUserID = helperInputData.UpdateUserID,
                        CountryID = helperInputData.CountryId <= 0 ? null : helperInputData.CountryId
                    };

                    if (helper != null)
                    {
                        var helperResponse = this.helperRepository.GetHelperByIndexAsync(helperInputData.HelperIndex).Result;
                        if (helperResponse != null)
                        {
                            helper.HelperID = helperResponse.HelperID;
                            helper.UserID = helperResponse.UserID;
                            helper.HelperIndex = helperResponse.HelperIndex;
                            user.UserID = helper.UserID;
                            user.ExistingEmail = helperResponse.Email;
                            var tokenResult = await userService.UpdateUser(user);
                            if (tokenResult.AlreadyExists)
                            {
                                response.ResponseStatusCode = PCISEnum.StatusCodes.UserExists;
                                response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UserAlreadyExists);
                                return response;
                            }
                            helperResponse.FirstName = helper.FirstName;
                            helperResponse.LastName = helper.LastName;
                            helperResponse.MiddleName = helper.MiddleName;
                            helperResponse.Phone = helper.Phone;
                            helperResponse.Phone2 = helper.Phone2;
                            helperResponse.HelperTitleID = helper.HelperTitleID;
                            helperResponse.SupervisorHelperID = helper.SupervisorHelperID;
                            helperResponse.UpdateDate = helper.UpdateDate;
                            helperResponse.Email = helper.Email;
                            helperResponse.ReviewerID = helper.ReviewerID;
                            helperResponse.StartDate = helper.StartDate;
                            helperResponse.EndDate = helper.EndDate;
                            helperResponse.HelperExternalID = helper.HelperExternalID;
                            helperResponse.IsEmailReminderAlerts = helper.IsEmailReminderAlerts;

                            var helperresult = this.helperRepository.UpdateHelper(helperResponse);
                            if (helperresult != null)
                            {
                                isSuccess = true;
                                helperID = helper.HelperID;
                                if (user.UserID != 0)
                                {
                                    address.AddressID = this.helperAddressRepository.GetHelperAddressByHelperIDAsync(helper.HelperID).Result.AddressID;
                                    address.AddressIndex = this.addressRepository.GetAddress(address.AddressID).Result.AddressIndex;
                                    if (address.AddressID != 0)
                                    {
                                        var addressResponse = this.addressRepository.UpdateAddress(address);
                                    }
                                }
                            }
                            this.azureADB2CService.UpdateUserExtensions(helper, systemRoleName).GetAwaiter().GetResult();
                        }
                    }
                }
                else
                {
                    response.ResponseStatusCode = PCISEnum.StatusCodes.UpdationFailed;
                    response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.InvalidHelperIndex);
                    return response;
                }
                if (isSuccess)
                {
                    response.ResponseStatusCode = PCISEnum.StatusCodes.UpdationSuccess;
                    response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationSuccess);
                    response.Index = (Guid)helperInputData.HelperIndex;
                    response.Id = helperID;
                }
                else
                {
                    response.ResponseStatusCode = PCISEnum.StatusCodes.UpdationFailed;
                    response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationFailed);
                }
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Validate helperdata from external api.
        /// </summary>
        /// <param name="helperDetailsDTO"></param>
        /// <returns></returns>
        public string ValidateHelperDataForExternal(HelperDetailsDTO helperDetailsDTO)
        {
            string ResponseStatus = string.Empty;
            List<CountryLookupDTO> countries = this.lookupRepository.GetAllCountries();
            List<LanguageDTO> languages = this.languageRepository.GetAgencyLanguageList(helperDetailsDTO.AgencyID);

            #region CountryId
            if (helperDetailsDTO.CountryId > 0 && helperDetailsDTO.CountryId != null)
            {
                int index = countries.FindIndex(x => x.CountryID == helperDetailsDTO.CountryId);
                if (index == -1)
                {
                    ResponseStatus = this.localize.GetLocalizedHtmlString(string.Format(PCISEnum.StatusMessages.RequiredParameterMissing, PCISEnum.Parameters.CountryID));
                    return ResponseStatus;
                }
            }
            #endregion  CountryId

            #region CountryStateId
            if (helperDetailsDTO.StateId > 0 && helperDetailsDTO.StateId != null)
            {
                List<CountryStateDTO> countryStates = this.lookupRepository.GetAllState().Result;
                int index = countryStates.FindIndex(x => x.CountryStateID == helperDetailsDTO.StateId && x.CountryID == helperDetailsDTO.CountryId);
                if (index == -1)
                {
                    ResponseStatus = this.localize.GetLocalizedHtmlString(string.Format(PCISEnum.StatusMessages.RequiredParameterMissing, PCISEnum.Parameters.CountryStateId));
                    return ResponseStatus;
                }
            }
            #endregion  CountryStateId

            #region Email
            if (!string.IsNullOrEmpty(helperDetailsDTO.Email))
            {
                bool isEmail = Regex.IsMatch(helperDetailsDTO.Email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
                if (!isEmail)
                {
                    ResponseStatus = this.localize.GetLocalizedHtmlString(string.Format(PCISEnum.StatusMessages.RequiredParameterMissing, PCISEnum.Parameters.Email));
                    return ResponseStatus;
                }
            }
            #endregion  Email

            var helperTitleList = this._helperTitleRepository.GetAgencyHelperTitleList(helperDetailsDTO.AgencyID);

            #region HelperTitleID
            if (helperDetailsDTO.HelperTitleID > 0 && helperDetailsDTO.HelperTitleID != null)
            {
                int index = helperTitleList.FindIndex(x => x.HelperTitleID == helperDetailsDTO.HelperTitleID);
                if (index == -1)
                {
                    ResponseStatus = this.localize.GetLocalizedHtmlString(string.Format(PCISEnum.StatusMessages.RequiredParameterMissing, PCISEnum.Parameters.HelperTitleID));
                    return ResponseStatus;
                }
            }
            #endregion  HelperTitleID

            #region RoleId
            var roles = this.lookupRepository.GetAllRolesLookup();
            if (helperDetailsDTO.RoleId >= 0 )
            {
                int index = roles.FindIndex(x => x.SystemRoleID == helperDetailsDTO.RoleId);
                if (index == -1)
                {
                    ResponseStatus = this.localize.GetLocalizedHtmlString(string.Format(PCISEnum.StatusMessages.RequiredParameterMissing, PCISEnum.Parameters.RoleId));
                    return ResponseStatus;
                }
            }
            #endregion  RoleId

            #region AgencyID
            var agencylist = this._agencyRepository.GetAgencyLookupWithID(helperDetailsDTO.AgencyID);
            if (helperDetailsDTO.AgencyID >= 0)
            {
                int index = agencylist.FindIndex(x => x.AgencyID == helperDetailsDTO.AgencyID);
                if (index == -1)
                {
                    ResponseStatus = this.localize.GetLocalizedHtmlString(string.Format(PCISEnum.StatusMessages.RequiredParameterMissing, PCISEnum.Parameters.AgencyID));
                    return ResponseStatus;
                }
            }
            #endregion  AgencyID



            #region SupervisorHelperID

            var Managers = this._helperRepository.GetAllManager(helperDetailsDTO.AgencyID, PCISEnum.Roles.OrgAdminRO, PCISEnum.Roles.OrgAdminRW, PCISEnum.Roles.Supervisor, false);
            if (helperDetailsDTO.SupervisorHelperID > 0)
            {
                int index = Managers.FindIndex(x => x.HelperID == helperDetailsDTO.SupervisorHelperID);
                if (index == -1)
                {
                    ResponseStatus = this.localize.GetLocalizedHtmlString(string.Format(PCISEnum.StatusMessages.RequiredParameterMissing, PCISEnum.Parameters.SupervisorHelperID));
                    return ResponseStatus;
                }
            }
            #endregion  SupervisorHelperID

            #region ReviewerID

            var reviwers = this.lookupRepository.GetAllAgencyHelperLookup(helperDetailsDTO.AgencyID, false);
            if (helperDetailsDTO.ReviewerID >= 0)
            {
                int index = reviwers.FindIndex(x => x.HelperID == helperDetailsDTO.ReviewerID);
                if (index == -1)
                {
                    ResponseStatus = this.localize.GetLocalizedHtmlString(string.Format(PCISEnum.StatusMessages.RequiredParameterMissing, PCISEnum.Parameters.ReviewerID));
                    return ResponseStatus;
                }
            }
            #endregion  ReviewerID

            return ResponseStatus;
        }

        /// <summary>
        /// Validate Zip and Phonecode
        /// </summary>
        /// <param name="personCountryId"></param>
        /// <param name="UScountryID"></param>
        /// <param name="inputValue"></param>
        /// <param name="type"></param>
        /// <param name="responseStatus"></param>
        /// <returns></returns>
        private string ValidatePhoneAndZipPattern(int? personCountryId, int? UScountryID, string inputValue, ref string responseStatus, string type = "" )
        {
            try
            {
                responseStatus = string.Empty;
                if (type != PCISEnum.Parameters.Zip)
                {
                    Regex phnNumberRegexUS = new Regex(PCISEnum.InputDTOValidationPattern.PhoneNumber_US);
                    Regex phnNumberRegex = new Regex(PCISEnum.InputDTOValidationPattern.PhoneNumber);
                    if (personCountryId == UScountryID && !phnNumberRegexUS.IsMatch(inputValue))
                    {
                        responseStatus = this.localize.GetLocalizedHtmlString(string.Format(PCISEnum.StatusMessages.RequiredParameterMissing, type));
                    }
                    else if (personCountryId != UScountryID && !phnNumberRegex.IsMatch(inputValue))
                    {
                        responseStatus = this.localize.GetLocalizedHtmlString(string.Format(PCISEnum.StatusMessages.RequiredParameterMissing, type));
                    }
                }
                else
                {
                    Regex zipRegexUS = new Regex(PCISEnum.InputDTOValidationPattern.Zip_US);
                    Regex zipRegex = new Regex(PCISEnum.InputDTOValidationPattern.Zip);
                    if (personCountryId == UScountryID && !zipRegexUS.IsMatch(inputValue))
                    {
                        responseStatus = this.localize.GetLocalizedHtmlString(string.Format(PCISEnum.StatusMessages.RequiredParameterMissing, PCISEnum.Parameters.Zip));
                    }
                    else if (personCountryId != UScountryID && !zipRegex.IsMatch(inputValue))
                    {
                        responseStatus = this.localize.GetLocalizedHtmlString(string.Format(PCISEnum.StatusMessages.RequiredParameterMissing, PCISEnum.Parameters.Zip));
                    }

                }
                return responseStatus;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
