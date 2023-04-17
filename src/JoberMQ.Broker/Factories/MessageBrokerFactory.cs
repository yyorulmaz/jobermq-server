using JoberMQ.Broker.Abstraction;
using JoberMQ.Client.Abstraction;
using JoberMQ.Common.Dbos;
using JoberMQ.Common.Enums.Broker;
using JoberMQ.Configuration.Abstraction;
using JoberMQ.Database.Abstraction.DbService;
using JoberMQ.Library.Database.Repository.Abstraction.Mem;
using JoberMQ.Library.StatusCode.Abstraction;
using JoberMQ.Server.Implementation.Broker.Default;
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
            IMemRepository<string, IClient> clientMaster,
            IDatabase database,
            ref IHubContext<THub> hubContext,
            ref bool isJoberActive) where THub : Hub
        {
            IMessageBroker messageBroker;

            switch (configuration.ConfigurationBroker.MessageBrokerFactory)
            {
                case MessageBrokerFactoryEnum.Default:
                    messageBroker = new DfMessageBroker<THub>(configuration, statusCode, messageMaster, clientMaster, database, hubContext, ref isJoberActive);
                    break;
                default:
                    messageBroker = new DfMessageBroker<THub>(configuration, statusCode, messageMaster, clientMaster, database, hubContext, ref isJoberActive);
                    break;
            }

            return messageBroker;
        }
    }
}
