using JoberMQ.Common.Enums.Broker;

namespace JoberMQ.Abstraction.Configuration
{
    public interface IConfigurationBroker
    {
        public MessageBrokerFactoryEnum MessageBrokerFactory { get; set; }

    }
}
