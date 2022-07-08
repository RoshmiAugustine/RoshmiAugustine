// -----------------------------------------------------------------------
// <copyright file="SystemRoleService.cs" company="Naicoits">
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
    public class SystemRoleService : BaseService, ISystemRoleService
    {
        private ISystemRoleRepository systemRoleRepository;
        CRUDResponseDTO response = new CRUDResponseDTO();
        /// Initializes a new instance of the LocalizeService/> class.
        private readonly LocalizeService localize;

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemRoleService"/> class.
        /// </summary>
        /// <param name="systemRoleRepository"></param>
        public SystemRoleService(ISystemRoleRepository systemRoleRepository, LocalizeService localizeService,
            IConfigurationRepository configRepo,
            IHttpContextAccessor httpContext) : base(configRepo, httpContext)
        {
            this.systemRoleRepository = systemRoleRepository;
            this.localize = localizeService;
        }

        /// <summary>
        /// Get System Role List
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns>SystemRoleListDTO</returns>
        public SystemRoleListResponseDTO GetSystemRoleList(int pageNumber, int pageSize)
        {
            try
            {
                SystemRoleListResponseDTO SystemRoleListDTO = new SystemRoleListResponseDTO();
                if (pageNumber <= 0)
                {
                    SystemRoleListDTO.SystemRoleList = null;
                    SystemRoleListDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PageNumber));
                    SystemRoleListDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return SystemRoleListDTO;
                }
                else if (pageSize <= 0)
                {
                    SystemRoleListDTO.SystemRoleList = null;
                    SystemRoleListDTO.ResponseStatus = string.Format(this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.RequiredParameterMissing), this.localize.GetLocalizedHtmlString(PCISEnum.Parameters.PageSize));
                    SystemRoleListDTO.ResponseStatusCode = PCISEnum.StatusCodes.RequiredParameterMissing;
                    return SystemRoleListDTO;
                }
                else
                {
                    var response = this.systemRoleRepository.GetAllSystemRoles(pageNumber, pageSize);
                    var count = this.systemRoleRepository.GetSystemRoleCount();
                    SystemRoleListDTO.SystemRoleList = response;
                    SystemRoleListDTO.TotalCount = count;
                    SystemRoleListDTO.ResponseStatus = this.localize.GetLocalizedHtmlString(PCISEnum.StatusMessages.Success);
                    SystemRoleListDTO.ResponseStatusCode = PCISEnum.StatusCodes.Success;
                    return SystemRoleListDTO;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
