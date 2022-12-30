using JoberMQ.Client.Abstraction;
using JoberMQ.Database.Abstraction.DbOpr;
using JoberMQ.Common.Enums.Permission;
using JoberMQ.Common.Enums.Queue;
using JoberMQ.Queue.Abstraction;
using JoberMQ.Queue.Implementation.Default;
using Microsoft.AspNetCore.SignalR;

namespace JoberMQ.Queue.Factories
{
    internal class QueueFactory
    {
        internal static IMessageQueue CreateQueue<THub>(
            IConfigurationQueue configurationQueue,
            string queueKey, 
            MatchTypeEnum matchType, 
            SendTypeEnum sendType,
            PermissionTypeEnum permissionType, 
            bool isDurable,
            IClientGroup clientGroup,
            IMessageDbOpr messageDbOpr,
            IHubContext<THub> context) where THub : Hub
        {
            IMessageQueue queue;

            switch (configurationQueue.QueueFactory)
            {
                case QueueFactoryEnum.Default:
                    switch (sendType)
                    {
                        case SendTypeEnum.Priority:
                            queue = new DfMessageQueuePriority<THub>(configurationQueue, queueKey, matchType, sendType, permissionType, isDurable, clientGroup, QueueDataBaseFactory.GetQueueDataBase(), messageDbOpr, context);
                            break;
                        case SendTypeEnum.FIFO:
                            queue = new DfMessageQueueFIFO<THub>(configurationQueue, queueKey, matchType, sendType, permissionType, isDurable, clientGroup, QueueDataBaseFactory.GetQueueDataBase(), messageDbOpr, context);
                            break;
                        case SendTypeEnum.LIFO:
                            queue = new DfMessageQueueLIFO<THub>(configurationQueue, queueKey, matchType, sendType, permissionType, isDurable, clientGroup, QueueDataBaseFactory.GetQueueDataBase(), messageDbOpr, context);
                            break;
                        default:
                            queue = new DfMessageQueueFIFO<THub>(configurationQueue, queueKey, matchType, sendType, permissionType, isDurable, clientGroup, QueueDataBaseFactory.GetQueueDataBase(), messageDbOpr, context);
                            break;
                    }
                    break;
                default:
                    switch (sendType)
                    {
                        case SendTypeEnum.Priority:
                            queue = new DfMessageQueuePriority<THub>(configurationQueue, queueKey, matchType, sendType, permissionType, isDurable, clientGroup, QueueDataBaseFactory.GetQueueDataBase(), messageDbOpr, context);
                            break;
                        case SendTypeEnum.FIFO:
                            queue = new DfMessageQueueFIFO<THub>(configurationQueue, queueKey, matchType, sendType, permissionType, isDurable, clientGroup, QueueDataBaseFactory.GetQueueDataBase(), messageDbOpr, context);
                            break;
                        case SendTypeEnum.LIFO:
                            queue = new DfMessageQueueLIFO<THub>(configurationQueue, queueKey, matchType, sendType, permissionType, isDurable, clientGroup, QueueDataBaseFactory.GetQueueDataBase(), messageDbOpr, context);
                            break;
                        default:
                            queue = new DfMessageQueueFIFO<THub>(configurationQueue, queueKey, matchType, sendType, permissionType, isDurable, clientGroup, QueueDataBaseFactory.GetQueueDataBase(), messageDbOpr, context);
                            break;
                    }
                    break;
            }

            return queue;
        }
    }
}
