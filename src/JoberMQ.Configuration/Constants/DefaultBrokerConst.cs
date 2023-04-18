using JoberMQ.Library.Database.Enums;
using JoberMQ.Library.Enums.Broker;

namespace JoberMQ.Configuration.Constants
{
    internal class DefaultBrokerConst
    {








        internal const MessageBrokerFactoryEnum BrokerFactory = MessageBrokerFactoryEnum.Default;

        internal const MemFactoryEnum MasterMessagesFactory = MemFactoryEnum.Default;
        internal const MemDataFactoryEnum MasterMessagesDataFactory = MemDataFactoryEnum.Data;
    }
}
