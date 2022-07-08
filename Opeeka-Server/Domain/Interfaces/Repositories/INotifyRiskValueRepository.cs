// -----------------------------------------------------------------------
// <copyright file="INotificationLogRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Opeeka.PICS.Domain.Entities;
using System.Collections.Generic;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    /// <summary>
    /// INotificationLogRepository.
    /// </summary>
    public interface INotifyRiskValueRepository : IAsyncRepository<NotifyRiskValue>
    {
        void AddBulkNotifyRiskValue(List<NotifyRiskValue> notifyRisk);
    }
}
