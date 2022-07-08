// -----------------------------------------------------------------------
// <copyright file="BaseService.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Infrastructure.Enums;

namespace Opeeka.PICS.Domain.Services
{
    public class BaseService
    {
        /// Initializes a new instance of the configurationRepository class.
        private readonly IConfigurationRepository configurationRepository;

        private readonly IHttpContextAccessor httpContextAccessor;
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseService"/> class.
        /// </summary>
        /// <param name="configRepository">configRepository.</param>
        public BaseService(IConfigurationRepository configRepository, IHttpContextAccessor httpContext)
        {
            this.configurationRepository = configRepository;
            this.httpContextAccessor = httpContext;
        }

        public string GetTimeZoneFromHeader()
        {
            string offset = "0";
            if (httpContextAccessor.HttpContext.Request.Headers.ContainsKey(PCISEnum.TokenHeaders.timeZone))
            {
                offset = httpContextAccessor.HttpContext.Request.Headers[PCISEnum.TokenHeaders.timeZone].ToString();
            }
            return offset;
        }

        /// <summary>
        /// Get Configuration List
        /// </summary>
        /// <param name="agencyID"></param>
        /// <returns></returns>
        public List<ConfigurationParameterDTO> GetConfigurationList(long agencyID = 0)
        {
            try
            {
                List<ConfigurationParameterDTO> response = new List<ConfigurationParameterDTO>();
                response = this.configurationRepository.GetConfigurationList(agencyID);
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get Configuration List
        /// </summary>
        /// <param name="agencyID"></param>
        /// <returns></returns>
        public ConfigurationParameterDTO GetConfigurationByName(string name, long agencyID = 0)
        {
            try
            {
                ConfigurationParameterDTO response = new ConfigurationParameterDTO();
                response = this.configurationRepository.GetConfigurationByName(name, agencyID);
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// To get RoleName from UserRole.
        /// </summary>
        /// <param name="userRole"></param>
        /// <returns></returns>
        public string GetRoleName(List<string> userRole)
        {
            try
            {
                if (userRole.Contains(PCISEnum.Roles.SuperAdmin))
                {
                    return PCISEnum.Roles.SuperAdmin;
                }
                else if (userRole.Contains(PCISEnum.Roles.OrgAdminRW) || userRole.Contains(PCISEnum.Roles.OrgAdminRO))
                {
                    return PCISEnum.Roles.OrgAdminRW;
                }
                else if (userRole.Contains(PCISEnum.Roles.Supervisor))
                {
                    return PCISEnum.Roles.Supervisor;
                }
                else if (userRole.Contains(PCISEnum.Roles.HelperRW) || userRole.Contains(PCISEnum.Roles.HelperRO))
                {
                    return PCISEnum.Roles.HelperRW;
                }
                else if (userRole.Contains(PCISEnum.Roles.Assessor))
                {
                    return PCISEnum.Roles.Assessor;
                }
                return string.Empty;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
