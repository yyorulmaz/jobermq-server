using JoberMQ.Client.Abstraction;
using JoberMQ.Client.Implementation.Default;
using JoberMQ.Common.Enums.Client;

namespace JoberMQ.Client.Factories
{
    internal class ClientFactory
    {
        internal static IClient CreateClient(ClientFactoryEnum clientFactory, string connectionId, string clientKey, ClientTypeEnum clientType)
        {
            IClient client;
            switch (clientFactory)
            {
                case ClientFactoryEnum.Default:
                    client = new DfClient(connectionId, clientKey, clientType);
                    break;
                default:
                    client = new DfClient(connectionId, clientKey, clientType);
                    break;
            }

            return client;
        }
    }
}
