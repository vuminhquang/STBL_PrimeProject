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
            // var primeFinder = new PrimeFinder(_services);
            //
            // primeFinder.SeedData(1000000);
            // BackgroundJob.Enqueue(() => Console.WriteLine("Hello, world!"));

        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            BackgroundJob.Schedule(() =>
                PrimeFinder.CacheSmallPrime(), TimeSpan.FromSeconds(10)
            );
            // BackgroundJob.Enqueue<PrimeFinder>(
            //     primeFinder => primeFinder.SeedData(int.MaxValue/2)
            // );
            // _proxyService.Start();
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            // _proxyService.Stop();
            return base.StopAsync(cancellationToken);
        }
    }
}