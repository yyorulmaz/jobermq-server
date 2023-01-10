using JoberMQ.Common.Enums.Configuration;
using JoberMQ.Configuration.Abstraction;
using JoberMQ.Configuration.Implementation.Default;

namespace JoberMQ.Configuration.Factories
{
    internal class ConfigurationJoberFactory
    {
        public static IConfigurationJober Create(ConfigurationJoberFactoryEnum factory)
        {
            IConfigurationJober configurationJober;

            switch (factory)
            {
                case ConfigurationJoberFactoryEnum.Default:
                    configurationJober = new DfConfigurationJober();
                    break;
                default:
                    configurationJober = new DfConfigurationJober();
                    break;
            }

            return configurationJober;
        }
    }
}
