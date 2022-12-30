using JoberMQ.Abstraction.Configuration;
using JoberMQ.Abstraction.Host;
using JoberMQ.Constants;
using JoberMQ.Entities.Enums.Configuration;
using JoberMQ.Factories.Configuration;

namespace JoberMQ.Implementation.Host.Default
{
    public class DfJoberHostBuilder : IJoberHostBuilder
    {
        IConfiguration configuration;
        IConfiguration IJoberHostBuilder.Configuration { get => configuration; set => configuration = value; }
    }
}
