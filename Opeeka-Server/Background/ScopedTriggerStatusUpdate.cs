using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Azure.Storage.Queues;
using Microsoft.Extensions.Configuration;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Infrastructure.Enums;
using System.Collections.Generic;
using System.Linq;

namespace Background
{
    internal interface IScopedTriggerStatusUpdate
    {
        Task DoWork(CancellationToken stoppingToken);
    }
    public class ScopedTriggerStatusUpdate : IScopedTriggerStatusUpdate
    {
        private readonly ILogger<ScopedTriggerStatusUpdate> _logger;
        private QueueClient _queueClient { get; set; }
        private static string QueuName = "statusupdate";
        private readonly IPersonRepository personRepository;
        private readonly IBackgroundProcessLogRepository backgroundProcessLogRepository;

        public ScopedTriggerStatusUpdate(ILogger<ScopedTriggerStatusUpdate> logger, IConfiguration configuration, IPersonRepository personRepository, IBackgroundProcessLogRepository backgroundProcessLogRepository)
        {
            _logger = logger;
            _queueClient = new QueueClient(configuration.GetValue<string>("queuestoragekey"), ScopedTriggerStatusUpdate.QueuName);
            this.personRepository = personRepository;
            this.backgroundProcessLogRepository = backgroundProcessLogRepository;
        }

        public async Task DoWork(CancellationToken stoppingToken)
        {
            var processLog = backgroundProcessLogRepository.GetBackgroundProcessLog("TriggerStatusUpdate");
            if (processLog == null)
            {
                processLog = new BackgroundProcessLog()
                {
                    ProcessName = "TriggerStatusUpdate",
                    LastProcessedDate = DateTime.UtcNow.Date.AddDays(-1)
                };
                backgroundProcessLogRepository.AddBackgroundProcessLog(processLog);
            }
            else
            {
                if(processLog.LastProcessedDate != DateTime.Now.Date)
                {
                    await ProcessTriggerStatusUpdateAsync();
                    processLog.LastProcessedDate = DateTime.UtcNow.Date;
                    var result = backgroundProcessLogRepository.UpdateBackgroundProcessLog(processLog);
                }
          
            }
        }

        private async Task<bool> ProcessTriggerStatusUpdateAsync()
        {
            bool processCompleted;
            try
            {
                int limit = PCISEnum.Limits.StatusLimit;
                int count = PCISEnum.Limits.StatusCount;
                List<long> personIDs = this.personRepository.GetActivePersons();
                if (personIDs != null && personIDs.Count > 0)
                {
                    count = personIDs.Count();
                    for (int i = 0; count > 0 && i <= count / limit; i++)
                    {
                        List<long> idList = new List<long>();
                        idList = personIDs.Skip(i * limit).Take(limit).ToList();
                        List<long> activeCollaboration = new List<long>();
                        if (idList != null && idList.Count > 0)
                        {
                            foreach (var item in idList)
                            {
                                //var personCollaborations = this.personRepository.GetActivePersonsCollaboration(item);
                              
                                //foreach (var personCollaboration in personCollaborations)
                                //{
                                //    if (!personCollaboration.EndDate.HasValue || personCollaboration.EndDate >= DateTime.Now.Date)
                                //    {
                                //        activeCollaboration.Add(personCollaboration.PersonID);
                                //        break;
                                //    }
                                //    else
                                //    {
                                //        continue;
                                //    }
                                //}
                            }
                            var removedIdList= idList.Except(activeCollaboration).ToList();
                            var updatePersonIDList = personRepository.GetAsync(x => removedIdList.Contains(x.PersonID)).Result.ToList();
                                if (updatePersonIDList != null && updatePersonIDList.Count > 0)
                                {
                                    foreach (Person person in updatePersonIDList)
                                    {
                                        person.IsActive = false;
                                        person.UpdateDate = DateTime.UtcNow;
                                    }
                                    var result = this.personRepository.UpdateBulkPersons(updatePersonIDList);
                                }
                        }
                    }
                }
                processCompleted = true;
            }
            catch (Exception e)
            {
                processCompleted = false;
            }
            return processCompleted;
        }
    }
}
