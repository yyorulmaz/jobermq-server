using JoberMQ.Common.Enums.Configuration;
using JoberMQ.Configuration.Abstraction;
using JoberMQ.Configuration.Implementation.Default;

namespace JoberMQ.Configuration.Factories
{
    internal class ConfigurationFactory
    {
        public static IConfiguration CreateConfiguration(
            ConfigurationFactoryEnum factory,
            ConfigurationJoberFactoryEnum configurationJoberFactory = ConfigurationJoberFactoryEnum.Default,
            ConfigurationStatusCodeFactoryEnum configurationStatusCodeFactory = ConfigurationStatusCodeFactoryEnum.Default,
            ConfigurationClientFactoryEnum configurationClientFactory = ConfigurationClientFactoryEnum.Default,
            ConfigurationMessageFactoryEnum configurationMessageFactory = ConfigurationMessageFactoryEnum.Default,
            ConfigurationDatabaseFactoryEnum configurationDatabaseFactory = ConfigurationDatabaseFactoryEnum.Default,
            ConfigurationQueueFactoryEnum configurationQueueFactory = ConfigurationQueueFactoryEnum.Default,
            ConfigurationDistributorFactoryEnum configurationDistributorFactory = ConfigurationDistributorFactoryEnum.Default,
            ConfigurationBrokerFactoryEnum configurationBrokerFactory = ConfigurationBrokerFactoryEnum.Default,
            ConfigurationSecurityFactoryEnum configurationSecurityFactory = ConfigurationSecurityFactoryEnum.Default,
            ConfigurationHostFactoryEnum configurationHostFactory = ConfigurationHostFactoryEnum.Default,
            ConfigurationTimingFactoryEnum configurationTimingFactory = ConfigurationTimingFactoryEnum.Default
            )
        {
            IConfiguration configuration;

            switch (factory)
            {
                case ConfigurationFactoryEnum.Default:
                    configuration = new DfConfiguration(
                        configurationJoberFactory,
                        configurationStatusCodeFactory,
                        configurationClientFactory,
                        configurationMessageFactory,
                        configurationDatabaseFactory,
                        configurationQueueFactory,
                        configurationDistributorFactory,
                        configurationBrokerFactory,
                        configurationSecurityFactory,
                        configurationHostFactory,
                        configurationTimingFactory);
                    break;
                default:
                    configuration = new DfConfiguration(
                        configurationJoberFactory,
                        configurationStatusCodeFactory,
                        configurationClientFactory,
                        configurationMessageFactory,
                        configurationDatabaseFactory,
                        configurationQueueFactory,
                        configurationDistributorFactory,
                        configurationBrokerFactory,
                        configurationSecurityFactory,
                        configurationHostFactory,
                        configurationTimingFactory);
                    break;
            }

            return configuration;
        }
    }
}
