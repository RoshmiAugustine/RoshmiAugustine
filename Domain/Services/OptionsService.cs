// -----------------------------------------------------------------------
// <copyright file="OptionsService.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.DTO.Input;
using Opeeka.PICS.Domain.DTO.Output;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Domain.Interfaces.Services;
using Opeeka.PICS.Domain.Resources;
using Opeeka.PICS.Infrastructure.Enums;

namespace Opeeka.PICS.Domain.Services
{
    public class OptionsService : BaseService, IOptionsService
    {
        /// Initializes a new instance of the collaborationlevelRepository/> class.
        private readonly ICollaborationLevelRepository collaborationlevelRepository;
        private readonly ICollaborationRepository collaborationRepository;
        private ICollaborationTagTypeRepository collaborationTagTypeRepository;
        private ICollaborationTagRepository collaborationTagRepository;
        private ITherapyTypeRepository therapyTypeRepository;
        private IHelperTitleRepository helperTitleRepository;
        private IHelperRepository helperRepository;
        private readonly INotificationLevelRepository notificationlevelRepository;
        private readonly IGenderRepository genderRepository;
        private readonly IIdentificationTypeRepository identificationTypeRepository;
        public readonly IRaceEthnicityRepository raceEthnicityRepository;
        public readonly ISupportTypeRepository supportTypeRepository;
        /// Initializes a new instance of the LocalizeService/> class.
        private readonly LocalizeService localize;
        private readonly IIdentifiedGenderRepository identifiedGenderRepository;
        public readonly IOptionsRepository optionsRepository;


        private readonly ISexualityRepository Sexualityrepository;
        private readonly INotificationTypeRepository notificationTypeRepository;
        CRUDResponseDTO response = new CRUDResponseDTO();
        private readonly IMapper mapper;

        /// <summary>
        ///  Initializes a new instance of the <see cref="OptionsService"/> class.
        /// </summary>
        /// <param name="collaborationlevelRepository"></param>
        public OptionsService(IMapper mapper,
        ICollaborationLevelRepository collaborationlevelRepository,
            ICollaborationRepository collaborationRepository,
            ICollaborationTagTypeRepository collaborationTagTypeRepository,
            ICollaborationTagRepository collaborationTagRepository,
            ITherapyTypeRepository therapyTypeRepository,
            IHelperTitleRepository helperTitleRepository,
            IHelperRepository helperRepository,
            INotificationLevelRepository notificationlevelRepository,
            IGenderRepository genderRepository,
            IIdentificationTypeRepository identificationTypeRepository,
            IRaceEthnicityRepository raceEthnicityRepository,
            ISupportTypeRepository supportTypeRepository,
            LocalizeService localizeService,
            IConfigurationRepository configRepo,
            IHttpContextAccessor httpContext,
             ISexualityRepository Sexualityrepository,
             INotificationTypeRepository notificationTypeRepository, IIdentifiedGenderRepository identifiedGenderRepository, IOptionsRepository optionsRepository)
            : base(configRepo, httpContext)

        {
            this.collaborationlevelRepository = collaborationlevelRepository;
            this.collaborationRepository = collaborationRepository;
            this.collaborationTagTypeRepository = collaborationTagTypeRepository;
            this.collaborationTagRepository = collaborationTagRepository;
            this.therapyTypeRepository = therapyTypeRepository;
            this.helperTitleRepository = helperTitleRepository;
            this.helperRepository = helperRepository;
            this.notificationlevelRepository = notificationlevelRepository;
            this.genderRepository = genderRepository;
            this.identificationTypeRepository = identificationTypeRepository;
            this.raceEthnicityRepository = raceEthnicityRepository;
            this.supportTypeRepository = supportTypeRepository;
            this.notificationTypeRepository = notificationTypeRepository;
            this.Sexualityrepository = Sexualityrepository;
            this.mapper = mapper;
            this.localize = localizeService;
            this.identifiedGenderRepository = identifiedGenderRepository;
            this.optionsRepository = optionsRepository;
        }

