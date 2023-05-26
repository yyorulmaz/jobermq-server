using JoberMQ.Common.Enums.Configuration;
using JoberMQ.Abstraction;
using JoberMQ.Abstraction.Configuration;
using JoberMQ.Constants;
using JoberMQ.Factories.Configuration;

namespace JoberMQ
{
    public class JoberHost
    {
        internal static IJoberMQ JoberMQ { get; set; }
        internal static bool IsJoberActive { get; set; }

        public static IConfiguration CreateConfiguration(ConfigurationFactoryEnum configurationFactory = ConfigurationConst.ConfigurationFactory)
            => ConfigurationFactory.Create(configurationFactory);

        public static JoberHostBuilder CreateBuilder()
            => new JoberHostBuilder();
    }
}
