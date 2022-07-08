using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace Background
{
    class TriggerStatusUpdate : BackgroundService
    {
        private readonly ILogger<TriggerStatusUpdate> _logger;
        public IServiceProvider services { get; }

        public TriggerStatusUpdate(ILogger<TriggerStatusUpdate> logger, IServiceProvider services)
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
                var scopedTriggerStatusUpdate =
                    scope.ServiceProvider
                        .GetRequiredService<IScopedTriggerStatusUpdate>();

                await scopedTriggerStatusUpdate.DoWork(stoppingToken);
            }
        }
    }
}
