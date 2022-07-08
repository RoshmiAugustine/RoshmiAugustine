// -----------------------------------------------------------------------
// <copyright file="INotificationLogRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    /// <summary>
    /// INotificationLogRepository.
    /// </summary>
    public interface INotifyRiskRepository : IAsyncRepository<NotifyRisk>
    {
        NotifyRisk AddNotifyRisk(NotifyRisk notifyRisk);
        int GetNotifyRiskCount(int assessmentID);
    }
}
