using JoberMQ.Abstraction.Configuration;
using JoberMQ.Abstraction.Host;
using JoberMQ.Abstraction.Jober;
using JoberMQ.Constants;
using JoberMQ.Factories.Jober;

namespace JoberMQ.Extensions
{
    public static class JoberBuilderExtensions
    {
        public static IJoberHostBuilder Configuration(this IJoberHostBuilder joberHostBuilder, IConfiguration configuration)
        {
            joberHostBuilder.Configuration = configuration;
            return joberHostBuilder;
        }
    }
}
