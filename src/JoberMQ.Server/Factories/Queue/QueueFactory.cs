using JoberMQ.Entities.Enums.Queue;
using JoberMQ.Entities.Models.Config;
using JoberMQ.Server.Abstraction.DbOpr;
using JoberMQ.Server.Abstraction.Queue;
using JoberMQ.Server.Implementation.Queue.Default;
using JoberMQNEW.Server.Abstraction.Client;

namespace JoberMQ.Server.Factories.Queue
{
    internal class QueueFactory
    {
        internal static IQueue CreateQueue(
            BrokerConfigModel brokerConfig,
            string queueKey, 
            MatchTypeEnum matchType, 
            SendTypeEnum sendType, 
            IClientGroup clientGroup,
            IMessageDbOpr messageDbOpr)
        {
            IQueue queue;

            switch (brokerConfig.QueueFactory)
            {
                case QueueFactoryEnum.Default:
                    switch (sendType)
                    {
                        case SendTypeEnum.Priority:
                            queue = new DfQueuePriority(brokerConfig, queueKey, matchType, sendType, clientGroup, QueueDataBaseFactory.GetQueueDataBase(), messageDbOpr);
                            break;
                        case SendTypeEnum.FIFO:
                            queue = new DfQueueFIFO(brokerConfig, queueKey, matchType, sendType, clientGroup, QueueDataBaseFactory.GetQueueDataBase(), messageDbOpr);
                            break;
                        case SendTypeEnum.LIFO:
                            queue = new DfQueueLIFO(brokerConfig, queueKey, matchType, sendType, clientGroup, QueueDataBaseFactory.GetQueueDataBase(), messageDbOpr);
                            break;
                        default:
                            queue = new DfQueueFIFO(brokerConfig, queueKey, matchType, sendType, clientGroup, QueueDataBaseFactory.GetQueueDataBase(), messageDbOpr);
                            break;
                    }
                    break;
                default:
                    switch (sendType)
                    {
                        case SendTypeEnum.Priority:
                            queue = new DfQueuePriority(brokerConfig, queueKey, matchType, sendType, clientGroup, QueueDataBaseFactory.GetQueueDataBase(), messageDbOpr);
                            break;
                        case SendTypeEnum.FIFO:
                            queue = new DfQueueFIFO(brokerConfig, queueKey, matchType, sendType, clientGroup, QueueDataBaseFactory.GetQueueDataBase(), messageDbOpr);
                            break;
                        case SendTypeEnum.LIFO:
                            queue = new DfQueueLIFO(brokerConfig, queueKey, matchType, sendType, clientGroup, QueueDataBaseFactory.GetQueueDataBase(), messageDbOpr);
                            break;
                        default:
                            queue = new DfQueueFIFO(brokerConfig, queueKey, matchType, sendType, clientGroup, QueueDataBaseFactory.GetQueueDataBase(), messageDbOpr);
                            break;
                    }
                    break;
            }

            return queue;
        }
    }
}
