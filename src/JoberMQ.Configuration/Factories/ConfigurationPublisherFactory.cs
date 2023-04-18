using JoberMQ.Configuration.Abstraction;
using JoberMQ.Configuration.Implementation.Default;
using JoberMQ.Library.Enums.Configuration;

namespace JoberMQ.Configuration.Factories
{
    internal class ConfigurationPublisherFactory
    {
        internal static IConfigurationPublisher Create(ConfigurationPublisherFactoryEnum configurationPublisherFactory)
        {
            IConfigurationPublisher configurationPublisher;

            switch (configurationPublisherFactory)
            {
                case ConfigurationPublisherFactoryEnum.Default:
                    configurationPublisher = new DfConfigurationPublisher();
                    break;
                default:
                    configurationPublisher = new DfConfigurationPublisher();
                    break;
            }

            return configurationPublisher;
        }
    }
}
