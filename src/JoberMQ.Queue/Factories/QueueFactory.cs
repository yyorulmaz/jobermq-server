﻿using JoberMQ.Client.Abstraction;
using JoberMQ.Configuration.Abstraction;
using JoberMQ.Database.Abstraction;
using JoberMQ.Common.Database.Repository.Abstraction.Mem;
using JoberMQ.Common.Database.Repository.Abstraction.Opr;
using JoberMQ.Common.Dbos;
using JoberMQ.Common.Enums.Permission;
using JoberMQ.Common.Enums.Queue;
using JoberMQ.Queue.Abstraction;
using JoberMQ.Queue.Implementation.Default;
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
            QueueMatchTypeEnum matchType,
            QueueOrderOfSendingTypeEnum queueOrderOfSendingType,
            PermissionTypeEnum permissionType,
            bool isDurable,
            IClientMasterData clientMasterData,
            IMemRepository<Guid, MessageDbo> masterMessages,
            IOprRepositoryGuid<MessageDbo> messageDbOpr,
            ref IHubContext<THub> context) where THub : Hub
        {
            IMessageQueue queue;

            switch (configuration.ConfigurationQueue.QueueFactory)
            {
                case QueueFactoryEnum.Default:
                    switch (queueOrderOfSendingType)
                    {
                        case QueueOrderOfSendingTypeEnum.Priority:
                            queue = new DfMessageQueuePriority<THub>(configuration, database, queueKey, matchType, queueOrderOfSendingType, permissionType, isDurable, clientMasterData, masterMessages, messageDbOpr, ref context);
                            break;
                        case QueueOrderOfSendingTypeEnum.FIFO:
                            queue = new DfMessageQueueFIFO<THub>(configuration, database, queueKey, matchType, queueOrderOfSendingType, permissionType, isDurable, clientMasterData, masterMessages, messageDbOpr, ref context);
                            break;
                        case QueueOrderOfSendingTypeEnum.LIFO:
                            queue = new DfMessageQueueLIFO<THub>(configuration, database, queueKey, matchType, queueOrderOfSendingType, permissionType, isDurable, clientMasterData, masterMessages, messageDbOpr, ref context);
                            break;
                        default:
                            queue = new DfMessageQueueFIFO<THub>(configuration, database, queueKey, matchType, queueOrderOfSendingType, permissionType, isDurable, clientMasterData, masterMessages, messageDbOpr, ref context);
                            break;
                    }
                    break;
                default:
                    switch (queueOrderOfSendingType)
                    {
                        case QueueOrderOfSendingTypeEnum.Priority:
                            queue = new DfMessageQueuePriority<THub>(configuration, database, queueKey, matchType, queueOrderOfSendingType, permissionType, isDurable, clientMasterData, masterMessages, messageDbOpr, ref context);
                            break;
                        case QueueOrderOfSendingTypeEnum.FIFO:
                            queue = new DfMessageQueueFIFO<THub>(configuration, database, queueKey, matchType, queueOrderOfSendingType, permissionType, isDurable, clientMasterData, masterMessages, messageDbOpr, ref context);
                            break;
                        case QueueOrderOfSendingTypeEnum.LIFO:
                            queue = new DfMessageQueueLIFO<THub>(configuration, database, queueKey, matchType, queueOrderOfSendingType, permissionType, isDurable, clientMasterData, masterMessages, messageDbOpr, ref context);
                            break;
                        default:
                            queue = new DfMessageQueueFIFO<THub>(configuration, database, queueKey, matchType, queueOrderOfSendingType, permissionType, isDurable, clientMasterData, masterMessages, messageDbOpr, ref context);
                            break;
                    }
                    break;
            }

            return queue;
        }
    }
}
