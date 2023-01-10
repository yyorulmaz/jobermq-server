using JoberMQ.Common.Enums.Client;
using JoberMQ.Configuration.Abstraction;
using JoberMQ.Configuration.Constants;
using JoberMQ.Library.Database.Enums;

namespace JoberMQ.Configuration.Implementation.Default
{
    internal class DfConfigurationClient : IConfigurationClient
    {
        MemFactoryEnum clientMasterFactory = DefaultClientConst.ClientMasterFactory;
        public MemFactoryEnum ClientMasterFactory { get => clientMasterFactory; set => clientMasterFactory = value; }
        MemDataFactoryEnum clientMasterDataFactory = DefaultClientConst.ClientMasterDataFactory;
        public MemDataFactoryEnum ClientMasterDataFactory { get => clientMasterDataFactory; set => clientMasterDataFactory = value; }


        ClientFactoryEnum clientFactory = DefaultClientConst.ClientFactory;
        public ClientFactoryEnum ClientFactory { get => clientFactory; set => clientFactory = value; }
    }
}
