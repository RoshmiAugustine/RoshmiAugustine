// -----------------------------------------------------------------------
// <copyright file="NotificationLogServiceUnitTest.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Moq;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Interfaces.Common;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Domain.Services;
using Opeeka.PICS.Infrastructure.Enums;
using Opeeka.PICS.UnitTests.Domain.Mocks.Common;
using Opeeka.PICS.UnitTests.Domain.Mocks.Repositories;
using System;
using System.Collections.Generic;
using Xunit;

namespace Opeeka.PICS.UnitTests.Domain.Services
{
    public class NotificationLogServiceUnitTest
    {
        /// Initializes a new instance of the NotificationLogRepository class.
        private Mock<INotificationLogRepository> mockNotificationLogRepository;

        private Mock<IConfigurationRepository> mockConfigRepository;
        private Mock<IHttpContextAccessor> httpContextAccessor;
        private NotificationLogService notificationLogService;
        private Mock<IQueryBuilder> querybuild;
        private Mock<IMapper> mapper; 
        private Mock<IUserRepository> mockUserRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AgencyService"/> class.
        /// </summary>
        public NotificationLogServiceUnitTest()
        {
            this.mockNotificationLogRepository = new Mock<INotificationLogRepository>();
            this.mockConfigRepository = new Mock<IConfigurationRepository>();
            this.httpContextAccessor = new Mock<IHttpContextAccessor>();
            var context = new DefaultHttpContext();
            context.Request.Headers[PCISEnum.TokenHeaders.timeZone] = "-330";
            this.httpContextAccessor.Setup(_ => _.HttpContext).Returns(context);
            this.querybuild = new MockQueryBuilder().SetupQueryBuilder(new DynamicQueryBuilderDTO());
            this.mapper = new Mock<IMapper>();
            this.mockUserRepository = new Mock<IUserRepository>();
        }

        /// <summary>
        /// Get System Role List Success
        /// </summary>
        [Fact]
        public void GetNotificationLogList_Success_ReturnsCorrectResult()
        {
            var mockNotificationLogs = GetMockNotificationLogs();
            this.mockNotificationLogRepository = new MockNotificationLogRepository().MockGetNotificationLogList(mockNotificationLogs);
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Success); this.querybuild = new MockQueryBuilder().SetupQueryBuilder(new DynamicQueryBuilderDTO()
            {
                OrderBy = " order by NH.NotificationDate desc",
                Page = 1,
                PageSize = 10,
                Paginate = " OFFSET 0 ROWS FETCH NEXT 5 ROWS ONLY"
            });
            this.notificationLogService = new NotificationLogService(this.mapper.Object, this.mockNotificationLogRepository.Object, localize.Object, mockConfigRepository.Object, httpContextAccessor.Object, querybuild.Object, this.mockUserRepository.Object);
            List<string> roles = new List<string>();
            roles.Add("Super Admin");
            var searchDTO = new NotificationLogSearchDTO()
            {
                helperID = 1,
                agencyID = 4,
                userRole = roles,
                SearchFilter = "{\"filters\":[],\"orderby\":{\"field\":\"notificationDate\",\"value\":\"desc\"},\"page\":1,\"size\":20}"
            };
            var result = this.notificationLogService.GetNotificationLogList(searchDTO);
            Assert.Equal(result.ResponseStatusCode, PCISEnum.StatusCodes.Success);
        }

