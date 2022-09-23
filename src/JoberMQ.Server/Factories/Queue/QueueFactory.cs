using JoberMQ.Entities.Enums.Queue;
using JoberMQ.Entities.Models.Config;
using JoberMQ.Server.Abstraction.Queue;
using JoberMQ.Server.Implementation.Queue;
using JoberMQNEW.Server.Abstraction.Client;

namespace JoberMQ.Server.Factories.Queue
{
    internal class QueueFactory
    {
        internal static IQueue CreateQueue(
            BrokerConfigModel brokerConfig,
            QueueFactoryEnum queueFactory,
            string distributorName, 
            string queueName, 
            MatchTypeEnum matchType, 
            SendTypeEnum sendType, 
            IClientGroup clientGroup)
        {
            IQueue queue;

            switch (queueFactory)
            {
                case QueueFactoryEnum.Default:
                    switch (sendType)
                    {
                        case SendTypeEnum.Priority:
                            queue = new DfQueuePriority(brokerConfig,distributorName, queueName, matchType, sendType, clientGroup, QueueDataBaseFactory.GetQueueDataBase());
                            break;
                        case SendTypeEnum.FIFO:
                            queue = new DfQueueFIFO(brokerConfig,distributorName, queueName, matchType, sendType, clientGroup, QueueDataBaseFactory.GetQueueDataBase());
                            break;
                        case SendTypeEnum.LIFO:
                            queue = new DfQueueLIFO(brokerConfig,distributorName, queueName, matchType, sendType, clientGroup, QueueDataBaseFactory.GetQueueDataBase());
                            break;
                        default:
                            queue = new DfQueueFIFO(brokerConfig, distributorName, queueName, matchType, sendType, clientGroup, QueueDataBaseFactory.GetQueueDataBase());
                            break;
                    }
                    break;
                default:
                    switch (sendType)
                    {
                        case SendTypeEnum.Priority:
                            queue = new DfQueuePriority(brokerConfig, distributorName, queueName, matchType, sendType, clientGroup, QueueDataBaseFactory.GetQueueDataBase());
                            break;
                        case SendTypeEnum.FIFO:
                            queue = new DfQueueFIFO(brokerConfig, distributorName, queueName, matchType, sendType, clientGroup, QueueDataBaseFactory.GetQueueDataBase());
                            break;
                        case SendTypeEnum.LIFO:
                            queue = new DfQueueLIFO(brokerConfig, distributorName, queueName, matchType, sendType, clientGroup, QueueDataBaseFactory.GetQueueDataBase());
                            break;
                        default:
                            queue = new DfQueueFIFO(brokerConfig, distributorName, queueName, matchType, sendType, clientGroup, QueueDataBaseFactory.GetQueueDataBase());
                            break;
                    }
                    break;
            }

            return queue;
        }
    }
}
