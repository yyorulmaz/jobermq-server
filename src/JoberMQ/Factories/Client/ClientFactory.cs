using JoberMQ.Abstraction.Client;
using JoberMQ.Implementation.Client.Default;
using JoberMQ.Common.Enums.Client;

namespace JoberMQ.Factories.Client
{
    internal class ClientFactory
    {
        internal static IClient Create(ClientFactoryEnum clientFactory, string connectionId, string clientKey, ClientTypeEnum clientType)
        {
            IClient result;

            switch (clientFactory)
            {
                case ClientFactoryEnum.Default:
                    result = new DefaultClient(connectionId, clientKey, clientType);
                    break;
                default:
                    throw new System.Exception("none");
            }

            return result;
        }
    }
}
