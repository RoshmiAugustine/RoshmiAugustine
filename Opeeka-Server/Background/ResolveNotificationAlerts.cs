using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace Background
{
    public class ResolveNotificationAlerts : BackgroundService
    {

        private readonly ILogger<ResolveNotificationAlerts> _logger;
        public IServiceProvider services { get; }

        public ResolveNotificationAlerts(ILogger<ResolveNotificationAlerts> logger, IServiceProvider services)
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
                var scopedResolveNotificationAlerts =
                    scope.ServiceProvider
                        .GetRequiredService<IScopedResolveNotificationAlerts>();

                await scopedResolveNotificationAlerts.DoWork(stoppingToken);
            }
        }
    }
}
