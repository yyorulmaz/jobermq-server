using JoberMQ.Common.Enums.Configuration;
using JoberMQ.Configuration.Abstraction;
using JoberMQ.Configuration.Implementation.Default;

namespace JoberMQ.Configuration.Factories
{
    internal class ConfigurationHostFactory
    {
        public static IConfigurationHost Create(ConfigurationHostFactoryEnum configurationHostFactory)
        {
            IConfigurationHost configurationHost;

            switch (configurationHostFactory)
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
