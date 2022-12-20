using JoberMQ.Abstraction.Configuration;

namespace JoberMQ.Abstraction.Host
{
    public interface IJoberHostBuilder
    {
        internal IConfiguration Configuration { get; set; }
    }
}