        /// <summary>
        /// GetNotificationLogList Returns No Result
        /// </summary>
        [Fact]
        public void GetNotificationLogList_ReturnsNoResult()
        {
            var mockNotificationLogs = new List<NotificationLogDTO>();
            this.mockNotificationLogRepository = new MockNotificationLogRepository().MockGetNotificationLogList(mockNotificationLogs);
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Success);
            this.querybuild = new MockQueryBuilder().SetupQueryBuilder(new DynamicQueryBuilderDTO()
            {
                OrderBy = " order by NH.NotificationDate desc",
                Page = 1,
                PageSize = 10,
                Paginate = " OFFSET 0 ROWS FETCH NEXT 5 ROWS ONLY"
            });
            this.notificationLogService = new NotificationLogService(this.mapper.Object, this.mockNotificationLogRepository.Object, localize.Object, mockConfigRepository.Object, httpContextAccessor.Object, querybuild.Object, this.mockUserRepository.Object);
            List<string> roles = new List<string>();
            roles.Add("Super Admin");
            var searchDTO = new NotificationLogSearchDTO()
            {
                helperID = 1,
                agencyID = 1,
                userRole = roles,
                SearchFilter = "{\"filters\":[],\"orderby\":{\"field\":\"notificationDate\",\"value\":\"desc\"},\"page\":1,\"size\":20}",
            };
            var result = this.notificationLogService.GetNotificationLogList(searchDTO);
            Assert.Equal(0, result.TotalCount);
        }

        /// <summary>
        /// GetNotificationLogList with Invalid Parameter Result
        /// </summary>
        [Fact]
        public void GetNotificationLogList_Failure_InvalidParameterResult()
        {
            var mockNotificationLogs = new List<NotificationLogDTO>();
            this.mockNotificationLogRepository = new MockNotificationLogRepository().MockGetNotificationLogList(mockNotificationLogs);
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Failure);
            this.notificationLogService = new NotificationLogService(this.mapper.Object, this.mockNotificationLogRepository.Object, localize.Object, mockConfigRepository.Object, httpContextAccessor.Object, querybuild.Object, this.mockUserRepository.Object);
            List<string> roles = new List<string>();
            roles.Add("Super Admin");
            var searchDTO = new NotificationLogSearchDTO()
            {
                helperID = 1,
                agencyID = 1,
                userRole = roles,
                SearchFilter = "{\"filters\":[],\"orderby\":{\"field\":\"notificationDate\",\"value\":\"desc\"},\"page\":1,\"size\":20}",
            };
            var result = this.notificationLogService.GetNotificationLogList(searchDTO);
            Assert.Equal(0, result.TotalCount);
            Assert.Null(result.NotificationLog);
        }

        /// <summary>
        /// GetNotificationLogs Exception Result
        /// </summary>
        [Fact]
        public void GetNotificationLogs_Failure_ExceptionResult()
        {
            var mockNotificationLogs = new List<NotificationLogDTO>();
            this.mockNotificationLogRepository = new MockNotificationLogRepository().MockGetNotificationLogListException(mockNotificationLogs);
            var localize = new MockLocalize().Localize(PCISEnum.StatusMessages.Failure);
            this.querybuild = new MockQueryBuilder().SetupQueryBuilder(new DynamicQueryBuilderDTO()
            {
                OrderBy = " order by NH.NotificationDate desc",
                Page = 1,
                PageSize = 10,
                Paginate = " OFFSET 0 ROWS FETCH NEXT 5 ROWS ONLY"
            });
            this.notificationLogService = new NotificationLogService(this.mapper.Object, this.mockNotificationLogRepository.Object, localize.Object, mockConfigRepository.Object, httpContextAccessor.Object, querybuild.Object, this.mockUserRepository.Object);
            List<string> roles = new List<string>();
            roles.Add("Super Admin");
            var searchDTO = new NotificationLogSearchDTO()
            {
                helperID = 1,
                agencyID = 1,
                userRole = roles,
                SearchFilter = "{\"filters\":[],\"orderby\":{\"field\":\"notificationDate\",\"value\":\"desc\"},\"page\":1,\"size\":20}",
            };
            Assert.ThrowsAny<Exception>(() => this.notificationLogService.GetNotificationLogList(searchDTO));
        }

        private List<NotificationLogDTO> GetMockNotificationLogs()
        {
            return new List<NotificationLogDTO>()
            {
                new NotificationLogDTO()
                {
                  NotificationLogID = 0,
                  NotificationDate= DateTime.UtcNow,
                  PersonID= 1,
                  NotificationTypeID= 0,
                  FKeyValue= 0,
                  NotificationData= "string",
                  NotificationResolutionStatusID= 1,
                  StatusDate= DateTime.UtcNow,
                  IsRemoved= true,
                  UpdateDate= DateTime.UtcNow,
                  UpdateUserID= 0
                },
                new NotificationLogDTO()
                {
                  NotificationLogID = 1,
                  NotificationDate= DateTime.UtcNow,
                  PersonID= 0,
                  NotificationTypeID= 0,
                  FKeyValue= 0,
                  NotificationData= "sfd",
                  NotificationResolutionStatusID= 1,
                  StatusDate= DateTime.UtcNow,
                  IsRemoved= true,
                  UpdateDate= DateTime.UtcNow,
                  UpdateUserID= 0
                },
                new NotificationLogDTO()
                {
                  NotificationLogID = 2,
                  NotificationDate= DateTime.UtcNow,
                  PersonID= 0,
                  NotificationTypeID= 0,
                  FKeyValue= 0,
                  NotificationData= "vbv",
                  NotificationResolutionStatusID= 1,
                  StatusDate= DateTime.UtcNow,
                  IsRemoved= true,
                  UpdateDate= DateTime.UtcNow,
                  UpdateUserID= 0
                }
            };
        }
    }
}
