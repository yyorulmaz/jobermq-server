using JoberMQ.Common.Enums.Configuration;
using JoberMQ.Configuration.Abstraction;
using JoberMQ.Configuration.Implementation.Default;

namespace JoberMQ.Configuration.Factories
{
    internal class ConfigurationQueueFactory
    {
        internal static IConfigurationQueue Create(ConfigurationQueueFactoryEnum configurationQueueFactory)
        {
            IConfigurationQueue configurationQueue;

            switch (configurationQueueFactory)
            {
                case ConfigurationQueueFactoryEnum.Default:
                    configurationQueue = new DfConfigurationQueue();
                    break;
                default:
                    configurationQueue = new DfConfigurationQueue();
                    break;
            }

            return configurationQueue;
        }
    }
}
