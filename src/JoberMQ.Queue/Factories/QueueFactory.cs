using JoberMQ.Client.Abstraction;
using JoberMQ.Configuration.Abstraction;
using JoberMQ.Database.Abstraction;
using JoberMQ.Library.Database.Repository.Abstraction.Mem;
using JoberMQ.Library.Database.Repository.Abstraction.Opr;
using JoberMQ.Library.Dbos;
using JoberMQ.Library.Enums.Permission;
using JoberMQ.Library.Enums.Queue;
using JoberMQ.Queue.Abstraction;
using JoberMQ.Queue.Implementation.Default;
using JoberMQ.State.Abstraction;
using Microsoft.AspNetCore.SignalR;
using System;

namespace JoberMQ.Queue.Factories
{
    internal class QueueFactory
    {
        internal static IMessageQueue Create<THub>(
            IConfiguration configuration,
            IDatabase database,
            string queueKey,
            MatchTypeEnum matchType,
            SendTypeEnum sendType,
            PermissionTypeEnum permissionType,
            bool isDurable,
            IClientMasterData clientMasterData,
            IMemRepository<Guid, MessageDbo> masterMessages,
            IOprRepositoryGuid<MessageDbo> messageDbOpr,
            ref IHubContext<THub> context,
            ref IJoberState joberState) where THub : Hub
        {
            IMessageQueue queue;

            switch (configuration.ConfigurationQueue.QueueFactory)
            {
                case QueueFactoryEnum.Default:
                    switch (sendType)
                    {
                        case SendTypeEnum.Priority:
                            queue = new DfMessageQueuePriority<THub>(configuration, database, queueKey, matchType, sendType, permissionType, isDurable, clientMasterData, masterMessages, messageDbOpr, ref joberState, ref context);
                            break;
                        case SendTypeEnum.FIFO:
                            queue = new DfMessageQueueFIFO<THub>(configuration, database, queueKey, matchType, sendType, permissionType, isDurable, clientMasterData, masterMessages, messageDbOpr, ref joberState, ref context);
                            break;
                        case SendTypeEnum.LIFO:
                            queue = new DfMessageQueueLIFO<THub>(configuration, database, queueKey, matchType, sendType, permissionType, isDurable, clientMasterData, masterMessages, messageDbOpr, ref joberState, ref context);
                            break;
                        default:
                            queue = new DfMessageQueueFIFO<THub>(configuration, database, queueKey, matchType, sendType, permissionType, isDurable, clientMasterData, masterMessages, messageDbOpr, ref joberState, ref context);
                            break;
                    }
                    break;
                default:
                    switch (sendType)
                    {
                        case SendTypeEnum.Priority:
                            queue = new DfMessageQueuePriority<THub>(configuration, database, queueKey, matchType, sendType, permissionType, isDurable, clientMasterData, masterMessages, messageDbOpr,  ref joberState, ref context);
                            break;
                        case SendTypeEnum.FIFO:
                            queue = new DfMessageQueueFIFO<THub>(configuration, database, queueKey, matchType, sendType, permissionType, isDurable, clientMasterData, masterMessages, messageDbOpr,  ref joberState, ref context);
                            break;
                        case SendTypeEnum.LIFO:
                            queue = new DfMessageQueueLIFO<THub>(configuration, database, queueKey, matchType, sendType, permissionType, isDurable, clientMasterData, masterMessages, messageDbOpr,  ref joberState, ref context);
                            break;
                        default:
                            queue = new DfMessageQueueFIFO<THub>(configuration, database, queueKey, matchType, sendType, permissionType, isDurable, clientMasterData, masterMessages, messageDbOpr,  ref joberState, ref context);
                            break;
                    }
                    break;
            }

            return queue;
        }
    }
}
