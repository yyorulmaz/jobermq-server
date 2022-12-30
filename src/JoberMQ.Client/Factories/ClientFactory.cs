using JoberMQ.Client.Abstraction;
using JoberMQ.Client.Data;
using JoberMQ.Client.Implementation.Default;
using JoberMQ.Common.Enums.Client;

namespace JoberMQ.Client.Factories
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

        internal static IClientService CreateClientService(IConfigurationClient configurationClient)
        {
            IClientService clientService; ;

            switch (configurationClient.ClientServiceFactory)
            {
                case ClientServiceFactoryEnum.Default:
                    clientService = new DfClientManager(configurationClient, InMemoryClient.ClientDatas, InMemoryClient.ClientGroupDatas);
                    break;
                default:
                    clientService = new DfClientManager(configurationClient, InMemoryClient.ClientDatas, InMemoryClient.ClientGroupDatas);
                    break;
            }
            return clientService;
        }
    }
}
