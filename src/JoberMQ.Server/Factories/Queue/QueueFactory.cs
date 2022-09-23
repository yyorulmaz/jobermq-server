using JoberMQ.Entities.Enums.Queue;
using JoberMQ.Server.Abstraction.Queue;
using JoberMQ.Server.Implementation.Queue;
using JoberMQNEW.Server.Abstraction.Client;
using JoberMQNEW.Server.Data;

namespace JoberMQ.Server.Factories.Queue
{
    internal class QueueFactory
    {
        internal static IQueueDataBase GetQueueDataBase()
            => new DfQueueDataBase(InMemoryQueue.QueueDatas);

        internal static IQueue CreateQueue(
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
                            queue = new DfQueuePriority(distributorName, queueName, matchType, sendType, clientGroup, GetQueueDataBase());
                            break;
                        case SendTypeEnum.FIFO:
                            queue = new DfQueueFIFO(distributorName, queueName, matchType, sendType, clientGroup, GetQueueDataBase());
                            break;
                        case SendTypeEnum.LIFO:
                            queue = new DfQueueLIFO(distributorName, queueName, matchType, sendType, clientGroup, GetQueueDataBase());
                            break;
                        default:
                            queue = new DfQueueFIFO(distributorName, queueName, matchType, sendType, clientGroup, GetQueueDataBase());
                            break;
                    }
                    break;
                default:
                    switch (sendType)
                    {
                        case SendTypeEnum.Priority:
                            queue = new DfQueuePriority(distributorName, queueName, matchType, sendType, clientGroup, GetQueueDataBase());
                            break;
                        case SendTypeEnum.FIFO:
                            queue = new DfQueueFIFO(distributorName, queueName, matchType, sendType, clientGroup, GetQueueDataBase());
                            break;
                        case SendTypeEnum.LIFO:
                            queue = new DfQueueLIFO(distributorName, queueName, matchType, sendType, clientGroup, GetQueueDataBase());
                            break;
                        default:
                            queue = new DfQueueFIFO(distributorName, queueName, matchType, sendType, clientGroup, GetQueueDataBase());
                            break;
                    }
                    break;
            }

            return queue;
        }
    }
}
