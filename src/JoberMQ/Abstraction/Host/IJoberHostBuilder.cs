using JoberMQ.Configuration.Abstraction;

namespace JoberMQ.Abstraction.Host
{
    public interface IJoberHostBuilder
    {
        internal IConfiguration Configuration { get; set; }
    }
}
