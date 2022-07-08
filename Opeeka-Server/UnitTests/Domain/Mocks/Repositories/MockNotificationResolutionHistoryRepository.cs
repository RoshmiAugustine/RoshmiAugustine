
using Moq;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;

namespace Opeeka.PICS.UnitTests.Domain.Mocks.Repositories
{
    public class MockNotificationResolutionHistoryRepository : Mock<INotifiationResolutionHistoryRepository>
    {
        public MockNotificationResolutionHistoryRepository MockAddNotificationResolutionHistory(NotificationResolutionHistory notificationResolutionHistory)
        {
            Setup(x => x.AddNotificationResolutionHistory(It.IsAny<NotificationResolutionHistory>()))
                .Returns(notificationResolutionHistory);
            return this;
        }
    }
}
