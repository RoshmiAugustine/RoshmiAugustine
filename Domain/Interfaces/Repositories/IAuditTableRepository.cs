// -----------------------------------------------------------------------
// <copyright file="IAgencyRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using System.Collections.Generic;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    /// <summary>
    /// IAgencyRepository.
    /// </summary>
    public interface IAuditTableRepository : IAsyncRepository<AuditTableName>
    {
        List<AuditTableNameDTO> GetAuditableTableField(string tableName);
    }
}
