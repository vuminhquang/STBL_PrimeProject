using System;
using System.Threading;
using System.Threading.Tasks;
using AddinEngine.Abstract;

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