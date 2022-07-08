// -----------------------------------------------------------------------
// <copyright file="ConfigurationExtentions.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.Extensions.Configuration;
using Opeeka.PICS.Domain.Entities;
public static class ConfigurationExtensions
{
    public static TenantMapping GetTenantMapping(this IConfiguration configuration)
    {
        return configuration.GetSection("Tenants").Get<TenantMapping>();
    }
}