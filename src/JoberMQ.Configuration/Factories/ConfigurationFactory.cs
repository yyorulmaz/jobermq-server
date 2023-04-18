using JoberMQ.Configuration.Abstraction;
using JoberMQ.Configuration.Implementation.Default;
using JoberMQ.Library.Enums.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberMQ.Configuration.Factories
{
    internal class ConfigurationFactory
    {
        public static IConfiguration Create(
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
            ConfigurationTimingFactoryEnum configurationTimingFactory = ConfigurationTimingFactoryEnum.Default,
            ConfigurationPublisherFactoryEnum configurationPublisherFactory = ConfigurationPublisherFactoryEnum.Default
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
                        configurationTimingFactory,
                        configurationPublisherFactory
                        );
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
                        configurationTimingFactory,
                        configurationPublisherFactory
                        );
                    break;
            }

            return configuration;
        }
    }
}
