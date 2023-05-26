using JoberMQ.Common.Enums.Broker;
using JoberMQ.Abstraction.Configuration;
using JoberMQ.Constants;

namespace JoberMQ.Implementation.Configuration.Default
{
    internal class DefaultConfigurationBroker : IConfigurationBroker
    {
        MessageBrokerFactoryEnum brokerFactory = ConfigurationBrokerConst.BrokerFactory;
        public MessageBrokerFactoryEnum MessageBrokerFactory { get => brokerFactory; set => brokerFactory = value; }
    }
}
