// -----------------------------------------------------------------------
// <copyright file="AzureStorageQueue.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using Azure.Storage.Queues;
using Microsoft.Extensions.Configuration;
using Opeeka.PICS.Domain.Interfaces;

namespace Opeeka.PICS.Infrastructure.Queue
{
    public class AzureStorageQueue : IQueue
    {
        private readonly IConfiguration configuration;

        public AzureStorageQueue(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<bool> Push(string queueName, string value)
        {
            var queueClient = new QueueClient(configuration.GetValue<string>("queuestoragekey"), queueName);
            var result = await queueClient.SendMessageAsync(value);
            return result.Value.MessageId != "";
        }
    }
}
