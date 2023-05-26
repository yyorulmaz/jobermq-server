using JoberMQ.Configuration.Abstraction;
using JoberMQ.Configuration.Factories;
using JoberMQ.Common.Enums.Configuration;
using System;
using System.IO;
using System.Reflection;

namespace JoberMQ.Configuration.Implementation.Default
{
    internal class DfConfiguration : IConfiguration
    {
        IConfigurationJober configurationJober;
        public IConfigurationJober ConfigurationJober { get => configurationJober; set => configurationJober = value; }


        IConfigurationStatusCode configurationStatusCode;
        public IConfigurationStatusCode ConfigurationStatusCode { get => configurationStatusCode; set => configurationStatusCode = value; }


        IConfigurationClient configurationClient;
        public IConfigurationClient ConfigurationClient { get => configurationClient; set => configurationClient = value; }


        IConfigurationMessage configurationMessage;
        public IConfigurationMessage ConfigurationMessage { get => configurationMessage; set => configurationMessage = value; }


        IConfigurationDatabase configurationDatabase;
        public IConfigurationDatabase ConfigurationDatabase { get => configurationDatabase; set => configurationDatabase = value; }


        IConfigurationQueue configurationQueue;
        public IConfigurationQueue ConfigurationQueue { get => configurationQueue; set => configurationQueue = value; }


        IConfigurationDistributor configurationDistributor;
        public IConfigurationDistributor ConfigurationDistributor { get => configurationDistributor; set => configurationDistributor = value; }


        IConfigurationBroker configurationBroker;
        public IConfigurationBroker ConfigurationBroker { get => configurationBroker; set => configurationBroker = value; }


        IConfigurationSecurity configurationSecurity;
        public IConfigurationSecurity ConfigurationSecurity { get => configurationSecurity; set => configurationSecurity = value; }


        IConfigurationHost configurationHost;
        public IConfigurationHost ConfigurationHost { get => configurationHost; set => configurationHost = value; }


        IConfigurationTiming configurationTiming;
        public IConfigurationTiming ConfigurationTiming { get => configurationTiming; set => configurationTiming = value; }


        IConfigurationPublisher configurationPublisher;
        public IConfigurationPublisher ConfigurationPublisher { get => configurationPublisher; set => configurationPublisher = value; }


        public DfConfiguration(
            ConfigurationJoberFactoryEnum configurationJoberFactory,
            ConfigurationStatusCodeFactoryEnum configurationStatusCodeFactory,
            ConfigurationClientFactoryEnum configurationClientFactory,
            ConfigurationMessageFactoryEnum configurationMessageFactory,
            ConfigurationDatabaseFactoryEnum configurationDatabaseFactory,
            ConfigurationQueueFactoryEnum configurationQueueFactory,
            ConfigurationDistributorFactoryEnum configurationDistributorFactory,
            ConfigurationBrokerFactoryEnum configurationBrokerFactory,
            ConfigurationSecurityFactoryEnum configurationSecurityFactory,
            ConfigurationHostFactoryEnum configurationHostFactory,
            ConfigurationTimingFactoryEnum configurationTimingFactory,
            ConfigurationPublisherFactoryEnum configurationPublisherFactory
            )
        {
            configurationJober = ConfigurationJoberFactory.Create(configurationJoberFactory);
            configurationStatusCode = ConfigurationStatusCodeFactory.Create(configurationStatusCodeFactory);
            configurationClient = ConfigurationClientFactory.Create(configurationClientFactory);
            configurationMessage = ConfigurationMessageFactory.Create(configurationMessageFactory);

            configurationDatabase = ConfigurationDatabaseFactory.Create(configurationDatabaseFactory);
            if (configurationDatabaseFactory == ConfigurationDatabaseFactoryEnum.Default)
            {
                string applicationDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

                Console.WriteLine(applicationDirectory);
                foreach (var item in configurationDatabase.DbTextFileConfigDatas)
                {
                    item.Value.DbPath = Path.Combine(new string[] { applicationDirectory, item.Value.DbPath });
                }
            }

            configurationQueue = ConfigurationQueueFactory.Create(configurationQueueFactory);
            configurationDistributor = ConfigurationDistributorFactory.Create(configurationDistributorFactory);
            configurationBroker = ConfigurationBrokerFactory.Create(configurationBrokerFactory);
            configurationSecurity = ConfigurationSecurityFactory.Create(configurationSecurityFactory);
            configurationHost = ConfigurationHostFactory.Create(configurationHostFactory);
            configurationTiming = ConfigurationTimingFactory.Create(configurationTimingFactory);
            configurationPublisher = ConfigurationPublisherFactory.Create(configurationPublisherFactory);
        }
    }
}
