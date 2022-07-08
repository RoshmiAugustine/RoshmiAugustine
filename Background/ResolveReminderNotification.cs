using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace Background
{
    public class ResolveReminderNotification : BackgroundService
    {
        private readonly ILogger<ResolveReminderNotification> _logger;
        public IServiceProvider services { get; }

        public ResolveReminderNotification(ILogger<ResolveReminderNotification> logger, IServiceProvider services)
        {
            _logger = logger;
            this.services = services;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                await CallHostedService(stoppingToken);
            }
        }

        private async Task CallHostedService(CancellationToken stoppingToken)
        {
            using (var scope = services.CreateScope())
            {
                var scopedResolveReminderNotification =
                    scope.ServiceProvider
                        .GetRequiredService<IScopedResolveReminderNotification>();

                await scopedResolveReminderNotification.DoWork(stoppingToken);
            }
        }
    }
}

