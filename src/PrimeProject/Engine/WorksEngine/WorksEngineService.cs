using System;
using System.Threading;
using System.Threading.Tasks;
using AddinEngine.Abstract;
using Hangfire;
using PrimeFinderService.Services;

namespace WorksEngine
{
    public class WorksEngineService : BackgroundService2
    {
        private readonly IServiceProvider _services;

        public WorksEngineService(IServiceProvider services)
        {
            _services = services;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            BackgroundJob.Schedule(() =>
                PrimeFinder.CacheSmallPrime(), TimeSpan.FromSeconds(10)
            );
            return base.StartAsync(cancellationToken);
        }
    }
}