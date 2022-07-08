// -----------------------------------------------------------------------
// <copyright file="INotifiationResolutionNoteRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    /// <summary>
    /// INotifiationResolutionNoteRepository.
    /// </summary>
    public interface INotifiationResolutionNoteRepository : IAsyncRepository<NotificationResolutionNote>
    {
        /// <summary>
        /// AddNotifiationResolutionNote.
        /// </summary>
        /// <param name="notificationResolutionNote">notificationResolutionNote.</param>
        /// <returns>NotificationResolutionNote.</returns>
        NotificationResolutionNote AddNotificationResolutionNote(NotificationResolutionNote notificationResolutionNote);
    }
}
