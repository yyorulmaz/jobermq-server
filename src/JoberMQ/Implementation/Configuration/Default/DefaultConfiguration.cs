using JoberMQ.Common.Enums.Configuration;
using JoberMQ.Abstraction.Configuration;
using JoberMQ.Factories.Configuration;
using JoberMQ.Constants;

namespace JoberMQ.Implementation.Configuration.Default
{
    internal class DefaultConfiguration : IConfiguration
    {
        public DefaultConfiguration(
            ConfigurationClientFactoryEnum configurationClientFactory,
            ConfigurationStatusCodeFactoryEnum configurationStatusCodeFactory,
            ConfigurationPublisherFactoryEnum configurationPublisherFactory,
            ConfigurationTimingFactoryEnum configurationTimingFactory,
            ConfigurationQueueFactoryEnum configurationQueueFactory,
            ConfigurationMessageFactoryEnum configurationMessageFactory,
            ConfigurationDatabaseFactoryEnum configurationDatabaseFactory,
            ConfigurationHostFactoryEnum configurationHostFactory,
            ConfigurationSecurityFactoryEnum configurationSecurityFactory,
            ConfigurationDistributorFactoryEnum configurationDistributorFactory,
            ConfigurationBrokerFactoryEnum configurationBrokerFactory)
        {
            configurationClient = ConfigurationClientFactory.Create(configurationClientFactory);
            configurationStatusCode = ConfigurationStatusCodeFactory.Create(configurationStatusCodeFactory);
            configurationPublisher = ConfigurationPublisherFactory.Create(configurationPublisherFactory);
            configurationTiming = ConfigurationTimingFactory.Create(configurationTimingFactory);
            configurationQueue = ConfigurationQueueFactory.Create(configurationQueueFactory);
            configurationMessage = ConfigurationMessageFactory.Create(configurationMessageFactory);
            configurationDatabase = ConfigurationDatabaseFactory.Create(configurationDatabaseFactory);
            configurationHost = ConfigurationHostFactory.Create(configurationHostFactory);
            configurationSecurity = ConfigurationSecurityFactory.Create(configurationSecurityFactory);
            configurationDistributor = ConfigurationDistributorFactory.Create(configurationDistributorFactory);
            configurationBroker = ConfigurationBrokerFactory.Create(configurationBrokerFactory);
        }

        public bool isOwinHost = ConfigurationConst.IsOwinHost;
        public bool IsOwinHost { get => isOwinHost; set => isOwinHost = value; }

        IConfigurationClient configurationClient;
        public IConfigurationClient ConfigurationClient { get => configurationClient; set => configurationClient = value; }
        IConfigurationStatusCode configurationStatusCode;
        public IConfigurationStatusCode ConfigurationStatusCode { get => configurationStatusCode; set => configurationStatusCode = value; }
        IConfigurationPublisher configurationPublisher;
        public IConfigurationPublisher ConfigurationPublisher { get => configurationPublisher; set => configurationPublisher = value; }
        IConfigurationTiming configurationTiming;
        public IConfigurationTiming ConfigurationTiming { get => configurationTiming; set => configurationTiming = value; }
        IConfigurationQueue configurationQueue;
        public IConfigurationQueue ConfigurationQueue { get => configurationQueue; set => configurationQueue = value; }
        IConfigurationMessage configurationMessage;
        public IConfigurationMessage ConfigurationMessage { get => configurationMessage; set => configurationMessage = value; }
        IConfigurationDatabase configurationDatabase;
        public IConfigurationDatabase ConfigurationDatabase { get => configurationDatabase; set => configurationDatabase = value; }
        IConfigurationHost configurationHost;
        public IConfigurationHost ConfigurationHost { get => configurationHost; set => configurationHost = value; }
        IConfigurationSecurity configurationSecurity;
        public IConfigurationSecurity ConfigurationSecurity { get => configurationSecurity; set => configurationSecurity = value; }
        IConfigurationDistributor configurationDistributor;
        public IConfigurationDistributor ConfigurationDistributor { get => configurationDistributor; set => configurationDistributor = value; }
        IConfigurationBroker configurationBroker;
        public IConfigurationBroker ConfigurationBroker { get => configurationBroker; set => configurationBroker = value; }
    }
}
