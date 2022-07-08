// -----------------------------------------------------------------------
// <copyright file="SearchService.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.DTO.Input;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Domain.Interfaces.Services;
using Opeeka.PICS.Domain.Resources;
using Opeeka.PICS.Infrastructure.Enums;

namespace Opeeka.PICS.Domain.Services
{
    public class SearchService : BaseService, ISearchService
    {
        /// Initializes a new instance of the searchRepository/> class.
        private readonly ISearchRepository searchRepository;

        /// Initializes a new instance of the LocalizeService/> class.
        private readonly LocalizeService localize;

        private readonly IPersonRepository personRepository;
        /// <summary>
        /// SearchService.
        /// </summary>
        /// <param name="searchRepository">searchRepository.</param>
        /// <param name="configRepo">configRepo.</param>
        /// <param name="httpContext">httpContext.</param>
        public SearchService(ISearchRepository searchRepository, IConfigurationRepository configRepo, IHttpContextAccessor httpContext, LocalizeService localizeService,IPersonRepository personRepository) : base(configRepo, httpContext)
        {
            this.searchRepository = searchRepository;
            this.localize = localizeService;
            this.personRepository = personRepository;
        }

        /// <summary>
        /// UpperPaneSearch.
        /// </summary>
        /// <param name="helperID">helperID.</param>
        /// <param name="agencyID">agencyID.</param>
        /// <param name="searchKey">searchKey</param>
        /// <param name="pageNo">pageNo.</param>
        /// <param name="pageSize">pageSize.</param>
        /// <param name="roles">roles.</param>
        /// <returns>UpperPaneSearchResponseDTO.</returns>
        public UpperpaneSearchResponseDTO GetUpperpaneSearchResults(UpperpaneSearchKeyDTO upperpaneSearchKeyDTO, long agencyID, List<string> roles, int helperID)
        {
            try
            {
                UpperpaneSearchResponseDTO upperPaneSearchResponseDTO = new UpperpaneSearchResponseDTO();
                List<UpperpaneSearchDTO> response = new List<UpperpaneSearchDTO>();
                if (upperpaneSearchKeyDTO.searchKey != string.Empty)
                {
                    var roleName = this.GetRoleName(roles);
                    var sharedPersonIDs = this.personRepository.GetSharedPersonIDs(agencyID, roleName);
                    response = this.searchRepository.GetUpperpaneSearchResults(upperpaneSearchKeyDTO, roleName, agencyID, helperID, sharedPersonIDs); 

                    upperPaneSearchResponseDTO.UpperpaneSearchList = response;
                    upperPaneSearchResponseDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                    upperPaneSearchResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;

                    return upperPaneSearchResponseDTO;
                }
                else
                {
                    upperPaneSearchResponseDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.AgencyIndex));
                    upperPaneSearchResponseDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return upperPaneSearchResponseDTO;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        
    }
}
