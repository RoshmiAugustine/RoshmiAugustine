
using Moq;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Opeeka.PICS.UnitTests.Domain.Mocks.Repositories
{
    public class MockNotificationResolutionStatusRepository : Mock<INotifiationResolutionStatusRepository>
    {
        public MockNotificationResolutionStatusRepository MockGetNotificationStatus(NotificationResolutionStatus notificationResolutionStatus)
        {
            Setup(x => x.GetNotificationStatus(It.IsAny<string>()))
                .Returns(notificationResolutionStatus);
            Setup(x => x.GetRowAsync(It.IsAny<Expression<Func<NotificationResolutionStatus, bool>>>()))
                 .Returns(Task.FromResult(notificationResolutionStatus));
            return this;
        }
        // public MockNotificationResolutionStatusRepository MockGetResolutionStatusId(NotificationResolutionStatus result)
        // {
        //     Setup(x => x.GetRowAsync(It.IsAny<Expression<Func<NotificationResolutionStatus, bool>>>()))
        //          .Returns(Task.FromResult(result));

        //     return this;
        // }
    }
}
