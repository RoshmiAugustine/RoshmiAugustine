// -----------------------------------------------------------------------
// <copyright file="ConfigurationService.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.Extensions.Logging;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;

namespace Opeeka.PICS.Domain.Services
{
    public class ConfigurationService : IConfigurationService
    {
        /// Initializes a new instance of the personRepository/> class.
        private readonly IConfigurationRepository configurationRepository;

        /// Initializes a new instance of the <see cref="logger"/> class.
        private readonly ILogger<ConfigurationService> logger;

        public ConfigurationService(IConfigurationRepository configurationRepository, ILogger<ConfigurationService> logger)
        {
            this.configurationRepository = configurationRepository;
            this.logger = logger;
        }

        /// <summary>
        /// Get Configuration List
        /// </summary>
        /// <param name="agencyID"></param>
        /// <returns></returns>
        public List<ConfigurationParameterDTO> GetConfigurationList(int agencyID = 0)
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
        /// Get Configuration By Name
        /// </summary>
        /// <param name="name"></param>
        /// <param name="agencyID"></param>
        /// <returns></returns>
        public ConfigurationParameterDTO GetConfigurationByName(string name, int agencyID = 0)
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
    }
}
