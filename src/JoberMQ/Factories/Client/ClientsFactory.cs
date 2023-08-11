using JoberMQ.Abstraction.Client;
using JoberMQ.Implementation.Client.Default;
using JoberMQ.Common.Enums.Client;
using System.Collections.Concurrent;

namespace JoberMQ.Factories.Client
{
    internal class ClientsFactory
    {
        //internal static IClients Create(ClientsFactoryEnum clientsFactory)
        internal static IClients Create(ClientsFactoryEnum clientsFactory, ConcurrentDictionary<string, IClient> masterData)
        {
            IClients result;

            switch (clientsFactory)
            {
                case ClientsFactoryEnum.Default:
                    //result = new DefaultClients();
                    result = new DefaultClients(masterData);
                    break;
                default:
                    throw new System.Exception("none");
            }

            return result;
        }
    }
}
