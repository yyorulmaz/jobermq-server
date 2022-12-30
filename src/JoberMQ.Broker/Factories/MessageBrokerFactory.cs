using JoberMQ.Broker.Abstraction;
using JoberMQ.Client.Abstraction;
using JoberMQ.Common.Enums.Broker;
using JoberMQ.Database.Abstraction.DbService;
using JoberMQ.Distributor.Abstraction;
using JoberMQ.Queue.Abstraction;
using JoberMQ.Server.Implementation.Broker.Default;
using Microsoft.AspNetCore.SignalR;

namespace JoberMQ.Broker.Factories
{
    internal class MessageBrokerFactory
    {
        internal static IMessageBroker Create<THubContext>(
            IConfigurationBroker configurationBroker,
            IConfigurationDistributor configurationDistributor,
            IConfigurationQueue configurationQueue,
            IDatabaseService databaseService,
            IClientService clientService,
            IHubContext<THubContext> context) where THubContext : Hub
        {
            IMessageBroker messageBroker;

            switch (configurationBroker.MessageBrokerFactory)
            {
                case MessageBrokerFactoryEnum.Default:
                    messageBroker = new DfMessageBroker<THubContext>(configurationDistributor, configurationQueue, databaseService, clientService, context);
                    break;
                default:
                    messageBroker = new DfMessageBroker<THubContext>(configurationDistributor, configurationQueue, databaseService, clientService, context);
                    break;
            }

            return messageBroker;
        }
    }
}
