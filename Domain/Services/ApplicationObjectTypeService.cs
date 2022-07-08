// -----------------------------------------------------------------------
// <copyright file="WeatherForecastService.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Domain.Interfaces.Services;

namespace Opeeka.PICS.Domain.Services
{
    public class ApplicationObjectTypeService : BaseService, IApplicationObjectTypeService
    {

        private IApplicationObjectTypeRepository applicationObjectTypeRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeatherForecastService"/> class.
        /// </summary>
        /// <param name="iweatherForecastRepository">returns instance of weather forecast.</param>
        public ApplicationObjectTypeService(IApplicationObjectTypeRepository applicationObjectTypeRepository, IConfigurationRepository configRepo, IHttpContextAccessor httpContext)
            : base(configRepo, httpContext)
        {
            this.applicationObjectTypeRepository = applicationObjectTypeRepository;
        }

        public async Task<List<ApplicationObjectTypeDTO>> GetApplicationObjectTypes()
        {
            var result = await applicationObjectTypeRepository.GetApplicationObjectTypes();
            return result;
        }
    }
}
