using JoberMQ.Abstraction.Configuration;
using JoberMQ.Implementation.Configuration.Default;
using JoberMQ.Common.Enums.Configuration;

namespace JoberMQ.Factories.Configuration
{
    internal class ConfigurationPublisherFactory
    {
        internal static IConfigurationPublisher Create(ConfigurationPublisherFactoryEnum configurationPublisherFactory)
        {
            IConfigurationPublisher result;

            switch (configurationPublisherFactory)
            {
                case ConfigurationPublisherFactoryEnum.Default:
                    result = new DefaultConfigurationPublisher();
                    break;
                default:
                    throw new System.Exception("none");
            }

            return result;
        }
    }
}
