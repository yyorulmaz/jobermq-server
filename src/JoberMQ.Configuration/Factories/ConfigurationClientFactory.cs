using JoberMQ.Configuration.Abstraction;
using JoberMQ.Configuration.Implementation.Default;
using JoberMQ.Library.Enums.Configuration;

namespace JoberMQ.Configuration.Factories
{
    internal class ConfigurationClientFactory
    {
        internal static IConfigurationClient Create(ConfigurationClientFactoryEnum configurationClientFactory)
        {
            IConfigurationClient configurationClient;

            switch (configurationClientFactory)
            {
                case ConfigurationClientFactoryEnum.Default:
                    configurationClient = new DfConfigurationClient();
                    break;
                default:
                    configurationClient = new DfConfigurationClient();
                    break;
            }

            return configurationClient;
        }
    }
}
