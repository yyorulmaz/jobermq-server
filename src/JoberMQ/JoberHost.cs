using JoberMQ.Abstraction.Jober;
using JoberMQ.Configuration.Abstraction;
using JoberMQ.Configuration.Constants;
using JoberMQ.Configuration.Factories;
using JoberMQ.Library.Enums.Configuration;

namespace JoberMQ
{
    public class JoberHost
    {
        internal static IJober Jober { get; set; }

        public static IConfiguration CreateConfiguration(ConfigurationFactoryEnum configurationFactory = DefaultConfigurationConst.ConfigurationFactory)
            => ConfigurationFactory.Create(configurationFactory);

        public static JoberHostBuilder CreateBuilder()
            => new JoberHostBuilder();
    }
}
