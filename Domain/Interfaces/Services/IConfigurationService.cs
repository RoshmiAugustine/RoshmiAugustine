// -----------------------------------------------------------------------
// <copyright file="IConfigurationService.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Opeeka.PICS.Domain.DTO;
using System.Collections.Generic;

namespace Opeeka.PICS.Domain.Interfaces.Services
{
    public interface IConfigurationService
    {
        /// <summary>
        /// GetConfigurationList
        /// </summary>
        /// <param name="tenantId">Optional</param>
        /// <returns>GetConfigurationsListDTO</returns>
        public List<ConfigurationParameterDTO> GetConfigurationList(int tenantId = 0);

        /// <summary>
        /// GetConfigurationByName
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="agencyID">Optional</param>
        /// <returns>GetConfigurationDTO</returns>
        public ConfigurationParameterDTO GetConfigurationByName(string Name, int agencyID = 0);
    }
}
