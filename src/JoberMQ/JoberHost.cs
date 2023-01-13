using JoberMQ.Abstraction.Host;
using JoberMQ.Abstraction.Jober;
using JoberMQ.Common.Enums.Configuration;
using JoberMQ.Common.Enums.Host;
using JoberMQ.Configuration.Abstraction;
using JoberMQ.Configuration.Constants;
using JoberMQ.Configuration.Factories;
using JoberMQ.Factories.Host;

namespace JoberMQ
{
    public static class JoberHost
    {
        internal static IJober Jober { get; set; }

        public static IConfiguration CreateConfiguration()
            => ConfigurationFactory.CreateConfiguration(ConfigurationFactoryEnum.Default);

        public static IJoberHostBuilder CreateDefaultBuilder()
            => HostFactory.CreateJoberHostBuilder(HostFactoryEnum.Default);

        public static IJoberHostBuilder CreateDefaultBuilder(HostFactoryEnum hostFactory)
            => HostFactory.CreateJoberHostBuilder(hostFactory);
    }
}
