using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Interfaces.Repositories;

namespace Opeeka.PICS.UnitTests.Domain.Mocks.Repositories
{
    public class MockNotificationLevelRepository : Mock<INotificationLevelRepository>
    {



        public MockNotificationLevelRepository MockNotificationLevelList(List<NotificationLevelDTO> results)
        {
            Setup(x => x.GetNotificationLevelList(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<long>()))
                .Returns(results);
            Setup(x => x.GetNotificationLevelCount(1))
                .Returns(results.Count);
            return this;
        }

        public MockNotificationLevelRepository MockNotificationLevelListException(List<NotificationLevelDTO> results)
        {
            Setup(x => x.GetNotificationLevelList(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<long>()))
                .Throws<Exception>();
            Setup(x => x.GetNotificationLevelCount(1))
                .Returns(results.Count);
            return this;
        }

        public MockNotificationLevelRepository MockAddNotificationLevel(NotificationLevelDTO result)
        {
            Setup(x => x.AddNotificationLevel(It.IsAny<NotificationLevelDTO>()))
                .Returns(result);
            return this;
        }

        public MockNotificationLevelRepository MockAddNotificationLevelException(NotificationLevelDTO result)
        {
            Setup(x => x.AddNotificationLevel(It.IsAny<NotificationLevelDTO>()))
                 .Throws<Exception>();
            return this;
        }

        public MockNotificationLevelRepository MockUpdateNotificationLevel(NotificationLevelDTO result)
        {
            Setup(x => x.UpdateNotificationLevel(It.IsAny<NotificationLevelDTO>()))
               .Returns(result);
            Setup(x => x.GetNotificationLevel(It.IsAny<int>()))
               .Returns(Task.FromResult(result));
            return this;
        }

        public MockNotificationLevelRepository MockUpdateNotificationLevelException(NotificationLevelDTO result)
        {
            Setup(x => x.UpdateNotificationLevel(It.IsAny<NotificationLevelDTO>()))
                .Throws<Exception>();

            Setup(x => x.GetNotificationLevel(It.IsAny<int>()))
                 .Throws<Exception>();
            return this;
        }

        public MockNotificationLevelRepository MockdeleteNotificationLevel(NotificationLevelDTO result)
        {
            Setup(x => x.UpdateNotificationLevel(It.IsAny<NotificationLevelDTO>()))
               .Returns(result);

            Setup(x => x.GetNotificationLevel(It.IsAny<int>()))
                .Returns(Task.FromResult(result));
            return this;
        }

        public MockNotificationLevelRepository MockDeleteNotificationLevelException(NotificationLevelDTO result)
        {
            Setup(x => x.UpdateNotificationLevel(It.IsAny<NotificationLevelDTO>()))
                .Throws<Exception>();

            Setup(x => x.GetNotificationLevel(It.IsAny<int>()))
                 .Throws<Exception>();
            return this;
        }
    }
}
