using JoberMQ.Client.Abstraction;
using JoberMQ.Common.Dbos;
using JoberMQ.Common.Enums;
using JoberMQ.Common.Enums.Queue;
using JoberMQ.Configuration.Abstraction;
using JoberMQ.Library.Database.Repository.Abstraction.Mem;
using JoberMQ.Library.Database.Repository.Abstraction.Opr;
using JoberMQ.Queue.Abstraction;
using JoberMQ.Queue.Implementation.Default;
using Microsoft.AspNetCore.SignalR;
using System;

namespace JoberMQ.Queue.Factories
{
    internal class QueueFactory
    {
        internal static IMessageQueue Create<THub>(
            IConfigurationQueue configurationQueue,
            string queueKey,
            MatchTypeEnum matchType,
            SendTypeEnum sendType,
            PermissionTypeEnum permissionType,
            bool isDurable,
            IMemRepository<string, IClient> masterClient,
            IMemRepository<Guid, MessageDbo> masterMessages,
            IOprRepositoryGuid<MessageDbo> messageDbOpr,
            ref bool isJoberActive,
            IHubContext<THub> context) where THub : Hub
        {
            IMessageQueue queue;

            switch (configurationQueue.QueueFactory)
            {
                case QueueFactoryEnum.Default:
                    switch (sendType)
                    {
                        case SendTypeEnum.Priority:
                            queue = new DfMessageQueuePriority<THub>(queueKey, matchType, sendType, permissionType, isDurable, masterClient, masterMessages, messageDbOpr, ref isJoberActive, context);
                            break;
                        case SendTypeEnum.FIFO:
                            queue = new DfMessageQueueFIFO<THub>(queueKey, matchType, sendType, permissionType, isDurable, masterClient, masterMessages, messageDbOpr, ref isJoberActive, context);
                            break;
                        case SendTypeEnum.LIFO:
                            queue = new DfMessageQueueLIFO<THub>(queueKey, matchType, sendType, permissionType, isDurable, masterClient, masterMessages, messageDbOpr, ref isJoberActive, context);
                            break;
                        default:
                            queue = new DfMessageQueueFIFO<THub>(queueKey, matchType, sendType, permissionType, isDurable, masterClient, masterMessages, messageDbOpr, ref isJoberActive, context);
                            break;
                    }
                    break;
                default:
                    switch (sendType)
                    {
                        case SendTypeEnum.Priority:
                            queue = new DfMessageQueuePriority<THub>(queueKey, matchType, sendType, permissionType, isDurable, masterClient, masterMessages, messageDbOpr, ref isJoberActive, context);
                            break;
                        case SendTypeEnum.FIFO:
                            queue = new DfMessageQueueFIFO<THub>(queueKey, matchType, sendType, permissionType, isDurable, masterClient, masterMessages, messageDbOpr, ref isJoberActive, context);
                            break;
                        case SendTypeEnum.LIFO:
                            queue = new DfMessageQueueLIFO<THub>(queueKey, matchType, sendType, permissionType, isDurable, masterClient, masterMessages, messageDbOpr, ref isJoberActive, context);
                            break;
                        default:
                            queue = new DfMessageQueueFIFO<THub>(queueKey, matchType, sendType, permissionType, isDurable, masterClient, masterMessages, messageDbOpr, ref isJoberActive, context);
                            break;
                    }
                    break;
            }

            return queue;
        }
    }
}
