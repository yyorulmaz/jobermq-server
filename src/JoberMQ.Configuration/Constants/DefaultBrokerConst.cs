using JoberMQ.Common.Enums.Broker;
using JoberMQ.Library.Database.Enums;

namespace JoberMQ.Configuration.Constants
{
    internal class DefaultBrokerConst
    {








        internal const MessageBrokerFactoryEnum BrokerFactory = MessageBrokerFactoryEnum.Default;

        internal const MemFactoryEnum MasterMessagesFactory = MemFactoryEnum.Default;
        internal const MemDataFactoryEnum MasterMessagesDataFactory = MemDataFactoryEnum.Data;
    }
}
