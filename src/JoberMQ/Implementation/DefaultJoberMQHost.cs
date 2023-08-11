using System.Threading.Tasks;
using JoberMQ.Abstraction;
using JoberMQ.Abstraction.Configuration;
using JoberMQ.Constants;
using JoberMQ.Factories;
using JoberMQ.Factories.Configuration;
using JoberMQ.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace JoberMQ.Implementation
{
    internal class DefaultJoberMQHost : IJoberMQHost
    {
        public DefaultJoberMQHost(JoberMQ.Abstraction.Configuration.IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        readonly JoberMQ.Abstraction.Configuration.IConfiguration configuration;
        JoberMQ.Abstraction.Configuration.IConfiguration IJoberMQHost.Configuration => configuration;

        public async Task StartAsync(IHubContext<JoberHub> hubContext = null)
        {
            var joberMQ = JoberMQFactory.Create(JoberMQConst.JoberMQFactory, configuration == null ? ConfigurationFactory.Create(ConfigurationConst.ConfigurationFactory) : configuration);
            await joberMQ.StartAsync(configuration.IsOwinHost, hubContext);
        }
    }
}
