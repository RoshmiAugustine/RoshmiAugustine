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
    public interface IBackgroundProcessLogRepository : IAsyncRepository<BackgroundProcessLog>
    {
        BackgroundProcessLog GetBackgroundProcessLog(string processName);
        BackgroundProcessLog AddBackgroundProcessLog(BackgroundProcessLog backgroundProcessLog);
        BackgroundProcessLog UpdateBackgroundProcessLog(BackgroundProcessLog backgroundProcessLog);
    }
}
