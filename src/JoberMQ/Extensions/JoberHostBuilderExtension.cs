using JoberMQ.Abstraction.Jober;
using JoberMQ.Configuration.Abstraction;
using JoberMQ.Configuration.Constants;
using JoberMQ.Configuration.Factories;
using JoberMQ.Factories.Jober;

namespace JoberMQ.Extensions
{
    public static class JoberHostBuilderExtension
    {
        public static JoberHostBuilder Configuration(this JoberHostBuilder joberHostBuilder, IConfiguration configuration)
        {
            joberHostBuilder.Configuration = configuration;
            return joberHostBuilder;
        }

        public static IJober Build(this JoberHostBuilder joberHostBuilder)
            => JoberFactory.Create(DefaultJoberConst.JoberFactory, joberHostBuilder.Configuration == null ? ConfigurationFactory.Create(DefaultConfigurationConst.ConfigurationFactory) : joberHostBuilder.Configuration);
    }
}
