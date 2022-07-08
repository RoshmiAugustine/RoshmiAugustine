// -----------------------------------------------------------------------
// <copyright file="UserProfileService.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.DTO.Output;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Domain.Interfaces.Services;
using Opeeka.PICS.Domain.Resources;
using Opeeka.PICS.Infrastructure.Enums;

namespace Opeeka.PICS.Domain.Services
{
    public class UserProfileService : BaseService, IUserProfileService
    {
        private readonly IUserProfileRepository userProfileRepository;
        private readonly IHelperRepository helperRepository;
        private readonly IHelperService helperService;
        private readonly IMapper mapper;



        /// Initializes a new instance of the LocalizeService/> class.
        private readonly LocalizeService localize;

        /// Initializes a new instance of the httpContext class.
        private readonly IHttpContextAccessor httpContext;

        public UserProfileService(LocalizeService localizeService, IConfigurationRepository configRepo, IHttpContextAccessor httpContext, IUserProfileRepository userProfileRepository, IHelperRepository helperRepository, IHelperService helperService, IMapper mapper)
            : base(configRepo, httpContext)
        {
            this.localize = localizeService;
            this.userProfileRepository = userProfileRepository;
            this.httpContext = httpContext;
            this.helperRepository = helperRepository;
            this.helperService = helperService;
            this.mapper = mapper;
        }

        /// <summary>
        /// AddUserProfile.
        /// </summary>
        /// <param name="userProfileDTO">userProfileDTO.</param>
        /// <returns>UserProfileResponseDTO.</returns>
        public UserProfileResponseDTO AddUserProfile(UserProfileDTO userProfileDTO)
        {
            try
            {
                UserProfileResponseDTO resultDTO = new UserProfileResponseDTO();
                int userProfileID = userProfileRepository.AddUserProfile(userProfileDTO);
                if (userProfileID != 0)
                {
                    resultDTO.ResponseStatusCode = PCISEnum.StatusCodes.InsertionSuccess;
                    resultDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.InsertionSuccess);
                }
                else
                {
                    resultDTO.ResponseStatusCode = PCISEnum.StatusCodes.insertionFailed;
                    resultDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.insertionFailed);
                }
                return resultDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ///// <summary>
        ///// DeleteUserProfile.
        ///// </summary>
        ///// <param name="userID">userID.</param>
        ///// <returns>UserProfileResponseDTO.</returns>
        //public UserProfileResponseDTO DeleteUserProfile(int userID)
        //{
        //    try
        //    {

        //        UserProfileResponseDTO resultDTO = new UserProfileResponseDTO();
        //        UserProfile userProfile = userProfileRepository.GetUserProfileByID(userID).Result;
        //        userProfileRepository.DeleteUserProfile(userProfile);
        //        if (isDeleted)
        //        {
        //            resultDTO.ResponseStatusCode = PCISEnum.StatusCodes.DeletionSuccess;
        //            resultDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.DeletionSuccess);
        //        }
        //        else
        //        {
        //            resultDTO.ResponseStatusCode = PCISEnum.StatusCodes.DeletionFailed;
        //            resultDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.DeletionFailed);
        //        }
        //        return resultDTO;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        
        /// <summary>
        /// Function to update user profile data
        /// </summary>
        /// <param name="userData"></param>
        /// <param name="agencyID"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public UserProfileResponseDTO UpdateUserProfile(UpdateUserProfileDTO userData, long agencyID,int userId)
        {
            try
            {
                UserProfileResponseDTO userProfileResponse = new UserProfileResponseDTO();
                var helperInfo = helperRepository.GetHelperInfo(userData.HelperIndex, agencyID);
                if(helperInfo?.Role == PCISEnum.Roles.SuperAdmin || helperInfo == null)
                {
                    userProfileResponse.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationFailed);
                    userProfileResponse.ResponseStatusCode = PCISEnum.StatusCodes.UpdationFailed;
                }
                else
                {
                    HelperDetailsDTO helperData = new HelperDetailsDTO();

                    this.mapper.Map<HelperInfoDTO, HelperDetailsDTO>(helperInfo, helperData);
                    helperData.FirstName = userData.FirstName;
                    helperData.MiddleName = userData.MiddleName;
                    helperData.LastName = userData.LastName;
                    helperData.Phone1 = userData.Phone1;
                    helperData.Phone2 = userData.Phone2;
                    helperData.HelperTitleID = userData.HelperTitleID == 0 ? null : (int?)userData.HelperTitleID;
                    helperData.Address = userData.Address;
                    helperData.UpdateUserID = userId;
                    var result = this.helperService.UpdateHelperDetails(helperData, agencyID).Result;
                    if (result != null)
                    {
                        userProfileResponse.ResponseStatus = result.ResponseStatus;
                        userProfileResponse.ResponseStatusCode = result.ResponseStatusCode;
                    }
                }

                return userProfileResponse;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
