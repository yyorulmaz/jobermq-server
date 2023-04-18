using JoberMQ.Client.Abstraction;
using JoberMQ.Client.Implementation.Default;
using JoberMQ.Library.Enums.Client;
using System.Collections.Concurrent;

namespace JoberMQ.Client.Factories
{
    public class ClientMasterDataFactory
    {
        public static IClientMasterData Create(ClientMasterDataFactoryEnum clientMasterDataFactory, ConcurrentDictionary<string, IClient> masterData)
        {
            IClientMasterData clientMasterData;

            switch (clientMasterDataFactory)
            {
                case ClientMasterDataFactoryEnum.Default:
                    clientMasterData = new DfClientMasterData(masterData);
                    break;
                default:
                    clientMasterData = new DfClientMasterData(masterData);
                    break;
            }

            return clientMasterData;
        }
    }
}
