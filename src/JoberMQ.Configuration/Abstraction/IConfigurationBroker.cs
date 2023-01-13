using JoberMQ.Common.Enums.Broker;
using JoberMQ.Library.Database.Enums;

namespace JoberMQ.Configuration.Abstraction
{
    public interface IConfigurationBroker
    {










        public MessageBrokerFactoryEnum MessageBrokerFactory { get; set; }

        public MemFactoryEnum MasterMessagesFactory { get; set; }
        public MemDataFactoryEnum MasterMessagesDataFactory { get; set; }
    }
}
