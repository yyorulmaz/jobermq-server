using JoberMQ.Configuration.Abstraction;
using JoberMQ.Configuration.Implementation.Default;
using JoberMQ.Library.Enums.Configuration;

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
