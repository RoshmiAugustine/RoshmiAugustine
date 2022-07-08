// -----------------------------------------------------------------------
// <copyright file="TenantProvider.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Opeeka.PICS.Domain.Interfaces.Providers.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Infrastructure.Enums;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using System.Threading.Tasks;

namespace Opeeka.PICS.Domain.Providers.Implementations
{
    public class TenantProvider : ITenantProvider
    {
        private readonly TenantMapping _tenants;
        private readonly IHttpContextAccessor _accessor;
        private readonly IAgencyRepository agencyRepository;
        public TenantProvider(IConfiguration configuration, IHttpContextAccessor accessor, IAgencyRepository agencyRepository)
        {
            this._tenants = configuration.GetTenantMapping();
            _accessor = accessor;
            this.agencyRepository = agencyRepository;
        }
        //  public DatabaseTenantProvider(TenantMapping tenants)
        // {
        //     this._tenants = tenants;
        // }


        public async Task<long> GetCurrentTenant()
        {
            string agencyAbbrev = _accessor.HttpContext.Request.Headers[PCISEnum.Parameters.tenantId].ToString();
            var agency = await agencyRepository.GetAgencyDetailsByAbbrev(agencyAbbrev);
            return agency.AgencyID;
        }

    }

}
