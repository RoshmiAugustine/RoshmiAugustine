// -----------------------------------------------------------------------
// <copyright file="MockNotificationTypeRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Moq;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Opeeka.PICS.UnitTests.Domain.Mocks.Repositories
{
    public class MockNotificationTypeRepository : Mock<INotificationTypeRepository>
    {
        public MockNotificationTypeRepository MockGetAllNotificationType(List<NotificationType> result)
        {
            Setup(x => x.GetAllNotificationType()).Returns(result);
            return this;
        }

        public MockNotificationTypeRepository MockGetAllNotificationTypeException()
        {
            Setup(x => x.GetAllNotificationType()).Throws<Exception>();
            return this;
        }

        public MockNotificationTypeRepository MockGetNotificationType(NotificationType result)
        {
            Setup(x => x.GetRowAsync(It.IsAny<Expression<Func<NotificationType, bool>>>()))
                 .Returns(Task.FromResult(result));
            Setup(x => x.GetNotificationType(It.IsAny<string>()))
                .Returns(Task.FromResult(result));

            return this;
        }
        // public MockNotificationTypeRepository MockGetNotificationTypeByName(NotificationType result)
        // {

        //     return this;
        // }
        public MockNotificationTypeRepository MockGetNotificationTypeException()
        {
            Setup(x => x.GetRowAsync(It.IsAny<Expression<Func<NotificationType, bool>>>())).Throws<Exception>();

            return this;
        }
    }
}
