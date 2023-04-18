using JoberMQ.Configuration.Abstraction;
using JoberMQ.Configuration.Constants;
using JoberMQ.Library.Database.Enums;
using JoberMQ.Library.Enums.Client;

namespace JoberMQ.Configuration.Implementation.Default
{
    internal class DfConfigurationClient : IConfigurationClient
    {
        ClientMasterDataFactoryEnum clientMasterDataFactory = DefaultClientConst.ClientMasterDataFactory;
        public ClientMasterDataFactoryEnum ClientMasterDataFactory { get => clientMasterDataFactory; set => clientMasterDataFactory = value; }

        ClientChildDataFactoryEnum clientChildDataFactory = DefaultClientConst.ClientChildDataFactory;
        public ClientChildDataFactoryEnum ClientChildDataFactory { get => clientChildDataFactory; set => clientChildDataFactory = value; }

        ClientFactoryEnum clientFactory = DefaultClientConst.ClientFactory;
        public ClientFactoryEnum ClientFactory { get => clientFactory; set => clientFactory = value; }
    }
}
