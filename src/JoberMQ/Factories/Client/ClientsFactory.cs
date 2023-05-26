using JoberMQ.Abstraction.Client;
using JoberMQ.Implementation.Client.Default;
using JoberMQ.Common.Enums.Client;

namespace JoberMQ.Factories.Client
{
    internal class ClientsFactory
    {
        internal static IClients Create(ClientsFactoryEnum clientsFactory)
        {
            IClients result;

            switch (clientsFactory)
            {
                case ClientsFactoryEnum.Default:
                    result = new DefaultClients();
                    break;
                default:
                    throw new System.Exception("none");
            }

            return result;
        }
    }
}
