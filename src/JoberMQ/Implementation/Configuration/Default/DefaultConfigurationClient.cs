using JoberMQ.Abstraction.Configuration;
using JoberMQ.Constants;
using JoberMQ.Common.Enums.Client;

namespace JoberMQ.Implementation.Configuration.Default
{
    public class DefaultConfigurationClient : IConfigurationClient
    {
        ClientsFactoryEnum clientsFactory = ConfigurationClientConst.ClientsFactory;
        public ClientsFactoryEnum ClientsFactory { get => clientsFactory; set => clientsFactory = value; }
        ClientFactoryEnum clientFactory = ConfigurationClientConst.ClientFactory;
        public ClientFactoryEnum ClientFactory { get => clientFactory; set => clientFactory = value; }
    }
}
