using JoberMQ.Common.Enums.Configuration;
using JoberMQ.Extensions;

namespace JoberMQ.Server
{
    public class WorkerJoberMQ : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public WorkerJoberMQ(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            #region senaryo 1
            //var jober = JoberMQ.JoberHost
            //.CreateDefaultBuilder()
            //.Build();

            //jober.StartAsync(); 
            #endregion


            #region senaryo 2
            var configuration = JoberMQ.Factories.Configuration.ConfigurationFactory.CreateConfiguration(ConfigurationFactoryEnum.Default);

            var jober = JoberMQ.JoberHost
            .CreateDefaultBuilder()
            .Configuration(configuration)
            .Build();


            jober.StartAsync();
            #endregion
        }
    }
}