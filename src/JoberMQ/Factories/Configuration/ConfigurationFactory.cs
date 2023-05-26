using JoberMQ.Common.Enums.Configuration;
using JoberMQ.Abstraction.Configuration;
using JoberMQ.Constants;
using JoberMQ.Implementation.Configuration.Default;

namespace JoberMQ.Factories.Configuration
{
    internal class ConfigurationFactory
    {
        internal static IConfiguration Create(
            ConfigurationFactoryEnum configurationFactory,
            ConfigurationClientFactoryEnum configurationClientFactory = ConfigurationConst.ConfigurationClientFactory,
            ConfigurationStatusCodeFactoryEnum configurationStatusCodeFactory = ConfigurationConst.ConfigurationStatusCodeFactory,
            ConfigurationPublisherFactoryEnum configurationPublisherFactory = ConfigurationConst.ConfigurationPublisherFactory,
            ConfigurationTimingFactoryEnum configurationTimingFactory = ConfigurationConst.ConfigurationTimingFactory,
            ConfigurationQueueFactoryEnum configurationQueueFactory = ConfigurationConst.ConfigurationQueueFactory,
            ConfigurationMessageFactoryEnum configurationMessageFactory = ConfigurationConst.ConfigurationMessageFactory,
            ConfigurationDatabaseFactoryEnum configurationDatabaseFactory = ConfigurationConst.ConfigurationDatabaseFactory,
            ConfigurationHostFactoryEnum configurationHostFactory = ConfigurationConst.ConfigurationHostFactory,
            ConfigurationSecurityFactoryEnum configurationSecurityFactory = ConfigurationConst.ConfigurationSecurityFactory,
            ConfigurationDistributorFactoryEnum configurationDistributorFactory = ConfigurationConst.ConfigurationDistributorFactory,
            ConfigurationBrokerFactoryEnum configurationBrokerFactory = ConfigurationConst.ConfigurationBrokerFactory)
        {
            IConfiguration result;

            switch (configurationFactory)
            {
                case ConfigurationFactoryEnum.Default:
                    result = new DefaultConfiguration(
                        configurationClientFactory, 
                        configurationStatusCodeFactory, 
                        configurationPublisherFactory, 
                        configurationTimingFactory,
                        configurationQueueFactory,
                        configurationMessageFactory,
                        configurationDatabaseFactory, 
                        configurationHostFactory,
                        configurationSecurityFactory,
                        configurationDistributorFactory,
                        configurationBrokerFactory);
                    break;
                default:
                    throw new System.Exception("none");
            }

            return result;
        }
    }
}
