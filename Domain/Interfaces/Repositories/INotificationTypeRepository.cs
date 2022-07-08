// -----------------------------------------------------------------------
// <copyright file="INotificationTypeRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Opeeka.PICS.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    public interface INotificationTypeRepository : IAsyncRepository<NotificationType>
    {
        /// <summary>
        /// GetAllNotificationType
        /// </summary>
        /// <returns>NotificationType</returns>
        List<NotificationType> GetAllNotificationType();
        Task<NotificationType> GetNotificationType(string name);
    }
}
