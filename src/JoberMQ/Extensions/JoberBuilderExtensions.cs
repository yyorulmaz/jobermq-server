using JoberMQ.Abstraction.Host;

namespace JoberMQ.Extensions
{
    public static class JoberBuilderExtensions
    {
        public static IJoberHostBuilder Configuration(this IJoberHostBuilder joberHostBuilder, JoberMQ.Configuration.Abstraction.IConfiguration configuration)
        {
            joberHostBuilder.Configuration = configuration;
            return joberHostBuilder;
        }
    }
}
