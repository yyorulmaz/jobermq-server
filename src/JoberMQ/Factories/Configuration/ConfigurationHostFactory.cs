using JoberMQ.Abstraction.Configuration;
using JoberMQ.Common.Enums.Configuration;
using JoberMQ.Implementation.Configuration.Default;

namespace JoberMQ.Factories.Configuration
{
    public class ConfigurationHostFactory
    {
        public static IConfigurationHost CreateConfigurationHost(ConfigurationHostFactoryEnum factory)
        {
            IConfigurationHost configurationHost;

            switch (factory)
            {
                case ConfigurationHostFactoryEnum.Default:
                    configurationHost = new DfConfigurationHost();
                    break;
                default:
                    configurationHost = new DfConfigurationHost();
                    break;
            }

            return configurationHost;
        }
    }
}
