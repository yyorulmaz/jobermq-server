using JoberMQ.Broker.Abstraction;
using JoberMQ.Client.Abstraction;
using JoberMQ.Common.StatusCode.Enums;
using JoberMQ.Common.StatusCode.Models;
using JoberMQ.Database.Abstraction.Configuration;
using JoberMQ.Distributor.Abstraction;
using JoberMQ.Queue.Abstraction;
using JoberMQ.Timing.Abstraction;
using System.Collections.Concurrent;

namespace JoberMQ.Abstraction.Configuration
{
    public interface IConfiguration
    {
        public IConfigurationDatabase ConfigurationDatabase { get; set; }
        public IConfigurationClient ConfigurationClient { get; set; }
        public IConfigurationQueue ConfigurationQueue { get; set; }
        public IConfigurationDistributor ConfigurationDistributor { get; set; }
        public IConfigurationBroker ConfigurationBroker { get; set; }
        public IConfigurationTiming ConfigurationTiming { get; set; }
        public IConfigurationHost ConfigurationHost { get; set; }


        public StatusCodeFactoryEnum StatusCodeFactory { get; set; }
        public StatusCodeMessageLanguageEnum StatusCodeMessageLanguage { get; set; }
        public ConcurrentDictionary<string, StatusCodeModel> StatusCodeDatas { get; set; }
        public string SecurityKey { get; set; }

    }
}
