﻿using JoberMQ.Broker.Abstraction;
using JoberMQ.Broker.Implementation.Default;
using JoberMQ.Client.Abstraction;
using JoberMQ.Configuration.Abstraction;
using JoberMQ.Database.Abstraction;
using JoberMQ.Common.Database.Repository.Abstraction.Mem;
using JoberMQ.Common.Dbos;
using JoberMQ.Common.Enums.Broker;
using JoberMQ.Common.StatusCode.Abstraction;
using Microsoft.AspNetCore.SignalR;
using System;

namespace JoberMQ.Broker.Factories
{
    internal class MessageBrokerFactory
    {
        internal static IMessageBroker Create<THub>(
            IConfiguration configuration,
            IStatusCode statusCode,
            IMemRepository<Guid, MessageDbo> messageMaster,
            IClientMasterData clientMasterData,
            IDatabase database,
            ref IHubContext<THub> hubContext) where THub : Hub
        {
            IMessageBroker messageBroker;

            switch (configuration.ConfigurationBroker.MessageBrokerFactory)
            {
                case MessageBrokerFactoryEnum.Default:
                    messageBroker = new DfMessageBroker<THub>(configuration, statusCode, messageMaster, clientMasterData, database, hubContext);
                    break;
                default:
                    messageBroker = new DfMessageBroker<THub>(configuration, statusCode, messageMaster, clientMasterData, database, hubContext);
                    break;
            }

            return messageBroker;
        }
    }
}
