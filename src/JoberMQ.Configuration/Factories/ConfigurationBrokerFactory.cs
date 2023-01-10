using JoberMQ.Common.Enums.Configuration;
using JoberMQ.Configuration.Abstraction;
using JoberMQ.Configuration.Implementation.Default;

namespace JoberMQ.Configuration.Factories
{
    internal class ConfigurationBrokerFactory
    {
        internal static IConfigurationBroker Create(ConfigurationBrokerFactoryEnum configurationBrokerFactory)
        {
            IConfigurationBroker configurationBroker;

            switch (configurationBrokerFactory)
            {
                case ConfigurationBrokerFactoryEnum.Default:
                    configurationBroker = new DfConfigurationBroker();
                    break;
                default:
                    configurationBroker = new DfConfigurationBroker();
                    break;
            }

            return configurationBroker;
        }
    }
}
