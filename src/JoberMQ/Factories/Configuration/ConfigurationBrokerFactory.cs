using JoberMQ.Abstraction.Configuration;
using JoberMQ.Implementation.Configuration.Default;
using JoberMQ.Common.Enums.Configuration;

namespace JoberMQ.Factories.Configuration
{
    internal class ConfigurationBrokerFactory
    {
        internal static IConfigurationBroker Create(ConfigurationBrokerFactoryEnum configurationBrokerFactory)
        {
            IConfigurationBroker result;

            switch (configurationBrokerFactory)
            {
                case ConfigurationBrokerFactoryEnum.Default:
                    result = new DefaultConfigurationBroker();
                    break;
                default:
                    throw new System.Exception("none");
            }

            return result;
        }
    }
}
