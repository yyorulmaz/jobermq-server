using JoberMQ.Entities.Enums.Client;
using JoberMQ.Entities.Models.Config;
using JoberMQ.Server.Implementation.Client.Default;
using JoberMQNEW.Server.Abstraction.Client;
using JoberMQNEW.Server.Data;

namespace JoberMQ.Server.Factories.Client
{
    internal class ClientFactory
    {
        internal static IClient CreateClient(ClientFactoryEnum clientFactory, ClientTypeEnum clientType, string connectionId, string clientKey, string clientGroupKey)
        {
            IClient client;
            switch (clientFactory)
            {
                case ClientFactoryEnum.Default:
                    client = new DfClient(clientType, connectionId, clientKey, clientGroupKey);
                    break;
                default:
                    client = new DfClient(clientType, connectionId, clientKey, clientGroupKey);
                    break;
            }

            return client;
        }

        internal static IClientGroup CreateClientGroup(ClientGroupFactoryEnum clientGroupFactory, string groupName)
        {
            IClientGroup clientGroup;
            switch (clientGroupFactory)
            {
                case ClientGroupFactoryEnum.Default:
                    clientGroup = new DfClientGroup(groupName);
                    break;
                default:
                    clientGroup = new DfClientGroup(groupName);
                    break;
            }

            return clientGroup;
        }

        internal static IClientService CreateClientService(ServerConfigModel serverConfig)
        {
            IClientService clientService; ;

            switch (serverConfig.ClientServiceFactory)
            {
                case ClientServiceFactoryEnum.Default:
                    clientService = new DfClientManager(serverConfig,InMemoryClient.ClientDatas, InMemoryClient.ClientGroupDatas);
                    break;
                default:
                    clientService = new DfClientManager(serverConfig,InMemoryClient.ClientDatas, InMemoryClient.ClientGroupDatas);
                    break;
            }
            return clientService;
        }
    }
}
