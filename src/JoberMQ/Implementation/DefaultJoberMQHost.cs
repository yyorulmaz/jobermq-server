using System.Threading.Tasks;
using JoberMQ.Abstraction;
using JoberMQ.Abstraction.Configuration;
using JoberMQ.Constants;
using JoberMQ.Factories;
using JoberMQ.Factories.Configuration;

namespace JoberMQ.Implementation
{
    internal class DefaultJoberMQHost : IJoberMQHost
    {
        public DefaultJoberMQHost(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        readonly IConfiguration configuration;
        IConfiguration IJoberMQHost.Configuration => configuration;

        public async Task StartAsync()
        {
            var joberMQ = JoberMQFactory.Create(JoberMQConst.JoberMQFactory, configuration == null ? ConfigurationFactory.Create(ConfigurationConst.ConfigurationFactory) : configuration);
            await joberMQ.StartAsync();
        }
    }
}
