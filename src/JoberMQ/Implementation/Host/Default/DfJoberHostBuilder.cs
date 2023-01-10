using JoberMQ.Abstraction.Host;
using JoberMQ.Configuration.Abstraction;

namespace JoberMQ.Implementation.Host.Default
{
    public class DfJoberHostBuilder : IJoberHostBuilder
    {
        IConfiguration configuration;
        IConfiguration IJoberHostBuilder.Configuration { get => configuration; set => configuration = value; }
    }
}
