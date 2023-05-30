using JoberMQ;
using JoberMQ.Extensions;
using JoberMQ.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace JoberMQServer
{
    public class Worker : BackgroundService
    {
        //private readonly ILogger<Worker> _logger;

        //public Worker(ILogger<Worker> logger)
        //{
        //    _logger = logger;
        //}


        //IHubContext<JoberHub> hubContext;
        //public Worker(IHubContext<JoberHub> hubContext)
        //{
        //    this.hubContext = hubContext;
        //}

        IServiceProvider serviceProvider;
        public Worker(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var configuration = JoberHost.CreateConfiguration();
            configuration.IsOwinHost = false;

            var jober = JoberHost
                .CreateBuilder()
                .Configuration(configuration)
                .Build();

            await jober.StartAsync(Class1.serviceProvider.GetService<IHubContext<JoberHub>>());
        }
    }
}