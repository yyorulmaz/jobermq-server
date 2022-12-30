using JoberMQ.Client.Abstraction;
using JoberMQ.Client.Implementation.Default;
using JoberMQ.Common.Enums.Configuration;

namespace JoberMQ.Client.Factories
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