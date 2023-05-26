using JoberMQ.Common.Database.Enums;
using JoberMQ.Common.Enums.Broker;

namespace JoberMQ.Configuration.Abstraction
{
    public interface IConfigurationBroker
    {










        public MessageBrokerFactoryEnum MessageBrokerFactory { get; set; }

        public MemFactoryEnum MasterMessagesFactory { get; set; }
        public MemDataFactoryEnum MasterMessagesDataFactory { get; set; }
    }
}
