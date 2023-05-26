using JoberMQ.Abstraction.Configuration;
using JoberMQ.Implementation.Configuration.Default;
using JoberMQ.Common.Enums.Configuration;

namespace JoberMQ.Factories.Configuration
{
    internal class ConfigurationQueueFactory
    {
        internal static IConfigurationQueue Create(ConfigurationQueueFactoryEnum configurationQueueFactory)
        {
            IConfigurationQueue result;

            switch (configurationQueueFactory)
            {
                case ConfigurationQueueFactoryEnum.Default:
                    result = new DefaultConfigurationQueue();
                    break;
                default:
                    throw new System.Exception("none");
            }

            return result;
        }
    }
}
