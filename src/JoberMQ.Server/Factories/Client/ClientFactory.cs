using JoberMQ.Entities.Enums.Client;
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


        internal static IClientService CreateClientService(ClientServiceFactoryEnum clientServiceFactory)
        {
            IClientService clientService; ;

            switch (clientServiceFactory)
            {
                case ClientServiceFactoryEnum.Default:
                    clientService = new DfClientManager(InMemoryClient.ClientDatas, InMemoryClient.ClientGroupDatas);
                    break;
                default:
                    clientService = new DfClientManager(InMemoryClient.ClientDatas, InMemoryClient.ClientGroupDatas);
                    break;
            }
            return clientService;
        }
    }
}
