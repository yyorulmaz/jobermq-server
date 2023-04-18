using JoberMQ.Configuration.Abstraction;
using JoberMQ.Configuration.Constants;
using JoberMQ.Library.Database.Enums;
using JoberMQ.Library.Enums.Broker;

namespace JoberMQ.Configuration.Implementation.Default
{
    internal class DfConfigurationBroker : IConfigurationBroker
    {
        MessageBrokerFactoryEnum brokerFactory = DefaultBrokerConst.BrokerFactory;
        public MessageBrokerFactoryEnum MessageBrokerFactory { get => brokerFactory; set => brokerFactory = value; }


        MemFactoryEnum masterMessagesFactory = DefaultBrokerConst.MasterMessagesFactory;
        public MemFactoryEnum MasterMessagesFactory { get => masterMessagesFactory; set => masterMessagesFactory = value; }
        MemDataFactoryEnum masterMessagesDataFactory = DefaultBrokerConst.MasterMessagesDataFactory;
        public MemDataFactoryEnum MasterMessagesDataFactory { get => masterMessagesDataFactory; set => masterMessagesDataFactory = value; }
    }
}
