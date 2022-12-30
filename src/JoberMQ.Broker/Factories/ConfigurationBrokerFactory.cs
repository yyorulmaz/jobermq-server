using JoberMQ.Broker.Abstraction;
using JoberMQ.Broker.Implementation.Default;
using JoberMQ.Common.Enums.Configuration;

namespace JoberMQ.Broker.Factories
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
