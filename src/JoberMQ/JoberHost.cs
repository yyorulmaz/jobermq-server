using JoberMQ.Abstraction.Host;
using JoberMQ.Abstraction.Jober;
using JoberMQ.Common.Enums.Host;
using JoberMQ.Constants;
using JoberMQ.Factories.Host;

namespace JoberMQ
{
    public static class JoberHost
    {
        internal static IJober Jober { get; set; }
        public static IJoberHostBuilder CreateDefaultBuilder()
            => HostFactory.CreateJoberHostBuilder(DefaultConst.HostFactory);

        public static IJoberHostBuilder CreateDefaultBuilder(HostFactoryEnum hostFactory)
            => HostFactory.CreateJoberHostBuilder(hostFactory);
    }
}
