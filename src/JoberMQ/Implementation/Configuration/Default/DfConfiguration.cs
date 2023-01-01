using JoberMQ.Abstraction.Configuration;
using JoberMQ.Broker.Abstraction;
using JoberMQ.Client.Abstraction;
using JoberMQ.Common.Enums.Database;
using JoberMQ.Common.Enums.Jober;
using JoberMQ.Common.StatusCode.Enums;
using JoberMQ.Common.StatusCode.Models;
using JoberMQ.Constants;
using JoberMQ.Database.Abstraction.Configuration;
using JoberMQ.Database.Factories;
using JoberMQ.Distributor.Abstraction;
using JoberMQ.Distributor.Constants;
using JoberMQ.Factories.Configuration;
using JoberMQ.Queue.Abstraction;
using JoberMQ.Timing.Abstraction;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Reflection;

namespace JoberMQ.Implementation.Configuration.Default
{
    internal class DfConfiguration : IConfiguration
    {
        public DfConfiguration()
        {
            configurationDatabase = JoberMQ.Database.Factories.ConfigurationDatabaseFactory.Create(DefaultConst.ConfigurationDatabaseFactory);
            if (DefaultConst.ConfigurationDatabaseFactory == Common.Enums.Configuration.ConfigurationDatabaseFactoryEnum.Default)
            {
                string applicationDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

                Console.WriteLine(applicationDirectory);
                foreach (var item in configurationDatabase.DbTextFileConfigDatas)
                {
                    item.Value.DbPath = Path.Combine(new string[] { applicationDirectory, item.Value.DbPath });
                }
            }


            configurationClient = JoberMQ.Client.Factories.ConfigurationClientFactory.Create(DefaultConst.ConfigurationClientFactory);
            configurationQueue = JoberMQ.Queue.Factories.ConfigurationQueueFactory.Create(DefaultConst.ConfigurationQueueFactory);
            configurationDistributor = JoberMQ.Distributor.Factories.ConfigurationDistributorFactory.Create(DefaultConst.ConfigurationDistributorFactory);
            configurationBroker = JoberMQ.Broker.Factories.ConfigurationBrokerFactory.Create(DefaultConst.ConfigurationBrokerFactory);
            configurationTiming = JoberMQ.Timing.Factories.ConfigurationTimingFactory.Create(DefaultConst.ConfigurationTimingFactory);
            configurationHost = ConfigurationHostFactory.CreateConfigurationHost(DefaultHostConst.ConfigurationHostFactory);
        }

        IConfigurationDatabase configurationDatabase;
        public IConfigurationDatabase ConfigurationDatabase { get => configurationDatabase; set => configurationDatabase = value; }


        IConfigurationClient configurationClient;
        public IConfigurationClient ConfigurationClient { get => configurationClient; set => configurationClient = value; }


        IConfigurationQueue configurationQueue;
        public IConfigurationQueue ConfigurationQueue { get => configurationQueue; set => configurationQueue = value; }


        IConfigurationDistributor configurationDistributor;
        public IConfigurationDistributor ConfigurationDistributor { get => configurationDistributor; set => configurationDistributor = value; }


        IConfigurationBroker configurationBroker;
        public IConfigurationBroker ConfigurationBroker { get => configurationBroker; set => configurationBroker = value; }


        IConfigurationTiming configurationTiming;
        public IConfigurationTiming ConfigurationTiming { get => configurationTiming; set => configurationTiming = value; }


        IConfigurationHost configurationHost;
        public IConfigurationHost ConfigurationHost { get => configurationHost; set => configurationHost = value; }


        StatusCodeFactoryEnum statusCodeFactory = DefaultConst.StatusCodeFactory;
        public StatusCodeFactoryEnum StatusCodeFactory { get => statusCodeFactory; set => statusCodeFactory = value; }
        StatusCodeMessageLanguageEnum statusCodeMessageLanguage = DefaultConst.StatusCodeMessageLanguage;
        public StatusCodeMessageLanguageEnum StatusCodeMessageLanguage { get => statusCodeMessageLanguage; set => statusCodeMessageLanguage = value; }
        ConcurrentDictionary<string, StatusCodeModel> statusCodeDatas = DefaultConst.DefaultStatusCodeDatas;
        public ConcurrentDictionary<string, StatusCodeModel> StatusCodeDatas { get => statusCodeDatas; set => statusCodeDatas = value; }

        string securityKey = DefaultConst.SecurityKey;
        public string SecurityKey { get => securityKey; set => securityKey = value; }
    }
}
