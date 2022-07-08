using System;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Infrastructure.Data.Repositories.Base;

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{
    public class NotifiationResolutionNoteRepository : BaseRepository<NotificationResolutionNote>, INotifiationResolutionNoteRepository
    {
        private readonly IMapper mapper;
        private readonly ILogger<NotifiationResolutionNoteRepository> logger;
        private readonly OpeekaDBContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotifiationResolutionNoteRepository"/> class.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public NotifiationResolutionNoteRepository(ILogger<NotifiationResolutionNoteRepository> logger, OpeekaDBContext dbContext, IMapper mapper)
            : base(dbContext)
        {
            this.mapper = mapper;
            this.logger = logger;
            this._dbContext = dbContext;
        }

        /// <summary>
        /// AddNotifiationResolutionNote.
        /// </summary>
        /// <param name="notificationResolutionNote">notificationResolutionNote.</param>
        /// <returns>NotificationResolutionNote.</returns>
        public NotificationResolutionNote AddNotificationResolutionNote(NotificationResolutionNote notificationResolutionNote)
        {
            try
            {
                var result = this.AddAsync(notificationResolutionNote).Result;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
