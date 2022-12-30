using JoberMQ.Abstraction.Host;
using JoberMQ.Constants;
using JoberMQ.Factories.Host;

namespace JoberMQ
{
    public static class JoberHost
    {
        public static IJoberHostBuilder CreateDefaultBuilder()
            => HostFactory.CreateJoberHostBuilder(DefaultConst.HostFactory);
    }
}
