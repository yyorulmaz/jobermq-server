using JoberMQ.Abstraction.Host;
using JoberMQ.Abstraction.Jober;
using JoberMQ.Common.Enums.Configuration;
using JoberMQ.Common.Enums.Jober;
using JoberMQ.Configuration.Constants;
using JoberMQ.Configuration.Factories;
using JoberMQ.Factories.Jober;

namespace JoberMQ.Extensions
{
    public static class JoberExtensions
    {
        public static IJober Build(this IJoberHostBuilder joberHostBuilder)
        {
            if (joberHostBuilder.Configuration == null)
                joberHostBuilder.Configuration = ConfigurationFactory.CreateConfiguration(ConfigurationFactoryEnum.Default);

            return JoberFactory.CreateJober(JoberFactoryEnum.Default, joberHostBuilder.Configuration);
        }
    }
}
