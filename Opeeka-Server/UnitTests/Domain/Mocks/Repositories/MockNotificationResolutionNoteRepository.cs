
using Moq;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using System;

namespace Opeeka.PICS.UnitTests.Domain.Mocks.Repositories
{
    public class MockNotificationResolutionNoteRepository : Mock<INotifiationResolutionNoteRepository>
    {
        public MockNotificationResolutionNoteRepository MockAddNotificationResolutionNote(NotificationResolutionNote notificationResolutionNote)
        {
            Setup(x => x.AddNotificationResolutionNote(It.IsAny<NotificationResolutionNote>()))
                .Returns(notificationResolutionNote);
            return this;
        }

        public MockNotificationResolutionNoteRepository MockAddNotificationResolutionNoteException(NotificationResolutionNote notificationResolutionNote)
        {
            Setup(x => x.AddNotificationResolutionNote(It.IsAny<NotificationResolutionNote>()))
                .Throws<Exception>();
            return this;
        }
    }
}
