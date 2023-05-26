using JoberMQ.Common.Enums.Configuration;
using JoberMQ.Abstraction.Configuration;
using JoberMQ.Implementation.Configuration.Default;

namespace JoberMQ.Factories.Configuration
{
    internal class ConfigurationHostFactory
    {
        public static IConfigurationHost Create(ConfigurationHostFactoryEnum configurationHostFactory)
        {
            IConfigurationHost configurationHost;

            switch (configurationHostFactory)
            {
                case ConfigurationHostFactoryEnum.Default:
                    configurationHost = new DefaultConfigurationHost();
                    break;
                default:
                    throw new System.Exception("none");
            }

            return configurationHost;
        }
    }
}
