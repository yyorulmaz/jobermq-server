using JoberMQ.Abstraction.Host;
using JoberMQ.Abstraction.Jober;
using JoberMQ.Constants;
using JoberMQ.Entities.Enums.Configuration;
using JoberMQ.Factories.Configuration;
using JoberMQ.Factories.Jober;

namespace JoberMQ.Extensions
{
    public static class JoberExtensions
    {
        public static IJober Build(this IJoberHostBuilder joberHostBuilder)
        {
            if (joberHostBuilder.Configuration == null)
                joberHostBuilder.Configuration = ConfigurationFactory.CreateConfiguration(DefaultConst.ConfigurationFactory);

            return JoberFactory.CreateJober(DefaultConst.JoberFactory, joberHostBuilder.Configuration);
        }
    }
}
