// -----------------------------------------------------------------------
// <copyright file="ITenantProvider.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System.Threading.Tasks;

namespace Opeeka.PICS.Domain.Interfaces.Providers.Contract
{
    public interface ITenantProvider
    {
        Task<long> GetCurrentTenant();
    }
}