using JoberMQ.Abstraction;
using JoberMQ.Abstraction.Configuration;
using JoberMQ.Implementation;

namespace JoberMQ.Extensions
{
    public static class JoberHostBuilderExtension
    {
        public static JoberHostBuilder Configuration(this JoberHostBuilder joberHostBuilder, IConfiguration configuration)
        {
            joberHostBuilder.Configuration = configuration;
            return joberHostBuilder;
        }

        public static IJoberMQHost Build(this JoberHostBuilder joberHostBuilder)
            => new DefaultJoberMQHost(joberHostBuilder.Configuration);
    }
}
