
using Moq;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Opeeka.PICS.UnitTests.Domain.Mocks.Repositories
{
    public class MockNotificationLogRepository : Mock<INotificationLogRepository>
    {
        public MockNotificationLogRepository MockUpdateNotificationLog(NotificationLogDTO notificationLogDTO, NotificationLog notificationLog)
        {
            Setup(x => x.GetNotificationLog(It.IsAny<int>()))
                .Returns(Task.FromResult(notificationLogDTO));
            Setup(x => x.UpdateNotificationLog(It.IsAny<NotificationLog>()))
                .Returns(notificationLog);
            return this;
        }

        public MockNotificationLogRepository MockUpdateNotificationLogException(NotificationLogDTO notificationLogDTO, NotificationLog notificationLog)
        {
            Setup(x => x.GetNotificationLog(It.IsAny<int>()))
                   .Returns(Task.FromResult(notificationLogDTO));
            Setup(x => x.UpdateNotificationLog(It.IsAny<NotificationLog>()))
                .Throws<Exception>();
            return this;
        }

        /// <summary>
        /// Mock Get Notification Log List
        /// </summary>
        /// <param name="result"></param>
        /// <returns>MockNotificationLogRepository</returns>
        public MockNotificationLogRepository MockGetNotificationLogList(List<NotificationLogDTO> result)
        {
            Setup(x => x.GetNotificationLogList(It.IsAny<NotificationLogSearchDTO>(), It.IsAny<DynamicQueryBuilderDTO>())).Returns(Tuple.Create(result, result.Count));
            return this;
        }

        /// <summary>
        /// Mock Get Notification Log List Exception
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public MockNotificationLogRepository MockGetNotificationLogListException(List<NotificationLogDTO> result)
        {
            Setup(x => x.GetNotificationLogList(It.IsAny<NotificationLogSearchDTO>(), It.IsAny<DynamicQueryBuilderDTO>())).Throws<Exception>();
            return this;
        }
        public MockNotificationLogRepository MockAddNotificationLog(NotificationLog notificationLog)
        {
            Setup(x => x.AddNotificationLog(It.IsAny<NotificationLog>()))
                .Returns(notificationLog);
            return this;
        }
        public MockNotificationLogRepository MockAddNotificationLogException(NotificationLog notificationLog)
        {
            Setup(x => x.AddNotificationLog(It.IsAny<NotificationLog>()))
                .Throws<Exception>();
            return this;
        }
        public MockNotificationLogRepository MockGetNotificationLog(NotificationLog result1, IReadOnlyList<NotificationLog> result2)
        {
            Setup(x => x.GetRowAsync(It.IsAny<Expression<Func<NotificationLog, bool>>>()))
                 .Returns(Task.FromResult(result1));

            MockGetAssessmentNotificationLog(result2);

            return this;
        }
        public MockNotificationLogRepository MockGetAssessmentNotificationLog(IReadOnlyList<NotificationLog> result)
        {
            Setup(x => x.GetAssessmentNotificationLog(It.IsAny<List<int>>(), It.IsAny<int>()))
                .Returns(result);
            return this;
        }
    }
}