        /// <summary>
        /// AddCollaborationLevel.
        /// </summary>
        /// <param name="collaborationLevelData">collaborationLevelData.</param>
        /// <returns>CRUDResponseDTO.</returns>
        public CRUDResponseDTO AddCollaborationLevel(CollaborationLevelDTO collaborationLevelData, int userID, long agencyID)
        {
            try
            {
                Int64? CollaborationLevelID;
                if (collaborationLevelData != null)
                {
                    bool isValid = optionsRepository.IsValidListOrder(collaborationLevelData.ListOrder, PCISEnum.Options.CollaborationLevel, agencyID);
                    if (isValid)
                    {
                        var collaborationLevel = new CollaborationLevelDTO
                        {
                            Name = collaborationLevelData.Name,
                            Abbrev = collaborationLevelData.Abbrev,
                            Description = collaborationLevelData.Description,
                            ListOrder = collaborationLevelData.ListOrder,
                            IsRemoved = false,
                            UpdateDate = DateTime.UtcNow,
                            UpdateUserID = userID,
                            AgencyID = agencyID
                        };

                        CollaborationLevelID = this.collaborationlevelRepository.AddCollaborationLevel(collaborationLevel).CollaborationLevelID;
                        if (CollaborationLevelID != 0)
                        {
                            response.ResponseStatusCode = PCISEnum.StatusCodes.InsertionSuccess;
                            response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.InsertionSuccess);
                        }
                    }
                    else
                    {
                        response.ResponseStatusCode = PCISEnum.StatusCodes.InvalidListOrder;
                        response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.InvalidListOrder);
                    }
                }
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// DeleteCollaborationLevel.
        /// </summary>
        /// <param name="collaborationlevelID"></param>
        /// <returns>CRUDResponseDTO.</returns>
        public CRUDResponseDTO DeleteCollaborationLevel(int collaborationlevelID, long agencyID)
        {
            try
            {
                var collaborationLevel = this.collaborationlevelRepository.GetCollaborationLevel(collaborationlevelID).Result;
                if (collaborationLevel?.AgencyID == agencyID)
                {
                    if (collaborationLevel != null && collaborationLevel.CollaborationLevelID != 0)
                    {
                        var collaborationCount = this.collaborationRepository.GetCollaborationCountByLevel(collaborationlevelID);
                        if (collaborationCount == 0)
                        {
                            collaborationLevel.IsRemoved = true;
                            collaborationLevel.UpdateDate = DateTime.UtcNow;
                            var languageResponse = this.collaborationlevelRepository.UpdateCollaborationLevel(collaborationLevel);
                            if (languageResponse != null)
                            {
                                response.ResponseStatusCode = PCISEnum.StatusCodes.DeletionSuccess;
                                response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.DeletionSuccess);
                            }
                            else
                            {
                                response.ResponseStatusCode = PCISEnum.StatusCodes.DeletionFailed;
                                response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.DeletionFailed);
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
        /// GetCollaborationLevelList.
        /// </summary>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize</param>
        /// <returns>CollaborationLevelResponseDTO.</returns>
        public CollaborationLevelResponseDTO GetCollaborationLevelList(int pageNumber, int pageSize, long agencyID)
        {
            try
            {
                CollaborationLevelResponseDTO collaborationLevelResponseDTO = new CollaborationLevelResponseDTO();
                if (pageNumber <= 0)
                {
                    collaborationLevelResponseDTO.CollaborationLevels = null;
                    collaborationLevelResponseDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PageNumber));
                    collaborationLevelResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return collaborationLevelResponseDTO;
                }
                else if (pageSize <= 0)
                {
                    collaborationLevelResponseDTO.CollaborationLevels = null;
                    collaborationLevelResponseDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PageSize));
                    collaborationLevelResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return collaborationLevelResponseDTO;
                }
                else
                {
                    var response = this.collaborationlevelRepository.GetCollaborationLevelList(pageNumber, pageSize, agencyID);
                    var count = this.collaborationlevelRepository.GetCollaborationLevelCount(agencyID);
                    collaborationLevelResponseDTO.CollaborationLevels = response;
                    collaborationLevelResponseDTO.TotalCount = count;
                    collaborationLevelResponseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                    collaborationLevelResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                    return collaborationLevelResponseDTO;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// UpdateCollaborationLevel.
        /// </summary>
        /// <param name="collaborationLevelData">collaborationLevelData.</param>
        /// <returns>CRUDResponseDTO.</returns>
        public CRUDResponseDTO UpdateCollaborationLevel(CollaborationLevelDTO collaborationLevelData, int userID, long agencyID)
        {
            try
            {
                if (collaborationLevelData != null)
                {
                    bool isValid = optionsRepository.IsValidListOrder(collaborationLevelData.ListOrder, PCISEnum.Options.CollaborationLevel, agencyID, collaborationLevelData.CollaborationLevelID);
                    if (isValid)
                    {
                        var collaborationLevelupdate = this.collaborationlevelRepository.GetCollaborationLevel(collaborationLevelData.CollaborationLevelID).Result;
                        if (collaborationLevelupdate != null && collaborationLevelupdate.CollaborationLevelID != 0)
                        {
                            var collaborationLevel = new CollaborationLevelDTO
                            {
                                CollaborationLevelID = collaborationLevelData.CollaborationLevelID,
                                Name = collaborationLevelData.Name,
                                Abbrev = collaborationLevelData.Abbrev,
                                Description = collaborationLevelData.Description,
                                ListOrder = collaborationLevelData.ListOrder,
                                UpdateDate = DateTime.UtcNow,
                                UpdateUserID = userID,
                                AgencyID = agencyID
                            };

                            var addressResponse = this.collaborationlevelRepository.UpdateCollaborationLevel(collaborationLevel);
                            if (addressResponse != null)
                            {
                                response.ResponseStatusCode = PCISEnum.StatusCodes.UpdationSuccess;
                                response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationSuccess);
                            }
                            else
                            {
                                response.ResponseStatusCode = PCISEnum.StatusCodes.UpdationFailed;
                                response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationFailed);
                            }
                        }
                        else
                        {
                            response.ResponseStatusCode = PCISEnum.StatusCodes.UpdationFailed;
                            response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationFailed);
                        }
                    }
                    else
                    {
                        response.ResponseStatusCode = PCISEnum.StatusCodes.InvalidListOrder;
                        response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.InvalidListOrder);
                    }
                }
                else
                {
                    response.ResponseStatusCode = PCISEnum.StatusCodes.UpdationFailed;
                    response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationFailed);
                }

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get the Collaboration Tag Type list
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns>CRUDResponseDTO</returns>
        public CollaborationTagTypeListResponseDTO GetCollaborationTagTypeList(int pageNumber, int pageSize, long agencyID)
        {
            try
            {
                CollaborationTagTypeListResponseDTO CollaborationTagTypeListDTO = new CollaborationTagTypeListResponseDTO();
                if (pageNumber <= 0)
                {
                    CollaborationTagTypeListDTO.CollaborationTagTypeList = null;
                    CollaborationTagTypeListDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PageNumber));
                    CollaborationTagTypeListDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return CollaborationTagTypeListDTO;
                }
                else if (pageSize <= 0)
                {
                    CollaborationTagTypeListDTO.CollaborationTagTypeList = null;
                    CollaborationTagTypeListDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PageSize));
                    CollaborationTagTypeListDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return CollaborationTagTypeListDTO;
                }
                else
                {
                    var response = this.collaborationTagTypeRepository.GetCollaborationTagTypeList(pageNumber, pageSize, agencyID);
                    var count = this.collaborationTagTypeRepository.GetCollaborationTagTypeCount(agencyID);
                    CollaborationTagTypeListDTO.CollaborationTagTypeList = response;
                    CollaborationTagTypeListDTO.TotalCount = count;
                    CollaborationTagTypeListDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                    CollaborationTagTypeListDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                    return CollaborationTagTypeListDTO;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///  Add a new Collaboration Tag Type
        /// </summary>
        /// <param name="collaborationTagTypeData"></param>
        /// <returns>Object of CRUDResponseDTO</returns>
        public CRUDResponseDTO AddCollaborationTagType(CollaborationTagTypeDetailsDTO collaborationTagTypeData, int userID, long agencyID)
        {
            try
            {
                int? CollaborationTagTypeID;
                if (collaborationTagTypeData != null)
                {
                    bool isValid = optionsRepository.IsValidListOrder(collaborationTagTypeData.ListOrder, PCISEnum.Options.CollaborationTagType, agencyID);
                    if (isValid)
                    {
                        var collaborationTagType = new CollaborationTagTypeDTO
                        {
                            Name = collaborationTagTypeData.Name,
                            Abbrev = collaborationTagTypeData.Abbrev,
                            Description = collaborationTagTypeData.Description,
                            ListOrder = collaborationTagTypeData.ListOrder,
                            IsRemoved = false,
                            UpdateDate = DateTime.UtcNow,
                            UpdateUserID = userID,
                            AgencyID = agencyID
                        };

                        CollaborationTagTypeID = this.collaborationTagTypeRepository.AddCollaborationTagType(collaborationTagType).CollaborationTagTypeID;
                        if (CollaborationTagTypeID != 0)
                        {
                            response.ResponseStatusCode = PCISEnum.StatusCodes.InsertionSuccess;
                            response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.InsertionSuccess);
                        }
                        else
                        {
                            response.ResponseStatusCode = PCISEnum.StatusCodes.insertionFailed;
                            response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.insertionFailed);
                        }
                    }
                    else
                    {
                        response.ResponseStatusCode = PCISEnum.StatusCodes.InvalidListOrder;
                        response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.InvalidListOrder);
                    }
                }
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Update an existing Collaboration Tag Type
        /// </summary>
        /// <param name="collaborationTagTypeData"></param>
        /// <returns>CRUDResponseDTO</returns>
        public CRUDResponseDTO UpdateCollaborationTagType(CollaborationTagTypeDetailsDTO collaborationTagTypeData, int userID, long agencyID)
        {
            try
            {
                if (collaborationTagTypeData != null)
                {
                    bool isValid = optionsRepository.IsValidListOrder(collaborationTagTypeData.ListOrder, PCISEnum.Options.CollaborationTagType, agencyID, collaborationTagTypeData.CollaborationTagTypeID);
                    if (isValid)
                    {
                        var collaborationTagType = new CollaborationTagTypeDTO
                        {
                            CollaborationTagTypeID = collaborationTagTypeData.CollaborationTagTypeID,
                            Name = collaborationTagTypeData.Name,
                            Abbrev = collaborationTagTypeData.Abbrev,
                            Description = collaborationTagTypeData.Description,
                            ListOrder = collaborationTagTypeData.ListOrder,
                            UpdateDate = DateTime.UtcNow,
                            UpdateUserID = userID,
                            AgencyID = agencyID
                        };

                        var updateResponse = this.collaborationTagTypeRepository.UpdateCollaborationTagType(collaborationTagType);
                        if (updateResponse != null)
                        {
                            response.ResponseStatusCode = PCISEnum.StatusCodes.UpdationSuccess;
                            response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationSuccess);
                        }
                        else
                        {
                            response.ResponseStatusCode = PCISEnum.StatusCodes.UpdationFailed;
                            response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationFailed);
                        }
                    }
                    else
                    {
                        response.ResponseStatusCode = PCISEnum.StatusCodes.InvalidListOrder;
                        response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.InvalidListOrder);
                    }
                }

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Delete an existing Collaboration Tag Type
        /// </summary>
        /// <param name="collaborationTagTypeID"></param>
        /// <returns>CRUDResponseDTO</returns>
        public CRUDResponseDTO DeleteCollaborationTagType(int collaborationTagTypeID, long agencyID)
        {
            try
            {
                var collaborationTagType = this.collaborationTagTypeRepository.GetCollaborationTagType(collaborationTagTypeID).Result;
                if (collaborationTagType?.AgencyID == agencyID)
                {
                    if (collaborationTagType != null)
                    {
                        var usedCount = this.collaborationTagRepository.GetCollaborationTagTypeCountByCollaborationTag(collaborationTagTypeID);
                        if (usedCount == 0)
                        {
                            collaborationTagType.IsRemoved = true;
                            collaborationTagType.UpdateDate = DateTime.UtcNow;
                            var collaborationTagTypeResponse = this.collaborationTagTypeRepository.UpdateCollaborationTagType(collaborationTagType);
                            if (collaborationTagTypeResponse != null)
                            {
                                response.ResponseStatusCode = PCISEnum.StatusCodes.DeletionSuccess;
                                response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.DeletionSuccess);
                            }
                            else
                            {
                                response.ResponseStatusCode = PCISEnum.StatusCodes.DeletionFailed;
                                response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.DeletionFailed);
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
        /// Get the Collaboration Tag Type list for an agency
        /// </summary>
        /// <param name="agencyId"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns>CollaborationTagTypeListResponseDTO</returns>
        public CollaborationTagTypeListResponseDTO GetCollaborationTagTypeList(int agencyId, int pageNumber, int pageSize)
        {
            try
            {
                CollaborationTagTypeListResponseDTO CollaborationTagTypeListDTO = new CollaborationTagTypeListResponseDTO();
                if (pageNumber <= 0)
                {
                    CollaborationTagTypeListDTO.CollaborationTagTypeList = null;
                    CollaborationTagTypeListDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PageNumber));
                    CollaborationTagTypeListDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return CollaborationTagTypeListDTO;
                }
                else if (pageSize <= 0)
                {
                    CollaborationTagTypeListDTO.CollaborationTagTypeList = null;
                    CollaborationTagTypeListDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PageSize));
                    CollaborationTagTypeListDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return CollaborationTagTypeListDTO;
                }
                else
                {
                    var response = this.collaborationTagTypeRepository.GetCollaborationTagTypeList(pageNumber, pageSize, agencyId);
                    var count = this.collaborationTagTypeRepository.GetCollaborationTagTypeCount(agencyId);
                    CollaborationTagTypeListDTO.CollaborationTagTypeList = response;
                    CollaborationTagTypeListDTO.TotalCount = count;
                    CollaborationTagTypeListDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                    CollaborationTagTypeListDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                    return CollaborationTagTypeListDTO;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get the Therapy Type list
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns>CRUDResponseDTO</returns>
        public TherapyTypesResponseDTO GetTherapyTypeList(int pageNumber, int pageSize, long agencyID)
        {
            try
            {
                TherapyTypesResponseDTO TherapyTypesDTO = new TherapyTypesResponseDTO();
                if (pageNumber <= 0)
                {
                    TherapyTypesDTO.TherapyTypes = null;
                    TherapyTypesDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PageNumber));
                    TherapyTypesDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return TherapyTypesDTO;
                }
                else if (pageSize <= 0)
                {
                    TherapyTypesDTO.TherapyTypes = null;
                    TherapyTypesDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PageSize));
                    TherapyTypesDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return TherapyTypesDTO;
                }
                else
                {
                    var response = this.therapyTypeRepository.GetTherapyTypeList(pageNumber, pageSize, agencyID);
                    var count = this.therapyTypeRepository.GetTherapyTypeCount(agencyID);
                    TherapyTypesDTO.TherapyTypes = response;
                    TherapyTypesDTO.TotalCount = count;
                    TherapyTypesDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                    TherapyTypesDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                    return TherapyTypesDTO;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///  Add a new Therapy Type
        /// </summary>
        /// <param name="therapyTypeData"></param>
        /// <returns>Object of CRUDResponseDTO</returns>
        public CRUDResponseDTO AddTherapyType(TherapyTypeDetailsDTO therapyTypeData, int userID, long agencyID)
        {
            try
            {
                int? TherapyTypeID;
                if (therapyTypeData != null)
                {
                    bool isValid = optionsRepository.IsValidListOrder(therapyTypeData.ListOrder, PCISEnum.Options.TherapyType, agencyID);
                    if (isValid)
                    {
                        var therapyType = new TherapyTypeDTO
                        {
                            Name = therapyTypeData.Name,
                            Abbrev = therapyTypeData.Abbrev,
                            Description = therapyTypeData.Description,
                            ListOrder = therapyTypeData.ListOrder,
                            IsRemoved = false,
                            UpdateDate = DateTime.UtcNow,
                            UpdateUserID = userID,
                            AgencyID = agencyID,
                            IsResidential = therapyTypeData.IsResidential
                        };

                        TherapyTypeID = this.therapyTypeRepository.AddTherapyType(therapyType).TherapyTypeID;
                        if (TherapyTypeID != 0)
                        {
                            response.ResponseStatusCode = PCISEnum.StatusCodes.InsertionSuccess;
                            response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.InsertionSuccess);
                        }
                        else
                        {
                            response.ResponseStatusCode = PCISEnum.StatusCodes.insertionFailed;
                            response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.insertionFailed);
                        }
                    }
                    else
                    {
                        response.ResponseStatusCode = PCISEnum.StatusCodes.InvalidListOrder;
                        response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.InvalidListOrder);
                    }
                }
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Update an existing Therapy Type
        /// </summary>
        /// <param name="therapyTypeData"></param>
        /// <returns>CRUDResponseDTO</returns>
        public CRUDResponseDTO UpdateTherapyType(TherapyTypeDetailsDTO therapyTypeData, int userID, long agencyID)
        {
            try
            {
                if (therapyTypeData != null)
                {
                    bool isValid = optionsRepository.IsValidListOrder(therapyTypeData.ListOrder, PCISEnum.Options.TherapyType, agencyID, therapyTypeData.TherapyTypeID);
                    if (isValid)
                    {
                        var therapyType = new TherapyTypeDTO
                        {
                            TherapyTypeID = therapyTypeData.TherapyTypeID,
                            Name = therapyTypeData.Name,
                            Abbrev = therapyTypeData.Abbrev,
                            Description = therapyTypeData.Description,
                            ListOrder = therapyTypeData.ListOrder,
                            UpdateDate = DateTime.UtcNow,
                            UpdateUserID = userID,
                            AgencyID = agencyID,
                            IsResidential = therapyTypeData.IsResidential
                        };

                        var updateResponse = this.therapyTypeRepository.UpdateTherapyType(therapyType);
                        if (updateResponse != null)
                        {
                            response.ResponseStatusCode = PCISEnum.StatusCodes.UpdationSuccess;
                            response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationSuccess);
                        }
                        else
                        {
                            response.ResponseStatusCode = PCISEnum.StatusCodes.UpdationFailed;
                            response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationFailed);
                        }
                    }
                    else
                    {
                        response.ResponseStatusCode = PCISEnum.StatusCodes.InvalidListOrder;
                        response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.InvalidListOrder);
                    }
                }

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Delete an existing Therapy Type
        /// </summary>
        /// <param name="therapyTypeID"></param>
        /// <returns>CRUDResponseDTO</returns>
        public CRUDResponseDTO DeleteTherapyType(int therapyTypeID, long agencyID)
        {
            try
            {
                var therapyType = this.therapyTypeRepository.GetTherapyType(therapyTypeID).Result;
                if (therapyType?.AgencyID == agencyID)
                {
                    if (therapyType != null)
                    {
                        var usedCount = this.collaborationRepository.GetCollaborationCountByTherapy(therapyTypeID);
                        if (usedCount == 0)
                        {
                            therapyType.IsRemoved = true;
                            therapyType.UpdateDate = DateTime.UtcNow;
                            var therapyTypeResponse = this.therapyTypeRepository.UpdateTherapyType(therapyType);
                            if (therapyTypeResponse != null)
                            {
                                response.ResponseStatusCode = PCISEnum.StatusCodes.DeletionSuccess;
                                response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.DeletionSuccess);
                            }
                            else
                            {
                                response.ResponseStatusCode = PCISEnum.StatusCodes.DeletionFailed;
                                response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.DeletionFailed);
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
        /// Get the Therapy Type list for an agency
        /// </summary>
        /// <param name="agencyId"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns>TherapyTypesResponseDTO</returns>
        public TherapyTypesResponseDTO GetTherapyTypeList(int agencyId, int pageNumber, int pageSize)
        {
            try
            {
                TherapyTypesResponseDTO TherapyTypesDTO = new TherapyTypesResponseDTO();
                if (pageNumber <= 0)
                {
                    TherapyTypesDTO.TherapyTypes = null;
                    TherapyTypesDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PageNumber));
                    TherapyTypesDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return TherapyTypesDTO;
                }
                else if (pageSize <= 0)
                {
                    TherapyTypesDTO.TherapyTypes = null;
                    TherapyTypesDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PageSize));
                    TherapyTypesDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return TherapyTypesDTO;
                }
                else
                {
                    var response = this.therapyTypeRepository.GetTherapyTypeList(agencyId, pageNumber, pageSize);
                    var count = this.therapyTypeRepository.GetTherapyTypeCount(agencyId);
                    TherapyTypesDTO.TherapyTypes = response;
                    TherapyTypesDTO.TotalCount = count;
                    TherapyTypesDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                    TherapyTypesDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                    return TherapyTypesDTO;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get the Helper title list
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns>CRUDResponseDTO</returns>
        public HelperTitleResponseDTO GetHelperTitleList(int pageNumber, int pageSize, long agencyID)
        {
            try
            {
                HelperTitleResponseDTO HelperTitleListDTO = new HelperTitleResponseDTO();
                if (pageNumber <= 0)
                {
                    HelperTitleListDTO.HelperTitles = null;
                    HelperTitleListDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PageNumber));
                    HelperTitleListDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return HelperTitleListDTO;
                }
                else if (pageSize <= 0)
                {
                    HelperTitleListDTO.HelperTitles = null;
                    HelperTitleListDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PageSize));
                    HelperTitleListDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return HelperTitleListDTO;
                }
                else
                {
                    var response = this.helperTitleRepository.GetHelperTitleList(pageNumber, pageSize, agencyID);
                    var count = this.helperTitleRepository.GetHelperTitleCount(agencyID);
                    HelperTitleListDTO.HelperTitles = response;
                    HelperTitleListDTO.TotalCount = count;
                    HelperTitleListDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                    HelperTitleListDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                    return HelperTitleListDTO;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///  Add a new Helper title
        /// </summary>
        /// <param name="helperTitleData"></param>
        /// <returns>Object of CRUDResponseDTO</returns>
        public CRUDResponseDTO AddHelperTitle(HelperTitleDetailsDTO helperTitleData, int userID, long agencyID)
        {
            try
            {
                int? HelperTitleID;
                if (helperTitleData != null)
                {
                    bool isValid = optionsRepository.IsValidListOrder(helperTitleData.ListOrder, PCISEnum.Options.HelperTitle, agencyID);
                    if (isValid)
                    {
                        var helperTitle = new HelperTitleDTO
                        {
                            Name = helperTitleData.Name,
                            Abbrev = helperTitleData.Abbrev,
                            Description = helperTitleData.Description,
                            ListOrder = helperTitleData.ListOrder,
                            IsRemoved = false,
                            UpdateDate = DateTime.UtcNow,
                            UpdateUserID = userID,
                            AgencyID = agencyID
                        };

                        HelperTitleID = this.helperTitleRepository.AddHelperTitle(helperTitle).HelperTitleID;
                        if (HelperTitleID != 0)
                        {
                            response.ResponseStatusCode = PCISEnum.StatusCodes.InsertionSuccess;
                            response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.InsertionSuccess);
                        }
                        else
                        {
                            response.ResponseStatusCode = PCISEnum.StatusCodes.insertionFailed;
                            response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.insertionFailed);
                        }
                    }
                    else
                    {
                        response.ResponseStatusCode = PCISEnum.StatusCodes.InvalidListOrder;
                        response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.InvalidListOrder);
                    }
                }
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Update an existing Helper title
        /// </summary>
        /// <param name="helperTitleData"></param>
        /// <returns>CRUDResponseDTO</returns>
        public CRUDResponseDTO UpdateHelperTitle(HelperTitleDetailsDTO helperTitleData, int userID, long agencyID)
        {
            try
            {
                if (helperTitleData != null)
                {
                    bool isValid = optionsRepository.IsValidListOrder(helperTitleData.ListOrder, PCISEnum.Options.CollaborationTagType, agencyID, helperTitleData.HelperTitleID);
                    if (isValid)
                    {

                        var helperTitle = new HelperTitleDTO
                        {
                            HelperTitleID = helperTitleData.HelperTitleID,
                            Name = helperTitleData.Name,
                            Abbrev = helperTitleData.Abbrev,
                            Description = helperTitleData.Description,
                            ListOrder = helperTitleData.ListOrder,
                            UpdateDate = DateTime.UtcNow,
                            UpdateUserID = userID,
                            AgencyID = agencyID
                        };

                        var updateResponse = this.helperTitleRepository.UpdateHelperTitle(helperTitle);
                        if (updateResponse != null)
                        {
                            response.ResponseStatusCode = PCISEnum.StatusCodes.UpdationSuccess;
                            response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationSuccess);
                        }
                        else
                        {
                            response.ResponseStatusCode = PCISEnum.StatusCodes.UpdationFailed;
                            response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationFailed);
                        }
                    }
                    else
                    {
                        response.ResponseStatusCode = PCISEnum.StatusCodes.InvalidListOrder;
                        response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.InvalidListOrder);
                    }
                }

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Delete an existing Helper title
        /// </summary>
        /// <param name="helperTitleID"></param>
        /// <returns>CRUDResponseDTO</returns>
        public CRUDResponseDTO DeleteHelperTitle(int helperTitleID, long agencyID)
        {
            try
            {
                var helperTitle = this.helperTitleRepository.GetHelperTitle(helperTitleID).Result;
                if (helperTitle?.AgencyID == agencyID)
                {
                    if (helperTitle != null)
                    {
                        var personCount = this.helperRepository.GetHelperCountByHelperTitle(helperTitleID);
                        if (personCount == 0)
                        {
                            helperTitle.IsRemoved = true;
                            helperTitle.UpdateDate = DateTime.UtcNow;
                            var helperTitleResponse = this.helperTitleRepository.UpdateHelperTitle(helperTitle);
                            if (helperTitleResponse != null)
                            {
                                response.ResponseStatusCode = PCISEnum.StatusCodes.DeletionSuccess;
                                response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.DeletionSuccess);
                            }
                            else
                            {
                                response.ResponseStatusCode = PCISEnum.StatusCodes.DeletionFailed;
                                response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.DeletionFailed);
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
        /// Get the Helper title for an agency list
        /// </summary>
        /// <param name="agencyId"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns>HelperTitleResponseDTO</returns>
        public HelperTitleResponseDTO GetHelperTitleList(long agencyId, int pageNumber, int pageSize)
        {
            try
            {
                HelperTitleResponseDTO HelperTitleListDTO = new HelperTitleResponseDTO();
                if (pageNumber <= 0)
                {
                    HelperTitleListDTO.HelperTitles = null;
                    HelperTitleListDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PageNumber));
                    HelperTitleListDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return HelperTitleListDTO;
                }
                else if (pageSize <= 0)
                {
                    HelperTitleListDTO.HelperTitles = null;
                    HelperTitleListDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PageSize));
                    HelperTitleListDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return HelperTitleListDTO;
                }
                else
                {
                    var response = this.helperTitleRepository.GetHelperTitleList(agencyId, pageNumber, pageSize);
                    var count = this.helperTitleRepository.GetHelperTitleCount(agencyId);
                    HelperTitleListDTO.HelperTitles = response;
                    HelperTitleListDTO.TotalCount = count;
                    HelperTitleListDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                    HelperTitleListDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                    return HelperTitleListDTO;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// AddNotificationLevel.
        /// </summary>
        /// <param name="notificationLevelData">notificationLevelData.</param>
        /// <returns>CRUDResponseDTO.</returns>
        public CRUDResponseDTO AddNotificationLevel(NotificationLevelDTO notificationLevelData, int userID, long agencyID)
        {
            try
            {
                Int64? NotificationLevelID;
                if (notificationLevelData != null)
                {
                    bool isValid = optionsRepository.IsValidListOrder(notificationLevelData.ListOrder, PCISEnum.Options.NotificationLevel, agencyID);
                    if (isValid)
                    {
                        var notificationLevel = new NotificationLevelDTO
                        {
                            Name = notificationLevelData.Name,
                            Abbrev = notificationLevelData.Abbrev,
                            Description = notificationLevelData.Description,
                            ListOrder = notificationLevelData.ListOrder,
                            IsRemoved = false,
                            UpdateDate = DateTime.UtcNow,
                            UpdateUserID = userID,
                            AgencyID = agencyID,
                            NotificationTypeID = notificationLevelData.NotificationTypeID
                        };

                        NotificationLevelID = this.notificationlevelRepository.AddNotificationLevel(notificationLevel).NotificationLevelID;
                        if (NotificationLevelID != 0)
                        {
                            response.ResponseStatusCode = PCISEnum.StatusCodes.InsertionSuccess;
                            response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.InsertionSuccess);
                        }
                    }
                    else
                    {
                        response.ResponseStatusCode = PCISEnum.StatusCodes.InvalidListOrder;
                        response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.InvalidListOrder);
                    }
                }
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// DeleteNotificationLevel.
        /// </summary>
        /// <param name="notificationlevelID">notificationlevelID.</param>
        /// <returns>CRUDResponseDTO.</returns>
        public CRUDResponseDTO DeleteNotificationLevel(int notificationlevelID, long agencyID)
        {
            try
            {
                var notificationLevel = this.notificationlevelRepository.GetNotificationLevel(notificationlevelID).Result;
                if (notificationLevel?.AgencyID == agencyID)
                {
                    if (notificationLevel != null && notificationLevel.NotificationLevelID != 0)
                    {
                        var notificationcount = this.notificationlevelRepository.GetNotificationLevelCountByNotificationLevelID(notificationlevelID);

                        if (notificationcount == 0)
                        {
                            notificationLevel.IsRemoved = true;
                            notificationLevel.UpdateDate = DateTime.UtcNow;
                            var languageResponse = this.notificationlevelRepository.UpdateNotificationLevel(notificationLevel);
                            if (languageResponse != null)
                            {
                                response.ResponseStatusCode = PCISEnum.StatusCodes.DeletionSuccess;
                                response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.DeletionSuccess);
                            }
                            else
                            {
                                response.ResponseStatusCode = PCISEnum.StatusCodes.DeletionFailed;
                                response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.DeletionFailed);
                            }
                        }
                        else
                        {
                            response.ResponseStatusCode = PCISEnum.StatusCodes.DeleteRecordInUse;
                            response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.DeleteRecordInUse);
                        }
                    }
                    else
                    {
                        response.ResponseStatusCode = PCISEnum.StatusCodes.DeletionFailed;
                        response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.DeletionFailed);
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
        /// GetNotificationLevelList.
        /// </summary>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <returns>NotificationLevelResponseDTO.</returns>
        public NotificationLevelResponseDTO GetNotificationLevelList(int pageNumber, int pageSize, long agencyID)
        {
            try
            {
                NotificationLevelResponseDTO notificationLevelResponseDTO = new NotificationLevelResponseDTO();
                if (pageNumber <= 0)
                {
                    notificationLevelResponseDTO.NotificationLevels = null;
                    notificationLevelResponseDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PageNumber));
                    notificationLevelResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return notificationLevelResponseDTO;
                }
                else if (pageSize <= 0)
                {
                    notificationLevelResponseDTO.NotificationLevels = null;
                    notificationLevelResponseDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PageSize));
                    notificationLevelResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return notificationLevelResponseDTO;
                }
                else
                {
                    var response = this.notificationlevelRepository.GetNotificationLevelList(pageNumber, pageSize, agencyID);
                    foreach (var notificationLevel in response)
                    {
                        var notificationTypes = this.notificationTypeRepository.GetAllNotificationType();
                        if (notificationLevel.NotificationTypeID > 0)
                        {
                            var currentType = notificationTypes.Where(x => x.NotificationTypeID == notificationLevel.NotificationTypeID).FirstOrDefault();
                            if (currentType != null)
                            {
                                notificationLevel.NotificationType = currentType.Name;
                            }
                        }
                    }
                    var count = this.notificationlevelRepository.GetNotificationLevelCount(agencyID);
                    notificationLevelResponseDTO.NotificationLevels = response;
                    notificationLevelResponseDTO.TotalCount = count;
                    notificationLevelResponseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                    notificationLevelResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                    return notificationLevelResponseDTO;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// UpdateNotificationLevel.
        /// </summary>
        /// <param name="notificationLevelData">notificationLevelData.</param>
        /// <returns>CRUDResponseDTO.</returns>
        public CRUDResponseDTO UpdateNotificationLevel(NotificationLevelDTO notificationLevelData, int userID, long agencyID)
        {
            try
            {
                if (notificationLevelData != null)
                {
                    bool isValid = optionsRepository.IsValidListOrder(notificationLevelData.ListOrder, PCISEnum.Options.NotificationLevel, agencyID, notificationLevelData.NotificationLevelID);
                    if (isValid)
                    {
                        var notificationLevelUpdate = this.notificationlevelRepository.GetNotificationLevel(notificationLevelData.NotificationLevelID).Result;
                        if (notificationLevelUpdate.NotificationLevelID != 0 && notificationLevelUpdate != null)
                        {
                            var notificationLevel = new NotificationLevelDTO
                            {
                                NotificationLevelID = notificationLevelData.NotificationLevelID,
                                Name = notificationLevelData.Name,
                                Abbrev = notificationLevelData.Abbrev,
                                Description = notificationLevelData.Description,
                                ListOrder = notificationLevelData.ListOrder,
                                UpdateDate = DateTime.UtcNow,
                                UpdateUserID = userID,
                                AgencyID = agencyID,
                                RequireResolution = notificationLevelData.RequireResolution,
                                NotificationTypeID = notificationLevelData.NotificationTypeID
                            };

                            var addressResponse = this.notificationlevelRepository.UpdateNotificationLevel(notificationLevel);
                            if (addressResponse != null)
                            {
                                response.ResponseStatusCode = PCISEnum.StatusCodes.UpdationSuccess;
                                response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationSuccess);
                            }
                            else
                            {
                                response.ResponseStatusCode = PCISEnum.StatusCodes.UpdationFailed;
                                response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationFailed);
                            }
                        }
                        else
                        {
                            response.ResponseStatusCode = PCISEnum.StatusCodes.UpdationFailed;
                            response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationFailed);
                        }
                    }
                    else
                    {
                        response.ResponseStatusCode = PCISEnum.StatusCodes.InvalidListOrder;
                        response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.InvalidListOrder);
                    }
                }
                else
                {
                    response.ResponseStatusCode = PCISEnum.StatusCodes.UpdationFailed;
                    response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationFailed);
                }

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// AddGender.
        /// </summary>
        /// <param name="genderData">genderData.</param>
        /// <returns>CRUDResponseDTO.</returns>
        public CRUDResponseDTO AddGender(GenderDTO genderData, int userID, long agencyID)
        {
            try
            {
                Int64? GenderID;
                if (genderData != null)
                {
                    bool isValid = optionsRepository.IsValidListOrder(genderData.ListOrder, PCISEnum.Options.Gender, agencyID);
                    if (isValid)
                    {
                        var gender = new GenderDTO
                        {
                            Name = genderData.Name,
                            Abbrev = genderData.Abbrev,
                            Description = genderData.Description,
                            ListOrder = genderData.ListOrder,
                            IsRemoved = false,
                            UpdateDate = DateTime.UtcNow,
                            UpdateUserID = userID,
                            AgencyID = agencyID
                        };
                        GenderID = this.genderRepository.AddGender(gender).GenderID;
                        if (GenderID != 0)
                        {
                            response.ResponseStatusCode = PCISEnum.StatusCodes.InsertionSuccess;
                            response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.InsertionSuccess);
                        }
                    }
                    else
                    {
                        response.ResponseStatusCode = PCISEnum.StatusCodes.InvalidListOrder;
                        response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.InvalidListOrder);
                    }
                }
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// DeleteGender.
        /// </summary>
        /// <param name="genderID">genderID.</param>
        /// <returns>CRUDResponseDTO.</returns>
        public CRUDResponseDTO DeleteGender(int genderID, long agencyID)
        {
            try
            {
                var gender = this.genderRepository.GetGender(genderID).Result;
                if (gender?.AgencyID == agencyID)
                {
                    if (gender != null && gender.GenderID != 0)
                    {
                        var notificationCount = this.genderRepository.GetGenderCountByGenderID(genderID);
                        if (notificationCount == 0)
                        {
                            gender.IsRemoved = true;
                            gender.UpdateDate = DateTime.UtcNow;
                            var languageResponse = this.genderRepository.UpdateGender(gender);
                            if (languageResponse != null)
                            {
                                response.ResponseStatusCode = PCISEnum.StatusCodes.DeletionSuccess;
                                response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.DeletionSuccess);
                            }
                            else
                            {
                                response.ResponseStatusCode = PCISEnum.StatusCodes.DeletionFailed;
                                response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.DeletionFailed);
                            }
                        }
                        else
                        {
                            response.ResponseStatusCode = PCISEnum.StatusCodes.DeleteRecordInUse;
                            response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.DeleteRecordInUse);
                        }
                    }
                    else
                    {
                        response.ResponseStatusCode = PCISEnum.StatusCodes.DeletionFailed;
                        response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.DeletionFailed);
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
        /// GetGenderList.
        /// </summary>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <returns>GenderResponseDTO.</returns>
        public GenderResponseDTO GetGenderList(int pageNumber, int pageSize, long agencyID)
        {
            try
            {
                GenderResponseDTO genderResponseDTO = new GenderResponseDTO();
                if (pageNumber <= 0)
                {
                    genderResponseDTO.Genders = null;
                    genderResponseDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PageNumber));
                    genderResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return genderResponseDTO;
                }
                else if (pageSize <= 0)
                {
                    genderResponseDTO.Genders = null;
                    genderResponseDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PageSize));
                    genderResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return genderResponseDTO;
                }
                else
                {
                    var response = this.genderRepository.GetGenderList(pageNumber, pageSize, agencyID);
                    var count = this.genderRepository.GetGenderCount(agencyID);
                    genderResponseDTO.Genders = response;
                    genderResponseDTO.TotalCount = count;
                    genderResponseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                    genderResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                    return genderResponseDTO;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// UpdateGender.
        /// </summary>
        /// <param name="genderData">genderData.</param>
        /// <returns>CRUDResponseDTO.</returns>
        public CRUDResponseDTO UpdateGender(GenderDTO genderData, int userID, long agencyID)
        {
            try
            {
                if (genderData != null)
                {
                    bool isValid = optionsRepository.IsValidListOrder(genderData.ListOrder, PCISEnum.Options.Gender, agencyID, genderData.GenderID);
                    if (isValid)
                    {
                        var genderUpdate = this.genderRepository.GetGender(genderData.GenderID).Result;
                        if (genderUpdate.GenderID != 0 && genderUpdate != null)
                        {
                            var gender = new GenderDTO
                            {
                                GenderID = genderData.GenderID,
                                Name = genderData.Name,
                                Abbrev = genderData.Abbrev,
                                Description = genderData.Description,
                                ListOrder = genderData.ListOrder,
                                UpdateDate = DateTime.UtcNow,
                                UpdateUserID = userID,
                                AgencyID = agencyID
                            };

                            var addressResponse = this.genderRepository.UpdateGender(gender);
                            if (addressResponse != null)
                            {
                                response.ResponseStatusCode = PCISEnum.StatusCodes.UpdationSuccess;
                                response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationSuccess);
                            }
                            else
                            {
                                response.ResponseStatusCode = PCISEnum.StatusCodes.UpdationFailed;
                                response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationFailed);
                            }
                        }
                        else
                        {
                            response.ResponseStatusCode = PCISEnum.StatusCodes.UpdationFailed;
                            response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationFailed);
                        }
                    }
                    else
                    {
                        response.ResponseStatusCode = PCISEnum.StatusCodes.InvalidListOrder;
                        response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.InvalidListOrder);
                    }
                }
                else
                {
                    response.ResponseStatusCode = PCISEnum.StatusCodes.UpdationFailed;
                    response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationFailed);
                }

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// GetIdentificationTypeList.
        /// </summary>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <returns></returns>
        public IdentificationTypesResponseDTO GetIdentificationTypeList(int pageNumber, int pageSize, long agencyID)
        {
            try
            {
                IdentificationTypesResponseDTO identificationTypeResponseDTO = new IdentificationTypesResponseDTO();
                if (pageNumber <= 0)
                {
                    identificationTypeResponseDTO.identificationTypes = null;
                    identificationTypeResponseDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PageNumber));
                    identificationTypeResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return identificationTypeResponseDTO;
                }
                else if (pageSize <= 0)
                {
                    identificationTypeResponseDTO.identificationTypes = null;
                    identificationTypeResponseDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PageSize));
                    identificationTypeResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return identificationTypeResponseDTO;
                }
                else
                {
                    var response = this.identificationTypeRepository.GetIdentificationTypeList(pageNumber, pageSize, agencyID);
                    var count = this.identificationTypeRepository.GetIdentificationTypeCount(agencyID);
                    List<IdentificationTypeDTO> identificationTypesList = new List<IdentificationTypeDTO>();

                    this.mapper.Map<List<IdentificationType>, List<IdentificationTypeDTO>>(response, identificationTypesList);

                    identificationTypeResponseDTO.identificationTypes = identificationTypesList;
                    identificationTypeResponseDTO.TotalCount = count;
                    identificationTypeResponseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                    identificationTypeResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                    return identificationTypeResponseDTO;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// AddIdentificationType.
        /// </summary>
        /// <param name="identificationTypeData">identificationTypeData.</param>
        /// <returns>CRUDResponseDTO</returns>
        public CRUDResponseDTO AddIdentificationType(IdentificationTypeDTO identificationTypeData, int userID, long agencyID)
        {
            try
            {
                Int64? IdentificationTypeID;
                if (identificationTypeData != null)
                {
                    bool isValid = optionsRepository.IsValidListOrder(identificationTypeData.ListOrder, PCISEnum.Options.IdentificationType, agencyID);
                    if (isValid)
                    {
                        var identificationTypeDTO = new IdentificationTypeDTO
                        {
                            Name = identificationTypeData.Name,
                            Abbrev = identificationTypeData.Abbrev,
                            Description = identificationTypeData.Description,
                            ListOrder = identificationTypeData.ListOrder,
                            IsRemoved = false,
                            UpdateDate = DateTime.UtcNow,
                            UpdateUserID = userID,
                            AgencyID = agencyID
                        };

                        IdentificationType identificationType = new IdentificationType();
                        this.mapper.Map<IdentificationTypeDTO, IdentificationType>(identificationTypeDTO, identificationType);

                        IdentificationTypeID = this.identificationTypeRepository.AddIdentificationType(identificationType).IdentificationTypeID;
                        if (IdentificationTypeID != 0)
                        {
                            response.ResponseStatusCode = PCISEnum.StatusCodes.InsertionSuccess;
                            response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.InsertionSuccess);
                        }

                    }
                    else
                    {
                        response.ResponseStatusCode = PCISEnum.StatusCodes.InvalidListOrder;
                        response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.InvalidListOrder);
                    }
                }
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// UpdateIdentificationType.
        /// </summary>
        /// <param name="identificationTypeData">identificationTypeData.</param>
        /// <returns>CRUDResponseDTO.</returns>
        public CRUDResponseDTO UpdateIdentificationType(IdentificationTypeDTO identificationTypeData, int userID, long agencyID)
        {
            try
            {
                if (identificationTypeData != null)
                {
                    bool isValid = optionsRepository.IsValidListOrder(identificationTypeData.ListOrder, PCISEnum.Options.IdentificationType, agencyID, identificationTypeData.IdentificationTypeID);
                    if (isValid)
                    {
                        var identificationTypeUpdate = this.identificationTypeRepository.GetIdentificationType(identificationTypeData.IdentificationTypeID).Result;
                        if (identificationTypeUpdate.IdentificationTypeID != 0 && identificationTypeUpdate != null)
                        {
                            var identificationTypeDTO = new IdentificationTypeDTO
                            {
                                IdentificationTypeID = identificationTypeData.IdentificationTypeID,
                                Name = identificationTypeData.Name,
                                Abbrev = identificationTypeData.Abbrev,
                                Description = identificationTypeData.Description,
                                ListOrder = identificationTypeData.ListOrder,
                                UpdateDate = DateTime.UtcNow,
                                IsRemoved = false,
                                UpdateUserID = userID,
                                AgencyID = agencyID
                            };

                            IdentificationType identificationType = new IdentificationType();
                            this.mapper.Map<IdentificationTypeDTO, IdentificationType>(identificationTypeDTO, identificationType);
                            var addressResponse = this.identificationTypeRepository.UpdateIdentificationType(identificationType);
                            if (addressResponse != null)
                            {
                                response.ResponseStatusCode = PCISEnum.StatusCodes.UpdationSuccess;
                                response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationSuccess);
                            }
                            else
                            {
                                response.ResponseStatusCode = PCISEnum.StatusCodes.UpdationFailed;
                                response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationFailed);
                            }
                        }
                        else
                        {
                            response.ResponseStatusCode = PCISEnum.StatusCodes.UpdationFailed;
                            response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationFailed);
                        }
                    }
                    else
                    {
                        response.ResponseStatusCode = PCISEnum.StatusCodes.InvalidListOrder;
                        response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.InvalidListOrder);
                    }
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
        /// DeleteIdentificationType.
        /// </summary>
        /// <param name="identificationTypeID">identificationTypeID.</param>
        /// <returns>CRUDResponseDTO.</returns>
        public CRUDResponseDTO DeleteIdentificationType(Int64 identificationTypeID, long agencyID)
        {
            try
            {
                var identificationType = this.identificationTypeRepository.GetIdentificationType(identificationTypeID).Result;
                if (identificationType?.AgencyID == agencyID)
                {
                    if (identificationType != null && identificationType.IdentificationTypeID != 0)
                    {
                        var identificationTypeUsedCount = this.identificationTypeRepository.GetIdentificationTypeUsedByID(identificationTypeID);
                        if (identificationTypeUsedCount == 0)
                        {
                            identificationType.IsRemoved = true;
                            identificationType.UpdateDate = DateTime.UtcNow;
                            var languageResponse = this.identificationTypeRepository.UpdateIdentificationType(identificationType);
                            if (languageResponse != null)
                            {
                                response.ResponseStatusCode = PCISEnum.StatusCodes.DeletionSuccess;
                                response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.DeletionSuccess);
                            }
                            else
                            {
                                response.ResponseStatusCode = PCISEnum.StatusCodes.DeletionFailed;
                                response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.DeletionFailed);
                            }
                        }
                        else
                        {
                            response.ResponseStatusCode = PCISEnum.StatusCodes.DeleteRecordInUse;
                            response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.DeleteRecordInUse);
                        }
                    }
                    else
                    {
                        response.ResponseStatusCode = PCISEnum.StatusCodes.DeletionFailed;
                        response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.DeletionFailed);
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
        /// UpdateSexuality.
        /// </summary>
        /// <param name="EditSexualityDTO,updateUserID,AgencyID">genderData.</param>
        /// <returns>CRUDResponseDTO.</returns>
        public CRUDResponseDTO UpdateSexuality(EditSexualityDTO editSexualityDTO, int updateUserID, long AgencyID)
        {
            try
            {
                if (editSexualityDTO.SexualityID != 0)
                {
                    bool isValid = optionsRepository.IsValidListOrder(editSexualityDTO.ListOrder, PCISEnum.Options.Sexuality, AgencyID, editSexualityDTO.SexualityID);
                    if (isValid)
                    {
                        Sexuality sexuality = this.Sexualityrepository.GetSexuality(editSexualityDTO.SexualityID).Result;
                        if (sexuality.SexualityID != 0 && sexuality != null)
                        {
                            sexuality.AgencyID = AgencyID;
                            sexuality.UpdateUserID = updateUserID;
                            sexuality.Name = editSexualityDTO.Name;
                            sexuality.Abbrev = editSexualityDTO.Abbrev;
                            sexuality.Description = editSexualityDTO.Description;
                            sexuality.ListOrder = editSexualityDTO.ListOrder;
                            sexuality.UpdateDate = DateTime.UtcNow;

                            var result = this.Sexualityrepository.UpdateSexuality(sexuality);
                            if (result != null)
                            {
                                response.ResponseStatusCode = PCISEnum.StatusCodes.UpdationSuccess;
                                response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationSuccess);
                            }
                            else
                            {
                                response.ResponseStatusCode = PCISEnum.StatusCodes.UpdationFailed;
                                response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationFailed);
                            }
                        }
                    }
                    else
                    {
                        response.ResponseStatusCode = PCISEnum.StatusCodes.InvalidListOrder;
                        response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.InvalidListOrder);
                    }
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
        /// RemoveSexuality.
        /// </summary>
        /// <param name="SexualityID">SexualityID.</param>
        /// <returns>CRUDResponseDTO.</returns>
        public CRUDResponseDTO RemoveSexuality(int SexualityID, long agencyID)
        {
            try
            {
                if (SexualityID != 0)
                {
                    var count = this.Sexualityrepository.GetSexualityCount(SexualityID);
                    if (count == 0)
                    {
                        Sexuality sexuality = this.Sexualityrepository.GetSexuality(SexualityID).Result;
                        if (sexuality?.AgencyID == agencyID)
                        {
                            if (sexuality.SexualityID != 0 && sexuality != null)
                            {
                                sexuality.IsRemoved = true;
                                sexuality.UpdateDate = DateTime.UtcNow;

                                var result = this.Sexualityrepository.UpdateSexuality(sexuality);
                                if (result != null)
                                {
                                    response.ResponseStatusCode = PCISEnum.StatusCodes.DeletionSuccess;
                                    response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.DeletionSuccess);
                                }
                            }
                        }
                        else
                        {
                            response.ResponseStatusCode = PCISEnum.StatusCodes.DeletionFailed;
                            response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.DeletionFailed);
                        }
                    }
                    else
                    {
                        response.ResponseStatusCode = PCISEnum.StatusCodes.DeleteRecordInUse;
                        response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.DeleteRecordInUse);
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
        /// GetRaceEthnicityList.
        /// </summary>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <returns></returns>
        public RaceEthnicityResponseDTO GetRaceEthnicityList(int pageNumber, int pageSize, long agencyID)
        {
            try
            {
                RaceEthnicityResponseDTO raceEthnicityResponseDTO = new RaceEthnicityResponseDTO();
                if (pageNumber <= 0)
                {
                    raceEthnicityResponseDTO.RaceEthnicities = null;
                    raceEthnicityResponseDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PageNumber));
                    raceEthnicityResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return raceEthnicityResponseDTO;
                }
                else if (pageSize <= 0)
                {
                    raceEthnicityResponseDTO.RaceEthnicities = null;
                    raceEthnicityResponseDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PageSize));
                    raceEthnicityResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return raceEthnicityResponseDTO;
                }
                else
                {
                    var response = this.raceEthnicityRepository.GetRaceEthnicityList(pageNumber, pageSize, agencyID);
                    var count = this.raceEthnicityRepository.GetRaceEthnicityCount(agencyID);
                    List<RaceEthnicityDTO> raceEthnicitysList = new List<RaceEthnicityDTO>();

                    this.mapper.Map<List<RaceEthnicity>, List<RaceEthnicityDTO>>(response, raceEthnicitysList);

                    raceEthnicityResponseDTO.RaceEthnicities = raceEthnicitysList;
                    raceEthnicityResponseDTO.TotalCount = count;
                    raceEthnicityResponseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                    raceEthnicityResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                    return raceEthnicityResponseDTO;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// AddRaceEthnicity.
        /// </summary>
        /// <param name="raceEthnicityData">raceEthnicityData.</param>
        /// <returns>CRUDResponseDTO</returns>
        public CRUDResponseDTO AddRaceEthnicity(RaceEthnicityDTO raceEthnicityData, int userID, long agencyID)
        {
            try
            {
                Int64? RaceEthnicityID;
                if (raceEthnicityData != null)
                {
                    bool isValid = optionsRepository.IsValidListOrder(raceEthnicityData.ListOrder, PCISEnum.Options.RaceEthnicity, agencyID);
                    if (isValid)
                    {
                        var raceEthnicityDTO = new RaceEthnicityDTO
                        {
                            Name = raceEthnicityData.Name,
                            Abbrev = raceEthnicityData.Abbrev,
                            Description = raceEthnicityData.Description,
                            ListOrder = raceEthnicityData.ListOrder,
                            IsRemoved = false,
                            UpdateDate = DateTime.UtcNow,
                            UpdateUserID = userID,
                            AgencyID = agencyID
                        };

                        RaceEthnicity raceEthnicity = new RaceEthnicity();
                        this.mapper.Map<RaceEthnicityDTO, RaceEthnicity>(raceEthnicityDTO, raceEthnicity);

                        RaceEthnicityID = this.raceEthnicityRepository.AddRaceEthnicity(raceEthnicity).RaceEthnicityID;
                        if (RaceEthnicityID != 0)
                        {
                            response.ResponseStatusCode = PCISEnum.StatusCodes.InsertionSuccess;
                            response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.InsertionSuccess);
                        }
                    }
                    else
                    {
                        response.ResponseStatusCode = PCISEnum.StatusCodes.InvalidListOrder;
                        response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.InvalidListOrder);
                    }
                }
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// UpdateRaceEthnicity.
        /// </summary>
        /// <param name="raceEthnicityData">raceEthnicityData.</param>
        /// <returns>CRUDResponseDTO.</returns>
        public CRUDResponseDTO UpdateRaceEthnicity(RaceEthnicityDTO raceEthnicityData, int userID, long agencyID)
        {
            try
            {
                if (raceEthnicityData != null)
                {
                    bool isValid = optionsRepository.IsValidListOrder(raceEthnicityData.ListOrder, PCISEnum.Options.RaceEthnicity, agencyID, raceEthnicityData.RaceEthnicityID);
                    if (isValid)
                    {
                        var raceEthnicityUpdate = this.raceEthnicityRepository.GetRaceEthnicity(raceEthnicityData.RaceEthnicityID).Result;
                        if (raceEthnicityUpdate.RaceEthnicityID != 0 && raceEthnicityUpdate != null)
                        {
                            var raceEthnicityDTO = new RaceEthnicityDTO
                            {
                                RaceEthnicityID = raceEthnicityData.RaceEthnicityID,
                                Name = raceEthnicityData.Name,
                                Abbrev = raceEthnicityData.Abbrev,
                                Description = raceEthnicityData.Description,
                                ListOrder = raceEthnicityData.ListOrder,
                                UpdateDate = DateTime.UtcNow,
                                IsRemoved = false,
                                UpdateUserID = userID,
                                AgencyID = agencyID
                            };

                            RaceEthnicity raceEthnicity = new RaceEthnicity();
                            this.mapper.Map<RaceEthnicityDTO, RaceEthnicity>(raceEthnicityDTO, raceEthnicity);
                            var addressResponse = this.raceEthnicityRepository.UpdateRaceEthnicity(raceEthnicity);
                            if (addressResponse != null)
                            {
                                response.ResponseStatusCode = PCISEnum.StatusCodes.UpdationSuccess;
                                response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationSuccess);
                            }
                            else
                            {
                                response.ResponseStatusCode = PCISEnum.StatusCodes.UpdationFailed;
                                response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationFailed);
                            }
                        }
                        else
                        {
                            response.ResponseStatusCode = PCISEnum.StatusCodes.UpdationFailed;
                            response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationFailed);
                        }
                    }
                    else
                    {
                        response.ResponseStatusCode = PCISEnum.StatusCodes.InvalidListOrder;
                        response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.InvalidListOrder);
                    }
                }
                else
                {
                    response.ResponseStatusCode = PCISEnum.StatusCodes.UpdationFailed;
                    response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationFailed);
                }

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// DeleteRaceEthnicity.
        /// </summary>
        /// <param name="raceEthnicityID">raceEthnicityID.</param>
        /// <returns>CRUDResponseDTO.</returns>
        public CRUDResponseDTO DeleteRaceEthnicity(Int64 raceEthnicityID, long agencyID)
        {
            try
            {
                var raceEthnicity = this.raceEthnicityRepository.GetRaceEthnicity(raceEthnicityID).Result;
                if (raceEthnicity?.AgencyID == agencyID)
                {
                    if (raceEthnicity != null && raceEthnicity.RaceEthnicityID != 0)
                    {
                        var raceEthnicityUsedCount = this.raceEthnicityRepository.GetRaceEthnicityUsedByID(raceEthnicityID);
                        if (raceEthnicityUsedCount == 0)
                        {
                            raceEthnicity.IsRemoved = true;
                            raceEthnicity.UpdateDate = DateTime.UtcNow;
                            var languageResponse = this.raceEthnicityRepository.UpdateRaceEthnicity(raceEthnicity);
                            if (languageResponse != null)
                            {
                                response.ResponseStatusCode = PCISEnum.StatusCodes.DeletionSuccess;
                                response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.DeletionSuccess);
                            }
                            else
                            {
                                response.ResponseStatusCode = PCISEnum.StatusCodes.DeletionFailed;
                                response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.DeletionFailed);
                            }
                        }
                        else
                        {
                            response.ResponseStatusCode = PCISEnum.StatusCodes.DeleteRecordInUse;
                            response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.DeleteRecordInUse);
                        }
                    }
                    else
                    {
                        response.ResponseStatusCode = PCISEnum.StatusCodes.DeletionFailed;
                        response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.DeletionFailed);
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
        /// GetSupportTypeList.
        /// </summary>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <returns></returns>
        public SupportTypeResponseDTO GetSupportTypeList(int pageNumber, int pageSize, long agencyID)
        {
            try
            {
                SupportTypeResponseDTO supportTypeResponseDTO = new SupportTypeResponseDTO();
                if (pageNumber <= 0)
                {
                    supportTypeResponseDTO.SupportTypes = null;
                    supportTypeResponseDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PageNumber));
                    supportTypeResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return supportTypeResponseDTO;
                }
                else if (pageSize <= 0)
                {
                    supportTypeResponseDTO.SupportTypes = null;
                    supportTypeResponseDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PageSize));
                    supportTypeResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return supportTypeResponseDTO;
                }
                else
                {
                    var response = this.supportTypeRepository.GetSupportTypeList(pageNumber, pageSize, agencyID);
                    var count = this.supportTypeRepository.GetSupportTypeCount(agencyID);
                    List<SupportTypeDTO> supportTypesList = new List<SupportTypeDTO>();

                    this.mapper.Map<List<SupportType>, List<SupportTypeDTO>>(response, supportTypesList);

                    supportTypeResponseDTO.SupportTypes = supportTypesList;
                    supportTypeResponseDTO.TotalCount = count;
                    supportTypeResponseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                    supportTypeResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                    return supportTypeResponseDTO;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// AddSupportType.
        /// </summary>
        /// <param name="supportTypeData">supportTypeData.</param>
        /// <returns>CRUDResponseDTO</returns>
        public CRUDResponseDTO AddSupportType(SupportTypeDTO supportTypeData, int userID, long agencyID)
        {
            try
            {
                Int64? SupportTypeID;
                if (supportTypeData != null)
                {
                    bool isValid = optionsRepository.IsValidListOrder(supportTypeData.ListOrder, PCISEnum.Options.SupportType, agencyID);
                    if (isValid)
                    {
                        var supportTypeDTO = new SupportTypeDTO
                        {
                            Name = supportTypeData.Name,
                            Abbrev = supportTypeData.Abbrev,
                            Description = supportTypeData.Description,
                            ListOrder = supportTypeData.ListOrder,
                            IsRemoved = false,
                            UpdateDate = DateTime.UtcNow,
                            UpdateUserID = userID,
                            AgencyID = agencyID
                        };

                        SupportType supportType = new SupportType();
                        this.mapper.Map<SupportTypeDTO, SupportType>(supportTypeDTO, supportType);
                        SupportTypeID = this.supportTypeRepository.AddSupportType(supportType).SupportTypeID;
                        if (SupportTypeID != 0)
                        {
                            response.ResponseStatusCode = PCISEnum.StatusCodes.InsertionSuccess;
                            response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.InsertionSuccess);
                        }
                    }
                    else
                    {
                        response.ResponseStatusCode = PCISEnum.StatusCodes.InvalidListOrder;
                        response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.InvalidListOrder);
                    }
                }
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// UpdateSupportType.
        /// </summary>
        /// <param name="supportTypeData">supportTypeData.</param>
        /// <returns>CRUDResponseDTO.</returns>
        public CRUDResponseDTO UpdateSupportType(SupportTypeDTO supportTypeData, int userID, long agencyID)
        {
            try
            {
                if (supportTypeData != null)
                {
                    bool isValid = optionsRepository.IsValidListOrder(supportTypeData.ListOrder, PCISEnum.Options.SupportType, agencyID, supportTypeData.SupportTypeID);
                    if (isValid)
                    {
                        var supportTypeUpdate = this.supportTypeRepository.GetSupportType(supportTypeData.SupportTypeID).Result;
                        if (supportTypeUpdate.SupportTypeID != 0 && supportTypeUpdate != null)
                        {
                            var supportTypeDTO = new SupportTypeDTO
                            {
                                SupportTypeID = supportTypeData.SupportTypeID,
                                Name = supportTypeData.Name,
                                Abbrev = supportTypeData.Abbrev,
                                Description = supportTypeData.Description,
                                ListOrder = supportTypeData.ListOrder,
                                UpdateDate = DateTime.UtcNow,
                                IsRemoved = false,
                                UpdateUserID = userID,
                                AgencyID = agencyID
                            };

                            SupportType supportType = new SupportType();
                            this.mapper.Map<SupportTypeDTO, SupportType>(supportTypeDTO, supportType);
                            var addressResponse = this.supportTypeRepository.UpdateSupportType(supportType);
                            if (addressResponse != null)
                            {
                                response.ResponseStatusCode = PCISEnum.StatusCodes.UpdationSuccess;
                                response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationSuccess);
                            }
                            else
                            {
                                response.ResponseStatusCode = PCISEnum.StatusCodes.UpdationFailed;
                                response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationFailed);
                            }
                        }
                        else
                        {
                            response.ResponseStatusCode = PCISEnum.StatusCodes.UpdationFailed;
                            response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationFailed);
                        }
                    }
                    else
                    {
                        response.ResponseStatusCode = PCISEnum.StatusCodes.InvalidListOrder;
                        response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.InvalidListOrder);
                    }
                }
                else
                {
                    response.ResponseStatusCode = PCISEnum.StatusCodes.UpdationFailed;
                    response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationFailed);
                }

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// DeleteSupportType.
        /// </summary>
        /// <param name="supportTypeID">supportTypeID.</param>
        /// <returns>CRUDResponseDTO.</returns>
        public CRUDResponseDTO DeleteSupportType(Int64 supportTypeID, long agencyID)
        {
            try
            {
                var supportType = this.supportTypeRepository.GetSupportType(supportTypeID).Result;
                if (supportType?.AgencyID == agencyID)
                {
                    if (supportType != null && supportType.SupportTypeID != 0)
                    {
                        var supportTypeUsedCount = this.supportTypeRepository.GetSupportTypeUsedByID(supportTypeID);
                        if (supportTypeUsedCount == 0)
                        {
                            supportType.IsRemoved = true;
                            supportType.UpdateDate = DateTime.UtcNow;
                            var languageResponse = this.supportTypeRepository.UpdateSupportType(supportType);
                            if (languageResponse != null)
                            {
                                response.ResponseStatusCode = PCISEnum.StatusCodes.DeletionSuccess;
                                response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.DeletionSuccess);
                            }
                            else
                            {
                                response.ResponseStatusCode = PCISEnum.StatusCodes.DeletionFailed;
                                response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.DeletionFailed);
                            }
                        }
                        else
                        {
                            response.ResponseStatusCode = PCISEnum.StatusCodes.DeleteRecordInUse;
                            response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.DeleteRecordInUse);
                        }
                    }
                    else
                    {
                        response.ResponseStatusCode = PCISEnum.StatusCodes.DeletionFailed;
                        response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.DeletionFailed);
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
        /// AddSexuality
        /// </summary>
        /// <param name="sexualityData"></param>
        /// <param name="agencyID"></param>
        /// <param name="updateUserID"></param>
        /// <returns>CRUDResponseDTO</returns>
        public CRUDResponseDTO AddSexuality(SexualityInputDTO sexualityData, long agencyID, int updateUserID)
        {
            try
            {
                Sexuality sexuality = new Sexuality();
                if (sexualityData != null)
                {
                    bool isValid = optionsRepository.IsValidListOrder(sexualityData.ListOrder, PCISEnum.Options.Sexuality, agencyID);
                    if (isValid)
                    {
                        var sexualityDTO = new SexualityDTO
                        {
                            Name = sexualityData.Name,
                            Abbrev = sexualityData.Abbrev,
                            Description = sexualityData.Description,
                            ListOrder = sexualityData.ListOrder,
                            IsRemoved = false,
                            UpdateDate = DateTime.UtcNow,
                            UpdateUserID = updateUserID,
                            AgencyID = agencyID
                        };
                        this.mapper.Map<SexualityDTO, Sexuality>(sexualityDTO, sexuality);
                        var Sexuality = this.Sexualityrepository.AddSexuality(sexuality);
                        if (Sexuality != null && Sexuality.SexualityID > 0)
                        {
                            response.ResponseStatusCode = PCISEnum.StatusCodes.InsertionSuccess;
                            response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.InsertionSuccess);
                        }
                        else
                        {
                            response.ResponseStatusCode = PCISEnum.StatusCodes.insertionFailed;
                            response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.insertionFailed);
                        }
                    }
                    else
                    {
                        response.ResponseStatusCode = PCISEnum.StatusCodes.InvalidListOrder;
                        response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.InvalidListOrder);
                    }
                }
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SexualityResponseDTO GetSexualityList(int pageNumber, int pageSize, long agencyID)
        {
            try
            {
                SexualityResponseDTO sexualityResponseDTO = new SexualityResponseDTO();
                if (pageNumber <= 0)
                {
                    sexualityResponseDTO.Sexualities = null;
                    sexualityResponseDTO.ResponseStatus = string.Format(PCISEnum.StatusMessages.RequiredParameterMissing, PCISEnum.Parameters.PageNumber);
                    sexualityResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return sexualityResponseDTO;
                }
                else if (pageSize <= 0)
                {
                    sexualityResponseDTO.Sexualities = null;
                    sexualityResponseDTO.ResponseStatus = string.Format(PCISEnum.StatusMessages.RequiredParameterMissing, PCISEnum.Parameters.PageSize);
                    sexualityResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return sexualityResponseDTO;
                }
                else
                {
                    var response = this.Sexualityrepository.GetSexualityList(pageNumber, pageSize, agencyID);
                    var count = this.Sexualityrepository.GetAgencySexualityCount(agencyID);
                    List<SexualityDTO> sexualitiesList = new List<SexualityDTO>();

                    this.mapper.Map<List<Sexuality>, List<SexualityDTO>>(response, sexualitiesList);

                    sexualityResponseDTO.Sexualities = sexualitiesList;
                    sexualityResponseDTO.TotalCount = count;
                    sexualityResponseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                    sexualityResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                    return sexualityResponseDTO;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// AddIdentifiedGender.
        /// </summary>
        /// <param name="identifiedGenderData">identifiedGenderData.</param>
        /// <param name="userID">userID.</param>
        /// <param name="agencyID">agencyID.</param>
        /// <returns></returns>
        public CRUDResponseDTO AddIdentifiedGender(IdentifiedGenderDTO identifiedGenderData, int userID, long agencyID)
        {
            try
            {
                Int64? IdentifiedGenderID;
                if (identifiedGenderData != null)
                {
                    bool isValid = optionsRepository.IsValidListOrder(identifiedGenderData.ListOrder, PCISEnum.Options.IdentifiedGender, agencyID);
                    if (isValid)
                    {
                        var identifiedGenderDTO = new IdentifiedGenderDTO
                        {
                            Name = identifiedGenderData.Name,
                            Abbrev = identifiedGenderData.Abbrev,
                            Description = identifiedGenderData.Description,
                            ListOrder = identifiedGenderData.ListOrder,
                            IsRemoved = false,
                            UpdateDate = DateTime.UtcNow,
                            UpdateUserID = userID,
                            AgencyID = agencyID
                        };

                        IdentifiedGender identifiedGender = new IdentifiedGender();
                        this.mapper.Map<IdentifiedGenderDTO, IdentifiedGender>(identifiedGenderDTO, identifiedGender);

                        IdentifiedGenderID = this.identifiedGenderRepository.AddIdentifiedGender(identifiedGender).IdentifiedGenderID;
                        if (IdentifiedGenderID != 0)
                        {
                            response.ResponseStatusCode = PCISEnum.StatusCodes.InsertionSuccess;
                            response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.InsertionSuccess);
                        }
                    }
                    else
                    {
                        response.ResponseStatusCode = PCISEnum.StatusCodes.InvalidListOrder;
                        response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.InvalidListOrder);
                    }
                }
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// UpdateIdentifiedGender.
        /// </summary>
        /// <param name="identifiedGenderData">identifiedGenderData.</param>
        /// <param name="userID">userID.</param>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>CRUDResponseDTO.</returns>
        public CRUDResponseDTO UpdateIdentifiedGender(IdentifiedGenderDTO identifiedGenderData, int userID, long agencyID)
        {
            try
            {
                if (identifiedGenderData != null)
                {
                    bool isValid = optionsRepository.IsValidListOrder(identifiedGenderData.ListOrder, PCISEnum.Options.IdentifiedGender, agencyID, identifiedGenderData.IdentifiedGenderID);
                    if (isValid)
                    {
                        IdentifiedGender identifiedGenderUpdate = this.identifiedGenderRepository.GetIdentifiedGender(identifiedGenderData.IdentifiedGenderID).Result;
                        if (identifiedGenderUpdate.IdentifiedGenderID != 0 && identifiedGenderUpdate != null)
                        {
                            identifiedGenderUpdate.Name = identifiedGenderData.Name;
                            identifiedGenderUpdate.Abbrev = identifiedGenderData.Abbrev;
                            identifiedGenderUpdate.Description = identifiedGenderData.Description;
                            identifiedGenderUpdate.ListOrder = identifiedGenderData.ListOrder;
                            identifiedGenderUpdate.UpdateDate = DateTime.UtcNow;
                            identifiedGenderUpdate.UpdateUserID = userID;
                            identifiedGenderUpdate.AgencyID = agencyID;

                            var responseIdentifiedGender = this.identifiedGenderRepository.UpdateIdentifiedGender(identifiedGenderUpdate);
                            if (responseIdentifiedGender != null)
                            {
                                this.response.ResponseStatusCode = PCISEnum.StatusCodes.UpdationSuccess;
                                this.response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationSuccess);
                            }
                            else
                            {
                                this.response.ResponseStatusCode = PCISEnum.StatusCodes.UpdationFailed;
                                this.response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationFailed);
                            }
                        }
                        else
                        {
                            response.ResponseStatusCode = PCISEnum.StatusCodes.UpdationFailed;
                            response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationFailed);
                        }
                    }
                    else
                    {
                        response.ResponseStatusCode = PCISEnum.StatusCodes.InvalidListOrder;
                        response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.InvalidListOrder);
                    }
                }
                else
                {
                    response.ResponseStatusCode = PCISEnum.StatusCodes.UpdationFailed;
                    response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.UpdationFailed);
                }

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// GetIdentifiedGenderList.
        /// </summary>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>IdentifiedGenderResponseDTO.</returns>
        public IdentifiedGenderResponseDTO GetIdentifiedGenderList(int pageNumber, int pageSize, long agencyID)
        {
            try
            {
                IdentifiedGenderResponseDTO identifiedGenderResponseDTO = new IdentifiedGenderResponseDTO();
                if (pageNumber <= 0)
                {
                    identifiedGenderResponseDTO.IdentifiedGender = null;
                    identifiedGenderResponseDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PageNumber));
                    identifiedGenderResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return identifiedGenderResponseDTO;
                }
                else if (pageSize <= 0)
                {
                    identifiedGenderResponseDTO.IdentifiedGender = null;
                    identifiedGenderResponseDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PageSize));
                    identifiedGenderResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return identifiedGenderResponseDTO;
                }
                else
                {
                    List<IdentifiedGender> response = this.identifiedGenderRepository.GetIdentifiedGenderList(pageNumber, pageSize, agencyID);
                    var count = this.identifiedGenderRepository.GetIdentifiedGenderCount(agencyID);
                    List<IdentifiedGenderDTO> identifiedGenderList = new List<IdentifiedGenderDTO>();
                    this.mapper.Map<List<IdentifiedGender>, List<IdentifiedGenderDTO>>(response, identifiedGenderList);
                    identifiedGenderResponseDTO.IdentifiedGender = identifiedGenderList;
                    identifiedGenderResponseDTO.TotalCount = count;
                    identifiedGenderResponseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                    identifiedGenderResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                    return identifiedGenderResponseDTO;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Delete an Identified Gender.
        /// </summary>
        /// <param name="identifiedGenderID">identifiedGenderID.</param>
        /// <returns>CRUDResponseDTO.</returns>
        public CRUDResponseDTO DeleteIdentifiedGender(int identifiedGenderID, long agencyID)
        {
            try
            {
                var identifiedGender = this.identifiedGenderRepository.GetIdentifiedGender(identifiedGenderID).Result;
                if (identifiedGender?.AgencyID == agencyID)
                {
                    if (identifiedGender != null)
                    {
                        var personCount = this.identifiedGenderRepository.GetPeopleCountByIdentifiedGenderID(identifiedGenderID);
                        if (personCount == 0)
                        {
                            identifiedGender.IsRemoved = true;
                            identifiedGender.UpdateDate = DateTime.UtcNow;
                            var identifiedGenderResponse = this.identifiedGenderRepository.UpdateIdentifiedGender(identifiedGender);
                            if (identifiedGenderResponse != null)
                            {
                                response.ResponseStatusCode = PCISEnum.StatusCodes.DeletionSuccess;
                                response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.DeletionSuccess);
                            }
                            else
                            {
                                response.ResponseStatusCode = PCISEnum.StatusCodes.DeletionFailed;
                                response.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.DeletionFailed);
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
    }
}
