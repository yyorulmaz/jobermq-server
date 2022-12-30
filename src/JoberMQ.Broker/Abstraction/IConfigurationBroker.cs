using JoberMQ.Common.Enums.Broker;

namespace JoberMQ.Broker.Abstraction
{
    public interface IConfigurationBroker
    {
        public MessageBrokerFactoryEnum MessageBrokerFactory { get; set; }
    }
}
