using JoberMQ.Client.Abstraction;
using JoberMQ.Client.Constants;
using JoberMQ.Common.Enums.Client;

namespace JoberMQ.Client.Implementation.Default
{
    internal class DfConfigurationClient : IConfigurationClient
    {
        ClientFactoryEnum clientFactory = DefaultClientConst.ClientFactory;
        public ClientFactoryEnum ClientFactory { get => clientFactory; set => clientFactory = value; }

        ClientGroupFactoryEnum clientGroupFactory = DefaultClientConst.ClientGroupFactory;
        public ClientGroupFactoryEnum ClientGroupFactory { get => clientGroupFactory; set => clientGroupFactory = value; }
        ClientServiceFactoryEnum clientServiceFactory = DefaultClientConst.ClientServiceFactory;
        public ClientServiceFactoryEnum ClientServiceFactory { get => clientServiceFactory; set => clientServiceFactory = value; }
    }
}
