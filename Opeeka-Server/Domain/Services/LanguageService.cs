// -----------------------------------------------------------------------
// <copyright file="LanguageService.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using Microsoft.AspNetCore.Http;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.DTO.Output;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Domain.Interfaces.Services;
using Opeeka.PICS.Domain.Resources;
using Opeeka.PICS.Infrastructure.Enums;

namespace Opeeka.PICS.Domain.Services
{
    public class LanguageService : BaseService, ILanguageService
    {
        private ILanguageRepository languageRepository;
        private IPersonRepository personRepository;
        private IOptionsRepository optionsRepository;
        CRUDResponseDTO response = new CRUDResponseDTO();
        /// Initializes a new instance of the LocalizeService/> class.
        private readonly LocalizeService localize;
        /// <summary>
        /// Initializes a new instance of the <see cref="LanguageService"/> class.
        /// </summary>
        /// <param name="languageRepository"></param>
        public LanguageService(ILanguageRepository languageRepository, IPersonRepository personRepository, LocalizeService localizeService, IConfigurationRepository configRepo, IHttpContextAccessor httpContext, IOptionsRepository optionsRepository)
            : base(configRepo, httpContext)
        {
            this.languageRepository = languageRepository;
            this.personRepository = personRepository;
            this.optionsRepository = optionsRepository;
            this.localize = localizeService;
        }

        /// <summary>
        /// Add Language
        /// </summary>
        /// <param name="languageData"></param>
        /// <returns>Object of CRUDResponseDTO</returns>
        public CRUDResponseDTO AddLanguage(LanguageDetailsDTO languageData, int userID, long agencyID)
        {
            try
            {
                int? LanguageID;
                if (languageData != null)
                {
                    bool isValid = optionsRepository.IsValidListOrder(languageData.ListOrder, PCISEnum.Options.Language, agencyID);
                    if (isValid)
                    {
                        var language = new LanguageDTO
                        {
                            Name = languageData.Name,
                            Abbrev = languageData.Abbrev,
                            Description = languageData.Description,
                            ListOrder = languageData.ListOrder,
                            IsRemoved = false,
                            UpdateDate = DateTime.UtcNow,
                            UpdateUserID = userID,
                            AgencyID = agencyID
                        };

                        LanguageID = this.languageRepository.AddLanguage(language).LanguageID;
                        if (LanguageID != 0)
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
        /// UpdateLanguage
        /// </summary>
        /// <param name="languageData"></param>
        /// <returns>CRUDResponseDTO</returns>
        public CRUDResponseDTO UpdateLanguage(LanguageDetailsDTO languageData, int userID, long agencyID)
        {
            try
            {
                if (languageData != null)
                {
                    bool isValid = optionsRepository.IsValidListOrder(languageData.ListOrder, PCISEnum.Options.Language, agencyID, languageData.LanguageID);
                    if (isValid)
                    {
                        var language = new LanguageDTO
                        {
                            LanguageID = languageData.LanguageID,
                            Name = languageData.Name,
                            Abbrev = languageData.Abbrev,
                            Description = languageData.Description,
                            ListOrder = languageData.ListOrder,
                            UpdateDate = DateTime.UtcNow,
                            UpdateUserID = userID,
                            AgencyID = agencyID
                        };

                        var addressResponse = this.languageRepository.UpdateLanguage(language);
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
        /// DeleteLanguage
        /// </summary>
        /// <param name="languageID"></param>
        /// <returns>CRUDResponseDTO</returns>
        public CRUDResponseDTO DeleteLanguage(int languageID, long agencyID)
        {
            try
            {
                var language = this.languageRepository.GetLanguage(languageID).Result;
                if (language.AgencyID == agencyID)
                {
                    if (language != null)
                    {
                        var personCount = this.personRepository.GetPersonByLanguageCount(languageID);
                        if (personCount == 0)
                        {
                            language.IsRemoved = true;
                            language.UpdateDate = DateTime.UtcNow;
                            var languageResponse = this.languageRepository.UpdateLanguage(language);
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

        public LanguageListResponseDTO GetLanguageList(int pageNumber, int pageSize, long agencyID)
        {
            try
            {
                LanguageListResponseDTO LanguageListDTO = new LanguageListResponseDTO();
                if (pageNumber <= 0)
                {
                    LanguageListDTO.LanguageList = null;
                    LanguageListDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PageNumber));
                    LanguageListDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return LanguageListDTO;
                }
                else if (pageSize <= 0)
                {
                    LanguageListDTO.LanguageList = null;
                    LanguageListDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PageSize));
                    LanguageListDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return LanguageListDTO;
                }
                else
                {
                    var response = this.languageRepository.GetLanguageList(pageNumber, pageSize, agencyID);
                    var count = this.languageRepository.GetLanguageCount(agencyID);
                    LanguageListDTO.LanguageList = response;
                    LanguageListDTO.TotalCount = count;
                    LanguageListDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                    LanguageListDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                    return LanguageListDTO;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
