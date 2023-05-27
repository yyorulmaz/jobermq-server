using JoberMQ;
using JoberMQ.Extensions;

namespace JoberMQServer
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var configuration = JoberHost.CreateConfiguration();
            // edit configuration

            var jober = JoberHost
                .CreateBuilder()
                .Configuration(configuration)
                .Build();

            jober.StartAsync();
        }
    }
}