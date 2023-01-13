using JoberMQ.Abstraction.Host;
using JoberMQ.Common.Enums.Host;
using JoberMQ.Implementation.Host.Default;

namespace JoberMQ.Factories.Host
{
    internal class HostFactory
    {
        internal static IJoberHostBuilder CreateJoberHostBuilder(HostFactoryEnum factory)
        {
            IJoberHostBuilder joberHostBuilder;

            switch (factory)
            {
                case HostFactoryEnum.Default:
                    joberHostBuilder = new DfJoberHostBuilder();
                    break;
                default:
                    joberHostBuilder = new DfJoberHostBuilder();
                    break;
            }

            return joberHostBuilder;
        }
    }
}
