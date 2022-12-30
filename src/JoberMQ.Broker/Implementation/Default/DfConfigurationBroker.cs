using JoberMQ.Broker.Abstraction;
using JoberMQ.Broker.Constants;
using JoberMQ.Common.Enums.Broker;

namespace JoberMQ.Broker.Implementation.Default
{
    internal class DfConfigurationBroker : IConfigurationBroker
    {
        MessageBrokerFactoryEnum brokerFactory = DefaultBrokerConst.BrokerFactory;
        public MessageBrokerFactoryEnum MessageBrokerFactory { get => brokerFactory; set => brokerFactory = value; }
    }
}
